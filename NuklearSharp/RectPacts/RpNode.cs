using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct RpNode
	{
		public ushort x;
		public ushort y;
		public RpNode* next;

	}
}
