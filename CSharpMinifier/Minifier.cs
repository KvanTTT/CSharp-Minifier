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
		private const string VarId = "var";
		public static string ParserTempFileName = "temp.cs";

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

		public List<string> IgnoredIdentifiers
		{
			get;
			private set;
		}

		public List<string> IgnoredComments
		{
			get;
			private set;
		}

		#region Public

		public Minifier(MinifierOptions options = null, string[] ignoredIdentifiers = null, string[] ignoredComments = null)
		{
			Options = options ?? new MinifierOptions();

			//if (Options.LocalVarsCompressing || Options.MembersCompressing || Options.TypesCompressing)
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

			IgnoredIdentifiers = ignoredIdentifiers == null ? new List<string>() : ignoredIdentifiers.ToList();
			IgnoredComments = new List<string>();
			if (ignoredComments != null)
				foreach (var comment in ignoredComments)
				{
					var str = comment;
					if (str.StartsWith("//"))
						str = str.Substring("//".Length);
					else if (str.StartsWith("/*") && str.EndsWith("*/"))
						str = str.Substring("/*".Length, str.Length - "/*".Length - "*/".Length);
					if (!IgnoredComments.Contains(str))
						IgnoredComments.Add(str);
				}
		}

		public string MinifyFiles(string[] csFiles)
		{
			CSharpParser parser = new CSharpParser();
			SyntaxTree[] trees = csFiles.Select(file => parser.Parse(file, file + "_" + ParserTempFileName)).ToArray();

			SyntaxTree globalTree = new SyntaxTree();
			globalTree.FileName = ParserTempFileName;

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

		public string MinifyFromString(string csharpCode)
		{
			SyntaxTree = new CSharpParser().Parse(csharpCode, ParserTempFileName);

			return Minify();
		}

		public string Minify()
		{
			if (Options.CommentsRemoving || Options.RegionsRemoving)
				RemoveCommentsAndRegions();

			if (Options.LocalVarsCompressing)
				CompressLocals();
			if (Options.MembersCompressing)
				CompressMembers();
			if (Options.TypesCompressing)
				CompressTypes();

			if (Options.MiscCompressing || Options.RemoveNamespaces)
				CompressHelper();

			string result;
			if (Options.SpacesRemoving)
			{
				if (Options.MiscCompressing || Options.LocalVarsCompressing || Options.MembersCompressing || Options.TypesCompressing)
				{
					// TODO: Fix it.
					SyntaxTree = new CSharpParser().Parse(SyntaxTree.GetText(), ParserTempFileName);
				}
				
				result = ToStringWithoutSpaces();
			}
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

		#endregion

		#region Misc Compression

		private void CompressHelper()
		{
			CompileAndResolve();
			CompressHelper(SyntaxTree);
		}

		private void CompressHelper(AstNode node)
		{
			foreach (var children in node.Children)
			{
				if (Options.MiscCompressing && children is PrimitiveExpression)
				{
					var primitiveExpression = (PrimitiveExpression)children;
					if (IsIntegerNumber(primitiveExpression.Value))
					{
						var str = primitiveExpression.Value.ToString();
						long number;
						if (long.TryParse(str, out number))
						{
							string hex = "0x" + number.ToString("X");
							primitiveExpression.LiteralValue = str.Length < hex.Length ? str : hex;
						}
					}
				}
				else if (Options.MiscCompressing && children is CSharpModifierToken)
				{
					// private int a => int a
					var modifier = ((CSharpModifierToken)children).Modifier;
					if (modifier.HasFlag(Modifiers.Private) && (modifier & ~Modifiers.Private) == 0)
						children.Remove();
					else
						modifier &= ~Modifiers.Private;
				}
				else if (Options.RemoveNamespaces && children is NamespaceDeclaration)
				{
					var childrenCount = children.Children.Count();
					children.Children.ElementAt(childrenCount - 1).Remove();
					children.Children.ElementAt(2).Remove();
					children.Children.ElementAt(1).Remove();
					children.Children.ElementAt(0).Remove();
					var namespaceChildrens = children.Children;

					var parent = children.Parent;
					foreach (var c in parent.Children)
					{
						if (c == children)
						{
							foreach (var nsChildren in namespaceChildrens)
								parent.InsertChildAfter(c, nsChildren.Clone(), new Role<AstNode>(nsChildren.Role.ToString()));
							c.Remove();
							break;
						}
					}
					foreach (var c in parent.Children)
						CompressHelper(c);
				}
				else if (Options.MiscCompressing && children is VariableDeclarationStatement)
				{
					// List<byte> a = new List<byte>() => var a = new List<byte>()
					// var a = new b() => b a = new b()
					var varDecExpr = (VariableDeclarationStatement)children;
					if (!varDecExpr.Modifiers.HasFlag(Modifiers.Const))
					{
						var type = varDecExpr.Type.ToString().Replace(" ", "");
						if (type == VarId)
						{
							// Resolving expression type.
							CompileAndResolve();
							var expectedType = _resolver.GetExpectedType(varDecExpr.Variables.Single().Initializer);
							if (expectedType.Namespace != "System.Collections.Generic")
							{
								string typeStr = expectedType.Name;
								bool replace = NamesGenerator.CSharpTypeSynonyms.TryGetValue(typeStr, out typeStr);
								if (!replace)
									typeStr = expectedType.Name;
								if (typeStr.Length <= VarId.Length)
									replace = true;
								else
									replace = false;
								if (replace)
								{
									if (expectedType.Namespace == "System")
										varDecExpr.Type = new PrimitiveType(typeStr);
									else
										varDecExpr.Type = new SimpleType(typeStr);
								}
							}
						}
						else
						{
							if (varDecExpr.Variables.Count == 1)
							{
								string typeStr;
								var typeStrWithoutNamespaces = varDecExpr.Type.ToString();
								typeStrWithoutNamespaces = typeStrWithoutNamespaces.Substring(typeStrWithoutNamespaces.LastIndexOf('.') + 1);
								NamesGenerator.CSharpTypeSynonyms.TryGetValue(typeStrWithoutNamespaces, out typeStr);
								if (typeStr == null)
									typeStr = varDecExpr.Type.ToString();
								var initializer = varDecExpr.Variables.Single().Initializer;
								if (((typeStr == "string" || typeStr == "char" || typeStr == "bool") && initializer != NullReferenceExpression.Null)
									|| initializer is ObjectCreateExpression)
								{
									if (VarId.Length < type.Length)
										varDecExpr.Type = new SimpleType(VarId);
								}
							}
						}
					}
					foreach (var variable in varDecExpr.Variables)
						CompressHelper(children);
				}
				else
				{
					if (Options.MiscCompressing && children is BlockStatement && children.Role.ToString() != "Body")
					{
						// if (a) { b; } => if (a) b;
						var childrenCount = children.Children.Count();
						if (childrenCount == 3)
							children.ReplaceWith(children.Children.ElementAt(1));
						else if (childrenCount < 3)
							children.Remove();
					}
					else if (Options.MiscCompressing && children is BinaryOperatorExpression)
					{
						// if (a == true) => if (a)
						// if (a == false) => if (!a)
						var binaryExpression = (BinaryOperatorExpression)children;
						var primitiveExpression = binaryExpression.Left as PrimitiveExpression;
						var expression = binaryExpression.Right;
						if (primitiveExpression == null)
						{
							primitiveExpression = binaryExpression.Right as PrimitiveExpression;
							expression = binaryExpression.Left;
						}
						if (primitiveExpression != null && primitiveExpression.Value is bool)
						{
							var boolean = (bool)primitiveExpression.Value;
							expression.Remove();
							if (boolean)
								children.ReplaceWith(expression);
							else
								children.ReplaceWith(new UnaryOperatorExpression(UnaryOperatorType.Not, expression));
						}
					}
					CompressHelper(children);
				}
			}
		}

		public static bool IsIntegerNumber(object value)
		{
			return value is sbyte || value is byte ||
				value is short || value is ushort || value is int ||
				value is uint || value is long || value is ulong;
		}

		#endregion

		#region Comments & Regions Removing

		private void RemoveCommentsAndRegions()
		{
			RemoveCommentsAndRegions(SyntaxTree);
		}

		private void RemoveCommentsAndRegions(AstNode node)
		{
			foreach (var children in node.Children)
			{
				if (Options.CommentsRemoving && children is Comment)
				{
					var properties = children.GetProperties();
					var commentType = properties.GetPropertyValueEnum<CommentType>(children, "CommentType");
					var content = properties.GetPropertyValue(children, "Content");
					if (!IgnoredComments.Contains(content) && commentType != CommentType.InactiveCode)
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

		private void CompressLocals()
		{
			var localsVisitor = new MinifyLocalsAstVisitor(IgnoredIdentifiers);
			CompileAndAcceptVisitor(localsVisitor);
			var substitutor = new Substitutor(new MinIdGenerator());
			var ignoredNames = new List<string>(IgnoredIdentifiers);
			ignoredNames.AddRange(localsVisitor.NotLocalsIdNames);
			var substituton = substitutor.Generate(localsVisitor.MethodVars, ignoredNames.ToArray());

			var astSubstitution = new List<Tuple<AstNode, string>>();
			foreach (var method in localsVisitor.MethodVars)
			{
				var localsSubst = substituton[method.Key];
				foreach (NameNode localVar in method.Value)
					AppendResolvedNodesAndNewNames(ResolveResultType.Local, astSubstitution, localVar.Node, localsSubst[localVar.Name]);
			}

			foreach (var resolveNode in astSubstitution)
				RenameNode(resolveNode.Item1, resolveNode.Item2);
		}

		private void CompressMembers()
		{
			var membersVisitor = new MinifyMembersAstVisitor(IgnoredIdentifiers, Options.ConsoleApp, Options.CompressPublic, Options.RemoveToStringMethods);
			CompileAndAcceptVisitor(membersVisitor);
			var substitutor = new Substitutor(new MinIdGenerator());
			var ignoredNames = new List<string>(IgnoredIdentifiers);
			ignoredNames.AddRange(membersVisitor.NotMembersIdNames);
			var substituton = substitutor.Generate(membersVisitor.TypesMembers, ignoredNames.ToArray());

			var astSubstitution = new List<Tuple<AstNode, string>>();
			foreach (var typeMembers in membersVisitor.TypesMembers)
			{
				var membersSubst = substituton[typeMembers.Key];
				foreach (NameNode member in typeMembers.Value)
					AppendResolvedNodesAndNewNames(ResolveResultType.Member, astSubstitution, member.Node, membersSubst[member.Name]);
			}

			foreach (var resolveNode in astSubstitution)
				RenameNode(resolveNode.Item1, resolveNode.Item2);
		}

		private void CompressTypes()
		{
			var typesVisitor = new MinifyTypesAstVisitor(IgnoredIdentifiers, Options.CompressPublic);
			CompileAndAcceptVisitor(typesVisitor);
			var substitutor = new Substitutor(new MinIdGenerator());
			var ignoredNames = new List<string>(IgnoredIdentifiers);
			ignoredNames.AddRange(typesVisitor.NotTypesIdNames);
			var substitution = substitutor.Generate(typesVisitor.Types, ignoredNames.ToArray());

			var astSubstitution = new List<Tuple<AstNode, string>>();
			foreach (var type in typesVisitor.Types)
				AppendResolvedNodesAndNewNames(ResolveResultType.Type, astSubstitution, type.Node, substitution[type.Name]);

			foreach (var resolveNode in astSubstitution)
				RenameNode(resolveNode.Item1, resolveNode.Item2);
		}

		private void CompileAndAcceptVisitor(DepthFirstAstVisitor visitor)
		{
			CompileAndResolve();
			SyntaxTree.AcceptVisitor(visitor);
		}

		private void CompileAndResolve()
		{
			_unresolvedFile = SyntaxTree.ToTypeSystem();
			_projectContent = _projectContent.AddOrUpdateFiles(_unresolvedFile);
			_compilation = _projectContent.CreateCompilation();
			_resolver = new CSharpAstResolver(_compilation, SyntaxTree, _unresolvedFile);
		}

		private void AppendResolvedNodesAndNewNames(ResolveResultType type, List<Tuple<AstNode, string>> astSubstitution, AstNode node, string newName)
		{
			ResolveResult resolveResult = _resolver.Resolve(node);
			if (resolveResult != null)
			{
				var findReferences = new FindReferences();
				FoundReferenceCallback callback = delegate(AstNode matchNode, ResolveResult result)
				{
					astSubstitution.Add(new Tuple<AstNode, string>(matchNode, newName));
				};

				if (type == ResolveResultType.Local)
					findReferences.FindLocalReferences((resolveResult as LocalResolveResult).Variable, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
				else if (type == ResolveResultType.Member)
				{
					var memberResolveResult = resolveResult as MemberResolveResult;
					var searchScopes = findReferences.GetSearchScopes(memberResolveResult.Member);
					findReferences.FindReferencesInFile(searchScopes, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
				}
				else if (type == ResolveResultType.Type)
				{
					var typeResolveResult = resolveResult as TypeResolveResult;
					var searchScopes = findReferences.GetSearchScopes(typeResolveResult.Type.GetDefinition());
					findReferences.FindReferencesInFile(searchScopes, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
				}
			}
			else
			{
			}
		}

		private void RenameNode(AstNode node, string newName)
		{
			if (node is ParameterDeclaration)
				((ParameterDeclaration)node).Name = newName;
			else if (node is VariableInitializer)
				((VariableInitializer)node).Name = newName;

			else if (node is VariableInitializer)
				((VariableInitializer)node).Name = newName;
			else if (node is MethodDeclaration)
				((MethodDeclaration)node).Name = newName;
			else if (node is PropertyDeclaration)
				((PropertyDeclaration)node).Name = newName;
			else if (node is IndexerDeclaration)
				((IndexerDeclaration)node).Name = newName;
			else if (node is OperatorDeclaration)
				((OperatorDeclaration)node).Name = newName;
			else if (node is MemberReferenceExpression)
				((MemberReferenceExpression)node).MemberName = newName;
			else if (node is IdentifierExpression)
				((IdentifierExpression)node).Identifier = newName;
			else if (node is InvocationExpression)
			{
				var invExpression = (InvocationExpression)node;
				if (invExpression.Target is IdentifierExpression)
					((IdentifierExpression)invExpression.Target).Identifier = newName;
				else if (invExpression.Target is MemberReferenceExpression)
					((MemberReferenceExpression)invExpression.Target).MemberName = newName;
				else
				{
				}
			}
			else if (node is NamedExpression)
				((NamedExpression)node).Name = newName;

			else if (node is TypeDeclaration)
				((TypeDeclaration)node).Name = newName;
			else if (node is SimpleType)
				((SimpleType)node).Identifier = newName;
			else
			{
			}
		}

		#endregion

		#region Removing of spaces and line breaks

		AstNode _prevNode;
		StringBuilder _line;
		StringBuilder _result;

		private string ToStringWithoutSpaces()
		{
			_result = new StringBuilder();
			_line = new StringBuilder(Options.LineLength);

			_prevNode = null;
			foreach (var children in SyntaxTree.Children)
			{
				RemoveSpacesAndAppend(children);
				if (children.Children.Count() <= 1)
					_prevNode = children;
			}
			_result.Append(_line);

			return _result.ToString();
		}

		private void RemoveSpacesAndAppend(AstNode node)
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
					_result.Append(newString);
				else
				{
					if (_line.Length + newString.Length > Options.LineLength)
					{
						_result.AppendLine(_line.ToString());
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
				foreach (AstNode children in node.Children)
				{
					RemoveSpacesAndAppend(children);
					if (children.Children.Count() <= 1)
						_prevNode = children;
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

			if (node is ThisReferenceExpression)
				return "this";
			else if (node is NullReferenceExpression)
				return "null";
			else if (node is CSharpTokenNode || node is CSharpModifierToken)
				return nodeRole;

			return properties
				.Where(p => NameKeys.Contains(p.Name))
				.FirstOrDefault()
				.GetValue(node, null)
				.ToString();
		}

		#endregion
	}
}
