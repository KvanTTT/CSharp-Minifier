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

        public string MinifyFromString(string cSharpCode, bool compressIdentifiers = true, bool removeSpaces = true, bool removeComments = true)
        {
            SyntaxTree syntaxTree = new CSharpParser().Parse(cSharpCode);

            if (removeComments)
                RemoveComments(syntaxTree);

            if (compressIdentifiers)
                CompressIdentifiers(syntaxTree);

            string result = removeSpaces ? SyntaxTreeToStringWithoutSpaces(syntaxTree) : syntaxTree.GetText();

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

        public string SyntaxTreeToStringWithoutSpaces(SyntaxTree syntaxTree)
        {
            StringBuilder result = new StringBuilder();

            _prevNode = null;
            foreach (var children in syntaxTree.Children)
                TraverseChilds(children, result);

            return result.ToString();
        }

        private void TraverseChilds(AstNode node, StringBuilder stringBuilder)
        {
            if (node.Children.Count() == 0)
            {
                bool insertSpace = true;
                char last = ' ';
                if (stringBuilder.Length != 0)
                    last = stringBuilder[stringBuilder.Length - 1];
                if (last == ' ' || last == '\r' || last == '\n' || _prevNode == null || node == null)
                    insertSpace = false;
                else
                {
                    if ((_prevNode is CSharpTokenNode && _prevNode.Role.ToString().All(c => !char.IsLetterOrDigit(c))) ||
                        (node is CSharpTokenNode && node.Role.ToString().All(c => !char.IsLetterOrDigit(c))))
                        insertSpace = false;
                }
                stringBuilder.Append((insertSpace ? " " : "") + GetLeafNodeString(node));
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
