using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct MouseButton
	{
		public int down;
		public uint clicked;
		public Vec2 clicked_pos;

	}
}
