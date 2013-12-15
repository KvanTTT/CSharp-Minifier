using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.CSharp.Resolver;
using ICSharpCode.NRefactory.CSharp.TypeSystem;
using ICSharpCode.NRefactory.Semantics;
using ICSharpCode.NRefactory.TypeSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public class Minifier
	{
		private static string[] NameKeys = new string[] { "Name", "LiteralValue", "Keyword" };

		private CSharpUnresolvedFile _unresolvedFile;
		private IProjectContent _projectContent;
		private ICompilation _compilation;
		private CSharpAstResolver _resolver;

		public SyntaxTree SyntaxTree
		{
			get;
			private set;
		}

		public MinifierOptions Options
		{
			get;
			private set;
		}

		public Minifier(MinifierOptions options)
		{
			Options = options;

			if (Options.IdentifiersCompressing)
			{
				_projectContent = new CSharpProjectContent();
				var assemblies = new List<Assembly>
			{
				typeof(object).Assembly, // mscorlib
				typeof(Uri).Assembly, // System.dll
				typeof(Enumerable).Assembly, // System.Core.dll
			};

				var unresolvedAssemblies = new IUnresolvedAssembly[assemblies.Count];
				Parallel.For(
					0, assemblies.Count,
					delegate(int i)
					{
						var loader = new CecilLoader();
						var path = assemblies[i].Location;
						unresolvedAssemblies[i] = loader.LoadAssemblyFile(assemblies[i].Location);
					});
				_projectContent = _projectContent.AddAssemblyReferences((IEnumerable<IUnresolvedAssembly>)unresolvedAssemblies);
			}
		}

		public string MinifyFiles(string[] csFiles)
		{
			CSharpParser parser = new CSharpParser();
			SyntaxTree[] trees = csFiles.Select(file => parser.Parse(file, file + "_temp.cs")).ToArray();

			SyntaxTree globalTree = new SyntaxTree();
			globalTree.FileName = "temp.cs";

			var usings = new List<UsingDeclaration>();
			var types = new List<TypeDeclaration>();
			foreach (var tree in trees)
			{
				List<UsingDeclaration> treeUsings = new List<UsingDeclaration>();
				GetUsingsAndRemoveUsingsAndNamespaces(tree, treeUsings, types);
				usings.AddRange(treeUsings.Where(u1 => !usings.Exists(u2 => u2.Namespace == u1.Namespace)));
			}

			foreach (var u in usings)
				globalTree.AddChild(u.Clone(), new Role<AstNode>("UsingDeclaration"));
			foreach (var t in types)
				globalTree.AddChild(t.Clone(), new Role<AstNode>("TypeDeclaration"));

			SyntaxTree = globalTree;

			return Minify();
		}

		public string MinifyFromString(string CSharpCode)
		{
			SyntaxTree = new CSharpParser().Parse(CSharpCode, "temp.cs");

			return Minify();
		}

		public string Minify()
		{
			if (Options.CommentsRemoving || Options.RegionsRemoving)
				RemoveCommentsAndRegions();

			if (Options.IdentifiersCompressing)
			{
				_unresolvedFile = SyntaxTree.ToTypeSystem();
				_projectContent = _projectContent.AddOrUpdateFiles(_unresolvedFile);
				_compilation = _projectContent.CreateCompilation();
				_resolver = new CSharpAstResolver(_compilation, SyntaxTree, _unresolvedFile);
				CompressIdentifiers();
			}

			if (Options.MiscCompressing)
				CompressMisc();

			string result;
			if (Options.SpacesRemoving)
				result = SyntaxTreeToStringWithoutSpaces(Options.LineLength);
			else
				result = SyntaxTree.GetText();

			return result;
		}

		private void GetUsingsAndRemoveUsingsAndNamespaces(SyntaxTree tree, List<UsingDeclaration> usings, List<TypeDeclaration> types)
		{
			foreach (var child in tree.Children)
				GetUsingsAndRemoveUsingsAndNamespaces(child, usings, types);
		}

		private void GetUsingsAndRemoveUsingsAndNamespaces(AstNode node, List<UsingDeclaration> usings, List<TypeDeclaration> types)
		{
			if (node.Role.ToString() == "Member" && node.GetType().Name == "UsingDeclaration")
			{
				usings.Add((UsingDeclaration)node);
			}
			else if (node.Role.ToString() == "Member" && node.GetType().Name == "NamespaceDeclaration")
			{
				var parent = node.Parent;
				foreach (var child in node.Children)
				{
					if (child.NodeType == NodeType.TypeDeclaration)
					{
						types.Add((TypeDeclaration)child);
					}
					else if (child.ToString() == "Member" && child.GetType().Name == "NamespaceDeclaration")
					{
						GetUsingsAndRemoveUsingsAndNamespaces(child, usings, types);
					}
				}
			}
			else
			{
				if (node.Children.Count() >= 1)
				{
					foreach (var child in node.Children)
						GetUsingsAndRemoveUsingsAndNamespaces(child, usings, types);
				}
			}
		}

		#region Misc Compression

		private void CompressMisc()
		{
			foreach (var children in SyntaxTree.Children)
				CompressMisc(children);
		}

		private void CompressMisc(AstNode node)
		{
			foreach (var children in node.Children)
			{
				if (children is PrimitiveExpression)
				{
					var primitiveExpression = ((PrimitiveExpression)children);
					var str = primitiveExpression.Value.ToString();
					long number;
					if (long.TryParse(str, out number))
					{
						string hex = "0x" + number.ToString("X");
						primitiveExpression.LiteralValue = str.Length < hex.Length ? str : hex;
					}
				}
				else if (children is CSharpModifierToken)
				{
					var modifier = ((CSharpModifierToken)children).Modifier;
					if ((modifier & Modifiers.Private) == Modifiers.Private && (modifier & ~Modifiers.Private) == 0)
						children.Remove();
					else
						modifier &= ~Modifiers.Private;
				}
				else
				{
					if (children is BlockStatement)
					{
						if (children.Children.Count() == 3)
							children.ReplaceWith(children.Children.ElementAt(1));
					}
					CompressMisc(children);
				}
			}
		}

		#endregion

		#region Comments Removing

		private void RemoveCommentsAndRegions()
		{
			foreach (var children in SyntaxTree.Children)
				RemoveCommentsAndRegions(children);
		}

		private void RemoveCommentsAndRegions(AstNode node)
		{
			foreach (var children in node.Children)
			{
				if (Options.CommentsRemoving && children is Comment)
				{
					var commentType = children.GetPropertyValueEnum<CommentType>("CommentType");
					if (commentType != CommentType.InactiveCode)
						children.Remove();
				}
				else if (Options.RegionsRemoving && children is PreProcessorDirective)
				{
					var type = children.GetPropertyValueEnum<PreProcessorDirectiveType>("Type");
					switch (type)
					{
						case PreProcessorDirectiveType.Region:
						case PreProcessorDirectiveType.Endregion:
						case PreProcessorDirectiveType.Warning:
							children.Remove();
							break;
					}
				}
				else
					RemoveCommentsAndRegions(children);
			}
		}

		#endregion

		#region Identifiers Compressing

		private void CompressIdentifiers()
		{
			var visitor = new MinifyLocalsAstVisitor();
			SyntaxTree.AcceptVisitor(visitor);

			var idGenerator = new MinIdGenerator();
			var substitutor = new Substitutor(idGenerator);
			var newSubstituton = substitutor.Generate(visitor.MethodsVars, visitor.AllIdNames);

			foreach (var method in visitor.MethodsVars)
			{
				var method2 = newSubstituton[method.Key];
				foreach (LocalVarDec v in method.Value)
				{
					RenameLocal(v.Node, method2[v.Name]);
				}
			}
		}

		private void RenameLocal(AstNode node, string newName)
		{
			LocalResolveResult lrr = _resolver.Resolve(node) as LocalResolveResult;

			if (lrr != null)
			{
				FindReferences fr = new FindReferences();
				FoundReferenceCallback callback = delegate(AstNode matchNode, ResolveResult result)
				{
					if (matchNode is ParameterDeclaration)
						((ParameterDeclaration)matchNode).Name = newName;
					else if (matchNode is VariableInitializer)
						((VariableInitializer)matchNode).Name = newName;
					else if (matchNode is IdentifierExpression)
						((IdentifierExpression)matchNode).Identifier = newName;
				};
				fr.FindLocalReferences(lrr.Variable, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
			}
		}

		#endregion

		#region Removing of spaces and line breaks

		AstNode _prevNode;
		StringBuilder _line;

		private string SyntaxTreeToStringWithoutSpaces(int lineLength)
		{
			StringBuilder result = new StringBuilder();
			_line = new StringBuilder(Options.LineLength);

			_prevNode = null;
			foreach (var children in SyntaxTree.Children)
				TraverseChilds(children, result);
			result.Append(_line);

			return result.ToString();
		}

		private void TraverseChilds(AstNode node, StringBuilder stringBuilder)
		{
			if (node.Children.Count() == 0)
			{
				string beginSymbols = " ";
				char last = (char)0;
				if (_line.Length != 0)
					last = _line[_line.Length - 1];

				if (last == ' ' || last == '\r' || last == '\n' || _prevNode == null || node == null)
					beginSymbols = "";
				else
				{
					var prevComment = _prevNode as Comment;
					if (prevComment != null)
					{
						if (prevComment.CommentType == CommentType.SingleLine || prevComment.CommentType == CommentType.Documentation)
							beginSymbols = Environment.NewLine;
						else
							beginSymbols = "";
					}
					else if (node is PreProcessorDirective || _prevNode is PreProcessorDirective)
						beginSymbols = Environment.NewLine;
					else
					{
						if ((_prevNode is CSharpTokenNode && _prevNode.Role.ToString().All(c => !char.IsLetterOrDigit(c))) ||
							(node is CSharpTokenNode && node.Role.ToString().All(c => !char.IsLetterOrDigit(c))) ||
							node is Comment)
								beginSymbols = "";
					}
				}

				string newString = beginSymbols + GetLeafNodeString(node);
				if (Options.LineLength == 0)
					stringBuilder.Append(newString);
				else
				{
					if (_line.Length + newString.Length > Options.LineLength)
					{
						stringBuilder.AppendLine(_line.ToString());
						_line.Clear();
						_line.Append(newString.TrimStart());
					}
					else
					{
						_line.Append(newString);
					}
				}
			}
			else
			{
				foreach (AstNode child in node.Children)
				{
					TraverseChilds(child, stringBuilder);
					if (child.Children.Count() <= 1)
						_prevNode = child;
				}
			}
		}

		public static string GetLeafNodeString(AstNode node)
		{
			string nodeRole = node.Role.ToString();
			var properties = node.GetProperties();
			if (nodeRole == "Comment")
			{
				CommentType commentType = properties.GetPropertyValueEnum<CommentType>(node, "CommentType");
				string content = properties.GetPropertyValue(node, "Content");
				switch (commentType)
				{
					default:
					case CommentType.SingleLine:
						return "//" + content;
					case CommentType.Documentation:
						return "///" + content;
					case CommentType.MultiLine:
						return "/*" + content + "*/";
					case CommentType.InactiveCode:
						return content;
					case CommentType.MultiLineDocumentation:
						return "/**" + content + "*/";
				}
			}
			else if (nodeRole == "Modifier")
			{
				return properties.GetPropertyValue(node, "Modifier").ToLower();
			}
			else if (nodeRole == "Target" || nodeRole == "Right")
			{
				string typeName = node.GetType().Name;
				if (typeName == "ThisReferenceExpression")
					return "this";
				else if (typeName == "NullReferenceExpression")
					return "null";
			}
			else if (nodeRole == "PreProcessorDirective")
			{
				var type = properties.GetPropertyValue(node, "Type").ToLower();
				var argument = properties.GetPropertyValue(node, "Argument");
				var result = "#" + type;
				if (argument != string.Empty)
					result += " " + argument;
				return result;
			}

			if (node is CSharpTokenNode || node is CSharpModifierToken)
				return nodeRole;

			return properties
				.Where(p => NameKeys.Contains(p.Name)).FirstOrDefault().GetValue(node, null).ToString();
		}

		#endregion
	}
}
