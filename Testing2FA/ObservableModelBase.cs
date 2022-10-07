using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Testing2FA
{
    public class ObservableModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void HandlePropertyChanged<T>(
            ref T field, 
            T value,
            Action? alsoDo = null,
            [CallerMemberName] string? propertyName = null)
        {
            if ((field == null && value != null) ||
                field?.Equals(value) == false)
            {
                field = value;

                OnPropertyChanged(propertyName);

                alsoDo?.Invoke();
            }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
