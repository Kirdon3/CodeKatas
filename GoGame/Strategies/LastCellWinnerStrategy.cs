namespace GoGame
{
    public class LastCellWinnerStrategy : WinnerFindingStrategy
    {
        public LastCellWinnerStrategy(Board board) : base(board)
        {
        }

        public override StoneColor GetWinner()
        {
            int whiteCells = 0;
            int blackCells = 0;
            for (int i = 1; i < Board.BoardSize; i++)
            {
                for (int j = 1; j < Board.BoardSize; j++)
                {
                    if (IsFullySurroundedBy(i, j, StoneColor.Black))
                    {
                        blackCells++;
                    }
                    if (IsFullySurroundedBy(i, j, StoneColor.White))
                    {
                        whiteCells++;
                    }
                }
            }
            if (whiteCells>blackCells)
            {
                return StoneColor.White;
            }
            if (blackCells>whiteCells)
            {
                return StoneColor.Black;
            }
            return StoneColor.None;
        }

        public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            var EDGE = 2;
            bool surroundedOnLeft = (x < EDGE || _board.GetStoneColor(x - 1, y) == surroundingStoneColor);
            bool surroundedOnRight = (x > Board.BoardSize - EDGE || _board.GetStoneColor(x + 1, y) == surroundingStoneColor);
            bool surroundedOnBottom = (y > Board.BoardSize - EDGE || _board.GetStoneColor(x, y + 1) == surroundingStoneColor);
            bool surroundedOnTop = (y < EDGE || _board.GetStoneColor(x, y - 1) == surroundingStoneColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnBottom && surroundedOnTop;

            return fullySurrounded;
        }
    }
}