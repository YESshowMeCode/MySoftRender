using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Color
    {

        public float _r;
        public float _g;
        public float _b;


        public Color()
        {

        }

        public Color(float r, float g, float b)
        {
            this._r = MathUntil.Range(r, 0, 1);
            this._g = MathUntil.Range(g, 0, 1);
            this._b = MathUntil.Range(b, 0, 1);
        }

        public Color(System.Drawing.Color c)
        {
            this._r = MathUntil.Range((float)c.R / 255, 0, 1);
            this._g = MathUntil.Range((float)c.G / 255, 0, 1);
            this._b = MathUntil.Range((float)c.B / 255, 0, 1);
        }

        public float r
        {
            get
            {
                return MathUntil.Range(_r, 0, 1);
            }
            set
            {
                _r = MathUntil.Range(value, 0, 1);
            }
        }

        public float g
        {
            get
            {
                return MathUntil.Range(_g, 0, 1);
            }
            set
            {
                _g = MathUntil.Range(value, 0, 1);
            }
        }

        public float b
        {
            get
            {
                return MathUntil.Range(_b, 0, 1);
            }
            set
            {
                _b = MathUntil.Range(value, 0, 1);
            }
        }


        public static Color Lerp(Color right, Color left, float f)
        {
            return new Color(right.r * f + left.r * (1 - f), right.g * f + left.g * (1 - f), right.b * f + left.b * (1 - f));
        }


        public static Color operator +(Color right, Color left)
        {
            return new Color(right.r + left.r, right.g + left.g, right.b + left.b);
        }

        public static Color operator -(Color right, Color left)
        {
            return new Color(right.r - left.r, right.g - left.g, right.b - left.b);
        }

        public static Color operator *(Color right, float f)
        {
            return new Color(right.r * f, right.g * f, right.b * f);
        }
    }
}
