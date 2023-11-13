using System.Globalization;
using System.Windows.Data;

namespace Sigma.gg.Helpers;
public class ParsedKdaConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is Double kda)
        {
            return kda.ToString("0.00", CultureInfo.GetCultureInfo("en-GB")) + " KDA";
        }
        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}