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
        private static Dictionary<string, SolidColorBrush> Brushes = new()
        {
            { "Primary", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0077CC")) },
            { "Primary2", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009DDF")) },
            { "Primary3", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00BDD2")) },
            { "Primary4", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00D8AE")) },
            { "Primary5", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#90ED85")) },

            { "GreyFriendDark", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3F4756")) },
            { "GreyFriendLight", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A3ABBD")) },

            { "SquashOrange", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C25211")) },
            { "SquashBlueGreen", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008E87")) },

            { "Warning", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5858")) }
        };

        public static void SetCircleButtonColor(Button button)
        {
            button.Background = Brushes["Primary3"];
        }

        public static void SetCircleButtonDefaultColor(Button button)
        {
            button.Background = Brushes["Primary"];
        }

        public static void SetFinishButtonReadyColor(Button button)
        {
            button.Background = Brushes["SquashOrange"];
        }

        public static void SetTimerWarningColor(TextBlock timer)
        {
            timer.Foreground = Brushes["Warning"];
        }
    }
}
