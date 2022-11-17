using System.Collections.ObjectModel;

namespace CodeLearn.Db.WPF
{
    public class WPFDatabaseProvider
    {
        private CodeLearnContext _context = new();

        /// <summary>
        /// Implemented for CreateExerciseWindow's combobox.
        /// </summary>
        public List<ExerciseType> ExerciseTypes { get; set; }

        /// <summary>
        /// Implemented for CreateExerciseWindow's combobox.
        /// </summary>
        public List<DataType> MethodDataTypes { get; set; }

        public WPFDatabaseProvider()
        {
            ExerciseTypes = _context.ExerciseTypes.ToList();
            MethodDataTypes = _context.DataTypes.ToList();
        }

        #region Initializing default data methods for CreateExerciseWindow
        public void InitializeMethodParameterDataTypes(ObservableCollection<DataType> dataTypes)
        {
            var items = _context.DataTypes.Where(x => x.Name != "Void").ToArray();
            for (int i = 0; i < items.Length; i++)
                dataTypes.Add(items[i]);
        }

        public void InitializeTestMethodInfo(TestMethodInfo testMethod)
        {
            testMethod.Name = "TestMethod";
            testMethod.TestMethodParameters = new ObservableCollection<TestMethodParameter>();
            testMethod.ReturnType = _context.DataTypes.First(dt => dt.Name == "Void");
            testMethod.TestCases = new ObservableCollection<TestCase>();
        }

        public void InitializeExercise(Exercise exercise, TestMethodInfo testMethod)
        {
            exercise.ExerciseType = _context.ExerciseTypes.First(et => et.Name == "Method coding");
            exercise.ClassName = "TestClass";
            exercise.TestMethodInfos.Add(testMethod);
        }
        #endregion

        #region Access methods
        public void SaveExercise(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
        }

        public Exercise GetTestExercise()
        {
            return _context.Exercises.First(s => s.Id == 1002);
        }
        #endregion
    }
}