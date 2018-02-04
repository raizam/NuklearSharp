namespace NuklearSharp
{
	public class Panel
	{
		public int type;
		public uint flags;
		public Rect bounds = new Rect();
		public Scroll offset;
		public float at_x;
		public float at_y;
		public float max_x;
		public float footer_height;
		public float header_height;
		public float border;
		public uint has_scrolling;
		public Rect clip = new Rect();
		public MenuState menu = new MenuState();
		public RowLayout row = new RowLayout();
		public Chart chart = new Chart();
		public CommandBuffer buffer;
		public Panel parent;
	}
}
