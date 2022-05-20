using CodeLearn.Database;
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

namespace CodeLearn.Windows
{
    public partial class DoExercise_Window : Window
    {
        private CodeExecuter CodeExecuter;

        public exercise Exercise { get; set; }

        public DoExercise_Window()
        {
            InitializeComponent();
            InitializeTestExercise();
            DataContext = this;

            //txtInput.Text = "for (int i = 0; i < 10; i++)\n\tLog(i);";
            CodeExecuter = new CodeExecuter(new ExecuteLogHandler(PrintResult), 
                Exercise.class_name, Exercise.test_method_info.First().name);
        }

        void InitializeTestExercise()
        {
            Exercise = App.DB.exercises.FirstOrDefault(s => s.id == 1002);
        }

        private void PrintResult(object msg)
        {
            txtOutput.Text += string.Concat(msg, Environment.NewLine);
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = string.Empty;
            CodeExecuter.FormatSources(txtInput.Text);
            CodeExecuter.Execute();
        }
    }
}
