using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using Picture.DAL.Formats;

namespace Picture.DAL.IO
{
    public unsafe partial class ImageIO
    {
        #region LockBitmap

        public static LockBitmapInfo LockBitmap(Bitmap B)
        {
            return LockBitmap(B, PixelFormat.Format32bppRgb, 4);
        }

        public static LockBitmapInfo LockBitmapWithAlpha(Bitmap B)
        {
            return LockBitmap(B, PixelFormat.Format32bppArgb, 4);
        }

        public static LockBitmapInfo LockBitmap(Bitmap B, PixelFormat pf, int pixelsize)
        {
            LockBitmapInfo lbi;
            GraphicsUnit unit = GraphicsUnit.Pixel;
            RectangleF boundsF = B.GetBounds(ref unit);
            Rectangle bounds = new Rectangle
                (
                    (int)boundsF.X,
                    (int)boundsF.Y,
                    (int)boundsF.Width,
                    (int)boundsF.Height
                );
            
            lbi.b = B;
            lbi.width = (int)boundsF.Width;
            lbi.height = (int)boundsF.Height;
            lbi.bitmapData = B.LockBits(bounds, ImageLockMode.ReadWrite, pf);
            lbi.linewidth = lbi.bitmapData.Stride;
            lbi.data = (byte*)(lbi.bitmapData.Scan0.ToPointer());
            
            return lbi;
        }


        public static void UnlockBitmap(LockBitmapInfo lbi)
        {
            lbi.b.UnlockBits(lbi.bitmapData);
            lbi.bitmapData = null;
            lbi.data = null;
        }

        #endregion

        #region Bitmap, File => Internal formats


        /// <summary>
        /// //���������� � rgb � ������ � ������
        /// </summary>
        /// <param name="B"></param>
        /// <returns></returns>
        public static ColorFloatImageFormat BitmapToColorFloatImage(Bitmap B)
        {
            int W = B.Width, H = B.Height;
            ColorFloatImageFormat res = new ColorFloatImageFormat(W, H);

            if (B.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                Color[] pi = B.Palette.Entries;
                byte[] pal = new byte[1024];
                for (int i = 0; i < pi.Length; i++)
                {
                    Color C = pi[i];
                    pal[i * 4] = C.B;
                    pal[i * 4 + 1] = C.G;
                    pal[i * 4 + 2] = C.R;
                    pal[i * 4 + 3] = C.A;
                }

                LockBitmapInfo lbi = LockBitmap(B, PixelFormat.Format8bppIndexed, 1);
                try
                {
                    for (int j = 0; j < H; j++)
                        for (int i = 0; i < W; i++)
                        {
                            int c = lbi.data[lbi.linewidth * j + i];
                            int b = pal[c * 4];
                            int g = pal[c * 4 + 1];
                            int r = pal[c * 4 + 2];
                            
                            res[i, j] = new ColorFloatPixel() 
                            { 
                                B = b,
                                G = g, 
                                R = r, 
                                A = 0.0f 
                            };
                        }
                }
                finally
                {
                    UnlockBitmap(lbi);
                }
            }
            else
            {   
                LockBitmapInfo lbi = LockBitmap(B);
                try
                {
                    for (int j = 0; j < H; j++)
                        for (int i = 0; i < W; i++)
                        {
                            int b = lbi.data[lbi.linewidth * j + i * 4];
                            int g = lbi.data[lbi.linewidth * j + i * 4 + 1];
                            int r = lbi.data[lbi.linewidth * j + i * 4 + 2];
                            
                            res[i, j] = new ColorFloatPixel() 
                            { 
                                B = b, 
                                G = g, 
                                R = r, 
                                A = 0.0f 
                            };
                        }
                }
                finally
                {
                    UnlockBitmap(lbi);
                }
            }

            return res;
        }

        public static ColorFloatImageFormat FileToColorFloatImage(string filename)
        {
            if (CheckPGM(filename))
                return ReadPGM(filename).ToColorFloatImage();

            Bitmap B = new Bitmap(filename);
            ColorFloatImageFormat res = BitmapToColorFloatImage(B);
            B.Dispose();
            
            return res;
        }

        #endregion

        #region PGM

        static bool CheckPGM(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            try
            {
                if (fs.Length < 2)
                    return false;

                int b1 = fs.ReadByte();
                int b2 = fs.ReadByte();
                
                return (b1 == 'P' && b2 == '5');
            }
            finally
            {
                fs.Close();
            }
        }

