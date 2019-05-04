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

namespace SoftRender
{
    public partial class SoftRender : Form
    {

        private Device mDevice;
        private Bitmap mBmp;
        private Rectangle mRect;
        private Graphics mGraphics;
        private Scene mScene;
        private PixelFormat mPixelFormat;
        private BitmapData mData;

        private Matrix mViewMat;
        private Matrix mProjectionMat;
        private Mesh mCube;
        private bool mIsMouseLeftDown = false;
        private Vector2 mMouseLeftPos = new Vector2();


        public SoftRender()
        {
            InitializeComponent();
        }

        private void InitSettings()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }



    }
}
