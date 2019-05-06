
namespace SoftRender.Render
{
	struct Vector2
	{
		public float X;
		public float Y;

		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public Vector2(Vector2 vec2)
		{
			X = vec2.X;
			Y = vec2.Y;
		}
	}
}
