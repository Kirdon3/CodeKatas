using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisKata
{
    public class TennisGameTestFixture
    {
        private Player playerOne;
        private Player playerTwo;
        private int setsToWin;


        public PlayerBuilder BuildPlayer()
        {
            return new PlayerBuilder();
        }

        public TennisGameTestFixture WithPlayerOne(Player player)
        {
            this.playerOne = player;
            return this;
        }

        public TennisGameTestFixture WithPlayerTwo(Player player)
        {
            this.playerTwo = player;
            return this;
        }

        public TennisGameTestFixture WithSetsToWin(int numberOfSets)
        {
            this.setsToWin = numberOfSets;
            return this;
        }

        public TennisMatch Create()
        {
            var tennisMatch = SetUpTennisMatchScore();

            return tennisMatch;
        }

        private TennisMatch SetUpTennisMatchScore()
        {
            var tennisMatch = new TennisMatch(playerOne.PlayerName, playerTwo.PlayerName, setsToWin);


            this.SetUpMatchScores(tennisMatch);
            this.SetUpSetScores(tennisMatch);
            this.SetUpGameScores(tennisMatch);

            return tennisMatch;
        }

        private void SetUpMatchScores(TennisMatch tennisMatch)
        {
            for (int i = 0; i < playerOne.CurrentMatchScore * 4 * 6; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            for (int i = 0; i < playerTwo.CurrentMatchScore * 4 * 6; i++)
            {
                tennisMatch.PlayerTwoScored();
            }
        }

        private void SetUpSetScores(TennisMatch tennisMatch)
        {
            for (int i = 0; i < playerOne.CurrentSetScore * 4; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            for (int i = 0; i < playerTwo.CurrentSetScore * 4; i++)
            {
                tennisMatch.PlayerTwoScored();
            }
        }

        private void SetUpGameScores(TennisMatch tennisMatch)
        {
            for (int i = 0; i < playerOne.CurrentGameScore; i++)
            {
                tennisMatch.PlayerOneScored();
            }

            for (int i = 0; i < playerTwo.CurrentGameScore; i++)
            {
                tennisMatch.PlayerTwoScored();
            }
        }
    }
}
