using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public class MinifyLocalsAstVisitor : DepthFirstAstVisitor
	{
		private string _currentNamespace;
		private string _currentType;
		private List<NameNode> _currentMethodVars;
		private IEnumerable<string> _ignoredLocals;

		public HashSet<string> NotLocalsIdNames
		{
			get;
			private set;
		}

		public Dictionary<string, List<NameNode>> MethodVars
		{
			get;
			private set;
		}

		public MinifyLocalsAstVisitor(IEnumerable<string> ignoredLocals = null)
		{
			NotLocalsIdNames = new HashSet<string>();
			MethodVars = new Dictionary<string, List<NameNode>>();
			_ignoredLocals = ignoredLocals ?? new List<string>();
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
		{
			_currentNamespace = namespaceDeclaration.Name;
			base.VisitNamespaceDeclaration(namespaceDeclaration);
		}

		public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
		{
			_currentType = typeDeclaration.Name;
			base.VisitTypeDeclaration(typeDeclaration);
		}

		#region Visit members with body declarations

		public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
		{
			VisitMember(methodDeclaration.Name, methodDeclaration.Parameters.Select(p => p.Type.ToString()));
			base.VisitMethodDeclaration(methodDeclaration);
		}

		public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
		{
			VisitMember(propertyDeclaration.Name);
			base.VisitPropertyDeclaration(propertyDeclaration);
		}

		public override void VisitEventDeclaration(EventDeclaration eventDeclaration)
		{
			foreach (var v in eventDeclaration.Variables)
				VisitMember(v.Name);
			base.VisitEventDeclaration(eventDeclaration);
		}

		public override void VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
		{
			VisitMember(indexerDeclaration.Name, indexerDeclaration.Parameters.Select(p => p.Type.ToString()));
			base.VisitIndexerDeclaration(indexerDeclaration);
		}

		public override void VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
		{
			VisitMember(operatorDeclaration.Name, operatorDeclaration.Parameters.Select(p => p.Type.ToString()));
			base.VisitOperatorDeclaration(operatorDeclaration);
		}

		public override void VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
		{
			string prefix = "";
			if ((constructorDeclaration.Modifiers & Modifiers.Static) == Modifiers.Static)
				prefix = "static.";
			VisitMember(prefix + constructorDeclaration.Name, constructorDeclaration.Parameters.Select(p => p.Type.ToString()));
			base.VisitConstructorDeclaration(constructorDeclaration);
		}

		public override void VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
		{
			VisitMember(destructorDeclaration.Name);
			base.VisitDestructorDeclaration(destructorDeclaration);
		}

		private void VisitMember(string memberName, IEnumerable<string> parameters = null)
		{
			StringBuilder memberKey = new StringBuilder();
			memberKey.AppendFormat("{0}.{1}.{2}", _currentNamespace, _currentType, memberName);
			if (parameters != null)
			{
				memberKey.Append('(');
				foreach (var param in parameters)
				{
					memberKey.Append(param);
					memberKey.Append(',');
				}
				if (parameters.Count() != 0)
					memberKey.Remove(memberKey.Length - 1, 1);
				memberKey.Append(')');
			}
			_currentMethodVars = new List<NameNode>();
			MethodVars.Add(memberKey.ToString(), _currentMethodVars);
		}

		#endregion

		#region Visit local declarations

		public override void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
		{
			if (!_ignoredLocals.Contains(parameterDeclaration.Name))
				_currentMethodVars.Add(new NameNode(parameterDeclaration.Name, parameterDeclaration));
			base.VisitParameterDeclaration(parameterDeclaration);
		}

		public override void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
		{
			foreach (var varInitializer in variableDeclarationStatement.Variables)
			{
				if (!_ignoredLocals.Contains(varInitializer.Name))
					_currentMethodVars.Add(new NameNode(varInitializer.Name, varInitializer));
			}
			base.VisitVariableDeclarationStatement(variableDeclarationStatement);
		}

		#endregion

		public override void VisitIdentifier(Identifier identifier)
		{
			if (_currentMethodVars == null || _currentMethodVars.FirstOrDefault(v => v.Name == identifier.Name) == null)
				NotLocalsIdNames.Add(identifier.Name);
			base.VisitIdentifier(identifier);
		}
	}
}
