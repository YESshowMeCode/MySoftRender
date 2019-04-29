using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SoftRender.Render
{
    class Texture
    {
        public int width;
        public int height;
        public List<List<Color>> textureData = new List<List<Color>>(1024);

        public Texture()
        {
        }

        public void LoadTexture(string path)
        {
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            Bitmap _textrue = new Bitmap(img, 256, 256);
            for (int i = 0; i < _textrue.Width; ++i)
            {
                for (int j = 0; j < _textrue.Height; ++j)
                {
                    Color col = new Color();
                    col.r = _textrue.GetPixel(i, j).R / 256;
                    col.g = _textrue.GetPixel(i, j).G / 256;
                    col.b = _textrue.GetPixel(i, j).B / 256;

                    textureData[i][j] = col;
                }
            }
        }

        public Color Sample(float u,float v)
        {
            u = MathUntil.Range(u, 0, 1);
            v = MathUntil.Range(v, 0, 1);
            int intu = (int)(width * u);
            int intv = (int)(height * v);
            return textureData[intu][intv];
        }


    }
}
