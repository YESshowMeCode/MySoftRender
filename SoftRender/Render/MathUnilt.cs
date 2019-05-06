
namespace SoftRender.Render
{
    class MathUntily
    {
        /// <summary>
        /// 数值
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static float Lerp(float x1, float x2, float k)
        {
            return x1 + (x2 - x1) * k;
        }

        /// <summary>
        /// 坐标线性插值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Vector4 Lerp(Vector4 v1, Vector4 v2, float k)
        {
            return new Vector4(
                v1.X + (v2.X - v1.X) * k,
                v1.Y + (v2.Y - v1.Y) * k,
                v1.Z + (v2.Z - v1.Z) * k,
                v1.W + (v2.W - v1.W) * k);
        }

        /// <summary>
        /// 计算两个Vector2之间的差值
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Vector2 Lerp(Vector2 v1, Vector2 v2, float k)
        {
            return new Vector2(
                v1.X + (v2.X - v1.X) * k,
                v1.Y + (v2.Y - v1.Y) * k);
        }

        /// <summary>
        /// 颜色线性插值
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public static Color3 Lerp(Color3 c1, Color3 c2, float k)
        {
            byte r = (byte)(c1.R + (c2.R - c1.R) * k);
            byte g = (byte)(c1.G + (c2.G - c1.G) * k);
            byte b = (byte)(c1.B + (c2.B - c1.B) * k);
            return new Color3(r, g, b);
        }
    }
}
