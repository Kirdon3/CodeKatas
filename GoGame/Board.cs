namespace GoGame
{
    public class Board
    {
        public const int BoardSize = 19;
        private Rules rules;

        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BoardSize, BoardSize];
        private StoneColor[,] stoneColorMatrix = new StoneColor[BoardSize, BoardSize];



        public Board()
        {
            this.rules = new Rules(this);
        }

        public void MakePositionEmpty(int x, int y)
        {
            positionStatusMatrix[x, y] = PositionStatus.Empty;
        }


        public PositionStatus GetPositionStatus(int x, int y)
        {
            return positionStatusMatrix[x, y];

        }

        public void AddStone(StoneColor stoneColor, int x, int y)
        {
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