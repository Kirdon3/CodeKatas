namespace GoGame
{
    public class KomiWinnerStrategy : WinnerFindingStrategy
    {
        public KomiWinnerStrategy(Board b) : base(b)
        {
        }

        public override StoneColor GetWinner()
        {
            return StoneColor.White;
        }
    }
}