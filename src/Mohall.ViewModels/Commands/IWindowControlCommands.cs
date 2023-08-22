using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mohall.ViewModels.Commands
{
    public interface IWindowControlCommands
    {
        public void ReturnToMenu(IWindowDataContext window);

        public void ExitMohall(ICloseable window);
    }
}
