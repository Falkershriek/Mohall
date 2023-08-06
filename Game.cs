using static Mohall.Game;

namespace Mohall
{
    /// <summary>
    /// Contains the logic of the Mohall game.
    /// </summary>
    public class Game
    {
        public Game()
        {
            NewGame();
            UpdateGame();
        }

        /// <summary>
        /// Statistics database entry for the current game.
        /// </summary>
        private readonly GameEntry gameEntry = new();

        public GameEntry GameEntry { get { return gameEntry; } }

        /// <summary>
        /// Contains values for the possible game stages.
        /// </summary>
        public enum GameStage
        {
            Stage0,
            Stage1,
            Stage2,
            Stage3,
            Stage4,
            Stage5,
        }

        /// <summary>
        /// Contains value of the current game stage.
        /// </summary>
        public GameStage CurrentGameStage { get; private set; }

        /// <summary>
        /// List of doors to be used in the game.
        /// </summary>
        public List<Door> Doors = new();

        /// <summary>
        /// Number of doors in the game.
        /// </summary>
        public int NumberOfDoors
        {
            get
            {
                return Doors.Count;
            }
        }

        /// <summary>
        /// Initializes a new game.
        /// </summary>
        /// <param name="nOfDoors">Number of doors to be created in the new game.</param>
        public void NewGame(int nOfDoors = 3)
        {
            if (nOfDoors < 3) nOfDoors = 3;

            Doors = new();
            CurrentGameStage = GameStage.Stage0;

            while (nOfDoors > 0)
            {
                Doors.Add(new Door());
                nOfDoors--;
            }

            RandomlyAssignReward();
        }

        /// <summary>
        /// Advances the game to the next stage. Once the final stage is reached, wraps back to stage zero.
        /// </summary>
        public void AdvanceGameStage()
        {
            if (CurrentGameStage < GameStage.Stage5)
            {
                CurrentGameStage++;
            }
            else
            {
                NewGame();
            }
            UpdateGame();
        }

