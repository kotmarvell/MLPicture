﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Picture.BLL.Formats
{
    public class GrayscaleByteImageFormat
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public readonly byte[] rawdata;

        public GrayscaleByteImageFormat(int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            rawdata = new byte[Width * Height];
        }

        public byte this[int x, int y]
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
    }
}
