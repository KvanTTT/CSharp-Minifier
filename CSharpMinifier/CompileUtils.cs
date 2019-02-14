using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CSharpMinifier
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Emulates <c>System.CodeDom.Compiler.CompilerError</c>.
    /// </summary>

    public sealed class CompilerError
    {
        public string ErrorNumber { get; }
        public string ErrorText   { get; }
        public string FileName    { get; }
        public bool IsWarning     { get; }
        public int Line           { get; }
        public int Column         { get; }

        public CompilerError(bool isWarning, string errorNumber, string errorText,
                             string fileName, int line, int column)
        {
            IsWarning   = isWarning;
            ErrorNumber = errorNumber;
            ErrorText   = errorText;
            FileName    = fileName;
            Line        = line;
            Column      = column;
        }

        public override string ToString() =>
            $"{FileName}({Line},{Column}): {(IsWarning ? "warning" : "error")} {ErrorNumber}: {ErrorText}";
    }

    /// <summary>
    /// Emulates <c>System.CodeDom.Compiler.CompilerErrorCollection</c>.
    /// </summary>

    public sealed class CompilerErrorCollection : Collection<CompilerError>
    {
        public CompilerErrorCollection(IEnumerable<CompilerError> errors) :
            base(Array.AsReadOnly(errors.ToArray())) {}

        public bool HasErrors => this.Any(e => !e.IsWarning);
        public bool HasWarnings => this.Any(e => e.IsWarning);
    }

    /// <summary>
    /// Emulates <c>System.CodeDom.Compiler.CompilerResults</c>.
    /// </summary>

    public class CompilerResults
    {
        internal CompilerResults(IEnumerable<CompilerError> errors,
                                 int nativeCompilerReturnValue,
                                 IList<string> output)
        {
            Errors = new CompilerErrorCollection(errors);
            NativeCompilerReturnValue = nativeCompilerReturnValue;
            Output = new ReadOnlyCollection<string>(output);
        }

        public CompilerErrorCollection Errors     { get; }
        public int NativeCompilerReturnValue      { get; }
        public IReadOnlyCollection<string> Output { get; }
    }

    public class CompileUtils
    {
        public static bool CanCompile(string program, string cscPath)
        {
            return !Compile(program, cscPath).Errors.HasErrors;
        }

        public static CompilerResults Compile(string program, string cscPath)
        {
            if (!File.Exists(cscPath))
                throw new FileNotFoundException("C# compiler not found at: " + cscPath);

            var path = Path.ChangeExtension(Path.GetTempFileName(), ".cs");
            File.WriteAllText(path, program, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
            using (var process = Process.Start(new ProcessStartInfo
            {
                FileName               = cscPath,
                Arguments              = path,
                CreateNoWindow         = true,
                UseShellExecute        = false,
                RedirectStandardError  = true,
                RedirectStandardOutput = true,
            }))
            {
                var output = new List<string>();

                void OnData(object sender, DataReceivedEventArgs args)
                {
                    if (args.Data == null)
                        return; // EOI
                    lock (output)
                        output.Add(args.Data);
                }

                process.OutputDataReceived += OnData;
                process.ErrorDataReceived += OnData;

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();

                var errors =
                    from line in output
                    from Match m in Regex.Matches(line, @"^(.+) *\( *([0-9]+) *, *([0-9]+) *\) *: *(error|warning) +(CS[0-9]+) *: *(.+)$")
                    select m.Groups.Cast<Group>()
                                   .Skip(1)
                                   .Select(g => g.Value)
                                   .ToArray()
                    into groups
                    select new CompilerError(fileName: groups[0].Trim(),
                                             line: int.Parse(groups[1], NumberStyles.None, CultureInfo.InvariantCulture),
                                             column: int.Parse(groups[2], NumberStyles.None, CultureInfo.InvariantCulture),
                                             isWarning: groups[3] == "warning",
                                             errorNumber: groups[4],
                                             errorText: groups[5].Trim());

                return new CompilerResults(errors, process.ExitCode, output);
            }
        }
    }
}
