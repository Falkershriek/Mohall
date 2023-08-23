using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.GameMode.Components
{
    /// <summary>
    /// Contains a list of GameDoors.
    /// </summary>
    public class GameDoorList : List<GameDoor>
    {
        #region Constructors
        public GameDoorList(int numberOfDoors = 3)
        {
            if (numberOfDoors < 3) numberOfDoors = 3;
            Initialize(numberOfDoors);
        }

        /// <summary>
        /// Initialize the GameDoorList.
        /// </summary>
        /// <param name="numberOfDoors"></param>
        private void Initialize(int numberOfDoors = 3)
        {
            for (int i = 1; i <= numberOfDoors; i++)
            {
                Add(new GameDoor());
            }
            RandomlyAssignReward();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Find the number of the selected door.
        /// </summary>
        /// <returns>Door number, starting from 1. Returns -1 if no match is found.</returns>
        internal int SelectedDoorNumber()
        {
            return FindDoorNumber(door => door.IsSelected);
        }

        /// <summary>
        /// Find the number of the door with the reward.
        /// </summary>
        /// <returns>Door number, starting from 1. Returns -1 if no match is found.</returns>
        internal int RewardDoorNumber()
        {
            return FindDoorNumber(door => door.HasReward);
        }

        /// <summary>
        /// Find the number of the door matching the given condition.
        /// </summary>
        /// <param name="match">Condition which the door must match.</param>
        /// <returns>Door number, starting from 1. Returns -1 if no match is found.</returns>
        internal int FindDoorNumber(Predicate<GameDoor> match)
        {
            int doorNumber = this.FindIndex(match);
            return (doorNumber >= 0) ? doorNumber + 1 : -1;
        }

        /// <summary>
        /// Enable all doors if true, disable all doors if false.
        /// </summary>
        /// <param name="enableValue"></param>
        internal void EnableAllDoors(bool enableValue = true)
        {
            foreach (GameDoor door in this) door.IsEnabled = enableValue;
        }

        /// <summary>
        /// Open all doors.
        /// </summary>
        internal void OpenAllDoors()
        {
            foreach (GameDoor door in this) door.IsOpen = true;
        }

        /// <summary>
        /// Deselect all doors.
        /// </summary>
        internal void DeselectAllDoors()
        {
            foreach (GameDoor door in this) door.IsSelected = false;
        }

        /// <summary>
        /// Reset all doors.
        /// </summary>
        internal void ResetAllDoors()
        {
            foreach (GameDoor door in this) door.Reset();
            RandomlyAssignReward();
        }

        /// <summary>
        /// Select the given door and deselects all other doors.
        /// </summary>
        /// <param name="doorNumber">The door to select.</param>
        internal void SelectDoor(int doorNumber)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].IsSelected = (i == doorNumber - 1);
            }
        }

        /// <summary>
        /// Open the given door.
        /// </summary>
        /// <param name="doorNumber">The door to open.</param>
        internal void OpenDoor(int doorNumber)
        {
            this[doorNumber - 1].IsOpen = true;
        }

        /// <summary>
        /// Find all empty doors in the given list.
        /// </summary>
        /// <returns>List of empty doors.</returns>
        private List<GameDoor> GetEmptyDoors(List<GameDoor> gameDoors)
        {
            return gameDoors.FindAll(door => !door.HasReward);
        }

        /// <summary>
        /// Find all unselected doors in the given list.
        /// </summary>
        /// <returns>List of unselected doors.</returns>
        private List<GameDoor> GetUnselectedDoors(List<GameDoor> gameDoors)
        {
            return gameDoors.FindAll(door => !door.IsSelected);
        }

        /// <summary>
        /// Randomly assign reward to one of the doors.
        /// </summary>
        private void RandomlyAssignReward()
        {
            Random rnd = new();
            this[rnd.Next(0, this.Count)].HasReward = true;
        }

        /// <summary>
        /// Randomly open one empty, unselected door.
        /// </summary>
        internal void OpenRandomDoor()
        {
            List<GameDoor> emptyUnselectedDoors = GetEmptyDoors(GetUnselectedDoors(this));

            if (emptyUnselectedDoors.Count == 0) throw new Exception("No empty, unselected doors were found!");

            Random rnd = new();
            GameDoor randomDoor = emptyUnselectedDoors[rnd.Next(0, emptyUnselectedDoors.Count)];

            randomDoor.IsOpen = true;
        }
        #endregion
    }
}
