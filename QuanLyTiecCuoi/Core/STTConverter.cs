using System;
using System.Globalization;
using System.Windows.Data;

namespace QuanLyTiecCuoi  // hoặc namespace nào bạn đang dùng cho converter
{
    public class STTConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
