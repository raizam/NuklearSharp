using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Image
	{
		public Handle handle = new Handle();
		public ushort w;
		public ushort h;
		public PinnedArray<ushort> region = new PinnedArray<ushort>(4);

		public int IsSubimage()
		{
			return (int)((((this.w) == (0)) && ((this.h) == (0)))?1:0);
		}

		public StyleItem StyleItemImage()
		{
			StyleItem i =  new StyleItem();
			i.type = (int)(Nuklear.NK_STYLE_ITEM_IMAGE);
			i.data.image = (Image)(this);
			return (StyleItem)(i);
		}

	}
}
