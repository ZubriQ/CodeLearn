using Microsoft.CodeAnalysis.Text;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CodeLearn.WPF.UserControls
{
    /// <summary>
    /// Interaction logic for FilterBox.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        #region Properties
        public string SearchText
        {
            get => txt_SearchBar.Text;
            set => txt_SearchBar.Text = value;
        }
        public string FilterName
        {
            get => txt_FilterName.Text;
            set => txt_FilterName.Text = value;
        }
        #endregion

        public event TextChangedEventHandler TextChanged;

        public SearchBar()
        {
            InitializeComponent();
        }

        private void txt_SearchBar_TextChanged(object sender, TextChangedEventArgs args)
        {
            HandleChangedText(args);
            UpdateButtonState();
        }

        private void HandleChangedText(TextChangedEventArgs args)
        {
            TextChangedEventHandler handler = TextChanged;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        private void UpdateButtonState()
        {
            if (txt_SearchBar.Text.Length > 0)
            {
                btn_ResetSearch.IsEnabled = true;
            }
            else
            {
                btn_ResetSearch.IsEnabled = false;
            }
        }

        private void btn_ResetSearch_Click(object sender, RoutedEventArgs e)
        {
            txt_SearchBar.Text = "";
        }

        private void txt_SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            PaletteController.SetFocusedSearchBoxUnderlineColor(brd_Underline);
        }
    }
}
