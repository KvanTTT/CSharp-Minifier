using System;
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.CSharp;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using ICSharpCode.NRefactory.TypeSystem;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public class Minifier
	{
		private static string[] NameKeys = new string[] { "Name", "LiteralValue", "Keyword" };

		private static string[] KnownTypesAndMethods = new string[] { 
			"Console", "Write", "WriteLine", "Read", "ReadLine" };

		Lazy<IList<IUnresolvedAssembly>> BuiltInLibs = new Lazy<IList<IUnresolvedAssembly>>(
		delegate
		{
			Assembly[] assemblies = {
						typeof(object).Assembly, // mscorlib
						typeof(Uri).Assembly, // System.dll
						typeof(Enumerable).Assembly, // System.Core.dll
						typeof(ICSharpCode.NRefactory.TypeSystem.IProjectContent).Assembly,
					};
			IUnresolvedAssembly[] projectContents = new IUnresolvedAssembly[assemblies.Length];
			Parallel.For(
				0, assemblies.Length,
				delegate(int i)
				{
					CecilLoader loader = new CecilLoader();
					projectContents[i] = loader.LoadAssemblyFile(assemblies[i].Location);
				});
			return projectContents;
		});

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
		}

		public string MinifyFiles(string[] csFiles)
		{
			CSharpParser parser = new CSharpParser();
			SyntaxTree[] trees = csFiles.Select(file => parser.Parse(file, file + "_temp.cs")).ToArray();

			SyntaxTree globalTree = new SyntaxTree();

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

			return Minify(globalTree);
		}

		public string MinifyFromString(string CSharpCode)
		{
			SyntaxTree syntaxTree = new CSharpParser().Parse(CSharpCode);

			return Minify(syntaxTree);
		}

		public string Minify(SyntaxTree syntaxTree)
		{
			if (CommentsRemoving)
				RemoveComments(syntaxTree);

			if (IdentifiersCompressing)
				CompressIdentifiers(syntaxTree);

			string result = SpacesRemoving ? SyntaxTreeToStringWithoutSpaces(syntaxTree, LineLength) : syntaxTree.GetText();

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

		public void RemoveComments(SyntaxTree syntaxTree)
		{
			foreach (var children in syntaxTree.Children)
			{
				if (children.Role.ToString() == "Comment")
				{
					CommentType commentType = (CommentType)Enum.Parse(typeof(CommentType),
						GetPropertyValue(children, "CommentType"));
					if (commentType != CommentType.InactiveCode)
						children.Remove();
				}
			}
		}

		#endregion

		#region Compress Identifiers

		public void CompressIdentifiers(SyntaxTree syntaxTree)
		{
			//syntaxTree.AcceptVisitor(new MinifierAstVisitor());
		}

		#endregion

		#region Removing of spaces and line breaks

		AstNode _prevNode;
		StringBuilder _line;

		public string SyntaxTreeToStringWithoutSpaces(SyntaxTree syntaxTree, int lineLength)
		{
			StringBuilder result = new StringBuilder();
			_line = new StringBuilder(LineLength);

			_prevNode = null;
			foreach (var children in syntaxTree.Children)
				TraverseChilds(children, result);
			result.Append(_line);

			return result.ToString();
		}

		private void TraverseChilds(AstNode node, StringBuilder stringBuilder)
		{
			if (node.Children.Count() == 0)
			{
				bool insertSpace = true;
				char last = ' ';
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
			var properties = GetProperties(node);
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

		#region Utils

		public static PropertyInfo[] GetProperties(AstNode node)
		{
			return node.GetType().GetProperties();
		}

		public static string GetPropertyValue(AstNode node, string propertyName)
		{
			return node.GetType()
				   .GetProperties()
				   .Where(p => p.Name == propertyName)
				   .FirstOrDefault()
				   .GetValue(node, null)
				   .ToString();
		}

		#endregion
	}
}
