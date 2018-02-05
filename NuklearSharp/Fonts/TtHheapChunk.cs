using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TtHheapChunk
	{
		public TtHheapChunk* next;

	}
}
