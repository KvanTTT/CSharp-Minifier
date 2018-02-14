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
            var rewriter = new CSharpRewriter(SemanticModel, Options);
            var root = SyntaxTree.GetRoot();
            root = rewriter.Visit(root);
            var newRoot = root.ReplaceTrivia(root.DescendantTrivia(), rewriter.CommentAndRegionsTriviaNodes);
            return newRoot.ToFullString();
        }
    }
}
