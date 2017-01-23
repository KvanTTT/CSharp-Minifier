using System.Collections.Generic;
class TextUtils
{
	public enum FONT { DEFAULT = 5, MONOSPACE };
	private static FONT selectedFont = FONT.DEFAULT;

	private static Dictionary<FONT, Dictionary<char, int>> LetterWidths = new Dictionary<FONT, Dictionary<char, int>>{
		{
			FONT.DEFAULT,
			new Dictionary<char, int> {
				{' ', 8 }
			}
		},
		{
			FONT.MONOSPACE,
			new Dictionary<char, int> {
				{' ', 24 }
			}
		}
	};
}
