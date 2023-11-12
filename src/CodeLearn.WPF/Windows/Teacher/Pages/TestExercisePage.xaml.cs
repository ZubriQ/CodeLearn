using CodeLearn.Db;
using CodeLearn.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TestExercisePage : Page, INotifyPropertyChanged
    {
        private CodeManager _codeManager = new();
        private Exercise[]? _exercises;
        private Exercise? _exercise = null;

        public static WindowSettings WindowSettings { get; set; } = new();
        public Exercise Exercise
        {
            get => _exercise;
            set
            {
                if (value == _exercise)
                {
                    return;
                }
                _exercise = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TestExercisePage()
        {
            InitializeComponent();
            InitializeComboBoxData();
            sv_ExerciseTesting.Visibility = Visibility.Collapsed;
            DataContext = this;
        }

        private void InitializeComboBoxData()
        {
            _exercises = App.DB.GetExercises()?.ToArray();
            scb_SearchComboBox.ItemsSource = _exercises;
        }

        private void sb_SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sb_SearchBar.SearchText.Length > 3)
            {
                var exercises = _exercises?.Where(e => e.ShortDescription.ToLower()
                    .Contains(sb_SearchBar.SearchText.ToLower()));
                scb_SearchComboBox.ItemsSource = exercises;
            }
            else
            {
                scb_SearchComboBox.ItemsSource = _exercises;
            }
        }

        private void scb_SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeCurrentExercise();
            ChangeWindowState();
        }

        private void ChangeCurrentExercise()
        {
            Exercise = scb_SearchComboBox.SelectedItem as Exercise;
            txt_Output.Text = " . . . ";
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
                txt_Output.Text = "All tests passed successfully.";
            }
            else
            {
                txt_Output.Text = "Tests failed.";
            }
        }
    }
}
