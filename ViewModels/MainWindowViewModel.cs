using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
