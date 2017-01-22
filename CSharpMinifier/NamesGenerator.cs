using System.Collections.Generic;

namespace CSharpMinifier
{
	public abstract class NamesGenerator
	{
		public static string[] CSharpKeywords = new string[]
		{
			"abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
			"checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double",
			"else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float",
			"for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal",
			"is", "lock", "long", "namespace", "new", "null", "object", "operator", "out",
			"override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte",
			"sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch",
			"this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe",
			"ushort", "using", "virtual", "void", "volatile", "while"
		};

		public static Dictionary<string, string> CSharpTypeSynonyms = new Dictionary<string, string>()
		{
			{ "Boolean", "bool" },
			{ "Char", "char" },
			{ "Byte", "byte" },
			{ "Double", "double" },
			{ "False", "false" },
			{ "Single", "float" },
			{ "Int32", "int" },
			{ "Int64", "long" },
			{ "Object", "object" },
			{ "SByte", "sbyte" },
			{ "Int16", "short" },
			{ "String", "string" },
			{ "True", "true" },
			{ "UInt32", "uint" },
			{ "UInt64", "ulong" },
			{ "UInt16", "ushort" }
		};

		public abstract string Next();

		public NamesGenerator()
		{
			Reset();
		}

		public virtual void Reset()
		{
			CurrentCombinationNumber = -1;
			CurrentCombination = string.Empty;
			Prefix = "";
			Postfix = "";
		}

		public virtual int CurrentCombinationNumber
		{
			get;
			set;
		}

		public string CurrentCombination
		{
			get;
			protected set;
		}

		public string Prefix
		{
			get;
			protected set;
		}

		public string Postfix
		{
			get;
			protected set;
		}
	}
}
