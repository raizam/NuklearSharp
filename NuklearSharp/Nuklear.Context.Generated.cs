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
                                (NkRect)(nk_rect_((float)(s.X), (float)(s.Y), (float)(s.W), (float)(s.H))));
                        }
                        break;
                    case NK_COMMAND_LINE:
                        {
                            NkCommandLine l = (NkCommandLine)(cmd);
                            nk_draw_list_stroke_line(ctx.DrawList, (NkVec2)(nk_vec2_((float)(l.Begin.x), (float)(l.Begin.y))),
                                (NkVec2)(nk_vec2_((float)(l.End.x), (float)(l.End.y))), (NkColor)(l.Color), (float)(l.LineThickness));
                        }
                        break;
                    case NK_COMMAND_CURVE:
                        {
                            NkCommandCurve q = (NkCommandCurve)(cmd);
                            nk_draw_list_stroke_curve(ctx.DrawList, (NkVec2)(nk_vec2_((float)(q.Begin.x), (float)(q.Begin.y))),
                                (NkVec2)(nk_vec2_((float)(q.Ctrl0.x), (float)(q.Ctrl0.y))),
                                (NkVec2)(nk_vec2_((float)(q.Ctrl1.x), (float)(q.Ctrl1.y))),
                                (NkVec2)(nk_vec2_((float)(q.End.x), (float)(q.End.y))), (NkColor)(q.Color),
                                (uint)(config.CurveSegmentCount), (float)(q.LineThickness));
                        }
                        break;
                    case NK_COMMAND_RECT:
                        {
                            NkCommandRect r = (NkCommandRect)(cmd);
                            nk_draw_list_stroke_rect(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (NkColor)(r.Color),
                                (float)(r.Rounding), (float)(r.LineThickness));
                        }
                        break;
                    case NK_COMMAND_RECT_FILLED:
                        {
                            NkCommandRectFilled r = (NkCommandRectFilled)(cmd);
                            nk_draw_list_fill_rect(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (NkColor)(r.Color),
                                (float)(r.Rounding));
                        }
                        break;
                    case NK_COMMAND_RECT_MULTI_COLOR:
                        {
                            NkCommandRectMultiColor r = (NkCommandRectMultiColor)(cmd);
                            nk_draw_list_fill_rect_multi_color(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (NkColor)(r.Left),
                                (NkColor)(r.Top), (NkColor)(r.Right), (NkColor)(r.Bottom));
                        }
                        break;
                    case NK_COMMAND_CIRCLE:
                        {
                            NkCommandCircle c = (NkCommandCircle)(cmd);
                            nk_draw_list_stroke_circle(ctx.DrawList,
                                (NkVec2)(nk_vec2_((float)((float)(c.X) + (float)(c.W) / 2), (float)((float)(c.Y) + (float)(c.H) / 2))),
                                (float)((float)(c.W) / 2), (NkColor)(c.Color), (uint)(config.CircleSegmentCount), (float)(c.LineThickness));
                        }
                        break;
                    case NK_COMMAND_CIRCLE_FILLED:
                        {
                            NkCommandCircleFilled c = (NkCommandCircleFilled)(cmd);
                            nk_draw_list_fill_circle(ctx.DrawList,
                                (NkVec2)(nk_vec2_((float)((float)(c.X) + (float)(c.W) / 2), (float)((float)(c.Y) + (float)(c.H) / 2))),
                                (float)((float)(c.W) / 2), (NkColor)(c.Color), (uint)(config.CircleSegmentCount));
                        }
                        break;
                    case NK_COMMAND_ARC:
                        {
                            NkCommandArc c = (NkCommandArc)(cmd);
                            nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))));
                            nk_draw_list_path_arc_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))), (float)(c.R),
                                (float)(c.A[0]), (float)(c.A[1]), (uint)(config.ArcSegmentCount));
                            nk_draw_list_path_stroke(ctx.DrawList, (NkColor)(c.Color), (int)(NK_STROKE_CLOSED), (float)(c.LineThickness));
                        }
                        break;
                    case NK_COMMAND_ARC_FILLED:
                        {
                            NkCommandArcFilled c = (NkCommandArcFilled)(cmd);
                            nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))));
                            nk_draw_list_path_arc_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))), (float)(c.R),
                                (float)(c.A[0]), (float)(c.A[1]), (uint)(config.ArcSegmentCount));
                            nk_draw_list_path_fill(ctx.DrawList, (NkColor)(c.Color));
                        }
                        break;
                    case NK_COMMAND_TRIANGLE:
                        {
                            NkCommandTriangle t = (NkCommandTriangle)(cmd);
                            nk_draw_list_stroke_triangle(ctx.DrawList, (NkVec2)(nk_vec2_((float)(t.A.x), (float)(t.A.y))),
                                (NkVec2)(nk_vec2_((float)(t.B.x), (float)(t.B.y))), (NkVec2)(nk_vec2_((float)(t.C.x), (float)(t.C.y))),
                                (NkColor)(t.Color), (float)(t.LineThickness));
                        }
                        break;
                    case NK_COMMAND_TRIANGLE_FILLED:
                        {
                            NkCommandTriangleFilled t = (NkCommandTriangleFilled)(cmd);
                            nk_draw_list_fill_triangle(ctx.DrawList, (NkVec2)(nk_vec2_((float)(t.A.x), (float)(t.A.y))),
                                (NkVec2)(nk_vec2_((float)(t.B.x), (float)(t.B.y))), (NkVec2)(nk_vec2_((float)(t.C.x), (float)(t.C.y))),
                                (NkColor)(t.Color));
                        }
                        break;
                    case NK_COMMAND_POLYGON:
                        {
                            int i;
                            NkCommandPolygon p = (NkCommandPolygon)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                NkVec2 pnt = (NkVec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(pnt));
                            }
                            nk_draw_list_path_stroke(ctx.DrawList, (NkColor)(p.Color), (int)(NK_STROKE_CLOSED), (float)(p.LineThickness));
                        }
                        break;
                    case NK_COMMAND_POLYGON_FILLED:
                        {
                            int i;
                            NkCommandPolygonFilled p = (NkCommandPolygonFilled)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                NkVec2 pnt = (NkVec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(pnt));
                            }
                            nk_draw_list_path_fill(ctx.DrawList, (NkColor)(p.Color));
                        }
                        break;
                    case NK_COMMAND_POLYLINE:
                        {
                            int i;
                            NkCommandPolyline p = (NkCommandPolyline)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                NkVec2 pnt = (NkVec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(pnt));
                            }
                            nk_draw_list_path_stroke(ctx.DrawList, (NkColor)(p.Color), (int)(NK_STROKE_OPEN), (float)(p.LineThickness));
                        }
                        break;
                    case NK_COMMAND_TEXT:
                        {
                            NkCommandText t = (NkCommandText)(cmd);
                            nk_draw_list_add_text(ctx.DrawList, t.Font,
                                (NkRect)(nk_rect_((float)(t.X), (float)(t.Y), (float)(t.W), (float)(t.H))), t.String, (int)(t.Length),
                                (float)(t.Height), (NkColor)(t.Foreground));
                        }
                        break;
                    case NK_COMMAND_IMAGE:
                        {
                            NkCommandImage i = (NkCommandImage)(cmd);
                            nk_draw_list_add_image(ctx.DrawList, (NkImage)(i.Img),
                                (NkRect)(nk_rect_((float)(i.X), (float)(i.Y), (float)(i.W), (float)(i.H))), (NkColor)(i.Col));
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
            _in_.mouse.ScrollDelta = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
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

        public static void nk_input_scroll(NkContext ctx, NkVec2 val)
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

        public static void nk_style_from_table(NkContext ctx, NkColor[] table)
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
            text.color = (NkColor)(table[NK_COLOR_TEXT]);
            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            button = style.Button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_BUTTON])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_BUTTON_HOVER])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_BUTTON_ACTIVE])));
            button.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            button.text_background = (NkColor)(table[NK_COLOR_BUTTON]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.image_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(1.0f);
            button.rounding = (float)(4.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.ContextualButton;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_BUTTON_HOVER])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_BUTTON_ACTIVE])));
            button.border_color = (NkColor)(table[NK_COLOR_WINDOW]);
            button.text_background = (NkColor)(table[NK_COLOR_WINDOW]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.MenuButton;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.border_color = (NkColor)(table[NK_COLOR_WINDOW]);
            button.text_background = (NkColor)(table[NK_COLOR_WINDOW]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(1.0f);
            button.draw_begin = null;
            button.draw_end = null;
            toggle = style.Checkbox;

            toggle.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE])));
            toggle.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.userdata = (NkHandle)(nk_handle_ptr(null));
            toggle.text_background = (NkColor)(table[NK_COLOR_WINDOW]);
            toggle.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            toggle.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            toggle.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            toggle.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            toggle.touch_padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            toggle.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            toggle.border = (float)(0.0f);
            toggle.spacing = (float)(4);
            toggle = style.Option;

            toggle.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE])));
            toggle.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_HOVER])));
            toggle.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TOGGLE_CURSOR])));
            toggle.userdata = (NkHandle)(nk_handle_ptr(null));
            toggle.text_background = (NkColor)(table[NK_COLOR_WINDOW]);
            toggle.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            toggle.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            toggle.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            toggle.padding = (NkVec2)(nk_vec2_((float)(3.0f), (float)(3.0f)));
            toggle.touch_padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            toggle.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            toggle.border = (float)(0.0f);
            toggle.spacing = (float)(4);
            select = style.Selectable;

            select.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SELECT])));
            select.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SELECT])));
            select.pressed = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SELECT])));
            select.normal_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SELECT_ACTIVE])));
            select.hover_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SELECT_ACTIVE])));
            select.pressed_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SELECT_ACTIVE])));
            select.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            select.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            select.text_pressed = (NkColor)(table[NK_COLOR_TEXT]);
            select.text_normal_active = (NkColor)(table[NK_COLOR_TEXT]);
            select.text_hover_active = (NkColor)(table[NK_COLOR_TEXT]);
            select.text_pressed_active = (NkColor)(table[NK_COLOR_TEXT]);
            select.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            select.touch_padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            select.userdata = (NkHandle)(nk_handle_ptr(null));
            select.rounding = (float)(0.0f);
            select.draw_begin = null;
            select.draw_end = null;
            slider = style.Slider;

            slider.normal = (NkStyleItem)(nk_style_item_hide());
            slider.hover = (NkStyleItem)(nk_style_item_hide());
            slider.active = (NkStyleItem)(nk_style_item_hide());
            slider.bar_normal = (NkColor)(table[NK_COLOR_SLIDER]);
            slider.bar_hover = (NkColor)(table[NK_COLOR_SLIDER]);
            slider.bar_active = (NkColor)(table[NK_COLOR_SLIDER]);
            slider.bar_filled = (NkColor)(table[NK_COLOR_SLIDER_CURSOR]);
            slider.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER_CURSOR])));
            slider.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER_CURSOR_HOVER])));
            slider.cursor_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER_CURSOR_ACTIVE])));
            slider.inc_symbol = (int)(NK_SYMBOL_TRIANGLE_RIGHT);
            slider.dec_symbol = (int)(NK_SYMBOL_TRIANGLE_LEFT);
            slider.cursor_size = (NkVec2)(nk_vec2_((float)(16), (float)(16)));
            slider.padding = (NkVec2)(nk_vec2_((float)(2), (float)(2)));
            slider.spacing = (NkVec2)(nk_vec2_((float)(2), (float)(2)));
            slider.userdata = (NkHandle)(nk_handle_ptr(null));
            slider.show_buttons = (int)(nk_false);
            slider.bar_height = (float)(8);
            slider.rounding = (float)(0);
            slider.draw_begin = null;
            slider.draw_end = null;
            button = style.Slider.inc_button;
            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(nk_rgb((int)(40), (int)(40), (int)(40)))));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(nk_rgb((int)(42), (int)(42), (int)(42)))));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(nk_rgb((int)(44), (int)(44), (int)(44)))));
            button.border_color = (NkColor)(nk_rgb((int)(65), (int)(65), (int)(65)));
            button.text_background = (NkColor)(nk_rgb((int)(40), (int)(40), (int)(40)));
            button.text_normal = (NkColor)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_hover = (NkColor)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_active = (NkColor)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.padding = (NkVec2)(nk_vec2_((float)(8.0f), (float)(8.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(1.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Slider.dec_button = (nk_style_button)(style.Slider.inc_button);
            prog = style.Progress;

            prog.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER])));
            prog.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER])));
            prog.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER])));
            prog.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER_CURSOR])));
            prog.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER_CURSOR_HOVER])));
            prog.cursor_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SLIDER_CURSOR_ACTIVE])));
            prog.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            prog.cursor_border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            prog.userdata = (NkHandle)(nk_handle_ptr(null));
            prog.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            prog.rounding = (float)(0);
            prog.border = (float)(0);
            prog.cursor_rounding = (float)(0);
            prog.cursor_border = (float)(0);
            prog.draw_begin = null;
            prog.draw_end = null;
            scroll = style.Scrollh;

            scroll.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SCROLLBAR])));
            scroll.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SCROLLBAR])));
            scroll.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SCROLLBAR])));
            scroll.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SCROLLBAR_CURSOR])));
            scroll.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SCROLLBAR_CURSOR_HOVER])));
            scroll.cursor_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_SCROLLBAR_CURSOR_ACTIVE])));
            scroll.dec_symbol = (int)(NK_SYMBOL_CIRCLE_SOLID);
            scroll.inc_symbol = (int)(NK_SYMBOL_CIRCLE_SOLID);
            scroll.userdata = (NkHandle)(nk_handle_ptr(null));
            scroll.border_color = (NkColor)(table[NK_COLOR_SCROLLBAR]);
            scroll.cursor_border_color = (NkColor)(table[NK_COLOR_SCROLLBAR]);
            scroll.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            scroll.show_buttons = (int)(nk_false);
            scroll.border = (float)(0);
            scroll.rounding = (float)(0);
            scroll.border_cursor = (float)(0);
            scroll.rounding_cursor = (float)(0);
            scroll.draw_begin = null;
            scroll.draw_end = null;
            style.Scrollv = (nk_style_scrollbar)(style.Scrollh);
            button = style.Scrollh.inc_button;
            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(nk_rgb((int)(40), (int)(40), (int)(40)))));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(nk_rgb((int)(42), (int)(42), (int)(42)))));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(nk_rgb((int)(44), (int)(44), (int)(44)))));
            button.border_color = (NkColor)(nk_rgb((int)(65), (int)(65), (int)(65)));
            button.text_background = (NkColor)(nk_rgb((int)(40), (int)(40), (int)(40)));
            button.text_normal = (NkColor)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_hover = (NkColor)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_active = (NkColor)(nk_rgb((int)(175), (int)(175), (int)(175)));
            button.padding = (NkVec2)(nk_vec2_((float)(4.0f), (float)(4.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
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

            edit.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_EDIT])));
            edit.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_EDIT])));
            edit.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_EDIT])));
            edit.cursor_normal = (NkColor)(table[NK_COLOR_TEXT]);
            edit.cursor_hover = (NkColor)(table[NK_COLOR_TEXT]);
            edit.cursor_text_normal = (NkColor)(table[NK_COLOR_EDIT]);
            edit.cursor_text_hover = (NkColor)(table[NK_COLOR_EDIT]);
            edit.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            edit.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            edit.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            edit.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            edit.selected_normal = (NkColor)(table[NK_COLOR_TEXT]);
            edit.selected_hover = (NkColor)(table[NK_COLOR_TEXT]);
            edit.selected_text_normal = (NkColor)(table[NK_COLOR_EDIT]);
            edit.selected_text_hover = (NkColor)(table[NK_COLOR_EDIT]);
            edit.scrollbar_size = (NkVec2)(nk_vec2_((float)(10), (float)(10)));
            edit.scrollbar = (nk_style_scrollbar)(style.Scrollv);
            edit.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            edit.row_padding = (float)(2);
            edit.cursor_size = (float)(4);
            edit.border = (float)(1);
            edit.rounding = (float)(0);
            property = style.Property;

            property.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            property.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            property.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            property.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            property.label_normal = (NkColor)(table[NK_COLOR_TEXT]);
            property.label_hover = (NkColor)(table[NK_COLOR_TEXT]);
            property.label_active = (NkColor)(table[NK_COLOR_TEXT]);
            property.sym_left = (int)(NK_SYMBOL_TRIANGLE_LEFT);
            property.sym_right = (int)(NK_SYMBOL_TRIANGLE_RIGHT);
            property.userdata = (NkHandle)(nk_handle_ptr(null));
            property.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            property.border = (float)(1);
            property.rounding = (float)(10);
            property.draw_begin = null;
            property.draw_end = null;
            button = style.Property.dec_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            button.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[NK_COLOR_PROPERTY]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Property.inc_button = (nk_style_button)(style.Property.dec_button);
            edit = style.Property.edit;

            edit.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            edit.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            edit.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_PROPERTY])));
            edit.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            edit.cursor_normal = (NkColor)(table[NK_COLOR_TEXT]);
            edit.cursor_hover = (NkColor)(table[NK_COLOR_TEXT]);
            edit.cursor_text_normal = (NkColor)(table[NK_COLOR_EDIT]);
            edit.cursor_text_hover = (NkColor)(table[NK_COLOR_EDIT]);
            edit.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            edit.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            edit.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            edit.selected_normal = (NkColor)(table[NK_COLOR_TEXT]);
            edit.selected_hover = (NkColor)(table[NK_COLOR_TEXT]);
            edit.selected_text_normal = (NkColor)(table[NK_COLOR_EDIT]);
            edit.selected_text_hover = (NkColor)(table[NK_COLOR_EDIT]);
            edit.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            edit.cursor_size = (float)(8);
            edit.border = (float)(0);
            edit.rounding = (float)(0);
            chart = style.Chart;

            chart.background = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_CHART])));
            chart.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            chart.selected_color = (NkColor)(table[NK_COLOR_CHART_COLOR_HIGHLIGHT]);
            chart.color = (NkColor)(table[NK_COLOR_CHART_COLOR]);
            chart.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            chart.border = (float)(0);
            chart.rounding = (float)(0);
            combo = style.Combo;
            combo.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_COMBO])));
            combo.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_COMBO])));
            combo.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_COMBO])));
            combo.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            combo.label_normal = (NkColor)(table[NK_COLOR_TEXT]);
            combo.label_hover = (NkColor)(table[NK_COLOR_TEXT]);
            combo.label_active = (NkColor)(table[NK_COLOR_TEXT]);
            combo.sym_normal = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            combo.sym_hover = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            combo.sym_active = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            combo.content_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            combo.button_padding = (NkVec2)(nk_vec2_((float)(0), (float)(4)));
            combo.spacing = (NkVec2)(nk_vec2_((float)(4), (float)(0)));
            combo.border = (float)(1);
            combo.rounding = (float)(0);
            button = style.Combo.button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_COMBO])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_COMBO])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_COMBO])));
            button.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[NK_COLOR_COMBO]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            tab = style.Tab;
            tab.background = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TAB_HEADER])));
            tab.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            tab.text = (NkColor)(table[NK_COLOR_TEXT]);
            tab.sym_minimize = (int)(NK_SYMBOL_TRIANGLE_RIGHT);
            tab.sym_maximize = (int)(NK_SYMBOL_TRIANGLE_DOWN);
            tab.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            tab.spacing = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            tab.indent = (float)(10.0f);
            tab.border = (float)(1);
            tab.rounding = (float)(0);
            button = style.Tab.tab_minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TAB_HEADER])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TAB_HEADER])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TAB_HEADER])));
            button.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[NK_COLOR_TAB_HEADER]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Tab.tab_maximize_button = (nk_style_button)(button);
            button = style.Tab.node_minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            button.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[NK_COLOR_TAB_HEADER]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
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
            win.header.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            win.header.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            win.header.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            win.header.label_normal = (NkColor)(table[NK_COLOR_TEXT]);
            win.header.label_hover = (NkColor)(table[NK_COLOR_TEXT]);
            win.header.label_active = (NkColor)(table[NK_COLOR_TEXT]);
            win.header.label_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.header.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.header.spacing = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            button = style.Window.header.close_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            button.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[NK_COLOR_HEADER]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.Window.header.minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_HEADER])));
            button.border_color = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[NK_COLOR_HEADER]);
            button.text_normal = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_hover = (NkColor)(table[NK_COLOR_TEXT]);
            button.text_active = (NkColor)(table[NK_COLOR_TEXT]);
            button.padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (uint)(NK_TEXT_CENTERED);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            win.background = (NkColor)(table[NK_COLOR_WINDOW]);
            win.fixed_background = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_WINDOW])));
            win.border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.popup_border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.combo_border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.contextual_border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.menu_border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.group_border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.tooltip_border_color = (NkColor)(table[NK_COLOR_BORDER]);
            win.scaler = (NkStyleItem)(nk_style_item_color((NkColor)(table[NK_COLOR_TEXT])));
            win.rounding = (float)(0.0f);
            win.spacing = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.scrollbar_size = (NkVec2)(nk_vec2_((float)(10), (float)(10)));
            win.min_size = (NkVec2)(nk_vec2_((float)(64), (float)(64)));
            win.combo_border = (float)(1.0f);
            win.contextual_border = (float)(1.0f);
            win.menu_border = (float)(1.0f);
            win.group_border = (float)(1.0f);
            win.tooltip_border = (float)(1.0f);
            win.popup_border = (float)(1.0f);
            win.border = (float)(2.0f);
            win.min_row_height_padding = (float)(8);
            win.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.group_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.popup_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.combo_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.contextual_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.menu_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.tooltip_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
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

        public static int nk_style_push_vec2(NkContext ctx, NkVec2* address, NkVec2 value)
        {
            nk_config_stack_vec2 type_stack;
            nk_config_stack_vec2_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.vectors;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (NkVec2)(*address);
            *address = (NkVec2)(value);
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

        public static int nk_style_push_color(NkContext ctx, NkColor* address, NkColor value)
        {
            nk_config_stack_color type_stack;
            nk_config_stack_color_element element;
            if (ctx == null) return (int)(0);
            type_stack = ctx.Stacks.colors;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return (int)(0);
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (NkColor)(*address);
            *address = (NkColor)(value);
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
            *element.address = (NkVec2)(element.old_value);
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
            *element.address = (NkColor)(element.old_value);
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

        public static void nk_style_load_cursor(NkContext ctx, int cursor, NkCursor c)
        {
            NkStyle style;
            if (ctx == null) return;
            style = ctx.Style;
            style.Cursors[cursor] = c;
        }

        public static void nk_style_load_all_cursors(NkContext ctx, NkCursor[] cursors)
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
            buffer.Clip = (NkRect)(nk_null_rect);
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
            NkVec2 scrollbar_size = new NkVec2();
            NkVec2 panel_padding = new NkVec2();
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
            scrollbar_size = (NkVec2)(style.Window.scrollbar_size);
            panel_padding = (NkVec2)(nk_panel_get_padding(style, (int)(panel_type)));
            if (((win.Flags & NK_WINDOW_MOVABLE) != 0) && ((win.Flags & NK_WINDOW_ROM) == 0))
            {
                int left_mouse_down;
                int left_mouse_click_in_cursor;
                NkRect header = new NkRect();
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
                    (int)(nk_input_has_mouse_click_down_in_rect(_in_, (int)(NK_BUTTON_LEFT), (NkRect)(header), (int)(nk_true)));
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
            layout.Bounds = (NkRect)(win.Bounds);
            layout.Bounds.x += (float)(panel_padding.x);
            layout.Bounds.w -= (float)(2 * panel_padding.x);
            if ((win.Flags & NK_WINDOW_BORDER) != 0)
            {
                layout.Border = (float)(nk_panel_get_border(style, (uint)(win.Flags), (int)(panel_type)));
                layout.Bounds = (NkRect)(nk_shrink_rect_((NkRect)(layout.Bounds), (float)(layout.Border)));
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
                NkRect header = new NkRect();
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
                    text.text = (NkColor)(style.Window.header.label_active);
                }
                else if ((nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(header))) != 0)
                {
                    background = style.Window.header.hover;
                    text.text = (NkColor)(style.Window.header.label_hover);
                }
                else
                {
                    background = style.Window.header.normal;
                    text.text = (NkColor)(style.Window.header.label_normal);
                }
                header.h += (float)(1.0f);
                if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
                {
                    text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                    nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
                }
                else
                {
                    text.background = (NkColor)(background.Data.Color);
                    nk_fill_rect(_out_, (NkRect)(header), (float)(0), (NkColor)(background.Data.Color));
                }
                {
                    NkRect button = new NkRect();
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
                            ((nk_do_button_symbol(ref ws, win.Buffer, (NkRect)(button), (int)(style.Window.header.close_symbol),
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
                            ((nk_do_button_symbol(ref ws, win.Buffer, (NkRect)(button),
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
                    NkRect label = new NkRect();
                    float t = (float)(font.Width((NkHandle)(font.Userdata), (float)(font.Height), title, (int)(text_len)));
                    text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
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
                    nk_widget_text(_out_, (NkRect)(label), title, (int)(text_len), &text, (uint)(NK_TEXT_LEFT), font);
                }
            }

            if (((layout.Flags & NK_WINDOW_MINIMIZED) == 0) && ((layout.Flags & NK_WINDOW_DYNAMIC) == 0))
            {
                NkRect body = new NkRect();
                body.x = (float)(win.Bounds.x);
                body.w = (float)(win.Bounds.w);
                body.y = (float)(win.Bounds.y + layout.HeaderHeight);
                body.h = (float)(win.Bounds.h - layout.HeaderHeight);
                if ((style.Window.fixed_background.Type) == (NK_STYLE_ITEM_IMAGE))
                    nk_draw_image(_out_, (NkRect)(body), style.Window.fixed_background.Data.Image, (NkColor)(nk_white));
                else nk_fill_rect(_out_, (NkRect)(body), (float)(0), (NkColor)(style.Window.fixed_background.Data.Color));
            }

            {
                NkRect clip = new NkRect();
                layout.Clip = (NkRect)(layout.Bounds);
                nk_unify(ref clip, ref win.Buffer.Clip, (float)(layout.Clip.x), (float)(layout.Clip.y),
                    (float)(layout.Clip.x + layout.Clip.w), (float)(layout.Clip.y + layout.Clip.h));
                nk_push_scissor(_out_, (NkRect)(clip));
                layout.Clip = (NkRect)(clip);
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
            NkVec2 scrollbar_size = new NkVec2();
            NkVec2 panel_padding = new NkVec2();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            window = ctx.Current;
            layout = window.Layout;
            style = ctx.Style;
            _out_ = window.Buffer;
            _in_ = (((layout.Flags & NK_WINDOW_ROM) != 0) || ((layout.Flags & NK_WINDOW_NO_INPUT) != 0)) ? null : ctx.Input;
            if (nk_panel_is_sub((int)(layout.Type)) == 0) nk_push_scissor(_out_, (NkRect)(nk_null_rect));
            scrollbar_size = (NkVec2)(style.Window.scrollbar_size);
            panel_padding = (NkVec2)(nk_panel_get_padding(style, (int)(layout.Type)));
            layout.AtY += (float)(layout.Row.height);
            if (((layout.Flags & NK_WINDOW_DYNAMIC) != 0) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0))
            {
                NkRect empty_space = new NkRect();
                if ((layout.AtY) < (layout.Bounds.y + layout.Bounds.h)) layout.Bounds.h = (float)(layout.AtY - layout.Bounds.y);
                empty_space.x = (float)(window.Bounds.x);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.h = (float)(panel_padding.y);
                empty_space.w = (float)(window.Bounds.w);
                nk_fill_rect(_out_, (NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                empty_space.x = (float)(window.Bounds.x);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.w = (float)(panel_padding.x + layout.Border);
                empty_space.h = (float)(layout.Bounds.h);
                nk_fill_rect(_out_, (NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                empty_space.x = (float)(layout.Bounds.x + layout.Bounds.w - layout.Border);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.w = (float)(panel_padding.x + layout.Border);
                empty_space.h = (float)(layout.Bounds.h);
                if (((layout.Offset.y) == (0)) && ((layout.Flags & NK_WINDOW_NO_SCROLLBAR) == 0))
                    empty_space.w += (float)(scrollbar_size.x);
                nk_fill_rect(_out_, (NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                if ((layout.Offset.x != 0) && ((layout.Flags & NK_WINDOW_NO_SCROLLBAR) == 0))
                {
                    empty_space.x = (float)(window.Bounds.x);
                    empty_space.y = (float)(layout.Bounds.y + layout.Bounds.h);
                    empty_space.w = (float)(window.Bounds.w);
                    empty_space.h = (float)(scrollbar_size.y);
                    nk_fill_rect(_out_, (NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                }
            }

            if ((((layout.Flags & NK_WINDOW_NO_SCROLLBAR) == 0) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0)) &&
                ((window.ScrollbarHidingTimer) < (4.0f)))
            {
                NkRect scroll = new NkRect();
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
                        if (((nk_input_is_mouse_hovering_rect(_in_, (NkRect)(layout.Bounds))) != 0) &&
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
                            (nk_do_scrollbarv(ref state, _out_, (NkRect)(scroll), (int)(scroll_has_scrolling), (float)(scroll_offset),
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
                            (nk_do_scrollbarh(ref state, _out_, (NkRect)(scroll), (int)(scroll_has_scrolling), (float)(scroll_offset),
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
                NkColor border_color = (NkColor)(nk_panel_get_border_color(style, (int)(layout.Type)));
                float padding_y =
                    (float)
                        ((layout.Flags & NK_WINDOW_MINIMIZED) != 0
                            ? style.Window.border + window.Bounds.y + layout.HeaderHeight
                            : (layout.Flags & NK_WINDOW_DYNAMIC) != 0
                                ? layout.Bounds.y + layout.Bounds.h + layout.FooterHeight
                                : window.Bounds.y + window.Bounds.h);
                NkRect b = window.Bounds;
                b.h = padding_y - window.Bounds.y;
                nk_stroke_rect(_out_, b, 0, layout.Border, border_color);
            }

            if ((((layout.Flags & NK_WINDOW_SCALABLE) != 0) && ((_in_) != null)) && ((layout.Flags & NK_WINDOW_MINIMIZED) == 0))
            {
                NkRect scaler = new NkRect();
                scaler.w = (float)(scrollbar_size.x);
                scaler.h = (float)(scrollbar_size.y);
                scaler.y = (float)(layout.Bounds.y + layout.Bounds.h);
                if ((layout.Flags & NK_WINDOW_SCALE_LEFT) != 0) scaler.x = (float)(layout.Bounds.x - panel_padding.x * 0.5f);
                else scaler.x = (float)(layout.Bounds.x + layout.Bounds.w + panel_padding.x);
                if ((layout.Flags & NK_WINDOW_NO_SCROLLBAR) != 0) scaler.x -= (float)(scaler.w);
                {
                    NkStyleItem item = style.Window.scaler;
                    if ((item.Type) == (NK_STYLE_ITEM_IMAGE))
                        nk_draw_image(_out_, (NkRect)(scaler), item.Data.Image, (NkColor)(nk_white));
                    else
                    {
                        if ((layout.Flags & NK_WINDOW_SCALE_LEFT) != 0)
                        {
                            nk_fill_triangle(_out_, (float)(scaler.x), (float)(scaler.y), (float)(scaler.x), (float)(scaler.y + scaler.h),
                                (float)(scaler.x + scaler.w), (float)(scaler.y + scaler.h), (NkColor)(item.Data.Color));
                        }
                        else
                        {
                            nk_fill_triangle(_out_, (float)(scaler.x + scaler.w), (float)(scaler.y), (float)(scaler.x + scaler.w),
                                (float)(scaler.y + scaler.h), (float)(scaler.x), (float)(scaler.y + scaler.h), (NkColor)(item.Data.Color));
                        }
                    }
                }
                if ((window.Flags & NK_WINDOW_ROM) == 0)
                {
                    NkVec2 window_size = (NkVec2)(style.Window.min_size);
                    int left_mouse_down = (int)(((nk_mouse_button*)_in_.mouse.Buttons + NK_BUTTON_LEFT)->down);
                    int left_mouse_click_in_scaler =
                        (int)(nk_input_has_mouse_click_down_in_rect(_in_, (int)(NK_BUTTON_LEFT), (NkRect)(scaler), (int)(nk_true)));
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

        public static int nk_begin(NkContext ctx, char* title, NkRect bounds, uint flags)
        {
            return (int)(nk_begin_titled(ctx, title, title, (NkRect)(bounds), (uint)(flags)));
        }

        public static int nk_begin_titled(NkContext ctx, char* name, char* title, NkRect bounds, uint flags)
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
                win.Bounds = (NkRect)(bounds);
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
                if ((win.Flags & (NK_WINDOW_MOVABLE | NK_WINDOW_SCALABLE)) == 0) win.Bounds = (NkRect)(bounds);
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
                NkRect win_bounds =
                    (NkRect)
                        (((win.Flags & NK_WINDOW_MINIMIZED) == 0)
                            ? win.Bounds
                            : nk_rect_((float)(win.Bounds.x), (float)(win.Bounds.y), (float)(win.Bounds.w), (float)(h)));
                inpanel =
                    (int)
                        (nk_input_has_mouse_click_down_in_rect(ctx.Input, (int)(NK_BUTTON_LEFT), (NkRect)(win_bounds), (int)(nk_true)));
                inpanel = (int)(((inpanel) != 0) && ((ctx.Input.mouse.Buttons[NK_BUTTON_LEFT].clicked) != 0) ? 1 : 0);
                ishovered = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(win_bounds)));
                if (((win != ctx.Active) && ((ishovered) != 0)) && (ctx.Input.mouse.Buttons[NK_BUTTON_LEFT].down == 0))
                {
                    iter = win.Next;
                    while ((iter) != null)
                    {
                        NkRect iter_bounds =
                            (NkRect)
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
                        NkRect iter_bounds =
                            (NkRect)
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

        public static NkRect nk_window_get_bounds(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null))
                return (NkRect)(nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            return (NkRect)(ctx.Current.Bounds);
        }

        public static NkVec2 nk_window_get_position(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            return (NkVec2)(nk_vec2_((float)(ctx.Current.Bounds.x), (float)(ctx.Current.Bounds.y)));
        }

        public static NkVec2 nk_window_get_size(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            return (NkVec2)(nk_vec2_((float)(ctx.Current.Bounds.w), (float)(ctx.Current.Bounds.h)));
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

        public static NkRect nk_window_get_content_region(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null))
                return (NkRect)(nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            return (NkRect)(ctx.Current.Layout.Clip);
        }

        public static NkVec2 nk_window_get_content_region_min(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            return (NkVec2)(nk_vec2_((float)(ctx.Current.Layout.Clip.x), (float)(ctx.Current.Layout.Clip.y)));
        }

        public static NkVec2 nk_window_get_content_region_max(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            return
                (NkVec2)
                    (nk_vec2_((float)(ctx.Current.Layout.Clip.x + ctx.Current.Layout.Clip.w),
                        (float)(ctx.Current.Layout.Clip.y + ctx.Current.Layout.Clip.h)));
        }

        public static NkVec2 nk_window_get_content_region_size(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            return (NkVec2)(nk_vec2_((float)(ctx.Current.Layout.Clip.w), (float)(ctx.Current.Layout.Clip.h)));
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
            return (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(ctx.Current.Bounds)));
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
                        ((nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(iter.Popup.win.Bounds))) != 0)) return (int)(1);
                    if ((iter.Flags & NK_WINDOW_MINIMIZED) != 0)
                    {
                        NkRect header = (NkRect)(iter.Bounds);
                        header.h = (float)(ctx.Style.Font.Height + 2 * ctx.Style.Window.header.padding.y);
                        if ((nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(header))) != 0) return (int)(1);
                    }
                    else if ((nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(iter.Bounds))) != 0)
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

        public static void nk_window_set_bounds(NkContext ctx, char* name, NkRect bounds)
        {
            NkWindow win;
            if (ctx == null) return;
            win = nk_window_find(ctx, name);
            if (win == null) return;
            win.Bounds = (NkRect)(bounds);
        }

        public static void nk_window_set_position(NkContext ctx, char* name, NkVec2 pos)
        {
            NkWindow win = nk_window_find(ctx, name);
            if (win == null) return;
            win.Bounds.x = (float)(pos.x);
            win.Bounds.y = (float)(pos.y);
        }

        public static void nk_window_set_size(NkContext ctx, char* name, NkVec2 size)
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
            nk_push_scissor(_out_, (NkRect)(layout.Clip));
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
            NkVec2 item_spacing = new NkVec2();
            NkColor color = new NkColor();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            layout = win.Layout;
            style = ctx.Style;
            _out_ = win.Buffer;
            color = (NkColor)(style.Window.background);
            item_spacing = (NkVec2)(style.Window.spacing);
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
                NkRect background = new NkRect();
                background.x = (float)(win.Bounds.x);
                background.w = (float)(win.Bounds.w);
                background.y = (float)(layout.AtY - 1.0f);
                background.h = (float)(layout.Row.height + 1.0f);
                nk_fill_rect(_out_, (NkRect)(background), (float)(0), (NkColor)(color));
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
                nk_zero(ptr, (ulong)(sizeof(NkRect)));
            }
        }

        public static void nk_layout_space_push(NkContext ctx, NkRect rect)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            layout.Row.item = (NkRect)(rect);
        }

        public static NkRect nk_layout_space_bounds(NkContext ctx)
        {
            NkRect ret = new NkRect();
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x = (float)(layout.Clip.x);
            ret.y = (float)(layout.Clip.y);
            ret.w = (float)(layout.Clip.w);
            ret.h = (float)(layout.Row.height);
            return (NkRect)(ret);
        }

        public static NkRect nk_layout_widget_bounds(NkContext ctx)
        {
            NkRect ret = new NkRect();
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x = (float)(layout.AtX);
            ret.y = (float)(layout.AtY);
            ret.w = (float)(layout.Bounds.w - ((layout.AtX - layout.Bounds.x) < (0) ? (0) : (layout.AtX - layout.Bounds.x)));
            ret.h = (float)(layout.Row.height);
            return (NkRect)(ret);
        }

        public static NkVec2 nk_layout_space_to_screen(NkContext ctx, NkVec2 ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(layout.AtX - (float)(layout.Offset.x));
            ret.y += (float)(layout.AtY - (float)(layout.Offset.y));
            return (NkVec2)(ret);
        }

        public static NkVec2 nk_layout_space_to_local(NkContext ctx, NkVec2 ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(-layout.AtX + (float)(layout.Offset.x));
            ret.y += (float)(-layout.AtY + (float)(layout.Offset.y));
            return (NkVec2)(ret);
        }

        public static NkRect nk_layout_space_rect_to_screen(NkContext ctx, NkRect ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(layout.AtX - (float)(layout.Offset.x));
            ret.y += (float)(layout.AtY - (float)(layout.Offset.y));
            return (NkRect)(ret);
        }

        public static NkRect nk_layout_space_rect_to_local(NkContext ctx, NkRect ret)
        {
            NkWindow win;
            NkPanel layout;
            win = ctx.Current;
            layout = win.Layout;
            ret.x += (float)(-layout.AtX + (float)(layout.Offset.x));
            ret.y += (float)(-layout.AtY + (float)(layout.Offset.y));
            return (NkRect)(ret);
        }

        public static void nk_panel_alloc_row(NkContext ctx, NkWindow win)
        {
            NkPanel layout = win.Layout;
            NkVec2 spacing = (NkVec2)(ctx.Style.Window.spacing);
            float row_height = (float)(layout.Row.height - spacing.y);
            nk_panel_layout(ctx, win, (float)(row_height), (int)(layout.Row.columns));
        }

        public static int nk_tree_state_base(NkContext ctx, int type, NkImage img, char* title, ref int state)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle style;
            NkCommandBuffer _out_;
            nk_input _in_;
            nk_style_button button;
            int symbol;
            float row_height;
            NkVec2 item_spacing = new NkVec2();
            NkRect header = new NkRect();
            NkRect sym = new NkRect();
            nk_text text = new nk_text();
            uint ws = (uint)(0);
            int widget_state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            _out_ = win.Buffer;
            style = ctx.Style;
            item_spacing = (NkVec2)(style.Window.spacing);
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
                    nk_draw_image(_out_, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
                    text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                }
                else
                {
                    text.background = (NkColor)(background.Data.Color);
                    nk_fill_rect(_out_, (NkRect)(header), (float)(0), (NkColor)(style.Tab.border_color));
                    nk_fill_rect(_out_, (NkRect)(nk_shrink_rect_((NkRect)(header), (float)(style.Tab.border))),
                        (float)(style.Tab.rounding), (NkColor)(background.Data.Color));
                }
            }
            else text.background = (NkColor)(style.Window.background);
            _in_ = ((layout.Flags & NK_WINDOW_ROM) == 0) ? ctx.Input : null;
            _in_ = (((_in_) != null) && ((widget_state) == (NK_WIDGET_VALID))) ? ctx.Input : null;
            if ((nk_button_behavior(ref ws, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
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
                nk_do_button_symbol(ref ws, win.Buffer, (NkRect)(sym), (int)(symbol), (int)(NK_BUTTON_DEFAULT), button, null,
                    style.Font);
                if ((img) != null)
                {
                    sym.x = (float)(sym.x + sym.w + 4 * item_spacing.x);
                    nk_draw_image(win.Buffer, (NkRect)(sym), img, (NkColor)(nk_white));
                    sym.w = (float)(style.Font.Height + style.Tab.spacing.x);
                }
            }

            {
                NkRect label = new NkRect();
                header.w = (float)((header.w) < (sym.w + item_spacing.x) ? (sym.w + item_spacing.x) : (header.w));
                label.x = (float)(sym.x + sym.w + item_spacing.x);
                label.y = (float)(sym.y);
                label.w = (float)(header.w - (sym.w + item_spacing.y + style.Tab.indent));
                label.h = (float)(style.Font.Height);
                text.text = (NkColor)(style.Tab.text);
                text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                nk_widget_text(_out_, (NkRect)(label), title, (int)(nk_strlen(title)), &text, (uint)(NK_TEXT_LEFT), style.Font);
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

        public static int nk_tree_base(NkContext ctx, int type, NkImage img, char* title, int initial_state, char* hash,
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

        public static int nk_tree_state_image_push(NkContext ctx, int type, NkImage img, char* title, ref int state)
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

        public static int nk_tree_image_push_hashed(NkContext ctx, int type, NkImage img, char* title, int initial_state,
            char* hash, int len, int seed)
        {
            return (int)(nk_tree_base(ctx, (int)(type), img, title, (int)(initial_state), hash, (int)(len), (int)(seed)));
        }

        public static void nk_tree_pop(NkContext ctx)
        {
            nk_tree_state_pop(ctx);
        }

        public static NkRect nk_widget_bounds(NkContext ctx)
        {
            NkRect bounds = new NkRect();
            if ((ctx == null) || (ctx.Current == null))
                return (NkRect)(nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            nk_layout_peek(&bounds, ctx);
            return (NkRect)(bounds);
        }

        public static NkVec2 nk_widget_position(NkContext ctx)
        {
            NkRect bounds = new NkRect();
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            nk_layout_peek(&bounds, ctx);
            return (NkVec2)(nk_vec2_((float)(bounds.x), (float)(bounds.y)));
        }

        public static NkVec2 nk_widget_size(NkContext ctx)
        {
            NkRect bounds = new NkRect();
            if ((ctx == null) || (ctx.Current == null)) return (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            nk_layout_peek(&bounds, ctx);
            return (NkVec2)(nk_vec2_((float)(bounds.w), (float)(bounds.h)));
        }

        public static float nk_widget_width(NkContext ctx)
        {
            NkRect bounds = new NkRect();
            if ((ctx == null) || (ctx.Current == null)) return (float)(0);
            nk_layout_peek(&bounds, ctx);
            return (float)(bounds.w);
        }

        public static float nk_widget_height(NkContext ctx)
        {
            NkRect bounds = new NkRect();
            if ((ctx == null) || (ctx.Current == null)) return (float)(0);
            nk_layout_peek(&bounds, ctx);
            return (float)(bounds.h);
        }

        public static int nk_widget_is_hovered(NkContext ctx)
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkRect bounds = new NkRect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Active != ctx.Current)) return (int)(0);
            c = (NkRect)(ctx.Current.Layout.Clip);
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
            return (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(bounds)));
        }

        public static int nk_widget_is_mouse_clicked(NkContext ctx, int btn)
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkRect bounds = new NkRect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Active != ctx.Current)) return (int)(0);
            c = (NkRect)(ctx.Current.Layout.Clip);
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
            return (int)(nk_input_mouse_clicked(ctx.Input, (int)(btn), (NkRect)(bounds)));
        }

        public static int nk_widget_has_mouse_click_down(NkContext ctx, int btn, int down)
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkRect bounds = new NkRect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Active != ctx.Current)) return (int)(0);
            c = (NkRect)(ctx.Current.Layout.Clip);
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
            return (int)(nk_input_has_mouse_click_down_in_rect(ctx.Input, (int)(btn), (NkRect)(bounds), (int)(down)));
        }

        public static void nk_spacing(NkContext ctx, int cols)
        {
            NkWindow win;
            NkPanel layout;
            NkRect none = new NkRect();
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

        public static void nk_text_colored(NkContext ctx, char* str, int len, uint alignment, NkColor color)
        {
            NkWindow win;
            NkStyle style;
            NkVec2 item_padding = new NkVec2();
            NkRect bounds = new NkRect();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            style = ctx.Style;
            nk_panel_alloc_space(&bounds, ctx);
            item_padding = (NkVec2)(style.Text.padding);
            text.padding.x = (float)(item_padding.x);
            text.padding.y = (float)(item_padding.y);
            text.background = (NkColor)(style.Window.background);
            text.text = (NkColor)(color);
            nk_widget_text(win.Buffer, (NkRect)(bounds), str, (int)(len), &text, (uint)(alignment), style.Font);
        }

        public static void nk_text_wrap_colored(NkContext ctx, char* str, int len, NkColor color)
        {
            NkWindow win;
            NkStyle style;
            NkVec2 item_padding = new NkVec2();
            NkRect bounds = new NkRect();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            style = ctx.Style;
            nk_panel_alloc_space(&bounds, ctx);
            item_padding = (NkVec2)(style.Text.padding);
            text.padding.x = (float)(item_padding.x);
            text.padding.y = (float)(item_padding.y);
            text.background = (NkColor)(style.Window.background);
            text.text = (NkColor)(color);
            nk_widget_text_wrap(win.Buffer, (NkRect)(bounds), str, (int)(len), &text, style.Font);
        }

        public static void nk_text_(NkContext ctx, char* str, int len, uint alignment)
        {
            if (ctx == null) return;
            nk_text_colored(ctx, str, (int)(len), (uint)(alignment), (NkColor)(ctx.Style.Text.color));
        }

        public static void nk_text_wrap(NkContext ctx, char* str, int len)
        {
            if (ctx == null) return;
            nk_text_wrap_colored(ctx, str, (int)(len), (NkColor)(ctx.Style.Text.color));
        }

        public static void nk_label(NkContext ctx, char* str, uint alignment)
        {
            nk_text_(ctx, str, (int)(nk_strlen(str)), (uint)(alignment));
        }

        public static void nk_label_colored(NkContext ctx, char* str, uint align, NkColor color)
        {
            nk_text_colored(ctx, str, (int)(nk_strlen(str)), (uint)(align), (NkColor)(color));
        }

        public static void nk_label_wrap(NkContext ctx, char* str)
        {
            nk_text_wrap(ctx, str, (int)(nk_strlen(str)));
        }

        public static void nk_label_colored_wrap(NkContext ctx, char* str, NkColor color)
        {
            nk_text_wrap_colored(ctx, str, (int)(nk_strlen(str)), (NkColor)(color));
        }

        public static void nk_image_(NkContext ctx, NkImage img)
        {
            NkWindow win;
            NkRect bounds = new NkRect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            if (nk_widget(&bounds, ctx) == 0) return;
            nk_draw_image(win.Buffer, (NkRect)(bounds), img, (NkColor)(nk_white));
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
            NkRect bounds = new NkRect();
            int state;
            if ((((style == null) || (ctx == null)) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), title, (int)(len),
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

        public static int nk_button_color(NkContext ctx, NkColor color)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_style_button button = new nk_style_button();
            int ret = (int)(0);
            NkRect bounds = new NkRect();
            NkRect content = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            button = (nk_style_button)(ctx.Style.Button);
            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(color)));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(color)));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(color)));
            ret =
                (int)
                    (nk_do_button(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), button, _in_, (int)(ctx.ButtonBehavior),
                        &content));
            nk_draw_button(win.Buffer, &bounds, (uint)(ctx.LastWidgetState), button);
            return (int)(ret);
        }

        public static int nk_button_symbol_styled(NkContext ctx, nk_style_button style, int symbol)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (int)(symbol),
                        (int)(ctx.ButtonBehavior), style, _in_, ctx.Style.Font));
        }

        public static int nk_button_symbol(NkContext ctx, int symbol)
        {
            if (ctx == null) return (int)(0);
            return (int)(nk_button_symbol_styled(ctx, ctx.Style.Button, (int)(symbol)));
        }

        public static int nk_button_image_styled(NkContext ctx, nk_style_button style, NkImage img)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (NkImage)(img),
                        (int)(ctx.ButtonBehavior), style, _in_));
        }

        public static int nk_button_image(NkContext ctx, NkImage img)
        {
            if (ctx == null) return (int)(0);
            return (int)(nk_button_image_styled(ctx, ctx.Style.Button, (NkImage)(img)));
        }

        public static int nk_button_symbol_text_styled(NkContext ctx, nk_style_button style, int symbol, char* text, int len,
            uint align)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (int)(symbol), text,
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

        public static int nk_button_image_text_styled(NkContext ctx, nk_style_button style, NkImage img, char* text, int len,
            uint align)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (NkImage)(img), text,
                        (int)(len), (uint)(align), (int)(ctx.ButtonBehavior), style, ctx.Style.Font, _in_));
        }

        public static int nk_button_image_text(NkContext ctx, NkImage img, char* text, int len, uint align)
        {
            return
                (int)(nk_button_image_text_styled(ctx, ctx.Style.Button, (NkImage)(img), text, (int)(len), (uint)(align)));
        }

        public static int nk_button_image_label(NkContext ctx, NkImage img, char* label, uint align)
        {
            return (int)(nk_button_image_text(ctx, (NkImage)(img), label, (int)(nk_strlen(label)), (uint)(align)));
        }

        public static int nk_button_image_label_styled(NkContext ctx, nk_style_button style, NkImage img, char* label,
            uint text_alignment)
        {
            return
                (int)
                    (nk_button_image_text_styled(ctx, style, (NkImage)(img), label, (int)(nk_strlen(label)), (uint)(text_alignment)));
        }

        public static int nk_selectable_text(NkContext ctx, char* str, int len, uint align, ref int value)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            int state;
            NkRect bounds = new NkRect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null))) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_selectable(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), str, (int)(len), (uint)(align),
                        ref value, style.Selectable, _in_, style.Font));
        }

        public static int nk_selectable_image_text(NkContext ctx, NkImage img, char* str, int len, uint align, ref int value)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            int state;
            NkRect bounds = new NkRect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null))) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_selectable_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), str, (int)(len), (uint)(align),
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

        public static int nk_selectable_image_label(NkContext ctx, NkImage img, char* str, uint align, ref int value)
        {
            return
                (int)(nk_selectable_image_text(ctx, (NkImage)(img), str, (int)(nk_strlen(str)), (uint)(align), ref value));
        }

        public static int nk_select_label(NkContext ctx, char* str, uint align, int value)
        {
            nk_selectable_text(ctx, str, (int)(nk_strlen(str)), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_select_image_label(NkContext ctx, NkImage img, char* str, uint align, int value)
        {
            nk_selectable_image_text(ctx, (NkImage)(img), str, (int)(nk_strlen(str)), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_select_image_text(NkContext ctx, NkImage img, char* str, int len, uint align, int value)
        {
            nk_selectable_image_text(ctx, (NkImage)(img), str, (int)(len), (uint)(align), ref value);
            return (int)(value);
        }

        public static int nk_check_text(NkContext ctx, char* text, int len, int active)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(active);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(active);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            nk_do_toggle(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), &active, text, (int)(len),
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
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(is_active);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(state);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            nk_do_toggle(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), &is_active, text, (int)(len),
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
            NkRect bounds = new NkRect();
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
                    (nk_do_slider(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (float)(min_value), (float)(old_value),
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
            NkRect bounds = new NkRect();
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
                    (nk_do_progress(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (ulong)(*cur), (ulong)(max),
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
            NkRect bounds = new NkRect();
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
                    (nk_do_edit(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (uint)(flags), filter, edit, style.Edit,
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

        public static int nk_color_pick(NkContext ctx, NkColorF* color, int fmt)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle config;
            nk_input _in_;
            int state;
            NkRect bounds = new NkRect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (color == null)) return (int)(0);
            win = ctx.Current;
            config = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_color_picker(ref ctx.LastWidgetState, win.Buffer, color, (int)(fmt), (NkRect)(bounds),
                        (NkVec2)(nk_vec2_((float)(0), (float)(0))), _in_, config.Font));
        }

        public static NkColorF nk_color_picker(NkContext ctx, NkColorF color, int fmt)
        {
            nk_color_pick(ctx, &color, (int)(fmt));
            return (NkColorF)(color);
        }

        public static int nk_chart_begin_colored(NkContext ctx, int type, NkColor color, NkColor highlight, int count,
            float min_value, float max_value)
        {
            NkWindow win;
            NkChart chart;
            NkStyle config;
            nk_style_chart style;
            NkStyleItem background;
            NkRect bounds = new NkRect();
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
                slot.color = (NkColor)(color);
                slot.highlight = (NkColor)(highlight);
                slot.min = (float)((min_value) < (max_value) ? (min_value) : (max_value));
                slot.max = (float)((min_value) < (max_value) ? (max_value) : (min_value));
                slot.range = (float)(slot.max - slot.min);
            }

            background = style.background;
            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                nk_draw_image(win.Buffer, (NkRect)(bounds), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                nk_fill_rect(win.Buffer, (NkRect)(bounds), (float)(style.rounding), (NkColor)(style.border_color));
                nk_fill_rect(win.Buffer, (NkRect)(nk_shrink_rect_((NkRect)(bounds), (float)(style.border))),
                    (float)(style.rounding), (NkColor)(style.background.Data.Color));
            }

            return (int)(1);
        }

        public static int nk_chart_begin(NkContext ctx, int type, int count, float min_value, float max_value)
        {
            return
                (int)
                    (nk_chart_begin_colored(ctx, (int)(type), (NkColor)(ctx.Style.Chart.color),
                        (NkColor)(ctx.Style.Chart.selected_color), (int)(count), (float)(min_value), (float)(max_value)));
        }

        public static void nk_chart_add_slot_colored(NkContext ctx, int type, NkColor color, NkColor highlight, int count,
            float min_value, float max_value)
        {
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            if ((ctx.Current.Layout.Chart.Slot) >= (4)) return;
            {
                NkChart chart = ctx.Current.Layout.Chart;
                nk_chart_slot slot = chart.Slots[chart.Slot++];
                slot.type = (int)(type);
                slot.count = (int)(count);
                slot.color = (NkColor)(color);
                slot.highlight = (NkColor)(highlight);
                slot.min = (float)((min_value) < (max_value) ? (min_value) : (max_value));
                slot.max = (float)((min_value) < (max_value) ? (max_value) : (min_value));
                slot.range = (float)(slot.max - slot.min);
            }

        }

        public static void nk_chart_add_slot(NkContext ctx, int type, int count, float min_value, float max_value)
        {
            nk_chart_add_slot_colored(ctx, (int)(type), (NkColor)(ctx.Style.Chart.color),
                (NkColor)(ctx.Style.Chart.selected_color), (int)(count), (float)(min_value), (float)(max_value));
        }

        public static uint nk_chart_push_line(NkContext ctx, NkWindow win, NkChart g, float value, int slot)
        {
            NkPanel layout = win.Layout;
            nk_input i = ctx.Input;
            NkCommandBuffer _out_ = win.Buffer;
            uint ret = (uint)(0);
            NkVec2 cur = new NkVec2();
            NkRect bounds = new NkRect();
            NkColor color = new NkColor();
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
                color = (NkColor)(g.Slots[slot].color);
                if (((layout.Flags & NK_WINDOW_ROM) == 0) &&
                    ((((g.Slots[slot].last.x - 3) <= (i.mouse.Pos.x)) && ((i.mouse.Pos.x) < (g.Slots[slot].last.x - 3 + 6))) &&
                     (((g.Slots[slot].last.y - 3) <= (i.mouse.Pos.y)) && ((i.mouse.Pos.y) < (g.Slots[slot].last.y - 3 + 6)))))
                {
                    ret = (uint)((nk_input_is_mouse_hovering_rect(i, (NkRect)(bounds))) != 0 ? NK_CHART_HOVERING : 0);
                    ret |=
                        (uint)
                            ((((i.mouse.Buttons[NK_BUTTON_LEFT].down) != 0) && ((i.mouse.Buttons[NK_BUTTON_LEFT].clicked) != 0))
                                ? NK_CHART_CLICKED
                                : 0);
                    color = (NkColor)(g.Slots[slot].highlight);
                }
                nk_fill_rect(_out_, (NkRect)(bounds), (float)(0), (NkColor)(color));
                g.Slots[slot].index += (int)(1);
                return (uint)(ret);
            }

            color = (NkColor)(g.Slots[slot].color);
            cur.x = (float)(g.X + (step * (float)(g.Slots[slot].index)));
            cur.y = (float)((g.Y + g.H) - (ratio * g.H));
            nk_stroke_line(_out_, (float)(g.Slots[slot].last.x), (float)(g.Slots[slot].last.y), (float)(cur.x),
                (float)(cur.y), (float)(1.0f), (NkColor)(color));
            bounds.x = (float)(cur.x - 3);
            bounds.y = (float)(cur.y - 3);
            bounds.w = (float)(bounds.h = (float)(6));
            if ((layout.Flags & NK_WINDOW_ROM) == 0)
            {
                if ((nk_input_is_mouse_hovering_rect(i, (NkRect)(bounds))) != 0)
                {
                    ret = (uint)(NK_CHART_HOVERING);
                    ret |=
                        (uint)
                            (((i.mouse.Buttons[NK_BUTTON_LEFT].down == 0) && ((i.mouse.Buttons[NK_BUTTON_LEFT].clicked) != 0))
                                ? NK_CHART_CLICKED
                                : 0);
                    color = (NkColor)(g.Slots[slot].highlight);
                }
            }

            nk_fill_rect(_out_, (NkRect)(nk_rect_((float)(cur.x - 2), (float)(cur.y - 2), (float)(4), (float)(4))),
                (float)(0), (NkColor)(color));
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
            NkColor color = new NkColor();
            NkRect item = new NkRect();
            if ((chart.Slots[slot].index) >= (chart.Slots[slot].count)) return (uint)(nk_false);
            if ((chart.Slots[slot].count) != 0)
            {
                float padding = (float)(chart.Slots[slot].count - 1);
                item.w = (float)((chart.W - padding) / (float)(chart.Slots[slot].count));
            }

            color = (NkColor)(chart.Slots[slot].color);
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
                color = (NkColor)(chart.Slots[slot].highlight);
            }

            nk_fill_rect(_out_, (NkRect)(item), (float)(0), (NkColor)(color));
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
            NkRect bounds = new NkRect();
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

            panel.Bounds = (NkRect)(bounds);
            panel.Flags = (uint)(flags);
            panel.Scrollbar.x = offset.x;
            panel.Scrollbar.y = offset.y;
            panel.Buffer = (NkCommandBuffer)(win.Buffer);
            panel.Layout = (NkPanel)(nk_create_panel(ctx));
            ctx.Current = panel;
            nk_panel_begin(ctx, (flags & NK_WINDOW_TITLE) != 0 ? title : null, (int)(NK_PANEL_GROUP));
            win.Buffer = (NkCommandBuffer)(panel.Buffer);
            win.Buffer.Clip = (NkRect)(panel.Layout.Clip);
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
            NkRect clip = new NkRect();
            NkWindow pan = new NkWindow();
            NkVec2 panel_padding = new NkVec2();
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            g = win.Layout;
            parent = g.Parent;

            panel_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (int)(NK_PANEL_GROUP)));
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
            nk_push_scissor(pan.Buffer, (NkRect)(clip));
            nk_end(ctx);
            win.Buffer = (NkCommandBuffer)(pan.Buffer);
            nk_push_scissor(win.Buffer, (NkRect)(parent.Clip));
            ctx.Current = win;
            win.Layout = parent;
            g.Bounds = (NkRect)(pan.Bounds);
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
            NkVec2 item_spacing = new NkVec2();
            if (((ctx == null) || (view == null)) || (title == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            item_spacing = (NkVec2)(style.Window.spacing);
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

        public static int nk_popup_begin(NkContext ctx, int type, char* title, uint flags, NkRect rect)
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
            popup.Bounds = (NkRect)(rect);
            popup.Seq = (uint)(ctx.Seq);
            popup.Layout = (NkPanel)(nk_create_panel(ctx));
            popup.Flags = (uint)(flags);
            popup.Flags |= (uint)(NK_WINDOW_BORDER);
            if ((type) == (NK_POPUP_DYNAMIC)) popup.Flags |= (uint)(NK_WINDOW_DYNAMIC);
            nk_start_popup(ctx, win);
            popup.Buffer = (NkCommandBuffer)(win.Buffer);
            nk_push_scissor(popup.Buffer, (NkRect)(nk_null_rect));
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

        public static int nk_nonblock_begin(NkContext ctx, uint flags, NkRect body, NkRect header, int panel_type)
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
                in_body = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(body)));
                in_header = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(header)));
                if (((pressed) != 0) && ((in_body == 0) || ((in_header) != 0))) is_active = (int)(nk_false);
            }

            win.Popup.header = (NkRect)(header);
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

            popup.Bounds = (NkRect)(body);
            popup.Parent = win;
            popup.Layout = (NkPanel)(nk_create_panel(ctx));
            popup.Flags = (uint)(flags);
            popup.Flags |= (uint)(NK_WINDOW_BORDER);
            popup.Flags |= (uint)(NK_WINDOW_DYNAMIC);
            popup.Seq = (uint)(ctx.Seq);
            win.Popup.active = (int)(1);
            nk_start_popup(ctx, win);
            popup.Buffer = (NkCommandBuffer)(win.Buffer);
            nk_push_scissor(popup.Buffer, (NkRect)(nk_null_rect));
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

            nk_push_scissor(popup.Buffer, (NkRect)(nk_null_rect));
            nk_end(ctx);
            win.Buffer = (NkCommandBuffer)(popup.Buffer);
            nk_finish_popup(ctx, win);
            ctx.Current = win;
            nk_push_scissor(win.Buffer, (NkRect)(win.Layout.Clip));
        }

        public static int nk_tooltip_begin(NkContext ctx, float width)
        {
            int x;
            int y;
            int w;
            int h;
            NkWindow win;
            nk_input _in_;
            NkRect bounds = new NkRect();
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
                        (uint)(NK_WINDOW_NO_SCROLLBAR | NK_WINDOW_BORDER), (NkRect)(bounds)));
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
            NkVec2 padding = new NkVec2();
            int text_len;
            float text_width;
            float text_height;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (text == null)) return;
            style = ctx.Style;
            padding = (NkVec2)(style.Window.padding);
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

        public static int nk_contextual_begin(NkContext ctx, uint flags, NkVec2 size, NkRect trigger_bounds)
        {
            NkWindow win;
            NkWindow popup;
            NkRect body = new NkRect();
            NkRect null_rect = new NkRect();
            int is_clicked = (int)(0);
            int is_active = (int)(0);
            int is_open = (int)(0);
            int ret = (int)(0);
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            ++win.Popup.con_count;
            popup = win.Popup.win;
            is_open = (int)(((popup) != null) && ((win.Popup.type) == (NK_PANEL_CONTEXTUAL)) ? 1 : 0);
            is_clicked = (int)(nk_input_mouse_clicked(ctx.Input, (int)(NK_BUTTON_RIGHT), (NkRect)(trigger_bounds)));
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
                    (nk_nonblock_begin(ctx, (uint)(flags | NK_WINDOW_NO_SCROLLBAR), (NkRect)(body), (NkRect)(null_rect),
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
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget_fitting(&bounds, ctx, (NkVec2)(style.ContextualButton.padding)));
            if (state == 0) return (int)(nk_false);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), text, (int)(len), (uint)(alignment),
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

        public static int nk_contextual_item_image_text(NkContext ctx, NkImage img, char* text, int len, uint align)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget_fitting(&bounds, ctx, (NkVec2)(style.ContextualButton.padding)));
            if (state == 0) return (int)(nk_false);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (NkImage)(img), text,
                    (int)(len), (uint)(align), (int)(NK_BUTTON_DEFAULT), style.ContextualButton, style.Font, _in_)) != 0)
            {
                nk_contextual_close(ctx);
                return (int)(nk_true);
            }

            return (int)(nk_false);
        }

        public static int nk_contextual_item_image_label(NkContext ctx, NkImage img, char* label, uint align)
        {
            return (int)(nk_contextual_item_image_text(ctx, (NkImage)(img), label, (int)(nk_strlen(label)), (uint)(align)));
        }

        public static int nk_contextual_item_symbol_text(NkContext ctx, int symbol, char* text, int len, uint align)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            int state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget_fitting(&bounds, ctx, (NkVec2)(style.ContextualButton.padding)));
            if (state == 0) return (int)(nk_false);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (int)(symbol), text,
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
                NkRect body = new NkRect();
                if ((panel.AtY) < (panel.Bounds.y + panel.Bounds.h))
                {
                    NkVec2 padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (int)(panel.Type)));
                    body = (NkRect)(panel.Bounds);
                    body.y = (float)(panel.AtY + panel.FooterHeight + panel.Border + padding.y + panel.Row.height);
                    body.h = (float)((panel.Bounds.y + panel.Bounds.h) - body.y);
                }
                {
                    int pressed = (int)(nk_input_is_mouse_pressed(ctx.Input, (int)(NK_BUTTON_LEFT)));
                    int in_body = (int)(nk_input_is_mouse_hovering_rect(ctx.Input, (NkRect)(body)));
                    if (((pressed) != 0) && ((in_body) != 0)) popup.Flags |= (uint)(NK_WINDOW_HIDDEN);
                }
            }

            if ((popup.Flags & NK_WINDOW_HIDDEN) != 0) popup.Seq = (uint)(0);
            nk_popup_end(ctx);
            return;
        }

        public static int nk_combo_begin(NkContext ctx, NkWindow win, NkVec2 size, int is_clicked, NkRect header)
        {
            NkWindow popup;
            int is_open = (int)(0);
            int is_active = (int)(0);
            NkRect body = new NkRect();
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
                nk_nonblock_begin(ctx, (uint)(0), (NkRect)(body),
                    (NkRect)
                        ((((is_clicked) != 0) && ((is_open) != 0)) ? nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)) : header),
                    (int)(NK_PANEL_COMBO)) == 0) return (int)(0);
            win.Popup.type = (int)(NK_PANEL_COMBO);
            win.Popup.name = (uint)(hash);
            return (int)(1);
        }

        public static int nk_combo_begin_text(NkContext ctx, char* selected, int len, NkVec2 size)
        {
            nk_input _in_;
            NkWindow win;
            NkStyle style;
            int s;
            int is_clicked = (int)(nk_false);
            NkRect header = new NkRect();
            NkStyleItem background;
            nk_text text = new nk_text();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (selected == null))
                return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                text.text = (NkColor)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                text.text = (NkColor)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                text.text = (NkColor)(style.Combo.label_normal);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                nk_fill_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect label = new NkRect();
                NkRect button = new NkRect();
                NkRect content = new NkRect();
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
                text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(header.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)(button.x - (style.Combo.content_padding.x + style.Combo.spacing.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                nk_widget_text(win.Buffer, (NkRect)(label), selected, (int)(len), &text, (uint)(NK_TEXT_LEFT), ctx.Style.Font);
                nk_draw_button_symbol(win.Buffer, &button, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (NkVec2)(size), (int)(is_clicked), (NkRect)(header)));
        }

        public static int nk_combo_begin_label(NkContext ctx, char* selected, NkVec2 size)
        {
            return (int)(nk_combo_begin_text(ctx, selected, (int)(nk_strlen(selected)), (NkVec2)(size)));
        }

        public static int nk_combo_begin_color(NkContext ctx, NkColor color, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0) background = style.Combo.active;
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) background = style.Combo.hover;
            else background = style.Combo.normal;
            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                nk_fill_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                NkRect bounds = new NkRect();
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
                nk_fill_rect(win.Buffer, (NkRect)(bounds), (float)(0), (NkColor)(color));
                nk_draw_button_symbol(win.Buffer, &button, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (NkVec2)(size), (int)(is_clicked), (NkRect)(header)));
        }

        public static int nk_combo_begin_symbol(NkContext ctx, int symbol, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            NkColor sym_background = new NkColor();
            NkColor symbol_color = new NkColor();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                symbol_color = (NkColor)(style.Combo.symbol_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                symbol_color = (NkColor)(style.Combo.symbol_hover);
            }
            else
            {
                background = style.Combo.normal;
                symbol_color = (NkColor)(style.Combo.symbol_hover);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                sym_background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                sym_background = (NkColor)(background.Data.Color);
                nk_fill_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect bounds = new NkRect();
                NkRect content = new NkRect();
                NkRect button = new NkRect();
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
                nk_draw_symbol(win.Buffer, (int)(symbol), (NkRect)(bounds), (NkColor)(sym_background),
                    (NkColor)(symbol_color), (float)(1.0f), style.Font);
                nk_draw_button_symbol(win.Buffer, &bounds, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (NkVec2)(size), (int)(is_clicked), (NkRect)(header)));
        }

        public static int nk_combo_begin_symbol_text(NkContext ctx, char* selected, int len, int symbol, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            NkColor symbol_color = new NkColor();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if (s == 0) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                symbol_color = (NkColor)(style.Combo.symbol_active);
                text.text = (NkColor)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                symbol_color = (NkColor)(style.Combo.symbol_hover);
                text.text = (NkColor)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                symbol_color = (NkColor)(style.Combo.symbol_normal);
                text.text = (NkColor)(style.Combo.label_normal);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                nk_fill_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                NkRect label = new NkRect();
                NkRect image = new NkRect();
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
                nk_draw_symbol(win.Buffer, (int)(symbol), (NkRect)(image), (NkColor)(text.background),
                    (NkColor)(symbol_color), (float)(1.0f), style.Font);
                text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(image.x + image.w + style.Combo.spacing.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)((button.x - style.Combo.content_padding.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                nk_widget_text(win.Buffer, (NkRect)(label), selected, (int)(len), &text, (uint)(NK_TEXT_LEFT), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (NkVec2)(size), (int)(is_clicked), (NkRect)(header)));
        }

        public static int nk_combo_begin_image(NkContext ctx, NkImage img, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            int is_clicked = (int)(nk_false);
            int s;
            NkStyleItem background;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            s = (int)(nk_widget(&header, ctx));
            if ((s) == (NK_WIDGET_INVALID)) return (int)(0);
            _in_ = (((win.Layout.Flags & NK_WINDOW_ROM) != 0) || ((s) == (NK_WIDGET_ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0) background = style.Combo.active;
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0) background = style.Combo.hover;
            else background = style.Combo.normal;
            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                nk_fill_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect bounds = new NkRect();
                NkRect content = new NkRect();
                NkRect button = new NkRect();
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
                nk_draw_image(win.Buffer, (NkRect)(bounds), img, (NkColor)(nk_white));
                nk_draw_button_symbol(win.Buffer, &bounds, &content, (uint)(ctx.LastWidgetState), ctx.Style.Combo.button,
                    (int)(sym), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (NkVec2)(size), (int)(is_clicked), (NkRect)(header)));
        }

        public static int nk_combo_begin_image_text(NkContext ctx, char* selected, int len, NkImage img, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
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
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, (int)(NK_BUTTON_DEFAULT))) != 0)
                is_clicked = (int)(nk_true);
            if ((ctx.LastWidgetState & NK_WIDGET_STATE_ACTIVED) != 0)
            {
                background = style.Combo.active;
                text.text = (NkColor)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & NK_WIDGET_STATE_HOVER) != 0)
            {
                background = style.Combo.hover;
                text.text = (NkColor)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                text.text = (NkColor)(style.Combo.label_normal);
            }

            if ((background.Type) == (NK_STYLE_ITEM_IMAGE))
            {
                text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                nk_draw_image(win.Buffer, (NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                nk_fill_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(win.Buffer, (NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                NkRect label = new NkRect();
                NkRect image = new NkRect();
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
                nk_draw_image(win.Buffer, (NkRect)(image), img, (NkColor)(nk_white));
                text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(image.x + image.w + style.Combo.spacing.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)((button.x - style.Combo.content_padding.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                nk_widget_text(win.Buffer, (NkRect)(label), selected, (int)(len), &text, (uint)(NK_TEXT_LEFT), style.Font);
            }

            return (int)(nk_combo_begin(ctx, win, (NkVec2)(size), (int)(is_clicked), (NkRect)(header)));
        }

        public static int nk_combo_begin_symbol_label(NkContext ctx, char* selected, int type, NkVec2 size)
        {
            return (int)(nk_combo_begin_symbol_text(ctx, selected, (int)(nk_strlen(selected)), (int)(type), (NkVec2)(size)));
        }

        public static int nk_combo_begin_image_label(NkContext ctx, char* selected, NkImage img, NkVec2 size)
        {
            return
                (int)(nk_combo_begin_image_text(ctx, selected, (int)(nk_strlen(selected)), (NkImage)(img), (NkVec2)(size)));
        }

        public static int nk_combo_item_text(NkContext ctx, char* text, int len, uint align)
        {
            return (int)(nk_contextual_item_text(ctx, text, (int)(len), (uint)(align)));
        }

        public static int nk_combo_item_label(NkContext ctx, char* label, uint align)
        {
            return (int)(nk_contextual_item_label(ctx, label, (uint)(align)));
        }

        public static int nk_combo_item_image_text(NkContext ctx, NkImage img, char* text, int len, uint alignment)
        {
            return (int)(nk_contextual_item_image_text(ctx, (NkImage)(img), text, (int)(len), (uint)(alignment)));
        }

        public static int nk_combo_item_image_label(NkContext ctx, NkImage img, char* text, uint alignment)
        {
            return (int)(nk_contextual_item_image_label(ctx, (NkImage)(img), text, (uint)(alignment)));
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

        public static int nk_combo(NkContext ctx, char** items, int count, int selected, int item_height, NkVec2 size)
        {
            int i = (int)(0);
            int max_height;
            NkVec2 item_spacing = new NkVec2();
            NkVec2 window_padding = new NkVec2();
            if (((ctx == null) || (items == null)) || (count == 0)) return (int)(selected);
            item_spacing = (NkVec2)(ctx.Style.Window.spacing);
            window_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (int)(ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            if ((nk_combo_begin_label(ctx, items[selected], (NkVec2)(size))) != 0)
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
            int count, int item_height, NkVec2 size)
        {
            int i;
            int max_height;
            NkVec2 item_spacing = new NkVec2();
            NkVec2 window_padding = new NkVec2();
            char* current_item;
            char* iter;
            ;
            int length = (int)(0);
            if ((ctx == null) || (items_separated_by_separator == null)) return (int)(selected);
            item_spacing = (NkVec2)(ctx.Style.Window.spacing);
            window_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (int)(ctx.Current.Layout.Type)));
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
            if ((nk_combo_begin_text(ctx, current_item, (int)(length), (NkVec2)(size))) != 0)
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
            int item_height, NkVec2 size)
        {
            return
                (int)
                    (nk_combo_separator(ctx, items_separated_by_zeros, (int)('\0'), (int)(selected), (int)(count),
                        (int)(item_height), (NkVec2)(size)));
        }

        public static int nk_combo_callback(NkContext ctx, NkComboCallback item_getter, void* userdata, int selected,
            int count, int item_height, NkVec2 size)
        {
            int i;
            int max_height;
            NkVec2 item_spacing = new NkVec2();
            NkVec2 window_padding = new NkVec2();
            char* item;
            if ((ctx == null) || (item_getter == null)) return (int)(selected);
            item_spacing = (NkVec2)(ctx.Style.Window.spacing);
            window_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (int)(ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            item_getter(userdata, (int)(selected), &item);
            if ((nk_combo_begin_label(ctx, item, (NkVec2)(size))) != 0)
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

        public static void nk_combobox(NkContext ctx, char** items, int count, int* selected, int item_height, NkVec2 size)
        {
            *selected = (int)(nk_combo(ctx, items, (int)(count), (int)(*selected), (int)(item_height), (NkVec2)(size)));
        }

        public static void nk_combobox_string(NkContext ctx, char* items_separated_by_zeros, int* selected, int count,
            int item_height, NkVec2 size)
        {
            *selected =
                (int)
                    (nk_combo_string(ctx, items_separated_by_zeros, (int)(*selected), (int)(count), (int)(item_height),
                        (NkVec2)(size)));
        }

        public static void nk_combobox_separator(NkContext ctx, char* items_separated_by_separator, int separator,
            int* selected, int count, int item_height, NkVec2 size)
        {
            *selected =
                (int)
                    (nk_combo_separator(ctx, items_separated_by_separator, (int)(separator), (int)(*selected), (int)(count),
                        (int)(item_height), (NkVec2)(size)));
        }

        public static void nk_combobox_callback(NkContext ctx, NkComboCallback item_getter, void* userdata, int* selected,
            int count, int item_height, NkVec2 size)
        {
            *selected =
                (int)
                    (nk_combo_callback(ctx, item_getter, userdata, (int)(*selected), (int)(count), (int)(item_height),
                        (NkVec2)(size)));
        }

        public static int nk_menu_begin(NkContext ctx, NkWindow win, char* id, int is_clicked, NkRect header, NkVec2 size)
        {
            int is_open = (int)(0);
            int is_active = (int)(0);
            NkRect body = new NkRect();
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
                nk_nonblock_begin(ctx, (uint)(NK_WINDOW_NO_SCROLLBAR), (NkRect)(body), (NkRect)(header), (int)(NK_PANEL_MENU)) ==
                0) return (int)(0);
            win.Popup.type = (int)(NK_PANEL_MENU);
            win.Popup.name = (uint)(hash);
            return (int)(1);
        }

        public static int nk_menu_begin_text(NkContext ctx, char* title, int len, uint align, NkVec2 size)
        {
            NkWindow win;
            nk_input _in_;
            NkRect header = new NkRect();
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), title, (int)(len), (uint)(align),
                    (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, _in_, ctx.Style.Font)) != 0) is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, title, (int)(is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static int nk_menu_begin_label(NkContext ctx, char* text, uint align, NkVec2 size)
        {
            return (int)(nk_menu_begin_text(ctx, text, (int)(nk_strlen(text)), (uint)(align), (NkVec2)(size)));
        }

        public static int nk_menu_begin_image(NkContext ctx, char* id, NkImage img, NkVec2 size)
        {
            NkWindow win;
            NkRect header = new NkRect();
            nk_input _in_;
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (NkImage)(img),
                    (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, _in_)) != 0) is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, id, (int)(is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static int nk_menu_begin_symbol(NkContext ctx, char* id, int sym, NkVec2 size)
        {
            NkWindow win;
            nk_input _in_;
            NkRect header = new NkRect();
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (int)(sym),
                    (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, _in_, ctx.Style.Font)) != 0) is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, id, (int)(is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static int nk_menu_begin_image_text(NkContext ctx, char* title, int len, uint align, NkImage img,
            NkVec2 size)
        {
            NkWindow win;
            NkRect header = new NkRect();
            nk_input _in_;
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (NkImage)(img), title,
                    (int)(len), (uint)(align), (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, ctx.Style.Font, _in_)) != 0)
                is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, title, (int)(is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static int nk_menu_begin_image_label(NkContext ctx, char* title, uint align, NkImage img, NkVec2 size)
        {
            return
                (int)
                    (nk_menu_begin_image_text(ctx, title, (int)(nk_strlen(title)), (uint)(align), (NkImage)(img), (NkVec2)(size)));
        }

        public static int nk_menu_begin_symbol_text(NkContext ctx, char* title, int len, uint align, int sym, NkVec2 size)
        {
            NkWindow win;
            NkRect header = new NkRect();
            nk_input _in_;
            int is_clicked = (int)(nk_false);
            uint state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            win = ctx.Current;
            state = (uint)(nk_widget(&header, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (NK_WIDGET_ROM)) || ((win.Layout.Flags & NK_WINDOW_ROM) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (int)(sym), title, (int)(len),
                    (uint)(align), (int)(NK_BUTTON_DEFAULT), ctx.Style.MenuButton, ctx.Style.Font, _in_)) != 0)
                is_clicked = (int)(nk_true);
            return (int)(nk_menu_begin(ctx, win, title, (int)(is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static int nk_menu_begin_symbol_label(NkContext ctx, char* title, uint align, int sym, NkVec2 size)
        {
            return
                (int)
                    (nk_menu_begin_symbol_text(ctx, title, (int)(nk_strlen(title)), (uint)(align), (int)(sym), (NkVec2)(size)));
        }

        public static int nk_menu_item_text(NkContext ctx, char* title, int len, uint align)
        {
            return (int)(nk_contextual_item_text(ctx, title, (int)(len), (uint)(align)));
        }

        public static int nk_menu_item_label(NkContext ctx, char* label, uint align)
        {
            return (int)(nk_contextual_item_label(ctx, label, (uint)(align)));
        }

        public static int nk_menu_item_image_label(NkContext ctx, NkImage img, char* label, uint align)
        {
            return (int)(nk_contextual_item_image_label(ctx, (NkImage)(img), label, (uint)(align)));
        }

        public static int nk_menu_item_image_text(NkContext ctx, NkImage img, char* text, int len, uint align)
        {
            return (int)(nk_contextual_item_image_text(ctx, (NkImage)(img), text, (int)(len), (uint)(align)));
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