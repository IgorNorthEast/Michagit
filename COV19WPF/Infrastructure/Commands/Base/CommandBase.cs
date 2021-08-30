using System;
using System.Windows.Input;

namespace COV19WPF.Infrastructure.Commands.Base
{
    internal class CommandBase : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public CommandBase(Action execute)
            : this(execute, null)
        { }

        public CommandBase(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute is null.");

            this.execute = execute;
            this.canExecute = canExecute;
            this.RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
            SimpleCommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
        }

        ~CommandBase()
        {
            RemoveCommand();
        }

        public void RemoveCommand()
        {
            SimpleCommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute;
        }

        public void Execute(object parameter)
        {
            execute();
            SimpleCommandManager.RefreshCommandStates();
        }

        public bool CanExecute
        {
            get { return canExecute == null || canExecute(); }
        }

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        private readonly Action RaiseCanExecuteChangedAction;

        public event EventHandler CanExecuteChanged;
    }
}