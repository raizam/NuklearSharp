using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtBakedchar
	{
		public ushort x0;
		public ushort y0;
		public ushort x1;
		public ushort y1;
		public float xoff;
		public float yoff;
		public float xadvance;

	}
}
