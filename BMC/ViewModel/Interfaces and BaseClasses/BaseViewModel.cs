using BMC.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BMC
{
    public abstract class BaseViewModel : INotifyPropertyChanged, ICommonData
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual DataClass GetControlData() { return null; }
        public virtual DataClass GetPowerData() { return null; }
    }
}
