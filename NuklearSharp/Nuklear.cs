using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public static unsafe partial class Nuklear
	{
		public delegate void* NkAllocDelegate(nk_handle handle, void* old, ulong size);

		public delegate void NkFreeDelegate(nk_handle handle, void* old);

		public class nk_allocator
		{
			public nk_handle userdata;
			public NkAllocDelegate alloc;
			public NkFreeDelegate free;
		}

		public delegate float NkTextWidthDelegate(nk_handle handle, float height, sbyte* name, int length);
		public delegate float NkQueryFontGlyphDelegate(nk_handle handle, float height, nk_user_font_glyph *glyph, uint codepoint, uint next_codepoint);

		public class nk_user_font
		{
			public nk_handle userdata;
			public float height;
			public NkTextWidthDelegate width;
			public NkQueryFontGlyphDelegate query;
			/* font glyph callback to query drawing info */
			public nk_handle texture;
		}

		public class nk_buffer
		{
			public nk_buffer_marker[] marker = new nk_buffer_marker[2];
			public nk_allocator pool;
			public int type;
			public nk_memory memory;
			public float grow_factor;
			public ulong allocated;
			public ulong needed;
			public ulong calls;
			public ulong size;
		}

		public class nk_command_buffer
		{
			private readonly List<object> _commands = new List<object>();

			public List<Object> commands
			{
				get { return _commands; }
			}

			public nk_rect clip;
			public int use_clipping;
			public nk_handle userdata = new nk_handle();
		}

		public delegate void NkPluginPaste(nk_handle handle, nk_text_edit text_edit);

		public delegate void NkPluginCopy(nk_handle handle, string text, int length);

		public class nk_clipboard
		{
			public nk_handle userdata;
			public NkPluginPaste paste;
			public NkPluginCopy copy;
		}

		public class nk_text_undo_state
		{
			public nk_text_undo_record[] undo_rec = new nk_text_undo_record[99];
			public uint[] undo_char = new uint[999];
			public short undo_point;
			public short redo_point;
			public short undo_char_point;
			public short redo_char_point;
		}

		public delegate int NkPluginFilter(nk_text_edit text_edit, uint unicode);

		public class nk_text_edit
		{
			public nk_clipboard clip;
			public nk_str _string_;
			public NkPluginFilter filter;
			public nk_vec2 scrollbar;
			public int cursor;
			public int select_start;
			public int select_end;
			public byte mode;
			public byte cursor_at_end_of_line;
			public byte initialized;
			public byte has_preferred_x;
			public byte single_line;
			public byte active;
			public byte padding1;
			public float preferred_x;
			public nk_text_undo_state undo;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_style_item
		{
			public int type;
			public nk_style_item_data data;
		}

		public class nk_style
		{
			public nk_user_font font;
			public nk_cursor[] cursors = new nk_cursor[7];
			public nk_cursor* cursor_active;
			public nk_cursor* cursor_last;
			public int cursor_visible;
			public nk_style_text text;
			public nk_style_button button;
			public nk_style_button contextual_button;
			public nk_style_button menu_button;
			public nk_style_toggle option;
			public nk_style_toggle checkbox;
			public nk_style_selectable selectable;
			public nk_style_slider slider;
			public nk_style_progress progress;
			public nk_style_property property;
			public nk_style_edit edit;
			public nk_style_chart chart;
			public nk_style_scrollbar scrollh;
			public nk_style_scrollbar scrollv;
			public nk_style_tab tab;
			public nk_style_combo combo;
			public nk_style_window window;
		}

		public class nk_page_element
		{
			public nk_page_data data;
			public nk_page_element _next_;
			public nk_page_element prev;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct nk_property
		{
			[FieldOffset(0)] public int i;
			[FieldOffset(0)] public float f;
			[FieldOffset(0)] public double d;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_property_variant
		{
			public int kind;

			public nk_property value;
			public nk_property min_value;
			public nk_property max_value;
			public nk_property step;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_command
		{
			public int type;
		}

		public static nk_rect nk_null_rect = new nk_rect {x = -8192.0f, y = -8192.0f, w = 16384, h = 16384};
		public static nk_color nk_red = new nk_color {r = 255, g = 0, b = 0, a = 255};
		public static nk_color nk_green = new nk_color {r = 0, g = 255, b = 0, a = 255};
		public static nk_color nk_blue = new nk_color {r = 0, g = 0, g = 255, b = 255};
		public static nk_color nk_white = new nk_color {r = 255, g = 255, b = 255, a = 255};
		public static nk_color nk_black = new nk_color {r = 0, g = 0, b = 0, a = 255};
		public static nk_color nk_yellow = new nk_color {r = 255, g = 255, b = 0, a = 255};

		public static nk_color[] nk_default_color_style =
		{
			new nk_color {r = 175, g = 175, b = 175, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255},
			new nk_color {r = 65, g = 65, b = 65, a = 255},
			new nk_color {r = 50, g = 50, b = 50, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255},
			new nk_color {r = 35, g = 35, b = 35, a = 255},
			new nk_color {r = 100, g = 100, b = 100, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 35, g = 35, b = 35, a = 255},
			new nk_color {r = 38, g = 38, b = 38, a = 255},
			new nk_color {r = 100, g = 100, b = 100, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 150, g = 150, b = 150, a = 255},
			new nk_color {r = 38, g = 38, b = 38, a = 255},
			new nk_color {r = 38, g = 38, b = 38, a = 255},
			new nk_color {r = 175, g = 175, b = 175, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 45, g = 45, b = 45, a = 255},
			new nk_color {r = 255, g = 0, b = 0, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255},
			new nk_color {r = 100, g = 100, b = 100, a = 255},
			new nk_color {r = 120, g = 120, b = 120, a = 255},
			new nk_color {r = 150, g = 150, b = 150, a = 255},
			new nk_color {r = 40, g = 40, b = 40, a = 255}
		};

		public static string[] nk_color_names = {
			"NK_COLOR_TEXT", "NK_COLOR_WINDOW", "NK_COLOR_HEADER", "NK_COLOR_BORDER", "NK_COLOR_BUTTON", "NK_COLOR_BUTTON_HOVER",
			"NK_COLOR_BUTTON_ACTIVE", "NK_COLOR_TOGGLE", "NK_COLOR_TOGGLE_HOVER", "NK_COLOR_TOGGLE_CURSOR", "NK_COLOR_SELECT",
			"NK_COLOR_SELECT_ACTIVE", "NK_COLOR_SLIDER", "NK_COLOR_SLIDER_CURSOR", "NK_COLOR_SLIDER_CURSOR_HOVER",
			"NK_COLOR_SLIDER_CURSOR_ACTIVE", "NK_COLOR_PROPERTY", "NK_COLOR_EDIT", "NK_COLOR_EDIT_CURSOR", "NK_COLOR_COMBO",
			"NK_COLOR_CHART", "NK_COLOR_CHART_COLOR", "NK_COLOR_CHART_COLOR_HIGHLIGHT", "NK_COLOR_SCROLLBAR",
			"NK_COLOR_SCROLLBAR_CURSOR", "NK_COLOR_SCROLLBAR_CURSOR_HOVER", "NK_COLOR_SCROLLBAR_CURSOR_ACTIVE",
			"NK_COLOR_TAB_HEADER"
		};

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_inv_sqrt_union
		{
			[FieldOffset(0)] public uint i;

			[FieldOffset(0)] public float f;
		}

		public static float nk_inv_sqrt(float number)
		{
			var threehalfs = 1.5f;
			var conv = new nk_inv_sqrt_union
			{
				i = 0,
			};
			var x2 = number*0.5f;
			conv.i = 0x5f375A84 - (conv.i >> 1);
			conv.f = conv.f*(threehalfs - (x2*conv.f*conv.f));
			return conv.f;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_murmur_hash_union
		{
			[FieldOffset(0)] public uint* i;

			[FieldOffset(0)] public byte* b;
		}

		public static uint nk_murmur_hash(void* key, int len, uint seed)
		{
			nk_murmur_hash_union conv = new nk_murmur_hash_union();
			byte* data = (byte*) (key);
			int nblocks = (int) (len/4);
			uint h1 = (uint) (seed);
			uint c1 = (uint) (0xcc9e2d51);
			uint c2 = (uint) (0x1b873593);
			byte* tail;
			uint* blocks;
			uint k1;
			int i;
			if (key == null) return (uint) (0);
			conv.b = (data + nblocks*4);
			blocks = conv.i;
			for (i = (int) (-nblocks); i; ++i)
			{
				k1 = (uint) (blocks[i]);
				k1 *= (uint) (c1);
				k1 = (uint) ((k1) << (15) | ((k1) >> (32 - 15)));
				k1 *= (uint) (c2);
				h1 ^= (uint) (k1);
				h1 = (uint) ((h1) << (13) | ((h1) >> (32 - 13)));
				h1 = (uint) (h1*5 + 0xe6546b64);
			}
			tail = (data + nblocks*4);
			k1 = (uint) (0);
			switch (len & 3)
			{
				case 3:
					k1 ^= ((uint) (tail[2] << 16));
				case 2:
					k1 ^= ((uint) (tail[1] << 8u));
				case 1:
					k1 ^= (uint) (tail[0]);
					k1 *= (uint) (c1);
					k1 = (uint) ((k1) << (15) | ((k1) >> (32 - 15)));
					k1 *= (uint) (c2);
					h1 ^= (uint) (k1);
					break;
				default:
					break;
			}

			h1 ^= ((uint) (len));
			h1 ^= (uint) (h1 >> 16);
			h1 *= (uint) (0x85ebca6b);
			h1 ^= (uint) (h1 >> 13);
			h1 *= (uint) (0xc2b2ae35);
			h1 ^= (uint) (h1 >> 16);
			return (uint) (h1);
		}

		public class nk_draw_list
		{
			public nk_rect clip_rect = new nk_rect();
			public nk_vec2[] circle_vtx = new nk_vec2[12];
			public nk_convert_config config = new nk_convert_config();
			public nk_buffer buffer;
			public nk_buffer vertices;
			public nk_buffer elements;
			public uint element_count;
			public uint vertex_count;
			public uint cmd_count;
			public ulong cmd_offset;
			public uint path_count;
			public uint path_offset;
			public int line_AA;
			public int shape_AA;
		}

		public class nk_mouse
		{
			public nk_mouse_button[] buttons = new nk_mouse_button[NK_BUTTON_MAX];
			public nk_vec2 pos = new nk_vec2();
			public nk_vec2 prev = new nk_vec2();
			public nk_vec2 delta = new nk_vec2();
			public nk_vec2 scroll_delta = new nk_vec2();
			public byte grab;
			public byte grabbed;
			public byte ungrab;
		}

		public class nk_keyboard
		{
			public nk_key[] keys = new nk_key[NK_KEY_MAX];
			public PinnedArray<sbyte> text = new PinnedArray<sbyte>(16);
			public int text_len;
		}

		public class nk_input
		{
			public nk_keyboard keyboard = new nk_keyboard();
			public nk_mouse mouse = new nk_mouse();
		}

		public static void nk_input_button(nk_context ctx, int id, int x, int y, int down)
		{
			if (ctx == null) return;
			var _in_ = ctx.input;
			if (_in_.mouse.buttons[id].down == down) return;
			_in_.mouse.buttons[id].clicked_pos.x = x;
			_in_.mouse.buttons[id].clicked_pos.y = y;
			_in_.mouse.buttons[id].down = down;
			_in_.mouse.buttons[id].clicked++;
		}

		public static void nk_input_glyph(nk_context ctx, sbyte* glyph)
		{
			if (ctx == null) return;
			var _in_ = ctx.input;
			uint unicode;
			var len = nk_utf_decode(glyph, &unicode, 4);

			
			if ((len != 0) && (_in_.keyboard.text_len + len < 16))
			{
				nk_utf_encode(unicode, (sbyte *)_in_.keyboard.text + _in_.keyboard.text_len, 16 - _in_.keyboard.text_len);
				_in_.keyboard.text_len += len;
			}
		}

		public static uint nk_convert(nk_context ctx, nk_buffer cmds, nk_buffer vertices, nk_buffer elements, nk_convert_config config)
		{
			var res = NK_CONVERT_SUCCESS;
			nk_command* cmd;
			if ((ctx == null) || (cmds == null) || (vertices == null) || (elements == null) || (config == null) || (config.vertex_layout == null)) return NK_CONVERT_INVALID_PARAM;
			nk_draw_list_setup(ctx.draw_list, config, cmds, vertices, elements, config.line_AA, config.shape_AA);
			for ((cmd) = nk__begin(ctx); (cmd) != 0; (cmd) = nk__next(ctx, cmd))
			{
				switch (cmd->type)
				{
					case NK_COMMAND_NOP: break;
					case NK_COMMAND_SCISSOR:
						{
							nk_command_scissor s = (nk_command_scissor)(cmd); nk_draw_list_add_clip(ctx.draw_list, (nk_rect)(nk_rect_((float)(s.x), (float)(s.y), (float)(s.w), (float)(s.h))));
						}
						break;
					case NK_COMMAND_LINE:
						{
							nk_command_line l = (nk_command_line)(cmd); nk_draw_list_stroke_line(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(l.begin.x), (float)(l.begin.y))), (nk_vec2)(nk_vec2_((float)(l.end.x), (float)(l.end.y))), (nk_color)(l.color), (float)(l.line_thickness));
						}
						break;
					case NK_COMMAND_CURVE:
						{
							nk_command_curve q = (nk_command_curve)(cmd); nk_draw_list_stroke_curve(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(q.begin.x), (float)(q.begin.y))), (nk_vec2)(nk_vec2_((float)(q.ctrl_0.x), (float)(q.ctrl_0.y))), (nk_vec2)(nk_vec2_((float)(q.ctrl_1.x), (float)(q.ctrl_1.y))), (nk_vec2)(nk_vec2_((float)(q.end.x), (float)(q.end.y))), (nk_color)(q.color), (uint)(config.curve_segment_count), (float)(q.line_thickness));
						}
						break;
					case NK_COMMAND_RECT:
						{
							nk_command_rect r = (nk_command_rect)(cmd); nk_draw_list_stroke_rect(ctx.draw_list, (nk_rect)(nk_rect_((float)(r.x), (float)(r.y), (float)(r.w), (float)(r.h))), (nk_color)(r.color), (float)(r.rounding), (float)(r.line_thickness));
						}
						break;
					case NK_COMMAND_RECT_FILLED:
						{
							nk_command_rect_filled r = (nk_command_rect_filled)(cmd); nk_draw_list_fill_rect(ctx.draw_list, (nk_rect)(nk_rect_((float)(r.x), (float)(r.y), (float)(r.w), (float)(r.h))), (nk_color)(r.color), (float)(r.rounding));
						}
						break;
					case NK_COMMAND_RECT_MULTI_COLOR:
						{
							nk_command_rect_multi_color r = (nk_command_rect_multi_color)(cmd); nk_draw_list_fill_rect_multi_color(ctx.draw_list, (nk_rect)(nk_rect_((float)(r.x), (float)(r.y), (float)(r.w), (float)(r.h))), (nk_color)(r.left), (nk_color)(r.top), (nk_color)(r.right), (nk_color)(r.bottom));
						}
						break;
					case NK_COMMAND_CIRCLE:
						{
							nk_command_circle c = (nk_command_circle)(cmd); nk_draw_list_stroke_circle(ctx.draw_list, (nk_vec2)(nk_vec2_((float)((float)(c.x) + (float)(c.w) / 2), (float)((float)(c.y) + (float)(c.h) / 2))), (float)((float)(c.w) / 2), (nk_color)(c.color), (uint)(config.circle_segment_count), (float)(c.line_thickness));
						}
						break;
					case NK_COMMAND_CIRCLE_FILLED:
						{
							nk_command_circle_filled c = (nk_command_circle_filled)(cmd); nk_draw_list_fill_circle(ctx.draw_list, (nk_vec2)(nk_vec2_((float)((float)(c.x) + (float)(c.w) / 2), (float)((float)(c.y) + (float)(c.h) / 2))), (float)((float)(c.w) / 2), (nk_color)(c.color), (uint)(config.circle_segment_count));
						}
						break;
					case NK_COMMAND_ARC:
						{
							nk_command_arc c = (nk_command_arc)(cmd); nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(c.cx), (float)(c.cy)))); nk_draw_list_path_arc_to(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(c.cx), (float)(c.cy))), (float)(c.r), (float)(c.a[0]), (float)(c.a[1]), (uint)(config.arc_segment_count)); nk_draw_list_path_stroke(ctx.draw_list, (nk_color)(c.color), (int)(NK_STROKE_CLOSED), (float)(c.line_thickness));
						}
						break;
					case NK_COMMAND_ARC_FILLED:
						{
							nk_command_arc_filled c = (nk_command_arc_filled)(cmd); nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(c.cx), (float)(c.cy)))); nk_draw_list_path_arc_to(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(c.cx), (float)(c.cy))), (float)(c.r), (float)(c.a[0]), (float)(c.a[1]), (uint)(config.arc_segment_count)); nk_draw_list_path_fill(ctx.draw_list, (nk_color)(c.color));
						}
						break;
					case NK_COMMAND_TRIANGLE:
						{
							nk_command_triangle t = (nk_command_triangle)(cmd); nk_draw_list_stroke_triangle(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(t.a.x), (float)(t.a.y))), (nk_vec2)(nk_vec2_((float)(t.b.x), (float)(t.b.y))), (nk_vec2)(nk_vec2_((float)(t.c.x), (float)(t.c.y))), (nk_color)(t.color), (float)(t.line_thickness));
						}
						break;
					case NK_COMMAND_TRIANGLE_FILLED:
						{
							nk_command_triangle_filled t = (nk_command_triangle_filled)(cmd); nk_draw_list_fill_triangle(ctx.draw_list, (nk_vec2)(nk_vec2_((float)(t.a.x), (float)(t.a.y))), (nk_vec2)(nk_vec2_((float)(t.b.x), (float)(t.b.y))), (nk_vec2)(nk_vec2_((float)(t.c.x), (float)(t.c.y))), (nk_color)(t.color));
						}
						break;
					case NK_COMMAND_POLYGON:
						{
							int i; nk_command_polygon p = (nk_command_polygon)(cmd); for (i = (int)(0); (i) < (p.point_count); ++i)
							{
								nk_vec2 pnt = (nk_vec2)(nk_vec2_((float)(p.points.x), (float)(p.points.y))); nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2)(pnt));
							} nk_draw_list_path_stroke(ctx.draw_list, (nk_color)(p.color), (int)(NK_STROKE_CLOSED), (float)(p.line_thickness));
						}
						break;
					case NK_COMMAND_POLYGON_FILLED:
						{
							int i; nk_command_polygon_filled p = (nk_command_polygon_filled)(cmd); for (i = (int)(0); (i) < (p.point_count); ++i)
							{
								nk_vec2 pnt = (nk_vec2)(nk_vec2_((float)(p.points.x), (float)(p.points.y))); nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2)(pnt));
							} nk_draw_list_path_fill(ctx.draw_list, (nk_color)(p.color));
						}
						break;
					case NK_COMMAND_POLYLINE:
						{
							int i; nk_command_polyline p = (nk_command_polyline)(cmd); for (i = (int)(0); (i) < (p.point_count); ++i)
							{
								nk_vec2 pnt = (nk_vec2)(nk_vec2_((float)(p.points.x), (float)(p.points.y))); nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2)(pnt));
							} nk_draw_list_path_stroke(ctx.draw_list, (nk_color)(p.color), (int)(NK_STROKE_OPEN), (float)(p.line_thickness));
						}
						break;
					case NK_COMMAND_TEXT:
						{
							nk_command_text t = (nk_command_text)(cmd); nk_draw_list_add_text(ctx.draw_list, t.font, (nk_rect)(nk_rect_((float)(t.x), (float)(t.y), (float)(t.w), (float)(t.h))), t._string_, (int)(t.length), (float)(t.height), (nk_color)(t.foreground));
						}
						break;
					case NK_COMMAND_IMAGE:
						{
							nk_command_image i = (nk_command_image)(cmd); nk_draw_list_add_image(ctx.draw_list, (nk_image)(i.img), (nk_rect)(nk_rect_((float)(i.x), (float)(i.y), (float)(i.w), (float)(i.h))), (nk_color)(i.col));
						}
						break;
					case NK_COMMAND_CUSTOM:
						{
							nk_command_custom c = (nk_command_custom)(cmd); c.callback(ctx.draw_list, (short)(c.x), (short)(c.y), (ushort)(c.w), (ushort)(c.h), (nk_handle)(c.callback_data));
						}
						break;
					default: break;
				}
			}
			res |= (uint)(((cmds.needed) > (cmds.allocated + (cmds.memory.size - cmds.size))) ? NK_CONVERT_COMMAND_BUFFER_FULL : 0);
			res |= (uint)(((vertices.needed) > (vertices.allocated)) ? NK_CONVERT_VERTEX_BUFFER_FULL : 0);
			res |= (uint)(((elements.needed) > (elements.allocated)) ? NK_CONVERT_ELEMENT_BUFFER_FULL : 0);
			return (uint)(res);
		}

		public static void nk_textedit_clear_state(nk_text_edit state, int type, NkPluginFilter filter)
		{
			state.undo.undo_point = 0;
			state.undo.undo_char_point = 0;
			state.undo.redo_point = 99;
			state.undo.redo_char_point = 999;
			state.select_end = state.select_start = 0;
			state.cursor = 0;
			state.has_preferred_x = 0;
			state.preferred_x = 0;
			state.cursor_at_end_of_line = 0;
			state.initialized = 1;
			state.single_line = (byte)((type == NK_TEXT_EDIT_SINGLE_LINE)?1:0);
			state.mode = NK_TEXT_EDIT_MODE_VIEW;
			state.filter = filter;
			state.scrollbar = nk_vec2_(0, 0);
		}
	}
}