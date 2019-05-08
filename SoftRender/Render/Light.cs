
namespace SoftRender.Render
{
	class Light
	{
		private Vector4 m_Position;
		private Color3 m_Color;

		/// <summary>
		/// 位置
		/// </summary>
		public Vector4 Position
		{
			get { return m_Position; }
			set { m_Position = value; }
		}

		/// <summary>
		/// 颜色
		/// </summary>
		public Color3 Color
		{
			get { return m_Color; }
			set { m_Color = value;}
		}

		public Light(Vector4 pos, Color3 color)
		{
			m_Position = pos;
			m_Color = color;
		}
	}
}
