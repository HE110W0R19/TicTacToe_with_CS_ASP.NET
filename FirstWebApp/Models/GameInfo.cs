using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
	public class GameInfo
	{
		public Guid PlayerXGuid { get; private set; }
		public Guid PlayerOGuid { get; private set; }
		public Guid PlayerTurnGuid { get; private set; }

		public int EncodedField { get; private set; }
		public char[] DecodedField => DecodeField();
		public string PlayerOName => Database.Users[PlayerOGuid];
		public string PlayerXName => Database.Users[PlayerXGuid];

		private Dictionary<int, char> _ValueIndex = new Dictionary<int, char>
		{
			{0, ' ' },
			{1, 'X' },
			{2, 'O' }
		};

		public char[] DecodeField()
		{
			var result = new char[9];
			var encodedField = EncodedField;

			for(int i = 8; i >= 0; --i)
			{
				var encodedCell = encodedField - (encodedField >> 2 << 2);
				encodedField >>= 2;

				result[i] = _ValueIndex[encodedCell];
			}

			return result;
		}

		public GameInfo(Guid playerXGuid, Guid playerOGuid, Guid playerTurnGuid)
		{
			PlayerXGuid = playerXGuid;
			PlayerOGuid = playerOGuid;
			PlayerTurnGuid = playerTurnGuid;

			//X,O,X,O, ,O, ,X, ;

			EncodedField = 0b_01_10_01_10_00_10_00_01_00;
		}
	}
}

/*
    Teacher code (C.Andrey)
 */