using System.Runtime.InteropServices;

namespace NuklearSharp
{

        public unsafe partial class nk_scroll
        {
            public uint x;
            public uint y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct nk_command
        {
            public NkCommandType type;
            public ulong next;
        }

        public unsafe partial class nk_row_layout
        {
            public NkPanelRowLayoutType type;
            public int index;
            public float height;
            public float min_height;
            public int columns;
            public float* ratio;
            public float item_width;
            public float item_height;
            public float item_offset;
            public float filled;
            public NkRect item = new NkRect();
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
            public NkWindow win;
            public NkPanelType type;
            public NkPopupBuffer buf = new NkPopupBuffer();
            public uint name;
            public int active;
            public uint combo_count;
            public uint con_count;
            public uint con_old;
            public uint active_con;
            public NkRect header = new NkRect();
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
            public NkTextEditMode mode;
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
            public NkPropertyStatus state;
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
    public unsafe static partial class Nk
    {
        public static void nk_push_scissor(NkCommandBuffer b, NkRect r)
        {
            NkCommandScissor cmd;
            if (b == null) return;
            b.Clip.x = (float)(r.x);
            b.Clip.y = (float)(r.y);
            b.Clip.w = (float)(r.w);
            b.Clip.h = (float)(r.h);
            cmd = (NkCommandScissor)(nk_command_buffer_push(b, (NkCommandType.SCISSOR)));
            if (cmd == null) return;
            cmd.X = ((short)(r.x));
            cmd.Y = ((short)(r.y));
            cmd.W = ((ushort)((0) < (r.w) ? (r.w) : (0)));
            cmd.H = ((ushort)((0) < (r.h) ? (r.h) : (0)));
        }

