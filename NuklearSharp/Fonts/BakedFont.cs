using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class BakedFont
	{
		public float height;
		public float ascent;
		public float descent;
		public uint glyph_offset;
		public uint glyph_count;
		public uint* ranges;

	}
}
