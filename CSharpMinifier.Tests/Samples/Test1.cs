// Comment at the begin
using System;
using System.Collections.Generic;

namespace A
{
	#region Test region

	internal class A
	{
		protected const char qwer = 'd', fdsa = 's';
		protected int C = 22;
		protected int zzz;
		protected Dictionary<string, int> dict = new Dictionary<string, int>()
		{
			{ "a", 23 }
		};

		class a
		{
		}

		protected string Prop
		{
			get;
			set;
		}

		public void B()
		{
			const uint M = 34;

			List<int> asdf = new List<int>();
			List<int> qwer;

			int A = 10;
			var b = new B();
			B b1 = new B();
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

		public void TryCatch()
		{
			string s;
			try
			{
				s = "success";
			}
			catch (Exception e)
			{
				s = "fail";
			}
		}

		public void EmptyStatements()
		{
			int a = 1, b = 2;
			if (a == b) { }
			int c = 1, d = 2;
			if (c == d) ;
			{ }
			;
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