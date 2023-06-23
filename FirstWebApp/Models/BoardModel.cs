namespace FirstWebApp.Models
{
    public static class BoardModel
    {
        public static TicTacToeModel boardInfo = new TicTacToeModel();
        public static bool isX = true;
        public static void restart()
        {
            boardInfo = new TicTacToeModel();
        }
    }
}
