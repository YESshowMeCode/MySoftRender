
namespace SoftRender.Render
{
	class Material
	{
		private float m_AmbientStregth;
		private Color3 m_Diffuse;

		/// <summary>
		/// 环境光系数
		/// </summary>
		public float AmbientStregth
		{
			get { return m_AmbientStregth; }
			set { m_AmbientStregth = value; }
		}

		/// <summary>
		/// 光颜色
		/// </summary>
		public Color3 Diffuse
		{
			get { return m_Diffuse; }
			set { m_Diffuse = value; }
		}

		/// <summary>
		/// 指定一个环境光系数和一个漫反射光来构造材质
		/// </summary>
		/// <param name="ambient"></param>
		/// <param name="color"></param>
		public Material(float ambient, Color3 color)
		{
			m_AmbientStregth = ambient;
			m_Diffuse = color;
		}
	}
}
