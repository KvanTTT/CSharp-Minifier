using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpMinifier
{
    public class IdentifierGenerator
    {
        public Dictionary<VariableDeclaratorSyntax, string> RenamedVariables { get; private set; }

        public Dictionary<MethodDeclarationSyntax, string> RenamedMethods { get; private set; }

        public Dictionary<ClassDeclarationSyntax, string> RenamedTypes { get; private set; }

        public Dictionary<FieldDeclarationSyntax, string> RenamedFields { get; set; }

        private List<string> _existingNames = new List<string>();

        private const int FirstSymbolCode = 'a';
        private const int LastSymbolCode = 'z';

        private static string[] _cSharpKeywords = new string[]
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

        public IdentifierGenerator()
        {
            RenamedVariables = new Dictionary<VariableDeclaratorSyntax, string>();
            RenamedMethods = new Dictionary<MethodDeclarationSyntax, string>();
            RenamedTypes = new Dictionary<ClassDeclarationSyntax, string>();
            RenamedFields = new Dictionary<FieldDeclarationSyntax, string>();
        }

        public string GetNextName(SyntaxNode node)
        {
            if (_existingNames.Count > 0)
            {
                string lastName = _existingNames[_existingNames.Count - 1];
                string nextName = IncrementName(lastName);
                _existingNames.Add(nextName);
                AddToDictionary(node);
                return nextName;
            }
            char firstSymbol = (char)FirstSymbolCode;
            _existingNames.Add(firstSymbol.ToString());
            AddToDictionary(node);
            return firstSymbol.ToString();
        }

        private void AddToDictionary(SyntaxNode node)
        {
            switch (node.Kind())
            {
                case SyntaxKind.MethodDeclaration:
                    RenamedMethods.Add((MethodDeclarationSyntax)node, LastName);
                    break;
                case SyntaxKind.VariableDeclarator:
                    RenamedVariables.Add((VariableDeclaratorSyntax)node, LastName);
                    break;
                case SyntaxKind.ClassDeclaration:
                    RenamedTypes.Add((ClassDeclarationSyntax)node, LastName);
                    break;
                case SyntaxKind.FieldDeclaration:
                    RenamedFields.Add((FieldDeclarationSyntax)node, LastName);
                    break;
            }
        }


        private string LastName => _existingNames.LastOrDefault();

        private string IncrementName(string lastName)
        {
            if(lastName.Length == 0)
            {
                return "a";
            }
            char lastChar = lastName[lastName.Length - 1];
            string fragment = lastName.Substring(0, lastName.Length - 1);
            if (lastChar < LastSymbolCode)
            {
                lastChar++;
                return fragment + lastChar;
            }
            return IncrementName(fragment) + Char.ConvertFromUtf32(FirstSymbolCode);
        }
    }
}
