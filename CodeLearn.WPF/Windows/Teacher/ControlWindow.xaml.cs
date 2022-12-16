using CodeLearn.WPF.Windows.Teacher.Pages;
using CodeLearn.WPF.Windows.Teacher.Pages.Catalogs;
using CodeLearn.WPF.Windows.Teacher.Pages.Create;
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

        #region Initialization
        public ControlWindow()
        {
            InitializeComponent();
            InitializePages();
            InitializeHomePage();
        }
        
        private void InitializePages()
        {
            pages.Add("btn_Home", new HomePage());
            pages.Add("btn_TestExercise", new TestExercisePage());
            pages.Add("btn_CreateExercise", new CreateExercisePage());
            pages.Add("btn_CreateTesting", new CreateTestingPage());
            pages.Add("btn_Exercises", new ExercisesPage());
            pages.Add("btn_Testings", new TestingsPage());
            pages.Add("btn_TestingResults", new TestingResultsPage());
        }

        private void InitializeHomePage()
        {
            ControlWindowFrame.Navigate(pages["btn_Home"]);
        }
        #endregion

        #region Verticical ribbon
        private void Navigate(object sender, RoutedEventArgs e)
        {
            try
            {
                var pageName = (sender as Button)?.Name;
                if (pageName != null)
                    ControlWindowFrame.Navigate(pages[pageName]);
            }
            catch (Exception)
            {
                MessageBox.Show("Such a page does not exist.", "Navigation error");
            }
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
