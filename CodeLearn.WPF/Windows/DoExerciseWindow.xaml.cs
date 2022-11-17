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
using CodeLearn.Lib;
using CodeLearn.Db;

namespace CodeLearn.WPF.Windows
{
    public partial class DoExerciseWindow : Window
    {
        private CodeManager codeManager { get; set; } = new();

        public Exercise Exercise { get; set; }

        public DoExerciseWindow()
        {
            InitializeComponent();
            InitializeTestExercise();
            DataContext = this;
        }

        void InitializeTestExercise()
        {
            Exercise = App.DB.GetTestExercise();
            Exercise.CodingArea = @"public static double GetArea(double a, double b)
{
    return a*b;
}";
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtOutput.Text = string.Empty;
                CheckMethod();
            }
            catch (Exception)
            {

            }
        }

        private void CheckMethod()
        {
            string? code = txtInput.Text;
            if (!string.IsNullOrEmpty(code))
            {
                codeManager.CompileAndTestMethod(code, Exercise);
            }
            //return true;
        }
    }
}
