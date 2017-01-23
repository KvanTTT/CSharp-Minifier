using Microsoft.CSharp;
using System.CodeDom.Compiler;

namespace CSharpMinifier
{
	public class CompileUtils
	{
		public static bool CanCompile(string program)
		{
			return !Compile(program).Errors.HasErrors;
		}

		public static CompilerResults Compile(string program)
		{
			CompilerResults compilerResults = null;
			using (CSharpCodeProvider provider = new CSharpCodeProvider())
			{
				compilerResults = provider.CompileAssemblyFromSource(new CompilerParameters(new string[]
				{
					"System.dll"
				}),
				new string[] { program });
			}
			return compilerResults;
		}
	}
}
