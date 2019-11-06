using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace picture
{
    class Program
    {
        static void Main(string[] args)
        {
            string InputFileName = "C:/Users/marve/OneDrive/Рабочий стол/image.jpg";
            string OutputFileName = "C:/Users/marve/OneDrive/Рабочий стол/image2.jpg";
            if (!File.Exists(InputFileName))
                return;

            ColorFloatImage image = ImageIO.FileToColorFloatImage(InputFileName);

            ImageIO.ImageToFile(image, OutputFileName);
        }
    }
}
