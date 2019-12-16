using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


/// <summary>
/// finish this
/// </summary>


namespace projVN
{
    struct FontMapEntry
    {
        public static FontMapEntry Default{ get { return new FontMapEntry(-1, -1, -1, -1, -1, '\0'); } }
        public int Index { get; }
        public int Width { get; }
        public int Height { get; }
        public int Offset1 { get; }
        public int Offset2 { get; }
        public char Character { get; }
        public FontMapEntry(int index, int offset1, int offset2, int width, int height, char c)
        {
            Index = index;
            Width = width;
            Height = height;
            Offset1 = offset1;
            Offset2 = offset2;
            Character = c;
        }
    }

    class FontMap
    {
        public const int MAX_ASCII = 126;   // 
        public const int MIN_ASCII = 32;    // can also be used as offset for array index
        public const int ASCII_RANGE = MAX_ASCII - MIN_ASCII + 1;

        private Font ftype;
        private Brush brushType;
        private Bitmap map;
        private Texture2D mapTexture;
        private FontMapEntry[] charIndexes = new FontMapEntry[ASCII_RANGE];  // index for each ascii value. int value represent offset in the texture2D
        private String asciiAsString;
        private int sumMapWidth;
        private int maxMapHeight;
        private GraphicsDevice g;

        public int TotalMapWidth { get { return sumMapWidth; } }
        public int MaxMapHeight { get { return maxMapHeight; } }
        public Texture2D FontMapTexture { get { return mapTexture; } }
        public String FullString { get { return asciiAsString; } }
        public FontMap(GraphicsDevice gdev, Font f, Brush b)
        {
            g = gdev;
            ftype = f;
            brushType = b;
            LoadBitmapSet();
        }

        public FontMap(GraphicsDevice gdev, Font f, float initialSize, FontStyle style, Brush b)
        {
            g = gdev;
            ftype = new Font(f.FontFamily, initialSize, style, f.Unit, f.GdiCharSet, f.GdiVerticalFont);
            brushType = b;
            LoadBitmapSet();
        }

        public FontMap(GraphicsDevice gdev, String name, float initalSize, FontStyle style, Brush b)
        {
            g = gdev;
            ftype = new Font(new FontFamily(name), initalSize, style);
            brushType = b;
            LoadBitmapSet();
        }

        private void LoadBitmapSet()
        {
            int tempoffset = 0;
            int tempwidth = 0;
            char tempchar = '\0';
            maxMapHeight = -1;
            asciiAsString = "";

            // setup the ASCII array with proper text offsets/indexes
            for(int i = 0, l = MIN_ASCII; i < ASCII_RANGE; i++, l++)
            {
                tempchar = (char)l;
                asciiAsString += tempchar;
                tempoffset = sumMapWidth;
                Size t = TextRenderer.MeasureText($"{tempchar}", ftype);
                tempwidth = tempoffset + t.Width;
                sumMapWidth = tempwidth;
                charIndexes[i] = new FontMapEntry(i, tempoffset, tempwidth, t.Width, t.Height, tempchar);
                if (t.Height > maxMapHeight) maxMapHeight = t.Height;
            }
            Console.WriteLine(asciiAsString);
            // setup the bitmap
            map = new Bitmap(sumMapWidth, maxMapHeight);

            // draw text on the bitmap
            //DrawStringOnBitmap(map, asciiAsString, ftype, brushType);
            Graphics gr = Graphics.FromImage(map);
            gr.DrawString(asciiAsString, ftype, Brushes.Black, 0, 0);
            gr.Flush();


            // setup the texture2D
            //LoadBitmapToTexture2D(g, map, this.mapTexture);
            BitmapData dat = map.LockBits(new Rectangle(0, 0, map.Width, map.Height), ImageLockMode.ReadOnly, map.PixelFormat);
            byte[] arr = new byte[dat.Stride * map.Height];
            Marshal.Copy(dat.Scan0, arr, 0, arr.Length);
            mapTexture = new Texture2D(g, map.Width, map.Height, true, SurfaceFormat.Color);
            mapTexture.SetData(arr);
            map.UnlockBits(dat);
            
        }

        public void SaveImage()
        {
            using (Stream st = File.Create("test.png"))
                FontMapTexture.SaveAsPng(st, FontMapTexture.Width, FontMapTexture.Height);
        }

        private void DrawStringOnBitmap(Bitmap bm, String s, Font f, Brush b)
        {
            Graphics g = Graphics.FromImage(bm);
            g.DrawString(s, ftype, b, 0, 0);
            g.Flush();
        }

        private Texture2D LoadBitmapToTexture2D(GraphicsDevice gdev, Bitmap bm)
        {
            BitmapData dat = bm.LockBits(new Rectangle(0, 0, bm.Width, bm.Height), ImageLockMode.ReadOnly, bm.PixelFormat);
            byte[] arr = new byte[dat.Stride * bm.Height];
            Marshal.Copy(dat.Scan0, arr, 0, arr.Length);
            Texture2D text = new Texture2D(gdev, bm.Width, bm.Height, true, SurfaceFormat.Color);
            text.SetData(arr);
            bm.UnlockBits(dat);
            return text;
        }

        public FontMapEntry GetCharacterOffset(char c)
        {
            int cindex = (int)c;
            if(cindex >= MIN_ASCII && cindex <= MAX_ASCII)
                return charIndexes[cindex - MIN_ASCII];
            return FontMapEntry.Default;
        }
    }
}
