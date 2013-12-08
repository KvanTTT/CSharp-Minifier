using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharpMinifier.Tests
{
	[TestFixture]
	public class MinifierTests
	{
		Dictionary<string, string> Samples;

		[SetUp]
		public void Init()
		{
			Samples = new Dictionary<string, string>();
			var sampleFiles = Directory.GetFiles(@"..\..\..\Test Code");
			foreach (var file in sampleFiles)
			{
				var code = File.ReadAllText(file);
				Samples.Add(Path.GetFileNameWithoutExtension(file), code);
				if (!CompileUtils.CanCompile(code))
					Assert.Inconclusive("All input code should be compilied");
			}
		}

		[Test]
		public void RemoveSpaces()
		{
			var minifier = new Minifier(false, true, false);
			foreach (var sample in Samples)
			{
				var minified = minifier.MinifyFromString(sample.Value);
				Assert.IsTrue(CompileUtils.CanCompile(minified));
			}
		}

		[Test]
		public void CompressIdentifiers()
		{
			var minifier = new Minifier(true, false, false);
			foreach (var sample in Samples)
			{
				var minified = minifier.MinifyFromString(sample.Value);
				Assert.IsTrue(CompileUtils.CanCompile(minified));
			}
		}
	}
}
