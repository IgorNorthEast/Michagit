// using System;
// using System.Windows.Input;
// namespace COV19WPF.Infrastructure.Commands.Base
// {
//      public class Command : CommandBase
//     {
//         public static ICommand Create(ref Command command, Action execute)
//         {
//             return command ?? (command = new Command(execute));
//         }

//         public static ICommand Create(ref Command command, Action execute, Func<bool> canExecute)
//         {
//             return command ?? (command = new Command(execute, canExecute));
//         }

//         public static ICommand Create<TParam>(ref Command<TParam> command, Action<TParam> execute)
//         {
//             return command ?? (command = new Command<TParam>(execute));
//         }

//         public static ICommand Create<TParam>(ref Command<TParam> command, Action<TParam> execute, Predicate<TParam> canExecute)
//         {
//             return command ?? (command = new Command<TParam>(execute, canExecute));
//         }

//         private Action _execute;
//         private Func<bool> _canExecute;

//         private Command(Action execute, Func<bool> canExecute)
//         {
//             _canExecute = canExecute;
//             _execute = execute;
//         }

//         private Command(Action execute) : this(execute, null)
//         {
//         }

//         public override bool CanExecute(object parameter)
//         {
//             return _canExecute == null ? true : _canExecute();
//         }

//         public override void Execute(object parameter)
//         {
//             _execute();
//         }

//         public override void Dispose()
//         {
//             _canExecute = null;
//             _execute = null;
//         }
//     }


//     public class Command<T> : CommandBase
//     {
//         private Action<T> _execute;
//         private Predicate<T> _canExecute;

//         internal Command(Action<T> execute, Predicate<T> canExecute)
//         {
//             _canExecute = canExecute;
//             _execute = execute;
//         }

//         internal Command(Action<T> execute) : this(execute, null)
//         {
//         }

//         public override bool CanExecute(object parameter)
//         {
//             return _canExecute == null ? true : _canExecute((T)parameter);
//         }

//         public override void Execute(object parameter)
//         {
//             _execute?.Invoke((T)parameter);
//         }

//         public override void Dispose()
//         {
//             _canExecute = null;
//             _execute = null;
//         }
//     }

//     public abstract class CommandBase : ICommand, IDisposable
//     {
//         public event EventHandler CanExecuteChanged
//         {
//             add => CommandManager.RequerySuggested += value;
//             remove => CommandManager.RequerySuggested -= value;
//         }

//         public abstract bool CanExecute(object parameter);

//         public abstract void Execute(object parameter);

//         public abstract void Dispose();
//     }
// }
//     internal class CommandBase : ICommand
//     {
//         private readonly Action execute;
//         private readonly Func<bool> canExecute;

//         public CommandBase(Action execute)
//             : this(execute, null)
//         { }

//         public CommandBase(Action execute, Func<bool> canExecute)
//         {
//             if (execute == null)
//                 throw new ArgumentNullException("execute is null.");

//             this.execute = execute;
//             this.canExecute = canExecute;
//             this.RaiseCanExecuteChangedAction = RaiseCanExecuteChanged;
//             SimpleCommandManager.AddRaiseCanExecuteChangedAction(ref RaiseCanExecuteChangedAction);
//         }

//         ~CommandBase()
//         {
//             RemoveCommand();
//         }

//         public void RemoveCommand()
//         {
//             SimpleCommandManager.RemoveRaiseCanExecuteChangedAction(RaiseCanExecuteChangedAction);
//         }

//         bool ICommand.CanExecute(object parameter)
//         {
//             return CanExecute;
//         }

//         public void Execute(object parameter)
//         {
//             execute();
//             SimpleCommandManager.RefreshCommandStates();
//         }

//         public bool CanExecute
//         {
//             get { return canExecute == null || canExecute(); }
//         }

//         public void RaiseCanExecuteChanged()
//         {
//             var handler = CanExecuteChanged;
//             if (handler != null)
//             {
//                 handler(this, new EventArgs());
//             }
//         }

//         private readonly Action RaiseCanExecuteChangedAction;

//         public event EventHandler CanExecuteChanged;
//     }

// }