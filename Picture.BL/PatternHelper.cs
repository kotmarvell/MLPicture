using Picture.DAL.Formats;
using Picture.DAL.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL
{
    public static class PatternHelper
    {
        /// <summary>
        /// Преобразование из одномерного массива пикселей в матрицу, в соответвии с размерами картинки
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static ColorFloatPixel[,] ColorImage(ColorFloatImageFormat image)
        {
            ColorFloatPixel[,] imageData = new ColorFloatPixel[image.Height, image.Width];

            for (int height = 0, countPixel = 0; height < image.Height; ++height)
            {
                for (int width = 0; width < image.Width; ++width)
                {
                    imageData[height, width] = image.RawData[countPixel++];
                }
            }
                
            return imageData;
        }

        #region MatchWithPattern
        /// <summary>
        /// Проходит по каждому пикселю исходного изображения,
        /// сравнивая его с матрицей паттерна
        /// и выставляет вес, равный количеству
        /// </summary>
        /// <param name="image"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int[,] MatchWithPattern(ColorFloatPixel[,] image, ColorFloatPixel[,] pattern)
        {
            int heightImage = image.GetUpperBound(0) + 1;
            int widthImage = image.Length / heightImage;


            int heightPattern = pattern.GetUpperBound(0) + 1;
            int widthPattern = pattern.Length / heightPattern;

            int[,] imageData = new int[heightImage, widthImage];

            for (int i = 0; i < (heightImage - heightPattern); i++)
            {
                for (int l = 0; l < (widthImage - widthPattern); l++)
                {
                    imageData[i, l] = Match(image, pattern, i, l);
                }
            }

            return imageData;
        }

        private static int Match(ColorFloatPixel[,] image, ColorFloatPixel[,] pattern, int startHeight, int startWidth)
        {
            int coefficient = 0;

            int height = pattern.GetUpperBound(0) + 1;
            int width = pattern.Length / height;

            int iPattern = 0;
            int lPattern = 0;

            for (int i = startHeight; i < startHeight + height; i++)
            {
                lPattern = 0;
                for (int l = startWidth; l < startWidth + width; l++)
                {
                    if (ComparePixel(image[i, l], pattern[iPattern, lPattern]))
                    {
                        coefficient++;
                    }
                    lPattern++;
                }
                iPattern++;
            }
            
            return coefficient;
        }

        private static bool ComparePixel(ColorFloatPixel pixelA, ColorFloatPixel pixelB)
        {
            return pixelA.Equals(pixelB);
        }
        #endregion

        public static int Sum(int[,] data, string OutputFileName, ColorFloatPixel[,] image, ColorFloatPixel[,] pattern)
        { 

            int height = data.GetUpperBound(0) + 1;
            int width = data.Length / height;

            int max = 0;
            int count = 0;

            for (int i = 0; i < height; i++)
            {
                for (int l = 0; l < width; l++)
                {
                    if (max < data[i, l])
                    {
                        max = data[i, l];
                        count = 0;
                    }
                    if (max == data[i, l])
                    {
                        count++;
                    }
                }
            }

            for(int i = 0; i < height; i++)
            {
                for (int l = 0; l < width; l++)
                {
                    if (max == data[i, l])
                    {
                        image = SerchPattern(image, pattern, l, i);
                    }
                    if ((max - 1) == data[i, l])
                    {
                        image = SerchPattern(image, pattern, l, i);
                    }
                    if ((max - 2) == data[i, l])
                    {
                        image = SerchPattern(image, pattern, l, i);
                    }
                }
            }

            ColorFloatImageFormat imageRes = new ColorFloatImageFormat(width, height);
            int length = 0;
            int k = 0;
            while(length < (width * height))
            {
                for (int g = 0; g < width; g++)
                {
                    imageRes.RawData[length] = image[k, g];
                    length++;
                }
                k++;
            }

            ImageIO.ImageToFile(imageRes, OutputFileName);
            return count;
        }

        public static ColorFloatPixel[,] SerchPattern(ColorFloatPixel[,] image, ColorFloatPixel[,] pattern, int startWidth, int startHight)
        {
            int heightPattern = pattern.GetUpperBound(0) + 1;
            int widthPattern = pattern.Length / heightPattern;

            int heightImage = image.GetUpperBound(0) + 1;
            int widthImage = image.Length / heightImage;

            for (int l = startWidth; l < (startWidth + widthPattern); l++)
            {
                image[startHight, l].R = 0;
                image[startHight, l].G = 0;
                image[startHight, l].B = 0;
            }

            for (int i = startHight; i < (startHight + heightPattern); i++)
            {
                image[i, startWidth].R = 0;
                image[i, startWidth].G = 0;
                image[i, startWidth].B = 0;
            }

            for (int l = startWidth; l < (startWidth + widthPattern); l++)
            {
                image[startHight + heightPattern, l].R = 0;
                image[startHight + heightPattern, l].G = 0;
                image[startHight + heightPattern, l].B = 0;
            }

            for (int i = startHight; i < (startHight + heightPattern); i++)
            {
                image[i, startWidth + widthPattern].R = 0;
                image[i, startWidth + widthPattern].G = 0;
                image[i, startWidth + widthPattern].B = 0;
            }

            return image;
        }
    }
}
