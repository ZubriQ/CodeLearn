using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace CodeLearn.WPF
{
    public static class PaletteController
    {
        public static void SetCircleButtonColor(Button button)
        {
            button.Background = Palette.Brushes["Primary3"];
        }

        public static void SetCircleButtonDefaultColor(Button button)
        {
            button.Background = Palette.Brushes["Primary"];
        }

        public static void SetFinishButtonReadyColor(Button button)
        {
            button.Background = Palette.Brushes["SquashOrange"];
        }

        public static void SetTimerWarningColor(TextBlock timer)
        {
            timer.Foreground = Palette.Brushes["Warning"];
        }

        public static void SetMenuButtonPressedColor(Button button)
        {
            button.Background = Palette.Brushes["GreyFriendDark3"];
            button.Foreground = Palette.Brushes["Primary5"];
        }

        public static void SetMenuButtonReleasedColor(Button button)
        {
            button.Background = Brushes.Transparent;
            button.Foreground = Brushes.Transparent;
        }

        public static void SetFocusedSearchBoxUnderlineColor(Border underline)
        {
            underline.Background = Palette.Brushes["Primary"];
        }

        public static void SetNullTextForegroundColor(TextBox textBox)
        {
            textBox.Foreground = Palette.Brushes["GreyFriendLight"];
        }

        public static void SetNotNullTextForegroundColor(TextBox textBox)
        {
            textBox.Foreground = Palette.Brushes["Primary5"];
        }
    }
}
