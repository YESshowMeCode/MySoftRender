
namespace SoftRender.Render
{
	class Light
	{
		private Vector4 mPosition;
		private Color3 mColor;

		/// <summary>
		/// 位置
		/// </summary>
		public Vector4 Position
		{
			get { return mPosition; }
			set { mPosition = value; }
		}

		/// <summary>
		/// 颜色
		/// </summary>
		public Color3 Color
		{
			get { return mColor; }
			set { mColor = value;}
		}

		public Light(Vector4 pos, Color3 color)
		{
			mPosition = pos;
			mColor = color;
		}
	}
}
