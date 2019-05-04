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
        private Clip m_HodgmanClip;
        private ScanLine m_ScanLine;
        


    }
}
