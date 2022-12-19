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
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl
    {
        public string Password
        {
            get
            {
                return pb_Password.Password;
            }
            set
            {
                pb_Password.Password = value;
            }
        }

        public PasswordControl()
        {
            InitializeComponent();
        }
    }
}
