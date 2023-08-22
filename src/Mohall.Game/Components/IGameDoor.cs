using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.GameMode.Components
{
    /// <summary>
    /// Interface for accessing door data externally.
    /// </summary>
    public interface IGameDoor
    {
        #region Events
        public event EventHandler PropertyChanged;

        protected virtual void OnPropertyChanged() { }
        #endregion

        #region Properties
        /// <summary>
        /// This door is selected.
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// This door has the reward.
        /// </summary>
        public bool HasReward { get; }

        /// <summary>
        /// This door is open.
        /// </summary>
        public bool IsOpen { get; }

        /// <summary>
        /// This door is enabled.
        /// </summary>
        public bool IsEnabled { get; }
        #endregion
    }
}
