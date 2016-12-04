using NUnit.Framework;

namespace TennisKata
{
    [TestFixture]
    public class TennisGameTests
    {
        private readonly string playerOneName = "John";
        private readonly string playerTwoName = "Tonny";
        private TennisGameTestFixture testFixture;

        [SetUp]
        public void SetUp()
        {
            this.testFixture = new TennisGameTestFixture();
        }

        [Test]
        public void TennisGame_WhenStartingAGame_MatchScoreIsZeroToZero()
        {
            // Arrange
            var tennisMatch = testFixture.WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).Create()).Create();
            var expectedScore = new [] {0, 0};
            
            // Act
            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
        }

        [Test]
        public void TennisGame_WhenStartingAGame_SetScoreIsZeroToZero()
        {
            // Arrange
            var tennisMatch = testFixture.WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).Create()).Create();
            var expectedScore = new [] { 0, 0 };

            // Act
            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }

        [Test]
        public void TennisGame_WhenStartingAGame_GameScoreIsZeroToZero()
        {
            // Arrange
            var tennisMatch = testFixture.WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).Create()).Create();
            var expectedScore = new [] { 0, 0 };

            // Act
            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetGameScore());
        }

        [Test]
        public void TennisGame_AfterPlayerOneScores_HisScoreIsIncreased()
        {
            // Arrange
            var tennisMatch = testFixture.WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).Create()).Create();
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

            var tennisMatch = testFixture.WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).WithPoints(3).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).Create()).Create();
            // Act
            tennisMatch.PlayerOneScored();

            // Assert
            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }

        [Test]
        public void TennisGame_WhenPlayersAreTiedWithThreePoints_ScoringTheFourthDoesntWinTheGame()
        {
            // Arrange
            var tennisMatch = testFixture.WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).WithPoints(3).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).WithPoints(3).Create()).Create();

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
            var tennisMatch = testFixture
                .WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).WithPoints(3).WithGames(playerOneGames).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).WithGames(playerTwoGames).Create()).Create();
            
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
            var tennisMatch = testFixture
                .WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).WithPoints(3).WithGames(5).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).WithGames(5).Create()).Create();
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
            var tennisMatch = testFixture
                .WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).WithPoints(3).WithGames(5).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).WithGames(6).Create()).Create();
            
            var expectedResult = new[] { 6, 6 };

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
            var tennisMatch = testFixture
                .WithPlayerOne(testFixture.BuildPlayer().WithName(playerOneName).WithPoints(3).WithGames(5).Create())
                .WithPlayerTwo(testFixture.BuildPlayer().WithName(playerTwoName).WithGames(6).Create()).Create();
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
    }
}