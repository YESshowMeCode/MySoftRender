using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Math
{
    class MathUntil
    {

        public static Matrix GetTranslate(float x, float y, float z)
        {
            return new Matrix(1, 0, 0, 0,
                              0, 1, 0, 0,
                              0, 0, 1, 0,
                              x, y, z, 1);
        }

        public static Matrix GetScale(float x, float y, float z)
        {
            return new Matrix(x, 0, 0, 0,
                              0, y, 0, 0,
                              0, 0, z, 0,
                              0, 0, 0, 1);
        }

        public static Matrix GetRotateX(float f)
        {
            Matrix matrix = new Matrix();
            matrix.Identity();
            matrix[1, 1] = (float)(System.Math.Cos(f));
            matrix[1, 2] = (float)(System.Math.Sin(f));
            matrix[2, 1] = (float)(System.Math.Sin(f));
            matrix[2, 2] = (float)(System.Math.Cos(f));
            return matrix;
        }

        public static Matrix GetRotateY(float f)
        {
            Matrix matrix = new Matrix();
            matrix.Identity();
            matrix[0, 0] = (float)(System.Math.Cos(f));
            matrix[0, 2] = (float)(System.Math.Sin(f));
            matrix[2, 0] = (float)(System.Math.Sin(f));
            matrix[2, 2] = (float)(System.Math.Cos(f));
            return matrix;
        }

        public static Matrix GetRotateZ(float f)
        {
            Matrix matrix = new Matrix();
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
