using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CodeLearn.Db
{
    /// <summary>
    /// Exercise type combobox updating.
    /// </summary>
    public partial class Exercise : INotifyPropertyChanged
    {
        [NotMapped]
        public ExerciseType ExerciseTypeNotifiable
        {
            get
            {
                return ExerciseType;
            }
            set
            {
                if (value != ExerciseType)
                {
                    ExerciseType = value;
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

    /// <summary>
    /// TestMethodInfo data_type combobox updating.
    /// </summary>
    public partial class TestMethodInfo : INotifyPropertyChanged
    {
        [NotMapped]
        public DataType ReturnTypeNotifiable
        {
            get
            {
                return ReturnType;
            }
            set
            {
                if (value != ReturnType)
                {
                    ReturnType = value;
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

    public partial class TestCase
    {
        public TestCaseParameter this[int index]
        {
            get => Enumerable.ElementAt(this.TestCaseParameters, index);
            set
            {
                if (this.TestCaseParameters is List<TestCaseParameter> list)
                {
                    list[index] = value;
                }
                else if (this.TestCaseParameters is TestCaseParameter[] array)
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
