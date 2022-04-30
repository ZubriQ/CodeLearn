namespace CodeLearn.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
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

        //public test_method_parameters this[int index] 
        //{
        //    get
        //    {
        //        return Enumerable.ElementAt(test_method_parameters, index);
        //    }
        //}
    }

    public partial class test_cases
    {
        public test_case_parameters this[int index]
        {
            get => Enumerable.ElementAt(this.test_case_parameters, index);
            set
            {
                if (this.test_case_parameters is List<test_case_parameters> list)
                {
                    list[index] = value;
                }
                else if (this.test_case_parameters is test_case_parameters[] array)
                {
                    array[index] = value;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}
