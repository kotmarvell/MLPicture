using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Picture.DAL.Formats;
using Picture.DAL.IO;
using Picture.BLL;

namespace picture
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileName = "E:/MLPicture/picture/assets/inputs/znak.jpg";
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

            for (int i = 0; i < pattern.Height * pattern.Width; i++)
            {

                if ((image.RawData[i].R <= 255) && (pattern.RawData[i].R >= 240) && (pattern.RawData[i].G >= 240) && (pattern.RawData[i].G <= 255) &&
                    (pattern.RawData[i].B >= 240) && (pattern.RawData[i].B <= 255))
                {
                    pattern.RawData[i].R = 255;
                    pattern.RawData[i].G = 255;
                    pattern.RawData[i].B = 250;
                    pattern.RawData[i].A = -1;
                }
            }

            ColorFloatPixel[,] matrixPixelPattern = new ColorFloatPixel[pattern.Height, pattern.Width];
            matrixPixelPattern = PatternHelper.ColorImage(pattern);

            int[,] data = new int[image.Height, image.Width];
            data = PatternHelper.MatchWithPattern(matrixPixelImage, matrixPixelPattern);

            Console.WriteLine(PatternHelper.Sum(data, OutputFileName, matrixPixelImage, matrixPixelPattern));

        }
    }
}
