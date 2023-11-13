using System;
using System.Drawing;
using System.Windows.Data;
using System.Windows.Media;

namespace Sigma.gg.Helpers;
public class WinColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is bool win)
        {
            if (win)
            {
                var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#5460FD");
                return color;
            }
            else
            {
                var color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FD5471");
                return color;
            }
        }
        else
        {
            return null;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}