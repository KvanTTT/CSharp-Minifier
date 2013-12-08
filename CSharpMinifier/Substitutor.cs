using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		public Dictionary<string, Dictionary<string, string>> Generate(Dictionary<string, List<LocalVarDec>> MethodsVars, HashSet<string> excludedNames)
		{
			var result = new Dictionary<string, Dictionary<string, string>>();

			foreach (var vars in MethodsVars)
			{
				int varCount = vars.Value.Count;
				string[] newNames = new string[varCount];
				Dictionary<string, string> newSubstitution = new Dictionary<string, string>();

				Generator.Reset();
				for (int i = 0; i < varCount; i++)
				{
					string newName;
					do
					{
						newName = Generator.Next();
					}
					while (excludedNames.Contains(newName));
					newNames[i] = newName;
				}

				int ind = 0;
				foreach (LocalVarDec v in vars.Value)
					if (!newSubstitution.ContainsKey(v.Name))
						newSubstitution.Add(v.Name, newNames[ind++]);

				result.Add(vars.Key, newSubstitution);
			}

			return result;
		}
	}
}
