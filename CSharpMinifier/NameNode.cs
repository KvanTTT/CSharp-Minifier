using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public class NameNode
	{
		public string Name;
		public AstNode Node;

		public NameNode(string name, AstNode node)
		{
			Name = name;
			Node = node;
		}

		public override string ToString()
		{
			return Name + "; " + Node.ToString();
		}
	}
}
