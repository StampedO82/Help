using System;
using System.Windows.Input;

namespace AuxiliaryFeature.GUI
{
    public class DelegateCommand<T> : ICommand
    {
        #region Fields

        private readonly System.Predicate<T> _canExecute;
        private readonly System.Action<T> _execute;

        #endregion

        #region Constructors

        public DelegateCommand(System.Action<T> execute)
            : this(execute, null) { }

        public DelegateCommand(System.Action<T> execute, System.Predicate<T> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        #endregion

        #region ICommand implementation

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }

    public class DelegateCommand : ICommand
    {
        #region Fields

        private readonly Func<bool> _canExecute;
        private readonly System.Action _execute;

        #endregion

        #region Constructors

        public DelegateCommand(System.Action execute)
            : this(execute, null) { }

        public DelegateCommand(System.Action execute, Func<bool> canExecuteMethod)
        {
            this._execute = execute;
            this._canExecute = canExecuteMethod;
        }

        #endregion

        #region ICommand implementation

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        #endregion
    }
}
