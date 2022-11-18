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

namespace CodeLearn.WPF.Windows
{
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void btn_DoExercise_Click(object sender, RoutedEventArgs e)
        {
            DoExerciseWindow w = new DoExerciseWindow();
            w.ShowDialog();
        }

        private void btn_CreateExercise_Click(object sender, RoutedEventArgs e)
        {
            CreateExerciseWindow w = new CreateExerciseWindow();
            w.ShowDialog();
        }
    }
}
