using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Table
	{
		public uint seq;
		public uint size;
		public PinnedArray<uint> keys = new PinnedArray<uint>(51);
		public PinnedArray<uint> values = new PinnedArray<uint>(51);
		public Table next;
		public Table prev;

	}
}
