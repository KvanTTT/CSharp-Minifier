
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpMinifier
{
	public class MinifierOptions
	{
		public bool LocalVarsCompressing
		{
			get;
			set;
		}

		public bool MembersCompressing
		{
			get;
			set;
		}

		public bool TypesCompressing
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

		public bool NamespacesRemoving
		{
			get;
			set;
		}

		public bool PublicCompressing
		{
			get;
			set;
		}

		public bool ToStringMethodsRemoving
		{
			get;
			set;
		}

		public bool UselessMembersCompressing
		{
			get;
			set;
		}

		public bool EnumToIntConversion
		{
			get;
			set;
		}

		public MinifierOptions(bool maxCompression = true)
		{
			if (maxCompression)
			{
				LocalVarsCompressing = true;
				MembersCompressing = true;
				TypesCompressing = true;
				SpacesRemoving = true;
				CommentsRemoving = true;
				RegionsRemoving = true;
				MiscCompressing = true;
				NamespacesRemoving = true;
				PublicCompressing = true;
				ToStringMethodsRemoving = true;
				UselessMembersCompressing = true;
				EnumToIntConversion = true;
			}
		}
	}
}
