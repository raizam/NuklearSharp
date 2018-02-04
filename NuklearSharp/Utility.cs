using System.Runtime.InteropServices;

namespace NuklearSharp
{
	unsafe partial class Nuklear
	{
		[StructLayout(LayoutKind.Explicit)]
		private struct InvSqrtUnion
		{
			[FieldOffset(0)] public uint i;

			[FieldOffset(0)] public float f;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct MurmurHashUnion
		{
			[FieldOffset(0)] public uint* i;

			[FieldOffset(0)] public byte* b;

			public MurmurHashUnion(void* ptr)
			{
				i = (uint*) ptr;
				b = (byte*) ptr;
			}
		}

		public static float InvSqrt(float number)
		{
			var threehalfs = 1.5f;
			var conv = new InvSqrtUnion
			{
				i = 0,
				f = number,
			};
			var x2 = number*0.5f;
			conv.i = 0x5f375A84 - (conv.i >> 1);
			conv.f = conv.f*(threehalfs - (x2*conv.f*conv.f));
			return conv.f;
		}

		public static int UtfDecode(char* c, int pos, char* u, int clen)
		{
			*u = c[pos];

			return 1;
		}

		public static int UtfDecode(char* c, char* u, int clen)
		{
			return UtfDecode(c, 0, u, clen);
		}

		public static int UtfEncode(char c, char* u, int clen)
		{
			*u = c;

			return 1;
		}

		public static int UtfLen(char* str, int len)
		{
			return len;
		}

		public static string StyleGetColorByName(int c)
		{
			return ColorNames[c];
		}

		public static int Strlen(sbyte* str)
		{
			var siz = 0;
			while (((str) != null) && (*str++ != '\0'))
			{
				siz++;
			}
			return siz;
		}
	}
}