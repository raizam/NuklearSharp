using Microsoft.Xna.Framework;

namespace NuklearSharp.MonoGame
{
	internal static class Helpers
	{
		internal static Color ToNkColor(this Microsoft.Xna.Framework.Color color)
		{
			return new Color {a = color.A, b = color.B, g = color.G, r = color.R};
		}

		internal static Colorf ToNkColorf(this Microsoft.Xna.Framework.Color color)
		{
			const float s = 1.0f/255.0f;
			return new Colorf
			{
				r = color.R*s,
				g = color.G*s,
				b = color.B*s,
				a = color.A*s
			};
		}

		internal static Vec2 ToNkVec2(this Vector2 v2)
		{
			return new Vec2 {x = v2.X, y = v2.Y};
		}

		internal static Microsoft.Xna.Framework.Color ToColor(this Color c)
		{
			return new Microsoft.Xna.Framework.Color(c.r, c.g, c.b, c.a);
		}

		internal static Microsoft.Xna.Framework.Color ToColor(this Colorf c)
		{
			return new Microsoft.Xna.Framework.Color(c.r, c.g, c.b, c.a);
		}

		internal static Rect ToRect(this Rectangle rect)
		{
			return new Rect
			{
				x = rect.X,
				y = rect.Y,
				w = rect.Width,
				h = rect.Height
			};
		}
	}
}