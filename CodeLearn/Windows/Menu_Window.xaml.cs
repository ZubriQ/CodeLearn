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

namespace CodeLearn.Windows
{
    public partial class Menu_Window : Window
    {
        public Menu_Window()
        {
            InitializeComponent();
        }

        private void btn_CreateExercise_Click(object sender, RoutedEventArgs e)
        {
            CreateExercise_Window w = new CreateExercise_Window();
            w.ShowDialog();
        }

        private void btn_DoExercise_Click(object sender, RoutedEventArgs e)
        {
            DoExercise_Window w = new DoExercise_Window();
            w.ShowDialog();
        }
    }
}
