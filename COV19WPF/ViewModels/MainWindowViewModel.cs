using System.Reflection.Metadata;
using System.Net.Mime;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Text;
//using COV19WPF.Infrastructure.Commands.Base;
using COV19WPF.Models;
using ReactiveUI;
using System.Reactive;

namespace COV19WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region SelectedPageIndex

        /// <summary> Int index of selected page </summary>
        private int _SelectedPageIndex;
        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

        #region TestDataPoint

        /// <summary> Test data collection for graphics visualization </summary>
        private IEnumerable<DataPoint> _TestDataPoints;
        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _TestDataPoints;
            set => Set(ref _TestDataPoints, value);
        }

        #endregion


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

        #region CloseApplicationCommand

        public ReactiveCommand<Unit, Unit> OnCloseApplicationCommand => ReactiveCommand.Create(OnCloseApplicationCommandExecuted);
        private void OnCloseApplicationCommandExecuted()
        {
            if (Avalonia.Application.Current.ApplicationLifetime is Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
            {
                try
                {
                    desktop.Shutdown();
                }
                catch (System.Exception)
                {

                }
            }
        }

        // private Command _CloseApplicationCommand;
        // public ICommand CloseApplicationCommand => Command.Create(ref _CloseApplicationCommand, OnCloseApplicationCommandExecuted);

        #endregion

        #region ChangeIndexTabCommand

        // private Command<string> _ChangeTabIndexCommand;
        // public ICommand ChangeTabIndexCommand => Command.Create(ref _ChangeTabIndexCommand, ChangeTabIndex, CanChangeTabIndexCommandExecute);

        private ReactiveCommand<object, Unit> ChangeTabIndexCommand => ReactiveCommand.Create<object>(ChangeTabIndex, CanChangeTabIndexCommand);
        IObservable<bool> CanChangeTabIndexCommand => this.WhenAny(vm => vm.SelectedPageIndex, p1 => CanChangeTabIndexCommandExecute());
        private void ChangeTabIndex(object p)
        {
            if (p is null) return;
            SelectedPageIndex += Convert.ToInt32(p);
        }
        private bool CanChangeTabIndexCommandExecute() => _SelectedPageIndex >= 0;
        #endregion

        public MainWindowViewModel()
        {

            #region Commands

            //ChangeTabIndexCommand = ReactiveCommand.Create<object>(ChangeTabIndex, CanChangeTabIndexCommand);

            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 360;
                var y = Math.Sin(2 * Math.PI * x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }
    }
}
