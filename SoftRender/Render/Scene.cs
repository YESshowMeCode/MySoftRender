using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Scene
    {
        private Light m_Light;
        private Camera m_Camera;
        private List<Mesh> m_Mesh;
        private bool m_UseLight;

        public Light Light
        {
            get { return m_Light; }
        }

        public List<Mesh> Meshs
        {
            get { return m_Mesh; }
        }

        public Camera Camera
        {
            get { return m_Camera; }
        }

        public bool UseLight
        {
            get { return m_UseLight; }
            set { m_UseLight = value; }
        }

        public Scene()
        {
            m_UseLight = false;
            InitCamera();
        }

        public void InitCamera()
        {
            m_Camera = new Camera();
            m_Camera.Position = new Vector4(0, 0, -5, 1);
            m_Camera.Target = new Vector4(0, 0, 0, 1);
            m_Camera.Up = new Vector4(0, 1, 0, 1);
        }

        public void AddLight(Light light)
        {
            m_Light = light;
        }

        public void DeleLight()
        {
            m_Light = null;
        }

        public void AddMesh(Mesh mesh)
        {
            if (m_Mesh == null)
            {
                m_Mesh = new List<Mesh>();
            }
            m_Mesh.Add(mesh);
        }

        public void Render(Device device ,Matrix4x4 viewMat,Matrix4x4 projMat)
        {
            foreach(Mesh mesh in m_Mesh)
            {
                mesh.Render(this, device, viewMat, projMat);
            }
        }

    }
}
