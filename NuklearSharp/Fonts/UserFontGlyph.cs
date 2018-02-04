using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct UserFontGlyph
	{
		public fixed float uv_x[2];
		public fixed float uv_y[2];
		public Vec2 offset;
		public float width;
		public float height;
		public float xadvance;
	}
}
