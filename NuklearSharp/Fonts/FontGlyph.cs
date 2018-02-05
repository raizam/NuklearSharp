using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct FontGlyph
	{
		public char codepoint;
		public float xadvance;
		public float x0;
		public float y0;
		public float x1;
		public float y1;
		public float w;
		public float h;
		public float u0;
		public float v0;
		public float u1;
		public float v1;

	}
}
