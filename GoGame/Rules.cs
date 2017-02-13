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
            RemoveStoneIfSurrounded(x - 1, y-1);
        }

        private void RemoveStoneIfSurrounded(int x, int y)
        {
            var oppositeColor = GetOppositeColor(x, y);


            var fullySurrounded = IsFullySurroundedBy(x, y, oppositeColor);

            if (fullySurrounded)
            {
                Board.MakePositionEmpty(x, y);
            }

            bool almostSurroundedToTheRight = IsAlmostFullySurrounded(x, y, oppositeColor);
            
            bool almostSurrounded = IsAlmostFullySurrounded(x+1, y, oppositeColor);

            if (almostSurrounded && almostSurroundedToTheRight)
            {
                Board.MakePositionEmpty(x, y);
                Board.MakePositionEmpty(x+1, y);
            }

        }

        private bool IsAlmostFullySurrounded(int x, int y, StoneColor oppositeColor)
        {
            if (Board.GetStoneColor(x,y) == StoneColor.None)
            {
                return false;
            }
            var EDGE = 2;
            bool surroundedOnLeft = (x < EDGE || Board.GetStoneColor(x - 1, y) == oppositeColor);
            bool surroundedOnRight = (x > Board.BoardSize - EDGE || Board.GetStoneColor(x + 1, y) == oppositeColor);
            bool surroundedOnBottom = (y > Board.BoardSize - EDGE || Board.GetStoneColor(x, y + 1) == oppositeColor);
            bool surroundedOnTop = (y < EDGE || Board.GetStoneColor(x, y - 1) == oppositeColor);

            int surroundedEdges = 0;

            if (surroundedOnLeft) surroundedEdges++;
            if (surroundedOnRight) surroundedEdges++;
            if (surroundedOnBottom) surroundedEdges++;
            if (surroundedOnTop) surroundedEdges++;



            bool surroundedOnLeftSameColor = (x < EDGE || Board.GetStoneColor(x - 1, y) == Board.GetStoneColor(x,y));
            bool surroundedOnRightSameColor = (x > Board.BoardSize - EDGE || Board.GetStoneColor(x + 1, y) == Board.GetStoneColor(x, y));
            bool surroundedOnBottomSameColor = (y > Board.BoardSize - EDGE || Board.GetStoneColor(x, y + 1) == Board.GetStoneColor(x,y));
            bool surroundedOnTopSameColor = (y < EDGE || Board.GetStoneColor(x, y - 1) == Board.GetStoneColor(x, y));

            int surroundedEdgesSameColor = 0;

            if (surroundedOnLeftSameColor) surroundedEdgesSameColor++;
            if (surroundedOnRightSameColor) surroundedEdgesSameColor++;
            if (surroundedOnBottomSameColor) surroundedEdgesSameColor++;
            if (surroundedOnTopSameColor) surroundedEdgesSameColor++;

            bool fullySurrounded = surroundedEdges == 3 && surroundedEdgesSameColor == 1;

            return fullySurrounded;
        }

        private StoneColor GetOppositeColor(int x, int y)
        {
            return Board.GetStoneColor(x, y) == StoneColor.White ? StoneColor.Black : StoneColor.White;
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
            var result = new WinnerByCountStrategy(Board).GetWinner();
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