using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct FontBakeData
	{
		public TtFontinfo info;
		public RpRect* rects;
		public TtPackRange* ranges;
		public uint range_count;

	}
}
