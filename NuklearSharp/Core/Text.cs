using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Text
	{
		public Vec2 padding;
		public Color background;
		public Color text;

	}
}
