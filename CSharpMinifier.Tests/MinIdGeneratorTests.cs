using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier.Tests
{
	[TestFixture]
	public class MinIdGeneratorTests
	{
		[Test]
		public void Sequence()
		{
			var minIdGenerator = new MinIdGenerator();
			for (int i = 0; i < MinIdGenerator.Chars0.Length; i++)
				Assert.AreEqual(MinIdGenerator.Chars0[i].ToString(), minIdGenerator.Next());
			for (int i = 0; i < MinIdGenerator.CharsN.Length; i++)
				Assert.AreEqual(MinIdGenerator.Chars0[0].ToString() + MinIdGenerator.CharsN[i], minIdGenerator.Next());
		}
	}
}
