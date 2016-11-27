using NUnit.Framework;

namespace TennisKata
{
    [TestFixture]
    public class TennisGameTests
    {
        [Test]
        public void TennisGame_WhenStartingAGame_MatchScoreIsZeroToZero()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] {0, 0};
            
            // Act
            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
        }

        [Test]
        public void TennisGame_WhenStartingAGame_SetScoreIsZeroToZero()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] { 0, 0 };

            // Act
            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }

        [Test]
        public void TennisGame_WhenStartingAGame_GameScoreIsZeroToZero()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] { 0, 0 };

            // Act
            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetGameScore());
        }

        [Test]
        public void TennisGame_AfterPlayerOneScores_HisScoreIsIncreased()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 0, 0, 0, 0, 0);
            var expectedScore = new [] { 1, 0 };

            // Act
            tennisMatch.PlayerOneScored();

            // Assert
            Assert.AreEqual(expectedScore,tennisMatch.GetGameScore());
        }

        [Test]
        public void TennisGame_WhenPlayerScoresFourPoints_HeWinsAGame()
        {
            // Arrange
            var expectedScore = new [] { 1, 0 };
            var tennisMatch = CreateTennisMatch(0, 0, 3, 0, 0, 0);

            // Act
            tennisMatch.PlayerOneScored();

            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }

        [Test]
        public void TennisGame_WhenPlayersAreTiedWithThreePoints_ScoringTheFourthDoesntWinTheGame()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 0, 3, 0, 0, 3);
            var expectedScore = new [] { 0, 0 };

            // Act
            tennisMatch.PlayerOneScored();

            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());

        }

        [TestCase(5, 4)]
        [TestCase(6, 5)]
        public void TennisGame_WhenPlayerWinsRequredGames_HeWinsASet(int playerOneGames, int playerTwoGames)
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, playerOneGames, 3, 0, playerTwoGames, 0);
            var expectedScore = new [] { 1, 0 };

            // Act
            tennisMatch.PlayerOneScored();

            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
        }

        [Test]
        public void TennisGame_WhenTheSetIsTied_WinningAGameDoesntWinASet()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 5, 3, 0, 5, 0);
            var expectedResult = new[] {0, 0};

            // Act
            tennisMatch.PlayerOneScored();
            // Assert
            Assert.AreEqual(expectedResult, tennisMatch.GetMatchScore());
        }

        [Test]
        public void TennisGame_WhenPlayingTieBreakAndWinningFourPoints_DoesntWinAGame()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 5, 3, 0, 6, 0);
            var expectedResult = new[] { 6, 6 };

            // Act
            tennisMatch.PlayerOneScored();

            // Act
            for (int i = 0; i < 4; i++)
            {
                tennisMatch.PlayerOneScored();
            }
            // Assert
            Assert.AreEqual(expectedResult, tennisMatch.GetSetScore());
        }

        [Test]
        public void TennisGame_WhenPlayingTieBreakAndWinningSevenPoints_WinsAGame()
        {
            // Arrange
            var tennisMatch = CreateTennisMatch(0, 5, 3, 0, 6, 0);
            var expectedResult = new[] { 1, 0 };

            tennisMatch.PlayerOneScored();

            // Act
            for (int i = 0; i < 7; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            // Assert
            Assert.AreEqual(expectedResult, tennisMatch.GetMatchScore());
        }

        private TennisMatch CreateTennisMatch(int playerOneSets, int playerOneGames, int playerOnePoints, int playerTwoSets, int playerTwoGames, int playerTwoPoints)
        {
            var tennisMatch = new TennisMatch("Pawe³", "Tomek", 2);


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