
namespace SoftRender.Render
{
	class Material
	{
		private float mAmbientStregth;
		private Color3 mDiffuse;

		/// <summary>
		/// 环境光系数
		/// </summary>
		public float AmbientStregth
		{
			get { return mAmbientStregth; }
			set { mAmbientStregth = value; }
		}

		/// <summary>
		/// 光颜色
		/// </summary>
		public Color3 Diffuse
		{
			get { return mDiffuse; }
			set { mDiffuse = value; }
		}

		/// <summary>
		/// 指定一个环境光系数和一个漫反射光来构造材质
		/// </summary>
		/// <param name="ambient"></param>
		/// <param name="color"></param>
		public Material(float ambient, Color3 color)
		{
			mAmbientStregth = ambient;
			mDiffuse = color;
		}
	}
}
