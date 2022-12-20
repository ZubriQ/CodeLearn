using Microsoft.CodeAnalysis.Text;
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

namespace CodeLearn.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for CourseItemControl.xaml
    /// </summary>
    public partial class CourseItemControl : UserControl
    {
        public event EventHandler Click;

        public CourseItemControl()
        {
            InitializeComponent();
        }

        private void btn_StartTesting_Click(object sender, RoutedEventArgs args)
        {
            HandleClick(args);
        }

        private void HandleClick(RoutedEventArgs args)
        {
            EventHandler handler = Click;
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
