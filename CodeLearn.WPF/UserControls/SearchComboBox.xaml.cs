using System.ComponentModel;
using System.Windows.Controls;

namespace CodeLearn.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for SearchComboBox.xaml
    /// </summary>
    public partial class SearchComboBox : UserControl
    {
        #region Properties
        public string FilterName
        {
            get => txt_FilterName.Text;
            set => txt_FilterName.Text = value;
        }

        [Bindable(true)]
        public string DisplayMemberPath
        {
            get => cb_SearchComboBox.DisplayMemberPath;
            set => cb_SearchComboBox.DisplayMemberPath = value;
        }

        [Bindable(true)]
        public System.Collections.IEnumerable? ItemsSource
        {
            get => cb_SearchComboBox.ItemsSource;
            set => cb_SearchComboBox.ItemsSource = value;
        }

        [Bindable(true)]
        [Browsable(false)]
        public object SelectedItem
        {
            get => cb_SearchComboBox.SelectedItem;
            set => cb_SearchComboBox.SelectedItem = value;
        }
        #endregion

        public event SelectionChangedEventHandler SelectionChanged;

        public SearchComboBox()
        {
            InitializeComponent();
        }
        
        private void cb_SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            HandleChangedSelection(args);
        }

        private void HandleChangedSelection(SelectionChangedEventArgs args)
        {
            SelectionChangedEventHandler handler = SelectionChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }
    }
}
