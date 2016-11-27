using NUnit.Framework;

namespace TennisKata
{
    public class TennisMatch
    {
        private readonly Player playerOne;
        private readonly Player playerTwo;
        private readonly int setsToWin;

        public TennisMatch(string playerOne, string playerTwo, int setsToWinTheMatch)
        {
            this.playerOne = new Player(playerOne);
            this.playerTwo = new Player(playerTwo);
            this.setsToWin = setsToWinTheMatch;
        }

        private bool MatchFinished { get; set; }

        private bool PlayerScored(Player scorer, Player opponent)
        {
            if (MatchFinished)
            {
                return false;
            }

            scorer.CurrentGameScore += 1;

            if (IsInTieBreak(scorer, opponent))
            {
                if (WonATieBreak(scorer, opponent))
                {
                    scorer.CurrentSetScore += 1;
                    StartNewGame(scorer, opponent);
                }
            }
            else
            {
                if (ScorerWonAGame(scorer, opponent))
                {
                    scorer.CurrentSetScore += 1;
                    StartNewGame(scorer, opponent);
                }
            }

            if (WonASet(scorer, opponent))
            {
                scorer.CurrentMatchScore += 1;
                StartNewSet(scorer, opponent);

                if (WonAMatch(scorer))
                {
                    MatchFinished = true;
                    return false;
                }
            }

            return true;
        }

        public bool PlayerOneScored()
        {
            return PlayerScored(playerOne, playerTwo);
        }

        public bool PlayerTwoScored()
        {
            return PlayerScored(playerTwo, playerOne);
        }

        public int[] GetGameScore()
        {
            int[] score = { playerOne.CurrentGameScore, playerTwo.CurrentGameScore };
            return score;
        }

        public int[] GetSetScore()
        {
            int[] score = { playerOne.CurrentSetScore, playerTwo.CurrentSetScore };
            return score;
        }
        public int[] GetMatchScore()
        {
            int[] score = { playerOne.CurrentMatchScore, playerTwo.CurrentMatchScore };
            return score;
        }

        private bool WonAMatch(Player scorer)
        {
            return scorer.CurrentMatchScore == setsToWin;
        }

        private static bool WonASet(Player scorer, Player opponent)
        {
            return (scorer.CurrentSetScore >= 6 && scorer.CurrentSetScore > opponent.CurrentSetScore + 1) || (scorer.CurrentSetScore == 7 && opponent.CurrentSetScore == 6);
        }

        private static bool WonATieBreak(Player scorer, Player opponent)
        {
            return scorer.CurrentGameScore > 6 && scorer.CurrentGameScore > opponent.CurrentGameScore + 1;
        }

        private bool IsInTieBreak(Player scorer, Player opponent)
        {
            return scorer.CurrentSetScore == 6 && opponent.CurrentSetScore == 6;
        }

        private void StartNewGame(Player scorer, Player opponent)
        {
            scorer.CurrentGameScore = 0;
            opponent.CurrentGameScore = 0;
        }

        private void StartNewSet(Player scorer, Player opponent)
        {
            scorer.CurrentSetScore = 0;
            opponent.CurrentSetScore = 0;
        }

        private bool ScorerWonAGame(Player scorer, Player opponent)
        {
            return scorer.CurrentGameScore > 3 && scorer.CurrentGameScore > opponent.CurrentGameScore + 1;
        }
    }


    [TestFixture]
    public class TennisGameTests
    {
        [Test]
        public void TennisGame_WhenStartingAGame_MatchScoreIsZeroToZero()
        {
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] {0, 0};
            
            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
        }
        [Test]
        public void TennisGame_WhenStartingAGame_SetScoreIsZeroToZero()
        {
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] { 0, 0 };

            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }
        [Test]
        public void TennisGame_WhenStartingAGame_GameScoreIsZeroToZero()
        {
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] { 0, 0 };

            Assert.AreEqual(expectedScore, tennisMatch.GetGameScore());
        }
        [Test]
        public void TennisGame_AfterPlayerOneScores_HisScoreIsIncreased()
        {
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] { 1, 0 };

            tennisMatch.PlayerOneScored();

            Assert.AreEqual(expectedScore,tennisMatch.GetGameScore());
        }
        [Test]
        public void TennisGame_WhenPlayerScoresFourPoints_HeWinsAGame()
        {
            var expectedScore = new [] { 1, 0 };
            var tennisMatch = CreateTennisMatch(0, 0, 3, 0, 0, 0);

            tennisMatch.PlayerOneScored();

            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }
        [Test]
        public void TennisGame_WhenPlayersAreTiedWithThreePoints_ScoringTheFourthDoesntWinTheGame()
        {
            var tennisMatch = CreateTennisMatch(0, 0, 3, 0, 0, 3);
            var expectedScore = new [] { 0, 0 };

            tennisMatch.PlayerOneScored();

            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());

        }
        [TestCase(5, 4)]
        [TestCase(6, 5)]
        public void TennisGame_WhenPlayerWinsRequredGames_HeWinsASet(int playerOneGames, int playerTwoGames)
        {
            var tennisMatch = CreateTennisMatch(0, playerOneGames, 3, 0, playerTwoGames, 0);
            var expectedScore = new [] { 1, 0 };

            tennisMatch.PlayerOneScored();

            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
        }
        [Test]
        public void TennisGame_WhenTheSetIsTied_WinningAGameDoesntWinASet()
        {
            var tennisMatch = CreateTennisMatch(0, 5, 3, 0, 5, 0);
            var expectedResult = new[] {0, 0};

            tennisMatch.PlayerOneScored();
            Assert.AreEqual(expectedResult, tennisMatch.GetMatchScore());
        }
        [Test]
        public void TennisGame_WhenPlayingTieBreakAndWinningFourPoints_DoesntWinAGame()
        {
            var tennisMatch = CreateTennisMatch(0, 5, 3, 0, 6, 0);
            var expectedResult = new[] { 6, 6 };
            
            tennisMatch.PlayerOneScored();

            // Act
            for (int i = 0; i < 4; i++)
            {
                tennisMatch.PlayerOneScored();
            }
            Assert.AreEqual(expectedResult, tennisMatch.GetSetScore());
        }
        [Test]
        public void TennisGame_WhenPlayingTieBreakAndWinningSevenPoints_WinsAGame()
        {
            var tennisMatch = CreateTennisMatch(0, 5, 3, 0, 6, 0);
            var expectedResult = new[] { 1, 0 };

            tennisMatch.PlayerOneScored();

            // Act
            for (int i = 0; i < 7; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            Assert.AreEqual(expectedResult, tennisMatch.GetMatchScore());
        }

        private TennisMatch CreateTennisMatch(int playerOneSets, int playerOneGames, int playerOnePoints, int playerTwoSets, int playerTwoGames, int playerTwoPoints)
        {
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);


            for (int i = 0; i < playerOneSets * 4 * 6; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            for (int i = 0; i < playerTwoSets * 4 * 6; i++)
            {
                tennisMatch.PlayerTwoScored();
            }

            for (int i = 0; i < playerOneGames * 4 ; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            for (int i = 0; i < playerTwoGames * 4; i++)
            {
                tennisMatch.PlayerTwoScored();
            }

            for (int i = 0; i < playerOnePoints; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            for (int i = 0; i < playerTwoPoints; i++)
            {
                tennisMatch.PlayerTwoScored();
            }

            return tennisMatch;
        }
    }
}
