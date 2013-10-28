using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public class MinifierAstVisitor : DepthFirstAstVisitor
	{
		public override void VisitFieldDeclaration(FieldDeclaration fieldDeclaration)
		{
			base.VisitFieldDeclaration(fieldDeclaration);
		}

		public override void VisitIdentifier(Identifier identifier)
		{
			base.VisitIdentifier(identifier);
		}

		public override void VisitIdentifierExpression(IdentifierExpression identifierExpression)
		{
			base.VisitIdentifierExpression(identifierExpression);
		}

		public override void VisitNamedArgumentExpression(NamedArgumentExpression namedArgumentExpression)
		{
			base.VisitNamedArgumentExpression(namedArgumentExpression);
		}

		public override void VisitParameterDeclaration(ParameterDeclaration parameterDeclaration)
		{
			base.VisitParameterDeclaration(parameterDeclaration);
		}

		public override void VisitPrimitiveType(PrimitiveType primitiveType)
		{
			base.VisitPrimitiveType(primitiveType);
		}

		public override void VisitPropertyDeclaration(PropertyDeclaration propertyDeclaration)
		{
			base.VisitPropertyDeclaration(propertyDeclaration);
		}

		public override void VisitSimpleType(SimpleType simpleType)
		{
			base.VisitSimpleType(simpleType);
		}

		public override void VisitTypeDeclaration(TypeDeclaration typeDeclaration)
		{
			base.VisitTypeDeclaration(typeDeclaration);
		}

		public override void VisitTypeParameterDeclaration(TypeParameterDeclaration typeParameterDeclaration)
		{
			base.VisitTypeParameterDeclaration(typeParameterDeclaration);
		}

		public override void VisitVariableDeclarationStatement(VariableDeclarationStatement variableDeclarationStatement)
		{
			base.VisitVariableDeclarationStatement(variableDeclarationStatement);
		}


	}
}
