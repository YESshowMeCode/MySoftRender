using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Vertex
    {

        private Vector4 m_Position;
        private Vector4 m_Normal;
        private Vector2 m_UV;
        private Color m_Color;
        private Color m_LightColor;

        private Vector4 m_ScreenPosition;
        private Vector4 m_ClipPosition;


        public Vector4 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Vector4 Normal
        {
            get { return m_Normal; }
            set { m_Normal = value; }
        }

        public Vector2 UV
        {
            get { return m_UV; }
            set { m_UV = value; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public Color LightColor
        {
            get { return m_LightColor; }
            set { m_LightColor = value; }
        }

        public Vector4 ScreenPosition
        {
            get { return m_ScreenPosition; }
            set { m_ScreenPosition = value; }
        }

        public Vector4 ClipPosition
        {
            get { return m_ClipPosition; }
            set { m_ClipPosition = value; }
        }

        public Vertex()
        {
            m_LightColor = new Color(255, 255, 255);
        }

        public Vertex(Vector4 position, Vector4 normal,Vector2 uv,Color col)
        {
            m_Position = position;
            m_Normal = normal;
            m_UV = uv;
            m_Color = col;
            m_LightColor = new Color(255, 255, 255);
        }

        public static void SwapVertex(ref Vertex v1,ref Vertex v2)
        {
            Vertex tempv = v2;
            v2 = v1;
            v1 = tempv;
        }




    }
}
