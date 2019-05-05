using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Matrix4x4
    {
        private float[,] _m = new float[4, 4];
        public Matrix4x4()
        {

        }

        public Matrix4x4(float a1, float a2, float a3, float a4,
            float b1, float b2, float b3, float b4,
            float c1, float c2, float c3, float c4,
            float d1, float d2, float d3, float d4)
        {
            _m[0, 0] = a1;  _m[0, 1] = a2;  _m[0, 2] = a3;  _m[0, 3] = a4;
            //
            _m[1, 0] = b1;  _m[1, 1] = b2;  _m[1, 2] = b3;  _m[1, 3] = b4;
            //
            _m[2, 0] = c1;  _m[2, 1] = c2;  _m[2, 2] = c3;  _m[2, 3] = c4;
            //
            _m[3, 0] = d1;  _m[3, 1] = d2;  _m[3, 2] = d3;  _m[3, 3] = d4;
        }

        public float this[int i,int j]
        {
            get
            {
                return _m[i,j];
            }
            set
            {
                _m[i, j] = value;
            }
        }

        public static Matrix4x4 operator +(Matrix4x4 right, Matrix4x4 left)
        {
            Matrix4x4 res = new Matrix4x4();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    res._m[i, j] = right._m[i, j] + left._m[i, j];
                }
            }
                return res;
        }

        public static Matrix4x4 operator -(Matrix4x4 right, Matrix4x4 left)
        {
            Matrix4x4 res = new Matrix4x4();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    res._m[i,j] = right._m[i, j] - left._m[i, j];
                }
            }
            return res;
        }
         

        public static Matrix4x4 operator *(Matrix4x4 right, Matrix4x4 left)
        {
            Matrix4x4 res = new Matrix4x4();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    for (int k = 0; k < 4; ++k)
                    {
                        res._m[i, j] += right._m[i, k] * left._m[k, j];
                    }
                }
            }
            return res;
        }

        public static Matrix4x4 operator *(Matrix4x4 matrix, float k)
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    matrix._m[i, j] *= k;
                }
            }
            return matrix;
        }

        public Vector4 LeftApply(Vector4 vec)
        {
            Vector4 res = new Vector4();
            res.x = vec.x * _m[0, 0] + vec.y * _m[1, 0] + vec.z * _m[2, 0] + vec.w * _m[3, 0];
            res.y = vec.x * _m[0, 1] + vec.y * _m[1, 1] + vec.z * _m[2, 1] + vec.w * _m[3, 1];
            res.z = vec.x * _m[0, 2] + vec.y * _m[1, 2] + vec.z * _m[2, 2] + vec.w * _m[3, 2];
            res.w = vec.x * _m[0, 3] + vec.y * _m[1, 3] + vec.z * _m[2, 3] + vec.w * _m[3, 3];
            return res;
        }

        public static Matrix4x4 RotateX(float rotateX)
        {
            Matrix4x4 matrix = new Matrix4x4();
            rotateX = rotateX * (float)Math.PI / 180;
            matrix[1, 1] = (float)Math.Cos(rotateX);
            matrix[1, 2] = (float)Math.Sin(rotateX);
            matrix[2, 1] = (float)Math.Sin(rotateX);
            matrix[2, 2] = (float)Math.Cos(rotateX);
            return matrix;
        }

        public static Matrix4x4 RotateY(float rotateY)
        {
            Matrix4x4 matrix = new Matrix4x4();
            rotateY = rotateY * (float)Math.PI / 180;
            matrix[0, 0] = (float)Math.Cos(rotateY);
            matrix[0, 2] = (float)Math.Sin(rotateY);
            matrix[2, 0] = (float)Math.Sin(rotateY);
            matrix[2, 2] = (float)Math.Cos(rotateY);
            return matrix;
        }

        public void SetZero()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    _m[i, j] = 0;
                }
            }
        }

        public void Identity()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i == j)
                    {
                        _m[i, j] = 1;
                    }
                    else
                    {
                        _m[i, j] = 0;
                    }
                }
            }
        }

        public  void Transpose()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    float tmp = _m[i, j];
                    _m[i, j] = _m[j, i];
                    _m[j, i] = tmp;
                }
            }
        }


    }
}
