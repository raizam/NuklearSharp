using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct BufferMarker
	{
		public int active;
		public ulong offset;

	}
}
