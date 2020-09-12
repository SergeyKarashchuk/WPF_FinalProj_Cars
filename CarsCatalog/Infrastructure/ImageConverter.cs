using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CarsCatalog.Infrastructure
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                if (File.Exists($"{str}"))
                    return new BitmapImage(new Uri(str));

                var imageDestInApplication = $"/Data/Images/{str}";
                if (File.Exists($"{Environment.CurrentDirectory}{imageDestInApplication}"))
                    return new BitmapImage(new Uri(Environment.CurrentDirectory + imageDestInApplication));
            }
            var bi = new BitmapImage(new Uri($"/CarsCatalog;component/Images/No_Image.png", UriKind.RelativeOrAbsolute));
            return bi;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
