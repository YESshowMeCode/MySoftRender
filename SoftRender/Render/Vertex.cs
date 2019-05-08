
namespace SoftRender.Render
{
	class Vertex
	{
		private Vector4 m_Position;
		private Vector4 m_Normal;
		private Vector2 m_UV;
		private Color3 m_Color;
		private Color3 m_LightColor;
		//进行扫描线算法的时候用到的 
		private Vector4 m_ScreenPosition;
		private Vector4 m_ClipPosition;

		/// <summary>
		/// 顶点位置
		/// </summary>
		public Vector4 Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}

		/// <summary>
		/// 法线
		/// </summary>
		public Vector4 Normal
		{
			get { return m_Normal; }
			set { m_Normal = value; }
		}

		/// <summary>
		/// 纹理坐标
		/// </summary>
		public Vector2 UV
		{
			get { return m_UV; }
			set { m_UV = value; }
		}

		/// <summary>
		/// 顶点色
		/// </summary>
		public Color3 Color
		{
			get { return m_Color; }
			set { m_Color = value; }
		}
	
		/// <summary>
		/// 光照颜色，用来在计算的时候做插值
		/// </summary>
		public Color3 LightColor
		{
			get { return m_LightColor; }
			set { m_LightColor = value; }
		}

		/// <summary>
		/// 屏幕上的位置
		/// </summary>
		public Vector4 ScreenPosition
		{
			get { return m_ScreenPosition; }
			set { m_ScreenPosition = value; }
		}

		/// <summary>
		/// 裁剪位置
		/// </summary>
		public Vector4 ClipPosition
		{
			get { return m_ClipPosition; }
			set { m_ClipPosition = value; }
		}

		public Vertex()
		{
			this.m_LightColor = new Color3(255, 255, 255);
		}

		public Vertex(Vector4 position, Vector4 normal, Vector2 uv, Color3 c)
		{
			this.m_Position = position;
			this.m_Normal = normal;
			this.m_UV = uv;
			this.m_Color = c;
			this.m_LightColor = new Color3(255, 255, 255);
		}

		/// <summary>
		/// 交换两个顶点
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		public static void SwapVertex(ref Vertex v1, ref Vertex v2)
		{
			var tempv = v2;
			v2 = v1;
			v1 = tempv;
		}

	}
}
