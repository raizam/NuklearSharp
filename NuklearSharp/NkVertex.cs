using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	internal unsafe struct NkVertex
	{
		[FieldOffset(0)] internal fixed float position [3];
		[FieldOffset(12)] internal fixed byte col [4];
		[FieldOffset(16)] internal fixed float uv [2];
	}
}