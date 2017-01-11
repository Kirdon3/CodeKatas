namespace GoGame
{
    class Rules
    {
        public Board Board;
        private bool inKomiMode = true;
        private StoneColor currentWinner = StoneColor.White;

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
            if (inKomiMode)
            {
                return StoneColor.White;
            }
            for (int i = 1; i < Board.BoardSize; i++)
            {
                for (int j = 1; j < Board.BoardSize; j++)
                {
                    if (IsFullySurroundedBy(i, j, StoneColor.Black))
                    {
                        return StoneColor.Black;
                    }
                    if (IsFullySurroundedBy(i, j, StoneColor.White))
                    {
                        return StoneColor.White;
                    }
                }
            }
            bool sorrounded = IsFullySurroundedBy(1, 1, StoneColor.Black);
            if (sorrounded)
            {
                return Board.GetStoneColor(1, 2);
            }

            return currentWinner;
        }

        public void NotifyStoneAdded(StoneColor stoneColor, int x, int y)
        {
            inKomiMode = false;
        }
    }

    public class Board
    {
        public Board()
        {
            this.rules = new Rules { Board = this };
        }

        public void MakePositionEmpty(int x, int y)
        {
            positionStatusMatrix[x, y] = PositionStatus.Empty;
        }

        public const int BoardSize = 19;
        private Rules rules;

        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BoardSize, BoardSize];

        private StoneColor[,] stoneColorMatrix = new StoneColor[BoardSize, BoardSize];


        public PositionStatus GetPositionStatus(int x, int y)
        {
            return positionStatusMatrix[x, y];

        }

        public void AddStone(StoneColor stoneColor, int x, int y)
        {
            rules.NotifyStoneAdded(stoneColor, x, y);

            positionStatusMatrix[x, y] = PositionStatus.Filled;
            stoneColorMatrix[x, y] = stoneColor;

            rules.CheckStoneArroundPositionAndRemoveIfNeeded(x, y);
        }

       

        public StoneColor GetStoneColor(int x, int y)
        {
            return stoneColorMatrix[x, y];
        }


        public StoneColor GetWinner()
        {
           return rules.GetWinner();
        }
    }
}