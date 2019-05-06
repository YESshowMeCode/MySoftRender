
namespace SoftRender.Render
{
	public enum FaceTypes
	{
		LEFT = 0,
		RIGHT,
		TOP,
		BUTTOM,
		NEAR,
		FAR,
		NONE,
	}

	struct Face
	{
		public int A;
		public int B;
		public int C;
		public FaceTypes FaceType;

		public Face(int a,int b,int c)
		{
			this.A = a;
			this.B = b;
			this.C = c;
			FaceType = FaceTypes.NONE;
		}

		public Face(int a, int b, int c, FaceTypes face)
		{
			this.A = a;
			this.B = b;
			this.C = c;
			this.FaceType = face;
		}
	}
}
