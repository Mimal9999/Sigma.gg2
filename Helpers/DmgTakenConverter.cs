using System;
using System.Windows.Data;

namespace Sigma.gg.Helpers;
public class DmgTakenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is int dmg)
        {
            return $"T: {dmg}";
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