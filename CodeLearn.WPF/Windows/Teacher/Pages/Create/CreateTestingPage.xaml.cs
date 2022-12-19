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
    /// Interaction logic for CreateTestingPage.xaml
    /// </summary>
    public partial class CreateTestingPage : Page
    {
        private CreateTestingSettings _settings;

        #region Properties
        public Testing Testing { get; set; } = new Testing();
        public static ObservableCollection<Exercise> AvailableExercises { get; set; } = new();
        public List<int> Duration { get; set; } = new List<int>();
        public int SelectedDuration
        {
            get
            {
                return (int)cb_Duration.SelectedItem;
            }
        }
        public WindowSettings WindowSettings { get; set; } = new();
        #endregion

        #region Initialization
        public CreateTestingPage()
        {
            InitializeComponent();
            InitializeSettings();
            InitializeInitialData();
            InitializePossibleDurations();
            DataContext = this;
        }

        private void InitializeInitialData()
        {
            InitializeAvailableExercises();
            InitializeTestingExercises();
        }

        private void InitializeSettings()
        {
            _settings = new CreateTestingSettings()
            {
                DurationOptionsCount = 36, 
                ComboboxDefaultValue = 60
            };
        }

        private void InitializePossibleDurations()
        {
            Duration = new List<int>();
            for (int i = 1; i <= _settings.DurationOptionsCount; i++)
            {
                Duration.Add(i * 5);
            }
            cb_Duration.ItemsSource = Duration;
            cb_Duration.SelectedValue = _settings.ComboboxDefaultValue;
        }

        private void InitializeAvailableExercises()
        {
            App.DB.InitializeTestingExercises(AvailableExercises);
        }


        public void InitializeTestingExercises()
        {
            Testing.Exercises = new ObservableCollection<Exercise>();
        }
        #endregion

        #region Exercise, Testing buttons
        private void btn_AddExercise_Click(object sender, RoutedEventArgs e)
        {
            Testing.Exercises.Add(new Exercise());
        }

        private void btn_RemoveExercise_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as Button;
            var contex = s.DataContext;
            if (contex is Exercise)
                Testing.Exercises.Remove(contex as Exercise);
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            // TODO: check entered data
            ObtainTestingData();
            App.DB.SaveTesting(Testing);
            MessageBox.Show("Testing has been successfully saved.");
        }

        private void ObtainTestingData()
        {
            Testing.DurationInMinutes = SelectedDuration;
            if (App.Teacher != null)
            {
                Testing.TestCreator = App.Teacher;
            }
        }
        #endregion
    }
}
