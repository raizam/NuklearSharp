using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		public unsafe partial class nk_scroll
		{
			public uint x;
			public uint y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_command
		{
			public int type;
			public ulong next;
		}

		public unsafe partial class nk_row_layout
		{
			public int type;
			public int index;
			public float height;
			public float min_height;
			public int columns;
			public float* ratio;
			public float item_width;
			public float item_height;
			public float item_offset;
			public float filled;
			public nk_rect item = new nk_rect();
			public int tree_depth;
			public PinnedArray<float> templates = new PinnedArray<float>(16);
		}

		public unsafe partial class nk_menu_state
		{
			public float x;
			public float y;
			public float w;
			public float h;
			public nk_scroll offset = new nk_scroll();
		}

		public unsafe partial class nk_popup_state
		{
			public nk_window win;
			public int type;
			public nk_popup_buffer buf = new nk_popup_buffer();
			public uint name;
			public int active;
			public uint combo_count;
			public uint con_count;
			public uint con_old;
			public uint active_con;
			public nk_rect header = new nk_rect();
		}

		public unsafe partial class nk_edit_state
		{
			public uint name;
			public uint seq;
			public uint old;
			public int active;
			public int prev;
			public int cursor;
			public int sel_start;
			public int sel_end;
			public nk_scroll scrollbar = new nk_scroll();
			public byte mode;
			public byte single_line;
		}

		public unsafe partial class nk_property_state
		{
			public int active;
			public int prev;
			public string buffer;
			public int cursor;
			public int select_start;
			public int select_end;
			public uint name;
			public uint seq;
			public uint old;
			public int state;
		}

		public unsafe partial class nk_table
		{
			public uint seq;
			public uint size;
			public PinnedArray<uint> keys = new PinnedArray<uint>(51);
			public PinnedArray<uint> values = new PinnedArray<uint>(51);
			public nk_table next;
			public nk_table prev;
		}

		public static void nk_push_scissor(nk_command_buffer b, nk_rect r)
		{
			nk_command_scissor cmd;
			if (b == null) return;
			b.clip.x = (float) (r.x);
			b.clip.y = (float) (r.y);
			b.clip.w = (float) (r.w);
			b.clip.h = (float) (r.h);
			cmd = (nk_command_scissor) (nk_command_buffer_push(b, (int) (NK_COMMAND_SCISSOR)));
			if (cmd == null) return;
			cmd.x = ((short) (r.x));
			cmd.y = ((short) (r.y));
			cmd.w = ((ushort) ((0) < (r.w) ? (r.w) : (0)));
			cmd.h = ((ushort) ((0) < (r.h) ? (r.h) : (0)));
		}

		public static void nk_stroke_line(nk_command_buffer b, float x0, float y0, float x1, float y1,
			float line_thickness, nk_color c)
		{
			nk_command_line cmd;
			if ((b == null) || (line_thickness <= 0)) return;
			cmd = (nk_command_line) (nk_command_buffer_push(b, (int) (NK_COMMAND_LINE)));
			if (cmd == null) return;
			cmd.line_thickness = ((ushort) (line_thickness));
			cmd.begin.x = ((short) (x0));
			cmd.begin.y = ((short) (y0));
			cmd.end.x = ((short) (x1));
			cmd.end.y = ((short) (y1));
			cmd.color = (nk_color) (c);
		}

		public static void nk_stroke_curve(nk_command_buffer b, float ax, float ay, float ctrl0x, float ctrl0y,
			float ctrl1x, float ctrl1y, float bx, float by, float line_thickness, nk_color col)
		{
			nk_command_curve cmd;
			if (((b == null) || ((col.a) == (0))) || (line_thickness <= 0)) return;
			cmd = (nk_command_curve) (nk_command_buffer_push(b, (int) (NK_COMMAND_CURVE)));
			if (cmd == null) return;
			cmd.line_thickness = ((ushort) (line_thickness));
			cmd.begin.x = ((short) (ax));
			cmd.begin.y = ((short) (ay));
			cmd.ctrl_0.x = ((short) (ctrl0x));
			cmd.ctrl_0.y = ((short) (ctrl0y));
			cmd.ctrl_1.x = ((short) (ctrl1x));
			cmd.ctrl_1.y = ((short) (ctrl1y));
			cmd.end.x = ((short) (bx));
			cmd.end.y = ((short) (by));
			cmd.color = (nk_color) (col);
		}

		public static void nk_stroke_rect(nk_command_buffer b, nk_rect rect, float rounding, float line_thickness,
			nk_color c)
		{
			nk_command_rect cmd;
			if (((((b == null) || ((c.a) == (0))) || ((rect.w) == (0))) || ((rect.h) == (0))) || (line_thickness <= 0))
				return;
			if ((b.use_clipping) != 0)
			{
				if (
					!(!(((((b.clip.x) > (rect.x + rect.w)) || ((b.clip.x + b.clip.w) < (rect.x))) ||
					     ((b.clip.y) > (rect.y + rect.h))) || ((b.clip.y + b.clip.h) < (rect.y))))) return;
			}

			cmd = (nk_command_rect) (nk_command_buffer_push(b, (int) (NK_COMMAND_RECT)));
			if (cmd == null) return;
			cmd.rounding = ((ushort) (rounding));
			cmd.line_thickness = ((ushort) (line_thickness));
			cmd.x = ((short) (rect.x));
			cmd.y = ((short) (rect.y));
			cmd.w = ((ushort) ((0) < (rect.w) ? (rect.w) : (0)));
			cmd.h = ((ushort) ((0) < (rect.h) ? (rect.h) : (0)));
			cmd.color = (nk_color) (c);
		}

		public static void nk_fill_rect(nk_command_buffer b, nk_rect rect, float rounding, nk_color c)
		{
			nk_command_rect_filled cmd;
			if ((((b == null) || ((c.a) == (0))) || ((rect.w) == (0))) || ((rect.h) == (0))) return;
			if ((b.use_clipping) != 0)
			{
				if (
					!(!(((((b.clip.x) > (rect.x + rect.w)) || ((b.clip.x + b.clip.w) < (rect.x))) ||
					     ((b.clip.y) > (rect.y + rect.h))) || ((b.clip.y + b.clip.h) < (rect.y))))) return;
			}

			cmd = (nk_command_rect_filled) (nk_command_buffer_push(b, (int) (NK_COMMAND_RECT_FILLED)));
			if (cmd == null) return;
			cmd.rounding = ((ushort) (rounding));
			cmd.x = ((short) (rect.x));
			cmd.y = ((short) (rect.y));
			cmd.w = ((ushort) ((0) < (rect.w) ? (rect.w) : (0)));
			cmd.h = ((ushort) ((0) < (rect.h) ? (rect.h) : (0)));
			cmd.color = (nk_color) (c);
		}

		public static void nk_fill_rect_multi_color(nk_command_buffer b, nk_rect rect, nk_color left, nk_color top,
			nk_color right, nk_color bottom)
		{
			nk_command_rect_multi_color cmd;
			if (((b == null) || ((rect.w) == (0))) || ((rect.h) == (0))) return;
			if ((b.use_clipping) != 0)
			{
				if (
					!(!(((((b.clip.x) > (rect.x + rect.w)) || ((b.clip.x + b.clip.w) < (rect.x))) ||
					     ((b.clip.y) > (rect.y + rect.h))) || ((b.clip.y + b.clip.h) < (rect.y))))) return;
			}

			cmd = (nk_command_rect_multi_color) (nk_command_buffer_push(b, (int) (NK_COMMAND_RECT_MULTI_COLOR)));
			if (cmd == null) return;
			cmd.x = ((short) (rect.x));
			cmd.y = ((short) (rect.y));
			cmd.w = ((ushort) ((0) < (rect.w) ? (rect.w) : (0)));
			cmd.h = ((ushort) ((0) < (rect.h) ? (rect.h) : (0)));
			cmd.left = (nk_color) (left);
			cmd.top = (nk_color) (top);
			cmd.right = (nk_color) (right);
			cmd.bottom = (nk_color) (bottom);
		}

		public static void nk_stroke_circle(nk_command_buffer b, nk_rect r, float line_thickness, nk_color c)
		{
			nk_command_circle cmd;
			if ((((b == null) || ((r.w) == (0))) || ((r.h) == (0))) || (line_thickness <= 0)) return;
			if ((b.use_clipping) != 0)
			{
				if (
					!(!(((((b.clip.x) > (r.x + r.w)) || ((b.clip.x + b.clip.w) < (r.x))) || ((b.clip.y) > (r.y + r.h))) ||
					    ((b.clip.y + b.clip.h) < (r.y))))) return;
			}

			cmd = (nk_command_circle) (nk_command_buffer_push(b, (int) (NK_COMMAND_CIRCLE)));
			if (cmd == null) return;
			cmd.line_thickness = ((ushort) (line_thickness));
			cmd.x = ((short) (r.x));
			cmd.y = ((short) (r.y));
			cmd.w = ((ushort) ((r.w) < (0) ? (0) : (r.w)));
			cmd.h = ((ushort) ((r.h) < (0) ? (0) : (r.h)));
			cmd.color = (nk_color) (c);
		}

		public static void nk_fill_circle(nk_command_buffer b, nk_rect r, nk_color c)
		{
			nk_command_circle_filled cmd;
			if ((((b == null) || ((c.a) == (0))) || ((r.w) == (0))) || ((r.h) == (0))) return;
			if ((b.use_clipping) != 0)
			{
				if (
					!(!(((((b.clip.x) > (r.x + r.w)) || ((b.clip.x + b.clip.w) < (r.x))) || ((b.clip.y) > (r.y + r.h))) ||
					    ((b.clip.y + b.clip.h) < (r.y))))) return;
			}

			cmd = (nk_command_circle_filled) (nk_command_buffer_push(b, (int) (NK_COMMAND_CIRCLE_FILLED)));
			if (cmd == null) return;
			cmd.x = ((short) (r.x));
			cmd.y = ((short) (r.y));
			cmd.w = ((ushort) ((r.w) < (0) ? (0) : (r.w)));
			cmd.h = ((ushort) ((r.h) < (0) ? (0) : (r.h)));
			cmd.color = (nk_color) (c);
		}

		public static void nk_stroke_arc(nk_command_buffer b, float cx, float cy, float radius, float a_min, float a_max,
			float line_thickness, nk_color c)
		{
			nk_command_arc cmd;
			if (((b == null) || ((c.a) == (0))) || (line_thickness <= 0)) return;
			cmd = (nk_command_arc) (nk_command_buffer_push(b, (int) (NK_COMMAND_ARC)));
			if (cmd == null) return;
			cmd.line_thickness = ((ushort) (line_thickness));
			cmd.cx = ((short) (cx));
			cmd.cy = ((short) (cy));
			cmd.r = ((ushort) (radius));
			cmd.a[0] = (float) (a_min);
			cmd.a[1] = (float) (a_max);
			cmd.color = (nk_color) (c);
		}

		public static void nk_fill_arc(nk_command_buffer b, float cx, float cy, float radius, float a_min, float a_max,
			nk_color c)
		{
			nk_command_arc_filled cmd;
			if ((b == null) || ((c.a) == (0))) return;
			cmd = (nk_command_arc_filled) (nk_command_buffer_push(b, (int) (NK_COMMAND_ARC_FILLED)));
			if (cmd == null) return;
			cmd.cx = ((short) (cx));
			cmd.cy = ((short) (cy));
			cmd.r = ((ushort) (radius));
			cmd.a[0] = (float) (a_min);
			cmd.a[1] = (float) (a_max);
			cmd.color = (nk_color) (c);
		}

		public static void nk_stroke_triangle(nk_command_buffer b, float x0, float y0, float x1, float y1, float x2,
			float y2, float line_thickness, nk_color c)
		{
			nk_command_triangle cmd;
			if (((b == null) || ((c.a) == (0))) || (line_thickness <= 0)) return;
			if ((b.use_clipping) != 0)
			{
				if (
					((!((((b.clip.x) <= (x0)) && ((x0) < (b.clip.x + b.clip.w))) &&
					    (((b.clip.y) <= (y0)) && ((y0) < (b.clip.y + b.clip.h))))) &&
					 (!((((b.clip.x) <= (x1)) && ((x1) < (b.clip.x + b.clip.w))) &&
					    (((b.clip.y) <= (y1)) && ((y1) < (b.clip.y + b.clip.h)))))) &&
					(!((((b.clip.x) <= (x2)) && ((x2) < (b.clip.x + b.clip.w))) &&
					   (((b.clip.y) <= (y2)) && ((y2) < (b.clip.y + b.clip.h)))))) return;
			}

			cmd = (nk_command_triangle) (nk_command_buffer_push(b, (int) (NK_COMMAND_TRIANGLE)));
			if (cmd == null) return;
			cmd.line_thickness = ((ushort) (line_thickness));
			cmd.a.x = ((short) (x0));
			cmd.a.y = ((short) (y0));
			cmd.b.x = ((short) (x1));
			cmd.b.y = ((short) (y1));
			cmd.c.x = ((short) (x2));
			cmd.c.y = ((short) (y2));
			cmd.color = (nk_color) (c);
		}

		public static void nk_fill_triangle(nk_command_buffer b, float x0, float y0, float x1, float y1, float x2,
			float y2, nk_color c)
		{
			nk_command_triangle_filled cmd;
			if ((b == null) || ((c.a) == (0))) return;
			if (b == null) return;
			if ((b.use_clipping) != 0)
			{
				if (
					((!((((b.clip.x) <= (x0)) && ((x0) < (b.clip.x + b.clip.w))) &&
					    (((b.clip.y) <= (y0)) && ((y0) < (b.clip.y + b.clip.h))))) &&
					 (!((((b.clip.x) <= (x1)) && ((x1) < (b.clip.x + b.clip.w))) &&
					    (((b.clip.y) <= (y1)) && ((y1) < (b.clip.y + b.clip.h)))))) &&
					(!((((b.clip.x) <= (x2)) && ((x2) < (b.clip.x + b.clip.w))) &&
					   (((b.clip.y) <= (y2)) && ((y2) < (b.clip.y + b.clip.h)))))) return;
			}

			cmd = (nk_command_triangle_filled) (nk_command_buffer_push(b, (int) (NK_COMMAND_TRIANGLE_FILLED)));
			if (cmd == null) return;
			cmd.a.x = ((short) (x0));
			cmd.a.y = ((short) (y0));
			cmd.b.x = ((short) (x1));
			cmd.b.y = ((short) (y1));
			cmd.c.x = ((short) (x2));
			cmd.c.y = ((short) (y2));
			cmd.color = (nk_color) (c);
		}

		public static void nk_draw_image(nk_command_buffer b, nk_rect r, nk_image img, nk_color col)
		{
			nk_command_image cmd;
			if (b == null) return;
			if ((b.use_clipping) != 0)
			{
				if ((((b.clip.w) == (0)) || ((b.clip.h) == (0))) ||
				    (!(!(((((b.clip.x) > (r.x + r.w)) || ((b.clip.x + b.clip.w) < (r.x))) || ((b.clip.y) > (r.y + r.h))) ||
				         ((b.clip.y + b.clip.h) < (r.y)))))) return;
			}

			cmd = (nk_command_image) (nk_command_buffer_push(b, (int) (NK_COMMAND_IMAGE)));
			if (cmd == null) return;
			cmd.x = ((short) (r.x));
			cmd.y = ((short) (r.y));
			cmd.w = ((ushort) ((0) < (r.w) ? (r.w) : (0)));
			cmd.h = ((ushort) ((0) < (r.h) ? (r.h) : (0)));
			cmd.img = (nk_image) (img);
			cmd.col = (nk_color) (col);
		}

		public static void nk_push_custom(nk_command_buffer b, nk_rect r, NkCommandCustomCallback cb, nk_handle usr)
		{
			nk_command_custom cmd;
			if (b == null) return;
			if ((b.use_clipping) != 0)
			{
				if ((((b.clip.w) == (0)) || ((b.clip.h) == (0))) ||
				    (!(!(((((b.clip.x) > (r.x + r.w)) || ((b.clip.x + b.clip.w) < (r.x))) || ((b.clip.y) > (r.y + r.h))) ||
				         ((b.clip.y + b.clip.h) < (r.y)))))) return;
			}

			cmd = (nk_command_custom) (nk_command_buffer_push(b, (int) (NK_COMMAND_CUSTOM)));
			if (cmd == null) return;
			cmd.x = ((short) (r.x));
			cmd.y = ((short) (r.y));
			cmd.w = ((ushort) ((0) < (r.w) ? (r.w) : (0)));
			cmd.h = ((ushort) ((0) < (r.h) ? (r.h) : (0)));
			cmd.callback_data = (nk_handle) (usr);
			cmd.callback = cb;
		}

		public static void nk_draw_text(nk_command_buffer b, nk_rect r, char* _string_, int length, nk_user_font font,
			nk_color bg, nk_color fg)
		{
			float text_width = (float) (0);
			nk_command_text cmd;
			if ((((b == null) || (_string_ == null)) || (length == 0)) || (((bg.a) == (0)) && ((fg.a) == (0)))) return;
			if ((b.use_clipping) != 0)
			{
				if ((((b.clip.w) == (0)) || ((b.clip.h) == (0))) ||
				    (!(!(((((b.clip.x) > (r.x + r.w)) || ((b.clip.x + b.clip.w) < (r.x))) || ((b.clip.y) > (r.y + r.h))) ||
				         ((b.clip.y + b.clip.h) < (r.y)))))) return;
			}

			text_width =
				(float) (font.width((nk_handle) (font.userdata), (float) (font.height), _string_, (int) (length)));
			if ((text_width) > (r.w))
			{
				int glyphs = (int) (0);
				float txt_width = (float) (text_width);
				length =
					(int)
						(nk_text_clamp(font, _string_, (int) (length), (float) (r.w), &glyphs, &txt_width, null,
							(int) (0)));
			}

			if (length == 0) return;
			cmd = (nk_command_text) (nk_command_buffer_push(b, (int) (NK_COMMAND_TEXT)));
			if (cmd == null) return;
			cmd.x = ((short) (r.x));
			cmd.y = ((short) (r.y));
			cmd.w = ((ushort) (r.w));
			cmd.h = ((ushort) (r.h));
			cmd.background = (nk_color) (bg);
			cmd.foreground = (nk_color) (fg);
			cmd.font = font;
			cmd.length = (int) (length);
			cmd.height = (float) (font.height);
			cmd._string_ = new PinnedArray<char>(length);
			CRuntime.memcpy((void*) cmd._string_, _string_, length*sizeof (char));
			cmd._string_[length] = ('\0');
		}

		public static void nk_widget_text(nk_command_buffer o, nk_rect b, char* _string_, int len, nk_text* t, uint a,
			nk_user_font f)
		{
			nk_rect label = new nk_rect();
			float text_width;
			if ((o == null) || (t == null)) return;
			b.h = (float) ((b.h) < (2*t->padding.y) ? (2*t->padding.y) : (b.h));
			label.x = (float) (0);
			label.w = (float) (0);
			label.y = (float) (b.y + t->padding.y);
			label.h = (float) ((f.height) < (b.h - 2*t->padding.y) ? (f.height) : (b.h - 2*t->padding.y));
			text_width = (float) (f.width((nk_handle) (f.userdata), (float) (f.height), _string_, (int) (len)));
			text_width += (float) (2.0f*t->padding.x);
			if ((a & NK_TEXT_ALIGN_LEFT) != 0)
			{
				label.x = (float) (b.x + t->padding.x);
				label.w = (float) ((0) < (b.w - 2*t->padding.x) ? (b.w - 2*t->padding.x) : (0));
			}
			else if ((a & NK_TEXT_ALIGN_CENTERED) != 0)
			{
				label.w = (float) ((1) < (2*t->padding.x + text_width) ? (2*t->padding.x + text_width) : (1));
				label.x = (float) (b.x + t->padding.x + ((b.w - 2*t->padding.x) - label.w)/2);
				label.x = (float) ((b.x + t->padding.x) < (label.x) ? (label.x) : (b.x + t->padding.x));
				label.w = (float) ((b.x + b.w) < (label.x + label.w) ? (b.x + b.w) : (label.x + label.w));
				if ((label.w) >= (label.x)) label.w -= (float) (label.x);
			}
			else if ((a & NK_TEXT_ALIGN_RIGHT) != 0)
			{
				label.x =
					(float)
						((b.x + t->padding.x) < ((b.x + b.w) - (2*t->padding.x + text_width))
							? ((b.x + b.w) - (2*t->padding.x + text_width))
							: (b.x + t->padding.x));
				label.w = (float) (text_width + 2*t->padding.x);
			}
			else return;
			if ((a & NK_TEXT_ALIGN_MIDDLE) != 0)
			{
				label.y = (float) (b.y + b.h/2.0f - f.height/2.0f);
				label.h =
					(float)
						((b.h/2.0f) < (b.h - (b.h/2.0f + f.height/2.0f))
							? (b.h - (b.h/2.0f + f.height/2.0f))
							: (b.h/2.0f));
			}
			else if ((a & NK_TEXT_ALIGN_BOTTOM) != 0)
			{
				label.y = (float) (b.y + b.h - f.height);
				label.h = (float) (f.height);
			}

			nk_draw_text(o, (nk_rect) (label), _string_, (int) (len), f, (nk_color) (t->background),
				(nk_color) (t->text));
		}

		public static void nk_widget_text_wrap(nk_command_buffer o, nk_rect b, char* _string_, int len, nk_text* t,
			nk_user_font f)
		{
			float width;
			int glyphs = (int) (0);
			int fitting = (int) (0);
			int done = (int) (0);
			nk_rect line = new nk_rect();
			nk_text text = new nk_text();
			uint* seperator = stackalloc uint[1];
			seperator[0] = (uint) (' ');

			if ((o == null) || (t == null)) return;
			text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			text.background = (nk_color) (t->background);
			text.text = (nk_color) (t->text);
			b.w = (float) ((b.w) < (2*t->padding.x) ? (2*t->padding.x) : (b.w));
			b.h = (float) ((b.h) < (2*t->padding.y) ? (2*t->padding.y) : (b.h));
			b.h = (float) (b.h - 2*t->padding.y);
			line.x = (float) (b.x + t->padding.x);
			line.y = (float) (b.y + t->padding.y);
			line.w = (float) (b.w - 2*t->padding.x);
			line.h = (float) (2*t->padding.y + f.height);
			fitting = (int) (nk_text_clamp(f, _string_, (int) (len), (float) (line.w), &glyphs, &width, seperator, 1));
			while ((done) < (len))
			{
				if ((fitting == 0) || ((line.y + line.h) >= (b.y + b.h))) break;
				nk_widget_text(o, (nk_rect) (line), &_string_[done], (int) (fitting), &text, (uint) (NK_TEXT_LEFT), f);
				done += (int) (fitting);
				line.y += (float) (f.height + 2*t->padding.y);
				fitting =
					(int)
						(nk_text_clamp(f, &_string_[done], (int) (len - done), (float) (line.w), &glyphs, &width,
							seperator, 1));
			}
		}

		public static void nk_draw_symbol(nk_command_buffer _out_, int type, nk_rect content, nk_color background,
			nk_color foreground, float border_width, nk_user_font font)
		{
			switch (type)
			{
				case NK_SYMBOL_X:
				case NK_SYMBOL_UNDERSCORE:
				case NK_SYMBOL_PLUS:
				case NK_SYMBOL_MINUS:
				{
					char X = ((type) == (NK_SYMBOL_X))
						? 'x'
						: ((type) == (NK_SYMBOL_UNDERSCORE)) ? '_' : ((type) == (NK_SYMBOL_PLUS)) ? '+' : '-';
					nk_text text = new nk_text();
					text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
					text.background = (nk_color) (background);
					text.text = (nk_color) (foreground);
					nk_widget_text(_out_, (nk_rect) (content), &X, (int) (1), &text, (uint) (NK_TEXT_CENTERED), font);
				}
					break;
				case NK_SYMBOL_CIRCLE_SOLID:
				case NK_SYMBOL_CIRCLE_OUTLINE:
				case NK_SYMBOL_RECT_SOLID:
				case NK_SYMBOL_RECT_OUTLINE:
				{
					if (((type) == (NK_SYMBOL_RECT_SOLID)) || ((type) == (NK_SYMBOL_RECT_OUTLINE)))
					{
						nk_fill_rect(_out_, (nk_rect) (content), (float) (0), (nk_color) (foreground));
						if ((type) == (NK_SYMBOL_RECT_OUTLINE))
							nk_fill_rect(_out_, (nk_rect) (nk_shrink_rect_((nk_rect) (content), (float) (border_width))),
								(float) (0), (nk_color) (background));
					}
					else
					{
						nk_fill_circle(_out_, (nk_rect) (content), (nk_color) (foreground));
						if ((type) == (NK_SYMBOL_CIRCLE_OUTLINE))
							nk_fill_circle(_out_, (nk_rect) (nk_shrink_rect_((nk_rect) (content), (float) (1))),
								(nk_color) (background));
					}
				}
					break;
				case NK_SYMBOL_TRIANGLE_UP:
				case NK_SYMBOL_TRIANGLE_DOWN:
				case NK_SYMBOL_TRIANGLE_LEFT:
				case NK_SYMBOL_TRIANGLE_RIGHT:
				{
					int heading;
					nk_vec2* points = stackalloc nk_vec2[3];
					heading =
						(int)
							(((type) == (NK_SYMBOL_TRIANGLE_RIGHT))
								? NK_RIGHT
								: ((type) == (NK_SYMBOL_TRIANGLE_LEFT))
									? NK_LEFT
									: ((type) == (NK_SYMBOL_TRIANGLE_UP)) ? NK_UP : NK_DOWN);
					nk_triangle_from_direction(points, (nk_rect) (content), (float) (0), (float) (0), (int) (heading));
					nk_fill_triangle(_out_, (float) (points[0].x), (float) (points[0].y), (float) (points[1].x),
						(float) (points[1].y), (float) (points[2].x), (float) (points[2].y), (nk_color) (foreground));
				}
					break;
				default:
				case NK_SYMBOL_NONE:
				case NK_SYMBOL_MAX:
					break;
			}

		}

		public static nk_style_item nk_draw_button(nk_command_buffer _out_, nk_rect* bounds, uint state,
			nk_style_button style)
		{
			nk_style_item background;
			if ((state & NK_WIDGET_STATE_HOVER) != 0) background = style.hover;
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0) background = style.active;
			else background = style.normal;
			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(_out_, (nk_rect) (*bounds), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				nk_fill_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (float) (style.border),
					(nk_color) (style.border_color));
			}

			return background;
		}

		public static void nk_draw_button_text(nk_command_buffer _out_, nk_rect* bounds, nk_rect* content, uint state,
			nk_style_button style, char* txt, int len, uint text_alignment, nk_user_font font)
		{
			nk_text text = new nk_text();
			nk_style_item background;
			background = nk_draw_button(_out_, bounds, (uint) (state), style);
			if ((background.type) == (NK_STYLE_ITEM_COLOR)) text.background = (nk_color) (background.data.color);
			else text.background = (nk_color) (style.text_background);
			if ((state & NK_WIDGET_STATE_HOVER) != 0) text.text = (nk_color) (style.text_hover);
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0) text.text = (nk_color) (style.text_active);
			else text.text = (nk_color) (style.text_normal);
			text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			nk_widget_text(_out_, (nk_rect) (*content), txt, (int) (len), &text, (uint) (text_alignment), font);
		}

		public static void nk_draw_button_symbol(nk_command_buffer _out_, nk_rect* bounds, nk_rect* content, uint state,
			nk_style_button style, int type, nk_user_font font)
		{
			nk_color sym = new nk_color();
			nk_color bg = new nk_color();
			nk_style_item background;
			background = nk_draw_button(_out_, bounds, (uint) (state), style);
			if ((background.type) == (NK_STYLE_ITEM_COLOR)) bg = (nk_color) (background.data.color);
			else bg = (nk_color) (style.text_background);
			if ((state & NK_WIDGET_STATE_HOVER) != 0) sym = (nk_color) (style.text_hover);
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0) sym = (nk_color) (style.text_active);
			else sym = (nk_color) (style.text_normal);
			nk_draw_symbol(_out_, (int) (type), (nk_rect) (*content), (nk_color) (bg), (nk_color) (sym), (float) (1),
				font);
		}

		public static void nk_draw_button_image(nk_command_buffer _out_, nk_rect* bounds, nk_rect* content, uint state,
			nk_style_button style, nk_image img)
		{
			nk_draw_button(_out_, bounds, (uint) (state), style);
			nk_draw_image(_out_, (nk_rect) (*content), img, (nk_color) (nk_white));
		}

		public static void nk_draw_button_text_symbol(nk_command_buffer _out_, nk_rect* bounds, nk_rect* label,
			nk_rect* symbol, uint state, nk_style_button style, char* str, int len, int type, nk_user_font font)
		{
			nk_color sym = new nk_color();
			nk_text text = new nk_text();
			nk_style_item background;
			background = nk_draw_button(_out_, bounds, (uint) (state), style);
			if ((background.type) == (NK_STYLE_ITEM_COLOR)) text.background = (nk_color) (background.data.color);
			else text.background = (nk_color) (style.text_background);
			if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				sym = (nk_color) (style.text_hover);
				text.text = (nk_color) (style.text_hover);
			}
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				sym = (nk_color) (style.text_active);
				text.text = (nk_color) (style.text_active);
			}
			else
			{
				sym = (nk_color) (style.text_normal);
				text.text = (nk_color) (style.text_normal);
			}

			text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			nk_draw_symbol(_out_, (int) (type), (nk_rect) (*symbol), (nk_color) (style.text_background),
				(nk_color) (sym), (float) (0), font);
			nk_widget_text(_out_, (nk_rect) (*label), str, (int) (len), &text, (uint) (NK_TEXT_CENTERED), font);
		}

		public static void nk_draw_button_text_image(nk_command_buffer _out_, nk_rect* bounds, nk_rect* label,
			nk_rect* image, uint state, nk_style_button style, char* str, int len, nk_user_font font, nk_image img)
		{
			nk_text text = new nk_text();
			nk_style_item background;
			background = nk_draw_button(_out_, bounds, (uint) (state), style);
			if ((background.type) == (NK_STYLE_ITEM_COLOR)) text.background = (nk_color) (background.data.color);
			else text.background = (nk_color) (style.text_background);
			if ((state & NK_WIDGET_STATE_HOVER) != 0) text.text = (nk_color) (style.text_hover);
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0) text.text = (nk_color) (style.text_active);
			else text.text = (nk_color) (style.text_normal);
			text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			nk_widget_text(_out_, (nk_rect) (*label), str, (int) (len), &text, (uint) (NK_TEXT_CENTERED), font);
			nk_draw_image(_out_, (nk_rect) (*image), img, (nk_color) (nk_white));
		}

		public static void nk_draw_checkbox(nk_command_buffer _out_, uint state, nk_style_toggle style, int active,
			nk_rect* label, nk_rect* selector, nk_rect* cursors, char* _string_, int len, nk_user_font font)
		{
			nk_style_item background;
			nk_style_item cursor;
			nk_text text = new nk_text();
			if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.hover;
				cursor = style.cursor_hover;
				text.text = (nk_color) (style.text_hover);
			}
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.hover;
				cursor = style.cursor_hover;
				text.text = (nk_color) (style.text_active);
			}
			else
			{
				background = style.normal;
				cursor = style.cursor_normal;
				text.text = (nk_color) (style.text_normal);
			}

			if ((background.type) == (NK_STYLE_ITEM_COLOR))
			{
				nk_fill_rect(_out_, (nk_rect) (*selector), (float) (0), (nk_color) (style.border_color));
				nk_fill_rect(_out_, (nk_rect) (nk_shrink_rect_((nk_rect) (*selector), (float) (style.border))),
					(float) (0), (nk_color) (background.data.color));
			}
			else nk_draw_image(_out_, (nk_rect) (*selector), background.data.image, (nk_color) (nk_white));
			if ((active) != 0)
			{
				if ((cursor.type) == (NK_STYLE_ITEM_IMAGE))
					nk_draw_image(_out_, (nk_rect) (*cursors), cursor.data.image, (nk_color) (nk_white));
				else nk_fill_rect(_out_, (nk_rect) (*cursors), (float) (0), (nk_color) (cursor.data.color));
			}

			text.padding.x = (float) (0);
			text.padding.y = (float) (0);
			text.background = (nk_color) (style.text_background);
			nk_widget_text(_out_, (nk_rect) (*label), _string_, (int) (len), &text, (uint) (NK_TEXT_LEFT), font);
		}

		public static void nk_draw_option(nk_command_buffer _out_, uint state, nk_style_toggle style, int active,
			nk_rect* label, nk_rect* selector, nk_rect* cursors, char* _string_, int len, nk_user_font font)
		{
			nk_style_item background;
			nk_style_item cursor;
			nk_text text = new nk_text();
			if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.hover;
				cursor = style.cursor_hover;
				text.text = (nk_color) (style.text_hover);
			}
			else if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.hover;
				cursor = style.cursor_hover;
				text.text = (nk_color) (style.text_active);
			}
			else
			{
				background = style.normal;
				cursor = style.cursor_normal;
				text.text = (nk_color) (style.text_normal);
			}

			if ((background.type) == (NK_STYLE_ITEM_COLOR))
			{
				nk_fill_circle(_out_, (nk_rect) (*selector), (nk_color) (style.border_color));
				nk_fill_circle(_out_, (nk_rect) (nk_shrink_rect_((nk_rect) (*selector), (float) (style.border))),
					(nk_color) (background.data.color));
			}
			else nk_draw_image(_out_, (nk_rect) (*selector), background.data.image, (nk_color) (nk_white));
			if ((active) != 0)
			{
				if ((cursor.type) == (NK_STYLE_ITEM_IMAGE))
					nk_draw_image(_out_, (nk_rect) (*cursors), cursor.data.image, (nk_color) (nk_white));
				else nk_fill_circle(_out_, (nk_rect) (*cursors), (nk_color) (cursor.data.color));
			}

			text.padding.x = (float) (0);
			text.padding.y = (float) (0);
			text.background = (nk_color) (style.text_background);
			nk_widget_text(_out_, (nk_rect) (*label), _string_, (int) (len), &text, (uint) (NK_TEXT_LEFT), font);
		}

		public static void nk_draw_selectable(nk_command_buffer _out_, uint state, nk_style_selectable style, int active,
			nk_rect* bounds, nk_rect* icon, nk_image img, char* _string_, int len, uint align, nk_user_font font)
		{
			nk_style_item background;
			nk_text text = new nk_text();
			text.padding = (nk_vec2) (style.padding);
			if (active == 0)
			{
				if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
				{
					background = style.pressed;
					text.text = (nk_color) (style.text_pressed);
				}
				else if ((state & NK_WIDGET_STATE_HOVER) != 0)
				{
					background = style.hover;
					text.text = (nk_color) (style.text_hover);
				}
				else
				{
					background = style.normal;
					text.text = (nk_color) (style.text_normal);
				}
			}
			else
			{
				if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
				{
					background = style.pressed_active;
					text.text = (nk_color) (style.text_pressed_active);
				}
				else if ((state & NK_WIDGET_STATE_HOVER) != 0)
				{
					background = style.hover_active;
					text.text = (nk_color) (style.text_hover_active);
				}
				else
				{
					background = style.normal_active;
					text.text = (nk_color) (style.text_normal_active);
				}
			}

			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(_out_, (nk_rect) (*bounds), background.data.image, (nk_color) (nk_white));
				text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			}
			else
			{
				nk_fill_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (nk_color) (background.data.color));
				text.background = (nk_color) (background.data.color);
			}

			if (((img) != null) && ((icon) != null))
				nk_draw_image(_out_, (nk_rect) (*icon), img, (nk_color) (nk_white));
			nk_widget_text(_out_, (nk_rect) (*bounds), _string_, (int) (len), &text, (uint) (align), font);
		}

		public static void nk_draw_slider(nk_command_buffer _out_, uint state, nk_style_slider style, nk_rect* bounds,
			nk_rect* visual_cursor, float min, float value, float max)
		{
			nk_rect fill = new nk_rect();
			nk_rect bar = new nk_rect();
			nk_style_item background;
			nk_color bar_color = new nk_color();
			nk_style_item cursor;
			if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.active;
				bar_color = (nk_color) (style.bar_active);
				cursor = style.cursor_active;
			}
			else if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.hover;
				bar_color = (nk_color) (style.bar_hover);
				cursor = style.cursor_hover;
			}
			else
			{
				background = style.normal;
				bar_color = (nk_color) (style.bar_normal);
				cursor = style.cursor_normal;
			}

			bar.x = (float) (bounds->x);
			bar.y = (float) ((visual_cursor->y + visual_cursor->h/2) - bounds->h/12);
			bar.w = (float) (bounds->w);
			bar.h = (float) (bounds->h/6);
			fill.w = (float) ((visual_cursor->x + (visual_cursor->w/2.0f)) - bar.x);
			fill.x = (float) (bar.x);
			fill.y = (float) (bar.y);
			fill.h = (float) (bar.h);
			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(_out_, (nk_rect) (*bounds), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				nk_fill_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (float) (style.border),
					(nk_color) (style.border_color));
			}

			nk_fill_rect(_out_, (nk_rect) (bar), (float) (style.rounding), (nk_color) (bar_color));
			nk_fill_rect(_out_, (nk_rect) (fill), (float) (style.rounding), (nk_color) (style.bar_filled));
			if ((cursor.type) == (NK_STYLE_ITEM_IMAGE))
				nk_draw_image(_out_, (nk_rect) (*visual_cursor), cursor.data.image, (nk_color) (nk_white));
			else nk_fill_circle(_out_, (nk_rect) (*visual_cursor), (nk_color) (cursor.data.color));
		}

		public static void nk_draw_progress(nk_command_buffer _out_, uint state, nk_style_progress style,
			nk_rect* bounds, nk_rect* scursor, ulong value, ulong max)
		{
			nk_style_item background;
			nk_style_item cursor;
			if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.active;
				cursor = style.cursor_active;
			}
			else if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.hover;
				cursor = style.cursor_hover;
			}
			else
			{
				background = style.normal;
				cursor = style.cursor_normal;
			}

			if ((background.type) == (NK_STYLE_ITEM_COLOR))
			{
				nk_fill_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (float) (style.border),
					(nk_color) (style.border_color));
			}
			else nk_draw_image(_out_, (nk_rect) (*bounds), background.data.image, (nk_color) (nk_white));
			if ((cursor.type) == (NK_STYLE_ITEM_COLOR))
			{
				nk_fill_rect(_out_, (nk_rect) (*scursor), (float) (style.rounding), (nk_color) (cursor.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*scursor), (float) (style.rounding), (float) (style.border),
					(nk_color) (style.border_color));
			}
			else nk_draw_image(_out_, (nk_rect) (*scursor), cursor.data.image, (nk_color) (nk_white));
		}

		public static void nk_draw_scrollbar(nk_command_buffer _out_, uint state, nk_style_scrollbar style,
			nk_rect* bounds, nk_rect* scroll)
		{
			nk_style_item background;
			nk_style_item cursor;
			if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.active;
				cursor = style.cursor_active;
			}
			else if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.hover;
				cursor = style.cursor_hover;
			}
			else
			{
				background = style.normal;
				cursor = style.cursor_normal;
			}

			if ((background.type) == (NK_STYLE_ITEM_COLOR))
			{
				nk_fill_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (float) (style.border),
					(nk_color) (style.border_color));
			}
			else
			{
				nk_draw_image(_out_, (nk_rect) (*bounds), background.data.image, (nk_color) (nk_white));
			}

			if ((background.type) == (NK_STYLE_ITEM_COLOR))
			{
				nk_fill_rect(_out_, (nk_rect) (*scroll), (float) (style.rounding_cursor), (nk_color) (cursor.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*scroll), (float) (style.rounding_cursor),
					(float) (style.border_cursor), (nk_color) (style.cursor_border_color));
			}
			else nk_draw_image(_out_, (nk_rect) (*scroll), cursor.data.image, (nk_color) (nk_white));
		}

		public static void nk_edit_draw_text(nk_command_buffer _out_, nk_style_edit style, float pos_x, float pos_y,
			float x_offset, char* text, int byte_len, float row_height, nk_user_font font, nk_color background,
			nk_color foreground, int is_selected)
		{
			if ((((text == null) || (byte_len == 0)) || (_out_ == null)) || (style == null)) return;
			{
				int glyph_len = (int) (0);
				char unicode = (char) 0;
				int text_len = (int) (0);
				float line_width = (float) (0);
				float glyph_width;
				char* line = text;
				float line_offset = (float) (0);
				int line_count = (int) (0);
				nk_text txt = new nk_text();
				txt.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
				txt.background = (nk_color) (background);
				txt.text = (nk_color) (foreground);
				glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (byte_len - text_len)));
				if (glyph_len == 0) return;
				while (((text_len) < (byte_len)) && ((glyph_len) != 0))
				{
					if ((unicode) == ('\n'))
					{
						nk_rect label = new nk_rect();
						label.y = (float) (pos_y + line_offset);
						label.h = (float) (row_height);
						label.w = (float) (line_width);
						label.x = (float) (pos_x);
						if (line_count == 0) label.x += (float) (x_offset);
						if ((is_selected) != 0)
							nk_fill_rect(_out_, (nk_rect) (label), (float) (0), (nk_color) (background));
						nk_widget_text(_out_, (nk_rect) (label), line, (int) ((text + text_len) - line), &txt,
							(uint) (NK_TEXT_CENTERED), font);
						text_len++;
						line_count++;
						line_width = (float) (0);
						line = text + text_len;
						line_offset += (float) (row_height);
						glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (byte_len - text_len)));
						continue;
					}
					if ((unicode) == ('\r'))
					{
						text_len++;
						glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (byte_len - text_len)));
						continue;
					}
					glyph_width =
						(float)
							(font.width((nk_handle) (font.userdata), (float) (font.height), text + text_len,
								(int) (glyph_len)));
					line_width += (float) (glyph_width);
					text_len += (int) (glyph_len);
					glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (byte_len - text_len)));
					continue;
				}
				if ((line_width) > (0))
				{
					nk_rect label = new nk_rect();
					label.y = (float) (pos_y + line_offset);
					label.h = (float) (row_height);
					label.w = (float) (line_width);
					label.x = (float) (pos_x);
					if (line_count == 0) label.x += (float) (x_offset);
					if ((is_selected) != 0)
						nk_fill_rect(_out_, (nk_rect) (label), (float) (0), (nk_color) (background));
					nk_widget_text(_out_, (nk_rect) (label), line, (int) ((text + text_len) - line), &txt,
						(uint) (NK_TEXT_LEFT), font);
				}
			}

		}

		public static void nk_draw_property(nk_command_buffer _out_, nk_style_property style, nk_rect* bounds,
			nk_rect* label, uint state, char* name, int len, nk_user_font font)
		{
			nk_text text = new nk_text();
			nk_style_item background;
			if ((state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.active;
				text.text = (nk_color) (style.label_active);
			}
			else if ((state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.hover;
				text.text = (nk_color) (style.label_hover);
			}
			else
			{
				background = style.normal;
				text.text = (nk_color) (style.label_normal);
			}

			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(_out_, (nk_rect) (*bounds), background.data.image, (nk_color) (nk_white));
				text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			}
			else
			{
				text.background = (nk_color) (background.data.color);
				nk_fill_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(_out_, (nk_rect) (*bounds), (float) (style.rounding), (float) (style.border),
					(nk_color) (background.data.color));
			}

			text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			nk_widget_text(_out_, (nk_rect) (*label), name, (int) (len), &text, (uint) (NK_TEXT_CENTERED), font);
		}

		public static void nk_draw_color_picker(nk_command_buffer o, nk_rect* matrix, nk_rect* hue_bar,
			nk_rect* alpha_bar, nk_colorf col)
		{
			nk_color black = (nk_color) (nk_black);
			nk_color white = (nk_color) (nk_white);
			nk_color black_trans = new nk_color();
			float crosshair_size = (float) (7.0f);
			nk_color temp = new nk_color();
			float* hsva = stackalloc float[4];
			float line_y;
			int i;
			nk_colorf_hsva_fv(hsva, (nk_colorf) (col));
			for (i = (int) (0); (i) < (6); ++i)
			{
				nk_fill_rect_multi_color(o,
					(nk_rect)
						(nk_rect_((float) (hue_bar->x), (float) (hue_bar->y + (float) (i)*(hue_bar->h/6.0f) + 0.5f),
							(float) (hue_bar->w), (float) ((hue_bar->h/6.0f) + 0.5f))), (nk_color) (hue_colors[i]),
					(nk_color) (hue_colors[i]), (nk_color) (hue_colors[i + 1]), (nk_color) (hue_colors[i + 1]));
			}
			line_y = ((float) ((int) (hue_bar->y + hsva[0]*matrix->h + 0.5f)));
			nk_stroke_line(o, (float) (hue_bar->x - 1), (float) (line_y), (float) (hue_bar->x + hue_bar->w + 2),
				(float) (line_y), (float) (1), (nk_color) (nk_rgb((int) (255), (int) (255), (int) (255))));
			if ((alpha_bar) != null)
			{
				float alpha =
					(float) ((0) < ((1.0f) < (col.a) ? (1.0f) : (col.a)) ? ((1.0f) < (col.a) ? (1.0f) : (col.a)) : (0));
				line_y = ((float) ((int) (alpha_bar->y + (1.0f - alpha)*matrix->h + 0.5f)));
				nk_fill_rect_multi_color(o, (nk_rect) (*alpha_bar), (nk_color) (white), (nk_color) (white),
					(nk_color) (black), (nk_color) (black));
				nk_stroke_line(o, (float) (alpha_bar->x - 1), (float) (line_y),
					(float) (alpha_bar->x + alpha_bar->w + 2), (float) (line_y), (float) (1),
					(nk_color) (nk_rgb((int) (255), (int) (255), (int) (255))));
			}

			temp = (nk_color) (nk_hsv_f((float) (hsva[0]), (float) (1.0f), (float) (1.0f)));
			nk_fill_rect_multi_color(o, (nk_rect) (*matrix), (nk_color) (white), (nk_color) (temp), (nk_color) (temp),
				(nk_color) (white));
			nk_fill_rect_multi_color(o, (nk_rect) (*matrix), (nk_color) (black_trans), (nk_color) (black_trans),
				(nk_color) (black), (nk_color) (black));
			{
				nk_vec2 p = new nk_vec2();
				float S = (float) (hsva[1]);
				float V = (float) (hsva[2]);
				p.x = ((float) ((int) (matrix->x + S*matrix->w)));
				p.y = ((float) ((int) (matrix->y + (1.0f - V)*matrix->h)));
				nk_stroke_line(o, (float) (p.x - crosshair_size), (float) (p.y), (float) (p.x - 2), (float) (p.y),
					(float) (1.0f), (nk_color) (white));
				nk_stroke_line(o, (float) (p.x + crosshair_size + 1), (float) (p.y), (float) (p.x + 3), (float) (p.y),
					(float) (1.0f), (nk_color) (white));
				nk_stroke_line(o, (float) (p.x), (float) (p.y + crosshair_size + 1), (float) (p.x), (float) (p.y + 3),
					(float) (1.0f), (nk_color) (white));
				nk_stroke_line(o, (float) (p.x), (float) (p.y - crosshair_size), (float) (p.x), (float) (p.y - 2),
					(float) (1.0f), (nk_color) (white));
			}

		}

		public static void nk_push_table(nk_window win, nk_table tbl)
		{
			if (win.tables == null)
			{
				win.tables = tbl;
				tbl.next = null;
				tbl.prev = null;
				tbl.size = (uint) (0);
				win.table_count = (uint) (1);
				return;
			}

			win.tables.prev = tbl;
			tbl.next = win.tables;
			tbl.prev = null;
			tbl.size = (uint) (0);
			win.tables = tbl;
			win.table_count++;
		}

		public static void nk_remove_table(nk_window win, nk_table tbl)
		{
			if ((win.tables) == (tbl)) win.tables = tbl.next;
			if ((tbl.next) != null) tbl.next.prev = tbl.prev;
			if ((tbl.prev) != null) tbl.prev.next = tbl.next;
			tbl.next = null;
			tbl.prev = null;
		}

		public static uint* nk_find_value(nk_window win, uint name)
		{
			nk_table iter = win.tables;
			while ((iter) != null)
			{
				uint i = (uint) (0);
				uint size = (uint) (iter.size);
				for (i = (uint) (0); (i) < (size); ++i)
				{
					if ((iter.keys[i]) == (name))
					{
						iter.seq = (uint) (win.seq);
						return (uint*) iter.values + i;
					}
				}
				size = (uint) (51);
				iter = iter.next;
			}
			return null;
		}

	}
}