using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
    public unsafe partial class Nk
    {
        public static uint nk_convert(NkContext ctx, NkBuffer<nk_draw_command> cmds, NkBuffer<byte> vertices,
            NkBuffer<ushort> elements, NkConvertConfig config)
        {
            uint res = (uint)(NK_CONVERT_SUCCESS);

            if ((((((ctx == null) || (cmds == null)) || (vertices == null)) || (elements == null)) || (config == null)) ||
                (config.VertexLayout == null)) return (uint)(NK_CONVERT_INVALID_PARAM);
            nk_draw_list_setup(ctx.DrawList, config, cmds, vertices, elements, (int)(config.LineAa), (int)(config.ShapeAa));
            var top_window = nk__begin(ctx);

            int cnt = 0;
            for (var cmd = top_window.Buffer.First; cmd != null; cmd = cmd.Next)
            {
                switch (cmd.Header.type)
                {
                    case NK_COMMAND_NOP:
                        break;
                    case NK_COMMAND_SCISSOR:
                        {
                            NkCommandScissor s = (NkCommandScissor)(cmd);
                            nk_draw_list_add_clip(ctx.DrawList,
                                (nk_rect)(nk_rect_((float)(s.X), (float)(s.Y), (float)(s.W), (float)(s.H))));
                        }
                        break;
                    case NK_COMMAND_LINE:
                        {
                            NkCommandLine l = (NkCommandLine)(cmd);
                            nk_draw_list_stroke_line(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(l.Begin.x), (float)(l.Begin.y))),
                                (nk_vec2)(nk_vec2_((float)(l.End.x), (float)(l.End.y))), (nk_color)(l.Color), (float)(l.LineThickness));
                        }
                        break;
                    case NK_COMMAND_CURVE:
                        {
                            NkCommandCurve q = (NkCommandCurve)(cmd);
                            nk_draw_list_stroke_curve(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(q.Begin.x), (float)(q.Begin.y))),
                                (nk_vec2)(nk_vec2_((float)(q.Ctrl0.x), (float)(q.Ctrl0.y))),
                                (nk_vec2)(nk_vec2_((float)(q.Ctrl1.x), (float)(q.Ctrl1.y))),
                                (nk_vec2)(nk_vec2_((float)(q.End.x), (float)(q.End.y))), (nk_color)(q.Color),
                                (uint)(config.CurveSegmentCount), (float)(q.LineThickness));
                        }
                        break;
                    case NK_COMMAND_RECT:
                        {
                            NkCommandRect r = (NkCommandRect)(cmd);
                            nk_draw_list_stroke_rect(ctx.DrawList,
                                (nk_rect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (nk_color)(r.Color),
                                (float)(r.Rounding), (float)(r.LineThickness));
                        }
                        break;
                    case NK_COMMAND_RECT_FILLED:
                        {
                            NkCommandRectFilled r = (NkCommandRectFilled)(cmd);
                            nk_draw_list_fill_rect(ctx.DrawList,
                                (nk_rect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (nk_color)(r.Color),
                                (float)(r.Rounding));
                        }
                        break;
                    case NK_COMMAND_RECT_MULTI_COLOR:
                        {
                            NkCommandRectMultiColor r = (NkCommandRectMultiColor)(cmd);
                            nk_draw_list_fill_rect_multi_color(ctx.DrawList,
                                (nk_rect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (nk_color)(r.Left),
                                (nk_color)(r.Top), (nk_color)(r.Right), (nk_color)(r.Bottom));
                        }
                        break;
                    case NK_COMMAND_CIRCLE:
                        {
                            NkCommandCircle c = (NkCommandCircle)(cmd);
                            nk_draw_list_stroke_circle(ctx.DrawList,
                                (nk_vec2)(nk_vec2_((float)((float)(c.X) + (float)(c.W) / 2), (float)((float)(c.Y) + (float)(c.H) / 2))),
                                (float)((float)(c.W) / 2), (nk_color)(c.Color), (uint)(config.CircleSegmentCount), (float)(c.LineThickness));
                        }
                        break;
                    case NK_COMMAND_CIRCLE_FILLED:
                        {
                            NkCommandCircleFilled c = (NkCommandCircleFilled)(cmd);
                            nk_draw_list_fill_circle(ctx.DrawList,
                                (nk_vec2)(nk_vec2_((float)((float)(c.X) + (float)(c.W) / 2), (float)((float)(c.Y) + (float)(c.H) / 2))),
                                (float)((float)(c.W) / 2), (nk_color)(c.Color), (uint)(config.CircleSegmentCount));
                        }
                        break;
                    case NK_COMMAND_ARC:
                        {
                            NkCommandArc c = (NkCommandArc)(cmd);
                            nk_draw_list_path_line_to(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))));
                            nk_draw_list_path_arc_to(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))), (float)(c.R),
                                (float)(c.A[0]), (float)(c.A[1]), (uint)(config.ArcSegmentCount));
                            nk_draw_list_path_stroke(ctx.DrawList, (nk_color)(c.Color), (int)(NK_STROKE_CLOSED), (float)(c.LineThickness));
                        }
                        break;
                    case NK_COMMAND_ARC_FILLED:
                        {
                            NkCommandArcFilled c = (NkCommandArcFilled)(cmd);
                            nk_draw_list_path_line_to(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))));
                            nk_draw_list_path_arc_to(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))), (float)(c.R),
                                (float)(c.A[0]), (float)(c.A[1]), (uint)(config.ArcSegmentCount));
                            nk_draw_list_path_fill(ctx.DrawList, (nk_color)(c.Color));
                        }
                        break;
                    case NK_COMMAND_TRIANGLE:
                        {
                            NkCommandTriangle t = (NkCommandTriangle)(cmd);
                            nk_draw_list_stroke_triangle(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(t.A.x), (float)(t.A.y))),
                                (nk_vec2)(nk_vec2_((float)(t.B.x), (float)(t.B.y))), (nk_vec2)(nk_vec2_((float)(t.C.x), (float)(t.C.y))),
                                (nk_color)(t.Color), (float)(t.LineThickness));
                        }
                        break;
                    case NK_COMMAND_TRIANGLE_FILLED:
                        {
                            NkCommandTriangleFilled t = (NkCommandTriangleFilled)(cmd);
                            nk_draw_list_fill_triangle(ctx.DrawList, (nk_vec2)(nk_vec2_((float)(t.A.x), (float)(t.A.y))),
                                (nk_vec2)(nk_vec2_((float)(t.B.x), (float)(t.B.y))), (nk_vec2)(nk_vec2_((float)(t.C.x), (float)(t.C.y))),
                                (nk_color)(t.Color));
                        }
                        break;
                    case NK_COMMAND_POLYGON:
                        {
                            int i;
                            NkCommandPolygon p = (NkCommandPolygon)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                nk_vec2 pnt = (nk_vec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (nk_vec2)(pnt));
                            }
                            nk_draw_list_path_stroke(ctx.DrawList, (nk_color)(p.Color), (int)(NK_STROKE_CLOSED), (float)(p.LineThickness));
                        }
                        break;
                    case NK_COMMAND_POLYGON_FILLED:
                        {
                            int i;
                            NkCommandPolygonFilled p = (NkCommandPolygonFilled)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                nk_vec2 pnt = (nk_vec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (nk_vec2)(pnt));
                            }
                            nk_draw_list_path_fill(ctx.DrawList, (nk_color)(p.Color));
                        }
                        break;
                    case NK_COMMAND_POLYLINE:
                        {
                            int i;
                            NkCommandPolyline p = (NkCommandPolyline)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                nk_vec2 pnt = (nk_vec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (nk_vec2)(pnt));
                            }
                            nk_draw_list_path_stroke(ctx.DrawList, (nk_color)(p.Color), (int)(NK_STROKE_OPEN), (float)(p.LineThickness));
                        }
                        break;
                    case NK_COMMAND_TEXT:
                        {
                            NkCommandText t = (NkCommandText)(cmd);
                            nk_draw_list_add_text(ctx.DrawList, t.Font,
                                (nk_rect)(nk_rect_((float)(t.X), (float)(t.Y), (float)(t.W), (float)(t.H))), t.String, (int)(t.Length),
                                (float)(t.Height), (nk_color)(t.Foreground));
                        }
                        break;
                    case NK_COMMAND_IMAGE:
                        {
                            NkCommandImage i = (NkCommandImage)(cmd);
                            nk_draw_list_add_image(ctx.DrawList, (nk_image)(i.Img),
                                (nk_rect)(nk_rect_((float)(i.X), (float)(i.Y), (float)(i.W), (float)(i.H))), (nk_color)(i.Col));
                        }
                        break;
                    case NK_COMMAND_CUSTOM:
                        {
                            NkCommandCustom c = (NkCommandCustom)(cmd);
                            c.Callback(ctx.DrawList, (short)(c.X), (short)(c.Y), (ushort)(c.W), (ushort)(c.H),
                                (NkHandle)(c.CallbackData));
                        }
                        break;
                    default:
                        break;
                }
                ++cnt;
            }

            return res;
        }

        public static void nk_input_begin(NkContext ctx)
        {
            int i;
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            for (i = (int)(0); (i) < (NK_BUTTON_MAX); ++i)
            {
                ((nk_mouse_button*)_in_.mouse.Buttons + i)->clicked = (uint)(0);
            }
            _in_.keyboard.TextLen = (int)(0);
            _in_.mouse.ScrollDelta = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            _in_.mouse.Prev.x = (float)(_in_.mouse.Pos.x);
            _in_.mouse.Prev.y = (float)(_in_.mouse.Pos.y);
            _in_.mouse.Delta.x = (float)(0);
            _in_.mouse.Delta.y = (float)(0);
            for (i = (int)(0); (i) < (NK_KEY_MAX); i++)
            {
                ((nk_key*)_in_.keyboard.Keys + i)->clicked = (uint)(0);
            }
        }

        public static void nk_input_end(NkContext ctx)
        {
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            if ((_in_.mouse.Grab) != 0) _in_.mouse.Grab = (byte)(0);
            if ((_in_.mouse.Ungrab) != 0)
            {
                _in_.mouse.Grabbed = (byte)(0);
                _in_.mouse.Ungrab = (byte)(0);
                _in_.mouse.Grab = (byte)(0);
            }

        }

        public static void nk_input_motion(NkContext ctx, int x, int y)
        {
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            _in_.mouse.Pos.x = ((float)(x));
            _in_.mouse.Pos.y = ((float)(y));
            _in_.mouse.Delta.x = (float)(_in_.mouse.Pos.x - _in_.mouse.Prev.x);
            _in_.mouse.Delta.y = (float)(_in_.mouse.Pos.y - _in_.mouse.Prev.y);
        }

        public static void nk_input_key(NkContext ctx, int key, int down)
        {
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            if (((nk_key*)_in_.keyboard.Keys + key)->down != down) ((nk_key*)_in_.keyboard.Keys + key)->clicked++;
            ((nk_key*)_in_.keyboard.Keys + key)->down = (int)(down);
        }

        public static void nk_input_button(NkContext ctx, int id, int x, int y, int down)
        {
            nk_mouse_button* btn;
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            if ((_in_.mouse.Buttons[id].down) == (down)) return;
            btn = (nk_mouse_button*)_in_.mouse.Buttons + id;
            btn->clicked_pos.x = ((float)(x));
            btn->clicked_pos.y = ((float)(y));
            btn->down = (int)(down);
            btn->clicked++;
        }

        public static void nk_input_scroll(NkContext ctx, nk_vec2 val)
        {
            if (ctx == null) return;
            ctx.Input.mouse.ScrollDelta.x += (float)(val.x);
            ctx.Input.mouse.ScrollDelta.y += (float)(val.y);
        }

        public static void nk_input_glyph(NkContext ctx, char* glyph)
        {
            int len = (int)(0);
            char unicode;
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            len = (int)(nk_utf_decode(glyph, &unicode, (int)(4)));
            if (((len) != 0) && ((_in_.keyboard.TextLen + len) < (16)))
            {
                nk_utf_encode(unicode, (char*)_in_.keyboard.Text + _in_.keyboard.TextLen, (int)(16 - _in_.keyboard.TextLen));
                _in_.keyboard.TextLen += (int)(len);
            }

        }

        public static void nk_input_char(NkContext ctx, char c)
        {
            char* glyph = stackalloc char[4];
            if (ctx == null) return;
            glyph[0] = c;
            nk_input_glyph(ctx, glyph);
        }

        public static void nk_input_unicode(NkContext ctx, char unicode)
        {
            char* rune = stackalloc char[4];
            if (ctx == null) return;
            nk_utf_encode(unicode, rune, (int)(4));
            nk_input_glyph(ctx, rune);
        }

        public static void nk_style_default(NkContext ctx)
        {
            nk_style_from_table(ctx, null);
        }

        public static void nk_style_from_table(NkContext ctx, nk_color[] table)
        {
            NkStyle style;
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
            style = ctx.Style;
            table = (table == null) ? nk_default_color_style : table;
            text = style.Text;
            text.color = (nk_color)(table[NK_COLOR_TEXT]);
            text.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            button = style.Button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_BUTTON])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_BUTTON_HOVER])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_BUTTON_ACTIVE])));
            button.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            button.text_background = (nk_color)(table[NK_COLOR_BUTTON]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.image_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(1.0f);
            button.rounding = (float)(4.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.ContextualButton;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_BUTTON_HOVER])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_BUTTON_ACTIVE])));
            button.border_color = (nk_color)(table[NK_COLOR_WINDOW]);
            button.text_background = (nk_color)(table[NK_COLOR_WINDOW]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.MenuButton;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.border_color = (nk_color)(table[NK_COLOR_WINDOW]);
            button.text_background = (nk_color)(table[NK_COLOR_WINDOW]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(1.0f);
            button.draw_begin = null;
            button.draw_end = null;
            toggle = style.Checkbox;

            toggle.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE])));
            toggle.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.cursor_normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.cursor_hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.userdata = (NkHandle)(nk_handle_ptr(null));
            toggle.text_background = (nk_color)(table[NK_COLOR_WINDOW]);
            toggle.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            toggle.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            toggle.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            toggle.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            toggle.touch_padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            toggle.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            toggle.border = (float)(0.0f);
            toggle.spacing = (float)(4);
            toggle = style.Option;

            toggle.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE])));
            toggle.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.cursor_normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.cursor_hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.userdata = (NkHandle)(nk_handle_ptr(null));
            toggle.text_background = (nk_color)(table[NK_COLOR_WINDOW]);
            toggle.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            toggle.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            toggle.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            toggle.padding = (nk_vec2)(nk_vec2_((float)(3.0f), (float)(3.0f)));
            toggle.touch_padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            toggle.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            toggle.border = (float)(0.0f);
            toggle.spacing = (float)(4);
            select = style.Selectable;

            select.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SELECT])));
            select.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SELECT])));
            select.pressed = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SELECT])));
            select.normal_active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SELECT_ACTIVE])));
            select.hover_active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SELECT_ACTIVE])));
            select.pressed_active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SELECT_ACTIVE])));
            select.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            select.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            select.text_pressed = (nk_color)(table[NK_COLOR_TEXT]);
            select.text_normal_active = (nk_color)(table[NK_COLOR_TEXT]);
            select.text_hover_active = (nk_color)(table[NK_COLOR_TEXT]);
            select.text_pressed_active = (nk_color)(table[NK_COLOR_TEXT]);
            select.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            select.touch_padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            select.userdata = (NkHandle)(nk_handle_ptr(null));
            select.rounding = (float)(0.0f);
            select.draw_begin = null;
            select.draw_end = null;
            slider = style.Slider;

            slider.normal = (NkStyleItem)(nk_style_item_hide());
            slider.hover = (NkStyleItem)(nk_style_item_hide());
            slider.active = (NkStyleItem)(nk_style_item_hide());
            slider.bar_normal = (nk_color)(table[NK_COLOR_SLIDER]);
            slider.bar_hover = (nk_color)(table[NK_COLOR_SLIDER]);
            slider.bar_active = (nk_color)(table[NK_COLOR_SLIDER]);
            slider.bar_filled = (nk_color)(table[NK_COLOR_SLIDER_CURSOR]);
            slider.cursor_normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER_CURSOR])));
            slider.cursor_hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER_CURSOR_HOVER])));
            slider.cursor_active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER_CURSOR_ACTIVE])));
            slider.inc_symbol = (int)(NK_SYMBOL_TRIANGLE_RIGHT);
            slider.dec_symbol = (int)(NK_SYMBOL_TRIANGLE_LEFT);
            slider.cursor_size = (nk_vec2)(nk_vec2_((float)(16), (float)(16)));
            slider.padding = (nk_vec2)(nk_vec2_((float)(2), (float)(2)));
            slider.spacing = (nk_vec2)(nk_vec2_((float)(2), (float)(2)));
            slider.userdata = (NkHandle)(nk_handle_ptr(null));
            slider.show_buttons = (int)(nk_false);
            slider.bar_height = (float)(8);
            slider.rounding = (float)(0);
            slider.draw_begin = null;
            slider.draw_end = null;
            button = style.Slider.inc_button;
            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(nk_rgb((int)(40), (int)(40), (int)(40)))));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(nk_rgb((int)(42), (int)(42), (int)(42)))));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(nk_rgb((int)(44), (int)(44), (int)(44)))));
            button.border_color = (nk_color)(nk_rgb((int)(65), (int)(65), (int)(65)));
            button.text_background = (nk_color)(nk_rgb((int)(40), (int)(40), (int)(40)));
            button.text_normal = (nk_color)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_hover = (nk_color)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_active = (nk_color)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.padding = (nk_vec2)(nk_vec2_((float)(8.0f), (float)(8.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(1.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Slider.dec_button = (nk_style_button)(style.Slider.inc_button);
            prog = style.Progress;

            prog.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER])));
            prog.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER])));
            prog.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER])));
            prog.cursor_normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER_CURSOR])));
            prog.cursor_hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER_CURSOR_HOVER])));
            prog.cursor_active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SLIDER_CURSOR_ACTIVE])));
            prog.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            prog.cursor_border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            prog.userdata = (NkHandle)(nk_handle_ptr(null));
            prog.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            prog.rounding = (float)(0);
            prog.border = (float)(0);
            prog.cursor_rounding = (float)(0);
            prog.cursor_border = (float)(0);
            prog.draw_begin = null;
            prog.draw_end = null;
            scroll = style.Scrollh;

            scroll.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SCROLLBAR])));
            scroll.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SCROLLBAR])));
            scroll.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SCROLLBAR])));
            scroll.cursor_normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SCROLLBAR_CURSOR])));
            scroll.cursor_hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SCROLLBAR_CURSOR_HOVER])));
            scroll.cursor_active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_SCROLLBAR_CURSOR_ACTIVE])));
            scroll.dec_symbol = (int)(NK_SYMBOL_CIRCLE_SOLID);
            scroll.inc_symbol = (int)(NK_SYMBOL_CIRCLE_SOLID);
            scroll.userdata = (NkHandle)(nk_handle_ptr(null));
            scroll.border_color = (nk_color)(table[NK_COLOR_SCROLLBAR]);
            scroll.cursor_border_color = (nk_color)(table[NK_COLOR_SCROLLBAR]);
            scroll.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            scroll.show_buttons = (int)(nk_false);
            scroll.border = (float)(0);
            scroll.rounding = (float)(0);
            scroll.border_cursor = (float)(0);
            scroll.rounding_cursor = (float)(0);
            scroll.draw_begin = null;
            scroll.draw_end = null;
            style.Scrollv = (nk_style_scrollbar)(style.Scrollh);
            button = style.Scrollh.inc_button;
            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(nk_rgb((int)(40), (int)(40), (int)(40)))));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(nk_rgb((int)(42), (int)(42), (int)(42)))));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(nk_rgb((int)(44), (int)(44), (int)(44)))));
            button.border_color = (nk_color)(nk_rgb((int)(65), (int)(65), (int)(65)));
            button.text_background = (nk_color)(nk_rgb((int)(40), (int)(40), (int)(40)));
            button.text_normal = (nk_color)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_hover = (nk_color)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_active = (nk_color)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.padding = (nk_vec2)(nk_vec2_((float)(4.0f), (float)(4.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(1.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Scrollh.dec_button = (nk_style_button)(style.Scrollh.inc_button);
            style.Scrollv.inc_button = (nk_style_button)(style.Scrollh.inc_button);
            style.Scrollv.dec_button = (nk_style_button)(style.Scrollh.inc_button);
            edit = style.Edit;

            edit.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_EDIT])));
            edit.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_EDIT])));
            edit.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_EDIT])));
            edit.cursor_normal = (nk_color)(table[NK_COLOR_TEXT]);
            edit.cursor_hover = (nk_color)(table[NK_COLOR_TEXT]);
            edit.cursor_text_normal = (nk_color)(table[NK_COLOR_EDIT]);
            edit.cursor_text_hover = (nk_color)(table[NK_COLOR_EDIT]);
            edit.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            edit.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            edit.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            edit.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            edit.selected_normal = (nk_color)(table[NK_COLOR_TEXT]);
            edit.selected_hover = (nk_color)(table[NK_COLOR_TEXT]);
            edit.selected_text_normal = (nk_color)(table[NK_COLOR_EDIT]);
            edit.selected_text_hover = (nk_color)(table[NK_COLOR_EDIT]);
            edit.scrollbar_size = (nk_vec2)(nk_vec2_((float)(10), (float)(10)));
            edit.scrollbar = (nk_style_scrollbar)(style.Scrollv);
            edit.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            edit.row_padding = (float)(2);
            edit.cursor_size = (float)(4);
            edit.border = (float)(1);
            edit.rounding = (float)(0);
            property = style.Property;

            property.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            property.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            property.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            property.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            property.label_normal = (nk_color)(table[NK_COLOR_TEXT]);
            property.label_hover = (nk_color)(table[NK_COLOR_TEXT]);
            property.label_active = (nk_color)(table[NK_COLOR_TEXT]);
            property.sym_left = (int)(NK_SYMBOL_TRIANGLE_LEFT);
            property.sym_right = (int)(NK_SYMBOL_TRIANGLE_RIGHT);
            property.userdata = (NkHandle)(nk_handle_ptr(null));
            property.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            property.border = (float)(1);
            property.rounding = (float)(10);
            property.draw_begin = null;
            property.draw_end = null;
            button = style.Property.dec_button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            button.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (nk_color)(table[NK_COLOR_PROPERTY]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Property.inc_button = (nk_style_button)(style.Property.dec_button);
            edit = style.Property.edit;

            edit.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            edit.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            edit.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_PROPERTY])));
            edit.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            edit.cursor_normal = (nk_color)(table[NK_COLOR_TEXT]);
            edit.cursor_hover = (nk_color)(table[NK_COLOR_TEXT]);
            edit.cursor_text_normal = (nk_color)(table[NK_COLOR_EDIT]);
            edit.cursor_text_hover = (nk_color)(table[NK_COLOR_EDIT]);
            edit.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            edit.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            edit.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            edit.selected_normal = (nk_color)(table[NK_COLOR_TEXT]);
            edit.selected_hover = (nk_color)(table[NK_COLOR_TEXT]);
            edit.selected_text_normal = (nk_color)(table[NK_COLOR_EDIT]);
            edit.selected_text_hover = (nk_color)(table[NK_COLOR_EDIT]);
            edit.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            edit.cursor_size = (float)(8);
            edit.border = (float)(0);
            edit.rounding = (float)(0);
            chart = style.Chart;

            chart.background = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_CHART])));
            chart.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            chart.selected_color = (nk_color)(table[NK_COLOR_CHART_COLOR_HIGHLIGHT]);
            chart.color = (nk_color)(table[NK_COLOR_CHART_COLOR]);
            chart.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            chart.border = (float)(0);
            chart.rounding = (float)(0);
            combo = style.Combo;
            combo.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_COMBO])));
            combo.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_COMBO])));
            combo.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_COMBO])));
            combo.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            combo.label_normal = (nk_color)(table[NK_COLOR_TEXT]);
            combo.label_hover = (nk_color)(table[NK_COLOR_TEXT]);
            combo.label_active = (nk_color)(table[NK_COLOR_TEXT]);
            combo.sym_normal = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            combo.sym_hover = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            combo.sym_active = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            combo.content_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            combo.button_padding = (nk_vec2)(nk_vec2_((float)(0), (float)(4)));
            combo.spacing = (nk_vec2)(nk_vec2_((float)(4), (float)(0)));
            combo.border = (float)(1);
            combo.rounding = (float)(0);
            button = style.Combo.button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_COMBO])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_COMBO])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_COMBO])));
            button.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (nk_color)(table[NK_COLOR_COMBO]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            tab = style.Tab;
            tab.background = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TAB_HEADER])));
            tab.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            tab.text = (nk_color)(table[NK_COLOR_TEXT]);
            tab.sym_minimize = (int)(NK_SYMBOL_TRIANGLE_RIGHT);
            tab.sym_maximize = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            tab.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            tab.spacing = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            tab.indent = (float)(10.0f);
            tab.border = (float)(1);
            tab.rounding = (float)(0);
            button = style.Tab.tab_minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TAB_HEADER])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TAB_HEADER])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TAB_HEADER])));
            button.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (nk_color)(table[NK_COLOR_TAB_HEADER]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Tab.tab_maximize_button = (nk_style_button)(button);
            button = style.Tab.node_minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            button.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (nk_color)(table[NK_COLOR_TAB_HEADER]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Tab.node_maximize_button = (nk_style_button)(button);
            win = style.Window;
            win.header.align = (int)(NK_HEADER_RIGHT);
            win.header.close_symbol = (int)(NK_SYMBOL_X);
            win.header.minimize_symbol = (int)(NK_SYMBOL_MINUS);
            win.header.maximize_symbol = (int)(NK_SYMBOL_PLUS);
            win.header.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            win.header.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            win.header.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            win.header.label_normal = (nk_color)(table[NK_COLOR_TEXT]);
            win.header.label_hover = (nk_color)(table[NK_COLOR_TEXT]);
            win.header.label_active = (nk_color)(table[NK_COLOR_TEXT]);
            win.header.label_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.header.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.header.spacing = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            button = style.Window.header.close_button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            button.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (nk_color)(table[NK_COLOR_HEADER]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.Window.header.minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_HEADER])));
            button.border_color = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (nk_color)(table[NK_COLOR_HEADER]);
            button.text_normal = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_hover = (nk_color)(table[NK_COLOR_TEXT]);
            button.text_active = (nk_color)(table[NK_COLOR_TEXT]);
            button.padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (nk_vec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            win.background = (nk_color)(table[NK_COLOR_WINDOW]);
            win.fixed_background = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_WINDOW])));
            win.border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.popup_border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.combo_border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.contextual_border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.menu_border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.group_border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.tooltip_border_color = (nk_color)(table[NK_COLOR_BORDER]);
            win.scaler = (NkStyleItem)(nk_style_item_color((nk_color)(table[NK_COLOR_TEXT])));
            win.rounding = (float)(0.0f);
            win.spacing = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.scrollbar_size = (nk_vec2)(nk_vec2_((float)(10), (float)(10)));
            win.min_size = (nk_vec2)(nk_vec2_((float)(64), (float)(64)));
            win.combo_border = (float)(1.0f);
            win.contextual_border = (float)(1.0f);
            win.menu_border = (float)(1.0f);
            win.group_border = (float)(1.0f);
            win.tooltip_border = (float)(1.0f);
            win.popup_border = (float)(1.0f);
            win.border = (float)(2.0f);
            win.min_row_height_padding = (float)(8);
            win.padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.group_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.popup_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.combo_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.contextual_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.menu_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
            win.tooltip_padding = (nk_vec2)(nk_vec2_((float)(4), (float)(4)));
        }

        public static void nk_style_set_font(NkContext ctx, NkUserFont font)
        {
            NkStyle style;
            if (ctx == null) return;
            style = ctx.Style;
            style.Font = font;
            ctx.Stacks.fonts.head = (int)(0);
            if ((ctx.Current) != null) nk_layout_reset_min_row_height(ctx);
        }

        public static int nk_style_push_font(NkContext ctx, NkUserFont font)
        {
            nk_config_stack_user_font font_stack;
            nk_config_stack_user_font_element element;
            if (ctx == null) return (int)(0);
            font_stack = ctx.Stacks.fonts;
            if ((font_stack.head) >= (int)font_stack.elements.Length) return (int)(0);
            element = font_stack.elements[font_stack.head++];
            element.address = ctx.Style.Font;
            element.old_value = ctx.Style.Font;
            ctx.Style.Font = font;
            return (int)(1);
        }

        public static int nk_style_pop_font(NkContext ctx)
        {
            nk_config_stack_user_font font_stack;
            nk_config_stack_user_font_element element;
            if (ctx == null) return (int)(0);
            font_stack = ctx.Stacks.fonts;
            if ((font_stack.head) < (1)) return (int)(0);
            element = font_stack.elements[--font_stack.head];
            element.address = element.old_value;
            return (int)(1);
        }

        public static int nk_style_push_style_item(NkContext ctx, NkStyleItem address, NkStyleItem value)
        {
            nk_config_stack_style_item type_stack;
            nk_config_stack_style_item_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.style_items;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (NkStyleItem)(address);
            address = (NkStyleItem)(value);
            return (int)(1);
        }

        public static int nk_style_push_float(NkContext ctx, float* address, float value)
        {
            nk_config_stack_float type_stack;
            nk_config_stack_float_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.floats;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (float)(*address);
            *address = (float)(value);
            return (int)(1);
        }

        public static int nk_style_push_vec2(NkContext ctx, nk_vec2* address, nk_vec2 value)
        {
            nk_config_stack_vec2 type_stack;
            nk_config_stack_vec2_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.vectors;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (nk_vec2)(*address);
            *address = (nk_vec2)(value);
            return (int)(1);
        }

        public static int nk_style_push_flags(NkContext ctx, uint* address, uint value)
        {
            nk_config_stack_flags type_stack;
            nk_config_stack_flags_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.flags;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (uint)(*address);
            *address = (uint)(value);
            return (int)(1);
        }

        public static int nk_style_push_color(NkContext ctx, nk_color* address, nk_color value)
        {
            nk_config_stack_color type_stack;
            nk_config_stack_color_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.colors;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (nk_color)(*address);
            *address = (nk_color)(value);
            return (int)(1);
        }

        public static int nk_style_pop_style_item(NkContext ctx)
        {
            nk_config_stack_style_item type_stack;
            nk_config_stack_style_item_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.style_items;
            if ((type_stack.head) < (1)) return (int)(0);
            element = type_stack.elements[(--type_stack.head)];
            element.address = (NkStyleItem)(element.old_value);
            return (int)(1);
        }

        public static int nk_style_pop_float(NkContext ctx)
        {
            nk_config_stack_float type_stack;
            nk_config_stack_float_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.floats;
            if ((type_stack.head) < (1)) return (int)(0);
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (float)(element.old_value);
            return (int)(1);
        }

        public static int nk_style_pop_vec2(NkContext ctx)
        {
            nk_config_stack_vec2 type_stack;
            nk_config_stack_vec2_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.vectors;
            if ((type_stack.head) < (1)) return (int)(0);
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (nk_vec2)(element.old_value);
            return (int)(1);
        }

        public static int nk_style_pop_flags(NkContext ctx)
        {
            nk_config_stack_flags type_stack;
            nk_config_stack_flags_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.flags;
            if ((type_stack.head) < (1)) return (int)(0);
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (uint)(element.old_value);
            return (int)(1);
        }

        public static int nk_style_pop_color(NkContext ctx)
        {
            nk_config_stack_color type_stack;
            nk_config_stack_color_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.colors;
            if ((type_stack.head) < (1)) return (int)(0);
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (nk_color)(element.old_value);
            return (int)(1);
        }

        public static int nk_style_set_cursor(NkContext ctx, int c)
        {
            NkStyle style;
            if (ctx == null) return (int)(0);
            style = ctx.Style;
            if ((style.Cursors[c]) != null)
            {
                style.CursorActive = style.Cursors[c];
                return (int)(1);
            }

            return (int)(0);
        }

        public static void nk_style_show_cursor(NkContext ctx)
        {
            ctx.Style.CursorVisible = (int)(nk_true);
        }

        public static void nk_style_hide_cursor(NkContext ctx)
        {
            ctx.Style.CursorVisible = (int)(nk_false);
        }

        public static void nk_style_load_cursor(NkContext ctx, int cursor, nk_cursor c)
        {
            NkStyle style;
            if (ctx == null) return;
            style = ctx.Style;
            style.Cursors[cursor] = c;
        }

        public static void nk_style_load_all_cursors(NkContext ctx, nk_cursor[] cursors)
        {
            int i = (int)(0);
            NkStyle style;
            if (ctx == null) return;
            style = ctx.Style;
            for (i = (int)(0); (i) < (NK_CURSOR_COUNT); ++i)
            {
                style.Cursors[i] = cursors[i];
            }
            style.CursorVisible = (int)(nk_true);
        }

        public static void nk_setup(NkContext ctx, NkUserFont font)
        {
            if (ctx == null) return;

            nk_style_default(ctx);
            ctx.Seq = (uint)(1);
            if ((font) != null) ctx.Style.Font = font;
            nk_draw_list_init(ctx.DrawList);
        }

        public static void nk_clear(NkContext ctx)
        {
            NkWindow iter;
            NkWindow next;
            if (ctx == null) return;
            ctx.Build = (int)(0);
            ctx.LastWidgetState = (uint)(0);
            ctx.Style.CursorActive = ctx.Style.Cursors[NK_CURSOR_ARROW];

            nk_draw_list_clear(ctx.DrawList);
            iter = ctx.Begin;
            while ((iter) != null)
            {
                if ((((iter.Flags & NK_WINDOW_MINIMIZED) != 0) && ((iter.Flags & NK_WINDOW_CLOSED) == 0)) &&
                    ((iter.Seq) == (ctx.Seq)))
                {
                    iter = iter.Next;
                    continue;
                }
                if ((((iter.Flags & NK_WINDOW_HIDDEN) != 0) || ((iter.Flags & NK_WINDOW_CLOSED) != 0)) && ((iter) == (ctx.Active)))
                {
                    ctx.Active = iter.Prev;
                    ctx.End = iter.Prev;
                    if ((ctx.Active) != null) ctx.Active.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
                }
                if (((iter.Popup.win) != null) && (iter.Popup.win.Seq != ctx.Seq))
                {
                    nk_free_window(ctx, iter.Popup.win);
                    iter.Popup.win = null;
                }
                {
                    nk_table n;
                    nk_table it = iter.Tables;
                    while ((it) != null)
                    {
                        n = it.next;
                        if (it.seq != ctx.Seq)
                        {
                            nk_remove_table(iter, it);
                            if ((it) == (iter.Tables)) iter.Tables = n;
                        }
                        it = n;
                    }
                }
                if ((iter.Seq != ctx.Seq) || ((iter.Flags & NK_WINDOW_CLOSED) != 0))
                {
                    next = iter.Next;
                    nk_remove_window(ctx, iter);
                    nk_free_window(ctx, iter);
                    iter = next;
                }
                else iter = iter.Next;
            }
            ctx.Seq++;
        }

        public static void nk_start_buffer(NkContext ctx, NkCommandBuffer buffer)
        {
            if ((ctx == null) || (buffer == null)) return;

            buffer.First = buffer.Last = null;
            buffer.Count = 0;
            buffer.Clip = (nk_rect)(nk_null_rect);
        }

        public static void nk_start(NkContext ctx, NkWindow win)
        {
            nk_start_buffer(ctx, win.Buffer);
        }

        public static void nk_start_popup(NkContext ctx, NkWindow win)
        {
            if ((ctx == null) || (win == null)) return;

            var buf = win.Popup.buf.Buffer;

            buf.First = buf.Last = null;
            buf.Count = 0;

            win.Popup.buf.OldBuffer = win.Buffer;
            win.Buffer = buf;
        }

        public static void nk_finish_popup(NkContext ctx, NkWindow win)
        {
            if ((ctx == null) || (win == null)) return;

            win.Buffer = win.Popup.buf.OldBuffer;
        }

        public static void nk_finish(NkContext ctx, NkWindow win)
        {
            if ((ctx == null) || (win == null) || win.Popup.active == 0) return;


        }

        public static int nk_panel_begin(NkContext ctx, char* title, int panel_type)
        {
            nk_input _in_;
            NkWindow win;
            NkPanel layout;
            NkCommandBuffer _out_;
            NkStyle style;
            NkUserFont font;
            nk_vec2 scrollbar_size = new nk_vec2();
            nk_vec2 panel_padding = new nk_vec2();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);

            if (((ctx.Current.Flags & NK_WINDOW_HIDDEN) != 0) || ((ctx.Current.Flags & NK_WINDOW_CLOSED) != 0))
            {
                ctx.Current.Layout.Type = (int)(panel_type);
                return (int)(0);
            }

            style = ctx.Style;
            font = style.Font;
            win = ctx.Current;
            layout = win.Layout;
            _out_ = win.Buffer;
            _in_ = (win.Flags & NK_WINDOW_NO_INPUT) != 0 ? null : ctx.Input;
            scrollbar_size = (nk_vec2)(style.Window.scrollbar_size);
            panel_padding = (nk_vec2)(nk_panel_get_padding(style, (int)(panel_type)));
            if (((win.Flags & NK_WINDOW_MOVABLE) != 0) && ((win.Flags & NK_WINDOW_ROM) == 0))
            {
                int left_mouse_down;
                int left_mouse_click_in_cursor;
                nk_rect header = new nk_rect();
                header.x = (float)(win.Bounds.x);
                header.y = (float)(win.Bounds.y);
                header.w = (float)(win.Bounds.w);
                if ((nk_panel_has_header((uint)(win.Flags), title)) != 0)
                {
                    header.h = (float)(font.Height + 2.0f * style.Window.header.padding.y);
                    header.h += (float)(2.0f * style.Window.header.label_padding.y);
                }
                else header.h = (float)(panel_padding.y);
                left_mouse_down = (int)(((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->down);
                left_mouse_click_in_cursor =
                    (int)(nk_input_has_mouse_click_down_in_rect(_in_, (int)(NK_BUTTON_LEFT), (nk_rect)(header), (int)(nk_true)));
                if (((left_mouse_down) != 0) && ((left_mouse_click_in_cursor) != 0))
                {
                    win.Bounds.x = (float)(win.Bounds.x + _in_.mouse.Delta.x);
                    win.Bounds.y = (float)(win.Bounds.y + _in_.mouse.Delta.y);
                    ((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->clicked_pos.x += (float)(_in_.mouse.Delta.x);
                    ((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->clicked_pos.y += (float)(_in_.mouse.Delta.y);
                    ctx.Style.CursorActive = ctx.Style.Cursors[NK_CURSOR_MOVE];
                }
            }

            layout.Type = (int)(panel_type);
            layout.Flags = (uint)(win.Flags);
            layout.Bounds = (nk_rect)(win.Bounds);
            layout.Bounds.x += (float)(panel_padding.x);
            layout.Bounds.w -= (float)(2 * panel_padding.x);
            if ((win.Flags & NK_WINDOW_BORDER) != 0)
            {
                layout.Border = (float)(nk_panel_get_border(style, (uint)(win.Flags), (int)(panel_type)));
                layout.Bounds = (nk_rect)(nk_shrink_rect_((nk_rect)(layout.Bounds), (float)(layout.Border)));
            }
            else layout.Border = (float)(0);
            layout.AtY = (float)(layout.Bounds.y);
            layout.AtX = (float)(layout.Bounds.x);
            layout.MaxX = (float)(0);
            layout.HeaderHeight = (float)(0);
            layout.FooterHeight = (float)(0);
            nk_layout_reset_min_row_height(ctx);
            layout.Row.index = (int)(0);
            layout.Row.columns = (int)(0);
            layout.Row.ratio = null;
            layout.Row.item_width = (float)(0);
            layout.Row.tree_depth = (int)(0);
            layout.Row.height = (float)(panel_padding.y);
            layout.HasScrolling = (uint)(nk_true);
            if ((win.Flags & NK_WINDOW_NO_SCROLLBAR) == 0) layout.Bounds.w -= (float)(scrollbar_size.x);
            if (nk_panel_is_nonblock((int)(panel_type)) == 0)
            {
                layout.FooterHeight = (float)(0);
                if (((win.Flags & NK_WINDOW_NO_SCROLLBAR) == 0) || ((win.Flags & NK_WINDOW_SCALABLE) != 0))
                    layout.FooterHeight = (float)(scrollbar_size.y);
                layout.Bounds.h -= (float)(layout.FooterHeight);
            }

            if ((nk_panel_has_header((uint)(win.Flags), title)) != 0)
            {
                nk_text text = new nk_text();
                nk_rect header = new nk_rect();
                NkStyleItem background = null;
                header.x = (float)(win.Bounds.x);
                header.y = (float)(win.Bounds.y);
                header.w = (float)(win.Bounds.w);
                header.h = (float)(font.Height + 2.0f * style.Window.header.padding.y);
                header.h += (float)(2.0f * style.Window.header.label_padding.y);
                layout.HeaderHeight = (float)(header.h);
                layout.Bounds.y += (float)(header.h);
                layout.Bounds.h -= (float)(header.h);
                layout.AtY += (float)(header.h);
                if ((ctx.Active) == (win))
                {
                    background = style.Window.header.active;
                    text.text = (nk_color)(style.Window.header.label_active);
                }
                else if ((nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(header))) != 0)
                {
                    background = style.Window.header.hover;
                    text.text = (nk_color)(style.Window.header.label_hover);
                }
                else
                {
                    background = style.Window.header.normal;
                    text.text = (nk_color)(style.Window.header.label_normal);
                }
                header.h += (float)(1.0f);
                if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
                {
                    text.background = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                    nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
                }
                else
                {
                    text.background = (nk_color)(background.Data.Color);
                    nk_fill_rect(_out_, (nk_rect)(header), (float)(0), (nk_color)(background.Data.Color));
                }
                {
                    nk_rect button = new nk_rect();
                    button.y = (float)(header.y + style.Window.header.padding.y);
                    button.h = (float)(header.h - 2 * style.Window.header.padding.y);
                    button.w = (float)(button.h);
                    if ((win.Flags & NK_WINDOW_CLOSABLE) != 0)
                    {
                        uint ws = (uint)(0);
                        if ((style.Window.header.align) == (NK_HEADER_RIGHT))
                        {
                            button.x = (float)((header.w + header.x) - (button.w + style.Window.header.padding.x));
                            header.w -= (float)(button.w + style.Window.header.spacing.x + style.Window.header.padding.x);
                        }
                        else
                        {
                            button.x = (float)(header.x + style.Window.header.padding.x);
                            header.x += (float)(button.w + style.Window.header.spacing.x + style.Window.header.padding.x);
                        }
                        if (
                            ((nk_do_button_symbol(ref ws, win.Buffer, (nk_rect)(button), (int)(style.Window.header.close_symbol),
                                (int)(NK_BUTTON_DEFAULT), style.Window.header.close_button, _in_, style.Font)) != 0) &&
                            ((win.Flags & NK_WINDOW_ROM) == 0))
                        {
                            layout.Flags |= (uint)(NK_WINDOW_HIDDEN);
                            layout.Flags &= ((uint)(~(uint)NK_WINDOW_MINIMIZED));
                        }
                    }
                    if ((win.Flags & NK_WINDOW_MINIMIZABLE) != 0)
                    {
                        uint ws = (uint)(0);
                        if ((style.Window.header.align) == (NK_HEADER_RIGHT))
                        {
                            button.x = (float)((header.w + header.x) - button.w);
                            if ((win.Flags & NK_WINDOW_CLOSABLE) == 0)
                            {
                                button.x -= (float)(style.Window.header.padding.x);
                                header.w -= (float)(style.Window.header.padding.x);
                            }
                            header.w -= (float)(button.w + style.Window.header.spacing.x);
                        }
                        else
                        {
                            button.x = (float)(header.x);
                            header.x += (float)(button.w + style.Window.header.spacing.x + style.Window.header.padding.x);
                        }
                        if (
                            ((nk_do_button_symbol(ref ws, win.Buffer, (nk_rect)(button),
                                (int)
                                    ((layout.Flags & NK_WINDOW_MINIMIZED) != 0
                                        ? style.Window.header.maximize_symbol
                                        : style.Window.header.minimize_symbol), (int)(NK_BUTTON_DEFAULT), style.Window.header.minimize_button, _in_,
                                style.Font)) != 0) && ((win.Flags & NK_WINDOW_ROM) == 0))
                            layout.Flags =
                                (uint)
                                    ((layout.Flags & NK_WINDOW_MINIMIZED) != 0
                                        ? layout.Flags & (uint)(~(uint)NK_WINDOW_MINIMIZED)
                                        : layout.Flags | NK_WINDOW_MINIMIZED);
                    }
                }
                {
                    int text_len = (int)(nk_strlen(title));
                    nk_rect label = new nk_rect();
                    float t = (float)(font.Width((NkHandle)(font.Userdata), (float)(font.Height), title, (int)(text_len)));
                    text.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
                    label.x = (float)(header.x + style.Window.header.padding.x);
                    label.x += (float)(style.Window.header.label_padding.x);
                    label.y = (float)(header.y + style.Window.header.label_padding.y);
                    label.h = (float)(font.Height + 2 * style.Window.header.label_padding.y);
                    label.w = (float)(t + 2 * style.Window.header.spacing.x);
                    label.w =
                        (float)
                            (((label.w) < (header.x + header.w - label.x) ? (label.w) : (header.x + header.w - label.x)) < (0)
                                ? (0)
                                : ((label.w) < (header.x + header.w - label.x) ? (label.w) : (header.x + header.w - label.x)));
                    nk_widget_text(_out_, (nk_rect)(label), title, (int)(text_len), &text, (uint)(NK_TEXT_LEFT), font);
                }
            }

            if (((layout.Flags & NK_WINDOW_MINIMIZED) == 0) && ((layout.Flags & NK_WINDOW_DYNAMIC) == 0))
            {
                nk_rect body = new nk_rect();
                body.x = (float)(win.Bounds.x);
                body.w = (float)(win.Bounds.w);
                body.y = (float)(win.Bounds.y + layout.HeaderHeight);
                body.h = (float)(win.Bounds.h - layout.HeaderHeight);
                if ((style.Window.fixed_background.Type) == (NK_STYLE_ITEM_IMAGE))
                    nk_draw_image(_out_, (nk_rect)(body), style.Window.fixed_background.Data.Image, (nk_color)(nk_white));
                else nk_fill_rect(_out_, (nk_rect)(body), (float)(0), (nk_color)(style.Window.fixed_background.Data.Color));
            }

            {
                nk_rect clip = new nk_rect();
                layout.Clip = (nk_rect)(layout.Bounds);
                nk_unify(ref clip, ref win.Buffer.Clip, (float)(layout.Clip.x), (float)(layout.Clip.y),
                    (float)(layout.Clip.x + layout.Clip.w), (float)(layout.Clip.y + layout.Clip.h));
                nk_push_scissor(_out_, (nk_rect)(clip));
                layout.Clip = (nk_rect)(clip);
            }

            return (int)(((layout.Flags & NK_WINDOW_HIDDEN) == 0) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0) ? 1 : 0);
        }

        public static void nk_panel_end(NkContext ctx)
        {
            nk_input _in_;
            NkWindow window;
            NkPanel layout;
            NkStyle style;
            NkCommandBuffer _out_;
            nk_vec2 scrollbar_size = new nk_vec2();
            nk_vec2 panel_padding = new nk_vec2();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            window = ctx.Current;
            layout = window.Layout;
            style = ctx.Style;
            _out_ = window.Buffer;
            _in_ = (((layout.Flags & NK_WINDOW_ROM) != 0) || ((layout.Flags & NK_WINDOW_NO_INPUT) != 0)) ? null : ctx.Input;
            if (nk_panel_is_sub((int)(layout.Type)) == 0) nk_push_scissor(_out_, (nk_rect)(nk_null_rect));
            scrollbar_size = (nk_vec2)(style.Window.scrollbar_size);
            panel_padding = (nk_vec2)(nk_panel_get_padding(style, (int)(layout.Type)));
            layout.AtY += (float)(layout.Row.height);
            if (((layout.Flags & NK_WINDOW_DYNAMIC) != 0) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0))
            {
                nk_rect empty_space = new nk_rect();
                if ((layout.AtY) < (layout.Bounds.y + layout.Bounds.h)) layout.Bounds.h = (float)(layout.AtY - layout.Bounds.y);
                empty_space.x = (float)(window.Bounds.x);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.h = (float)(panel_padding.y);
                empty_space.w = (float)(window.Bounds.w);
                nk_fill_rect(_out_, (nk_rect)(empty_space), (float)(0), (nk_color)(style.Window.background));
                empty_space.x = (float)(window.Bounds.x);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.w = (float)(panel_padding.x + layout.Border);
                empty_space.h = (float)(layout.Bounds.h);
                nk_fill_rect(_out_, (nk_rect)(empty_space), (float)(0), (nk_color)(style.Window.background));
                empty_space.x = (float)(layout.Bounds.x + layout.Bounds.w - layout.Border);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.w = (float)(panel_padding.x + layout.Border);
                empty_space.h = (float)(layout.Bounds.h);
                if (((layout.Offset.y) == (0)) && ((layout.Flags & NK_WINDOW_NO_SCROLLBAR) == 0))
                    empty_space.w += (float)(scrollbar_size.x);
                nk_fill_rect(_out_, (nk_rect)(empty_space), (float)(0), (nk_color)(style.Window.background));
                if ((layout.Offset.x != 0) && ((layout.Flags & NK_WINDOW_NO_SCROLLBAR) == 0))
                {
                    empty_space.x = (float)(window.Bounds.x);
                    empty_space.y = (float)(layout.Bounds.y + layout.Bounds.h);
                    empty_space.w = (float)(window.Bounds.w);
                    empty_space.h = (float)(scrollbar_size.y);
                    nk_fill_rect(_out_, (nk_rect)(empty_space), (float)(0), (nk_color)(style.Window.background));
                }
            }

            if ((((layout.Flags & NK_WINDOW_NO_SCROLLBAR) == 0) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0)) &&
                ((window.ScrollbarHidingTimer) < (4.0f)))
            {
                nk_rect scroll = new nk_rect();
                int scroll_has_scrolling;
                float scroll_target;
                float scroll_offset;
                float scroll_step;
                float scroll_inc;
                if ((nk_panel_is_sub((int)(layout.Type))) != 0)
                {
                    NkWindow root_window = window;
                    NkPanel root_panel = window.Layout;
                    while ((root_panel.Parent) != null)
                    {
                        root_panel = root_panel.Parent;
                    }
                    while ((root_window.Parent) != null)
                    {
                        root_window = root_window.Parent;
                    }
                    scroll_has_scrolling = (int)(0);
                    if (((root_window) == (ctx.Active)) && ((layout.HasScrolling) != 0))
                    {
                        if (((nk_input_is_mouse_hovering_rect(_in_, (nk_rect)(layout.Bounds))) != 0) &&
                            (!(((((root_panel.Clip.x) > (layout.Bounds.x + layout.Bounds.w)) ||
                                 ((root_panel.Clip.x + root_panel.Clip.w) < (layout.Bounds.x))) ||
                                ((root_panel.Clip.y) > (layout.Bounds.y + layout.Bounds.h))) ||
                               ((root_panel.Clip.y + root_panel.Clip.h) < (layout.Bounds.y)))))
                        {
                            root_panel = window.Layout;
                            while ((root_panel.Parent) != null)
                            {
                                root_panel.HasScrolling = (uint)(nk_false);
                                root_panel = root_panel.Parent;
                            }
                            root_panel.HasScrolling = (uint)(nk_false);
                            scroll_has_scrolling = (int)(nk_true);
                        }
                    }
                }
                else if (nk_panel_is_sub((int)(layout.Type)) == 0)
                {
                    scroll_has_scrolling = (int)(((window) == (ctx.Active)) && ((layout.HasScrolling) != 0) ? 1 : 0);
                    if ((((_in_) != null) && (((_in_.mouse.ScrollDelta.y) > (0)) || ((_in_.mouse.ScrollDelta.x) > (0)))) &&
                        ((scroll_has_scrolling) != 0)) window.Scrolled = (uint)(nk_true);
                    else window.Scrolled = (uint)(nk_false);
                }
                else scroll_has_scrolling = (int)(nk_false);
                {
                    uint state = (uint)(0);
                    scroll.x = (float)(layout.Bounds.x + layout.Bounds.w + panel_padding.x);
                    scroll.y = (float)(layout.Bounds.y);
                    scroll.w = (float)(scrollbar_size.x);
                    scroll.h = (float)(layout.Bounds.h);
                    scroll_offset = ((float)(layout.Offset.y));
                    scroll_step = (float)(scroll.h * 0.10f);
                    scroll_inc = (float)(scroll.h * 0.01f);
                    scroll_target = ((float)((int)(layout.AtY - scroll.y)));
                    scroll_offset =
                        (float)
                            (nk_do_scrollbarv(ref state, _out_, (nk_rect)(scroll), (int)(scroll_has_scrolling), (float)(scroll_offset),
                                (float)(scroll_target), (float)(scroll_step), (float)(scroll_inc), ctx.Style.Scrollv, _in_, style.Font));
                    layout.Offset.y = ((uint)(scroll_offset));
                    if (((_in_) != null) && ((scroll_has_scrolling) != 0)) _in_.mouse.ScrollDelta.y = (float)(0);
                }
                {
                    uint state = (uint)(0);
                    scroll.x = (float)(layout.Bounds.x);
                    scroll.y = (float)(layout.Bounds.y + layout.Bounds.h);
                    scroll.w = (float)(layout.Bounds.w);
                    scroll.h = (float)(scrollbar_size.y);
                    scroll_offset = ((float)(layout.Offset.x));
                    scroll_target = ((float)((int)(layout.MaxX - scroll.x)));
                    scroll_step = (float)(layout.MaxX * 0.05f);
                    scroll_inc = (float)(layout.MaxX * 0.005f);
                    scroll_offset =
                        (float)
                            (nk_do_scrollbarh(ref state, _out_, (nk_rect)(scroll), (int)(scroll_has_scrolling), (float)(scroll_offset),
                                (float)(scroll_target), (float)(scroll_step), (float)(scroll_inc), ctx.Style.Scrollh, _in_, style.Font));
                    layout.Offset.x = ((uint)(scroll_offset));
                }
            }

            if ((window.Flags & NK_WINDOW_SCROLL_AUTO_HIDE) != 0)
            {
                int has_input =
                    (int)
                        (((ctx.Input.mouse.Delta.x != 0) || (ctx.Input.mouse.Delta.y != 0)) || (ctx.Input.mouse.ScrollDelta.y != 0)
                            ? 1
                            : 0);
                int is_window_hovered = (int)(nk_window_is_hovered(ctx));
                int any_item_active = (int)(ctx.LastWidgetState & NK_WIDGET_STATE_MODIFIED);
                if (((has_input == 0) && ((is_window_hovered) != 0)) || ((is_window_hovered == 0) && (any_item_active == 0)))
                    window.ScrollbarHidingTimer += (float)(ctx.DeltaTimeSeconds);
                else window.ScrollbarHidingTimer = (float)(0);
            }
            else window.ScrollbarHidingTimer = (float)(0);
            if ((layout.Flags & NK_WINDOW_BORDER) != 0)
            {
                nk_color border_color = (nk_color)(nk_panel_get_border_color(style, (int)(layout.Type)));
                float padding_y =
                    (float)
                        ((layout.Flags & NK_WINDOW_MINIMIZED) != 0
                            ? style.Window.border + window.Bounds.y + layout.HeaderHeight
                            : (layout.Flags & NK_WINDOW_DYNAMIC) != 0
                                ? layout.Bounds.y + layout.Bounds.h + layout.FooterHeight
                                : window.Bounds.y + window.Bounds.h);
                nk_rect b = window.Bounds;
                b.h = padding_y - window.Bounds.y;
                nk_stroke_rect(_out_, b, 0, layout.Border, border_color);
            }

            if ((((layout.Flags & NK_WINDOW_SCALABLE) != 0) && ((_in_) != null)) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0))
            {
                nk_rect scaler = new nk_rect();
                scaler.w = (float)(scrollbar_size.x);
                scaler.h = (float)(scrollbar_size.y);
                scaler.y = (float)(layout.Bounds.y + layout.Bounds.h);
                if ((layout.Flags & NK_WINDOW_SCALE_LEFT) != 0) scaler.x = (float)(layout.Bounds.x - panel_padding.x * 0.5f);
                else scaler.x = (float)(layout.Bounds.x + layout.Bounds.w + panel_padding.x);
                if ((layout.Flags & NK_WINDOW_NO_SCROLLBAR) != 0) scaler.x -= (float)(scaler.w);
                {
                    NkStyleItem item = style.Window.scaler;
                    if ((item.Type) == (NK_STYLE_ITEM_IMAGE))
                        nk_draw_image(_out_, (nk_rect)(scaler), item.Data.Image, (nk_color)(nk_white));
                    else
                    {
                        if ((layout.Flags & NK_WINDOW_SCALE_LEFT) != 0)
                        {
                            nk_fill_triangle(_out_, (float)(scaler.x), (float)(scaler.y), (float)(scaler.x), (float)(scaler.y + scaler.h),
                                (float)(scaler.x + scaler.w), (float)(scaler.y + scaler.h), (nk_color)(item.Data.Color));
                        }
                        else
                        {
                            nk_fill_triangle(_out_, (float)(scaler.x + scaler.w), (float)(scaler.y), (float)(scaler.x + scaler.w),
                                (float)(scaler.y + scaler.h), (float)(scaler.x), (float)(scaler.y + scaler.h), (nk_color)(item.Data.Color));
                        }
                    }
                }
                if ((window.Flags & NK_WINDOW_ROM) == 0)
                {
                    nk_vec2 window_size = (nk_vec2)(style.Window.min_size);
                    int left_mouse_down = (int)(((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->down);
                    int left_mouse_click_in_scaler =
                        (int)(nk_input_has_mouse_click_down_in_rect(_in_, (int)(NK_BUTTON_LEFT), (nk_rect)(scaler), (int)(nk_true)));
                    if (((left_mouse_down) != 0) && ((left_mouse_click_in_scaler) != 0))
                    {
                        float delta_x = (float)(_in_.mouse.Delta.x);
                        if ((layout.Flags & NK_WINDOW_SCALE_LEFT) != 0)
                        {
                            delta_x = (float)(-delta_x);
                            window.Bounds.x += (float)(_in_.mouse.Delta.x);
                        }
                        if ((window.Bounds.w + delta_x) >= (window_size.x))
                        {
                            if (((delta_x) < (0)) || (((delta_x) > (0)) && ((_in_.mouse.Pos.x) >= (scaler.x))))
                            {
                                window.Bounds.w = (float)(window.Bounds.w + delta_x);
                                scaler.x += (float)(_in_.mouse.Delta.x);
                            }
                        }
                        if ((layout.Flags & NK_WINDOW_DYNAMIC) == 0)
                        {
                            if ((window_size.y) < (window.Bounds.h + _in_.mouse.Delta.y))
                            {
                                if (((_in_.mouse.Delta.y) < (0)) || (((_in_.mouse.Delta.y) > (0)) && ((_in_.mouse.Pos.y) >= (scaler.y))))
                                {
                                    window.Bounds.h = (float)(window.Bounds.h + _in_.mouse.Delta.y);
                                    scaler.y += (float)(_in_.mouse.Delta.y);
                                }
                            }
                        }
                        ctx.Style.CursorActive = ctx.Style.Cursors[NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT];
                        ((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->clicked_pos.x = (float)(scaler.x + scaler.w / 2.0f);
                        ((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->clicked_pos.y = (float)(scaler.y + scaler.h / 2.0f);
                    }
                }
            }

            if (nk_panel_is_sub((int)(layout.Type)) == 0)
            {
                if ((layout.Flags & NK_WINDOW_HIDDEN) != 0) nk_command_buffer_reset(window.Buffer);
                else nk_finish(ctx, window);
            }

            if ((layout.Flags & NK_WINDOW_REMOVE_ROM) != 0)
            {
                layout.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
                layout.Flags &= (uint)(~(uint)(NK_WINDOW_REMOVE_ROM));
            }

            window.Flags = (uint)(layout.Flags);
            if ((((window.Property.active) != 0) && (window.Property.old != window.Property.seq)) &&
                ((window.Property.active) == (window.Property.prev)))
            {
            }
            else
            {
                window.Property.old = (uint)(window.Property.seq);
                window.Property.prev = (int)(window.Property.active);
                window.Property.seq = (uint)(0);
            }

            if ((((window.Edit.active) != 0) && (window.Edit.old != window.Edit.seq)) &&
                ((window.Edit.active) == (window.Edit.prev)))
            {
            }
            else
            {
                window.Edit.old = (uint)(window.Edit.seq);
                window.Edit.prev = (int)(window.Edit.active);
                window.Edit.seq = (uint)(0);
            }

            if (((window.Popup.active_con) != 0) && (window.Popup.con_old != window.Popup.con_count))
            {
                window.Popup.con_count = (uint)(0);
                window.Popup.con_old = (uint)(0);
                window.Popup.active_con = (uint)(0);
            }
            else
            {
                window.Popup.con_old = (uint)(window.Popup.con_count);
                window.Popup.con_count = (uint)(0);
            }

            window.Popup.combo_count = (uint)(0);
        }

        public static uint* nk_add_value(NkContext ctx, NkWindow win, uint name, uint value)
        {
            if ((win == null) || (ctx == null)) return null;
            if ((win.Tables == null) || ((win.Tables.size) >= (51)))
            {
                nk_table tbl = nk_create_table(ctx);
                if (tbl == null) return null;
                nk_push_table(win, tbl);
            }

            win.Tables.seq = (uint)(win.Seq);
            win.Tables.keys[win.Tables.size] = (uint)(name);
            win.Tables.values[win.Tables.size] = (uint)(value);
            return (uint*)win.Tables.values + (win.Tables.size++);
        }

        public static NkWindow nk_find_window(NkContext ctx, uint hash, char* name)
        {
            NkWindow iter;
            iter = ctx.Begin;
            while ((iter) != null)
            {
                if ((iter.Name) == (hash))
                {
                    int max_len = (int)(nk_strlen(iter.NameString));
                    if (nk_stricmpn(iter.NameString, name, (int)(max_len)) == 0) return iter;
                }
                iter = iter.Next;
            }
            return null;
        }

        public static void nk_insert_window(NkContext ctx, NkWindow win, int loc)
        {
            NkWindow iter;
            if ((win == null) || (ctx == null)) return;
            iter = ctx.Begin;
            while ((iter) != null)
            {
                if ((iter) == (win)) return;
                iter = iter.Next;
            }
            if (ctx.Begin == null)
            {
                win.Next = null;
                win.Prev = null;
                ctx.Begin = win;
                ctx.End = win;
                ctx.Count = (uint)(1);
                return;
            }

            if ((loc) == (NK_INSERT_BACK))
            {
                NkWindow end;
                end = ctx.End;
                end.Flags |= (uint)(NK_WINDOW_ROM);
                end.Next = win;
                win.Prev = ctx.End;
                win.Next = null;
                ctx.End = win;
                ctx.Active = ctx.End;
                ctx.End.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
            }
            else
            {
                ctx.Begin.Prev = win;
                win.Next = ctx.Begin;
                win.Prev = null;
                ctx.Begin = win;
                ctx.Begin.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
            }

            ctx.Count++;
        }

        public static void nk_remove_window(NkContext ctx, NkWindow win)
        {
            if (((win) == (ctx.Begin)) || ((win) == (ctx.End)))
            {
                if ((win) == (ctx.Begin))
                {
                    ctx.Begin = win.Next;
                    if ((win.Next) != null) win.Next.Prev = null;
                }
                if ((win) == (ctx.End))
                {
                    ctx.End = win.Prev;
                    if ((win.Prev) != null) win.Prev.Next = null;
                }
            }
            else
            {
                if ((win.Next) != null) win.Next.Prev = win.Prev;
                if ((win.Prev) != null) win.Prev.Next = win.Next;
            }

            if (((win) == (ctx.Active)) || (ctx.Active == null))
            {
                ctx.Active = ctx.End;
                if ((ctx.End) != null) ctx.End.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
            }

            win.Next = null;
            win.Prev = null;
            ctx.Count--;
        }

        public static int nk_begin(NkContext ctx, char* title, nk_rect bounds, uint flags)
        {
            return (int)(nk_begin_titled(ctx, title, title, (nk_rect)(bounds), (uint)(flags)));
        }

        public static int nk_begin_titled(NkContext ctx, char* name, char* title, nk_rect bounds, uint flags)
        {
            NkWindow win;
            NkStyle style;
            uint title_hash;
            int title_len;
            int ret = (int)(0);
            if ((((ctx == null) || ((ctx.Current) != null)) || (title == null)) || (name == null)) return (int)(0);
            style = ctx.Style;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null)
            {
                ulong name_length = (ulong)(nk_strlen(name));
                win = (NkWindow)(nk_create_window(ctx));
                if (win == null) return (int)(0);
                if ((flags & NK_WINDOW_BACKGROUND) != 0) nk_insert_window(ctx, win, (int)(NK_INSERT_FRONT));
                else nk_insert_window(ctx, win, (int)(NK_INSERT_BACK));
                nk_command_buffer_init(win.Buffer, (int)(NK_CLIPPING_ON));
                win.Flags = (uint)(flags);
                win.Bounds = (nk_rect)(bounds);
                win.Name = (uint)(title_hash);
                name_length = (ulong)((name_length) < (64 - 1) ? (name_length) : (64 - 1));
                nk_memcopy(win.NameString, name, (ulong)(name_length));
                win.NameString[name_length] = (char)(0);
                win.Popup.win = null;
                if (ctx.Active == null) ctx.Active = win;
            }
            else
            {
                win.Flags &= (uint)(~(uint)(NK_WINDOW_PRIVATE - 1));
                win.Flags |= (uint)(flags);
                if ((win.Flags & (NK_WINDOW_MOVABLE | NK_WINDOW_SCALABLE)) == 0) win.Bounds = (nk_rect)(bounds);
                win.Seq = (uint)(ctx.Seq);
                if ((ctx.Active == null) && ((win.Flags & NK_WINDOW_HIDDEN) == 0))
                {
                    ctx.Active = win;
                    ctx.End = win;
                }
            }

            if ((win.Flags & NK_WINDOW_HIDDEN) != 0)
            {
                ctx.Current = win;
                win.Layout = null;
                return (int)(0);
            }
            else nk_start(ctx, win);
            if (((win.Flags & NK_WINDOW_HIDDEN) == 0) && ((win.Flags & NK_WINDOW_NO_INPUT) == 0))
            {
                int inpanel;
                int ishovered;
                NkWindow iter = win;
                float h =
                    (float)(ctx.Style.Font.Height + 2.0f * style.Window.header.padding.y + (2.0f * style.Window.header.label_padding.y));
                nk_rect win_bounds =
                    (nk_rect)
                        (((win.Flags & NK_WINDOW_MINIMIZED) == 0)
                            ? win.Bounds
                            : nk_rect_((float)(win.Bounds.x), (float)(win.Bounds.y), (float)(win.Bounds.w), (float)(h)));
                inpanel =
                    (int)
                        (nk_input_has_mouse_click_down_in_rect(ctx.Input, (int)(NK_BUTTON_LEFT), (nk_rect)(win_bounds), (int)(nk_true)));
                inpanel = (int)(((inpanel) != 0) && ((ctx.Input.mouse.Buttons[NK_BUTTON_LEFT].clicked) != 0) ? 1 : 0);
                ishovered = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(win_bounds)));
                if (((win != ctx.Active) && ((ishovered) != 0)) && (ctx.Input.mouse.Buttons[NK_BUTTON_LEFT].down == 0))
                {
                    iter = win.Next;
                    while ((iter) != null)
                    {
                        nk_rect iter_bounds =
                            (nk_rect)
                                (((iter.Flags & NK_WINDOW_MINIMIZED) == 0)
                                    ? iter.Bounds
                                    : nk_rect_((float)(iter.Bounds.x), (float)(iter.Bounds.y), (float)(iter.Bounds.w), (float)(h)));
                        if (
                            (!(((((iter_bounds.x) > (win_bounds.x + win_bounds.w)) || ((iter_bounds.x + iter_bounds.w) < (win_bounds.x))) ||
                                ((iter_bounds.y) > (win_bounds.y + win_bounds.h))) || ((iter_bounds.y + iter_bounds.h) < (win_bounds.y)))) &&
                            ((iter.Flags & NK_WINDOW_HIDDEN) == 0)) break;
                        if (((((iter.Popup.win) != null) && ((iter.Popup.active) != 0)) && ((iter.Flags & NK_WINDOW_HIDDEN) == 0)) &&
                            (!(((((iter.Popup.win.Bounds.x) > (win.Bounds.x + win_bounds.w)) ||
                                 ((iter.Popup.win.Bounds.x + iter.Popup.win.Bounds.w) < (win.Bounds.x))) ||
                                ((iter.Popup.win.Bounds.y) > (win_bounds.y + win_bounds.h))) ||
                               ((iter.Popup.win.Bounds.y + iter.Popup.win.Bounds.h) < (win_bounds.y))))) break;
                        iter = iter.Next;
                    }
                }
                if ((((iter) != null) && ((inpanel) != 0)) && (win != ctx.End))
                {
                    iter = win.Next;
                    while ((iter) != null)
                    {
                        nk_rect iter_bounds =
                            (nk_rect)
                                (((iter.Flags & NK_WINDOW_MINIMIZED) == 0)
                                    ? iter.Bounds
                                    : nk_rect_((float)(iter.Bounds.x), (float)(iter.Bounds.y), (float)(iter.Bounds.w), (float)(h)));
                        if (((((iter_bounds.x) <= (ctx.Input.mouse.Pos.x)) && ((ctx.Input.mouse.Pos.x) < (iter_bounds.x + iter_bounds.w))) &&
                             (((iter_bounds.y) <= (ctx.Input.mouse.Pos.y)) && ((ctx.Input.mouse.Pos.y) < (iter_bounds.y + iter_bounds.h)))) &&
                            ((iter.Flags & NK_WINDOW_HIDDEN) == 0)) break;
                        if (((((iter.Popup.win) != null) && ((iter.Popup.active) != 0)) && ((iter.Flags & NK_WINDOW_HIDDEN) == 0)) &&
                            (!(((((iter.Popup.win.Bounds.x) > (win_bounds.x + win_bounds.w)) ||
                                 ((iter.Popup.win.Bounds.x + iter.Popup.win.Bounds.w) < (win_bounds.x))) ||
                                ((iter.Popup.win.Bounds.y) > (win_bounds.y + win_bounds.h))) ||
                               ((iter.Popup.win.Bounds.y + iter.Popup.win.Bounds.h) < (win_bounds.y))))) break;
                        iter = iter.Next;
                    }
                }
                if ((((iter) != null) && ((win.Flags & NK_WINDOW_ROM) == 0)) && ((win.Flags & NK_WINDOW_BACKGROUND) != 0))
                {
                    win.Flags |= ((uint)(NK_WINDOW_ROM));
                    iter.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
                    ctx.Active = iter;
                    if ((iter.Flags & NK_WINDOW_BACKGROUND) == 0)
                    {
                        nk_remove_window(ctx, iter);
                        nk_insert_window(ctx, iter, (int)(NK_INSERT_BACK));
                    }
                }
                else
                {
                    if ((iter == null) && (ctx.End != win))
                    {
                        if ((win.Flags & NK_WINDOW_BACKGROUND) == 0)
                        {
                            nk_remove_window(ctx, win);
                            nk_insert_window(ctx, win, (int)(NK_INSERT_BACK));
                        }
                        win.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
                        ctx.Active = win;
                    }
                    if ((ctx.End != win) && ((win.Flags & NK_WINDOW_BACKGROUND) == 0)) win.Flags |= (uint)(NK_WINDOW_ROM);
                }
            }

            win.Layout = (NkPanel)(nk_create_panel(ctx));
            ctx.Current = win;
            ret = (int)(nk_panel_begin(ctx, title, (int)(NK_PANEL_WINDOW)));
            win.Layout.Offset = win.Scrollbar;

            return (int)(ret);
        }

        public static void nk_end(NkContext ctx)
        {
            NkPanel layout;
            if ((ctx == null) || (ctx.Current == null)) return;
            layout = ctx.Current.Layout;
            if ((layout == null) || (((layout.Type) == (NK_PANEL_WINDOW)) && ((ctx.Current.Flags & NK_WINDOW_HIDDEN) != 0)))
            {
                ctx.Current = null;
                return;
            }

            nk_panel_end(ctx);

            ctx.Current = null;
        }

        public static nk_rect nk_window_get_bounds(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null))
                return (nk_rect)(nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            return (nk_rect)(ctx.Current.Bounds);
        }

        public static nk_vec2 nk_window_get_position(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            return (nk_vec2)(nk_vec2_((float)(ctx.Current.Bounds.x), (float)(ctx.Current.Bounds.y)));
        }

        public static nk_vec2 nk_window_get_size(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            return (nk_vec2)(nk_vec2_((float)(ctx.Current.Bounds.w), (float)(ctx.Current.Bounds.h)));
        }

        public static float nk_window_get_width(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (float)(0);
            return (float)(ctx.Current.Bounds.w);
        }

        public static float nk_window_get_height(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (float)(0);
            return (float)(ctx.Current.Bounds.h);
        }

        public static nk_rect nk_window_get_content_region(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null))
                return (nk_rect)(nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            return (nk_rect)(ctx.Current.Layout.Clip);
        }

        public static nk_vec2 nk_window_get_content_region_min(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            return (nk_vec2)(nk_vec2_((float)(ctx.Current.Layout.Clip.x), (float)(ctx.Current.Layout.Clip.y)));
        }

        public static nk_vec2 nk_window_get_content_region_max(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            return
                (nk_vec2)
                    (nk_vec2_((float)(ctx.Current.Layout.Clip.x + ctx.Current.Layout.Clip.w),
                        (float)(ctx.Current.Layout.Clip.y + ctx.Current.Layout.Clip.h)));
        }

        public static nk_vec2 nk_window_get_content_region_size(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            return (nk_vec2)(nk_vec2_((float)(ctx.Current.Layout.Clip.w), (float)(ctx.Current.Layout.Clip.h)));
        }

        public static NkCommandBuffer nk_window_get_canvas(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return null;
            return ctx.Current.Buffer;
        }

        public static NkPanel nk_window_get_panel(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return null;
            return ctx.Current.Layout;
        }

        public static int nk_window_has_focus(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (int)(0);
            return (int)((ctx.Current) == (ctx.Active) ? 1 : 0);
        }

        public static int nk_window_is_hovered(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (int)(0);
            if ((ctx.Current.Flags & NK_WINDOW_HIDDEN) != 0) return (int)(0);
            return (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(ctx.Current.Bounds)));
        }

        public static int nk_window_is_any_hovered(NkContext ctx)
        {
            NkWindow iter;
            if (ctx == null) return (int)(0);
            iter = ctx.Begin;
            while ((iter) != null)
            {
                if ((iter.Flags & NK_WINDOW_HIDDEN) == 0)
                {
                    if ((((iter.Popup.active) != 0) && ((iter.Popup.win) != null)) &&
                        ((nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(iter.Popup.win.Bounds))) != 0)) return (int)(1);
                    if ((iter.Flags & NK_WINDOW_MINIMIZED) != 0)
                    {
                        nk_rect header = (nk_rect)(iter.Bounds);
                        header.h = (float)(ctx.Style.Font.Height + 2 * ctx.Style.Window.header.padding.y);
                        if ((nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(header))) != 0) return (int)(1);
                    }
                    else if ((nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(iter.Bounds))) != 0)
                    {
                        return (int)(1);
                    }
                }
                iter = iter.Next;
            }
            return (int)(0);
        }

        public static int nk_item_is_any_active(NkContext ctx)
        {
            int any_hovered = (int)(nk_window_is_any_hovered(ctx));
            int any_active = (int)(ctx.LastWidgetState & NK_WIDGET_STATE_MODIFIED);
            return (int)(((any_hovered) != 0) || ((any_active) != 0) ? 1 : 0);
        }

        public static int nk_window_is_collapsed(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return (int)(0);
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return (int)(0);
            return (int)(win.Flags & NK_WINDOW_MINIMIZED);
        }

        public static int nk_window_is_closed(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return (int)(1);
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return (int)(1);
            return (int)(win.Flags & NK_WINDOW_CLOSED);
        }

        public static int nk_window_is_hidden(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return (int)(1);
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return (int)(1);
            return (int)(win.Flags & NK_WINDOW_HIDDEN);
        }

        public static int nk_window_is_active(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return (int)(0);
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return (int)(0);
            return (int)((win) == (ctx.Active) ? 1 : 0);
        }

        public static NkWindow nk_window_find(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            return nk_find_window(ctx, (uint)(title_hash), name);
        }

        public static void nk_window_close(NkContext ctx, char* name)
        {
            NkWindow win;
            if (ctx == null) return;
            win = nk_window_find(ctx, name);
            if (win == null) return;
            if ((ctx.Current) == (win)) return;
            win.Flags |= (uint)(NK_WINDOW_HIDDEN);
            win.Flags |= (uint)(NK_WINDOW_CLOSED);
        }

        public static void nk_window_set_bounds(NkContext ctx, char* name, nk_rect bounds)
        {
            NkWindow win;
            if (ctx == null) return;
            win = nk_window_find(ctx, name);
            if (win == null) return;
            win.Bounds = (nk_rect)(bounds);
        }

        public static void nk_window_set_position(NkContext ctx, char* name, nk_vec2 pos)
        {
            NkWindow win = nk_window_find(ctx, name);
            if (win == null) return;
            win.Bounds.x = (float)(pos.x);
            win.Bounds.y = (float)(pos.y);
        }

        public static void nk_window_set_size(NkContext ctx, char* name, nk_vec2 size)
        {
            NkWindow win = nk_window_find(ctx, name);
            if (win == null) return;
            win.Bounds.w = (float)(size.x);
            win.Bounds.h = (float)(size.y);
        }

        public static void nk_window_collapse(NkContext ctx, char* name, int c)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return;
            if ((c) == (NK_MINIMIZED)) win.Flags |= (uint)(NK_WINDOW_MINIMIZED);
            else win.Flags &= (uint)(~(uint)(NK_WINDOW_MINIMIZED));
        }

        public static void nk_window_collapse_if(NkContext ctx, char* name, int c, int cond)
        {
            if ((ctx == null) || (cond == 0)) return;
            nk_window_collapse(ctx, name, (int)(c));
        }

        public static void nk_window_show(NkContext ctx, char* name, int s)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return;
            if ((s) == (NK_HIDDEN))
            {
                win.Flags |= (uint)(NK_WINDOW_HIDDEN);
            }
            else win.Flags &= (uint)(~(uint)(NK_WINDOW_HIDDEN));
        }

        public static void nk_window_show_if(NkContext ctx, char* name, int s, int cond)
        {
            if ((ctx == null) || (cond == 0)) return;
            nk_window_show(ctx, name, (int)(s));
        }

        public static void nk_window_set_focus(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(NK_WINDOW_TITLE)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (((win) != null) && (ctx.End != win))
            {
                nk_remove_window(ctx, win);
                nk_insert_window(ctx, win, (int)(NK_INSERT_BACK));
            }

            ctx.Active = win;
        }

        public static void nk_menubar_begin(NkContext ctx)
        {
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            layout = ctx.Current.Layout;
            if (((layout.Flags & NK_WINDOW_HIDDEN) != 0) || ((layout.Flags & NK_WINDOW_MINIMIZED) != 0)) return;
            layout.Menu.x = (float)(layout.AtX);
            layout.Menu.y = (float)(layout.AtY + layout.Row.height);
            layout.Menu.w = (float)(layout.Bounds.w);
            layout.Menu.offset = layout.Offset;

            layout.Offset.y = (uint)(0);
        }

        public static void nk_menubar_end(NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            NkCommandBuffer _out_;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            _out_ = win.Buffer;
            layout = win.Layout;
            if (((layout.Flags & NK_WINDOW_HIDDEN) != 0) || ((layout.Flags & NK_WINDOW_MINIMIZED) != 0)) return;
            layout.Menu.h = (float)(layout.AtY - layout.Menu.y);
            layout.Bounds.y += (float)(layout.Menu.h + ctx.Style.Window.spacing.y + layout.Row.height);
            layout.Bounds.h -= (float)(layout.Menu.h + ctx.Style.Window.spacing.y + layout.Row.height);
            layout.Offset.x = (uint)(layout.Menu.offset.x);
            layout.Offset.y = (uint)(layout.Menu.offset.y);
            layout.AtY = (float)(layout.Bounds.y - layout.Row.height);
            layout.Clip.y = (float)(layout.Bounds.y);
            layout.Clip.h = (float)(layout.Bounds.h);
            nk_push_scissor(_out_, (nk_rect)(layout.Clip));
        }

        public static void nk_layout_set_min_row_height(NkContext ctx, float height)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            layout.Row.min_height = (float)(height);
        }

        public static void nk_layout_reset_min_row_height(NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            layout.Row.min_height = (float)(ctx.Style.Font.Height);
            layout.Row.min_height += (float)(ctx.Style.Text.padding.y * 2);
            layout.Row.min_height += (float)(ctx.Style.Window.min_row_height_padding * 2);
        }

        public static void nk_panel_layout(NkContext ctx, NkWindow win, float height, int cols)
        {
            NkPanel layout;
            NkStyle style;
            NkCommandBuffer _out_;
            nk_vec2 item_spacing = new nk_vec2();
            nk_color color = new nk_color();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            layout = win.Layout;
            style = ctx.Style;
            _out_ = win.Buffer;
            color = (nk_color)(style.Window.background);
            item_spacing = (nk_vec2)(style.Window.spacing);
            layout.Row.index = (int)(0);
            layout.AtY += (float)(layout.Row.height);
            layout.Row.columns = (int)(cols);
            if ((height) == (0.0f))
                layout.Row.height =
                    (float)(((height) < (layout.Row.min_height) ? (layout.Row.min_height) : (height)) + item_spacing.y);
            else layout.Row.height = (float)(height + item_spacing.y);
            layout.Row.item_offset = (float)(0);
            if ((layout.Flags & NK_WINDOW_DYNAMIC) != 0)
            {
                nk_rect background = new nk_rect();
                background.x = (float)(win.Bounds.x);
                background.w = (float)(win.Bounds.w);
                background.y = (float)(layout.AtY - 1.0f);
                background.h = (float)(layout.Row.height + 1.0f);
                nk_fill_rect(_out_, (nk_rect)(background), (float)(0), (nk_color)(color));
            }

        }

        public static void nk_row_layout_(NkContext ctx, int fmt, float height, int cols, int width)
        {
            NkWindow win;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            nk_panel_layout(ctx, win, (float)(height), (int)(cols));
            if ((fmt) == (NK_DYNAMIC)) win.Layout.Row.type = (int)(NK_LAYOUT_DYNAMIC_FIXED);
            else win.Layout.Row.type = (int)(NK_LAYOUT_STATIC_FIXED);
            win.Layout.Row.ratio = null;
            win.Layout.Row.filled = (float)(0);
            win.Layout.Row.item_offset = (float)(0);
            win.Layout.Row.item_width = ((float)(width));
        }

        public static float nk_layout_ratio_from_pixel(NkContext ctx, float pixel_width)
        {
            NkWindow win;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (float)(0);
            win = ctx.Current;
            return
                (float)
                    (((pixel_width / win.Bounds.x) < (1.0f) ? (pixel_width / win.Bounds.x) : (1.0f)) < (0.0f)
                        ? (0.0f)
                        : ((pixel_width / win.Bounds.x) < (1.0f) ? (pixel_width / win.Bounds.x) : (1.0f)));
        }

        public static void nk_layout_row_dynamic(NkContext ctx, float height, int cols)
        {
            nk_row_layout_(ctx, (int)(NK_DYNAMIC), (float)(height), (int)(cols), (int)(0));
        }

        public static void nk_layout_row_static(NkContext ctx, float height, int item_width, int cols)
        {
            nk_row_layout_(ctx, (int)(NK_STATIC), (float)(height), (int)(cols), (int)(item_width));
        }

        public static void nk_layout_row_begin(NkContext ctx, int fmt, float row_height, int cols)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            nk_panel_layout(ctx, win, (float)(row_height), (int)(cols));
            if ((fmt) == (NK_DYNAMIC)) layout.Row.type = (int)(NK_LAYOUT_DYNAMIC_ROW);
            else layout.Row.type = (int)(NK_LAYOUT_STATIC_ROW);
            layout.Row.ratio = null;
            layout.Row.filled = (float)(0);
            layout.Row.item_width = (float)(0);
            layout.Row.item_offset = (float)(0);
            layout.Row.columns = (int)(cols);
        }

        public static void nk_layout_row_push(NkContext ctx, float ratio_or_width)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if ((layout.Row.type != NK_LAYOUT_STATIC_ROW) && (layout.Row.type != NK_LAYOUT_DYNAMIC_ROW)) return;
            if ((layout.Row.type) == (NK_LAYOUT_DYNAMIC_ROW))
            {
                float ratio = (float)(ratio_or_width);
                if ((ratio + layout.Row.filled) > (1.0f)) return;
                if ((ratio) > (0.0f))
                    layout.Row.item_width =
                        (float)((0) < ((1.0f) < (ratio) ? (1.0f) : (ratio)) ? ((1.0f) < (ratio) ? (1.0f) : (ratio)) : (0));
                else layout.Row.item_width = (float)(1.0f - layout.Row.filled);
            }
            else layout.Row.item_width = (float)(ratio_or_width);
        }

        public static void nk_layout_row_end(NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if ((layout.Row.type != NK_LAYOUT_STATIC_ROW) && (layout.Row.type != NK_LAYOUT_DYNAMIC_ROW)) return;
            layout.Row.item_width = (float)(0);
            layout.Row.item_offset = (float)(0);
        }

        public static void nk_layout_row(NkContext ctx, int fmt, float height, int cols, float* ratio)
        {
            int i;
            int n_undef = (int)(0);
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            nk_panel_layout(ctx, win, (float)(height), (int)(cols));
            if ((fmt) == (NK_DYNAMIC))
            {
                float r = (float)(0);
                layout.Row.ratio = ratio;
                for (i = (int)(0); (i) < (cols); ++i)
                {
                    if ((ratio[i]) < (0.0f)) n_undef++;
                    else r += (float)(ratio[i]);
                }
                r = (float)((0) < ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) ? ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) : (0));
                layout.Row.type = (int)(NK_LAYOUT_DYNAMIC);
                layout.Row.item_width = (float)((((r) > (0)) && ((n_undef) > (0))) ? (r / (float)(n_undef)) : 0);
            }
            else
            {
                layout.Row.ratio = ratio;
                layout.Row.type = (int)(NK_LAYOUT_STATIC);
                layout.Row.item_width = (float)(0);
                layout.Row.item_offset = (float)(0);
            }

            layout.Row.item_offset = (float)(0);
            layout.Row.filled = (float)(0);
        }

        public static void nk_layout_row_template_begin(NkContext ctx, float height)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            nk_panel_layout(ctx, win, (float)(height), (int)(1));
            layout.Row.type = (int)(NK_LAYOUT_TEMPLATE);
            layout.Row.columns = (int)(0);
            layout.Row.ratio = null;
            layout.Row.item_width = (float)(0);
            layout.Row.item_height = (float)(0);
            layout.Row.item_offset = (float)(0);
            layout.Row.filled = (float)(0);
            layout.Row.item.x = (float)(0);
            layout.Row.item.y = (float)(0);
            layout.Row.item.w = (float)(0);
            layout.Row.item.h = (float)(0);
        }

        public static void nk_layout_row_template_push_dynamic(NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if (layout.Row.type != NK_LAYOUT_TEMPLATE) return;
            if ((layout.Row.columns) >= (16)) return;
            layout.Row.templates[layout.Row.columns++] = (float)(-1.0f);
        }

        public static void nk_layout_row_template_push_variable(NkContext ctx, float min_width)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if (layout.Row.type != NK_LAYOUT_TEMPLATE) return;
            if ((layout.Row.columns) >= (16)) return;
            layout.Row.templates[layout.Row.columns++] = (float)(-min_width);
        }

        public static void nk_layout_row_template_push_static(NkContext ctx, float width)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if (layout.Row.type != NK_LAYOUT_TEMPLATE) return;
            if ((layout.Row.columns) >= (16)) return;
            layout.Row.templates[layout.Row.columns++] = (float)(width);
        }

        public static void nk_layout_row_template_end(NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            int i = (int)(0);
            int variable_count = (int)(0);
            int min_variable_count = (int)(0);
            float min_fixed_width = (float)(0.0f);
            float total_fixed_width = (float)(0.0f);
            float max_variable_width = (float)(0.0f);
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if (layout.Row.type != NK_LAYOUT_TEMPLATE) return;
            for (i = (int)(0); (i) < (layout.Row.columns); ++i)
            {
                float width = (float)(layout.Row.templates[i]);
                if ((width) >= (0.0f))
                {
                    total_fixed_width += (float)(width);
                    min_fixed_width += (float)(width);
                }
                else if ((width) < (-1.0f))
                {
                    width = (float)(-width);
                    total_fixed_width += (float)(width);
                    max_variable_width = (float)((max_variable_width) < (width) ? (width) : (max_variable_width));
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
                        (nk_layout_row_calculate_usable_space(ctx.Style, (int)(layout.Type), (float)(layout.Bounds.w),
                            (int)(layout.Row.columns)));
                float var_width =
                    (float)(((space - min_fixed_width) < (0.0f) ? (0.0f) : (space - min_fixed_width)) / (float)(variable_count));
                int enough_space = (int)((var_width) >= (max_variable_width) ? 1 : 0);
                if (enough_space == 0)
                    var_width =
                        (float)(((space - total_fixed_width) < (0) ? (0) : (space - total_fixed_width)) / (float)(min_variable_count));
                for (i = (int)(0); (i) < (layout.Row.columns); ++i)
                {
                    float* width = (float*)layout.Row.templates + i;
                    *width =
                        (float)(((*width) >= (0.0f)) ? *width : (((*width) < (-1.0f)) && (enough_space == 0)) ? -(*width) : var_width);
                }
            }

        }

        public static void nk_layout_space_begin(NkContext ctx, int fmt, float height, int widget_count)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            nk_panel_layout(ctx, win, (float)(height), (int)(widget_count));
            if ((fmt) == (NK_STATIC)) layout.Row.type = (int)(NK_LAYOUT_STATIC_FREE);
            else layout.Row.type = (int)(NK_LAYOUT_DYNAMIC_FREE);
            layout.Row.ratio = null;
            layout.Row.filled = (float)(0);
            layout.Row.item_width = (float)(0);
            layout.Row.item_offset = (float)(0);
        }

        public static void nk_layout_space_end(NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            layout.Row.item_width = (float)(0);
            layout.Row.item_height = (float)(0);
            layout.Row.item_offset = (float)(0);
            fixed (void* ptr = &layout.Row.item)
            {
                nk_zero(ptr, (ulong)(sizeof(nk_rect)));
            }
        }

        public static void nk_layout_space_push(NkContext ctx, nk_rect rect)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            layout.Row.item = (nk_rect)(rect);
        }

        public static nk_rect nk_layout_space_bounds(NkContext ctx)
        {
            nk_rect ret = new nk_rect();
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x = (float)(layout.Clip.x);
            ret.y = (float)(layout.Clip.y);
            ret.w = (float)(layout.Clip.w);
            ret.h = (float)(layout.Row.height);
            return (nk_rect)(ret);
        }

        public static nk_rect nk_layout_widget_bounds(NkContext ctx)
        {
            nk_rect ret = new nk_rect();
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x = (float)(layout.AtX);
            ret.y = (float)(layout.AtY);
            ret.w = (float)(layout.Bounds.w - ((layout.AtX - layout.Bounds.x) < (0) ? (0) : (layout.AtX - layout.Bounds.x)));
            ret.h = (float)(layout.Row.height);
            return (nk_rect)(ret);
        }

        public static nk_vec2 nk_layout_space_to_screen(NkContext ctx, nk_vec2 ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(layout.AtX - (float)(layout.Offset.x));
            ret.y += (float)(layout.AtY - (float)(layout.Offset.y));
            return (nk_vec2)(ret);
        }

        public static nk_vec2 nk_layout_space_to_local(NkContext ctx, nk_vec2 ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(-layout.AtX + (float)(layout.Offset.x));
            ret.y += (float)(-layout.AtY + (float)(layout.Offset.y));
            return (nk_vec2)(ret);
        }

        public static nk_rect nk_layout_space_rect_to_screen(NkContext ctx, nk_rect ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(layout.AtX - (float)(layout.Offset.x));
            ret.y += (float)(layout.AtY - (float)(layout.Offset.y));
            return (nk_rect)(ret);
        }

        public static nk_rect nk_layout_space_rect_to_local(NkContext ctx, nk_rect ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(-layout.AtX + (float)(layout.Offset.x));
            ret.y += (float)(-layout.AtY + (float)(layout.Offset.y));
            return (nk_rect)(ret);
        }

        public static void nk_panel_alloc_row(NkContext ctx, NkWindow win)
        {
            NkPanel layout = win.Layout;
            nk_vec2 spacing = (nk_vec2)(ctx.Style.Window.spacing);
            float row_height = (float)(layout.Row.height - spacing.y);
            nk_panel_layout(ctx, win, (float)(row_height), (int)(layout.Row.columns));
        }

        public static int nk_tree_state_base(NkContext ctx, int type, nk_image img, char* title, ref int state)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle style;
            NkCommandBuffer _out_;
            nk_input _in_;
            nk_style_button button;
            int symbol;
            float row_height;
            nk_vec2 item_spacing = new nk_vec2();
            nk_rect header = new nk_rect();
            nk_rect sym = new nk_rect();
            nk_text text = new nk_text();
            uint ws = (uint)(0);
            int widget_state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            _out_ = win.Buffer;
            style = ctx.Style;
            item_spacing = (nk_vec2)(style.Window.spacing);
            row_height = (float)(style.Font.Height + 2 * style.Tab.padding.y);
            nk_layout_set_min_row_height(ctx, (float)(row_height));
            nk_layout_row_dynamic(ctx, (float)(row_height), (int)(1));
            nk_layout_reset_min_row_height(ctx);
            widget_state = (int)(nk_widget(&header, ctx));
            if ((type) == (NK_TREE_TAB))
            {
                NkStyleItem background = style.Tab.background;
                if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
                {
                    nk_draw_image(_out_, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
                    text.background = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                }
                else
                {
                    text.background = (nk_color)(background.Data.Color);
                    nk_fill_rect(_out_, (nk_rect)(header), (float)(0), (nk_color)(style.Tab.border_color));
                    nk_fill_rect(_out_, (nk_rect)(nk_shrink_rect_((nk_rect)(header), (float)(style.Tab.border))),
                        (float)(style.Tab.rounding), (nk_color)(background.Data.Color));
                }
            }
            else text.background = (nk_color)(style.Window.background);
            _in_ = ((layout.Flags & NK_WINDOW_ROM) == 0) ? ctx.Input : null;
            _in_ = (((_in_) != null) && ((widget_state) == (NK_WIDGET_VALID))) ? ctx.Input : null;
            if ((nk_button_behavior(ref ws, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                state = (int)(((state) == (NK_MAXIMIZED)) ? NK_MINIMIZED : NK_MAXIMIZED);
            if ((state) == (NK_MAXIMIZED))
            {
                symbol = (int)(style.Tab.sym_maximize);
                if ((type) == (NK_TREE_TAB)) button = style.Tab.tab_maximize_button;
                else button = style.Tab.node_maximize_button;
            }
            else
            {
                symbol = (int)(style.Tab.sym_minimize);
                if ((type) == (NK_TREE_TAB)) button = style.Tab.tab_minimize_button;
                else button = style.Tab.node_minimize_button;
            }

            {
                sym.w = (float)(sym.h = (float)(style.Font.Height));
                sym.y = (float)(header.y + style.Tab.padding.y);
                sym.x = (float)(header.x + style.Tab.padding.x);
                nk_do_button_symbol(ref ws, win.Buffer, (nk_rect)(sym), (int)(symbol), (int)(NK_BUTTON_DEFAULT), button, null,
                    style.Font);
                if ((img) != null)
                {
                    sym.x = (float)(sym.x + sym.w + 4 * item_spacing.x);
                    nk_draw_image(win.Buffer, (nk_rect)(sym), img, (nk_color)(nk_white));
                    sym.w = (float)(style.Font.Height + style.Tab.spacing.x);
                }
            }

            {
                nk_rect label = new nk_rect();
                header.w = (float)((header.w) < (sym.w + item_spacing.x) ? (sym.w + item_spacing.x) : (header.w));
                label.x = (float)(sym.x + sym.w + item_spacing.x);
                label.y = (float)(sym.y);
                label.w = (float)(header.w - (sym.w + item_spacing.y + style.Tab.indent));
                label.h = (float)(style.Font.Height);
                text.text = (nk_color)(style.Tab.text);
                text.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
                nk_widget_text(_out_, (nk_rect)(label), title, (int)(nk_strlen(title)), &text, (uint)(NK_TEXT_LEFT), style.Font);
            }

            if ((state) == (NK_MAXIMIZED))
            {
                layout.AtX = (float)(header.x + (float)(layout.Offset.x) + style.Tab.indent);
                layout.Bounds.w = (float)((layout.Bounds.w) < (style.Tab.indent) ? (style.Tab.indent) : (layout.Bounds.w));
                layout.Bounds.w -= (float)(style.Tab.indent + style.Window.padding.x);
                layout.Row.tree_depth++;
                return (int)(nk_true);
            }
            else return (int)(nk_false);
        }

        public static int nk_tree_base(NkContext ctx, int type, nk_image img, char* title, int initial_state, char* hash,
            int len, int line)
        {
            NkWindow win = ctx.Current;
            int title_len = (int)(0);
            uint tree_hash = (uint)(0);
            uint* state = null;
            if (hash == null)
            {
                title_len = (int)(nk_strlen(title));
                tree_hash = (uint)(nk_murmur_hash(title, (int)(title_len), (uint)(line)));
            }
            else tree_hash = (uint)(nk_murmur_hash(hash, (int)(len), (uint)(line)));
            state = nk_find_value(win, (uint)(tree_hash));
            if (state == null)
            {
                state = nk_add_value(ctx, win, (uint)(tree_hash), (uint)(0));
                *state = (uint)(initial_state);
            }

            int kkk = (int)(*state);
            int result = (int)(nk_tree_state_base(ctx, (int)(type), img, title, ref kkk));
            *state = (uint)kkk;
            return result;
        }

        public static int nk_tree_state_push(NkContext ctx, int type, char* title, ref int state)
        {
            return (int)(nk_tree_state_base(ctx, (int)(type), null, title, ref state));
        }

        public static int nk_tree_state_image_push(NkContext ctx, int type, nk_image img, char* title, ref int state)
        {
            return (int)(nk_tree_state_base(ctx, (int)(type), img, title, ref state));
        }

        public static void nk_tree_state_pop(NkContext ctx)
        {
            NkWindow win = null;
            NkPanel layout = null;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            layout.AtX -= (float)(ctx.Style.Tab.indent + ctx.Style.Window.padding.x);
            layout.Bounds.w += (float)(ctx.Style.Tab.indent + ctx.Style.Window.padding.x);
            layout.Row.tree_depth--;
        }

        public static int nk_tree_push_hashed(NkContext ctx, int type, char* title, int initial_state, char* hash, int len,
            int line)
        {
            return (int)(nk_tree_base(ctx, (int)(type), null, title, (int)(initial_state), hash, (int)(len), (int)(line)));
        }

        public static int nk_tree_image_push_hashed(NkContext ctx, int type, nk_image img, char* title, int initial_state,
            char* hash, int len, int seed)
        {
            return (int)(nk_tree_base(ctx, (int)(type), img, title, (int)(initial_state), hash, (int)(len), (int)(seed)));
        }

        public static void nk_tree_pop(NkContext ctx)
        {
            nk_tree_state_pop(ctx);
        }

        public static nk_rect nk_widget_bounds(NkContext ctx)
        {
            nk_rect bounds = new nk_rect();
            if ((ctx == null) || (ctx.Current == null))
                return (nk_rect)(nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            nk_layout_peek(&bounds, ctx);
            return (nk_rect)(bounds);
        }

        public static nk_vec2 nk_widget_position(NkContext ctx)
        {
            nk_rect bounds = new nk_rect();
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            nk_layout_peek(&bounds, ctx);
            return (nk_vec2)(nk_vec2_((float)(bounds.x), (float)(bounds.y)));
        }

        public static nk_vec2 nk_widget_size(NkContext ctx)
        {
            nk_rect bounds = new nk_rect();
            if ((ctx == null) || (ctx.Current == null)) return (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
            nk_layout_peek(&bounds, ctx);
            return (nk_vec2)(nk_vec2_((float)(bounds.w), (float)(bounds.h)));
        }

        public static float nk_widget_width(NkContext ctx)
        {
            nk_rect bounds = new nk_rect();
            if ((ctx == null) || (ctx.Current == null)) return (float)(0);
            nk_layout_peek(&bounds, ctx);
            return (float)(bounds.w);
        }

        public static float nk_widget_height(NkContext ctx)
        {
            nk_rect bounds = new nk_rect();
            if ((ctx == null) || (ctx.Current == null)) return (float)(0);
            nk_layout_peek(&bounds, ctx);
            return (float)(bounds.h);
        }

        public static int nk_widget_is_hovered(NkContext ctx)
        {
            nk_rect c = new nk_rect();
            nk_rect v = new nk_rect();
            nk_rect bounds = new nk_rect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Active != ctx.Current)) return (int)(0);
            c = (nk_rect)(ctx.Current.Layout.Clip);
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            nk_layout_peek(&bounds, ctx);
            nk_unify(ref v, ref c, (float)(bounds.x), (float)(bounds.y), (float)(bounds.x + bounds.w),
                (float)(bounds.y + bounds.h));
            if (
                !(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
                    ((bounds.y + bounds.h) < (c.y))))) return (int)(0);
            return (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(bounds)));
        }

        public static int nk_widget_is_mouse_clicked(NkContext ctx, int btn)
        {
            nk_rect c = new nk_rect();
            nk_rect v = new nk_rect();
            nk_rect bounds = new nk_rect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Active != ctx.Current)) return (int)(0);
            c = (nk_rect)(ctx.Current.Layout.Clip);
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            nk_layout_peek(&bounds, ctx);
            nk_unify(ref v, ref c, (float)(bounds.x), (float)(bounds.y), (float)(bounds.x + bounds.w),
                (float)(bounds.y + bounds.h));
            if (
                !(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
                    ((bounds.y + bounds.h) < (c.y))))) return (int)(0);
            return (int)(nk_input_mouse_clicked(ctx.Input, (int)(btn), (nk_rect)(bounds)));
        }

        public static int nk_widget_has_mouse_click_down(NkContext ctx, int btn, int down)
        {
            nk_rect c = new nk_rect();
            nk_rect v = new nk_rect();
            nk_rect bounds = new nk_rect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Active != ctx.Current)) return (int)(0);
            c = (nk_rect)(ctx.Current.Layout.Clip);
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            nk_layout_peek(&bounds, ctx);
            nk_unify(ref v, ref c, (float)(bounds.x), (float)(bounds.y), (float)(bounds.x + bounds.w),
                (float)(bounds.y + bounds.h));
            if (
                !(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
                    ((bounds.y + bounds.h) < (c.y))))) return (int)(0);
            return (int)(nk_input_has_mouse_click_down_in_rect(ctx.Input, (int)(btn), (nk_rect)(bounds), (int)(down)));
        }

        public static void nk_spacing(NkContext ctx, int cols)
        {
            NkWindow win;
            NkPanel layout;
            nk_rect none = new nk_rect();
            int i;
            int index;
            int rows;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            index = (int)((layout.Row.index + cols) % layout.Row.columns);
            rows = (int)((layout.Row.index + cols) / layout.Row.columns);
            if ((rows) != 0)
            {
                for (i = (int)(0); (i) < (rows); ++i)
                {
                    nk_panel_alloc_row(ctx, win);
                }
                cols = (int)(index);
            }

            if ((layout.Row.type != NK_LAYOUT_DYNAMIC_FIXED) && (layout.Row.type != NK_LAYOUT_STATIC_FIXED))
            {
                for (i = (int)(0); (i) < (cols); ++i)
                {
                    nk_panel_alloc_space(&none, ctx);
                }
            }

            layout.Row.index = (int)(index);
        }

        public static void nk_text_colored(NkContext ctx, char* str, int len, uint alignment, nk_color color)
        {
            NkWindow win;
            NkStyle style;
            nk_vec2 item_padding = new nk_vec2();
            nk_rect bounds = new nk_rect();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            style = ctx.Style;
            nk_panel_alloc_space(&bounds, ctx);
            item_padding = (nk_vec2)(style.Text.padding);
            text.padding.x = (float)(item_padding.x);
            text.padding.y = (float)(item_padding.y);
            text.background = (nk_color)(style.Window.background);
            text.text = (nk_color)(color);
            nk_widget_text(win.Buffer, (nk_rect)(bounds), str, (int)(len), &text, (uint)(alignment), style.Font);
        }

        public static void nk_text_wrap_colored(NkContext ctx, char* str, int len, nk_color color)
        {
            NkWindow win;
            NkStyle style;
            nk_vec2 item_padding = new nk_vec2();
            nk_rect bounds = new nk_rect();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            style = ctx.Style;
            nk_panel_alloc_space(&bounds, ctx);
            item_padding = (nk_vec2)(style.Text.padding);
            text.padding.x = (float)(item_padding.x);
            text.padding.y = (float)(item_padding.y);
            text.background = (nk_color)(style.Window.background);
            text.text = (nk_color)(color);
            nk_widget_text_wrap(win.Buffer, (nk_rect)(bounds), str, (int)(len), &text, style.Font);
        }

        public static void nk_text_(NkContext ctx, char* str, int len, uint alignment)
        {
            if (ctx == null) return;
            nk_text_colored(ctx, str, (int)(len), (uint)(alignment), (nk_color)(ctx.Style.Text.color));
        }

        public static void nk_text_wrap(NkContext ctx, char* str, int len)
        {
            if (ctx == null) return;
            nk_text_wrap_colored(ctx, str, (int)(len), (nk_color)(ctx.Style.Text.color));
        }

        public static void nk_label(NkContext ctx, char* str, uint alignment)
        {
            nk_text_(ctx, str, (int)(nk_strlen(str)), (uint)(alignment));
        }

        public static void nk_label_colored(NkContext ctx, char* str, uint align, nk_color color)
        {
            nk_text_colored(ctx, str, (int)(nk_strlen(str)), (uint)(align), (nk_color)(color));
        }

        public static void nk_label_wrap(NkContext ctx, char* str)
        {
            nk_text_wrap(ctx, str, (int)(nk_strlen(str)));
        }

        public static void nk_label_colored_wrap(NkContext ctx, char* str, nk_color color)
        {
            nk_text_wrap_colored(ctx, str, (int)(nk_strlen(str)), (nk_color)(color));
        }

        public static void nk_image_(NkContext ctx, nk_image img)
        {
            NkWindow win;
            nk_rect bounds = new nk_rect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            if (nk_widget(&bounds, ctx) == 0) return;
            nk_draw_image(win.Buffer, (nk_rect)(bounds), img, (nk_color)(nk_white));
        }

        public static void nk_button_set_behavior(NkContext ctx, int behavior)
        {
            if (ctx == null) return;
            ctx.ButtonBehavior = (int)(behavior);
        }

        public static int nk_button_push_behavior(NkContext ctx, int behavior)
        {
            nk_config_stack_button_behavior button_stack;
            NkConfigStackButtonBehaviorElement element;
            if (ctx == null) return (int)(0);
            button_stack = ctx.Stacks.button_behaviors;
            if ((button_stack.head) >= ((int)((int)button_stack.elements.Length))) return (int)(0);
            element = button_stack.elements[button_stack.head++];
            element.old_value = (int)(ctx.ButtonBehavior);
            ctx.ButtonBehavior = (int)(behavior);
            return (int)(1);
        }

        public static int nk_button_pop_behavior(NkContext ctx)
        {
            nk_config_stack_button_behavior button_stack;
            NkConfigStackButtonBehaviorElement element;
            if (ctx == null) return (int)(0);
            button_stack = ctx.Stacks.button_behaviors;
            if ((button_stack.head) < (1)) return (int)(0);
            element = button_stack.elements[--button_stack.head];
            ctx.ButtonBehavior = element.old_value;
            return (int)(1);
        }

        public static int nk_button_text_styled(NkContext ctx, nk_style_button style, char* title, int len)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int state;
            if ((((style == null) || (ctx == null)) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), title, (int)(len),
                        (uint)(style.text_alignment), (int)(ctx.ButtonBehavior), style, _in_, ctx.Style.Font));
        }

        public static int nk_button_text(NkContext ctx, char* title, int len)
        {
            if (ctx == null) return (int)(0);
            return (int)(nk_button_text_styled(ctx, ctx.Style.Button, title, (int)(len)));
        }

        public static int nk_button_label_styled(NkContext ctx, nk_style_button style, char* title)
        {
            return (int)(nk_button_text_styled(ctx, style, title, (int)(nk_strlen(title))));
        }

        public static int nk_button_label(NkContext ctx, char* title)
        {
            return (int)(nk_button_text(ctx, title, (int)(nk_strlen(title))));
        }

        public static int nk_button_color(NkContext ctx, nk_color color)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_style_button button = new nk_style_button();
            int ret = (int)(0);
            nk_rect bounds = new nk_rect();
            nk_rect content = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            button = (nk_style_button)(ctx.Style.Button);
            button.normal = (NkStyleItem)(nk_style_item_color((nk_color)(color)));
            button.hover = (NkStyleItem)(nk_style_item_color((nk_color)(color)));
            button.active = (NkStyleItem)(nk_style_item_color((nk_color)(color)));
            ret =
                (int)
                    (nk_do_button(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), button, _in_, (int)(ctx.ButtonBehavior),
                        &content));
            nk_draw_button(win.Buffer, &bounds, (uint)(ctx.LastWidgetState), button);
            return (int)(ret);
        }

        public static int nk_button_symbol_styled(NkContext ctx, nk_style_button style, int symbol)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_symbol(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (int)(symbol),
                        (int)(ctx.ButtonBehavior), style, _in_, ctx.Style.Font));
        }

        public static int nk_button_symbol(NkContext ctx, int symbol)
        {
            if (ctx == null) return (int)(0);
            return (int)(nk_button_symbol_styled(ctx, ctx.Style.Button, (int)(symbol)));
        }

        public static int nk_button_image_styled(NkContext ctx, nk_style_button style, nk_image img)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_image(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (nk_image)(img),
                        (int)(ctx.ButtonBehavior), style, _in_));
        }

        public static int nk_button_image(NkContext ctx, nk_image img)
        {
            if (ctx == null) return (int)(0);
            return (int)(nk_button_image_styled(ctx, ctx.Style.Button, (nk_image)(img)));
        }

        public static int nk_button_symbol_text_styled(NkContext ctx, nk_style_button style, int symbol, char* text, int len,
            uint align)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (int)(symbol), text,
                        (int)(len), (uint)(align), (int)(ctx.ButtonBehavior), style, ctx.Style.Font, _in_));
        }

        public static int nk_button_symbol_text(NkContext ctx, int symbol, char* text, int len, uint align)
        {
            if (ctx == null) return (int)(0);
            return (int)(nk_button_symbol_text_styled(ctx, ctx.Style.Button, (int)(symbol), text, (int)(len), (uint)(align)));
        }

        public static int nk_button_symbol_label(NkContext ctx, int symbol, char* label, uint align)
        {
            return (int)(nk_button_symbol_text(ctx, (int)(symbol), label, (int)(nk_strlen(label)), (uint)(align)));
        }

        public static int nk_button_symbol_label_styled(NkContext ctx, nk_style_button style, int symbol, char* title,
            uint align)
        {
            return
                (int)(nk_button_symbol_text_styled(ctx, style, (int)(symbol), title, (int)(nk_strlen(title)), (uint)(align)));
        }

        public static int nk_button_image_text_styled(NkContext ctx, nk_style_button style, nk_image img, char* text, int len,
            uint align)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (nk_image)(img), text,
                        (int)(len), (uint)(align), (int)(ctx.ButtonBehavior), style, ctx.Style.Font, _in_));
        }

        public static int nk_button_image_text(NkContext ctx, nk_image img, char* text, int len, uint align)
        {
            return
                (int)(nk_button_image_text_styled(ctx, ctx.Style.Button, (nk_image)(img), text, (int)(len), (uint)(align)));
        }

        public static int nk_button_image_label(NkContext ctx, nk_image img, char* label, uint align)
        {
            return (int)(nk_button_image_text(ctx, (nk_image)(img), label, (int)(nk_strlen(label)), (uint)(align)));
        }

        public static int nk_button_image_label_styled(NkContext ctx, nk_style_button style, nk_image img, char* label,
            uint text_alignment)
        {
            return
                (int)
                    (nk_button_image_text_styled(ctx, style, (nk_image)(img), label, (int)(nk_strlen(label)), (uint)(text_alignment)));
        }

        public static int nk_selectable_text(NkContext ctx, char* str, int len, uint align, ref int value)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            int state;
            nk_rect bounds = new nk_rect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null))) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_selectable(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), str, (int)(len), (uint)(align),
                        ref value, style.Selectable, _in_, style.Font));
        }

        public static int nk_selectable_image_text(NkContext ctx, nk_image img, char* str, int len, uint align, ref int value)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            int state;
            nk_rect bounds = new nk_rect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null))) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_selectable_image(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), str, (int)(len), (uint)(align),
                        ref value, img, style.Selectable, _in_, style.Font));
        }

        public static int nk_select_text(NkContext ctx, char* str, int len, uint align, int value)
        {
            nk_selectable_text(ctx, str, (int)(len), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_selectable_label(NkContext ctx, char* str, uint align, ref int value)
        {
            return (int)(nk_selectable_text(ctx, str, (int)(nk_strlen(str)), (uint)(align), ref value));
        }

        public static int nk_selectable_image_label(NkContext ctx, nk_image img, char* str, uint align, ref int value)
        {
            return
                (int)(nk_selectable_image_text(ctx, (nk_image)(img), str, (int)(nk_strlen(str)), (uint)(align), ref value));
        }

        public static int nk_select_label(NkContext ctx, char* str, uint align, int value)
        {
            nk_selectable_text(ctx, str, (int)(nk_strlen(str)), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_select_image_label(NkContext ctx, nk_image img, char* str, uint align, int value)
        {
            nk_selectable_image_text(ctx, (nk_image)(img), str, (int)(nk_strlen(str)), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_select_image_text(NkContext ctx, nk_image img, char* str, int len, uint align, int value)
        {
            nk_selectable_image_text(ctx, (nk_image)(img), str, (int)(len), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_check_text(NkContext ctx, char* text, int len, int active)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(active);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(active);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            nk_do_toggle(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), &active, text, (int)(len),
                (int)(NK_TOGGLE_CHECK), style.Checkbox, _in_, style.Font);
            return (int)(active);
        }

        public static uint nk_check_flags_text(NkContext ctx, char* text, int len, uint flags, uint value)
        {
            int old_active;
            if ((ctx == null) || (text == null)) return (uint)(flags);
            old_active = ((int)((flags & value) & value));
            if ((nk_check_text(ctx, text, (int)(len), (int)(old_active))) != 0) flags |= (uint)(value);
            else flags &= (uint)(~value);
            return (uint)(flags);
        }

        public static int nk_checkbox_text(NkContext ctx, char* text, int len, int* active)
        {
            int old_val;
            if (((ctx == null) || (text == null)) || (active == null)) return (int)(0);
            old_val = (int)(*active);
            *active = (int)(nk_check_text(ctx, text, (int)(len), (int)(*active)));
            return (old_val != *active) ? 1 : 0;
        }

        public static int nk_checkbox_flags_text(NkContext ctx, char* text, int len, uint* flags, uint value)
        {
            int active;
            if (((ctx == null) || (text == null)) || (flags == null)) return (int)(0);
            active = ((int)((*flags & value) & value));
            if ((nk_checkbox_text(ctx, text, (int)(len), &active)) != 0)
            {
                if ((active) != 0) *flags |= (uint)(value);
                else *flags &= (uint)(~value);
                return (int)(1);
            }

            return (int)(0);
        }

        public static int nk_check_label(NkContext ctx, char* label, int active)
        {
            return (int)(nk_check_text(ctx, label, (int)(nk_strlen(label)), (int)(active)));
        }

        public static uint nk_check_flags_label(NkContext ctx, char* label, uint flags, uint value)
        {
            return (uint)(nk_check_flags_text(ctx, label, (int)(nk_strlen(label)), (uint)(flags), (uint)(value)));
        }

        public static int nk_checkbox_label(NkContext ctx, char* label, int* active)
        {
            return (int)(nk_checkbox_text(ctx, label, (int)(nk_strlen(label)), active));
        }

        public static int nk_checkbox_flags_label(NkContext ctx, char* label, uint* flags, uint value)
        {
            return (int)(nk_checkbox_flags_text(ctx, label, (int)(nk_strlen(label)), flags, (uint)(value)));
        }

        public static int nk_option_text(NkContext ctx, char* text, int len, int is_active)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(is_active);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(state);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            nk_do_toggle(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), &is_active, text, (int)(len),
                (int)(NK_TOGGLE_OPTION), style.Option, _in_, style.Font);
            return (int)(is_active);
        }

        public static int nk_radio_text(NkContext ctx, char* text, int len, int* active)
        {
            int old_value;
            if (((ctx == null) || (text == null)) || (active == null)) return (int)(0);
            old_value = (int)(*active);
            *active = (int)(nk_option_text(ctx, text, (int)(len), (int)(old_value)));
            return (old_value != *active) ? 1 : 0;
        }

        public static int nk_option_label(NkContext ctx, char* label, int active)
        {
            return (int)(nk_option_text(ctx, label, (int)(nk_strlen(label)), (int)(active)));
        }

        public static int nk_radio_label(NkContext ctx, char* label, int* active)
        {
            return (int)(nk_radio_text(ctx, label, (int)(nk_strlen(label)), active));
        }

        public static int nk_slider_float(NkContext ctx, float min_value, ref float value, float max_value, float value_step)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            int ret = (int)(0);
            float old_value;
            nk_rect bounds = new nk_rect();
            int state;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)))
                return (int)(ret);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(ret);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            old_value = (float)(value);
            value =
                (float)
                    (nk_do_slider(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (float)(min_value), (float)(old_value),
                        (float)(max_value), (float)(value_step), style.Slider, _in_, style.Font));
            return (((old_value) > (value)) || ((old_value) < (value))) ? 1 : 0;
        }

        public static float nk_slide_float(NkContext ctx, float min, float val, float max, float step)
        {
            nk_slider_float(ctx, (float)(min), ref val, (float)(max), (float)(step));
            return (float)(val);
        }

        public static int nk_slide_int(NkContext ctx, int min, int val, int max, int step)
        {
            float value = (float)(val);
            nk_slider_float(ctx, (float)(min), ref value, (float)(max), (float)(step));
            return (int)(value);
        }

        public static int nk_slider_int(NkContext ctx, int min, ref int val, int max, int step)
        {
            int ret;
            float value = (float)(val);
            ret = (int)(nk_slider_float(ctx, (float)(min), ref value, (float)(max), (float)(step)));
            val = ((int)(value));
            return (int)(ret);
        }

        public static int nk_progress(NkContext ctx, ulong* cur, ulong max, int is_modifyable)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle style;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int state;
            ulong old_value;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (cur == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            old_value = (ulong)(*cur);
            *cur =
                (ulong)
                    (nk_do_progress(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (ulong)(*cur), (ulong)(max),
                        (int)(is_modifyable), style.Progress, _in_));
            return (*cur != old_value) ? 1 : 0;
        }

        public static ulong nk_prog(NkContext ctx, ulong cur, ulong max, int modifyable)
        {
            nk_progress(ctx, &cur, (ulong)(max), (int)(modifyable));
            return (ulong)(cur);
        }

        public static void nk_edit_focus(NkContext ctx, uint flags)
        {
            uint hash;
            NkWindow win;
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            hash = (uint)(win.Edit.seq);
            win.Edit.active = (int)(nk_true);
            win.Edit.name = (uint)(hash);
            if ((flags & NK_EDIT_ALWAYS_INSERT_MODE) != 0) win.Edit.mode = (byte)(NK_TEXT_EDIT_MODE_INSERT);
        }

        public static void nk_edit_unfocus(NkContext ctx)
        {
            NkWindow win;
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            win.Edit.active = (int)(nk_false);
            win.Edit.name = (uint)(0);
        }

        public static uint nk_edit_string(NkContext ctx, uint flags, NkStr str, int max,
            NkPluginFilter filter)
        {
            uint hash;
            uint state;
            nk_text_edit edit;
            NkWindow win;
            if (((ctx == null))) return (uint)(0);
            filter = (filter == null) ? nk_filter_default : filter;
            win = ctx.Current;
            hash = (uint)(win.Edit.seq);
            edit = ctx.TextEdit;
            nk_textedit_clear_state(ctx.TextEdit,
                (int)((flags & NK_EDIT_MULTILINE) != 0 ? NK_TEXT_EDIT_MULTI_LINE : NK_TEXT_EDIT_SINGLE_LINE), filter);
            if (((win.Edit.active) != 0) && ((hash) == (win.Edit.name)))
            {
                if ((flags & NK_EDIT_NO_CURSOR) != 0) edit.cursor = (int)(str.Len);
                else edit.cursor = (int)(win.Edit.cursor);
                if ((flags & NK_EDIT_SELECTABLE) == 0)
                {
                    edit.select_start = (int)(win.Edit.cursor);
                    edit.select_end = (int)(win.Edit.cursor);
                }
                else
                {
                    edit.select_start = (int)(win.Edit.sel_start);
                    edit.select_end = (int)(win.Edit.sel_end);
                }
                edit.mode = (byte)(win.Edit.mode);
                edit.scrollbar.x = ((float)(win.Edit.scrollbar.x));
                edit.scrollbar.y = ((float)(win.Edit.scrollbar.y));
                edit.active = (byte)(nk_true);
            }
            else edit.active = (byte)(nk_false);
            max = (int)((1) < (max) ? (max) : (1));

            if (str.Len > max)
            {
                str.Str = str.Str.Substring(0, max);
            }

            edit._string_ = str;
            state = (uint)(nk_edit_buffer(ctx, (uint)(flags), edit, filter));
            if ((edit.active) != 0)
            {
                win.Edit.cursor = (int)(edit.cursor);
                win.Edit.sel_start = (int)(edit.select_start);
                win.Edit.sel_end = (int)(edit.select_end);
                win.Edit.mode = (byte)(edit.mode);
                win.Edit.scrollbar.x = ((uint)(edit.scrollbar.x));
                win.Edit.scrollbar.y = ((uint)(edit.scrollbar.y));
            }

            return (uint)(state);
        }

        public static uint nk_edit_buffer(NkContext ctx, uint flags, nk_text_edit edit, NkPluginFilter filter)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            int state;
            nk_rect bounds = new nk_rect();
            uint ret_flags = (uint)(0);
            byte prev_state;
            uint hash;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (uint)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (uint)(state);
            _in_ = (win.Layout.Flags & NK_WINDOW_ROM) != 0 ? null : ctx.Input;
            hash = (uint)(win.Edit.seq++);
            if (((win.Edit.active) != 0) && ((hash) == (win.Edit.name)))
            {
                if ((flags & NK_EDIT_NO_CURSOR) != 0) edit.cursor = (int)(edit._string_.Len);
                if ((flags & NK_EDIT_SELECTABLE) == 0)
                {
                    edit.select_start = (int)(edit.cursor);
                    edit.select_end = (int)(edit.cursor);
                }
                if ((flags & NK_EDIT_CLIPBOARD) != 0) edit.clip = (NkClipboard)(ctx.Clip);
                edit.active = ((byte)(win.Edit.active));
            }
            else edit.active = (byte)(nk_false);
            edit.mode = (byte)(win.Edit.mode);
            filter = (filter == null) ? nk_filter_default : filter;
            prev_state = (byte)(edit.active);
            _in_ = (flags & NK_EDIT_READ_ONLY) != 0 ? null : _in_;
            ret_flags =
                (uint)
                    (nk_do_edit(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (uint)(flags), filter, edit, style.Edit,
                        _in_, style.Font));
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
                ctx.Style.CursorActive = ctx.Style.Cursors[NK_CURSOR_TEXT];
            if (((edit.active) != 0) && (prev_state != edit.active))
            {
                win.Edit.active = (int)(nk_true);
                win.Edit.name = (uint)(hash);
            }
            else if (((prev_state) != 0) && (edit.active == 0))
            {
                win.Edit.active = (int)(nk_false);
            }

            return (uint)(ret_flags);
        }

        public static void nk_property_int(NkContext ctx, char* name, int min, ref int val, int max, int step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if ((((ctx == null) || (ctx.Current == null)) || (name == null))) return;
            variant = (NkPropertyVariant)(nk_property_variant_int((int)(val), (int)(min), (int)(max), (int)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (int)(NK_FILTER_INT));
            val = (int)(variant.value.i);
        }

        public static void nk_property_float(NkContext ctx, char* name, float min, ref float val, float max, float step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if ((((ctx == null) || (ctx.Current == null)) || (name == null))) return;
            variant =
                (NkPropertyVariant)(nk_property_variant_float((float)(val), (float)(min), (float)(max), (float)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (int)(NK_FILTER_FLOAT));
            val = (float)(variant.value.f);
        }

        public static void nk_property_double(NkContext ctx, char* name, double min, ref double val, double max, double step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if ((((ctx == null) || (ctx.Current == null)) || (name == null))) return;
            variant =
                (NkPropertyVariant)(nk_property_variant_double((double)(val), (double)(min), (double)(max), (double)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (int)(NK_FILTER_FLOAT));
            val = (double)(variant.value.d);
        }

        public static int nk_propertyi(NkContext ctx, char* name, int min, int val, int max, int step, float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if (((ctx == null) || (ctx.Current == null)) || (name == null)) return (int)(val);
            variant = (NkPropertyVariant)(nk_property_variant_int((int)(val), (int)(min), (int)(max), (int)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (int)(NK_FILTER_INT));
            val = (int)(variant.value.i);
            return (int)(val);
        }

        public static float nk_propertyf(NkContext ctx, char* name, float min, float val, float max, float step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if (((ctx == null) || (ctx.Current == null)) || (name == null)) return (float)(val);
            variant =
                (NkPropertyVariant)(nk_property_variant_float((float)(val), (float)(min), (float)(max), (float)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (int)(NK_FILTER_FLOAT));
            val = (float)(variant.value.f);
            return (float)(val);
        }

        public static double nk_propertyd(NkContext ctx, char* name, double min, double val, double max, double step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if (((ctx == null) || (ctx.Current == null)) || (name == null)) return (double)(val);
            variant =
                (NkPropertyVariant)(nk_property_variant_double((double)(val), (double)(min), (double)(max), (double)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (int)(NK_FILTER_FLOAT));
            val = (double)(variant.value.d);
            return (double)(val);
        }

        public static int nk_color_pick(NkContext ctx, nk_colorf* color, int fmt)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle config;
            nk_input _in_;
            int state;
            nk_rect bounds = new nk_rect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (color == null)) return (int)(0);
            win = ctx.Current;
            config = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_color_picker(ref ctx.LastWidgetState, win.Buffer, color, (int)(fmt), (nk_rect)(bounds),
                        (nk_vec2)(nk_vec2_((float)(0), (float)(0))), _in_, config.Font));
        }

        public static nk_colorf nk_color_picker(NkContext ctx, nk_colorf color, int fmt)
        {
            nk_color_pick(ctx, &color, (int)(fmt));
            return (nk_colorf)(color);
        }

        public static int nk_chart_begin_colored(NkContext ctx, int type, nk_color color, nk_color highlight, int count,
            float min_value, float max_value)
        {
            NkWindow win;
            NkChart chart;
            NkStyle config;
            nk_style_chart style;
            NkStyleItem background;
            nk_rect bounds = new nk_rect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            if (nk_widget(&bounds, ctx) == 0)
            {
                chart = ctx.Current.Layout.Chart;
                return (int)(0);
            }

            win = ctx.Current;
            config = ctx.Style;
            chart = win.Layout.Chart;
            style = config.Chart;

            chart.X = (float)(bounds.x + style.padding.x);
            chart.Y = (float)(bounds.y + style.padding.y);
            chart.W = (float)(bounds.w - 2 * style.padding.x);
            chart.H = (float)(bounds.h - 2 * style.padding.y);
            chart.W = (float)((chart.W) < (2 * style.padding.x) ? (2 * style.padding.x) : (chart.W));
            chart.H = (float)((chart.H) < (2 * style.padding.y) ? (2 * style.padding.y) : (chart.H));
            {
                nk_chart_slot slot = chart.Slots[chart.Slot++];
                slot.type = (int)(type);
                slot.count = (int)(count);
                slot.color = (nk_color)(color);
                slot.highlight = (nk_color)(highlight);
                slot.min = (float)((min_value) < (max_value) ? (min_value) : (max_value));
                slot.max = (float)((min_value) < (max_value) ? (max_value) : (min_value));
                slot.range = (float)(slot.max - slot.min);
            }

            background = style.background;
            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                nk_draw_image(win.Buffer, (nk_rect)(bounds), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                nk_fill_rect(win.Buffer, (nk_rect)(bounds), (float)(style.rounding), (nk_color)(style.border_color));
                nk_fill_rect(win.Buffer, (nk_rect)(nk_shrink_rect_((nk_rect)(bounds), (float)(style.border))),
                    (float)(style.rounding), (nk_color)(style.background.Data.Color));
            }

            return (int)(1);
        }

        public static int nk_chart_begin(NkContext ctx, int type, int count, float min_value, float max_value)
        {
            return
                (int)
                    (nk_chart_begin_colored(ctx, (int)(type), (nk_color)(ctx.Style.Chart.color),
                        (nk_color)(ctx.Style.Chart.selected_color), (int)(count), (float)(min_value), (float)(max_value)));
        }

        public static void nk_chart_add_slot_colored(NkContext ctx, int type, nk_color color, nk_color highlight, int count,
            float min_value, float max_value)
        {
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            if ((ctx.Current.Layout.Chart.Slot) >= (4)) return;
            {
                NkChart chart = ctx.Current.Layout.Chart;
                nk_chart_slot slot = chart.Slots[chart.Slot++];
                slot.type = (int)(type);
                slot.count = (int)(count);
                slot.color = (nk_color)(color);
                slot.highlight = (nk_color)(highlight);
                slot.min = (float)((min_value) < (max_value) ? (min_value) : (max_value));
                slot.max = (float)((min_value) < (max_value) ? (max_value) : (min_value));
                slot.range = (float)(slot.max - slot.min);
            }

        }

        public static void nk_chart_add_slot(NkContext ctx, int type, int count, float min_value, float max_value)
        {
            nk_chart_add_slot_colored(ctx, (int)(type), (nk_color)(ctx.Style.Chart.color),
                (nk_color)(ctx.Style.Chart.selected_color), (int)(count), (float)(min_value), (float)(max_value));
        }

        public static uint nk_chart_push_line(NkContext ctx, NkWindow win, NkChart g, float value, int slot)
        {
            NkPanel layout = win.Layout;
            nk_input i = ctx.Input;
            NkCommandBuffer _out_ = win.Buffer;
            uint ret = (uint)(0);
            nk_vec2 cur = new nk_vec2();
            nk_rect bounds = new nk_rect();
            nk_color color = new nk_color();
            float step;
            float range;
            float ratio;
            step = (float)(g.W / (float)(g.Slots[slot].count));
            range = (float)(g.Slots[slot].max - g.Slots[slot].min);
            ratio = (float)((value - g.Slots[slot].min) / range);
            if ((g.Slots[slot].index) == (0))
            {
                g.Slots[slot].last.x = (float)(g.X);
                g.Slots[slot].last.y = (float)((g.Y + g.H) - ratio * g.H);
                bounds.x = (float)(g.Slots[slot].last.x - 2);
                bounds.y = (float)(g.Slots[slot].last.y - 2);
                bounds.w = (float)(bounds.h = (float)(4));
                color = (nk_color)(g.Slots[slot].color);
                if (((layout.Flags & NK_WINDOW_ROM) == 0) &&
                    ((((g.Slots[slot].last.x - 3) <= (i.mouse.Pos.x)) && ((i.mouse.Pos.x) < (g.Slots[slot].last.x - 3 + 6))) &&
                     (((g.Slots[slot].last.y - 3) <= (i.mouse.Pos.y)) && ((i.mouse.Pos.y) < (g.Slots[slot].last.y - 3 + 6)))))
                {
                    ret = (uint)((nk_input_is_mouse_hovering_rect(i, (nk_rect)(bounds))) != 0 ? NK_CHART_HOVERING : 0);
                    ret |=
                        (uint)
                            ((((i.mouse.Buttons[NK_BUTTON_LEFT].down) != 0) && ((i.mouse.Buttons[NK_BUTTON_LEFT].clicked) != 0))
                                ? NK_CHART_CLICKED
                                : 0);
                    color = (nk_color)(g.Slots[slot].highlight);
                }
                nk_fill_rect(_out_, (nk_rect)(bounds), (float)(0), (nk_color)(color));
                g.Slots[slot].index += (int)(1);
                return (uint)(ret);
            }

            color = (nk_color)(g.Slots[slot].color);
            cur.x = (float)(g.X + (step * (float)(g.Slots[slot].index)));
            cur.y = (float)((g.Y + g.H) - (ratio * g.H));
            nk_stroke_line(_out_, (float)(g.Slots[slot].last.x), (float)(g.Slots[slot].last.y), (float)(cur.x),
                (float)(cur.y), (float)(1.0f), (nk_color)(color));
            bounds.x = (float)(cur.x - 3);
            bounds.y = (float)(cur.y - 3);
            bounds.w = (float)(bounds.h = (float)(6));
            if ((layout.Flags & NK_WINDOW_ROM) == 0)
            {
                if ((nk_input_is_mouse_hovering_rect(i, (nk_rect)(bounds))) != 0)
                {
                    ret = (uint)(NK_CHART_HOVERING);
                    ret |=
                        (uint)
                            (((i.mouse.Buttons[NK_BUTTON_LEFT].down == 0) && ((i.mouse.Buttons[NK_BUTTON_LEFT].clicked) != 0))
                                ? NK_CHART_CLICKED
                                : 0);
                    color = (nk_color)(g.Slots[slot].highlight);
                }
            }

            nk_fill_rect(_out_, (nk_rect)(nk_rect_((float)(cur.x - 2), (float)(cur.y - 2), (float)(4), (float)(4))),
                (float)(0), (nk_color)(color));
            g.Slots[slot].last.x = (float)(cur.x);
            g.Slots[slot].last.y = (float)(cur.y);
            g.Slots[slot].index += (int)(1);
            return (uint)(ret);
        }

        public static uint nk_chart_push_column(NkContext ctx, NkWindow win, NkChart chart, float value, int slot)
        {
            NkCommandBuffer _out_ = win.Buffer;
            nk_input _in_ = ctx.Input;
            NkPanel layout = win.Layout;
            float ratio;
            uint ret = (uint)(0);
            nk_color color = new nk_color();
            nk_rect item = new nk_rect();
            if ((chart.Slots[slot].index) >= (chart.Slots[slot].count)) return (uint)(nk_false);
            if ((chart.Slots[slot].count) != 0)
            {
                float padding = (float)(chart.Slots[slot].count - 1);
                item.w = (float)((chart.W - padding) / (float)(chart.Slots[slot].count));
            }

            color = (nk_color)(chart.Slots[slot].color);
            item.h =
                (float)
                    (chart.H *
                     (((value / chart.Slots[slot].range) < (0)) ? -(value / chart.Slots[slot].range) : (value / chart.Slots[slot].range)));
            if ((value) >= (0))
            {
                ratio =
                    (float)
                        ((value + (((chart.Slots[slot].min) < (0)) ? -(chart.Slots[slot].min) : (chart.Slots[slot].min))) /
                         (((chart.Slots[slot].range) < (0)) ? -(chart.Slots[slot].range) : (chart.Slots[slot].range)));
                item.y = (float)((chart.Y + chart.H) - chart.H * ratio);
            }
            else
            {
                ratio = (float)((value - chart.Slots[slot].max) / chart.Slots[slot].range);
                item.y = (float)(chart.Y + (chart.H * (((ratio) < (0)) ? -(ratio) : (ratio))) - item.h);
            }

            item.x = (float)(chart.X + ((float)(chart.Slots[slot].index) * item.w));
            item.x = (float)(item.x + ((float)(chart.Slots[slot].index)));
            if (((layout.Flags & NK_WINDOW_ROM) == 0) &&
                ((((item.x) <= (_in_.mouse.Pos.x)) && ((_in_.mouse.Pos.x) < (item.x + item.w))) &&
                 (((item.y) <= (_in_.mouse.Pos.y)) && ((_in_.mouse.Pos.y) < (item.y + item.h)))))
            {
                ret = (uint)(NK_CHART_HOVERING);
                ret |=
                    (uint)
                        (((((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->down == 0) &&
                          ((((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->clicked) != 0))
                            ? NK_CHART_CLICKED
                            : 0);
                color = (nk_color)(chart.Slots[slot].highlight);
            }

            nk_fill_rect(_out_, (nk_rect)(item), (float)(0), (nk_color)(color));
            chart.Slots[slot].index += (int)(1);
            return (uint)(ret);
        }

        public static uint nk_chart_push_slot(NkContext ctx, float value, int slot)
        {
            uint flags;
            NkWindow win;
            if (((ctx == null) || (ctx.Current == null)) || ((slot) >= (4))) return (uint)(nk_false);
            if ((slot) >= (ctx.Current.Layout.Chart.Slot)) return (uint)(nk_false);
            win = ctx.Current;
            if ((win.Layout.Chart.Slot) < (slot)) return (uint)(nk_false);
            switch (win.Layout.Chart.Slots[slot].type)
            {
                case NK_CHART_LINES:
                    flags = (uint)(nk_chart_push_line(ctx, win, win.Layout.Chart, (float)(value), (int)(slot)));
                    break;
                case NK_CHART_COLUMN:
                    flags = (uint)(nk_chart_push_column(ctx, win, win.Layout.Chart, (float)(value), (int)(slot)));
                    break;
                default:
                case NK_CHART_MAX:
                    flags = (uint)(0);
                    break;
            }

            return (uint)(flags);
        }

        public static uint nk_chart_push(NkContext ctx, float value)
        {
            return (uint)(nk_chart_push_slot(ctx, (float)(value), (int)(0)));
        }

        public static void nk_chart_end(NkContext ctx)
        {
            NkWindow win;
            NkChart chart;
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            chart = win.Layout.Chart;

            return;
        }

        public static void nk_plot(NkContext ctx, int type, float* values, int count, int offset)
        {
            int i = (int)(0);
            float min_value;
            float max_value;
            if (((ctx == null) || (values == null)) || (count == 0)) return;
            min_value = (float)(values[offset]);
            max_value = (float)(values[offset]);
            for (i = (int)(0); (i) < (count); ++i)
            {
                min_value = (float)((values[i + offset]) < (min_value) ? (values[i + offset]) : (min_value));
                max_value = (float)((values[i + offset]) < (max_value) ? (max_value) : (values[i + offset]));
            }
            if ((nk_chart_begin(ctx, (int)(type), (int)(count), (float)(min_value), (float)(max_value))) != 0)
            {
                for (i = (int)(0); (i) < (count); ++i)
                {
                    nk_chart_push(ctx, (float)(values[i + offset]));
                }
                nk_chart_end(ctx);
            }

        }

        public static void nk_plot_function(NkContext ctx, int type, void* userdata, NkFloatValueGetter value_getter,
            int count, int offset)
        {
            int i = (int)(0);
            float min_value;
            float max_value;
            if (((ctx == null) || (value_getter == null)) || (count == 0)) return;
            max_value = (float)(min_value = (float)(value_getter(userdata, (int)(offset))));
            for (i = (int)(0); (i) < (count); ++i)
            {
                float value = (float)(value_getter(userdata, (int)(i + offset)));
                min_value = (float)((value) < (min_value) ? (value) : (min_value));
                max_value = (float)((value) < (max_value) ? (max_value) : (value));
            }
            if ((nk_chart_begin(ctx, (int)(type), (int)(count), (float)(min_value), (float)(max_value))) != 0)
            {
                for (i = (int)(0); (i) < (count); ++i)
                {
                    nk_chart_push(ctx, (float)(value_getter(userdata, (int)(i + offset))));
                }
                nk_chart_end(ctx);
            }

        }

        public static int nk_group_scrolled_offset_begin(NkContext ctx, nk_scroll offset, char* title, uint flags)
        {
            nk_rect bounds = new nk_rect();
            NkWindow panel = new NkWindow();
            NkWindow win;
            win = ctx.Current;
            nk_panel_alloc_space(&bounds, ctx);
            {
                if (
                    (!(!(((((bounds.x) > (win.Layout.Clip.x + win.Layout.Clip.w)) || ((bounds.x + bounds.w) < (win.Layout.Clip.x))) ||
                          ((bounds.y) > (win.Layout.Clip.y + win.Layout.Clip.h))) || ((bounds.y + bounds.h) < (win.Layout.Clip.y))))) &&
                    ((flags & NK_WINDOW_MOVABLE) == 0))
                {
                    return (int)(0);
                }
            }

            if ((win.Flags & NK_WINDOW_ROM) != 0) flags |= (uint)(NK_WINDOW_ROM);

            panel.Bounds = (nk_rect)(bounds);
            panel.Flags = (uint)(flags);
            panel.Scrollbar.x = offset.x;
            panel.Scrollbar.y = offset.y;
            panel.Buffer = (NkCommandBuffer)(win.Buffer);
            panel.Layout = (NkPanel)(nk_create_panel(ctx));
            ctx.Current = panel;
            nk_panel_begin(ctx, (flags & NK_WINDOW_TITLE) != 0 ? title : null, (int)(NK_PANEL_GROUP));
            win.Buffer = (NkCommandBuffer)(panel.Buffer);
            win.Buffer.Clip = (nk_rect)(panel.Layout.Clip);
            panel.Layout.Offset = offset;

            panel.Layout.Parent = win.Layout;
            win.Layout = panel.Layout;
            ctx.Current = win;
            if (((panel.Layout.Flags & NK_WINDOW_CLOSED) != 0) || ((panel.Layout.Flags & NK_WINDOW_MINIMIZED) != 0))
            {
                uint f = (uint)(panel.Layout.Flags);
                nk_group_scrolled_end(ctx);
                if ((f & NK_WINDOW_CLOSED) != 0) return (int)(NK_WINDOW_CLOSED);
                if ((f & NK_WINDOW_MINIMIZED) != 0) return (int)(NK_WINDOW_MINIMIZED);
            }

            return (int)(1);
        }

        public static void nk_group_scrolled_end(NkContext ctx)
        {
            NkWindow win;
            NkPanel parent;
            NkPanel g;
            nk_rect clip = new nk_rect();
            NkWindow pan = new NkWindow();
            nk_vec2 panel_padding = new nk_vec2();
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            g = win.Layout;
            parent = g.Parent;

            panel_padding = (nk_vec2)(nk_panel_get_padding(ctx.Style, (int)(NK_PANEL_GROUP)));
            pan.Bounds.y = (float)(g.Bounds.y - (g.HeaderHeight + g.Menu.h));
            pan.Bounds.x = (float)(g.Bounds.x - panel_padding.x);
            pan.Bounds.w = (float)(g.Bounds.w + 2 * panel_padding.x);
            pan.Bounds.h = (float)(g.Bounds.h + g.HeaderHeight + g.Menu.h);
            if ((g.Flags & NK_WINDOW_BORDER) != 0)
            {
                pan.Bounds.x -= (float)(g.Border);
                pan.Bounds.y -= (float)(g.Border);
                pan.Bounds.w += (float)(2 * g.Border);
                pan.Bounds.h += (float)(2 * g.Border);
            }

            if ((g.Flags & NK_WINDOW_NO_SCROLLBAR) == 0)
            {
                pan.Bounds.w += (float)(ctx.Style.Window.scrollbar_size.x);
                pan.Bounds.h += (float)(ctx.Style.Window.scrollbar_size.y);
            }

            pan.Scrollbar.x = (uint)(g.Offset.x);
            pan.Scrollbar.y = (uint)(g.Offset.y);
            pan.Flags = (uint)(g.Flags);
            pan.Buffer = (NkCommandBuffer)(win.Buffer);
            pan.Layout = g;
            pan.Parent = win;
            ctx.Current = pan;
            nk_unify(ref clip, ref parent.Clip, (float)(pan.Bounds.x), (float)(pan.Bounds.y),
                (float)(pan.Bounds.x + pan.Bounds.w), (float)(pan.Bounds.y + pan.Bounds.h + panel_padding.x));
            nk_push_scissor(pan.Buffer, (nk_rect)(clip));
            nk_end(ctx);
            win.Buffer = (NkCommandBuffer)(pan.Buffer);
            nk_push_scissor(win.Buffer, (nk_rect)(parent.Clip));
            ctx.Current = win;
            win.Layout = parent;
            g.Bounds = (nk_rect)(pan.Bounds);
            return;
        }

        public static int nk_group_scrolled_begin(NkContext ctx, nk_scroll scroll, char* title, uint flags)
        {
            return (int)(nk_group_scrolled_offset_begin(ctx, scroll, title, (uint)(flags)));
        }

        public static int nk_group_begin_titled(NkContext ctx, char* id, char* title, uint flags)
        {
            int id_len;
            uint id_hash;
            NkWindow win;
            uint* x_offset;
            uint* y_offset;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (id == null)) return (int)(0);
            win = ctx.Current;
            id_len = (int)(nk_strlen(id));
            id_hash = (uint)(nk_murmur_hash(id, (int)(id_len), (uint)(NK_PANEL_GROUP)));
            x_offset = nk_find_value(win, (uint)(id_hash));
            if (x_offset == null)
            {
                x_offset = nk_add_value(ctx, win, (uint)(id_hash), (uint)(0));
                y_offset = nk_add_value(ctx, win, (uint)(id_hash + 1), (uint)(0));
                if ((x_offset == null) || (y_offset == null)) return (int)(0);
                *x_offset = (uint)(*y_offset = (uint)(0));
            }
            else y_offset = nk_find_value(win, (uint)(id_hash + 1));
            return
                (int)(nk_group_scrolled_offset_begin(ctx, new nk_scroll { x = *x_offset, y = *y_offset }, title, (uint)(flags)));
        }

        public static int nk_group_begin(NkContext ctx, char* title, uint flags)
        {
            return (int)(nk_group_begin_titled(ctx, title, title, (uint)(flags)));
        }

        public static void nk_group_end(NkContext ctx)
        {
            nk_group_scrolled_end(ctx);
        }

        public static int nk_list_view_begin(NkContext ctx, nk_list_view view, char* title, uint flags, int row_height,
            int row_count)
        {
            int title_len;
            uint title_hash;
            uint* x_offset;
            uint* y_offset;
            int result;
            NkWindow win;
            NkPanel layout;
            NkStyle style;
            nk_vec2 item_spacing = new nk_vec2();
            if (((ctx == null) || (view == null)) || (title == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            item_spacing = (nk_vec2)(style.Window.spacing);
            row_height += (int)((0) < ((int)(item_spacing.y)) ? ((int)(item_spacing.y)) : (0));
            title_len = (int)(nk_strlen(title));
            title_hash = (uint)(nk_murmur_hash(title, (int)(title_len), (uint)(NK_PANEL_GROUP)));
            x_offset = nk_find_value(win, (uint)(title_hash));
            if (x_offset == null)
            {
                x_offset = nk_add_value(ctx, win, (uint)(title_hash), (uint)(0));
                y_offset = nk_add_value(ctx, win, (uint)(title_hash + 1), (uint)(0));
                if ((x_offset == null) || (y_offset == null)) return (int)(0);
                *x_offset = (uint)(*y_offset = (uint)(0));
            }
            else y_offset = nk_find_value(win, (uint)(title_hash + 1));
            view.scroll_value = *y_offset;
            view.scroll_pointer = y_offset;
            *y_offset = (uint)(0);
            result =
                (int)(nk_group_scrolled_offset_begin(ctx, new nk_scroll { x = *x_offset, y = *y_offset }, title, (uint)(flags)));
            win = ctx.Current;
            layout = win.Layout;
            view.total_height = (int)(row_height * ((row_count) < (1) ? (1) : (row_count)));
            view.begin =
                ((int)
                    (((float)(view.scroll_value) / (float)(row_height)) < (0.0f)
                        ? (0.0f)
                        : ((float)(view.scroll_value) / (float)(row_height))));
            view.count =
                (int)
                    ((nk_iceilf((float)((layout.Clip.h) / (float)(row_height)))) < (0)
                        ? (0)
                        : (nk_iceilf((float)((layout.Clip.h) / (float)(row_height)))));
            view.end = (int)(view.begin + view.count);
            view.ctx = ctx;
            return (int)(result);
        }

        public static int nk_popup_begin(NkContext ctx, int type, char* title, uint flags, nk_rect rect)
        {
            NkWindow popup;
            NkWindow win;
            NkPanel panel;
            int title_len;
            uint title_hash;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            panel = win.Layout;
            title_len = (int)(nk_strlen(title));
            title_hash = (uint)(nk_murmur_hash(title, (int)(title_len), (uint)(NK_PANEL_POPUP)));
            popup = win.Popup.win;
            if (popup == null)
            {
                popup = (NkWindow)(nk_create_window(ctx));
                popup.Parent = win;
                win.Popup.win = popup;
                win.Popup.active = (int)(0);
                win.Popup.type = (int)(NK_PANEL_POPUP);
            }

            if (win.Popup.name != title_hash)
            {
                if (win.Popup.active == 0)
                {
                    win.Popup.name = (uint)(title_hash);
                    win.Popup.active = (int)(1);
                    win.Popup.type = (int)(NK_PANEL_POPUP);
                }
                else return (int)(0);
            }

            ctx.Current = popup;
            rect.x += (float)(win.Layout.Clip.x);
            rect.y += (float)(win.Layout.Clip.y);
            popup.Parent = win;
            popup.Bounds = (nk_rect)(rect);
            popup.Seq = (uint)(ctx.Seq);
            popup.Layout = (NkPanel)(nk_create_panel(ctx));
            popup.Flags = (uint)(flags);
            popup.Flags |= (uint)(NK_WINDOW_BORDER);
            if ((type) == (NK_POPUP_DYNAMIC)) popup.Flags |= (uint)(NK_WINDOW_DYNAMIC);
            nk_start_popup(ctx, win);
            popup.Buffer = (NkCommandBuffer)(win.Buffer);
            nk_push_scissor(popup.Buffer, (nk_rect)(nk_null_rect));
            if ((nk_panel_begin(ctx, title, (int)(NK_PANEL_POPUP))) != 0)
            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (uint)(NK_WINDOW_ROM);
                    root.Flags &= (uint)(~(uint)(NK_WINDOW_REMOVE_ROM));
                    root = root.Parent;
                }
                win.Popup.active = (int)(1);
                popup.Layout.Offset = popup.Scrollbar;
                popup.Layout.Parent = win.Layout;
                return (int)(1);
            }
            else
            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (uint)(NK_WINDOW_REMOVE_ROM);
                    root = root.Parent;
                }
                win.Popup.active = (int)(0);
                ctx.Current = win;
                nk_free_panel(ctx, popup.Layout);
                popup.Layout = null;
                return (int)(0);
            }

        }

        public static int nk_nonblock_begin(NkContext ctx, uint flags, nk_rect body, nk_rect header, int panel_type)
        {
            NkWindow popup;
            NkWindow win;
            NkPanel panel;
            int is_active = (int)(nk_true);
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            panel = win.Layout;
            popup = win.Popup.win;
            if (popup == null)
            {
                popup = (NkWindow)(nk_create_window(ctx));
                popup.Parent = win;
                win.Popup.win = popup;
                win.Popup.type = (int)(panel_type);
                nk_command_buffer_init(popup.Buffer, (int)(NK_CLIPPING_ON));
            }
            else
            {
                int pressed;
                int in_body;
                int in_header;
                pressed = (int)(nk_input_is_mouse_pressed(ctx.Input, (int)(NK_BUTTON_LEFT)));
                in_body = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(body)));
                in_header = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(header)));
                if (((pressed) != 0) && ((in_body == 0) || ((in_header) != 0))) is_active = (int)(nk_false);
            }

            win.Popup.header = (nk_rect)(header);
            if (is_active == 0)
            {
                NkPanel root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (uint)(NK_WINDOW_REMOVE_ROM);
                    root = root.Parent;
                }
                return (int)(is_active);
            }

            popup.Bounds = (nk_rect)(body);
            popup.Parent = win;
            popup.Layout = (NkPanel)(nk_create_panel(ctx));
            popup.Flags = (uint)(flags);
            popup.Flags |= (uint)(NK_WINDOW_BORDER);
            popup.Flags |= (uint)(NK_WINDOW_DYNAMIC);
            popup.Seq = (uint)(ctx.Seq);
            win.Popup.active = (int)(1);
            nk_start_popup(ctx, win);
            popup.Buffer = (NkCommandBuffer)(win.Buffer);
            nk_push_scissor(popup.Buffer, (nk_rect)(nk_null_rect));
            ctx.Current = popup;
            nk_panel_begin(ctx, null, (int)(panel_type));
            win.Buffer = (NkCommandBuffer)(popup.Buffer);
            popup.Layout.Parent = win.Layout;
            popup.Layout.Offset = popup.Scrollbar;

            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (uint)(NK_WINDOW_ROM);
                    root = root.Parent;
                }
            }

            return (int)(is_active);
        }

        public static void nk_popup_close(NkContext ctx)
        {
            NkWindow popup;
            if ((ctx == null) || (ctx.Current == null)) return;
            popup = ctx.Current;
            popup.Flags |= (uint)(NK_WINDOW_HIDDEN);
        }

        public static void nk_popup_end(NkContext ctx)
        {
            NkWindow win;
            NkWindow popup;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            popup = ctx.Current;
            if (popup.Parent == null) return;
            win = popup.Parent;
            if ((popup.Flags & NK_WINDOW_HIDDEN) != 0)
            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (uint)(NK_WINDOW_REMOVE_ROM);
                    root = root.Parent;
                }
                win.Popup.active = (int)(0);
            }

            nk_push_scissor(popup.Buffer, (nk_rect)(nk_null_rect));
            nk_end(ctx);
            win.Buffer = (NkCommandBuffer)(popup.Buffer);
            nk_finish_popup(ctx, win);
            ctx.Current = win;
            nk_push_scissor(win.Buffer, (nk_rect)(win.Layout.Clip));
        }

        public static int nk_tooltip_begin(NkContext ctx, float width)
        {
            int x;
            int y;
            int w;
            int h;
            NkWindow win;
            nk_input _in_;
            nk_rect bounds = new nk_rect();
            int ret;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            _in_ = ctx.Input;
            if (((win.Popup.win) != null) && ((win.Popup.type & NK_PANEL_SET_NONBLOCK) != 0)) return (int)(0);
            w = (int)(nk_iceilf((float)(width)));
            h = (int)(nk_iceilf((float)(nk_null_rect.h)));
            x = (int)(nk_ifloorf((float)(_in_.mouse.Pos.x + 1)) - (int)(win.Layout.Clip.x));
            y = (int)(nk_ifloorf((float)(_in_.mouse.Pos.y + 1)) - (int)(win.Layout.Clip.y));
            bounds.x = ((float)(x));
            bounds.y = ((float)(y));
            bounds.w = ((float)(w));
            bounds.h = ((float)(h));
            ret =
                (int)
                    (nk_popup_begin(ctx, (int)(NK_POPUP_DYNAMIC), "__##Tooltip##__",
                        (uint)(NK_WINDOW_NO_SCROLLBAR | NK_WINDOW_BORDER), (nk_rect)(bounds)));
            if ((ret) != 0) win.Layout.Flags &= (uint)(~(uint)(NK_WINDOW_ROM));
            win.Popup.type = (int)(NK_PANEL_TOOLTIP);
            ctx.Current.Layout.Type = (int)(NK_PANEL_TOOLTIP);
            return (int)(ret);
        }

        public static void nk_tooltip_end(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return;
            ctx.Current.Seq--;
            nk_popup_close(ctx);
            nk_popup_end(ctx);
        }

        public static void nk_tooltip(NkContext ctx, char* text)
        {
            NkStyle style;
            nk_vec2 padding = new nk_vec2();
            int text_len;
            float text_width;
            float text_height;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (text == null)) return;
            style = ctx.Style;
            padding = (nk_vec2)(style.Window.padding);
            text_len = (int)(nk_strlen(text));
            text_width =
                (float)(style.Font.Width((NkHandle)(style.Font.Userdata), (float)(style.Font.Height), text, (int)(text_len)));
            text_width += (float)(4 * padding.x);
            text_height = (float)(style.Font.Height + 2 * padding.y);
            if ((nk_tooltip_begin(ctx, (float)(text_width))) != 0)
            {
                nk_layout_row_dynamic(ctx, (float)(text_height), (int)(1));
                nk_text_(ctx, text, (int)(text_len), (uint)(NK_TEXT_LEFT));
                nk_tooltip_end(ctx);
            }

        }

        public static int nk_contextual_begin(NkContext ctx, uint flags, nk_vec2 size, nk_rect trigger_bounds)
        {
            NkWindow win;
            NkWindow popup;
            nk_rect body = new nk_rect();
            nk_rect null_rect = new nk_rect();
            int is_clicked = (int)(0);
            int is_active = (int)(0);
            int is_open = (int)(0);
            int ret = (int)(0);
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            ++win.Popup.con_count;
            popup = win.Popup.win;
            is_open = (int)(((popup) != null) && ((win.Popup.type) == (NK_PANEL_CONTEXTUAL)) ? 1 : 0);
            is_clicked = (int)(nk_input_mouse_clicked(ctx.Input, (int)(NK_BUTTON_RIGHT), (nk_rect)(trigger_bounds)));
            if (((win.Popup.active_con) != 0) && (win.Popup.con_count != win.Popup.active_con)) return (int)(0);
            if (((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) ||
                (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int)(0);
            win.Popup.active_con = (uint)(win.Popup.con_count);
            if ((is_clicked) != 0)
            {
                body.x = (float)(ctx.Input.mouse.Pos.x);
                body.y = (float)(ctx.Input.mouse.Pos.y);
            }
            else
            {
                body.x = (float)(popup.Bounds.x);
                body.y = (float)(popup.Bounds.y);
            }

            body.w = (float)(size.x);
            body.h = (float)(size.y);
            ret =
                (int)
                    (nk_nonblock_begin(ctx, (uint)(flags | NK_WINDOW_NO_SCROLLBAR), (nk_rect)(body), (nk_rect)(null_rect),
                        (int)(NK_PANEL_CONTEXTUAL)));
            if ((ret) != 0) win.Popup.type = (int)(NK_PANEL_CONTEXTUAL);
            else
            {
                win.Popup.active_con = (uint)(0);
                if ((win.Popup.win) != null) win.Popup.win.Flags = (uint)(0);
            }

            return (int)(ret);
        }

        public static int nk_contextual_item_text(NkContext ctx, char* text, int len, uint alignment)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget_fitting(&bounds, ctx, (nk_vec2)(style.ContextualButton.padding)));
            if (state == 0) return (int)(nk_false);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), text, (int)(len), (uint)(alignment),
                    (int)(NK_BUTTON_DEFAULT), style.ContextualButton, _in_, style.Font)) != 0)
            {
                nk_contextual_close(ctx);
                return (int)(nk_true);
            }

            return (int)(nk_false);
        }

        public static int nk_contextual_item_label(NkContext ctx, char* label, uint align)
        {
            return (int)(nk_contextual_item_text(ctx, label, (int)(nk_strlen(label)), (uint)(align)));
        }

        public static int nk_contextual_item_image_text(NkContext ctx, nk_image img, char* text, int len, uint align)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget_fitting(&bounds, ctx, (nk_vec2)(style.ContextualButton.padding)));
            if (state == 0) return (int)(nk_false);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (nk_image)(img), text,
                    (int)(len), (uint)(align), (int)(NK_BUTTON_DEFAULT), style.ContextualButton, style.Font, _in_)) != 0)
            {
                nk_contextual_close(ctx);
                return (int)(nk_true);
            }

            return (int)(nk_false);
        }

        public static int nk_contextual_item_image_label(NkContext ctx, nk_image img, char* label, uint align)
        {
            return (int)(nk_contextual_item_image_text(ctx, (nk_image)(img), label, (int)(nk_strlen(label)), (uint)(align)));
        }

        public static int nk_contextual_item_symbol_text(NkContext ctx, int symbol, char* text, int len, uint align)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            nk_rect bounds = new nk_rect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget_fitting(&bounds, ctx, (nk_vec2)(style.ContextualButton.padding)));
            if (state == 0) return (int)(nk_false);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(bounds), (int)(symbol), text,
                    (int)(len), (uint)(align), (int)(NK_BUTTON_DEFAULT), style.ContextualButton, style.Font, _in_)) != 0)
            {
                nk_contextual_close(ctx);
                return (int)(nk_true);
            }

            return (int)(nk_false);
        }

        public static int nk_contextual_item_symbol_label(NkContext ctx, int symbol, char* text, uint align)
        {
            return (int)(nk_contextual_item_symbol_text(ctx, (int)(symbol), text, (int)(nk_strlen(text)), (uint)(align)));
        }

        public static void nk_contextual_close(NkContext ctx)
        {
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            nk_popup_close(ctx);
        }

        public static void nk_contextual_end(NkContext ctx)
        {
            NkWindow popup;
            NkPanel panel;
            if ((ctx == null) || (ctx.Current == null)) return;
            popup = ctx.Current;
            panel = popup.Layout;
            if ((panel.Flags & NK_WINDOW_DYNAMIC) != 0)
            {
                nk_rect body = new nk_rect();
                if ((panel.AtY) < (panel.Bounds.y + panel.Bounds.h))
                {
                    nk_vec2 padding = (nk_vec2)(nk_panel_get_padding(ctx.Style, (int)(panel.Type)));
                    body = (nk_rect)(panel.Bounds);
                    body.y = (float)(panel.AtY + panel.FooterHeight + panel.Border + padding.y + panel.Row.height);
                    body.h = (float)((panel.Bounds.y + panel.Bounds.h) - body.y);
                }
                {
                    int pressed = (int)(nk_input_is_mouse_pressed(ctx.Input, (int)(NK_BUTTON_LEFT)));
                    int in_body = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (nk_rect)(body)));
                    if (((pressed) != 0) && ((in_body) != 0)) popup.Flags |= (uint)(NK_WINDOW_HIDDEN);
                }
            }

            if ((popup.Flags & NK_WINDOW_HIDDEN) != 0) popup.Seq = (uint)(0);
            nk_popup_end(ctx);
            return;
        }

        public static int nk_combo_begin(NkContext ctx, NkWindow win, nk_vec2 size, int is_clicked, nk_rect header)
        {
            NkWindow popup;
            int is_open = (int)(0);
            int is_active = (int)(0);
            nk_rect body = new nk_rect();
            uint hash;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            popup = win.Popup.win;
            body.x = (float)(header.x);
            body.w = (float)(size.x);
            body.y = (float)(header.y + header.h - ctx.Style.Window.combo_border);
            body.h = (float)(size.y);
            hash = (uint)(win.Popup.combo_count++);
            is_open = (int)((popup != null) ? nk_true : nk_false);
            is_active =
                (int)((((popup) != null) && ((win.Popup.name) == (hash))) && ((win.Popup.type) == (NK_PANEL_COMBO)) ? 1 : 0);
            if ((((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) || (((is_open) != 0) && (is_active == 0))) ||
                (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int)(0);
            if (
                nk_nonblock_begin(ctx, (uint)(0), (nk_rect)(body),
                    (nk_rect)
                        ((((is_clicked) != 0) && ((is_open) != 0)) ? nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)) : header),
                    (int)(NK_PANEL_COMBO)) == 0) return (int)(0);
            win.Popup.type = (int)(NK_PANEL_COMBO);
            win.Popup.name = (uint)(hash);
            return (int)(1);
        }

        public static int nk_combo_begin_text(NkContext ctx, char* selected, int len, nk_vec2 size)
        {
            nk_input _in_;
            NkWindow win;
            NkStyle style;
            int s;
            int is_clicked = (int)(nk_false);
            nk_rect header = new nk_rect();
            NkStyleItem background;
            nk_text text = new nk_text();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (selected == null))
                return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                text.text = (nk_color)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                text.text = (nk_color)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                text.text = (nk_color)(style.Combo.label_normal);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                text.background = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                text.background = (nk_color)(background.Data.Color);
                nk_fill_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (nk_color)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (nk_color)(style.Combo.border_color));
            }

            {
                nk_rect label = new nk_rect();
                nk_rect button = new nk_rect();
                nk_rect content = new nk_rect();
                int sym;
                if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) sym = (int)(style.Combo.sym_hover);
                else if ((is_clicked) != 0) sym = (int)(style.Combo.sym_active);
                else sym = (int)(style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.x);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                text.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(header.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)(button.x - (style.Combo.content_padding.x + style.Combo.spacing.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                nk_widget_text(win.Buffer, (nk_rect)(label), selected, (int)(len), &text, (uint)(NK_TEXT_LEFT), ctx.Style.Font);
                nk_draw_button_symbol(win.Buffer, &button, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (nk_vec2)(size), (int)(is_clicked), (nk_rect)(header)));
        }

        public static int nk_combo_begin_label(NkContext ctx, char* selected, nk_vec2 size)
        {
            return (int)(nk_combo_begin_text(ctx, selected, (int)(nk_strlen(selected)), (nk_vec2)(size)));
        }

        public static int nk_combo_begin_color(NkContext ctx, nk_color color, nk_vec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0) background = style.Combo.active;
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) background = style.Combo.hover;
            else background = style.Combo.normal;
            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                nk_fill_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (nk_color)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (nk_color)(style.Combo.border_color));
            }

            {
                nk_rect content = new nk_rect();
                nk_rect button = new nk_rect();
                nk_rect bounds = new nk_rect();
                int sym;
                if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) sym = (int)(style.Combo.sym_hover);
                else if ((is_clicked) != 0) sym = (int)(style.Combo.sym_active);
                else sym = (int)(style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.x);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                bounds.h = (float)(header.h - 4 * style.Combo.content_padding.y);
                bounds.y = (float)(header.y + 2 * style.Combo.content_padding.y);
                bounds.x = (float)(header.x + 2 * style.Combo.content_padding.x);
                bounds.w = (float)((button.x - (style.Combo.content_padding.x + style.Combo.spacing.x)) - bounds.x);
                nk_fill_rect(win.Buffer, (nk_rect)(bounds), (float)(0), (nk_color)(color));
                nk_draw_button_symbol(win.Buffer, &button, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (nk_vec2)(size), (int)(is_clicked), (nk_rect)(header)));
        }

        public static int nk_combo_begin_symbol(NkContext ctx, int symbol, nk_vec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            nk_color sym_background = new nk_color();
            nk_color symbol_color = new nk_color();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                symbol_color = (nk_color)(style.Combo.symbol_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                symbol_color = (nk_color)(style.Combo.symbol_hover);
            }
            else
            {
                background = style.Combo.normal;
                symbol_color = (nk_color)(style.Combo.symbol_hover);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                sym_background = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                sym_background = (nk_color)(background.Data.Color);
                nk_fill_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (nk_color)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (nk_color)(style.Combo.border_color));
            }

            {
                nk_rect bounds = new nk_rect();
                nk_rect content = new nk_rect();
                nk_rect button = new nk_rect();
                int sym;
                if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) sym = (int)(style.Combo.sym_hover);
                else if ((is_clicked) != 0) sym = (int)(style.Combo.sym_active);
                else sym = (int)(style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.y);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                bounds.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                bounds.y = (float)(header.y + style.Combo.content_padding.y);
                bounds.x = (float)(header.x + style.Combo.content_padding.x);
                bounds.w = (float)((button.x - style.Combo.content_padding.y) - bounds.x);
                nk_draw_symbol(win.Buffer, (int)(symbol), (nk_rect)(bounds), (nk_color)(sym_background),
                    (nk_color)(symbol_color), (float)(1.0f), style.Font);
                nk_draw_button_symbol(win.Buffer, &bounds, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (nk_vec2)(size), (int)(is_clicked), (nk_rect)(header)));
        }

        public static int nk_combo_begin_symbol_text(NkContext ctx, char* selected, int len, int symbol, nk_vec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            nk_color symbol_color = new nk_color();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if (s == 0) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                symbol_color = (nk_color)(style.Combo.symbol_active);
                text.text = (nk_color)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                symbol_color = (nk_color)(style.Combo.symbol_hover);
                text.text = (nk_color)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                symbol_color = (nk_color)(style.Combo.symbol_normal);
                text.text = (nk_color)(style.Combo.label_normal);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                text.background = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                text.background = (nk_color)(background.Data.Color);
                nk_fill_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (nk_color)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (nk_color)(style.Combo.border_color));
            }

            {
                nk_rect content = new nk_rect();
                nk_rect button = new nk_rect();
                nk_rect label = new nk_rect();
                nk_rect image = new nk_rect();
                int sym;
                if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) sym = (int)(style.Combo.sym_hover);
                else if ((is_clicked) != 0) sym = (int)(style.Combo.sym_active);
                else sym = (int)(style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.x);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                nk_draw_button_symbol(win.Buffer, &button, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
                image.x = (float)(header.x + style.Combo.content_padding.x);
                image.y = (float)(header.y + style.Combo.content_padding.y);
                image.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                image.w = (float)(image.h);
                nk_draw_symbol(win.Buffer, (int)(symbol), (nk_rect)(image), (nk_color)(text.background),
                    (nk_color)(symbol_color), (float)(1.0f), style.Font);
                text.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(image.x + image.w + style.Combo.spacing.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)((button.x - style.Combo.content_padding.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                nk_widget_text(win.Buffer, (nk_rect)(label), selected, (int)(len), &text, (uint)(NK_TEXT_LEFT), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (nk_vec2)(size), (int)(is_clicked), (nk_rect)(header)));
        }

        public static int nk_combo_begin_image(NkContext ctx, nk_image img, nk_vec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0) background = style.Combo.active;
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) background = style.Combo.hover;
            else background = style.Combo.normal;
            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                nk_fill_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (nk_color)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (nk_color)(style.Combo.border_color));
            }

            {
                nk_rect bounds = new nk_rect();
                nk_rect content = new nk_rect();
                nk_rect button = new nk_rect();
                int sym;
                if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) sym = (int)(style.Combo.sym_hover);
                else if ((is_clicked) != 0) sym = (int)(style.Combo.sym_active);
                else sym = (int)(style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.y);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                bounds.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                bounds.y = (float)(header.y + style.Combo.content_padding.y);
                bounds.x = (float)(header.x + style.Combo.content_padding.x);
                bounds.w = (float)((button.x - style.Combo.content_padding.y) - bounds.x);
                nk_draw_image(win.Buffer, (nk_rect)(bounds), img, (nk_color)(nk_white));
                nk_draw_button_symbol(win.Buffer, &bounds, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (nk_vec2)(size), (int)(is_clicked), (nk_rect)(header)));
        }

        public static int nk_combo_begin_image_text(NkContext ctx, char* selected, int len, nk_image img, nk_vec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if (s == 0) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (nk_rect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                text.text = (nk_color)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                text.text = (nk_color)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                text.text = (nk_color)(style.Combo.label_normal);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                text.background = (nk_color)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (nk_rect)(header), background.Data.Image, (nk_color)(nk_white));
            }
            else
            {
                text.background = (nk_color)(background.Data.Color);
                nk_fill_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (nk_color)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (nk_rect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (nk_color)(style.Combo.border_color));
            }

            {
                nk_rect content = new nk_rect();
                nk_rect button = new nk_rect();
                nk_rect label = new nk_rect();
                nk_rect image = new nk_rect();
                int sym;
                if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) sym = (int)(style.Combo.sym_hover);
                else if ((is_clicked) != 0) sym = (int)(style.Combo.sym_active);
                else sym = (int)(style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.x);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                nk_draw_button_symbol(win.Buffer, &button, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
                image.x = (float)(header.x + style.Combo.content_padding.x);
                image.y = (float)(header.y + style.Combo.content_padding.y);
                image.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                image.w = (float)(image.h);
                nk_draw_image(win.Buffer, (nk_rect)(image), img, (nk_color)(nk_white));
                text.padding = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(image.x + image.w + style.Combo.spacing.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)((button.x - style.Combo.content_padding.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                nk_widget_text(win.Buffer, (nk_rect)(label), selected, (int)(len), &text, (uint)(NK_TEXT_LEFT), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (nk_vec2)(size), (int)(is_clicked), (nk_rect)(header)));
        }

        public static int nk_combo_begin_symbol_label(NkContext ctx, char* selected, int type, nk_vec2 size)
        {
            return (int)(nk_combo_begin_symbol_text(ctx, selected, (int)(nk_strlen(selected)), (int)(type), (nk_vec2)(size)));
        }

        public static int nk_combo_begin_image_label(NkContext ctx, char* selected, nk_image img, nk_vec2 size)
        {
            return
                (int)(nk_combo_begin_image_text(ctx, selected, (int)(nk_strlen(selected)), (nk_image)(img), (nk_vec2)(size)));
        }

        public static int nk_combo_item_text(NkContext ctx, char* text, int len, uint align)
        {
            return (int)(nk_contextual_item_text(ctx, text, (int)(len), (uint)(align)));
        }

        public static int nk_combo_item_label(NkContext ctx, char* label, uint align)
        {
            return (int)(nk_contextual_item_label(ctx, label, (uint)(align)));
        }

        public static int nk_combo_item_image_text(NkContext ctx, nk_image img, char* text, int len, uint alignment)
        {
            return (int)(nk_contextual_item_image_text(ctx, (nk_image)(img), text, (int)(len), (uint)(alignment)));
        }

        public static int nk_combo_item_image_label(NkContext ctx, nk_image img, char* text, uint alignment)
        {
            return (int)(nk_contextual_item_image_label(ctx, (nk_image)(img), text, (uint)(alignment)));
        }

        public static int nk_combo_item_symbol_text(NkContext ctx, int sym, char* text, int len, uint alignment)
        {
            return (int)(nk_contextual_item_symbol_text(ctx, (int)(sym), text, (int)(len), (uint)(alignment)));
        }

        public static int nk_combo_item_symbol_label(NkContext ctx, int sym, char* label, uint alignment)
        {
            return (int)(nk_contextual_item_symbol_label(ctx, (int)(sym), label, (uint)(alignment)));
        }

        public static void nk_combo_end(NkContext ctx)
        {
            nk_contextual_end(ctx);
        }

        public static void nk_combo_close(NkContext ctx)
        {
            nk_contextual_close(ctx);
        }

        public static int nk_combo(NkContext ctx, char** items, int count, int selected, int item_height, nk_vec2 size)
        {
            int i = (int)(0);
            int max_height;
            nk_vec2 item_spacing = new nk_vec2();
            nk_vec2 window_padding = new nk_vec2();
            if (((ctx == null) || (items == null)) || (count == 0)) return (int)(selected);
            item_spacing = (nk_vec2)(ctx.Style.Window.spacing);
            window_padding = (nk_vec2)(nk_panel_get_padding(ctx.Style, (int)(ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            if ((nk_combo_begin_label(ctx, items[selected], (nk_vec2)(size))) != 0)
            {
                nk_layout_row_dynamic(ctx, (float)(item_height), (int)(1));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    if ((nk_combo_item_label(ctx, items[i], (uint)(NK_TEXT_LEFT))) != 0) selected = (int)(i);
                }
                nk_combo_end(ctx);
            }

            return (int)(selected);
        }

        public static int nk_combo_separator(NkContext ctx, char* items_separated_by_separator, int separator, int selected,
            int count, int item_height, nk_vec2 size)
        {
            int i;
            int max_height;
            nk_vec2 item_spacing = new nk_vec2();
            nk_vec2 window_padding = new nk_vec2();
            char* current_item;
            char* iter;
            ;
            int length = (int)(0);
            if ((ctx == null) || (items_separated_by_separator == null)) return (int)(selected);
            item_spacing = (nk_vec2)(ctx.Style.Window.spacing);
            window_padding = (nk_vec2)(nk_panel_get_padding(ctx.Style, (int)(ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            current_item = items_separated_by_separator;
            for (i = (int)(0); (i) < (count); ++i)
            {
                iter = current_item;
                while (((*iter) != 0) && (*iter != separator))
                {
                    iter++;
                }
                length = ((int)(iter - current_item));
                if ((i) == (selected)) break;
                current_item = iter + 1;
            }
            if ((nk_combo_begin_text(ctx, current_item, (int)(length), (nk_vec2)(size))) != 0)
            {
                current_item = items_separated_by_separator;
                nk_layout_row_dynamic(ctx, (float)(item_height), (int)(1));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    iter = current_item;
                    while (((*iter) != 0) && (*iter != separator))
                    {
                        iter++;
                    }
                    length = ((int)(iter - current_item));
                    if ((nk_combo_item_text(ctx, current_item, (int)(length), (uint)(NK_TEXT_LEFT))) != 0) selected = (int)(i);
                    current_item = current_item + length + 1;
                }
                nk_combo_end(ctx);
            }

            return (int)(selected);
        }

        public static int nk_combo_string(NkContext ctx, char* items_separated_by_zeros, int selected, int count,
            int item_height, nk_vec2 size)
        {
            return
                (int)
                    (nk_combo_separator(ctx, items_separated_by_zeros, (int)('\0'), (int)(selected), (int)(count),
                        (int)(item_height), (nk_vec2)(size)));
        }

        public static int nk_combo_callback(NkContext ctx, NkComboCallback item_getter, void* userdata, int selected,
            int count, int item_height, nk_vec2 size)
        {
            int i;
            int max_height;
            nk_vec2 item_spacing = new nk_vec2();
            nk_vec2 window_padding = new nk_vec2();
            char* item;
            if ((ctx == null) || (item_getter == null)) return (int)(selected);
            item_spacing = (nk_vec2)(ctx.Style.Window.spacing);
            window_padding = (nk_vec2)(nk_panel_get_padding(ctx.Style, (int)(ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            item_getter(userdata, (int)(selected), &item);
            if ((nk_combo_begin_label(ctx, item, (nk_vec2)(size))) != 0)
            {
                nk_layout_row_dynamic(ctx, (float)(item_height), (int)(1));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    item_getter(userdata, (int)(i), &item);
                    if ((nk_combo_item_label(ctx, item, (uint)(NK_TEXT_LEFT))) != 0) selected = (int)(i);
                }
                nk_combo_end(ctx);
            }

            return (int)(selected);
        }

        public static void nk_combobox(NkContext ctx, char** items, int count, int* selected, int item_height, nk_vec2 size)
        {
            *selected = (int)(nk_combo(ctx, items, (int)(count), (int)(*selected), (int)(item_height), (nk_vec2)(size)));
        }

        public static void nk_combobox_string(NkContext ctx, char* items_separated_by_zeros, int* selected, int count,
            int item_height, nk_vec2 size)
        {
            *selected =
                (int)
                    (nk_combo_string(ctx, items_separated_by_zeros, (int)(*selected), (int)(count), (int)(item_height),
                        (nk_vec2)(size)));
        }

        public static void nk_combobox_separator(NkContext ctx, char* items_separated_by_separator, int separator,
            int* selected, int count, int item_height, nk_vec2 size)
        {
            *selected =
                (int)
                    (nk_combo_separator(ctx, items_separated_by_separator, (int)(separator), (int)(*selected), (int)(count),
                        (int)(item_height), (nk_vec2)(size)));
        }

        public static void nk_combobox_callback(NkContext ctx, NkComboCallback item_getter, void* userdata, int* selected,
            int count, int item_height, nk_vec2 size)
        {
            *selected =
                (int)
                    (nk_combo_callback(ctx, item_getter, userdata, (int)(*selected), (int)(count), (int)(item_height),
                        (nk_vec2)(size)));
        }

        public static int nk_menu_begin(NkContext ctx, NkWindow win, char* id, int is_clicked, nk_rect header, nk_vec2 size)
        {
            int is_open = (int)(0);
            int is_active = (int)(0);
            nk_rect body = new nk_rect();
            NkWindow popup;
            uint hash = (uint)(nk_murmur_hash(id, (int)(nk_strlen(id)), (uint)(NK_PANEL_MENU)));
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            body.x = (float)(header.x);
            body.w = (float)(size.x);
            body.y = (float)(header.y + header.h);
            body.h = (float)(size.y);
            popup = win.Popup.win;
            is_open = (int)(popup != null ? nk_true : nk_false);
            is_active =
                (int)((((popup) != null) && ((win.Popup.name) == (hash))) && ((win.Popup.type) == (NK_PANEL_MENU)) ? 1 : 0);
            if ((((((is_clicked) != 0) && ((is_open) != 0)) && (is_active == 0)) || (((is_open) != 0) && (is_active == 0))) ||
                (((is_open == 0) && (is_active == 0)) && (is_clicked == 0))) return (int)(0);
            if (
                nk_nonblock_begin(ctx, (uint)(NK_WINDOW_NO_SCROLLBAR), (nk_rect)(body), (nk_rect)(header), (int)(NK_PANEL_MENU)) ==
                0) return (int)(0);
            win.Popup.type = (int)(NK_PANEL_MENU);
            win.Popup.name = (uint)(hash);
            return (int)(1);
        }

        public static int nk_menu_begin_text(NkContext ctx, char* title, int len, uint align, nk_vec2 size)
        {
            NkWindow win;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(header), title, (int)(len), (uint)(align),
                    (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, _in_, ctx.Style.Font)) != 0) is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, title, (int)(is_clicked), (nk_rect)(header), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_label(NkContext ctx, char* text, uint align, nk_vec2 size)
        {
            return (int)(nk_menu_begin_text(ctx, text, (int)(nk_strlen(text)), (uint)(align), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_image(NkContext ctx, char* id, nk_image img, nk_vec2 size)
        {
            NkWindow win;
            nk_rect header = new nk_rect();
            nk_input _in_;
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_image(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(header), (nk_image)(img),
                    (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, _in_)) != 0) is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, id, (int)(is_clicked), (nk_rect)(header), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_symbol(NkContext ctx, char* id, int sym, nk_vec2 size)
        {
            NkWindow win;
            nk_input _in_;
            nk_rect header = new nk_rect();
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_symbol(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(header), (int)(sym),
                    (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, _in_, ctx.Style.Font)) != 0) is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, id, (int)(is_clicked), (nk_rect)(header), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_image_text(NkContext ctx, char* title, int len, uint align, nk_image img,
            nk_vec2 size)
        {
            NkWindow win;
            nk_rect header = new nk_rect();
            nk_input _in_;
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(header), (nk_image)(img), title,
                    (int)(len), (uint)(align), (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, ctx.Style.Font, _in_)) != 0)
                is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, title, (int)(is_clicked), (nk_rect)(header), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_image_label(NkContext ctx, char* title, uint align, nk_image img, nk_vec2 size)
        {
            return
                (int)
                    (nk_menu_begin_image_text(ctx, title, (int)(nk_strlen(title)), (uint)(align), (nk_image)(img), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_symbol_text(NkContext ctx, char* title, int len, uint align, int sym, nk_vec2 size)
        {
            NkWindow win;
            nk_rect header = new nk_rect();
            nk_input _in_;
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (nk_rect)(header), (int)(sym), title, (int)(len),
                    (uint)(align), (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, ctx.Style.Font, _in_)) != 0)
                is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, title, (int)(is_clicked), (nk_rect)(header), (nk_vec2)(size)));
        }

        public static int nk_menu_begin_symbol_label(NkContext ctx, char* title, uint align, int sym, nk_vec2 size)
        {
            return
                (int)
                    (nk_menu_begin_symbol_text(ctx, title, (int)(nk_strlen(title)), (uint)(align), (int)(sym), (nk_vec2)(size)));
        }

        public static int nk_menu_item_text(NkContext ctx, char* title, int len, uint align)
        {
            return (int)(nk_contextual_item_text(ctx, title, (int)(len), (uint)(align)));
        }

        public static int nk_menu_item_label(NkContext ctx, char* label, uint align)
        {
            return (int)(nk_contextual_item_label(ctx, label, (uint)(align)));
        }

        public static int nk_menu_item_image_label(NkContext ctx, nk_image img, char* label, uint align)
        {
            return (int)(nk_contextual_item_image_label(ctx, (nk_image)(img), label, (uint)(align)));
        }

        public static int nk_menu_item_image_text(NkContext ctx, nk_image img, char* text, int len, uint align)
        {
            return (int)(nk_contextual_item_image_text(ctx, (nk_image)(img), text, (int)(len), (uint)(align)));
        }

        public static int nk_menu_item_symbol_text(NkContext ctx, int sym, char* text, int len, uint align)
        {
            return (int)(nk_contextual_item_symbol_text(ctx, (int)(sym), text, (int)(len), (uint)(align)));
        }

        public static int nk_menu_item_symbol_label(NkContext ctx, int sym, char* label, uint align)
        {
            return (int)(nk_contextual_item_symbol_label(ctx, (int)(sym), label, (uint)(align)));
        }

        public static void nk_menu_close(NkContext ctx)
        {
            nk_contextual_close(ctx);
        }

        public static void nk_menu_end(NkContext ctx)
        {
            nk_contextual_end(ctx);
        }
    }
}