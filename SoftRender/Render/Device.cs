using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace SoftRender.Render
{
	public enum RenderMode
	{
		/// <summary>
		/// 线框模式
		/// </summary>
		WIREFRAME = 0,
		/// <summary>
		/// 顶点色
		/// </summary>
		VERTEXCOLOR,
		/// <summary>
		/// 单张纹理
		/// </summary>
		TEXTURED,
		/// <summary>
		/// 多张纹理
		/// </summary>
		CUBETEXTURED,
	}

	class Device
	{
		public static Vector4 sClipmin = new Vector4(-1, -1, -1, 1);
		public static Vector4 sClipmax = new Vector4(1, 1, 1, 1);

		// 渲染缓冲相关
		private Bitmap m_Bmp;
		private BitmapData m_BmData;
		private int m_Height;
		private int m_Width;
		private readonly float[] m_DepthBuffer;
		private RenderMode m_RenderMode;
		
		/// <summary>
		/// 高
		/// </summary>
		public int Height
		{
			get { return this.m_Height; }
		}

		/// <summary>
		/// 宽
		/// </summary>
		public int Width
		{
			get { return this.m_Width; }
		}

		/// <summary>
		/// 渲染模式
		/// </summary>
		public RenderMode RenderMode
		{
			get { return m_RenderMode; }
			set { m_RenderMode = value; }
		}


		public Device(Bitmap bmp)
		{
			this.m_Bmp = bmp;
			this.m_Height = bmp.Height;
			this.m_Width = bmp.Width;
			this.m_DepthBuffer = new float[bmp.Width * bmp.Height];
			m_RenderMode = RenderMode.TEXTURED;
		}

		/// <summary>
		/// 清除后台缓存
		/// </summary>
		/// <param name="data"></param>
		public void Clear(BitmapData data)
		{
			for (int index = 0; index < m_DepthBuffer.Length; index++)
			{
				m_DepthBuffer[index] = float.MaxValue;
			}
            unsafe
            {
                byte* ptr = (byte*)(data.Scan0);
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {

                        *ptr = 0;
                        *(ptr + 1) = 0;
                        *(ptr + 2) = 0;
                        ptr += 3;
                    }
                    ptr += data.Stride - data.Width * 3;
                }
            }
		}

		/// <summary>
		/// 绘画某个位置的像素
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		/// <param name="color"></param>
		public void Putpixel(int x, int y, float z, Color3 color)
		{
			int index = (x + y * Width);
			if (m_DepthBuffer[index] < z)
				return;

			m_DepthBuffer[index] = z;
            unsafe
            {
                byte* ptr = (byte*)(this.m_BmData.Scan0);
                byte* row = ptr + (y * this.m_BmData.Stride);
                row[x * 3] = color.B;
                row[x * 3 + 1] = color.G;
                row[x * 3 + 2] = color.R;
            }
		}

		/// <summary>
		/// 透视投影.
		/// 过程由两步组成：(1)乘以投影矩阵 (2)透视除法
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="mvp"></param>
		/// <returns></returns>
		public Vector4 Projection(Vector4 vector, Matrix4x4 mvp)
		{
            Vector4 point = mvp * vector;
			Vector4 viewpoint = ProjectionDev(point);
			return viewpoint;
		}

		/// <summary>
		/// 变换到齐次坐标系DNC
        ///
		/// </summary>
		/// <param name="vector"></param>
		/// <param name="MVP"></param>
		/// <returns></returns>
		public Vector4 ToHomogeneousDNC(Vector4 vector, Matrix4x4 MVP)
		{
            //转换到投影坐标系
            Vector4 vec = MVP * vector;
            //转到齐次坐标系
            vec.X = vec.X / vec.W;
            vec.Y = vec.Y / vec.W;
            vec.Z = vec.Z / vec.W;
			return vec;
		}

		/// <summary>
		/// 透视除法
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		private Vector4 ProjectionDev(Vector4 x)
		{
			Vector4 val = new Vector4();
			float rhw = 1.0f / x.W;
			val.X = (1.0f + x.X * rhw) * Width * 0.5f;
			val.Y = (1.0f - x.Y * rhw) * Height * 0.5f;
			val.Z = x.Z * rhw;
			val.W = 1.0f;
			return val;
		}

		/// <summary>
		/// 画一个点
		/// </summary>
		/// <param name="point"></param>
		/// <param name="c"></param>
		public void DrawPoint(Vector4 point, Color3 c)
		{
			if (point.X >= 0 && point.Y >= 0 && point.X <= Width && point.Y <= Height)
			{
				if (point.X == Width)
					point.X = point.X - 1;
				if (point.Y == Height)
					point.Y = point.Y - 1;

				Putpixel((int)point.X, (int)point.Y, point.Z, c);
			}
		}

		/// <summary>
		/// 画一条直线
		/// </summary>
		/// <param name="point0"></param>
		/// <param name="point1"></param>
		/// <param name="scene"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		public void DrawLine(Vector4 point0, Vector4 point1, Scene scene, Vertex v1, Vertex v2)
		{
			int x0 = (int)point0.X;
			int y0 = (int)point0.Y;
			int x1 = (int)point1.X;
			int y1 = (int)point1.Y;

			int dx = x1 - x0;
			int dy = y1 - y0;
			int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
			if (steps == 0)
				return;

			float offsetX = (float)dx / (float)steps;
			float offsetY = (float)dy / (float)steps;

			float x = x0;
			float y = y0;

			Color3 vColor = new Color3(128, 128, 128);
			for (int i = 1; i <= steps; i++)
			{
				float dt = (float)(i) / (float)steps;
				vColor = MathUntily.Lerp(v1.Color, v2.Color, dt);
				float z = MathUntily.Lerp(point0.Z, point1.Z, dt);
				if (float.IsNaN(z))
					return;

				DrawPoint(new Vector4((int)x, (int)y, z, 0), vColor);
				x += offsetX;
				y += offsetY;
			}
		}

		/// <summary>
		/// 设备渲染事件
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="bmp"></param>
		/// <param name="viewMatrix"></param>
		public void Render(Scene scene, BitmapData bmp, Matrix4x4 viewMat, Matrix4x4 proMat)
		{
			this.m_BmData = bmp;
			if (scene != null)
				scene.Render(this, viewMat, proMat);
		}


		/// <summary>
		/// 齐次坐标系转换为屏幕坐标显示
        /// 屏幕空间左下角的像素坐标都是（0,0），因此右上角的
        /// 坐标为（Width，Height）Y方向，与我们的NDC是反过来
        /// 的，所以映射到（0,1）
        /// 区间后，还需要反向一下
		/// </summary>
		/// <param name="vector"></param>
		/// <returns></returns>
		public Vector4 ViewPort(Vector4 vector)
		{
			Vector4 val = new Vector4();
			val.X = (1.0f + vector.X) * Width * 0.5f;
			val.Y = (1.0f - vector.Y) * Height * 0.5f;
			val.Z = vector.Z;
			val.W = 1.0f;
			return val;
		}

		/// <summary>
		/// 判断三角形的法向量z值是否大于0, 如果大于0，则在背面
		/// </summary>
		/// <param name="tri"></param>
		/// <returns></returns>
		public bool IsInBack(Triangle tri)
		{
			Vector4 v1 = tri.Vertices[0].ScreenPosition;
			Vector4 v2 = tri.Vertices[1].ScreenPosition;
			Vector4 v3 = tri.Vertices[2].ScreenPosition;
			v1.Z = v2.Z = v3.Z = 0;
			Vector4 v1v2 = v2 - v1;
			Vector4 v1v3 = v3 - v1;
			return Vector4.Cross(v1v2, v1v3).Z > 0;
		}

		/// <summary>
		/// 获取某一点的颜色
		/// </summary>
		/// <param name="u"></param>
		/// <param name="v"></param>
		/// <param name="texture"></param>
		/// <returns></returns>
		public Color3 GetPixelColor(float u, float v, TextureMap texture)
		{
			int x = Math.Abs((int)((1f - u) * texture.GetWidth()) % texture.GetWidth());
			int y = Math.Abs((int)((1f - v) * texture.GetHeight()) % texture.GetHeight());

			byte red = 0;
			byte green = 0;
			byte blue = 0;
            unsafe
            {
                byte* ptr = (byte*)(texture.data.Scan0);
                byte* row = ptr + (y * texture.data.Stride);

                red = row[x * 3 + 2];
                green = row[x * 3 + 1];
                blue = row[x * 3];
            }

			return new Color3(red, green, blue);
		}

	}
}
