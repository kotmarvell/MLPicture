using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.DAL.Formats
{
    public class GrayscaleByteImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public byte[] RewData { get; }

        public GrayscaleByteImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            RewData = new byte[Width * Height];
        }

        public byte this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException($"Trying to access pixel ({x}, {y}) in {Width}x{Height} image");
                return RewData[y * Width + x];
            }
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException($"Trying to access pixel ({x}, {y}) in {Width}x{Height} image");
                RewData[y * Width + x] = value;
            }
        }
    }
}
