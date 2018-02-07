using Microsoft.Xna.Framework;
namespace NuklearSharp.MonoGame
{
	public static class ContextExtensions
	{
		public static bool BeginTitled(this Context ctx, string name, string title, Microsoft.Xna.Framework.Rectangle bounds, uint flags)
		{
			return ctx.BeginTitled(name, title, bounds.ToRect(), flags);
		}

		public static bool ButtonColor(this Context ctx, Microsoft.Xna.Framework.Color color)
		{
			return ctx.ButtonColor(color.ToNkColor()) != 0;
		}

		public static void LabelColored(this Context ctx, string str, uint align, Microsoft.Xna.Framework.Color color)
		{
			ctx.LabelColored(str, align, color.ToNkColor());
		}

		public static bool ComboBeginColor(this Context ctx, Microsoft.Xna.Framework.Color color, Vector2 size)
		{
			return ctx.ComboBeginColor(color.ToNkColor(), size.ToNkVec2()) != 0;
		}

		public static Microsoft.Xna.Framework.Color ColorPicker(this Context ctx, Microsoft.Xna.Framework.Color color, int fmt)
		{
			return ctx.ColorPicker(color.ToNkColorf(), fmt).ToColor();
		}
	}
}
