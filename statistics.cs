using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Mohall
{
    /// <summary>
    /// Class for the creation and management of the game statistics data(base).
    /// </summary>
    public class Statistics
    {
        public Statistics()
        {
            statisticsDB = new(@"StatisticsData.db");
            gamesCol = statisticsDB.GetCollection<GameEntry>("games");
            UpdateStatistics();
        }
        private readonly LiteDatabase statisticsDB;
        private readonly ILiteCollection<GameEntry> gamesCol;
        public int TotalGamesPlayed { get; private set; } = 0;
        public int TotalGamesPlayedWithSwap { get; private set; } = 0;
        public int TotalGamesPlayedWithNoSwap { get; private set; } = 0;
        public int TotalWins { get; private set; } = 0;
        public int TotalWinsAfterSwap { get; private set; } = 0;
        public int TotalWinsWithoutSwap { get; private set; } = 0;
        public int RewardsBehindDoor1 { get; private set; } = 0;
        public int RewardsBehindDoor2 { get; private set; } = 0;
        public int RewardsBehindDoor3 { get; private set; } = 0;
        public string SwapWinRatio { get; private set; } = "0:0";
        public string NoSwapWinRatio { get; private set; } = "0:0";

        /// <summary>
        /// Adds the given GameEntry to the game statistics.
        /// </summary>
        /// <param name="gameToAdd">Entry to be added to the statistics.</param>
        public void AddEntry(GameEntry gameToAdd)
        {
            gamesCol.Insert(gameToAdd);
        }

        private int Gcd(int a, int b)
        {
            return (b == 0) ? a : Gcd(b, a % b);
        }

        private string Ratio(int a, int b)
        {
            int gcd = Gcd(a, b);
            return (a / gcd).ToString() + ":" + (b / gcd).ToString();
        }

        /// <summary>
        /// Calculates and updates the values of the statistics in the database.
        /// </summary>
        public void UpdateStatistics()
        {
            TotalGamesPlayed = gamesCol.Count();
            if (TotalGamesPlayed > 0)
            {
                TotalGamesPlayedWithSwap = gamesCol.Find(x => x.PlayerSwapped).Count();
                TotalGamesPlayedWithNoSwap = TotalGamesPlayed - TotalGamesPlayedWithSwap;
                TotalWins = gamesCol.Find(x => x.PlayerWon).Count();
                TotalWinsAfterSwap = gamesCol.Find(x => x.PlayerWon && x.PlayerSwapped).Count();
                TotalWinsWithoutSwap = TotalWins - TotalWinsAfterSwap;
                SwapWinRatio = Ratio(TotalWinsAfterSwap, TotalGamesPlayedWithSwap);
                NoSwapWinRatio = Ratio(TotalWinsWithoutSwap, TotalGamesPlayedWithNoSwap);
                RewardsBehindDoor1 = gamesCol.Find(x => x.RewardDoor == 1).Count();
                RewardsBehindDoor2 = gamesCol.Find(x => x.RewardDoor == 2).Count();
                RewardsBehindDoor3 = gamesCol.Find(x => x.RewardDoor == 3).Count();
            }
        }
    }

    /// <summary>
    /// Class representing a single game entry in the statistics database.
    /// </summary>
    public class GameEntry
    {
        public GameEntry()
        {
            SimulatedGame = false;
            PlayerSwapped = false;
            PlayerWon = false;
            RewardDoor = -1;
            FirstChoice = -1;
            FinalChoice = -1;
        }

        public string? PlayerName { get; set; }
        public bool SimulatedGame { get; set; }
        public bool PlayerSwapped { get; set; }
        public bool PlayerWon { get; set; }
        public int RewardDoor { get; set; }
        public int FirstChoice { get; set; }
        public int FinalChoice { get; set; }

    }
}
