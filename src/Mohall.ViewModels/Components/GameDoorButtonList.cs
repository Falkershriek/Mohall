using Mohall.GameMode.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mohall.ViewModels.Components
{
    /// <summary>
    /// Contains a list of door buttons.
    /// </summary>
    public class GameDoorButtonList : ObservableCollection<GameDoorButton>
    {
        #region Initializers
        public GameDoorButtonList(int gameDoorNumber, List<GameDoor> gameDoorList)
        {
            Initialize(gameDoorList);
        }

        /// <summary>
        /// Initialize the door button list.
        /// </summary>
        /// <param name="gameDoorList">List of game doors the door button list is to be based on.</param>
        public void Initialize(List<GameDoor> gameDoorList)
        {
            AddGameDoorButtons(gameDoorList);
        }
        #endregion

        #region Methods
        /// <summary>
        /// For each game door, add a door buttons to the list.
        /// </summary>
        /// <param name="gameDoorList">List of game doors the door button list is to be based on.</param>
        public void AddGameDoorButtons(List<GameDoor> gameDoorList)
        {
            int doorNumber = 1;
            foreach(IGameDoor gameDoor in gameDoorList)
            {
                Add(new GameDoorButton(doorNumber, gameDoor));
                doorNumber++;
            }
        }
        #endregion
    }
}
