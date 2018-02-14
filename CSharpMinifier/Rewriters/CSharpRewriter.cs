using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mono.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CSharpMinifier.Rewriters
{
    class CSharpRewriter : CSharpSyntaxRewriter
    {
        private readonly SemanticModel _semanticModel;
        private MinifierOptions _options;
        private IdentifierGenerator _identifierGenerator; 

        public CSharpRewriter(SemanticModel semanticModel, MinifierOptions options = null, bool visitIntoStructuredTrivia = true) : base(visitIntoStructuredTrivia)
        {
            _semanticModel = semanticModel;
            _options = options ?? new MinifierOptions();
            _identifierGenerator = new IdentifierGenerator();
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

        public override SyntaxNode VisitVariableDeclarator(VariableDeclaratorSyntax node)
        {
            if (_options.LocalVarsCompressing)
            {
                var name = _identifierGenerator.GetNextName(node);
                var newNode = node.WithIdentifier(Identifier(name));
                return newNode;
            }
            return base.VisitVariableDeclarator(node);
        }

        public override SyntaxNode VisitIdentifierName(IdentifierNameSyntax node)
        {
            if(_options.LocalVarsCompressing && _identifierGenerator.RenamedVariables.Any(i => i.Key.Identifier.ValueText == node.Identifier.ValueText))
            {
                var newName = _identifierGenerator.RenamedVariables.First(i => i.Key.Identifier.ValueText == node.Identifier.ValueText && node.FullSpan.IntersectsWith(i.Key.Parent.Parent.Parent.FullSpan)).Value;
                return node.WithIdentifier(Identifier(newName)
                    .WithTriviaFrom(node.Identifier));
            }
            return base.VisitIdentifierName(node);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            return base.VisitPropertyDeclaration(node);
        }


        public SyntaxTrivia CommentAndRegionsTriviaNodes(SyntaxTrivia arg1, SyntaxTrivia arg2)
        {
            if (arg1.IsKind(SyntaxKind.SingleLineCommentTrivia) || arg1.IsKind(SyntaxKind.MultiLineCommentTrivia)
                || arg1.IsKind(SyntaxKind.RegionDirectiveTrivia) || arg1.IsKind(SyntaxKind.EndRegionDirectiveTrivia))
            {
                arg2 = CarriageReturn;
            }
            else
            {
                arg2 = arg1;
            }
            return arg2;
        }
    }
}
