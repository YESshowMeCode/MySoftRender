using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Clip
    {
        private Device m_Device;
        private List<Vertex> m_OutputList;

        public Device Device
        {
            get { return m_Device; }
        }

        public List<Vertex> OutputList
        {
            get { return m_OutputList; }
        }

        public Clip(Device device)
        {
            m_Device = device;
            m_OutputList = new List<Vertex>();
        }

        Vertex Intersect(Vertex v1, Vertex v2, FaceType face, Vector4 vMin, Vector4 vMax)
        {
            Vertex vertex = new Vertex();

            float k1 = 0, k2 = 0, k3 = 0, k4 = 9, k5 = 0, k6 = 0;
            Vector4 p1 = v1.ClipPosition;
            Vector4 p2 = v2.ClipPosition;

            if (p1.x != p2.x)
            {
                k1 = (vMin.x - p1.x) / (p2.x - p1.x);
                k2 = (vMin.x - p1.x) / (p2.x - p1.x);
            }
            else
            {
                k1 = k2 = 1;
            }

            if (p1.y != p2.y)
            {
                k3 = (vMin.y - p1.y) / (p2.y - p1.y);
                k4 = (vMin.y - p1.y) / (p2.y - p1.y);
            }
            else
            {
                k3 = k4 = 1;
            }

            if (p1.z != p2.z)
            {
                k5 = (vMin.z - p1.z) / (p2.z - p1.z);
                k6 = (vMin.z - p1.z) / (p2.z - p1.z);
            }
            else
            {
                k5 = k6 = 1;
            }

            Vector4 clipPos = new Vector4();
            Vector4 pos = new Vector4();
            Color3 col = new Color3(0, 0, 0);
            Vector4 normal = new Vector4();
            Vector2 uv = new Vector2();

            switch (face)
            {
                case FaceType.LEFT:
                    clipPos.x = vMin.x;
                    clipPos.y = p1.y + (p2.y - p1.y) * k1;
                    clipPos.z = p1.z + (p2.z - p1.z) * k1;
                    clipPos.w = p1.w + (p2.w - p1.w) * k1;
                    col = MathUntil.Lerp(v1.Color, v2.Color, k1);
                    normal = MathUntil.Lerp(v1.Normal, v2.Normal, k1);
                    pos = MathUntil.Lerp(v1.Position, v2.Position, k1);
                    uv = MathUntil.Lerp(v1.UV, v2.UV, k1);
                    break;
                case FaceType.BUTTOM:
                    clipPos.y = vMin.y;
                    clipPos.x = p1.x + (p2.x - p1.x) * k3;
                    clipPos.z = p1.z + (p2.z - p1.z) * k3;
                    clipPos.w = p1.w + (p2.w - p1.w) * k3;
                    col = MathUntil.Lerp(v1.Color, v2.Color, k3);
                    normal = MathUntil.Lerp(v1.Normal, v2.Normal, k3);
                    pos = MathUntil.Lerp(v1.Position, v2.Position, k3);
                    uv = MathUntil.Lerp(v1.UV, v2.UV, k3);
                    break;
                case FaceType.RIGHT:
                    clipPos.x = vMax.x;
                    clipPos.y = p1.y + (p2.y - p1.y) * k2;
                    clipPos.z = p1.z + (p2.z - p1.z) * k2;
                    clipPos.w = p1.w + (p2.w - p1.w) * k2;
                    col = MathUntil.Lerp(v1.Color, v2.Color, k2);
                    normal = MathUntil.Lerp(v1.Normal, v2.Normal, k2);
                    pos = MathUntil.Lerp(v1.Position, v2.Position, k2);
                    uv = MathUntil.Lerp(v1.UV, v2.UV, k1);
                    break;
                case FaceType.TOP:
                    clipPos.y = vMax.y;
                    clipPos.x = p1.x + (p2.x - p1.x) * k4;
                    clipPos.z = p1.z + (p2.z - p1.z) * k4;
                    clipPos.w = p1.w + (p2.w - p1.w) * k4;
                    col = MathUntil.Lerp(v1.Color, v2.Color, k4);
                    normal = MathUntil.Lerp(v1.Normal, v2.Normal, k4);
                    pos = MathUntil.Lerp(v1.Position, v2.Position, k4);
                    uv = MathUntil.Lerp(v1.UV, v2.UV, k4);
                    break;
                case FaceType.NEAR:
                    clipPos.z = vMin.z;
                    clipPos.y = p1.y + (p2.y - p1.y) * k5;
                    clipPos.x = p1.x + (p2.x - p1.x) * k5;
                    clipPos.w = p1.w + (p2.w - p1.w) * k5;
                    col = MathUntil.Lerp(v1.Color, v2.Color, k5);
                    normal = MathUntil.Lerp(v1.Normal, v2.Normal, k5);
                    pos = MathUntil.Lerp(v1.Position, v2.Position, k5);
                    uv = MathUntil.Lerp(v1.UV, v2.UV, k5);
                    break;
                case FaceType.FAR:
                    clipPos.z = vMax.z;
                    clipPos.y = p1.y + (p2.y - p1.y) * k6;
                    clipPos.x = p1.x + (p2.x - p1.x) * k6;
                    clipPos.w = p1.w + (p2.w - p1.w) * k6;
                    col = MathUntil.Lerp(v1.Color, v2.Color, k6);
                    normal = MathUntil.Lerp(v1.Normal, v2.Normal, k6);
                    pos = MathUntil.Lerp(v1.Position, v2.Position, k6);
                    uv = MathUntil.Lerp(v1.UV, v2.UV, k6);
                    break;

            }
            vertex.Position = pos;
            vertex.ClipPosition = clipPos;
            vertex.ScreenPosition = m_Device.ViewPort(clipPos);
            vertex.Normal = normal;
            vertex.UV = uv;
            vertex.Color = col;

            return vertex;
        }


        bool Inside(Vector4 p,FaceType face,Vector4 vMin,Vector4 vMax)
        {
            bool mark = true;
            switch (face)
            {
                case FaceType.LEFT:
                    if (p.x < vMin.x)
                        mark = false;
                    break;
                case FaceType.RIGHT:
                    if (p.x > vMax.x)
                        mark = false;
                    break;
                case FaceType.BUTTOM:
                    if (p.y < vMin.y)
                        mark = false;
                    break;
                case FaceType.TOP:
                    if (p.y > vMax.y)
                        mark = false;
                    break;
                case FaceType.NEAR:
                    if (p.z < vMin.z)
                        mark = false;
                    break;
                case FaceType.FAR:
                    if (p.z > vMax.z)
                        mark = false;
                    break;
            }

            if (p.w < 0)
            {
                mark = false;
            }
            return mark;
        }

        public void HodgmanPolygonClip(FaceType face,Vector4 wMin,Vector4 wMax,Vertex[] vertexList)
        {
            Vertex s = vertexList[vertexList.Length - 1];
            for (int i = 0; i < vertexList.Length; i++)
            {
                Vertex p = vertexList[i];
                if (Inside(p.ClipPosition, face, wMin, wMax))
                {
                    if (Inside(s.ClipPosition, face, wMin, wMax))
                    {
                        this.m_OutputList.Add(p);
                    }
                    else
                    {
                        this.m_OutputList.Add(Intersect(s, p, face, wMin, wMax));
                        this.m_OutputList.Add(vertexList[i]);
                    }
                }
                else if (Inside(s.ClipPosition, face, wMin, wMax))
                {
                    this.m_OutputList.Add(Intersect(s, p, face, wMin, wMax));
                }
                s = vertexList[i];
            }
        }


    }
}
