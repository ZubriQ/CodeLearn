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

        void InitializeExercise()
        {
            Exercise = new exercise();
            // Test data.
            Exercise.exercise_description = "Test description.";
            Exercise.exercise_type_id = 1;
            Exercise.exercise_type = App.DB.exercise_type.First( et => et.name == "Method coding");
            Exercise.context = "context info";
            Exercise.context_description = "test context description";
            Exercise.class_name = "TestClass";
            // Test data.
            TestMethodInfo = new test_method_info();
            TestMethodInfo.name = "TestMethod";
            TestMethodInfo.test_method_parameters = new ObservableCollection<test_method_parameters>();
            TestMethodInfo.data_type = App.DB.data_type.First( d => d.name == "Void");
            TestMethodInfo.return_type_id = 2;

            TestMethodInfo.test_case = new ObservableCollection<test_case>();
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

        // Method parameters.
        private void btn_AddMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.test_method_parameters.Count < 5)
                TestMethodInfo.test_method_parameters.Add(new test_method_parameters());
        }

        private void btn_RemoveMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            // TODO: are you sure? because it'll affect the params.
            if (TestMethodInfo.test_method_parameters.Count > 0)
                TestMethodInfo.test_method_parameters.Remove(
                    TestMethodInfo.test_method_parameters.LastOrDefault());
        }

        // Test Cases.
        private void btn_AddTestMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (TestMethodInfo.test_method_parameters.Count > 0)
            {
                var tc = new test_case();
                tc.test_case_parameter = new test_case_parameter[TestMethodInfo.test_method_parameters.Count];
                TestMethodInfo.test_case.Add(tc);
            }
        }

        private void btn_RemoveTestCase(object sender, RoutedEventArgs e)
        {
            var s = sender as Button;
            var contex = s.DataContext;
            if (contex is test_case)
                TestMethodInfo.test_case.Remove(contex as test_case);
        }

        private void txt_TestCaseResult_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TODO: placeholders
            var s = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(s.Text))
            {
                //MessageBox.Show(s.);
            }
        }
    }
}
