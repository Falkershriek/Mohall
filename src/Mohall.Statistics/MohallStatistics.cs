using LiteDB;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Animation;

namespace Mohall.Statistics
{
    /// <summary>
    /// Class for the creation and management of Mohall statistics.
    /// </summary>
    public class MohallStatistics : IMohallStatistics
    {
        #region Fields
        private readonly LiteDatabase statisticsDB;
        private readonly ILiteCollection<IStatisticsEntry> gamesCol;
        private int totalGamesPlayed;
        private int totalGamesPlayedWithSwap;
        private int totalGamesPlayedWithNoSwap;
        private int totalWins;
        private int totalWinsAfterSwap;
        private int totalWinsWithoutSwap;
        private int rewardsBehindDoor1;
        private int rewardsBehindDoor2;
        private int rewardsBehindDoor3;
        private string swapWinRatio;
        private string noSwapWinRatio;
        #endregion

        #region Constructors
        public MohallStatistics()
        {
            statisticsDB = new(@"StatisticsData.db");
            gamesCol = statisticsDB.GetCollection<IStatisticsEntry>("games");

            Initialize();
            UpdateStatistics();
        }

        /// <summary>
        /// Initialize the statistics.
        /// </summary>
        private void Initialize()
        {
            TotalGamesPlayed = 0;
            TotalGamesPlayedWithSwap = 0;
            TotalGamesPlayedWithNoSwap = 0;
            TotalWins = 0;
            TotalWinsAfterSwap = 0;
            TotalWinsWithoutSwap = 0;
            RewardsBehindDoor1 = 0;
            RewardsBehindDoor2 = 0;
            RewardsBehindDoor3 = 0;
            SwapWinRatio = "0:0";
            NoSwapWinRatio = "0:0";
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        public string StatisticsStr
        {
            get => AssembledStatistics();
        }

        public int TotalGamesPlayed
        {
            get => totalGamesPlayed;
            private set { SetField(ref totalGamesPlayed, value); }
        }

        public int TotalGamesPlayedWithSwap
        {
            get => totalGamesPlayedWithSwap;
            private set { SetField(ref totalGamesPlayedWithSwap, value); }
        }

        public int TotalGamesPlayedWithNoSwap
        {
            get => totalGamesPlayedWithNoSwap;
            private set { SetField(ref totalGamesPlayedWithNoSwap, value); }
        }

        public int TotalWins
        {
            get => totalWins;
            private set { SetField(ref totalWins, value); }
        }

        public int TotalWinsAfterSwap
        {
            get => totalWinsAfterSwap;
            private set { SetField(ref totalWinsAfterSwap, value); }
        }

        public int TotalWinsWithoutSwap
        {
            get => totalWinsWithoutSwap;
            private set { SetField(ref totalWinsWithoutSwap, value); }
        }

        public int RewardsBehindDoor1
        {
            get => rewardsBehindDoor1;
            private set { SetField(ref rewardsBehindDoor1, value); }
        }

        public int RewardsBehindDoor2
        {
            get => rewardsBehindDoor2;
            private set { SetField(ref rewardsBehindDoor2, value); }
        }

        public int RewardsBehindDoor3
        {
            get => rewardsBehindDoor3;
            private set { SetField(ref rewardsBehindDoor3, value); }
        }

        public string SwapWinRatio
        {
            get => swapWinRatio;
            private set { SetField(ref swapWinRatio, value); }
        }

        public string NoSwapWinRatio
        {
            get => noSwapWinRatio;
            private set { SetField(ref noSwapWinRatio, value); }
        }
        #endregion

        #region Methods
        public string AssembledStatistics()
        {
            string statistics;
            statistics = "Total games played: " + TotalGamesPlayed.ToString();
            statistics += "\n";
            statistics += "Total games won: " + TotalWins.ToString();
            statistics += "\n";
            statistics += "Total games won after swap: " + TotalWinsAfterSwap.ToString();
            statistics += "\n";
            statistics += "Swap win ratio: " + SwapWinRatio;
            statistics += "\n";
            statistics += "No swap win ratio: " + NoSwapWinRatio;
            statistics  += "\n";
            statistics += "Rewards behind door 1/2/3: " + RewardsBehindDoor1.ToString() + "/" + RewardsBehindDoor2.ToString() + "/" + RewardsBehindDoor3.ToString();

            return statistics;
        }

        /// <summary>
        /// Set the given field to the given value and send a property changed notification using the given property name. If the field and the value are equal, do nothing. 
        /// </summary>
        /// <param name="field">Field to set to the given value.</param>
        /// <param name="value">Value to set the given field to.</param>
        /// <param name="propertyName">Name of the property to use to send a notification.</param>
        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (value == null || value.Equals(field)) return;
            field = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Add the given statistics entry to the statistics database.
        /// </summary>
        /// <param name="entryToAdd">Entry to be added to the statistics.</param>
        public void AddEntry(IStatisticsEntry entryToAdd)
        {
            gamesCol.Insert(entryToAdd);
            UpdateStatistics();
            OnPropertyChanged(nameof(StatisticsStr));
        }

        /// <summary>
        /// Greatest common divisor of the two given numbers.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns></returns>
        private int GreatestCommonDivisor(int a, int b)
        {
            return (b == 0) ? a : GreatestCommonDivisor(b, a % b);
        }

        /// <summary>
        /// Ratio of the two given numbers.
        /// </summary>
        /// <param name="a">First number.</param>
        /// <param name="b">Second number.</param>
        /// <returns></returns>
        private string Ratio(int a, int b)
        {
            int gcd = GreatestCommonDivisor(a, b);
            gcd = (gcd == 0) ? 1 : gcd;
            return (a / gcd).ToString() + ":" + (b / gcd).ToString();
        }

        /// <summary>
        /// Calculate and update the values of the statistics in the database.
        /// </summary>
        public void UpdateStatistics()
        {
            TotalGamesPlayed = gamesCol.Count();
            if (TotalGamesPlayed <= 0) return;

            TotalGamesPlayedWithSwap = gamesCol.Find(x => x.PlayerSwapped).Count();
            TotalGamesPlayedWithNoSwap = TotalGamesPlayed - TotalGamesPlayedWithSwap;
            TotalWins = gamesCol.Find(x => x.PlayerWon).Count();
            TotalWinsAfterSwap = gamesCol.Find(x => x.PlayerWon && x.PlayerSwapped).Count();
            TotalWinsWithoutSwap = TotalWins - TotalWinsAfterSwap;
            SwapWinRatio = Ratio(TotalWinsAfterSwap, TotalGamesPlayedWithSwap);
            NoSwapWinRatio = Ratio(TotalWinsWithoutSwap, TotalGamesPlayedWithNoSwap);
            RewardsBehindDoor1 = gamesCol.Count(x => x.RewardDoorNumber == 1);
            RewardsBehindDoor2 = gamesCol.Count(x => x.RewardDoorNumber == 2);
            RewardsBehindDoor3 = gamesCol.Count(x => x.RewardDoorNumber == 3);
        }
        #endregion
    }
}