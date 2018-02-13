using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMinifier.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CSharpMinifier
{
    public class RoslynMinifier : IMinifier
    {
        public SyntaxTree SyntaxTree { get; private set; }

        public SemanticModel SemanticModel { get; private set; }

        public MinifierOptions Options { get; private set; }

        public List<string> IgnoredIdentifiers { get; private set; }

        public List<string> IgnoredComments { get; private set; }

        public RoslynMinifier(MinifierOptions options = null, string[] ignoredIdentifiers = null, string[] ignoredComments = null)
        {
            Options = options ?? new MinifierOptions();
            IgnoredIdentifiers = ignoredIdentifiers?.ToList() ?? new List<string>();
            IgnoredComments = ignoredComments?.ToList() ?? new List<string>();
        }

        public string MinifyFiles(string[] csFiles)
        {
            throw new NotImplementedException();
        }

        public string MinifyFromString(string csharpCode)
        {
            SyntaxTree = CSharpSyntaxTree.ParseText(csharpCode);
            var Mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var compilation = CSharpCompilation.Create("MyCompilation", new[] { SyntaxTree }, new[] { Mscorlib });
            SemanticModel = compilation.GetSemanticModel(SyntaxTree);
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
            if (Options.EnumToIntConversion)
                ConvertEnumToInts();
            if (Options.MiscCompressing || Options.NamespacesRemoving)
                CompressMixed();

            string result;
            if (Options.SpacesRemoving)
                result = GetStringWithoutSpaces();
            else
                result = SyntaxTree.ToString();

            return result;
        }

        private void RemoveCommentsAndRegions()
        {
            var rewriter = new CSharpRewriter(SemanticModel);
            var root = SyntaxTree.GetRoot();
            root = rewriter.Visit(root);
            var newRoot = root.ReplaceTrivia(root.DescendantTrivia(), rewriter.CommentAndRegionsTriviaNodes);
            SyntaxTree = CSharpSyntaxTree.Create((CSharpSyntaxNode)newRoot);
        }

        private void CompressLocals()
        {
            var root = (CompilationUnitSyntax)SyntaxTree.GetRoot();
            var locals = root.DescendantNodes().OfType<VariableDeclarationSyntax>();
            foreach (var variables in locals.Select(l => l.Variables))
            {
                foreach (var variable in variables)
                {
                }
            }
            var localsCopy = locals;
        }

        private string GetStringWithoutSpaces()
        {
            throw new NotImplementedException();
        }

        private void CompressMixed()
        {
            throw new NotImplementedException();
        }

        private void ConvertEnumToInts()
        {
            throw new NotImplementedException();
        }

        private void CompressTypes()
        {
            throw new NotImplementedException();
        }

        private void CompressMembers()
        {
            throw new NotImplementedException();
        }
    }
}
