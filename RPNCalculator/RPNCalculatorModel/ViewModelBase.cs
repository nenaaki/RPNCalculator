using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RPNCalculatorModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetValueProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependencies = null)
            where T : struct, IEquatable<T>
        {
            if (field.Equals(value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            if (dependencies != null)
            {
                foreach (string dependency in dependencies)
                    OnPropertyChanged(dependency);
            }

            return true;
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName]string propertyName = null, string[] dependencies = null)
            where T : class
        {
            if (field == value)
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            if (dependencies != null)
            {
                foreach (string dependency in dependencies)
                    OnPropertyChanged(dependency);
            }

            return true;
        }
    }
}