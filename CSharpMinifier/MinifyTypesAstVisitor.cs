using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public class MinifyTypesAstVisitor : DepthFirstAstVisitor
	{
		private string _currentNamespace;
		private List<NameNode> _currentMembers;
		private IEnumerable<string> _ignoredTypes;

		public bool CompressPublic
		{
			get;
			private set;
		}

		public HashSet<string> NotTypesIdNames
		{
			get;
			private set;
		}

		public List<NameNode> Types
		{
			get;
			private set;
		}

		public MinifyTypesAstVisitor(IEnumerable<string> ignoredTypes, bool compressPublic)
		{
			_ignoredTypes = ignoredTypes;
			CompressPublic = compressPublic;
			Types = new List<NameNode>();
			NotTypesIdNames = new HashSet<string>();
		}

		public override void VisitNamespaceDeclaration(NamespaceDeclaration namespaceDeclaration)
		{
			_currentNamespace = namespaceDeclaration.Name;
			base.VisitNamespaceDeclaration(namespaceDeclaration);
		}

		public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
		{
			_currentMembers = new List<NameNode>();
			if (CheckModifiers(typeDeclaration) && !_ignoredTypes.Contains(typeDeclaration.Name))
				Types.Add(new NameNode(typeDeclaration.Name, typeDeclaration));
			base.VisitTypeDeclaration(typeDeclaration);
		}

		public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
		{
			foreach (var v in fieldDeclaration.Variables)
				NotTypesIdNames.Add(v.Name);
			base.VisitFieldDeclaration(fieldDeclaration);
		}

		public override void VisitMethodDeclaration(MethodDeclaration methodDeclaration)
		{
			NotTypesIdNames.Add(methodDeclaration.Name);
			base.VisitMethodDeclaration(methodDeclaration);
		}

		public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
		{
			NotTypesIdNames.Add(propertyDeclaration.Name);
			base.VisitPropertyDeclaration(propertyDeclaration);
		}

		public override void VisitEventDeclaration(EventDeclaration eventDeclaration)
		{
			foreach (var v in eventDeclaration.Variables)
				NotTypesIdNames.Add(v.Name);
			base.VisitEventDeclaration(eventDeclaration);
		}

		public override void VisitIdentifier(Identifier identifier)
		{
			if (Types.FirstOrDefault(t => t.Name == identifier.Name) == null)
				NotTypesIdNames.Add(identifier.Name);
			base.VisitIdentifier(identifier);
		}

		private bool CheckModifiers(EntityDeclaration declaration)
		{
			return CompressPublic || (declaration.Modifiers & Modifiers.Public) == Modifiers.None;
		}
	}
}
