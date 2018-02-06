namespace NuklearSharp
{
	partial class DrawList
	{
		public Rect clip_rect;
		public Vec2[] circle_vtx = new Vec2[12];
		public ConvertConfig config;
		public Buffer buffer;
		public Buffer vertices;
		public Buffer elements;
		public uint element_count;
		public uint vertex_count;
		public uint cmd_count;
		public ulong cmd_offset;
		public uint path_count;
		public uint path_offset;
		public int line_AA;
		public int shape_AA;
		public Handle userdata;
	}
}
