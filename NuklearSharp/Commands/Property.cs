using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Property
	{
		[FieldOffset(0)]
		public int i;

		[FieldOffset(0)]
		public float f;

		[FieldOffset(0)]
		public double d;
	}
}
