using System;
using System.Collections.Generic;

namespace SoftRender.Render
{
	class Mesh
	{
		private string m_Name;
		private Vertex[] m_Vertices;
		private Face[] m_Faces;

		private Material m_Material;
		private Matrix4x4 m_Transform;
		private RenderTexture[] m_TextureMaps;
		private Clip m_Hodgmanclip;
		private ScanLine m_Scanline;

		/// <summary>
		/// 模型名称
		/// </summary>
		public string Name
		{
			get { return m_Name; }
		}

		/// <summary>
		/// 顶点集合
		/// </summary>
		public Vertex[] Vertices
		{
			get { return m_Vertices; }
			set { m_Vertices = value; }
		}

		/// <summary>
		/// 模型面
		/// </summary>
		public Face[] Faces
		{
			get { return m_Faces; }
			set { m_Faces = value; }
		}

		/// <summary>
		/// 材质
		/// </summary>
		public Material Material
		{
			get { return m_Material; }
			set { m_Material = value; }
		}

		/// <summary>
		/// 模型的矩阵
		/// </summary>
		public Matrix4x4 Transform
		{
			get { return m_Transform; }
			set { m_Transform = value; }
		}

		/// <summary>
		/// 贴图
		/// </summary>
		public RenderTexture[] TextureMaps
		{
			get { return m_TextureMaps; }
			set { m_TextureMaps = value; }
		}
		/// <summary>
		/// 顶点数量和名字构造
		/// </summary>
		/// <param name="name"></param>
		/// <param name="verticesCount"></param>
		public Mesh(string name)
		{
			m_Name = name;
            m_Material = new Material(0.9f, new Color3(255, 255, 255), new Color3(0, 0, 128), new Color3(0, 0, 0), 0.5f);
			m_Transform = new Matrix4x4(1);
		}

		/// <summary>
		/// 获取要画的三角形列表
		/// </summary>
		/// <param name="vertex"></param>
		/// <returns></returns>
		public List<Triangle> GetDrawTriangleList(List<Vertex> vertex)
		{
			List<Triangle> t = new List<Triangle>();
			for (int i = 0; i < vertex.Count - 2; i++)
			{
				t.Add(new Triangle(vertex[0], vertex[i + 1], vertex[i + 2]));
			}
			return t;
		}

		/// <summary>
		/// 根据模型的面的方向返回贴图
		/// </summary>
		/// <param name="types"></param>
		/// <returns></returns>
		public RenderTexture GetTextureByFace(FaceTypes types)
		{
			if (m_TextureMaps.Length == 0)
				return null;

			if (types == FaceTypes.NONE)
			{
				return m_TextureMaps[0];
			}
			else
			{

				int index = (int)types;
				if (m_TextureMaps.Length == 6 && index >= 0 && index < 6)
					return m_TextureMaps[index];
				else
					return m_TextureMaps[0];
			}
		}
	
		/// <summary>
        /// 实现了“基础光照模型”，在世界空间进行顶点光照处理
		/// </summary>
		/// <param name="position"></param>
		/// <param name="normal"></param>
		/// <param name="light"></param>
		/// <returns></returns>
		public Color3 GetLightColor(Vector4 position, Vector4 normal, Light light ,Vector4 cameraPosition)
		{
			// 环境光
			Color3 ambient = light.Color * m_Material.AmbientStregth;
            //自发光
            Color3 emissive = m_Material.Emssive;
			// 漫反射
			Vector4 nor = normal * m_Transform;
			Vector4 lightdir = (light.Position - position).Normalize();
			float diff = Math.Max(Vector4.Dot(normal.Normalize(), lightdir), 0);
			Color3 diffuse = m_Material.Diffuse * diff;

            Vector4 viewDir = (cameraPosition - position).Normalize();
            Vector4 h = (viewDir + lightdir).Normalize();
            float specular = (float)System.Math.Pow(Clamp(Vector4.Dot(h, normal)), m_Material.Shininess);

            Color3 specularColor = m_Material.Specular * specular * light.Color;//镜面高光

            return ambient + diffuse + specularColor + emissive;
		}

