using Mohall.GameMode;
using Mohall.Statistics;
using Mohall.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mohall.ViewModels
{
    public class ViewModelStatistics : ViewModelBase, IWindowControlCommands
    {
        #region Fields
        private readonly ViewModelMainWindow menuContextReference;
        #endregion

        #region Constructors
        public ViewModelStatistics(ViewModelMainWindow menu)
        {
            menuContextReference = menu;
            GlobalStatistics = new();
        }
        #endregion

        #region Properties
        public MohallStatistics GlobalStatistics { get; internal set; }
        #endregion

        #region Commands
        private ICommand? returnToMenuCommand;
        public ICommand ReturnToMenuCommand => returnToMenuCommand ??= new RelayCommandHandler<IWindowDataContext>(ReturnToMenu);

        /// <summary>
        /// Return to the Mohall main menu.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void ReturnToMenu(IWindowDataContext window)
        {
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
