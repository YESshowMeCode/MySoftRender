using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftRender.Render
{
    class Camera
    {
        private Vector4 m_Position;
        private Vector4 m_target;
        private Vector4 m_Up;


        public Vector4 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        public Vector4 Target
        {
            get
            {
                return m_target;
            }
            set
            {
                m_target = value ;
            }
        }

        public Vector4 Up
        {
            get
            {
                return m_Up;
            }
            set
            {
                m_Up = value;
            }
        }

        public Matrix4x4 GetLookAt()
        {
            Matrix4x4 view = new Matrix4x4(1);

            Vector4 xAxis, yAxis, zAxis;

            zAxis = m_target - m_Position;
            zAxis.Normalize();
            xAxis = Vector4.Cross(m_Up, m_Position);
            xAxis.Normalize();
            yAxis = Vector4.Cross(zAxis, xAxis);
            yAxis.Normalize();

            view[0, 0] = xAxis.x;
            view[1, 0] = xAxis.y;
            view[2, 0] = xAxis.z;
            view[3, 0] = -Vector4.Dot(xAxis, m_Position);

            view[0, 1] = yAxis.x;
            view[1, 1] = yAxis.y;
            view[2, 1] = yAxis.z;
            view[3, 1] = -Vector4.Dot(yAxis, m_Position);

            view[0, 2] = zAxis.x;
            view[1, 2] = zAxis.y;
            view[2, 2] = zAxis.z;
            view[3, 2] = -Vector4.Dot(zAxis, m_Position);

            view[0, 3] = 0.0f;
            view[1, 3] = 0.0f;
            view[2, 3] = 0.0f;
            view[3, 3] = 1.0f;

            return view;
        }


        public Matrix4x4 GetProject(float fov,float aspect,float zn,float zf)
        {
            Matrix4x4 project = new Matrix4x4(1);
            project.SetZero();
            project[0, 0] = 1 / ((float)Math.Tan(fov * 0.5f) * aspect);
            project[1, 1] = 1 / ((float)Math.Tan(fov * 0.5f));
            project[2, 2] = (zf + zn) / (zf - zn);
            project[2, 3] = 1.0f;
            project[3, 2] = 2 * (zn * zf) / (zn - zf);

            return project;
        }

        public void Rotate(Vector4 position ,float x,float y)
        {
            m_Position = (Matrix4x4.RotateX(x) * Matrix4x4.RotateY(y)).LeftApply(position);
        }

        public void MoveForward(float distance)
        {
            Vector4 dir = (m_target - m_Position);
            float w = m_Position.w;
            if (distance > 0 && dir.length < 1.5f)
            {
                return;
            }

            if (distance < 0 && dir.length > 30)
            {
                return;
            }

            m_Position = m_Position + (dir.Normalize() * distance);
            m_Position.w = w;
        }



        public void MoveTheta(float r)
        {
            m_Position = m_Position * Matrix4x4.RotateX(r);
        }

        public void MovePhi(float r)
        {
            m_Position = m_Position * Matrix4x4.RotateY(r);
        }

    }

}