		/// <summary>
		/// 网格渲染
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="device"></param>
		/// <param name="viewMat"></param>
		/// <param name="proMat"></param>
		public void Render(Scene scene, Device device, Matrix4x4 viewMat, Matrix4x4 proMat)
		{
            //MVP矩阵，因为输入的点在世界坐标系不需要世界矩阵
			Matrix4x4 MVP = m_Transform * viewMat * proMat;
			foreach (var faces in m_Faces)
			{
				Vertex verA = m_Vertices[faces.A];
				Vertex verB = m_Vertices[faces.B];
				Vertex verC = m_Vertices[faces.C];

				Vertex verA2 = new Vertex();
				Vertex verB2 = new Vertex();
				Vertex verC2 = new Vertex();

				if (scene.IsUseLight && scene.Lights != null)
				{
                    verA2.LightColor = GetLightColor(verA.Position, verA.Normal, scene.Lights, scene.Camera.Position);
                    verB2.LightColor = GetLightColor(verA.Position, verB.Normal, scene.Lights, scene.Camera.Position);
                    verC2.LightColor = GetLightColor(verA.Position, verC.Normal, scene.Lights, scene.Camera.Position);
				}

				// 转换到齐次坐标
				verA.ClipPosition = device.ToHomogeneousDNC(verA.Position, MVP);
				verB.ClipPosition = device.ToHomogeneousDNC(verB.Position, MVP);
				verC.ClipPosition = device.ToHomogeneousDNC(verC.Position, MVP);

				verA2.Color = verA.Color;
				verB2.Color = verB.Color;
				verC2.Color = verC.Color;

				//对应屏幕坐标 左上角
				verA.ScreenPosition = device.ViewPort(verA.ClipPosition);
				verB.ScreenPosition = device.ViewPort(verB.ClipPosition);
				verC.ScreenPosition = device.ViewPort(verC.ClipPosition);

				verA2.ClipPosition = verA.ClipPosition;
				verA2.ScreenPosition = verA.ScreenPosition;
                verA2.Position = m_Transform * verA.Position;
				verA2.UV = verA.UV;

				verB2.ClipPosition = verB.ClipPosition;
				verB2.ScreenPosition = verB.ScreenPosition;
                verB2.Position = m_Transform * verB.Position;
				verB2.UV = verB.UV;

				verC2.ClipPosition = verC.ClipPosition;
				verC2.ScreenPosition = verC.ScreenPosition;
                verC2.Position = m_Transform * verC.Position;
				verC2.UV = verC.UV;

				verA2.Normal = verA.Normal;
				verB2.Normal = verB.Normal;
				verC2.Normal = verC.Normal;

				List<Vertex> list = new List<Vertex>();
				list.Add(verA2);
				list.Add(verB2);
				list.Add(verC2);
				Triangle triang1 = new Triangle(verA2, verB2, verC2);
				//进行裁剪
				List<Vertex> triangleVertex = new List<Vertex>();
				//放在构造函数中初始化引起list 集合累加

				for (FaceTypes face = FaceTypes.LEFT; face <= FaceTypes.FAR; face++)
				{
					if (list.Count == 0) break;
					m_Hodgmanclip = new Clip(device);
					m_Hodgmanclip.HodgmanPolygonClip(face, Device.sClipmin, Device.sClipmax, list.ToArray());
					list = m_Hodgmanclip.OutputList;
				}

				List<Triangle> tringleList = GetDrawTriangleList(list);
				if (device.RenderMode == RenderMode.WIREFRAME)
				{
					for (int i = 0; i < tringleList.Count; i++)
					{
						if (!device.IsInBack(tringleList[i]))
						{
							device.DrawLine(device.ViewPort(tringleList[i].Vertices[0].ClipPosition), device.ViewPort(tringleList[i].Vertices[1].ClipPosition), scene, tringleList[i].Vertices[0], tringleList[i].Vertices[1]);
							device.DrawLine(device.ViewPort(tringleList[i].Vertices[1].ClipPosition), device.ViewPort(tringleList[i].Vertices[2].ClipPosition), scene, tringleList[i].Vertices[1], tringleList[i].Vertices[2]);
							device.DrawLine(device.ViewPort(tringleList[i].Vertices[2].ClipPosition), device.ViewPort(tringleList[i].Vertices[0].ClipPosition), scene, tringleList[i].Vertices[2], tringleList[i].Vertices[0]);
						}
					}
				}
				else
				{
					if (m_Scanline == null)
						m_Scanline = new ScanLine(device);
					for (int i = 0; i < tringleList.Count; i++)
					{
						if (!device.IsInBack(tringleList[i]))
							m_Scanline.ProcessScanLine(tringleList[i], scene, triang1, faces.FaceType, this);
					}
				}
			}
		}


        /// <summary>
        /// [0, 1]的范围值
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public float Clamp(float g)
        {
            if (g.CompareTo(0) < 0)
                return 0;
            else if (g.CompareTo(1) > 0)
                return 1;
            return g;
        }
	}
}
