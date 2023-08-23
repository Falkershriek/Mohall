using Mohall.Statistics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.GameMode.Components
{
    internal class GameStatisticsEntry : IStatisticsEntry
    {
        #region Properties
        public string PlayerName { get; internal set; } = "unknown";
        public bool SimulatedGame { get; internal set; } = false;
        public bool PlayerSwapped { get; internal set; } = false;
        public bool PlayerWon { get; internal set; } = false;
        public int RewardDoorNumber { get; internal set; } = -1;
        public int FirstChosenDoorNumber { get; internal set; } = -1;
        public int FinalChosenDoorNumber { get; internal set; } = -1;
        #endregion
    }
}
