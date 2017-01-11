using System;
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
            board.AddStone(StoneColor.Black, 1, 1);
            PositionStatus result = board.GetPositionStatus(1, 1);
            Assert.AreEqual(PositionStatus.Filled, result);
        }

        [Test]
        public void AddStore_ToEmptyPosition_OtherEmptyPositionIsUnaffected()
        {
            Board board = MakeBoard();

            board.AddStone(StoneColor.Black, 1, 1);
            var status = board.GetPositionStatus(1, 2);

            Assert.AreEqual(PositionStatus.Empty, status);
        }

        // What happens when you're ouyt of board position?

        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard();

            board.AddStone(StoneColor.White, 2, 1);
            board.AddStone(StoneColor.White, 1, 2); board.AddStone(StoneColor.Black, 2, 2); board.AddStone(StoneColor.White, 3, 2);
            board.AddStone(StoneColor.White, 2, 3);

            var result = board.GetPositionStatus(2, 2);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone2()
        {
            var board = MakeBoard();

                                                            board.AddStone(StoneColor.White, 2, 2);
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                            board.AddStone(StoneColor.White, 2, 4);

            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_AboveSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard();

            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                            board.AddStone(StoneColor.White, 2, 4);

            board.AddStone(StoneColor.White, 2, 2);
            
            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_LeftSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard();

                                                             board.AddStone(StoneColor.White, 2, 2);
                                                             board.AddStone(StoneColor.Black, 2, 3); board.AddStone(StoneColor.White, 3, 3);
                                                             board.AddStone(StoneColor.White, 2, 4);

            board.AddStone(StoneColor.White, 1, 3);
            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_RightSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard();

                                                                board.AddStone(StoneColor.White, 2, 2);
            board.AddStone(StoneColor.White, 1, 3); board.AddStone(StoneColor.Black, 2, 3);
                                                                board.AddStone(StoneColor.White, 2, 4);

            board.AddStone(StoneColor.White, 3, 3);
            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void DetermineWinner_ByDefaultBecauseOfKomi_WhiteWins()
        {
            var board = MakeBoard();
            StoneColor result = board.GetWinner();
            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlack_BlackWins()
        {
            var board = MakeBoard();

            /*Black Teritory here*/ board.AddStone(StoneColor.Black, 1,2);
            board.AddStone(StoneColor.Black, 2, 1);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlackAndWhitePlayerLast_BlackWins()
        {
            var board = MakeBoard();

            /*Black Teritory here*/ board.AddStone(StoneColor.Black, 1,2);
            board.AddStone(StoneColor.Black, 2, 1);
            board.AddStone(StoneColor.White, 3, 1);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_NotCornerCellForBlack_BlackWins()
        {
            var board = MakeBoard();
                                                    board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.Black, 2, 2);/*Black Teritory here*/board.AddStone(StoneColor.Black, 4, 2);
                                    board.AddStone(StoneColor.Black, 3, 3);
            board.AddStone(StoneColor.White, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_BlackDoesntOwnAnyCell_WhiteWins()
        {
            var board = MakeBoard();
            board.AddStone(StoneColor.Black, 3, 1);
            board.AddStone(StoneColor.White, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_NotCornerCellForWhite_WhiteWins()
        {
            var board = MakeBoard();
                                                board.AddStone(StoneColor.White, 3, 1);
            board.AddStone(StoneColor.White, 2, 2);/*Black Teritory here*/board.AddStone(StoneColor.White, 4, 2);
                                                board.AddStone(StoneColor.White, 3, 3);
            board.AddStone(StoneColor.Black, 3, 4);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }


        private static Board MakeBoard()
        {
            return new Board();
        }
    }
}
