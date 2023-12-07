namespace FirstWebApp.Models
{
    [Obsolete]
    public class TicTacToeModelObsolete
    {
        public string? makeMoveName { get; set; }
        public string winnerName { get; set; }
        public char[] boardRandom;
        public TicTacToeModelObsolete()
        {
            makeMoveName = string.Empty;
            winnerName = string.Empty;
            boardRandom = new char[9] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        }
    }
}
