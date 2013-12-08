using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier.Tests
{
	[TestFixture]
	public class IdGeneratorTests
	{
		[Test]
		public void MinIdGeneratorTest()
		{
			var minIdGenerator = new MinIdGenerator();
			for (int i = 0; i < MinIdGenerator.Chars0.Length; i++)
				Assert.AreEqual(MinIdGenerator.Chars0[i].ToString(), minIdGenerator.Next());
			for (int i = 0; i < MinIdGenerator.CharsN.Length; i++)
				Assert.AreEqual(MinIdGenerator.Chars0[0].ToString() + MinIdGenerator.CharsN[i], minIdGenerator.Next());
		}

		[Test]
		public void RandomIdGeneratorTest()
		{
			var randomIdGenerator = new RandomIdGenerator(1, 1);

			var generatedIds = new HashSet<string>();
			for (int i = 0; i < MinIdGenerator.Chars0.Length; i++)
				generatedIds.Add(randomIdGenerator.Next());
			
			Assert.AreEqual(MinIdGenerator.Chars0.Length, generatedIds.Count);
		}
	}
}
