using System;
using System.Collections.Generic;

namespace SoftRender.Render
{
	class Mesh
	{
		private string mName;
		private Vertex[] mVertices;
		private Face[] mFaces;

		private Material mMaterial;
		private Matrix4x4 mTransform;
		private RenderTexture[] mTextureMaps;
		private Clip mHodgmanclip;
		private ScanLine mScanline;

		/// <summary>
		/// 模型名称
		/// </summary>
		public string Name
		{
			get { return mName; }
		}

		/// <summary>
		/// 顶点集合
		/// </summary>
		public Vertex[] Vertices
		{
			get { return mVertices; }
			set { mVertices = value; }
		}

		/// <summary>
		/// 模型面
		/// </summary>
		public Face[] Faces
		{
			get { return mFaces; }
			set { mFaces = value; }
		}

		/// <summary>
		/// 材质
		/// </summary>
		public Material Material
		{
			get { return mMaterial; }
			set { mMaterial = value; }
		}

		/// <summary>
		/// 模型的矩阵
		/// </summary>
		public Matrix4x4 Transform
		{
			get { return mTransform; }
			set { mTransform = value; }
		}

		/// <summary>
		/// 贴图
		/// </summary>
		public RenderTexture[] TextureMaps
		{
			get { return mTextureMaps; }
			set { mTextureMaps = value; }
		}
		/// <summary>
		/// 顶点数量和名字构造
		/// </summary>
		/// <param name="name"></param>
		/// <param name="verticesCount"></param>
		public Mesh(string name)
		{
			mName = name;
			mMaterial = new Material(0.9f, new Color3(200, 200, 200));
			mTransform = new Matrix4x4(1);
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
			if (mTextureMaps.Length == 0)
				return null;

			if (types == FaceTypes.NONE)
			{
				return mTextureMaps[0];
			}
			else
			{
				int index = (int)types;
				if (mTextureMaps.Length == 6 && index >= 0 && index < 6)
					return mTextureMaps[index];
				else
					return mTextureMaps[0];
			}
		}
	
		/// <summary>
		/// 计算顶点所受的光照的颜色
		/// </summary>
		/// <param name="position"></param>
		/// <param name="normal"></param>
		/// <param name="light"></param>
		/// <returns></returns>
		public Color3 GetLightColor(Vector4 position, Vector4 normal, Light light)
		{
			// 环境光
			Color3 ambient = light.Color * mMaterial.AmbientStregth;
			// 漫反射
			Vector4 nor = normal * mTransform;
			Vector4 lightdir = (light.Position - position).Normalize();
			float diff = Math.Max(Vector4.Dot(normal.Normalize(), lightdir), 0);
			Color3 diffuse = mMaterial.Diffuse * diff;
			return ambient + diffuse;
		}

		/// <summary>
		/// 设备渲染事件
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="bmp"></param>
		/// <param name="viewMatrix"></param>
		public void Render(Scene scene, Device device, Matrix4x4 viewMat, Matrix4x4 proMat)
		{
			Matrix4x4 viewMatrix = mTransform * viewMat * proMat;
			foreach (var faces in mFaces)
			{
				Vertex verA = mVertices[faces.A];
				Vertex verB = mVertices[faces.B];
				Vertex verC = mVertices[faces.C];

				Vertex verA2 = new Vertex();
				Vertex verB2 = new Vertex();
				Vertex verC2 = new Vertex();

				if (scene.IsUseLight && scene.Lights != null)
				{
					verA2.LightColor = GetLightColor(verA.Position, verA.Normal, scene.Lights);
					verB2.LightColor = GetLightColor(verA.Position, verB.Normal, scene.Lights);
					verC2.LightColor = GetLightColor(verA.Position, verC.Normal, scene.Lights);
				}

				// 转换到齐次坐标
				verA.ClipPosition = device.ToHomogeneous(verA.Position, viewMatrix);
				verB.ClipPosition = device.ToHomogeneous(verB.Position, viewMatrix);
				verC.ClipPosition = device.ToHomogeneous(verC.Position, viewMatrix);

				verA2.Color = verA.Color;
				verB2.Color = verB.Color;
				verC2.Color = verC.Color;

				//对应屏幕坐标 左上角
				verA.ScreenPosition = device.ViewPort(verA.ClipPosition);
				verB.ScreenPosition = device.ViewPort(verB.ClipPosition);
				verC.ScreenPosition = device.ViewPort(verC.ClipPosition);

				verA2.ClipPosition = verA.ClipPosition;
				verA2.ScreenPosition = verA.ScreenPosition;
				verA2.Position = mTransform.LeftApply(verA.Position);
				verA2.UV = verA.UV;

				verB2.ClipPosition = verB.ClipPosition;
				verB2.ScreenPosition = verB.ScreenPosition;
				verB2.Position = mTransform.LeftApply(verB.Position);
				verB2.UV = verB.UV;

				verC2.ClipPosition = verC.ClipPosition;
				verC2.ScreenPosition = verC.ScreenPosition;
				verC2.Position = mTransform.LeftApply(verC.Position);
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
					mHodgmanclip = new Clip(device);
					mHodgmanclip.HodgmanPolygonClip(face, Device.sClipmin, Device.sClipmax, list.ToArray());
					list = mHodgmanclip.OutputList;
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
					if (mScanline == null)
						mScanline = new ScanLine(device);
					for (int i = 0; i < tringleList.Count; i++)
					{
						if (!device.IsInBack(tringleList[i]))
							mScanline.ProcessScanLine(tringleList[i], scene, triang1, faces.FaceType, this);
					}
				}
			}
		}

	}
}
