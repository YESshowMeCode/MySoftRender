using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Math
{
    class Matrix
    {
        private float[,] _m = new float[4, 4];
        public Matrix()
        {

        }

        public Matrix(float a1, float a2, float a3, float a4,
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

        public static Matrix operator +(Matrix right, Matrix left)
        {
            Matrix res = new Matrix();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    res._m[i, j] = right._m[i, j] + left._m[i, j];
                }
            }
                return res;
        }

        public static Matrix operator -(Matrix right, Matrix left)
        {
            Matrix res = new Matrix();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    res._m[i,j] = right._m[i, j] - left._m[i, j];
                }
            }
            return res;
        }
         

        public static Matrix operator *(Matrix right, Matrix left)
        {
            Matrix res = new Matrix();
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; i < 4; ++j)
                {
                    for (int k = 0; k < 4; ++k)
                    {
                        res._m[i, j] += right._m[i, k] * left._m[k, j];
                    }
                }
            }
            return res;
        }

        public static Matrix operator *(Matrix matrix, float k)
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
