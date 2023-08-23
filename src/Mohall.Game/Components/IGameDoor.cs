using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.GameMode.Components
{
    /// <summary>
    /// Interface for accessing door data externally.
    /// </summary>
    public interface IGameDoor : INotifyPropertyChanged
    {
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
