using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeLearn.Db
{
    #region WPF Create Exercise
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
    #endregion

    #region DataGrid WPF output
    public partial class TestingResult
    {
        public int AnswersCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < TestingAnswers.Count; i++)
                {
                    count++;
                }
                return count;
            }
        }

        public int CorrectAnswersCount
        {
            get
            {
                int count = 0;
                foreach (var answer in TestingAnswers)
                {
                    if (answer.IsCorrect)
                    {
                        count++;
                    }
                }
                return count;
            }
        }
    }

    public partial class Student
    {
        public string FullName
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(LastName);
                sb.Append(' ');
                sb.Append(FirstName);
                sb.Append(' ');
                if (!string.IsNullOrEmpty(Patronymic))
                {
                    sb.Append(Patronymic);
                }
                return sb.ToString();
            }
        }

        public string Initials
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(LastName);
                sb.Append(' ');
                sb.Append(FirstName[0]);
                sb.Append('.');
                if (!string.IsNullOrEmpty(Patronymic))
                {
                    sb.Append(Patronymic[0]);
                    sb.Append('.');
                }
                return sb.ToString();
            }
        }
    }
    #endregion

    #region WPF Create Testing
    /// <summary>
    /// Exercise type combobox updating.
    /// </summary>
    public partial class Testing : INotifyPropertyChanged
    {
        private ICollection<Exercise> _exercisesNotifiable;

        [NotMapped]
        public ICollection<Exercise> ExercisesNotifiable
        {
            get
            {
                return _exercisesNotifiable;
            }
            set
            {
                if (value != _exercisesNotifiable)
                {
                    _exercisesNotifiable = value;
                    Exercises.Clear();
                    foreach (var exercise in _exercisesNotifiable)
                    {
                        Exercises.Add(exercise);
                    }
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

    public class ExerciseViewModel : INotifyPropertyChanged
    {
        private Exercise _selectedExercise;
        public Exercise SelectedExercise
        {
            get { return _selectedExercise; }
            set
            {
                if (_selectedExercise != value)
                {
                    _selectedExercise = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion
}
