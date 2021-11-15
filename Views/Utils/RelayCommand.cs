using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Utils
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _handler;
        private Func<bool> _func;

        public RelayCommand(Action<T> handler, Func<bool> func = null)
        {
            _handler = handler;
            _func = func;
        }


        public bool CanExecute(object parameter)
        {
            if (_func != null)
                return _func();

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler((T)parameter);
        }

        public void RaiseCanExcuteChange()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _handler;
        private Func<bool> _func;

        public RelayCommand(Action handler, Func<bool> func = null)
        {
            _handler = handler;
            _func = func;
        }



        public bool CanExecute(object parameter)
        {
            if (_func != null)
                return _func();

            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }

        public void RaiseCanExcuteChange()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

    }
}
