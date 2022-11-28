using System.Windows.Controls;

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
    }
}
