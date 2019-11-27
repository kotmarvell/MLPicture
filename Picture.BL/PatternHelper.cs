using Picture.BLL.Formats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL
{
    public class PatternHelper
    {
        public PatternHelper(string inputFile)
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
    }
}
