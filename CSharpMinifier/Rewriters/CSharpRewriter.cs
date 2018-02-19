using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.Rename;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CSharpMinifier.Rewriters
{
    class CSharpRewriter : CSharpSyntaxRewriter
    {
        private SemanticModel _semanticModel;
        private AdhocWorkspace _workspace;
        private MinifierOptions _options;
        private IdentifierGenerator _identifierGenerator; 

        public CSharpRewriter(MinifierOptions options = null, bool visitIntoStructuredTrivia = true) : base(visitIntoStructuredTrivia)
        {
            _options = options ?? new MinifierOptions();
            _identifierGenerator = new IdentifierGenerator();
            //Add cause MSBuild does not work without it
            var _ = typeof(Microsoft.CodeAnalysis.CSharp.Formatting.CSharpFormattingOptions);
        }

        public SyntaxNode VisitAndRename(SyntaxNode node)
        {
            node = Visit(node);
            return RenameAll(node);
        }

        private SyntaxNode RenameAll(SyntaxNode node)
        {
            var mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            _workspace = new AdhocWorkspace();
            var project = _workspace.AddProject("MinifierProject", LanguageNames.CSharp);
            project = project.AddMetadataReference(mscorlib);
            _workspace.TryApplyChanges(project.Solution);
            var source = node.GetText();
            var document = _workspace.AddDocument(project.Id, "Doc", source);
            _semanticModel = document.GetSemanticModelAsync().Result;
            var newNode = _semanticModel.SyntaxTree.GetRoot();
            var newSolution = document.Project.Solution;
            foreach (KeyValuePair<VariableDeclaratorSyntax, string> variable in _identifierGenerator.RenamedVariables)
            {
                var nodeToSearch = newNode.DescendantNodes()
                    .OfType<VariableDeclaratorSyntax>().FirstOrDefault(x => x.Identifier.ValueText.Equals(variable.Key.Identifier.ValueText));
                newNode = Rename(document, nodeToSearch, variable.Value);
            }
            foreach (var classToRename in _identifierGenerator.RenamedTypes)
            {
                var nodeToSearch = newNode.DescendantNodes()
                    .OfType<ClassDeclarationSyntax>().FirstOrDefault(x => x.Identifier.ValueText.Equals(classToRename.Key.Identifier.ValueText));
                newNode = Rename(document, nodeToSearch, classToRename.Value);
            }
            foreach (var fieldToRename in _identifierGenerator.RenamedFields)
            {
                
            }

            newNode = newSolution.Projects.First().Documents.First().GetSemanticModelAsync().Result.SyntaxTree.GetRoot();
            return newNode;
        }

        private SyntaxNode Rename(Document document, SyntaxNode nodeToRename, string newName)
        {
            var symbolInfo = _semanticModel.GetSymbolInfo(nodeToRename).Symbol ?? _semanticModel.GetDeclaredSymbol(nodeToRename);
            var solution = Renamer.RenameSymbolAsync(document.Project.Solution, symbolInfo, newName,
                _workspace.Options).Result;
            _workspace.TryApplyChanges(solution);
            document = _workspace.CurrentSolution.GetDocument(document.Id);
            _semanticModel = document.GetSemanticModelAsync().Result;
            return _semanticModel.SyntaxTree.GetRoot();
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
            if (_options.PublicCompressing)
            {
                _identifierGenerator.GetNextName(node);
            }
            return base.VisitFieldDeclaration(node);
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
