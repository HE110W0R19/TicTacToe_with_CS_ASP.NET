using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
	public class LobbyPageModel
	{
		public string CurrentPlayerName { get; private set; }
		public LobbyTableModel[] Tables { get; private set; }

		public LobbyPageModel(string currentPlayerName) 
		{
			CurrentPlayerName = currentPlayerName;
			Tables = new LobbyTableModel[3];

			for(int i = 0; i < Tables.Length; ++i)
			{
				var lobbyTabelGuid = Database.Tables.ElementAt(i).Key;
				Tables[i] = new LobbyTableModel(lobbyTabelGuid);
			}
		}
	}

	public class LobbyTableModel
	{
		private readonly Guid _tabelGuid;
		public LobbyTableModel(Guid tabelGuid) 
		{
			_tabelGuid = tabelGuid;
		}
	}
}

/*
    Teacher code (C.Andrey)
 */
