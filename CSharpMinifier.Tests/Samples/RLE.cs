using System.Collections.Generic;

namespace Asciimation
{
	public class RLE
	{
		#region Encode

		public static byte[] Encode(byte[] bytes)
		{
			List<byte> result = new List<byte>();

			int i = 0;
			while (i < bytes.Length)
			{
				int j = i;
				do
				{
					j++;
				}
				while (j != bytes.Length && bytes[j] == bytes[i]);

				int repeatCount = j - i;
				if (repeatCount >= 2)
				{
					int segmentCount = repeatCount / 129;
					int rest = repeatCount % 129;

					for (int k = 0; k < segmentCount; k++)
					{
						result.Add(127);
						result.Add(bytes[i]);
					}
					result.Add((byte)(rest - 2));
					result.Add(bytes[i]);
					i = j;
				}
				else
				{
					while (j != bytes.Length && bytes[j] != bytes[j - 1])
						j++;

					int nonrepeatCount = j - i;
					if (j != bytes.Length)
						nonrepeatCount--;
					int segmentCount = nonrepeatCount / 128;
					int rest = nonrepeatCount % 128;

					for (int k = 0; k < segmentCount; k++)
					{
						result.Add(0xFF);
						for (int l = 0; l < 128; l++)
							result.Add(bytes[i + k * 128 + l]);
					}
					result.Add((byte)(0x80 | (rest - 1)));
					for (int l = 0; l < rest; l++)
						result.Add(bytes[i + segmentCount * 128 + l]);
					i = j;
					if (j != bytes.Length)
						i--;
				}
			}

			return result.ToArray();
		}

		#endregion

		public static byte[] Decode(byte[] bytes)
		{
			List<byte> result = new List<byte>();

			int i = 0;
			while (i < bytes.Length)
			{
				if ((bytes[i] & 0x80) == 0)
				{
					int repeatCount = bytes[i] + 2;
					for (int j = 0; j < repeatCount; j++)
						result.Add(bytes[i + 1]);
					i = i + 2;
				}
				else
				{
					int repeatCount = (0x7F & bytes[i]) + 1;
					for (int j = 0; j < repeatCount; j++)
						result.Add(bytes[i + 1 + j]);
					i = i + 1 + repeatCount;
				}
			}

			return result.ToArray();
		}
	}
}
