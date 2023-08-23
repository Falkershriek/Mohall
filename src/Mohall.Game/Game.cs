using Mohall.GameMode.Components;
using Mohall.Statistics;
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
        private readonly GameStatisticsEntry gameStatistics;
        #endregion

        #region Constructors
        public Game(int numberOfGameDoors = 3, string playerName = "")
        {
            CurrentGameStage = GameStage.Stage1;
            GameDoorList = new(numberOfGameDoors);

            gameStatistics = new();
            SetStatisticsPlayerName(playerName);
            SetGameStatisticsToDefault();
        }

        private void SetStatisticsPlayerName(string playerName)
        {
            gameStatistics.PlayerName = playerName;
        }
        #endregion

        #region Properties
        public GameStage CurrentGameStage { get; private set; }
        public GameDoorList GameDoorList { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Write default values into the game statistics entry.
        /// </summary>
        private void SetGameStatisticsToDefault()
        {
            gameStatistics.SimulatedGame = false;
            gameStatistics.PlayerSwapped = false;
            gameStatistics.PlayerWon = false;
            gameStatistics.RewardDoorNumber = GameDoorList.RewardDoorNumber();
            gameStatistics.FirstChosenDoorNumber = -1;
            gameStatistics.FinalChosenDoorNumber = -1;
        }

        /// <summary>
        /// Gets statistics entry of the current game.
        /// </summary>
        /// <returns>Game statistics entry.</returns>
        public IStatisticsEntry GetGameStatistics()
        {
            return gameStatistics;
        }

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
            SetGameStatisticsToDefault();
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
                    gameStatistics.RewardDoorNumber = GameDoorList.RewardDoorNumber();
                    GameDoorList.EnableAllDoors(true);
                    break;
                case GameStage.Stage2:
                    gameStatistics.FirstChosenDoorNumber = GameDoorList.SelectedDoorNumber();
                    GameDoorList.EnableAllDoors(false);
                    break;
                case GameStage.Stage3:
                    GameDoorList.OpenRandomDoor();
                    GameDoorList.EnableAllDoors(true);
                    break;
                case GameStage.Stage4:
                    gameStatistics.FinalChosenDoorNumber = GameDoorList.SelectedDoorNumber();
                    gameStatistics.PlayerSwapped = gameStatistics.FirstChosenDoorNumber != gameStatistics.FinalChosenDoorNumber;
                    GameDoorList.EnableAllDoors(false);
                    break;
                case GameStage.Stage4_1:
                    gameStatistics.PlayerWon = SelectedDoorHasReward();
                    GameDoorList.OpenAllDoors();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
