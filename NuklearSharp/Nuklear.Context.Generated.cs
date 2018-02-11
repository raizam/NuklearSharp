using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Nuklear
	{
		public static uint nk_convert(nk_context ctx, NkBuffer<nk_draw_command> cmds, NkBuffer<byte> vertices,
			NkBuffer<ushort> elements, nk_convert_config config)
		{
			uint res = (uint) (NK_CONVERT_SUCCESS);

			if ((((((ctx == null) || (cmds == null)) || (vertices == null)) || (elements == null)) || (config == null)) ||
			    (config.vertex_layout == null)) return (uint) (NK_CONVERT_INVALID_PARAM);
			nk_draw_list_setup(ctx.draw_list, config, cmds, vertices, elements, (int) (config.line_AA), (int) (config.shape_AA));
			var top_window = nk__begin(ctx);

			int cnt = 0;
			for (var cmd = top_window.buffer.first; cmd != null; cmd = cmd.next)
			{
				switch (cmd.header.type)
				{
					case NK_COMMAND_NOP:
						break;
					case NK_COMMAND_SCISSOR:
					{
						nk_command_scissor s = (nk_command_scissor) (cmd);
						nk_draw_list_add_clip(ctx.draw_list,
							(nk_rect) (nk_rect_((float) (s.x), (float) (s.y), (float) (s.w), (float) (s.h))));
					}
						break;
					case NK_COMMAND_LINE:
					{
						nk_command_line l = (nk_command_line) (cmd);
						nk_draw_list_stroke_line(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (l.begin.x), (float) (l.begin.y))),
							(nk_vec2) (nk_vec2_((float) (l.end.x), (float) (l.end.y))), (nk_color) (l.color), (float) (l.line_thickness));
					}
						break;
					case NK_COMMAND_CURVE:
					{
						nk_command_curve q = (nk_command_curve) (cmd);
						nk_draw_list_stroke_curve(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (q.begin.x), (float) (q.begin.y))),
							(nk_vec2) (nk_vec2_((float) (q.ctrl_0.x), (float) (q.ctrl_0.y))),
							(nk_vec2) (nk_vec2_((float) (q.ctrl_1.x), (float) (q.ctrl_1.y))),
							(nk_vec2) (nk_vec2_((float) (q.end.x), (float) (q.end.y))), (nk_color) (q.color),
							(uint) (config.curve_segment_count), (float) (q.line_thickness));
					}
						break;
					case NK_COMMAND_RECT:
					{
						nk_command_rect r = (nk_command_rect) (cmd);
						nk_draw_list_stroke_rect(ctx.draw_list,
							(nk_rect) (nk_rect_((float) (r.x), (float) (r.y), (float) (r.w), (float) (r.h))), (nk_color) (r.color),
							(float) (r.rounding), (float) (r.line_thickness));
					}
						break;
					case NK_COMMAND_RECT_FILLED:
					{
						nk_command_rect_filled r = (nk_command_rect_filled) (cmd);
						nk_draw_list_fill_rect(ctx.draw_list,
							(nk_rect) (nk_rect_((float) (r.x), (float) (r.y), (float) (r.w), (float) (r.h))), (nk_color) (r.color),
							(float) (r.rounding));
					}
						break;
					case NK_COMMAND_RECT_MULTI_COLOR:
					{
						nk_command_rect_multi_color r = (nk_command_rect_multi_color) (cmd);
						nk_draw_list_fill_rect_multi_color(ctx.draw_list,
							(nk_rect) (nk_rect_((float) (r.x), (float) (r.y), (float) (r.w), (float) (r.h))), (nk_color) (r.left),
							(nk_color) (r.top), (nk_color) (r.right), (nk_color) (r.bottom));
					}
						break;
					case NK_COMMAND_CIRCLE:
					{
						nk_command_circle c = (nk_command_circle) (cmd);
						nk_draw_list_stroke_circle(ctx.draw_list,
							(nk_vec2) (nk_vec2_((float) ((float) (c.x) + (float) (c.w)/2), (float) ((float) (c.y) + (float) (c.h)/2))),
							(float) ((float) (c.w)/2), (nk_color) (c.color), (uint) (config.circle_segment_count), (float) (c.line_thickness));
					}
						break;
					case NK_COMMAND_CIRCLE_FILLED:
					{
						nk_command_circle_filled c = (nk_command_circle_filled) (cmd);
						nk_draw_list_fill_circle(ctx.draw_list,
							(nk_vec2) (nk_vec2_((float) ((float) (c.x) + (float) (c.w)/2), (float) ((float) (c.y) + (float) (c.h)/2))),
							(float) ((float) (c.w)/2), (nk_color) (c.color), (uint) (config.circle_segment_count));
					}
						break;
					case NK_COMMAND_ARC:
					{
						nk_command_arc c = (nk_command_arc) (cmd);
						nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (c.cx), (float) (c.cy))));
						nk_draw_list_path_arc_to(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (c.cx), (float) (c.cy))), (float) (c.r),
							(float) (c.a[0]), (float) (c.a[1]), (uint) (config.arc_segment_count));
						nk_draw_list_path_stroke(ctx.draw_list, (nk_color) (c.color), (int) (NK_STROKE_CLOSED), (float) (c.line_thickness));
					}
						break;
					case NK_COMMAND_ARC_FILLED:
					{
						nk_command_arc_filled c = (nk_command_arc_filled) (cmd);
						nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (c.cx), (float) (c.cy))));
						nk_draw_list_path_arc_to(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (c.cx), (float) (c.cy))), (float) (c.r),
							(float) (c.a[0]), (float) (c.a[1]), (uint) (config.arc_segment_count));
						nk_draw_list_path_fill(ctx.draw_list, (nk_color) (c.color));
					}
						break;
					case NK_COMMAND_TRIANGLE:
					{
						nk_command_triangle t = (nk_command_triangle) (cmd);
						nk_draw_list_stroke_triangle(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (t.a.x), (float) (t.a.y))),
							(nk_vec2) (nk_vec2_((float) (t.b.x), (float) (t.b.y))), (nk_vec2) (nk_vec2_((float) (t.c.x), (float) (t.c.y))),
							(nk_color) (t.color), (float) (t.line_thickness));
					}
						break;
					case NK_COMMAND_TRIANGLE_FILLED:
					{
						nk_command_triangle_filled t = (nk_command_triangle_filled) (cmd);
						nk_draw_list_fill_triangle(ctx.draw_list, (nk_vec2) (nk_vec2_((float) (t.a.x), (float) (t.a.y))),
							(nk_vec2) (nk_vec2_((float) (t.b.x), (float) (t.b.y))), (nk_vec2) (nk_vec2_((float) (t.c.x), (float) (t.c.y))),
							(nk_color) (t.color));
					}
						break;
					case NK_COMMAND_POLYGON:
					{
						int i;
						nk_command_polygon p = (nk_command_polygon) (cmd);
						for (i = (int) (0); (i) < (p.point_count); ++i)
						{
							nk_vec2 pnt = (nk_vec2) (nk_vec2_((float) (p.points[i].x), (float) (p.points[i].y)));
							nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2) (pnt));
						}
						nk_draw_list_path_stroke(ctx.draw_list, (nk_color) (p.color), (int) (NK_STROKE_CLOSED), (float) (p.line_thickness));
					}
						break;
					case NK_COMMAND_POLYGON_FILLED:
					{
						int i;
						nk_command_polygon_filled p = (nk_command_polygon_filled) (cmd);
						for (i = (int) (0); (i) < (p.point_count); ++i)
						{
							nk_vec2 pnt = (nk_vec2) (nk_vec2_((float) (p.points[i].x), (float) (p.points[i].y)));
							nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2) (pnt));
						}
						nk_draw_list_path_fill(ctx.draw_list, (nk_color) (p.color));
					}
						break;
					case NK_COMMAND_POLYLINE:
					{
						int i;
						nk_command_polyline p = (nk_command_polyline) (cmd);
						for (i = (int) (0); (i) < (p.point_count); ++i)
						{
							nk_vec2 pnt = (nk_vec2) (nk_vec2_((float) (p.points[i].x), (float) (p.points[i].y)));
							nk_draw_list_path_line_to(ctx.draw_list, (nk_vec2) (pnt));
						}
						nk_draw_list_path_stroke(ctx.draw_list, (nk_color) (p.color), (int) (NK_STROKE_OPEN), (float) (p.line_thickness));
					}
						break;
					case NK_COMMAND_TEXT:
					{
						nk_command_text t = (nk_command_text) (cmd);
						nk_draw_list_add_text(ctx.draw_list, t.font,
							(nk_rect) (nk_rect_((float) (t.x), (float) (t.y), (float) (t.w), (float) (t.h))), t._string_, (int) (t.length),
							(float) (t.height), (nk_color) (t.foreground));
					}
						break;
					case NK_COMMAND_IMAGE:
					{
						nk_command_image i = (nk_command_image) (cmd);
						nk_draw_list_add_image(ctx.draw_list, (nk_image) (i.img),
							(nk_rect) (nk_rect_((float) (i.x), (float) (i.y), (float) (i.w), (float) (i.h))), (nk_color) (i.col));
					}
						break;
					case NK_COMMAND_CUSTOM:
					{
						nk_command_custom c = (nk_command_custom) (cmd);
						c.callback(ctx.draw_list, (short) (c.x), (short) (c.y), (ushort) (c.w), (ushort) (c.h),
							(nk_handle) (c.callback_data));
					}
						break;
					default:
						break;
				}
				++cnt;
			}

			return res;
		}

		public static void nk_input_begin(nk_context ctx)
		{
			int i;
			nk_input _in_;
			if (ctx == null) return;
			_in_ = ctx.input;
			for (i = (int) (0); (i) < (NK_BUTTON_MAX); ++i)
			{
				((nk_mouse_button*) _in_.mouse.buttons + i)->clicked = (uint) (0);
			}
			_in_.keyboard.text_len = (int) (0);
			_in_.mouse.scroll_delta = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			_in_.mouse.prev.x = (float) (_in_.mouse.pos.x);
			_in_.mouse.prev.y = (float) (_in_.mouse.pos.y);
			_in_.mouse.delta.x = (float) (0);
			_in_.mouse.delta.y = (float) (0);
			for (i = (int) (0); (i) < (NK_KEY_MAX); i++)
			{
				((nk_key*) _in_.keyboard.keys + i)->clicked = (uint) (0);
			}
		}

		public static void nk_input_end(nk_context ctx)
		{
			nk_input _in_;
			if (ctx == null) return;
			_in_ = ctx.input;
			if ((_in_.mouse.grab) != 0) _in_.mouse.grab = (byte) (0);
			if ((_in_.mouse.ungrab) != 0)
			{
				_in_.mouse.grabbed = (byte) (0);
				_in_.mouse.ungrab = (byte) (0);
				_in_.mouse.grab = (byte) (0);
			}

		}

		public static void nk_input_motion(nk_context ctx, int x, int y)
		{
			nk_input _in_;
			if (ctx == null) return;
			_in_ = ctx.input;
			_in_.mouse.pos.x = ((float) (x));
			_in_.mouse.pos.y = ((float) (y));
			_in_.mouse.delta.x = (float) (_in_.mouse.pos.x - _in_.mouse.prev.x);
			_in_.mouse.delta.y = (float) (_in_.mouse.pos.y - _in_.mouse.prev.y);
		}

		public static void nk_input_key(nk_context ctx, int key, int down)
		{
			nk_input _in_;
			if (ctx == null) return;
			_in_ = ctx.input;
			if (((nk_key*) _in_.keyboard.keys + key)->down != down) ((nk_key*) _in_.keyboard.keys + key)->clicked++;
			((nk_key*) _in_.keyboard.keys + key)->down = (int) (down);
		}

		public static void nk_input_button(nk_context ctx, int id, int x, int y, int down)
		{
			nk_mouse_button* btn;
			nk_input _in_;
			if (ctx == null) return;
			_in_ = ctx.input;
			if ((_in_.mouse.buttons[id].down) == (down)) return;
			btn = (nk_mouse_button*) _in_.mouse.buttons + id;
			btn->clicked_pos.x = ((float) (x));
			btn->clicked_pos.y = ((float) (y));
			btn->down = (int) (down);
			btn->clicked++;
		}

		public static void nk_input_scroll(nk_context ctx, nk_vec2 val)
		{
			if (ctx == null) return;
			ctx.input.mouse.scroll_delta.x += (float) (val.x);
			ctx.input.mouse.scroll_delta.y += (float) (val.y);
		}

		public static void nk_input_glyph(nk_context ctx, char* glyph)
		{
			int len = (int) (0);
			char unicode;
			nk_input _in_;
			if (ctx == null) return;
			_in_ = ctx.input;
			len = (int) (nk_utf_decode(glyph, &unicode, (int) (4)));
			if (((len) != 0) && ((_in_.keyboard.text_len + len) < (16)))
			{
				nk_utf_encode(unicode, (char*) _in_.keyboard.text + _in_.keyboard.text_len, (int) (16 - _in_.keyboard.text_len));
				_in_.keyboard.text_len += (int) (len);
			}

		}

		public static void nk_input_char(nk_context ctx, char c)
		{
			char* glyph = stackalloc char[4];
			if (ctx == null) return;
			glyph[0] = c;
			nk_input_glyph(ctx, glyph);
		}

		public static void nk_input_unicode(nk_context ctx, char unicode)
		{
			char* rune = stackalloc char[4];
			if (ctx == null) return;
			nk_utf_encode(unicode, rune, (int) (4));
			nk_input_glyph(ctx, rune);
		}

		public static void nk_style_default(nk_context ctx)
		{
			nk_style_from_table(ctx, null);
		}

		public static void nk_style_from_table(nk_context ctx, nk_color[] table)
		{
			nk_style style;
			nk_style_text text;
			nk_style_button button;
			nk_style_toggle toggle;
			nk_style_selectable select;
			nk_style_slider slider;
			nk_style_progress prog;
			nk_style_scrollbar scroll;
			nk_style_edit edit;
			nk_style_property property;
			nk_style_combo combo;
			nk_style_chart chart;
			nk_style_tab tab;
			nk_style_window win;
			if (ctx == null) return;
			style = ctx.style;
			table = (table == null) ? nk_default_color_style : table;
			text = style.text;
			text.color = (nk_color) (table[NK_COLOR_TEXT]);
			text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			button = style.button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_BUTTON])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_BUTTON_HOVER])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_BUTTON_ACTIVE])));
			button.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			button.text_background = (nk_color) (table[NK_COLOR_BUTTON]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			button.image_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (1.0f);
			button.rounding = (float) (4.0f);
			button.draw_begin = null;
			button.draw_end = null;
			button = style.contextual_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_BUTTON_HOVER])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_BUTTON_ACTIVE])));
			button.border_color = (nk_color) (table[NK_COLOR_WINDOW]);
			button.text_background = (nk_color) (table[NK_COLOR_WINDOW]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			button = style.menu_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.border_color = (nk_color) (table[NK_COLOR_WINDOW]);
			button.text_background = (nk_color) (table[NK_COLOR_WINDOW]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (1.0f);
			button.draw_begin = null;
			button.draw_end = null;
			toggle = style.checkbox;

			toggle.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE])));
			toggle.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_HOVER])));
			toggle.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_HOVER])));
			toggle.cursor_normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_CURSOR])));
			toggle.cursor_hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_CURSOR])));
			toggle.userdata = (nk_handle) (nk_handle_ptr(null));
			toggle.text_background = (nk_color) (table[NK_COLOR_WINDOW]);
			toggle.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			toggle.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			toggle.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			toggle.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			toggle.touch_padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			toggle.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			toggle.border = (float) (0.0f);
			toggle.spacing = (float) (4);
			toggle = style.option;

			toggle.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE])));
			toggle.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_HOVER])));
			toggle.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_HOVER])));
			toggle.cursor_normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_CURSOR])));
			toggle.cursor_hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TOGGLE_CURSOR])));
			toggle.userdata = (nk_handle) (nk_handle_ptr(null));
			toggle.text_background = (nk_color) (table[NK_COLOR_WINDOW]);
			toggle.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			toggle.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			toggle.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			toggle.padding = (nk_vec2) (nk_vec2_((float) (3.0f), (float) (3.0f)));
			toggle.touch_padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			toggle.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			toggle.border = (float) (0.0f);
			toggle.spacing = (float) (4);
			select = style.selectable;

			select.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SELECT])));
			select.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SELECT])));
			select.pressed = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SELECT])));
			select.normal_active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SELECT_ACTIVE])));
			select.hover_active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SELECT_ACTIVE])));
			select.pressed_active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SELECT_ACTIVE])));
			select.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			select.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			select.text_pressed = (nk_color) (table[NK_COLOR_TEXT]);
			select.text_normal_active = (nk_color) (table[NK_COLOR_TEXT]);
			select.text_hover_active = (nk_color) (table[NK_COLOR_TEXT]);
			select.text_pressed_active = (nk_color) (table[NK_COLOR_TEXT]);
			select.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			select.touch_padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			select.userdata = (nk_handle) (nk_handle_ptr(null));
			select.rounding = (float) (0.0f);
			select.draw_begin = null;
			select.draw_end = null;
			slider = style.slider;

			slider.normal = (nk_style_item) (nk_style_item_hide());
			slider.hover = (nk_style_item) (nk_style_item_hide());
			slider.active = (nk_style_item) (nk_style_item_hide());
			slider.bar_normal = (nk_color) (table[NK_COLOR_SLIDER]);
			slider.bar_hover = (nk_color) (table[NK_COLOR_SLIDER]);
			slider.bar_active = (nk_color) (table[NK_COLOR_SLIDER]);
			slider.bar_filled = (nk_color) (table[NK_COLOR_SLIDER_CURSOR]);
			slider.cursor_normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER_CURSOR])));
			slider.cursor_hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER_CURSOR_HOVER])));
			slider.cursor_active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER_CURSOR_ACTIVE])));
			slider.inc_symbol = (int) (NK_SYMBOL_TRIANGLE_RIGHT);
			slider.dec_symbol = (int) (NK_SYMBOL_TRIANGLE_LEFT);
			slider.cursor_size = (nk_vec2) (nk_vec2_((float) (16), (float) (16)));
			slider.padding = (nk_vec2) (nk_vec2_((float) (2), (float) (2)));
			slider.spacing = (nk_vec2) (nk_vec2_((float) (2), (float) (2)));
			slider.userdata = (nk_handle) (nk_handle_ptr(null));
			slider.show_buttons = (int) (nk_false);
			slider.bar_height = (float) (8);
			slider.rounding = (float) (0);
			slider.draw_begin = null;
			slider.draw_end = null;
			button = style.slider.inc_button;
			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (nk_rgb((int) (40), (int) (40), (int) (40)))));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (nk_rgb((int) (42), (int) (42), (int) (42)))));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (nk_rgb((int) (44), (int) (44), (int) (44)))));
			button.border_color = (nk_color) (nk_rgb((int) (65), (int) (65), (int) (65)));
			button.text_background = (nk_color) (nk_rgb((int) (40), (int) (40), (int) (40)));
			button.text_normal = (nk_color) (nk_rgb((int) (175), (int) (175), (int) (175)));
			button.text_hover = (nk_color) (nk_rgb((int) (175), (int) (175), (int) (175)));
			button.text_active = (nk_color) (nk_rgb((int) (175), (int) (175), (int) (175)));
			button.padding = (nk_vec2) (nk_vec2_((float) (8.0f), (float) (8.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (1.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.slider.dec_button = (nk_style_button) (style.slider.inc_button);
			prog = style.progress;

			prog.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER])));
			prog.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER])));
			prog.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER])));
			prog.cursor_normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER_CURSOR])));
			prog.cursor_hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER_CURSOR_HOVER])));
			prog.cursor_active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SLIDER_CURSOR_ACTIVE])));
			prog.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			prog.cursor_border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			prog.userdata = (nk_handle) (nk_handle_ptr(null));
			prog.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			prog.rounding = (float) (0);
			prog.border = (float) (0);
			prog.cursor_rounding = (float) (0);
			prog.cursor_border = (float) (0);
			prog.draw_begin = null;
			prog.draw_end = null;
			scroll = style.scrollh;

			scroll.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SCROLLBAR])));
			scroll.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SCROLLBAR])));
			scroll.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SCROLLBAR])));
			scroll.cursor_normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SCROLLBAR_CURSOR])));
			scroll.cursor_hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SCROLLBAR_CURSOR_HOVER])));
			scroll.cursor_active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_SCROLLBAR_CURSOR_ACTIVE])));
			scroll.dec_symbol = (int) (NK_SYMBOL_CIRCLE_SOLID);
			scroll.inc_symbol = (int) (NK_SYMBOL_CIRCLE_SOLID);
			scroll.userdata = (nk_handle) (nk_handle_ptr(null));
			scroll.border_color = (nk_color) (table[NK_COLOR_SCROLLBAR]);
			scroll.cursor_border_color = (nk_color) (table[NK_COLOR_SCROLLBAR]);
			scroll.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			scroll.show_buttons = (int) (nk_false);
			scroll.border = (float) (0);
			scroll.rounding = (float) (0);
			scroll.border_cursor = (float) (0);
			scroll.rounding_cursor = (float) (0);
			scroll.draw_begin = null;
			scroll.draw_end = null;
			style.scrollv = (nk_style_scrollbar) (style.scrollh);
			button = style.scrollh.inc_button;
			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (nk_rgb((int) (40), (int) (40), (int) (40)))));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (nk_rgb((int) (42), (int) (42), (int) (42)))));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (nk_rgb((int) (44), (int) (44), (int) (44)))));
			button.border_color = (nk_color) (nk_rgb((int) (65), (int) (65), (int) (65)));
			button.text_background = (nk_color) (nk_rgb((int) (40), (int) (40), (int) (40)));
			button.text_normal = (nk_color) (nk_rgb((int) (175), (int) (175), (int) (175)));
			button.text_hover = (nk_color) (nk_rgb((int) (175), (int) (175), (int) (175)));
			button.text_active = (nk_color) (nk_rgb((int) (175), (int) (175), (int) (175)));
			button.padding = (nk_vec2) (nk_vec2_((float) (4.0f), (float) (4.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (1.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.scrollh.dec_button = (nk_style_button) (style.scrollh.inc_button);
			style.scrollv.inc_button = (nk_style_button) (style.scrollh.inc_button);
			style.scrollv.dec_button = (nk_style_button) (style.scrollh.inc_button);
			edit = style.edit;

			edit.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_EDIT])));
			edit.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_EDIT])));
			edit.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_EDIT])));
			edit.cursor_normal = (nk_color) (table[NK_COLOR_TEXT]);
			edit.cursor_hover = (nk_color) (table[NK_COLOR_TEXT]);
			edit.cursor_text_normal = (nk_color) (table[NK_COLOR_EDIT]);
			edit.cursor_text_hover = (nk_color) (table[NK_COLOR_EDIT]);
			edit.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			edit.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			edit.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			edit.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			edit.selected_normal = (nk_color) (table[NK_COLOR_TEXT]);
			edit.selected_hover = (nk_color) (table[NK_COLOR_TEXT]);
			edit.selected_text_normal = (nk_color) (table[NK_COLOR_EDIT]);
			edit.selected_text_hover = (nk_color) (table[NK_COLOR_EDIT]);
			edit.scrollbar_size = (nk_vec2) (nk_vec2_((float) (10), (float) (10)));
			edit.scrollbar = (nk_style_scrollbar) (style.scrollv);
			edit.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			edit.row_padding = (float) (2);
			edit.cursor_size = (float) (4);
			edit.border = (float) (1);
			edit.rounding = (float) (0);
			property = style.property;

			property.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			property.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			property.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			property.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			property.label_normal = (nk_color) (table[NK_COLOR_TEXT]);
			property.label_hover = (nk_color) (table[NK_COLOR_TEXT]);
			property.label_active = (nk_color) (table[NK_COLOR_TEXT]);
			property.sym_left = (int) (NK_SYMBOL_TRIANGLE_LEFT);
			property.sym_right = (int) (NK_SYMBOL_TRIANGLE_RIGHT);
			property.userdata = (nk_handle) (nk_handle_ptr(null));
			property.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			property.border = (float) (1);
			property.rounding = (float) (10);
			property.draw_begin = null;
			property.draw_end = null;
			button = style.property.dec_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			button.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (nk_color) (table[NK_COLOR_PROPERTY]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.property.inc_button = (nk_style_button) (style.property.dec_button);
			edit = style.property.edit;

			edit.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			edit.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			edit.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_PROPERTY])));
			edit.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			edit.cursor_normal = (nk_color) (table[NK_COLOR_TEXT]);
			edit.cursor_hover = (nk_color) (table[NK_COLOR_TEXT]);
			edit.cursor_text_normal = (nk_color) (table[NK_COLOR_EDIT]);
			edit.cursor_text_hover = (nk_color) (table[NK_COLOR_EDIT]);
			edit.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			edit.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			edit.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			edit.selected_normal = (nk_color) (table[NK_COLOR_TEXT]);
			edit.selected_hover = (nk_color) (table[NK_COLOR_TEXT]);
			edit.selected_text_normal = (nk_color) (table[NK_COLOR_EDIT]);
			edit.selected_text_hover = (nk_color) (table[NK_COLOR_EDIT]);
			edit.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			edit.cursor_size = (float) (8);
			edit.border = (float) (0);
			edit.rounding = (float) (0);
			chart = style.chart;

			chart.background = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_CHART])));
			chart.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			chart.selected_color = (nk_color) (table[NK_COLOR_CHART_COLOR_HIGHLIGHT]);
			chart.color = (nk_color) (table[NK_COLOR_CHART_COLOR]);
			chart.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			chart.border = (float) (0);
			chart.rounding = (float) (0);
			combo = style.combo;
			combo.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_COMBO])));
			combo.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_COMBO])));
			combo.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_COMBO])));
			combo.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			combo.label_normal = (nk_color) (table[NK_COLOR_TEXT]);
			combo.label_hover = (nk_color) (table[NK_COLOR_TEXT]);
			combo.label_active = (nk_color) (table[NK_COLOR_TEXT]);
			combo.sym_normal = (int) (NK_SYMBOL_TRIANGLE_DOWN);
			combo.sym_hover = (int) (NK_SYMBOL_TRIANGLE_DOWN);
			combo.sym_active = (int) (NK_SYMBOL_TRIANGLE_DOWN);
			combo.content_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			combo.button_padding = (nk_vec2) (nk_vec2_((float) (0), (float) (4)));
			combo.spacing = (nk_vec2) (nk_vec2_((float) (4), (float) (0)));
			combo.border = (float) (1);
			combo.rounding = (float) (0);
			button = style.combo.button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_COMBO])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_COMBO])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_COMBO])));
			button.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (nk_color) (table[NK_COLOR_COMBO]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			tab = style.tab;
			tab.background = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TAB_HEADER])));
			tab.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			tab.text = (nk_color) (table[NK_COLOR_TEXT]);
			tab.sym_minimize = (int) (NK_SYMBOL_TRIANGLE_RIGHT);
			tab.sym_maximize = (int) (NK_SYMBOL_TRIANGLE_DOWN);
			tab.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			tab.spacing = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			tab.indent = (float) (10.0f);
			tab.border = (float) (1);
			tab.rounding = (float) (0);
			button = style.tab.tab_minimize_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TAB_HEADER])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TAB_HEADER])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TAB_HEADER])));
			button.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (nk_color) (table[NK_COLOR_TAB_HEADER]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.tab.tab_maximize_button = (nk_style_button) (button);
			button = style.tab.node_minimize_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			button.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (nk_color) (table[NK_COLOR_TAB_HEADER]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (2.0f), (float) (2.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			style.tab.node_maximize_button = (nk_style_button) (button);
			win = style.window;
			win.header.align = (int) (NK_HEADER_RIGHT);
			win.header.close_symbol = (int) (NK_SYMBOL_X);
			win.header.minimize_symbol = (int) (NK_SYMBOL_MINUS);
			win.header.maximize_symbol = (int) (NK_SYMBOL_PLUS);
			win.header.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			win.header.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			win.header.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			win.header.label_normal = (nk_color) (table[NK_COLOR_TEXT]);
			win.header.label_hover = (nk_color) (table[NK_COLOR_TEXT]);
			win.header.label_active = (nk_color) (table[NK_COLOR_TEXT]);
			win.header.label_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.header.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.header.spacing = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			button = style.window.header.close_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			button.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (nk_color) (table[NK_COLOR_HEADER]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			button = style.window.header.minimize_button;

			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_HEADER])));
			button.border_color = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
			button.text_background = (nk_color) (table[NK_COLOR_HEADER]);
			button.text_normal = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_hover = (nk_color) (table[NK_COLOR_TEXT]);
			button.text_active = (nk_color) (table[NK_COLOR_TEXT]);
			button.padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.touch_padding = (nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f)));
			button.userdata = (nk_handle) (nk_handle_ptr(null));
			button.text_alignment = (uint) (NK_TEXT_CENTERED);
			button.border = (float) (0.0f);
			button.rounding = (float) (0.0f);
			button.draw_begin = null;
			button.draw_end = null;
			win.background = (nk_color) (table[NK_COLOR_WINDOW]);
			win.fixed_background = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_WINDOW])));
			win.border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.popup_border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.combo_border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.contextual_border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.menu_border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.group_border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.tooltip_border_color = (nk_color) (table[NK_COLOR_BORDER]);
			win.scaler = (nk_style_item) (nk_style_item_color((nk_color) (table[NK_COLOR_TEXT])));
			win.rounding = (float) (0.0f);
			win.spacing = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.scrollbar_size = (nk_vec2) (nk_vec2_((float) (10), (float) (10)));
			win.min_size = (nk_vec2) (nk_vec2_((float) (64), (float) (64)));
			win.combo_border = (float) (1.0f);
			win.contextual_border = (float) (1.0f);
			win.menu_border = (float) (1.0f);
			win.group_border = (float) (1.0f);
			win.tooltip_border = (float) (1.0f);
			win.popup_border = (float) (1.0f);
			win.border = (float) (2.0f);
			win.min_row_height_padding = (float) (8);
			win.padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.group_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.popup_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.combo_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.contextual_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.menu_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
			win.tooltip_padding = (nk_vec2) (nk_vec2_((float) (4), (float) (4)));
		}

		public static void nk_style_set_font(nk_context ctx, nk_user_font font)
		{
			nk_style style;
			if (ctx == null) return;
			style = ctx.style;
			style.font = font;
			ctx.stacks.fonts.head = (int) (0);
			if ((ctx.current) != null) nk_layout_reset_min_row_height(ctx);
		}

		public static int nk_style_push_font(nk_context ctx, nk_user_font font)
		{
			nk_config_stack_user_font font_stack;
			nk_config_stack_user_font_element element;
			if (ctx == null) return (int) (0);
			font_stack = ctx.stacks.fonts;
			if ((font_stack.head) >= (int) font_stack.elements.Length) return (int) (0);
			element = font_stack.elements[font_stack.head++];
			element.address = ctx.style.font;
			element.old_value = ctx.style.font;
			ctx.style.font = font;
			return (int) (1);
		}

		public static int nk_style_pop_font(nk_context ctx)
		{
			nk_config_stack_user_font font_stack;
			nk_config_stack_user_font_element element;
			if (ctx == null) return (int) (0);
			font_stack = ctx.stacks.fonts;
			if ((font_stack.head) < (1)) return (int) (0);
			element = font_stack.elements[--font_stack.head];
			element.address = element.old_value;
			return (int) (1);
		}

		public static int nk_style_push_style_item(nk_context ctx, nk_style_item address, nk_style_item value)
		{
			nk_config_stack_style_item type_stack;
			nk_config_stack_style_item_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.style_items;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (nk_style_item) (address);
			address = (nk_style_item) (value);
			return (int) (1);
		}

		public static int nk_style_push_float(nk_context ctx, float* address, float value)
		{
			nk_config_stack_float type_stack;
			nk_config_stack_float_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.floats;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (float) (*address);
			*address = (float) (value);
			return (int) (1);
		}

		public static int nk_style_push_vec2(nk_context ctx, nk_vec2* address, nk_vec2 value)
		{
			nk_config_stack_vec2 type_stack;
			nk_config_stack_vec2_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.vectors;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (nk_vec2) (*address);
			*address = (nk_vec2) (value);
			return (int) (1);
		}

		public static int nk_style_push_flags(nk_context ctx, uint* address, uint value)
		{
			nk_config_stack_flags type_stack;
			nk_config_stack_flags_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.flags;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (uint) (*address);
			*address = (uint) (value);
			return (int) (1);
		}

		public static int nk_style_push_color(nk_context ctx, nk_color* address, nk_color value)
		{
			nk_config_stack_color type_stack;
			nk_config_stack_color_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.colors;
			if ((type_stack.head) >= (int) type_stack.elements.Length) return (int) (0);
			element = type_stack.elements[(type_stack.head++)];
			element.address = address;
			element.old_value = (nk_color) (*address);
			*address = (nk_color) (value);
			return (int) (1);
		}

		public static int nk_style_pop_style_item(nk_context ctx)
		{
			nk_config_stack_style_item type_stack;
			nk_config_stack_style_item_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.style_items;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			element.address = (nk_style_item) (element.old_value);
			return (int) (1);
		}

		public static int nk_style_pop_float(nk_context ctx)
		{
			nk_config_stack_float type_stack;
			nk_config_stack_float_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.floats;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (float) (element.old_value);
			return (int) (1);
		}

		public static int nk_style_pop_vec2(nk_context ctx)
		{
			nk_config_stack_vec2 type_stack;
			nk_config_stack_vec2_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.vectors;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (nk_vec2) (element.old_value);
			return (int) (1);
		}

		public static int nk_style_pop_flags(nk_context ctx)
		{
			nk_config_stack_flags type_stack;
			nk_config_stack_flags_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.flags;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (uint) (element.old_value);
			return (int) (1);
		}

		public static int nk_style_pop_color(nk_context ctx)
		{
			nk_config_stack_color type_stack;
			nk_config_stack_color_element element;
			if (ctx == null) return (int) (0);
			type_stack = ctx.stacks.colors;
			if ((type_stack.head) < (1)) return (int) (0);
			element = type_stack.elements[(--type_stack.head)];
			*element.address = (nk_color) (element.old_value);
			return (int) (1);
		}

		public static int nk_style_set_cursor(nk_context ctx, int c)
		{
			nk_style style;
			if (ctx == null) return (int) (0);
			style = ctx.style;
			if ((style.cursors[c]) != null)
			{
				style.cursor_active = style.cursors[c];
				return (int) (1);
			}

			return (int) (0);
		}

		public static void nk_style_show_cursor(nk_context ctx)
		{
			ctx.style.cursor_visible = (int) (nk_true);
		}

		public static void nk_style_hide_cursor(nk_context ctx)
		{
			ctx.style.cursor_visible = (int) (nk_false);
		}

		public static void nk_style_load_cursor(nk_context ctx, int cursor, nk_cursor c)
		{
			nk_style style;
			if (ctx == null) return;
			style = ctx.style;
			style.cursors[cursor] = c;
		}

		public static void nk_style_load_all_cursors(nk_context ctx, nk_cursor[] cursors)
		{
			int i = (int) (0);
			nk_style style;
			if (ctx == null) return;
			style = ctx.style;
			for (i = (int) (0); (i) < (NK_CURSOR_COUNT); ++i)
			{
				style.cursors[i] = cursors[i];
			}
			style.cursor_visible = (int) (nk_true);
		}

		public static void nk_setup(nk_context ctx, nk_user_font font)
		{
			if (ctx == null) return;

			nk_style_default(ctx);
			ctx.seq = (uint) (1);
			if ((font) != null) ctx.style.font = font;
			nk_draw_list_init(ctx.draw_list);
		}

		public static void nk_clear(nk_context ctx)
		{
			nk_window iter;
			nk_window next;
			if (ctx == null) return;
			ctx.build = (int) (0);
			ctx.last_widget_state = (uint) (0);
			ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_ARROW];

			nk_draw_list_clear(ctx.draw_list);
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((((iter.flags & NK_WINDOW_MINIMIZED) != 0) && ((iter.flags & NK_WINDOW_CLOSED) == 0)) &&
				    ((iter.seq) == (ctx.seq)))
				{
					iter = iter.next;
					continue;
				}
				if ((((iter.flags & NK_WINDOW_HIDDEN) != 0) || ((iter.flags & NK_WINDOW_CLOSED) != 0)) && ((iter) == (ctx.active)))
				{
					ctx.active = iter.prev;
					ctx.end = iter.prev;
					if ((ctx.active) != null) ctx.active.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
				}
				if (((iter.popup.win) != null) && (iter.popup.win.seq != ctx.seq))
				{
					nk_free_window(ctx, iter.popup.win);
					iter.popup.win = null;
				}
				{
					nk_table n;
					nk_table it = iter.tables;
					while ((it) != null)
					{
						n = it.next;
						if (it.seq != ctx.seq)
						{
							nk_remove_table(iter, it);
							if ((it) == (iter.tables)) iter.tables = n;
						}
						it = n;
					}
				}
				if ((iter.seq != ctx.seq) || ((iter.flags & NK_WINDOW_CLOSED) != 0))
				{
					next = iter.next;
					nk_remove_window(ctx, iter);
					nk_free_window(ctx, iter);
					iter = next;
				}
				else iter = iter.next;
			}
			ctx.seq++;
		}

		public static void nk_start_buffer(nk_context ctx, nk_command_buffer buffer)
		{
			if ((ctx == null) || (buffer == null)) return;

			buffer.first = buffer.last = null;
			buffer.count = 0;
			buffer.clip = (nk_rect) (nk_null_rect);
		}

		public static void nk_start(nk_context ctx, nk_window win)
		{
			nk_start_buffer(ctx, win.buffer);
		}

		public static void nk_start_popup(nk_context ctx, nk_window win)
		{
			if ((ctx == null) || (win == null)) return;

			var buf = win.popup.buf.buffer;

			buf.first = buf.last = null;
			buf.count = 0;

			win.popup.buf.old_buffer = win.buffer;
			win.buffer = buf;
		}

		public static void nk_finish_popup(nk_context ctx, nk_window win)
		{
			if ((ctx == null) || (win == null)) return;

			win.buffer = win.popup.buf.old_buffer;
		}

		public static void nk_finish(nk_context ctx, nk_window win)
		{
			if ((ctx == null) || (win == null) || win.popup.active == 0) return;


		}

		public static int nk_panel_begin(nk_context ctx, char* title, int panel_type)
		{
			nk_input _in_;
			nk_window win;
			nk_panel layout;
			nk_command_buffer _out_;
			nk_style style;
			nk_user_font font;
			nk_vec2 scrollbar_size = new nk_vec2();
			nk_vec2 panel_padding = new nk_vec2();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);

			if (((ctx.current.flags & NK_WINDOW_HIDDEN) != 0) || ((ctx.current.flags & NK_WINDOW_CLOSED) != 0))
			{
				ctx.current.layout.type = (int) (panel_type);
				return (int) (0);
			}

			style = ctx.style;
			font = style.font;
			win = ctx.current;
			layout = win.layout;
			_out_ = win.buffer;
			_in_ = (win.flags & NK_WINDOW_NO_INPUT) != 0 ? null : ctx.input;
			scrollbar_size = (nk_vec2) (style.window.scrollbar_size);
			panel_padding = (nk_vec2) (nk_panel_get_padding(style, (int) (panel_type)));
			if (((win.flags & NK_WINDOW_MOVABLE) != 0) && ((win.flags & NK_WINDOW_ROM) == 0))
			{
				int left_mouse_down;
				int left_mouse_click_in_cursor;
				nk_rect header = new nk_rect();
				header.x = (float) (win.bounds.x);
				header.y = (float) (win.bounds.y);
				header.w = (float) (win.bounds.w);
				if ((nk_panel_has_header((uint) (win.flags), title)) != 0)
				{
					header.h = (float) (font.height + 2.0f*style.window.header.padding.y);
					header.h += (float) (2.0f*style.window.header.label_padding.y);
				}
				else header.h = (float) (panel_padding.y);
				left_mouse_down = (int) (((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down);
				left_mouse_click_in_cursor =
					(int) (nk_input_has_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (header), (int) (nk_true)));
				if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
				{
					win.bounds.x = (float) (win.bounds.x + _in_.mouse.delta.x);
					win.bounds.y = (float) (win.bounds.y + _in_.mouse.delta.y);
					((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.x += (float) (_in_.mouse.delta.x);
					((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.y += (float) (_in_.mouse.delta.y);
					ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_MOVE];
				}
			}

			layout.type = (int) (panel_type);
			layout.flags = (uint) (win.flags);
			layout.bounds = (nk_rect) (win.bounds);
			layout.bounds.x += (float) (panel_padding.x);
			layout.bounds.w -= (float) (2*panel_padding.x);
			if ((win.flags & NK_WINDOW_BORDER) != 0)
			{
				layout.border = (float) (nk_panel_get_border(style, (uint) (win.flags), (int) (panel_type)));
				layout.bounds = (nk_rect) (nk_shrink_rect_((nk_rect) (layout.bounds), (float) (layout.border)));
			}
			else layout.border = (float) (0);
			layout.at_y = (float) (layout.bounds.y);
			layout.at_x = (float) (layout.bounds.x);
			layout.max_x = (float) (0);
			layout.header_height = (float) (0);
			layout.footer_height = (float) (0);
			nk_layout_reset_min_row_height(ctx);
			layout.row.index = (int) (0);
			layout.row.columns = (int) (0);
			layout.row.ratio = null;
			layout.row.item_width = (float) (0);
			layout.row.tree_depth = (int) (0);
			layout.row.height = (float) (panel_padding.y);
			layout.has_scrolling = (uint) (nk_true);
			if ((win.flags & NK_WINDOW_NO_SCROLLBAR) == 0) layout.bounds.w -= (float) (scrollbar_size.x);
			if (nk_panel_is_nonblock((int) (panel_type)) == 0)
			{
				layout.footer_height = (float) (0);
				if (((win.flags & NK_WINDOW_NO_SCROLLBAR) == 0) || ((win.flags & NK_WINDOW_SCALABLE) != 0))
					layout.footer_height = (float) (scrollbar_size.y);
				layout.bounds.h -= (float) (layout.footer_height);
			}

			if ((nk_panel_has_header((uint) (win.flags), title)) != 0)
			{
				nk_text text = new nk_text();
				nk_rect header = new nk_rect();
				nk_style_item background = null;
				header.x = (float) (win.bounds.x);
				header.y = (float) (win.bounds.y);
				header.w = (float) (win.bounds.w);
				header.h = (float) (font.height + 2.0f*style.window.header.padding.y);
				header.h += (float) (2.0f*style.window.header.label_padding.y);
				layout.header_height = (float) (header.h);
				layout.bounds.y += (float) (header.h);
				layout.bounds.h -= (float) (header.h);
				layout.at_y += (float) (header.h);
				if ((ctx.active) == (win))
				{
					background = style.window.header.active;
					text.text = (nk_color) (style.window.header.label_active);
				}
				else if ((nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (header))) != 0)
				{
					background = style.window.header.hover;
					text.text = (nk_color) (style.window.header.label_hover);
				}
				else
				{
					background = style.window.header.normal;
					text.text = (nk_color) (style.window.header.label_normal);
				}
				header.h += (float) (1.0f);
				if ((background.type) == (NK_STYLE_ITEM_IMAGE))
				{
					text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
					nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
				}
				else
				{
					text.background = (nk_color) (background.data.color);
					nk_fill_rect(_out_, (nk_rect) (header), (float) (0), (nk_color) (background.data.color));
				}
				{
					nk_rect button = new nk_rect();
					button.y = (float) (header.y + style.window.header.padding.y);
					button.h = (float) (header.h - 2*style.window.header.padding.y);
					button.w = (float) (button.h);
					if ((win.flags & NK_WINDOW_CLOSABLE) != 0)
					{
						uint ws = (uint) (0);
						if ((style.window.header.align) == (NK_HEADER_RIGHT))
						{
							button.x = (float) ((header.w + header.x) - (button.w + style.window.header.padding.x));
							header.w -= (float) (button.w + style.window.header.spacing.x + style.window.header.padding.x);
						}
						else
						{
							button.x = (float) (header.x + style.window.header.padding.x);
							header.x += (float) (button.w + style.window.header.spacing.x + style.window.header.padding.x);
						}
						if (
							((nk_do_button_symbol(ref ws, win.buffer, (nk_rect) (button), (int) (style.window.header.close_symbol),
								(int) (NK_BUTTON_DEFAULT), style.window.header.close_button, _in_, style.font)) != 0) &&
							((win.flags & NK_WINDOW_ROM) == 0))
						{
							layout.flags |= (uint) (NK_WINDOW_HIDDEN);
							layout.flags &= ((uint) (~(uint) NK_WINDOW_MINIMIZED));
						}
					}
					if ((win.flags & NK_WINDOW_MINIMIZABLE) != 0)
					{
						uint ws = (uint) (0);
						if ((style.window.header.align) == (NK_HEADER_RIGHT))
						{
							button.x = (float) ((header.w + header.x) - button.w);
							if ((win.flags & NK_WINDOW_CLOSABLE) == 0)
							{
								button.x -= (float) (style.window.header.padding.x);
								header.w -= (float) (style.window.header.padding.x);
							}
							header.w -= (float) (button.w + style.window.header.spacing.x);
						}
						else
						{
							button.x = (float) (header.x);
							header.x += (float) (button.w + style.window.header.spacing.x + style.window.header.padding.x);
						}
						if (
							((nk_do_button_symbol(ref ws, win.buffer, (nk_rect) (button),
								(int)
									((layout.flags & NK_WINDOW_MINIMIZED) != 0
										? style.window.header.maximize_symbol
										: style.window.header.minimize_symbol), (int) (NK_BUTTON_DEFAULT), style.window.header.minimize_button, _in_,
								style.font)) != 0) && ((win.flags & NK_WINDOW_ROM) == 0))
							layout.flags =
								(uint)
									((layout.flags & NK_WINDOW_MINIMIZED) != 0
										? layout.flags & (uint) (~(uint) NK_WINDOW_MINIMIZED)
										: layout.flags | NK_WINDOW_MINIMIZED);
					}
				}
				{
					int text_len = (int) (nk_strlen(title));
					nk_rect label = new nk_rect();
					float t = (float) (font.width((nk_handle) (font.userdata), (float) (font.height), title, (int) (text_len)));
					text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
					label.x = (float) (header.x + style.window.header.padding.x);
					label.x += (float) (style.window.header.label_padding.x);
					label.y = (float) (header.y + style.window.header.label_padding.y);
					label.h = (float) (font.height + 2*style.window.header.label_padding.y);
					label.w = (float) (t + 2*style.window.header.spacing.x);
					label.w =
						(float)
							(((label.w) < (header.x + header.w - label.x) ? (label.w) : (header.x + header.w - label.x)) < (0)
								? (0)
								: ((label.w) < (header.x + header.w - label.x) ? (label.w) : (header.x + header.w - label.x)));
					nk_widget_text(_out_, (nk_rect) (label), title, (int) (text_len), &text, (uint) (NK_TEXT_LEFT), font);
				}
			}

			if (((layout.flags & NK_WINDOW_MINIMIZED) == 0) && ((layout.flags & NK_WINDOW_DYNAMIC) == 0))
			{
				nk_rect body = new nk_rect();
				body.x = (float) (win.bounds.x);
				body.w = (float) (win.bounds.w);
				body.y = (float) (win.bounds.y + layout.header_height);
				body.h = (float) (win.bounds.h - layout.header_height);
				if ((style.window.fixed_background.type) == (NK_STYLE_ITEM_IMAGE))
					nk_draw_image(_out_, (nk_rect) (body), style.window.fixed_background.data.image, (nk_color) (nk_white));
				else nk_fill_rect(_out_, (nk_rect) (body), (float) (0), (nk_color) (style.window.fixed_background.data.color));
			}

			{
				nk_rect clip = new nk_rect();
				layout.clip = (nk_rect) (layout.bounds);
				nk_unify(ref clip, ref win.buffer.clip, (float) (layout.clip.x), (float) (layout.clip.y),
					(float) (layout.clip.x + layout.clip.w), (float) (layout.clip.y + layout.clip.h));
				nk_push_scissor(_out_, (nk_rect) (clip));
				layout.clip = (nk_rect) (clip);
			}

			return (int) (((layout.flags & NK_WINDOW_HIDDEN) == 0) && ((layout.flags & NK_WINDOW_MINIMIZED) == 0) ? 1 : 0);
		}

		public static void nk_panel_end(nk_context ctx)
		{
			nk_input _in_;
			nk_window window;
			nk_panel layout;
			nk_style style;
			nk_command_buffer _out_;
			nk_vec2 scrollbar_size = new nk_vec2();
			nk_vec2 panel_padding = new nk_vec2();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			window = ctx.current;
			layout = window.layout;
			style = ctx.style;
			_out_ = window.buffer;
			_in_ = (((layout.flags & NK_WINDOW_ROM) != 0) || ((layout.flags & NK_WINDOW_NO_INPUT) != 0)) ? null : ctx.input;
			if (nk_panel_is_sub((int) (layout.type)) == 0) nk_push_scissor(_out_, (nk_rect) (nk_null_rect));
			scrollbar_size = (nk_vec2) (style.window.scrollbar_size);
			panel_padding = (nk_vec2) (nk_panel_get_padding(style, (int) (layout.type)));
			layout.at_y += (float) (layout.row.height);
			if (((layout.flags & NK_WINDOW_DYNAMIC) != 0) && ((layout.flags & NK_WINDOW_MINIMIZED) == 0))
			{
				nk_rect empty_space = new nk_rect();
				if ((layout.at_y) < (layout.bounds.y + layout.bounds.h)) layout.bounds.h = (float) (layout.at_y - layout.bounds.y);
				empty_space.x = (float) (window.bounds.x);
				empty_space.y = (float) (layout.bounds.y);
				empty_space.h = (float) (panel_padding.y);
				empty_space.w = (float) (window.bounds.w);
				nk_fill_rect(_out_, (nk_rect) (empty_space), (float) (0), (nk_color) (style.window.background));
				empty_space.x = (float) (window.bounds.x);
				empty_space.y = (float) (layout.bounds.y);
				empty_space.w = (float) (panel_padding.x + layout.border);
				empty_space.h = (float) (layout.bounds.h);
				nk_fill_rect(_out_, (nk_rect) (empty_space), (float) (0), (nk_color) (style.window.background));
				empty_space.x = (float) (layout.bounds.x + layout.bounds.w - layout.border);
				empty_space.y = (float) (layout.bounds.y);
				empty_space.w = (float) (panel_padding.x + layout.border);
				empty_space.h = (float) (layout.bounds.h);
				if (((layout.offset.y) == (0)) && ((layout.flags & NK_WINDOW_NO_SCROLLBAR) == 0))
					empty_space.w += (float) (scrollbar_size.x);
				nk_fill_rect(_out_, (nk_rect) (empty_space), (float) (0), (nk_color) (style.window.background));
				if ((layout.offset.x != 0) && ((layout.flags & NK_WINDOW_NO_SCROLLBAR) == 0))
				{
					empty_space.x = (float) (window.bounds.x);
					empty_space.y = (float) (layout.bounds.y + layout.bounds.h);
					empty_space.w = (float) (window.bounds.w);
					empty_space.h = (float) (scrollbar_size.y);
					nk_fill_rect(_out_, (nk_rect) (empty_space), (float) (0), (nk_color) (style.window.background));
				}
			}

			if ((((layout.flags & NK_WINDOW_NO_SCROLLBAR) == 0) && ((layout.flags & NK_WINDOW_MINIMIZED) == 0)) &&
			    ((window.scrollbar_hiding_timer) < (4.0f)))
			{
				nk_rect scroll = new nk_rect();
				int scroll_has_scrolling;
				float scroll_target;
				float scroll_offset;
				float scroll_step;
				float scroll_inc;
				if ((nk_panel_is_sub((int) (layout.type))) != 0)
				{
					nk_window root_window = window;
					nk_panel root_panel = window.layout;
					while ((root_panel.parent) != null)
					{
						root_panel = root_panel.parent;
					}
					while ((root_window.parent) != null)
					{
						root_window = root_window.parent;
					}
					scroll_has_scrolling = (int) (0);
					if (((root_window) == (ctx.active)) && ((layout.has_scrolling) != 0))
					{
						if (((nk_input_is_mouse_hovering_rect(_in_, (nk_rect) (layout.bounds))) != 0) &&
						    (!(((((root_panel.clip.x) > (layout.bounds.x + layout.bounds.w)) ||
						         ((root_panel.clip.x + root_panel.clip.w) < (layout.bounds.x))) ||
						        ((root_panel.clip.y) > (layout.bounds.y + layout.bounds.h))) ||
						       ((root_panel.clip.y + root_panel.clip.h) < (layout.bounds.y)))))
						{
							root_panel = window.layout;
							while ((root_panel.parent) != null)
							{
								root_panel.has_scrolling = (uint) (nk_false);
								root_panel = root_panel.parent;
							}
							root_panel.has_scrolling = (uint) (nk_false);
							scroll_has_scrolling = (int) (nk_true);
						}
					}
				}
				else if (nk_panel_is_sub((int) (layout.type)) == 0)
				{
					scroll_has_scrolling = (int) (((window) == (ctx.active)) && ((layout.has_scrolling) != 0) ? 1 : 0);
					if ((((_in_) != null) && (((_in_.mouse.scroll_delta.y) > (0)) || ((_in_.mouse.scroll_delta.x) > (0)))) &&
					    ((scroll_has_scrolling) != 0)) window.scrolled = (uint) (nk_true);
					else window.scrolled = (uint) (nk_false);
				}
				else scroll_has_scrolling = (int) (nk_false);
				{
					uint state = (uint) (0);
					scroll.x = (float) (layout.bounds.x + layout.bounds.w + panel_padding.x);
					scroll.y = (float) (layout.bounds.y);
					scroll.w = (float) (scrollbar_size.x);
					scroll.h = (float) (layout.bounds.h);
					scroll_offset = ((float) (layout.offset.y));
					scroll_step = (float) (scroll.h*0.10f);
					scroll_inc = (float) (scroll.h*0.01f);
					scroll_target = ((float) ((int) (layout.at_y - scroll.y)));
					scroll_offset =
						(float)
							(nk_do_scrollbarv(ref state, _out_, (nk_rect) (scroll), (int) (scroll_has_scrolling), (float) (scroll_offset),
								(float) (scroll_target), (float) (scroll_step), (float) (scroll_inc), ctx.style.scrollv, _in_, style.font));
					layout.offset.y = ((uint) (scroll_offset));
					if (((_in_) != null) && ((scroll_has_scrolling) != 0)) _in_.mouse.scroll_delta.y = (float) (0);
				}
				{
					uint state = (uint) (0);
					scroll.x = (float) (layout.bounds.x);
					scroll.y = (float) (layout.bounds.y + layout.bounds.h);
					scroll.w = (float) (layout.bounds.w);
					scroll.h = (float) (scrollbar_size.y);
					scroll_offset = ((float) (layout.offset.x));
					scroll_target = ((float) ((int) (layout.max_x - scroll.x)));
					scroll_step = (float) (layout.max_x*0.05f);
					scroll_inc = (float) (layout.max_x*0.005f);
					scroll_offset =
						(float)
							(nk_do_scrollbarh(ref state, _out_, (nk_rect) (scroll), (int) (scroll_has_scrolling), (float) (scroll_offset),
								(float) (scroll_target), (float) (scroll_step), (float) (scroll_inc), ctx.style.scrollh, _in_, style.font));
					layout.offset.x = ((uint) (scroll_offset));
				}
			}

			if ((window.flags & NK_WINDOW_SCROLL_AUTO_HIDE) != 0)
			{
				int has_input =
					(int)
						(((ctx.input.mouse.delta.x != 0) || (ctx.input.mouse.delta.y != 0)) || (ctx.input.mouse.scroll_delta.y != 0)
							? 1
							: 0);
				int is_window_hovered = (int) (nk_window_is_hovered(ctx));
				int any_item_active = (int) (ctx.last_widget_state & NK_WIDGET_STATE_MODIFIED);
				if (((has_input == 0) && ((is_window_hovered) != 0)) || ((is_window_hovered == 0) && (any_item_active == 0)))
					window.scrollbar_hiding_timer += (float) (ctx.delta_time_seconds);
				else window.scrollbar_hiding_timer = (float) (0);
			}
			else window.scrollbar_hiding_timer = (float) (0);
			if ((layout.flags & NK_WINDOW_BORDER) != 0)
			{
				nk_color border_color = (nk_color) (nk_panel_get_border_color(style, (int) (layout.type)));
				float padding_y =
					(float)
						((layout.flags & NK_WINDOW_MINIMIZED) != 0
							? style.window.border + window.bounds.y + layout.header_height
							: (layout.flags & NK_WINDOW_DYNAMIC) != 0
								? layout.bounds.y + layout.bounds.h + layout.footer_height
								: window.bounds.y + window.bounds.h);
				nk_rect b = window.bounds;
				b.h = padding_y - window.bounds.y;
				nk_stroke_rect(_out_, b, 0, layout.border, border_color);
			}

			if ((((layout.flags & NK_WINDOW_SCALABLE) != 0) && ((_in_) != null)) && ((layout.flags & NK_WINDOW_MINIMIZED) == 0))
			{
				nk_rect scaler = new nk_rect();
				scaler.w = (float) (scrollbar_size.x);
				scaler.h = (float) (scrollbar_size.y);
				scaler.y = (float) (layout.bounds.y + layout.bounds.h);
				if ((layout.flags & NK_WINDOW_SCALE_LEFT) != 0) scaler.x = (float) (layout.bounds.x - panel_padding.x*0.5f);
				else scaler.x = (float) (layout.bounds.x + layout.bounds.w + panel_padding.x);
				if ((layout.flags & NK_WINDOW_NO_SCROLLBAR) != 0) scaler.x -= (float) (scaler.w);
				{
					nk_style_item item = style.window.scaler;
					if ((item.type) == (NK_STYLE_ITEM_IMAGE))
						nk_draw_image(_out_, (nk_rect) (scaler), item.data.image, (nk_color) (nk_white));
					else
					{
						if ((layout.flags & NK_WINDOW_SCALE_LEFT) != 0)
						{
							nk_fill_triangle(_out_, (float) (scaler.x), (float) (scaler.y), (float) (scaler.x), (float) (scaler.y + scaler.h),
								(float) (scaler.x + scaler.w), (float) (scaler.y + scaler.h), (nk_color) (item.data.color));
						}
						else
						{
							nk_fill_triangle(_out_, (float) (scaler.x + scaler.w), (float) (scaler.y), (float) (scaler.x + scaler.w),
								(float) (scaler.y + scaler.h), (float) (scaler.x), (float) (scaler.y + scaler.h), (nk_color) (item.data.color));
						}
					}
				}
				if ((window.flags & NK_WINDOW_ROM) == 0)
				{
					nk_vec2 window_size = (nk_vec2) (style.window.min_size);
					int left_mouse_down = (int) (((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down);
					int left_mouse_click_in_scaler =
						(int) (nk_input_has_mouse_click_down_in_rect(_in_, (int) (NK_BUTTON_LEFT), (nk_rect) (scaler), (int) (nk_true)));
					if (((left_mouse_down) != 0) && ((left_mouse_click_in_scaler) != 0))
					{
						float delta_x = (float) (_in_.mouse.delta.x);
						if ((layout.flags & NK_WINDOW_SCALE_LEFT) != 0)
						{
							delta_x = (float) (-delta_x);
							window.bounds.x += (float) (_in_.mouse.delta.x);
						}
						if ((window.bounds.w + delta_x) >= (window_size.x))
						{
							if (((delta_x) < (0)) || (((delta_x) > (0)) && ((_in_.mouse.pos.x) >= (scaler.x))))
							{
								window.bounds.w = (float) (window.bounds.w + delta_x);
								scaler.x += (float) (_in_.mouse.delta.x);
							}
						}
						if ((layout.flags & NK_WINDOW_DYNAMIC) == 0)
						{
							if ((window_size.y) < (window.bounds.h + _in_.mouse.delta.y))
							{
								if (((_in_.mouse.delta.y) < (0)) || (((_in_.mouse.delta.y) > (0)) && ((_in_.mouse.pos.y) >= (scaler.y))))
								{
									window.bounds.h = (float) (window.bounds.h + _in_.mouse.delta.y);
									scaler.y += (float) (_in_.mouse.delta.y);
								}
							}
						}
						ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT];
						((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.x = (float) (scaler.x + scaler.w/2.0f);
						((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked_pos.y = (float) (scaler.y + scaler.h/2.0f);
					}
				}
			}

			if (nk_panel_is_sub((int) (layout.type)) == 0)
			{
				if ((layout.flags & NK_WINDOW_HIDDEN) != 0) nk_command_buffer_reset(window.buffer);
				else nk_finish(ctx, window);
			}

			if ((layout.flags & NK_WINDOW_REMOVE_ROM) != 0)
			{
				layout.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
				layout.flags &= (uint) (~(uint) (NK_WINDOW_REMOVE_ROM));
			}

			window.flags = (uint) (layout.flags);
			if ((((window.property.active) != 0) && (window.property.old != window.property.seq)) &&
			    ((window.property.active) == (window.property.prev)))
			{
			}
			else
			{
				window.property.old = (uint) (window.property.seq);
				window.property.prev = (int) (window.property.active);
				window.property.seq = (uint) (0);
			}

			if ((((window.edit.active) != 0) && (window.edit.old != window.edit.seq)) &&
			    ((window.edit.active) == (window.edit.prev)))
			{
			}
			else
			{
				window.edit.old = (uint) (window.edit.seq);
				window.edit.prev = (int) (window.edit.active);
				window.edit.seq = (uint) (0);
			}

			if (((window.popup.active_con) != 0) && (window.popup.con_old != window.popup.con_count))
			{
				window.popup.con_count = (uint) (0);
				window.popup.con_old = (uint) (0);
				window.popup.active_con = (uint) (0);
			}
			else
			{
				window.popup.con_old = (uint) (window.popup.con_count);
				window.popup.con_count = (uint) (0);
			}

			window.popup.combo_count = (uint) (0);
		}

		public static uint* nk_add_value(nk_context ctx, nk_window win, uint name, uint value)
		{
			if ((win == null) || (ctx == null)) return null;
			if ((win.tables == null) || ((win.tables.size) >= (51)))
			{
				nk_table tbl = nk_create_table(ctx);
				if (tbl == null) return null;
				nk_push_table(win, tbl);
			}

			win.tables.seq = (uint) (win.seq);
			win.tables.keys[win.tables.size] = (uint) (name);
			win.tables.values[win.tables.size] = (uint) (value);
			return (uint*) win.tables.values + (win.tables.size++);
		}

		public static nk_window nk_find_window(nk_context ctx, uint hash, char* name)
		{
			nk_window iter;
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((iter.name) == (hash))
				{
					int max_len = (int) (nk_strlen(iter.name_string));
					if (nk_stricmpn(iter.name_string, name, (int) (max_len)) == 0) return iter;
				}
				iter = iter.next;
			}
			return null;
		}

		public static void nk_insert_window(nk_context ctx, nk_window win, int loc)
		{
			nk_window iter;
			if ((win == null) || (ctx == null)) return;
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((iter) == (win)) return;
				iter = iter.next;
			}
			if (ctx.begin == null)
			{
				win.next = null;
				win.prev = null;
				ctx.begin = win;
				ctx.end = win;
				ctx.count = (uint) (1);
				return;
			}

			if ((loc) == (NK_INSERT_BACK))
			{
				nk_window end;
				end = ctx.end;
				end.flags |= (uint) (NK_WINDOW_ROM);
				end.next = win;
				win.prev = ctx.end;
				win.next = null;
				ctx.end = win;
				ctx.active = ctx.end;
				ctx.end.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			}
			else
			{
				ctx.begin.prev = win;
				win.next = ctx.begin;
				win.prev = null;
				ctx.begin = win;
				ctx.begin.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			}

			ctx.count++;
		}

		public static void nk_remove_window(nk_context ctx, nk_window win)
		{
			if (((win) == (ctx.begin)) || ((win) == (ctx.end)))
			{
				if ((win) == (ctx.begin))
				{
					ctx.begin = win.next;
					if ((win.next) != null) win.next.prev = null;
				}
				if ((win) == (ctx.end))
				{
					ctx.end = win.prev;
					if ((win.prev) != null) win.prev.next = null;
				}
			}
			else
			{
				if ((win.next) != null) win.next.prev = win.prev;
				if ((win.prev) != null) win.prev.next = win.next;
			}

			if (((win) == (ctx.active)) || (ctx.active == null))
			{
				ctx.active = ctx.end;
				if ((ctx.end) != null) ctx.end.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			}

			win.next = null;
			win.prev = null;
			ctx.count--;
		}

		public static int nk_begin(nk_context ctx, char* title, nk_rect bounds, uint flags)
		{
			return (int) (nk_begin_titled(ctx, title, title, (nk_rect) (bounds), (uint) (flags)));
		}

		public static int nk_begin_titled(nk_context ctx, char* name, char* title, nk_rect bounds, uint flags)
		{
			nk_window win;
			nk_style style;
			uint title_hash;
			int title_len;
			int ret = (int) (0);
			if ((((ctx == null) || ((ctx.current) != null)) || (title == null)) || (name == null)) return (int) (0);
			style = ctx.style;
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null)
			{
				ulong name_length = (ulong) (nk_strlen(name));
				win = (nk_window) (nk_create_window(ctx));
				if (win == null) return (int) (0);
				if ((flags & NK_WINDOW_BACKGROUND) != 0) nk_insert_window(ctx, win, (int) (NK_INSERT_FRONT));
				else nk_insert_window(ctx, win, (int) (NK_INSERT_BACK));
				nk_command_buffer_init(win.buffer, (int) (NK_CLIPPING_ON));
				win.flags = (uint) (flags);
				win.bounds = (nk_rect) (bounds);
				win.name = (uint) (title_hash);
				name_length = (ulong) ((name_length) < (64 - 1) ? (name_length) : (64 - 1));
				nk_memcopy(win.name_string, name, (ulong) (name_length));
				win.name_string[name_length] = (char) (0);
				win.popup.win = null;
				if (ctx.active == null) ctx.active = win;
			}
			else
			{
				win.flags &= (uint) (~(uint) (NK_WINDOW_PRIVATE - 1));
				win.flags |= (uint) (flags);
				if ((win.flags & (NK_WINDOW_MOVABLE | NK_WINDOW_SCALABLE)) == 0) win.bounds = (nk_rect) (bounds);
				win.seq = (uint) (ctx.seq);
				if ((ctx.active == null) && ((win.flags & NK_WINDOW_HIDDEN) == 0))
				{
					ctx.active = win;
					ctx.end = win;
				}
			}

			if ((win.flags & NK_WINDOW_HIDDEN) != 0)
			{
				ctx.current = win;
				win.layout = null;
				return (int) (0);
			}
			else nk_start(ctx, win);
			if (((win.flags & NK_WINDOW_HIDDEN) == 0) && ((win.flags & NK_WINDOW_NO_INPUT) == 0))
			{
				int inpanel;
				int ishovered;
				nk_window iter = win;
				float h =
					(float) (ctx.style.font.height + 2.0f*style.window.header.padding.y + (2.0f*style.window.header.label_padding.y));
				nk_rect win_bounds =
					(nk_rect)
						(((win.flags & NK_WINDOW_MINIMIZED) == 0)
							? win.bounds
							: nk_rect_((float) (win.bounds.x), (float) (win.bounds.y), (float) (win.bounds.w), (float) (h)));
				inpanel =
					(int)
						(nk_input_has_mouse_click_down_in_rect(ctx.input, (int) (NK_BUTTON_LEFT), (nk_rect) (win_bounds), (int) (nk_true)));
				inpanel = (int) (((inpanel) != 0) && ((ctx.input.mouse.buttons[NK_BUTTON_LEFT].clicked) != 0) ? 1 : 0);
				ishovered = (int) (nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (win_bounds)));
				if (((win != ctx.active) && ((ishovered) != 0)) && (ctx.input.mouse.buttons[NK_BUTTON_LEFT].down == 0))
				{
					iter = win.next;
					while ((iter) != null)
					{
						nk_rect iter_bounds =
							(nk_rect)
								(((iter.flags & NK_WINDOW_MINIMIZED) == 0)
									? iter.bounds
									: nk_rect_((float) (iter.bounds.x), (float) (iter.bounds.y), (float) (iter.bounds.w), (float) (h)));
						if (
							(!(((((iter_bounds.x) > (win_bounds.x + win_bounds.w)) || ((iter_bounds.x + iter_bounds.w) < (win_bounds.x))) ||
							    ((iter_bounds.y) > (win_bounds.y + win_bounds.h))) || ((iter_bounds.y + iter_bounds.h) < (win_bounds.y)))) &&
							((iter.flags & NK_WINDOW_HIDDEN) == 0)) break;
						if (((((iter.popup.win) != null) && ((iter.popup.active) != 0)) && ((iter.flags & NK_WINDOW_HIDDEN) == 0)) &&
						    (!(((((iter.popup.win.bounds.x) > (win.bounds.x + win_bounds.w)) ||
						         ((iter.popup.win.bounds.x + iter.popup.win.bounds.w) < (win.bounds.x))) ||
						        ((iter.popup.win.bounds.y) > (win_bounds.y + win_bounds.h))) ||
						       ((iter.popup.win.bounds.y + iter.popup.win.bounds.h) < (win_bounds.y))))) break;
						iter = iter.next;
					}
				}
				if ((((iter) != null) && ((inpanel) != 0)) && (win != ctx.end))
				{
					iter = win.next;
					while ((iter) != null)
					{
						nk_rect iter_bounds =
							(nk_rect)
								(((iter.flags & NK_WINDOW_MINIMIZED) == 0)
									? iter.bounds
									: nk_rect_((float) (iter.bounds.x), (float) (iter.bounds.y), (float) (iter.bounds.w), (float) (h)));
						if (((((iter_bounds.x) <= (ctx.input.mouse.pos.x)) && ((ctx.input.mouse.pos.x) < (iter_bounds.x + iter_bounds.w))) &&
						     (((iter_bounds.y) <= (ctx.input.mouse.pos.y)) && ((ctx.input.mouse.pos.y) < (iter_bounds.y + iter_bounds.h)))) &&
						    ((iter.flags & NK_WINDOW_HIDDEN) == 0)) break;
						if (((((iter.popup.win) != null) && ((iter.popup.active) != 0)) && ((iter.flags & NK_WINDOW_HIDDEN) == 0)) &&
						    (!(((((iter.popup.win.bounds.x) > (win_bounds.x + win_bounds.w)) ||
						         ((iter.popup.win.bounds.x + iter.popup.win.bounds.w) < (win_bounds.x))) ||
						        ((iter.popup.win.bounds.y) > (win_bounds.y + win_bounds.h))) ||
						       ((iter.popup.win.bounds.y + iter.popup.win.bounds.h) < (win_bounds.y))))) break;
						iter = iter.next;
					}
				}
				if ((((iter) != null) && ((win.flags & NK_WINDOW_ROM) == 0)) && ((win.flags & NK_WINDOW_BACKGROUND) != 0))
				{
					win.flags |= ((uint) (NK_WINDOW_ROM));
					iter.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
					ctx.active = iter;
					if ((iter.flags & NK_WINDOW_BACKGROUND) == 0)
					{
						nk_remove_window(ctx, iter);
						nk_insert_window(ctx, iter, (int) (NK_INSERT_BACK));
					}
				}
				else
				{
					if ((iter == null) && (ctx.end != win))
					{
						if ((win.flags & NK_WINDOW_BACKGROUND) == 0)
						{
							nk_remove_window(ctx, win);
							nk_insert_window(ctx, win, (int) (NK_INSERT_BACK));
						}
						win.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
						ctx.active = win;
					}
					if ((ctx.end != win) && ((win.flags & NK_WINDOW_BACKGROUND) == 0)) win.flags |= (uint) (NK_WINDOW_ROM);
				}
			}

			win.layout = (nk_panel) (nk_create_panel(ctx));
			ctx.current = win;
			ret = (int) (nk_panel_begin(ctx, title, (int) (NK_PANEL_WINDOW)));
			win.layout.offset = win.scrollbar;

			return (int) (ret);
		}

		public static void nk_end(nk_context ctx)
		{
			nk_panel layout;
			if ((ctx == null) || (ctx.current == null)) return;
			layout = ctx.current.layout;
			if ((layout == null) || (((layout.type) == (NK_PANEL_WINDOW)) && ((ctx.current.flags & NK_WINDOW_HIDDEN) != 0)))
			{
				ctx.current = null;
				return;
			}

			nk_panel_end(ctx);

			ctx.current = null;
		}

		public static nk_rect nk_window_get_bounds(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null))
				return (nk_rect) (nk_rect_((float) (0), (float) (0), (float) (0), (float) (0)));
			return (nk_rect) (ctx.current.bounds);
		}

		public static nk_vec2 nk_window_get_position(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			return (nk_vec2) (nk_vec2_((float) (ctx.current.bounds.x), (float) (ctx.current.bounds.y)));
		}

		public static nk_vec2 nk_window_get_size(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			return (nk_vec2) (nk_vec2_((float) (ctx.current.bounds.w), (float) (ctx.current.bounds.h)));
		}

		public static float nk_window_get_width(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (float) (0);
			return (float) (ctx.current.bounds.w);
		}

		public static float nk_window_get_height(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (float) (0);
			return (float) (ctx.current.bounds.h);
		}

		public static nk_rect nk_window_get_content_region(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null))
				return (nk_rect) (nk_rect_((float) (0), (float) (0), (float) (0), (float) (0)));
			return (nk_rect) (ctx.current.layout.clip);
		}

		public static nk_vec2 nk_window_get_content_region_min(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			return (nk_vec2) (nk_vec2_((float) (ctx.current.layout.clip.x), (float) (ctx.current.layout.clip.y)));
		}

		public static nk_vec2 nk_window_get_content_region_max(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			return
				(nk_vec2)
					(nk_vec2_((float) (ctx.current.layout.clip.x + ctx.current.layout.clip.w),
						(float) (ctx.current.layout.clip.y + ctx.current.layout.clip.h)));
		}

		public static nk_vec2 nk_window_get_content_region_size(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			return (nk_vec2) (nk_vec2_((float) (ctx.current.layout.clip.w), (float) (ctx.current.layout.clip.h)));
		}

		public static nk_command_buffer nk_window_get_canvas(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return null;
			return ctx.current.buffer;
		}

		public static nk_panel nk_window_get_panel(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return null;
			return ctx.current.layout;
		}

		public static int nk_window_has_focus(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (int) (0);
			return (int) ((ctx.current) == (ctx.active) ? 1 : 0);
		}

		public static int nk_window_is_hovered(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return (int) (0);
			if ((ctx.current.flags & NK_WINDOW_HIDDEN) != 0) return (int) (0);
			return (int) (nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (ctx.current.bounds)));
		}

		public static int nk_window_is_any_hovered(nk_context ctx)
		{
			nk_window iter;
			if (ctx == null) return (int) (0);
			iter = ctx.begin;
			while ((iter) != null)
			{
				if ((iter.flags & NK_WINDOW_HIDDEN) == 0)
				{
					if ((((iter.popup.active) != 0) && ((iter.popup.win) != null)) &&
					    ((nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (iter.popup.win.bounds))) != 0)) return (int) (1);
					if ((iter.flags & NK_WINDOW_MINIMIZED) != 0)
					{
						nk_rect header = (nk_rect) (iter.bounds);
						header.h = (float) (ctx.style.font.height + 2*ctx.style.window.header.padding.y);
						if ((nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (header))) != 0) return (int) (1);
					}
					else if ((nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (iter.bounds))) != 0)
					{
						return (int) (1);
					}
				}
				iter = iter.next;
			}
			return (int) (0);
		}

		public static int nk_item_is_any_active(nk_context ctx)
		{
			int any_hovered = (int) (nk_window_is_any_hovered(ctx));
			int any_active = (int) (ctx.last_widget_state & NK_WIDGET_STATE_MODIFIED);
			return (int) (((any_hovered) != 0) || ((any_active) != 0) ? 1 : 0);
		}

		public static int nk_window_is_collapsed(nk_context ctx, char* name)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return (int) (0);
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null) return (int) (0);
			return (int) (win.flags & NK_WINDOW_MINIMIZED);
		}

		public static int nk_window_is_closed(nk_context ctx, char* name)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return (int) (1);
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null) return (int) (1);
			return (int) (win.flags & NK_WINDOW_CLOSED);
		}

		public static int nk_window_is_hidden(nk_context ctx, char* name)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return (int) (1);
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null) return (int) (1);
			return (int) (win.flags & NK_WINDOW_HIDDEN);
		}

		public static int nk_window_is_active(nk_context ctx, char* name)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return (int) (0);
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null) return (int) (0);
			return (int) ((win) == (ctx.active) ? 1 : 0);
		}

		public static nk_window nk_window_find(nk_context ctx, char* name)
		{
			int title_len;
			uint title_hash;
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			return nk_find_window(ctx, (uint) (title_hash), name);
		}

		public static void nk_window_close(nk_context ctx, char* name)
		{
			nk_window win;
			if (ctx == null) return;
			win = nk_window_find(ctx, name);
			if (win == null) return;
			if ((ctx.current) == (win)) return;
			win.flags |= (uint) (NK_WINDOW_HIDDEN);
			win.flags |= (uint) (NK_WINDOW_CLOSED);
		}

		public static void nk_window_set_bounds(nk_context ctx, char* name, nk_rect bounds)
		{
			nk_window win;
			if (ctx == null) return;
			win = nk_window_find(ctx, name);
			if (win == null) return;
			win.bounds = (nk_rect) (bounds);
		}

		public static void nk_window_set_position(nk_context ctx, char* name, nk_vec2 pos)
		{
			nk_window win = nk_window_find(ctx, name);
			if (win == null) return;
			win.bounds.x = (float) (pos.x);
			win.bounds.y = (float) (pos.y);
		}

		public static void nk_window_set_size(nk_context ctx, char* name, nk_vec2 size)
		{
			nk_window win = nk_window_find(ctx, name);
			if (win == null) return;
			win.bounds.w = (float) (size.x);
			win.bounds.h = (float) (size.y);
		}

		public static void nk_window_collapse(nk_context ctx, char* name, int c)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return;
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null) return;
			if ((c) == (NK_MINIMIZED)) win.flags |= (uint) (NK_WINDOW_MINIMIZED);
			else win.flags &= (uint) (~(uint) (NK_WINDOW_MINIMIZED));
		}

		public static void nk_window_collapse_if(nk_context ctx, char* name, int c, int cond)
		{
			if ((ctx == null) || (cond == 0)) return;
			nk_window_collapse(ctx, name, (int) (c));
		}

		public static void nk_window_show(nk_context ctx, char* name, int s)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return;
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (win == null) return;
			if ((s) == (NK_HIDDEN))
			{
				win.flags |= (uint) (NK_WINDOW_HIDDEN);
			}
			else win.flags &= (uint) (~(uint) (NK_WINDOW_HIDDEN));
		}

		public static void nk_window_show_if(nk_context ctx, char* name, int s, int cond)
		{
			if ((ctx == null) || (cond == 0)) return;
			nk_window_show(ctx, name, (int) (s));
		}

		public static void nk_window_set_focus(nk_context ctx, char* name)
		{
			int title_len;
			uint title_hash;
			nk_window win;
			if (ctx == null) return;
			title_len = (int) (nk_strlen(name));
			title_hash = (uint) (nk_murmur_hash(name, (int) (title_len), (uint) (NK_WINDOW_TITLE)));
			win = nk_find_window(ctx, (uint) (title_hash), name);
			if (((win) != null) && (ctx.end != win))
			{
				nk_remove_window(ctx, win);
				nk_insert_window(ctx, win, (int) (NK_INSERT_BACK));
			}

			ctx.active = win;
		}

		public static void nk_menubar_begin(nk_context ctx)
		{
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			layout = ctx.current.layout;
			if (((layout.flags & NK_WINDOW_HIDDEN) != 0) || ((layout.flags & NK_WINDOW_MINIMIZED) != 0)) return;
			layout.menu.x = (float) (layout.at_x);
			layout.menu.y = (float) (layout.at_y + layout.row.height);
			layout.menu.w = (float) (layout.bounds.w);
			layout.menu.offset = layout.offset;

			layout.offset.y = (uint) (0);
		}

		public static void nk_menubar_end(nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			nk_command_buffer _out_;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			_out_ = win.buffer;
			layout = win.layout;
			if (((layout.flags & NK_WINDOW_HIDDEN) != 0) || ((layout.flags & NK_WINDOW_MINIMIZED) != 0)) return;
			layout.menu.h = (float) (layout.at_y - layout.menu.y);
			layout.bounds.y += (float) (layout.menu.h + ctx.style.window.spacing.y + layout.row.height);
			layout.bounds.h -= (float) (layout.menu.h + ctx.style.window.spacing.y + layout.row.height);
			layout.offset.x = (uint) (layout.menu.offset.x);
			layout.offset.y = (uint) (layout.menu.offset.y);
			layout.at_y = (float) (layout.bounds.y - layout.row.height);
			layout.clip.y = (float) (layout.bounds.y);
			layout.clip.h = (float) (layout.bounds.h);
			nk_push_scissor(_out_, (nk_rect) (layout.clip));
		}

		public static void nk_layout_set_min_row_height(nk_context ctx, float height)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			layout.row.min_height = (float) (height);
		}

		public static void nk_layout_reset_min_row_height(nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			layout.row.min_height = (float) (ctx.style.font.height);
			layout.row.min_height += (float) (ctx.style.text.padding.y*2);
			layout.row.min_height += (float) (ctx.style.window.min_row_height_padding*2);
		}

		public static void nk_panel_layout(nk_context ctx, nk_window win, float height, int cols)
		{
			nk_panel layout;
			nk_style style;
			nk_command_buffer _out_;
			nk_vec2 item_spacing = new nk_vec2();
			nk_color color = new nk_color();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			layout = win.layout;
			style = ctx.style;
			_out_ = win.buffer;
			color = (nk_color) (style.window.background);
			item_spacing = (nk_vec2) (style.window.spacing);
			layout.row.index = (int) (0);
			layout.at_y += (float) (layout.row.height);
			layout.row.columns = (int) (cols);
			if ((height) == (0.0f))
				layout.row.height =
					(float) (((height) < (layout.row.min_height) ? (layout.row.min_height) : (height)) + item_spacing.y);
			else layout.row.height = (float) (height + item_spacing.y);
			layout.row.item_offset = (float) (0);
			if ((layout.flags & NK_WINDOW_DYNAMIC) != 0)
			{
				nk_rect background = new nk_rect();
				background.x = (float) (win.bounds.x);
				background.w = (float) (win.bounds.w);
				background.y = (float) (layout.at_y - 1.0f);
				background.h = (float) (layout.row.height + 1.0f);
				nk_fill_rect(_out_, (nk_rect) (background), (float) (0), (nk_color) (color));
			}

		}

		public static void nk_row_layout_(nk_context ctx, int fmt, float height, int cols, int width)
		{
			nk_window win;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			nk_panel_layout(ctx, win, (float) (height), (int) (cols));
			if ((fmt) == (NK_DYNAMIC)) win.layout.row.type = (int) (NK_LAYOUT_DYNAMIC_FIXED);
			else win.layout.row.type = (int) (NK_LAYOUT_STATIC_FIXED);
			win.layout.row.ratio = null;
			win.layout.row.filled = (float) (0);
			win.layout.row.item_offset = (float) (0);
			win.layout.row.item_width = ((float) (width));
		}

		public static float nk_layout_ratio_from_pixel(nk_context ctx, float pixel_width)
		{
			nk_window win;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (float) (0);
			win = ctx.current;
			return
				(float)
					(((pixel_width/win.bounds.x) < (1.0f) ? (pixel_width/win.bounds.x) : (1.0f)) < (0.0f)
						? (0.0f)
						: ((pixel_width/win.bounds.x) < (1.0f) ? (pixel_width/win.bounds.x) : (1.0f)));
		}

		public static void nk_layout_row_dynamic(nk_context ctx, float height, int cols)
		{
			nk_row_layout_(ctx, (int) (NK_DYNAMIC), (float) (height), (int) (cols), (int) (0));
		}

		public static void nk_layout_row_static(nk_context ctx, float height, int item_width, int cols)
		{
			nk_row_layout_(ctx, (int) (NK_STATIC), (float) (height), (int) (cols), (int) (item_width));
		}

		public static void nk_layout_row_begin(nk_context ctx, int fmt, float row_height, int cols)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			nk_panel_layout(ctx, win, (float) (row_height), (int) (cols));
			if ((fmt) == (NK_DYNAMIC)) layout.row.type = (int) (NK_LAYOUT_DYNAMIC_ROW);
			else layout.row.type = (int) (NK_LAYOUT_STATIC_ROW);
			layout.row.ratio = null;
			layout.row.filled = (float) (0);
			layout.row.item_width = (float) (0);
			layout.row.item_offset = (float) (0);
			layout.row.columns = (int) (cols);
		}

		public static void nk_layout_row_push(nk_context ctx, float ratio_or_width)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if ((layout.row.type != NK_LAYOUT_STATIC_ROW) && (layout.row.type != NK_LAYOUT_DYNAMIC_ROW)) return;
			if ((layout.row.type) == (NK_LAYOUT_DYNAMIC_ROW))
			{
				float ratio = (float) (ratio_or_width);
				if ((ratio + layout.row.filled) > (1.0f)) return;
				if ((ratio) > (0.0f))
					layout.row.item_width =
						(float) ((0) < ((1.0f) < (ratio) ? (1.0f) : (ratio)) ? ((1.0f) < (ratio) ? (1.0f) : (ratio)) : (0));
				else layout.row.item_width = (float) (1.0f - layout.row.filled);
			}
			else layout.row.item_width = (float) (ratio_or_width);
		}

		public static void nk_layout_row_end(nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if ((layout.row.type != NK_LAYOUT_STATIC_ROW) && (layout.row.type != NK_LAYOUT_DYNAMIC_ROW)) return;
			layout.row.item_width = (float) (0);
			layout.row.item_offset = (float) (0);
		}

		public static void nk_layout_row(nk_context ctx, int fmt, float height, int cols, float* ratio)
		{
			int i;
			int n_undef = (int) (0);
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			nk_panel_layout(ctx, win, (float) (height), (int) (cols));
			if ((fmt) == (NK_DYNAMIC))
			{
				float r = (float) (0);
				layout.row.ratio = ratio;
				for (i = (int) (0); (i) < (cols); ++i)
				{
					if ((ratio[i]) < (0.0f)) n_undef++;
					else r += (float) (ratio[i]);
				}
				r = (float) ((0) < ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) ? ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) : (0));
				layout.row.type = (int) (NK_LAYOUT_DYNAMIC);
				layout.row.item_width = (float) ((((r) > (0)) && ((n_undef) > (0))) ? (r/(float) (n_undef)) : 0);
			}
			else
			{
				layout.row.ratio = ratio;
				layout.row.type = (int) (NK_LAYOUT_STATIC);
				layout.row.item_width = (float) (0);
				layout.row.item_offset = (float) (0);
			}

			layout.row.item_offset = (float) (0);
			layout.row.filled = (float) (0);
		}

		public static void nk_layout_row_template_begin(nk_context ctx, float height)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			nk_panel_layout(ctx, win, (float) (height), (int) (1));
			layout.row.type = (int) (NK_LAYOUT_TEMPLATE);
			layout.row.columns = (int) (0);
			layout.row.ratio = null;
			layout.row.item_width = (float) (0);
			layout.row.item_height = (float) (0);
			layout.row.item_offset = (float) (0);
			layout.row.filled = (float) (0);
			layout.row.item.x = (float) (0);
			layout.row.item.y = (float) (0);
			layout.row.item.w = (float) (0);
			layout.row.item.h = (float) (0);
		}

		public static void nk_layout_row_template_push_dynamic(nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if (layout.row.type != NK_LAYOUT_TEMPLATE) return;
			if ((layout.row.columns) >= (16)) return;
			layout.row.templates[layout.row.columns++] = (float) (-1.0f);
		}

		public static void nk_layout_row_template_push_variable(nk_context ctx, float min_width)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if (layout.row.type != NK_LAYOUT_TEMPLATE) return;
			if ((layout.row.columns) >= (16)) return;
			layout.row.templates[layout.row.columns++] = (float) (-min_width);
		}

		public static void nk_layout_row_template_push_static(nk_context ctx, float width)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if (layout.row.type != NK_LAYOUT_TEMPLATE) return;
			if ((layout.row.columns) >= (16)) return;
			layout.row.templates[layout.row.columns++] = (float) (width);
		}

		public static void nk_layout_row_template_end(nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			int i = (int) (0);
			int variable_count = (int) (0);
			int min_variable_count = (int) (0);
			float min_fixed_width = (float) (0.0f);
			float total_fixed_width = (float) (0.0f);
			float max_variable_width = (float) (0.0f);
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			if (layout.row.type != NK_LAYOUT_TEMPLATE) return;
			for (i = (int) (0); (i) < (layout.row.columns); ++i)
			{
				float width = (float) (layout.row.templates[i]);
				if ((width) >= (0.0f))
				{
					total_fixed_width += (float) (width);
					min_fixed_width += (float) (width);
				}
				else if ((width) < (-1.0f))
				{
					width = (float) (-width);
					total_fixed_width += (float) (width);
					max_variable_width = (float) ((max_variable_width) < (width) ? (width) : (max_variable_width));
					variable_count++;
				}
				else
				{
					min_variable_count++;
					variable_count++;
				}
			}
			if ((variable_count) != 0)
			{
				float space =
					(float)
						(nk_layout_row_calculate_usable_space(ctx.style, (int) (layout.type), (float) (layout.bounds.w),
							(int) (layout.row.columns)));
				float var_width =
					(float) (((space - min_fixed_width) < (0.0f) ? (0.0f) : (space - min_fixed_width))/(float) (variable_count));
				int enough_space = (int) ((var_width) >= (max_variable_width) ? 1 : 0);
				if (enough_space == 0)
					var_width =
						(float) (((space - total_fixed_width) < (0) ? (0) : (space - total_fixed_width))/(float) (min_variable_count));
				for (i = (int) (0); (i) < (layout.row.columns); ++i)
				{
					float* width = (float*) layout.row.templates + i;
					*width =
						(float) (((*width) >= (0.0f)) ? *width : (((*width) < (-1.0f)) && (enough_space == 0)) ? -(*width) : var_width);
				}
			}

		}

		public static void nk_layout_space_begin(nk_context ctx, int fmt, float height, int widget_count)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			nk_panel_layout(ctx, win, (float) (height), (int) (widget_count));
			if ((fmt) == (NK_STATIC)) layout.row.type = (int) (NK_LAYOUT_STATIC_FREE);
			else layout.row.type = (int) (NK_LAYOUT_DYNAMIC_FREE);
			layout.row.ratio = null;
			layout.row.filled = (float) (0);
			layout.row.item_width = (float) (0);
			layout.row.item_offset = (float) (0);
		}

		public static void nk_layout_space_end(nk_context ctx)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			layout.row.item_width = (float) (0);
			layout.row.item_height = (float) (0);
			layout.row.item_offset = (float) (0);
			fixed (void* ptr = &layout.row.item)
			{
				nk_zero(ptr, (ulong) (sizeof (nk_rect)));
			}
		}

		public static void nk_layout_space_push(nk_context ctx, nk_rect rect)
		{
			nk_window win;
			nk_panel layout;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			layout.row.item = (nk_rect) (rect);
		}

		public static nk_rect nk_layout_space_bounds(nk_context ctx)
		{
			nk_rect ret = new nk_rect();
			nk_window win;
			nk_panel layout;
			win = ctx.current;
			layout = win.layout;
			ret.x = (float) (layout.clip.x);
			ret.y = (float) (layout.clip.y);
			ret.w = (float) (layout.clip.w);
			ret.h = (float) (layout.row.height);
			return (nk_rect) (ret);
		}

		public static nk_rect nk_layout_widget_bounds(nk_context ctx)
		{
			nk_rect ret = new nk_rect();
			nk_window win;
			nk_panel layout;
			win = ctx.current;
			layout = win.layout;
			ret.x = (float) (layout.at_x);
			ret.y = (float) (layout.at_y);
			ret.w = (float) (layout.bounds.w - ((layout.at_x - layout.bounds.x) < (0) ? (0) : (layout.at_x - layout.bounds.x)));
			ret.h = (float) (layout.row.height);
			return (nk_rect) (ret);
		}

		public static nk_vec2 nk_layout_space_to_screen(nk_context ctx, nk_vec2 ret)
		{
			nk_window win;
			nk_panel layout;
			win = ctx.current;
			layout = win.layout;
			ret.x += (float) (layout.at_x - (float) (layout.offset.x));
			ret.y += (float) (layout.at_y - (float) (layout.offset.y));
			return (nk_vec2) (ret);
		}

		public static nk_vec2 nk_layout_space_to_local(nk_context ctx, nk_vec2 ret)
		{
			nk_window win;
			nk_panel layout;
			win = ctx.current;
			layout = win.layout;
			ret.x += (float) (-layout.at_x + (float) (layout.offset.x));
			ret.y += (float) (-layout.at_y + (float) (layout.offset.y));
			return (nk_vec2) (ret);
		}

		public static nk_rect nk_layout_space_rect_to_screen(nk_context ctx, nk_rect ret)
		{
			nk_window win;
			nk_panel layout;
			win = ctx.current;
			layout = win.layout;
			ret.x += (float) (layout.at_x - (float) (layout.offset.x));
			ret.y += (float) (layout.at_y - (float) (layout.offset.y));
			return (nk_rect) (ret);
		}

		public static nk_rect nk_layout_space_rect_to_local(nk_context ctx, nk_rect ret)
		{
			nk_window win;
			nk_panel layout;
			win = ctx.current;
			layout = win.layout;
			ret.x += (float) (-layout.at_x + (float) (layout.offset.x));
			ret.y += (float) (-layout.at_y + (float) (layout.offset.y));
			return (nk_rect) (ret);
		}

		public static void nk_panel_alloc_row(nk_context ctx, nk_window win)
		{
			nk_panel layout = win.layout;
			nk_vec2 spacing = (nk_vec2) (ctx.style.window.spacing);
			float row_height = (float) (layout.row.height - spacing.y);
			nk_panel_layout(ctx, win, (float) (row_height), (int) (layout.row.columns));
		}

		public static int nk_tree_state_base(nk_context ctx, int type, nk_image img, char* title, ref int state)
		{
			nk_window win;
			nk_panel layout;
			nk_style style;
			nk_command_buffer _out_;
			nk_input _in_;
			nk_style_button button;
			int symbol;
			float row_height;
			nk_vec2 item_spacing = new nk_vec2();
			nk_rect header = new nk_rect();
			nk_rect sym = new nk_rect();
			nk_text text = new nk_text();
			uint ws = (uint) (0);
			int widget_state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			_out_ = win.buffer;
			style = ctx.style;
			item_spacing = (nk_vec2) (style.window.spacing);
			row_height = (float) (style.font.height + 2*style.tab.padding.y);
			nk_layout_set_min_row_height(ctx, (float) (row_height));
			nk_layout_row_dynamic(ctx, (float) (row_height), (int) (1));
			nk_layout_reset_min_row_height(ctx);
			widget_state = (int) (nk_widget(&header, ctx));
			if ((type) == (NK_TREE_TAB))
			{
				nk_style_item background = style.tab.background;
				if ((background.type) == (NK_STYLE_ITEM_IMAGE))
				{
					nk_draw_image(_out_, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
					text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				}
				else
				{
					text.background = (nk_color) (background.data.color);
					nk_fill_rect(_out_, (nk_rect) (header), (float) (0), (nk_color) (style.tab.border_color));
					nk_fill_rect(_out_, (nk_rect) (nk_shrink_rect_((nk_rect) (header), (float) (style.tab.border))),
						(float) (style.tab.rounding), (nk_color) (background.data.color));
				}
			}
			else text.background = (nk_color) (style.window.background);
			_in_ = ((layout.flags & NK_WINDOW_ROM) == 0) ? ctx.input : null;
			_in_ = (((_in_) != null) && ((widget_state) == (NK_WIDGET_VALID))) ? ctx.input : null;
			if ((nk_button_behavior(ref ws, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				state = (int) (((state) == (NK_MAXIMIZED)) ? NK_MINIMIZED : NK_MAXIMIZED);
			if ((state) == (NK_MAXIMIZED))
			{
				symbol = (int) (style.tab.sym_maximize);
				if ((type) == (NK_TREE_TAB)) button = style.tab.tab_maximize_button;
				else button = style.tab.node_maximize_button;
			}
			else
			{
				symbol = (int) (style.tab.sym_minimize);
				if ((type) == (NK_TREE_TAB)) button = style.tab.tab_minimize_button;
				else button = style.tab.node_minimize_button;
			}

			{
				sym.w = (float) (sym.h = (float) (style.font.height));
				sym.y = (float) (header.y + style.tab.padding.y);
				sym.x = (float) (header.x + style.tab.padding.x);
				nk_do_button_symbol(ref ws, win.buffer, (nk_rect) (sym), (int) (symbol), (int) (NK_BUTTON_DEFAULT), button, null,
					style.font);
				if ((img) != null)
				{
					sym.x = (float) (sym.x + sym.w + 4*item_spacing.x);
					nk_draw_image(win.buffer, (nk_rect) (sym), img, (nk_color) (nk_white));
					sym.w = (float) (style.font.height + style.tab.spacing.x);
				}
			}

			{
				nk_rect label = new nk_rect();
				header.w = (float) ((header.w) < (sym.w + item_spacing.x) ? (sym.w + item_spacing.x) : (header.w));
				label.x = (float) (sym.x + sym.w + item_spacing.x);
				label.y = (float) (sym.y);
				label.w = (float) (header.w - (sym.w + item_spacing.y + style.tab.indent));
				label.h = (float) (style.font.height);
				text.text = (nk_color) (style.tab.text);
				text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
				nk_widget_text(_out_, (nk_rect) (label), title, (int) (nk_strlen(title)), &text, (uint) (NK_TEXT_LEFT), style.font);
			}

			if ((state) == (NK_MAXIMIZED))
			{
				layout.at_x = (float) (header.x + (float) (layout.offset.x) + style.tab.indent);
				layout.bounds.w = (float) ((layout.bounds.w) < (style.tab.indent) ? (style.tab.indent) : (layout.bounds.w));
				layout.bounds.w -= (float) (style.tab.indent + style.window.padding.x);
				layout.row.tree_depth++;
				return (int) (nk_true);
			}
			else return (int) (nk_false);
		}

		public static int nk_tree_base(nk_context ctx, int type, nk_image img, char* title, int initial_state, char* hash,
			int len, int line)
		{
			nk_window win = ctx.current;
			int title_len = (int) (0);
			uint tree_hash = (uint) (0);
			uint* state = null;
			if (hash == null)
			{
				title_len = (int) (nk_strlen(title));
				tree_hash = (uint) (nk_murmur_hash(title, (int) (title_len), (uint) (line)));
			}
			else tree_hash = (uint) (nk_murmur_hash(hash, (int) (len), (uint) (line)));
			state = nk_find_value(win, (uint) (tree_hash));
			if (state == null)
			{
				state = nk_add_value(ctx, win, (uint) (tree_hash), (uint) (0));
				*state = (uint) (initial_state);
			}

			int kkk = (int) (*state);
			int result = (int) (nk_tree_state_base(ctx, (int) (type), img, title, ref kkk));
			*state = (uint) kkk;
			return result;
		}

		public static int nk_tree_state_push(nk_context ctx, int type, char* title, ref int state)
		{
			return (int) (nk_tree_state_base(ctx, (int) (type), null, title, ref state));
		}

		public static int nk_tree_state_image_push(nk_context ctx, int type, nk_image img, char* title, ref int state)
		{
			return (int) (nk_tree_state_base(ctx, (int) (type), img, title, ref state));
		}

		public static void nk_tree_state_pop(nk_context ctx)
		{
			nk_window win = null;
			nk_panel layout = null;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			layout.at_x -= (float) (ctx.style.tab.indent + ctx.style.window.padding.x);
			layout.bounds.w += (float) (ctx.style.tab.indent + ctx.style.window.padding.x);
			layout.row.tree_depth--;
		}

		public static int nk_tree_push_hashed(nk_context ctx, int type, char* title, int initial_state, char* hash, int len,
			int line)
		{
			return (int) (nk_tree_base(ctx, (int) (type), null, title, (int) (initial_state), hash, (int) (len), (int) (line)));
		}

		public static int nk_tree_image_push_hashed(nk_context ctx, int type, nk_image img, char* title, int initial_state,
			char* hash, int len, int seed)
		{
			return (int) (nk_tree_base(ctx, (int) (type), img, title, (int) (initial_state), hash, (int) (len), (int) (seed)));
		}

		public static void nk_tree_pop(nk_context ctx)
		{
			nk_tree_state_pop(ctx);
		}

		public static nk_rect nk_widget_bounds(nk_context ctx)
		{
			nk_rect bounds = new nk_rect();
			if ((ctx == null) || (ctx.current == null))
				return (nk_rect) (nk_rect_((float) (0), (float) (0), (float) (0), (float) (0)));
			nk_layout_peek(&bounds, ctx);
			return (nk_rect) (bounds);
		}

		public static nk_vec2 nk_widget_position(nk_context ctx)
		{
			nk_rect bounds = new nk_rect();
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			nk_layout_peek(&bounds, ctx);
			return (nk_vec2) (nk_vec2_((float) (bounds.x), (float) (bounds.y)));
		}

		public static nk_vec2 nk_widget_size(nk_context ctx)
		{
			nk_rect bounds = new nk_rect();
			if ((ctx == null) || (ctx.current == null)) return (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
			nk_layout_peek(&bounds, ctx);
			return (nk_vec2) (nk_vec2_((float) (bounds.w), (float) (bounds.h)));
		}

		public static float nk_widget_width(nk_context ctx)
		{
			nk_rect bounds = new nk_rect();
			if ((ctx == null) || (ctx.current == null)) return (float) (0);
			nk_layout_peek(&bounds, ctx);
			return (float) (bounds.w);
		}

		public static float nk_widget_height(nk_context ctx)
		{
			nk_rect bounds = new nk_rect();
			if ((ctx == null) || (ctx.current == null)) return (float) (0);
			nk_layout_peek(&bounds, ctx);
			return (float) (bounds.h);
		}

		public static int nk_widget_is_hovered(nk_context ctx)
		{
			nk_rect c = new nk_rect();
			nk_rect v = new nk_rect();
			nk_rect bounds = new nk_rect();
			if (((ctx == null) || (ctx.current == null)) || (ctx.active != ctx.current)) return (int) (0);
			c = (nk_rect) (ctx.current.layout.clip);
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			nk_layout_peek(&bounds, ctx);
			nk_unify(ref v, ref c, (float) (bounds.x), (float) (bounds.y), (float) (bounds.x + bounds.w),
				(float) (bounds.y + bounds.h));
			if (
				!(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
				    ((bounds.y + bounds.h) < (c.y))))) return (int) (0);
			return (int) (nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (bounds)));
		}

		public static int nk_widget_is_mouse_clicked(nk_context ctx, int btn)
		{
			nk_rect c = new nk_rect();
			nk_rect v = new nk_rect();
			nk_rect bounds = new nk_rect();
			if (((ctx == null) || (ctx.current == null)) || (ctx.active != ctx.current)) return (int) (0);
			c = (nk_rect) (ctx.current.layout.clip);
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			nk_layout_peek(&bounds, ctx);
			nk_unify(ref v, ref c, (float) (bounds.x), (float) (bounds.y), (float) (bounds.x + bounds.w),
				(float) (bounds.y + bounds.h));
			if (
				!(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
				    ((bounds.y + bounds.h) < (c.y))))) return (int) (0);
			return (int) (nk_input_mouse_clicked(ctx.input, (int) (btn), (nk_rect) (bounds)));
		}

		public static int nk_widget_has_mouse_click_down(nk_context ctx, int btn, int down)
		{
			nk_rect c = new nk_rect();
			nk_rect v = new nk_rect();
			nk_rect bounds = new nk_rect();
			if (((ctx == null) || (ctx.current == null)) || (ctx.active != ctx.current)) return (int) (0);
			c = (nk_rect) (ctx.current.layout.clip);
			c.x = ((float) ((int) (c.x)));
			c.y = ((float) ((int) (c.y)));
			c.w = ((float) ((int) (c.w)));
			c.h = ((float) ((int) (c.h)));
			nk_layout_peek(&bounds, ctx);
			nk_unify(ref v, ref c, (float) (bounds.x), (float) (bounds.y), (float) (bounds.x + bounds.w),
				(float) (bounds.y + bounds.h));
			if (
				!(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
				    ((bounds.y + bounds.h) < (c.y))))) return (int) (0);
			return (int) (nk_input_has_mouse_click_down_in_rect(ctx.input, (int) (btn), (nk_rect) (bounds), (int) (down)));
		}

		public static void nk_spacing(nk_context ctx, int cols)
		{
			nk_window win;
			nk_panel layout;
			nk_rect none = new nk_rect();
			int i;
			int index;
			int rows;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			layout = win.layout;
			index = (int) ((layout.row.index + cols)%layout.row.columns);
			rows = (int) ((layout.row.index + cols)/layout.row.columns);
			if ((rows) != 0)
			{
				for (i = (int) (0); (i) < (rows); ++i)
				{
					nk_panel_alloc_row(ctx, win);
				}
				cols = (int) (index);
			}

			if ((layout.row.type != NK_LAYOUT_DYNAMIC_FIXED) && (layout.row.type != NK_LAYOUT_STATIC_FIXED))
			{
				for (i = (int) (0); (i) < (cols); ++i)
				{
					nk_panel_alloc_space(&none, ctx);
				}
			}

			layout.row.index = (int) (index);
		}

		public static void nk_text_colored(nk_context ctx, char* str, int len, uint alignment, nk_color color)
		{
			nk_window win;
			nk_style style;
			nk_vec2 item_padding = new nk_vec2();
			nk_rect bounds = new nk_rect();
			nk_text text = new nk_text();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			style = ctx.style;
			nk_panel_alloc_space(&bounds, ctx);
			item_padding = (nk_vec2) (style.text.padding);
			text.padding.x = (float) (item_padding.x);
			text.padding.y = (float) (item_padding.y);
			text.background = (nk_color) (style.window.background);
			text.text = (nk_color) (color);
			nk_widget_text(win.buffer, (nk_rect) (bounds), str, (int) (len), &text, (uint) (alignment), style.font);
		}

		public static void nk_text_wrap_colored(nk_context ctx, char* str, int len, nk_color color)
		{
			nk_window win;
			nk_style style;
			nk_vec2 item_padding = new nk_vec2();
			nk_rect bounds = new nk_rect();
			nk_text text = new nk_text();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			style = ctx.style;
			nk_panel_alloc_space(&bounds, ctx);
			item_padding = (nk_vec2) (style.text.padding);
			text.padding.x = (float) (item_padding.x);
			text.padding.y = (float) (item_padding.y);
			text.background = (nk_color) (style.window.background);
			text.text = (nk_color) (color);
			nk_widget_text_wrap(win.buffer, (nk_rect) (bounds), str, (int) (len), &text, style.font);
		}

		public static void nk_text_(nk_context ctx, char* str, int len, uint alignment)
		{
			if (ctx == null) return;
			nk_text_colored(ctx, str, (int) (len), (uint) (alignment), (nk_color) (ctx.style.text.color));
		}

		public static void nk_text_wrap(nk_context ctx, char* str, int len)
		{
			if (ctx == null) return;
			nk_text_wrap_colored(ctx, str, (int) (len), (nk_color) (ctx.style.text.color));
		}

		public static void nk_label(nk_context ctx, char* str, uint alignment)
		{
			nk_text_(ctx, str, (int) (nk_strlen(str)), (uint) (alignment));
		}

		public static void nk_label_colored(nk_context ctx, char* str, uint align, nk_color color)
		{
			nk_text_colored(ctx, str, (int) (nk_strlen(str)), (uint) (align), (nk_color) (color));
		}

		public static void nk_label_wrap(nk_context ctx, char* str)
		{
			nk_text_wrap(ctx, str, (int) (nk_strlen(str)));
		}

		public static void nk_label_colored_wrap(nk_context ctx, char* str, nk_color color)
		{
			nk_text_wrap_colored(ctx, str, (int) (nk_strlen(str)), (nk_color) (color));
		}

		public static void nk_image_(nk_context ctx, nk_image img)
		{
			nk_window win;
			nk_rect bounds = new nk_rect();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			win = ctx.current;
			if (nk_widget(&bounds, ctx) == 0) return;
			nk_draw_image(win.buffer, (nk_rect) (bounds), img, (nk_color) (nk_white));
		}

		public static void nk_button_set_behavior(nk_context ctx, int behavior)
		{
			if (ctx == null) return;
			ctx.button_behavior = (int) (behavior);
		}

		public static int nk_button_push_behavior(nk_context ctx, int behavior)
		{
			nk_config_stack_button_behavior button_stack;
			nk_config_stack_button_behavior_element element;
			if (ctx == null) return (int) (0);
			button_stack = ctx.stacks.button_behaviors;
			if ((button_stack.head) >= ((int) ((int) button_stack.elements.Length))) return (int) (0);
			element = button_stack.elements[button_stack.head++];
			element.old_value = (int) (ctx.button_behavior);
			ctx.button_behavior = (int) (behavior);
			return (int) (1);
		}

		public static int nk_button_pop_behavior(nk_context ctx)
		{
			nk_config_stack_button_behavior button_stack;
			nk_config_stack_button_behavior_element element;
			if (ctx == null) return (int) (0);
			button_stack = ctx.stacks.button_behaviors;
			if ((button_stack.head) < (1)) return (int) (0);
			element = button_stack.elements[--button_stack.head];
			ctx.button_behavior = element.old_value;
			return (int) (1);
		}

		public static int nk_button_text_styled(nk_context ctx, nk_style_button style, char* title, int len)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int state;
			if ((((style == null) || (ctx == null)) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_button_text(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), title, (int) (len),
						(uint) (style.text_alignment), (int) (ctx.button_behavior), style, _in_, ctx.style.font));
		}

		public static int nk_button_text(nk_context ctx, char* title, int len)
		{
			if (ctx == null) return (int) (0);
			return (int) (nk_button_text_styled(ctx, ctx.style.button, title, (int) (len)));
		}

		public static int nk_button_label_styled(nk_context ctx, nk_style_button style, char* title)
		{
			return (int) (nk_button_text_styled(ctx, style, title, (int) (nk_strlen(title))));
		}

		public static int nk_button_label(nk_context ctx, char* title)
		{
			return (int) (nk_button_text(ctx, title, (int) (nk_strlen(title))));
		}

		public static int nk_button_color(nk_context ctx, nk_color color)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_style_button button = new nk_style_button();
			int ret = (int) (0);
			nk_rect bounds = new nk_rect();
			nk_rect content = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			button = (nk_style_button) (ctx.style.button);
			button.normal = (nk_style_item) (nk_style_item_color((nk_color) (color)));
			button.hover = (nk_style_item) (nk_style_item_color((nk_color) (color)));
			button.active = (nk_style_item) (nk_style_item_color((nk_color) (color)));
			ret =
				(int)
					(nk_do_button(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), button, _in_, (int) (ctx.button_behavior),
						&content));
			nk_draw_button(win.buffer, &bounds, (uint) (ctx.last_widget_state), button);
			return (int) (ret);
		}

		public static int nk_button_symbol_styled(nk_context ctx, nk_style_button style, int symbol)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_button_symbol(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (int) (symbol),
						(int) (ctx.button_behavior), style, _in_, ctx.style.font));
		}

		public static int nk_button_symbol(nk_context ctx, int symbol)
		{
			if (ctx == null) return (int) (0);
			return (int) (nk_button_symbol_styled(ctx, ctx.style.button, (int) (symbol)));
		}

		public static int nk_button_image_styled(nk_context ctx, nk_style_button style, nk_image img)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_button_image(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (nk_image) (img),
						(int) (ctx.button_behavior), style, _in_));
		}

		public static int nk_button_image(nk_context ctx, nk_image img)
		{
			if (ctx == null) return (int) (0);
			return (int) (nk_button_image_styled(ctx, ctx.style.button, (nk_image) (img)));
		}

		public static int nk_button_symbol_text_styled(nk_context ctx, nk_style_button style, int symbol, char* text, int len,
			uint align)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_button_text_symbol(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (int) (symbol), text,
						(int) (len), (uint) (align), (int) (ctx.button_behavior), style, ctx.style.font, _in_));
		}

		public static int nk_button_symbol_text(nk_context ctx, int symbol, char* text, int len, uint align)
		{
			if (ctx == null) return (int) (0);
			return (int) (nk_button_symbol_text_styled(ctx, ctx.style.button, (int) (symbol), text, (int) (len), (uint) (align)));
		}

		public static int nk_button_symbol_label(nk_context ctx, int symbol, char* label, uint align)
		{
			return (int) (nk_button_symbol_text(ctx, (int) (symbol), label, (int) (nk_strlen(label)), (uint) (align)));
		}

		public static int nk_button_symbol_label_styled(nk_context ctx, nk_style_button style, int symbol, char* title,
			uint align)
		{
			return
				(int) (nk_button_symbol_text_styled(ctx, style, (int) (symbol), title, (int) (nk_strlen(title)), (uint) (align)));
		}

		public static int nk_button_image_text_styled(nk_context ctx, nk_style_button style, nk_image img, char* text, int len,
			uint align)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_button_text_image(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (nk_image) (img), text,
						(int) (len), (uint) (align), (int) (ctx.button_behavior), style, ctx.style.font, _in_));
		}

		public static int nk_button_image_text(nk_context ctx, nk_image img, char* text, int len, uint align)
		{
			return
				(int) (nk_button_image_text_styled(ctx, ctx.style.button, (nk_image) (img), text, (int) (len), (uint) (align)));
		}

		public static int nk_button_image_label(nk_context ctx, nk_image img, char* label, uint align)
		{
			return (int) (nk_button_image_text(ctx, (nk_image) (img), label, (int) (nk_strlen(label)), (uint) (align)));
		}

		public static int nk_button_image_label_styled(nk_context ctx, nk_style_button style, nk_image img, char* label,
			uint text_alignment)
		{
			return
				(int)
					(nk_button_image_text_styled(ctx, style, (nk_image) (img), label, (int) (nk_strlen(label)), (uint) (text_alignment)));
		}

		public static int nk_selectable_text(nk_context ctx, char* str, int len, uint align, ref int value)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_style style;
			int state;
			nk_rect bounds = new nk_rect();
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null))) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			style = ctx.style;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_selectable(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), str, (int) (len), (uint) (align),
						ref value, style.selectable, _in_, style.font));
		}

		public static int nk_selectable_image_text(nk_context ctx, nk_image img, char* str, int len, uint align, ref int value)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_style style;
			int state;
			nk_rect bounds = new nk_rect();
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null))) return (int) (0);
			win = ctx.current;
			layout = win.layout;
			style = ctx.style;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_selectable_image(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), str, (int) (len), (uint) (align),
						ref value, img, style.selectable, _in_, style.font));
		}

		public static int nk_select_text(nk_context ctx, char* str, int len, uint align, int value)
		{
			nk_selectable_text(ctx, str, (int) (len), (uint) (align), ref value);
			return (int) (value);
		}

		public static int nk_selectable_label(nk_context ctx, char* str, uint align, ref int value)
		{
			return (int) (nk_selectable_text(ctx, str, (int) (nk_strlen(str)), (uint) (align), ref value));
		}

		public static int nk_selectable_image_label(nk_context ctx, nk_image img, char* str, uint align, ref int value)
		{
			return
				(int) (nk_selectable_image_text(ctx, (nk_image) (img), str, (int) (nk_strlen(str)), (uint) (align), ref value));
		}

		public static int nk_select_label(nk_context ctx, char* str, uint align, int value)
		{
			nk_selectable_text(ctx, str, (int) (nk_strlen(str)), (uint) (align), ref value);
			return (int) (value);
		}

		public static int nk_select_image_label(nk_context ctx, nk_image img, char* str, uint align, int value)
		{
			nk_selectable_image_text(ctx, (nk_image) (img), str, (int) (nk_strlen(str)), (uint) (align), ref value);
			return (int) (value);
		}

		public static int nk_select_image_text(nk_context ctx, nk_image img, char* str, int len, uint align, int value)
		{
			nk_selectable_image_text(ctx, (nk_image) (img), str, (int) (len), (uint) (align), ref value);
			return (int) (value);
		}

		public static int nk_check_text(nk_context ctx, char* text, int len, int active)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_style style;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (active);
			win = ctx.current;
			style = ctx.style;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (active);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			nk_do_toggle(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), &active, text, (int) (len),
				(int) (NK_TOGGLE_CHECK), style.checkbox, _in_, style.font);
			return (int) (active);
		}

		public static uint nk_check_flags_text(nk_context ctx, char* text, int len, uint flags, uint value)
		{
			int old_active;
			if ((ctx == null) || (text == null)) return (uint) (flags);
			old_active = ((int) ((flags & value) & value));
			if ((nk_check_text(ctx, text, (int) (len), (int) (old_active))) != 0) flags |= (uint) (value);
			else flags &= (uint) (~value);
			return (uint) (flags);
		}

		public static int nk_checkbox_text(nk_context ctx, char* text, int len, int* active)
		{
			int old_val;
			if (((ctx == null) || (text == null)) || (active == null)) return (int) (0);
			old_val = (int) (*active);
			*active = (int) (nk_check_text(ctx, text, (int) (len), (int) (*active)));
			return (old_val != *active) ? 1 : 0;
		}

		public static int nk_checkbox_flags_text(nk_context ctx, char* text, int len, uint* flags, uint value)
		{
			int active;
			if (((ctx == null) || (text == null)) || (flags == null)) return (int) (0);
			active = ((int) ((*flags & value) & value));
			if ((nk_checkbox_text(ctx, text, (int) (len), &active)) != 0)
			{
				if ((active) != 0) *flags |= (uint) (value);
				else *flags &= (uint) (~value);
				return (int) (1);
			}

			return (int) (0);
		}

		public static int nk_check_label(nk_context ctx, char* label, int active)
		{
			return (int) (nk_check_text(ctx, label, (int) (nk_strlen(label)), (int) (active)));
		}

		public static uint nk_check_flags_label(nk_context ctx, char* label, uint flags, uint value)
		{
			return (uint) (nk_check_flags_text(ctx, label, (int) (nk_strlen(label)), (uint) (flags), (uint) (value)));
		}

		public static int nk_checkbox_label(nk_context ctx, char* label, int* active)
		{
			return (int) (nk_checkbox_text(ctx, label, (int) (nk_strlen(label)), active));
		}

		public static int nk_checkbox_flags_label(nk_context ctx, char* label, uint* flags, uint value)
		{
			return (int) (nk_checkbox_flags_text(ctx, label, (int) (nk_strlen(label)), flags, (uint) (value)));
		}

		public static int nk_option_text(nk_context ctx, char* text, int len, int is_active)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_style style;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (is_active);
			win = ctx.current;
			style = ctx.style;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (state);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			nk_do_toggle(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), &is_active, text, (int) (len),
				(int) (NK_TOGGLE_OPTION), style.option, _in_, style.font);
			return (int) (is_active);
		}

		public static int nk_radio_text(nk_context ctx, char* text, int len, int* active)
		{
			int old_value;
			if (((ctx == null) || (text == null)) || (active == null)) return (int) (0);
			old_value = (int) (*active);
			*active = (int) (nk_option_text(ctx, text, (int) (len), (int) (old_value)));
			return (old_value != *active) ? 1 : 0;
		}

		public static int nk_option_label(nk_context ctx, char* label, int active)
		{
			return (int) (nk_option_text(ctx, label, (int) (nk_strlen(label)), (int) (active)));
		}

		public static int nk_radio_label(nk_context ctx, char* label, int* active)
		{
			return (int) (nk_radio_text(ctx, label, (int) (nk_strlen(label)), active));
		}

		public static int nk_slider_float(nk_context ctx, float min_value, ref float value, float max_value, float value_step)
		{
			nk_window win;
			nk_panel layout;
			nk_input _in_;
			nk_style style;
			int ret = (int) (0);
			float old_value;
			nk_rect bounds = new nk_rect();
			int state;
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)))
				return (int) (ret);
			win = ctx.current;
			style = ctx.style;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (ret);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			old_value = (float) (value);
			value =
				(float)
					(nk_do_slider(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (float) (min_value), (float) (old_value),
						(float) (max_value), (float) (value_step), style.slider, _in_, style.font));
			return (((old_value) > (value)) || ((old_value) < (value))) ? 1 : 0;
		}

		public static float nk_slide_float(nk_context ctx, float min, float val, float max, float step)
		{
			nk_slider_float(ctx, (float) (min), ref val, (float) (max), (float) (step));
			return (float) (val);
		}

		public static int nk_slide_int(nk_context ctx, int min, int val, int max, int step)
		{
			float value = (float) (val);
			nk_slider_float(ctx, (float) (min), ref value, (float) (max), (float) (step));
			return (int) (value);
		}

		public static int nk_slider_int(nk_context ctx, int min, ref int val, int max, int step)
		{
			int ret;
			float value = (float) (val);
			ret = (int) (nk_slider_float(ctx, (float) (min), ref value, (float) (max), (float) (step)));
			val = ((int) (value));
			return (int) (ret);
		}

		public static int nk_progress(nk_context ctx, ulong* cur, ulong max, int is_modifyable)
		{
			nk_window win;
			nk_panel layout;
			nk_style style;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int state;
			ulong old_value;
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) || (cur == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			old_value = (ulong) (*cur);
			*cur =
				(ulong)
					(nk_do_progress(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (ulong) (*cur), (ulong) (max),
						(int) (is_modifyable), style.progress, _in_));
			return (*cur != old_value) ? 1 : 0;
		}

		public static ulong nk_prog(nk_context ctx, ulong cur, ulong max, int modifyable)
		{
			nk_progress(ctx, &cur, (ulong) (max), (int) (modifyable));
			return (ulong) (cur);
		}

		public static void nk_edit_focus(nk_context ctx, uint flags)
		{
			uint hash;
			nk_window win;
			if ((ctx == null) || (ctx.current == null)) return;
			win = ctx.current;
			hash = (uint) (win.edit.seq);
			win.edit.active = (int) (nk_true);
			win.edit.name = (uint) (hash);
			if ((flags & NK_EDIT_ALWAYS_INSERT_MODE) != 0) win.edit.mode = (byte) (NK_TEXT_EDIT_MODE_INSERT);
		}

		public static void nk_edit_unfocus(nk_context ctx)
		{
			nk_window win;
			if ((ctx == null) || (ctx.current == null)) return;
			win = ctx.current;
			win.edit.active = (int) (nk_false);
			win.edit.name = (uint) (0);
		}

		public static uint nk_edit_string(nk_context ctx, uint flags, NkStr str, int max,
			NkPluginFilter filter)
		{
			uint hash;
			uint state;
			nk_text_edit edit;
			nk_window win;
			if (((ctx == null))) return (uint) (0);
			filter = (filter == null) ? nk_filter_default : filter;
			win = ctx.current;
			hash = (uint) (win.edit.seq);
			edit = ctx.text_edit;
			nk_textedit_clear_state(ctx.text_edit,
				(int) ((flags & NK_EDIT_MULTILINE) != 0 ? NK_TEXT_EDIT_MULTI_LINE : NK_TEXT_EDIT_SINGLE_LINE), filter);
			if (((win.edit.active) != 0) && ((hash) == (win.edit.name)))
			{
				if ((flags & NK_EDIT_NO_CURSOR) != 0) edit.cursor = (int) (str.len);
				else edit.cursor = (int) (win.edit.cursor);
				if ((flags & NK_EDIT_SELECTABLE) == 0)
				{
					edit.select_start = (int) (win.edit.cursor);
					edit.select_end = (int) (win.edit.cursor);
				}
				else
				{
					edit.select_start = (int) (win.edit.sel_start);
					edit.select_end = (int) (win.edit.sel_end);
				}
				edit.mode = (byte) (win.edit.mode);
				edit.scrollbar.x = ((float) (win.edit.scrollbar.x));
				edit.scrollbar.y = ((float) (win.edit.scrollbar.y));
				edit.active = (byte) (nk_true);
			}
			else edit.active = (byte) (nk_false);
			max = (int) ((1) < (max) ? (max) : (1));

			if (str.len > max)
			{
				str.str = str.str.Substring(0, max);
			}

			edit._string_ = str;
			state = (uint) (nk_edit_buffer(ctx, (uint) (flags), edit, filter));
			if ((edit.active) != 0)
			{
				win.edit.cursor = (int) (edit.cursor);
				win.edit.sel_start = (int) (edit.select_start);
				win.edit.sel_end = (int) (edit.select_end);
				win.edit.mode = (byte) (edit.mode);
				win.edit.scrollbar.x = ((uint) (edit.scrollbar.x));
				win.edit.scrollbar.y = ((uint) (edit.scrollbar.y));
			}

			return (uint) (state);
		}

		public static uint nk_edit_buffer(nk_context ctx, uint flags, nk_text_edit edit, NkPluginFilter filter)
		{
			nk_window win;
			nk_style style;
			nk_input _in_;
			int state;
			nk_rect bounds = new nk_rect();
			uint ret_flags = (uint) (0);
			byte prev_state;
			uint hash;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (uint) (0);
			win = ctx.current;
			style = ctx.style;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (uint) (state);
			_in_ = (win.layout.flags & NK_WINDOW_ROM) != 0 ? null : ctx.input;
			hash = (uint) (win.edit.seq++);
			if (((win.edit.active) != 0) && ((hash) == (win.edit.name)))
			{
				if ((flags & NK_EDIT_NO_CURSOR) != 0) edit.cursor = (int) (edit._string_.len);
				if ((flags & NK_EDIT_SELECTABLE) == 0)
				{
					edit.select_start = (int) (edit.cursor);
					edit.select_end = (int) (edit.cursor);
				}
				if ((flags & NK_EDIT_CLIPBOARD) != 0) edit.clip = (nk_clipboard) (ctx.clip);
				edit.active = ((byte) (win.edit.active));
			}
			else edit.active = (byte) (nk_false);
			edit.mode = (byte) (win.edit.mode);
			filter = (filter == null) ? nk_filter_default : filter;
			prev_state = (byte) (edit.active);
			_in_ = (flags & NK_EDIT_READ_ONLY) != 0 ? null : _in_;
			ret_flags =
				(uint)
					(nk_do_edit(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (uint) (flags), filter, edit, style.edit,
						_in_, style.font));
			if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0)
				ctx.style.cursor_active = ctx.style.cursors[NK_CURSOR_TEXT];
			if (((edit.active) != 0) && (prev_state != edit.active))
			{
				win.edit.active = (int) (nk_true);
				win.edit.name = (uint) (hash);
			}
			else if (((prev_state) != 0) && (edit.active == 0))
			{
				win.edit.active = (int) (nk_false);
			}

			return (uint) (ret_flags);
		}

		public static void nk_property_int(nk_context ctx, char* name, int min, ref int val, int max, int step,
			float inc_per_pixel)
		{
			nk_property_variant variant = new nk_property_variant();
			if ((((ctx == null) || (ctx.current == null)) || (name == null))) return;
			variant = (nk_property_variant) (nk_property_variant_int((int) (val), (int) (min), (int) (max), (int) (step)));
			nk_property_(ctx, name, &variant, (float) (inc_per_pixel), (int) (NK_FILTER_INT));
			val = (int) (variant.value.i);
		}

		public static void nk_property_float(nk_context ctx, char* name, float min, ref float val, float max, float step,
			float inc_per_pixel)
		{
			nk_property_variant variant = new nk_property_variant();
			if ((((ctx == null) || (ctx.current == null)) || (name == null))) return;
			variant =
				(nk_property_variant) (nk_property_variant_float((float) (val), (float) (min), (float) (max), (float) (step)));
			nk_property_(ctx, name, &variant, (float) (inc_per_pixel), (int) (NK_FILTER_FLOAT));
			val = (float) (variant.value.f);
		}

		public static void nk_property_double(nk_context ctx, char* name, double min, ref double val, double max, double step,
			float inc_per_pixel)
		{
			nk_property_variant variant = new nk_property_variant();
			if ((((ctx == null) || (ctx.current == null)) || (name == null))) return;
			variant =
				(nk_property_variant) (nk_property_variant_double((double) (val), (double) (min), (double) (max), (double) (step)));
			nk_property_(ctx, name, &variant, (float) (inc_per_pixel), (int) (NK_FILTER_FLOAT));
			val = (double) (variant.value.d);
		}

		public static int nk_propertyi(nk_context ctx, char* name, int min, int val, int max, int step, float inc_per_pixel)
		{
			nk_property_variant variant = new nk_property_variant();
			if (((ctx == null) || (ctx.current == null)) || (name == null)) return (int) (val);
			variant = (nk_property_variant) (nk_property_variant_int((int) (val), (int) (min), (int) (max), (int) (step)));
			nk_property_(ctx, name, &variant, (float) (inc_per_pixel), (int) (NK_FILTER_INT));
			val = (int) (variant.value.i);
			return (int) (val);
		}

		public static float nk_propertyf(nk_context ctx, char* name, float min, float val, float max, float step,
			float inc_per_pixel)
		{
			nk_property_variant variant = new nk_property_variant();
			if (((ctx == null) || (ctx.current == null)) || (name == null)) return (float) (val);
			variant =
				(nk_property_variant) (nk_property_variant_float((float) (val), (float) (min), (float) (max), (float) (step)));
			nk_property_(ctx, name, &variant, (float) (inc_per_pixel), (int) (NK_FILTER_FLOAT));
			val = (float) (variant.value.f);
			return (float) (val);
		}

		public static double nk_propertyd(nk_context ctx, char* name, double min, double val, double max, double step,
			float inc_per_pixel)
		{
			nk_property_variant variant = new nk_property_variant();
			if (((ctx == null) || (ctx.current == null)) || (name == null)) return (double) (val);
			variant =
				(nk_property_variant) (nk_property_variant_double((double) (val), (double) (min), (double) (max), (double) (step)));
			nk_property_(ctx, name, &variant, (float) (inc_per_pixel), (int) (NK_FILTER_FLOAT));
			val = (double) (variant.value.d);
			return (double) (val);
		}

		public static int nk_color_pick(nk_context ctx, nk_colorf* color, int fmt)
		{
			nk_window win;
			nk_panel layout;
			nk_style config;
			nk_input _in_;
			int state;
			nk_rect bounds = new nk_rect();
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) || (color == null)) return (int) (0);
			win = ctx.current;
			config = ctx.style;
			layout = win.layout;
			state = (int) (nk_widget(&bounds, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			return
				(int)
					(nk_do_color_picker(ref ctx.last_widget_state, win.buffer, color, (int) (fmt), (nk_rect) (bounds),
						(nk_vec2) (nk_vec2_((float) (0), (float) (0))), _in_, config.font));
		}

		public static nk_colorf nk_color_picker(nk_context ctx, nk_colorf color, int fmt)
		{
			nk_color_pick(ctx, &color, (int) (fmt));
			return (nk_colorf) (color);
		}

		public static int nk_chart_begin_colored(nk_context ctx, int type, nk_color color, nk_color highlight, int count,
			float min_value, float max_value)
		{
			nk_window win;
			nk_chart chart;
			nk_style config;
			nk_style_chart style;
			nk_style_item background;
			nk_rect bounds = new nk_rect();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			if (nk_widget(&bounds, ctx) == 0)
			{
				chart = ctx.current.layout.chart;
				return (int) (0);
			}

			win = ctx.current;
			config = ctx.style;
			chart = win.layout.chart;
			style = config.chart;

			chart.x = (float) (bounds.x + style.padding.x);
			chart.y = (float) (bounds.y + style.padding.y);
			chart.w = (float) (bounds.w - 2*style.padding.x);
			chart.h = (float) (bounds.h - 2*style.padding.y);
			chart.w = (float) ((chart.w) < (2*style.padding.x) ? (2*style.padding.x) : (chart.w));
			chart.h = (float) ((chart.h) < (2*style.padding.y) ? (2*style.padding.y) : (chart.h));
			{
				nk_chart_slot slot = chart.slots[chart.slot++];
				slot.type = (int) (type);
				slot.count = (int) (count);
				slot.color = (nk_color) (color);
				slot.highlight = (nk_color) (highlight);
				slot.min = (float) ((min_value) < (max_value) ? (min_value) : (max_value));
				slot.max = (float) ((min_value) < (max_value) ? (max_value) : (min_value));
				slot.range = (float) (slot.max - slot.min);
			}

			background = style.background;
			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(win.buffer, (nk_rect) (bounds), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				nk_fill_rect(win.buffer, (nk_rect) (bounds), (float) (style.rounding), (nk_color) (style.border_color));
				nk_fill_rect(win.buffer, (nk_rect) (nk_shrink_rect_((nk_rect) (bounds), (float) (style.border))),
					(float) (style.rounding), (nk_color) (style.background.data.color));
			}

			return (int) (1);
		}

		public static int nk_chart_begin(nk_context ctx, int type, int count, float min_value, float max_value)
		{
			return
				(int)
					(nk_chart_begin_colored(ctx, (int) (type), (nk_color) (ctx.style.chart.color),
						(nk_color) (ctx.style.chart.selected_color), (int) (count), (float) (min_value), (float) (max_value)));
		}

		public static void nk_chart_add_slot_colored(nk_context ctx, int type, nk_color color, nk_color highlight, int count,
			float min_value, float max_value)
		{
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			if ((ctx.current.layout.chart.slot) >= (4)) return;
			{
				nk_chart chart = ctx.current.layout.chart;
				nk_chart_slot slot = chart.slots[chart.slot++];
				slot.type = (int) (type);
				slot.count = (int) (count);
				slot.color = (nk_color) (color);
				slot.highlight = (nk_color) (highlight);
				slot.min = (float) ((min_value) < (max_value) ? (min_value) : (max_value));
				slot.max = (float) ((min_value) < (max_value) ? (max_value) : (min_value));
				slot.range = (float) (slot.max - slot.min);
			}

		}

		public static void nk_chart_add_slot(nk_context ctx, int type, int count, float min_value, float max_value)
		{
			nk_chart_add_slot_colored(ctx, (int) (type), (nk_color) (ctx.style.chart.color),
				(nk_color) (ctx.style.chart.selected_color), (int) (count), (float) (min_value), (float) (max_value));
		}

		public static uint nk_chart_push_line(nk_context ctx, nk_window win, nk_chart g, float value, int slot)
		{
			nk_panel layout = win.layout;
			nk_input i = ctx.input;
			nk_command_buffer _out_ = win.buffer;
			uint ret = (uint) (0);
			nk_vec2 cur = new nk_vec2();
			nk_rect bounds = new nk_rect();
			nk_color color = new nk_color();
			float step;
			float range;
			float ratio;
			step = (float) (g.w/(float) (g.slots[slot].count));
			range = (float) (g.slots[slot].max - g.slots[slot].min);
			ratio = (float) ((value - g.slots[slot].min)/range);
			if ((g.slots[slot].index) == (0))
			{
				g.slots[slot].last.x = (float) (g.x);
				g.slots[slot].last.y = (float) ((g.y + g.h) - ratio*g.h);
				bounds.x = (float) (g.slots[slot].last.x - 2);
				bounds.y = (float) (g.slots[slot].last.y - 2);
				bounds.w = (float) (bounds.h = (float) (4));
				color = (nk_color) (g.slots[slot].color);
				if (((layout.flags & NK_WINDOW_ROM) == 0) &&
				    ((((g.slots[slot].last.x - 3) <= (i.mouse.pos.x)) && ((i.mouse.pos.x) < (g.slots[slot].last.x - 3 + 6))) &&
				     (((g.slots[slot].last.y - 3) <= (i.mouse.pos.y)) && ((i.mouse.pos.y) < (g.slots[slot].last.y - 3 + 6)))))
				{
					ret = (uint) ((nk_input_is_mouse_hovering_rect(i, (nk_rect) (bounds))) != 0 ? NK_CHART_HOVERING : 0);
					ret |=
						(uint)
							((((i.mouse.buttons[NK_BUTTON_LEFT].down) != 0) && ((i.mouse.buttons[NK_BUTTON_LEFT].clicked) != 0))
								? NK_CHART_CLICKED
								: 0);
					color = (nk_color) (g.slots[slot].highlight);
				}
				nk_fill_rect(_out_, (nk_rect) (bounds), (float) (0), (nk_color) (color));
				g.slots[slot].index += (int) (1);
				return (uint) (ret);
			}

			color = (nk_color) (g.slots[slot].color);
			cur.x = (float) (g.x + (step*(float) (g.slots[slot].index)));
			cur.y = (float) ((g.y + g.h) - (ratio*g.h));
			nk_stroke_line(_out_, (float) (g.slots[slot].last.x), (float) (g.slots[slot].last.y), (float) (cur.x),
				(float) (cur.y), (float) (1.0f), (nk_color) (color));
			bounds.x = (float) (cur.x - 3);
			bounds.y = (float) (cur.y - 3);
			bounds.w = (float) (bounds.h = (float) (6));
			if ((layout.flags & NK_WINDOW_ROM) == 0)
			{
				if ((nk_input_is_mouse_hovering_rect(i, (nk_rect) (bounds))) != 0)
				{
					ret = (uint) (NK_CHART_HOVERING);
					ret |=
						(uint)
							(((i.mouse.buttons[NK_BUTTON_LEFT].down == 0) && ((i.mouse.buttons[NK_BUTTON_LEFT].clicked) != 0))
								? NK_CHART_CLICKED
								: 0);
					color = (nk_color) (g.slots[slot].highlight);
				}
			}

			nk_fill_rect(_out_, (nk_rect) (nk_rect_((float) (cur.x - 2), (float) (cur.y - 2), (float) (4), (float) (4))),
				(float) (0), (nk_color) (color));
			g.slots[slot].last.x = (float) (cur.x);
			g.slots[slot].last.y = (float) (cur.y);
			g.slots[slot].index += (int) (1);
			return (uint) (ret);
		}

		public static uint nk_chart_push_column(nk_context ctx, nk_window win, nk_chart chart, float value, int slot)
		{
			nk_command_buffer _out_ = win.buffer;
			nk_input _in_ = ctx.input;
			nk_panel layout = win.layout;
			float ratio;
			uint ret = (uint) (0);
			nk_color color = new nk_color();
			nk_rect item = new nk_rect();
			if ((chart.slots[slot].index) >= (chart.slots[slot].count)) return (uint) (nk_false);
			if ((chart.slots[slot].count) != 0)
			{
				float padding = (float) (chart.slots[slot].count - 1);
				item.w = (float) ((chart.w - padding)/(float) (chart.slots[slot].count));
			}

			color = (nk_color) (chart.slots[slot].color);
			item.h =
				(float)
					(chart.h*
					 (((value/chart.slots[slot].range) < (0)) ? -(value/chart.slots[slot].range) : (value/chart.slots[slot].range)));
			if ((value) >= (0))
			{
				ratio =
					(float)
						((value + (((chart.slots[slot].min) < (0)) ? -(chart.slots[slot].min) : (chart.slots[slot].min)))/
						 (((chart.slots[slot].range) < (0)) ? -(chart.slots[slot].range) : (chart.slots[slot].range)));
				item.y = (float) ((chart.y + chart.h) - chart.h*ratio);
			}
			else
			{
				ratio = (float) ((value - chart.slots[slot].max)/chart.slots[slot].range);
				item.y = (float) (chart.y + (chart.h*(((ratio) < (0)) ? -(ratio) : (ratio))) - item.h);
			}

			item.x = (float) (chart.x + ((float) (chart.slots[slot].index)*item.w));
			item.x = (float) (item.x + ((float) (chart.slots[slot].index)));
			if (((layout.flags & NK_WINDOW_ROM) == 0) &&
			    ((((item.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (item.x + item.w))) &&
			     (((item.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (item.y + item.h)))))
			{
				ret = (uint) (NK_CHART_HOVERING);
				ret |=
					(uint)
						(((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->down == 0) &&
						  ((((nk_mouse_button*) _in_.mouse.buttons + NK_BUTTON_LEFT)->clicked) != 0))
							? NK_CHART_CLICKED
							: 0);
				color = (nk_color) (chart.slots[slot].highlight);
			}

			nk_fill_rect(_out_, (nk_rect) (item), (float) (0), (nk_color) (color));
			chart.slots[slot].index += (int) (1);
			return (uint) (ret);
		}

		public static uint nk_chart_push_slot(nk_context ctx, float value, int slot)
		{
			uint flags;
			nk_window win;
			if (((ctx == null) || (ctx.current == null)) || ((slot) >= (4))) return (uint) (nk_false);
			if ((slot) >= (ctx.current.layout.chart.slot)) return (uint) (nk_false);
			win = ctx.current;
			if ((win.layout.chart.slot) < (slot)) return (uint) (nk_false);
			switch (win.layout.chart.slots[slot].type)
			{
				case NK_CHART_LINES:
					flags = (uint) (nk_chart_push_line(ctx, win, win.layout.chart, (float) (value), (int) (slot)));
					break;
				case NK_CHART_COLUMN:
					flags = (uint) (nk_chart_push_column(ctx, win, win.layout.chart, (float) (value), (int) (slot)));
					break;
				default:
				case NK_CHART_MAX:
					flags = (uint) (0);
					break;
			}

			return (uint) (flags);
		}

		public static uint nk_chart_push(nk_context ctx, float value)
		{
			return (uint) (nk_chart_push_slot(ctx, (float) (value), (int) (0)));
		}

		public static void nk_chart_end(nk_context ctx)
		{
			nk_window win;
			nk_chart chart;
			if ((ctx == null) || (ctx.current == null)) return;
			win = ctx.current;
			chart = win.layout.chart;

			return;
		}

		public static void nk_plot(nk_context ctx, int type, float* values, int count, int offset)
		{
			int i = (int) (0);
			float min_value;
			float max_value;
			if (((ctx == null) || (values == null)) || (count == 0)) return;
			min_value = (float) (values[offset]);
			max_value = (float) (values[offset]);
			for (i = (int) (0); (i) < (count); ++i)
			{
				min_value = (float) ((values[i + offset]) < (min_value) ? (values[i + offset]) : (min_value));
				max_value = (float) ((values[i + offset]) < (max_value) ? (max_value) : (values[i + offset]));
			}
			if ((nk_chart_begin(ctx, (int) (type), (int) (count), (float) (min_value), (float) (max_value))) != 0)
			{
				for (i = (int) (0); (i) < (count); ++i)
				{
					nk_chart_push(ctx, (float) (values[i + offset]));
				}
				nk_chart_end(ctx);
			}

		}

		public static void nk_plot_function(nk_context ctx, int type, void* userdata, NkFloatValueGetter value_getter,
			int count, int offset)
		{
			int i = (int) (0);
			float min_value;
			float max_value;
			if (((ctx == null) || (value_getter == null)) || (count == 0)) return;
			max_value = (float) (min_value = (float) (value_getter(userdata, (int) (offset))));
			for (i = (int) (0); (i) < (count); ++i)
			{
				float value = (float) (value_getter(userdata, (int) (i + offset)));
				min_value = (float) ((value) < (min_value) ? (value) : (min_value));
				max_value = (float) ((value) < (max_value) ? (max_value) : (value));
			}
			if ((nk_chart_begin(ctx, (int) (type), (int) (count), (float) (min_value), (float) (max_value))) != 0)
			{
				for (i = (int) (0); (i) < (count); ++i)
				{
					nk_chart_push(ctx, (float) (value_getter(userdata, (int) (i + offset))));
				}
				nk_chart_end(ctx);
			}

		}

		public static int nk_group_scrolled_offset_begin(nk_context ctx, nk_scroll offset, char* title, uint flags)
		{
			nk_rect bounds = new nk_rect();
			nk_window panel = new nk_window();
			nk_window win;
			win = ctx.current;
			nk_panel_alloc_space(&bounds, ctx);
			{
				if (
					(!(!(((((bounds.x) > (win.layout.clip.x + win.layout.clip.w)) || ((bounds.x + bounds.w) < (win.layout.clip.x))) ||
					      ((bounds.y) > (win.layout.clip.y + win.layout.clip.h))) || ((bounds.y + bounds.h) < (win.layout.clip.y))))) &&
					((flags & NK_WINDOW_MOVABLE) == 0))
				{
					return (int) (0);
				}
			}

			if ((win.flags & NK_WINDOW_ROM) != 0) flags |= (uint) (NK_WINDOW_ROM);

			panel.bounds = (nk_rect) (bounds);
			panel.flags = (uint) (flags);
			panel.scrollbar.x = offset.x;
			panel.scrollbar.y = offset.y;
			panel.buffer = (nk_command_buffer) (win.buffer);
			panel.layout = (nk_panel) (nk_create_panel(ctx));
			ctx.current = panel;
			nk_panel_begin(ctx, (flags & NK_WINDOW_TITLE) != 0 ? title : null, (int) (NK_PANEL_GROUP));
			win.buffer = (nk_command_buffer) (panel.buffer);
			win.buffer.clip = (nk_rect) (panel.layout.clip);
			panel.layout.offset = offset;

			panel.layout.parent = win.layout;
			win.layout = panel.layout;
			ctx.current = win;
			if (((panel.layout.flags & NK_WINDOW_CLOSED) != 0) || ((panel.layout.flags & NK_WINDOW_MINIMIZED) != 0))
			{
				uint f = (uint) (panel.layout.flags);
				nk_group_scrolled_end(ctx);
				if ((f & NK_WINDOW_CLOSED) != 0) return (int) (NK_WINDOW_CLOSED);
				if ((f & NK_WINDOW_MINIMIZED) != 0) return (int) (NK_WINDOW_MINIMIZED);
			}

			return (int) (1);
		}

		public static void nk_group_scrolled_end(nk_context ctx)
		{
			nk_window win;
			nk_panel parent;
			nk_panel g;
			nk_rect clip = new nk_rect();
			nk_window pan = new nk_window();
			nk_vec2 panel_padding = new nk_vec2();
			if ((ctx == null) || (ctx.current == null)) return;
			win = ctx.current;
			g = win.layout;
			parent = g.parent;

			panel_padding = (nk_vec2) (nk_panel_get_padding(ctx.style, (int) (NK_PANEL_GROUP)));
			pan.bounds.y = (float) (g.bounds.y - (g.header_height + g.menu.h));
			pan.bounds.x = (float) (g.bounds.x - panel_padding.x);
			pan.bounds.w = (float) (g.bounds.w + 2*panel_padding.x);
			pan.bounds.h = (float) (g.bounds.h + g.header_height + g.menu.h);
			if ((g.flags & NK_WINDOW_BORDER) != 0)
			{
				pan.bounds.x -= (float) (g.border);
				pan.bounds.y -= (float) (g.border);
				pan.bounds.w += (float) (2*g.border);
				pan.bounds.h += (float) (2*g.border);
			}

			if ((g.flags & NK_WINDOW_NO_SCROLLBAR) == 0)
			{
				pan.bounds.w += (float) (ctx.style.window.scrollbar_size.x);
				pan.bounds.h += (float) (ctx.style.window.scrollbar_size.y);
			}

			pan.scrollbar.x = (uint) (g.offset.x);
			pan.scrollbar.y = (uint) (g.offset.y);
			pan.flags = (uint) (g.flags);
			pan.buffer = (nk_command_buffer) (win.buffer);
			pan.layout = g;
			pan.parent = win;
			ctx.current = pan;
			nk_unify(ref clip, ref parent.clip, (float) (pan.bounds.x), (float) (pan.bounds.y),
				(float) (pan.bounds.x + pan.bounds.w), (float) (pan.bounds.y + pan.bounds.h + panel_padding.x));
			nk_push_scissor(pan.buffer, (nk_rect) (clip));
			nk_end(ctx);
			win.buffer = (nk_command_buffer) (pan.buffer);
			nk_push_scissor(win.buffer, (nk_rect) (parent.clip));
			ctx.current = win;
			win.layout = parent;
			g.bounds = (nk_rect) (pan.bounds);
			return;
		}

		public static int nk_group_scrolled_begin(nk_context ctx, nk_scroll scroll, char* title, uint flags)
		{
			return (int) (nk_group_scrolled_offset_begin(ctx, scroll, title, (uint) (flags)));
		}

		public static int nk_group_begin_titled(nk_context ctx, char* id, char* title, uint flags)
		{
			int id_len;
			uint id_hash;
			nk_window win;
			uint* x_offset;
			uint* y_offset;
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) || (id == null)) return (int) (0);
			win = ctx.current;
			id_len = (int) (nk_strlen(id));
			id_hash = (uint) (nk_murmur_hash(id, (int) (id_len), (uint) (NK_PANEL_GROUP)));
			x_offset = nk_find_value(win, (uint) (id_hash));
			if (x_offset == null)
			{
				x_offset = nk_add_value(ctx, win, (uint) (id_hash), (uint) (0));
				y_offset = nk_add_value(ctx, win, (uint) (id_hash + 1), (uint) (0));
				if ((x_offset == null) || (y_offset == null)) return (int) (0);
				*x_offset = (uint) (*y_offset = (uint) (0));
			}
			else y_offset = nk_find_value(win, (uint) (id_hash + 1));
			return
				(int) (nk_group_scrolled_offset_begin(ctx, new nk_scroll {x = *x_offset, y = *y_offset}, title, (uint) (flags)));
		}

		public static int nk_group_begin(nk_context ctx, char* title, uint flags)
		{
			return (int) (nk_group_begin_titled(ctx, title, title, (uint) (flags)));
		}

		public static void nk_group_end(nk_context ctx)
		{
			nk_group_scrolled_end(ctx);
		}

		public static int nk_list_view_begin(nk_context ctx, nk_list_view view, char* title, uint flags, int row_height,
			int row_count)
		{
			int title_len;
			uint title_hash;
			uint* x_offset;
			uint* y_offset;
			int result;
			nk_window win;
			nk_panel layout;
			nk_style style;
			nk_vec2 item_spacing = new nk_vec2();
			if (((ctx == null) || (view == null)) || (title == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			item_spacing = (nk_vec2) (style.window.spacing);
			row_height += (int) ((0) < ((int) (item_spacing.y)) ? ((int) (item_spacing.y)) : (0));
			title_len = (int) (nk_strlen(title));
			title_hash = (uint) (nk_murmur_hash(title, (int) (title_len), (uint) (NK_PANEL_GROUP)));
			x_offset = nk_find_value(win, (uint) (title_hash));
			if (x_offset == null)
			{
				x_offset = nk_add_value(ctx, win, (uint) (title_hash), (uint) (0));
				y_offset = nk_add_value(ctx, win, (uint) (title_hash + 1), (uint) (0));
				if ((x_offset == null) || (y_offset == null)) return (int) (0);
				*x_offset = (uint) (*y_offset = (uint) (0));
			}
			else y_offset = nk_find_value(win, (uint) (title_hash + 1));
			view.scroll_value = *y_offset;
			view.scroll_pointer = y_offset;
			*y_offset = (uint) (0);
			result =
				(int) (nk_group_scrolled_offset_begin(ctx, new nk_scroll {x = *x_offset, y = *y_offset}, title, (uint) (flags)));
			win = ctx.current;
			layout = win.layout;
			view.total_height = (int) (row_height*((row_count) < (1) ? (1) : (row_count)));
			view.begin =
				((int)
					(((float) (view.scroll_value)/(float) (row_height)) < (0.0f)
						? (0.0f)
						: ((float) (view.scroll_value)/(float) (row_height))));
			view.count =
				(int)
					((nk_iceilf((float) ((layout.clip.h)/(float) (row_height)))) < (0)
						? (0)
						: (nk_iceilf((float) ((layout.clip.h)/(float) (row_height)))));
			view.end = (int) (view.begin + view.count);
			view.ctx = ctx;
			return (int) (result);
		}

		public static int nk_popup_begin(nk_context ctx, int type, char* title, uint flags, nk_rect rect)
		{
			nk_window popup;
			nk_window win;
			nk_panel panel;
			int title_len;
			uint title_hash;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			panel = win.layout;
			title_len = (int) (nk_strlen(title));
			title_hash = (uint) (nk_murmur_hash(title, (int) (title_len), (uint) (NK_PANEL_POPUP)));
			popup = win.popup.win;
			if (popup == null)
			{
				popup = (nk_window) (nk_create_window(ctx));
				popup.parent = win;
				win.popup.win = popup;
				win.popup.active = (int) (0);
				win.popup.type = (int) (NK_PANEL_POPUP);
			}

			if (win.popup.name != title_hash)
			{
				if (win.popup.active == 0)
				{
					win.popup.name = (uint) (title_hash);
					win.popup.active = (int) (1);
					win.popup.type = (int) (NK_PANEL_POPUP);
				}
				else return (int) (0);
			}

			ctx.current = popup;
			rect.x += (float) (win.layout.clip.x);
			rect.y += (float) (win.layout.clip.y);
			popup.parent = win;
			popup.bounds = (nk_rect) (rect);
			popup.seq = (uint) (ctx.seq);
			popup.layout = (nk_panel) (nk_create_panel(ctx));
			popup.flags = (uint) (flags);
			popup.flags |= (uint) (NK_WINDOW_BORDER);
			if ((type) == (NK_POPUP_DYNAMIC)) popup.flags |= (uint) (NK_WINDOW_DYNAMIC);
			nk_start_popup(ctx, win);
			popup.buffer = (nk_command_buffer) (win.buffer);
			nk_push_scissor(popup.buffer, (nk_rect) (nk_null_rect));
			if ((nk_panel_begin(ctx, title, (int) (NK_PANEL_POPUP))) != 0)
			{
				nk_panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (NK_WINDOW_ROM);
					root.flags &= (uint) (~(uint) (NK_WINDOW_REMOVE_ROM));
					root = root.parent;
				}
				win.popup.active = (int) (1);
				popup.layout.offset = popup.scrollbar;
				popup.layout.parent = win.layout;
				return (int) (1);
			}
			else
			{
				nk_panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (NK_WINDOW_REMOVE_ROM);
					root = root.parent;
				}
				win.popup.active = (int) (0);
				ctx.current = win;
				nk_free_panel(ctx, popup.layout);
				popup.layout = null;
				return (int) (0);
			}

		}

		public static int nk_nonblock_begin(nk_context ctx, uint flags, nk_rect body, nk_rect header, int panel_type)
		{
			nk_window popup;
			nk_window win;
			nk_panel panel;
			int is_active = (int) (nk_true);
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			panel = win.layout;
			popup = win.popup.win;
			if (popup == null)
			{
				popup = (nk_window) (nk_create_window(ctx));
				popup.parent = win;
				win.popup.win = popup;
				win.popup.type = (int) (panel_type);
				nk_command_buffer_init(popup.buffer, (int) (NK_CLIPPING_ON));
			}
			else
			{
				int pressed;
				int in_body;
				int in_header;
				pressed = (int) (nk_input_is_mouse_pressed(ctx.input, (int) (NK_BUTTON_LEFT)));
				in_body = (int) (nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (body)));
				in_header = (int) (nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (header)));
				if (((pressed) != 0) && ((in_body == 0) || ((in_header) != 0))) is_active = (int) (nk_false);
			}

			win.popup.header = (nk_rect) (header);
			if (is_active == 0)
			{
				nk_panel root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (NK_WINDOW_REMOVE_ROM);
					root = root.parent;
				}
				return (int) (is_active);
			}

			popup.bounds = (nk_rect) (body);
			popup.parent = win;
			popup.layout = (nk_panel) (nk_create_panel(ctx));
			popup.flags = (uint) (flags);
			popup.flags |= (uint) (NK_WINDOW_BORDER);
			popup.flags |= (uint) (NK_WINDOW_DYNAMIC);
			popup.seq = (uint) (ctx.seq);
			win.popup.active = (int) (1);
			nk_start_popup(ctx, win);
			popup.buffer = (nk_command_buffer) (win.buffer);
			nk_push_scissor(popup.buffer, (nk_rect) (nk_null_rect));
			ctx.current = popup;
			nk_panel_begin(ctx, null, (int) (panel_type));
			win.buffer = (nk_command_buffer) (popup.buffer);
			popup.layout.parent = win.layout;
			popup.layout.offset = popup.scrollbar;

			{
				nk_panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (NK_WINDOW_ROM);
					root = root.parent;
				}
			}

			return (int) (is_active);
		}

		public static void nk_popup_close(nk_context ctx)
		{
			nk_window popup;
			if ((ctx == null) || (ctx.current == null)) return;
			popup = ctx.current;
			popup.flags |= (uint) (NK_WINDOW_HIDDEN);
		}

		public static void nk_popup_end(nk_context ctx)
		{
			nk_window win;
			nk_window popup;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			popup = ctx.current;
			if (popup.parent == null) return;
			win = popup.parent;
			if ((popup.flags & NK_WINDOW_HIDDEN) != 0)
			{
				nk_panel root;
				root = win.layout;
				while ((root) != null)
				{
					root.flags |= (uint) (NK_WINDOW_REMOVE_ROM);
					root = root.parent;
				}
				win.popup.active = (int) (0);
			}

			nk_push_scissor(popup.buffer, (nk_rect) (nk_null_rect));
			nk_end(ctx);
			win.buffer = (nk_command_buffer) (popup.buffer);
			nk_finish_popup(ctx, win);
			ctx.current = win;
			nk_push_scissor(win.buffer, (nk_rect) (win.layout.clip));
		}

		public static int nk_tooltip_begin(nk_context ctx, float width)
		{
			int x;
			int y;
			int w;
			int h;
			nk_window win;
			nk_input _in_;
			nk_rect bounds = new nk_rect();
			int ret;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			_in_ = ctx.input;
			if (((win.popup.win) != null) && ((win.popup.type & NK_PANEL_SET_NONBLOCK) != 0)) return (int) (0);
			w = (int) (nk_iceilf((float) (width)));
			h = (int) (nk_iceilf((float) (nk_null_rect.h)));
			x = (int) (nk_ifloorf((float) (_in_.mouse.pos.x + 1)) - (int) (win.layout.clip.x));
			y = (int) (nk_ifloorf((float) (_in_.mouse.pos.y + 1)) - (int) (win.layout.clip.y));
			bounds.x = ((float) (x));
			bounds.y = ((float) (y));
			bounds.w = ((float) (w));
			bounds.h = ((float) (h));
			ret =
				(int)
					(nk_popup_begin(ctx, (int) (NK_POPUP_DYNAMIC), "__##Tooltip##__",
						(uint) (NK_WINDOW_NO_SCROLLBAR | NK_WINDOW_BORDER), (nk_rect) (bounds)));
			if ((ret) != 0) win.layout.flags &= (uint) (~(uint) (NK_WINDOW_ROM));
			win.popup.type = (int) (NK_PANEL_TOOLTIP);
			ctx.current.layout.type = (int) (NK_PANEL_TOOLTIP);
			return (int) (ret);
		}

		public static void nk_tooltip_end(nk_context ctx)
		{
			if ((ctx == null) || (ctx.current == null)) return;
			ctx.current.seq--;
			nk_popup_close(ctx);
			nk_popup_end(ctx);
		}

		public static void nk_tooltip(nk_context ctx, char* text)
		{
			nk_style style;
			nk_vec2 padding = new nk_vec2();
			int text_len;
			float text_width;
			float text_height;
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) || (text == null)) return;
			style = ctx.style;
			padding = (nk_vec2) (style.window.padding);
			text_len = (int) (nk_strlen(text));
			text_width =
				(float) (style.font.width((nk_handle) (style.font.userdata), (float) (style.font.height), text, (int) (text_len)));
			text_width += (float) (4*padding.x);
			text_height = (float) (style.font.height + 2*padding.y);
			if ((nk_tooltip_begin(ctx, (float) (text_width))) != 0)
			{
				nk_layout_row_dynamic(ctx, (float) (text_height), (int) (1));
				nk_text_(ctx, text, (int) (text_len), (uint) (NK_TEXT_LEFT));
				nk_tooltip_end(ctx);
			}

		}

		public static int nk_contextual_begin(nk_context ctx, uint flags, nk_vec2 size, nk_rect trigger_bounds)
		{
			nk_window win;
			nk_window popup;
			nk_rect body = new nk_rect();
			nk_rect null_rect = new nk_rect();
			int is_clicked = (int) (0);
			int is_active = (int) (0);
			int is_open = (int) (0);
			int ret = (int) (0);
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			++win.popup.con_count;
			popup = win.popup.win;
			is_open = (int) (((popup) != null) && ((win.popup.type) == (NK_PANEL_CONTEXTUAL)) ? 1 : 0);
			is_clicked = (int) (nk_input_mouse_clicked(ctx.input, (int) (NK_BUTTON_RIGHT), (nk_rect) (trigger_bounds)));
			if (((win.popup.active_con) != 0) && (win.popup.con_count != win.popup.active_con)) return (int) (0);
			if (((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) ||
			    (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int) (0);
			win.popup.active_con = (uint) (win.popup.con_count);
			if ((is_clicked) != 0)
			{
				body.x = (float) (ctx.input.mouse.pos.x);
				body.y = (float) (ctx.input.mouse.pos.y);
			}
			else
			{
				body.x = (float) (popup.bounds.x);
				body.y = (float) (popup.bounds.y);
			}

			body.w = (float) (size.x);
			body.h = (float) (size.y);
			ret =
				(int)
					(nk_nonblock_begin(ctx, (uint) (flags | NK_WINDOW_NO_SCROLLBAR), (nk_rect) (body), (nk_rect) (null_rect),
						(int) (NK_PANEL_CONTEXTUAL)));
			if ((ret) != 0) win.popup.type = (int) (NK_PANEL_CONTEXTUAL);
			else
			{
				win.popup.active_con = (uint) (0);
				if ((win.popup.win) != null) win.popup.win.flags = (uint) (0);
			}

			return (int) (ret);
		}

		public static int nk_contextual_item_text(nk_context ctx, char* text, int len, uint alignment)
		{
			nk_window win;
			nk_input _in_;
			nk_style style;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			state = (int) (nk_widget_fitting(&bounds, ctx, (nk_vec2) (style.contextual_button.padding)));
			if (state == 0) return (int) (nk_false);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_text(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), text, (int) (len), (uint) (alignment),
					(int) (NK_BUTTON_DEFAULT), style.contextual_button, _in_, style.font)) != 0)
			{
				nk_contextual_close(ctx);
				return (int) (nk_true);
			}

			return (int) (nk_false);
		}

		public static int nk_contextual_item_label(nk_context ctx, char* label, uint align)
		{
			return (int) (nk_contextual_item_text(ctx, label, (int) (nk_strlen(label)), (uint) (align)));
		}

		public static int nk_contextual_item_image_text(nk_context ctx, nk_image img, char* text, int len, uint align)
		{
			nk_window win;
			nk_input _in_;
			nk_style style;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			state = (int) (nk_widget_fitting(&bounds, ctx, (nk_vec2) (style.contextual_button.padding)));
			if (state == 0) return (int) (nk_false);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_text_image(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (nk_image) (img), text,
					(int) (len), (uint) (align), (int) (NK_BUTTON_DEFAULT), style.contextual_button, style.font, _in_)) != 0)
			{
				nk_contextual_close(ctx);
				return (int) (nk_true);
			}

			return (int) (nk_false);
		}

		public static int nk_contextual_item_image_label(nk_context ctx, nk_image img, char* label, uint align)
		{
			return (int) (nk_contextual_item_image_text(ctx, (nk_image) (img), label, (int) (nk_strlen(label)), (uint) (align)));
		}

		public static int nk_contextual_item_symbol_text(nk_context ctx, int symbol, char* text, int len, uint align)
		{
			nk_window win;
			nk_input _in_;
			nk_style style;
			nk_rect bounds = new nk_rect();
			int state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			state = (int) (nk_widget_fitting(&bounds, ctx, (nk_vec2) (style.contextual_button.padding)));
			if (state == 0) return (int) (nk_false);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_text_symbol(ref ctx.last_widget_state, win.buffer, (nk_rect) (bounds), (int) (symbol), text,
					(int) (len), (uint) (align), (int) (NK_BUTTON_DEFAULT), style.contextual_button, style.font, _in_)) != 0)
			{
				nk_contextual_close(ctx);
				return (int) (nk_true);
			}

			return (int) (nk_false);
		}

		public static int nk_contextual_item_symbol_label(nk_context ctx, int symbol, char* text, uint align)
		{
			return (int) (nk_contextual_item_symbol_text(ctx, (int) (symbol), text, (int) (nk_strlen(text)), (uint) (align)));
		}

		public static void nk_contextual_close(nk_context ctx)
		{
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return;
			nk_popup_close(ctx);
		}

		public static void nk_contextual_end(nk_context ctx)
		{
			nk_window popup;
			nk_panel panel;
			if ((ctx == null) || (ctx.current == null)) return;
			popup = ctx.current;
			panel = popup.layout;
			if ((panel.flags & NK_WINDOW_DYNAMIC) != 0)
			{
				nk_rect body = new nk_rect();
				if ((panel.at_y) < (panel.bounds.y + panel.bounds.h))
				{
					nk_vec2 padding = (nk_vec2) (nk_panel_get_padding(ctx.style, (int) (panel.type)));
					body = (nk_rect) (panel.bounds);
					body.y = (float) (panel.at_y + panel.footer_height + panel.border + padding.y + panel.row.height);
					body.h = (float) ((panel.bounds.y + panel.bounds.h) - body.y);
				}
				{
					int pressed = (int) (nk_input_is_mouse_pressed(ctx.input, (int) (NK_BUTTON_LEFT)));
					int in_body = (int) (nk_input_is_mouse_hovering_rect(ctx.input, (nk_rect) (body)));
					if (((pressed) != 0) && ((in_body) != 0)) popup.flags |= (uint) (NK_WINDOW_HIDDEN);
				}
			}

			if ((popup.flags & NK_WINDOW_HIDDEN) != 0) popup.seq = (uint) (0);
			nk_popup_end(ctx);
			return;
		}

		public static int nk_combo_begin(nk_context ctx, nk_window win, nk_vec2 size, int is_clicked, nk_rect header)
		{
			nk_window popup;
			int is_open = (int) (0);
			int is_active = (int) (0);
			nk_rect body = new nk_rect();
			uint hash;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			popup = win.popup.win;
			body.x = (float) (header.x);
			body.w = (float) (size.x);
			body.y = (float) (header.y + header.h - ctx.style.window.combo_border);
			body.h = (float) (size.y);
			hash = (uint) (win.popup.combo_count++);
			is_open = (int) ((popup != null) ? nk_true : nk_false);
			is_active =
				(int) ((((popup) != null) && ((win.popup.name) == (hash))) && ((win.popup.type) == (NK_PANEL_COMBO)) ? 1 : 0);
			if ((((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) || (((is_open) != 0) && (is_active == 0))) ||
			    (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int) (0);
			if (
				nk_nonblock_begin(ctx, (uint) (0), (nk_rect) (body),
					(nk_rect)
						((((is_clicked) != 0) && ((is_open) != 0)) ? nk_rect_((float) (0), (float) (0), (float) (0), (float) (0)) : header),
					(int) (NK_PANEL_COMBO)) == 0) return (int) (0);
			win.popup.type = (int) (NK_PANEL_COMBO);
			win.popup.name = (uint) (hash);
			return (int) (1);
		}

		public static int nk_combo_begin_text(nk_context ctx, char* selected, int len, nk_vec2 size)
		{
			nk_input _in_;
			nk_window win;
			nk_style style;
			int s;
			int is_clicked = (int) (nk_false);
			nk_rect header = new nk_rect();
			nk_style_item background;
			nk_text text = new nk_text();
			if ((((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) || (selected == null))
				return (int) (0);
			win = ctx.current;
			style = ctx.style;
			s = (int) (nk_widget(&header, ctx));
			if ((s) == (NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.input;
			if ((nk_button_behavior(ref ctx.last_widget_state, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				is_clicked = (int) (nk_true);
			if ((ctx.last_widget_state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				text.text = (nk_color) (style.combo.label_active);
			}
			else if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				text.text = (nk_color) (style.combo.label_hover);
			}
			else
			{
				background = style.combo.normal;
				text.text = (nk_color) (style.combo.label_normal);
			}

			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				text.background = (nk_color) (background.data.color);
				nk_fill_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(nk_color) (style.combo.border_color));
			}

			{
				nk_rect label = new nk_rect();
				nk_rect button = new nk_rect();
				nk_rect content = new nk_rect();
				int sym;
				if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
				label.x = (float) (header.x + style.combo.content_padding.x);
				label.y = (float) (header.y + style.combo.content_padding.y);
				label.w = (float) (button.x - (style.combo.content_padding.x + style.combo.spacing.x) - label.x);
				label.h = (float) (header.h - 2*style.combo.content_padding.y);
				nk_widget_text(win.buffer, (nk_rect) (label), selected, (int) (len), &text, (uint) (NK_TEXT_LEFT), ctx.style.font);
				nk_draw_button_symbol(win.buffer, &button, &content, (uint) (ctx.last_widget_state), ctx.style.combo.button,
					(int) (sym), style.font);
			}

			return (int) (nk_combo_begin(ctx, win, (nk_vec2) (size), (int) (is_clicked), (nk_rect) (header)));
		}

		public static int nk_combo_begin_label(nk_context ctx, char* selected, nk_vec2 size)
		{
			return (int) (nk_combo_begin_text(ctx, selected, (int) (nk_strlen(selected)), (nk_vec2) (size)));
		}

		public static int nk_combo_begin_color(nk_context ctx, nk_color color, nk_vec2 size)
		{
			nk_window win;
			nk_style style;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			int s;
			nk_style_item background;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			s = (int) (nk_widget(&header, ctx));
			if ((s) == (NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.input;
			if ((nk_button_behavior(ref ctx.last_widget_state, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				is_clicked = (int) (nk_true);
			if ((ctx.last_widget_state & NK_WIDGET_STATE_ACTIVED) != 0) background = style.combo.active;
			else if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) background = style.combo.hover;
			else background = style.combo.normal;
			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				nk_fill_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(nk_color) (style.combo.border_color));
			}

			{
				nk_rect content = new nk_rect();
				nk_rect button = new nk_rect();
				nk_rect bounds = new nk_rect();
				int sym;
				if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				bounds.h = (float) (header.h - 4*style.combo.content_padding.y);
				bounds.y = (float) (header.y + 2*style.combo.content_padding.y);
				bounds.x = (float) (header.x + 2*style.combo.content_padding.x);
				bounds.w = (float) ((button.x - (style.combo.content_padding.x + style.combo.spacing.x)) - bounds.x);
				nk_fill_rect(win.buffer, (nk_rect) (bounds), (float) (0), (nk_color) (color));
				nk_draw_button_symbol(win.buffer, &button, &content, (uint) (ctx.last_widget_state), ctx.style.combo.button,
					(int) (sym), style.font);
			}

			return (int) (nk_combo_begin(ctx, win, (nk_vec2) (size), (int) (is_clicked), (nk_rect) (header)));
		}

		public static int nk_combo_begin_symbol(nk_context ctx, int symbol, nk_vec2 size)
		{
			nk_window win;
			nk_style style;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			int s;
			nk_style_item background;
			nk_color sym_background = new nk_color();
			nk_color symbol_color = new nk_color();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			s = (int) (nk_widget(&header, ctx));
			if ((s) == (NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.input;
			if ((nk_button_behavior(ref ctx.last_widget_state, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				is_clicked = (int) (nk_true);
			if ((ctx.last_widget_state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				symbol_color = (nk_color) (style.combo.symbol_active);
			}
			else if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				symbol_color = (nk_color) (style.combo.symbol_hover);
			}
			else
			{
				background = style.combo.normal;
				symbol_color = (nk_color) (style.combo.symbol_hover);
			}

			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				sym_background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				sym_background = (nk_color) (background.data.color);
				nk_fill_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(nk_color) (style.combo.border_color));
			}

			{
				nk_rect bounds = new nk_rect();
				nk_rect content = new nk_rect();
				nk_rect button = new nk_rect();
				int sym;
				if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.y);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				bounds.h = (float) (header.h - 2*style.combo.content_padding.y);
				bounds.y = (float) (header.y + style.combo.content_padding.y);
				bounds.x = (float) (header.x + style.combo.content_padding.x);
				bounds.w = (float) ((button.x - style.combo.content_padding.y) - bounds.x);
				nk_draw_symbol(win.buffer, (int) (symbol), (nk_rect) (bounds), (nk_color) (sym_background),
					(nk_color) (symbol_color), (float) (1.0f), style.font);
				nk_draw_button_symbol(win.buffer, &bounds, &content, (uint) (ctx.last_widget_state), ctx.style.combo.button,
					(int) (sym), style.font);
			}

			return (int) (nk_combo_begin(ctx, win, (nk_vec2) (size), (int) (is_clicked), (nk_rect) (header)));
		}

		public static int nk_combo_begin_symbol_text(nk_context ctx, char* selected, int len, int symbol, nk_vec2 size)
		{
			nk_window win;
			nk_style style;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			int s;
			nk_style_item background;
			nk_color symbol_color = new nk_color();
			nk_text text = new nk_text();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			s = (int) (nk_widget(&header, ctx));
			if (s == 0) return (int) (0);
			_in_ = (((win.layout.flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.input;
			if ((nk_button_behavior(ref ctx.last_widget_state, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				is_clicked = (int) (nk_true);
			if ((ctx.last_widget_state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				symbol_color = (nk_color) (style.combo.symbol_active);
				text.text = (nk_color) (style.combo.label_active);
			}
			else if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				symbol_color = (nk_color) (style.combo.symbol_hover);
				text.text = (nk_color) (style.combo.label_hover);
			}
			else
			{
				background = style.combo.normal;
				symbol_color = (nk_color) (style.combo.symbol_normal);
				text.text = (nk_color) (style.combo.label_normal);
			}

			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				text.background = (nk_color) (background.data.color);
				nk_fill_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(nk_color) (style.combo.border_color));
			}

			{
				nk_rect content = new nk_rect();
				nk_rect button = new nk_rect();
				nk_rect label = new nk_rect();
				nk_rect image = new nk_rect();
				int sym;
				if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				nk_draw_button_symbol(win.buffer, &button, &content, (uint) (ctx.last_widget_state), ctx.style.combo.button,
					(int) (sym), style.font);
				image.x = (float) (header.x + style.combo.content_padding.x);
				image.y = (float) (header.y + style.combo.content_padding.y);
				image.h = (float) (header.h - 2*style.combo.content_padding.y);
				image.w = (float) (image.h);
				nk_draw_symbol(win.buffer, (int) (symbol), (nk_rect) (image), (nk_color) (text.background),
					(nk_color) (symbol_color), (float) (1.0f), style.font);
				text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
				label.x = (float) (image.x + image.w + style.combo.spacing.x + style.combo.content_padding.x);
				label.y = (float) (header.y + style.combo.content_padding.y);
				label.w = (float) ((button.x - style.combo.content_padding.x) - label.x);
				label.h = (float) (header.h - 2*style.combo.content_padding.y);
				nk_widget_text(win.buffer, (nk_rect) (label), selected, (int) (len), &text, (uint) (NK_TEXT_LEFT), style.font);
			}

			return (int) (nk_combo_begin(ctx, win, (nk_vec2) (size), (int) (is_clicked), (nk_rect) (header)));
		}

		public static int nk_combo_begin_image(nk_context ctx, nk_image img, nk_vec2 size)
		{
			nk_window win;
			nk_style style;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			int s;
			nk_style_item background;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			s = (int) (nk_widget(&header, ctx));
			if ((s) == (NK_WIDGET_INVALID)) return (int) (0);
			_in_ = (((win.layout.flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.input;
			if ((nk_button_behavior(ref ctx.last_widget_state, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				is_clicked = (int) (nk_true);
			if ((ctx.last_widget_state & NK_WIDGET_STATE_ACTIVED) != 0) background = style.combo.active;
			else if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) background = style.combo.hover;
			else background = style.combo.normal;
			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				nk_fill_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(nk_color) (style.combo.border_color));
			}

			{
				nk_rect bounds = new nk_rect();
				nk_rect content = new nk_rect();
				nk_rect button = new nk_rect();
				int sym;
				if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.y);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				bounds.h = (float) (header.h - 2*style.combo.content_padding.y);
				bounds.y = (float) (header.y + style.combo.content_padding.y);
				bounds.x = (float) (header.x + style.combo.content_padding.x);
				bounds.w = (float) ((button.x - style.combo.content_padding.y) - bounds.x);
				nk_draw_image(win.buffer, (nk_rect) (bounds), img, (nk_color) (nk_white));
				nk_draw_button_symbol(win.buffer, &bounds, &content, (uint) (ctx.last_widget_state), ctx.style.combo.button,
					(int) (sym), style.font);
			}

			return (int) (nk_combo_begin(ctx, win, (nk_vec2) (size), (int) (is_clicked), (nk_rect) (header)));
		}

		public static int nk_combo_begin_image_text(nk_context ctx, char* selected, int len, nk_image img, nk_vec2 size)
		{
			nk_window win;
			nk_style style;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			int s;
			nk_style_item background;
			nk_text text = new nk_text();
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			style = ctx.style;
			s = (int) (nk_widget(&header, ctx));
			if (s == 0) return (int) (0);
			_in_ = (((win.layout.flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.input;
			if ((nk_button_behavior(ref ctx.last_widget_state, (nk_rect) (header), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
				is_clicked = (int) (nk_true);
			if ((ctx.last_widget_state & NK_WIDGET_STATE_ACTIVED) != 0)
			{
				background = style.combo.active;
				text.text = (nk_color) (style.combo.label_active);
			}
			else if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0)
			{
				background = style.combo.hover;
				text.text = (nk_color) (style.combo.label_hover);
			}
			else
			{
				background = style.combo.normal;
				text.text = (nk_color) (style.combo.label_normal);
			}

			if ((background.type) == (NK_STYLE_ITEM_IMAGE))
			{
				text.background = (nk_color) (nk_rgba((int) (0), (int) (0), (int) (0), (int) (0)));
				nk_draw_image(win.buffer, (nk_rect) (header), background.data.image, (nk_color) (nk_white));
			}
			else
			{
				text.background = (nk_color) (background.data.color);
				nk_fill_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (nk_color) (background.data.color));
				nk_stroke_rect(win.buffer, (nk_rect) (header), (float) (style.combo.rounding), (float) (style.combo.border),
					(nk_color) (style.combo.border_color));
			}

			{
				nk_rect content = new nk_rect();
				nk_rect button = new nk_rect();
				nk_rect label = new nk_rect();
				nk_rect image = new nk_rect();
				int sym;
				if ((ctx.last_widget_state & NK_WIDGET_STATE_HOVER) != 0) sym = (int) (style.combo.sym_hover);
				else if ((is_clicked) != 0) sym = (int) (style.combo.sym_active);
				else sym = (int) (style.combo.sym_normal);
				button.w = (float) (header.h - 2*style.combo.button_padding.y);
				button.x = (float) ((header.x + header.w - header.h) - style.combo.button_padding.x);
				button.y = (float) (header.y + style.combo.button_padding.y);
				button.h = (float) (button.w);
				content.x = (float) (button.x + style.combo.button.padding.x);
				content.y = (float) (button.y + style.combo.button.padding.y);
				content.w = (float) (button.w - 2*style.combo.button.padding.x);
				content.h = (float) (button.h - 2*style.combo.button.padding.y);
				nk_draw_button_symbol(win.buffer, &button, &content, (uint) (ctx.last_widget_state), ctx.style.combo.button,
					(int) (sym), style.font);
				image.x = (float) (header.x + style.combo.content_padding.x);
				image.y = (float) (header.y + style.combo.content_padding.y);
				image.h = (float) (header.h - 2*style.combo.content_padding.y);
				image.w = (float) (image.h);
				nk_draw_image(win.buffer, (nk_rect) (image), img, (nk_color) (nk_white));
				text.padding = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
				label.x = (float) (image.x + image.w + style.combo.spacing.x + style.combo.content_padding.x);
				label.y = (float) (header.y + style.combo.content_padding.y);
				label.w = (float) ((button.x - style.combo.content_padding.x) - label.x);
				label.h = (float) (header.h - 2*style.combo.content_padding.y);
				nk_widget_text(win.buffer, (nk_rect) (label), selected, (int) (len), &text, (uint) (NK_TEXT_LEFT), style.font);
			}

			return (int) (nk_combo_begin(ctx, win, (nk_vec2) (size), (int) (is_clicked), (nk_rect) (header)));
		}

		public static int nk_combo_begin_symbol_label(nk_context ctx, char* selected, int type, nk_vec2 size)
		{
			return (int) (nk_combo_begin_symbol_text(ctx, selected, (int) (nk_strlen(selected)), (int) (type), (nk_vec2) (size)));
		}

		public static int nk_combo_begin_image_label(nk_context ctx, char* selected, nk_image img, nk_vec2 size)
		{
			return
				(int) (nk_combo_begin_image_text(ctx, selected, (int) (nk_strlen(selected)), (nk_image) (img), (nk_vec2) (size)));
		}

		public static int nk_combo_item_text(nk_context ctx, char* text, int len, uint align)
		{
			return (int) (nk_contextual_item_text(ctx, text, (int) (len), (uint) (align)));
		}

		public static int nk_combo_item_label(nk_context ctx, char* label, uint align)
		{
			return (int) (nk_contextual_item_label(ctx, label, (uint) (align)));
		}

		public static int nk_combo_item_image_text(nk_context ctx, nk_image img, char* text, int len, uint alignment)
		{
			return (int) (nk_contextual_item_image_text(ctx, (nk_image) (img), text, (int) (len), (uint) (alignment)));
		}

		public static int nk_combo_item_image_label(nk_context ctx, nk_image img, char* text, uint alignment)
		{
			return (int) (nk_contextual_item_image_label(ctx, (nk_image) (img), text, (uint) (alignment)));
		}

		public static int nk_combo_item_symbol_text(nk_context ctx, int sym, char* text, int len, uint alignment)
		{
			return (int) (nk_contextual_item_symbol_text(ctx, (int) (sym), text, (int) (len), (uint) (alignment)));
		}

		public static int nk_combo_item_symbol_label(nk_context ctx, int sym, char* label, uint alignment)
		{
			return (int) (nk_contextual_item_symbol_label(ctx, (int) (sym), label, (uint) (alignment)));
		}

		public static void nk_combo_end(nk_context ctx)
		{
			nk_contextual_end(ctx);
		}

		public static void nk_combo_close(nk_context ctx)
		{
			nk_contextual_close(ctx);
		}

		public static int nk_combo(nk_context ctx, char** items, int count, int selected, int item_height, nk_vec2 size)
		{
			int i = (int) (0);
			int max_height;
			nk_vec2 item_spacing = new nk_vec2();
			nk_vec2 window_padding = new nk_vec2();
			if (((ctx == null) || (items == null)) || (count == 0)) return (int) (selected);
			item_spacing = (nk_vec2) (ctx.style.window.spacing);
			window_padding = (nk_vec2) (nk_panel_get_padding(ctx.style, (int) (ctx.current.layout.type)));
			max_height = (int) (count*item_height + count*(int) (item_spacing.y));
			max_height += (int) ((int) (item_spacing.y)*2 + (int) (window_padding.y)*2);
			size.y = (float) ((size.y) < ((float) (max_height)) ? (size.y) : ((float) (max_height)));
			if ((nk_combo_begin_label(ctx, items[selected], (nk_vec2) (size))) != 0)
			{
				nk_layout_row_dynamic(ctx, (float) (item_height), (int) (1));
				for (i = (int) (0); (i) < (count); ++i)
				{
					if ((nk_combo_item_label(ctx, items[i], (uint) (NK_TEXT_LEFT))) != 0) selected = (int) (i);
				}
				nk_combo_end(ctx);
			}

			return (int) (selected);
		}

		public static int nk_combo_separator(nk_context ctx, char* items_separated_by_separator, int separator, int selected,
			int count, int item_height, nk_vec2 size)
		{
			int i;
			int max_height;
			nk_vec2 item_spacing = new nk_vec2();
			nk_vec2 window_padding = new nk_vec2();
			char* current_item;
			char* iter;
			;
			int length = (int) (0);
			if ((ctx == null) || (items_separated_by_separator == null)) return (int) (selected);
			item_spacing = (nk_vec2) (ctx.style.window.spacing);
			window_padding = (nk_vec2) (nk_panel_get_padding(ctx.style, (int) (ctx.current.layout.type)));
			max_height = (int) (count*item_height + count*(int) (item_spacing.y));
			max_height += (int) ((int) (item_spacing.y)*2 + (int) (window_padding.y)*2);
			size.y = (float) ((size.y) < ((float) (max_height)) ? (size.y) : ((float) (max_height)));
			current_item = items_separated_by_separator;
			for (i = (int) (0); (i) < (count); ++i)
			{
				iter = current_item;
				while (((*iter) != 0) && (*iter != separator))
				{
					iter++;
				}
				length = ((int) (iter - current_item));
				if ((i) == (selected)) break;
				current_item = iter + 1;
			}
			if ((nk_combo_begin_text(ctx, current_item, (int) (length), (nk_vec2) (size))) != 0)
			{
				current_item = items_separated_by_separator;
				nk_layout_row_dynamic(ctx, (float) (item_height), (int) (1));
				for (i = (int) (0); (i) < (count); ++i)
				{
					iter = current_item;
					while (((*iter) != 0) && (*iter != separator))
					{
						iter++;
					}
					length = ((int) (iter - current_item));
					if ((nk_combo_item_text(ctx, current_item, (int) (length), (uint) (NK_TEXT_LEFT))) != 0) selected = (int) (i);
					current_item = current_item + length + 1;
				}
				nk_combo_end(ctx);
			}

			return (int) (selected);
		}

		public static int nk_combo_string(nk_context ctx, char* items_separated_by_zeros, int selected, int count,
			int item_height, nk_vec2 size)
		{
			return
				(int)
					(nk_combo_separator(ctx, items_separated_by_zeros, (int) ('\0'), (int) (selected), (int) (count),
						(int) (item_height), (nk_vec2) (size)));
		}

		public static int nk_combo_callback(nk_context ctx, NkComboCallback item_getter, void* userdata, int selected,
			int count, int item_height, nk_vec2 size)
		{
			int i;
			int max_height;
			nk_vec2 item_spacing = new nk_vec2();
			nk_vec2 window_padding = new nk_vec2();
			char* item;
			if ((ctx == null) || (item_getter == null)) return (int) (selected);
			item_spacing = (nk_vec2) (ctx.style.window.spacing);
			window_padding = (nk_vec2) (nk_panel_get_padding(ctx.style, (int) (ctx.current.layout.type)));
			max_height = (int) (count*item_height + count*(int) (item_spacing.y));
			max_height += (int) ((int) (item_spacing.y)*2 + (int) (window_padding.y)*2);
			size.y = (float) ((size.y) < ((float) (max_height)) ? (size.y) : ((float) (max_height)));
			item_getter(userdata, (int) (selected), &item);
			if ((nk_combo_begin_label(ctx, item, (nk_vec2) (size))) != 0)
			{
				nk_layout_row_dynamic(ctx, (float) (item_height), (int) (1));
				for (i = (int) (0); (i) < (count); ++i)
				{
					item_getter(userdata, (int) (i), &item);
					if ((nk_combo_item_label(ctx, item, (uint) (NK_TEXT_LEFT))) != 0) selected = (int) (i);
				}
				nk_combo_end(ctx);
			}

			return (int) (selected);
		}

		public static void nk_combobox(nk_context ctx, char** items, int count, int* selected, int item_height, nk_vec2 size)
		{
			*selected = (int) (nk_combo(ctx, items, (int) (count), (int) (*selected), (int) (item_height), (nk_vec2) (size)));
		}

		public static void nk_combobox_string(nk_context ctx, char* items_separated_by_zeros, int* selected, int count,
			int item_height, nk_vec2 size)
		{
			*selected =
				(int)
					(nk_combo_string(ctx, items_separated_by_zeros, (int) (*selected), (int) (count), (int) (item_height),
						(nk_vec2) (size)));
		}

		public static void nk_combobox_separator(nk_context ctx, char* items_separated_by_separator, int separator,
			int* selected, int count, int item_height, nk_vec2 size)
		{
			*selected =
				(int)
					(nk_combo_separator(ctx, items_separated_by_separator, (int) (separator), (int) (*selected), (int) (count),
						(int) (item_height), (nk_vec2) (size)));
		}

		public static void nk_combobox_callback(nk_context ctx, NkComboCallback item_getter, void* userdata, int* selected,
			int count, int item_height, nk_vec2 size)
		{
			*selected =
				(int)
					(nk_combo_callback(ctx, item_getter, userdata, (int) (*selected), (int) (count), (int) (item_height),
						(nk_vec2) (size)));
		}

		public static int nk_menu_begin(nk_context ctx, nk_window win, char* id, int is_clicked, nk_rect header, nk_vec2 size)
		{
			int is_open = (int) (0);
			int is_active = (int) (0);
			nk_rect body = new nk_rect();
			nk_window popup;
			uint hash = (uint) (nk_murmur_hash(id, (int) (nk_strlen(id)), (uint) (NK_PANEL_MENU)));
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			body.x = (float) (header.x);
			body.w = (float) (size.x);
			body.y = (float) (header.y + header.h);
			body.h = (float) (size.y);
			popup = win.popup.win;
			is_open = (int) (popup != null ? nk_true : nk_false);
			is_active =
				(int) ((((popup) != null) && ((win.popup.name) == (hash))) && ((win.popup.type) == (NK_PANEL_MENU)) ? 1 : 0);
			if ((((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) || (((is_open) != 0) && (is_active == 0))) ||
			    (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int) (0);
			if (
				nk_nonblock_begin(ctx, (uint) (NK_WINDOW_NO_SCROLLBAR), (nk_rect) (body), (nk_rect) (header), (int) (NK_PANEL_MENU)) ==
				0) return (int) (0);
			win.popup.type = (int) (NK_PANEL_MENU);
			win.popup.name = (uint) (hash);
			return (int) (1);
		}

		public static int nk_menu_begin_text(nk_context ctx, char* title, int len, uint align, nk_vec2 size)
		{
			nk_window win;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			uint state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			state = (uint) (nk_widget(&header, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_text(ref ctx.last_widget_state, win.buffer, (nk_rect) (header), title, (int) (len), (uint) (align),
					(int) (NK_BUTTON_DEFAULT), ctx.style.menu_button, _in_, ctx.style.font)) != 0) is_clicked = (int) (nk_true);
			return (int) (nk_menu_begin(ctx, win, title, (int) (is_clicked), (nk_rect) (header), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_label(nk_context ctx, char* text, uint align, nk_vec2 size)
		{
			return (int) (nk_menu_begin_text(ctx, text, (int) (nk_strlen(text)), (uint) (align), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_image(nk_context ctx, char* id, nk_image img, nk_vec2 size)
		{
			nk_window win;
			nk_rect header = new nk_rect();
			nk_input _in_;
			int is_clicked = (int) (nk_false);
			uint state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			state = (uint) (nk_widget(&header, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_image(ref ctx.last_widget_state, win.buffer, (nk_rect) (header), (nk_image) (img),
					(int) (NK_BUTTON_DEFAULT), ctx.style.menu_button, _in_)) != 0) is_clicked = (int) (nk_true);
			return (int) (nk_menu_begin(ctx, win, id, (int) (is_clicked), (nk_rect) (header), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_symbol(nk_context ctx, char* id, int sym, nk_vec2 size)
		{
			nk_window win;
			nk_input _in_;
			nk_rect header = new nk_rect();
			int is_clicked = (int) (nk_false);
			uint state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			state = (uint) (nk_widget(&header, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_symbol(ref ctx.last_widget_state, win.buffer, (nk_rect) (header), (int) (sym),
					(int) (NK_BUTTON_DEFAULT), ctx.style.menu_button, _in_, ctx.style.font)) != 0) is_clicked = (int) (nk_true);
			return (int) (nk_menu_begin(ctx, win, id, (int) (is_clicked), (nk_rect) (header), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_image_text(nk_context ctx, char* title, int len, uint align, nk_image img,
			nk_vec2 size)
		{
			nk_window win;
			nk_rect header = new nk_rect();
			nk_input _in_;
			int is_clicked = (int) (nk_false);
			uint state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			state = (uint) (nk_widget(&header, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_text_image(ref ctx.last_widget_state, win.buffer, (nk_rect) (header), (nk_image) (img), title,
					(int) (len), (uint) (align), (int) (NK_BUTTON_DEFAULT), ctx.style.menu_button, ctx.style.font, _in_)) != 0)
				is_clicked = (int) (nk_true);
			return (int) (nk_menu_begin(ctx, win, title, (int) (is_clicked), (nk_rect) (header), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_image_label(nk_context ctx, char* title, uint align, nk_image img, nk_vec2 size)
		{
			return
				(int)
					(nk_menu_begin_image_text(ctx, title, (int) (nk_strlen(title)), (uint) (align), (nk_image) (img), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_symbol_text(nk_context ctx, char* title, int len, uint align, int sym, nk_vec2 size)
		{
			nk_window win;
			nk_rect header = new nk_rect();
			nk_input _in_;
			int is_clicked = (int) (nk_false);
			uint state;
			if (((ctx == null) || (ctx.current == null)) || (ctx.current.layout == null)) return (int) (0);
			win = ctx.current;
			state = (uint) (nk_widget(&header, ctx));
			if (state == 0) return (int) (0);
			_in_ = (((state) == (NK_WIDGET_ROM)) || ((win.layout.flags & NK_WINDOW_ROM) != 0)) ? null : ctx.input;
			if (
				(nk_do_button_text_symbol(ref ctx.last_widget_state, win.buffer, (nk_rect) (header), (int) (sym), title, (int) (len),
					(uint) (align), (int) (NK_BUTTON_DEFAULT), ctx.style.menu_button, ctx.style.font, _in_)) != 0)
				is_clicked = (int) (nk_true);
			return (int) (nk_menu_begin(ctx, win, title, (int) (is_clicked), (nk_rect) (header), (nk_vec2) (size)));
		}

		public static int nk_menu_begin_symbol_label(nk_context ctx, char* title, uint align, int sym, nk_vec2 size)
		{
			return
				(int)
					(nk_menu_begin_symbol_text(ctx, title, (int) (nk_strlen(title)), (uint) (align), (int) (sym), (nk_vec2) (size)));
		}

		public static int nk_menu_item_text(nk_context ctx, char* title, int len, uint align)
		{
			return (int) (nk_contextual_item_text(ctx, title, (int) (len), (uint) (align)));
		}

		public static int nk_menu_item_label(nk_context ctx, char* label, uint align)
		{
			return (int) (nk_contextual_item_label(ctx, label, (uint) (align)));
		}

		public static int nk_menu_item_image_label(nk_context ctx, nk_image img, char* label, uint align)
		{
			return (int) (nk_contextual_item_image_label(ctx, (nk_image) (img), label, (uint) (align)));
		}

		public static int nk_menu_item_image_text(nk_context ctx, nk_image img, char* text, int len, uint align)
		{
			return (int) (nk_contextual_item_image_text(ctx, (nk_image) (img), text, (int) (len), (uint) (align)));
		}

		public static int nk_menu_item_symbol_text(nk_context ctx, int sym, char* text, int len, uint align)
		{
			return (int) (nk_contextual_item_symbol_text(ctx, (int) (sym), text, (int) (len), (uint) (align)));
		}

		public static int nk_menu_item_symbol_label(nk_context ctx, int sym, char* label, uint align)
		{
			return (int) (nk_contextual_item_symbol_label(ctx, (int) (sym), label, (uint) (align)));
		}

		public static void nk_menu_close(nk_context ctx)
		{
			nk_contextual_close(ctx);
		}

		public static void nk_menu_end(nk_context ctx)
		{
			nk_contextual_end(ctx);
		}
	}
}