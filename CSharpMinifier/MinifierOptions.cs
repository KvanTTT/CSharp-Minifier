
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

		public bool MiscCompressing
		{
			get;
			set;
		}

		public bool ConsoleApp
		{
			get;
			set;
		}

		public bool RemoveNamespaces
		{
			get;
			set;
		}

		public bool CompressPublic
		{
			get;
			set;
		}

		public bool RemoveToStringMethods
		{
			get;
			set;
		}

		public MinifierOptions(bool maxCompression = true)
		{
			if (maxCompression)
			{
				IdentifiersCompressing = true;
				SpacesRemoving = true;
				CommentsRemoving = true;
				RegionsRemoving = true;
				MiscCompressing = true;
				RemoveNamespaces = true;
				CompressPublic = true;
				RemoveToStringMethods = true;
			}
		}
	}
}
