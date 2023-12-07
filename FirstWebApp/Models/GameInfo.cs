using FirstWebApp.ServerDatabase;
using FirstWebApp.Utilities;

namespace FirstWebApp.Models
{
	public class GameInfo
	{
		public Guid PlayerXGuid { get; private set; }
		public Guid PlayerOGuid { get; private set; }
		public Guid PlayerTurnGuid { get; private set; }

		public readonly Database database;

		public int EncodedField { get; private set; }
		public char[] DecodedField = new char[9];
		public char CurrentTurnSimbol => PlayerTurnGuid == PlayerXGuid ? 'X' : 'O';
		public int turnCounter => DecodedField.Count(x => x != ' ');

		public bool isSecondPlayerInGame => PlayerOGuid != Guid.Empty;

        public bool isPlayerXTurn => PlayerXGuid == PlayerTurnGuid;

		public bool isVictory => IsWinnerLeftDiagonal || IsWinnerRightDiagonal || IsWinnerRow || IsWinnerCol;

		public bool isDraw => DecodedField.Any(x => x != ' ');

		public string PlayerOName => database.Users[PlayerOGuid];
		public string PlayerXName => database.Users[PlayerXGuid];

		public bool AllPlayerInGame => PlayerOGuid != Guid.Empty && PlayerXGuid != Guid.Empty;

		public bool IsWinnerLeftDiagonal {
			get
			{
				int field = EncodedField;
				if (!isPlayerXTurn)
				{
					field = TicTacToeUtilities.SetEncodedFieldCellOAsX(field);
				}
				return (field & 0b_01_00_00_00_01_00_00_00_01) == 0b_01_00_00_00_01_00_00_00_01;
			}
		}

        public bool IsWinnerRightDiagonal
        {
            get
            {
                int field = EncodedField;
                if (!isPlayerXTurn)
                {
                    field = TicTacToeUtilities.SetEncodedFieldCellOAsX(field);
                }
                return (field & 0b_00_00_01_00_01_00_01_00_00) == 0b_00_00_01_00_01_00_01_00_00;
            }
        }

        public bool IsWinnerRow
        {
            get
            {
                int field = EncodedField;
                if (!isPlayerXTurn)
                {
                    field = TicTacToeUtilities.SetEncodedFieldCellOAsX(field);
                }

                return (field & 0b_01_00_00_00_01_00_00_00_01) == 0b_01_00_00_00_01_00_00_00_01 ||
                    (field >> 6 & 0b_01_00_00_00_01_00_00_00_01) == 0b_01_00_00_00_01_00_00_00_01 ||
                    (field >> 12 & 0b_01_00_00_00_01_00_00_00_01) == 0b_01_00_00_00_01_00_00_00_01;
            }
        }

        public bool IsWinnerCol
        {
            get
            {
                int field = EncodedField;
                if (!isPlayerXTurn)
                {
                    field = TicTacToeUtilities.SetEncodedFieldCellOAsX(field);
                }

                return (field & 0b_01_00_00_01_00_00_01_00_00) == 0b_01_00_00_01_00_00_01_00_00 ||
                    (field >> 2 & 0b_01_00_00_01_00_00_01_00_00) == 0b_01_00_00_01_00_00_01_00_00 ||
                    (field >> 4 & 0b_01_00_00_01_00_00_01_00_00) == 0b_01_00_00_01_00_00_01_00_00;
            }
        }

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

		public GameInfo(Guid playerXGuid, Guid playerOGuid, Guid playerTurnGuid, Database dataBase)
		{
			database = dataBase;
			PlayerXGuid = playerXGuid;
			PlayerOGuid = playerOGuid;
			PlayerTurnGuid = playerTurnGuid;
			CreateRandomBoard();
		}

		public GameInfo()
		{
			database = new Database();
			PlayerXGuid = Guid.Empty;
			PlayerOGuid = Guid.Empty;
			PlayerTurnGuid = Guid.Empty;
			CreateRandomBoard();
		}

		public GameInfo(Guid playerXGuid, int encodedField, Database dataBase)
		{
			database = dataBase;
			PlayerXGuid = playerXGuid;
			PlayerOGuid = Guid.Empty;
			PlayerTurnGuid = playerXGuid;
			CreateRandomBoard();
		}

        public GameInfo(GameInfo gameInfo)
        {
            PlayerXGuid = gameInfo.PlayerXGuid;
            PlayerOGuid = gameInfo.PlayerOGuid;
            PlayerTurnGuid = gameInfo.PlayerTurnGuid;
			database = gameInfo.database;
			SetField(gameInfo.EncodedField);
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

		internal void SetSecodPlayer(Guid playerGuid)
		{
			PlayerOGuid = playerGuid;
		}

        public void SetNextPlayerTurn()
        {
            PlayerTurnGuid = PlayerTurnGuid == PlayerXGuid ? PlayerOGuid : PlayerXGuid;
        }
    }
}

/*
    Teacher code (C.Andrey)
 */