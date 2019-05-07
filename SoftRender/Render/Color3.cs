using System;

namespace SoftRender.Render
{
    // rgb 0-255
    public class Color3
    {
        public byte R;
        public byte G;
        public byte B;

        /// <summary>
        /// 用rgb构造一个颜色
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public Color3(byte red, byte green, byte blue)
        {
            this.R = red;
            this.G = green;
            this.B = blue;
        }

        public Color3()
        {
            R = 255;
            G = 255;
            B = 255;
        }

        /// <summary>
        /// 颜色之间相乘
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Color3 operator *(Color3 c1, Color3 c2)
        {
            float r = (c1.R / 255f) * (c2.R / 255f);
            float g = (c1.G / 255f) * (c2.G / 255f);
            float b = (c1.B / 255f) * (c2.B / 255f);
            return new Color3((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        /// <summary>
        /// 颜色乘数值
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Color3 operator *(Color3 c1, float t)
        {
            byte r = (byte)Math.Min((c1.R * t), 255);
            byte g = (byte)Math.Min((c1.G * t), 255);
            byte b = (byte)Math.Min((c1.B * t), 255);
            return new Color3(r, g, b);
        }

        /// <summary>
        /// 颜色相加
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Color3 operator +(Color3 c1, Color3 c2)
        {
            byte r = (byte)Math.Min(c1.R + c2.R, 255);
            byte g = (byte)Math.Min(c1.G + c2.G, 255);
            byte b = (byte)Math.Min(c1.B + c2.B, 255);
            return new Color3(r, g, b);
        }
    }
}
