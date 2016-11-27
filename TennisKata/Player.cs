using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisKata
{
    class Player
    {
        public string playerName { get; set; }

        public int gameScore { get; set; }

        public int setScore { get; set; }

        public int matchScore { get; set; }



        public Player(string playerName)
        {
            this.playerName = playerName;
        }
    }
}
