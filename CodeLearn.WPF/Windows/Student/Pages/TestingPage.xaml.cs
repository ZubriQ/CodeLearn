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
        private readonly Testing _testing;

        private List<Exercise> exerciseList = new();

        /// <summary>
        /// If all exercises are clicked, enables the finish button.
        /// </summary>
        private Dictionary<int, bool> clickedExercises = new();

        private DoExercisePage[] doExercisePages;
        private DoExercisePage? currentDoExercisePage;

        private DispatcherTimer timer = new();
        private TimeSpan duration;

        private Button? lastPressedButton;

        private int currentExerciseIndex;

        public TestingPage(Testing testing)
        {
            InitializeComponent();
            // TODO: TestingPageInitializer
            this._testing = testing;
            doExercisePages = new DoExercisePage[0];
            btn_FinishTesting.IsEnabled = false;
            InitializeExercises();
            InitializeExerciseMap();
            InitializeTestingExercisePages();
            InitializeDuration();
            InitializeTimer();
            InitializeFirstExercisePage();
        }

        #region Initialization
        private void InitializeExercises()
        {
            exerciseList = _testing.Exercises.ToList();
            lv_TestingExercises.ItemsSource = exerciseList;
        }

        private void InitializeExerciseMap()
        {
            for (int i = 0; i < exerciseList.Count; i++)
            {
                clickedExercises[i] = false;
            }
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
            duration = TimeSpan.FromMinutes(_testing.DurationInMinutes);
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += TimerTick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        // TODO: Thread
        //private async void DispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    await Task.Run(() =>
        //    {
        //        duration = duration.Subtract(new TimeSpan(0, 0, 1));
        //        UpdateDurationColor(duration);
        //    });

        //    txt_Timer.Text = duration.ToString();
        //}

        private void TimerTick(object sender, EventArgs e)
        {
            var time = duration.Subtract(new TimeSpan(0, 0, 1));
            UpdateDurationText(time);
            UpdateDurationColor(time);
        }

        private void UpdateDurationText(TimeSpan time)
        {
            duration = time;
            txt_Timer.Text = duration.ToString();

            if (time == TimeSpan.Zero)
            {
                timer.Stop();
                FinalizeTesting();
            }
        }

        private void UpdateDurationColor(TimeSpan time)
        {
            var warningTime = new TimeSpan(0, 5, 0);
            if (time == warningTime)
            {
                PaletteController.SetTimerWarningColor(txt_Timer);
            }
        }

        private void InitializeFirstExercisePage()
        {
            if (doExercisePages.Length > 0)
            {
                var currentPage = doExercisePages[0];
                TestingExerciseFrame.Navigate(currentPage);
                currentExerciseIndex = 0;
                currentDoExercisePage = currentPage;
            }
        }
        #endregion

        #region Navigation
        private void btn_TestingExercise_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var context = button?.DataContext;

            if (context is Exercise && button != null)
            {
                currentDoExercisePage = doExercisePages.Where(p => p.Exercise == context)
                                                       .First();
                SetCurrentExerciseIndex((Exercise)context);
                TestingExerciseFrame.Navigate(currentDoExercisePage);

                UpdateExerciseMap();
                UpdateFinishButton();
                ChangeButtonsColors(button);
                UpdateExerciseTitle();
            }
        }

        private void SetCurrentExerciseIndex(Exercise context)
        {
            currentExerciseIndex = Array.FindIndex(doExercisePages, w => w.Exercise == context);
        }

        private void UpdateExerciseMap()
        {
            clickedExercises[currentExerciseIndex] = true;
        }

        private void UpdateFinishButton()
        {
            foreach(var click in clickedExercises)
            {
                if (click.Value == false)
                {
                    return;
                }
            }
            btn_FinishTesting.IsEnabled = true;
            btn_FinishTesting.ToolTip = null;
        }

        private void ChangeButtonsColors(Button button)
        {
            if (lastPressedButton != null)
            {
                PaletteController.SetCircleButtonDefaultColor(lastPressedButton);
            }
            PaletteController.SetCircleButtonColor(button);
            lastPressedButton = button;
        }

        private void UpdateExerciseTitle()
        {
            txt_ExerciseNumber.Text = $"Exercise: {currentExerciseIndex + 1}";
        }
        #endregion

        #region Forward/Backward turned off Navigation
        private void btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Navigate(isNextPage: false);
            UpdateExerciseTitle();
        }
        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            Navigate(isNextPage: true);
            UpdateExerciseTitle();
        }
        private void Navigate(bool isNextPage)
        {
            if (isNextPage)
            {
                NavigateForward();
            }
            else
            {
                NavigateBackward();
            }
        }
        private void NavigateForward()
        {
            if (currentExerciseIndex < doExercisePages.Length - 1)
            {
                currentExerciseIndex += 1;
                Navigate();
            }
        }
        private void NavigateBackward()
        {
            if (currentExerciseIndex > 0)
            {
                currentExerciseIndex -= 1;
                Navigate();
            }
        }
        private void Navigate()
        {
            currentDoExercisePage = doExercisePages[currentExerciseIndex];
            TestingExerciseFrame.Navigate(currentDoExercisePage);
        }
        #endregion

        #region Finalizing testing
        private void btn_FinishTesting_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = ShowMessageBox();
            if (result == MessageBoxResult.OK)
            {
                FinalizeTesting();
            }
        }

        private MessageBoxResult ShowMessageBox()
        {
            string messageBoxText = "Are you sure you want to complete the test?";
            string caption = "Submit";
            MessageBoxButton button = MessageBoxButton.OKCancel;
            MessageBoxImage icon = MessageBoxImage.Question;
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }

        private void FinalizeTesting()
        {
            string[] exerciseAnswers = new string[doExercisePages.Length];
            Exercise[] exercises = new Exercise[doExercisePages.Length];
            ObtainTestingData(exerciseAnswers, exercises);

            var testingPage = new TestingResultPage(_testing, exercises, exerciseAnswers);
            NavigationService.Navigate(testingPage);
        }

        private void ObtainTestingData(string[] exerciseAnswers, Exercise[] exercises)
        {
            for (int i = 0; i < doExercisePages.Length; i++)
            {
                exercises[i] = doExercisePages[i].Exercise;
                exerciseAnswers[i] = doExercisePages[i].Exercise.CodingArea;
            }
        }
        #endregion

        private void TestingExerciseFrame_Navigated(object sender, NavigationEventArgs e)
        {
            TestingExerciseFrame.RemoveBackEntry();
        }
    }
}
