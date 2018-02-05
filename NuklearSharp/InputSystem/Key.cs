using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Key
	{
		public int down;
		public uint clicked;

	}
}
