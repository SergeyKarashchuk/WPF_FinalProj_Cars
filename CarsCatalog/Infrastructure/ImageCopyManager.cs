using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsCatalog.Infrastructure
{
    public static class ImageCopyManager
    {
        public static string CopyImageToFolder()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Image files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png"
            };
            if (ofd.ShowDialog() == true)
            {
                FileInfo file = new FileInfo(ofd.FileName);
                string newFileName = file.FullName;                
                return newFileName;
            }
            return null;
        }
    }
}
