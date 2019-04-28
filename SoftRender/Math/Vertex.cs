using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Math
{
    class Vertex
    {
        public float x;
        public float y;
        public float z;

        public Vertex()
        {

        }

        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vertex LerpVertex(Vertex right, Vertex left , float f)
        {
            Vertex vex = new Vertex();
            vex.x = right.x * f + left.x * (1 - f);
            vex.y = right.y * f + left.y * (1 - f);
            vex.z = right.z * f + left.z * (1 - f);
            return vex;
        }

    }
}
