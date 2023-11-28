using LanguageExt.Pretty;
using System;
using System.Drawing;
using System.Windows.Data;

namespace Sigma.gg.Helpers;
public class MultikillImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string multikillString)
        {
            if (multikillString == "Penta Kill")
            {
                //return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Orange");
                return "pack://application:,,,/Assets/multikillPenta.png";
            }
            else if (multikillString == "Quadra Kill")
            {
                //return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Magenta");
                return "pack://application:,,,/Assets/multikillQuadra.png";
            }
            else if (multikillString == "Triple Kill")
            {
                //return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Magenta");
                return "pack://application:,,,/Assets/multikillTripple.png";
            }
            else if (multikillString == "Double Kill")
            {
                //return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Magenta");
                return "pack://application:,,,/Assets/multikillDouble.png";
            }
            else
            {
                //return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("DimGray");
                return null;
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