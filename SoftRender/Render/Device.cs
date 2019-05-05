using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace SoftRender.Render
{

    public enum RenderMode
    {
        WIREFRAME = 0,
        VERTEXCOLOR,
        TEXTURED,
        CUBETEXTURED,
    }

    class Device
    {
        public static Vector4 sClipMin = new Vector4(-1, -1, -1, -1);
        public static Vector4 sClipMax = new Vector4(1, 1, 1, 1);

        private Bitmap m_Bmp;
        private BitmapData m_BmpData;
        private int m_Height;
        private int m_Width;
        private readonly float[] m_DepthBuffer;
        private RenderMode m_RenderMode;

        public int Height
        {
            get { return m_Height; }
        }

        public int Width
        {
            get { return m_Width; }
        }

        public RenderMode RenderMode
        {
            get { return m_RenderMode; }
            set { m_RenderMode = value; }
        }

        public Device(Bitmap bmp)
        {
            m_Bmp = bmp;
            m_Height = bmp.Height;
            m_Width = bmp.Width;
            m_DepthBuffer = new float[bmp.Height * bmp.Width];
            m_RenderMode = RenderMode.WIREFRAME;
        }

        public void Clear(BitmapData bmpData)
        {
            for(int index = 0; index < m_DepthBuffer.Length; index++)
            {
                m_DepthBuffer[index] = float.MaxValue;
            }
            //unsafe
            //{
            //    byte* ptr = (byte*)(bmpData.Scan0);
            //    for(int i = 0; i < bmpData.Height; i++)
            //    {
            //        for(int j = 0; j < bmpData.Width; j++)
            //        {
            //            *ptr = 0;
            //            *(ptr + 1) = 0;
            //            *(ptr + 2) = 0;
            //            ptr += 3;
            //        }
            //        ptr += bmpData.Stride - bmpData.Width * 3;
            //    }
            //}
        }


        public void PutPixel(int x,int y,float z,Color col)
        {
            int index = (x + y * Width);
            if (m_DepthBuffer[index] < z)
                return;
            m_DepthBuffer[index] = z;
        }

        public Vector4 Projection(Vector4 vector,Matrix4x4 mvp)
        {
            Vector4 point = mvp.LeftApply(vector);
            Vector4 viewPoint = ProjectionDev(point);
            return viewPoint;
        }

        private Vector4 ProjectionDev(Vector4 v)
        {
            Vector4 vec = new Vector4();
            float rhw = 1.0f / v.x;
            vec.x = (1.0f + v.x * rhw) * Width * 0.5f;
            vec.y = (1.0f - v.y * rhw) * Height * 0.5f;
            vec.z = v.z * rhw;
            vec.w = 1.0f;
            return vec;
        }

        public Vector4 ToHomogeneous(Vector4 x,Matrix4x4 mat)
        {
            Vector4 val = mat.LeftApply(x);
            float rh = 1.0f / val.w;
            val.x = val.x * rh;
            val.y = val.y * rh;
            val.z = val.z * rh;
            return val;
        }


        public void DrawPoint(Vector4 point,Color col)
        {
            if (point.x >= 0 && point.y >= 0 && point.x <= Width && point.y <= Height)
            {
                if (point.x == Width)
                    point.x = point.x - 1;
                if (point.y == Height)
                    point.y = point.y - 1;

                PutPixel((int)point.x, (int)point.y, point.z, col);
            }
        }

        public void DrawLine(Vector4 point0, Vector4 point1, Scene scene, Vertex v1, Vertex v2)
        {
            int x0 = (int)point0.x;
            int y0 = (int)point0.y;
            int x1 = (int)point1.x;
            int y1 = (int)point1.y;

            int dx = x1 - x0;
            int dy = y1 - y0;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            if (steps == 0)
                return;

            float offsetX = (float)dx / (float)steps;
            float offsetY = (float)dy / (float)steps;

            float x = x0;
            float y = y0;

            Color vColor = new Color(128, 128, 128);
            for (int i = 1; i <= steps; i++)
            {
                float dt = (float)(i) / (float)steps;
                vColor = MathUntil.Lerp(v1.Color, v2.Color, dt);
                float z = MathUntil.Lerp(point0.z, point1.z, dt);
                if (float.IsNaN(z))
                    return;

                DrawPoint(new Vector4((int)x, (int)y, z, 0), vColor);
                x += offsetX;
                y += offsetY;
            }
        }

        public void Render(Scene scene, BitmapData bmp, Matrix4x4 viewMat, Matrix4x4 proMat)
        {
            m_BmpData = bmp;
            if (scene != null)
                scene.Render(this, viewMat, proMat);
        }

        public List<Triangle> GetDrawTriangleList(List<Vertex> vertex)
        {
            List<Triangle> t = new List<Triangle>();
            for (int i = 0; i < vertex.Count - 2; i++)
            {
                t.Add(new Triangle(vertex[0], vertex[i + 1], vertex[i + 2]));
            }
            return t;
        }

        public Vector4 ViewPort(Vector4 x)
        {
            Vector4 val = new Vector4();
            val.x = (1.0f + x.x) * Width * 0.5f;
            val.y = (1.0f - x.y) * Height * 0.5f;
            val.z = x.z;
            val.w = 1.0f;
            return val;
        }

        public bool IsInBack(Triangle tri)
        {
            Vector4 v1 = tri.Vertexs[0].ScreenPosition;
            Vector4 v2 = tri.Vertexs[1].ScreenPosition;
            Vector4 v3 = tri.Vertexs[2].ScreenPosition;
            v1.z = v2.z = v3.z = 0;
            Vector4 v1v2 = v2 - v1;
            Vector4 v1v3 = v3 - v1;
            return Vector4.Cross(v1v2, v1v3).z > 0;
        }

        public Color GetPixelColor(float u, float v, TextureMap texture)
        {
            int x = Math.Abs((int)((1f - u) * texture.GetWidth()) % texture.GetWidth());
            int y = Math.Abs((int)((1f - v) * texture.GetHeight()) % texture.GetHeight());

            byte red = 0;
            byte green = 0;
            byte blue = 0;
            return new Color(red, green, blue);
        }
    }
}
