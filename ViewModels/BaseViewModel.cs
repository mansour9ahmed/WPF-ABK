using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new Dictionary<string, List<string>>();

        public bool HasErrors => _errorsByPropertyName.Any();

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ? _errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public void AddError(string propertyName,string errorMessage)
        {
            if(!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }

            if (!_errorsByPropertyName[propertyName].Contains(errorMessage))
            {
                _errorsByPropertyName[propertyName].Add(errorMessage);
                OnErrorChanged(propertyName);
            }
        }

        public void ClearErrors(string propertyName)
        {
            if(_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorChanged(propertyName);
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetValue<T>(ref T prop, T value,[CallerMemberName] string name = null)
        {
            if(!object.Equals(prop,value))
            {
                prop = value;
                RaisePropertyChanged(name);
            }
        }
    }
}
