
namespace SoftRender.Render
{
	class Vertex
	{
		private Vector4 mPosition;
		private Vector4 mNormal;
		private Vector2 mUV;
		private Color3 mColor;
		private Color3 mLightColor;
		//进行扫描线算法的时候用到的 
		private Vector4 mScreenPosition;
		private Vector4 mClipPosition;

		/// <summary>
		/// 顶点位置
		/// </summary>
		public Vector4 Position
		{
			get { return mPosition; }
			set { mPosition = value; }
		}

		/// <summary>
		/// 法线
		/// </summary>
		public Vector4 Normal
		{
			get { return mNormal; }
			set { mNormal = value; }
		}

		/// <summary>
		/// 纹理坐标
		/// </summary>
		public Vector2 UV
		{
			get { return mUV; }
			set { mUV = value; }
		}

		/// <summary>
		/// 顶点色
		/// </summary>
		public Color3 Color
		{
			get { return mColor; }
			set { mColor = value; }
		}
	
		/// <summary>
		/// 光照颜色，用来在计算的时候做插值
		/// </summary>
		public Color3 LightColor
		{
			get { return mLightColor; }
			set { mLightColor = value; }
		}

		/// <summary>
		/// 屏幕上的位置
		/// </summary>
		public Vector4 ScreenPosition
		{
			get { return mScreenPosition; }
			set { mScreenPosition = value; }
		}

		/// <summary>
		/// 裁剪位置
		/// </summary>
		public Vector4 ClipPosition
		{
			get { return mClipPosition; }
			set { mClipPosition = value; }
		}

		public Vertex()
		{
			this.mLightColor = new Color3(255, 255, 255);
		}

		public Vertex(Vector4 position, Vector4 normal, Vector2 uv, Color3 c)
		{
			this.mPosition = position;
			this.mNormal = normal;
			this.mUV = uv;
			this.mColor = c;
			this.mLightColor = new Color3(255, 255, 255);
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
