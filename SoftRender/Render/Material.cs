
namespace SoftRender.Render
{
	class Material
	{
		private float m_AmbientStregth;
		private Color3 m_Diffuse;
        private Color3 m_Emissive;
        private Color3 m_Specular;
        private float m_shininess;

        /// <summary>
        /// 环境光颜色
        /// </summary>
        public Color3 Emssive
        {
            get { return m_Emissive; }
            set { m_Emissive = value; }
        }

        /// <summary>
        /// 高光反射光颜色
        /// </summary>
        public Color3 Specular
        {
            get { return m_Specular; }
            set { m_Specular = value; }
        }

        /// <summary>
        /// 材质光泽度
        /// </summary>
        public float Shininess
        {
            get { return m_shininess; }
            set { m_shininess = value; }
        }

		/// <summary>
		/// 环境光系数
		/// </summary>
		public float AmbientStregth
		{
			get { return m_AmbientStregth; }
			set { m_AmbientStregth = value; }
		}

		/// <summary>
		/// 漫反射光颜色
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
		public Material(float ambient, Color3 diffuseColor,Color3 emissiveColor,Color3 specularColor,float shininess)
		{
			m_AmbientStregth = ambient;
            m_Diffuse = diffuseColor;
            m_shininess = shininess;
            m_Emissive = emissiveColor;
            m_Specular = specularColor;
		}
	}
}
