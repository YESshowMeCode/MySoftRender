using System;

namespace SoftRender.Render
{
	//矩阵类
	class Matrix4x4
	{
		//用数组表示4x4矩阵的表示
		public float[,] matrix;

		/// <summary>
		/// 直接访问某个位置的值
		/// </summary>
		/// <param name="i"></param>
		/// <param name="j"></param>
		/// <returns></returns>
		public float this[int i, int j]
		{
			get { return matrix[i, j]; }
			set { matrix[i, j] = value; }
		}
	
		public Matrix4x4(int x)
		{
			matrix = new float[4, 4];
			this.Identity();
		}

		/// <summary>
		/// 标准化向量
		/// </summary>
		public void Identity()
		{
			this.matrix[0, 0] = this.matrix[1, 1] = this.matrix[2, 2] = this.matrix[3, 3] = 1.0f;
			this.matrix[0, 1] = this.matrix[0, 2] = this.matrix[0, 3] = 0.0f;
			this.matrix[1, 0] = this.matrix[1, 2] = this.matrix[1, 3] = 0.0f;
			this.matrix[2, 0] = this.matrix[2, 1] = this.matrix[2, 3] = 0.0f;
			this.matrix[3, 0] = this.matrix[3, 1] = this.matrix[3, 2] = 0.0f;
		}

