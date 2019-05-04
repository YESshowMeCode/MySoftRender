using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Vector4
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Vector4()
        {

        }

        public Vector4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Vector4(float x, float y, float z)
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

        public Vector4 Normalize()
        {
            return new Vector4(x / length, y / length, z / length, 1.0f);
        }

        public static Vector4 operator +(Vector4 right, Vector4 left)
        {
            Vector4 vec = new Vector4();
            vec.x = right.x + left.x;
            vec.y = right.y + right.y;
            vec.z = right.z + right.z;
            vec.w = 0;
            return vec;
        }

        public static Vector4 operator -(Vector4 right, Vector4 left)
        {
            Vector4 vec = new Vector4();
            vec.x = right.x - left.x;
            vec.y = right.y - left.y;
            vec.z = right.z - left.z;
            vec.w = 0;
            return vec;
        }

        public static Vector4 operator *(Vector4 right, float f)
        {

            return new Vector4(right.x * f, right.y * f, right.z * f, 1);
        }

        public static Vector4 operator /(Vector4 right,float f)
        {
            return new Vector4(right.x / f, right.y / f, right.z / f);
        }


        public static Vector4 operator *(Vector4 right,Matrix left)
        {
            Vector4 res = new Vector4();
            res.x = right.x * left[0, 0] + right.y * left[1, 0] + right.z * left[2, 0] + right.w * left[3, 0];
            res.y = right.x * left[0, 1] + right.y * left[1, 1] + right.z * left[2, 1] + right.w * left[3, 1];
            res.z = right.x * left[0, 2] + right.y * left[1, 2] + right.z * left[2, 2] + right.w * left[3, 2];
            res.w = right.x * left[0, 3] + right.y * left[1, 3] + right.z * left[2, 3] + right.w * left[3, 3];
            return res;
        }

        public Vector4 MultiplyMatrix(Matrix matrix)
        {
            Vector4 vec = new Vector4();
            vec.x = x * matrix[0, 0] + y * matrix[0, 1] + z * matrix[0, 2] + w * matrix[0, 3];
            vec.y = x * matrix[1, 0] + y * matrix[1, 1] + z * matrix[1, 2] + w * matrix[1, 3];
            vec.z = x * matrix[2, 0] + y * matrix[2, 1] + z * matrix[2, 1] + w * matrix[2, 3];
            vec.w = x * matrix[3, 0] + y * matrix[3, 1] + z * matrix[3, 2] + w * matrix[3, 3];
            return vec;
        }

        public static float Dot(Vector4 right, Vector4 left)
        {
            return right.x * left.x + right.y * left.y + right.z * left.z;
        }


        public static Vector4 Cross(Vector4 right, Vector4 left)
        {
            Vector4 vec = new Vector4();
            vec.x = right.y + left.z - right.z * left.y;
            vec.y = right.z * left.x - right.x * left.z;
            vec.z = right.x * left.y - right.y * left.x;
            return vec;
        }
    }
}
