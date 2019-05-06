using System.Collections.Generic;

namespace SoftRender.Render
{
	class Scene
	{
		private Light mLight;
		private Camera mCamera;
		private List<Mesh> mMeshs;
		private bool mUseLight;

		/// <summary>
		/// 光照
		/// </summary>
		public Light Lights
		{
			get { return mLight; }
		}

		/// <summary>
		/// 场景中的模型
		/// </summary>
		public List<Mesh> Meshs
		{
			get { return mMeshs; }
		}

		/// <summary>
		/// 场景摄像机
		/// </summary>
		public Camera Camera
		{
			get { return mCamera; }
		}

		/// <summary>
		/// 光照开关
		/// </summary>
		public bool IsUseLight
		{
			get { return mUseLight; }
			set { mUseLight = value; }
		}

		public Scene()
		{
			mUseLight = false;
			InitCarmera();
		}

		/// <summary>
		/// 为当前场景初始化一个摄像机
		/// </summary>
		public void InitCarmera()
		{
			mCamera = new Camera();
			mCamera.Position = new Vector4(0,0,-5, 1);
			mCamera.Target = new Vector4(0, 0, 0, 1);
			mCamera.Up = new Vector4(0, 1, 0, 1);
		}

		/// <summary>
		/// 添加一个光
		/// </summary>
		public void AddLight(Light light)
		{
			mLight = light;
		}

		/// <summary>
		/// 删除光
		/// </summary>
		/// <param name="light"></param>
		public void DelLight()
		{
			mLight = null;
		}

		/// <summary>
		/// 增加一个渲染的模型
		/// </summary>
		/// <param name="msh"></param>
		public void AddMesh(Mesh msh)
		{
			if (mMeshs == null)
				mMeshs = new List<Mesh>();

			mMeshs.Add(msh);
		}

		/// <summary>
		/// 渲染事件
		/// </summary>
		public void Render(Device device, Matrix4x4 viewMat, Matrix4x4 proMat)
		{
			// 模型渲染
			foreach(Mesh msh in mMeshs)
			{
				msh.Render(this, device, viewMat, proMat);
			}
		}
	}
}
