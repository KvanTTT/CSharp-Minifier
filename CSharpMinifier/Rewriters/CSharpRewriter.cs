using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CSharpMinifier.Rewriters
{
    class CSharpRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private readonly string[] _namesGenerator;
        private int _currentIndex = 0;

        public CSharpRewriter(SemanticModel semanticModel,bool visitIntoStructuredTrivia = true) : base(visitIntoStructuredTrivia)
        {
            _semanticModel = semanticModel;
            _namesGenerator = new string[]{ "a","b","c","d","e","f","g","h","j","l","m","n"};
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
            return base.VisitLocalDeclarationStatement(node);
        }

        public override SyntaxNode VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            if (node.IsKind(SyntaxKind.VariableDeclarator))
            {
                var newNode= node.ReplaceToken(node.Identifier, Identifier(_namesGenerator[_currentIndex]));
                _currentIndex++;
                return newNode;
            }
            return base.VisitVariableDeclarator(node);
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
