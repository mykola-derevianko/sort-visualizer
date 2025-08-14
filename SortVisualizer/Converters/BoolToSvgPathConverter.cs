using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SortVisualizer.Converters;
public class BoolToSvgPathConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        bool isPlaying = value is bool b && b;

        return isPlaying
            ? "/Assets/Icons/pause.svg"
            : "/Assets/Icons/play.svg";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
