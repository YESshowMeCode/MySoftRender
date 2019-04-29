using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Device
    {
        public int deviceWidth;
        public int deviceheight;

        public Texture tex;
        List<List<float>> zBuffer;

        public void InitDevice(int width,int height)
        {
            deviceheight = height;
            deviceWidth = width;

            zBuffer = new List<List<float>>();
            for (int i = 0; i < width; ++i)
            {
                List<float> tmp = new List<float>(height);
                zBuffer.Add(tmp);
            }

            tex = new Texture();
            tex.LoadTexture("??");

        }


        public void DrawPrimitive(Vertex vex1, Vertex vex2, Vertex vex3, Matrix mvp)
        {
            vex1.point = vex1.point.MultiplyMatrix(mvp);
            vex2.point = vex2.point.MultiplyMatrix(mvp);
            vex3.point = vex3.point.MultiplyMatrix(mvp);

            if (SimpleCVVCullCheck(vex1) && SimpleCVVCullCheck(vex2) && SimpleCVVCullCheck(vex3))
            {
                return;
            }
            RasterizationPrepared(vex1);
            RasterizationPrepared(vex2);
            RasterizationPrepared(vex3);




        }

        public void Release()
        {
            zBuffer.Clear();
        }

        public bool SimpleCVVCullCheck(Vertex vertex)
        {
            float w = vertex.point.w;
            if (vertex.point.x < -w || vertex.point.x > w)
            {
                return true;
            }
            if (vertex.point.y < -w || vertex.point.y > w)
            {
                return true;
            }
            if (vertex.point.z < -w || vertex.point.z > w)
            {
                return true;
            }
            return false;
        }

        public void RasterizationPrepared(Vertex vertex)
        {
            float reciprocalW = 1.0f / vertex.point.w;
            vertex.point.x = (vertex.point.x * reciprocalW + 1.0f) * 0.5f * deviceWidth;
            vertex.point.y = (1.0f - vertex.point.y * reciprocalW) * 0.5f * deviceheight;

            vertex.point.z = reciprocalW;
            vertex.u *= vertex.point.z;
            vertex.v *= vertex.point.z;
        }


        public void RasterizeTrangle(Vertex v1, Vertex v2, Vertex v3)
        {

            if (v2.point.y < v1.point.y)
            {
                SwapVertex(v1, v2);
            }
            if (v3.point.y < v1.point.y)
            {
                SwapVertex(v1, v3);
            }
            if (v3.point.y < v2.point.y)
            {
                SwapVertex(v2, v3);
            }

            int vy1 = (int)v1.point.y;
            int vy2 = (int)v2.point.y;
            int vy3 = (int)v3.point.y;

            

        }

        public void SwapVertex(Vertex v1, Vertex v2)
        {
            Vertex tmp = v1;
            v1 = v2;
            v2 = tmp;
        }

    }
}
