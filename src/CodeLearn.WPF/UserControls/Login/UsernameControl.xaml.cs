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

namespace CodeLearn.WPF.UserControls.Login
{
    /// <summary>
    /// Interaction logic for UsernameControl.xaml
    /// </summary>
    public partial class UsernameControl : UserControl
    {
        public string Username
        {
            get => txt_Username.Text;
            set => txt_Username.Text = value;
        }

        public UsernameControl()
        {
            InitializeComponent();
        }
    }
}
