using Picture.DAL.Formats;
using Picture.DAL.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.DAL.Models
{
    public class Pattern
    {
        public string FileName { get; set; }
        public ColorFloatImageFormat PatternImage { get; set; }
        public ColorFloatPixel[,] Matrix { get; set; }
        public int MaxSimilarity { get; set; }
        public double CoefficientSimilarity { get; set; }
        public Pattern(string PatternFileName = "")
        {
            FileName = PatternFileName;
            PatternImage = ImageIO.FileToColorFloatImage(PatternFileName);
        }
    }
}
