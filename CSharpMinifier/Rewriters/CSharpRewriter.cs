using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CSharpMinifier.Rewriters
{
    class CSharpRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        public CSharpRewriter(SemanticModel semanticModel,bool visitIntoStructuredTrivia = true) : base(visitIntoStructuredTrivia)
        {
            _semanticModel = semanticModel;
        }
        
        public override SyntaxNode VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitEmptyStatement(EmptyStatementSyntax node)
        {
            return null;
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (node.Declaration.Variables.Count > 1)
            {
                return node;
            }
            if (node.Declaration.Variables[0].Initializer == null)
            {
                return node;
            }
            VariableDeclaratorSyntax declarator = node.Declaration.Variables.First();
            var variableName = declarator.Identifier;
            TypeSyntax variableTypeName = node.Declaration.Type;
            ITypeSymbol variableType =
                (ITypeSymbol)_semanticModel.GetSymbolInfo(variableTypeName)
                    .Symbol;
            TypeInfo initializerInfo =
                _semanticModel.GetTypeInfo(declarator
                    .Initializer
                    .Value);
            if (Equals(variableType, initializerInfo.Type))
            {
                TypeSyntax varTypeName =
                    IdentifierName("var")
                        .WithLeadingTrivia(
                            variableTypeName.GetLeadingTrivia())
                        .WithTrailingTrivia(
                            variableTypeName.GetTrailingTrivia());

                return node.ReplaceNode(variableTypeName, varTypeName);
            }

            return base.VisitLocalDeclarationStatement(node);
        }

        public SyntaxTrivia CommentAndRegionsTriviaNodes(SyntaxTrivia arg1, SyntaxTrivia arg2)
        {
            if (arg1.IsKind(SyntaxKind.SingleLineCommentTrivia) || arg1.IsKind(SyntaxKind.MultiLineCommentTrivia)
                || arg1.IsKind(SyntaxKind.RegionDirectiveTrivia) || arg1.IsKind(SyntaxKind.EndRegionDirectiveTrivia))
            {
                arg2 = SyntaxFactory.CarriageReturn;
            }
            else
            {
                arg2 = arg1;
            }
            return arg2;
        }
    }
}
