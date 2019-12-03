using Picture.DAL.Formats;
using Picture.DAL.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.DAL.Models
{
    public class ImageResult
    {
        public string NameFile { get; set; }
        public ColorFloatImageFormat image { get; set; }
        public ColorFloatPixel[,] matrixPixelImage { get; set; }
        public ImageResult(string ResultFileName = "")
        {
            NameFile = ResultFileName;
            image = ImageIO.FileToColorFloatImage(ResultFileName);
        }
    }
}
