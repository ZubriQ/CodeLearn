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

namespace CodeLearn.WPF.Windows.Student.Pages
{
    /// <summary>
    /// Interaction logic for TestingResultPage.xaml
    /// </summary>
    public partial class TestingResultPage : Page
    {
        private readonly CodeManager _codeManager = new();
        private readonly Testing _testing;
        private readonly Exercise[] _exercises;
        private readonly string[] _exerciseAnswers;
        
        public TestingResult TestingResult { get; set; } = new();
        public TestingAnswer[] TestingAnswers { get; set; }

        public TestingResultPage(Testing course, Exercise[] exercises, string[] exerciseAnswers)
        {
            InitializeComponent();
            this._exerciseAnswers = exerciseAnswers;
            this._exercises = exercises;
            _testing = course;
            TestingAnswers = new TestingAnswer[exercises.Length];

            InitializeTestData();
            Task.Run(TestAnswersAsync).Wait();
        }

        private void InitializeTestData()
        {
            InitializeTestingResult();
            InitializeAnswers();
            TestingResult.TestingAnswers = TestingAnswers;
        }

        private void InitializeTestingResult()
        {
            // A stub.
            App.Student = App.DB.GetTestStudent();

            // Updating initial data.
            TestingResult.Student = App.Student;
            TestingResult.Course = _testing;
            TestingResult.TestingAnswers = TestingAnswers;
            TestingResult.Score = 0;
            TestingResult.PassingDate = DateTime.Now;
        }

        private void InitializeAnswers()
        {
            for (int i = 0; i < _exerciseAnswers.Length; i++)
            {
                TestingAnswer answer = new();
                answer.TestingResult = TestingResult;
                answer.Exercise = _exercises[i];
                answer.Answer = _exerciseAnswers[i];
                TestingAnswers[i] = answer;
            }
        }

        private async Task TestAnswersAsync()
        {
            _codeManager.SetTestingResultData(TestingResult);
            var result = _codeManager.CompileAndTest(_exerciseAnswers, _exercises);

            await App.DB.SaveTestingResultAsync(result);
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            var homePage = new SelectCoursePage();
            NavigationService.Navigate(homePage);
        }
    }
}
