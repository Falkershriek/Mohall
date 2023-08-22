using Mohall.GameMode;
using Mohall.GameMode.Components;
using Mohall.ViewModels.Commands;
using Mohall.ViewModels.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mohall.ViewModels
{
    public class ViewModelGameMode : ViewModelBase, IWindowControlCommands
    {
        #region Fields
        private readonly ViewModelMainWindow menuContextReference;
        private GameDoorButtonList doorButtonList;
        private readonly Game game;

        /// <summary>
        /// Dictionary of stage-based game directions.
        /// </summary>
        private readonly Dictionary<string, string> gameDirections = new()
        {
            { nameof(GameStage.Stage1), "Pick a door. Behind one of the Doors is a reward. The other two Doors are empty. Press \"Continue\" when you're ready."},
            { nameof(GameStage.Stage2), "From the remaining Doors, I will now open one that contains no reward." },
            { nameof(GameStage.Stage3), "Next, the remaining Doors will be opened. However, before that happens, I will allow you to change your choice. If you want to, you can pick another door. Once you continue, your choice will become final." },
            { nameof(GameStage.Stage4), "Your choice is now set in stone. Let's open the remaining Doors!" },
            { "victory", "Congratulations, you've won the reward!" },
            { "defeat", "You've lost. Better luck next time!" }
        };
        #endregion

        #region Constructors
        public ViewModelGameMode(ViewModelMainWindow menu)
        {
            menuContextReference = menu;
            int numberOfDoors = 3;
            game = new(numberOfDoors);

            DoorButtonList = new(numberOfDoors, game.GameDoorList);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Game directions based on the current game stage.
        /// </summary>
        public string GameDirections
        {
            set
            {
                OnPropertyChanged();
            }
            get
            {
                if (game.CurrentGameStage == GameStage.Stage4_1) return (game.SelectedDoorHasReward()) ? gameDirections["victory"] : gameDirections["defeat"];

                return gameDirections[game.CurrentGameStage.ToString()];
            }
        }

        /// <summary>
        /// List of door buttons.
        /// </summary>
        public GameDoorButtonList DoorButtonList
        {
            get => doorButtonList;
            set { doorButtonList = value; }
        }
        #endregion


        #region Commands
        private ICommand? continueGameCommand;
        public ICommand ContinueGameCommand => continueGameCommand ??= new RelayCommandHandler<string>(ContinueGame);

        /// <summary>
        /// Move the game into the next stage and updates the game directions.
        /// </summary>
        /// <param name="bla"></param>
        public void ContinueGame(string bla)
        {
            game.AdvanceGameStage();
            GameDirections = "";
        }

        private ICommand? selectDoorCommand;
        public ICommand SelectDoorCommand => selectDoorCommand ??= new RelayCommandHandler<string>(SelectDoor);

        /// <summary>
        /// Select the door with the given number.
        /// </summary>
        /// <param name="doorNumber">Number of the door to be selected.</param>
        public void SelectDoor(string doorNumber)
        {
            game.SelectDoor(int.Parse(doorNumber));
        }

        private ICommand? returnToMenuCommand;
        public ICommand ReturnToMenuCommand => returnToMenuCommand ??= new RelayCommandHandler<IWindowDataContext>(ReturnToMenu);

        /// <summary>
        /// Return to the Mohall main menu.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void ReturnToMenu(IWindowDataContext window)
        {
            game.NewGame(); // to be implemented differently later
            window.DataContext = menuContextReference;
        }

        private ICommand? closeWindowCommand;
        public ICommand CloseWindowCommand => closeWindowCommand ??= new RelayCommandHandler<ICloseable>(ExitMohall);

        /// <summary>
        /// Exit the Mohall window.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void ExitMohall(ICloseable window)
        {
            window?.Close();
        }
        #endregion
    }
}
