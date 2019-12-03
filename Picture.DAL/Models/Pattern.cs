using Picture.DAL.Formats;
using Picture.DAL.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.DAL.Models
{
    public class Pattern
    {
        public string NameFile { get; set; }
        public ColorFloatImageFormat pattern { get; set; }
        public ColorFloatPixel[,] matrixPixelPattern { get; set; }
        public Pattern(string PatternFileName = "")
        {
            NameFile = PatternFileName;
            pattern = ImageIO.FileToColorFloatImage(PatternFileName);
        }
    }
}
