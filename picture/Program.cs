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

            int count = 0;
            for (int i = 0; i < image.Height * image.Width; i++)
            {
                if ((image.rawdata[i].r <= 255) && (image.rawdata[i].r >= 200) && (image.rawdata[i].g >= 200) && (image.rawdata[i].g <= 255) && (image.rawdata[i].b <= 100))
                {
                    image.rawdata[i].r = 255;
                    image.rawdata[i].g = 255;
                    image.rawdata[i].b = 0;
                }
            }
            ImageIO.ImageToFile(image, OutputFileName);
            ColorFloatPixel[,] imageData = new ColorFloatPixel[image.Height, image.Width];

            for (int i = 0, k = 0; i < image.Height; ++i)
            {
                for (int l = 0; l < image.Width; ++l)
                {
                    imageData[i, l] = image.rawdata[k++];
                }
            }

            int[,] data = new int[image.Height, image.Width];

            string InputPattern = "E:/MLPicture/picture/assets/inputs/finish.jpg";
            ColorFloatImageFormat pattern = ImageIO.FileToColorFloatImage(InputPattern);
            ColorFloatPixel[,] patternData = new ColorFloatPixel[pattern.Height, pattern.Width];
            
            //for (int i = 0, k = 0; i < pattern.Height; ++i)
            //{
            //    for (int l = 0; l < pattern.Width; ++l)
            //    {
            //        patternData[i, l] = pattern.rawdata[k++];
            //    }
            //}
            //int m = 0;
            //int z = 0;
            //int max = 0;
            //int countM = 0;
            //for (int height = 0; height < image.Height - 47; height++)
            //{
            //    for (int width = 0; width < image.Width - 48; width++)
            //    {
            //        count = 0;
            //        m = 0;
            //        z = 0;
            //        for (int i = height; i < height + 47; i++)
            //        {
            //            z = 0;
            //            for (int l = width; l < width + 48; l++)
            //            {
            //                if ((patternData[m, z].r == imageData[i, l].r) && (patternData[m, z].g == imageData[i, l].g) && (patternData[m, z].b == imageData[i, l].b))
            //                {
            //                    count++;
            //                }
            //                z++;
            //            }
            //            m++;
            //        }
            //        if(count > max)
            //        {
            //            max = count;
            //        }
            //        data[height, width] = count;
            //    }
            //}


            //for (int height = 0; height < image.Height; height++)
            //{
            //    for (int width = 0; width < image.Width; width++)
            //    {
            //        if(data[height,width] >= max-1)
            //        {
            //            Console.WriteLine(height + " " + width);
            //        }
            //    }
            //}

            //Console.WriteLine(countM);

        }
    }
}
