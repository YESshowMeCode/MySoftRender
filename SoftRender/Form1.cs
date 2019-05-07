using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SoftRender.Render;
using System.Drawing.Imaging;

namespace SoftRender
{
    public partial class SoftRender : Form
    {

        private Device m_Device;
        private Bitmap m_Bmp;
        private Rectangle m_Rect;
        private Graphics m_Graphics;
        private Scene m_Scene;
        private PixelFormat m_PixelFormat;
        private BitmapData m_Data;

        private Matrix4x4 m_ViewMat;
        private Matrix4x4 m_ProjectionMat;
        private Mesh m_Cube;
        private bool m_IsMouseLeftDown = false;
        private Vector2 m_MouseLeftPos = new Vector2();


        public SoftRender()
        {
            InitializeComponent();
            InitSettings();
            Init();
            CenterToScreen();
        }

        private void InitSettings()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void Init()
        {
            m_Bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format24bppRgb);
            m_Device = new Device(m_Bmp);
            m_Scene = new Scene();
            m_ViewMat = m_Scene.Camera.GetLookAt();
            m_ProjectionMat = m_Scene.Camera.GetProject((float)System.Math.PI * 0.3f, (float)ClientSize.Width / (float)ClientSize.Height, 1f, 100.0f);
            m_Rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            m_PixelFormat = m_Bmp.PixelFormat;
            AddCubeToScene(m_Scene);
        }
        private void AddCubeToScene(Scene scene)
        {
            if (scene == null)
                return;

            m_Cube = new Mesh("Cube");
            m_Cube.Vertices = new Vertex[24] {
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, -1, -1, 1), new Vector2(0, 0), new Color3(0, 0, 0)),
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, -1, -1, 1), new Vector2(1, 0), new Color3(0, 0, 0)),
                new Vertex(new Vector4(-1, -1, -1, 1), new Vector4(-1, -1, -1, 1), new Vector2(0, 0), new Color3(0, 0, 0)),

                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, -1, -1, 1), new Vector2(1, 0), new Color3(255, 0, 0)),
                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, -1, -1, 1),  new Vector2(0, 0), new Color3(255, 0, 0)),
                new Vertex(new Vector4(1, -1, -1, 1), new Vector4(1, -1, -1, 1), new Vector2(1, 0), new Color3(255, 0, 0)),

                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 1, -1, 1), new Vector2(1, 0), new Color3(255, 255, 0)),
                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 1, -1, 1), new Vector2(0, 1), new Color3(255, 255, 0)),
                new Vertex(new Vector4(1, 1, -1, 1), new Vector4(1, 1, -1, 1), new Vector2(1, 1), new Color3(255, 255, 0)),

                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 1, -1, 1), new Vector2(0, 0), new Color3(0, 255, 0)),
                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 1, -1, 1), new Vector2(1, 1), new Color3(0, 255, 0)),
                new Vertex(new Vector4(-1, 1, -1, 1), new Vector4(-1, 1, -1, 1), new Vector2(0, 1), new Color3(0, 255, 0)),

                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, -1, 1, 1), new Vector2(0, 1), new Color3(0, 0, 255)),
                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, -1, 1, 1), new Vector2(0, 0), new Color3(0, 0, 255)),
                new Vertex(new Vector4(-1, -1, 1, 1), new Vector4(-1, -1, 1, 1), new Vector2(0, 0), new Color3(0, 0, 255)),

                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, -1, 1, 1), new Vector2(1, 1), new Color3(255, 0, 255)),
                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, -1, 1, 1), new Vector2(1, 0), new Color3(255, 0, 255)),
                new Vertex(new Vector4(1, -1, 1, 1), new Vector4(1, -1, 1, 1), new Vector2(1, 0), new Color3(255, 0, 255)),

                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1), new Vector2(1, 1), new Color3(255, 255, 255)),
                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1), new Vector2(1, 1), new Color3(255, 255, 255)),
                new Vertex(new Vector4(1, 1, 1, 1), new Vector4(1, 1, 1, 1), new Vector2(1, 1), new Color3(255, 255, 255)),

                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 1, 1, 1), new Vector2(0, 1), new Color3(0, 255, 255)),
                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 1, 1, 1), new Vector2(0, 1), new Color3(0, 255, 255)),
                new Vertex(new Vector4(-1, 1, 1, 1), new Vector4(-1, 1, 1, 1), new Vector2(0, 1), new Color3(0, 255, 255)),

			};

            m_Cube.Faces = new Face[] {
				// 正面
				new Face(2, 5, 8, FaceTypes.NEAR),
				new Face(2, 8, 11, FaceTypes.NEAR),
				// 右面
				new Face(4, 16, 7, FaceTypes.RIGHT),
				new Face(16, 19, 7, FaceTypes.RIGHT),
				// 左面
				new Face(13, 1, 10, FaceTypes.LEFT),
				new Face(13, 10, 22, FaceTypes.LEFT),
				// 背面
				new Face(17, 14, 23, FaceTypes.FAR),
				new Face(17, 23, 20, FaceTypes.FAR),
				// 上面
				new Face(9, 6, 18, FaceTypes.TOP),
				new Face(9, 18, 21, FaceTypes.TOP),
				// 下面
				new Face(12, 15, 3, FaceTypes.BUTTOM),
				new Face(12, 3, 0, FaceTypes.BUTTOM)
			};
            RenderTexture[] textures = new RenderTexture[6];
            for (int i = 0; i < 6; i++)
            {
                string path = "../../Texture/env2.bmp";
                textures[i] = new RenderTexture(path);
            }
            m_Cube.TextureMaps = textures;
            scene.AddMesh(m_Cube);
        }


        /// <summary>
        /// 绘制事件
        /// </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs pe)
        {
            m_Data = this.m_Bmp.LockBits(m_Rect, ImageLockMode.ReadWrite, this.m_PixelFormat);
            this.m_Device.Clear(m_Data);
            m_Device.Render(m_Scene, m_Data, m_ViewMat, m_ProjectionMat);
            this.m_Bmp.UnlockBits(m_Data);
            m_Graphics = pe.Graphics;
            m_Graphics.DrawImage(this.m_Bmp, new Rectangle(0, 0, m_Bmp.Width, m_Bmp.Height));
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="g"></param>
        private void OnLoad(object sender, EventArgs g)
        {
            this.MouseWheel += new MouseEventHandler(OnMouseWheel);
            this.MouseMove += new MouseEventHandler(OnMouseMove);
            this.MouseDown += new MouseEventHandler(OnMouseDown);
            this.MouseUp += new MouseEventHandler(OnMouseUp);
        }

        /// <summary>
        /// 键盘事件
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.NumPad0:
                case Keys.Q:
                    m_Device.RenderMode = RenderMode.WIREFRAME;
                    break;
                case Keys.NumPad1:
                case Keys.W:
                    m_Device.RenderMode = RenderMode.VERTEXCOLOR;
                    break;
                case Keys.NumPad2:
                case Keys.E:
                    m_Device.RenderMode = RenderMode.TEXTURED;
                    break;
                case Keys.NumPad3:
                case Keys.R:
                    m_Device.RenderMode = RenderMode.CUBETEXTURED;
                    break;
                case Keys.F1:
                    Light light = new Light(new Vector4(5, 5, -5, 1), new Color3(200, 255, 255));
                    m_Scene.AddLight(light);
                    m_Scene.IsUseLight = true;
                    break;
                case Keys.F2:
                    m_Scene.DelLight();
                    m_Scene.IsUseLight = false;
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }

            if (keyData != Keys.Escape)
                this.Invalidate();

            return true;
        }

        /// <summary>
        /// 鼠标滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta == 0)
                return;

            m_Scene.Camera.MoveForward(e.Delta / (float)900);
            m_ViewMat = m_Scene.Camera.GetLookAt();
            this.Invalidate();
        }

        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_IsMouseLeftDown == false)
                return;

            if (e.Button == MouseButtons.Left)
            {
                float x = e.X;
                float y = e.Y;
                float dx = m_MouseLeftPos.X - x;
                float dy = m_MouseLeftPos.Y - y;
                m_Cube.Transform = m_Cube.Transform * Matrix4x4.RotateY(dx / 10f);
                m_Cube.Transform = m_Cube.Transform * Matrix4x4.RotateX(dy / 10f);
                m_MouseLeftPos.X = x;
                m_MouseLeftPos.Y = y;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_IsMouseLeftDown = true;
                m_MouseLeftPos.X = e.X;
                m_MouseLeftPos.Y = e.Y;
            }
        }

        /// <summary>
        /// 鼠标抬起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_IsMouseLeftDown = false;
                m_MouseLeftPos.X = 0f;
                m_MouseLeftPos.X = 0f;
            }
        }

    }
}
