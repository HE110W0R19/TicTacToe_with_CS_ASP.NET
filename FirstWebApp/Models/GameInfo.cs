using FirstWebApp.ServerDatabase;
using FirstWebApp.Utilities;

namespace FirstWebApp.Models
{
	public class GameInfo
	{
		public Guid PlayerXGuid { get; private set; }
		public Guid PlayerOGuid { get; private set; }
		public Guid PlayerTurnGuid { get; private set; }

		public int EncodedField { get; private set; }
		public char[] DecodedField = new char[9];
		public char CurrentTurnSimbol => PlayerTurnGuid == PlayerXGuid ? 'X' : 'O';
		public int turnCounter => DecodedField.Count(x => x != ' ');

		public string PlayerOName => Database.Users[PlayerOGuid];
		public string PlayerXName => Database.Users[PlayerXGuid];

		public void SetField(int encodedValue)
		{
			if (encodedValue == EncodedField)
			{
				throw new ArgumentException($"\"Нельзя изменить неизменяемое\"");
			}

			EncodedField = encodedValue;
			DecodedField = TicTacToeUtilities.DecodeField(encodedValue);
		}

		public void SetField(char[] decodedValue)
		{
			if (new string(decodedValue) == new string(DecodedField))
			{
				throw new ArgumentException($"\"Нельзя изменить неизменяемое\"");
			}

			EncodedField = TicTacToeUtilities.EncodeField(decodedValue);
			DecodedField = decodedValue;
		}

		public GameInfo(Guid playerXGuid, Guid playerOGuid, Guid playerTurnGuid)
		{
			PlayerXGuid = playerXGuid;
			PlayerOGuid = playerOGuid;
			PlayerTurnGuid = playerTurnGuid;

		}

		public GameInfo()
		{
			PlayerXGuid = Guid.Empty;
			PlayerOGuid = Guid.Empty;
			PlayerTurnGuid = Guid.Empty;
			CreateRandomBoard();
		}

		private void CreateRandomBoard()
		{
			var random = new Random();
			var board = new char[9] {' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };

			for (int i = 0; i < board.Length; i++)
			{
				if (random.Next(0, 9) % 2 == 0)
				{
					board[random.Next(0, 9)] = 'O';
				}
				else
				{
					board[random.Next(0, 9)] = 'X';
				}
			}

			SetField(board);
		}
	}
}

/*
    Teacher code (C.Andrey)
 */