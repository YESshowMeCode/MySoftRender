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

        public void Normalize()
        {
            x = x / length;
            y = y / length;
            z = z / length;
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

        public static Vector operator /(Vector right,float f)
        {
            return new Vector(right.x / f, right.y / f, right.z / f);
        }

        public Vector MultiplyMatrix(Matrix matrix)
        {
            Vector vec = new Vector();
            vec.x = x * matrix[0, 0] + y * matrix[0, 1] + z * matrix[0, 2] + w * matrix[0, 3];
            vec.y = x * matrix[1, 0] + y * matrix[1, 1] + z * matrix[1, 2] + w * matrix[1, 3];
            vec.z = x * matrix[2, 0] + y * matrix[2, 1] + z * matrix[2, 1] + w * matrix[2, 3];
            vec.w = x * matrix[3, 0] + y * matrix[3, 1] + z * matrix[3, 2] + w * matrix[3, 3];
            return vec;
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
