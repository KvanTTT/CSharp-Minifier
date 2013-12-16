// Comment at the begin
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
			bool q = true;

			Console.WriteLine(A);
			if (q)
				Console.WriteLine(0);
			if (q == false)
				Console.WriteLine(1);
			if (C == 4)
			{
				Console.WriteLine(C);
			}
			if (C == 7)
			{
				Console.WriteLine(A);
				Console.WriteLine(b);
			}
			{
			}
			Console.WriteLine(this.C);
		}

		private void MethodWithOneStatement()
		{
			Console.WriteLine(true);
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
		const int a = 0/*comment*/;
		const int b = 0xFF;
		private const string s = "asdf";

		public C()
			: base()
		{
		}
	}
}
// Comment at the end