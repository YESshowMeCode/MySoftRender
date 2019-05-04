using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class ScanLine
    {

        private Color m_UserColor;
        private Device m_Device;

        public ScanLine(Device device)
        {
            m_Device = device;
            m_UserColor = new Color(255, 255, 255);
        }

    }
}
