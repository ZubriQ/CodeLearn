using System.Collections.Generic;
using System.Windows.Media;

namespace CodeLearn.WPF
{
    public static class Palette
    {
        public static Dictionary<string, SolidColorBrush> Brushes = new()
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
    }
}
