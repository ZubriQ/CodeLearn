using CodeLearn.Database;
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
using System.Windows.Shapes;

namespace CodeLearn.Windows
{
    public partial class CreateExercise_Window : Window
    {
        List<exercise_type> _exerciseTypes; // ComboBox
        List<data_type> _methodDataTypes; // Combobox

        public exercise Exercise { get; set; }
        public test_method_info TestMethodInfo { get; set; }

        // Method parameters' types.
        public static ObservableCollection<data_type> ParameterDataTypes { get; set; } = new ObservableCollection<data_type>();

        public CreateExercise_Window()
        {
            InitializeComponent();
            InitializeComboBoxes();
            InitializeExercise();
            InitializeParamDataTypes();
            DataContext = this;
        }

        #region Initialization
        void InitializeExercise()
        {
            Exercise = new exercise(); // Exercise.
            Exercise.exercise_type_id = 1;
            Exercise.exercise_type = App.DB.exercise_type.First(et => et.name == "Method coding");
            Exercise.class_name = "TestClass";
            TestMethodInfo = new test_method_info(); // Method & method parameters.
            TestMethodInfo.name = "TestMethod";
            TestMethodInfo.test_method_parameters = new ObservableCollection<test_method_parameters>();
            TestMethodInfo.return_type_id = 2;
            TestMethodInfo.data_type = App.DB.data_type.First(d => d.name == "Void");
            TestMethodInfo.test_cases = new ObservableCollection<test_cases>(); // Method's Test cases.
            Exercise.test_method_info.Add(TestMethodInfo);
        }

        void InitializeComboBoxes()
        {
            _exerciseTypes = App.DB.exercise_type.ToList();
            cbExerciseType.ItemsSource = _exerciseTypes;
            _methodDataTypes = App.DB.data_type.ToList();
            cbMethodReturnType.ItemsSource = _methodDataTypes;
        }

        void InitializeParamDataTypes()
        {
            var types = App.DB.data_type.Where(x => x.name != "Void").ToArray();
            for (int i = 0; i < types.Length; i++)
                ParameterDataTypes.Add(types[i]);
        }
        #endregion

        // Method parameters.
        private void btn_AddMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.test_cases.Count > 0)
            {
                if (MessageBox.Show("This action will remove the Test cases.\nContinue?",
                    "Parameter adding", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    TestMethodInfo.test_cases.Clear();
                    TestMethodInfo.test_method_parameters.Add(new test_method_parameters());
                }
            }
            else if (TestMethodInfo.test_method_parameters.Count < 5)
                TestMethodInfo.test_method_parameters.Add(new test_method_parameters());
        }

        private void btn_RemoveMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.test_cases.Count > 0)
            {
                if (MessageBox.Show("This action will remove the Test cases.\nContinue?",
                    "Parameter removing", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    TestMethodInfo.test_cases.Clear();
                    TestMethodInfo.test_method_parameters.Remove(
                        TestMethodInfo.test_method_parameters.LastOrDefault());
                }
            }
            else if (TestMethodInfo.test_method_parameters.Count > 0)
            {
                TestMethodInfo.test_method_parameters.Remove(
                            TestMethodInfo.test_method_parameters.LastOrDefault());
            }    
        }

        // Test Cases.
        private void btn_AddTestMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.test_method_parameters.Count > 0)
            {
                var tc = new test_cases();
                tc.test_case_parameters = new test_case_parameters[TestMethodInfo.test_method_parameters.Count];
                for (int i = 0; i < tc.test_case_parameters.Count; i++)
                {
                    tc[i] = new test_case_parameters();
                    tc[i].position = i;
                }
                TestMethodInfo.test_cases.Add(tc);
            }
        }

        private void btn_RemoveTestCase(object sender, RoutedEventArgs e)
        {
            var s = sender as Button;
            var contex = s.DataContext;
            if (contex is test_cases)
                TestMethodInfo.test_cases.Remove(contex as test_cases);
        }

        //private void txt_TestCaseResult_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    // TODO: placeholders
        //    //var s = sender as TextBox;
        //    //if (!string.IsNullOrWhiteSpace(s.Text))
        //    //{
        //    //    //MessageBox.Show(s.);
        //    //}
        //}

        #region Add an exercise into the db.
        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            AddExercise();
        }

        void AddExercise()
        {
            InitializeParametersPositions();
            //InitializeTestParametersPositions();
            App.DB.exercises.Add(Exercise);
            App.DB.SaveChanges();
            MessageBox.Show("Exercise has been successfully added and saved.");
        }

        void InitializeParametersPositions()
        {
            int i = 0;
            foreach (var parameter in TestMethodInfo.test_method_parameters)
                parameter.position = i++;
        }

        //void InitializeTestParametersPositions()
        //{
        //    foreach (var tc in TestMethodInfo.test_case)
        //    {
        //        int i = 0;
        //        foreach (var parameter in tc.test_case_parameter)
        //        {
        //            //parameter.position = i++;
        //        }
        //    }
        //}
        #endregion


        // Result Placeholders.
        private void txt_TestCaseResult_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            var bindExpresion = textBox.GetBindingExpression(TextBox.TextProperty);
            var source = bindExpresion.ResolvedSource;
            var propValue = source.GetType().GetProperty(bindExpresion.ResolvedSourcePropertyName).GetValue(source);
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

           
            if (String.IsNullOrEmpty(textBox.Text))
            {

                source.GetType().GetProperty(bindExpresion.ResolvedSourcePropertyName).SetValue(source, null);
                bindExpresion.UpdateTarget();
            }
        }
    }
}
