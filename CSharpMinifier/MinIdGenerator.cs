using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public class MinIdGenerator : NamesGenerator
	{
		public static string Chars0 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
		public static string CharsN = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";

		List<int> Indexes;

		public MinIdGenerator()
			: base()
		{
		}

		public override void Reset()
		{
			base.Reset();
			CurrentCombinationNumber = -1;
			Indexes = new List<int>();
		}

		public override string Next()
		{
			int i = Indexes.Count - 1;
			bool inc;
			do
			{
				if (i == -1)
				{
					Indexes.Add(0);
					break;
				}
				Indexes[i] = Indexes[i] + 1;
				inc = false;
				if ((i == 0 && Indexes[i] >= Chars0.Length) || Indexes[i] >= CharsN.Length)
				{
					Indexes[i--] = 0;
					inc = true;
				}
			}
			while (inc);

			StringBuilder result = new StringBuilder();
			for (int j = 0; j < Indexes.Count; j++)
				result.Append(j == 0 ? Chars0[Indexes[j]] : CharsN[Indexes[j]]);

			CurrentCombinationNumber++;
			return result.ToString();
		}
	}
}
