using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Mesh
    {

        public List<Vertex> vertexBuffer = new List<Vertex>();
        public List<int> indexBuffer;

        public Mesh()
        {

        }

        public void AddVertexData(float x, float y, float z, float u, float v, Color col)
        {
            Vertex vex = new Vertex(new Vector(x, y, z), u, v, col);
            vertexBuffer.Add(vex);
        }

        public void AddVertexData(Vector vec, float u, float v, Color col)
        {
            AddVertexData(vec.x, vec.y, vec.z, u, v, col);
        }

        public void DrawElement()
        {

        }

    }
}
