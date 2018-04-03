namespace NuklearSharp
{
    public unsafe class NkCommandBuffer
    {
        public NkCommandBase First;
        public NkCommandBase Last;
        public int Count;

        public NkRect Clip;
        public bool UseClipping;
        public NkHandle Userdata = new NkHandle();

        public void nk_push_scissor(NkRect r)
        {
            NkCommandScissor cmd;
            if (this == null) return;
            Clip.x = r.x;
            Clip.y = r.y;
            Clip.w = r.w;
            Clip.h = r.h;
            cmd = (NkCommandScissor)Nk.nk_command_buffer_push(this, NkCommandType.SCISSOR);
            if (cmd == null) return;
            cmd.X = (short)r.x;
            cmd.Y = (short)r.y;
            cmd.W = (ushort)(0 < r.w ? r.w : 0);
            cmd.H = (ushort)(0 < r.h ? r.h : 0);
        }

        public void nk_stroke_line(float x0, float y0, float x1, float y1,
            float line_thickness, NkColor c)
        {
            NkCommandLine cmd;
            if (this == null || line_thickness <= 0) return;
            cmd = (NkCommandLine)Nk.nk_command_buffer_push(this, NkCommandType.LINE);
            if (cmd == null) return;
            cmd.LineThickness = (ushort)line_thickness;
            cmd.Begin.x = (short)x0;
            cmd.Begin.y = (short)y0;
            cmd.End.x = (short)x1;
            cmd.End.y = (short)y1;
            cmd.Color = c;
        }

        public void nk_stroke_curve(float ax, float ay, float ctrl0x, float ctrl0y,
            float ctrl1x, float ctrl1y, float bx, float by, float line_thickness, NkColor col)
        {
            NkCommandCurve cmd;
            if (this == null || col.a == 0 || line_thickness <= 0) return;
            cmd = (NkCommandCurve)Nk.nk_command_buffer_push(this, NkCommandType.CURVE);
            if (cmd == null) return;
            cmd.LineThickness = (ushort)line_thickness;
            cmd.Begin.x = (short)ax;
            cmd.Begin.y = (short)ay;
            cmd.Ctrl0.x = (short)ctrl0x;
            cmd.Ctrl0.y = (short)ctrl0y;
            cmd.Ctrl1.x = (short)ctrl1x;
            cmd.Ctrl1.y = (short)ctrl1y;
            cmd.End.x = (short)bx;
            cmd.End.y = (short)@by;
            cmd.Color = col;
        }

        public void nk_stroke_rect(NkRect rect, float rounding, float line_thickness,
            NkColor c)
        {
            NkCommandRect cmd;
            if (this == null || c.a == 0 || rect.w == 0 || rect.h == 0 || line_thickness <= 0)
                return;
            if (UseClipping)
            {
                if (
                    Clip.x > rect.x + rect.w || Clip.x + Clip.w < rect.x ||
                    Clip.y > rect.y + rect.h || Clip.y + Clip.h < rect.y) return;
            }

            cmd = (NkCommandRect)Nk.nk_command_buffer_push(this, NkCommandType.RECT);
            if (cmd == null) return;
            cmd.Rounding = (ushort)rounding;
            cmd.LineThickness = (ushort)line_thickness;
            cmd.X = (short)rect.x;
            cmd.Y = (short)rect.y;
            cmd.W = (ushort)(0 < rect.w ? rect.w : 0);
            cmd.H = (ushort)(0 < rect.h ? rect.h : 0);
            cmd.Color = c;
        }

        public void nk_fill_rect(NkRect rect, float rounding, NkColor c)
        {
            NkCommandRectFilled cmd;
            if (this == null || c.a == 0 || rect.w == 0 || rect.h == 0) return;
            if (UseClipping)
            {
                if (
                    !!(Clip.x > rect.x + rect.w || Clip.x + Clip.w < rect.x ||
                       Clip.y > rect.y + rect.h || Clip.y + Clip.h < rect.y)) return;
            }

            cmd = (NkCommandRectFilled)Nk.nk_command_buffer_push(this, NkCommandType.RECT_FILLED);
            if (cmd == null) return;
            cmd.Rounding = (ushort)rounding;
            cmd.X = (short)rect.x;
            cmd.Y = (short)rect.y;
            cmd.W = (ushort)(0 < rect.w ? rect.w : 0);
            cmd.H = (ushort)(0 < rect.h ? rect.h : 0);
            cmd.Color = c;
        }

        public void nk_fill_rect_multi_color(NkRect rect, NkColor left, NkColor top,
            NkColor right, NkColor bottom)
        {
            NkCommandRectMultiColor cmd;
            if (this == null || rect.w == 0 || rect.h == 0) return;
            if (UseClipping)
            {
                if (
                    !!(Clip.x > rect.x + rect.w || Clip.x + Clip.w < rect.x ||
                       Clip.y > rect.y + rect.h || Clip.y + Clip.h < rect.y)) return;
            }

            cmd = (NkCommandRectMultiColor)Nk.nk_command_buffer_push(this, NkCommandType.RECT_MULTI_COLOR);
            if (cmd == null) return;
            cmd.X = (short)rect.x;
            cmd.Y = (short)rect.y;
            cmd.W = (ushort)(0 < rect.w ? rect.w : 0);
            cmd.H = (ushort)(0 < rect.h ? rect.h : 0);
            cmd.Left = left;
            cmd.Top = top;
            cmd.Right = right;
            cmd.Bottom = bottom;
        }

        public void nk_stroke_circle(NkRect r, float line_thickness, NkColor c)
        {
            NkCommandCircle cmd;
            if (this == null || r.w == 0 || r.h == 0 || line_thickness <= 0) return;
            if (UseClipping)
            {
                if (
                    !!(Clip.x > r.x + r.w || Clip.x + Clip.w < r.x || Clip.y > r.y + r.h ||
                       Clip.y + Clip.h < r.y)) return;
            }

            cmd = (NkCommandCircle)Nk.nk_command_buffer_push(this, NkCommandType.CIRCLE);
            if (cmd == null) return;
            cmd.LineThickness = (ushort)line_thickness;
            cmd.X = (short)r.x;
            cmd.Y = (short)r.y;
            cmd.W = (ushort)(r.w < 0 ? 0 : r.w);
            cmd.H = (ushort)(r.h < 0 ? 0 : r.h);
            cmd.Color = c;
        }

        public void nk_fill_circle(NkRect r, NkColor c)
        {
            NkCommandCircleFilled cmd;
            if (this == null || c.a == 0 || r.w == 0 || r.h == 0) return;
            if (UseClipping)
            {
                if (
                    !!(Clip.x > r.x + r.w || Clip.x + Clip.w < r.x || Clip.y > r.y + r.h ||
                       Clip.y + Clip.h < r.y)) return;
            }

            cmd = (NkCommandCircleFilled)Nk.nk_command_buffer_push(this, NkCommandType.CIRCLE_FILLED);
            if (cmd == null) return;
            cmd.X = (short)r.x;
            cmd.Y = (short)r.y;
            cmd.W = (ushort)(r.w < 0 ? 0 : r.w);
            cmd.H = (ushort)(r.h < 0 ? 0 : r.h);
            cmd.Color = c;
        }

        public void nk_stroke_arc(float cx, float cy, float radius, float a_min, float a_max,
            float line_thickness, NkColor c)
        {
            NkCommandArc cmd;
            if (this == null || c.a == 0 || line_thickness <= 0) return;
            cmd = (NkCommandArc)Nk.nk_command_buffer_push(this, NkCommandType.ARC);
            if (cmd == null) return;
            cmd.LineThickness = (ushort)line_thickness;
            cmd.Cx = (short)cx;
            cmd.Cy = (short)cy;
            cmd.R = (ushort)radius;
            cmd.A[0] = a_min;
            cmd.A[1] = a_max;
            cmd.Color = c;
        }

        public void nk_fill_arc(float cx, float cy, float radius, float a_min, float a_max,
            NkColor c)
        {
            NkCommandArcFilled cmd;
            if (this == null || c.a == 0) return;
            cmd = (NkCommandArcFilled)Nk.nk_command_buffer_push(this, NkCommandType.ARC_FILLED);
            if (cmd == null) return;
            cmd.Cx = (short)cx;
            cmd.Cy = (short)cy;
            cmd.R = (ushort)radius;
            cmd.A[0] = a_min;
            cmd.A[1] = a_max;
            cmd.Color = c;
        }

        public void nk_stroke_triangle(float x0, float y0, float x1, float y1, float x2,
            float y2, float line_thickness, NkColor c)
        {
            NkCommandTriangle cmd;
            if (this == null || c.a == 0 || line_thickness <= 0) return;
            if (UseClipping)
            {
                if (
                    !(Clip.x <= x0 && x0 < Clip.x + Clip.w && Clip.y <= y0 && y0 < Clip.y + Clip.h) &&
                    !(Clip.x <= x1 && x1 < Clip.x + Clip.w && Clip.y <= y1 && y1 < Clip.y + Clip.h) &&
                    !(Clip.x <= x2 && x2 < Clip.x + Clip.w && Clip.y <= y2 && y2 < Clip.y + Clip.h)) return;
            }

            cmd = (NkCommandTriangle)Nk.nk_command_buffer_push(this, NkCommandType.TRIANGLE);
            if (cmd == null) return;
            cmd.LineThickness = (ushort)line_thickness;
            cmd.A.x = (short)x0;
            cmd.A.y = (short)y0;
            cmd.B.x = (short)x1;
            cmd.B.y = (short)y1;
            cmd.C.x = (short)x2;
            cmd.C.y = (short)y2;
            cmd.Color = c;
        }

        public void nk_fill_triangle(float x0, float y0, float x1, float y1, float x2,
            float y2, NkColor c)
        {
            NkCommandTriangleFilled cmd;
            if (this == null || c.a == 0) return;
            if (this == null) return;
            if (UseClipping)
            {
                if (
                    !(Clip.x <= x0 && x0 < Clip.x + Clip.w && Clip.y <= y0 && y0 < Clip.y + Clip.h) &&
                    !(Clip.x <= x1 && x1 < Clip.x + Clip.w && Clip.y <= y1 && y1 < Clip.y + Clip.h) &&
                    !(Clip.x <= x2 && x2 < Clip.x + Clip.w && Clip.y <= y2 && y2 < Clip.y + Clip.h)) return;
            }

            cmd = (NkCommandTriangleFilled)Nk.nk_command_buffer_push(this, NkCommandType.TRIANGLE_FILLED);
            if (cmd == null) return;
            cmd.A.x = (short)x0;
            cmd.A.y = (short)y0;
            cmd.B.x = (short)x1;
            cmd.B.y = (short)y1;
            cmd.C.x = (short)x2;
            cmd.C.y = (short)y2;
            cmd.Color = c;
        }

        public void nk_draw_image(NkRect r, NkImage img, NkColor col)
        {
            NkCommandImage cmd;
            if (this == null) return;
            if (UseClipping)
            {
                if (Clip.w == 0 || Clip.h == 0 ||
                    !!(Clip.x > r.x + r.w || Clip.x + Clip.w < r.x || Clip.y > r.y + r.h ||
                       Clip.y + Clip.h < r.y)) return;
            }

            cmd = (NkCommandImage)Nk.nk_command_buffer_push(this, NkCommandType.IMAGE);
            if (cmd == null) return;
            cmd.X = (short)r.x;
            cmd.Y = (short)r.y;
            cmd.W = (ushort)(0 < r.w ? r.w : 0);
            cmd.H = (ushort)(0 < r.h ? r.h : 0);
            cmd.Img = img;
            cmd.Col = col;
        }

        public void nk_push_custom(NkRect r, NkCommandCustomCallback cb, NkHandle usr)
        {
            NkCommandCustom cmd;
            if (this == null) return;
            if (UseClipping)
            {
                if (Clip.w == 0 || Clip.h == 0 ||
                    !!(Clip.x > r.x + r.w || Clip.x + Clip.w < r.x || Clip.y > r.y + r.h ||
                       Clip.y + Clip.h < r.y)) return;
            }

            cmd = (NkCommandCustom)Nk.nk_command_buffer_push(this, NkCommandType.CUSTOM);
            if (cmd == null) return;
            cmd.X = (short)r.x;
            cmd.Y = (short)r.y;
            cmd.W = (ushort)(0 < r.w ? r.w : 0);
            cmd.H = (ushort)(0 < r.h ? r.h : 0);
            cmd.CallbackData = usr;
            cmd.Callback = cb;
        }

        public void nk_draw_text(NkRect r, char* _string_, int length, NkUserFont font,
            NkColor bg, NkColor fg)
        {
            float text_width = 0;
            NkCommandText cmd;
            if (this == null || _string_ == null || length == 0 || bg.a == 0 && fg.a == 0) return;
            if (UseClipping)
            {
                if (Clip.w == 0 || Clip.h == 0 ||
                    !!(Clip.x > r.x + r.w || Clip.x + Clip.w < r.x || Clip.y > r.y + r.h ||
                       Clip.y + Clip.h < r.y)) return;
            }

            text_width =
                font.Width((NkHandle)font.Userdata, (float)font.Height, _string_, (int)length);
            if (text_width > r.w)
            {
                int glyphs = 0;
                float txt_width = text_width;
                length =

                    Nk.nk_text_clamp(font, _string_, (int)length, (float)r.w, &glyphs, &txt_width, null,
                        (int)0);
            }

            if (length == 0) return;
            cmd = (NkCommandText)Nk.nk_command_buffer_push(this, NkCommandType.TEXT);
            if (cmd == null) return;
            cmd.X = (short)r.x;
            cmd.Y = (short)r.y;
            cmd.W = (ushort)r.w;
            cmd.H = (ushort)r.h;
            cmd.Background = bg;
            cmd.Foreground = fg;
            cmd.Font = font;
            cmd.Length = length;
            cmd.Height = font.Height;
            cmd.String = new PinnedArray<char>(length);
            CRuntime.Memcpy((void*)cmd.String, _string_, length * sizeof(char));
            cmd.String[length] = '\0';
        }

        public void nk_widget_text(NkRect b, char* _string_, int len, nk_text* t, Alignment a,
            NkUserFont f)
        {
            NkRect label = new NkRect();
            float text_width;
            if (this == null || t == null) return;
            b.h = b.h < 2 * t->padding.y ? 2 * t->padding.y : b.h;
            label.x = 0;
            label.w = 0;
            label.y = b.y + t->padding.y;
            label.h = f.Height < b.h - 2 * t->padding.y ? f.Height : b.h - 2 * t->padding.y;
            text_width = f.Width((NkHandle)f.Userdata, (float)f.Height, _string_, (int)len);
            text_width += 2.0f * t->padding.x;
            if ((a & Alignment.LEFT) != 0)
            {
                label.x = b.x + t->padding.x;
                label.w = 0 < b.w - 2 * t->padding.x ? b.w - 2 * t->padding.x : 0;
            }
            else if ((a & Alignment.CENTERED) != 0)
            {
                label.w = 1 < 2 * t->padding.x + text_width ? 2 * t->padding.x + text_width : 1;
                label.x = b.x + t->padding.x + (b.w - 2 * t->padding.x - label.w) / 2;
                label.x = b.x + t->padding.x < label.x ? label.x : b.x + t->padding.x;
                label.w = b.x + b.w < label.x + label.w ? b.x + b.w : label.x + label.w;
                if (label.w >= label.x) label.w -= label.x;
            }
            else if ((a & Alignment.RIGHT) != 0)
            {
                label.x =

                    b.x + t->padding.x < b.x + b.w - (2 * t->padding.x + text_width)
                        ? b.x + b.w - (2 * t->padding.x + text_width)
                        : b.x + t->padding.x;
                label.w = text_width + 2 * t->padding.x;
            }
            else return;
            if ((a & Alignment.MIDDLE) != 0)
            {
                label.y = b.y + b.h / 2.0f - f.Height / 2.0f;
                label.h =

                    b.h / 2.0f < b.h - (b.h / 2.0f + f.Height / 2.0f)
                        ? b.h - (b.h / 2.0f + f.Height / 2.0f)
                        : b.h / 2.0f;
            }
            else if ((a & Alignment.BOTTOM) != 0)
            {
                label.y = b.y + b.h - f.Height;
                label.h = f.Height;
            }

            nk_draw_text(label, _string_, len, f, t->background,
                t->text);
        }

        public void nk_widget_text_wrap(NkRect b, char* _string_, int len, nk_text* t,
            NkUserFont f)
        {
            float width;
            int glyphs = 0;
            int fitting = 0;
            int done = 0;
            NkRect line = new NkRect();
            nk_text text = new nk_text();
            uint* seperator = stackalloc uint[1];
            seperator[0] = ' ';

            if (this == null || t == null) return;
            text.padding = Nk.nk_vec2_((float)0, (float)0);
            text.background = (t->background);
            text.text = (t->text);
            b.w = b.w < 2 * t->padding.x ? 2 * t->padding.x : b.w;
            b.h = b.h < 2 * t->padding.y ? 2 * t->padding.y : b.h;
            b.h = b.h - 2 * t->padding.y;
            line.x = b.x + t->padding.x;
            line.y = b.y + t->padding.y;
            line.w = b.w - 2 * t->padding.x;
            line.h = 2 * t->padding.y + f.Height;
            fitting = Nk.nk_text_clamp(f, _string_, (int)len, (float)line.w, &glyphs, &width, seperator, 1);
            while (done < len)
            {
                if (fitting == 0 || line.y + line.h >= b.y + b.h) break;
                nk_widget_text(line, &_string_[done], fitting, &text, Alignment.MIDDLELEFT, f);
                done += fitting;
                line.y += f.Height + 2 * t->padding.y;
                fitting =

                    Nk.nk_text_clamp(f, &_string_[done], (int)(len - done), (float)line.w, &glyphs, &width,
                        seperator, 1);
            }
        }

        public void nk_draw_symbol(NkSymbolType type, NkRect content, NkColor background,
            NkColor foreground, float border_width, NkUserFont font)
        {
            switch (type)
            {
                case NkSymbolType.X:
                case NkSymbolType.UNDERSCORE:
                case NkSymbolType.PLUS:
                case NkSymbolType.MINUS:
                    {
                        char X = type == NkSymbolType.X
                            ? 'x'
                            : type == NkSymbolType.UNDERSCORE ? '_' : type == NkSymbolType.PLUS ? '+' : '-';
                        nk_text text = new nk_text();
                        text.padding = Nk.nk_vec2_((float)0, (float)0);
                        text.background = background;
                        text.text = foreground;
                        nk_widget_text(content, &X, 1, &text, Alignment.MIDDLECENTERED, font);
                    }
                    break;
                case NkSymbolType.CIRCLE_SOLID:
                case NkSymbolType.CIRCLE_OUTLINE:
                case NkSymbolType.RECT_SOLID:
                case NkSymbolType.RECT_OUTLINE:
                    {
                        if (type == NkSymbolType.RECT_SOLID || type == NkSymbolType.RECT_OUTLINE)
                        {
                            nk_fill_rect(content, 0, foreground);
                            if (type == NkSymbolType.RECT_OUTLINE)
                                nk_fill_rect(Nk.nk_shrink_rect_((NkRect)content, (float)border_width),
                                    0, background);
                        }
                        else
                        {
                            nk_fill_circle(content, foreground);
                            if (type == NkSymbolType.CIRCLE_OUTLINE)
                                nk_fill_circle(Nk.nk_shrink_rect_((NkRect)content, (float)1),
                                    background);
                        }
                    }
                    break;
                case NkSymbolType.TRIANGLE_UP:
                case NkSymbolType.TRIANGLE_DOWN:
                case NkSymbolType.TRIANGLE_LEFT:
                case NkSymbolType.TRIANGLE_RIGHT:
                    {
                        NkHeading heading;
                        NkVec2* points = stackalloc NkVec2[3];
                        heading =

                            type == NkSymbolType.TRIANGLE_RIGHT
                                ? NkHeading.NK_RIGHT
                                : type == NkSymbolType.TRIANGLE_LEFT
                                    ? NkHeading.NK_LEFT
                                    : type == NkSymbolType.TRIANGLE_UP ? NkHeading.NK_UP : NkHeading.NK_DOWN;
                        Nk.nk_triangle_from_direction(points, content, 0, 0, heading);
                        nk_fill_triangle(points[0].x, points[0].y, points[1].x,
                            points[1].y, points[2].x, points[2].y, foreground);
                    }
                    break;
                default:
                case NkSymbolType.NONE:
                case NkSymbolType.MAX:
                    break;
            }

        }

        public NkStyleItem nk_draw_button(NkRect* bounds, NkWidgetStates state,
            nk_style_button style)
        {
            NkStyleItem background;
            if ((state & NkWidgetStates.HOVER) != 0) background = style.hover;
            else if ((state & NkWidgetStates.ACTIVED) != 0) background = style.active;
            else background = style.normal;
            if (background.Type == NkStyleItemType.IMAGE)
            {
                nk_draw_image(*bounds, background.Data.Image, Nk.nk_white);
            }
            else
            {
                nk_fill_rect(*bounds, style.rounding, background.Data.Color);
                nk_stroke_rect(*bounds, style.rounding, style.border,
                    style.border_color);
            }

            return background;
        }

        public void nk_draw_button_text(NkRect* bounds, NkRect* content, NkWidgetStates state,
            nk_style_button style, char* txt, int len, Alignment text_alignment, NkUserFont font)
        {
            nk_text text = new nk_text();
            NkStyleItem background;
            background = nk_draw_button(bounds, state, style);
            if (background.Type == NkStyleItemType.COLOR) text.background = background.Data.Color;
            else text.background = style.text_background;
            if ((state & NkWidgetStates.HOVER) != 0) text.text = style.text_hover;
            else if ((state & NkWidgetStates.ACTIVED) != 0) text.text = style.text_active;
            else text.text = style.text_normal;
            text.padding = Nk.nk_vec2_((float)0, (float)0);
            nk_widget_text(*content, txt, len, &text, text_alignment, font);
        }

        public void nk_draw_button_symbol(NkRect* bounds, NkRect* content, NkWidgetStates state,
            nk_style_button style, NkSymbolType type, NkUserFont font)
        {
            NkColor sym = new NkColor();
            NkColor bg = new NkColor();
            NkStyleItem background;
            background = nk_draw_button(bounds, state, style);
            if (background.Type == NkStyleItemType.COLOR) bg = background.Data.Color;
            else bg = style.text_background;
            if ((state & NkWidgetStates.HOVER) != 0) sym = style.text_hover;
            else if ((state & NkWidgetStates.ACTIVED) != 0) sym = style.text_active;
            else sym = style.text_normal;
            nk_draw_symbol(type, *content, bg, sym, 1,
                font);
        }

        public void nk_draw_button_image(NkRect* bounds, NkRect* content, NkWidgetStates state,
            nk_style_button style, NkImage img)
        {
            nk_draw_button(bounds, state, style);
            nk_draw_image(*content, img, Nk.nk_white);
        }

        public void nk_draw_button_text_symbol(NkRect* bounds, NkRect* label,
            NkRect* symbol, NkWidgetStates state, nk_style_button style, char* str, int len, NkSymbolType type, NkUserFont font)
        {
            NkColor sym = new NkColor();
            nk_text text = new nk_text();
            NkStyleItem background;
            background = nk_draw_button(bounds, state, style);
            if (background.Type == NkStyleItemType.COLOR) text.background = background.Data.Color;
            else text.background = style.text_background;
            if ((state & NkWidgetStates.HOVER) != 0)
            {
                sym = style.text_hover;
                text.text = style.text_hover;
            }
            else if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                sym = style.text_active;
                text.text = style.text_active;
            }
            else
            {
                sym = style.text_normal;
                text.text = style.text_normal;
            }

            text.padding = Nk.nk_vec2_((float)0, (float)0);
            nk_draw_symbol(type, *symbol, style.text_background,
                sym, 0, font);
            nk_widget_text(*label, str, len, &text, Alignment.MIDDLECENTERED, font);
        }

        public void nk_draw_button_text_image(NkRect* bounds, NkRect* label,
            NkRect* image, NkWidgetStates state, nk_style_button style, char* str, int len, NkUserFont font, NkImage img)
        {
            nk_text text = new nk_text();
            NkStyleItem background;
            background = nk_draw_button(bounds, state, style);
            if (background.Type == NkStyleItemType.COLOR) text.background = background.Data.Color;
            else text.background = style.text_background;
            if ((state & NkWidgetStates.HOVER) != 0) text.text = style.text_hover;
            else if ((state & NkWidgetStates.ACTIVED) != 0) text.text = style.text_active;
            else text.text = style.text_normal;
            text.padding = Nk.nk_vec2_((float)0, (float)0);
            nk_widget_text(*label, str, len, &text, Alignment.MIDDLECENTERED, font);
            nk_draw_image(*image, img, Nk.nk_white);
        }

        public void nk_draw_checkbox(NkWidgetStates state, nk_style_toggle style, bool active,
            NkRect* label, NkRect* selector, NkRect* cursors, char* _string_, int len, NkUserFont font)
        {
            NkStyleItem background;
            NkStyleItem cursor;
            nk_text text = new nk_text();
            if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = style.text_hover;
            }
            else if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = style.text_active;
            }
            else
            {
                background = style.normal;
                cursor = style.cursor_normal;
                text.text = style.text_normal;
            }

            if (background.Type == NkStyleItemType.COLOR)
            {
                nk_fill_rect(*selector, 0, style.border_color);
                nk_fill_rect(Nk.nk_shrink_rect_(*selector, (float)style.border),
                    0, background.Data.Color);
            }
            else nk_draw_image(*selector, background.Data.Image, Nk.nk_white);
            if (active)
            {
                if (cursor.Type == NkStyleItemType.IMAGE)
                    nk_draw_image(*cursors, cursor.Data.Image, Nk.nk_white);
                else nk_fill_rect(*cursors, 0, cursor.Data.Color);
            }

            text.padding.x = 0;
            text.padding.y = 0;
            text.background = style.text_background;
            nk_widget_text(*label, _string_, len, &text, Alignment.MIDDLELEFT, font);
        }

        public void nk_draw_option(NkWidgetStates state, nk_style_toggle style, bool active,
            NkRect* label, NkRect* selector, NkRect* cursors, char* _string_, int len, NkUserFont font)
        {
            NkStyleItem background;
            NkStyleItem cursor;
            nk_text text = new nk_text();
            if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = style.text_hover;
            }
            else if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = style.text_active;
            }
            else
            {
                background = style.normal;
                cursor = style.cursor_normal;
                text.text = style.text_normal;
            }

            if (background.Type == NkStyleItemType.COLOR)
            {
                nk_fill_circle(*selector, style.border_color);
                nk_fill_circle(Nk.nk_shrink_rect_(*selector, (float)style.border),
                    background.Data.Color);
            }
            else nk_draw_image(*selector, background.Data.Image, Nk.nk_white);
            if (active)
            {
                if (cursor.Type == NkStyleItemType.IMAGE)
                    nk_draw_image(*cursors, cursor.Data.Image, Nk.nk_white);
                else nk_fill_circle(*cursors, cursor.Data.Color);
            }

            text.padding.x = 0;
            text.padding.y = 0;
            text.background = style.text_background;
            nk_widget_text(*label, _string_, len, &text, Alignment.MIDDLELEFT, font);
        }

        public void nk_draw_selectable(NkWidgetStates state, nk_style_selectable style, int active,
            NkRect* bounds, NkRect* icon, NkImage img, char* _string_, int len, Alignment align, NkUserFont font)
        {
            NkStyleItem background;
            nk_text text = new nk_text();
            text.padding = style.padding;
            if (active == 0)
            {
                if ((state & NkWidgetStates.ACTIVED) != 0)
                {
                    background = style.pressed;
                    text.text = style.text_pressed;
                }
                else if ((state & NkWidgetStates.HOVER) != 0)
                {
                    background = style.hover;
                    text.text = style.text_hover;
                }
                else
                {
                    background = style.normal;
                    text.text = style.text_normal;
                }
            }
            else
            {
                if ((state & NkWidgetStates.ACTIVED) != 0)
                {
                    background = style.pressed_active;
                    text.text = style.text_pressed_active;
                }
                else if ((state & NkWidgetStates.HOVER) != 0)
                {
                    background = style.hover_active;
                    text.text = style.text_hover_active;
                }
                else
                {
                    background = style.normal_active;
                    text.text = style.text_normal_active;
                }
            }

            if (background.Type == NkStyleItemType.IMAGE)
            {
                nk_draw_image(*bounds, background.Data.Image, Nk.nk_white);
                text.background = Nk.nk_rgba((int)0, (int)0, (int)0, (int)0);
            }
            else
            {
                nk_fill_rect(*bounds, style.rounding, background.Data.Color);
                text.background = background.Data.Color;
            }

            if (img != null && icon != null)
                nk_draw_image(*icon, img, Nk.nk_white);
            nk_widget_text(*bounds, _string_, len, &text, align, font);
        }

        public void nk_draw_slider(NkWidgetStates state, nk_style_slider style, NkRect* bounds,
            NkRect* visual_cursor, float min, float value, float max)
        {
            NkRect fill = new NkRect();
            NkRect bar = new NkRect();
            NkStyleItem background;
            NkColor bar_color = new NkColor();
            NkStyleItem cursor;
            if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.active;
                bar_color = style.bar_active;
                cursor = style.cursor_active;
            }
            else if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                bar_color = style.bar_hover;
                cursor = style.cursor_hover;
            }
            else
            {
                background = style.normal;
                bar_color = style.bar_normal;
                cursor = style.cursor_normal;
            }

            bar.x = (bounds->x);
            bar.y = visual_cursor->y + visual_cursor->h / 2 - bounds->h / 12;
            bar.w = (bounds->w);
            bar.h = bounds->h / 6;
            fill.w = visual_cursor->x + visual_cursor->w / 2.0f - bar.x;
            fill.x = bar.x;
            fill.y = bar.y;
            fill.h = bar.h;
            if (background.Type == NkStyleItemType.IMAGE)
            {
                nk_draw_image(*bounds, background.Data.Image, Nk.nk_white);
            }
            else
            {
                nk_fill_rect(*bounds, style.rounding, background.Data.Color);
                nk_stroke_rect(*bounds, style.rounding, style.border,
                    style.border_color);
            }

            nk_fill_rect(bar, style.rounding, bar_color);
            nk_fill_rect(fill, style.rounding, style.bar_filled);
            if (cursor.Type == NkStyleItemType.IMAGE)
                nk_draw_image(*visual_cursor, cursor.Data.Image, Nk.nk_white);
            else nk_fill_circle(*visual_cursor, cursor.Data.Color);
        }

        public void nk_draw_progress(NkWidgetStates state, nk_style_progress style,
            NkRect* bounds, NkRect* scursor, ulong value, ulong max)
        {
            NkStyleItem background;
            NkStyleItem cursor;
            if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.active;
                cursor = style.cursor_active;
            }
            else if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
            }
            else
            {
                background = style.normal;
                cursor = style.cursor_normal;
            }

            if (background.Type == NkStyleItemType.COLOR)
            {
                nk_fill_rect(*bounds, style.rounding, background.Data.Color);
                nk_stroke_rect(*bounds, style.rounding, style.border,
                    style.border_color);
            }
            else nk_draw_image(*bounds, background.Data.Image, Nk.nk_white);
            if (cursor.Type == NkStyleItemType.COLOR)
            {
                nk_fill_rect(*scursor, style.rounding, cursor.Data.Color);
                nk_stroke_rect(*scursor, style.rounding, style.border,
                    style.border_color);
            }
            else nk_draw_image(*scursor, cursor.Data.Image, Nk.nk_white);
        }

        public void nk_draw_scrollbar(NkWidgetStates state, nk_style_scrollbar style,
            NkRect* bounds, NkRect* scroll)
        {
            NkStyleItem background;
            NkStyleItem cursor;
            if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.active;
                cursor = style.cursor_active;
            }
            else if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
            }
            else
            {
                background = style.normal;
                cursor = style.cursor_normal;
            }

            if (background.Type == NkStyleItemType.COLOR)
            {
                nk_fill_rect(*bounds, style.rounding, background.Data.Color);
                nk_stroke_rect(*bounds, style.rounding, style.border,
                    style.border_color);
            }
            else
            {
                nk_draw_image(*bounds, background.Data.Image, Nk.nk_white);
            }

            if (background.Type == NkStyleItemType.COLOR)
            {
                nk_fill_rect(*scroll, style.rounding_cursor, cursor.Data.Color);
                nk_stroke_rect(*scroll, style.rounding_cursor,
                    style.border_cursor, style.cursor_border_color);
            }
            else nk_draw_image(*scroll, cursor.Data.Image, Nk.nk_white);
        }

        public void nk_edit_draw_text(nk_style_edit style, float pos_x, float pos_y,
            float x_offset, char* text, int byte_len, float row_height, NkUserFont font, NkColor background,
            NkColor foreground, bool is_selected)
        {
            if (text == null || byte_len == 0 || this == null || style == null) return;
            {
                int glyph_len = 0;
                char unicode = (char)0;
                int text_len = 0;
                float line_width = 0;
                float glyph_width;
                char* line = text;
                float line_offset = 0;
                int line_count = 0;
                nk_text txt = new nk_text();
                txt.padding = Nk.nk_vec2_((float)0, (float)0);
                txt.background = background;
                txt.text = foreground;
                glyph_len = Nk.nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len));
                if (glyph_len == 0) return;
                while (text_len < byte_len && glyph_len != 0)
                {
                    if (unicode == '\n')
                    {
                        NkRect label = new NkRect();
                        label.y = pos_y + line_offset;
                        label.h = row_height;
                        label.w = line_width;
                        label.x = pos_x;
                        if (line_count == 0) label.x += x_offset;
                        if (is_selected)
                            nk_fill_rect(label, 0, background);
                        nk_widget_text(label, line, (int)(text + text_len - line), &txt,
                            Alignment.MIDDLECENTERED, font);
                        text_len++;
                        line_count++;
                        line_width = 0;
                        line = text + text_len;
                        line_offset += row_height;
                        glyph_len = Nk.nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len));
                        continue;
                    }
                    if (unicode == '\r')
                    {
                        text_len++;
                        glyph_len = Nk.nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len));
                        continue;
                    }
                    glyph_width =

                        font.Width((NkHandle)font.Userdata, (float)font.Height, text + text_len,
                            (int)glyph_len);
                    line_width += glyph_width;
                    text_len += glyph_len;
                    glyph_len = Nk.nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len));
                    continue;
                }
                if (line_width > 0)
                {
                    NkRect label = new NkRect();
                    label.y = pos_y + line_offset;
                    label.h = row_height;
                    label.w = line_width;
                    label.x = pos_x;
                    if (line_count == 0) label.x += x_offset;
                    if (is_selected)
                        nk_fill_rect(label, 0, background);
                    nk_widget_text(label, line, (int)(text + text_len - line), &txt,
                        Alignment.MIDDLELEFT, font);
                }
            }

        }

        public void nk_draw_property(nk_style_property style, NkRect* bounds,
            NkRect* label, NkWidgetStates state, char* name, int len, NkUserFont font)
        {
            nk_text text = new nk_text();
            NkStyleItem background;
            if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.active;
                text.text = style.label_active;
            }
            else if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                text.text = style.label_hover;
            }
            else
            {
                background = style.normal;
                text.text = style.label_normal;
            }

            if (background.Type == NkStyleItemType.IMAGE)
            {
                nk_draw_image(*bounds, background.Data.Image, Nk.nk_white);
                text.background = Nk.nk_rgba((int)0, (int)0, (int)0, (int)0);
            }
            else
            {
                text.background = background.Data.Color;
                nk_fill_rect(*bounds, style.rounding, background.Data.Color);
                nk_stroke_rect(*bounds, style.rounding, style.border,
                    background.Data.Color);
            }

            text.padding = Nk.nk_vec2_((float)0, (float)0);
            nk_widget_text(*label, name, len, &text, Alignment.MIDDLECENTERED, font);
        }

        public void nk_draw_color_picker(NkRect* matrix, NkRect* hue_bar,
            NkRect* alpha_bar, NkColorF col)
        {
            NkColor black = Nk.nk_black;
            NkColor white = Nk.nk_white;
            NkColor black_trans = new NkColor();
            float crosshair_size = 7.0f;
            NkColor temp = new NkColor();
            float* hsva = stackalloc float[4];
            float line_y;
            int i;
            Nk.nk_colorf_hsva_fv(hsva, col);
            for (i = 0; i < 6; ++i)
            {
                nk_fill_rect_multi_color(
                    Nk.nk_rect_((float)hue_bar->x, (float)(hue_bar->y + (float)i * (hue_bar->h / 6.0f) + 0.5f),
                        (float)hue_bar->w, (float)(hue_bar->h / 6.0f + 0.5f)), Nk.hue_colors[i],
                    Nk.hue_colors[i], Nk.hue_colors[i + 1], Nk.hue_colors[i + 1]);
            }
            line_y = (int)(hue_bar->y + hsva[0] * matrix->h + 0.5f);
            nk_stroke_line(hue_bar->x - 1, line_y, hue_bar->x + hue_bar->w + 2,
                line_y, 1, Nk.nk_rgb((int)255, (int)255, (int)255));
            if (alpha_bar != null)
            {
                float alpha =
                    0 < (1.0f < col.a ? 1.0f : col.a) ? (1.0f < col.a ? 1.0f : col.a) : 0;
                line_y = (int)(alpha_bar->y + (1.0f - alpha) * matrix->h + 0.5f);
                nk_fill_rect_multi_color(*alpha_bar, white, white,
                    black, black);
                nk_stroke_line(alpha_bar->x - 1, line_y,
                    alpha_bar->x + alpha_bar->w + 2, line_y, 1,
                    Nk.nk_rgb((int)255, (int)255, (int)255));
            }

            temp = Nk.nk_hsv_f((float)hsva[0], (float)1.0f, (float)1.0f);
            nk_fill_rect_multi_color(*matrix, white, temp, temp,
                white);
            nk_fill_rect_multi_color(*matrix, black_trans, black_trans,
                black, black);
            {
                NkVec2 p = new NkVec2();
                float S = hsva[1];
                float V = hsva[2];
                p.x = (int)(matrix->x + S * matrix->w);
                p.y = (int)(matrix->y + (1.0f - V) * matrix->h);
                nk_stroke_line(p.x - crosshair_size, p.y, p.x - 2, p.y,
                    1.0f, white);
                nk_stroke_line(p.x + crosshair_size + 1, p.y, p.x + 3, p.y,
                    1.0f, white);
                nk_stroke_line(p.x, p.y + crosshair_size + 1, p.x, p.y + 3,
                    1.0f, white);
                nk_stroke_line(p.x, p.y - crosshair_size, p.x, p.y - 2,
                    1.0f, white);
            }

        }
    }
}