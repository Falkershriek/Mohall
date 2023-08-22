using Mohall.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mohall.ViewModels
{
    public class ViewModelMenu : ViewModelBase, IWindowControlCommands
    {
        #region Constructors
        public ViewModelMenu(ViewModelMainWindow menu)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Return to the Mohall main menu.
        /// </summary>
        /// <param name="window">Mohall main window data context.</param>
        public void ReturnToMenu(IWindowDataContext window)
        {
        }
        #endregion

        #region Commands
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
