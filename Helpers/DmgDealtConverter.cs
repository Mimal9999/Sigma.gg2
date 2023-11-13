using System;
using System.Windows.Data;

namespace Sigma.gg.Helpers;
public class DmgDealtConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is int dmg)
        {
            return $"D: {dmg}";
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