        static string ReadString(Stream stream)
        {
            StringBuilder sb = new StringBuilder();
            
            int c1 = stream.ReadByte();
            if (c1 == -1)
                return null;

            while (true)
            {
                if (c1 == 13 || c1 == 10 || c1 == -1)
                    return sb.ToString();
                else
                    sb.Append((char)c1);

                c1 = stream.ReadByte();
            }
        }

        static GrayscaleFloatImageFormat ReadPGM(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            try
            {
                while (true)
                {
                    string str = ReadString(fs).Trim();
                    if (str == null)
                        return null;
                    else if (str == "" || str.StartsWith("#"))
                        continue;
                    else if (str != "P5")
                        return null;
                    else
                        break;
                }

                int Width = -1, Height = -1, MaxL = -1;

                while (true)
                {
                    string str = ReadString(fs).Trim();
                    if (str == null)
                        return null;
                    else if (str == "" || str.StartsWith("#"))
                        continue;
                    
                    string[] arr = str.Split(' ', '\t');
                    Width = int.Parse(arr[0]);
                    Height = int.Parse(arr[1]);
                    
                    break;
                }

                while (true)
                {
                    string str = ReadString(fs).Trim();
                    if (str == null)
                        return null;
                    else if (str == "" || str.StartsWith("#"))
                        continue;
                    
                    MaxL = int.Parse(str);
                    
                    break;
                }

                GrayscaleFloatImageFormat res = new GrayscaleFloatImageFormat(Width, Height);

                if (MaxL <= 255)
                {
                    byte[] raw = new byte[Width * Height];
                    fs.Read(raw, 0, raw.Length);
                    for (int j = 0; j < Height; j++)
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            res[i, j] = raw[j * Width + i];
                        }
                    }
                }
                else
                {
                    byte[] raw = new byte[Width * Height * 2];
                    
                    fs.Read(raw, 0, raw.Length * 2);

                    for (int j = 0; j < Height; j++)
                    {
                        for (int i = 0; i < Width; i++)
                        {
                            res[i, j] = raw[(j * Width + i) * 2] + raw[(j * Width + i) * 2 + 1] * 255;
                        }
                    }
                }

                return res;
            }
            finally
            {
                fs.Close();
            }
        }

        #endregion

        #region Internal formats => Bitmap, File

        public static Bitmap ImageToBitmap(GrayscaleFloatImageFormat image)
        {
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);

            LockBitmapInfo lbi = LockBitmap(b);
            try
            {
                for (int j = 0; j < image.Height; j++)
                    for (int i = 0; i < image.Width; i++)
                    {
                        byte c = image[i, j] < 0.0f ? (byte)0 : image[i, j] > 255.0f ? (byte)255 : (byte)image[i, j];
                        lbi.data[lbi.linewidth * j + i * 4] = c;
                        lbi.data[lbi.linewidth * j + i * 4 + 1] = c;
                        lbi.data[lbi.linewidth * j + i * 4 + 2] = c;
                    }
            }
            finally
            {
                UnlockBitmap(lbi);
            }

            return b;
        }

        public static Bitmap ImageToBitmap(ColorFloatImageFormat image)
        {
            Bitmap b = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);

            LockBitmapInfo lbi = LockBitmap(b);
            try
            {
                for (int j = 0; j < image.Height; j++)
                    for (int i = 0; i < image.Width; i++)
                    {
                        ColorFloatPixel p = image[i, j];
                        lbi.data[lbi.linewidth * j + i * 4] = p.B < 0.0f ? (byte)0 : p.B > 255.0f ? (byte)255 : (byte)p.B;
                        lbi.data[lbi.linewidth * j + i * 4 + 1] = p.G < 0.0f ? (byte)0 : p.G > 255.0f ? (byte)255 : (byte)p.G;
                        lbi.data[lbi.linewidth * j + i * 4 + 2] = p.R < 0.0f ? (byte)0 : p.R > 255.0f ? (byte)255 : (byte)p.R;
                    }
            }
            finally
            {
                UnlockBitmap(lbi);
            }

            return b;
        }

        public static void ImageToFile(GrayscaleFloatImageFormat image, string filename)
        {
            using (Bitmap b = ImageToBitmap(image))
                b.Save(filename);
        }

        public static void ImageToFile(ColorFloatImageFormat image, string filename)
        {
            using (Bitmap b = ImageToBitmap(image))
                b.Save(filename);
        }

        #endregion
    }

    public unsafe struct LockBitmapInfo
    {
        public Bitmap b;
        public int linewidth;
        public BitmapData bitmapData;
        public byte* data;
        public int width;
        public int height;
    }
}
