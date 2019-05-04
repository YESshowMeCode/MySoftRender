using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Triangle
    {
        private List<Vertex> m_Vertexs;
        private float Weight1;
        private float weight2;
        private int a, b, c, d, dn1, dn2;
        private float u1, v1;
        private float u2, v2;
        private float u3, v3;
        private float w1, w2, w3;
        private float x1, y1, z1;
        private float x2, y2, z2;
        private float x3, y3, z3;


        public List<Vertex> Vertexs
        {
            get { return m_Vertexs; }
            set { m_Vertexs = value; }
        }

        public Triangle(Vertex a,Vertex b,Vertex c)
        {
            m_Vertexs = new List<Vertex>();
            m_Vertexs.Add(a);
            m_Vertexs.Add(b);
            m_Vertexs.Add(c);
        }


        public void PreCallLerp()
        {

            Vector4 p1 = m_Vertexs[0].ScreenPosition;
            Vector4 p2 = m_Vertexs[1].ScreenPosition;
            Vector4 p3 = m_Vertexs[2].ScreenPosition;

            a = (int)(p2.x - p1.x);
            b = (int)(p3.x - p1.x);
            c = (int)(p2.y - p1.y);
            d = (int)(p3.y - p1.y);

            dn1 = (b * c - a * d);
            dn2 = (a * d - b * c);

            u1 = m_Vertexs[0].UV.x / m_Vertexs[0].ClipPosition.w;
            u2 = m_Vertexs[1].UV.x / m_Vertexs[1].ClipPosition.w;
            u3 = m_Vertexs[2].UV.x / m_Vertexs[2].ClipPosition.w;
            v1 = m_Vertexs[0].UV.y / m_Vertexs[0].ClipPosition.w;
            v2 = m_Vertexs[1].UV.y / m_Vertexs[1].ClipPosition.w;
            v3 = m_Vertexs[2].UV.y / m_Vertexs[2].ClipPosition.w;

            w1 = 1.0f / m_Vertexs[0].ClipPosition.w;
            w2 = 1.0f / m_Vertexs[1].ClipPosition.w;
            w3 = 1.0f / m_Vertexs[2].ClipPosition.w;

            x1 = m_Vertexs[0].Normal.x / m_Vertexs[0].ClipPosition.w;
            x2 = m_Vertexs[1].Normal.x / m_Vertexs[1].ClipPosition.w;
            x3 = m_Vertexs[2].Normal.x / m_Vertexs[2].ClipPosition.w;
            y1 = m_Vertexs[0].Normal.y / m_Vertexs[0].ClipPosition.w;
            y2 = m_Vertexs[1].Normal.y / m_Vertexs[1].ClipPosition.w;
            y3 = m_Vertexs[2].Normal.y / m_Vertexs[2].ClipPosition.w;
            z1 = m_Vertexs[0].Normal.z / m_Vertexs[0].ClipPosition.w;
            z2 = m_Vertexs[1].Normal.z / m_Vertexs[1].ClipPosition.w;
            z3 = m_Vertexs[2].Normal.z / m_Vertexs[2].ClipPosition.w;

        }

        public void CallLerp(Vector4 p)
        {
            Vector4 p1 = m_Vertexs[0].ScreenPosition;
            float dx = p.x - p1.x;
            float dy = p.y - p1.y;
            Weight1 = (b * dy - d * dx) / dn1;
            weight2 = (a * dy - d * dx) / dn2;
        }

        public float LerpValue(float a,float b,float c)
        {
            return (1 - Weight1 - weight2) * a + Weight1 * b + weight2 * c;
        }

        public Vector2 GetUV()
        {
            float u = LerpValue(u1, u2, u3);
            float v = LerpValue(v1, v2, v3);
            float w = LerpValue(w1, w2, w3);
            return new Vector2(u / w, v / w);
        }

        public Vector4 GetNormal()
        {
            float x = LerpValue(x1, x2, x3);
            float y = LerpValue(y1, y2, y3);
            float z = LerpValue(z1, z2, z3);
            float w = LerpValue(w1, w2, w3);
            return new Vector4(x, y, z, 0);
        }

    }
}
