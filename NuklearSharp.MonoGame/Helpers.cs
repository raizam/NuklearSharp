using Microsoft.Xna.Framework;

namespace NuklearSharp.MonoGame
{
    internal static class Helpers
    {
        internal static Nk.nk_color ToNkColor(this Color color)
        {
            return new Nk.nk_color { a = color.A, b = color.B, g = color.G, r = color.R };
        }

        internal static Nk.nk_colorf ToNkColorf(this Color color)
        {
            const float s = 1.0f / 255.0f;
            return new Nk.nk_colorf
            {
                r = color.R * s,
                g = color.G * s,
                b = color.B * s,
                a = color.A * s
            };
        }

        internal static Nk.nk_vec2 ToNkVec2(this Vector2 v2)
        {
            return new Nk.nk_vec2 { x = v2.X, y = v2.Y };
        }

        internal static Color ToColor(this Nk.nk_color c)
        {
            return new Color(c.r, c.g, c.b, c.a);
        }

        internal static Color ToColor(this Nk.nk_colorf c)
        {
            return new Color(c.r, c.g, c.b, c.a);
        }

        internal static Nk.nk_rect ToRect(this Rectangle rect)
        {
            return new Nk.nk_rect { x = rect.X, y = rect.Y, w = rect.Width, h = rect.Height };
        }
    }
}