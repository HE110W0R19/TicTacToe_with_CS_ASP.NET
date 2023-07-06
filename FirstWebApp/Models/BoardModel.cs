namespace FirstWebApp.Models
{
    [Obsolete]
    public static class BoardModel
    {
        public static TicTacToeModelObsolete boardInfo = new TicTacToeModelObsolete();
        public static bool isX = true;
        public static void restart()
        {
            boardInfo = new TicTacToeModelObsolete();
        }
    }
}
