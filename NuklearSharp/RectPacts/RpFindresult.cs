using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct RpFindresult
	{
		public int x;
		public int y;
		public RpNode** prev_link;

	}
}
