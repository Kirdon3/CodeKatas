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
            FillBoard(@"
 123456789
1B
2
3 
4
5
6
7
8
9
", board);

            PositionStatus result = board.GetPositionStatus(1, 1);
            Assert.AreEqual(PositionStatus.Filled, result);
        }

        [Test]
        public void AddStore_ToEmptyPosition_OtherEmptyPositionIsUnaffected()
        {
            Board board = MakeBoard();
            FillBoard(@"
 123456789
1B
2
3
4
5
6
7
8
9
", board);

            board.AddStone(StoneColor.Black, 1, 1);
            var status = board.GetPositionStatus(1, 2);

            Assert.AreEqual(PositionStatus.Empty, status);
        }

        // What happens when you're out of board position?

        [TestCase(PositionStatus.Empty,    @"123456789
                                        1 B 
                                        2BWB
                                        3 B
                                        4
                                        ")]
        [TestCase(PositionStatus.Empty, @"123456789
                                        1 W 
                                        2WBW
                                        3 W
                                        4
                                        ")]
        [TestCase(PositionStatus.Filled, @"123456789
                                        1 W 
                                        2WWW
                                        3 W
                                        4
                                        ")]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone(PositionStatus expectedStatus, string boardMap)
        {
            var board = MakeBoard();
            FillBoard(boardMap, board);

            var result = board.GetPositionStatus(2, 2);

            Assert.AreEqual(expectedStatus, result);
        }

        [Test]
        public void AddStone_SurroundOppositeColorStone_RemoveOppositeColorStone2()
        {
            var board = MakeBoard();
            FillBoard(@"
 123456789
1 
2 W
3WBW 
4 W
5
6
7
8
9
", board);
            

            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_AboveSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard();
            FillBoard(@"
 123456789
1 
2 
3WBW
4 W
5
6
7
8
9
", board);


            board.AddStone(StoneColor.White, 2, 2);

            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_LeftSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard();
            FillBoard(@"
 123456789
1
2 W
3 BW
4 W
5
6
7
8
9
", board);

            board.AddStone(StoneColor.White, 1, 3);
            var result = board.GetPositionStatus(2, 3);

            Assert.AreEqual(PositionStatus.Empty, result);
        }

        [Test]
        public void AddStone_RightSurroundOppositeColorStone_RemoveOppositeColorStone()
        {
            var board = MakeBoard(); FillBoard(@"
 123456789
1
2 W
3WBW
4 
5
6
7
8
9
", board);


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
            FillBoard(@"
 123456789
1 B
2B 
3 
4
5
6
7
8
9
", board);


            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }
        [Test]
        public void DetermineWinner_AtLeast1CellWonForBlackAndWhitePlayerLast_BlackWins()
        {
            var board = MakeBoard(); FillBoard(@"
 123456789
1 B
2B 
3W 
4  
5
6
7
8
9
", board);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_NotCornerCellForBlack_BlackWins()
        {
            var board = MakeBoard(); FillBoard(@"
 123456789
1  B
2 B B
3  B
4  W
5
6
7
8
9
", board);


            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.Black, result);
        }

        [Test]
        public void DetermineWinner_BlackDoesntOwnAnyCell_WhiteWins()
        {
            var board = MakeBoard();
            FillBoard(@"
 123456789
1  
2 
3B  W
4  
5
6
7
8
9
", board);

            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_NotCornerCellForWhite_WhiteWins()
        {
            var board = MakeBoard();

            FillBoard(@"
 123456789
1  W
2 W W
3  W
4  B
5
6
7
8
9
", board);


            StoneColor result = board.GetWinner();

            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_BlackOwns1CellsWhiteOwns2Cells_BlackWins()
        {
            var board = MakeBoard();

            FillBoard(@"
 123456789
1 B  W  W
2B BW WW W
3 B  W  W
4
5
6
7
8
9
", board);


            var result = board.GetWinner();
            Assert.AreEqual(StoneColor.White, result);
        }

        [Test]
        public void DetermineWinner_BlackOwns1CellWhiteOwns2CellsBlackGoesLast_BlackWins()
        {
            var board = MakeBoard();

            FillBoard(@"
 123456789
1 W  W  B
2W WW WB B
3 W  W  B
4
5
6
7
8
9
", board);


            var result = board.GetWinner();
            Assert.AreEqual(StoneColor.White, result);
        }

//        [Test]
//        public void DetermineWinner_1BlackChainSorrounds2Cells_BlackWins()
//        {
//            var board = MakeBoard(); FillBoard(@"
// 123456789
//1  BB
//2 B  B
//3  BB
//4  W
//5
//6
//7
//8
//9
//", board);


//            StoneColor result = board.GetWinner();

//            Assert.AreEqual(StoneColor.Black, result);
//        }

//        [Test]
//        public void AddStone_Surround2OppositeColorStones_RemoveOppositeColorStone()
//        {
//            var board = MakeBoard();
//            FillBoard(@"
// 123456789
//1 
//2 WW
//3WBBW 
//4 WW
//5
//6
//7
//8
//9
//", board);


//            var opositeStone1 = board.GetPositionStatus(2, 3);
//            var opositeStone2 = board.GetPositionStatus(3, 3);

//            Assert.AreEqual(PositionStatus.Empty, opositeStone2);
//            Assert.AreEqual(PositionStatus.Empty, opositeStone1);
//        }

        private void FillBoard(string stoneMap, Board board)
        {
            string[] lines = stoneMap.Trim().Split('\n');
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                for (int j = 1; j < line.Length; j++)
                {
                    char cell = line[j];
                    if (cell == 'W')
                    {
                        board.AddStone(StoneColor.White, i, j);
                    }
                    if (cell == 'B')
                    {
                        board.AddStone(StoneColor.Black, i, j);
                    }
                }

            }

        }

        private static Board MakeBoard()
        {
            return new Board();
        }
    }
}
