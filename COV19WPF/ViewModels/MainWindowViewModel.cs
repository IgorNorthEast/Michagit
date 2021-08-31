using System.Net.Mime;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using System.Text;
using COV19WPF.Infrastructure.Commands.Base;
using COV19WPF.Models;

namespace COV19WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
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

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x < 30; x += 0.1)
            {
                const double to_rad = Math.PI / 360;
                var y = Math.Sin(x + to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y});
            }

            TestDataPoints = data_points;
        }
    }
}
