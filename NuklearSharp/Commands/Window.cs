namespace NuklearSharp
{
	public partial class Window
	{
		public uint seq;
		public uint name;
		public PinnedArray<char> name_string = new PinnedArray<char>(64);
		public uint flags;
		public Rect bounds = new Rect();
		public Scroll scrollbar = new Scroll();
		public CommandBuffer buffer = new CommandBuffer();
		public Panel layout;
		public float scrollbar_hiding_timer;
		public PropertyState property = new PropertyState();
		public PopupState popup = new PopupState();
		public EditState edit = new EditState();
		public uint scrolled;
		public Table tables;
		public uint table_count;
		public Window next;
		public Window prev;
		public Window parent;
	}
}