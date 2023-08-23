using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.Statistics
{
    /// <summary>
    /// Interface for single statistics entries representing individual games or simulations.
    /// </summary>
    public interface IStatisticsEntry
    {
        public string PlayerName { get; }
        public bool SimulatedGame { get; }
        public bool PlayerSwapped { get; }
        public bool PlayerWon { get; }
        public int RewardDoorNumber { get; }
        public int FirstChosenDoorNumber { get; }
        public int FinalChosenDoorNumber { get; }
    }
}
