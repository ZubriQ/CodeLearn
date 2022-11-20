using CodeLearn.Db;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Timers;
using System.Threading;
using System.Windows.Threading;

namespace CodeLearn.WPF.Windows.Student.Pages
{
    public partial class TestingPage : Page
    {
        private Testing Testing { get; set; }

        private List<Exercise> exerciseList = new();

        private DoExercisePage[] doExercisePages;

        private DispatcherTimer timer = new();

        private TimeSpan duration;

        public TestingPage(Testing testing)
        {
            InitializeComponent();

            this.Testing = testing;
            InitializeExercises();
            InitializeTestingExercisePages();
            InitializeDuration();
            InitializeTimer();
        }

        private void InitializeExercises()
        {
            exerciseList = Testing.Exercises.ToList();
            lv_TestingExercises.ItemsSource = exerciseList;
        }

        private void InitializeTestingExercisePages()
        {
            doExercisePages = new DoExercisePage[exerciseList.Count];
            for (int i = 0; i < exerciseList.Count; i++)
            {
                doExercisePages[i] = new DoExercisePage(exerciseList[i]);
            }
        }

        private void InitializeDuration()
        {
            duration = TimeSpan.FromMinutes(Testing.DurationInMinutes);
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            txt_Timer.Text = duration.ToString();
            duration = duration.Subtract(new TimeSpan(0, 0, 1));
        }

        private void btn_TestingExercise_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as Button;
            var context = s?.DataContext;
            if (context is Exercise)
            {
                var page = doExercisePages.Where(p => p.Exercise == context)
                                          .FirstOrDefault();
                TestingExerciseFrame.Navigate(page);
            }
        }
    }
}
