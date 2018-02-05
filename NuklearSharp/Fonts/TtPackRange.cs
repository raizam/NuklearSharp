using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtPackRange
	{
		public float font_size;
		public int first_unicode_codepoint_in_range;
		public int* array_of_unicode_codepoints;
		public int num_chars;
		public TtPackedchar* chardata_for_range;
		public byte h_oversample;
		public byte v_oversample;

	}
}
