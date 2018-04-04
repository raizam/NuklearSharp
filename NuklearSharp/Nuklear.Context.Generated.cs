namespace KlearUI
{
    public unsafe partial class Nk
    {
        public static VertexConvertResult nk_convert(NkContext ctx, NkBuffer<nk_draw_command> cmds, NkBuffer<byte> vertices,
            NkBuffer<ushort> elements, NkConvertConfig config)
        {
            VertexConvertResult res = (VertexConvertResult.Success);

            if ((((((ctx == null) || (cmds == null)) || (vertices == null)) || (elements == null)) || (config == null)) ||
                (config.VertexLayout == null)) return (VertexConvertResult.InvalidParam);
            nk_draw_list_setup(ctx.DrawList, config, cmds, vertices, elements, (config.LineAa), (config.ShapeAa));
            var top_window = ctx.nk__begin();

            int cnt = 0;
            for (var cmd = top_window.Buffer.First; cmd != null; cmd = cmd.Next)
            {
                switch (cmd.Header.type)
                {
                    case CommandType.Scissor:
                        {
                            NkCommandScissor s = (NkCommandScissor)(cmd);
                            nk_draw_list_add_clip(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(s.X), (float)(s.Y), (float)(s.W), (float)(s.H))));
                        }
                        break;
                    case CommandType.Line:
                        {
                            NkCommandLine l = (NkCommandLine)(cmd);
                            nk_draw_list_stroke_line(ctx.DrawList, (NkVec2)(nk_vec2_((float)(l.Begin.x), (float)(l.Begin.y))),
                                (NkVec2)(nk_vec2_((float)(l.End.x), (float)(l.End.y))), (NkColor)(l.Color), (float)(l.LineThickness));
                        }
                        break;
                    case CommandType.Curve:
                        {
                            NkCommandCurve q = (NkCommandCurve)(cmd);
                            nk_draw_list_stroke_curve(ctx.DrawList, (NkVec2)(nk_vec2_((float)(q.Begin.x), (float)(q.Begin.y))),
                                (NkVec2)(nk_vec2_((float)(q.Ctrl0.x), (float)(q.Ctrl0.y))),
                                (NkVec2)(nk_vec2_((float)(q.Ctrl1.x), (float)(q.Ctrl1.y))),
                                (NkVec2)(nk_vec2_((float)(q.End.x), (float)(q.End.y))), (NkColor)(q.Color),
                                (uint)(config.CurveSegmentCount), (float)(q.LineThickness));
                        }
                        break;
                    case CommandType.Rect:
                        {
                            NkCommandRect r = (NkCommandRect)(cmd);
                            nk_draw_list_stroke_rect(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (NkColor)(r.Color),
                                (float)(r.Rounding), (float)(r.LineThickness));
                        }
                        break;
                    case CommandType.RectFilled:
                        {
                            NkCommandRectFilled r = (NkCommandRectFilled)(cmd);
                            nk_draw_list_fill_rect(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (NkColor)(r.Color),
                                (float)(r.Rounding));
                        }
                        break;
                    case CommandType.RectMulticolor:
                        {
                            NkCommandRectMultiColor r = (NkCommandRectMultiColor)(cmd);
                            nk_draw_list_fill_rect_multi_color(ctx.DrawList,
                                (NkRect)(nk_rect_((float)(r.X), (float)(r.Y), (float)(r.W), (float)(r.H))), (NkColor)(r.Left),
                                (NkColor)(r.Top), (NkColor)(r.Right), (NkColor)(r.Bottom));
                        }
                        break;
                    case CommandType.Circle:
                        {
                            NkCommandCircle c = (NkCommandCircle)(cmd);
                            nk_draw_list_stroke_circle(ctx.DrawList,
                                (NkVec2)(nk_vec2_((float)((float)(c.X) + (float)(c.W) / 2), (float)((float)(c.Y) + (float)(c.H) / 2))),
                                (float)((float)(c.W) / 2), (NkColor)(c.Color), (uint)(config.CircleSegmentCount), (float)(c.LineThickness));
                        }
                        break;
                    case CommandType.CircleFilled:
                        {
                            NkCommandCircleFilled c = (NkCommandCircleFilled)(cmd);
                            nk_draw_list_fill_circle(ctx.DrawList,
                                (NkVec2)(nk_vec2_((float)((float)(c.X) + (float)(c.W) / 2), (float)((float)(c.Y) + (float)(c.H) / 2))),
                                (float)((float)(c.W) / 2), (NkColor)(c.Color), (uint)(config.CircleSegmentCount));
                        }
                        break;
                    case CommandType.Arc:
                        {
                            NkCommandArc c = (NkCommandArc)(cmd);
                            nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))));
                            nk_draw_list_path_arc_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))), (float)(c.R),
                                (float)(c.A[0]), (float)(c.A[1]), (uint)(config.ArcSegmentCount));
                            nk_draw_list_path_stroke(ctx.DrawList, (NkColor)(c.Color), true, (float)(c.LineThickness));
                        }
                        break;
                    case CommandType.ArcFilled:
                        {
                            NkCommandArcFilled c = (NkCommandArcFilled)(cmd);
                            nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))));
                            nk_draw_list_path_arc_to(ctx.DrawList, (NkVec2)(nk_vec2_((float)(c.Cx), (float)(c.Cy))), (float)(c.R),
                                (float)(c.A[0]), (float)(c.A[1]), (uint)(config.ArcSegmentCount));
                            nk_draw_list_path_fill(ctx.DrawList, (NkColor)(c.Color));
                        }
                        break;
                    case CommandType.Triangle:
                        {
                            NkCommandTriangle t = (NkCommandTriangle)(cmd);
                            nk_draw_list_stroke_triangle(ctx.DrawList, (NkVec2)(nk_vec2_((float)(t.A.x), (float)(t.A.y))),
                                (NkVec2)(nk_vec2_((float)(t.B.x), (float)(t.B.y))), (NkVec2)(nk_vec2_((float)(t.C.x), (float)(t.C.y))),
                                (NkColor)(t.Color), (float)(t.LineThickness));
                        }
                        break;
                    case CommandType.TriangleFilled:
                        {
                            NkCommandTriangleFilled t = (NkCommandTriangleFilled)(cmd);
                            nk_draw_list_fill_triangle(ctx.DrawList, (NkVec2)(nk_vec2_((float)(t.A.x), (float)(t.A.y))),
                                (NkVec2)(nk_vec2_((float)(t.B.x), (float)(t.B.y))), (NkVec2)(nk_vec2_((float)(t.C.x), (float)(t.C.y))),
                                (NkColor)(t.Color));
                        }
                        break;
                    case CommandType.Polygon:
                        {
                            int i;
                            NkCommandPolygon p = (NkCommandPolygon)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                NkVec2 pnt = (NkVec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(pnt));
                            }
                            nk_draw_list_path_stroke(ctx.DrawList, (NkColor)(p.Color), true, (float)(p.LineThickness));
                        }
                        break;
                    case CommandType.PolygonFilled:
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
                    case CommandType.Polyline:
                        {
                            int i;
                            NkCommandPolyline p = (NkCommandPolyline)(cmd);
                            for (i = (int)(0); (i) < (p.PointCount); ++i)
                            {
                                NkVec2 pnt = (NkVec2)(nk_vec2_((float)(p.Points[i].x), (float)(p.Points[i].y)));
                                nk_draw_list_path_line_to(ctx.DrawList, (NkVec2)(pnt));
                            }
                            nk_draw_list_path_stroke(ctx.DrawList, (NkColor)(p.Color), false, (float)(p.LineThickness));
                        }
                        break;
                    case CommandType.Text:
                        {
                            NkCommandText t = (NkCommandText)(cmd);
                            nk_draw_list_add_text(ctx.DrawList, t.Font,
                                (NkRect)(nk_rect_((float)(t.X), (float)(t.Y), (float)(t.W), (float)(t.H))), t.String, (int)(t.Length),
                                (float)(t.Height), (NkColor)(t.Foreground));
                        }
                        break;
                    case CommandType.Image:
                        {
                            NkCommandImage i = (NkCommandImage)(cmd);
                            nk_draw_list_add_image(ctx.DrawList, (NkImage)(i.Img),
                                (NkRect)(nk_rect_((float)(i.X), (float)(i.Y), (float)(i.W), (float)(i.H))), (NkColor)(i.Col));
                        }
                        break;
                    case CommandType.Custom:
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
            for (i = (int)(0); (i) < ((int)MouseButtons.MAX); ++i)
            {
                ((nk_mouse_button*)_in_.mouse.Buttons + i)->clicked = (uint)(0);
            }
            _in_.keyboard.TextLen = (int)(0);
            _in_.mouse.ScrollDelta = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            _in_.mouse.Prev.x = (float)(_in_.mouse.Pos.x);
            _in_.mouse.Prev.y = (float)(_in_.mouse.Pos.y);
            _in_.mouse.Delta.x = (float)(0);
            _in_.mouse.Delta.y = (float)(0);
            for (i = (int)(0); (i) < ((int)ControlKeys.MAX); i++)
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

        public static void nk_input_key(NkContext ctx, ControlKeys key, int down)
        {
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            if (((nk_key*)_in_.keyboard.Keys + (int)key)->down != down) ((nk_key*)_in_.keyboard.Keys + (int)key)->clicked++;
            ((nk_key*)_in_.keyboard.Keys + (int)key)->down = (int)(down);
        }

        public static void nk_input_button(NkContext ctx, MouseButtons id, int x, int y, int down)
        {
            nk_mouse_button* btn;
            nk_input _in_;
            if (ctx == null) return;
            _in_ = ctx.Input;
            if ((_in_.mouse.Buttons[(int)id].down) == (down)) return;
            btn = (nk_mouse_button*)_in_.mouse.Buttons + (int)id;
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
            text.color = (NkColor)(table[(int)StyleColors.Text]);
            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            button = style.Button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Button])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ButtonHover])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ButtonActive])));
            button.border_color = (NkColor)(table[(int)StyleColors.Border]);
            button.text_background = (NkColor)(table[(int)StyleColors.Button]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.image_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(1.0f);
            button.rounding = (float)(4.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.ContextualButton;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ButtonHover])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ButtonActive])));
            button.border_color = (NkColor)(table[(int)StyleColors.Window]);
            button.text_background = (NkColor)(table[(int)StyleColors.Window]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.MenuButton;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.border_color = (NkColor)(table[(int)StyleColors.Window]);
            button.text_background = (NkColor)(table[(int)StyleColors.Window]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(1.0f);
            button.draw_begin = null;
            button.draw_end = null;
            toggle = style.Checkbox;

            toggle.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Toogle])));
            toggle.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleHover])));
            toggle.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleHover])));
            toggle.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleCursor])));
            toggle.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleCursor])));
            toggle.userdata = (NkHandle)(nk_handle_ptr(null));
            toggle.text_background = (NkColor)(table[(int)StyleColors.Window]);
            toggle.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            toggle.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            toggle.text_active = (NkColor)(table[(int)StyleColors.Text]);
            toggle.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            toggle.touch_padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            toggle.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            toggle.border = (float)(0.0f);
            toggle.spacing = (float)(4);
            toggle = style.Option;

            toggle.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Toogle])));
            toggle.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleHover])));
            toggle.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleHover])));
            toggle.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleCursor])));
            toggle.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ToogleCursor])));
            toggle.userdata = (NkHandle)(nk_handle_ptr(null));
            toggle.text_background = (NkColor)(table[(int)StyleColors.Window]);
            toggle.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            toggle.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            toggle.text_active = (NkColor)(table[(int)StyleColors.Text]);
            toggle.padding = (NkVec2)(nk_vec2_((float)(3.0f), (float)(3.0f)));
            toggle.touch_padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            toggle.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            toggle.border = (float)(0.0f);
            toggle.spacing = (float)(4);
            select = style.Selectable;

            select.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Select])));
            select.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Select])));
            select.pressed = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Select])));
            select.normal_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SelectActive])));
            select.hover_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SelectActive])));
            select.pressed_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SelectActive])));
            select.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            select.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            select.text_pressed = (NkColor)(table[(int)StyleColors.Text]);
            select.text_normal_active = (NkColor)(table[(int)StyleColors.Text]);
            select.text_hover_active = (NkColor)(table[(int)StyleColors.Text]);
            select.text_pressed_active = (NkColor)(table[(int)StyleColors.Text]);
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
            slider.bar_normal = (NkColor)(table[(int)StyleColors.Slider]);
            slider.bar_hover = (NkColor)(table[(int)StyleColors.Slider]);
            slider.bar_active = (NkColor)(table[(int)StyleColors.Slider]);
            slider.bar_filled = (NkColor)(table[(int)StyleColors.SliderCursor]);
            slider.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SliderCursor])));
            slider.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SliderCursorHover])));
            slider.cursor_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SliderCursorActive])));
            slider.inc_symbol = (Symbols.TriangleRight);
            slider.dec_symbol = (Symbols.TriangleLeft);
            slider.cursor_size = (NkVec2)(nk_vec2_((float)(16), (float)(16)));
            slider.padding = (NkVec2)(nk_vec2_((float)(2), (float)(2)));
            slider.spacing = (NkVec2)(nk_vec2_((float)(2), (float)(2)));
            slider.userdata = (NkHandle)(nk_handle_ptr(null));
            slider.show_buttons = false;
            slider.bar_height = (float)(8);
            slider.rounding = (float)(0);
            slider.draw_begin = null;
            slider.draw_end = null;
            button = style.Slider.inc_button;
            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(NkColor.nk_rgb((int)(40), (int)(40), (int)(40)))));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(NkColor.nk_rgb((int)(42), (int)(42), (int)(42)))));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(NkColor.nk_rgb((int)(44), (int)(44), (int)(44)))));
            button.border_color = (NkColor)(NkColor.nk_rgb((int)(65), (int)(65), (int)(65)));
            button.text_background = (NkColor)(NkColor.nk_rgb((int)(40), (int)(40), (int)(40)));
            button.text_normal = (NkColor)(NkColor.nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_hover = (NkColor)(NkColor.nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_active = (NkColor)(NkColor.nk_rgb((int)(175), (int)(175), (int)(175)));
            button.padding = (NkVec2)(nk_vec2_((float)(8.0f), (float)(8.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(1.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Slider.dec_button = (nk_style_button)(style.Slider.inc_button);
            prog = style.Progress;

            prog.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Slider])));
            prog.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Slider])));
            prog.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Slider])));
            prog.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SliderCursor])));
            prog.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SliderCursorHover])));
            prog.cursor_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.SliderCursorActive])));
            prog.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            prog.cursor_border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            prog.userdata = (NkHandle)(nk_handle_ptr(null));
            prog.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            prog.rounding = (float)(0);
            prog.border = (float)(0);
            prog.cursor_rounding = (float)(0);
            prog.cursor_border = (float)(0);
            prog.draw_begin = null;
            prog.draw_end = null;
            scroll = style.Scrollh;

            scroll.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Scrollbar])));
            scroll.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Scrollbar])));
            scroll.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Scrollbar])));
            scroll.cursor_normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ScrollbarCursor])));
            scroll.cursor_hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ScrollbarCursorHover])));
            scroll.cursor_active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.ScrollbarCursorActive])));
            scroll.dec_symbol = (Symbols.CircleSolid);
            scroll.inc_symbol = (Symbols.CircleSolid);
            scroll.userdata = (NkHandle)(nk_handle_ptr(null));
            scroll.border_color = (NkColor)(table[(int)StyleColors.Scrollbar]);
            scroll.cursor_border_color = (NkColor)(table[(int)StyleColors.Scrollbar]);
            scroll.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            scroll.show_buttons = false;
            scroll.border = (float)(0);
            scroll.rounding = (float)(0);
            scroll.border_cursor = (float)(0);
            scroll.rounding_cursor = (float)(0);
            scroll.draw_begin = null;
            scroll.draw_end = null;
            style.Scrollv = (nk_style_scrollbar)(style.Scrollh);
            button = style.Scrollh.inc_button;
            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(NkColor.nk_rgb((int)(40), (int)(40), (int)(40)))));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(NkColor.nk_rgb((int)(42), (int)(42), (int)(42)))));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(NkColor.nk_rgb((int)(44), (int)(44), (int)(44)))));
            button.border_color = (NkColor)(NkColor.nk_rgb((int)(65), (int)(65), (int)(65)));
            button.text_background = (NkColor)(NkColor.nk_rgb((int)(40), (int)(40), (int)(40)));
            button.text_normal = (NkColor)(NkColor.nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_hover = (NkColor)(NkColor.nk_rgb((int)(175), (int)(175), (int)(175)));
            button.text_active = (NkColor)(NkColor.nk_rgb((int)(175), (int)(175), (int)(175)));
            button.padding = (NkVec2)(nk_vec2_((float)(4.0f), (float)(4.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(1.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Scrollh.dec_button = (nk_style_button)(style.Scrollh.inc_button);
            style.Scrollv.inc_button = (nk_style_button)(style.Scrollh.inc_button);
            style.Scrollv.dec_button = (nk_style_button)(style.Scrollh.inc_button);
            edit = style.Edit;

            edit.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Edit])));
            edit.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Edit])));
            edit.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Edit])));
            edit.cursor_normal = (NkColor)(table[(int)StyleColors.Text]);
            edit.cursor_hover = (NkColor)(table[(int)StyleColors.Text]);
            edit.cursor_text_normal = (NkColor)(table[(int)StyleColors.Edit]);
            edit.cursor_text_hover = (NkColor)(table[(int)StyleColors.Edit]);
            edit.border_color = (NkColor)(table[(int)StyleColors.Border]);
            edit.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            edit.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            edit.text_active = (NkColor)(table[(int)StyleColors.Text]);
            edit.selected_normal = (NkColor)(table[(int)StyleColors.Text]);
            edit.selected_hover = (NkColor)(table[(int)StyleColors.Text]);
            edit.selected_text_normal = (NkColor)(table[(int)StyleColors.Edit]);
            edit.selected_text_hover = (NkColor)(table[(int)StyleColors.Edit]);
            edit.scrollbar_size = (NkVec2)(nk_vec2_((float)(10), (float)(10)));
            edit.scrollbar = (nk_style_scrollbar)(style.Scrollv);
            edit.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            edit.row_padding = (float)(2);
            edit.cursor_size = (float)(4);
            edit.border = (float)(1);
            edit.rounding = (float)(0);
            property = style.Property;

            property.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            property.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            property.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            property.border_color = (NkColor)(table[(int)StyleColors.Border]);
            property.label_normal = (NkColor)(table[(int)StyleColors.Text]);
            property.label_hover = (NkColor)(table[(int)StyleColors.Text]);
            property.label_active = (NkColor)(table[(int)StyleColors.Text]);
            property.sym_left = (Symbols.TriangleLeft);
            property.sym_right = (Symbols.TriangleRight);
            property.userdata = (NkHandle)(nk_handle_ptr(null));
            property.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            property.border = (float)(1);
            property.rounding = (float)(10);
            property.draw_begin = null;
            property.draw_end = null;
            button = style.Property.dec_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            button.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[(int)StyleColors.Property]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Property.inc_button = (nk_style_button)(style.Property.dec_button);
            edit = style.Property.edit;

            edit.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            edit.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            edit.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Property])));
            edit.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            edit.cursor_normal = (NkColor)(table[(int)StyleColors.Text]);
            edit.cursor_hover = (NkColor)(table[(int)StyleColors.Text]);
            edit.cursor_text_normal = (NkColor)(table[(int)StyleColors.Edit]);
            edit.cursor_text_hover = (NkColor)(table[(int)StyleColors.Edit]);
            edit.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            edit.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            edit.text_active = (NkColor)(table[(int)StyleColors.Text]);
            edit.selected_normal = (NkColor)(table[(int)StyleColors.Text]);
            edit.selected_hover = (NkColor)(table[(int)StyleColors.Text]);
            edit.selected_text_normal = (NkColor)(table[(int)StyleColors.Edit]);
            edit.selected_text_hover = (NkColor)(table[(int)StyleColors.Edit]);
            edit.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            edit.cursor_size = (float)(8);
            edit.border = (float)(0);
            edit.rounding = (float)(0);
            chart = style.Chart;

            chart.background = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Chart])));
            chart.border_color = (NkColor)(table[(int)StyleColors.Border]);
            chart.selected_color = (NkColor)(table[(int)StyleColors.CharColorHighlight]);
            chart.color = (NkColor)(table[(int)StyleColors.CharColor]);
            chart.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            chart.border = (float)(0);
            chart.rounding = (float)(0);
            combo = style.Combo;
            combo.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Combo])));
            combo.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Combo])));
            combo.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Combo])));
            combo.border_color = (NkColor)(table[(int)StyleColors.Border]);
            combo.label_normal = (NkColor)(table[(int)StyleColors.Text]);
            combo.label_hover = (NkColor)(table[(int)StyleColors.Text]);
            combo.label_active = (NkColor)(table[(int)StyleColors.Text]);
            combo.sym_normal = (Symbols.TriangleDown);
            combo.sym_hover = (Symbols.TriangleDown);
            combo.sym_active = (Symbols.TriangleDown);
            combo.content_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            combo.button_padding = (NkVec2)(nk_vec2_((float)(0), (float)(4)));
            combo.spacing = (NkVec2)(nk_vec2_((float)(4), (float)(0)));
            combo.border = (float)(1);
            combo.rounding = (float)(0);
            button = style.Combo.button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Combo])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Combo])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Combo])));
            button.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[(int)StyleColors.Combo]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            tab = style.Tab;
            tab.background = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.TabHeader])));
            tab.border_color = (NkColor)(table[(int)StyleColors.Border]);
            tab.text = (NkColor)(table[(int)StyleColors.Text]);
            tab.sym_minimize = (Symbols.TriangleRight);
            tab.sym_maximize = (Symbols.TriangleDown);
            tab.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            tab.spacing = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            tab.indent = (float)(10.0f);
            tab.border = (float)(1);
            tab.rounding = (float)(0);
            button = style.Tab.tab_minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.TabHeader])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.TabHeader])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.TabHeader])));
            button.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[(int)StyleColors.TabHeader]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Tab.tab_maximize_button = (nk_style_button)(button);
            button = style.Tab.node_minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            button.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[(int)StyleColors.TabHeader]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(2.0f), (float)(2.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            style.Tab.node_maximize_button = (nk_style_button)(button);
            win = style.Window;
            win.header.align = (StyleHeaderAlign.Right);
            win.header.close_symbol = (Symbols.X);
            win.header.minimize_symbol = (Symbols.Minus);
            win.header.maximize_symbol = (Symbols.Plus);
            win.header.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            win.header.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            win.header.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            win.header.label_normal = (NkColor)(table[(int)StyleColors.Text]);
            win.header.label_hover = (NkColor)(table[(int)StyleColors.Text]);
            win.header.label_active = (NkColor)(table[(int)StyleColors.Text]);
            win.header.label_padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.header.padding = (NkVec2)(nk_vec2_((float)(4), (float)(4)));
            win.header.spacing = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            button = style.Window.header.close_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            button.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[(int)StyleColors.Header]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            button = style.Window.header.minimize_button;

            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Header])));
            button.border_color = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            button.text_background = (NkColor)(table[(int)StyleColors.Header]);
            button.text_normal = (NkColor)(table[(int)StyleColors.Text]);
            button.text_hover = (NkColor)(table[(int)StyleColors.Text]);
            button.text_active = (NkColor)(table[(int)StyleColors.Text]);
            button.padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.touch_padding = (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f)));
            button.userdata = (NkHandle)(nk_handle_ptr(null));
            button.text_alignment = (Align.MiddleCentered);
            button.border = (float)(0.0f);
            button.rounding = (float)(0.0f);
            button.draw_begin = null;
            button.draw_end = null;
            win.background = (NkColor)(table[(int)StyleColors.Window]);
            win.fixed_background = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Window])));
            win.border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.popup_border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.combo_border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.contextual_border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.menu_border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.group_border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.tooltip_border_color = (NkColor)(table[(int)StyleColors.Border]);
            win.scaler = (NkStyleItem)(nk_style_item_color((NkColor)(table[(int)StyleColors.Text])));
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
            if ((ctx.Current) != null) Layout.nk_layout_reset_min_row_height(ctx);
        }

        public static bool nk_style_push_font(NkContext ctx, NkUserFont font)
        {
            nk_config_stack_user_font font_stack;
            nk_config_stack_user_font_element element;
            if (ctx == null) return false;
            font_stack = ctx.Stacks.fonts;
            if ((font_stack.head) >= (int)font_stack.elements.Length) return false;
            element = font_stack.elements[font_stack.head++];
            element.address = ctx.Style.Font;
            element.old_value = ctx.Style.Font;
            ctx.Style.Font = font;
            return true;
        }

        public static bool nk_style_pop_font(NkContext ctx)
        {
            nk_config_stack_user_font font_stack;
            nk_config_stack_user_font_element element;
            if (ctx == null) return false;
            font_stack = ctx.Stacks.fonts;
            if ((font_stack.head) < (1)) return false;
            element = font_stack.elements[--font_stack.head];
            element.address = element.old_value;
            return true;
        }

        public static bool nk_style_push_style_item(NkContext ctx, NkStyleItem address, NkStyleItem value)
        {
            nk_config_stack_style_item type_stack;
            nk_config_stack_style_item_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.style_items;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return false;
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (NkStyleItem)(address);
            address = (NkStyleItem)(value);
            return true;
        }

        public static bool nk_style_push_float(NkContext ctx, float* address, float value)
        {
            nk_config_stack_float type_stack;
            nk_config_stack_float_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.floats;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return false;
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (float)(*address);
            *address = (float)(value);
            return true;
        }

        public static bool nk_style_push_vec2(NkContext ctx, NkVec2* address, NkVec2 value)
        {
            nk_config_stack_vec2 type_stack;
            nk_config_stack_vec2_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.vectors;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return false;
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (NkVec2)(*address);
            *address = (NkVec2)(value);
            return true;
        }

        public static bool nk_style_push_flags(NkContext ctx, uint* address, uint value)
        {
            nk_config_stack_flags type_stack;
            nk_config_stack_flags_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.flags;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return false;
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (uint)(*address);
            *address = (uint)(value);
            return true;
        }

        public static bool nk_style_push_color(NkContext ctx, NkColor* address, NkColor value)
        {
            nk_config_stack_color type_stack;
            nk_config_stack_color_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.colors;
            if ((type_stack.head) >= (int)type_stack.elements.Length) return false;
            element = type_stack.elements[(type_stack.head++)];
            element.address = address;
            element.old_value = (NkColor)(*address);
            *address = (NkColor)(value);
            return true;
        }

        public static bool nk_style_pop_style_item(NkContext ctx)
        {
            nk_config_stack_style_item type_stack;
            nk_config_stack_style_item_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.style_items;
            if ((type_stack.head) < (1)) return false;
            element = type_stack.elements[(--type_stack.head)];
            element.address = (NkStyleItem)(element.old_value);
            return true;
        }

        public static bool nk_style_pop_float(NkContext ctx)
        {
            nk_config_stack_float type_stack;
            nk_config_stack_float_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.floats;
            if ((type_stack.head) < (1)) return false;
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (float)(element.old_value);
            return true;
        }

        public static bool nk_style_pop_vec2(NkContext ctx)
        {
            nk_config_stack_vec2 type_stack;
            nk_config_stack_vec2_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.vectors;
            if ((type_stack.head) < (1)) return false;
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (NkVec2)(element.old_value);
            return true;
        }

        public static bool nk_style_pop_flags(NkContext ctx)
        {
            nk_config_stack_flags type_stack;
            nk_config_stack_flags_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.flags;
            if ((type_stack.head) < (1)) return false;
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (uint)(element.old_value);
            return true;
        }

        public static bool nk_style_pop_color(NkContext ctx)
        {
            nk_config_stack_color type_stack;
            nk_config_stack_color_element element;
            if (ctx == null) return false;
            type_stack = ctx.Stacks.colors;
            if ((type_stack.head) < (1)) return false;
            element = type_stack.elements[(--type_stack.head)];
            *element.address = (NkColor)(element.old_value);
            return true;
        }

        public static bool nk_style_set_cursor(NkContext ctx, int c)
        {
            NkStyle style;
            if (ctx == null) return false;
            style = ctx.Style;
            if ((style.Cursors[c]) != null)
            {
                style.CursorActive = style.Cursors[c];
                return true;
            }

            return false;
        }

        public static void nk_style_show_cursor(NkContext ctx)
        {
            ctx.Style.CursorVisible = true;
        }

        public static void nk_style_hide_cursor(NkContext ctx)
        {
            ctx.Style.CursorVisible = false;
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
            for (i = (int)(0); (i) < ((int)CursorKind.COUNT); ++i)
            {
                style.Cursors[i] = cursors[i];
            }
            style.CursorVisible = true;
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
            ctx.Build = false;
            ctx.LastWidgetState = (uint)(0);
            ctx.Style.CursorActive = ctx.Style.Cursors[(int)CursorKind.Arrow];

            nk_draw_list_clear(ctx.DrawList);
            iter = ctx.Begin;
            while ((iter) != null)
            {
                if ((((iter.Flags & PanelFlags.Minimized) != 0) && ((iter.Flags & PanelFlags.Closed) == 0)) &&
                    ((iter.Seq) == (ctx.Seq)))
                {
                    iter = iter.Next;
                    continue;
                }
                if ((((iter.Flags & PanelFlags.Hidden) != 0) || ((iter.Flags & PanelFlags.Closed) != 0)) && ((iter) == (ctx.Active)))
                {
                    ctx.Active = iter.Prev;
                    ctx.End = iter.Prev;
                    if ((ctx.Active) != null) ctx.Active.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
                }
                if (((iter.Popup.win) != null) && (iter.Popup.win.Seq != ctx.Seq))
                {
                    ctx.nk_free_window(iter.Popup.win);
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
                            iter.nk_remove_table(it);
                            if ((it) == (iter.Tables)) iter.Tables = n;
                        }
                        it = n;
                    }
                }
                if ((iter.Seq != ctx.Seq) || ((iter.Flags & PanelFlags.Closed) != 0))
                {
                    next = iter.Next;
                    nk_remove_window(ctx, iter);
                    ctx.nk_free_window(iter);
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

        public static bool nk_panel_begin(NkContext ctx, char* title, PanelKind panel_type)
        {
            nk_input _in_;
            NkWindow win;
            NkPanel layout;
            NkCommandBuffer _out_;
            NkStyle style;
            NkUserFont font;
            NkVec2 scrollbar_size = new NkVec2();
            NkVec2 panel_padding = new NkVec2();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;

            if (((ctx.Current.Flags & PanelFlags.Hidden) != 0) || ((ctx.Current.Flags & PanelFlags.Closed) != 0))
            {
                ctx.Current.Layout.Type = (panel_type);
                return false;
            }

            style = ctx.Style;
            font = style.Font;
            win = ctx.Current;
            layout = win.Layout;
            _out_ = win.Buffer;
            _in_ = (win.Flags & PanelFlags.NonInput) != 0 ? null : ctx.Input;
            scrollbar_size = (NkVec2)(style.Window.scrollbar_size);
            panel_padding = (NkVec2)(nk_panel_get_padding(style, (panel_type)));
            if (((win.Flags & PanelFlags.Movable) != 0) && ((win.Flags & PanelFlags.Rom) == 0))
            {
                bool left_mouse_down;
                bool left_mouse_click_in_cursor;
                NkRect header = new NkRect();
                header.x = (float)(win.Bounds.x);
                header.y = (float)(win.Bounds.y);
                header.w = (float)(win.Bounds.w);
                if ((nk_panel_has_header((win.Flags), title)) != 0)
                {
                    header.h = (float)(font.Height + 2.0f * style.Window.header.padding.y);
                    header.h += (float)(2.0f * style.Window.header.label_padding.y);
                }
                else header.h = (float)(panel_padding.y);
                left_mouse_down = (((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->down) != 0;
                left_mouse_click_in_cursor =
                   (_in_.nk_input_has_mouse_click_down_in_rect((int)(MouseButtons.Left), (NkRect)(header), true));
                if (((left_mouse_down)) && ((left_mouse_click_in_cursor)))
                {
                    win.Bounds.x = (float)(win.Bounds.x + _in_.mouse.Delta.x);
                    win.Bounds.y = (float)(win.Bounds.y + _in_.mouse.Delta.y);
                    ((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->clicked_pos.x += (float)(_in_.mouse.Delta.x);
                    ((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->clicked_pos.y += (float)(_in_.mouse.Delta.y);
                    ctx.Style.CursorActive = ctx.Style.Cursors[(int)CursorKind.Move];
                }
            }

            layout.Type = (panel_type);
            layout.Flags = (win.Flags);
            layout.Bounds = (NkRect)(win.Bounds);
            layout.Bounds.x += (float)(panel_padding.x);
            layout.Bounds.w -= (float)(2 * panel_padding.x);
            if ((win.Flags & PanelFlags.Border) != 0)
            {
                layout.Border = (float)(nk_panel_get_border(style, (win.Flags), (panel_type)));
                layout.Bounds = (NkRect)(nk_shrink_rect_((NkRect)(layout.Bounds), (float)(layout.Border)));
            }
            else layout.Border = (float)(0);
            layout.AtY = (float)(layout.Bounds.y);
            layout.AtX = (float)(layout.Bounds.x);
            layout.MaxX = (float)(0);
            layout.HeaderHeight = (float)(0);
            layout.FooterHeight = (float)(0);
            Layout.nk_layout_reset_min_row_height(ctx);
            layout.Row.index = (int)(0);
            layout.Row.columns = (int)(0);
            layout.Row.ratio = null;
            layout.Row.item_width = (float)(0);
            layout.Row.tree_depth = (int)(0);
            layout.Row.height = (float)(panel_padding.y);
            layout.HasScrolling = (true);
            if ((win.Flags & PanelFlags.NoScrollbar) == 0) layout.Bounds.w -= (float)(scrollbar_size.x);
            if (nk_panel_is_nonblock((panel_type)) == 0)
            {
                layout.FooterHeight = (float)(0);
                if (((win.Flags & PanelFlags.NoScrollbar) == 0) || ((win.Flags & PanelFlags.Scalable) != 0))
                    layout.FooterHeight = (float)(scrollbar_size.y);
                layout.Bounds.h -= (float)(layout.FooterHeight);
            }

            if ((nk_panel_has_header((win.Flags), title)) != 0)
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
                else if ((ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(header))))
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
                if ((background.Type) == (StyleItemKind.Image))
                {
                    text.background = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                    win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
                }
                else
                {
                    text.background = (NkColor)(background.Data.Color);
                    _out_.nk_fill_rect((NkRect)(header), (float)(0), (NkColor)(background.Data.Color));
                }
                {
                    NkRect button = new NkRect();
                    button.y = (float)(header.y + style.Window.header.padding.y);
                    button.h = (float)(header.h - 2 * style.Window.header.padding.y);
                    button.w = (float)(button.h);
                    if ((win.Flags & PanelFlags.Closable) != 0)
                    {
                        WidgetStates ws = (uint)(0);
                        if ((style.Window.header.align) == (StyleHeaderAlign.Right))
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
                            ((nk_do_button_symbol(ref ws, win.Buffer, (NkRect)(button), (style.Window.header.close_symbol),
                                ButtonBehavior.Default, style.Window.header.close_button, _in_, style.Font))) &&
                            ((win.Flags & PanelFlags.Rom) == 0))
                        {
                            layout.Flags |= (PanelFlags.Hidden);
                            layout.Flags &= ((PanelFlags)(~(uint)PanelFlags.Minimized));
                        }
                    }
                    if ((win.Flags & PanelFlags.Minimizable) != 0)
                    {
                        WidgetStates ws = (uint)(0);
                        if ((style.Window.header.align) == (StyleHeaderAlign.Right))
                        {
                            button.x = (float)((header.w + header.x) - button.w);
                            if ((win.Flags & PanelFlags.Closable) == 0)
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
                                    ((layout.Flags & PanelFlags.Minimized) != 0
                                        ? style.Window.header.maximize_symbol
                                        : style.Window.header.minimize_symbol), ButtonBehavior.Default, style.Window.header.minimize_button, _in_,
                                style.Font))) && ((win.Flags & PanelFlags.Rom) == 0))
                            layout.Flags =
                                    ((layout.Flags & PanelFlags.Minimized) != 0
                                        ? layout.Flags & (PanelFlags)(~(uint)PanelFlags.Minimized)
                                        : layout.Flags | PanelFlags.Minimized);
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
                    _out_.nk_widget_text((NkRect)(label), title, (int)(text_len), &text, (Align.MiddleLeft), font);
                }
            }

            if (((layout.Flags & PanelFlags.Minimized) == 0) && ((layout.Flags & PanelFlags.Dynamic) == 0))
            {
                NkRect body = new NkRect();
                body.x = (float)(win.Bounds.x);
                body.w = (float)(win.Bounds.w);
                body.y = (float)(win.Bounds.y + layout.HeaderHeight);
                body.h = (float)(win.Bounds.h - layout.HeaderHeight);
                if ((style.Window.fixed_background.Type) == (StyleItemKind.Image))
                    _out_.nk_draw_image((NkRect)(body), style.Window.fixed_background.Data.Image, (NkColor)(nk_white));
                else _out_.nk_fill_rect((NkRect)(body), (float)(0), (NkColor)(style.Window.fixed_background.Data.Color));
            }

            {
                NkRect clip = new NkRect();
                layout.Clip = (NkRect)(layout.Bounds);
                nk_unify(ref clip, ref win.Buffer.Clip, (float)(layout.Clip.x), (float)(layout.Clip.y),
                    (float)(layout.Clip.x + layout.Clip.w), (float)(layout.Clip.y + layout.Clip.h));
                _out_.nk_push_scissor((NkRect)(clip));
                layout.Clip = (NkRect)(clip);
            }

            return (((layout.Flags & PanelFlags.Hidden) == 0) && ((layout.Flags & PanelFlags.Minimized) == 0));
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
            _in_ = (((layout.Flags & PanelFlags.Rom) != 0) || ((layout.Flags & PanelFlags.NonInput) != 0)) ? null : ctx.Input;
            if (nk_panel_is_sub((layout.Type)) == 0) _out_.nk_push_scissor((NkRect)(nk_null_rect));
            scrollbar_size = (NkVec2)(style.Window.scrollbar_size);
            panel_padding = (NkVec2)(nk_panel_get_padding(style, (layout.Type)));
            layout.AtY += (float)(layout.Row.height);
            if (((layout.Flags & PanelFlags.Dynamic) != 0) && ((layout.Flags & PanelFlags.Minimized) == 0))
            {
                NkRect empty_space = new NkRect();
                if ((layout.AtY) < (layout.Bounds.y + layout.Bounds.h)) layout.Bounds.h = (float)(layout.AtY - layout.Bounds.y);
                empty_space.x = (float)(window.Bounds.x);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.h = (float)(panel_padding.y);
                empty_space.w = (float)(window.Bounds.w);
                _out_.nk_fill_rect((NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                empty_space.x = (float)(window.Bounds.x);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.w = (float)(panel_padding.x + layout.Border);
                empty_space.h = (float)(layout.Bounds.h);
                _out_.nk_fill_rect((NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                empty_space.x = (float)(layout.Bounds.x + layout.Bounds.w - layout.Border);
                empty_space.y = (float)(layout.Bounds.y);
                empty_space.w = (float)(panel_padding.x + layout.Border);
                empty_space.h = (float)(layout.Bounds.h);
                if (((layout.Offset.y) == (0)) && ((layout.Flags & PanelFlags.NoScrollbar) == 0))
                    empty_space.w += (float)(scrollbar_size.x);
                _out_.nk_fill_rect((NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                if ((layout.Offset.x != 0) && ((layout.Flags & PanelFlags.NoScrollbar) == 0))
                {
                    empty_space.x = (float)(window.Bounds.x);
                    empty_space.y = (float)(layout.Bounds.y + layout.Bounds.h);
                    empty_space.w = (float)(window.Bounds.w);
                    empty_space.h = (float)(scrollbar_size.y);
                    _out_.nk_fill_rect((NkRect)(empty_space), (float)(0), (NkColor)(style.Window.background));
                }
            }

            if ((((layout.Flags & PanelFlags.NoScrollbar) == 0) && ((layout.Flags & PanelFlags.Minimized) == 0)) &&
                ((window.ScrollbarHidingTimer) < (4.0f)))
            {
                NkRect scroll = new NkRect();
                bool scroll_has_scrolling;
                float scroll_target;
                float scroll_offset;
                float scroll_step;
                float scroll_inc;
                if ((nk_panel_is_sub((layout.Type))) != 0)
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
                    scroll_has_scrolling = false;
                    if (((root_window) == (ctx.Active)) && ((layout.HasScrolling)))
                    {
                        if (((_in_.nk_input_is_mouse_hovering_rect((NkRect)(layout.Bounds)))) &&
                            (!(((((root_panel.Clip.x) > (layout.Bounds.x + layout.Bounds.w)) ||
                                 ((root_panel.Clip.x + root_panel.Clip.w) < (layout.Bounds.x))) ||
                                ((root_panel.Clip.y) > (layout.Bounds.y + layout.Bounds.h))) ||
                               ((root_panel.Clip.y + root_panel.Clip.h) < (layout.Bounds.y)))))
                        {
                            root_panel = window.Layout;
                            while ((root_panel.Parent) != null)
                            {
                                root_panel.HasScrolling = (false);
                                root_panel = root_panel.Parent;
                            }
                            root_panel.HasScrolling = (false);
                            scroll_has_scrolling = true;
                        }
                    }
                }
                else if (nk_panel_is_sub((layout.Type)) == 0)
                {
                    scroll_has_scrolling = (((window) == (ctx.Active)) && ((layout.HasScrolling)));
                    if ((((_in_) != null) && (((_in_.mouse.ScrollDelta.y) > (0)) || ((_in_.mouse.ScrollDelta.x) > (0)))) &&
                        ((scroll_has_scrolling))) window.Scrolled = (true);
                    else window.Scrolled = (false);
                }
                else scroll_has_scrolling = false;
                {
                    WidgetStates state = (uint)(0);
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
                            (nk_do_scrollbarv(ref state, _out_, (NkRect)(scroll), (scroll_has_scrolling), (float)(scroll_offset),
                                (float)(scroll_target), (float)(scroll_step), (float)(scroll_inc), ctx.Style.Scrollv, _in_, style.Font));
                    layout.Offset.y = ((uint)(scroll_offset));
                    if (((_in_) != null) && ((scroll_has_scrolling))) _in_.mouse.ScrollDelta.y = (float)(0);
                }
                {
                    WidgetStates state = (uint)(0);
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
                            (nk_do_scrollbarh(ref state, _out_, (NkRect)(scroll), (scroll_has_scrolling), (float)(scroll_offset),
                                (float)(scroll_target), (float)(scroll_step), (float)(scroll_inc), ctx.Style.Scrollh, _in_, style.Font));
                    layout.Offset.x = ((uint)(scroll_offset));
                }
            }

            if ((window.Flags & PanelFlags.ScrollAutoHide) != 0)
            {
                int has_input =
                    (int)
                        (((ctx.Input.mouse.Delta.x != 0) || (ctx.Input.mouse.Delta.y != 0)) || (ctx.Input.mouse.ScrollDelta.y != 0)
                            ? 1
                            : 0);
                bool is_window_hovered = (nk_window_is_hovered(ctx));
                int any_item_active = (int)(ctx.LastWidgetState & WidgetStates.Modified);
                if (((has_input == 0) && ((is_window_hovered))) || ((is_window_hovered == false) && (any_item_active == 0)))
                    window.ScrollbarHidingTimer += (float)(ctx.DeltaTimeSeconds);
                else window.ScrollbarHidingTimer = (float)(0);
            }
            else window.ScrollbarHidingTimer = (float)(0);
            if ((layout.Flags & PanelFlags.Border) != 0)
            {
                NkColor border_color = (NkColor)(nk_panel_get_border_color(style, (layout.Type)));
                float padding_y =
                    (float)
                        ((layout.Flags & PanelFlags.Minimized) != 0
                            ? style.Window.border + window.Bounds.y + layout.HeaderHeight
                            : (layout.Flags & PanelFlags.Dynamic) != 0
                                ? layout.Bounds.y + layout.Bounds.h + layout.FooterHeight
                                : window.Bounds.y + window.Bounds.h);
                NkRect b = window.Bounds;
                b.h = padding_y - window.Bounds.y;
                _out_.nk_stroke_rect(b, 0, layout.Border, border_color);
            }

            if ((((layout.Flags & PanelFlags.Scalable) != 0) && ((_in_) != null)) && ((layout.Flags & PanelFlags.Minimized) == 0))
            {
                NkRect scaler = new NkRect();
                scaler.w = (float)(scrollbar_size.x);
                scaler.h = (float)(scrollbar_size.y);
                scaler.y = (float)(layout.Bounds.y + layout.Bounds.h);
                if ((layout.Flags & PanelFlags.ScaleLeft) != 0) scaler.x = (float)(layout.Bounds.x - panel_padding.x * 0.5f);
                else scaler.x = (float)(layout.Bounds.x + layout.Bounds.w + panel_padding.x);
                if ((layout.Flags & PanelFlags.NoScrollbar) != 0) scaler.x -= (float)(scaler.w);
                {
                    NkStyleItem item = style.Window.scaler;
                    if ((item.Type) == (StyleItemKind.Image))
                        _out_.nk_draw_image((NkRect)(scaler), item.Data.Image, (NkColor)(nk_white));
                    else
                    {
                        if ((layout.Flags & PanelFlags.ScaleLeft) != 0)
                        {
                            _out_.nk_fill_triangle((float)(scaler.x), (float)(scaler.y), (float)(scaler.x), (float)(scaler.y + scaler.h),
                                (float)(scaler.x + scaler.w), (float)(scaler.y + scaler.h), (NkColor)(item.Data.Color));
                        }
                        else
                        {
                            _out_.nk_fill_triangle((float)(scaler.x + scaler.w), (float)(scaler.y), (float)(scaler.x + scaler.w),
                                (float)(scaler.y + scaler.h), (float)(scaler.x), (float)(scaler.y + scaler.h), (NkColor)(item.Data.Color));
                        }
                    }
                }
                if ((window.Flags & PanelFlags.Rom) == 0)
                {
                    NkVec2 window_size = (NkVec2)(style.Window.min_size);
                    bool left_mouse_down = (((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->down) != 0;
                    bool left_mouse_click_in_scaler =
                        (_in_.nk_input_has_mouse_click_down_in_rect((int)(MouseButtons.Left), (NkRect)(scaler), true));
                    if (((left_mouse_down)) && ((left_mouse_click_in_scaler)))
                    {
                        float delta_x = (float)(_in_.mouse.Delta.x);
                        if ((layout.Flags & PanelFlags.ScaleLeft) != 0)
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
                        if ((layout.Flags & PanelFlags.Dynamic) == 0)
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
                        ctx.Style.CursorActive = ctx.Style.Cursors[(int)CursorKind.ResizeClockwise];
                        ((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->clicked_pos.x = (float)(scaler.x + scaler.w / 2.0f);
                        ((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->clicked_pos.y = (float)(scaler.y + scaler.h / 2.0f);
                    }
                }
            }

            if (nk_panel_is_sub((layout.Type)) == 0)
            {
                if ((layout.Flags & PanelFlags.Hidden) != 0) window.Buffer.nk_command_buffer_reset();
                else nk_finish(ctx, window);
            }

            if ((layout.Flags & PanelFlags.RemoveRom) != 0)
            {
                layout.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
                layout.Flags &= (PanelFlags)(~(uint)(PanelFlags.RemoveRom));
            }

            window.Flags = (layout.Flags);
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
                win.nk_push_table(tbl);
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
                end.Flags |= (PanelFlags.Rom);
                end.Next = win;
                win.Prev = ctx.End;
                win.Next = null;
                ctx.End = win;
                ctx.Active = ctx.End;
                ctx.End.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
            }
            else
            {
                ctx.Begin.Prev = win;
                win.Next = ctx.Begin;
                win.Prev = null;
                ctx.Begin = win;
                ctx.Begin.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
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
                if ((ctx.End) != null) ctx.End.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
            }

            win.Next = null;
            win.Prev = null;
            ctx.Count--;
        }

        public static bool nk_begin(NkContext ctx, char* title, NkRect bounds, PanelFlags flags)
        {
            return (nk_begin_titled(ctx, title, title, (NkRect)(bounds), (flags)));
        }

        public static bool nk_begin_titled(NkContext ctx, char* name, char* title, NkRect bounds, PanelFlags flags)
        {
            NkWindow win;
            NkStyle style;
            uint title_hash;
            int title_len;
            bool ret = false;
            if ((((ctx == null) || ((ctx.Current) != null)) || (title == null)) || (name == null)) return false;
            style = ctx.Style;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null)
            {
                ulong name_length = (ulong)(nk_strlen(name));
                win = (NkWindow)(ctx.nk_create_window());
                if (win == null) return false;
                if ((flags & PanelFlags.Background) != 0) nk_insert_window(ctx, win, (int)(NK_INSERT_FRONT));
                else nk_insert_window(ctx, win, (int)(NK_INSERT_BACK));
                win.Buffer.nk_command_buffer_init(true);
                win.Flags = (flags);
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
                win.Flags &= (PanelFlags)(~(uint)(PanelFlags.Private - 1));
                win.Flags |= (PanelFlags)(flags);
                if ((win.Flags & (PanelFlags.Movable | PanelFlags.Scalable)) == 0) win.Bounds = (NkRect)(bounds);
                win.Seq = (uint)(ctx.Seq);
                if ((ctx.Active == null) && ((win.Flags & PanelFlags.Hidden) == 0))
                {
                    ctx.Active = win;
                    ctx.End = win;
                }
            }

            if ((win.Flags & PanelFlags.Hidden) != 0)
            {
                ctx.Current = win;
                win.Layout = null;
                return false;
            }
            else nk_start(ctx, win);
            if (((win.Flags & PanelFlags.Hidden) == 0) && ((win.Flags & PanelFlags.NonInput) == 0))
            {
                bool inpanel;
                bool ishovered;
                NkWindow iter = win;
                float h =
                    (float)(ctx.Style.Font.Height + 2.0f * style.Window.header.padding.y + (2.0f * style.Window.header.label_padding.y));
                NkRect win_bounds =
                    (NkRect)
                        (((win.Flags & PanelFlags.Minimized) == 0)
                            ? win.Bounds
                            : nk_rect_((float)(win.Bounds.x), (float)(win.Bounds.y), (float)(win.Bounds.w), (float)(h)));
                inpanel =

                        (ctx.Input.nk_input_has_mouse_click_down_in_rect((int)(MouseButtons.Left), (NkRect)(win_bounds), true));
                inpanel = (((inpanel)) && ((ctx.Input.mouse.Buttons[(int)MouseButtons.Left].clicked) != 0));
                ishovered = (ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(win_bounds)));
                if (((win != ctx.Active) && ((ishovered))) && (ctx.Input.mouse.Buttons[(int)MouseButtons.Left].down == 0))
                {
                    iter = win.Next;
                    while ((iter) != null)
                    {
                        NkRect iter_bounds =
                            (NkRect)
                                (((iter.Flags & PanelFlags.Minimized) == 0)
                                    ? iter.Bounds
                                    : nk_rect_((float)(iter.Bounds.x), (float)(iter.Bounds.y), (float)(iter.Bounds.w), (float)(h)));
                        if (
                            (!(((((iter_bounds.x) > (win_bounds.x + win_bounds.w)) || ((iter_bounds.x + iter_bounds.w) < (win_bounds.x))) ||
                                ((iter_bounds.y) > (win_bounds.y + win_bounds.h))) || ((iter_bounds.y + iter_bounds.h) < (win_bounds.y)))) &&
                            ((iter.Flags & PanelFlags.Hidden) == 0)) break;
                        if (((((iter.Popup.win) != null) && ((iter.Popup.active) != 0)) && ((iter.Flags & PanelFlags.Hidden) == 0)) &&
                            (!(((((iter.Popup.win.Bounds.x) > (win.Bounds.x + win_bounds.w)) ||
                                 ((iter.Popup.win.Bounds.x + iter.Popup.win.Bounds.w) < (win.Bounds.x))) ||
                                ((iter.Popup.win.Bounds.y) > (win_bounds.y + win_bounds.h))) ||
                               ((iter.Popup.win.Bounds.y + iter.Popup.win.Bounds.h) < (win_bounds.y))))) break;
                        iter = iter.Next;
                    }
                }
                if ((((iter) != null) && ((inpanel))) && (win != ctx.End))
                {
                    iter = win.Next;
                    while ((iter) != null)
                    {
                        NkRect iter_bounds =
                            (NkRect)
                                (((iter.Flags & PanelFlags.Minimized) == 0)
                                    ? iter.Bounds
                                    : nk_rect_((float)(iter.Bounds.x), (float)(iter.Bounds.y), (float)(iter.Bounds.w), (float)(h)));
                        if (((((iter_bounds.x) <= (ctx.Input.mouse.Pos.x)) && ((ctx.Input.mouse.Pos.x) < (iter_bounds.x + iter_bounds.w))) &&
                             (((iter_bounds.y) <= (ctx.Input.mouse.Pos.y)) && ((ctx.Input.mouse.Pos.y) < (iter_bounds.y + iter_bounds.h)))) &&
                            ((iter.Flags & PanelFlags.Hidden) == 0)) break;
                        if (((((iter.Popup.win) != null) && ((iter.Popup.active) != 0)) && ((iter.Flags & PanelFlags.Hidden) == 0)) &&
                            (!(((((iter.Popup.win.Bounds.x) > (win_bounds.x + win_bounds.w)) ||
                                 ((iter.Popup.win.Bounds.x + iter.Popup.win.Bounds.w) < (win_bounds.x))) ||
                                ((iter.Popup.win.Bounds.y) > (win_bounds.y + win_bounds.h))) ||
                               ((iter.Popup.win.Bounds.y + iter.Popup.win.Bounds.h) < (win_bounds.y))))) break;
                        iter = iter.Next;
                    }
                }
                if ((((iter) != null) && ((win.Flags & PanelFlags.Rom) == 0)) && ((win.Flags & PanelFlags.Background) != 0))
                {
                    win.Flags |= ((PanelFlags.Rom));
                    iter.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
                    ctx.Active = iter;
                    if ((iter.Flags & PanelFlags.Background) == 0)
                    {
                        nk_remove_window(ctx, iter);
                        nk_insert_window(ctx, iter, (int)(NK_INSERT_BACK));
                    }
                }
                else
                {
                    if ((iter == null) && (ctx.End != win))
                    {
                        if ((win.Flags & PanelFlags.Background) == 0)
                        {
                            nk_remove_window(ctx, win);
                            nk_insert_window(ctx, win, (int)(NK_INSERT_BACK));
                        }
                        win.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
                        ctx.Active = win;
                    }
                    if ((ctx.End != win) && ((win.Flags & PanelFlags.Background) == 0)) win.Flags |= (PanelFlags.Rom);
                }
            }

            win.Layout = (NkPanel)(nk_create_panel(ctx));
            ctx.Current = win;
            ret = (nk_panel_begin(ctx, title, (PanelKind.Window)));
            win.Layout.Offset = win.Scrollbar;

            return (ret);
        }

        public static void nk_end(NkContext ctx)
        {
            NkPanel layout;
            if ((ctx == null) || (ctx.Current == null)) return;
            layout = ctx.Current.Layout;
            if ((layout == null) || (((layout.Type) == (PanelKind.Window)) && ((ctx.Current.Flags & PanelFlags.Hidden) != 0)))
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

        public static bool nk_window_has_focus(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return (false);
            return ((ctx.Current) == (ctx.Active));
        }

        public static bool nk_window_is_hovered(NkContext ctx)
        {
            if ((ctx == null) || (ctx.Current == null)) return false;
            if ((ctx.Current.Flags & PanelFlags.Hidden) != 0) return false;
            return (ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(ctx.Current.Bounds)));
        }

        public static bool nk_window_is_any_hovered(NkContext ctx)
        {
            NkWindow iter;
            if (ctx == null) return false;
            iter = ctx.Begin;
            while ((iter) != null)
            {
                if ((iter.Flags & PanelFlags.Hidden) == 0)
                {
                    if ((((iter.Popup.active) != 0) && ((iter.Popup.win) != null)) &&
                        ((ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(iter.Popup.win.Bounds))))) return true;
                    if ((iter.Flags & PanelFlags.Minimized) != 0)
                    {
                        NkRect header = (NkRect)(iter.Bounds);
                        header.h = (float)(ctx.Style.Font.Height + 2 * ctx.Style.Window.header.padding.y);
                        if ((ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(header)))) return true;
                    }
                    else if ((ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(iter.Bounds))))
                    {
                        return true;
                    }
                }
                iter = iter.Next;
            }
            return false;
        }

        public static bool nk_item_is_any_active(NkContext ctx)
        {
            bool any_hovered = (nk_window_is_any_hovered(ctx));
            bool any_active = (int)(ctx.LastWidgetState & WidgetStates.Modified) != 0;
            return (((any_hovered)) || ((any_active)));
        }

        public static bool nk_window_is_collapsed(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return false;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return false;
            return (win.Flags & PanelFlags.Minimized) != 0;
        }

        public static bool nk_window_is_closed(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return true;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return true;
            return (win.Flags & PanelFlags.Closed) != 0;
        }

        public static bool nk_window_is_hidden(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return true;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return true;
            return (win.Flags & PanelFlags.Hidden) != 0;
        }

        public static bool nk_window_is_active(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return false;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return false;
            return ((win) == (ctx.Active));
        }

        public static NkWindow nk_window_find(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            return nk_find_window(ctx, (uint)(title_hash), name);
        }

        public static void nk_window_close(NkContext ctx, char* name)
        {
            NkWindow win;
            if (ctx == null) return;
            win = nk_window_find(ctx, name);
            if (win == null) return;
            if ((ctx.Current) == (win)) return;
            win.Flags |= (PanelFlags.Hidden);
            win.Flags |= (PanelFlags.Closed);
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

        public static void nk_window_collapse(NkContext ctx, char* name, VisibleStates c)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return;
            if ((c) == (VisibleStates.Minimized)) win.Flags |= (PanelFlags.Minimized);
            else win.Flags &= (PanelFlags)(~(uint)(PanelFlags.Minimized));
        }

        public static void nk_window_collapse_if(NkContext ctx, char* name, VisibleStates c, int cond)
        {
            if ((ctx == null) || (cond == 0)) return;
            nk_window_collapse(ctx, name, (c));
        }

        public static void nk_window_show(NkContext ctx, char* name, ShowStates s)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
            win = nk_find_window(ctx, (uint)(title_hash), name);
            if (win == null) return;
            if ((s) == (ShowStates.Hidden))
            {
                win.Flags |= (PanelFlags.Hidden);
            }
            else win.Flags &= (PanelFlags)(~(uint)(PanelFlags.Hidden));
        }

        public static void nk_window_show_if(NkContext ctx, char* name, ShowStates s, int cond)
        {
            if ((ctx == null) || (cond == 0)) return;
            nk_window_show(ctx, name, s);
        }

        public static void nk_window_set_focus(NkContext ctx, char* name)
        {
            int title_len;
            uint title_hash;
            NkWindow win;
            if (ctx == null) return;
            title_len = (int)(nk_strlen(name));
            title_hash = (uint)(nk_murmur_hash(name, (int)(title_len), (uint)(PanelFlags.Title)));
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
            if (((layout.Flags & PanelFlags.Hidden) != 0) || ((layout.Flags & PanelFlags.Minimized) != 0)) return;
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
            if (((layout.Flags & PanelFlags.Hidden) != 0) || ((layout.Flags & PanelFlags.Minimized) != 0)) return;
            layout.Menu.h = (float)(layout.AtY - layout.Menu.y);
            layout.Bounds.y += (float)(layout.Menu.h + ctx.Style.Window.spacing.y + layout.Row.height);
            layout.Bounds.h -= (float)(layout.Menu.h + ctx.Style.Window.spacing.y + layout.Row.height);
            layout.Offset.x = (uint)(layout.Menu.offset.x);
            layout.Offset.y = (uint)(layout.Menu.offset.y);
            layout.AtY = (float)(layout.Bounds.y - layout.Row.height);
            layout.Clip.y = (float)(layout.Bounds.y);
            layout.Clip.h = (float)(layout.Bounds.h);
            _out_.nk_push_scissor((NkRect)(layout.Clip));
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
            if ((layout.Flags & PanelFlags.Dynamic) != 0)
            {
                NkRect background = new NkRect();
                background.x = (float)(win.Bounds.x);
                background.w = (float)(win.Bounds.w);
                background.y = (float)(layout.AtY - 1.0f);
                background.h = (float)(layout.Row.height + 1.0f);
                _out_.nk_fill_rect((NkRect)(background), (float)(0), (NkColor)(color));
            }

        }

        public static void nk_row_layout_(NkContext ctx, LayoutFormat fmt, float height, int cols, int width)
        {
            NkWindow win;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            nk_panel_layout(ctx, win, (float)(height), (int)(cols));
            if ((fmt) == (LayoutFormat.Dynamic)) win.Layout.Row.type = (int)(PanelRowLayoutType.DynamicFixed);
            else win.Layout.Row.type = (PanelRowLayoutType.StatixFixed);
            win.Layout.Row.ratio = null;
            win.Layout.Row.filled = (float)(0);
            win.Layout.Row.item_offset = (float)(0);
            win.Layout.Row.item_width = ((float)(width));
        }

        public static void nk_panel_alloc_row(NkContext ctx, NkWindow win)
        {
            NkPanel layout = win.Layout;
            NkVec2 spacing = (NkVec2)(ctx.Style.Window.spacing);
            float row_height = (float)(layout.Row.height - spacing.y);
            nk_panel_layout(ctx, win, (float)(row_height), (int)(layout.Row.columns));
        }

        public static void nk_text_colored(NkContext ctx, char* str, int len, Align alignment, NkColor color)
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
            win.Buffer.nk_widget_text((NkRect)(bounds), str, (int)(len), &text, (alignment), style.Font);
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
            win.Buffer.nk_widget_text_wrap((NkRect)(bounds), str, (int)(len), &text, style.Font);
        }

        public static void nk_text_(NkContext ctx, char* str, int len, Align alignment)
        {
            if (ctx == null) return;
            nk_text_colored(ctx, str, (int)(len), (alignment), (NkColor)(ctx.Style.Text.color));
        }

        public static void nk_text_wrap(NkContext ctx, char* str, int len)
        {
            if (ctx == null) return;
            nk_text_wrap_colored(ctx, str, (int)(len), (NkColor)(ctx.Style.Text.color));
        }

        public static void nk_label(NkContext ctx, char* str, Align alignment)
        {
            nk_text_(ctx, str, (int)(nk_strlen(str)), (alignment));
        }

        public static void nk_label_colored(NkContext ctx, char* str, Align align, NkColor color)
        {
            nk_text_colored(ctx, str, (int)(nk_strlen(str)), (align), (NkColor)(color));
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
            if (nk_widget(&bounds, ctx) == WidgetLayoutStates.Invalid) return;
            win.Buffer.nk_draw_image((NkRect)(bounds), img, (NkColor)(nk_white));
        }

        public static void nk_button_set_behavior(NkContext ctx, ButtonBehavior behavior)
        {
            if (ctx == null) return;
            ctx.ButtonBehavior = behavior;
        }

        public static bool nk_button_push_behavior(NkContext ctx, ButtonBehavior behavior)
        {
            nk_config_stack_button_behavior button_stack;
            NkConfigStackButtonBehaviorElement element;
            if (ctx == null) return false;
            button_stack = ctx.Stacks.button_behaviors;
            if ((button_stack.head) >= ((int)((int)button_stack.elements.Length))) return false;
            element = button_stack.elements[button_stack.head++];
            element.old_value = ctx.ButtonBehavior;
            ctx.ButtonBehavior = behavior;
            return true;
        }

        public static bool nk_button_pop_behavior(NkContext ctx)
        {
            nk_config_stack_button_behavior button_stack;
            NkConfigStackButtonBehaviorElement element;
            if (ctx == null) return false;
            button_stack = ctx.Stacks.button_behaviors;
            if ((button_stack.head) < (1)) return false;
            element = button_stack.elements[--button_stack.head];
            ctx.ButtonBehavior = element.old_value;
            return true;
        }

        public static bool nk_button_text_styled(NkContext ctx, nk_style_button style, char* title, int len)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if ((((style == null) || (ctx == null)) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return

                    (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), title, (int)(len),
                        (style.text_alignment), ctx.ButtonBehavior, style, _in_, ctx.Style.Font));
        }

        public static bool nk_button_text(NkContext ctx, char* title, int len)
        {
            if (ctx == null) return false;
            return (nk_button_text_styled(ctx, ctx.Style.Button, title, (int)(len)));
        }

        public static bool nk_button_label_styled(NkContext ctx, nk_style_button style, char* title)
        {
            return (nk_button_text_styled(ctx, style, title, (int)(nk_strlen(title))));
        }

        public static bool nk_button_label(NkContext ctx, char* title)
        {
            return (nk_button_text(ctx, title, (int)(nk_strlen(title))));
        }

        public static bool nk_button_color(NkContext ctx, NkColor color)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            nk_style_button button = new nk_style_button();
            bool ret = false;
            NkRect bounds = new NkRect();
            NkRect content = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            button = (nk_style_button)(ctx.Style.Button);
            button.normal = (NkStyleItem)(nk_style_item_color((NkColor)(color)));
            button.hover = (NkStyleItem)(nk_style_item_color((NkColor)(color)));
            button.active = (NkStyleItem)(nk_style_item_color((NkColor)(color)));
            ret =

                    (nk_do_button(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), button, _in_, ctx.ButtonBehavior,
                        &content));
            win.Buffer.nk_draw_button(&bounds, (ctx.LastWidgetState), button);
            return ret;
        }

        public static bool nk_button_symbol_styled(NkContext ctx, nk_style_button style, Symbols symbol)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return

                    (nk_do_button_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (symbol),
                       ctx.ButtonBehavior, style, _in_, ctx.Style.Font));
        }

        public static bool nk_button_symbol(NkContext ctx, Symbols symbol)
        {
            if (ctx == null) return false;
            return (nk_button_symbol_styled(ctx, ctx.Style.Button, (symbol)));
        }

        public static bool nk_button_image_styled(NkContext ctx, nk_style_button style, NkImage img)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return

                    (nk_do_button_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (NkImage)(img),
                        ctx.ButtonBehavior, style, _in_));
        }

        public static bool nk_button_image(NkContext ctx, NkImage img)
        {
            if (ctx == null) return false;
            return (nk_button_image_styled(ctx, ctx.Style.Button, (NkImage)(img)));
        }

        public static bool nk_button_symbol_text_styled(NkContext ctx, nk_style_button style, Symbols symbol, char* text, int len,
            Align align)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return
                    (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (symbol), text,
                        (int)(len), (align), ctx.ButtonBehavior, style, ctx.Style.Font, _in_));
        }

        public static bool nk_button_symbol_text(NkContext ctx, Symbols symbol, char* text, int len, Align align)
        {
            if (ctx == null) return false;
            return (nk_button_symbol_text_styled(ctx, ctx.Style.Button, (symbol), text, (int)(len), (align)));
        }

        public static bool nk_button_symbol_label(NkContext ctx, Symbols symbol, char* label, Align align)
        {
            return (bool)(nk_button_symbol_text(ctx, (symbol), label, (int)(nk_strlen(label)), (align)));
        }

        public static bool nk_button_symbol_label_styled(NkContext ctx, nk_style_button style, Symbols symbol, char* title,
            Align align)
        {
            return
                (bool)(nk_button_symbol_text_styled(ctx, style, (symbol), title, (int)(nk_strlen(title)), (align)));
        }

        public static bool nk_button_image_text_styled(NkContext ctx, nk_style_button style, NkImage img, char* text, int len,
            Align align)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return
                    (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (NkImage)(img), text,
                        (int)(len), (align), ctx.ButtonBehavior, style, ctx.Style.Font, _in_));
        }

        public static bool nk_button_image_text(NkContext ctx, NkImage img, char* text, int len, Align align)
        {
            return
                (nk_button_image_text_styled(ctx, ctx.Style.Button, (NkImage)(img), text, (int)(len), (align)));
        }

        public static bool nk_button_image_label(NkContext ctx, NkImage img, char* label, Align align)
        {
            return (nk_button_image_text(ctx, (NkImage)(img), label, (int)(nk_strlen(label)), (align)));
        }

        public static bool nk_button_image_label_styled(NkContext ctx, nk_style_button style, NkImage img, char* label,
            Align text_alignment)
        {
            return (nk_button_image_text_styled(ctx, style, (NkImage)(img), label, (int)(nk_strlen(label)), (text_alignment)));
        }

        public static bool nk_selectable_text(NkContext ctx, char* str, int len, Align align, ref int value)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            WidgetLayoutStates state;
            NkRect bounds = new NkRect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null))) return false;
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return
                    (nk_do_selectable(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), str, (int)(len), (align),
                        ref value, style.Selectable, _in_, style.Font));
        }

        public static int nk_selectable_image_text(NkContext ctx, NkImage img, char* str, int len, Align align, ref int value)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            WidgetLayoutStates state;
            NkRect bounds = new NkRect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null))) return (int)(0);
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_selectable_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), str, (int)(len), (align),
                        ref value, img, style.Selectable, _in_, style.Font));
        }

        public static int nk_select_text(NkContext ctx, char* str, int len, Align align, int value)
        {
            nk_selectable_text(ctx, str, (int)(len), (align), ref value);
            return (int)(value);
        }

        public static bool nk_selectable_label(NkContext ctx, char* str, Align align, ref int value)
        {
            return (nk_selectable_text(ctx, str, (int)(nk_strlen(str)), (align), ref value));
        }

        public static int nk_selectable_image_label(NkContext ctx, NkImage img, char* str, Align align, ref int value)
        {
            return
                (int)(nk_selectable_image_text(ctx, (NkImage)(img), str, (int)(nk_strlen(str)), (align), ref value));
        }

        public static int nk_select_label(NkContext ctx, char* str, Align align, int value)
        {
            nk_selectable_text(ctx, str, (int)(nk_strlen(str)), (align), ref value);
            return (int)(value);
        }

        public static int nk_select_image_label(NkContext ctx, NkImage img, char* str, Align align, int value)
        {
            nk_selectable_image_text(ctx, (NkImage)(img), str, (int)(nk_strlen(str)), (align), ref value);
            return (int)(value);
        }

        public static int nk_select_image_text(NkContext ctx, NkImage img, char* str, int len, Align align, int value)
        {
            nk_selectable_image_text(ctx, (NkImage)(img), str, (int)(len), (align), ref value);
            return (int)(value);
        }

        public static bool nk_check_text(NkContext ctx, char* text, int len, bool active)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (active);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return (active);
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            nk_do_toggle(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), &active, text, (int)(len),
                (ToggleKind.Check), style.Checkbox, _in_, style.Font);
            return (active);
        }

        public static uint nk_check_flags_text(NkContext ctx, char* text, int len, uint flags, uint value)
        {
            int old_active;
            if ((ctx == null) || (text == null)) return (uint)(flags);
            old_active = ((int)((flags & value) & value));
            if ((nk_check_text(ctx, text, (int)(len), (old_active != 0)))) flags |= (uint)(value);
            else flags &= (uint)(~value);
            return (uint)(flags);
        }

        public static bool nk_checkbox_text(NkContext ctx, char* text, int len, ref bool active)
        {
            bool old_val;
            if (((ctx == null) || (text == null))) return false;
            old_val = (active);
            active = (nk_check_text(ctx, text, (int)(len), (active)));
            return (old_val != active);
        }

        public static bool nk_checkbox_flags_text(NkContext ctx, char* text, int len, uint* flags, uint value)
        {
            bool active;
            if (((ctx == null) || (text == null)) || (flags == null)) return false;
            active = ((int)((*flags & value) & value)) != 0;
            if ((nk_checkbox_text(ctx, text, (int)(len), ref active)))
            {
                if ((active)) *flags |= (uint)(value);
                else *flags &= (uint)(~value);
                return true;
            }

            return false;
        }

        public static bool nk_check_label(NkContext ctx, char* label, bool active)
        {
            return (nk_check_text(ctx, label, (int)(nk_strlen(label)), (active)));
        }

        public static uint nk_check_flags_label(NkContext ctx, char* label, uint flags, uint value)
        {
            return (uint)(nk_check_flags_text(ctx, label, (int)(nk_strlen(label)), (uint)(flags), (uint)(value)));
        }

        public static bool nk_checkbox_label(NkContext ctx, char* label, ref bool active)
        {
            return (nk_checkbox_text(ctx, label, (int)(nk_strlen(label)), ref active));
        }

        public static bool nk_checkbox_flags_label(NkContext ctx, char* label, uint* flags, uint value)
        {
            return (nk_checkbox_flags_text(ctx, label, (int)(nk_strlen(label)), flags, (uint)(value)));
        }

        public static bool nk_option_text(NkContext ctx, char* text, int len, bool is_active)
        {
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (is_active);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            nk_do_toggle(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), &is_active, text, (int)(len),
                (ToggleKind.Option), style.Option, _in_, style.Font);
            return (is_active);
        }

        public static bool nk_radio_text(NkContext ctx, char* text, int len, bool* active)
        {
            bool old_value;
            if (((ctx == null) || (text == null)) || (active == null)) return false;
            old_value = (*active);
            *active = (nk_option_text(ctx, text, (int)(len), (old_value)));
            return (old_value != *active);
        }

        public static bool nk_option_label(NkContext ctx, char* label, bool active)
        {
            return (nk_option_text(ctx, label, (int)(nk_strlen(label)), (active)));
        }

        public static bool nk_radio_label(NkContext ctx, char* label, bool* active)
        {
            return (nk_radio_text(ctx, label, (int)(nk_strlen(label)), active));
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
            WidgetLayoutStates state;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)))
                return (int)(ret);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return (int)(ret);
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
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

        public static int nk_progress(NkContext ctx, ulong* cur, ulong max, bool is_modifyable)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle style;
            nk_input _in_;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            ulong old_value;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (cur == null)) return (int)(0);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            old_value = (ulong)(*cur);
            *cur =
                (ulong)
                    (nk_do_progress(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (ulong)(*cur), (ulong)(max),
                        (is_modifyable), style.Progress, _in_));
            return (*cur != old_value) ? 1 : 0;
        }

        public static ulong nk_prog(NkContext ctx, ulong cur, ulong max, bool modifyable)
        {
            nk_progress(ctx, &cur, (ulong)(max), (modifyable));
            return (ulong)(cur);
        }

        public static void nk_edit_focus(NkContext ctx, EditFlags flags)
        {
            uint hash;
            NkWindow win;
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            hash = (uint)(win.Edit.seq);
            win.Edit.active = 1;
            win.Edit.name = (uint)(hash);
            if ((flags & EditFlags.AlwaysInsertMode) != 0) win.Edit.mode = (NkTextEditMode.Insert);
        }

        public static void nk_edit_unfocus(NkContext ctx)
        {
            NkWindow win;
            if ((ctx == null) || (ctx.Current == null)) return;
            win = ctx.Current;
            win.Edit.active = 0;
            win.Edit.name = (uint)(0);
        }

        public static EditState nk_edit_string(NkContext ctx, EditFlags flags, NkStr str, int max,
            NkPluginFilter filter)
        {
            uint hash;
            EditState state;
            nk_text_edit edit;
            NkWindow win;
            if (((ctx == null))) return (uint)(0);
            filter = (filter == null) ? nk_filter_default : filter;
            win = ctx.Current;
            hash = (uint)(win.Edit.seq);
            edit = ctx.TextEdit;
            nk_textedit_clear_state(ctx.TextEdit,
                ((flags & EditFlags.Multiline) != 0 ? TextEditMode.Multiline : TextEditMode.SingleLine), filter);
            if (((win.Edit.active) != 0) && ((hash) == (win.Edit.name)))
            {
                if ((flags & EditFlags.NoCursor) != 0) edit.cursor = (int)(str.Len);
                else edit.cursor = (int)(win.Edit.cursor);
                if ((flags & EditFlags.Selectable) == 0)
                {
                    edit.select_start = (int)(win.Edit.cursor);
                    edit.select_end = (int)(win.Edit.cursor);
                }
                else
                {
                    edit.select_start = (int)(win.Edit.sel_start);
                    edit.select_end = (int)(win.Edit.sel_end);
                }
                edit.mode = (win.Edit.mode);
                edit.scrollbar.x = ((float)(win.Edit.scrollbar.x));
                edit.scrollbar.y = ((float)(win.Edit.scrollbar.y));
                edit.active = (byte)(1);
            }
            else edit.active = (byte)(0);
            max = (int)((1) < (max) ? (max) : (1));

            if (str.Len > max)
            {
                str.Str = str.Str.Substring(0, max);
            }

            edit._string_ = str;
            state = (nk_edit_buffer(ctx, (flags), edit, filter));
            if ((edit.active) != 0)
            {
                win.Edit.cursor = (int)(edit.cursor);
                win.Edit.sel_start = (int)(edit.select_start);
                win.Edit.sel_end = (int)(edit.select_end);
                win.Edit.mode = (edit.mode);
                win.Edit.scrollbar.x = ((uint)(edit.scrollbar.x));
                win.Edit.scrollbar.y = ((uint)(edit.scrollbar.y));
            }

            return (state);
        }

        public static EditState nk_edit_buffer(NkContext ctx, EditFlags flags, nk_text_edit edit, NkPluginFilter filter)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            int state;
            NkRect bounds = new NkRect();
            EditState ret_flags = (uint)(0);
            byte prev_state;
            uint hash;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (uint)(0);
            win = ctx.Current;
            style = ctx.Style;
            state = (int)(nk_widget(&bounds, ctx));
            if (state == 0) return 0;
            _in_ = (win.Layout.Flags & PanelFlags.Rom) != 0 ? null : ctx.Input;
            hash = (uint)(win.Edit.seq++);
            if (((win.Edit.active) != 0) && ((hash) == (win.Edit.name)))
            {
                if ((flags & EditFlags.NoCursor) != 0) edit.cursor = (int)(edit._string_.Len);
                if ((flags & EditFlags.Selectable) == 0)
                {
                    edit.select_start = (int)(edit.cursor);
                    edit.select_end = (int)(edit.cursor);
                }
                if ((flags & EditFlags.Clipboard) != 0) edit.clip = (NkClipboard)(ctx.Clip);
                edit.active = ((byte)(win.Edit.active));
            }
            else edit.active = (byte)(0);
            edit.mode = (win.Edit.mode);
            filter = (filter == null) ? nk_filter_default : filter;
            prev_state = (byte)(edit.active);
            _in_ = (flags & EditFlags.ReadOnly) != 0 ? null : _in_;
            ret_flags =
                    (nk_do_edit(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (flags), filter, edit, style.Edit,
                        _in_, style.Font));
            if ((ctx.LastWidgetState & WidgetStates.Hover) != 0)
                ctx.Style.CursorActive = ctx.Style.Cursors[(int)CursorKind.Text];
            if (((edit.active) != 0) && (prev_state != edit.active))
            {
                win.Edit.active = 1;
                win.Edit.name = (uint)(hash);
            }
            else if (((prev_state) != 0) && (edit.active == 0))
            {
                win.Edit.active = 0;
            }

            return (ret_flags);
        }



        public static void nk_property_int(this NkContext ctx, char* name, int min, ref int val, int max, int step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if ((((ctx == null) || (ctx.Current == null)) || (name == null))) return;
            variant = (NkPropertyVariant)(nk_property_variant_int((int)(val), (int)(min), (int)(max),
                (int)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (PropertyFilterKind.FilterInt));
            val = (int)(variant.value.i);
        }

        public static void nk_property_float(this NkContext ctx, char* name, float min, ref float val, float max,
            float step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if ((((ctx == null) || (ctx.Current == null)) || (name == null))) return;
            variant =
                (NkPropertyVariant)(nk_property_variant_float((float)(val), (float)(min), (float)(max),
                    (float)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (PropertyFilterKind.FilterFloat));
            val = (float)(variant.value.f);
        }

        public static void nk_property_double(this NkContext ctx, char* name, double min, ref double val, double max,
            double step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if ((((ctx == null) || (ctx.Current == null)) || (name == null))) return;
            variant =
                (NkPropertyVariant)(nk_property_variant_double((double)(val), (double)(min), (double)(max),
                    (double)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (PropertyFilterKind.FilterFloat));
            val = (double)(variant.value.d);
        }

        public static void nk_property_(NkContext ctx, char* name, NkPropertyVariant* variant, float incPerPixel,
         PropertyFilterKind filter)
        {
            var bounds = new NkRect();
            uint hash;
            string dummyBuffer = null;
            PropertyStatus dummyState = PropertyStatus.Default;
            var dummyCursor = 0;
            var dummySelectBegin = 0;
            var dummySelectEnd = 0;
            if (ctx == null || ctx.Current == null || ctx.Current.Layout == null) return;
            var win = ctx.Current;
            var layout = win.Layout;
            var style = ctx.Style;
            var s = nk_widget(&bounds, ctx);
            if (s == 0) return;
            if (name[0] == '#')
            {
                hash = nk_murmur_hash(name, nk_strlen(name), win.Property.seq++);
                name++;
            }
            else hash = nk_murmur_hash(name, nk_strlen(name), 42);

            var _in_ = s == WidgetLayoutStates.ROM && win.Property.active == 0 || (layout.Flags & PanelFlags.Rom) != 0
                ? null
                : ctx.Input;

            PropertyStatus oldState, state;
            string buffer;
            int cursor, selectBegin, selectEnd;
            if (win.Property.active != 0 && hash == win.Property.name)
            {
                oldState = win.Property.state;
                nk_do_property(ref ctx.LastWidgetState, win.Buffer, bounds, name, variant, incPerPixel,
                    ref win.Property.buffer, ref win.Property.state, ref win.Property.cursor,
                    ref win.Property.select_start, ref win.Property.select_end, style.Property, filter, _in_, style.Font,
                    ctx.TextEdit, ctx.ButtonBehavior);
                state = win.Property.state;
                buffer = win.Property.buffer;
                cursor = win.Property.cursor;
                selectBegin = win.Property.select_start;
                selectEnd = win.Property.select_end;
            }
            else
            {
                oldState = dummyState;
                nk_do_property(ref ctx.LastWidgetState, win.Buffer, bounds, name, variant, incPerPixel,
                    ref dummyBuffer, ref dummyState, ref dummyCursor,
                    ref dummySelectBegin, ref dummySelectEnd, style.Property, filter, _in_, style.Font,
                    ctx.TextEdit, ctx.ButtonBehavior);
                state = dummyState;
                buffer = dummyBuffer;
                cursor = dummyCursor;
                selectBegin = dummySelectBegin;
                selectEnd = dummySelectEnd;
            }

            ctx.TextEdit.clip = ctx.Clip;
            if (_in_ != null && state != PropertyStatus.Default && win.Property.active == 0)
            {
                win.Property.active = 1;
                win.Property.buffer = buffer;
                win.Property.cursor = cursor;
                win.Property.state = state;
                win.Property.name = hash;
                win.Property.select_start = selectBegin;
                win.Property.select_end = selectEnd;
                if (state == PropertyStatus.Drag)
                {
                    ctx.Input.mouse.Grab = 1;
                    ctx.Input.mouse.Grabbed = 1;
                }
            }

            if (state == PropertyStatus.Default && oldState != PropertyStatus.Default)
            {
                if (oldState == PropertyStatus.Drag)
                {
                    ctx.Input.mouse.Grab = 0;
                    ctx.Input.mouse.Grabbed = 0;
                    ctx.Input.mouse.Ungrab = 1;
                }
                win.Property.select_start = 0;
                win.Property.select_end = 0;
                win.Property.active = 0;
            }
        }

        public static int nk_propertyi(this NkContext ctx, char* name, int min, int val, int max, int step,
                float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if (((ctx == null) || (ctx.Current == null)) || (name == null)) return (int)(val);
            variant = (NkPropertyVariant)(nk_property_variant_int((int)(val), (int)(min), (int)(max),
                (int)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), PropertyFilterKind.FilterInt);
            val = (int)(variant.value.i);
            return (int)(val);
        }

        public static float nk_propertyf(this NkContext ctx, char* name, float min, float val, float max, float step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if (((ctx == null) || (ctx.Current == null)) || (name == null)) return (float)(val);
            variant =
                (NkPropertyVariant)(nk_property_variant_float((float)(val), (float)(min), (float)(max),
                    (float)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (PropertyFilterKind.FilterFloat));
            val = (float)(variant.value.f);
            return (float)(val);
        }

        public static double nk_propertyd(this NkContext ctx, char* name, double min, double val, double max,
            double step,
            float inc_per_pixel)
        {
            NkPropertyVariant variant = new NkPropertyVariant();
            if (((ctx == null) || (ctx.Current == null)) || (name == null)) return (double)(val);
            variant =
                (NkPropertyVariant)(nk_property_variant_double((double)(val), (double)(min), (double)(max),
                    (double)(step)));
            nk_property_(ctx, name, &variant, (float)(inc_per_pixel), (PropertyFilterKind.FilterFloat));
            val = (double)(variant.value.d);
            return (double)(val);
        }



        public static int nk_color_pick(NkContext ctx, NkColorF* color, ColorFormat fmt)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle config;
            nk_input _in_;
            WidgetLayoutStates state;
            NkRect bounds = new NkRect();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (color == null)) return (int)(0);
            win = ctx.Current;
            config = ctx.Style;
            layout = win.Layout;
            state = (nk_widget(&bounds, ctx));
            if (state == 0) return (int)(0);
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            return
                (int)
                    (nk_do_color_picker(ref ctx.LastWidgetState, win.Buffer, color, (fmt), (NkRect)(bounds),
                        (NkVec2)(nk_vec2_((float)(0), (float)(0))), _in_, config.Font));
        }

        public static NkColorF nk_color_picker(NkContext ctx, NkColorF color, ColorFormat fmt)
        {
            nk_color_pick(ctx, &color, (fmt));
            return (NkColorF)(color);
        }

        public static int nk_chart_begin_colored(NkContext ctx, ChartKind type, NkColor color, NkColor highlight, int count,
            float min_value, float max_value)
        {
            NkWindow win;
            NkChart chart;
            NkStyle config;
            nk_style_chart style;
            NkStyleItem background;
            NkRect bounds = new NkRect();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(0);
            if (nk_widget(&bounds, ctx) == WidgetLayoutStates.Invalid)
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
                slot.type = (type);
                slot.count = (int)(count);
                slot.color = (NkColor)(color);
                slot.highlight = (NkColor)(highlight);
                slot.min = (float)((min_value) < (max_value) ? (min_value) : (max_value));
                slot.max = (float)((min_value) < (max_value) ? (max_value) : (min_value));
                slot.range = (float)(slot.max - slot.min);
            }

            background = style.background;
            if ((background.Type) == (StyleItemKind.Image))
            {
                win.Buffer.nk_draw_image((NkRect)(bounds), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                win.Buffer.nk_fill_rect((NkRect)(bounds), (float)(style.rounding), (NkColor)(style.border_color));
                win.Buffer.nk_fill_rect((NkRect)(nk_shrink_rect_((NkRect)(bounds), (float)(style.border))),
                    (float)(style.rounding), (NkColor)(style.background.Data.Color));
            }

            return (int)(1);
        }

        public static int nk_chart_begin(NkContext ctx, ChartKind type, int count, float min_value, float max_value)
        {
            return
                (int)
                    (nk_chart_begin_colored(ctx, (type), (NkColor)(ctx.Style.Chart.color),
                        (NkColor)(ctx.Style.Chart.selected_color), (int)(count), (float)(min_value), (float)(max_value)));
        }

        public static void nk_chart_add_slot_colored(NkContext ctx, ChartKind type, NkColor color, NkColor highlight, int count,
            float min_value, float max_value)
        {
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            if ((ctx.Current.Layout.Chart.Slot) >= (4)) return;
            {
                NkChart chart = ctx.Current.Layout.Chart;
                nk_chart_slot slot = chart.Slots[chart.Slot++];
                slot.type = (type);
                slot.count = (int)(count);
                slot.color = (NkColor)(color);
                slot.highlight = (NkColor)(highlight);
                slot.min = (float)((min_value) < (max_value) ? (min_value) : (max_value));
                slot.max = (float)((min_value) < (max_value) ? (max_value) : (min_value));
                slot.range = (float)(slot.max - slot.min);
            }

        }

        public static void nk_chart_add_slot(NkContext ctx, ChartKind type, int count, float min_value, float max_value)
        {
            nk_chart_add_slot_colored(ctx, (type), (NkColor)(ctx.Style.Chart.color),
                (NkColor)(ctx.Style.Chart.selected_color), (int)(count), (float)(min_value), (float)(max_value));
        }

        public static ChartEvent nk_chart_push_line(NkContext ctx, NkWindow win, NkChart g, float value, int slot)
        {
            NkPanel layout = win.Layout;
            nk_input i = ctx.Input;
            NkCommandBuffer _out_ = win.Buffer;
            ChartEvent ret = (uint)(0);
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
                if (((layout.Flags & PanelFlags.Rom) == 0) &&
                    ((((g.Slots[slot].last.x - 3) <= (i.mouse.Pos.x)) && ((i.mouse.Pos.x) < (g.Slots[slot].last.x - 3 + 6))) &&
                     (((g.Slots[slot].last.y - 3) <= (i.mouse.Pos.y)) && ((i.mouse.Pos.y) < (g.Slots[slot].last.y - 3 + 6)))))
                {
                    ret = ((i.nk_input_is_mouse_hovering_rect((NkRect)(bounds))) ? ChartEvent.Hovering : 0);
                    ret |=

                            ((((i.mouse.Buttons[(int)MouseButtons.Left].down) != 0) && ((i.mouse.Buttons[(int)MouseButtons.Left].clicked) != 0))
                                ? ChartEvent.Clicked
                                : 0);
                    color = (NkColor)(g.Slots[slot].highlight);
                }
                _out_.nk_fill_rect((NkRect)(bounds), (float)(0), (NkColor)(color));
                g.Slots[slot].index += (int)(1);
                return (ret);
            }

            color = (NkColor)(g.Slots[slot].color);
            cur.x = (float)(g.X + (step * (float)(g.Slots[slot].index)));
            cur.y = (float)((g.Y + g.H) - (ratio * g.H));
            _out_.nk_stroke_line((float)(g.Slots[slot].last.x), (float)(g.Slots[slot].last.y), (float)(cur.x),
                (float)(cur.y), (float)(1.0f), (NkColor)(color));
            bounds.x = (float)(cur.x - 3);
            bounds.y = (float)(cur.y - 3);
            bounds.w = (float)(bounds.h = (float)(6));
            if ((layout.Flags & PanelFlags.Rom) == 0)
            {
                if ((i.nk_input_is_mouse_hovering_rect((NkRect)(bounds))))
                {
                    ret = (ChartEvent.Hovering);
                    ret |=
                            (((i.mouse.Buttons[(int)MouseButtons.Left].down == 0) && ((i.mouse.Buttons[(int)MouseButtons.Left].clicked) != 0))
                                ? ChartEvent.Clicked
                                : 0);
                    color = (NkColor)(g.Slots[slot].highlight);
                }
            }

            _out_.nk_fill_rect((NkRect)(nk_rect_((float)(cur.x - 2), (float)(cur.y - 2), (float)(4), (float)(4))),
                (float)(0), (NkColor)(color));
            g.Slots[slot].last.x = (float)(cur.x);
            g.Slots[slot].last.y = (float)(cur.y);
            g.Slots[slot].index += (int)(1);
            return (ret);
        }

        public static ChartEvent nk_chart_push_column(NkContext ctx, NkWindow win, NkChart chart, float value, int slot)
        {
            NkCommandBuffer _out_ = win.Buffer;
            nk_input _in_ = ctx.Input;
            NkPanel layout = win.Layout;
            float ratio;
            ChartEvent ret = (uint)(0);
            NkColor color = new NkColor();
            NkRect item = new NkRect();
            if ((chart.Slots[slot].index) >= (chart.Slots[slot].count)) return 0;
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
            if (((layout.Flags & PanelFlags.Rom) == 0) &&
                ((((item.x) <= (_in_.mouse.Pos.x)) && ((_in_.mouse.Pos.x) < (item.x + item.w))) &&
                 (((item.y) <= (_in_.mouse.Pos.y)) && ((_in_.mouse.Pos.y) < (item.y + item.h)))))
            {
                ret = (ChartEvent.Hovering);
                ret |=

                        (((((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->down == 0) &&
                          ((((nk_mouse_button*)_in_.mouse.Buttons + (int)MouseButtons.Left)->clicked) != 0))
                            ? ChartEvent.Clicked
                            : 0);
                color = (NkColor)(chart.Slots[slot].highlight);
            }

            _out_.nk_fill_rect((NkRect)(item), (float)(0), (NkColor)(color));
            chart.Slots[slot].index += (int)(1);
            return (ret);
        }

        public static ChartEvent nk_chart_push_slot(NkContext ctx, float value, int slot)
        {
            ChartEvent flags;
            NkWindow win;
            if (((ctx == null) || (ctx.Current == null)) || ((slot) >= (4))) return 0;
            if ((slot) >= (ctx.Current.Layout.Chart.Slot)) return 0;
            win = ctx.Current;
            if ((win.Layout.Chart.Slot) < (slot)) return 0;
            switch (win.Layout.Chart.Slots[slot].type)
            {
                case ChartKind.Lines:
                    flags = (nk_chart_push_line(ctx, win, win.Layout.Chart, (float)(value), (int)(slot)));
                    break;
                case ChartKind.Collumn:
                    flags = (nk_chart_push_column(ctx, win, win.Layout.Chart, (float)(value), (int)(slot)));
                    break;
                default:
                case ChartKind.NK_CHART_MAX:
                    flags = (uint)(0);
                    break;
            }

            return (flags);
        }

        public static ChartEvent nk_chart_push(NkContext ctx, float value)
        {
            return (nk_chart_push_slot(ctx, (float)(value), (int)(0)));
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

        public static void nk_plot(NkContext ctx, ChartKind type, float* values, int count, int offset)
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
            if ((nk_chart_begin(ctx, (type), (int)(count), (float)(min_value), (float)(max_value))) != 0)
            {
                for (i = (int)(0); (i) < (count); ++i)
                {
                    nk_chart_push(ctx, (float)(values[i + offset]));
                }
                nk_chart_end(ctx);
            }

        }

        public static void nk_plot_function(NkContext ctx, ChartKind type, void* userdata, NkFloatValueGetter value_getter,
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
            if ((nk_chart_begin(ctx, (type), (int)(count), (float)(min_value), (float)(max_value))) != 0)
            {
                for (i = (int)(0); (i) < (count); ++i)
                {
                    nk_chart_push(ctx, (float)(value_getter(userdata, (int)(i + offset))));
                }
                nk_chart_end(ctx);
            }

        }

        public static int nk_group_scrolled_offset_begin(NkContext ctx, nk_scroll offset, char* title, PanelFlags flags)
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
                    ((flags & PanelFlags.Movable) == 0))
                {
                    return (int)(0);
                }
            }

            if ((win.Flags & PanelFlags.Rom) != 0) flags |= (PanelFlags.Rom);

            panel.Bounds = (NkRect)(bounds);
            panel.Flags = (flags);
            panel.Scrollbar.x = offset.x;
            panel.Scrollbar.y = offset.y;
            panel.Buffer = (NkCommandBuffer)(win.Buffer);
            panel.Layout = (NkPanel)(nk_create_panel(ctx));
            ctx.Current = panel;
            nk_panel_begin(ctx, (flags & PanelFlags.Title) != 0 ? title : null, (PanelKind.Group));
            win.Buffer = (NkCommandBuffer)(panel.Buffer);
            win.Buffer.Clip = (NkRect)(panel.Layout.Clip);
            panel.Layout.Offset = offset;

            panel.Layout.Parent = win.Layout;
            win.Layout = panel.Layout;
            ctx.Current = win;
            if (((panel.Layout.Flags & PanelFlags.Closed) != 0) || ((panel.Layout.Flags & PanelFlags.Minimized) != 0))
            {
                var f = (panel.Layout.Flags);
                nk_group_scrolled_end(ctx);
                if ((f & PanelFlags.Closed) != 0) return (int)(PanelFlags.Closed);
                if ((f & PanelFlags.Minimized) != 0) return (int)(PanelFlags.Minimized);
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

            panel_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (PanelKind.Group)));
            pan.Bounds.y = (float)(g.Bounds.y - (g.HeaderHeight + g.Menu.h));
            pan.Bounds.x = (float)(g.Bounds.x - panel_padding.x);
            pan.Bounds.w = (float)(g.Bounds.w + 2 * panel_padding.x);
            pan.Bounds.h = (float)(g.Bounds.h + g.HeaderHeight + g.Menu.h);
            if ((g.Flags & PanelFlags.Border) != 0)
            {
                pan.Bounds.x -= (float)(g.Border);
                pan.Bounds.y -= (float)(g.Border);
                pan.Bounds.w += (float)(2 * g.Border);
                pan.Bounds.h += (float)(2 * g.Border);
            }

            if ((g.Flags & PanelFlags.NoScrollbar) == 0)
            {
                pan.Bounds.w += (float)(ctx.Style.Window.scrollbar_size.x);
                pan.Bounds.h += (float)(ctx.Style.Window.scrollbar_size.y);
            }

            pan.Scrollbar.x = (uint)(g.Offset.x);
            pan.Scrollbar.y = (uint)(g.Offset.y);
            pan.Flags = (g.Flags);
            pan.Buffer = (NkCommandBuffer)(win.Buffer);
            pan.Layout = g;
            pan.Parent = win;
            ctx.Current = pan;
            nk_unify(ref clip, ref parent.Clip, (float)(pan.Bounds.x), (float)(pan.Bounds.y),
                (float)(pan.Bounds.x + pan.Bounds.w), (float)(pan.Bounds.y + pan.Bounds.h + panel_padding.x));
            pan.Buffer.nk_push_scissor((NkRect)(clip));
            nk_end(ctx);
            win.Buffer = (NkCommandBuffer)(pan.Buffer);
            win.Buffer.nk_push_scissor((NkRect)(parent.Clip));
            ctx.Current = win;
            win.Layout = parent;
            g.Bounds = (NkRect)(pan.Bounds);
            return;
        }

        public static int nk_group_scrolled_begin(NkContext ctx, nk_scroll scroll, char* title, PanelFlags flags)
        {
            return (int)(nk_group_scrolled_offset_begin(ctx, scroll, title, (flags)));
        }

        public static int nk_group_begin_titled(NkContext ctx, char* id, char* title, PanelFlags flags)
        {
            int id_len;
            uint id_hash;
            NkWindow win;
            uint* x_offset;
            uint* y_offset;
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (id == null)) return (int)(0);
            win = ctx.Current;
            id_len = (int)(nk_strlen(id));
            id_hash = (uint)(nk_murmur_hash(id, (int)(id_len), (uint)(PanelKind.Group)));
            x_offset = win.nk_find_value((uint)(id_hash));
            if (x_offset == null)
            {
                x_offset = nk_add_value(ctx, win, (uint)(id_hash), (uint)(0));
                y_offset = nk_add_value(ctx, win, (uint)(id_hash + 1), (uint)(0));
                if ((x_offset == null) || (y_offset == null)) return (int)(0);
                *x_offset = (uint)(*y_offset = (uint)(0));
            }
            else y_offset = win.nk_find_value((uint)(id_hash + 1));
            return
                (int)(nk_group_scrolled_offset_begin(ctx, new nk_scroll { x = *x_offset, y = *y_offset }, title, (flags)));
        }

        public static int nk_group_begin(NkContext ctx, char* title, PanelFlags flags)
        {
            return (int)(nk_group_begin_titled(ctx, title, title, (flags)));
        }

        public static void nk_group_end(NkContext ctx)
        {
            nk_group_scrolled_end(ctx);
        }

        public static int nk_list_view_begin(NkContext ctx, nk_list_view view, char* title, PanelFlags flags, int row_height,
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
            title_hash = (uint)(nk_murmur_hash(title, (int)(title_len), (uint)(PanelKind.Group)));
            x_offset = win.nk_find_value((uint)(title_hash));
            if (x_offset == null)
            {
                x_offset = nk_add_value(ctx, win, (uint)(title_hash), (uint)(0));
                y_offset = nk_add_value(ctx, win, (uint)(title_hash + 1), (uint)(0));
                if ((x_offset == null) || (y_offset == null)) return (int)(0);
                *x_offset = (uint)(*y_offset = (uint)(0));
            }
            else y_offset = win.nk_find_value((uint)(title_hash + 1));
            view.scroll_value = *y_offset;
            view.scroll_pointer = y_offset;
            *y_offset = (uint)(0);
            result =
                (int)(nk_group_scrolled_offset_begin(ctx, new nk_scroll { x = *x_offset, y = *y_offset }, title, (flags)));
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

        public static int nk_popup_begin(NkContext ctx, PopupKind type, char* title, PanelFlags flags, NkRect rect)
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
            title_hash = (uint)(nk_murmur_hash(title, (int)(title_len), (uint)(PanelKind.Popup)));
            popup = win.Popup.win;
            if (popup == null)
            {
                popup = (NkWindow)(ctx.nk_create_window());
                popup.Parent = win;
                win.Popup.win = popup;
                win.Popup.active = (int)(0);
                win.Popup.type = (PanelKind.Popup);
            }

            if (win.Popup.name != title_hash)
            {
                if (win.Popup.active == 0)
                {
                    win.Popup.name = (uint)(title_hash);
                    win.Popup.active = (int)(1);
                    win.Popup.type = (PanelKind.Popup);
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
            popup.Flags = (flags);
            popup.Flags |= (PanelFlags.Border);
            if ((type) == (PopupKind.PopupDynamic)) popup.Flags |= (PanelFlags.Dynamic);
            nk_start_popup(ctx, win);
            popup.Buffer = (NkCommandBuffer)(win.Buffer);
            popup.Buffer.nk_push_scissor((NkRect)(nk_null_rect));
            if ((nk_panel_begin(ctx, title, (PanelKind.Popup))))
            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (PanelFlags.Rom);
                    root.Flags &= (PanelFlags)(~(uint)(PanelFlags.RemoveRom));
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
                    root.Flags |= (PanelFlags.RemoveRom);
                    root = root.Parent;
                }
                win.Popup.active = (int)(0);
                ctx.Current = win;
                nk_free_panel(ctx, popup.Layout);
                popup.Layout = null;
                return (int)(0);
            }

        }

        public static bool nk_nonblock_begin(NkContext ctx, PanelFlags flags, NkRect body, NkRect header, PanelKind panel_type)
        {
            NkWindow popup;
            NkWindow win;
            NkPanel panel;
            bool is_active = true;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            panel = win.Layout;
            popup = win.Popup.win;
            if (popup == null)
            {
                popup = (NkWindow)(ctx.nk_create_window());
                popup.Parent = win;
                win.Popup.win = popup;
                win.Popup.type = (panel_type);
                popup.Buffer.nk_command_buffer_init(true);
            }
            else
            {
                bool pressed;
                bool in_body;
                bool in_header;
                pressed = (ctx.Input.nk_input_is_mouse_pressed((int)(MouseButtons.Left)));
                in_body = (ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(body)));
                in_header = (ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(header)));
                if (((pressed)) && ((in_body == false) || ((in_header)))) is_active = false;
            }

            win.Popup.header = (NkRect)(header);
            if (is_active == false)
            {
                NkPanel root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (PanelFlags.RemoveRom);
                    root = root.Parent;
                }
                return (is_active);
            }

            popup.Bounds = (NkRect)(body);
            popup.Parent = win;
            popup.Layout = (NkPanel)(nk_create_panel(ctx));
            popup.Flags = (flags);
            popup.Flags |= (PanelFlags.Border);
            popup.Flags |= (PanelFlags.Dynamic);
            popup.Seq = (uint)(ctx.Seq);
            win.Popup.active = (int)(1);
            nk_start_popup(ctx, win);
            popup.Buffer = (NkCommandBuffer)(win.Buffer);
            popup.Buffer.nk_push_scissor((NkRect)(nk_null_rect));
            ctx.Current = popup;
            nk_panel_begin(ctx, null, (panel_type));
            win.Buffer = (NkCommandBuffer)(popup.Buffer);
            popup.Layout.Parent = win.Layout;
            popup.Layout.Offset = popup.Scrollbar;

            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (PanelFlags.Rom);
                    root = root.Parent;
                }
            }

            return (is_active);
        }

        public static void nk_popup_close(NkContext ctx)
        {
            NkWindow popup;
            if ((ctx == null) || (ctx.Current == null)) return;
            popup = ctx.Current;
            popup.Flags |= (PanelFlags.Hidden);
        }

        public static void nk_popup_end(NkContext ctx)
        {
            NkWindow win;
            NkWindow popup;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            popup = ctx.Current;
            if (popup.Parent == null) return;
            win = popup.Parent;
            if ((popup.Flags & PanelFlags.Hidden) != 0)
            {
                NkPanel root;
                root = win.Layout;
                while ((root) != null)
                {
                    root.Flags |= (PanelFlags.RemoveRom);
                    root = root.Parent;
                }
                win.Popup.active = (int)(0);
            }

            popup.Buffer.nk_push_scissor((NkRect)(nk_null_rect));
            nk_end(ctx);
            win.Buffer = (NkCommandBuffer)(popup.Buffer);
            nk_finish_popup(ctx, win);
            ctx.Current = win;
            win.Buffer.nk_push_scissor((NkRect)(win.Layout.Clip));
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
            if (((win.Popup.win) != null) && ((win.Popup.type & PanelKind.SetNonBlock) != 0)) return (int)(0);
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
                    (nk_popup_begin(ctx, (PopupKind.PopupDynamic), "__##Tooltip##__",
                        (PanelFlags.NoScrollbar | PanelFlags.Border), (NkRect)(bounds)));
            if ((ret) != 0) win.Layout.Flags &= (PanelFlags)(~(uint)(PanelFlags.Rom));
            win.Popup.type = (PanelKind.Tooltip);
            ctx.Current.Layout.Type = (PanelKind.Tooltip);
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
                Layout.nk_layout_row_dynamic(ctx, (float)(text_height), (int)(1));
                nk_text_(ctx, text, (int)(text_len), (Align.MiddleLeft));
                nk_tooltip_end(ctx);
            }

        }

        public static bool nk_contextual_begin(NkContext ctx, PanelFlags flags, NkVec2 size, NkRect trigger_bounds)
        {
            NkWindow win;
            NkWindow popup;
            NkRect body = new NkRect();
            NkRect null_rect = new NkRect();
            bool is_clicked = false;
            bool is_active = false;
            bool is_open = false;
            bool ret = false;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            ++win.Popup.con_count;
            popup = win.Popup.win;
            is_open = (((popup) != null) && ((win.Popup.type) == (PanelKind.Contextual)));
            is_clicked = (ctx.Input.nk_input_mouse_clicked((MouseButtons.Right), (NkRect)(trigger_bounds)));
            if (((win.Popup.active_con) != 0) && (win.Popup.con_count != win.Popup.active_con)) return false;
            if (((((is_clicked)) && ((is_open))) && (!is_active)) ||
                (((!is_open) && (!is_active)) && (!is_clicked))) return false;
            win.Popup.active_con = (uint)(win.Popup.con_count);
            if ((is_clicked))
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
                    (nk_nonblock_begin(ctx, (flags | PanelFlags.NoScrollbar), (NkRect)(body), (NkRect)(null_rect),
                        (PanelKind.Contextual)));
            if ((ret)) win.Popup.type = (PanelKind.Contextual);
            else
            {
                win.Popup.active_con = (uint)(0);
                if ((win.Popup.win) != null) win.Popup.win.Flags = (uint)(0);
            }

            return (ret);
        }

        public static bool nk_contextual_item_text(NkContext ctx, char* text, int len, Align alignment)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            state = (nk_widget_fitting(&bounds, ctx, (NkVec2)(style.ContextualButton.padding)));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), text, (int)(len), (alignment),
                    ButtonBehavior.Default, style.ContextualButton, _in_, style.Font)))
            {
                nk_contextual_close(ctx);
                return true;
            }

            return false;
        }

        public static bool nk_contextual_item_label(NkContext ctx, char* label, Align align)
        {
            return (nk_contextual_item_text(ctx, label, (int)(nk_strlen(label)), (align)));
        }

        public static bool nk_contextual_item_image_text(NkContext ctx, NkImage img, char* text, int len, Align align)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            state = (nk_widget_fitting(&bounds, ctx, (NkVec2)(style.ContextualButton.padding)));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (NkImage)(img), text,
                    (int)(len), (align), ButtonBehavior.Default, style.ContextualButton, style.Font, _in_)))
            {
                nk_contextual_close(ctx);
                return true;
            }

            return false;
        }

        public static bool nk_contextual_item_image_label(NkContext ctx, NkImage img, char* label, Align align)
        {
            return (nk_contextual_item_image_text(ctx, (NkImage)(img), label, (int)(nk_strlen(label)), (align)));
        }

        public static bool nk_contextual_item_symbol_text(NkContext ctx, Symbols symbol, char* text, int len, Align align)
        {
            NkWindow win;
            nk_input _in_;
            NkStyle style;
            NkRect bounds = new NkRect();
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            state = (nk_widget_fitting(&bounds, ctx, (NkVec2)(style.ContextualButton.padding)));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(bounds), (symbol), text,
                    (int)(len), (align), ButtonBehavior.Default, style.ContextualButton, style.Font, _in_)))
            {
                nk_contextual_close(ctx);
                return true;
            }

            return false;
        }

        public static bool nk_contextual_item_symbol_label(NkContext ctx, Symbols symbol, char* text, Align align)
        {
            return (nk_contextual_item_symbol_text(ctx, (symbol), text, (int)(nk_strlen(text)), (align)));
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
            if ((panel.Flags & PanelFlags.Dynamic) != 0)
            {
                NkRect body = new NkRect();
                if ((panel.AtY) < (panel.Bounds.y + panel.Bounds.h))
                {
                    NkVec2 padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (panel.Type)));
                    body = (NkRect)(panel.Bounds);
                    body.y = (float)(panel.AtY + panel.FooterHeight + panel.Border + padding.y + panel.Row.height);
                    body.h = (float)((panel.Bounds.y + panel.Bounds.h) - body.y);
                }
                {
                    bool pressed = (ctx.Input.nk_input_is_mouse_pressed((int)(MouseButtons.Left)));
                    bool in_body = (ctx.Input.nk_input_is_mouse_hovering_rect((NkRect)(body)));
                    if (((pressed)) && ((in_body))) popup.Flags |= (PanelFlags.Hidden);
                }
            }

            if ((popup.Flags & PanelFlags.Hidden) != 0) popup.Seq = (uint)(0);
            nk_popup_end(ctx);
            return;
        }

        public static bool nk_combo_begin(NkContext ctx, NkWindow win, NkVec2 size, bool is_clicked, NkRect header)
        {
            NkWindow popup;
            bool is_open = false;
            bool is_active = false;
            NkRect body = new NkRect();
            uint hash;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            popup = win.Popup.win;
            body.x = (float)(header.x);
            body.w = (float)(size.x);
            body.y = (float)(header.y + header.h - ctx.Style.Window.combo_border);
            body.h = (float)(size.y);
            hash = (uint)(win.Popup.combo_count++);
            is_open = ((popup != null));
            is_active =
                ((((popup) != null) && ((win.Popup.name) == (hash))) && ((win.Popup.type) == (PanelKind.Combo)));
            if ((((((is_clicked)) && ((is_open))) && (is_active == false)) || (((is_open)) && (is_active == false))) ||
                (((is_open == false) && (is_active == false)) && (is_clicked == false))) return false;
            if (
                nk_nonblock_begin(ctx, (uint)(0), (NkRect)(body),
                    (NkRect)
                        ((((is_clicked)) && ((is_open))) ? nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)) : header),
                    (PanelKind.Combo)) == false) return false;
            win.Popup.type = (PanelKind.Combo);
            win.Popup.name = (uint)(hash);
            return true;
        }

        public static bool nk_combo_begin_text(NkContext ctx, char* selected, int len, NkVec2 size)
        {
            nk_input _in_;
            NkWindow win;
            NkStyle style;
            WidgetLayoutStates s;
            bool is_clicked = false;
            NkRect header = new NkRect();
            NkStyleItem background;
            nk_text text = new nk_text();
            if ((((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) || (selected == null))
                return false;
            win = ctx.Current;
            style = ctx.Style;
            s = (nk_widget(&header, ctx));
            if ((s) == (WidgetLayoutStates.Invalid)) return false;
            _in_ = (((win.Layout.Flags & PanelFlags.Rom) != 0) || ((s) == (WidgetLayoutStates.ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, ButtonBehavior.Default)))
                is_clicked = true;
            if ((ctx.LastWidgetState & WidgetStates.Actived) != 0)
            {
                background = style.Combo.active;
                text.text = (NkColor)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & WidgetStates.Hover) != 0)
            {
                background = style.Combo.hover;
                text.text = (NkColor)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                text.text = (NkColor)(style.Combo.label_normal);
            }

            if ((background.Type) == (StyleItemKind.Image))
            {
                text.background = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                win.Buffer.nk_fill_rect((NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                win.Buffer.nk_stroke_rect((NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect label = new NkRect();
                NkRect button = new NkRect();
                NkRect content = new NkRect();
                Symbols sym;
                if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) sym = (style.Combo.sym_hover);
                else if ((is_clicked)) sym = (style.Combo.sym_active);
                else sym = (style.Combo.sym_normal);
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
                win.Buffer.nk_widget_text((NkRect)(label), selected, (int)(len), &text, (Align.MiddleLeft), ctx.Style.Font);
                win.Buffer.nk_draw_button_symbol(&button, &content, (ctx.LastWidgetState), ctx.Style.Combo.button,
                   (sym), style.Font);
            }

            return (nk_combo_begin(ctx, win, (NkVec2)(size), (is_clicked), (NkRect)(header)));
        }

        public static bool nk_combo_begin_label(NkContext ctx, char* selected, NkVec2 size)
        {
            return (nk_combo_begin_text(ctx, selected, (int)(nk_strlen(selected)), (NkVec2)(size)));
        }

        public static bool nk_combo_begin_color(NkContext ctx, NkColor color, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates s;
            NkStyleItem background;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            s = nk_widget(&header, ctx);
            if ((s) == (WidgetLayoutStates.Invalid)) return false;
            _in_ = (((win.Layout.Flags & PanelFlags.Rom) != 0) || ((s) == (WidgetLayoutStates.ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, ButtonBehavior.Default)))
                is_clicked = true;
            if ((ctx.LastWidgetState & WidgetStates.Actived) != 0) background = style.Combo.active;
            else if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) background = style.Combo.hover;
            else background = style.Combo.normal;
            if ((background.Type) == (StyleItemKind.Image))
            {
                win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                win.Buffer.nk_fill_rect((NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                win.Buffer.nk_stroke_rect((NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                NkRect bounds = new NkRect();
                Symbols sym;
                if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) sym = (style.Combo.sym_hover);
                else if ((is_clicked)) sym = (style.Combo.sym_active);
                else sym = (style.Combo.sym_normal);
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
                win.Buffer.nk_fill_rect((NkRect)(bounds), (float)(0), (NkColor)(color));
                win.Buffer.nk_draw_button_symbol(&button, &content, (ctx.LastWidgetState), ctx.Style.Combo.button,
                    (sym), style.Font);
            }

            return (nk_combo_begin(ctx, win, (NkVec2)(size), (is_clicked), (NkRect)(header)));
        }

        public static bool nk_combo_begin_symbol(NkContext ctx, Symbols symbol, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates s;
            NkStyleItem background;
            NkColor sym_background = new NkColor();
            NkColor symbol_color = new NkColor();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            s = nk_widget(&header, ctx);
            if ((s) == (WidgetLayoutStates.Invalid)) return false;
            _in_ = (((win.Layout.Flags & PanelFlags.Rom) != 0) || ((s) == (WidgetLayoutStates.ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, ButtonBehavior.Default)))
                is_clicked = true;
            if ((ctx.LastWidgetState & WidgetStates.Actived) != 0)
            {
                background = style.Combo.active;
                symbol_color = (NkColor)(style.Combo.symbol_active);
            }
            else if ((ctx.LastWidgetState & WidgetStates.Hover) != 0)
            {
                background = style.Combo.hover;
                symbol_color = (NkColor)(style.Combo.symbol_hover);
            }
            else
            {
                background = style.Combo.normal;
                symbol_color = (NkColor)(style.Combo.symbol_hover);
            }

            if ((background.Type) == (StyleItemKind.Image))
            {
                sym_background = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                sym_background = (NkColor)(background.Data.Color);
                win.Buffer.nk_fill_rect((NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                win.Buffer.nk_stroke_rect((NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect bounds = new NkRect();
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                Symbols sym;
                if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) sym = (style.Combo.sym_hover);
                else if ((is_clicked)) sym = (style.Combo.sym_active);
                else sym = (style.Combo.sym_normal);
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
                win.Buffer.nk_draw_symbol((symbol), (NkRect)(bounds), (NkColor)(sym_background),
                    (NkColor)(symbol_color), (float)(1.0f), style.Font);
                win.Buffer.nk_draw_button_symbol(&bounds, &content, (ctx.LastWidgetState), ctx.Style.Combo.button,
                   (sym), style.Font);
            }

            return (nk_combo_begin(ctx, win, (NkVec2)(size), (is_clicked), (NkRect)(header)));
        }

        public static bool nk_combo_begin_symbol_text(NkContext ctx, char* selected, int len, Symbols symbol, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates s;
            NkStyleItem background;
            NkColor symbol_color = new NkColor();
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            s = nk_widget(&header, ctx);
            if (s == 0) return false;
            _in_ = (((win.Layout.Flags & PanelFlags.Rom) != 0) || ((s) == (WidgetLayoutStates.ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, ButtonBehavior.Default)))
                is_clicked = true;
            if ((ctx.LastWidgetState & WidgetStates.Actived) != 0)
            {
                background = style.Combo.active;
                symbol_color = (NkColor)(style.Combo.symbol_active);
                text.text = (NkColor)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & WidgetStates.Hover) != 0)
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

            if ((background.Type) == (StyleItemKind.Image))
            {
                text.background = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                win.Buffer.nk_fill_rect((NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                win.Buffer.nk_stroke_rect((NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                NkRect label = new NkRect();
                NkRect image = new NkRect();
                Symbols sym;
                if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) sym = (style.Combo.sym_hover);
                else if ((is_clicked)) sym = (style.Combo.sym_active);
                else sym = (style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.x);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                win.Buffer.nk_draw_button_symbol(&button, &content, (ctx.LastWidgetState), ctx.Style.Combo.button,
                    (sym), style.Font);
                image.x = (float)(header.x + style.Combo.content_padding.x);
                image.y = (float)(header.y + style.Combo.content_padding.y);
                image.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                image.w = (float)(image.h);
                win.Buffer.nk_draw_symbol((symbol), (NkRect)(image), (NkColor)(text.background),
                    (NkColor)(symbol_color), (float)(1.0f), style.Font);
                text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(image.x + image.w + style.Combo.spacing.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)((button.x - style.Combo.content_padding.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                win.Buffer.nk_widget_text((NkRect)(label), selected, (int)(len), &text, (Align.MiddleLeft), style.Font);
            }

            return (nk_combo_begin(ctx, win, (NkVec2)(size), (is_clicked), (NkRect)(header)));
        }

        public static bool nk_combo_begin_image(NkContext ctx, NkImage img, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates s;
            NkStyleItem background;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            s = nk_widget(&header, ctx);
            if ((s) == (WidgetLayoutStates.Invalid)) return false;
            _in_ = (((win.Layout.Flags & PanelFlags.Rom) != 0) || ((s) == (WidgetLayoutStates.ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, ButtonBehavior.Default)))
                is_clicked = true;
            if ((ctx.LastWidgetState & WidgetStates.Actived) != 0) background = style.Combo.active;
            else if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) background = style.Combo.hover;
            else background = style.Combo.normal;
            if ((background.Type) == (StyleItemKind.Image))
            {
                win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                win.Buffer.nk_fill_rect((NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                win.Buffer.nk_stroke_rect((NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect bounds = new NkRect();
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                Symbols sym;
                if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) sym = (style.Combo.sym_hover);
                else if ((is_clicked)) sym = (style.Combo.sym_active);
                else sym = (style.Combo.sym_normal);
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
                win.Buffer.nk_draw_image((NkRect)(bounds), img, (NkColor)(nk_white));
                win.Buffer.nk_draw_button_symbol(&bounds, &content, (ctx.LastWidgetState), ctx.Style.Combo.button,
                    (sym), style.Font);
            }

            return (nk_combo_begin(ctx, win, (NkVec2)(size), (is_clicked), (NkRect)(header)));
        }

        public static bool nk_combo_begin_image_text(NkContext ctx, char* selected, int len, NkImage img, NkVec2 size)
        {
            NkWindow win;
            NkStyle style;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates s;
            NkStyleItem background;
            nk_text text = new nk_text();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            style = ctx.Style;
            s = nk_widget(&header, ctx);
            if (s == 0) return false;
            _in_ = (((win.Layout.Flags & PanelFlags.Rom) != 0) || ((s) == (WidgetLayoutStates.ROM))) ? null : ctx.Input;
            if ((nk_button_behavior(ref ctx.LastWidgetState, (NkRect)(header), _in_, ButtonBehavior.Default)))
                is_clicked = true;
            if ((ctx.LastWidgetState & WidgetStates.Actived) != 0)
            {
                background = style.Combo.active;
                text.text = (NkColor)(style.Combo.label_active);
            }
            else if ((ctx.LastWidgetState & WidgetStates.Hover) != 0)
            {
                background = style.Combo.hover;
                text.text = (NkColor)(style.Combo.label_hover);
            }
            else
            {
                background = style.Combo.normal;
                text.text = (NkColor)(style.Combo.label_normal);
            }

            if ((background.Type) == (StyleItemKind.Image))
            {
                text.background = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                win.Buffer.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                win.Buffer.nk_fill_rect((NkRect)(header), (float)(style.Combo.rounding), (NkColor)(background.Data.Color));
                win.Buffer.nk_stroke_rect((NkRect)(header), (float)(style.Combo.rounding), (float)(style.Combo.border),
                    (NkColor)(style.Combo.border_color));
            }

            {
                NkRect content = new NkRect();
                NkRect button = new NkRect();
                NkRect label = new NkRect();
                NkRect image = new NkRect();
                Symbols sym;
                if ((ctx.LastWidgetState & WidgetStates.Hover) != 0) sym = (style.Combo.sym_hover);
                else if ((is_clicked)) sym = (style.Combo.sym_active);
                else sym = (style.Combo.sym_normal);
                button.w = (float)(header.h - 2 * style.Combo.button_padding.y);
                button.x = (float)((header.x + header.w - header.h) - style.Combo.button_padding.x);
                button.y = (float)(header.y + style.Combo.button_padding.y);
                button.h = (float)(button.w);
                content.x = (float)(button.x + style.Combo.button.padding.x);
                content.y = (float)(button.y + style.Combo.button.padding.y);
                content.w = (float)(button.w - 2 * style.Combo.button.padding.x);
                content.h = (float)(button.h - 2 * style.Combo.button.padding.y);
                win.Buffer.nk_draw_button_symbol(&button, &content, (ctx.LastWidgetState), ctx.Style.Combo.button,
                    (sym), style.Font);
                image.x = (float)(header.x + style.Combo.content_padding.x);
                image.y = (float)(header.y + style.Combo.content_padding.y);
                image.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                image.w = (float)(image.h);
                win.Buffer.nk_draw_image((NkRect)(image), img, (NkColor)(nk_white));
                text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                label.x = (float)(image.x + image.w + style.Combo.spacing.x + style.Combo.content_padding.x);
                label.y = (float)(header.y + style.Combo.content_padding.y);
                label.w = (float)((button.x - style.Combo.content_padding.x) - label.x);
                label.h = (float)(header.h - 2 * style.Combo.content_padding.y);
                win.Buffer.nk_widget_text((NkRect)(label), selected, (int)(len), &text, Align.MiddleLeft, style.Font);
            }

            return (nk_combo_begin(ctx, win, (NkVec2)(size), (is_clicked), (NkRect)(header)));
        }

        public static bool nk_combo_begin_symbol_label(NkContext ctx, char* selected, Symbols type, NkVec2 size)
        {
            return (nk_combo_begin_symbol_text(ctx, selected, (int)(nk_strlen(selected)), (type), (NkVec2)(size)));
        }

        public static bool nk_combo_begin_image_label(NkContext ctx, char* selected, NkImage img, NkVec2 size)
        {
            return
                (nk_combo_begin_image_text(ctx, selected, (int)(nk_strlen(selected)), (NkImage)(img), (NkVec2)(size)));
        }

        public static bool nk_combo_item_text(NkContext ctx, char* text, int len, Align align)
        {
            return (nk_contextual_item_text(ctx, text, (int)(len), (align)));
        }

        public static bool nk_combo_item_label(NkContext ctx, char* label, Align align)
        {
            return (nk_contextual_item_label(ctx, label, (align)));
        }

        public static bool nk_combo_item_image_text(NkContext ctx, NkImage img, char* text, int len, Align alignment)
        {
            return (nk_contextual_item_image_text(ctx, (NkImage)(img), text, (int)(len), (alignment)));
        }

        public static bool nk_combo_item_image_label(NkContext ctx, NkImage img, char* text, Align alignment)
        {
            return (nk_contextual_item_image_label(ctx, (NkImage)(img), text, (alignment)));
        }

        public static bool nk_combo_item_symbol_text(NkContext ctx, Symbols sym, char* text, int len, Align alignment)
        {
            return (nk_contextual_item_symbol_text(ctx, (sym), text, (int)(len), (alignment)));
        }

        public static bool nk_combo_item_symbol_label(NkContext ctx, Symbols sym, char* label, Align alignment)
        {
            return (nk_contextual_item_symbol_label(ctx, (sym), label, (alignment)));
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
            window_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            if ((nk_combo_begin_label(ctx, items[selected], (NkVec2)(size))))
            {
                Layout.nk_layout_row_dynamic(ctx, (float)(item_height), (int)(1));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    if ((nk_combo_item_label(ctx, items[i], Align.MiddleLeft))) selected = (int)(i);
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
            window_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (ctx.Current.Layout.Type)));
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
            if ((nk_combo_begin_text(ctx, current_item, (int)(length), (NkVec2)(size))))
            {
                current_item = items_separated_by_separator;
                Layout.nk_layout_row_dynamic(ctx, (float)(item_height), (int)(1));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    iter = current_item;
                    while (((*iter) != 0) && (*iter != separator))
                    {
                        iter++;
                    }
                    length = ((int)(iter - current_item));
                    if ((nk_combo_item_text(ctx, current_item, (int)(length), Align.MiddleLeft))) selected = (int)(i);
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
            window_padding = (NkVec2)(nk_panel_get_padding(ctx.Style, (ctx.Current.Layout.Type)));
            max_height = (int)(count * item_height + count * (int)(item_spacing.y));
            max_height += (int)((int)(item_spacing.y) * 2 + (int)(window_padding.y) * 2);
            size.y = (float)((size.y) < ((float)(max_height)) ? (size.y) : ((float)(max_height)));
            item_getter(userdata, (int)(selected), &item);
            if ((nk_combo_begin_label(ctx, item, (NkVec2)(size))))
            {
                Layout.nk_layout_row_dynamic(ctx, (float)(item_height), (int)(1));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    item_getter(userdata, (int)(i), &item);
                    if ((nk_combo_item_label(ctx, item, Align.MiddleLeft))) selected = (int)(i);
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

        public static bool nk_menu_begin(NkContext ctx, NkWindow win, char* id, bool is_clicked, NkRect header, NkVec2 size)
        {
            bool is_open = false;
            bool is_active = false;
            NkRect body = new NkRect();
            NkWindow popup;
            uint hash = (uint)(nk_murmur_hash(id, (int)(nk_strlen(id)), (uint)(PanelKind.Menu)));
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            body.x = (float)(header.x);
            body.w = (float)(size.x);
            body.y = (float)(header.y + header.h);
            body.h = (float)(size.y);
            popup = win.Popup.win;
            is_open = (popup != null ? true : false);
            is_active =
                ((((popup) != null) && ((win.Popup.name) == (hash))) && ((win.Popup.type) == (PanelKind.Menu)));
            if ((((((is_clicked)) && ((is_open))) && (is_active == false)) || (((is_open)) && (is_active == false))) ||
                (((is_open == false) && (is_active == false)) && (is_clicked == false))) return false;
            if (
                nk_nonblock_begin(ctx, (PanelFlags.NoScrollbar), (NkRect)(body), (NkRect)(header), (PanelKind.Menu)) == false)
                return false;
            win.Popup.type = (PanelKind.Menu);
            win.Popup.name = (uint)(hash);
            return true;
        }

        public static bool nk_menu_begin_text(NkContext ctx, char* title, int len, Align align, NkVec2 size)
        {
            NkWindow win;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            state = (nk_widget(&header, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), title, (int)(len), (align),
                    ButtonBehavior.Default, ctx.Style.MenuButton, _in_, ctx.Style.Font))) is_clicked = true;
            return (nk_menu_begin(ctx, win, title, (is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_label(NkContext ctx, char* text, Align align, NkVec2 size)
        {
            return (nk_menu_begin_text(ctx, text, (int)(nk_strlen(text)), (align), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_image(NkContext ctx, char* id, NkImage img, NkVec2 size)
        {
            NkWindow win;
            NkRect header = new NkRect();
            nk_input _in_;
            bool is_clicked = false;
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            state = (nk_widget(&header, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (NkImage)(img),
                    ButtonBehavior.Default, ctx.Style.MenuButton, _in_))) is_clicked = true;
            return (nk_menu_begin(ctx, win, id, (is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_symbol(NkContext ctx, char* id, Symbols sym, NkVec2 size)
        {
            NkWindow win;
            nk_input _in_;
            NkRect header = new NkRect();
            bool is_clicked = false;
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            state = (nk_widget(&header, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (sym),
                    ButtonBehavior.Default, ctx.Style.MenuButton, _in_, ctx.Style.Font))) is_clicked = true;
            return (nk_menu_begin(ctx, win, id, (is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_image_text(NkContext ctx, char* title, int len, Align align, NkImage img,
            NkVec2 size)
        {
            NkWindow win;
            NkRect header = new NkRect();
            nk_input _in_;
            bool is_clicked = false;
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            state = (nk_widget(&header, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_image(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (NkImage)(img), title,
                    (int)(len), (align), ButtonBehavior.Default, ctx.Style.MenuButton, ctx.Style.Font, _in_)))
                is_clicked = true;
            return (nk_menu_begin(ctx, win, title, (is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_image_label(NkContext ctx, char* title, Align align, NkImage img, NkVec2 size)
        {
            return (nk_menu_begin_image_text(ctx, title, (int)(nk_strlen(title)), (align), (NkImage)(img), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_symbol_text(NkContext ctx, char* title, int len, Align align, int sym, NkVec2 size)
        {
            NkWindow win;
            NkRect header = new NkRect();
            nk_input _in_;
            bool is_clicked = false;
            WidgetLayoutStates state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            state = (nk_widget(&header, ctx));
            if (state == 0) return false;
            _in_ = (((state) == (WidgetLayoutStates.ROM)) || ((win.Layout.Flags & PanelFlags.Rom) != 0)) ? null : ctx.Input;
            if (
                (nk_do_button_text_symbol(ref ctx.LastWidgetState, win.Buffer, (NkRect)(header), (Symbols)(sym), title, (int)(len),
                    (align), ButtonBehavior.Default, ctx.Style.MenuButton, ctx.Style.Font, _in_)))
                is_clicked = true;
            return (nk_menu_begin(ctx, win, title, (is_clicked), (NkRect)(header), (NkVec2)(size)));
        }

        public static bool nk_menu_begin_symbol_label(NkContext ctx, char* title, Align align, int sym, NkVec2 size)
        {
            return

                    (nk_menu_begin_symbol_text(ctx, title, (int)(nk_strlen(title)), (align), (int)(sym), (NkVec2)(size)));
        }

        public static bool nk_menu_item_text(NkContext ctx, char* title, int len, Align align)
        {
            return (nk_contextual_item_text(ctx, title, (int)(len), (align)));
        }

        public static bool nk_menu_item_label(NkContext ctx, char* label, Align align)
        {
            return (nk_contextual_item_label(ctx, label, (align)));
        }

        public static bool nk_menu_item_image_label(NkContext ctx, NkImage img, char* label, Align align)
        {
            return (nk_contextual_item_image_label(ctx, (NkImage)(img), label, (align)));
        }

        public static bool nk_menu_item_image_text(NkContext ctx, NkImage img, char* text, int len, Align align)
        {
            return (nk_contextual_item_image_text(ctx, (NkImage)(img), text, (int)(len), (align)));
        }

        public static bool nk_menu_item_symbol_text(NkContext ctx, Symbols sym, char* text, int len, Align align)
        {
            return (nk_contextual_item_symbol_text(ctx, (sym), text, (int)(len), (align)));
        }

        public static bool nk_menu_item_symbol_label(NkContext ctx, Symbols sym, char* label, Align align)
        {
            return (nk_contextual_item_symbol_label(ctx, (sym), label, (align)));
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