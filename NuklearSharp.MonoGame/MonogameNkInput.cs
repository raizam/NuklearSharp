using Microsoft.Xna.Framework.Input;

namespace NuklearSharp.MonoGame
{
	/// <summary>
	/// Handles Nuklear Input
	/// </summary>
	public static class MonogameNkInput
	{
		private const int WHEEL_DELTA = 120;
		private static MouseState previousMouseState = default(MouseState);
		private static int previousWheel;

		public static void Update(ContextWrapper ctx)
		{
			var state = Mouse.GetState();

			ctx.InputBegin();

			if (previousMouseState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
				ctx.InputButton(Nuklear.NK_BUTTON_LEFT, state.X, state.Y, 1);
			else if (previousMouseState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released)
				ctx.InputButton(Nuklear.NK_BUTTON_LEFT, state.X, state.Y, 0);

			if (previousMouseState.RightButton == ButtonState.Released && state.RightButton == ButtonState.Pressed)
				ctx.InputButton(Nuklear.NK_BUTTON_RIGHT, state.X, state.Y, 1);
			else if (previousMouseState.RightButton == ButtonState.Pressed && state.RightButton == ButtonState.Released)
				ctx.InputButton(Nuklear.NK_BUTTON_RIGHT, state.X, state.Y, 0);

			ctx.InputMotion(state.X, state.Y);
			ctx.InputScroll(new Nuklear.nk_vec2 {x = 0, y = (state.ScrollWheelValue - previousWheel)/WHEEL_DELTA});
			ctx.InputEnd();

			previousWheel = state.ScrollWheelValue;
			previousMouseState = state;
		}
	}
}