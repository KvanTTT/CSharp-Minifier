using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public class MinifyMembersAstVisitor : DepthFirstAstVisitor
	{
		string _currentNamespace;
		List<NameNode> _currentMembers;
		IEnumerable<string> _ignoredMembers;

		public bool ConsoleApp
		{
			get;
			private set;
		}

		public bool CompressPublic
		{
			get;
			private set;
		}

		public bool RemoveToString
		{
			get;
			private set;
		}

		public HashSet<string> NotMembersIdNames
		{
			get;
			private set;
		}

		public Dictionary<string, List<NameNode>> TypeMembers
		{
			get;
			private set;
		}

		public MinifyMembersAstVisitor(IEnumerable<string> ignoredMembers, bool consoleApp, bool compressPublic, bool removeToString)
		{
			TypeMembers = new Dictionary<string, List<NameNode>>();
			NotMembersIdNames = new HashSet<string>();
			_ignoredMembers = ignoredMembers ?? new List<string>();
			ConsoleApp = consoleApp;
			CompressPublic = compressPublic;
			RemoveToString = removeToString;
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
		{
			_currentNamespace = namespaceDeclaration.Name;
			base.VisitNamespaceDeclaration(namespaceDeclaration);
		}

		public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
		{
			_currentMembers = new List<NameNode>();
			TypeMembers.Add(_currentNamespace + "." + typeDeclaration.Name, _currentMembers);
			base.VisitTypeDeclaration(typeDeclaration);
		}

		#region Visit type members

		public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
		{
			if (CheckModifiers(fieldDeclaration))
				foreach (var v in fieldDeclaration.Variables)
					if (!_ignoredMembers.Contains(v.Name))
						_currentMembers.Add(new NameNode(v.Name, v));
			base.VisitFieldDeclaration(fieldDeclaration);
		}

		public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
		{
			if (CheckModifiers(methodDeclaration) && !_ignoredMembers.Contains(methodDeclaration.Name) &&
				(ConsoleApp ? methodDeclaration.Name != "Main" : true) &&
				(methodDeclaration.Modifiers & Modifiers.Override) != Modifiers.Override)
					_currentMembers.Add(new NameNode(methodDeclaration.Name, methodDeclaration));
			if (RemoveToString && methodDeclaration.Name == "ToString" && (methodDeclaration.Modifiers & Modifiers.Override) == Modifiers.Override)
				methodDeclaration.Remove();
			base.VisitMethodDeclaration(methodDeclaration);
		}

		public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
		{
			if (CheckModifiers(propertyDeclaration) && !_ignoredMembers.Contains(propertyDeclaration.Name) &&
				(propertyDeclaration.Modifiers & Modifiers.Override) != Modifiers.Override)
				_currentMembers.Add(new NameNode(propertyDeclaration.Name, propertyDeclaration));
			base.VisitPropertyDeclaration(propertyDeclaration);
		}

		public override void VisitEventDeclaration(EventDeclaration eventDeclaration)
		{
			if (CheckModifiers(eventDeclaration) && !_ignoredMembers.Contains(eventDeclaration.Name))
				foreach (var v in eventDeclaration.Variables)
					_currentMembers.Add(new NameNode(v.Name, v));
			base.VisitEventDeclaration(eventDeclaration);
		}

		public override void VisitIndexerDeclaration(IndexerDeclaration indexerDeclaration)
		{
			base.VisitIndexerDeclaration(indexerDeclaration);
		}

		public override void VisitOperatorDeclaration(OperatorDeclaration operatorDeclaration)
		{
			base.VisitOperatorDeclaration(operatorDeclaration);
		}

		public override void VisitConstructorDeclaration(ConstructorDeclaration constructorDeclaration)
		{
			base.VisitConstructorDeclaration(constructorDeclaration);
		}

		public override void VisitDestructorDeclaration(DestructorDeclaration destructorDeclaration)
		{
			base.VisitDestructorDeclaration(destructorDeclaration);
		}

		#endregion

		public override void VisitIdentifier(Identifier identifier)
		{
			if (_currentMembers == null || _currentMembers.FirstOrDefault(v => v.Name == identifier.Name) == null)
				NotMembersIdNames.Add(identifier.Name);
			base.VisitIdentifier(identifier);
		}

		private bool CheckModifiers(EntityDeclaration declaration)
		{
			return CompressPublic || (declaration.Modifiers & Modifiers.Public) == Modifiers.None;
		}
	}
}
