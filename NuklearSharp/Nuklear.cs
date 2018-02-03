using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public static unsafe partial class Nuklear
	{
		public delegate float NkTextWidthDelegate(nk_handle handle, float height, char* text, int length);

		public delegate void NkQueryFontGlyphDelegate(nk_handle handle,
			float height, nk_user_font_glyph* glyph, char codepoint, char next_codepoint);

		public delegate void NkCommandCustomCallback(
			nk_draw_list list, short x, short y, ushort w, ushort h, nk_handle callback_data);

		public delegate void NkPluginPaste(nk_handle handle, nk_text_edit text_edit);

		public delegate void NkPluginCopy(nk_handle handle, char* text, int length);

		public delegate void NkDrawNotify(nk_command_buffer buffer, nk_handle handle);

		public delegate int NkPluginFilter(nk_text_edit text_edit, char unicode);

		public delegate float NkFloatValueGetter(void* handle, int index);

		public delegate float NkComboCallback(void* handle, int index, char** item);

		public delegate int QSortComparer(void* a, void* b);

		public static nk_vec2[,] nk_cursor_data =
		{
			{new nk_vec2 {x = 0, y = 3}, new nk_vec2 {x = 12, y = 19}, new nk_vec2 {x = 0, y = 0}},
			{new nk_vec2 {x = 13, y = 0}, new nk_vec2 {x = 7, y = 16}, new nk_vec2 {x = 4, y = 8}},
			{new nk_vec2 {x = 31, y = 0}, new nk_vec2 {x = 23, y = 23}, new nk_vec2 {x = 11, y = 11}},
			{new nk_vec2 {x = 21, y = 0}, new nk_vec2 {x = 9, y = 23}, new nk_vec2 {x = 5, y = 11}},
			{new nk_vec2 {x = 55, y = 18}, new nk_vec2 {x = 23, y = 9}, new nk_vec2 {x = 11, y = 5}},
			{new nk_vec2 {x = 73, y = 0}, new nk_vec2 {x = 17, y = 17}, new nk_vec2 {x = 9, y = 9}},
			{new nk_vec2 {x = 55, y = 0}, new nk_vec2 {x = 17, y = 17}, new nk_vec2 {x = 9, y = 9}}
		};

		public static nk_rect nk_null_rect = new nk_rect {x = -8192.0f, y = -8192.0f, w = 16384, h = 16384};
		public static nk_color nk_red = new nk_color {r = 255, g = 0, b = 0, a = 255};
		public static nk_color nk_green = new nk_color {r = 0, g = 255, b = 0, a = 255};
		public static nk_color nk_blue = new nk_color {r = 0, g = 0, b = 255, a = 255};
		public static nk_color nk_white = new nk_color {r = 255, g = 255, b = 255, a = 255};
		public static nk_color nk_black = new nk_color {r = 0, g = 0, b = 0, a = 255};
		public static nk_color nk_yellow = new nk_color {r = 255, g = 255, b = 0, a = 255};
		public static nk_color nk_black_trans = new nk_color {r = 0, g = 0, b = 0, a = 0};

		public static nk_color[] hue_colors =
		{
			new nk_color {r = 255, g = 0, b = 0, a = 255},
			new nk_color {r = 255, g = 255, b = 0, a = 255},
			new nk_color {r = 0, g = 255, b = 0, a = 255},
			new nk_color {r = 0, g = 255, b = 255, a = 255},
			new nk_color {r = 0, g = 0, b = 255, a = 255},
			new nk_color {r = 255, g = 0, b = 255, a = 255},
			new nk_color {r = 255, g = 0, b = 0, a = 255}
		};

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

		public static string[] nk_color_names =
		{
			"NK_COLOR_TEXT", "NK_COLOR_WINDOW", "NK_COLOR_HEADER", "NK_COLOR_BORDER", "NK_COLOR_BUTTON", "NK_COLOR_BUTTON_HOVER",
			"NK_COLOR_BUTTON_ACTIVE", "NK_COLOR_TOGGLE", "NK_COLOR_TOGGLE_HOVER", "NK_COLOR_TOGGLE_CURSOR", "NK_COLOR_SELECT",
			"NK_COLOR_SELECT_ACTIVE", "NK_COLOR_SLIDER", "NK_COLOR_SLIDER_CURSOR", "NK_COLOR_SLIDER_CURSOR_HOVER",
			"NK_COLOR_SLIDER_CURSOR_ACTIVE", "NK_COLOR_PROPERTY", "NK_COLOR_EDIT", "NK_COLOR_EDIT_CURSOR", "NK_COLOR_COMBO",
			"NK_COLOR_CHART", "NK_COLOR_CHART_COLOR", "NK_COLOR_CHART_COLOR_HIGHLIGHT", "NK_COLOR_SCROLLBAR",
			"NK_COLOR_SCROLLBAR_CURSOR", "NK_COLOR_SCROLLBAR_CURSOR_HOVER", "NK_COLOR_SCROLLBAR_CURSOR_ACTIVE",
			"NK_COLOR_TAB_HEADER"
		};

		private static PinnedArray<uint> ranges = new PinnedArray<uint>(new uint[] {0x0020, 0x00FF, 0});

		[StructLayout(LayoutKind.Explicit)]
		public struct nk_handle
		{
			[FieldOffset(0)] public void* ptr;

			[FieldOffset(0)] public int id;
		}

		public class nk_user_font
		{
			public nk_handle userdata;
			public float height;
			public nk_handle texture;

			public NkTextWidthDelegate width;
			public NkQueryFontGlyphDelegate query;
		}

		public class nk_font
		{
			public nk_font next;
			public nk_user_font handle = new nk_user_font();
			public nk_baked_font info = new nk_baked_font();
			public float scale;
			public nk_font_glyph* glyphs;
			public nk_font_glyph* fallback;
			public char fallback_codepoint;
			public nk_handle texture = new nk_handle();
			public nk_font_config config;

			public float text_width(nk_handle handle, float height, char* s, int length)
			{
				return nk_font_text_width(this, height, s, length);
			}

			public void query_font_glyph(nk_handle handle, float height, nk_user_font_glyph* glyph, char codepoint,
				char next_codepoint)
			{
				nk_font_query_font_glyph(this, height, glyph, codepoint, next_codepoint);
			}
		}

		public class nk_clipboard
		{
			public nk_handle userdata;
			public NkPluginPaste paste;
			public NkPluginCopy copy;
		}

		public class nk_keyboard
		{
			public PinnedArray<nk_key> keys = new PinnedArray<nk_key>(new nk_key[NK_KEY_MAX]);
			public PinnedArray<char> text = new PinnedArray<char>(new char[16]);
			public int text_len;
		}

		public class nk_mouse
		{
			public PinnedArray<nk_mouse_button> buttons = new PinnedArray<nk_mouse_button>(new nk_mouse_button[NK_BUTTON_MAX]);
			public nk_vec2 pos;
			public nk_vec2 prev;
			public nk_vec2 delta;
			public nk_vec2 scroll_delta;
			public byte grab;
			public byte grabbed;
			public byte ungrab;
		}

		public class nk_buffer
		{
			public nk_buffer_marker[] marker = new nk_buffer_marker[2];
			public int type;
			public nk_memory memory;
			public float grow_factor;
			public ulong allocated;
			public ulong needed;
			public ulong calls;
			public ulong size;
		}

		public class nk_context
		{
			public nk_input input = new nk_input();
			public nk_style style = new nk_style();
			public nk_buffer memory = new nk_buffer();
			public nk_clipboard clip = new nk_clipboard();
			public uint last_widget_state;
			public int button_behavior;
			public nk_configuration_stacks stacks = new nk_configuration_stacks();
			public float delta_time_seconds;
			public nk_draw_list draw_list = new nk_draw_list();
			public nk_handle userdata = new nk_handle();
			public nk_text_edit text_edit = new nk_text_edit();
			public nk_command_buffer overlay = new nk_command_buffer();
			public int build;
			public nk_window begin;
			public nk_window end;
			public nk_window active;
			public nk_window current;
			public uint count;
			public uint seq;
		}

		public class nk_panel
		{
			public int type;
			public uint flags;
			public nk_rect bounds = new nk_rect();
			public nk_scroll offset;
			public float at_x;
			public float at_y;
			public float max_x;
			public float footer_height;
			public float header_height;
			public float border;
			public uint has_scrolling;
			public nk_rect clip = new nk_rect();
			public nk_menu_state menu = new nk_menu_state();
			public nk_row_layout row = new nk_row_layout();
			public nk_chart chart = new nk_chart();
			public nk_command_buffer buffer;
			public nk_panel parent;
		}

		public class nk_window
		{
			public uint seq;
			public uint name;
			public PinnedArray<char> name_string = new PinnedArray<char>(64);
			public uint flags;
			public nk_rect bounds = new nk_rect();
			public nk_scroll scrollbar = new nk_scroll();
			public nk_command_buffer buffer = new nk_command_buffer();
			public nk_panel layout;
			public float scrollbar_hiding_timer;
			public nk_property_state property = new nk_property_state();
			public nk_popup_state popup = new nk_popup_state();
			public nk_edit_state edit = new nk_edit_state();
			public uint scrolled;
			public nk_table tables;
			public uint table_count;
			public nk_window next;
			public nk_window prev;
			public nk_window parent;
		}

		public class nk_draw_list
		{
			public nk_rect clip_rect;
			public nk_vec2[] circle_vtx = new nk_vec2[12];
			public nk_convert_config config;
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
			public nk_handle userdata;
		}

		public class nk_style_item_data
		{
			public nk_image image;
			public nk_color color;
		}

		public class nk_style_item
		{
			public int type;
			public nk_style_item_data data = new nk_style_item_data();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_rp_context
		{
			public int width;
			public int height;
			public int align;
			public int init_mode;
			public int heuristic;
			public int num_nodes;
			public nk_rp_node* active_head;
			public nk_rp_node* free_head;
			public nk_rp_node extra_0, extra_1;
		}

		public class nk_font_atlas
		{
			public void* pixel;
			public int tex_width;
			public int tex_height;
			public nk_recti custom;
			public nk_cursor[] cursors = new nk_cursor[NK_CURSOR_COUNT];
			public int glyph_count;
			public nk_font_glyph* glyphs;
			public nk_font default_font;
			public nk_font fonts;
			public nk_font_config config;
			public int font_num;

			public nk_font_atlas()
			{
				for (var i = 0; i < cursors.Length; ++i)
				{
					cursors[i] = new nk_cursor();
				}
			}
		}

		public class nk_text_undo_state
		{
			public PinnedArray<nk_text_undo_record> undo_rec = new PinnedArray<nk_text_undo_record>(new nk_text_undo_record[99]);
			public PinnedArray<uint> undo_char = new PinnedArray<uint>(new uint[999]);
			public short undo_point;
			public short redo_point;
			public short undo_char_point;
			public short redo_char_point;
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

		public class nk_style
		{
			public nk_user_font font;
			public nk_cursor[] cursors = new nk_cursor[NK_CURSOR_COUNT];
			public nk_cursor cursor_active;
			public nk_cursor cursor_last;
			public int cursor_visible;
			public nk_style_text text = new nk_style_text();
			public nk_style_button button = new nk_style_button();
			public nk_style_button contextual_button = new nk_style_button();
			public nk_style_button menu_button = new nk_style_button();
			public nk_style_toggle option = new nk_style_toggle();
			public nk_style_toggle checkbox = new nk_style_toggle();
			public nk_style_selectable selectable = new nk_style_selectable();
			public nk_style_slider slider = new nk_style_slider();
			public nk_style_progress progress = new nk_style_progress();
			public nk_style_property property = new nk_style_property();
			public nk_style_edit edit = new nk_style_edit();
			public nk_style_chart chart = new nk_style_chart();
			public nk_style_scrollbar scrollh = new nk_style_scrollbar();
			public nk_style_scrollbar scrollv = new nk_style_scrollbar();
			public nk_style_tab tab = new nk_style_tab();
			public nk_style_combo combo = new nk_style_combo();
			public nk_style_window window = new nk_style_window();
		}

		public class nk_chart
		{
			public int slot;
			public float x;
			public float y;
			public float w;
			public float h;
			public nk_chart_slot[] slots = new nk_chart_slot[4];
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_inv_sqrt_union
		{
			[FieldOffset(0)] public uint i;

			[FieldOffset(0)] public float f;
		}

		[StructLayout(LayoutKind.Explicit)]
		private struct nk_murmur_hash_union
		{
			[FieldOffset(0)] public uint* i;

			[FieldOffset(0)] public byte* b;

			public nk_murmur_hash_union(void* ptr)
			{
				i = (uint*) ptr;
				b = (byte*) ptr;
			}
		}

		public class nk_command_base
		{
			public nk_command header;
			public nk_handle userdata;
			public nk_command_base next;
		}

		public class nk_command_scissor : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
		}

		public class nk_command_line : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i begin = new nk_vec2i();
			public nk_vec2i end = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_curve : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i begin = new nk_vec2i();
			public nk_vec2i end = new nk_vec2i();
			public nk_vec2i ctrl_0 = new nk_vec2i();
			public nk_vec2i ctrl_1 = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_rect : nk_command_base
		{
			public ushort rounding;
			public ushort line_thickness;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_rect_filled : nk_command_base
		{
			public ushort rounding;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_rect_multi_color : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color left = new nk_color();
			public nk_color top = new nk_color();
			public nk_color bottom = new nk_color();
			public nk_color right = new nk_color();
		}

		public class nk_command_triangle : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i a = new nk_vec2i();
			public nk_vec2i b = new nk_vec2i();
			public nk_vec2i c = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_triangle_filled : nk_command_base
		{
			public nk_vec2i a = new nk_vec2i();
			public nk_vec2i b = new nk_vec2i();
			public nk_vec2i c = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_circle : nk_command_base
		{
			public short x;
			public short y;
			public ushort line_thickness;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_circle_filled : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_arc : nk_command_base
		{
			public short cx;
			public short cy;
			public ushort r;
			public ushort line_thickness;
			public PinnedArray<float> a = new PinnedArray<float>(2);
			public nk_color color = new nk_color();
		}

		public class nk_command_arc_filled : nk_command_base
		{
			public short cx;
			public short cy;
			public ushort r;
			public PinnedArray<float> a = new PinnedArray<float>(2);
			public nk_color color = new nk_color();
		}

		public class nk_command_polygon : nk_command_base
		{
			public nk_color color;
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i[] points;
		}

		public class nk_command_polygon_filled : nk_command_base
		{
			public nk_color color;
			public ushort point_count;
			public nk_vec2i[] points;
		}

		public class nk_command_polyline : nk_command_base
		{
			public nk_color color;
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i[] points;
		}

		public class nk_command_image : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_image img = new nk_image();
			public nk_color col = new nk_color();
		}

		public class nk_command_text : nk_command_base
		{
			public nk_user_font font;
			public nk_color background;
			public nk_color foreground;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public float height;
			public char* _string_;
			public int length;
		}

		public class nk_command_custom : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_handle callback_data;
			public NkCommandCustomCallback callback;
		}

		public class nk_command_buffer
		{
			private readonly List<nk_command_base> _commands = new List<nk_command_base>();

			public List<nk_command_base> commands
			{
				get { return _commands; }
			}

			public nk_command_base begin
			{
				get { return _commands[0]; }
			}

			public nk_command_base last
			{
				get { return _commands[_commands.Count - 1]; }
			}

			public nk_rect clip;
			public int use_clipping;
			public nk_handle userdata = new nk_handle();
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_config_stack_button_behavior_element
		{
			public int old_value;
		}

		public class nk_convert_config
		{
			public float global_alpha;
			public int line_AA;
			public int shape_AA;
			public uint circle_segment_count;
			public uint arc_segment_count;
			public uint curve_segment_count;
			public nk_draw_null_texture _null_ = new nk_draw_null_texture();
			public nk_draw_vertex_layout_element[] vertex_layout;
			public ulong vertex_size;
			public ulong vertex_alignment;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct nk_user_font_glyph
		{
			public fixed float uv_x [2];
			public fixed float uv_y [2];
			public nk_vec2 offset;
			public float width;
			public float height;
			public float xadvance;
		}


		private static readonly Func<object>[] _commandCreators =
		{
			null,
			() => nk_create_command<nk_command_scissor>(),
			() => nk_create_command<nk_command_line>(),
			() => nk_create_command<nk_command_curve>(),
			() => nk_create_command<nk_command_rect>(),
			() => nk_create_command<nk_command_rect_filled>(),
			() => nk_create_command<nk_command_rect_multi_color>(),
			() => nk_create_command<nk_command_circle>(),
			() => nk_create_command<nk_command_circle_filled>(),
			() => nk_create_command<nk_command_arc>(),
			() => nk_create_command<nk_command_arc_filled>(),
			() => nk_create_command<nk_command_triangle>(),
			() => nk_create_command<nk_command_triangle_filled>(),
			() => nk_create_command<nk_command_polygon>(),
			() => nk_create_command<nk_command_polygon_filled>(),
			() => nk_create_command<nk_command_polyline>(),
			() => nk_create_command<nk_command_text>(),
			() => nk_create_command<nk_command_image>(),
			() => nk_create_command<nk_command_custom>()
		};

		private static object nk_create_command<T>() where T : new()
		{
			return new T();
		}

		public static void nk_command_buffer_init(nk_command_buffer cmdbuf, int clip)
		{
			cmdbuf.use_clipping = clip;
			cmdbuf.commands.Clear();
		}

		public static void nk_command_buffer_reset(nk_command_buffer buffer)
		{
			if (buffer == null) return;
			buffer.commands.Clear();
			buffer.clip = nk_null_rect;
		}

		public static nk_command_base nk_command_buffer_push(nk_command_buffer b, int t)
		{
			if (b == null || t < 0 || t >= _commandCreators.Length || _commandCreators[t] == null) return null;

			var creator = _commandCreators[t];

			var command = (nk_command_base) creator();

			command.header = new nk_command
			{
				type = t
			};

			b.commands.Add(command);

			return command;
		}

		public static nk_window nk__begin(nk_context ctx)
		{
			if (ctx == null || ctx.count == 0) return null;
			if (ctx.build == 0)
			{
				nk_build(ctx);
				ctx.build = nk_true;
			}

			var iter = ctx.begin;
			while ((iter != null) &&
			       ((iter.buffer.commands.Count == 0) || (iter.flags & NK_WINDOW_HIDDEN) != 0 || (iter.seq != ctx.seq)))
			{
				iter = iter.next;
			}

			return iter;
		}


		public static void nk_build(nk_context ctx)
		{
			if (ctx.style.cursor_active == null) ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_ARROW];
			if ((ctx.style.cursor_active != null) && (ctx.input.mouse.grabbed == 0) && ((ctx.style.cursor_visible) != 0))
			{
				var mouse_bounds = new nk_rect();
				var cursor = ctx.style.cursor_active;
				nk_command_buffer_init(ctx.overlay, NK_CLIPPING_OFF);
				nk_start_buffer(ctx, ctx.overlay);
				mouse_bounds.x = ctx.input.mouse.pos.x - cursor.offset.x;
				mouse_bounds.y = ctx.input.mouse.pos.y - cursor.offset.y;
				mouse_bounds.w = cursor.size.x;
				mouse_bounds.h = cursor.size.y;
				nk_draw_image(ctx.overlay, mouse_bounds, cursor.img, nk_white);
				nk_finish_buffer(ctx, ctx.overlay);
			}

			var it = ctx.begin;
			nk_command_base cmd = null;
			for (; it != null;)
			{
				var next = it.next;
				if (((it.flags & NK_WINDOW_HIDDEN) != 0) || (it.seq != ctx.seq))
					goto cont;
				cmd = it.buffer.last;

				while ((next != null) &&
				       ((next.buffer.last == next.buffer.begin) || ((next.flags & NK_WINDOW_HIDDEN) != 0)))
				{
					next = next.next;
				}
				if (next != null) cmd.next = next.buffer.begin;
				cont:
				it = next;
			}
			/*it = ctx.begin;

			while (it != null)
			{
				nk_window _next_ = it.next;
				nk_popup_buffer buf;

				if (it.popup.buf.active == 0) goto skip;
				buf = it.popup.buf;
				cmd.next = buf.begin;
				cmd = ((nk_command*) ((void*) ((buffer) + (buf->last))));
				buf->active = (int) (nk_false);
				skip:
				;
				it = _next_;
			}
			if ((cmd) != null)
			{
				if (ctx.overlay.end != ctx.overlay.begin) cmd->_next_ = (ulong) (ctx.overlay.begin);
				else cmd->_next_ = (ulong) (ctx.memory.allocated);
			}*/
		}



		public static float nk_inv_sqrt(float number)
		{
			var threehalfs = 1.5f;
			var conv = new nk_inv_sqrt_union
			{
				i = 0,
			};
			conv.f = number;
			var x2 = number*0.5f;
			conv.i = 0x5f375A84 - (conv.i >> 1);
			conv.f = conv.f*(threehalfs - (x2*conv.f*conv.f));
			return conv.f;
		}

		public static int nk_utf_decode(char* c, int pos, char* u, int clen)
		{
			*u = c[pos];

			return 1;
		}

		public static int nk_utf_decode(char* c, char* u, int clen)
		{
			return nk_utf_decode(c, 0, u, clen);
		}

		public static int nk_utf_encode(char c, char* u, int clen)
		{
			*u = c;

			return 1;
		}

		public static int nk_utf_len(char* str, int len)
		{
			return len;
		}

		public static void nk_textedit_text(nk_text_edit state, string text, int total_len)
		{
			fixed (char* p = text)
			{
				nk_textedit_text(state, text, total_len);
			}
		}

		public static string nk_style_get_color_by_name(int c)
		{
			return nk_color_names[c];
		}

		public static int nk_init_fixed(nk_context ctx, void* memory, ulong size, nk_user_font font)
		{
			if (memory == null) return 0;
			nk_setup(ctx, font);
			nk_buffer_init_fixed(ctx.memory, memory, size);
			return 1;
		}

		public static void nk_buffer_init_default(nk_buffer buffer)
		{
			nk_buffer_init(buffer, 4*1024);
		}

		public static int nk_init(nk_context ctx, nk_user_font font)
		{
			nk_setup(ctx, font);
			nk_buffer_init(ctx.memory, 4*1024);
			return 1;
		}

		public static void nk_free(nk_context ctx)
		{
			if (ctx == null) return;
			nk_buffer_free(ctx.memory);

			ctx.seq = 0;
			ctx.build = 0;
			ctx.begin = null;
			ctx.end = null;
			ctx.active = null;
			ctx.current = null;
			ctx.count = 0;
		}

		public static nk_table nk_create_table(nk_context ctx)
		{
			var result = new nk_table();

			return result;
		}

		public static nk_window nk_create_window(nk_context ctx)
		{
			var result = new nk_window {seq = ctx.seq};

			return result;
		}

		public static void nk_free_window(nk_context ctx, nk_window win)
		{
			nk_table it = win.tables;
			if (win.popup.win != null)
			{
				nk_free_window(ctx, win.popup.win);
				win.popup.win = null;
			}

			win.next = null;
			win.prev = null;
			while (it != null)
			{
				var n = it.next;
				nk_remove_table(win, it);
				if (it == win.tables) win.tables = n;
				it = n;
			}
		}

		public static nk_panel nk_create_panel(nk_context ctx)
		{
			var result = new nk_panel();

			return result;
		}

		public static void nk_free_panel(nk_context ctx, nk_panel panel)
		{
		}

		public static int nk_popup_begin(nk_context ctx, int type, string title, uint flags, nk_rect rect)
		{
			fixed (char* ptr = title)
			{
				return nk_popup_begin(ctx, type, ptr, flags, rect);
			}
		}

		public static void nk_str_init_default(nk_str str)
		{
			nk_buffer_init(str.buffer, 32);
			str.len = 0;
		}

		public static void nk_str_init(nk_str str, ulong size)
		{
			nk_buffer_init(str.buffer, size);
			str.len = 0;
		}

		public static int nk_init_default(nk_context ctx, nk_user_font font)
		{
			return nk_init(ctx, font);
		}

		public static nk_font nk_font_atlas_add_default(nk_font_atlas atlas, float pixel_height, nk_font_config config)
		{
			fixed (byte* ptr = nk_proggy_clean_ttf_compressed_data_base85)
			{
				return nk_font_atlas_add_compressed_base85(atlas, ptr, pixel_height, config);
			}
		}

		public static void nk_property_(nk_context ctx, char* name, nk_property_variant* variant, float inc_per_pixel,
			int filter)
		{
			var bounds = new nk_rect();
			uint hash;
			char* dummy_buffer = stackalloc char[64];
			var dummy_state = NK_PROPERTY_DEFAULT;
			var dummy_length = 0;
			var dummy_cursor = 0;
			var dummy_select_begin = 0;
			var dummy_select_end = 0;
			if ((ctx == null) || (ctx.current == null) || (ctx.current.layout == null)) return;
			var win = ctx.current;
			var layout = win.layout;
			var style = ctx.style;
			var s = nk_widget(&bounds, ctx);
			if (s == 0) return;
			if (name[0] == '#')
			{
				hash = nk_murmur_hash(name, nk_strlen(name), win.property.seq++);
				name++;
			}
			else hash = nk_murmur_hash(name, nk_strlen(name), 42);

			var _in_ = ((s == NK_WIDGET_ROM) && (win.property.active == 0)) || ((layout.flags & NK_WINDOW_ROM) != 0)
				? null
				: ctx.input;

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

		public static void nk_stroke_polygon(nk_command_buffer b, float* points, int point_count, float line_thickness,
			nk_color col)
		{
			if ((b == null) || (col.a == 0) || (line_thickness <= 0)) return;
			var cmd = (nk_command_polygon) nk_command_buffer_push(b, NK_COMMAND_POLYGON);
			if (cmd == null) return;
			cmd.color = col;
			cmd.line_thickness = (ushort) line_thickness;
			cmd.point_count = (ushort) point_count;
			cmd.points = new nk_vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public static void nk_fill_polygon(nk_command_buffer b, float* points, int point_count, nk_color col)
		{
			nk_command_polygon_filled cmd;
			if ((b == null) || (col.a == 0)) return;
			cmd = (nk_command_polygon_filled) nk_command_buffer_push(b, NK_COMMAND_POLYGON_FILLED);
			if (cmd == null) return;
			cmd.color = col;
			cmd.point_count = (ushort) point_count;
			cmd.points = new nk_vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2 + 0];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public static void nk_stroke_polyline(nk_command_buffer b, float* points, int point_count, float line_thickness,
			nk_color col)
		{
			if ((b == null) || (col.a == 0) || (line_thickness <= 0)) return;
			var cmd = (nk_command_polyline) nk_command_buffer_push(b, NK_COMMAND_POLYLINE);
			if (cmd == null) return;
			cmd.color = col;
			cmd.point_count = (ushort) point_count;
			cmd.line_thickness = (ushort) line_thickness;
			cmd.points = new nk_vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public static nk_font_config nk_font_config_clone(nk_font_config src)
		{
			return new nk_font_config
			{
				next = src.next,
				ttf_blob = src.ttf_blob,
				ttf_size = src.ttf_size,
				ttf_data_owned_by_atlas = src.ttf_data_owned_by_atlas,
				merge_mode = src.merge_mode,
				pixel_snap = src.pixel_snap,
				oversample_v = src.oversample_v,
				oversample_h = src.oversample_h,
				padding = src.padding,
				size = src.size,
				coord_type = src.coord_type,
				spacing = src.spacing,
				range = src.range,
				font = src.font,
				fallback_glyph = src.fallback_glyph,
				n = src.n,
				p = src.p,
			};
		}
	}
}