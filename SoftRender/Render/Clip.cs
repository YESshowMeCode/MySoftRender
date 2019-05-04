using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Clip
    {
        private Device m_Device;
        private List<Vertex> m_OutputList;

        public Device Device
        {
            get { return m_Device; }
        }

        public List<Vertex> OutputList
        {
            get { return m_OutputList; }
        }

        public Clip(Device device)
        {
            m_Device = device;
            m_OutputList = new List<Vertex>();
        }




    }
}
