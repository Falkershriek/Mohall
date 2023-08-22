using Mohall.GameMode.Components;
using System;
using System.Windows.Documents;

namespace Mohall.GameMode
{
    /// <summary>
    /// Mohall game stages.
    /// </summary>
    public enum GameStage
    {
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage4_1,
    }

    /// <summary>
    /// Contains the Mohall game functionality.
    /// </summary>
    public class Game
    {
        #region Fields

        #endregion

        #region Constructors
        public Game(int numberOfGameDoors = 3)
        {
            Initialize(numberOfGameDoors);
        }

        /// <summary>
        /// Initialize the game.
        /// </summary>
        /// <param name="numberOfGameDoors">Number of doors to create in the game.</param>
        private void Initialize(int numberOfGameDoors = 3)
        {
            CurrentGameStage = GameStage.Stage1;
            GameDoorList = new(numberOfGameDoors);
        }
        #endregion

        #region Properties
        public GameStage CurrentGameStage { get; private set; }
        public GameDoorList GameDoorList { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Select the door with the given door number.
        /// </summary>
        /// <param name="doorNumber"></param>
        public void SelectDoor(int doorNumber)
        {
            GameDoorList.SelectDoor(doorNumber);
        }

        /// <summary>
        /// Check whether the selected door has the reward.
        /// </summary>
        /// <returns>true if the selected door has the reward, false otherwise.</returns>
        public bool SelectedDoorHasReward()
        {
            return GameDoorList.RewardDoorNumber() == GameDoorList.SelectedDoorNumber();
        }

        /// <summary>
        /// Advance the game to the next stage. If the final stage was already reached, wrap back to the first stage.
        /// </summary>
        public void AdvanceGameStage()
        {
            if (GameDoorList.SelectedDoorNumber() == -1) return;
            if (CurrentGameStage < GameStage.Stage4_1) CurrentGameStage++;
            else NewGame();
            UpdateGame();
        }

        /// <summary>
        /// Start a news game.
        /// </summary>
        public void NewGame()
        {
            CurrentGameStage = GameStage.Stage1;
            GameDoorList.ResetAllDoors();
        }

        /// <summary>
        /// Update the game in accordance with the current game stage.
        /// </summary>
        private void UpdateGame()
        {
            switch (CurrentGameStage)
            {
                case GameStage.Stage1:
                    NewGame();
                    //gameEntry.RewardDoor = RewardDoorNumber();
                    GameDoorList.EnableAllDoors(true);
                    break;
                case GameStage.Stage2:
                    //gameEntry.FirstChoice = SelectedDoorNumber();
                    GameDoorList.EnableAllDoors(false);
                    break;
                case GameStage.Stage3:
                    GameDoorList.OpenRandomDoor();
                    GameDoorList.EnableAllDoors(true);
                    break;
                case GameStage.Stage4:
                    //gameEntry.FinalChoice = SelectedDoorNumber();
                    //gameEntry.PlayerSwapped = gameEntry.FirstChoice != gameEntry.FinalChoice;
                    GameDoorList.EnableAllDoors(false);
                    break;
                case GameStage.Stage4_1:
                    //gameEntry.SelectedDoorHasReward = DidPlayerWin();
                    GameDoorList.OpenAllDoors();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
