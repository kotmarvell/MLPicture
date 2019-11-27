using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Picture.BLL.Formats;
using Picture.BLL.IO;
using Picture.BLL;

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
            ColorFloatPixel[,] matrixPixelImage = new ColorFloatPixel[image.Height, image.Width];
            matrixPixelImage = PatternHelper.ColorImage(image);

            string PatternFileName = "E:/MLPicture/picture/assets/inputs/finish.jpg";
            if (!File.Exists(PatternFileName))
                return;
            
            ColorFloatImageFormat pattern = ImageIO.FileToColorFloatImage(PatternFileName);
            ColorFloatPixel[,] matrixPixelPattern = new ColorFloatPixel[pattern.Height, pattern.Width];
            matrixPixelPattern = PatternHelper.ColorImage(pattern);

            int[,] data = new int[image.Height, image.Width];
            data = PatternHelper.MatchWithPattern(matrixPixelImage, matrixPixelPattern);

            //for (int i = 0; i < image.Height * image.Width; i++)
            //{
            //    if ((image.rawdata[i].r <= 255) && (image.rawdata[i].r >= 200) && (image.rawdata[i].g >= 200) && (image.rawdata[i].g <= 255) && (image.rawdata[i].b <= 100))
            //    {
            //        image.rawdata[i].r = 255;
            //        image.rawdata[i].g = 255;
            //        image.rawdata[i].b = 0;
            //    }
            //}


        }
    }
}
