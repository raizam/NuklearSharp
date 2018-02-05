using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class StyleWindow
	{
		public StyleWindowHeader header = new StyleWindowHeader();
		public StyleItem fixed_background = new StyleItem();
		public Color background = new Color();
		public Color border_color = new Color();
		public Color popup_border_color = new Color();
		public Color combo_border_color = new Color();
		public Color contextual_border_color = new Color();
		public Color menu_border_color = new Color();
		public Color group_border_color = new Color();
		public Color tooltip_border_color = new Color();
		public StyleItem scaler = new StyleItem();
		public float border;
		public float combo_border;
		public float contextual_border;
		public float menu_border;
		public float group_border;
		public float tooltip_border;
		public float popup_border;
		public float min_row_height_padding;
		public float rounding;
		public Vec2 spacing = new Vec2();
		public Vec2 scrollbar_size = new Vec2();
		public Vec2 min_size = new Vec2();
		public Vec2 padding = new Vec2();
		public Vec2 group_padding = new Vec2();
		public Vec2 popup_padding = new Vec2();
		public Vec2 combo_padding = new Vec2();
		public Vec2 contextual_padding = new Vec2();
		public Vec2 menu_padding = new Vec2();
		public Vec2 tooltip_padding = new Vec2();

	}
}
