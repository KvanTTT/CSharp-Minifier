using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
			var sampleFiles = Directory.GetFiles($"{System.AppDomain.CurrentDomain.BaseDirectory}..\\..\\Samples");
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
			var minifierOptions = new MinifierOptions(false)
			{
				SpacesRemoving = true
			};
			var minifier = new RoslynMinifier(minifierOptions);
			foreach (var sample in Samples)
			{
				var minified = minifier.MinifyFromString(sample.Value);
				Assert.IsTrue(CompileUtils.CanCompile(minified));
				if (sample.Key == "Test1")
					Assert.IsFalse(minified.Contains(" /*"));
			}
		}

		[Test]
		public void LineLengthConstraint()
		{
			var minifierOptions = new MinifierOptions
			{
				SpacesRemoving = false,
				CommentsRemoving = true,
				LineLength = 80,
				RegionsRemoving = true
			};
			var minifier = new RoslynMinifier(minifierOptions);
			foreach (var sample in Samples)
			{
				var minified = minifier.MinifyFromString(sample.Value);
				Assert.IsTrue(CompileUtils.CanCompile(minified));
			}
		}

		[Test]
		public void RemoveComments()
		{
			var minifierOptions = new MinifierOptions(false)
			{
				SpacesRemoving = true,
				CommentsRemoving = true
			};
			var minifier = new RoslynMinifier(minifierOptions);

			var test = Samples["Test1"];
			if (!test.Contains("//") || !test.Contains("/*") || !test.Contains("*/"))
				Assert.Inconclusive("Invalid test sample for RemoveComments test");
			var minified = minifier.MinifyFromString(test);
			Assert.IsTrue(CompileUtils.CanCompile(minified));
			Assert.IsFalse(minified.Contains("//"));
			Assert.IsFalse(minified.Contains("/*"));
			Assert.IsFalse(minified.Contains("*/"));
		}

		[Test]
		public void RemoveRegions()
		{
			var minifierOptions = new MinifierOptions
			{
				SpacesRemoving = true,
				RegionsRemoving = true
			};
			var minifier = new RoslynMinifier(minifierOptions);

			var test = Samples["Test1"];
			if (!test.Contains("#region") || !test.Contains("#endregion"))
				Assert.Inconclusive("Invalid test sample for RemoveRegions test");
			var minified = minifier.MinifyFromString(test);
			Assert.IsTrue(CompileUtils.CanCompile(minified));
			Assert.IsFalse(minified.Contains("#region"));
			Assert.IsFalse(minified.Contains("#endregion"));
		}

		[Test]
		public void CompressIdentifiers()
		{
			var minifierOptions = new MinifierOptions(false)
			{
				LocalVarsCompressing = true,
				MembersCompressing = true,
				TypesCompressing = true
			};
			var minifier = new RoslynMinifier(minifierOptions);
			foreach (var sample in Samples)
			{
				var minified = minifier.MinifyFromString(sample.Value);
				Assert.IsTrue(CompileUtils.CanCompile(minified));
			}
		}

		[Test]
		public void CompressMisc()
		{
			var minifierOptions = new MinifierOptions(false)
			{
				MiscCompressing = true
			};
			var minifier = new RoslynMinifier(minifierOptions);
			var minified = minifier.MinifyFromString(Samples["MiscCompression"]);
			Assert.IsTrue(minified.Contains("255"));
			Assert.IsTrue(minified.Contains("0x7048860F9180"));
			Assert.IsFalse(minified.Contains("private"));
			Assert.AreEqual(2, minified.Count(c => c == '{'));
			Assert.AreEqual(2, minified.Count(c => c == '}'));
		}

		[Test]
		public void IgnoredIdAndComments()
		{
			var minifier = new RoslynMinifier(null, new string[] { "unminifiedId" }, new string[] { "unremovableComment", "/*unremovableComment1*/" });
			var test = Samples["Test1"];
			if (!test.Contains("unminifiedId") || !test.Contains("unremovableComment") || !test.Contains("/*unremovableComment1*/"))
				Assert.Inconclusive("Invalid test sample for IgnoredIdAndComments test");
			var minified = minifier.MinifyFromString(test);
			Assert.IsTrue(minified.Contains("unminifiedId"));
			Assert.IsTrue(minified.Contains("unremovableComment"));
			Assert.IsTrue(minified.Contains("/*unremovableComment1*/"));
		}

		[Test]
		public void IncrementPlus()
		{
			var minifier = new RoslynMinifier(new MinifierOptions { LocalVarsCompressing = false });
			var testCode =
@"public class Test
{
	public void Main()
	{
		int x = 1;
		int y = 2;
		int z1 = y + ++x;
		int z2 = y + --x;
		int z3 = y - ++x;
		int z4 = y - --x;
	}
}";
			var minified = minifier.MinifyFromString(testCode);
			Assert.IsTrue(minified.Contains("int z1=y+ ++x"));
			Assert.IsTrue(minified.Contains("int z2=y+--x"));
			Assert.IsTrue(minified.Contains("int z3=y-++x"));
			Assert.IsTrue(minified.Contains("int z4=y- --x"));
		}

		[Test]
		public void NestedClassesWithSameInnterName()
		{
			var minifier = new RoslynMinifier();
			var testCode =
@"public static class A
{
    public static class One
    {
        public void foo() {}
    }
}

public static class B
{
    public static class One
    {
        public void foo() {}
    }
}";

			var minified = minifier.MinifyFromString(testCode);
			Assert.AreEqual("public static class b{public static class c{public void a(){}}}public static class d{public static class c{public void a(){}}}", minified);
		}

		[Test]
		public void ShouldProperlyConvertEnumWithoutInitializersToInt()
		{
			var minifier = new RoslynMinifier();
			var minified = minifier.MinifyFromString(Samples["EnumToIntConversion"]);

			Assert.IsTrue(CompileUtils.CanCompile(minified));
			Assert.AreEqual(
				"using System.Collections.Generic;class c{static int a=5;static Dictionary<int,Dictionary<char,int>>b=new Dictionary<int,Dictionary<char,int>>{{5,new Dictionary<char,int>{{' ',8}}},{6,new Dictionary<char,int>{{' ',24}}}};}",
				minified);
		}
	}
}
