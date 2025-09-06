using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Globalization;

namespace SortVisualizer.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public static readonly BoolToColorConverter Instance = new();

        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isHighlighted && isHighlighted)
            {
                return new SolidColorBrush(Color.Parse("#FFE6CC")); // Light orange highlight
            }
            return Brushes.Transparent;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}