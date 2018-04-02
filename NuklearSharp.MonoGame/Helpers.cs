using Microsoft.Xna.Framework;

namespace NuklearSharp.MonoGame
{
    internal static class Helpers
    {
        internal static NkColor ToNkColor(this Color color)
        {
            return new NkColor { a = color.A, b = color.B, g = color.G, r = color.R };
        }

        internal static NkColorF ToNkColorf(this Color color)
        {
            const float s = 1.0f / 255.0f;
            return new NkColorF
            {
                r = color.R * s,
                g = color.G * s,
                b = color.B * s,
                a = color.A * s
            };
        }

        internal static NkVec2 ToNkVec2(this Vector2 v2)
        {
            return new NkVec2 { x = v2.X, y = v2.Y };
        }

        internal static Color ToColor(this NkColor c)
        {
            return new Color(c.r, c.g, c.b, c.a);
        }

        internal static Color ToColor(this NkColorF c)
        {
            return new Color(c.r, c.g, c.b, c.a);
        }

        internal static NkRect ToRect(this Rectangle rect)
        {
            return new NkRect { x = rect.X, y = rect.Y, w = rect.Width, h = rect.Height };
        }
    }
}