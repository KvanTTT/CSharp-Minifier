using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public struct TypeMemberDec
	{
		public string Name;
		public AstNode Node;

		public TypeMemberDec(string name, AstNode node)
		{
			Name = name;
			Node = node;
		}
	}
}
