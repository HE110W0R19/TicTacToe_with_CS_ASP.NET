using FirstWebApp.ServerDatabase;

namespace FirstWebApp.Models
{
    public class TicTacToeModel : GameInfo
    {
        public Guid CurrentPlayerGuid { get; private set; }
        public string CurrentPlayerName => Database.Users[CurrentPlayerGuid];

        public bool IsCurrentPlayerX => CurrentPlayerGuid == PlayerXGuid;
        public bool IsCurrentPlayerO => CurrentPlayerGuid == PlayerOGuid;
        public bool IsCurrentPlayerTurn => CurrentPlayerGuid == PlayerTurnGuid;

        public TicTacToeModel(GameInfo gameInfo, Guid currentPlayerGuid) : base(gameInfo)
        {
            CurrentPlayerGuid = currentPlayerGuid;
        }
    }
}
