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
            string val = value as string;
            string dest = "/Data/Images/";            
            dest += val ?? "No_Image.png";

            if (!File.Exists($"{Environment.CurrentDirectory}{dest}"))
            {
                return null;
            }               
            BitmapImage img = new BitmapImage(new Uri(Environment.CurrentDirectory + dest));
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
