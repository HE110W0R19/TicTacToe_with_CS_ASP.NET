using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
	public class LobbyPageModel
	{
		public string CurrentPlayerName { get; private set; }
		public LobbyTableModel[] Tables { get; private set; }

		public LobbyPageModel(string currentPlayerName, Database dataBase) 
		{
			CurrentPlayerName = currentPlayerName;
			Tables = new LobbyTableModel[3];

			for(int i = 0; i < Tables.Length; ++i)
			{
				var lobbyTabelGuid = dataBase.Tables.ElementAt(i).Key;
				Tables[i] = new LobbyTableModel(lobbyTabelGuid, dataBase);
			}
		}
	}

	public class LobbyTableModel
	{
		public Guid TabelGuid { get; private set; }
		public Guid GameGuid { get; private set; }

		public GameInfo Game;
		public LobbyTableModel(Guid tabelGuid, Database dataBase) 
		{
			TabelGuid = tabelGuid;
			GameGuid = dataBase.Tables[TabelGuid] ?? Guid.Empty;
		
			Game = GameGuid == Guid.Empty ? new GameInfo() : dataBase.Games[GameGuid];
		}
	}
}

/*
    Teacher code (C.Andrey)
 */
