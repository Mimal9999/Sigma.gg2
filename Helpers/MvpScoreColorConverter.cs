using LanguageExt.Pretty;
using System;
using System.Drawing;
using System.Windows.Data;

namespace Sigma.gg.Helpers;
public class MvpScoreColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string mvpScoreString)
        {
            if (mvpScoreString == "MVP")
            {
                return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Orange");
            }
            else if (mvpScoreString == "ACE")
            {
                return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Magenta");
            }
            else
            {
                return (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("DimGray");
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