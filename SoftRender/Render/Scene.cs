using System.Collections.Generic;

namespace SoftRender.Render
{
	class Scene
	{
		private Light m_Light;
		private Camera m_Camera;
		private List<Mesh> m_Meshs;
		private bool m_UseLight;

		/// <summary>
		/// 光照
		/// </summary>
		public Light Lights
		{
			get { return m_Light; }
		}

		/// <summary>
		/// 场景中的模型
		/// </summary>
		public List<Mesh> Meshs
		{
			get { return m_Meshs; }
		}

		/// <summary>
		/// 场景摄像机
		/// </summary>
		public Camera Camera
		{
			get { return m_Camera; }
		}

		/// <summary>
		/// 光照开关
		/// </summary>
		public bool IsUseLight
		{
			get { return m_UseLight; }
			set { m_UseLight = value; }
		}

		public Scene()
		{
			m_UseLight = false;
			InitCarmera();
		}

		/// <summary>
		/// 为当前场景初始化一个摄像机
		/// </summary>
		public void InitCarmera()
		{
			m_Camera = new Camera();
			m_Camera.Position = new Vector4(0,0,-5, 1);
			m_Camera.Target = new Vector4(0, 0, 0, 1);
			m_Camera.Up = new Vector4(0, 1, 0, 1);
		}

		/// <summary>
		/// 添加一个光
		/// </summary>
		public void AddLight(Light light)
		{
			m_Light = light;
		}

		/// <summary>
		/// 删除光
		/// </summary>
		/// <param name="light"></param>
		public void DelLight()
		{
			m_Light = null;
		}

		/// <summary>
		/// 增加一个渲染的模型
		/// </summary>
		/// <param name="msh"></param>
		public void AddMesh(Mesh msh)
		{
			if (m_Meshs == null)
				m_Meshs = new List<Mesh>();

			m_Meshs.Add(msh);
		}

		/// <summary>
		/// 渲染事件
		/// </summary>
		public void Render(Device device, Matrix4x4 viewMat, Matrix4x4 proMat)
		{
			// 模型渲染
			foreach(Mesh msh in m_Meshs)
			{
				msh.Render(this, device, viewMat, proMat);
			}
		}
	}
}
