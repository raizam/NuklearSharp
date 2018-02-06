namespace NuklearSharp
{
	unsafe partial class Context
	{
		public Input input = new Input();
		public Style style = new Style();
		public Buffer memory = new Buffer();
		public Clipboard clip = new Clipboard();
		public uint last_widget_state;
		public int button_behavior;
		public ConfigurationStacks stacks = new ConfigurationStacks();
		public float delta_time_seconds;
		public DrawList draw_list = new DrawList();
		public Handle userdata = new Handle();
		public TextEdit text_edit = new TextEdit();
		public CommandBuffer overlay = new CommandBuffer();
		public int build;
		public Window begin;
		public Window end;
		public Window active;
		public Window current;
		public uint count;
		public uint seq;

		public Window Begin()
		{
			if (this.count == 0) return null;
			if (build == 0)
			{
				Build();
				build = Nuklear.nk_true;
			}

			var iter = begin;
			while ((iter != null) &&
			       ((iter.buffer.commands.Count == 0) || (iter.flags & Nuklear.NK_WINDOW_HIDDEN) != 0 || (iter.seq != seq)))
			{
				iter = iter.next;
			}

			return iter;
		}

		public void Build()
		{
			if (style.cursor_active == null) style.cursor_active = style.cursors[Nuklear.NK_CURSOR_ARROW];
			if ((style.cursor_active != null) && (input.mouse.grabbed == 0) && ((style.cursor_visible) != 0))
			{
				var mouse_bounds = new Rect();
				var cursor = style.cursor_active;
				overlay.Init(Nuklear.NK_CLIPPING_OFF);
				StartBuffer(overlay);
				mouse_bounds.x = input.mouse.pos.x - cursor.offset.x;
				mouse_bounds.y = input.mouse.pos.y - cursor.offset.y;
				mouse_bounds.w = cursor.size.x;
				mouse_bounds.h = cursor.size.y;
				overlay.DrawImage(mouse_bounds, cursor.img, Nuklear.nk_white);
				FinishBuffer(overlay);
			}

			var it = begin;
			CommandBase cmd = null;
			for (; it != null;)
			{
				var next = it.next;
				if (((it.flags & Nuklear.NK_WINDOW_HIDDEN) != 0) || (it.seq != seq))
					goto cont;
				cmd = it.buffer.last;

				while ((next != null) &&
				       ((next.buffer.last == next.buffer.begin) || ((next.flags & Nuklear.NK_WINDOW_HIDDEN) != 0)))
				{
					next = next.next;
				}
				if (next != null) cmd.next = next.buffer.begin;
				cont:
				it = next;
			}

			it = begin;

			while (it != null)
			{
				Window _next_ = it.next;
				PopupBuffer buf;

				if (it.popup.buf.active == 0) goto skip;
				buf = it.popup.buf;
				cmd.next = buf.begin;
				cmd = buf.last;
				buf.active = Nuklear.nk_false;
				skip:
				it = _next_;
			}
			if (cmd != null)
			{
				if (overlay.commands.Count > 0)
				{
					cmd.next = overlay.begin;
				}
				else
				{
					cmd.next = null;
				}
			}
		}

		public int InitFixed(void* m, ulong size, UserFont font)
		{
			if (m == null) return 0;
			Setup(font);


			this.memory.InitFixed(m, size);
			return 1;
		}

		public int Init(UserFont font)
		{
			Setup(font);

			this.memory.Init(4*1024);
			return 1;
		}

		public void Free()
		{
			memory.Free();

			seq = 0;
			build = 0;
			begin = null;
			end = null;
			active = null;
			current = null;
			count = 0;
		}

		public Table CreateTable()
		{
			var result = new Table();

			return result;
		}

		public Window CreateWindow()
		{
			var result = new Window {seq = seq};

			return result;
		}

		public void FreeWindow(Window win)
		{
			Table it = win.tables;
			if (win.popup.win != null)
			{
				FreeWindow(win.popup.win);
				win.popup.win = null;
			}

			win.next = null;
			win.prev = null;
			while (it != null)
			{
				var n = it.next;
				RemoveTable(win, it);
				if (it == win.tables) win.tables = n;
				it = n;
			}
		}

		public Panel CreatePanel()
		{
			var result = new Panel();

			return result;
		}

		public void FreePanel(Panel panel)
		{
		}

		public int InitDefault(UserFont font)
		{
			return Init(font);
		}

		public void Propertyz(char* name, PropertyVariant* variant, float inc_per_pixel, int filter)
		{
			var bounds = new Rect();
			uint hash;
			char* dummy_buffer = stackalloc char[64];
			var dummy_state = Nuklear.NK_PROPERTY_DEFAULT;
			var dummy_length = 0;
			var dummy_cursor = 0;
			var dummy_select_begin = 0;
			var dummy_select_end = 0;
			if ((current == null) || (current.layout == null)) return;
			var win = current;
			var layout = win.layout;
			var style = this.style;
			var s = Widget(&bounds, ctx);
			if (s == 0) return;
			if (name[0] == '#')
			{
				hash = Nuklear.MurmurHash(name, Nuklear.Strlen(name), win.property.seq++);
				name++;
			}
			else hash = Nuklear.MurmurHash(name, Nuklear.Strlen(name), 42);

			var _in_ = ((s == Nuklear.NK_WIDGET_ROM) && (win.property.active == 0)) ||
			           ((layout.flags & Nuklear.NK_WINDOW_ROM) != 0)
				? null
				: input;

			int old_state, state;
			char* buffer;
			int len, cursor, select_begin, select_end;
			if ((win.property.active != 0) && (hash == win.property.name))
			{
				old_state = win.property.state;

				nk_do_property(ref ctx.last_widget_state, win.buffer, bounds, name, variant, inc_per_pixel,
					win.property.buffer, ref win.property.length, ref win.property.state, ref win.property.cursor,
					ref win.property.select_start, ref win.property.select_end, style.property, filter, _in_, style.font,
					ctx.text_edit, ctx.button_behavior);
				state = win.property.state;
				buffer = win.property.buffer;
				len = win.property.length;
				cursor = win.property.cursor;
				select_begin = win.property.select_start;
				select_end = win.property.select_end;
			}
			else
			{
				old_state = dummy_state;
				nk_do_property(ref ctx.last_widget_state, win.buffer, bounds, name, variant, inc_per_pixel,
					dummy_buffer, ref dummy_length, ref dummy_state, ref dummy_cursor,
					ref dummy_select_begin, ref dummy_select_end, style.property, filter, _in_, style.font,
					ctx.text_edit, ctx.button_behavior);
				state = dummy_state;
				buffer = dummy_buffer;
				len = dummy_length;
				cursor = dummy_cursor;
				select_begin = dummy_select_begin;
				select_end = dummy_select_end;
			}

			ctx.text_edit.clip = ctx.clip;
			if ((_in_ != null) && (state != NK_PROPERTY_DEFAULT) && (win.property.active == 0))
			{
				win.property.active = 1;
				nk_memcopy(win.property.buffer, buffer, (ulong) len);
				win.property.length = len;
				win.property.cursor = cursor;
				win.property.state = state;
				win.property.name = hash;
				win.property.select_start = select_begin;
				win.property.select_end = select_end;
				if (state == NK_PROPERTY_DRAG)
				{
					ctx.input.mouse.grab = nk_true;
					ctx.input.mouse.grabbed = nk_true;
				}
			}

			if ((state == NK_PROPERTY_DEFAULT) && (old_state != NK_PROPERTY_DEFAULT))
			{
				if (old_state == NK_PROPERTY_DRAG)
				{
					ctx.input.mouse.grab = nk_false;
					ctx.input.mouse.grabbed = nk_false;
					ctx.input.mouse.ungrab = nk_true;
				}
				win.property.select_start = 0;
				win.property.select_end = 0;
				win.property.active = 0;
			}
		}
	}
}