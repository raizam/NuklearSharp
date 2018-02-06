using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuklearSharp
{
	partial class UserFont
	{
		public Handle userdata;
		public float height;
		public Handle texture;

		public Nuklear.NkTextWidthDelegate width;
		public Nuklear.NkQueryFontGlyphDelegate query;
	}
}
