using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SoftRender.Render
{
    class RenderTexture
    {

        private Bitmap m_Texture;
        private int m_Width;
        private int m_height;

        public Bitmap Texture
        {
            get { return m_Texture; }
        }

        public RenderTexture(string filePath)
        {
            try
            {
                Image img = Image.FromFile(filePath);
                m_Width = img.Width;
                m_height = img.Height;
                m_Texture = new Bitmap(img, m_Width, m_height);
            }
            catch
            {
                m_Width = 256;
                m_height = 256;
                m_Texture = new Bitmap(m_Width, m_height);
                FileTextureWithRed();
            }
        }

        public void FileTextureWithRed()
        {
            for(int i = 0; i < m_Width; ++i)
            {
                for(int j = 0; j < m_height; ++j)
                {
                    m_Texture.SetPixel(i, j, System.Drawing.Color.Red);
                }
            }
        }

        public Color3 GetPixelColor(int x,int y)
        {
            x = x > 0 ? x : 0;
            x = x > m_Width ? m_Width - 1 : x;

            y = y > 0 ? y : 0;
            y = y > m_height ? m_height - 1 : y;

            System.Drawing.Color col = m_Texture.GetPixel(x, y);
            return new Color3(col.R, col.G, col.B);
        }

        public Color3 GetPixelColor(float xRate,float yRate)
        {
            int x = (int)(xRate * (m_Width - 1));
            int y = (int)(yRate * (m_height - 1));
            x = x > 0 ? x : 0;
            x = x > m_Width ? m_Width - 1 : x;

            y = y > 0 ? y : 0;
            y = y > m_height ? m_height - 1 : y;

            System.Drawing.Color col = m_Texture.GetPixel(x, y);
            return new Color3(col.R, col.G, col.B);
        }

    }
}
