using CodeLearn.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeLearn.WPF.Windows
{
    public class WindowSettings : INotifyPropertyChanged
    {
        #region Fields and Properties
        private int _elementsMargin;

        public int ElementsMargin
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
            ElementsMargin = 3;
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
