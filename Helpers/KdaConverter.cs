using Sigma.gg.Models;
using System.Globalization;
using System.Windows.Data;

namespace Sigma.gg.Helpers;

public class KdaConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        // Tutaj możesz łączyć przekazane wartości i formatować treść, np.:
        if (values.Length == 3 && values[0] is int a && values[1] is int b && values[2] is int c)
        {
            return $"{a}/{b}/{c}";
        }
        return "Błąd konwersji";
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}