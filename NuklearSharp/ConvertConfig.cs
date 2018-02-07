namespace NuklearSharp
{
	public partial class ConvertConfig
	{
		public float global_alpha;
		public int line_AA;
		public int shape_AA;
		public uint circle_segment_count;
		public uint arc_segment_count;
		public uint curve_segment_count;
		public DrawNullTexture _null_ = new  DrawNullTexture();
		public DrawVertexLayoutElement[] vertex_layout;
		public ulong vertex_size;
		public ulong vertex_alignment;
	}
}
