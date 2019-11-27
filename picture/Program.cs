using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Picture.BLL.Formats;
using Picture.BLL.IO;
namespace picture
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileName = "E:/MLPicture/picture/assets/inputs/Проект ОДД_pages-to-jpg-0027.jpg";
            string OutputFileName = "E:/MLPicture/picture/assets/inputs/finish1.jpg";
            if (!File.Exists(InputFileName))
                return;

            ColorFloatImageFormat image = ImageIO.FileToColorFloatImage(InputFileName);
            
            //for (int i = 0; i < image.Height * image.Width; i++)
            //{
            //    if ((image.rawdata[i].r <= 255) && (image.rawdata[i].r >= 200) && (image.rawdata[i].g >= 200) && (image.rawdata[i].g <= 255) && (image.rawdata[i].b <= 100))
            //    {
            //        image.rawdata[i].r = 255;
            //        image.rawdata[i].g = 255;
            //        image.rawdata[i].b = 0;
            //    }
            //}
            ImageIO.ImageToFile(image, OutputFileName);

        }
    }
}
