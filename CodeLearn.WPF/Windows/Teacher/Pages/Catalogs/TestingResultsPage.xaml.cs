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

namespace CodeLearn.WPF.Windows.Teacher.Pages.Catalogs
{
    /// <summary>
    /// Interaction logic for TestingResultsPage.xaml
    /// </summary>
    public partial class TestingResultsPage : Page
    {
        private TestingResult[]? _testingResults;

        public TestingResultsPage()
        {
            InitializeComponent();
            InitializeTestingResults();
        }

        private void InitializeTestingResults()
        {
            _testingResults = App.DB.GetTestingResults()?.ToArray();
            dg_TestingResults.ItemsSource = _testingResults;
        }

        private void sb_LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sb_LastName.SearchText.Length > 1)
            {
                ObtainMatchingStudents();
            }
            else
            {
                dg_TestingResults.ItemsSource = _testingResults;
            }
        }

        private void ObtainMatchingStudents()
        {
            var tests = _testingResults?.Where(t => t.Student.LastName.ToLower()
                                        .Contains(sb_LastName.SearchText.ToLower()))
                                        .ToArray();
            dg_TestingResults.ItemsSource = tests;
        }


    }
}
