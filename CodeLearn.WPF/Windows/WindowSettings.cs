using CodeLearn.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeLearn.WPF.Windows
{
    public class WindowSettings : INotifyPropertyChanged
    {
        #region Fields and Properties
        private Thickness _elementsMargin;

        public Thickness ElementsMargin
        {
            get { return _elementsMargin; }
            set
            {
                if (value != _elementsMargin)
                {
                    _elementsMargin = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public WindowSettings()
        {
            ElementsMargin = new Thickness(4, 4, 4, 4);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
