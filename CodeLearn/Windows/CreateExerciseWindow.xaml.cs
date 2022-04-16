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
    public partial class CreateExerciseWindow : Window
    {
        exercise _exercise = new exercise();

        List<exercise_type> _exerciseTypes; // ComboBox

        List<data_type> _methodDataTypes; // Combobox

        // Method Parameters.
        public static ObservableCollection<data_type> ParameterDataTypes { get; set; } = new ObservableCollection<data_type>();
        public ObservableCollection<test_method_parameters> MethodParameters { get; set; } = new ObservableCollection<test_method_parameters>();

        // Test Case Parameters.
        public ObservableCollection<test_case> TestCases { get; set; } = new ObservableCollection<test_case>();


        public CreateExerciseWindow()
        {
            InitializeComponent();
            InitializeComboBoxes();
            InitializeParamDataTypes();
            DataContext = this;
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

        private void btn_AddMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            if (MethodParameters.Count < 5)
                MethodParameters.Add(new test_method_parameters());
        }

        private void btn_RemoveMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            // TODO: are you sure? 'coz it'll affect the params

            if (MethodParameters.Count > 0)
                MethodParameters.RemoveAt(MethodParameters.Count - 1);
        }

        private void btn_AddTestMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            //if (MethodParameters.Count > 0)
            var tc = new test_case();
            tc.test_case_parameter = new test_case_parameter[MethodParameters.Count];
            TestCases.Add(tc);
        }

        private void btn_RemoveTestMethodParameter_Click(object sender, RoutedEventArgs e)
        {
            //if (TestCases.Count > 0) ;
        }

        private void btn_RemoveTestCase(object sender, RoutedEventArgs e)
        {
            var s = sender as Button;
            var contex = s.DataContext;
            if (contex is test_case)
            {
                TestCases.Remove(contex as test_case);
            }
        }
    }
}
