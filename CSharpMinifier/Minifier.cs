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

		private SyntaxTree _syntaxTree;
		private CSharpUnresolvedFile _unresolvedFile;
		private IProjectContent _projectContent;
		private ICompilation _compilation;
		private CSharpAstResolver _resolver;

		public bool IdentifiersCompressing
		{
			get;
			private set;
		}

		public bool SpacesRemoving
		{
			get;
			private set;
		}

		public bool CommentsRemoving
		{
			get;
			private set;
		}

		public int LineLength
		{
			get;
			private set;
		}

		public Minifier(bool compressIdentifiers = true, bool removeSpaces = true, bool removeComments = true,
			int lineLength = 0)
		{
			IdentifiersCompressing = compressIdentifiers;
			SpacesRemoving = removeSpaces;
			CommentsRemoving = removeComments;
			LineLength = lineLength;

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

			_syntaxTree = globalTree;

			return Minify();
		}

		public string MinifyFromString(string CSharpCode)
		{
			_syntaxTree = new CSharpParser().Parse(CSharpCode, "temp.cs");

			return Minify();
		}

		public string Minify()
		{
			if (CommentsRemoving)
				RemoveComments();

			_unresolvedFile = _syntaxTree.ToTypeSystem();
			_projectContent = _projectContent.AddOrUpdateFiles(_unresolvedFile);
			_compilation = _projectContent.CreateCompilation();
			_resolver = new CSharpAstResolver(_compilation, _syntaxTree, _unresolvedFile);

			if (IdentifiersCompressing)
				CompressIdentifiers();

			string result;
			if (SpacesRemoving)
				result = SyntaxTreeToStringWithoutSpaces(LineLength);
			else
				result = _syntaxTree.GetText();

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

		#region Comments removing

		public void RemoveComments()
		{
			foreach (var children in _syntaxTree.Children)
			{
				if (children.Role.ToString() == "Comment")
				{
					CommentType commentType = (CommentType)Enum.Parse(typeof(CommentType),
						NRefactoryUtils.GetPropertyValue(children, "CommentType"));
					if (commentType != CommentType.InactiveCode)
						children.Remove();
				}
			}
		}

		#endregion

		#region Compress Identifiers

		public void CompressIdentifiers()
		{
			var visitor = new MinifyLocalsAstVisitor();
			_syntaxTree.AcceptVisitor(visitor);

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

		public void RenameLocal(AstNode node, string newName)
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
				fr.FindLocalReferences(lrr.Variable, _unresolvedFile, _syntaxTree, _compilation, callback, CancellationToken.None);
			}
		}

		#endregion

		#region Removing of spaces and line breaks

		AstNode _prevNode;
		StringBuilder _line;

		public string SyntaxTreeToStringWithoutSpaces(int lineLength)
		{
			StringBuilder result = new StringBuilder();
			_line = new StringBuilder(LineLength);

			_prevNode = null;
			foreach (var children in _syntaxTree.Children)
				TraverseChilds(children, result);
			result.Append(_line);

			return result.ToString();
		}

		private void TraverseChilds(AstNode node, StringBuilder stringBuilder)
		{
			if (node.Children.Count() == 0)
			{
				bool insertSpace = true;
				char last = (char)0;
				if (_line.Length != 0)
					last = _line[_line.Length - 1];
				if (last == ' ' || last == '\r' || last == '\n' || _prevNode == null || node == null)
					insertSpace = false;
				else
				{
					if ((_prevNode is CSharpTokenNode && _prevNode.Role.ToString().All(c => !char.IsLetterOrDigit(c))) ||
						(node is CSharpTokenNode && node.Role.ToString().All(c => !char.IsLetterOrDigit(c))))
						insertSpace = false;
				}

				string newString = (insertSpace ? " " : "") + GetLeafNodeString(node);
				if (LineLength == 0)
					stringBuilder.Append(newString);
				else
				{
					if (_line.Length + newString.Length > LineLength)
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
			var properties = NRefactoryUtils.GetProperties(node);
			if (nodeRole == "Comment")
			{
				string commentTypeString = properties
					.Where(p => p.Name == "CommentType").FirstOrDefault().GetValue(node, null).ToString();
				CommentType commentType = (CommentType)Enum.Parse(typeof(CommentType), commentTypeString);

				string content = properties.Where(p => p.Name == "Content")
					.FirstOrDefault().GetValue(node, null).ToString();

				switch (commentType)
				{
					default:
					case CommentType.SingleLine:
						return "//" + content + Environment.NewLine;
					case CommentType.Documentation:
						return "///" + content + Environment.NewLine;
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
				return properties
					.Where(p => p.Name == "Modifier").FirstOrDefault().GetValue(node, null).ToString().ToLower();
			}
			else if (nodeRole == "Target" || nodeRole == "Right")
			{
				string typeName = node.GetType().Name;
				if (typeName == "ThisReferenceExpression")
					return "this";
				else if (typeName == "NullReferenceExpression")
					return "null";
			}

			if (node is CSharpTokenNode || node is CSharpModifierToken)
				return node.Role.ToString();

			return properties
				.Where(p => NameKeys.Contains(p.Name)).FirstOrDefault().GetValue(node, null).ToString();
		}

		#endregion
	}
}
