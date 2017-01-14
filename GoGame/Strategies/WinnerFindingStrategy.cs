namespace GoGame
{
    public abstract class WinnerFindingStrategy
    {
        protected readonly Board _board;

        public WinnerFindingStrategy(Board board)
        {
            _board = board;
        }
        public abstract StoneColor GetWinner();

    }
}