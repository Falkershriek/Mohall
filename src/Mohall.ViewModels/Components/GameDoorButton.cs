using Mohall.GameMode.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.ViewModels.Components
{
    /// <summary>
    /// States of the door buttons.
    /// </summary>
    public enum DoorButtonState
    {
        Selected,
        LockedSelected,
        LockedDefault,
        Empty,
        Wrong,
        Missed,
        Correct,
        Default
    }

    /// <summary>
    /// Contains the elements and functionalities of a Mohall door button. 
    /// </summary>
    public class GameDoorButton : INotifyPropertyChanged
    {
        #region Fields
        private int doorNumber;
        private string doorButtonStyle;
        private DoorButtonState doorState;
        private IGameDoor gameDoor;
        #endregion

        #region Constructors
        public GameDoorButton(int doorNumber, IGameDoor gameDoor)
        {
            this.gameDoor = gameDoor;
            DoorNumber = doorNumber;

            Initialize();
        }

        /// <summary>
        /// Initialize the door button.
        /// </summary>
        private void Initialize()
        {
            SetDoorState();
            gameDoor.PropertyChanged += (sender, e) =>
            {
                SetDoorState();
            };
        }
        #endregion

        #region Events
        /// <summary>
        /// Event to be invoked when a property changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invoke the PropertyChanged event, telling the View to update its contents.
        /// </summary>
        /// <param name="propertyName">Name of the changed property</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Properties
        /// <summary>
        /// Number of the door button.
        /// </summary>
        public int DoorNumber
        {
            get => doorNumber;
            private set
            {
                if (value == doorNumber) return;
                doorNumber = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// State of the door button.
        /// </summary>
        private DoorButtonState DoorButtonState
        {
            get => doorState;
            set
            {
                if (value == doorState) return;
                doorState = value;
                DoorButtonStyle = Enum.GetName(value) + "DoorButton";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Style of the door button.
        /// </summary>
        public string DoorButtonStyle
        {
            get => doorButtonStyle;
            private set
            {
                if (value == doorButtonStyle) return;
                doorButtonStyle = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set the door state depending on whether it's selected, enabled, open and whether it has a reward.
        /// </summary>
        private void SetDoorState()
        {
            if (gameDoor.IsOpen)
            {
                if (gameDoor.IsSelected) DoorButtonState = (gameDoor.HasReward) ? Components.DoorButtonState.Correct : Components.DoorButtonState.Wrong;

                else DoorButtonState = (gameDoor.HasReward) ? Components.DoorButtonState.Missed : Components.DoorButtonState.Empty;
            }
            else
            {
                if (gameDoor.IsSelected) DoorButtonState = (gameDoor.IsEnabled) ? Components.DoorButtonState.Selected : Components.DoorButtonState.LockedSelected;

                else DoorButtonState = (gameDoor.IsEnabled) ? Components.DoorButtonState.Default : Components.DoorButtonState.LockedDefault;
            }
        }
        #endregion
    }
}
