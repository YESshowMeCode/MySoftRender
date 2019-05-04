﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Light
    {

        private Vector4 m_Position;
        private Color m_Color;

        public Vector4 Position
        {
            get { return m_Position; }
            set { m_Position = value; }
        }

        public Color Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        public Light(Vector4 pos,Color col)
        {
            m_Color = col;
            m_Position = pos;
        }
    }
}
