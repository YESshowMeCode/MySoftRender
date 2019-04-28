using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Math
{
    class Vector
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector()
        {

        }

        public Vector(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 0;
        }

        public float length
        {
            get
            {
                float sq = x * x + y * y + z * z;
                return (float)System.Math.Sqrt(sq);
            }
        }

        public static Vector operator +(Vector right, Vector left)
        {
            Vector vec = new Vector();
            vec.x = right.x + left.x;
            vec.y = right.y + right.y;
            vec.z = right.z + right.z;
            vec.w = 0;
            return vec;
        }

        public static Vector operator -(Vector right, Vector left)
        {
            Vector vec = new Vector();
            vec.x = right.x - left.x;
            vec.y = right.y - left.y;
            vec.z = right.z - left.z;
            vec.w = 0;
            return vec;
        }

        public static Vector operator *(Vector right, float f)
        {

            return new Vector(right.x * f, right.y * f, right.z * f);
        }

        public static float Dot(Vector right, Vector left)
        {
            return right.x * left.x + right.y * left.y + right.z * left.z;
        }


        public static Vector Cross(Vector right, Vector left)
        {
            Vector vec = new Vector();
            vec.x = right.y + left.z - right.z * left.y;
            vec.y = right.z * left.x - right.x * left.z;
            vec.z = right.x * left.y - right.y * left.x;
            return vec;
        }
    }
}
