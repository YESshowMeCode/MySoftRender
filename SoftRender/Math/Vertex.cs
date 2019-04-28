﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftRender.RenderData;

namespace SoftRender.Math
{
    class Vertex
    {
        public float x;
        public float y;
        public float z;


        public Vector point;

        public float u, v;

        public Color vexColor;

        public float onePerZ;


        public Vertex(Vector point,  float u, float v, Color col)
        {
            this.point = point;
            this.point.w = 1;
            vexColor = col;
            onePerZ = 1;
            this.u = u;
            this.v = v;
        }

        public Vertex(Vertex vex)
        {
            point = vex.point;
            this.vexColor = vex.vexColor;
            onePerZ = 1;
            this.u = vex.u;
            this.v = vex.v;
        }

    }
}
