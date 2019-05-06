using System;

namespace SoftRender.Render
{
	//相机类
	class Camera
	{
		private Vector4 mPosition;
		private Vector4 mTarget;
		private Vector4 mUp;

		/// <summary>
		/// 相机位置
		/// </summary>
		public Vector4 Position
		{
			get { return mPosition; }
			set { mPosition = value; }
		}

		/// <summary>
		/// 目标
		/// </summary>
		public Vector4 Target
		{
			get { return mTarget; }
			set { mTarget = value; }
		}

		/// <summary>
		/// 上方
		/// </summary>
		public Vector4 Up
		{
			get { return mUp; }
			set { mUp = value; }
		}

		/// <summary>
		/// 观察矩阵
		/// </summary>
		/// <returns></returns>
		public Matrix4x4 GetLookAt()
		{
			Matrix4x4 view = new Matrix4x4(1);
			Vector4 xaxis, yaxis, zaxis;

			//法向量 z
			zaxis = mTarget - mPosition;
			zaxis.Normalize();
			xaxis = Vector4.Cross(mUp, zaxis);
			xaxis.Normalize();
			yaxis = Vector4.Cross(zaxis, xaxis);
			yaxis.Normalize();

			view.matrix[0, 0] = xaxis.X;
			view.matrix[1, 0] = xaxis.Y;
			view.matrix[2, 0] = xaxis.Z;
			view.matrix[3, 0] = -Vector4.Dot(xaxis, mPosition);

			view.matrix[0, 1] = yaxis.X;
			view.matrix[1, 1] = yaxis.Y;
			view.matrix[2, 1] = yaxis.Z;
			view.matrix[3, 1] = -Vector4.Dot(yaxis, mPosition);

			view.matrix[0, 2] = zaxis.X;
			view.matrix[1, 2] = zaxis.Y;
			view.matrix[2, 2] = zaxis.Z;
			view.matrix[3, 2] = -Vector4.Dot(zaxis, mPosition);

			view.matrix[0, 3] = view.matrix[1, 3] = view.matrix[2, 3] = 0.0f;
			view.matrix[3, 3] = 1.0f;

			return view;
		}

		/// <summary>
		/// 投影矩阵
		/// </summary>
		/// <param name="fov">y方向的视角</param>
		/// <param name="aspect">纵横比</param>
		/// <param name="zn">近裁剪 平面到原点的距离</param>
		/// <param name="zf">远裁剪 平面到原点的距离</param>
		/// <returns></returns>
		public Matrix4x4 GetProject(float fov, float aspect, float zn, float zf)
		{
			Matrix4x4 project = new Matrix4x4(1);
			project.SetZero();
			project.matrix[0, 0] = 1 / ((float)Math.Tan(fov * 0.5f) * aspect);
			project.matrix[1, 1] = 1 / (float)Math.Tan(fov * 0.5f);
			project.matrix[2, 2] = (zf + zn) / (zf - zn);
			project.matrix[2, 3] = 1.0f;
			project.matrix[3, 2] = 2 * (zn * zf) / (zn - zf);

			return project;
		}

		/// <summary>
		/// 旋转摄像机
		/// </summary>
		/// <param name="position"></param>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public void Rotate(Vector4 position, float x, float y)
		{
			mPosition = (Matrix4x4.RotateX(x) * Matrix4x4.RotateY(y)).LeftApply(position);
		}

		/// <summary>
		/// 改变相机的位置
		/// </summary>
		/// <param name="pos"></param>
		public void UpdatePosition(Vector4 pos)
		{
			mPosition = pos;
		}

		/// <summary>
		/// 前后移动摄像机
		/// </summary>
		/// <param name="distance">移动的距离</param>
		public void MoveForward(float distance)
		{
			Vector4 dir = (mTarget - mPosition);
			float w = mPosition.W;
			if (distance > 0 && dir.Length < 1.5f)
				return;

			if (distance < 0 && dir.Length > 30)
				return;

			mPosition = mPosition + (dir.Normalize() * distance);
			mPosition.W = w;
		}

		/// <summary>
		/// 绕X轴旋转
		/// </summary>
		/// <param name="r"></param>
		public void MoveTheta(float r)
		{
			mPosition = mPosition * Matrix4x4.RotateX(r);
		}

		/// <summary>
		/// 绕y轴旋转
		/// </summary>
		/// <param name="r"></param>
		public void MovePhi(float r)
		{
			mPosition = mPosition * Matrix4x4.RotateY(r);
		}
	}
}
