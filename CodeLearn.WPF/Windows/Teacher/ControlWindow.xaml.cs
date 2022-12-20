using CodeLearn.Db;
using CodeLearn.WPF.Windows.Teacher.Pages;
using CodeLearn.WPF.Windows.Teacher.Pages.Catalogs;
using CodeLearn.WPF.Windows.Teacher.Pages.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
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
        #region Fields

        private readonly Dictionary<string, Page> pages = new();
        private Button _lastPressedButton;

        #endregion

        public WindowSettings WindowSettings { get; set; } = new();

        #region Initialization
        public ControlWindow(bool isTeacher)
        {
            SetVisibilitySettings(isTeacher);
            DataContext = this;
            InitializeComponent();
            InitializePages();
            InitializeHomePage();
            _lastPressedButton = btn_Home;
        }

        private void SetVisibilitySettings(bool isTeacher)
        {
            if (isTeacher)
            {
                WindowSettings.Visibility = Visibility.Visible;
            }
            else
            {
                WindowSettings.Visibility = Visibility.Collapsed;
            }
        }


        // Only for testing.
        public ControlWindow()
        {
            InitializeComponent();
            SetVisibilitySettings(true);
            InitializePages();
            InitializeHomePage();
            _lastPressedButton = btn_Home;
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

        #region Verticical ribbon navigation
        private void Navigate(object sender, RoutedEventArgs e)
        {
            try
            {
                var pressedButton = sender as Button;
                if (pressedButton != null)
                {
                    var pageName = pressedButton?.Name;
                    if (pageName != null)
                        ControlWindowFrame.Navigate(pages[pageName]);

                    ColorIcons(pressedButton);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Such a page does not exist.", "Navigation error");
            }
        }

        private void ColorIcons(Button? button)
        {
            if (_lastPressedButton != null)
            {
                PaletteController.SetMenuButtonReleasedColor(_lastPressedButton);

            }
            _lastPressedButton = button;
            PaletteController.SetMenuButtonPressedColor(_lastPressedButton);
        }
        #endregion

        #region Basic UI methods
        private void brd_TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btn_Maximize_Click(object sender, RoutedEventArgs e)
        {
            var button = e.OriginalSource as Button;
            if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                button.Template = FindResource("NormalizeButton") as ControlTemplate;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                button.Template = FindResource("MaximizeButton") as ControlTemplate;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.BorderThickness = new System.Windows.Thickness(4);
            }
            else
            {
                this.BorderThickness = new System.Windows.Thickness(0);
            }
        }

        private void ControlWindowFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ControlWindowFrame.RemoveBackEntry();
        }
        #endregion
    }
}
