using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public class RandomIdGenerator : NamesGenerator
	{
		private List<string> _generatedIds;
		private Random _random;

		public int MinLength
		{
			get;
			set;
		}

		public int MaxLength
		{
			get;
			set;
		}

		public RandomIdGenerator(int minLength, int maxLength)
			: base()
		{
			MinLength = minLength;
			MaxLength = MaxLength;
		}

		public override void Reset()
		{
			_generatedIds = new List<string>();
			_random = new Random();
			base.Reset();
		}

		public override string Next()
		{
			int length = _random.Next(MinLength, MaxLength + 1);

			StringBuilder result = new StringBuilder(length);
			do
			{
				result.Clear();
				result.Append(MinIdGenerator.Chars0[_random.Next(MinIdGenerator.Chars0.Length)]);
				for (int i = 1; i < length; i++)
					result.Append(MinIdGenerator.CharsN[_random.Next(MinIdGenerator.CharsN.Length)]);
				CurrentCombination = result.ToString();
			}
			while (_generatedIds.Contains(CurrentCombination));

			CurrentCombinationNumber++;

			return result.ToString();
		}
	}
}
