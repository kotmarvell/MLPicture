using Picture.BLL.Formats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL
{
    public class PatternHelper
    {
        public PatternHelper(ColorFloatImageFormat image)
        {

        }

        public ColorFloatPixel[,] ColorImage(ColorFloatImageFormat image)
        {
            ColorFloatPixel[,] imageData = new ColorFloatPixel[image.Height, image.Width];

            for (int height = 0, countPixel = 0; height < image.Height; ++height)
            {
                for (int width = 0; width < image.Width; ++width)
                {
                    imageData[height, width] = image.rawdata[countPixel++];
                }
            }
            return imageData;
        }

        public int[,] MatchWithPattern(ColorFloatPixel[,] image, ColorFloatPixel[,] pattern)
        {
            int heightImage = image.GetUpperBound(0) + 1;
            int widthImage = image.Length / heightImage;

            int heightPattern = pattern.GetUpperBound(0) + 1;
            int widthPattern = pattern.Length / heightPattern;

            int[,] imageData = new int[heightImage, widthImage];

            for (int i = 0; i < (heightImage - heightPattern); i++)
            {
                for(int l = 0; l < (widthImage - widthPattern); l++)
                {
                    imageData[i, l] = Match(image, pattern, i, l);
                }
            }

            return imageData;
        }

        private int Match (ColorFloatPixel[,] image, ColorFloatPixel[,] pattern, int startWidth, int startHeight)
        {
            int coefficient = 0;

            int height = pattern.GetUpperBound(0) + 1;
            int width = pattern.Length / height;

            for (int i = startHeight, iPattern = 0 ; i < startHeight + height; i++, iPattern++)
            {
                for(int l = startWidth, lPattern = 0; l < startWidth + width; l++, lPattern++)
                {
                    if(ComparePixel(image[i,l], pattern[iPattern, lPattern]))
                    {
                        coefficient++;
                    }
                }
            }
            return coefficient;
        }

        private bool ComparePixel(ColorFloatPixel Pixel_a, ColorFloatPixel Pixel_b)
        {
            if (Pixel_a.r == Pixel_b.r && Pixel_a.g == Pixel_b.g && Pixel_a.b == Pixel_b.b)
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
    }
}
