using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RollHelper
{
    class RelayCommand : ICommand
    {
        Action<object> ExecuteAction;
        Func<object, bool> CanExecuteFunc;

        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            ExecuteAction = execute;
            CanExecuteFunc = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            if (CanExecuteFunc != null)
                return CanExecuteFunc(parameter);
            return true;
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAction(parameter);
        }
    }
}
