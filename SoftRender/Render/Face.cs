using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    public enum FaceType
    {
        LEFT = 1,
        RIGHT,
        BUTTOM,
        NEAR,
        FAR,
        NONE,
    }

    class Face
    {
        public int A;
        public int B;
        public int C;
        public FaceType FaceType;

        public Face(int a,int b,int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Face(int a,int b,int c,FaceType faceType)
        {
            A = a;
            B = b;
            C = c;
            FaceType = faceType;
        }

    }
}
