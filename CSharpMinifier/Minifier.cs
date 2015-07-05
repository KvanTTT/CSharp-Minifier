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
		private AstNode _prevNode;

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

			if (Options.LocalVarsCompressing || Options.UselessMembersCompressing)
				CompressLocals();
			if (Options.MembersCompressing || Options.UselessMembersCompressing)
				CompressMembers();
			if (Options.TypesCompressing)
				CompressTypes();

			if (Options.MiscCompressing || Options.NamespacesRemoving)
				TraverseNodes();

			string result;
			if (Options.SpacesRemoving)
			{
				if (Options.MiscCompressing || Options.LocalVarsCompressing || Options.MembersCompressing || Options.TypesCompressing)
				{
					// TODO: Fix it.
					SyntaxTree = new CSharpParser().Parse(SyntaxTree.ToString(), ParserTempFileName);
				}
				
				result = GetStringWithoutSpaces();
			}
			else
				result = SyntaxTree.ToString();

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

		private void TraverseNodes()
		{
			CompileAndResolve();
			TraverseNodes(SyntaxTree);
		}

		private void TraverseNodes(AstNode node)
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
							primitiveExpression.SetValue(primitiveExpression.Value, str.Length < hex.Length ? str : hex);
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
				else if (Options.NamespacesRemoving && children is NamespaceDeclaration)
				{			
					var childsToRemove = children.Children.TakeWhile(c => !(c is CSharpTokenNode && c.Role.ToString() == "{"));
					foreach (var child in childsToRemove)
						child.Remove();
					if (children.Children.Count() > 0)
						children.Children.First().Remove();
					if (children.Children.Count() > 0)
						children.Children.Last().Remove();
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
						TraverseNodes(c);
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
						TraverseNodes(children);
				}
				else
				{
					if (Options.MiscCompressing && children is BlockStatement && children.Role.ToString() != "Body")
					{
						// if (a) { b; } => if (a) b;
						var childrenCount = children.Children.Count(c => !(c is NewLineNode));
						if (childrenCount == 3)
							children.ReplaceWith(children.Children.Skip(1).FirstOrDefault(c => !(c is NewLineNode)));
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
					TraverseNodes(children);
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

			var astSubstitution = new Dictionary<string, List<Tuple<string, List<AstNode>>>>();
			foreach (var method in localsVisitor.MethodVars)
			{
				var localVarsAstNodes = new List<Tuple<string, List<AstNode>>>();
				astSubstitution[method.Key] = localVarsAstNodes;
				var localsSubst = substituton[method.Key];
				foreach (NameNode localVar in method.Value)
					localVarsAstNodes.Add(new Tuple<string,List<AstNode>>(localsSubst[localVar.Name], GetResolvedNodes(ResolveResultType.Local, localVar.Node)));
			}

			RenameOrRemoveNodes(astSubstitution, true, Options.LocalVarsCompressing);
		}

		private void CompressMembers()
		{
			var membersVisitor = new MinifyMembersAstVisitor(IgnoredIdentifiers, Options.ConsoleApp, Options.PublicCompressing, Options.ToStringMethodsRemoving);
			CompileAndAcceptVisitor(membersVisitor);
			var substitutor = new Substitutor(new MinIdGenerator());
			var ignoredNames = new List<string>(IgnoredIdentifiers);
			ignoredNames.AddRange(membersVisitor.NotMembersIdNames);
			var substituton = substitutor.Generate(membersVisitor.TypesMembers, ignoredNames.ToArray());

			var astSubstitution = new Dictionary<string, List<Tuple<string, List<AstNode>>>>();
			foreach (var typeMembers in membersVisitor.TypesMembers)
			{
				var typeMembersAstNodes = new List<Tuple<string, List<AstNode>>>();
				astSubstitution[typeMembers.Key] = typeMembersAstNodes;
				var membersSubst = substituton[typeMembers.Key];
				foreach (NameNode member in typeMembers.Value)
					typeMembersAstNodes.Add(new Tuple<string,List<AstNode>>(membersSubst[member.Name], GetResolvedNodes(ResolveResultType.Member, member.Node)));
			}

			RenameOrRemoveNodes(astSubstitution, true, Options.MembersCompressing);
		}

		private void CompressTypes()
		{
			var typesVisitor = new MinifyTypesAstVisitor(IgnoredIdentifiers, Options.PublicCompressing);
			CompileAndAcceptVisitor(typesVisitor);
			var substitutor = new Substitutor(new MinIdGenerator());
			var ignoredNames = new List<string>(IgnoredIdentifiers);
			ignoredNames.AddRange(typesVisitor.NotTypesIdNames);
			var substitution = substitutor.Generate(typesVisitor.Types, ignoredNames.ToArray());

			var astSubstitution = new List<Tuple<string, List<AstNode>>>();
			foreach (var type in typesVisitor.Types)
				astSubstitution.Add(new Tuple<string, List<AstNode>>(substitution[type.Name], GetResolvedNodes(ResolveResultType.Type, type.Node)));

			RenameOrRemoveNodes(astSubstitution, false, Options.TypesCompressing);
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

		private List<AstNode> GetResolvedNodes(ResolveResultType type, AstNode node)
		{
			var resolvedNodes = new List<AstNode>();
			ResolveResult resolveResult = _resolver.Resolve(node);
			if (resolveResult != null)
			{
				var findReferences = new FindReferences();
				FoundReferenceCallback callback = delegate(AstNode matchNode, ResolveResult result)
				{
					resolvedNodes.Add(matchNode);
				};

				if (type == ResolveResultType.Local)
				{
					var localResolveResult = resolveResult as LocalResolveResult;
					if (localResolveResult != null)
						findReferences.FindLocalReferences(localResolveResult.Variable, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
				}
				else if (type == ResolveResultType.Member)
				{
					var memberResolveResult = resolveResult as MemberResolveResult;
					if (memberResolveResult != null)
					{
						var searchScopes = findReferences.GetSearchScopes(memberResolveResult.Member);
						findReferences.FindReferencesInFile(searchScopes, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
					}
				}
				else if (type == ResolveResultType.Type)
				{
					var typeResolveResult = resolveResult as TypeResolveResult;
					if (typeResolveResult != null)
					{
						var searchScopes = findReferences.GetSearchScopes(typeResolveResult.Type.GetDefinition());
						findReferences.FindReferencesInFile(searchScopes, _unresolvedFile, SyntaxTree, _compilation, callback, CancellationToken.None);
					}
				}
			}
			else
			{
			}
			return resolvedNodes;
		}

		private void RenameOrRemoveNodes(Dictionary<string, List<Tuple<string, List<AstNode>>>> substitution, bool removeOneRefNodes, bool rename)
		{
			foreach (var resolveNode in substitution)
				RenameOrRemoveNodes(resolveNode.Value, removeOneRefNodes, rename);
		}

		private void RenameOrRemoveNodes(List<Tuple<string, List<AstNode>>> substitution, bool removeOneRefNodes, bool rename)
		{
			foreach (var node in substitution)
			{
				if (rename)
				{
					foreach (var astNode in node.Item2)
						RenameNode(astNode, node.Item1);
				}

				var first = node.Item2.FirstOrDefault() as VariableInitializer;
				if (removeOneRefNodes && Options.UselessMembersCompressing && first != null)
				{
					bool constNode = false;
					var fieldDeclaration = first.Parent as FieldDeclaration;
					if (fieldDeclaration != null)
						constNode = fieldDeclaration.Modifiers.HasFlag(Modifiers.Const);
					else
					{
						var varDeclaration = first.Parent as VariableDeclarationStatement;
						if (varDeclaration != null)
							constNode = varDeclaration.Modifiers.HasFlag(Modifiers.Const);
					}

					if (!constNode || first.Initializer == NullReferenceExpression.Null)
					{
						//if (node.Item2.Count == 1)
						//	first.Parent.Remove();
					}
					else
					{
						var initializerStr = GetStringWithoutSpaces(first.Parent);
						var exprStr = GetStringWithoutSpaces(first.Initializer);
						if (initializerStr.Length + (node.Item2.Count - 1) * node.Item1.Length > (node.Item2.Count - 1) * exprStr.Length)
						{
							foreach (var child in node.Item2.Skip(1))
							{
								child.ReplaceWith(first.Initializer.Clone());
							}
							first.Parent.Remove();
						}
					}
				}
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
			else if (node is EnumMemberDeclaration)
				((EnumMemberDeclaration)node).Name = newName;
			else
			{
			}
		}

		#endregion

		#region Removing of spaces and line breaks

		private string GetStringWithoutSpaces()
		{
			return GetStringWithoutSpaces(SyntaxTree.Children);
		}

		private string GetStringWithoutSpaces(AstNode node)
		{
			return GetStringWithoutSpaces(new List<AstNode>() { node });
		}

		private string GetStringWithoutSpaces(IEnumerable<AstNode> nodes)
		{
			StringBuilder line;
			StringBuilder result;

			result = new StringBuilder();
			line = new StringBuilder(Options.LineLength);

			_prevNode = null;
			foreach (var node in nodes)
			{
				RemoveSpacesAndAppend(node, line, result);
				if (node.Children.Count() <= 1 && !(node is NewLineNode))
					_prevNode = node;
			}
			result.Append(line);

			return result.ToString();
		}

		private void RemoveSpacesAndAppend(AstNode node, StringBuilder line, StringBuilder result)
		{
			if (node.Children.Count() == 0)
			{
				string indent = GetIndent(node, line);

				string newString = indent + GetLeafNodeString(node);
				if (Options.LineLength == 0)
					result.Append(newString);
				else
				{
					if (line.Length + newString.Length > Options.LineLength)
					{
						result.AppendLine(line.ToString());
						line.Clear();
						line.Append(newString.TrimStart());
					}
					else
					{
						line.Append(newString);
					}
				}
			}
			else
			{
				List<AstNode> childrens;
				if (node is ArraySpecifier)
				{
					childrens = new List<AstNode>();
					childrens.Add(node.Children.FirstOrDefault(c => c.Role.ToString() == "["));
					childrens.AddRange(node.Children.Where(c => c.Role.ToString() != "[" && c.Role.ToString() != "]"));
					childrens.AddRange(node.Children.Where(c => c.Role.ToString() == "]"));
				}
				else if (node is ArrayInitializerExpression)
				{
					var arrayInitExpr = (ArrayInitializerExpression)node;
					childrens = node.Children.ToList();
					int exprNodeInd1 = -1, exprNodeInd2 = -1, commaInd1 = -1;
					for (int i = 0; i < childrens.Count; i++)
					{
						var c = childrens[i];
						if (!(c is NewLineNode || c is CSharpTokenNode))
						{
							if (exprNodeInd1 == -1)
								exprNodeInd1 = i;
							else
							{
								exprNodeInd2 = i;
								break;
							}
						}
						else if (exprNodeInd1 > -1 && exprNodeInd2 == -1 && c.ToString() == ",")
							commaInd1 = i;
					}
					if (exprNodeInd1 != -1 && commaInd1 == -1 && exprNodeInd2 != -1)
						childrens.Insert(exprNodeInd1 + 1,
							new CSharpTokenNode(TextLocation.Empty, Roles.Comma) { Role = Roles.Comma });
				}
				else
					childrens = node.Children.ToList();
				foreach (AstNode children in childrens)
				{
					RemoveSpacesAndAppend(children, line, result);
					if (children.Children.Count() <= 1 && !(children is NewLineNode))
						_prevNode = children;
				}
			}
		}

		private string GetIndent(AstNode node, StringBuilder line)
		{
			string indent = " ";
			char last = (char)0;
			if (line.Length != 0)
				last = line[line.Length - 1];

			if (last == ' ' || last == '\r' || last == '\n' || _prevNode == null || node == null)
				indent = "";
			else
			{
				var prevComment = _prevNode as Comment;
				if (prevComment != null)
				{
					if (prevComment.CommentType == CommentType.SingleLine || prevComment.CommentType == CommentType.Documentation)
						indent = Environment.NewLine;
					else
						indent = "";
				}
				else if (node is PreProcessorDirective || _prevNode is PreProcessorDirective)
					indent = Environment.NewLine;
				else
				{
					if ((_prevNode is CSharpTokenNode && _prevNode.Role.ToString().All(c => !char.IsLetterOrDigit(c))) ||
						_prevNode is NewLineNode ||
						(node is CSharpTokenNode && node.Role.ToString().All(c => !char.IsLetterOrDigit(c))) ||
						node is NewLineNode ||
						node is Comment)
							indent = "";
				}
			}

			return indent;
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
			else if (node is NewLineNode)
				return "";

			return properties
				.Where(p => NameKeys.Contains(p.Name))
				.FirstOrDefault()
				.GetValue(node, null)
				.ToString();
		}

		#endregion
	}
}
