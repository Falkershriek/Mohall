using Mohall.Statistics;
using Mohall.ViewModels.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace Mohall.ViewModels
{
    public class ViewModelMainWindow : ViewModelBase, IWindowControlCommands
    {
        #region Fields
        #endregion

        #region Constructors
        public ViewModelMainWindow()
        {
            Menu = new(this);
            GameMode = new(this);
            Statistics = new(this);
        }
        #endregion

        #region Properties
        public ViewModelMenu Menu { get; set; }
        public ViewModelGameMode GameMode { get; set; }
        public ViewModelStatistics Statistics { get; set; }
        #endregion

        #region Commands
        private ICommand? openStatisticsCommand;
        public ICommand OpenStatisticsCommand => openStatisticsCommand ??= new RelayCommandHandler<IWindowDataContext>(OpenStatistics);

        /// <summary>
        /// Launch the Mohall game window.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void OpenStatistics(IWindowDataContext window)
        {
            window.DataContext = Statistics;
        }

        private ICommand? playMohallCommand;
        public ICommand PlayMohallCommand => playMohallCommand ??= new RelayCommandHandler<IWindowDataContext>(PlayMohall);

        /// <summary>
        /// Launch the Mohall game window.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void PlayMohall(IWindowDataContext window)
        {
            window.DataContext = GameMode;
        }

        private ICommand? returnToMenuCommand;
        public ICommand ReturnToMenuCommand => returnToMenuCommand ??= new RelayCommandHandler<IWindowDataContext>(ReturnToMenu);

        /// <summary>
        /// Return to the Mohall main menu.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void ReturnToMenu(IWindowDataContext window)
        {
            window.DataContext = this;
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
