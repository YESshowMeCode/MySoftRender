using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace SoftRender.Render
{
    class TextureMap
    {
        public int width;
        public int height;
        public Bitmap bitmap;
        public BitmapData bitmapData;

        public TextureMap(string fileName,int width,int height)
        {
            this.width = width;
            this.height = height;
            bitmap = new Bitmap(fileName);
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }

        public BitmapData getBitmapData()
        {
            return bitmapData;
        }

        public BitmapData LockBits()
        {
            bitmapData = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            return bitmapData;
        }

        public void UnLockBits()
        {
            bitmap.UnlockBits(bitmapData);
        }
    }
}
