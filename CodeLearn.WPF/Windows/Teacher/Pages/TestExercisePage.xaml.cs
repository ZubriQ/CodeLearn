using CodeLearn.Db;
using CodeLearn.Lib;
using System;
using System.Collections.Generic;
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

namespace CodeLearn.WPF.Windows.Teacher.Pages
{
    /// <summary>
    /// Interaction logic for TestExercisePage.xaml
    /// </summary>
    public partial class TestExercisePage : Page
    {
        private CodeManager _codeManager = new();
        private Exercise[]? _exercises;

        public Exercise? Exercise { get; set; }

        public TestExercisePage()
        {
            InitializeComponent();
            InitializeComboBoxData();
            sv_ExerciseTesting.Visibility = Visibility.Collapsed;
        }

        private void InitializeComboBoxData()
        {
            _exercises = App.DB.GetExercises()?.ToArray();
            cb_Method.ItemsSource = _exercises;
        }

        private void txt_MethodName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_MethodName.Text.Length > 3)
            {
                var exercises = _exercises?.Where(e => e.ShortDescription.ToLower()
                    .Contains(txt_MethodName.Text.ToLower()));
                cb_Method.ItemsSource = exercises;
            }
            else
            {
                cb_Method.ItemsSource = _exercises;
            }
        }

        private void cb_Method_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCurrentExercise();
            ChangeWindowState();
        }

        private void ChangeCurrentExercise()
        {
            Exercise = cb_Method.SelectedItem as Exercise;
            DataContext = Exercise;
            txt_Output.Text = "";
        }

        private void ChangeWindowState()
        {
            if (Exercise == null)
            {
                sv_ExerciseTesting.Visibility = Visibility.Collapsed;
            }
            else
            {
                sv_ExerciseTesting.Visibility = Visibility.Visible;
            }
        }

        private void btn_Run_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                TestSelectedExercise();
            }
        }

        private void TestSelectedExercise()
        {
            try
            {
                txt_Output.Text = string.Empty;
                bool success = false;
                if (!string.IsNullOrEmpty(Exercise?.CodingArea))
                {
                    success = _codeManager.CompileAndTestMethod(Exercise.CodingArea, Exercise);
                }
                OutputTestingResult(success);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception");
            }
        }

        private void OutputTestingResult(bool success)
        {
            if (success)
            {
                txt_Output.Text = "All tests have been passed successfully.";
            }
            else
            {
                txt_Output.Text = "The code did not pass.";
            }
        }
    }
}
