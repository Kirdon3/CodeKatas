using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisKata
{
    public class TennisMatch
    {
        public TennisMatch(string playerOne, string playerTwo)
        {
            this.playerOne = new Player(playerOne);
            this.playerTwo = new Player(playerTwo); ;
        }

        private Player playerOne { get; set; }
        private Player playerTwo { get; set; }

        public bool PlayerOneScored()
        {
            playerOne.AddPoint(p2.)
            return false;
        }

        public bool PlayerTwoScored()
        {
            return false;            
        }
    }

    public class TennisGameTests
    {
        public void TennisGame_WhenStartingAGame_AGameIsCreated()
        {
            
        }
    }
}
