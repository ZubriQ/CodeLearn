using CodeLearn.Db;
using CodeLearn.WPF.UserControls;
using CodeLearn.WPF.Windows.Student;
using CodeLearn.WPF.Windows.Teacher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeLearn.WPF.Windows
{
    public partial class LoginWindow : Window, INotifyPropertyChanged
    {
        public enum LoginMode { Teacher, Student }

        LoginMode _selectedMode = LoginMode.Student;
        public LoginMode SelectedMode
        {
            get
            {
                return _selectedMode;
            }
            set
            {
                if (_selectedMode != value && PropertyChanged!=null) {
                    _selectedMode = value;
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("StudentButtonTemplate"));
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TeacherButtonTemplate"));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _invalidCredentials = "Invalid username or password.";


        public ControlTemplate StudentButtonTemplate
        {
            get
            {
                if (SelectedMode == LoginMode.Student) {
                    return FindResource("CheckedStudentButton") as ControlTemplate;
                }
                return FindResource("UncheckedStudentButton") as ControlTemplate;
            }
        }
        public ControlTemplate TeacherButtonTemplate
        {
            get
            {
                if (SelectedMode == LoginMode.Teacher) {
                    return FindResource("CheckedTeacherButton") as ControlTemplate;
                }
                return FindResource("UncheckedTeacherButton") as ControlTemplate;
            }
        }

        public WindowSettings WindowSettings { get; set; }

        #region Initialization
        public LoginWindow()
        {
            InitializeComponent();
            InitializeMarkupSettings();
            DataContext = this;
        }

        private void InitializeMarkupSettings()
        {
            WindowSettings = new WindowSettings()
            {
                ElementsMargin = new Thickness(40, 14, 40, 14)
            };
        }
        #endregion

        #region Default UI behaviour
        private void background_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        #endregion

        #region Role Buttons
        private void btn_PickStudentRole_Click(object sender, RoutedEventArgs e)
        {
            SelectedMode = LoginMode.Student;
        }

        private void btn_PickTeacherRole_Click(object sender, RoutedEventArgs e)
        {
            SelectedMode = LoginMode.Teacher;
        }

        #endregion

        #region Log In

        private async void btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMode == LoginMode.Student)
            {
                await SignInAsStudent();
            }
            else
            {
                await SignInAsTeacher();
            }
        }

        private async Task SignInAsStudent()
        {
            var user = await App.UserManager.FindByNameAsync(uc_UsernameControl.Username);
            if (user != null && await App.UserManager.CheckPasswordAsync(user, uc_PasswordControl.Password))
            {
                var student = await App.DB.GetStudentByUserId(user.Id);
                if (student != null)
                {
                    App.Student = student;
                    OpenControlWindow();
                }
            }
            else
            {
                MessageBox.Show(_invalidCredentials);
            }
        }

        private async Task SignInAsTeacher()
        {
            var user = await App.UserManager.FindByNameAsync(uc_UsernameControl.Username);
            if (user != null && await App.UserManager.CheckPasswordAsync(user, uc_PasswordControl.Password))
            {
                var teacher = await App.DB.GetTeacherByUserId(user.Id);
                if (teacher != null)
                {
                    App.Teacher = teacher;
                    OpenControlWindow();
                }
            }
            else
            {
                MessageBox.Show(_invalidCredentials);
            }
        }

        private void OpenControlWindow()
        {
            ControlWindow window = new ControlWindow(SelectedMode == LoginMode.Teacher);
            window.Show();
            Close();
        }

        #endregion

        private void uc_PasswordControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Call your desired method when the Enter key is pressed
                btn_LogIn_Click(sender, e);
            }
        }
    }
}