        /// <summary>
        /// Updates the game's elements in accordance with the current game stage.
        /// </summary>
        private void UpdateGame()
        {
            switch (CurrentGameStage)
            {
                case GameStage.Stage0:
                    NewGame();
                    gameEntry.RewardDoor = RewardDoorNumber();
                    EnableAllDoors(true);
                    break;
                case GameStage.Stage1:
                    break;
                case GameStage.Stage2:
                    gameEntry.FirstChoice = SelectedDoorNumber();
                    EnableAllDoors(false);
                    break;
                case GameStage.Stage3:
                    RandomlyOpenEmptyUnselectedDoor();
                    EnableAllDoors(true);
                    break;
                case GameStage.Stage4:
                    gameEntry.FinalChoice = SelectedDoorNumber();
                    gameEntry.PlayerSwapped = gameEntry.FirstChoice != gameEntry.FinalChoice;
                    EnableAllDoors(false);
                    break;
                case GameStage.Stage5:
                    gameEntry.PlayerWon = DidPlayerWin();
                    OpenAllDoors();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Game directions specific to the current game stage.
        /// </summary>
        /// <returns>String with game directions for the current stage.</returns>
        public string GetCurrentGameDirections()
        {
            return CurrentGameStage switch
            {
                GameStage.Stage0 => "Pick a door. Behind one of the Doors is a reward. The other two Doors are empty.",
                GameStage.Stage1 => "You can change your choice if you wish. Press \"Continue\" when you're ready.",
                GameStage.Stage2 => "From the remaining Doors, I will now open one that contains no reward.",
                GameStage.Stage3 => "Next, the remaining Doors will be opened. However, before that happens, I will allow you to change your choice. If you want to, you can pick another door. Once you continue, your choice will become final.",
                GameStage.Stage4 => "Your choice is now set in stone. Let's open the remaining Doors!",
                GameStage.Stage5 => (DidPlayerWin()) ? "Congratulations, you've won the reward!" : "You've lost. Better luck next time!",
                _ => "Something went wrong",
            };
        }

        /// <summary>
        /// Finds the currently selected door.
        /// </summary>
        /// <returns>Number of the currently selected door. -1 if no door is selected.</returns>
        public int SelectedDoorNumber()
        {
            int doorIndex = Doors.FindIndex(door => door.IsSelected);
            return (doorIndex >= 0) ? doorIndex + 1 : -1;
        }

        /// <summary>
        /// Finds the door with the reward.
        /// </summary>
        /// <returns>Number of the door with the reward. -1 if no door has a reward.</returns>
        public int RewardDoorNumber()
        {
            int doorIndex = Doors.FindIndex(door => door.HasReward);
            return (doorIndex >= 0) ? doorIndex + 1 : -1;
        }

        /// <summary>
        /// Checks whether the player selected a door with a reward.
        /// </summary>
        /// <returns>True if player selected a door with a reward, false if player selected an empty door.</returns>
        private bool DidPlayerWin()
        {
            return SelectedDoorNumber() == RewardDoorNumber();
        }

        /// <summary>
        /// Randomly assigns reward to one of the Doors.
        /// </summary>
        private void RandomlyAssignReward()
        {
            Random rnd = new();
            Door randomDoor = Doors[rnd.Next(0, NumberOfDoors)];

            randomDoor.HasReward = true;
        }

        /// <summary>
        /// Enables all Doors if true, disables all closed Doors if false. Only works on closed Doors (see DoorBtn.IsEnabled).
        /// </summary>
        /// <param name="enableValue">Value to set each door's "Enabled" parameter to.</param>
        private void EnableAllDoors(bool enableValue = true)
        {
            foreach (Door door in Doors) door.IsEnabled = enableValue;
        }

        /// <summary>
        /// Randomly opens one empty, unselected door.
        /// </summary>
        private void RandomlyOpenEmptyUnselectedDoor()
        {
            Random rnd = new();
            Door currDoor = Doors[rnd.Next(0, NumberOfDoors)];

            while (currDoor.IsSelected || currDoor.IsOpen || currDoor.HasReward)
            {
                currDoor = Doors[rnd.Next(0, NumberOfDoors)];
            }

            currDoor.IsOpen = true;
        }

        /// <summary>
        /// Opens all Doors.
        /// </summary>
        private void OpenAllDoors()
        {
            foreach (Door doorBtn in Doors) doorBtn.IsOpen = true;
        }

        /// <summary>
        /// Selects the given door and deselects all other Doors.
        /// </summary>
        /// <param name="doorBtn">The door to select.</param>
        public void SelectDoor(int doorIndex)
        {
            DeselectAllDoors();
            Doors[doorIndex].IsSelected = true;
        }

        /// <summary>
        /// Deselects all Doors.
        /// </summary>
        private void DeselectAllDoors()
        {
            foreach (Door doorBtn in Doors) doorBtn.IsSelected = false;
        }

        /// <summary>
        /// Contains the values of the in-game Doors.
        /// </summary>
        public class Door
        {
            public Door()
            {
                Initialize();
            }

            private bool isSelected;
            private bool hasReward;
            private bool isOpen;
            private bool isEnabled;

            /// <summary>
            /// This door is selected. Can only be set if the door is not currently open.
            /// </summary>
            public bool IsSelected
            {
                get { return isSelected; }
                set
                {
                    if (!IsOpen)
                    {
                        isSelected = value;
                    }
                }
            }

            /// <summary>
            /// This door has the reward.
            /// </summary>
            public bool HasReward
            {
                get { return hasReward; }
                set { hasReward = value; }
            }

            /// <summary>
            /// This door is open.
            /// </summary>
            public bool IsOpen
            {
                get { return isOpen; }
                set { isOpen = value; }
            }

            /// <summary>
            /// This door is enabled. Can only be set if the door is not currently open.
            /// </summary>
            public bool IsEnabled
            {
                get { return isEnabled; }
                set { isEnabled = value; }
            }

            /// <summary>
            /// Toggle the IsSelected status of the door.
            /// </summary>
            public void ToggleSelection()
            {
                IsSelected = !IsSelected;
            }

            /// <summary>
            /// Initialize the door's settings.
            /// </summary>
            public void Initialize()
            {
                IsOpen = false;
                IsEnabled = true;
                IsSelected = false;
                HasReward = false;
            }
        }
    }
}
