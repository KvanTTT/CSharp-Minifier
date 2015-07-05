using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
    public class MinifyEnumsAstVisitor : DepthFirstAstVisitor
    {
        private string _currentNamespace;
        private string _currentEnum;
        private List<NameNode> _currentEnumMembers;

        public Dictionary<TypeDeclaration, List<NameNode>> EnumMembers
        {
            get;
            private set;
        }

        public MinifyEnumsAstVisitor()
        {
            EnumMembers = new Dictionary<TypeDeclaration, List<NameNode>>();
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
        {
            _currentNamespace = namespaceDeclaration.Name;
            base.VisitNamespaceDeclaration(namespaceDeclaration);
        }

        public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
        {
            if (typeDeclaration.TypeKeyword.ToString() == "enum")
            {
                _currentEnumMembers = new List<NameNode>();
                EnumMembers.Add(typeDeclaration, _currentEnumMembers);
            }
            base.VisitTypeDeclaration(typeDeclaration);
        }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclaration enumMemberDeclaration)
        {
            _currentEnumMembers.Add(new NameNode(enumMemberDeclaration.Name, enumMemberDeclaration));
            base.VisitEnumMemberDeclaration(enumMemberDeclaration);
        }
    }
}
