using Microsoft.Xna.Framework;

namespace NuklearSharp.MonoGame
{
	public static class Helpers
	{
		public static Nuklear.nk_color ToNkColor(this Color color)
		{
			return new Nuklear.nk_color {a = color.A, b = color.B, g = color.G, r = color.R};
		}

		public static Nuklear.nk_colorf ToNkColorf(this Color color)
		{
			const float s = 1.0f/255.0f;
			return new Nuklear.nk_colorf
			{
				r = color.R*s,
				g = color.G*s,
				b = color.B*s,
				a = color.A*s
			};
		}

		public static Nuklear.nk_vec2 ToNkVec2(this Vector2 v2)
		{
			return new Nuklear.nk_vec2 {x = v2.X, y = v2.Y};
		}

		public static Color ToColor(this Nuklear.nk_color c)
		{
			return new Color(c.r, c.g, c.b, c.a);
		}

		public static Color ToColor(this Nuklear.nk_colorf c)
		{
			return new Color(c.r, c.g, c.b, c.a);
		}

		public static Nuklear.nk_rect ToRect(this Rectangle rect)
		{
			return new Nuklear.nk_rect {x = rect.X, y = rect.Y, w = rect.Width, h = rect.Height};
		}

		public static bool BeginTitled(this ContextWrapper ctx, string name, string title, Rectangle bounds, uint flags)
		{
			return ctx.BeginTitled(name, title, bounds.ToRect(), flags);
		}

		public static bool ButtonColor(this ContextWrapper ctx, Color color)
		{
			return ctx.ButtonColor(color.ToNkColor());
		}

		public static void LabelColored(this ContextWrapper ctx, string str, uint align, Color color)
		{
			ctx.LabelColored(str, align, color.ToNkColor());
		}

		public static bool ComboBeginColor(this ContextWrapper ctx, Color color, Vector2 size)
		{
			return ctx.ComboBeginColor(color.ToNkColor(), size.ToNkVec2());
		}

		public static Color ColorPicker(this ContextWrapper ctx, Color color, int fmt)
		{
			return ctx.ColorPicker(color.ToNkColorf(), fmt).ToColor();
		}
	}
}