
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public class MinifierOptions
	{
		public bool IdentifiersCompressing
		{
			get;
			set;
		}

		public bool SpacesRemoving
		{
			get;
			set;
		}

		public bool CommentsRemoving
		{
			get;
			set;
		}

		public int LineLength
		{
			get;
			set;
		}

		public bool RegionsRemoving
		{
			get;
			set;
		}

		public MinifierOptions()
		{
			IdentifiersCompressing = true;
			SpacesRemoving = true;
			CommentsRemoving = true;
			LineLength = 0;
			RegionsRemoving = true;
		}
	}
}
