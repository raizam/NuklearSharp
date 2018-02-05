using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class FontConfig
	{
		public FontConfig next;
		public void * ttf_blob;
		public ulong ttf_size;
		public byte ttf_data_owned_by_atlas;
		public byte merge_mode;
		public byte pixel_snap;
		public byte oversample_v;
		public byte oversample_h;
		public PinnedArray<byte> padding = new PinnedArray<byte>(3);
		public float size;
		public int coord_type;
		public Vec2 spacing = new Vec2();
		public uint* range;
		public BakedFont font;
		public uint fallback_glyph;
		public FontConfig n;
		public FontConfig p;

	}
}
