using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FakinReader.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields
        private bool _isBusy = false;
        private string _title = string.Empty;
        #endregion Fields

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion Events

        #region Properties
        public bool IsBusy
        {
            get { return _isBusy; }

            set { SetProperty(ref _isBusy, value); }
        }
        public string Title
        {
            get { return _title; }

            set { SetProperty(ref _title, value); }
        }
        #endregion Properties

        #region Methods
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;

            if (changed == null)
            {
                return;
            }

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyName);

            return true;
        }
        #endregion Methods
    }
}