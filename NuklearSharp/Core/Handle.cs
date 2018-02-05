using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct Handle
	{
		public Image SubimageHandle(ushort w, ushort h, Rect r)
		{
			Image s =  new Image();
			
			s.handle = (Handle)(this);
			s.w = (ushort)(w);
			s.h = (ushort)(h);
			s.region[0] = ((ushort)(r.x));
			s.region[1] = ((ushort)(r.y));
			s.region[2] = ((ushort)(r.w));
			s.region[3] = ((ushort)(r.h));
			return (Image)(s);
		}

		public Image ImageHandle()
		{
			Image s =  new Image();
			
			s.handle = (Handle)(this);
			s.w = (ushort)(0);
			s.h = (ushort)(0);
			s.region[0] = (ushort)(0);
			s.region[1] = (ushort)(0);
			s.region[2] = (ushort)(0);
			s.region[3] = (ushort)(0);
			return (Image)(s);
		}

	}
}
