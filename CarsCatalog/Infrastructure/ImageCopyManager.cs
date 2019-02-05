using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
                string newFileName = file.Name;                

                string newFileNameWOextencion = Path.GetFileNameWithoutExtension(file.Name);               

                string extencion = newFileName.Remove(0, newFileNameWOextencion.Length);               

                DirectoryInfo di = new DirectoryInfo($"{Environment.CurrentDirectory}/Data/Images");
                FileInfo[] file_list = di.GetFiles();
                int i = 0;
                while (file_list.Any(x =>
                (Path.GetFileNameWithoutExtension(x.Name) == $"{newFileNameWOextencion}{(i > 0 ? i.ToString() : "")}")))
                {
                    i++;
                }
                newFileName = $"{newFileNameWOextencion}{(i > 0 ? i.ToString() : "")}{extencion}";
                File.Copy(file.FullName, $"{Environment.CurrentDirectory}/Data/Images/{newFileName}");
                return newFileName;
            }
            return null;
        }
    }
}
