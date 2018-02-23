using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharpMinifier.Rewriters;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpMinifier
{
    public class RoslynMinifier : IMinifier
    {
        public MinifierOptions Options { get; private set; }

        public List<string> IgnoredIdentifiers { get; private set; }

        public List<string> IgnoredComments { get; private set; }

        private AdhocWorkspace _workspace;
        private PortableExecutableReference _mscorlib;

        public RoslynMinifier(MinifierOptions options = null, string[] ignoredIdentifiers = null, string[] ignoredComments = null)
        {
            Options = options ?? new MinifierOptions();
            IgnoredIdentifiers = ignoredIdentifiers?.ToList() ?? new List<string>();
            IgnoredComments = ignoredComments?.ToList() ?? new List<string>();
            _workspace = new AdhocWorkspace();
            _mscorlib = MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
        }

        public string MinifyFiles(string[] csFiles)
        {
            var project = _workspace.AddProject("MinifierProject", LanguageNames.CSharp);
            project = project.AddMetadataReference(_mscorlib);
            _workspace.TryApplyChanges(project.Solution);
            var tempName = "tempFile";

            for(int i = 0; i < csFiles.Length; i++)
            {
                var syntaxTree = CSharpSyntaxTree.ParseText(csFiles[i]);                
                _workspace.AddDocument(project.Id, tempName + i, syntaxTree.GetText());
            }

            _workspace = Minify(_workspace);
            StringBuilder builder = new StringBuilder();
            var minifiedSources = _workspace.CurrentSolution.Projects.First().Documents.Select(d => d.GetTextAsync().Result.ToString());

            foreach (var source in minifiedSources)
                builder.Append(source);

            return builder.ToString();
        }

        public string MinifyFromString(string csharpCode)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(csharpCode);
            var project = _workspace.AddProject("MinifierProject", LanguageNames.CSharp);
            project = project.AddMetadataReference(_mscorlib);
            _workspace.TryApplyChanges(project.Solution);
            var document = _workspace.AddDocument(project.Id, "Doc", syntaxTree.GetText());
            _workspace = Minify(_workspace);

            return _workspace.CurrentSolution.Projects.First()
                .Documents.First().GetTextAsync().Result.ToString();
        }

        private AdhocWorkspace Minify(AdhocWorkspace workspace)
        {
            var rewriter = new TokensMinifier(workspace, Options);
            return rewriter.MinifyIdentifiers();
        }        
    }
}