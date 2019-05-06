
namespace SoftRender.Render
{
	class Triangle
	{
		private Vertex[] mVertices;
		private float Weight1;
		private float Weight2;
		private int a, b, c, d, dn1, dn2; //差值计算
		private float u1, v1;
		private float u2, v2;
		private float u3, v3;
		private float w1, w2, w3;
		private float x1, y1, z1;
		private float x2, y2, z2;
		private float x3, y3, z3;

		/// <summary>
		/// 三角形的顶点数组
		/// </summary>
		public Vertex[] Vertices
		{
			get { return mVertices; }
			set { mVertices = value; }
		}

		public Triangle(Vertex a,Vertex b,Vertex c)
		{
			this.mVertices = new Vertex []{ a, b, c };
		}

		/// <summary>
		/// 三角形顶点之间差值计算
		/// </summary>
		public void PreCallLerp()
		{
			//p = a*p0+b*p1+c*p2;
			// a+b+c = 1;
			//b = (y-y1)*(x3-x)+(x1-x)(y3-y1)   /   (x1-x2)(y3-y1)+(y1-y2)(x1-x3)
			//c  =(y-y1)(x2-x1)+(y2-y1)(x1-x)   /   (x1-x3)(y2-y1)+(y2-y2)(x1-x2)
			Vector4 p1 = this.mVertices[0].ScreenPosition;
			Vector4 p2 = this.mVertices[1].ScreenPosition;
			Vector4 p3 = this.mVertices[2].ScreenPosition;
			//得到 P1 P2 P3 的 x y 值相互之间的差值
			a = (int)(p2.X - p1.X);
			b = (int)(p3.X - p1.X);
			c = (int)(p2.Y - p1.Y);
			d = (int)(p3.Y - p1.Y);
			dn1 = (b * c - a * d);
			dn2 = (a * d - b * c);

			u1 = mVertices[0].UV.X / mVertices[0].ClipPosition.W;
			u2 = mVertices[1].UV.X / mVertices[1].ClipPosition.W;
			u3 = mVertices[2].UV.X / mVertices[2].ClipPosition.W;
			v1 = mVertices[0].UV.Y / mVertices[0].ClipPosition.W;
			v2 = mVertices[1].UV.Y / mVertices[1].ClipPosition.W;
			v3 = mVertices[2].UV.Y / mVertices[2].ClipPosition.W;
			w1 = 1f / mVertices[0].ClipPosition.W;
			w2 = 1f / mVertices[1].ClipPosition.W;
			w3 = 1f / mVertices[2].ClipPosition.W;
			x1 = mVertices[0].Normal.X / mVertices[0].ClipPosition.W;
			x2 = mVertices[1].Normal.X / mVertices[1].ClipPosition.W;
			x3 = mVertices[2].Normal.X / mVertices[2].ClipPosition.W;
			y1 = mVertices[0].Normal.Y / mVertices[0].ClipPosition.W;
			y2 = mVertices[1].Normal.Y / mVertices[1].ClipPosition.W;
			y3 = mVertices[2].Normal.Y / mVertices[2].ClipPosition.W;
			z1 = mVertices[0].Normal.Z / mVertices[0].ClipPosition.W;
			z2 = mVertices[1].Normal.Z / mVertices[1].ClipPosition.W;
			z3 = mVertices[2].Normal.Z / mVertices[2].ClipPosition.W;
		}

		/// <summary>
		/// 计算 x1 y1 的值与 x   y 的差值 计算 b c
		/// </summary>
		/// <param name="p"></param>
		public void CallLerp(Vector4 p)
		{
			Vector4 p1 = this.mVertices[0].ScreenPosition;
			float dx = p.X - p1.X;
			float dy = p.Y - p1.Y;
			Weight1 = (float)(b * dy - d * dx) / (float)dn1;
			Weight2 = (float)(a * dy - c * dx) / (float)dn2;
		}
		
		/// <summary>
		/// 线性差值
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="c"></param>
		/// <returns></returns>
		public float LerpValue(float a, float b, float c)
		{
			return (1 - Weight1 - Weight2) * a + Weight1 * b + Weight2 * c;
		}

		/// <summary>
		/// 计算UV坐标
		/// </summary>
		/// <returns></returns>
		public Vector2 GetUV()
		{
			float u = LerpValue(u1, u2, u3);
			float v = LerpValue(v1, v2, v3);
			float w = LerpValue(w1, w2, w3);
			return new Vector2(u / w, v / w);
		}

		/// <summary>
		/// 计算三角的法线
		/// </summary>
		/// <returns></returns>
		public Vector4 GetNormal()
		{
			float x = LerpValue(x1, x2, x3);
			float y = LerpValue(y1, y2, y3);
			float z = LerpValue(z1, z2, z3);
			float w = LerpValue(w1, w2, w3);
			return new Vector4(x, y, z, 0);
		}
	}
}
