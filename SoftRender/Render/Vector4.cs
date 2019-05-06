using System;

namespace SoftRender.Render
{
	struct Vector4
	{
		public float X;
		public float Y;
		public float Z;
		public float W;

		public float Length
		{
			get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z); }
		}

		public Vector4(float x, float y, float z, float w) : this()
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		public Vector4(Vector4 vector)
		{
			this.X = vector.X;
			this.Y = vector.Y;
			this.Z = vector.Z;
			this.W = vector.W;
		}

		/// <summary>
		/// 矢量的加法
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector4 operator +(Vector4 a,Vector4 b)
		{
			return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, 1);
		}

		/// <summary>
		/// 矢量的减法
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector4 operator -(Vector4 a,Vector4 b)
		{
			return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, 1);
		}

		/// <summary>
		/// 矢量的值 乘法
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector4 operator *(Vector4 a, float b)
		{
			return new Vector4(a.X * b, a.Y * b, a.Z * b, 1);
		}

		/// <summary>
		/// 向量和矩阵相乘
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static Vector4 operator *(Vector4 lhs, Matrix4x4 rhs)
		{
			Vector4 v = new Vector4();
			v.X = lhs.X * rhs[0, 0] + lhs.Y * rhs[1, 0] + lhs.Z * rhs[2, 0] + lhs.W * rhs[3, 0];
			v.Y = lhs.X * rhs[0, 1] + lhs.Y * rhs[1, 1] + lhs.Z * rhs[2, 1] + lhs.W * rhs[3, 1];
			v.Z = lhs.X * rhs[0, 2] + lhs.Y * rhs[1, 2] + lhs.Z * rhs[2, 2] + lhs.W * rhs[3, 2];
			v.W = lhs.X * rhs[0, 3] + lhs.Y * rhs[1, 3] + lhs.Z * rhs[2, 3] + lhs.W * rhs[3, 3];
			return v;
		}
		/// <summary>
		/// 矢量与值得 除法
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector4 operator /(Vector4 a, float b)
		{
			if (b != 0)
				return new Vector4(a.X / b, a.Y / b, a.Z / b, 1);
			return a;
		}

		/// <summary>
		/// 点乘
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		/// <summary>
		/// 叉乘
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static Vector4 Cross(Vector4 a, Vector4 b)
		{
			float m1, m2, m3;
			m1 = a.Y * b.Z - a.Z * b.Y;
			m2 = a.Z * b.X - a.X * b.Z;
			m3 = a.X * b.Y - a.Y * b.X;
			return new Vector4(m1, m2, m3, 1f);
		}

		/// <summary>
		/// 归一化
		/// </summary>
		public Vector4 Normalize()
		{
			float length = (float)Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
			if (length != 0.0f)
			{
				float inv = 1.0f / length;
				this.X *= inv;
				this.Y *= inv;
				this.Z *= inv;
			}
			return this;
		}

		/// <summary>
		/// 交换两个Vector4
		/// </summary>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		public static void SwapVector4(ref Vector4 p1, ref Vector4 p2)
		{
			var tmp = p1;
			p1 = p2;
			p2 = tmp;
		}
	
	}
}
