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
            { "GreyFriendDark2", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#39404d")) },
            { "GreyFriendDark3", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#323945")) },
            { "GreyFriendDark4", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2c323c")) },
            { "GreyFriendDark5", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#262b34")) },
            { "GreyFriendDark6", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#20242b")) },
            { "GreyFriendLight", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A3ABBD")) },
            { "GreyFriendLight2", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b5bcca")) },
            { "GreyFriendLight3", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c8cdd7")) },
            { "GreyFriendLight4", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#dadde5")) },
            { "GreyFriendLight5", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#edeef2")) },
            { "SquashOrange", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#C25211")) },
            { "SquashBlueGreen", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#008E87")) },
            { "Warning", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5858")) }
        };
    }
}
