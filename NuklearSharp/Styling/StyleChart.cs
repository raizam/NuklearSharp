using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleChart
	{
		public StyleItem background = new StyleItem();
		public Color border_color = new Color();
		public Color selected_color = new Color();
		public Color color = new Color();
		public float border;
		public float rounding;
		public Vec2 padding = new Vec2();

	}
}
