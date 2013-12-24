using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public struct LocalVarDec
	{
		public string Name;
		public AstNode Node;

		public LocalVarDec(string name, AstNode node)
		{
			Name = name;
			Node = node;
		}
	}

	public class MinifyLocalsAstVisitor : DepthFirstAstVisitor
	{
		string CurrentNamespace;
		string CurrentType;
		List<LocalVarDec> CurrentMethodVars;

		public HashSet<string> AllIdNames
		{
			get;
			private set;
		}

		public Dictionary<string, List<LocalVarDec>> MethodsVars
		{
			get;
			private set;
		}

		public MinifyLocalsAstVisitor()
		{
			AllIdNames = new HashSet<string>();
			MethodsVars = new Dictionary<string, List<LocalVarDec>>();
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
		{
			CurrentNamespace = namespaceDeclaration.Name;
			base.VisitNamespaceDeclaration(namespaceDeclaration);
		}

		public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
		{
			CurrentType = typeDeclaration.Name;
			base.VisitTypeDeclaration(typeDeclaration);
		}

		public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
		{
			base.VisitFieldDeclaration(fieldDeclaration);
		}

		public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
		{
			base.VisitPropertyDeclaration(propertyDeclaration);
		}

		public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
		{
			StringBuilder methodKey = new StringBuilder();
			methodKey.AppendFormat("{0}.{1}.{2}(", CurrentNamespace, CurrentType, methodDeclaration.Name);
			foreach (var param in methodDeclaration.Parameters)
			{
				methodKey.Append(param.Type);
				methodKey.Append(',');
			}
			methodKey.Remove(methodKey.Length - 1, 1);
			methodKey.Append(")");
			CurrentMethodVars = new List<LocalVarDec>();
			MethodsVars.Add(methodKey.ToString(), CurrentMethodVars);
			base.VisitMethodDeclaration(methodDeclaration);
		}

		public override void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
		{
			CurrentMethodVars.Add(new LocalVarDec(parameterDeclaration.Name, parameterDeclaration));
			base.VisitParameterDeclaration(parameterDeclaration);
		}

		public override void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
		{
			foreach (var varInitializer in variableDeclarationStatement.Variables)
			{
				CurrentMethodVars.Add(new LocalVarDec(varInitializer.Name, varInitializer));
			}
			base.VisitVariableDeclarationStatement(variableDeclarationStatement);
		}

		public override void VisitIdentifier(Identifier identifier)
		{
			AllIdNames.Add(identifier.Name);
			base.VisitIdentifier(identifier);
		}
	}
}
