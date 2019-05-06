using System.Drawing;
using System.Drawing.Imaging;

namespace SoftRender.Render
{
	class TextureMap
	{
		public Bitmap bitmap;
		public BitmapData data;
		public int Width;
		public int Height;

		public TextureMap(string filename,int width,int height)
		{
			this.Width = width;
			this.Height = height;
			bitmap = new Bitmap(filename);
		}

		public int GetWidth()
		{
			return this.Width;

		}
		public int GetHeight()
		{
			return this.Height;
		}

		public BitmapData getBitmapData()
		{
			return this.data;
		}

		public BitmapData LockBits()
		{
			this.data = bitmap.LockBits(new Rectangle(0,0,Width,Height),ImageLockMode.ReadWrite,PixelFormat.Format24bppRgb);
			return this.data;
		}

		public void UnLockBits()
		{
			bitmap.UnlockBits(this.data);
		}
	}
}
