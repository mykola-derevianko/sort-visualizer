using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SortVisualizer.Converters
{
    public class StepFormatConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count >= 2 && values[0] is int current && values[1] is int total)
            {
                return $"Step: {current}/{total}";
            }

            return "Step: 0/0";
        }
    }

}
