using NUnit.Framework;

namespace GoGame
{
    [TestFixture]
    public class FullGameTests
    {
        [Test]
        public void GetPositionStatus_InitialStatus_EmptyPosition()
        {
            Board board = MakeBoard();

            PositionStatus result = board.GetPositionStatus(1, 1);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_ToEmptyPosition_PositionIsFilled()
        {
            Board board = MakeBoard();
            board.AddStoneToPosition(StoneColor.Black, 1, 1);
            PositionStatus result = board.GetPositionStatus(1,1);
            Assert.AreEqual(PositionStatus.Filled, result);
        }

        [Test]
        public void AddStore_ToEmptyPosition_OtherEmptyPositionIsUnaffected()
        {
            Board board = MakeBoard();

            board.AddStoneToPosition(StoneColor.Black, 1, 1);
            var status = board.GetPositionStatus(1, 2);

            Assert.AreEqual(PositionStatus.Empty, status);
        }

        // What happens when you're ouyt of board position?


        

        private static Board MakeBoard()
        {
            return new Board();
        }
    }

    public enum StoneColor
    {
        Black,
        White
    }

    public enum PositionStatus
    {
        Empty,
        Filled
    }

    public class Board
    {
        private const int BoardSize = 20;

        private PositionStatus positionStatus = PositionStatus.Empty;

        private PositionStatus[,] positionStatusMatrix = new PositionStatus[BoardSize, BoardSize];

        public PositionStatus GetPositionStatus(int x, int y)
        {
            return positionStatusMatrix[x , y ];
        }

        public void AddStoneToPosition(StoneColor stoneColor, int x, int y)
        {
            positionStatusMatrix[x , y ] = PositionStatus.Filled;
        }
    }
}
