using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class EditState
	{
		public uint name;
		public uint seq;
		public uint old;
		public int active;
		public int prev;
		public int cursor;
		public int sel_start;
		public int sel_end;
		public Scroll scrollbar = new Scroll();
		public byte mode;
		public byte single_line;

	}
}
