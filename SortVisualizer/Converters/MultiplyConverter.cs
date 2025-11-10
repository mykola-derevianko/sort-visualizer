using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SortVisualizer.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public int Factor { get; set; } = 1;

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int i)
            {
                if (parameter != null && int.TryParse(parameter.ToString(), out int p))
                    return i * p;
                return i * Factor;
            }
            return 0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => AvaloniaProperty.UnsetValue;
    }
}
