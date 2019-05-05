using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Mesh
    {
        private string m_Name;
        private Vertex[] m_VertexBuffer;
        private Face[] m_Face;
        private Material m_Material;
        private RenderTexture[] m_RenderTexture;
        private Matrix4x4 m_Transform;
        private Clip m_HodgmanClip;
        private ScanLine m_ScanLine;
        
        public string Name
        {
            get { return m_Name; }
        }

        public Vertex[] VertexBuffer
        {
            get { return m_VertexBuffer; }
            set { m_VertexBuffer = value; }
        }

        public Face[] Face
        {
            get { return m_Face; }
            set { m_Face = value; }
        }

        public Material Material
        {
            get { return m_Material; }
            set { m_Material = value; }
        }

        public RenderTexture[] RenderTexture
        {
            get { return m_RenderTexture; }
            set { m_RenderTexture = value; }
        }


        public Matrix4x4 Transform
        {
            get { return m_Transform; }
            set { m_Transform = value; }
        }

        public Mesh(string name)
        {
            m_Name = name;
            m_Material = new Material(0.9f, new Color3(200, 200, 200));
            m_Transform = new Matrix4x4();
            m_Transform.Identity();
        }

        public List<Triangle> GetDrawTriangleList(List<Vertex> vertexs)
        {
            List<Triangle> list = new List<Triangle>();
            for (int i = 0; i < vertexs.Count - 2; i++)
            {
                list.Add(new Triangle(vertexs[0], vertexs[i + 1], vertexs[i + 2]));
            }
            return list;
        }

        public RenderTexture GetTextureByFace(FaceType type)
        {
            if (m_RenderTexture.Length == 0)
                return null;

            if (type == FaceType.NONE)
            {
                return m_RenderTexture[0];
            }
            else
            {
                int index = (int)type;
                if (m_RenderTexture.Length == 6 && index >= 0 && index < 6)
                {
                    return m_RenderTexture[index];
                }
                else
                {
                    return m_RenderTexture[0];
                }
            }

        }


        public Color3 GetLightColor(Vector4 position ,Vector4 normal,Light light)
        {
            Color3 ambient = light.Color * m_Material.AmbientStrength;
            Vector4 nor = normal * m_Transform;
            Vector4 lightDir = (light.Position - position).Normalize();
            float diff = Math.Max(Vector4.Dot(normal.Normalize(), lightDir), 0);
            Color3 diffuse = m_Material.Diffuse * diff;
            return ambient + diffuse;
        }

        public void Render(Scene scene,Device device,Matrix4x4 viewMat,Matrix4x4 projMat)
        {
            Matrix4x4 viewMatrix = m_Transform * viewMat * projMat;

            foreach(var faces in m_Face)
            {
                Vertex verA = m_VertexBuffer[faces.A];
                Vertex verB = m_VertexBuffer[faces.B];
                Vertex verC = m_VertexBuffer[faces.C];

                Vertex verA2 = new Vertex();
                Vertex verB2 = new Vertex();
                Vertex verC2 = new Vertex();

                if (scene.UseLight && scene.Light != null)
                {
                    verA2.LightColor = GetLightColor(verA.Position, verA.Normal, scene.Light);
                    verB2.LightColor= GetLightColor(verA.Position, verB.Normal, scene.Light);
                    verC2.LightColor= GetLightColor(verA.Position, verC.Normal, scene.Light);
                }

                verA.ClipPosition = device.ToHomogeneous(verA.Position, viewMatrix);
                verB.ClipPosition = device.ToHomogeneous(verB.Position, viewMatrix);
                verC.ClipPosition = device.ToHomogeneous(verC.Position, viewMatrix);

                verA2.Color = verA.Color;
                verB2.Color = verB.Color;
                verC2.Color = verC.Color;


                verA.ScreenPosition = device.ViewPort(verA.ClipPosition);
                verB.ScreenPosition = device.ViewPort(verB.ClipPosition);
                verC.ScreenPosition = device.ViewPort(verC.ClipPosition);

                verA2.ClipPosition = verA.ClipPosition;
                verA2.ScreenPosition = verA.ScreenPosition;
                verA2.Position = m_Transform.LeftApply(verA.Position);
                verA2.UV = verA.UV;

                verB2.ClipPosition = verB.ClipPosition;
                verB2.ScreenPosition = verB.ScreenPosition;
                verB2.Position = m_Transform.LeftApply(verB.Position);
                verB2.UV = verB.UV;

                verC2.ClipPosition = verC.ClipPosition;
                verC2.ScreenPosition = verC.ScreenPosition;
                verC2.Position = m_Transform.LeftApply(verC.Position);
                verC2.UV = verC.UV;

                verA2.Normal = verA.Normal;
                verB2.Normal = verB.Normal;
                verC2.Normal = verC.Normal;

                List<Vertex> list = new List<Vertex>();
                list.Add(verA2);
                list.Add(verB2);
                list.Add(verC2);
                Triangle triang1 = new Triangle(verA2, verB2, verC2);
                List<Vertex> triangleVertex = new List<Vertex>();

                for (FaceType face = FaceType.LEFT; face <= FaceType.FAR; face++)
                {
                    if (list.Count == 0) break;
                    m_HodgmanClip = new Clip(device);
                    m_HodgmanClip.HodgmanPolygonClip(face, Device.sClipMin, Device.sClipMax, list.ToArray());
                    list = m_HodgmanClip.OutputList;
                }

                List<Triangle> tringleList = GetDrawTriangleList(list);
                if (device.RenderMode == RenderMode.WIREFRAME)
                {
                    for (int i = 0; i < tringleList.Count; i++)
                    {
                        if (!device.IsInBack(tringleList[i]))
                        {
                            device.DrawLine(device.ViewPort(tringleList[i].Vertexs[0].ClipPosition), device.ViewPort(tringleList[i].Vertexs[1].ClipPosition), scene, tringleList[i].Vertexs[0], tringleList[i].Vertexs[1]);
                            device.DrawLine(device.ViewPort(tringleList[i].Vertexs[1].ClipPosition), device.ViewPort(tringleList[i].Vertexs[2].ClipPosition), scene, tringleList[i].Vertexs[1], tringleList[i].Vertexs[2]);
                            device.DrawLine(device.ViewPort(tringleList[i].Vertexs[2].ClipPosition), device.ViewPort(tringleList[i].Vertexs[0].ClipPosition), scene, tringleList[i].Vertexs[2], tringleList[i].Vertexs[0]);
                        }
                    }
                }
                else
                {
                    if (m_ScanLine == null)
                        m_ScanLine = new ScanLine(device);
                    for (int i = 0; i < tringleList.Count; i++)
                    {
                        if (!device.IsInBack(tringleList[i]))
                            m_ScanLine.ProcessScanLine(tringleList[i], scene, triang1, faces.FaceType, this);
                    }
                }
            }

        }

    }
}
