using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
    public class TicTacToeModel : GameInfo
    {
        public Guid CurrentPlayerGuid { get; private set; }

        public string CurrentPlayerName;

        public bool IsCurrentPlayerX => CurrentPlayerGuid == PlayerXGuid;
        public bool IsCurrentPlayerO => CurrentPlayerGuid == PlayerOGuid;
        public bool IsCurrentPlayerTurn => CurrentPlayerGuid == PlayerTurnGuid;

        public TicTacToeModel(GameInfo gameInfo, Guid currentPlayerGuid, Database database) : base(gameInfo)
        {
            CurrentPlayerGuid = currentPlayerGuid;
            CurrentPlayerName = database.Users[CurrentPlayerGuid];
        }
    }
}
