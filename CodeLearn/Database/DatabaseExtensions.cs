namespace CodeLearn.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    // Exercise type combobox updating.
    public partial class exercise : INotifyPropertyChanged
    {
        [NotMapped]
        public exercise_type ExerciseTypeNotifable
        {
            get
            {
                return exercise_type;
            }
            set
            {
                if (value != exercise_type)
                {
                    exercise_type = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    // TestMethodInfo data_type combobox updating.
    public partial class test_method_info : INotifyPropertyChanged
    {
        [NotMapped]
        public data_type ReturnTypeNotifable
        {
            get
            {
                return data_type;
            }
            set
            {
                if (value != data_type)
                {
                    data_type = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
