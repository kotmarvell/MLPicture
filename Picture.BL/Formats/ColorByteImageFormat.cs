using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Picture.BLL.Formats
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ColorBytePixel
    {
        public byte B, G, R, A;
    }

    public class ColorByteImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public ColorBytePixel[] RawData { get; }

        public ColorByteImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            RawData = new ColorBytePixel[Width * Height];
        }

        public ColorBytePixel this[int x, int y]
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
