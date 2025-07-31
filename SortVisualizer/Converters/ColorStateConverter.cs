using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortVisualizer.Converters
{
    public class ColorStateConverter : IMultiValueConverter
    {
        public object Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            bool isSwapping = values[0] is true;
            bool isComparing = values[1] is true;

            if (isSwapping)
                return Brushes.Red;
            if (isComparing)
                return Brushes.Yellow;

            return Brushes.RoyalBlue;
        }
    }

}
