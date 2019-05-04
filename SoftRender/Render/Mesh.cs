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
        private List<Vertex> m_VertexBuffer;
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

        public List<Vertex> VertexBuffer
        {
            get { return m_VertexBuffer; }
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
            m_Material = new Material(0.9f, new Color(200, 200, 200));
            m_Transform = new Matrix4x4();
            m_Transform.Identity();
        }

        public List<Triangle> GetDrawTriangleList(List<Vertex> vertexs)
        {
            List<Triangle> list = new List<Triangle>();
            for(int i = 0; i < m_VertexBuffer.Count - 2; i++)
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


        public Color GetLightColor(Vector4 position ,Vector4 normal,Light light)
        {
            Color ambient = light.Color * m_Material.AmbientStrength;
            Vector4 nor = normal * m_Transform;
            Vector4 lightDir = (light.Position - position).Normalize();
            float diff = Math.Max(Vector4.Dot(normal.Normalize(), lightDir), 0);
            Color diffuse = m_Material.Diffuse * diff;
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
                }
            }

        }

    }
}
