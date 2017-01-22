using ICSharpCode.NRefactory.CSharp;
using System;
using System.Linq;
using System.Reflection;

namespace CSharpMinifier
{
	public static class NRefactoryUtils
	{
		public static PropertyInfo[] GetProperties(this AstNode node)
		{
			return node.GetType().GetProperties();
		}

		public static string GetPropertyValue(this AstNode node, string propertyName)
		{
			return node.GetType()
					.GetProperties()
					.GetPropertyValue(node, propertyName);
		}

		public static T GetPropertyValueEnum<T>(this AstNode node, string propertyName)
		{
			return (T)node.GetType()
					.GetProperties()
					.GetPropertyValueEnum<T>(node, propertyName);
		}

		public static string GetPropertyValue(this PropertyInfo[] properties, AstNode node, string propertyName)
		{
			return properties
					.Where(p => p.Name == propertyName)
					.FirstOrDefault()
					.GetValue(node, null)
					.ToString();
		}

		public static T GetPropertyValueEnum<T>(this PropertyInfo[] properties, AstNode node, string propertyName)
		{
			return (T)Enum.Parse(typeof(T), properties
					.Where(p => p.Name == propertyName)
					.FirstOrDefault()
					.GetValue(node, null)
					.ToString());
		}
	}
}
