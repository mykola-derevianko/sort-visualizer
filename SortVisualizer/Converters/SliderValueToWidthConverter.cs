using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SortVisualizer.Converters
{
    public class SliderValueToWidthConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double val && parameter is string param && double.TryParse(param, out double maxWidth))
            {
                // Assuming slider.Minimum = 0 and slider.Maximum = 100
                // Scale val to maxWidth
                return maxWidth * (val / 100);
            }
            return 0;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return Avalonia.Data.BindingOperations.DoNothing;
        }
    }
}
