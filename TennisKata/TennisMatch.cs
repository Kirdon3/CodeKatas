using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
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

        private void PlayerScored(Player scorer, Player opponent)
        {
            scorer.gameScore += 1;

            if (ScorerWonAGame(scorer, opponent))
            {
                scorer.setScore += 1;
                StartNewGame(scorer, opponent);

                if (scorer.setScore >= 6)
                {
                    scorer.matchScore += 1;

                    if (scorer.matchScore == setsToWin)
                    {
                        MatchFinished = true;
                    }
                }
            }
        }

        private void StartNewGame(Player scorer, Player opponent)
        {
            scorer.gameScore = 0;
            opponent.gameScore = 0;
        }

        private bool ScorerWonAGame(Player scorer, Player opponent)
        {
            return scorer.gameScore > 3 && scorer.gameScore > opponent.gameScore + 1;
        }

        public void PlayerOneScored()
        {
            PlayerScored(playerOne, playerTwo);
        }

        public void PlayerTwoScored()
        {
            PlayerScored(playerTwo, playerOne);

        }

        public int[] GetGameScore()
        {
            int[] score = { playerOne.gameScore,  playerTwo.gameScore };
            return score;
        }
        public int[] GetSetScore()
        {
            int[] score = { playerOne.setScore, playerTwo.setScore };
            return score;
        }
        public int[] GetMatchScore()
        {
            int[] score = { playerOne.matchScore, playerTwo.matchScore };
            return score;
        }
    }
    [TestFixture]
    public class TennisGameTests
    {
        [Test]
        public void TennisGame_WhenStartingAGame_MatchScoreIsZeroToZero()
        {
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);
            var expectedScore = new [] {0, 0};
            
            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
        }
        [Test]
        public void TennisGame_WhenStartingAGame_SetScoreIsZeroToZero()
        {
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);
            var expectedScore = new [] { 0, 0 };

            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());
        }
        [Test]
        public void TennisGame_WhenStartingAGame_GameScoreIsZeroToZero()
        {
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);
            var expectedScore = new [] { 0, 0 };

            Assert.AreEqual(expectedScore, tennisMatch.GetGameScore());
        }

        [Test]
        public void TennisGame_AfterPlayerOneScores_HisScoreIsIncreased()
        {
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);
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
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);
            var expectedScore = new [] { 0, 0 };

            for (int i = 0; i < 3; i++)
            {
                tennisMatch.PlayerOneScored();
                tennisMatch.PlayerTwoScored();
            }

            tennisMatch.PlayerOneScored();

            Assert.AreEqual(expectedScore, tennisMatch.GetSetScore());

        }

        [Test]
        public void TennisGame_WhenPlayerWinsSixGames_HeWinsASet()
        {
            var tennisMatch = new TennisMatch("Paweł", "Tomek", 2);
            var expectedScore = new [] { 1, 0 };

            for (int i = 0; i < 4 * 5+3; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            tennisMatch.PlayerOneScored();

            Assert.AreEqual(expectedScore, tennisMatch.GetMatchScore());
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
