using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL.Formats
{

    public class GrayscaleFloatImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public readonly float[] rawdata;

        public GrayscaleFloatImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            rawdata = new float[Width * Height];
        }

        public float this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", x, y, Width, Height));
                return rawdata[y * Width + x];
            }
            set
            {
                if (x < 0 || x >= Width || y < 0 || y >= Height)
                    throw new IndexOutOfRangeException(string.Format("Trying to access pixel ({0}, {1}) in {2}x{3} image", x, y, Width, Height));
                rawdata[y * Width + x] = value;
            }
        }

        public GrayscaleByteImageFormat ToGrayscaleByteImage()
        {
            GrayscaleByteImageFormat res = new GrayscaleByteImageFormat(Width, Height);
            for (int i = 0; i < res.rawdata.Length; i++)
                res.rawdata[i] = rawdata[i] < 0.0f ? (byte)0 : rawdata[i] > 255.0f ? (byte)255 : (byte)rawdata[i];
            return res;
        }

        public ColorFloatImageFormat ToColorFloatImage()
        {
            ColorFloatImageFormat res = new ColorFloatImageFormat(Width, Height);
            for (int i = 0; i < res.RawData.Length; i++)
                res.RawData[i] = new ColorFloatPixel() { B = rawdata[i], G = rawdata[i], R = rawdata[i], A = 0.0f };
            return res;
        }

        public ColorByteImageFormat ToColorByteImage()
        {
            ColorByteImageFormat res = new ColorByteImageFormat(Width, Height);
            for (int i = 0; i < res.rawdata.Length; i++)
            {
                byte c = rawdata[i] < 0.0f ? (byte)0 : rawdata[i] > 255.0f ? (byte)255 : (byte)rawdata[i];
                res.rawdata[i] = new ColorBytePixel() { b = c, g = c, r = c, a = 0 };
            }
            return res;
        }
    }
}
