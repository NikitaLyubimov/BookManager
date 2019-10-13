using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propeprtyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propeprtyName));
        }

        protected virtual bool SetProperty<T>(ref T source, T newValue, [CallerMemberName] string proeprtyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(source, newValue))
                return false;
            else
            {
                source = newValue;
                OnPropertyChanged(proeprtyName);
                return true;
            }
        }
    }
}
