using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyTiecCuoi.MVVM.Model
{
    public class IDatTiecServices : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public IDatTiecServices(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged;
    }

}
