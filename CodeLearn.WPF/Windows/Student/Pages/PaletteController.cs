using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeLearn.WPF.Windows.Student.Pages
{
    public static class PaletteController
    {
        // TODO: Color Dictionary

        public static void SetCircleButtonColor(Button button)
        {
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BDD2"));
            button.Background = brush;
        }

        public static void SetCircleButtonDefaultColor(Button button)
        {
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0077cc"));
            button.Background = brush;
        }

        public static void SetTimerWarningColor(TextBlock timer)
        {
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5858"));
            timer.Foreground = brush;
        }
    }
}
