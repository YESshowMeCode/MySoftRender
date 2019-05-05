using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Material
    {

        private float m_AmbientStrength;
        private Color3 m_Diffuse;

        public float AmbientStrength
        {
            get { return m_AmbientStrength; }
            set { m_AmbientStrength = value; }
        }

        public Color3 Diffuse
        {
            get { return m_Diffuse; }
            set { m_Diffuse = value; }
        }   

        public Material(float ambient,Color3 color)
        {
            m_AmbientStrength = ambient;
            m_Diffuse = color;
        }

    }
}
