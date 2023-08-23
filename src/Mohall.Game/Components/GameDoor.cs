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
    /// Contains the elements and functionalities of a Mohall door.
    /// </summary>
    public class GameDoor : IGameDoor
    {
        #region Fields
        private bool isSelected;
        private bool hasReward;
        private bool isOpen;
        private bool isEnabled;
        #endregion

        #region Constructors
        public GameDoor()
        {
            Initialize();
        }

        /// <summary>
        /// Initialize the door's settings.
        /// </summary>
        private void Initialize()
        {
            isOpen = false;
            isEnabled = true;
            isSelected = false;
            hasReward = false;
        }
        #endregion

        #region Events
        public event EventHandler PropertyChanged;

        protected virtual void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The door is selected.
        /// </summary>
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                if (isSelected == value) return;
                if (!isOpen && isEnabled) isSelected = value; OnPropertyChanged();
            }
        }

        /// <summary>
        /// The door has the reward.
        /// </summary>
        public bool HasReward
        {
            get => hasReward;
            set { hasReward = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// The door is open.
        /// </summary>
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                if (isOpen == value) return;
                isOpen = value; isEnabled = !value; OnPropertyChanged();
            }
        }

        /// <summary>
        /// The door is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                if (isEnabled == value) return;
                if (!isOpen) { isEnabled = value; OnPropertyChanged(); }
            }
        }
        #endregion

        #region methods
        /// <summary>
        /// Reset the door.
        /// </summary>
        internal void Reset()
        {
            IsOpen = false;
            //IsEnabled = true;
            IsSelected = false;
            HasReward = false;
        }

        /// <summary>
        /// Toggle the selection status of the door.
        /// </summary>
        internal void ToggleSelection()
        {
            IsSelected = !IsSelected;
        }
        #endregion
    }
}
