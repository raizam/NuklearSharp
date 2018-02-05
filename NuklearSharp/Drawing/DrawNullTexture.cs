using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct DrawNullTexture
	{
		public Handle texture;
		public Vec2 uv;

	}
}
