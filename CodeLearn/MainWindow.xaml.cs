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

namespace CodeLearn
{
    /// <summary>
    /// Main CodeLearn Window.
    /// </summary>
    public partial class MainWindow : Window
    {
        private CodeExecuter CodeExecuter;
        public MainWindow()
        {
            InitializeComponent();

            txtInput.Text = "for (int i = 0; i < 10; i++)\n\tLog(i);";

            CodeExecuter = new CodeExecuter(new ExecuteLogHandler(PrintResult));
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
