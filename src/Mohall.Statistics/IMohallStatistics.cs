using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.Statistics
{
    /// <summary>
    /// Interface for total game/simulation statistics.
    /// </summary>
    public interface IMohallStatistics : INotifyPropertyChanged
    {
        #region Properties
        public int TotalGamesPlayed { get; }
        public int TotalGamesPlayedWithSwap { get; }
        public int TotalGamesPlayedWithNoSwap { get; }
        public int TotalWins { get; }
        public int TotalWinsAfterSwap { get; }
        public int TotalWinsWithoutSwap { get; }
        public int RewardsBehindDoor1 { get; }
        public int RewardsBehindDoor2 { get; }
        public int RewardsBehindDoor3 { get; }
        public string SwapWinRatio { get; }
        public string NoSwapWinRatio { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Add the given statistics entry to the statistics database.
        /// </summary>
        /// <param name="entryToAdd">Entry to be added to the statistics.</param>
        public void AddEntry(IStatisticsEntry entryToAdd);

        /// <summary>
        /// Calculate and update the values of the statistics in the database.
        /// </summary>
        public void UpdateStatistics();
        #endregion
    }
}
