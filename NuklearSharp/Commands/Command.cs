using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Command
	{
		public int type;
		public ulong next;
		public Handle userdata;

	}
}
