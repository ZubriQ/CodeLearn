using CodeLearn.Db;
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

namespace CodeLearn.WPF.Windows.Student.Pages
{
    /// <summary>
    /// Interaction logic for SelectCoursePage.xaml
    /// </summary>
    public partial class SelectCoursePage : Page
    {
        public SelectCoursePage()
        {
            InitializeComponent();
            ic_Courses.ItemsSource = App.DB.GetCourses();
        }

        private void uc_CourseItemControl_Click(object sender, EventArgs e)
        {
            var ic = sender as UserControl;
            var contex = ic?.DataContext;
            if (contex is Testing)
            {
                TestingPage testing = new TestingPage((Testing)contex);
                NavigationService.Navigate(testing);
            }
        }
    }
}
