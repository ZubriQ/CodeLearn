using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace CodeLearn.WPF.Windows.Student.Pages
{
    /// <summary>
    /// For outputing items' indices.
    /// </summary>
    internal class ExerciseIndexConverter : IValueConverter
    {
        public object Convert(object value, Type TargetType, object parameter, CultureInfo culture)
        {
            ListBoxItem item = (ListBoxItem)value;
            ListBox listView = ItemsControl.ItemsControlFromItemContainer(item) as ListBox;
            int index = listView.ItemContainerGenerator.IndexFromContainer(item) + 1;
            return index.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