        public static void nk_stroke_line(NkCommandBuffer b, float x0, float y0, float x1, float y1,
            float line_thickness, NkColor c)
        {
            NkCommandLine cmd;
            if ((b == null) || (line_thickness <= 0)) return;
            cmd = (NkCommandLine)(nk_command_buffer_push(b, (NkCommandType.LINE)));
            if (cmd == null) return;
            cmd.LineThickness = ((ushort)(line_thickness));
            cmd.Begin.x = ((short)(x0));
            cmd.Begin.y = ((short)(y0));
            cmd.End.x = ((short)(x1));
            cmd.End.y = ((short)(y1));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_stroke_curve(NkCommandBuffer b, float ax, float ay, float ctrl0x, float ctrl0y,
            float ctrl1x, float ctrl1y, float bx, float by, float line_thickness, NkColor col)
        {
            NkCommandCurve cmd;
            if (((b == null) || ((col.a) == (0))) || (line_thickness <= 0)) return;
            cmd = (NkCommandCurve)(nk_command_buffer_push(b, (NkCommandType.CURVE)));
            if (cmd == null) return;
            cmd.LineThickness = ((ushort)(line_thickness));
            cmd.Begin.x = ((short)(ax));
            cmd.Begin.y = ((short)(ay));
            cmd.Ctrl0.x = ((short)(ctrl0x));
            cmd.Ctrl0.y = ((short)(ctrl0y));
            cmd.Ctrl1.x = ((short)(ctrl1x));
            cmd.Ctrl1.y = ((short)(ctrl1y));
            cmd.End.x = ((short)(bx));
            cmd.End.y = ((short)(by));
            cmd.Color = (NkColor)(col);
        }

        public static void nk_stroke_rect(NkCommandBuffer b, NkRect rect, float rounding, float line_thickness,
            NkColor c)
        {
            NkCommandRect cmd;
            if (((((b == null) || ((c.a) == (0))) || ((rect.w) == (0))) || ((rect.h) == (0))) || (line_thickness <= 0))
                return;
            if ((b.UseClipping))
            {
                if (
                    !(!(((((b.Clip.x) > (rect.x + rect.w)) || ((b.Clip.x + b.Clip.w) < (rect.x))) ||
                         ((b.Clip.y) > (rect.y + rect.h))) || ((b.Clip.y + b.Clip.h) < (rect.y))))) return;
            }

            cmd = (NkCommandRect)(nk_command_buffer_push(b, (NkCommandType.RECT)));
            if (cmd == null) return;
            cmd.Rounding = ((ushort)(rounding));
            cmd.LineThickness = ((ushort)(line_thickness));
            cmd.X = ((short)(rect.x));
            cmd.Y = ((short)(rect.y));
            cmd.W = ((ushort)((0) < (rect.w) ? (rect.w) : (0)));
            cmd.H = ((ushort)((0) < (rect.h) ? (rect.h) : (0)));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_fill_rect(NkCommandBuffer b, NkRect rect, float rounding, NkColor c)
        {
            NkCommandRectFilled cmd;
            if ((((b == null) || ((c.a) == (0))) || ((rect.w) == (0))) || ((rect.h) == (0))) return;
            if ((b.UseClipping))
            {
                if (
                    !(!(((((b.Clip.x) > (rect.x + rect.w)) || ((b.Clip.x + b.Clip.w) < (rect.x))) ||
                         ((b.Clip.y) > (rect.y + rect.h))) || ((b.Clip.y + b.Clip.h) < (rect.y))))) return;
            }

            cmd = (NkCommandRectFilled)(nk_command_buffer_push(b, (NkCommandType.RECT_FILLED)));
            if (cmd == null) return;
            cmd.Rounding = ((ushort)(rounding));
            cmd.X = ((short)(rect.x));
            cmd.Y = ((short)(rect.y));
            cmd.W = ((ushort)((0) < (rect.w) ? (rect.w) : (0)));
            cmd.H = ((ushort)((0) < (rect.h) ? (rect.h) : (0)));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_fill_rect_multi_color(NkCommandBuffer b, NkRect rect, NkColor left, NkColor top,
            NkColor right, NkColor bottom)
        {
            NkCommandRectMultiColor cmd;
            if (((b == null) || ((rect.w) == (0))) || ((rect.h) == (0))) return;
            if ((b.UseClipping))
            {
                if (
                    !(!(((((b.Clip.x) > (rect.x + rect.w)) || ((b.Clip.x + b.Clip.w) < (rect.x))) ||
                         ((b.Clip.y) > (rect.y + rect.h))) || ((b.Clip.y + b.Clip.h) < (rect.y))))) return;
            }

            cmd = (NkCommandRectMultiColor)(nk_command_buffer_push(b, (NkCommandType.RECT_MULTI_COLOR)));
            if (cmd == null) return;
            cmd.X = ((short)(rect.x));
            cmd.Y = ((short)(rect.y));
            cmd.W = ((ushort)((0) < (rect.w) ? (rect.w) : (0)));
            cmd.H = ((ushort)((0) < (rect.h) ? (rect.h) : (0)));
            cmd.Left = (NkColor)(left);
            cmd.Top = (NkColor)(top);
            cmd.Right = (NkColor)(right);
            cmd.Bottom = (NkColor)(bottom);
        }

        public static void nk_stroke_circle(NkCommandBuffer b, NkRect r, float line_thickness, NkColor c)
        {
            NkCommandCircle cmd;
            if ((((b == null) || ((r.w) == (0))) || ((r.h) == (0))) || (line_thickness <= 0)) return;
            if ((b.UseClipping))
            {
                if (
                    !(!(((((b.Clip.x) > (r.x + r.w)) || ((b.Clip.x + b.Clip.w) < (r.x))) || ((b.Clip.y) > (r.y + r.h))) ||
                        ((b.Clip.y + b.Clip.h) < (r.y))))) return;
            }

            cmd = (NkCommandCircle)(nk_command_buffer_push(b, (NkCommandType.CIRCLE)));
            if (cmd == null) return;
            cmd.LineThickness = ((ushort)(line_thickness));
            cmd.X = ((short)(r.x));
            cmd.Y = ((short)(r.y));
            cmd.W = ((ushort)((r.w) < (0) ? (0) : (r.w)));
            cmd.H = ((ushort)((r.h) < (0) ? (0) : (r.h)));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_fill_circle(NkCommandBuffer b, NkRect r, NkColor c)
        {
            NkCommandCircleFilled cmd;
            if ((((b == null) || ((c.a) == (0))) || ((r.w) == (0))) || ((r.h) == (0))) return;
            if ((b.UseClipping))
            {
                if (
                    !(!(((((b.Clip.x) > (r.x + r.w)) || ((b.Clip.x + b.Clip.w) < (r.x))) || ((b.Clip.y) > (r.y + r.h))) ||
                        ((b.Clip.y + b.Clip.h) < (r.y))))) return;
            }

            cmd = (NkCommandCircleFilled)(nk_command_buffer_push(b, (NkCommandType.CIRCLE_FILLED)));
            if (cmd == null) return;
            cmd.X = ((short)(r.x));
            cmd.Y = ((short)(r.y));
            cmd.W = ((ushort)((r.w) < (0) ? (0) : (r.w)));
            cmd.H = ((ushort)((r.h) < (0) ? (0) : (r.h)));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_stroke_arc(NkCommandBuffer b, float cx, float cy, float radius, float a_min, float a_max,
            float line_thickness, NkColor c)
        {
            NkCommandArc cmd;
            if (((b == null) || ((c.a) == (0))) || (line_thickness <= 0)) return;
            cmd = (NkCommandArc)(nk_command_buffer_push(b, (NkCommandType.ARC)));
            if (cmd == null) return;
            cmd.LineThickness = ((ushort)(line_thickness));
            cmd.Cx = ((short)(cx));
            cmd.Cy = ((short)(cy));
            cmd.R = ((ushort)(radius));
            cmd.A[0] = (float)(a_min);
            cmd.A[1] = (float)(a_max);
            cmd.Color = (NkColor)(c);
        }

        public static void nk_fill_arc(NkCommandBuffer b, float cx, float cy, float radius, float a_min, float a_max,
            NkColor c)
        {
            NkCommandArcFilled cmd;
            if ((b == null) || ((c.a) == (0))) return;
            cmd = (NkCommandArcFilled)(nk_command_buffer_push(b, (NkCommandType.ARC_FILLED)));
            if (cmd == null) return;
            cmd.Cx = ((short)(cx));
            cmd.Cy = ((short)(cy));
            cmd.R = ((ushort)(radius));
            cmd.A[0] = (float)(a_min);
            cmd.A[1] = (float)(a_max);
            cmd.Color = (NkColor)(c);
        }

        public static void nk_stroke_triangle(NkCommandBuffer b, float x0, float y0, float x1, float y1, float x2,
            float y2, float line_thickness, NkColor c)
        {
            NkCommandTriangle cmd;
            if (((b == null) || ((c.a) == (0))) || (line_thickness <= 0)) return;
            if ((b.UseClipping))
            {
                if (
                    ((!((((b.Clip.x) <= (x0)) && ((x0) < (b.Clip.x + b.Clip.w))) &&
                        (((b.Clip.y) <= (y0)) && ((y0) < (b.Clip.y + b.Clip.h))))) &&
                     (!((((b.Clip.x) <= (x1)) && ((x1) < (b.Clip.x + b.Clip.w))) &&
                        (((b.Clip.y) <= (y1)) && ((y1) < (b.Clip.y + b.Clip.h)))))) &&
                    (!((((b.Clip.x) <= (x2)) && ((x2) < (b.Clip.x + b.Clip.w))) &&
                       (((b.Clip.y) <= (y2)) && ((y2) < (b.Clip.y + b.Clip.h)))))) return;
            }

            cmd = (NkCommandTriangle)(nk_command_buffer_push(b, (NkCommandType.TRIANGLE)));
            if (cmd == null) return;
            cmd.LineThickness = ((ushort)(line_thickness));
            cmd.A.x = ((short)(x0));
            cmd.A.y = ((short)(y0));
            cmd.B.x = ((short)(x1));
            cmd.B.y = ((short)(y1));
            cmd.C.x = ((short)(x2));
            cmd.C.y = ((short)(y2));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_fill_triangle(NkCommandBuffer b, float x0, float y0, float x1, float y1, float x2,
            float y2, NkColor c)
        {
            NkCommandTriangleFilled cmd;
            if ((b == null) || ((c.a) == (0))) return;
            if (b == null) return;
            if ((b.UseClipping))
            {
                if (
                    ((!((((b.Clip.x) <= (x0)) && ((x0) < (b.Clip.x + b.Clip.w))) &&
                        (((b.Clip.y) <= (y0)) && ((y0) < (b.Clip.y + b.Clip.h))))) &&
                     (!((((b.Clip.x) <= (x1)) && ((x1) < (b.Clip.x + b.Clip.w))) &&
                        (((b.Clip.y) <= (y1)) && ((y1) < (b.Clip.y + b.Clip.h)))))) &&
                    (!((((b.Clip.x) <= (x2)) && ((x2) < (b.Clip.x + b.Clip.w))) &&
                       (((b.Clip.y) <= (y2)) && ((y2) < (b.Clip.y + b.Clip.h)))))) return;
            }

            cmd = (NkCommandTriangleFilled)(nk_command_buffer_push(b, (NkCommandType.TRIANGLE_FILLED)));
            if (cmd == null) return;
            cmd.A.x = ((short)(x0));
            cmd.A.y = ((short)(y0));
            cmd.B.x = ((short)(x1));
            cmd.B.y = ((short)(y1));
            cmd.C.x = ((short)(x2));
            cmd.C.y = ((short)(y2));
            cmd.Color = (NkColor)(c);
        }

        public static void nk_draw_image(NkCommandBuffer b, NkRect r, NkImage img, NkColor col)
        {
            NkCommandImage cmd;
            if (b == null) return;
            if ((b.UseClipping))
            {
                if ((((b.Clip.w) == (0)) || ((b.Clip.h) == (0))) ||
                    (!(!(((((b.Clip.x) > (r.x + r.w)) || ((b.Clip.x + b.Clip.w) < (r.x))) || ((b.Clip.y) > (r.y + r.h))) ||
                         ((b.Clip.y + b.Clip.h) < (r.y)))))) return;
            }

            cmd = (NkCommandImage)(nk_command_buffer_push(b, (NkCommandType.IMAGE)));
            if (cmd == null) return;
            cmd.X = ((short)(r.x));
            cmd.Y = ((short)(r.y));
            cmd.W = ((ushort)((0) < (r.w) ? (r.w) : (0)));
            cmd.H = ((ushort)((0) < (r.h) ? (r.h) : (0)));
            cmd.Img = (NkImage)(img);
            cmd.Col = (NkColor)(col);
        }

        public static void nk_push_custom(NkCommandBuffer b, NkRect r, NkCommandCustomCallback cb, NkHandle usr)
        {
            NkCommandCustom cmd;
            if (b == null) return;
            if ((b.UseClipping))
            {
                if ((((b.Clip.w) == (0)) || ((b.Clip.h) == (0))) ||
                    (!(!(((((b.Clip.x) > (r.x + r.w)) || ((b.Clip.x + b.Clip.w) < (r.x))) || ((b.Clip.y) > (r.y + r.h))) ||
                         ((b.Clip.y + b.Clip.h) < (r.y)))))) return;
            }

            cmd = (NkCommandCustom)(nk_command_buffer_push(b, (NkCommandType.CUSTOM)));
            if (cmd == null) return;
            cmd.X = ((short)(r.x));
            cmd.Y = ((short)(r.y));
            cmd.W = ((ushort)((0) < (r.w) ? (r.w) : (0)));
            cmd.H = ((ushort)((0) < (r.h) ? (r.h) : (0)));
            cmd.CallbackData = (NkHandle)(usr);
            cmd.Callback = cb;
        }

        public static void nk_draw_text(NkCommandBuffer b, NkRect r, char* _string_, int length, NkUserFont font,
            NkColor bg, NkColor fg)
        {
            float text_width = (float)(0);
            NkCommandText cmd;
            if ((((b == null) || (_string_ == null)) || (length == 0)) || (((bg.a) == (0)) && ((fg.a) == (0)))) return;
            if ((b.UseClipping))
            {
                if ((((b.Clip.w) == (0)) || ((b.Clip.h) == (0))) ||
                    (!(!(((((b.Clip.x) > (r.x + r.w)) || ((b.Clip.x + b.Clip.w) < (r.x))) || ((b.Clip.y) > (r.y + r.h))) ||
                         ((b.Clip.y + b.Clip.h) < (r.y)))))) return;
            }

            text_width =
                (float)(font.Width((NkHandle)(font.Userdata), (float)(font.Height), _string_, (int)(length)));
            if ((text_width) > (r.w))
            {
                int glyphs = (int)(0);
                float txt_width = (float)(text_width);
                length =
                    (int)
                        (nk_text_clamp(font, _string_, (int)(length), (float)(r.w), &glyphs, &txt_width, null,
                            (int)(0)));
            }

            if (length == 0) return;
            cmd = (NkCommandText)(nk_command_buffer_push(b, (NkCommandType.TEXT)));
            if (cmd == null) return;
            cmd.X = ((short)(r.x));
            cmd.Y = ((short)(r.y));
            cmd.W = ((ushort)(r.w));
            cmd.H = ((ushort)(r.h));
            cmd.Background = (NkColor)(bg);
            cmd.Foreground = (NkColor)(fg);
            cmd.Font = font;
            cmd.Length = (int)(length);
            cmd.Height = (float)(font.Height);
            cmd.String = new PinnedArray<char>(length);
            CRuntime.Memcpy((void*)cmd.String, _string_, length * sizeof(char));
            cmd.String[length] = ('\0');
        }

        public static void nk_widget_text(NkCommandBuffer o, NkRect b, char* _string_, int len, nk_text* t, Alignment a,
            NkUserFont f)
        {
            NkRect label = new NkRect();
            float text_width;
            if ((o == null) || (t == null)) return;
            b.h = (float)((b.h) < (2 * t->padding.y) ? (2 * t->padding.y) : (b.h));
            label.x = (float)(0);
            label.w = (float)(0);
            label.y = (float)(b.y + t->padding.y);
            label.h = (float)((f.Height) < (b.h - 2 * t->padding.y) ? (f.Height) : (b.h - 2 * t->padding.y));
            text_width = (float)(f.Width((NkHandle)(f.Userdata), (float)(f.Height), _string_, (int)(len)));
            text_width += (float)(2.0f * t->padding.x);
            if ((a & Alignment.LEFT) != 0)
            {
                label.x = (float)(b.x + t->padding.x);
                label.w = (float)((0) < (b.w - 2 * t->padding.x) ? (b.w - 2 * t->padding.x) : (0));
            }
            else if ((a & Alignment.CENTERED) != 0)
            {
                label.w = (float)((1) < (2 * t->padding.x + text_width) ? (2 * t->padding.x + text_width) : (1));
                label.x = (float)(b.x + t->padding.x + ((b.w - 2 * t->padding.x) - label.w) / 2);
                label.x = (float)((b.x + t->padding.x) < (label.x) ? (label.x) : (b.x + t->padding.x));
                label.w = (float)((b.x + b.w) < (label.x + label.w) ? (b.x + b.w) : (label.x + label.w));
                if ((label.w) >= (label.x)) label.w -= (float)(label.x);
            }
            else if ((a & Alignment.RIGHT) != 0)
            {
                label.x =
                    (float)
                        ((b.x + t->padding.x) < ((b.x + b.w) - (2 * t->padding.x + text_width))
                            ? ((b.x + b.w) - (2 * t->padding.x + text_width))
                            : (b.x + t->padding.x));
                label.w = (float)(text_width + 2 * t->padding.x);
            }
            else return;
            if ((a & Alignment.MIDDLE) != 0)
            {
                label.y = (float)(b.y + b.h / 2.0f - f.Height / 2.0f);
                label.h =
                    (float)
                        ((b.h / 2.0f) < (b.h - (b.h / 2.0f + f.Height / 2.0f))
                            ? (b.h - (b.h / 2.0f + f.Height / 2.0f))
                            : (b.h / 2.0f));
            }
            else if ((a & Alignment.BOTTOM) != 0)
            {
                label.y = (float)(b.y + b.h - f.Height);
                label.h = (float)(f.Height);
            }

            nk_draw_text(o, (NkRect)(label), _string_, (int)(len), f, (NkColor)(t->background),
                (NkColor)(t->text));
        }

        public static void nk_widget_text_wrap(NkCommandBuffer o, NkRect b, char* _string_, int len, nk_text* t,
            NkUserFont f)
        {
            float width;
            int glyphs = (int)(0);
            int fitting = (int)(0);
            int done = (int)(0);
            NkRect line = new NkRect();
            nk_text text = new nk_text();
            uint* seperator = stackalloc uint[1];
            seperator[0] = (uint)(' ');

            if ((o == null) || (t == null)) return;
            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            text.background = (NkColor)(t->background);
            text.text = (NkColor)(t->text);
            b.w = (float)((b.w) < (2 * t->padding.x) ? (2 * t->padding.x) : (b.w));
            b.h = (float)((b.h) < (2 * t->padding.y) ? (2 * t->padding.y) : (b.h));
            b.h = (float)(b.h - 2 * t->padding.y);
            line.x = (float)(b.x + t->padding.x);
            line.y = (float)(b.y + t->padding.y);
            line.w = (float)(b.w - 2 * t->padding.x);
            line.h = (float)(2 * t->padding.y + f.Height);
            fitting = (int)(nk_text_clamp(f, _string_, (int)(len), (float)(line.w), &glyphs, &width, seperator, 1));
            while ((done) < (len))
            {
                if ((fitting == 0) || ((line.y + line.h) >= (b.y + b.h))) break;
                nk_widget_text(o, (NkRect)(line), &_string_[done], (int)(fitting), &text, (Alignment.MIDDLELEFT), f);
                done += (int)(fitting);
                line.y += (float)(f.Height + 2 * t->padding.y);
                fitting =
                    (int)
                        (nk_text_clamp(f, &_string_[done], (int)(len - done), (float)(line.w), &glyphs, &width,
                            seperator, 1));
            }
        }

        public static void nk_draw_symbol(NkCommandBuffer _out_, NkSymbolType type, NkRect content, NkColor background,
            NkColor foreground, float border_width, NkUserFont font)
        {
            switch (type)
            {
                case NkSymbolType.X:
                case NkSymbolType.UNDERSCORE:
                case NkSymbolType.PLUS:
                case NkSymbolType.MINUS:
                    {
                        char X = ((type) == (NkSymbolType.X))
                            ? 'x'
                            : ((type) == (NkSymbolType.UNDERSCORE)) ? '_' : ((type) == (NkSymbolType.PLUS)) ? '+' : '-';
                        nk_text text = new nk_text();
                        text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                        text.background = (NkColor)(background);
                        text.text = (NkColor)(foreground);
                        nk_widget_text(_out_, (NkRect)(content), &X, (int)(1), &text, (Alignment.MIDDLECENTERED), font);
                    }
                    break;
                case NkSymbolType.CIRCLE_SOLID:
                case NkSymbolType.CIRCLE_OUTLINE:
                case NkSymbolType.RECT_SOLID:
                case NkSymbolType.RECT_OUTLINE:
                    {
                        if (((type) == (NkSymbolType.RECT_SOLID)) || ((type) == (NkSymbolType.RECT_OUTLINE)))
                        {
                            nk_fill_rect(_out_, (NkRect)(content), (float)(0), (NkColor)(foreground));
                            if ((type) == (NkSymbolType.RECT_OUTLINE))
                                nk_fill_rect(_out_, (NkRect)(nk_shrink_rect_((NkRect)(content), (float)(border_width))),
                                    (float)(0), (NkColor)(background));
                        }
                        else
                        {
                            nk_fill_circle(_out_, (NkRect)(content), (NkColor)(foreground));
                            if ((type) == (NkSymbolType.CIRCLE_OUTLINE))
                                nk_fill_circle(_out_, (NkRect)(nk_shrink_rect_((NkRect)(content), (float)(1))),
                                    (NkColor)(background));
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
        
                                (((type) == (NkSymbolType.TRIANGLE_RIGHT))
                                    ? NkHeading.NK_RIGHT
                                    : ((type) == (NkSymbolType.TRIANGLE_LEFT))
                                        ? NkHeading.NK_LEFT
                                        : ((type) == (NkSymbolType.TRIANGLE_UP)) ? NkHeading.NK_UP : NkHeading.NK_DOWN);
                        nk_triangle_from_direction(points, (NkRect)(content), (float)(0), (float)(0), (heading));
                        nk_fill_triangle(_out_, (float)(points[0].x), (float)(points[0].y), (float)(points[1].x),
                            (float)(points[1].y), (float)(points[2].x), (float)(points[2].y), (NkColor)(foreground));
                    }
                    break;
                default:
                case NkSymbolType.NONE:
                case NkSymbolType.MAX:
                    break;
            }

        }

        public static NkStyleItem nk_draw_button(NkCommandBuffer _out_, NkRect* bounds, NkWidgetStates state,
            nk_style_button style)
        {
            NkStyleItem background;
            if ((state & NkWidgetStates.HOVER) != 0) background = style.hover;
            else if ((state & NkWidgetStates.ACTIVED) != 0) background = style.active;
            else background = style.normal;
            if ((background.Type) == (NkStyleItemType.IMAGE))
            {
                nk_draw_image(_out_, (NkRect)(*bounds), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                nk_fill_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (float)(style.border),
                    (NkColor)(style.border_color));
            }

            return background;
        }

        public static void nk_draw_button_text(NkCommandBuffer _out_, NkRect* bounds, NkRect* content, NkWidgetStates state,
            nk_style_button style, char* txt, int len, Alignment text_alignment, NkUserFont font)
        {
            nk_text text = new nk_text();
            NkStyleItem background;
            background = nk_draw_button(_out_, bounds, (state), style);
            if ((background.Type) == (NkStyleItemType.COLOR)) text.background = (NkColor)(background.Data.Color);
            else text.background = (NkColor)(style.text_background);
            if ((state & NkWidgetStates.HOVER) != 0) text.text = (NkColor)(style.text_hover);
            else if ((state & NkWidgetStates.ACTIVED) != 0) text.text = (NkColor)(style.text_active);
            else text.text = (NkColor)(style.text_normal);
            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            nk_widget_text(_out_, (NkRect)(*content), txt, (int)(len), &text, (text_alignment), font);
        }

        public static void nk_draw_button_symbol(NkCommandBuffer _out_, NkRect* bounds, NkRect* content, NkWidgetStates state,
            nk_style_button style, NkSymbolType type, NkUserFont font)
        {
            NkColor sym = new NkColor();
            NkColor bg = new NkColor();
            NkStyleItem background;
            background = nk_draw_button(_out_, bounds, (state), style);
            if ((background.Type) == (NkStyleItemType.COLOR)) bg = (NkColor)(background.Data.Color);
            else bg = (NkColor)(style.text_background);
            if ((state & NkWidgetStates.HOVER) != 0) sym = (NkColor)(style.text_hover);
            else if ((state & NkWidgetStates.ACTIVED) != 0) sym = (NkColor)(style.text_active);
            else sym = (NkColor)(style.text_normal);
            nk_draw_symbol(_out_, (type), (NkRect)(*content), (NkColor)(bg), (NkColor)(sym), (float)(1),
                font);
        }

        public static void nk_draw_button_image(NkCommandBuffer _out_, NkRect* bounds, NkRect* content, NkWidgetStates state,
            nk_style_button style, NkImage img)
        {
            nk_draw_button(_out_, bounds, (state), style);
            nk_draw_image(_out_, (NkRect)(*content), img, (NkColor)(nk_white));
        }

        public static void nk_draw_button_text_symbol(NkCommandBuffer _out_, NkRect* bounds, NkRect* label,
            NkRect* symbol, NkWidgetStates state, nk_style_button style, char* str, int len, NkSymbolType type, NkUserFont font)
        {
            NkColor sym = new NkColor();
            nk_text text = new nk_text();
            NkStyleItem background;
            background = nk_draw_button(_out_, bounds, (state), style);
            if ((background.Type) == (NkStyleItemType.COLOR)) text.background = (NkColor)(background.Data.Color);
            else text.background = (NkColor)(style.text_background);
            if ((state & NkWidgetStates.HOVER) != 0)
            {
                sym = (NkColor)(style.text_hover);
                text.text = (NkColor)(style.text_hover);
            }
            else if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                sym = (NkColor)(style.text_active);
                text.text = (NkColor)(style.text_active);
            }
            else
            {
                sym = (NkColor)(style.text_normal);
                text.text = (NkColor)(style.text_normal);
            }

            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            nk_draw_symbol(_out_, type, (NkRect)(*symbol), (NkColor)(style.text_background),
                (NkColor)(sym), (float)(0), font);
            nk_widget_text(_out_, (NkRect)(*label), str, (int)(len), &text, (Alignment.MIDDLECENTERED), font);
        }

        public static void nk_draw_button_text_image(NkCommandBuffer _out_, NkRect* bounds, NkRect* label,
            NkRect* image, NkWidgetStates state, nk_style_button style, char* str, int len, NkUserFont font, NkImage img)
        {
            nk_text text = new nk_text();
            NkStyleItem background;
            background = nk_draw_button(_out_, bounds, (state), style);
            if ((background.Type) == (NkStyleItemType.COLOR)) text.background = (NkColor)(background.Data.Color);
            else text.background = (NkColor)(style.text_background);
            if ((state & NkWidgetStates.HOVER) != 0) text.text = (NkColor)(style.text_hover);
            else if ((state & NkWidgetStates.ACTIVED) != 0) text.text = (NkColor)(style.text_active);
            else text.text = (NkColor)(style.text_normal);
            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            nk_widget_text(_out_, (NkRect)(*label), str, (int)(len), &text, (Alignment.MIDDLECENTERED), font);
            nk_draw_image(_out_, (NkRect)(*image), img, (NkColor)(nk_white));
        }

        public static void nk_draw_checkbox(NkCommandBuffer _out_, NkWidgetStates state, nk_style_toggle style, int active,
            NkRect* label, NkRect* selector, NkRect* cursors, char* _string_, int len, NkUserFont font)
        {
            NkStyleItem background;
            NkStyleItem cursor;
            nk_text text = new nk_text();
            if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = (NkColor)(style.text_hover);
            }
            else if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = (NkColor)(style.text_active);
            }
            else
            {
                background = style.normal;
                cursor = style.cursor_normal;
                text.text = (NkColor)(style.text_normal);
            }

            if ((background.Type) == (NkStyleItemType.COLOR))
            {
                nk_fill_rect(_out_, (NkRect)(*selector), (float)(0), (NkColor)(style.border_color));
                nk_fill_rect(_out_, (NkRect)(nk_shrink_rect_((NkRect)(*selector), (float)(style.border))),
                    (float)(0), (NkColor)(background.Data.Color));
            }
            else nk_draw_image(_out_, (NkRect)(*selector), background.Data.Image, (NkColor)(nk_white));
            if ((active) != 0)
            {
                if ((cursor.Type) == (NkStyleItemType.IMAGE))
                    nk_draw_image(_out_, (NkRect)(*cursors), cursor.Data.Image, (NkColor)(nk_white));
                else nk_fill_rect(_out_, (NkRect)(*cursors), (float)(0), (NkColor)(cursor.Data.Color));
            }

            text.padding.x = (float)(0);
            text.padding.y = (float)(0);
            text.background = (NkColor)(style.text_background);
            nk_widget_text(_out_, (NkRect)(*label), _string_, (int)(len), &text, (Alignment.MIDDLELEFT), font);
        }

        public static void nk_draw_option(NkCommandBuffer _out_, NkWidgetStates state, nk_style_toggle style, int active,
            NkRect* label, NkRect* selector, NkRect* cursors, char* _string_, int len, NkUserFont font)
        {
            NkStyleItem background;
            NkStyleItem cursor;
            nk_text text = new nk_text();
            if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = (NkColor)(style.text_hover);
            }
            else if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.hover;
                cursor = style.cursor_hover;
                text.text = (NkColor)(style.text_active);
            }
            else
            {
                background = style.normal;
                cursor = style.cursor_normal;
                text.text = (NkColor)(style.text_normal);
            }

            if ((background.Type) == (NkStyleItemType.COLOR))
            {
                nk_fill_circle(_out_, (NkRect)(*selector), (NkColor)(style.border_color));
                nk_fill_circle(_out_, (NkRect)(nk_shrink_rect_((NkRect)(*selector), (float)(style.border))),
                    (NkColor)(background.Data.Color));
            }
            else nk_draw_image(_out_, (NkRect)(*selector), background.Data.Image, (NkColor)(nk_white));
            if ((active) != 0)
            {
                if ((cursor.Type) == (NkStyleItemType.IMAGE))
                    nk_draw_image(_out_, (NkRect)(*cursors), cursor.Data.Image, (NkColor)(nk_white));
                else nk_fill_circle(_out_, (NkRect)(*cursors), (NkColor)(cursor.Data.Color));
            }

            text.padding.x = (float)(0);
            text.padding.y = (float)(0);
            text.background = (NkColor)(style.text_background);
            nk_widget_text(_out_, (NkRect)(*label), _string_, (int)(len), &text, (Alignment.MIDDLELEFT), font);
        }

        public static void nk_draw_selectable(NkCommandBuffer _out_, NkWidgetStates state, nk_style_selectable style, int active,
            NkRect* bounds, NkRect* icon, NkImage img, char* _string_, int len, Alignment align, NkUserFont font)
        {
            NkStyleItem background;
            nk_text text = new nk_text();
            text.padding = (NkVec2)(style.padding);
            if (active == 0)
            {
                if ((state & NkWidgetStates.ACTIVED) != 0)
                {
                    background = style.pressed;
                    text.text = (NkColor)(style.text_pressed);
                }
                else if ((state & NkWidgetStates.HOVER) != 0)
                {
                    background = style.hover;
                    text.text = (NkColor)(style.text_hover);
                }
                else
                {
                    background = style.normal;
                    text.text = (NkColor)(style.text_normal);
                }
            }
            else
            {
                if ((state & NkWidgetStates.ACTIVED) != 0)
                {
                    background = style.pressed_active;
                    text.text = (NkColor)(style.text_pressed_active);
                }
                else if ((state & NkWidgetStates.HOVER) != 0)
                {
                    background = style.hover_active;
                    text.text = (NkColor)(style.text_hover_active);
                }
                else
                {
                    background = style.normal_active;
                    text.text = (NkColor)(style.text_normal_active);
                }
            }

            if ((background.Type) == (NkStyleItemType.IMAGE))
            {
                nk_draw_image(_out_, (NkRect)(*bounds), background.Data.Image, (NkColor)(nk_white));
                text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            }
            else
            {
                nk_fill_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (NkColor)(background.Data.Color));
                text.background = (NkColor)(background.Data.Color);
            }

            if (((img) != null) && ((icon) != null))
                nk_draw_image(_out_, (NkRect)(*icon), img, (NkColor)(nk_white));
            nk_widget_text(_out_, (NkRect)(*bounds), _string_, (int)(len), &text, (align), font);
        }

        public static void nk_draw_slider(NkCommandBuffer _out_, NkWidgetStates state, nk_style_slider style, NkRect* bounds,
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
                bar_color = (NkColor)(style.bar_active);
                cursor = style.cursor_active;
            }
            else if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                bar_color = (NkColor)(style.bar_hover);
                cursor = style.cursor_hover;
            }
            else
            {
                background = style.normal;
                bar_color = (NkColor)(style.bar_normal);
                cursor = style.cursor_normal;
            }

