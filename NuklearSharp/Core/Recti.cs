using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Recti
	{
		public short x;
		public short y;
		public short w;
		public short h;

	}
}
