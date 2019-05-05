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

        public void ProcessScanLine(Triangle triangle, Scene scene, Triangle ort, FaceType types, Mesh msh)
        {
            Vector4 P1 = triangle.Vertexs[0].ScreenPosition;
            Vector4 P2 = triangle.Vertexs[1].ScreenPosition;
            Vector4 P3 = triangle.Vertexs[2].ScreenPosition;

            Vertex V1 = triangle.Vertexs[0];
            Vertex V2 = triangle.Vertexs[1];
            Vertex V3 = triangle.Vertexs[2];

            // 根据y从小到大排序
            if (P1.y > P2.y)
            {
                Vector4.SwapVector4(ref P1, ref P2);
                Vertex.SwapVertex(ref V1, ref V2);
            }

            if (P2.y > P3.y)
            {
                Vector4.SwapVector4(ref P2, ref P3);
                Vertex.SwapVertex(ref V2, ref V3);
            }

            if (P1.y > P2.y)
            {
                Vector4.SwapVector4(ref P1, ref P2);
                Vertex.SwapVertex(ref V1, ref V2);
            }

            //计算斜率
            float dp1dp2 = 0, dp1dp3 = 0;
            if (P2.y - P1.y > 0)
                dp1dp2 = (P2.x - P1.x) / (P2.y - P1.y);

            if (P3.y - P1.y > 0)
                dp1dp3 = (P3.x - P1.x) / (P3.y - P1.y);

            if (dp1dp2 == 0)
            {
                if (P1.x > P2.x)
                {
                    Vector4.SwapVector4(ref P1, ref P2);
                    Vertex.SwapVertex(ref V1, ref V2);
                }
                for (var y = (int)P1.y; y < (int)P3.y; y++)
                    ScanLineX(triangle, (int)y, V1, V3, V2, V3, scene, ort, types, msh);
            }

            Vector4 temp1 = P1;
            Vector4 temp2 = P2;
            Vector4 temp3 = P3;

            if (dp1dp2 > dp1dp3)
            {
                for (int y = (int)P1.y; y <= (int)P3.y; y++)
                {
                    if (y < P2.y)
                        ScanLineX(triangle, y, V1, V3, V1, V2, scene, ort, types, msh);
                    else
                        ScanLineX(triangle, y, V1, V3, V2, V3, scene, ort, types, msh);
                }
            }
            else
            {
                for (int y = (int)P1.y; y <= (int)P3.y; y++)
                {
                    if (y < P2.y)
                        ScanLineX(triangle, y, V1, V2, V1, V3, scene, ort, types, msh);
                    else
                        ScanLineX(triangle, y, V2, V3, V1, V3, scene, ort, types, msh);
                }
            }
        }

        public void ScanLineX(Triangle triangle, int y, Vertex v1, Vertex v2, Vertex v3, Vertex v4, Scene scene, Triangle ort, FaceType types, Mesh msh)
        {
            Vector4 screen11 = v1.ScreenPosition;
            Vector4 screen12 = v2.ScreenPosition;
            Vector4 screen21 = v3.ScreenPosition;
            Vector4 screen22 = v4.ScreenPosition;

            var r1 = screen11.y != screen12.y ? (y - screen11.y) / (screen12.y - screen11.y) : 0.5f;
            var r2 = screen21.y != screen22.y ? (y - screen21.y) / (screen22.y - screen21.y) : 0.5f;
            r1 = Clamp(r1);
            r2 = Clamp(r2);

            int dx1 = (int)MathUntil.Lerp(screen11.x, screen12.x, r1);
            int dx2 = (int)MathUntil.Lerp(screen21.x, screen22.x, r2);

            float z1 = MathUntil.Lerp(screen11.z, screen12.z, r1);
            float z2 = MathUntil.Lerp(screen21.z, screen22.z, r2);

            Color c1 = MathUntil.Lerp(v1.Color, v2.Color, r1);
            Color c2 = MathUntil.Lerp(v3.Color, v4.Color, r2);
            Color c3 = new Color();

            Color lc1 = MathUntil.Lerp(v1.LightColor, v2.LightColor, r1);
            Color lc2 = MathUntil.Lerp(v3.LightColor, v4.LightColor, r2);

            Vector4 pos1 = MathUntil.Lerp(v1.Position, v2.Position, r1);
            Vector4 pos2 = MathUntil.Lerp(v3.Position, v4.Position, r2);
            Vector4 pos3 = new Vector4();

            Vector4 nor1 = MathUntil.Lerp(v1.Normal, v2.Normal, r1);
            Vector4 nor2 = MathUntil.Lerp(v3.Normal, v4.Normal, r2);
            Vector4 nor3 = new Vector4();

            // 计算线性方程的系数
            ort.PreCallLerp();

            Vector4 tmppos = new Vector4();
            for (int x = dx1; x < dx2; x++)
            {
                float r3 = Clamp((float)(x - dx1) / (dx2 - dx1));
                pos3 = MathUntil.Lerp(pos1, pos2, r3);
                float z = MathUntil.Lerp(z1, z2, r3);

                tmppos.x = x;
                tmppos.y = y;
                tmppos.z = z;
                tmppos.w = 0;

                ort.CallLerp(tmppos);
                nor3 = ort.GetNormal();

                Light light = scene.Light;
                if (scene.UseLight == false || light == null)
                    c3 = MathUntil.Lerp(c1, c2, r3);
                else
                    c3 = MathUntil.Lerp(c1, c2, r3) * MathUntil.Lerp(lc1, lc2, r3);

                m_UserColor = c3;
                if (m_Device.RenderMode == RenderMode.TEXTURED || m_Device.RenderMode == RenderMode.CUBETEXTURED)
                {
                    ort.CallLerp(new Vector4(x, y, 0, 0));
                    Vector2 uv = ort.GetUV();
                    FaceType typ = types;
                    if (m_Device.RenderMode == RenderMode.TEXTURED)
                        typ = FaceType.NONE;

                    RenderTexture texture = msh.GetTextureByFace(typ);
                    if (texture != null)
                    {
                        if (scene.UseLight == false || light == null)
                            m_UserColor = texture.GetPixelColor(uv.x, uv.y);
                        else
                            m_UserColor = texture.GetPixelColor(uv.x, uv.y) * MathUntil.Lerp(lc1, lc2, r3);
                    }
                }

                this.m_Device.DrawPoint(tmppos, m_UserColor);
            }
        }

        public float Clamp(float g)
        {
            if (g.CompareTo(0) < 0)
                return 0;
            else if (g.CompareTo(1) > 0)
                return 1;
            return g;
        }


    }
}
