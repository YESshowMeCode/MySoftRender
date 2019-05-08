using System.Drawing;

namespace SoftRender.Render
{
	class RenderTexture
	{
		private Bitmap m_Texture;
		private int m_Width;
		private int m_Height;

		/// <summary>
		/// 要渲染的图片的真实数据
		/// </summary>
		public Bitmap Texture
		{
			get { return m_Texture; }
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
				m_Width = img.Width;
				m_Height = img.Height;
				m_Texture = new Bitmap(img, m_Width, m_Height);
			}
			catch
			{
				m_Width = 256;
				m_Height = 256;
				m_Texture = new Bitmap(m_Width, m_Height);
				FillTextureWithRed();
			}
		}

		/// <summary>
		/// 设置用红色填充一张图片
		/// </summary>
		private void FillTextureWithRed()
		{
			for (int i = 0; i < m_Width; i++)
			{
				for (int j = 0; j < m_Height; j++)
				{
					m_Texture.SetPixel(i, j, System.Drawing.Color.Red);
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
			posX = posX >= m_Width ? m_Width - 1 : posX;

			posY = posY > 0 ? posY : 0;
			posY = posY >= m_Height ? m_Height - 1 : posY;
			System.Drawing.Color col = m_Texture.GetPixel(posX, posY);
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
			int posX = (int)(posXrate * (m_Width - 1));
			int posY = (int)(posYrate * (m_Height - 1));
			posX = posX > 0 ? posX : 0;
			posX = posX >= m_Width ? m_Width - 1 : posX;

			posY = posY > 0 ? posY : 0;
			posY = posY >= m_Height ? m_Height - 1 : posY;
			System.Drawing.Color col = m_Texture.GetPixel(posX, posY);
			return new Color3(col.R, col.G, col.B);
		}

	}
}
