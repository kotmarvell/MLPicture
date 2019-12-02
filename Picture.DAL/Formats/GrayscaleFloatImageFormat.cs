using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.DAL.Formats
{

    public class GrayscaleFloatImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float[] RawData { get; }

        public GrayscaleFloatImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            RawData = new float[Width * Height];
        }

        public float this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException($"Trying to access pixel ({x}, {y}) in {Width}x{Height} image");
                
                return RawData[y * Width + x];
            }
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException($"Trying to access pixel ({x}, {y}) in {Width}x{Height} image");
                RawData[y * Width + x] = value;
            }
        }

        public GrayscaleByteImageFormat ToGrayscaleByteImage()
        {
            GrayscaleByteImageFormat res = new GrayscaleByteImageFormat(Width, Height);
            for (int i = 0; i < res.RewData.Length; i++)
                res.RewData[i] = RawData[i] < 0.0f ? (byte)0 : RawData[i] > 255.0f ? (byte)255 : (byte)RawData[i];
            
            return res;
        }

        public ColorFloatImageFormat ToColorFloatImage()
        {
            ColorFloatImageFormat res = new ColorFloatImageFormat(Width, Height);
            for (int i = 0; i < res.RawData.Length; i++)
                res.RawData[i] = new ColorFloatPixel() 
                { 
                    B = RawData[i], 
                    G = RawData[i], 
                    R = RawData[i], 
                    A = 0.0f };
            
            return res;
        }

        public ColorByteImageFormat ToColorByteImage()
        {
            ColorByteImageFormat res = new ColorByteImageFormat(Width, Height);
            for (int i = 0; i < res.RawData.Length; i++)
            {
                byte c = RawData[i] < 0.0f ? (byte)0 : RawData[i] > 255.0f ? (byte)255 : (byte)RawData[i];
                res.RawData[i] = new ColorBytePixel() 
                { 
                    B = c, 
                    G = c, 
                    R = c, 
                    A = 0 
                };
            }
            
            return res;
        }
    }
}
