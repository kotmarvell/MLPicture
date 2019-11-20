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
            string InputFileName = "E:/MLPicture/picture/assets/inputs/image.jpg";
            string OutputFileName = "E:/MLPicture/picture/assets/inputs/image2.jpg";
            if (!File.Exists(InputFileName))
                return;

            
            ColorFloatImageFormat image = ImageIO.FileToColorFloatImage(InputFileName);
            ImageIO.ImageToFile(image, OutputFileName);
        }
    }
}
