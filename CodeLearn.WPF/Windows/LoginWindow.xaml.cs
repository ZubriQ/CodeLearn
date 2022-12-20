using CodeLearn.Db;
using CodeLearn.WPF.UserControls;
using CodeLearn.WPF.Windows.Student;
using CodeLearn.WPF.Windows.Teacher;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CodeLearn.WPF.Windows
{
    public partial class LoginWindow : Window
    {
        private bool _isStudentRole = true;
        private bool _isTeacherRole = false;
        private string _invalidCredentials = "Invalid username or password.";
        private Dictionary<string, ControlTemplate> _buttonTemplates = new();

        public WindowSettings WindowSettings { get; set; }

        #region Initialization
        public LoginWindow()
        {
            InitializeComponent();
            InitializeMarkupSettings();
            GetRoleButtonsTemplates();
            DataContext = this;
        }

        private void GetRoleButtonsTemplates()
        {
            _buttonTemplates.Add("Check Student", FindResource("CheckedStudentButton") as ControlTemplate);
            _buttonTemplates.Add("Uncheck Student", FindResource("UncheckedStudentButton") as ControlTemplate);
            _buttonTemplates.Add("Check Teacher", FindResource("CheckedTeacherButton") as ControlTemplate);
            _buttonTemplates.Add("Uncheck Teacher", FindResource("UncheckedTeacherButton") as ControlTemplate);
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
            if (_isStudentRole == false)
            {
                SwitchRoleToStudent();
            }
        }

        private void SwitchRoleToStudent()
        {
            _isTeacherRole = false;
            _isStudentRole = true;
            btn_PickTeacherRole.Template = _buttonTemplates["Uncheck Teacher"];
            btn_PickStudentRole.Template = _buttonTemplates["Check Student"];
        }

        private void btn_PickTeacherRole_Click(object sender, RoutedEventArgs e)
        {
            if (_isTeacherRole == false)
            {
                SwitchRoleToTeacher();
            }
        }

        private void SwitchRoleToTeacher()
        {
            _isTeacherRole = true;
            _isStudentRole = false;
            btn_PickStudentRole.Template = _buttonTemplates["Uncheck Student"];
            btn_PickTeacherRole.Template = _buttonTemplates["Check Teacher"];
        }
        #endregion

        #region Log In
        private void btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (_isStudentRole)
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
            ControlWindow window = new ControlWindow(_isTeacherRole);
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
