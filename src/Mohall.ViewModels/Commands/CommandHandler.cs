using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Mohall.ViewModels.Commands
{
    /// <summary>
    /// Decides whether the given action can be executed.
    /// </summary>
    public class CommandHandler : ICommand
    {
        private Action action;
        private bool canExecute;

        public CommandHandler(Action action, bool canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return canExecute;
        }

        public event EventHandler? CanExecuteChanged;

        public void Execute(object? parameter)
        {
            action();
        }
    }
}
