using Microsoft.Xna.Framework;

namespace NuklearSharp.MonoGame
{
	internal static class Helpers
	{
		internal static Nuklear.nk_color ToNkColor(this Color color)
		{
			return new Nuklear.nk_color {a = color.A, b = color.B, g = color.G, r = color.R};
		}

		internal static Nuklear.nk_colorf ToNkColorf(this Color color)
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

		internal static Nuklear.nk_vec2 ToNkVec2(this Vector2 v2)
		{
			return new Nuklear.nk_vec2 {x = v2.X, y = v2.Y};
		}

		internal static Color ToColor(this Nuklear.nk_color c)
		{
			return new Color(c.r, c.g, c.b, c.a);
		}

		internal static Color ToColor(this Nuklear.nk_colorf c)
		{
			return new Color(c.r, c.g, c.b, c.a);
		}
	}
}