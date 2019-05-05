using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class MathUntil
    {



        public static Matrix4x4 GetRotateX(float f)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.Identity();
            matrix[1, 1] = (float)(System.Math.Cos(f));
            matrix[1, 2] = (float)(System.Math.Sin(f));
            matrix[2, 1] = (float)(System.Math.Sin(f));
            matrix[2, 2] = (float)(System.Math.Cos(f));
            return matrix;
        }

        public static Matrix4x4 GetRotateY(float f)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.Identity();
            matrix[0, 0] = (float)(System.Math.Cos(f));
            matrix[0, 2] = (float)(System.Math.Sin(f));
            matrix[2, 0] = (float)(System.Math.Sin(f));
            matrix[2, 2] = (float)(System.Math.Cos(f));
            return matrix;
        }

        public static Matrix4x4 GetRotateZ(float f)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.Identity();
            matrix[0, 0] = (float)(System.Math.Cos(f));
            matrix[1, 0] = (float)(System.Math.Sin(f));
            matrix[0, 1] = (float)(System.Math.Sin(f));
            matrix[1, 1] = (float)(System.Math.Cos(f));
            return matrix;
        }

        public static float Lerp(float right, float left, float f)
        {
            return right * f + left * (1 - f);
        }

        public static Color3 Lerp(Color3 c1, Color3 c2, float k)
        {
            byte r = (byte)(c1.R + (c2.R - c1.R) * k);
            byte g = (byte)(c1.G + (c2.G - c1.G) * k);
            byte b = (byte)(c1.B + (c2.B - c1.B) * k);
            return new Color3(r, g, b);
        }

        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float k)
        {
            return new Vector2(
                v1.x + (v2.x - v1.x) * k,
                v1.y + (v2.y - v1.y) * k);
        }

        public static Vector4 Lerp(Vector4 v1, Vector4 v2, float k)
        {
            return new Vector4(
                v1.x + (v2.x - v1.x) * k,
                v1.y + (v2.y - v1.y) * k,
                v1.z + (v2.z - v1.z) * k,
                v1.w + (v2.w - v1.w) * k);
        }

        public static float Range(float f, float min, float max)
        {
            if (f < min)
            {
                f = min;
            }
            else if (f > max)
            {
                f = max;
            }
            return f;
        }
    }
}
