using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SortVisualizer.Converters;

public class PositiveNumberToBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int intValue)
            return intValue > 0;
        if (value is double doubleValue)
            return doubleValue > 0;
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        // Usually not needed, so just return UnsetValue
        return Avalonia.Data.BindingOperations.DoNothing;
    }
}
