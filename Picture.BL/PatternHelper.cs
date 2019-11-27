using Picture.BLL.Formats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL
{
    public static class PatternHelper
    {
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

        private static int Match(ColorFloatPixel[,] image, ColorFloatPixel[,] pattern, int startWidth, int startHeight)
        {
            int coefficient = 0;

            int height = pattern.GetUpperBound(0) + 1;
            int width = pattern.Length / height;

            for (int i = startHeight, iPattern = 0; i < startHeight + height; i++, iPattern++)
            {
                for (int l = startWidth, lPattern = 0; l < startWidth + width; l++, lPattern++)
                {
                    if (ComparePixel(image[i, l], pattern[iPattern, lPattern]))
                    {
                        coefficient++;
                    }
                }
            }
            
            return coefficient;
        }

        private static bool ComparePixel(ColorFloatPixel pixelA, ColorFloatPixel pixelB)
        {
            return pixelA.Equals(pixelB);
        }
        #endregion
    }
}
