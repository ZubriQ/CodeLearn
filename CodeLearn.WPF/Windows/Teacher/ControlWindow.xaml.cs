using CodeLearn.WPF.Windows.Teacher.Pages;
using CodeLearn.WPF.Windows.Teacher.Pages.Catalogs;
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
using System.Windows.Shapes;

namespace CodeLearn.WPF.Windows.Teacher
{
    /// <summary>
    /// Interaction logic for ControlWindow.xaml
    /// </summary>
    public partial class ControlWindow : Window
    {
        private readonly Dictionary<string, Page> pages = new();

        public ControlWindow()
        {
            InitializeComponent();
            InitializePages();
            InitializeHomePage();
        }

        #region Initialization
        private void InitializePages()
        {
            pages.Add("TestingResults", new TestingResultsPage());
            pages.Add("TestExercise", new TestExercisePage());
            pages.Add("CreateExercise", new CreateExercisePage());

        }

        private void InitializeHomePage()
        {
            ControlWindowFrame.Navigate(pages["TestingResults"]);
        }
        #endregion

        #region Verticical ribbon
        // TODO: make a dashboard that shows recent info.
        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_TestExercise_Click(object sender, RoutedEventArgs e)
        {
            ControlWindowFrame.Navigate(pages["TestExercise"]);
        }

        private void btn_CreateExercise_Click(object sender, RoutedEventArgs e)
        {
            ControlWindowFrame.Navigate(pages["CreateExercise"]);
        }

        private void btn_CreateTesting_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Exercises_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Testings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_TestResults_Click(object sender, RoutedEventArgs e)
        {
            ControlWindowFrame.Navigate(pages["TestingResults"]);
        }
        #endregion

        #region Basic UI methods
        private void brd_TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ControlWindowFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ControlWindowFrame.RemoveBackEntry();
        }
        #endregion


    }
}
