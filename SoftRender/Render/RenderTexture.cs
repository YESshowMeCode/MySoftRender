using System.Drawing;

namespace SoftRender.Render
{
	class RenderTexture
	{
		private Bitmap mTexture;
		private int mWidth;
		private int mHeight;

		/// <summary>
		/// 要渲染的图片的真实数据
		/// </summary>
		public Bitmap Texture
		{
			get { return mTexture; }
		}

		/// <summary>
		/// 构造函数，从本地文件中读取一个文件
		/// </summary>
		/// <param name="filePath"></param>
		public RenderTexture(string filePath)
		{
			try
			{
				Image img = Image.FromFile(filePath);
				mWidth = img.Width;
				mHeight = img.Height;
				mTexture = new Bitmap(img, mWidth, mHeight);
			}
			catch
			{
				mWidth = 256;
				mHeight = 256;
				mTexture = new Bitmap(mWidth, mHeight);
				FillTextureWithRed();
			}
		}

		/// <summary>
		/// 设置用红色填充一张图片
		/// </summary>
		private void FillTextureWithRed()
		{
			for (int i = 0; i < mWidth; i++)
			{
				for (int j = 0; j < mHeight; j++)
				{
					mTexture.SetPixel(i, j, System.Drawing.Color.Red);
				}
			}
		}

		/// <summary>
		/// 获取某一位置颜色
		/// </summary>
		/// <param name="posX"></param>
		/// <param name="posY"></param>
		/// <returns></returns>
		public Color3 GetPixelColor(int posX, int posY)
		{
			posX = posX > 0 ? posX : 0;
			posX = posX >= mWidth ? mWidth - 1 : posX;

			posY = posY > 0 ? posY : 0;
			posY = posY >= mHeight ? mHeight - 1 : posY;
			System.Drawing.Color col = mTexture.GetPixel(posX, posY);
			return new Color3(col.R, col.G, col.B);
		}

		/// <summary>
		/// 按照比例获取某一位置颜色
		/// </summary>
		/// <param name="posXrate"></param>
		/// <param name="posYrate"></param>
		/// <returns></returns>
		public Color3 GetPixelColor(float posXrate, float posYrate)
		{
			int posX = (int)(posXrate * (mWidth - 1));
			int posY = (int)(posYrate * (mHeight - 1));
			posX = posX > 0 ? posX : 0;
			posX = posX >= mWidth ? mWidth - 1 : posX;

			posY = posY > 0 ? posY : 0;
			posY = posY >= mHeight ? mHeight - 1 : posY;
			System.Drawing.Color col = mTexture.GetPixel(posX, posY);
			return new Color3(col.R, col.G, col.B);
		}

	}
}
