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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CodeExecuter CodeExecuter;
        public MainWindow()
        {
            InitializeComponent();

            CodeExecuter = new CodeExecuter(new ExecuteLogHandler(Log));
        }

        private void Log(object msg)
        {
            OutputTextBox.Text += string.Concat(msg, Environment.NewLine);
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            OutputTextBox.Text = string.Empty;
            CodeExecuter.FormatSources(InputTextBox.Text);
            CodeExecuter.Execute();
        }
    }
}
