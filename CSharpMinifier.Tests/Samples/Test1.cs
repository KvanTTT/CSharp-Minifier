using System;
namespace A
{
	#region Test region

	internal class A
	{
		public int C = 22;

		public void B()
		{
			int A = 10;
			var b = new B();
			b.A = 5;

			int C = 4;
			C = 7;
			this.C = 28;

			Console.WriteLine(A);
			Console.WriteLine(C);
			Console.WriteLine(this.C);
		}
	}

	#endregion

	internal class B
	{
		/*
		 * Multiline comment
		 */
		public int A;
	}

	// Single line comment

	internal class C
	{
		public C() : base()
		{
		}
	}
}