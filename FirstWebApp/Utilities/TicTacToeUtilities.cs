namespace FirstWebApp.Utilities
{
	public static class TicTacToeUtilities
	{
		private static Dictionary<int, char> _ValueIndex = new Dictionary<int, char>
		{
			{0, ' ' },
			{1, 'X' },
			{2, 'O' }
		};

		private static Dictionary<char, int> _EncodedValue = _ValueIndex.ToDictionary(x => x.Value, x => x.Key);

		public static int SetEncodedFieldCellOAsX(int field)
		{
			return field >> 1;
		}

		public static char[] DecodeField(int encodedField)
		{
			var result = new char[9];
			var encodedValue = encodedField;

			for (int i = 8; i >= 0; --i)
			{
				var encodedCell = encodedValue - (encodedValue >> 2 << 2);
				encodedValue >>= 2;

				result[i] = _ValueIndex[encodedCell];
			}

			return result;
		}

		public static int EncodeField(char[] decodedField)
		{
			var result = 0;

			for (int i = 0; i < decodedField.Length; ++i)
			{
				result <<= 2;
				result += _EncodedValue[decodedField[i]];
			}

			return result;
		}
	}
}
