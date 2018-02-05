using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PropertyVariant
	{
		public int kind;
		public Property value;
		public Property min_value;
		public Property max_value;
		public Property step;
	}
}