            bar.x = (float)(bounds->x);
            bar.y = (float)((visual_cursor->y + visual_cursor->h / 2) - bounds->h / 12);
            bar.w = (float)(bounds->w);
            bar.h = (float)(bounds->h / 6);
            fill.w = (float)((visual_cursor->x + (visual_cursor->w / 2.0f)) - bar.x);
            fill.x = (float)(bar.x);
            fill.y = (float)(bar.y);
            fill.h = (float)(bar.h);
            if ((background.Type) == (NkStyleItemType.IMAGE))
            {
                nk_draw_image(_out_, (NkRect)(*bounds), background.Data.Image, (NkColor)(nk_white));
            }
            else
            {
                nk_fill_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (float)(style.border),
                    (NkColor)(style.border_color));
            }

            nk_fill_rect(_out_, (NkRect)(bar), (float)(style.rounding), (NkColor)(bar_color));
            nk_fill_rect(_out_, (NkRect)(fill), (float)(style.rounding), (NkColor)(style.bar_filled));
            if ((cursor.Type) == (NkStyleItemType.IMAGE))
                nk_draw_image(_out_, (NkRect)(*visual_cursor), cursor.Data.Image, (NkColor)(nk_white));
            else nk_fill_circle(_out_, (NkRect)(*visual_cursor), (NkColor)(cursor.Data.Color));
        }

