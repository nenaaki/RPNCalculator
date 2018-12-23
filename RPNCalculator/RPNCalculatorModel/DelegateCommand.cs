using System;
using System.Windows.Input;

namespace RPNCalculatorModel
{
    public class DelegateCommand : ICommand
    {
        private Func<object, bool> _canExecute;

        private Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter)
        {
            _execute?.Invoke(parameter);
            OnCanExecuteChanged();
        }

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}