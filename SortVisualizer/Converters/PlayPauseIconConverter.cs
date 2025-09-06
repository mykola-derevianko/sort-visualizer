using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace SortVisualizer.Converters
{
    public class PlayPauseIconConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is bool isPlaying)
            {
                return isPlaying ? "/Assets/Icons/pause.svg" : "/Assets/Icons/play.svg";
            }
            
            return "/Assets/Icons/play.svg";
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}