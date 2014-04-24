// Comment at the begin
using System;
namespace A
{
	#region Test region

	internal class A
	{
		protected const char qwer = 'd', fdsa = 's';
		protected int C = 22;
		protected int zzz;

		protected string Prop
		{
			get;
			set;
		}

		public void B()
		{
			int A = 10;
			var b = new B();
			b.A = 5;

			int C = 4;
			C = 7;
			this.C = 28;
			bool q = true;
			/*unremovableComment*/
			int unminifiedId = 0;

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
			Console.WriteLine(unminifiedId);
		}

		public static A operator+(A a, A b)
		{
			return null;
		}

		static A()
		{
		}

		public A()
		{
			EventHandler(null, null);
		}

		~A()
		{
		}

		public event EventHandler EventHandler;

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
		
		/*unremovableComment1*/
		public C()
			: base()
		{
		}

		public override string ToString()
		{
			return "C string";
		}
	}

	internal class D : A
	{
	}

	interface Inter
	{
	}

	struct str
	{
	}
}
// Comment at the end