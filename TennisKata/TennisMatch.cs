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
}
