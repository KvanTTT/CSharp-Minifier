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

        public Dictionary<PropertyDeclarationSyntax, string> RenamedProperties { get; set; }

        private List<string> _existingNames = new List<string>();

        private const char FirstSymbolCode = 'a';
        private const char LastSymbolCode = 'z';

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
            RenamedProperties = new Dictionary<PropertyDeclarationSyntax, string>();
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
            _existingNames.Add(FirstSymbolCode.ToString());
            AddToDictionary(node);
            return FirstSymbolCode.ToString();
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
                case SyntaxKind.PropertyDeclaration:
                    RenamedProperties.Add((PropertyDeclarationSyntax)node, LastName);
                    break;
            }
        }


        private string LastName => _existingNames.LastOrDefault();

        private string IncrementName(string lastName)
        {
            if (lastName.Length <= 0)
                lastName = "a";
            char lastChar = lastName[lastName.Length - 1];
            string fragment = lastName.Substring(0, lastName.Length - 1);
            while (lastChar < LastSymbolCode)
            {
                lastChar++;
                if (_cSharpKeywords.Contains(fragment + lastChar))
                    continue;
                return fragment + lastChar;
            }
            return IncrementName(fragment) + FirstSymbolCode;
        }
    }
}