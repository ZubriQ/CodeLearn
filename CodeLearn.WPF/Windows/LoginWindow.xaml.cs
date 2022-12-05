using CodeLearn.Db;
using CodeLearn.WPF.Windows.Student;
using CodeLearn.WPF.Windows.Teacher;
using System;
using System.Windows;
using System.Windows.Input;

namespace CodeLearn.WPF.Windows
{
    public partial class LoginWindow : Window
    {
        private bool _isStudent = true;
        private string _invalidCredentials = "Invalid username or password.";

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void background_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Student_Click(object sender, RoutedEventArgs e)
        {
            btn_Student.IsEnabled = false;
            btn_Teacher.IsEnabled = true;
            _isStudent = true;
        }

        private void btn_Teacher_Click(object sender, RoutedEventArgs e)
        {
            btn_Student.IsEnabled = true;
            btn_Teacher.IsEnabled = false;
            _isStudent = false;
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            if (_isStudent)
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
            var user = App.DB.SignInAsStudent(txt_Username.Text, pb_Password.Password);
            if (user != null)
            {
                App.Student = user;
                HomeWindow window = new();
                window.Show();
                Close();
            }
            else
            {
                MessageBox.Show(_invalidCredentials);
            }
        }

        private void SignInAsTeacher()
        {
            var user = App.DB.SignInAsTeacher(txt_Username.Text, pb_Password.Password);
            if (user != null)
            {
                App.Teacher = user;
                ControlWindow window = new();
                window.Show();
                Close();
            }
            else
            {
                MessageBox.Show(_invalidCredentials);
            }
        }
    }
}
