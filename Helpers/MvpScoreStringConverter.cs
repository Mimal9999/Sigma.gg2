using System;
using System.Windows.Data;

namespace Sigma.gg.Helpers;
public class MvpScoreStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is string mvpScoreString)
        {
            if(mvpScoreString == "MVP")
            {
                return "MVP";
            }
            else if(mvpScoreString == "ACE")
            {
                return "ACE";
            }
            else if(mvpScoreString == "3")
            {
                return "3rd";
            }
            else
            {
                return $"{mvpScoreString}th";
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