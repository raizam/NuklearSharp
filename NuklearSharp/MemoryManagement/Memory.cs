using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Memory
	{
		public void * ptr;
		public ulong size;

	}
}