        public static void nk_draw_progress(NkCommandBuffer _out_, NkWidgetStates state, nk_style_progress style,
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

            if ((background.Type) == (NkStyleItemType.COLOR))
            {
                nk_fill_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (float)(style.border),
                    (NkColor)(style.border_color));
            }
            else nk_draw_image(_out_, (NkRect)(*bounds), background.Data.Image, (NkColor)(nk_white));
            if ((cursor.Type) == (NkStyleItemType.COLOR))
            {
                nk_fill_rect(_out_, (NkRect)(*scursor), (float)(style.rounding), (NkColor)(cursor.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*scursor), (float)(style.rounding), (float)(style.border),
                    (NkColor)(style.border_color));
            }
            else nk_draw_image(_out_, (NkRect)(*scursor), cursor.Data.Image, (NkColor)(nk_white));
        }

        public static void nk_draw_scrollbar(NkCommandBuffer _out_, NkWidgetStates state, nk_style_scrollbar style,
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

            if ((background.Type) == (NkStyleItemType.COLOR))
            {
                nk_fill_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (float)(style.border),
                    (NkColor)(style.border_color));
            }
            else
            {
                nk_draw_image(_out_, (NkRect)(*bounds), background.Data.Image, (NkColor)(nk_white));
            }

            if ((background.Type) == (NkStyleItemType.COLOR))
            {
                nk_fill_rect(_out_, (NkRect)(*scroll), (float)(style.rounding_cursor), (NkColor)(cursor.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*scroll), (float)(style.rounding_cursor),
                    (float)(style.border_cursor), (NkColor)(style.cursor_border_color));
            }
            else nk_draw_image(_out_, (NkRect)(*scroll), cursor.Data.Image, (NkColor)(nk_white));
        }

        public static void nk_edit_draw_text(NkCommandBuffer _out_, nk_style_edit style, float pos_x, float pos_y,
            float x_offset, char* text, int byte_len, float row_height, NkUserFont font, NkColor background,
            NkColor foreground, int is_selected)
        {
            if ((((text == null) || (byte_len == 0)) || (_out_ == null)) || (style == null)) return;
            {
                int glyph_len = (int)(0);
                char unicode = (char)0;
                int text_len = (int)(0);
                float line_width = (float)(0);
                float glyph_width;
                char* line = text;
                float line_offset = (float)(0);
                int line_count = (int)(0);
                nk_text txt = new nk_text();
                txt.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
                txt.background = (NkColor)(background);
                txt.text = (NkColor)(foreground);
                glyph_len = (int)(nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len)));
                if (glyph_len == 0) return;
                while (((text_len) < (byte_len)) && ((glyph_len) != 0))
                {
                    if ((unicode) == ('\n'))
                    {
                        NkRect label = new NkRect();
                        label.y = (float)(pos_y + line_offset);
                        label.h = (float)(row_height);
                        label.w = (float)(line_width);
                        label.x = (float)(pos_x);
                        if (line_count == 0) label.x += (float)(x_offset);
                        if ((is_selected) != 0)
                            nk_fill_rect(_out_, (NkRect)(label), (float)(0), (NkColor)(background));
                        nk_widget_text(_out_, (NkRect)(label), line, (int)((text + text_len) - line), &txt,
                            (Alignment.MIDDLECENTERED), font);
                        text_len++;
                        line_count++;
                        line_width = (float)(0);
                        line = text + text_len;
                        line_offset += (float)(row_height);
                        glyph_len = (int)(nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len)));
                        continue;
                    }
                    if ((unicode) == ('\r'))
                    {
                        text_len++;
                        glyph_len = (int)(nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len)));
                        continue;
                    }
                    glyph_width =
                        (float)
                            (font.Width((NkHandle)(font.Userdata), (float)(font.Height), text + text_len,
                                (int)(glyph_len)));
                    line_width += (float)(glyph_width);
                    text_len += (int)(glyph_len);
                    glyph_len = (int)(nk_utf_decode(text + text_len, &unicode, (int)(byte_len - text_len)));
                    continue;
                }
                if ((line_width) > (0))
                {
                    NkRect label = new NkRect();
                    label.y = (float)(pos_y + line_offset);
                    label.h = (float)(row_height);
                    label.w = (float)(line_width);
                    label.x = (float)(pos_x);
                    if (line_count == 0) label.x += (float)(x_offset);
                    if ((is_selected) != 0)
                        nk_fill_rect(_out_, (NkRect)(label), (float)(0), (NkColor)(background));
                    nk_widget_text(_out_, (NkRect)(label), line, (int)((text + text_len) - line), &txt,
                        (Alignment.MIDDLELEFT), font);
                }
            }

        }

        public static void nk_draw_property(NkCommandBuffer _out_, nk_style_property style, NkRect* bounds,
            NkRect* label, NkWidgetStates state, char* name, int len, NkUserFont font)
        {
            nk_text text = new nk_text();
            NkStyleItem background;
            if ((state & NkWidgetStates.ACTIVED) != 0)
            {
                background = style.active;
                text.text = (NkColor)(style.label_active);
            }
            else if ((state & NkWidgetStates.HOVER) != 0)
            {
                background = style.hover;
                text.text = (NkColor)(style.label_hover);
            }
            else
            {
                background = style.normal;
                text.text = (NkColor)(style.label_normal);
            }

            if ((background.Type) == (NkStyleItemType.IMAGE))
            {
                nk_draw_image(_out_, (NkRect)(*bounds), background.Data.Image, (NkColor)(nk_white));
                text.background = (NkColor)(nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
            }
            else
            {
                text.background = (NkColor)(background.Data.Color);
                nk_fill_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (NkColor)(background.Data.Color));
                nk_stroke_rect(_out_, (NkRect)(*bounds), (float)(style.rounding), (float)(style.border),
                    (NkColor)(background.Data.Color));
            }

            text.padding = (NkVec2)(nk_vec2_((float)(0), (float)(0)));
            nk_widget_text(_out_, (NkRect)(*label), name, (int)(len), &text, (Alignment.MIDDLECENTERED), font);
        }

        public static void nk_draw_color_picker(NkCommandBuffer o, NkRect* matrix, NkRect* hue_bar,
            NkRect* alpha_bar, NkColorF col)
        {
            NkColor black = (NkColor)(nk_black);
            NkColor white = (NkColor)(nk_white);
            NkColor black_trans = new NkColor();
            float crosshair_size = (float)(7.0f);
            NkColor temp = new NkColor();
            float* hsva = stackalloc float[4];
            float line_y;
            int i;
            nk_colorf_hsva_fv(hsva, (NkColorF)(col));
            for (i = (int)(0); (i) < (6); ++i)
            {
                nk_fill_rect_multi_color(o,
                    (NkRect)
                        (nk_rect_((float)(hue_bar->x), (float)(hue_bar->y + (float)(i) * (hue_bar->h / 6.0f) + 0.5f),
                            (float)(hue_bar->w), (float)((hue_bar->h / 6.0f) + 0.5f))), (NkColor)(hue_colors[i]),
                    (NkColor)(hue_colors[i]), (NkColor)(hue_colors[i + 1]), (NkColor)(hue_colors[i + 1]));
            }
            line_y = ((float)((int)(hue_bar->y + hsva[0] * matrix->h + 0.5f)));
            nk_stroke_line(o, (float)(hue_bar->x - 1), (float)(line_y), (float)(hue_bar->x + hue_bar->w + 2),
                (float)(line_y), (float)(1), (NkColor)(nk_rgb((int)(255), (int)(255), (int)(255))));
            if ((alpha_bar) != null)
            {
                float alpha =
                    (float)((0) < ((1.0f) < (col.a) ? (1.0f) : (col.a)) ? ((1.0f) < (col.a) ? (1.0f) : (col.a)) : (0));
                line_y = ((float)((int)(alpha_bar->y + (1.0f - alpha) * matrix->h + 0.5f)));
                nk_fill_rect_multi_color(o, (NkRect)(*alpha_bar), (NkColor)(white), (NkColor)(white),
                    (NkColor)(black), (NkColor)(black));
                nk_stroke_line(o, (float)(alpha_bar->x - 1), (float)(line_y),
                    (float)(alpha_bar->x + alpha_bar->w + 2), (float)(line_y), (float)(1),
                    (NkColor)(nk_rgb((int)(255), (int)(255), (int)(255))));
            }

            temp = (NkColor)(nk_hsv_f((float)(hsva[0]), (float)(1.0f), (float)(1.0f)));
            nk_fill_rect_multi_color(o, (NkRect)(*matrix), (NkColor)(white), (NkColor)(temp), (NkColor)(temp),
                (NkColor)(white));
            nk_fill_rect_multi_color(o, (NkRect)(*matrix), (NkColor)(black_trans), (NkColor)(black_trans),
                (NkColor)(black), (NkColor)(black));
            {
                NkVec2 p = new NkVec2();
                float S = (float)(hsva[1]);
                float V = (float)(hsva[2]);
                p.x = ((float)((int)(matrix->x + S * matrix->w)));
                p.y = ((float)((int)(matrix->y + (1.0f - V) * matrix->h)));
                nk_stroke_line(o, (float)(p.x - crosshair_size), (float)(p.y), (float)(p.x - 2), (float)(p.y),
                    (float)(1.0f), (NkColor)(white));
                nk_stroke_line(o, (float)(p.x + crosshair_size + 1), (float)(p.y), (float)(p.x + 3), (float)(p.y),
                    (float)(1.0f), (NkColor)(white));
                nk_stroke_line(o, (float)(p.x), (float)(p.y + crosshair_size + 1), (float)(p.x), (float)(p.y + 3),
                    (float)(1.0f), (NkColor)(white));
                nk_stroke_line(o, (float)(p.x), (float)(p.y - crosshair_size), (float)(p.x), (float)(p.y - 2),
                    (float)(1.0f), (NkColor)(white));
            }

        }

        public static void nk_push_table(NkWindow win, nk_table tbl)
        {
            if (win.Tables == null)
            {
                win.Tables = tbl;
                tbl.next = null;
                tbl.prev = null;
                tbl.size = (uint)(0);
                win.TableCount = (uint)(1);
                return;
            }

            win.Tables.prev = tbl;
            tbl.next = win.Tables;
            tbl.prev = null;
            tbl.size = (uint)(0);
            win.Tables = tbl;
            win.TableCount++;
        }

        public static void nk_remove_table(NkWindow win, nk_table tbl)
        {
            if ((win.Tables) == (tbl)) win.Tables = tbl.next;
            if ((tbl.next) != null) tbl.next.prev = tbl.prev;
            if ((tbl.prev) != null) tbl.prev.next = tbl.next;
            tbl.next = null;
            tbl.prev = null;
        }

        public static uint* nk_find_value(NkWindow win, uint name)
        {
            nk_table iter = win.Tables;
            while ((iter) != null)
            {
                uint i = (uint)(0);
                uint size = (uint)(iter.size);
                for (i = (uint)(0); (i) < (size); ++i)
                {
                    if ((iter.keys[i]) == (name))
                    {
                        iter.seq = (uint)(win.Seq);
                        return (uint*)iter.values + i;
                    }
                }
                size = (uint)(51);
                iter = iter.next;
            }
            return null;
        }

    }
}