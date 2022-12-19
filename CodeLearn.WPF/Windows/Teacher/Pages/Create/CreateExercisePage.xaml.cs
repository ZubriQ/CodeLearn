using CodeLearn.Db;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeLearn.WPF.Windows.Teacher.Pages.Create
{
    /// <summary>
    /// Interaction logic for CreateExercisePage.xaml
    /// </summary>
    public partial class CreateExercisePage : Page
    {
        #region Properties
        public Exercise Exercise { get; set; } = new Exercise();
        public TestMethodInfo TestMethodInfo { get; set; } = new TestMethodInfo();
        /// <summary>
        /// Method parameters' data types.
        /// </summary>
        public static ObservableCollection<DataType> ParameterDataTypes { get; set; } = new();
        public List<int> Scores { get; set; } = new List<int>();
        public int SelectedScore
        {
            get
            {
                return (int)cb_Score.SelectedValue;
            }
        }
        public WindowSettings WindowSettings { get; set; } = new();
        #endregion

        #region Initialization
        public CreateExercisePage()
        {
            InitializeComponent();
            InitializeInitialData();
            DataContext = this;
        }

        private void InitializeInitialData()
        {
            InitializeComboBoxes();
            InitializeMethodParameterDataTypes();
            InitializeExerciseAndTestMethod();
            InitializeExerciseExampleData();
            InitializeScores();
        }

        private void InitializeScores()
        {
            for (int i = 1; i <= 5; i++)
            {
                Scores.Add(i);
            }
            cb_Score.ItemsSource = Scores;
            cb_Score.SelectedValue = 1;
        }

        void InitializeComboBoxes()
        {
            cbExerciseType.ItemsSource = App.DB.ExerciseTypes;
            cbMethodReturnType.ItemsSource = App.DB.MethodDataTypes;
        }

        void InitializeMethodParameterDataTypes()
        {
            App.DB.InitializeMethodParameterDataTypes(ParameterDataTypes);
        }

        void InitializeExerciseAndTestMethod()
        {
            App.DB.InitializeTestMethodInfo(TestMethodInfo);
            App.DB.InitializeExercise(Exercise, TestMethodInfo);
        }

        void InitializeExerciseExampleData()
        {
            Exercise.Context = @"// example
GetNumber(50);
GetNumber(75.4);
GetNumber(1.333);";
            Exercise.CodingArea = @"// example
public static double GetNumber(double a)
{
    // return a;
}";
        }
        #endregion

        #region Method parameters
        private void btn_AddMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.TestCases.Count > 0)
            {
                if (MessageBox.Show("This action will remove the Test cases.\nContinue?",
                    "Parameter adding", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    TestMethodInfo.TestCases.Clear();
                    TestMethodInfo.TestMethodParameters.Add(new TestMethodParameter());
                }
            }
            else if (TestMethodInfo.TestMethodParameters.Count < 5)
                TestMethodInfo.TestMethodParameters.Add(new TestMethodParameter());
        }

        private void btn_RemoveMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.TestCases.Count > 0)
            {
                if (MessageBox.Show("This action will remove the Test cases.\nContinue?",
                    "Parameter removing", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    TestMethodInfo.TestCases.Clear();
                    TestMethodInfo.TestMethodParameters.Remove(TestMethodInfo.TestMethodParameters.Last());
                }
            }
            else if (TestMethodInfo.TestMethodParameters.Count > 0)
            {
                TestMethodInfo.TestMethodParameters.Remove(TestMethodInfo.TestMethodParameters.Last());
            }
        }
        #endregion

        #region Test Cases
        private void btn_AddTestMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (IsTestCaseAddable())
            {
                var testCase = new TestCase();
                testCase.TestCaseParameters =
                    new TestCaseParameter[TestMethodInfo.TestMethodParameters.Count];
                for (int i = 0; i < testCase.TestCaseParameters.Count; i++)
                {
                    testCase[i] = new TestCaseParameter();
                    testCase[i].Position = i;
                }
                TestMethodInfo.TestCases.Add(testCase);
            }
        }

        private bool IsTestCaseAddable()
        {
            return TestMethodInfo.TestMethodParameters.Count > 0 &&
                   TestMethodInfo.TestCases.Count < 5;
        }

        private void btn_RemoveTestCase(object sender, RoutedEventArgs e)
        {
            var s = sender as Button;
            var contex = s.DataContext;
            if (contex is TestCase)
                TestMethodInfo.TestCases.Remove(contex as TestCase);
        }
        #endregion

        #region Insert and save the exercise into the db
        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            InitializeParametersPositions();
            Exercise.Score = SelectedScore;
            App.DB.SaveExercise(Exercise); // TODO: check entered data
            MessageBox.Show("Exercise has been successfully saved.");
        }

        void InitializeParametersPositions()
        {
            int i = 0;
            foreach (var parameter in TestMethodInfo.TestMethodParameters)
                parameter.Position = i++;
        }
        #endregion

        #region Result placeholders
        private void txt_TestCaseResult_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var bindExpresion = textBox.GetBindingExpression(TextBox.TextProperty);
            var source = bindExpresion.ResolvedSource;
            var propValue = source.GetType()
                                  .GetProperty(bindExpresion.ResolvedSourcePropertyName)
                                  .GetValue(source);
            if (propValue == null)
            {
                textBox.Text = String.Empty;
            }
        }

        private void txt_TestCaseResult_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var bindExpresion = textBox.GetBindingExpression(TextBox.TextProperty);
            var source = bindExpresion.ResolvedSource;

            if (String.IsNullOrEmpty(textBox.Text) && source != null)
            {
                source.GetType()
                      .GetProperty(bindExpresion.ResolvedSourcePropertyName)
                      .SetValue(source, null);
                bindExpresion.UpdateTarget();
            }
        }
        #endregion

        private void cbExerciseType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbExerciseType.SelectedItem as ExerciseType;

            if (item.Name.ToLower() == "class coding")
            {
                txtClassName.IsEnabled = true;
            }
            else
            {
                txtClassName.Text = "TestClass";
                txtClassName.IsEnabled = false;
            }
        }
    }
}
