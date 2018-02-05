using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtActiveEdge
	{
		public TtActiveEdge* next;
		public float fx;
		public float fdx;
		public float fdy;
		public float direction;
		public float sy;
		public float ey;

	}
}
