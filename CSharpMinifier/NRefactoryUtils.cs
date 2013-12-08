using ICSharpCode.NRefactory.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public class NRefactoryUtils
	{
		public static PropertyInfo[] GetProperties(AstNode node)
		{
			return node.GetType().GetProperties();
		}

		public static string GetPropertyValue(AstNode node, string propertyName)
		{
			return node.GetType()
				   .GetProperties()
				   .Where(p => p.Name == propertyName)
				   .FirstOrDefault()
				   .GetValue(node, null)
				   .ToString();
		}
	}
}
