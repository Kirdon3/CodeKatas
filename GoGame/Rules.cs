namespace GoGame
{
    class Rules
    {
        public Board Board;

        public Rules(Board board)
        {
            Board = board;
        }

        public void CheckStoneArroundPositionAndRemoveIfNeeded(int x, int y)
        {
            RemoveStoneIfSurrounded(x, y - 1);
            RemoveStoneIfSurrounded(x, y + 1);
            RemoveStoneIfSurrounded(x - 1, y);
            RemoveStoneIfSurrounded(x + 1, y);
        }

        private void RemoveStoneIfSurrounded(int x, int y)
        {
            var fullySurrounded = IsFullySurroundedBy(x, y, StoneColor.White);
            if (fullySurrounded)
            {
                Board.MakePositionEmpty(x, y);
            }
        }

        public bool IsFullySurroundedBy(int x, int y, StoneColor surroundingStoneColor)
        {
            var EDGE = 2;
            bool surroundedOnLeft = (x <EDGE || Board.GetStoneColor(x-1, y) == surroundingStoneColor);
            bool surroundedOnRight = (x > Board.BoardSize - EDGE || Board.GetStoneColor(x + 1, y) == surroundingStoneColor);
            bool surroundedOnBottom = (y > Board.BoardSize - EDGE || Board.GetStoneColor(x, y + 1) == surroundingStoneColor);
            bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1)== surroundingStoneColor);


            bool fullySurrounded = surroundedOnLeft && surroundedOnRight && surroundedOnBottom && surroundedOnTop;

            return fullySurrounded;
        }

        public StoneColor GetWinner()
        {
            var result = new LastCellWinnerStrategy(Board).GetWinner();
            if (result != StoneColor.None)
            {
                return result;
            }
            return new KomiWinnerStrategy(Board).GetWinner();
        }

        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
           
        }
    }
}