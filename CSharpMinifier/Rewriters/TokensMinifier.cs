using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Rename;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CSharpMinifier.Rewriters
{
    class TokensMinifier : CSharpSyntaxRewriter
    {
        private SemanticModel _semanticModel;
        private AdhocWorkspace _workspace;
        private MinifierOptions _options;
        private IdentifierGenerator _identifierGenerator; 

        public TokensMinifier(AdhocWorkspace workspace, MinifierOptions options = null, bool visitIntoStructuredTrivia = true) : base(visitIntoStructuredTrivia)
        {
            _options = options ?? new MinifierOptions();
            _identifierGenerator = new IdentifierGenerator();
            _workspace = workspace;
            //Add cause MSBuild does not copy Charp.Workspace.dll
            var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
        }

        public AdhocWorkspace MinifyIdentifiers()
        {
            foreach(var project in _workspace.CurrentSolution.Projects)
            {
                foreach(var document in project.Documents)
                {
                    _semanticModel = document.GetSemanticModelAsync().Result;
                    var node = _semanticModel.SyntaxTree.GetRoot();
                    node = Visit(node);
                    node = node.ReplaceTrivia(node.DescendantTrivia(), ReplaceTriviaNodes);
                    RenameAll(document.WithSyntaxRoot(node));
                }
            }

            return _workspace;
        }

        private void RenameAll(Document document)
        {           
            _semanticModel = document.GetSemanticModelAsync().Result;
            var newNode = _semanticModel.SyntaxTree.GetRoot();

            foreach (KeyValuePair<VariableDeclaratorSyntax, string> variable in _identifierGenerator.RenamedVariables)
            {
                var nodeToSearch = newNode.DescendantNodes()
                    .OfType<VariableDeclaratorSyntax>().FirstOrDefault(x => x.Identifier.ValueText.Equals(variable.Key.Identifier.ValueText));
                (newNode, document) = Rename(nodeToSearch, document, variable.Value);
            }

            foreach(var method in _identifierGenerator.RenamedMethods)
            {
                var nodeToSearch = newNode.DescendantNodes()
                    .OfType<MethodDeclarationSyntax>().FirstOrDefault(x => x.Identifier.ValueText.Equals(method.Key.Identifier.ValueText));
                (newNode, document) = Rename(nodeToSearch, document, method.Value);
            }

            foreach (var classToRename in _identifierGenerator.RenamedTypes)
            {
                var nodeToSearch = newNode.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>().FirstOrDefault(x => x.Identifier.ValueText.Equals(classToRename.Key.Identifier.ValueText));
                (newNode, document) = Rename(nodeToSearch, document, classToRename.Value);
            }
            
        }

        private (SyntaxNode node, Document document) Rename(SyntaxNode nodeToRename, Document document, string newName)
        {
            var symbolInfo = _semanticModel.GetSymbolInfo(nodeToRename).Symbol ?? _semanticModel.GetDeclaredSymbol(nodeToRename);
            var solution = Renamer.RenameSymbolAsync(document.Project.Solution, symbolInfo, newName,
                _workspace.Options).Result;
            _workspace.TryApplyChanges(solution);
            document = _workspace.CurrentSolution.GetDocument(document.Id);
            _semanticModel = document.GetSemanticModelAsync().Result;
            return (_semanticModel.SyntaxTree.GetRoot(), document);
        }

        public override SyntaxNode VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
            if (_options.LocalVarsCompressing)
            {
                _identifierGenerator.GetNextName(node.Declaration.Variables.First());
            }
            return base.VisitLocalDeclarationStatement(node);
        }

        public override SyntaxNode VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (_options.PublicCompressing)
            {
                _identifierGenerator.GetNextName(node);
            }
            return base.VisitClassDeclaration(node);
        }

        public override SyntaxNode VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (_options.LocalVarsCompressing && node.Modifiers.Any(m => m.Value.Equals("private")))
            {
                _identifierGenerator.GetNextName(node.Declaration.Variables.First());
            }
            else if (_options.PublicCompressing && node.Modifiers.Any(m => m.Value.Equals("public")))
            {
                _identifierGenerator.GetNextName(node.Declaration.Variables.First());
            }
            return base.VisitFieldDeclaration(node);
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (_options.LocalVarsCompressing && node.Modifiers.Any(m => m.Value.Equals("private")))
            {
                _identifierGenerator.GetNextName(node);
            }
            else if (_options.PublicCompressing && node.Modifiers.Any(m => m.Value.Equals("public")))
            {
                _identifierGenerator.GetNextName(node);
            }
            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (_options.PublicCompressing && node.Modifiers.Any(m => m.Value.Equals("public")))
            {
                _identifierGenerator.GetNextName(node);
            }
            else if (_options.LocalVarsCompressing && node.Modifiers.Any(m => m.Value.Equals("private")))
            {
                _identifierGenerator.GetNextName(node);
            }
            return base.VisitPropertyDeclaration(node);
        }


        public SyntaxTrivia ReplaceTriviaNodes(SyntaxTrivia arg1, SyntaxTrivia arg2)
        {
            if (_options.CommentsRemoving || _options.RegionsRemoving)
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
            }
            if (_options.SpacesRemoving)
            {
                if (arg1.IsKind(SyntaxKind.WhitespaceTrivia) || arg1.IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    arg2 = CarriageReturn;
                }
                else
                {
                    arg2 = arg1;
                }
                
            }
            return arg2;
        }
    }
}
