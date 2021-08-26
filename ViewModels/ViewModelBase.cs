using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace Cov19Test.ViewModels
{
    // internal abstract class ViewModelBase : ReactiveObject
    // {

    // }
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if(Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
    }
}
