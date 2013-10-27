using System;
using System.Linq;
using ICSharpCode.NRefactory;
using ICSharpCode.NRefactory.CSharp;
using System.Text;
using System.Reflection;

namespace CSharpMinifier
{
    public class Minifier
    {
        private static string[] NameKeys = new string[] { "Name", "LiteralValue", "Keyword" };

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

        public string MinifyFromString(string cSharpCode)
        {
            SyntaxTree syntaxTree = new CSharpParser().Parse(cSharpCode);

			if (CommentsRemoving)
                RemoveComments(syntaxTree);

			if (IdentifiersCompressing)
                CompressIdentifiers(syntaxTree);

			string result = SpacesRemoving ? SyntaxTreeToStringWithoutSpaces(syntaxTree, LineLength) : syntaxTree.GetText();

            return result;
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
