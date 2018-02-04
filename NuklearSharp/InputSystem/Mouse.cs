namespace NuklearSharp
{
	public class Mouse
	{
		public PinnedArray<MouseButton> buttons = new PinnedArray<MouseButton>(new MouseButton[Nuklear.NK_BUTTON_MAX]);
		public Vec2 pos;
		public Vec2 prev;
		public Vec2 delta;
		public Vec2 scroll_delta;
		public byte grab;
		public byte grabbed;
		public byte ungrab;
	}
}
