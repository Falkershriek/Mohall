using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace Mohall
{
    public class Statistics
    {
        public Statistics()
        {
            statisticsDB = new(@"StatisticsData.db");
            gamesCol = statisticsDB.GetCollection<GameEntry>("games");
        }
        private readonly LiteDatabase statisticsDB;
        private readonly ILiteCollection<GameEntry> gamesCol;

        public void AddEntry(GameEntry gameToAdd)
        {
            gamesCol.Insert(gameToAdd);
        }

        public int TotalGamesPlayed()
        {
            return gamesCol.Count();
        }

        public int TotalGamesWon()
        { 
            return gamesCol.Find(x => x.PlayerWon).Count();
        }

        public int TotalGamesWonAfterSwap()
        {
            return gamesCol.Find(x => x.PlayerWon && x.PlayerSwapped).Count();
        }

        public int RewardsBehindDoor1()
        {
            return gamesCol.Find(x => x.RewardDoor == 1).Count();
        }

        public int RewardsBehindDoor2()
        {
            return gamesCol.Find(x => x.RewardDoor == 2).Count();
        }

        public int RewardsBehindDoor3()
        {
            return gamesCol.Find(x => x.RewardDoor == 3).Count();
        }
    }

    public class GameEntry
    {
        public int Id { get; set; }
        public string? PlayerName { get; set; }
        public bool SimulatedGame { get; set; }
        public bool PlayerSwapped { get; set; }
        public bool PlayerWon { get; set; }
        public int RewardDoor { get; set; }
        public int FirstChoice { get; set; }
        public int FinalChoice { get; set; }

    }
}