		/// <summary>
		/// 矩阵相加
		/// </summary>
		/// <param name="right"></param>
		/// <param name="left"></param>
		/// <returns></returns>
		public static Matrix4x4 operator +(Matrix4x4 right, Matrix4x4 left)
		{
			Matrix4x4 temp = new Matrix4x4(1);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					temp.matrix[i, j] = right.matrix[i, j] + left.matrix[i, j];
				}
			}
			return temp;
		}
		
		/// <summary>
		/// 矩阵相减
		/// </summary>
		/// <param name="right"></param>
		/// <param name="left"></param>
		/// <returns></returns>
		public static Matrix4x4 operator -(Matrix4x4 right, Matrix4x4 left)
		{
			Matrix4x4 temp = new Matrix4x4(1);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					temp.matrix[i, j] = right.matrix[i, j] + left.matrix[i, j];
				}
			}
			return temp;
		}
		
		/// <summary>
		/// 矩阵相乘
		/// </summary>
		/// <param name="right"></param>
		/// <param name="left"></param>
		/// <returns></returns>
		public static Matrix4x4 operator *(Matrix4x4 right, Matrix4x4 left)
		{
			Matrix4x4 temp = new Matrix4x4(1);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					temp.matrix[j, i] = (right.matrix[j, 0] * left.matrix[0, i]) +
						(right.matrix[j, 1] * left.matrix[1, i]) +
						(right.matrix[j, 2] * left.matrix[2, i]) +
						(right.matrix[j, 3] * left.matrix[3, i]);
				}
			}
			return temp;
		}
	
		/// <summary>
		/// 矩阵乘等
		/// </summary>
		/// <param name="matrix1"></param>
		/// <returns></returns>
		public Matrix4x4 mul(Matrix4x4 matrix1)
		{
			Matrix4x4 mm = new Matrix4x4(1);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					for (int k = 0; k < 4; k++)
					{
						mm.matrix[i, j] += this.matrix[j, k] * matrix1.matrix[k, j];
					}
				}
			}
			return mm;
		}

		/// <summary>
		/// 放缩数组
		/// </summary>
		/// <param name="c"></param>
		public void Scale(float c)
		{
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					this.matrix[i, j] = c * this.matrix[i, j];
				}
			}

		}

        public static Vector4 operator *(Matrix4x4 matrix, Vector4 vector)
        {
            Vector4 V = new Vector4();
            V.X = vector.X * matrix[0, 0] + vector.Y * matrix[1, 0] + vector.Z * matrix[2, 0] + vector.W * matrix[3, 0];
            V.Y = vector.X * matrix[0, 1] + vector.Y * matrix[1, 1] + vector.Z * matrix[2, 1] + vector.W * matrix[3, 1];
            V.Z = vector.X * matrix[0, 2] + vector.Y * matrix[1, 2] + vector.Z * matrix[2, 2] + vector.W * matrix[3, 2];
            V.W = vector.X * matrix[0, 3] + vector.Y * matrix[1, 3] + vector.Z * matrix[2, 3] + vector.W * matrix[3, 3];
            return V;
        }


		/// <summary>
		/// 平移变换矩阵
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public void TranslateM(float x, float y, float z)
		{
			this.matrix[3, 0] = x;
			this.matrix[3, 1] = y;
			this.matrix[3, 2] = z;
		}

		/// <summary>
		/// 缩放
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <returns></returns>
		public Matrix4x4 ScaleM(float x,float y,float z)
		{
			this.Identity();
			this.matrix[0, 0] = x;
			this.matrix[1, 1] = y;
			this.matrix[2, 2] = z;
			return this;
		}

		/// <summary>
		/// 旋转 Rotate
		/// </summary>
		/// <param name="rotateX"></param>
		/// <param name="rotateY"></param>
		/// <param name="rotateZ"></param>
		public void RotateM(float rotateX,float rotateY,float rotateZ)
		{
			rotateX = rotateX *(float) Math.PI / 180;
			rotateY = rotateY * (float)Math.PI / 180;
			rotateZ = rotateZ * (float)Math.PI / 180;
			Matrix4x4 z = new Matrix4x4(1);
			z.matrix[0, 0] = (float)Math.Cos(rotateZ);
			z.matrix[0, 1] = (float)Math.Sin(rotateZ);
			z.matrix[1, 0] = -(float)Math.Sin(rotateZ);
			z.matrix[1, 1] = (float)Math.Cos(rotateZ);
			Matrix4x4 x = new Matrix4x4(1);
			x.matrix[1, 1] = (float)Math.Cos(rotateX);
			x.matrix[1, 2] = -(float)Math.Sin(rotateX);
			x.matrix[2, 1] = (float)Math.Sin(rotateX);
			x.matrix[2, 2] = (float)Math.Cos(rotateX);
			Matrix4x4 y = new Matrix4x4(1);
			y.matrix[0, 0] = (float)Math.Cos(rotateY);
			y.matrix[0, 2] = (float)Math.Sin(rotateY);
			y.matrix[2, 0] = -(float)Math.Sin(rotateY);
			y.matrix[2, 2] = (float)Math.Cos(rotateY);

			this.Identity();
			Matrix4x4 final = y * x * z;
			for (int i = 0; i < 4; i++)
				for (int j = 0; j < 4; j++)
					this.matrix[i, j] = final.matrix[i, j];
		}

		/// <summary>
		/// x轴旋转 Rotate
		/// </summary>
		/// <param name="rotateX"></param>
		/// <returns></returns>
		public static Matrix4x4 RotateX(float rotateX)
		{
			rotateX = rotateX * (float)Math.PI / 180;
			Matrix4x4 x = new Matrix4x4(1);
			x.matrix[1, 1] = (float)Math.Cos(rotateX);
			x.matrix[1, 2] = (float)Math.Sin(rotateX);
			x.matrix[2, 1] = -(float)Math.Sin(rotateX);
			x.matrix[2, 2] = (float)Math.Cos(rotateX);
			return x;
		}

		/// <summary>
		/// Y轴旋转 Rotate
		/// </summary>
		/// <param name="rotateY"></param>
		/// <returns></returns>
		public static Matrix4x4 RotateY(float rotateY)
		{
			rotateY = rotateY * (float)Math.PI / 180;

			Matrix4x4 y = new Matrix4x4(1);
			y.matrix[0, 0] = (float)Math.Cos(rotateY);
			y.matrix[0, 2] = -(float)Math.Sin(rotateY);
			y.matrix[2, 0] = (float)Math.Sin(rotateY);
			y.matrix[2, 2] = (float)Math.Cos(rotateY);
			return y;
		}

		/// <summary>
		/// 归零
		/// </summary>
		public void SetZero()
		{
			this.matrix[0, 0] = this.matrix[0, 1] = this.matrix[0, 2] = this.matrix[0, 3] = 0.0f;
			this.matrix[1, 0] = this.matrix[1, 1] = this.matrix[1, 2] = this.matrix[1, 3] = 0.0f;
			this.matrix[2, 0] = this.matrix[2, 1] = this.matrix[2, 2] = this.matrix[2, 3] = 0.0f;
			this.matrix[3, 0] = this.matrix[3, 1] = this.matrix[3, 2] = this.matrix[3, 3] = 0.0f;
		}
	}
}
		

   

