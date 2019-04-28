using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.RenderData
{
    class Texture
    {
        public int width;
        public int height;
        public List<List<Color>> textureData = new List<List<Color>>(1024);

        public Texture()
        {

        }

        public Color Sample(float u,float v)
        {
            u = Math.MathUntil.Range(u, 0, 1);
            v = Math.MathUntil.Range(v, 0, 1);
            int intu = (int)(width * u);
            int intv = (int)(height * v);
            return textureData[intu][intv];
        }


    }
}
