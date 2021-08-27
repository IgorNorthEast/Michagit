using System.Net.Mime;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Cov19Test.Infrastructure.Commands.Base;

namespace Cov19Test.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Window Title
        /// <summary> Window title </summary>
        private string _Title = "COVID-19 Analisys";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Window status
        /// <summary> Window status </summary>
        private string _Status = "Ready!";
        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }
        #endregion

        #region Command

        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted()
        {
            if (Avalonia.Application.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Shutdown();
            }
        }
        private bool CanCloseApplicationCommandExecute() => true;

        #endregion

        public MainWindowViewModel()
        {

            #region Commands

            CloseApplicationCommand = new CommandBase(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            #endregion
        }
    }
}
