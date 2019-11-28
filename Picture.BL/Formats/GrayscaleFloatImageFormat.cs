using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL.Formats
{

    public class GrayscaleFloatImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float[] RewData { get; }

        public GrayscaleFloatImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            RewData = new float[Width * Height];
        }

        public float this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", x, y, Width, Height));
                return RewData[y * Width + x];
            }
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", x, y, Width, Height));
                RewData[y * Width + x] = value;
            }
        }

        public GrayscaleByteImageFormat ToGrayscaleByteImage()
        {
            GrayscaleByteImageFormat res = new GrayscaleByteImageFormat(Width, Height);
            for (int i = 0; i < res.RewData.Length; i++)
                res.RewData[i] = RewData[i] < 0.0f ? (byte)0 : RewData[i] > 255.0f ? (byte)255 : (byte)RewData[i];
            return res;
        }

        public ColorFloatImageFormat ToColorFloatImage()
        {
            ColorFloatImageFormat res = new ColorFloatImageFormat(Width, Height);
            for (int i = 0; i < res.RawData.Length; i++)
                res.RawData[i] = new ColorFloatPixel() { B = RewData[i], G = RewData[i], R = RewData[i], A = 0.0f };
            return res;
        }

        public ColorByteImageFormat ToColorByteImage()
        {
            ColorByteImageFormat res = new ColorByteImageFormat(Width, Height);
            for (int i = 0; i < res.RawData.Length; i++)
            {
                byte c = RewData[i] < 0.0f ? (byte)0 : RewData[i] > 255.0f ? (byte)255 : (byte)RewData[i];
                res.RawData[i] = new ColorBytePixel() { B = c, G = c, R = c, A = 0 };
            }
            return res;
        }
    }
}
