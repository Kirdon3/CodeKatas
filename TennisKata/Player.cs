using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisKata
{
    class Player
    {
        public string PlayerName { get; set; }

        public int CurrentGameScore { get; set; }

        public int CurrentSetScore { get; set; }

        public int CurrentMatchScore { get; set; }



        public Player(string playerName)
        {
            this.PlayerName = playerName;
        }
    }
}
