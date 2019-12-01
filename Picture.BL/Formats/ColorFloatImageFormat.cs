using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Picture.BLL.Formats
{ 
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ColorFloatPixel
    {
        public float B, G, R, A;

        public override bool Equals(object obj)
        {
            return obj is ColorFloatPixel pixel &&
                   B == pixel.B &&
                   G == pixel.G &&
                   R == pixel.R &&
                   A == pixel.A;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(B, G, R, A);
        }
    }

    public class ColorFloatImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ColorFloatPixel[] RawData { get; }

        public ColorFloatImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            RawData = new ColorFloatPixel[Width * Height];
        }

        public ColorFloatPixel this[int x, int y]
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
    }
}
