using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftRender.Math;

namespace SoftRender.RenderData
{
    class Until
    {
        //
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

        public Matrix CameraMatrix(Vector eyePos,Vector lookPos,Vector upAixs)
        {
            Matrix matrix = new Matrix();

            Vector lookDir = eyePos - lookPos;
            lookDir.Normalize();

            Vector rightDir = Vector.Cross(upAixs, lookDir);
            rightDir.Normalize();

            Vector upDir = Vector.Cross(lookDir, rightDir);
            upDir.Normalize();



            return matrix;
        }

    }
}
