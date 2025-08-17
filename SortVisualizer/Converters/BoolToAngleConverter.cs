using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SortVisualizer.Converters;
public class BoolToAngleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is true ? 90 : 0; // Arrow rotates 90° when expanded
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
