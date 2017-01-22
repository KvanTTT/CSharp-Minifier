using System.Collections.Generic;
using System.Text;

namespace CSharpMinifier
{
	public class MinIdGenerator : NamesGenerator
	{
		public static string Chars0 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";
		public static string CharsN = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";

		List<int> _indexes;

		public MinIdGenerator()
			: base()
		{
		}

		public override void Reset()
		{
			base.Reset();
			_indexes = new List<int>();
		}

		public override string Next()
		{
			int i = _indexes.Count - 1;
			bool inc;
			do
			{
				if (i == -1)
				{
					_indexes.Add(0);
					break;
				}
				_indexes[i] = _indexes[i] + 1;
				inc = false;
				if ((i == 0 && _indexes[i] >= Chars0.Length) || _indexes[i] >= CharsN.Length)
				{
					_indexes[i--] = 0;
					inc = true;
				}
			}
			while (inc);

			StringBuilder result = new StringBuilder(_indexes.Count);
			for (int j = 0; j < _indexes.Count; j++)
				result.Append(j == 0 ? Chars0[_indexes[j]] : CharsN[_indexes[j]]);

			CurrentCombination = result.ToString();
			CurrentCombinationNumber++;
			
			return Prefix + CurrentCombination + Postfix;
		}

		public override int CurrentCombinationNumber
		{
			get
			{
				return base.CurrentCombinationNumber;
			}
			set
			{
				base.CurrentCombinationNumber = value;
			}
		}
	}
}
