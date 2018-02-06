// Generated by Sichem at 2/6/2018 7:10:18 PM

using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct RpRect
	{
		public int id;
		public ushort w;
		public ushort h;
		public ushort x;
		public ushort y;
		public int was_packed;

		public void RpQsort(uint len, IntPtr* cmp)
		{
			uint right;uint left = (uint)(0);uint* stack = stackalloc uint[64];uint pos = (uint)(0);
			uint seed = (uint)(len / 2 * 69069 + 1);
			for (; ; ) {
for (; (left + 1) < (len); len++) {
RpRect pivot =  new RpRect();RpRect tmp =  new RpRect();if ((pos) == (64)) len = (uint)(stack[pos = (uint)(0)]);pivot = (RpRect)(this[left + seed % (len - left)]);seed = (uint)(seed * 69069 + 1);stack[pos++] = (uint)(len);for (right = (uint)(left - 1);;) {
while ((cmp(&this[++right], &pivot)) < (0)) {}while ((cmp(&pivot, &this[--len])) < (0)) {}if ((right) >= (len)) break;tmp = (RpRect)(this[right]);this[right] = (RpRect)(this[len]);this[len] = (RpRect)(tmp);}
}if ((pos) == (0)) break;left = (uint)(len);len = (uint)(stack[--pos]);}
		}

	}
}