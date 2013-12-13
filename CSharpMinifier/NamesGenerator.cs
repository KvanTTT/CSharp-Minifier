using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMinifier
{
	public abstract class NamesGenerator
	{
		public abstract string Next();

		public NamesGenerator()
		{
			Reset();
		}

		public virtual void Reset()
		{
			CurrentCombinationNumber = -1;
			CurrentCombination = string.Empty;
		}

		public virtual int CurrentCombinationNumber
		{
			get;
			set;
		}

		public string CurrentCombination
		{
			get;
			protected set;
		}
	}
}
