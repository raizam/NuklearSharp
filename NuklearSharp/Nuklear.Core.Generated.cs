using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_color
		{
			public byte r;
			public byte g;
			public byte b;
			public byte a;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_colorf
		{
			public float r;
			public float g;
			public float b;
			public float a;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_vec2
		{
			public float x;
			public float y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_vec2i
		{
			public short x;
			public short y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_rect
		{
			public float x;
			public float y;
			public float w;
			public float h;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_recti
		{
			public short x;
			public short y;
			public short w;
			public short h;
		}

		public unsafe partial class nk_image
		{
			public nk_handle handle = new nk_handle();
			public ushort w;
			public ushort h;
			public PinnedArray<ushort> region = new PinnedArray<ushort>(4);
		}

		public unsafe partial class nk_cursor
		{
			public nk_image img = new nk_image();
			public nk_vec2 size = new nk_vec2();
			public nk_vec2 offset = new nk_vec2();
		}

		public unsafe partial class nk_list_view
		{
			public int begin;
			public int end;
			public int count;
			public int total_height;
			public nk_context ctx;
			public uint* scroll_pointer;
			public uint scroll_value;
		}

		public unsafe partial class nk_str
		{
			public string str;

			public int len
			{
				get { return str.Length; }
			}
		}

		public unsafe partial class nk_chart_slot
		{
			public int type;
			public nk_color color = new nk_color();
			public nk_color highlight = new nk_color();
			public float min;
			public float max;
			public float range;
			public int count;
			public nk_vec2 last = new nk_vec2();
			public int index;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_text
		{
			public nk_vec2 padding;
			public nk_color background;
			public nk_color text;
		}

		public static nk_rect nk_recta(nk_vec2 pos, nk_vec2 size)
		{
			return (nk_rect) (nk_rect_((float) (pos.x), (float) (pos.y), (float) (size.x), (float) (size.y)));
		}

		public static nk_vec2 nk_rect_pos(nk_rect r)
		{
			nk_vec2 ret = new nk_vec2();
			ret.x = (float) (r.x);
			ret.y = (float) (r.y);
			return (nk_vec2) (ret);
		}

		public static nk_vec2 nk_rect_size(nk_rect r)
		{
			nk_vec2 ret = new nk_vec2();
			ret.x = (float) (r.w);
			ret.y = (float) (r.h);
			return (nk_vec2) (ret);
		}

		public static nk_rect nk_shrink_rect_(nk_rect r, float amount)
		{
			nk_rect res = new nk_rect();
			r.w = (float) ((r.w) < (2*amount) ? (2*amount) : (r.w));
			r.h = (float) ((r.h) < (2*amount) ? (2*amount) : (r.h));
			res.x = (float) (r.x + amount);
			res.y = (float) (r.y + amount);
			res.w = (float) (r.w - 2*amount);
			res.h = (float) (r.h - 2*amount);
			return (nk_rect) (res);
		}

		public static nk_rect nk_pad_rect(nk_rect r, nk_vec2 pad)
		{
			r.w = (float) ((r.w) < (2*pad.x) ? (2*pad.x) : (r.w));
			r.h = (float) ((r.h) < (2*pad.y) ? (2*pad.y) : (r.h));
			r.x += (float) (pad.x);
			r.y += (float) (pad.y);
			r.w -= (float) (2*pad.x);
			r.h -= (float) (2*pad.y);
			return (nk_rect) (r);
		}

		public static nk_color nk_rgba_cf(nk_colorf c)
		{
			return (nk_color) (nk_rgba_f((float) (c.r), (float) (c.g), (float) (c.b), (float) (c.a)));
		}

		public static nk_color nk_rgb_cf(nk_colorf c)
		{
			return (nk_color) (nk_rgb_f((float) (c.r), (float) (c.g), (float) (c.b)));
		}

		public static uint nk_color_u32(nk_color _in_)
		{
			uint _out_ = (uint) (_in_.r);
			_out_ |= (uint) ((uint) (_in_.g) << 8);
			_out_ |= (uint) ((uint) (_in_.b) << 16);
			_out_ |= (uint) ((uint) (_in_.a) << 24);
			return (uint) (_out_);
		}

		public static nk_colorf nk_color_cf(nk_color _in_)
		{
			nk_colorf o = new nk_colorf();
			nk_color_f(&o.r, &o.g, &o.b, &o.a, (nk_color) (_in_));
			return (nk_colorf) (o);
		}

		public static nk_image nk_subimage_handle(nk_handle handle, ushort w, ushort h, nk_rect r)
		{
			nk_image s = new nk_image();

			s.handle = (nk_handle) (handle);
			s.w = (ushort) (w);
			s.h = (ushort) (h);
			s.region[0] = ((ushort) (r.x));
			s.region[1] = ((ushort) (r.y));
			s.region[2] = ((ushort) (r.w));
			s.region[3] = ((ushort) (r.h));
			return (nk_image) (s);
		}

		public static nk_image nk_image_handle(nk_handle handle)
		{
			nk_image s = new nk_image();

			s.handle = (nk_handle) (handle);
			s.w = (ushort) (0);
			s.h = (ushort) (0);
			s.region[0] = (ushort) (0);
			s.region[1] = (ushort) (0);
			s.region[2] = (ushort) (0);
			s.region[3] = (ushort) (0);
			return (nk_image) (s);
		}

		public static int nk_image_is_subimage(nk_image img)
		{
			return (int) ((((img.w) == (0)) && ((img.h) == (0))) ? 1 : 0);
		}

		public static void nk_unify(ref nk_rect clip, ref nk_rect a, float x0, float y0, float x1, float y1)
		{
			clip.x = (float) ((a.x) < (x0) ? (x0) : (a.x));
			clip.y = (float) ((a.y) < (y0) ? (y0) : (a.y));
			clip.w = (float) (((a.x + a.w) < (x1) ? (a.x + a.w) : (x1)) - clip.x);
			clip.h = (float) (((a.y + a.h) < (y1) ? (a.y + a.h) : (y1)) - clip.y);
			clip.w = (float) ((0) < (clip.w) ? (clip.w) : (0));
			clip.h = (float) ((0) < (clip.h) ? (clip.h) : (0));
		}

		public static void nk_triangle_from_direction(nk_vec2* result, nk_rect r, float pad_x, float pad_y, int direction)
		{
			float w_half;
			float h_half;
			r.w = (float) ((2*pad_x) < (r.w) ? (r.w) : (2*pad_x));
			r.h = (float) ((2*pad_y) < (r.h) ? (r.h) : (2*pad_y));
			r.w = (float) (r.w - 2*pad_x);
			r.h = (float) (r.h - 2*pad_y);
			r.x = (float) (r.x + pad_x);
			r.y = (float) (r.y + pad_y);
			w_half = (float) (r.w/2.0f);
			h_half = (float) (r.h/2.0f);
			if ((direction) == (NK_UP))
			{
				result[0] = (nk_vec2) (nk_vec2_((float) (r.x + w_half), (float) (r.y)));
				result[1] = (nk_vec2) (nk_vec2_((float) (r.x + r.w), (float) (r.y + r.h)));
				result[2] = (nk_vec2) (nk_vec2_((float) (r.x), (float) (r.y + r.h)));
			}
			else if ((direction) == (NK_RIGHT))
			{
				result[0] = (nk_vec2) (nk_vec2_((float) (r.x), (float) (r.y)));
				result[1] = (nk_vec2) (nk_vec2_((float) (r.x + r.w), (float) (r.y + h_half)));
				result[2] = (nk_vec2) (nk_vec2_((float) (r.x), (float) (r.y + r.h)));
			}
			else if ((direction) == (NK_DOWN))
			{
				result[0] = (nk_vec2) (nk_vec2_((float) (r.x), (float) (r.y)));
				result[1] = (nk_vec2) (nk_vec2_((float) (r.x + r.w), (float) (r.y)));
				result[2] = (nk_vec2) (nk_vec2_((float) (r.x + w_half), (float) (r.y + r.h)));
			}
			else
			{
				result[0] = (nk_vec2) (nk_vec2_((float) (r.x), (float) (r.y + h_half)));
				result[1] = (nk_vec2) (nk_vec2_((float) (r.x + r.w), (float) (r.y)));
				result[2] = (nk_vec2) (nk_vec2_((float) (r.x + r.w), (float) (r.y + r.h)));
			}

		}

		public static void* nk_malloc(nk_handle unused, void* old, ulong size)
		{
			return CRuntime.malloc((ulong) (size));
		}

		public static void nk_mfree(nk_handle unused, void* ptr)
		{
			CRuntime.free(ptr);
		}

		public static void nk_str_init_fixed(nk_str str, void* memory, ulong size)
		{
			str.str = String.Empty;
		}

		public static int nk_str_append_text_char(nk_str s, char* str, int len)
		{
			if (((s == null) || (str == null)) || (len == 0)) return (int) (0);

			var s2 = new string(str);
			s.str += s2;
			return (int) (len);
		}

		public static int nk_str_append_str_char(nk_str s, char* str)
		{
			return (int) (nk_str_append_text_char(s, str, (int) (nk_strlen(str))));
		}

		public static int nk_str_append_text_utf8(nk_str str, char* text, int len)
		{
			int i = (int) (0);
			int byte_len = (int) (0);
			char unicode;
			if (((str == null) || (text == null)) || (len == 0)) return (int) (0);
			for (i = (int) (0); (i) < (len); ++i)
			{
				byte_len += (int) (nk_utf_decode(text + byte_len, &unicode, (int) (4)));
			}
			nk_str_append_text_char(str, text, (int) (byte_len));
			return (int) (len);
		}

		public static int nk_str_append_str_utf8(nk_str str, char* text)
		{
			int runes = (int) (0);
			int byte_len = (int) (0);
			int num_runes = (int) (0);
			int glyph_len = (int) (0);
			char unicode;
			if ((str == null) || (text == null)) return (int) (0);
			glyph_len = (int) (byte_len = (int) (nk_utf_decode(text + byte_len, &unicode, (int) (4))));
			while ((unicode != '\0') && ((glyph_len) != 0))
			{
				glyph_len = (int) (nk_utf_decode(text + byte_len, &unicode, (int) (4)));
				byte_len += (int) (glyph_len);
				num_runes++;
			}
			nk_str_append_text_char(str, text, (int) (byte_len));
			return (int) (runes);
		}

		public static int nk_str_insert_at_char(nk_str s, int pos, char* str, int len)
		{
			var s2 = new string(str);

			s.str = s.str.Substring(0, pos) + s2 + s.str.Substring(pos);

			return len;
		}

		public static int nk_str_insert_at_rune(nk_str str, int pos, char* cstr, int len)
		{
			return (int) (nk_str_insert_at_char(str, pos, cstr, (int) (len)));
		}

		public static int nk_str_insert_text_char(nk_str str, int pos, char* text, int len)
		{
			return (int) (nk_str_insert_text_utf8(str, (int) (pos), text, (int) (len)));
		}

		public static int nk_str_insert_str_char(nk_str str, int pos, char* text)
		{
			return (int) (nk_str_insert_text_utf8(str, (int) (pos), text, (int) (nk_strlen(text))));
		}

		public static int nk_str_insert_text_utf8(nk_str str, int pos, char* text, int len)
		{
			int i = (int) (0);
			int byte_len = (int) (0);
			char unicode;
			if (((str == null) || (text == null)) || (len == 0)) return (int) (0);
			for (i = (int) (0); (i) < (len); ++i)
			{
				byte_len += (int) (nk_utf_decode(text + byte_len, &unicode, (int) (4)));
			}
			nk_str_insert_at_rune(str, (int) (pos), text, (int) (byte_len));
			return (int) (len);
		}

		public static int nk_str_insert_str_utf8(nk_str str, int pos, char* text)
		{
			int runes = (int) (0);
			int byte_len = (int) (0);
			int num_runes = (int) (0);
			int glyph_len = (int) (0);
			char unicode;
			if ((str == null) || (text == null)) return (int) (0);
			glyph_len = (int) (byte_len = (int) (nk_utf_decode(text + byte_len, &unicode, (int) (4))));
			while ((unicode != '\0') && ((glyph_len) != 0))
			{
				glyph_len = (int) (nk_utf_decode(text + byte_len, &unicode, (int) (4)));
				byte_len += (int) (glyph_len);
				num_runes++;
			}
			nk_str_insert_at_rune(str, (int) (pos), text, (int) (byte_len));
			return (int) (runes);
		}

		public static int nk_str_insert_text_runes(nk_str str, int pos, char* runes, int len)
		{
			int i = (int) (0);
			int byte_len = (int) (0);
			char* glyph = stackalloc char[4];
			if (((str == null) || (runes == null)) || (len == 0)) return (int) (0);
			for (i = (int) (0); (i) < (len); ++i)
			{
				byte_len = (int) (nk_utf_encode(runes[i], glyph, (int) (4)));
				if (byte_len == 0) break;
				nk_str_insert_at_rune(str, (int) (pos + i), glyph, (int) (byte_len));
			}
			return (int) (len);
		}

		public static int nk_str_insert_str_runes(nk_str str, int pos, char* runes)
		{
			int i = (int) (0);
			char* glyph = stackalloc char[4];
			int byte_len;
			if ((str == null) || (runes == null)) return (int) (0);
			while (runes[i] != '\0')
			{
				byte_len = (int) (nk_utf_encode(runes[i], glyph, (int) (4)));
				nk_str_insert_at_rune(str, (int) (pos + i), glyph, (int) (byte_len));
				i++;
			}
			return (int) (i);
		}

		public static void nk_str_remove_chars(nk_str s, int len)
		{
			s.str = s.str.Substring(0, s.str.Length - len);
		}

		public static void nk_str_remove_runes(nk_str str, int len)
		{
			if ((str == null) || ((len) < (0))) return;
			nk_str_remove_chars(str, len);
		}

		public static void nk_str_delete_chars(nk_str s, int pos, int len)
		{
			if ((((s == null) || (len == 0)))) return;

			s.str = s.str.Substring(0, pos) + s.str.Substring(pos + len);
		}

		public static void nk_str_delete_runes(nk_str s, int pos, int len)
		{
			nk_str_delete_chars(s, pos, len);
		}

		public static char nk_str_rune_at(nk_str str, int pos)
		{
			return str.str[pos];
		}

		public static int nk_str_len(nk_str s)
		{
			return s.len;
		}

		public static int nk_str_len_char(nk_str s)
		{
			return s.len;
		}

		public static float nk_font_text_width(nk_font font, float height, char* text, int len)
		{
			char unicode;
			int text_len = (int) (0);
			float text_width = (float) (0);
			int glyph_len = (int) (0);
			float scale = (float) (0);

			if (((font == null) || (text == null)) || (len == 0)) return (float) (0);
			scale = (float) (height/font.info.height);
			glyph_len = (int) (text_len = (int) (nk_utf_decode(text, &unicode, (int) (len))));
			if (glyph_len == 0) return (float) (0);
			while ((text_len <= len) && ((glyph_len) != 0))
			{
				nk_font_glyph* g;
				if ((unicode) == (0xFFFD)) break;
				g = nk_font_find_glyph(font, unicode);
				text_width += (float) (g->xadvance*scale);
				glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (len - text_len)));
				text_len += (int) (glyph_len);
			}
			return (float) (text_width);
		}

		public static void nk_font_query_font_glyph(nk_font font, float height, nk_user_font_glyph* glyph, char codepoint,
			char next_codepoint)
		{
			float scale;
			nk_font_glyph* g;


			if ((font == null) || (glyph == null)) return;
			scale = (float) (height/font.info.height);
			g = nk_font_find_glyph(font, codepoint);
			glyph->width = (float) ((g->x1 - g->x0)*scale);
			glyph->height = (float) ((g->y1 - g->y0)*scale);
			glyph->offset = (nk_vec2) (nk_vec2_((float) (g->x0*scale), (float) (g->y0*scale)));
			glyph->xadvance = (float) (g->xadvance*scale);
			glyph->uv_x[0] = g->u0;
			glyph->uv_y[0] = g->v0;
			glyph->uv_x[1] = g->u1;
			glyph->uv_y[1] = g->v1;
		}

		public static nk_style_item nk_style_item_image(nk_image img)
		{
			nk_style_item i = new nk_style_item();
			i.type = (int) (NK_STYLE_ITEM_IMAGE);
			i.data.image = (nk_image) (img);
			return (nk_style_item) (i);
		}

		public static nk_style_item nk_style_item_color(nk_color col)
		{
			nk_style_item i = new nk_style_item();
			i.type = (int) (NK_STYLE_ITEM_COLOR);
			i.data.color = (nk_color) (col);
			return (nk_style_item) (i);
		}

		public static void nk_layout_widget_space(nk_rect* bounds, nk_context ctx, nk_window win, int modify)
		{
			nk_panel layout;
			nk_style style;
			nk_vec2 spacing = new nk_vec2();
			nk_vec2 padding = new nk_vec2();
			float item_offset = (float) (0);
			float item_width = (float) (0);
			float item_spacing = (float) (0);
			float panel_space = (float) (0);
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			style = ctx.style;
			spacing = (nk_vec2) (style.window.spacing);
			padding = (nk_vec2) (nk_panel_get_padding(style, (int) (layout.type)));
			panel_space =
				(float)
					(nk_layout_row_calculate_usable_space(ctx.style, (int) (layout.type), (float) (layout.bounds.w),
						(int) (layout.row.columns)));
			switch (layout.row.type)
			{
				case NK_LAYOUT_DYNAMIC_FIXED:
				{
					item_width = (float) (((1.0f) < (panel_space - 1.0f) ? (panel_space - 1.0f) : (1.0f))/(float) (layout.row.columns));
					item_offset = (float) ((float) (layout.row.index)*item_width);
					item_spacing = (float) ((float) (layout.row.index)*spacing.x);
				}
					break;
				case NK_LAYOUT_DYNAMIC_ROW:
				{
					item_width = (float) (layout.row.item_width*panel_space);
					item_offset = (float) (layout.row.item_offset);
					item_spacing = (float) (0);
					if ((modify) != 0)
					{
						layout.row.item_offset += (float) (item_width + spacing.x);
						layout.row.filled += (float) (layout.row.item_width);
						layout.row.index = (int) (0);
					}
				}
					break;
				case NK_LAYOUT_DYNAMIC_FREE:
				{
					bounds->x = (float) (layout.at_x + (layout.bounds.w*layout.row.item.x));
					bounds->x -= ((float) (layout.offset.x));
					bounds->y = (float) (layout.at_y + (layout.row.height*layout.row.item.y));
					bounds->y -= ((float) (layout.offset.y));
					bounds->w = (float) (layout.bounds.w*layout.row.item.w);
					bounds->h = (float) (layout.row.height*layout.row.item.h);
					return;
				}
				case NK_LAYOUT_DYNAMIC:
				{
					float ratio;
					ratio =
						(float)
							(((layout.row.ratio[layout.row.index]) < (0)) ? layout.row.item_width : layout.row.ratio[layout.row.index]);
					item_spacing = (float) ((float) (layout.row.index)*spacing.x);
					item_width = (float) (ratio*panel_space);
					item_offset = (float) (layout.row.item_offset);
					if ((modify) != 0)
					{
						layout.row.item_offset += (float) (item_width);
						layout.row.filled += (float) (ratio);
					}
				}
					break;
				case NK_LAYOUT_STATIC_FIXED:
				{
					item_width = (float) (layout.row.item_width);
					item_offset = (float) ((float) (layout.row.index)*item_width);
					item_spacing = (float) ((float) (layout.row.index)*spacing.x);
				}
					break;
				case NK_LAYOUT_STATIC_ROW:
				{
					item_width = (float) (layout.row.item_width);
					item_offset = (float) (layout.row.item_offset);
					item_spacing = (float) ((float) (layout.row.index)*spacing.x);
					if ((modify) != 0) layout.row.item_offset += (float) (item_width);
				}
					break;
				case NK_LAYOUT_STATIC_FREE:
				{
					bounds->x = (float) (layout.at_x + layout.row.item.x);
					bounds->w = (float) (layout.row.item.w);
					if (((bounds->x + bounds->w) > (layout.max_x)) && ((modify) != 0)) layout.max_x = (float) (bounds->x + bounds->w);
					bounds->x -= ((float) (layout.offset.x));
					bounds->y = (float) (layout.at_y + layout.row.item.y);
					bounds->y -= ((float) (layout.offset.y));
					bounds->h = (float) (layout.row.item.h);
					return;
				}
				case NK_LAYOUT_STATIC:
				{
					item_spacing = (float) ((float) (layout.row.index)*spacing.x);
					item_width = (float) (layout.row.ratio[layout.row.index]);
					item_offset = (float) (layout.row.item_offset);
					if ((modify) != 0) layout.row.item_offset += (float) (item_width);
				}
					break;
				case NK_LAYOUT_TEMPLATE:
				{
					item_width = (float) (layout.row.templates[layout.row.index]);
					item_offset = (float) (layout.row.item_offset);
					item_spacing = (float) ((float) (layout.row.index)*spacing.x);
					if ((modify) != 0) layout.row.item_offset += (float) (item_width);
				}
					break;
				default:
					;
					break;
			}

			bounds->w = (float) (item_width);
			bounds->h = (float) (layout.row.height - spacing.y);
			bounds->y = (float) (layout.at_y - (float) (layout.offset.y));
			bounds->x = (float) (layout.at_x + item_offset + item_spacing + padding.x);
			if (((bounds->x + bounds->w) > (layout.max_x)) && ((modify) != 0)) layout.max_x = (float) (bounds->x + bounds->w);
			bounds->x -= ((float) (layout.offset.x));
		}

		public static void nk_panel_alloc_space(nk_rect* bounds, nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if ((layout.row.index) >= (layout.row.columns)) nk_panel_alloc_row(ctx, win);
			nk_layout_widget_space(bounds, ctx, win, (int) (nk_true));
			layout.row.index++;
		}

		public static void nk_layout_peek(nk_rect* bounds, nk_context ctx)
		{
			float y;
			int index;
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			y = (float) (layout.at_y);
			index = (int) (layout.row.index);
			if ((layout.row.index) >= (layout.row.columns))
			{
				layout.at_y += (float) (layout.row.height);
				layout.row.index = (int) (0);
			}

			nk_layout_widget_space(bounds, ctx, win, (int) (nk_false));
			if (layout.row.index == 0)
			{
				bounds->x -= (float) (layout.row.item_offset);
			}

			layout.at_y = (float) (y);
			layout.row.index = (int) (index);
		}

		public static int nk_widget(nk_rect* bounds, nk_context ctx)
		{
			nk_rect c = new nk_rect();
			nk_rect v = new nk_rect();
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (NK_WIDGET_INVALID);
			nk_panel_alloc_space(bounds, ctx);
			win = ctx.current;
			layout = win.layout;
			_in_ = ctx.input;
			c = (nk_rect) (layout.clip);
			bounds->x = ((float) ((int) (bounds->x)));
			bounds->y = ((float) ((int) (bounds->y)));
			bounds->w = ((float) ((int) (bounds->w)));
			bounds->h = ((float) ((int) (bounds->h)));
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			nk_unify(ref v, ref c, (float) (bounds->x), (float) (bounds->y), (float) (bounds->x + bounds->w),
				(float) (bounds->y + bounds->h));
			if (
				!(!(((((bounds->x) > (c.x + c.w)) || ((bounds->x + bounds->w) < (c.x))) || ((bounds->y) > (c.y + c.h))) ||
				    ((bounds->y + bounds->h) < (c.y))))) return (int) (NK_WIDGET_INVALID);
			if (
				!((((v.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (v.x + v.w))) &&
				  (((v.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (v.y + v.h))))) return (int) (NK_WIDGET_ROM);
			return (int) (NK_WIDGET_VALID);
		}

		public static int nk_widget_fitting(nk_rect* bounds, nk_context ctx, nk_vec2 item_padding)
		{
			nk_window win;
			nk_style style;
			nk_panel layout;
			int state;
			nk_vec2 panel_padding = new nk_vec2();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (NK_WIDGET_INVALID);
			win = ctx.current;
			style = ctx.style;
			layout = win.layout;
			state = (int) (nk_widget(bounds, ctx));
			panel_padding = (nk_vec2) (nk_panel_get_padding(style, (int) (layout.type)));
			if ((layout.row.index) == (1))
			{
				bounds->w += (float) (panel_padding.x);
				bounds->x -= (float) (panel_padding.x);
			}
			else bounds->x -= (float) (item_padding.x);
			if ((layout.row.index) == (layout.row.columns)) bounds->w += (float) (panel_padding.x);
			else bounds->w += (float) (item_padding.x);
			return (int) (state);
		}

		public static void nk_list_view_end(nk_list_view view)
		{
			nk_context ctx;
			nk_window win;
			nk_panel layout;
			if ((view == null) || (view.ctx == null)) return;
			ctx = view.ctx;
			win = ctx.current;
			layout = win.layout;
			layout.at_y = (float) (layout.bounds.y + (float) (view.total_height));
			*view.scroll_pointer = (uint) (*view.scroll_pointer + view.scroll_value);
			nk_group_end(view.ctx);
		}
	}
}