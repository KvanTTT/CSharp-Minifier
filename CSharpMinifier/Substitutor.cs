using System.Collections.Generic;
using System.Linq;

namespace CSharpMinifier
{
	public class Substitutor
	{
		public NamesGenerator Generator
		{
			get;
			set;
		}

		public Substitutor(NamesGenerator generator)
		{
			Generator = generator;
		}

		public Dictionary<string, Dictionary<string, string>> Generate(Dictionary<string, List<NameNode>> idNameNodes, IEnumerable<string> excludedNames)
		{
			var result = new Dictionary<string, Dictionary<string, string>>();

			foreach (var vars in idNameNodes)
				result.Add(vars.Key, Generate(vars.Value, excludedNames));

			return result;
		}

		public Dictionary<string, string> Generate(List<NameNode> idNameNodes, IEnumerable<string> excludedNames)
		{
			Generator.Reset();

			int varCount = idNameNodes.Count;
			string[] newNames = new string[varCount];
			var newSubstitution = new Dictionary<string, string>();

			for (int i = 0; i < varCount; i++)
			{
				string newName;
				do
				{
					newName = Generator.Next();
				}
				while (excludedNames.Contains(newName) || NamesGenerator.CSharpKeywords.Contains(newName));
				newNames[i] = newName;
			}

			int ind = 0;
			foreach (NameNode v in idNameNodes)
				if (!newSubstitution.ContainsKey(v.Name))
					newSubstitution.Add(v.Name, newNames[ind++]);

			return newSubstitution;
		}
	}
}
