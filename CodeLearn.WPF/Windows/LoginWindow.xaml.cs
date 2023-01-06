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
        private void btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMode == LoginMode.Student)
            {
                SignInAsStudent();
            }
            else
            {
                SignInAsTeacher();
            }
        }

        private void SignInAsStudent()
        {
            var user = App.DB.SignInAsStudent(uc_UsernameControl.Username, 
                                              uc_PasswordControl.Password);
            if (user != null)
            {
                App.Student = user;
                OpenControlWindow();
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

        private void SignInAsTeacher()
        {
            var user = App.DB.SignInAsTeacher(uc_UsernameControl.Username,
                                              uc_PasswordControl.Password);
            if (user != null)
            {
                App.Teacher = user;
                OpenControlWindow();
            }
            else
            {
                MessageBox.Show(_invalidCredentials);
            }
        }
        #endregion
    }
}
