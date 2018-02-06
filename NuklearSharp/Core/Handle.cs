using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Explicit)]
	unsafe partial struct Handle
	{
		[FieldOffset(0)]
		public void* ptr;

		[FieldOffset(0)]
		public int id;
	}
}
