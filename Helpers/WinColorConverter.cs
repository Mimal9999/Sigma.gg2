﻿using System;
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
                return "pack://application:,,,/Assets/WinBackground.png";
            }
            else
            {
                return "pack://application:,,,/Assets/LostBackground.png";
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