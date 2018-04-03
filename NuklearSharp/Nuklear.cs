using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{


    [StructLayout(LayoutKind.Explicit)]
        public unsafe struct NkHandle
        {
            [FieldOffset(0)] public void* ptr;

            [FieldOffset(0)] public int id;
        }

        public class NkUserFont
        {
            public NkHandle Userdata;
            public float Height;
            public NkHandle Texture;

            public NkTextWidthDelegate Width;
            public NkQueryFontGlyphDelegate Query;
        }

    public class NkClipboard
        {
            public NkHandle Userdata;
            public NkPluginPaste Paste;
            public NkPluginCopy Copy;
        }

        public class NkKeyboard
        {
            public PinnedArray<nk_key> Keys = new PinnedArray<nk_key>(new nk_key[(int)NkKeys.MAX]);
            public PinnedArray<char> Text = new PinnedArray<char>(new char[16]);
            public int TextLen;
        }

        public class NkMouse
        {
            public PinnedArray<nk_mouse_button> Buttons = new PinnedArray<nk_mouse_button>(new nk_mouse_button[(int)NkButtons.MAX]);
            public NkVec2 Pos;
            public NkVec2 Prev;
            public NkVec2 Delta;
            public NkVec2 ScrollDelta;
            public byte Grab;
            public byte Grabbed;
            public byte Ungrab;
        }

        

        public class NkStyleItemData
        {
            public NkImage Image;
            public NkColor Color;
        }

        public class NkStyleItem
        {
            public NkStyleItemType Type;
            public NkStyleItemData Data= new NkStyleItemData();
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NkRpContext
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

        public unsafe class NkFontAtlas
        {
            public void* Pixel;
            public int TexWidth;
            public int TexHeight;
            public NkRectI Custom;
            public NkCursor[] Cursors = new NkCursor[(int)NkStyleCursor.COUNT];
            public int GlyphCount;
            public nk_font_glyph* Glyphs;
            public NkFont DefaultFont;
            public NkFont Fonts;
            public nk_font_config Config;
            public int FontNum;
            public NkFontAtlas()
            {
                for (var i = 0; i < Cursors.Length; ++i)
                {
                    Cursors[i] = new NkCursor();
                }
            }
        }

        public class NkTextUndoState
        {
            public PinnedArray<nk_text_undo_record> UndoRec = new PinnedArray<nk_text_undo_record>(new nk_text_undo_record[99]);
            public PinnedArray<uint> UndoChar = new PinnedArray<uint>(new uint[999]);
            public short UndoPoint;
            public short RedoPoint;
            public short UndoCharPoint;
            public short RedoCharPoint;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct NkProperty
        {
            [FieldOffset(0)] public int i;

            [FieldOffset(0)] public float f;

            [FieldOffset(0)] public double d;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NkPropertyVariant
        {
            public NkPropertyKind kind;
            public NkProperty value;
            public NkProperty min_value;
            public NkProperty max_value;
            public NkProperty step;
        }

        public class NkStyle
        {
            public NkUserFont Font;
            public NkCursor[] Cursors = new NkCursor[(int)NkStyleCursor.COUNT];
            public NkCursor CursorActive;
            public NkCursor CursorLast;
            public bool CursorVisible;
            public nk_style_text Text = new nk_style_text();
            public nk_style_button Button = new nk_style_button();
            public nk_style_button ContextualButton = new nk_style_button();
            public nk_style_button MenuButton = new nk_style_button();
            public nk_style_toggle Option = new nk_style_toggle();
            public nk_style_toggle Checkbox = new nk_style_toggle();
            public nk_style_selectable Selectable = new nk_style_selectable();
            public nk_style_slider Slider = new nk_style_slider();
            public nk_style_progress Progress = new nk_style_progress();
            public nk_style_property Property = new nk_style_property();
            public nk_style_edit Edit = new nk_style_edit();
            public nk_style_chart Chart = new nk_style_chart();
            public nk_style_scrollbar Scrollh = new nk_style_scrollbar();
            public nk_style_scrollbar Scrollv = new nk_style_scrollbar();
            public nk_style_tab Tab = new nk_style_tab();
            public nk_style_combo Combo = new nk_style_combo();
            public nk_style_window Window = new nk_style_window();
        }

        public class NkChart
        {
            public int Slot;
            public float X;
            public float Y;
            public float W;
            public float H;
            public nk_chart_slot[] Slots = new nk_chart_slot[4];
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct NkInvSqrtUnion
        {
            [FieldOffset(0)] public uint i;

            [FieldOffset(0)] public float f;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal unsafe struct NkMurmurHashUnion
        {
            [FieldOffset(0)] public uint* i;

            [FieldOffset(0)] public byte* b;

            public NkMurmurHashUnion(void* ptr)
            {
                i = (uint*)ptr;
                b = (byte*)ptr;
            }
        }

        public class NkCommandBase
        {
            public nk_command Header;
            public NkHandle Userdata;
            public NkCommandBase Next;
        }

        public class NkCommandScissor : NkCommandBase
        {
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
        }

        public class NkCommandLine : NkCommandBase
        {
            public ushort LineThickness;
            public NkPoint Begin = new NkPoint();
            public NkPoint End = new NkPoint();
            public NkColor Color = new NkColor();
        }

        public class NkCommandCurve : NkCommandBase
        {
            public ushort LineThickness;
            public NkPoint Begin = new NkPoint();
            public NkPoint End = new NkPoint();
            public NkPoint Ctrl0 = new NkPoint();
            public NkPoint Ctrl1 = new NkPoint();
            public NkColor Color = new NkColor();
        }

        public class NkCommandRect : NkCommandBase
        {
            public ushort Rounding;
            public ushort LineThickness;
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public NkColor Color = new NkColor();
        }

        public class NkCommandRectFilled : NkCommandBase
        {
            public ushort Rounding;
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public NkColor Color = new NkColor();
        }

        public class NkCommandRectMultiColor : NkCommandBase
        {
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public NkColor Left = new NkColor();
            public NkColor Top = new NkColor();
            public NkColor Bottom = new NkColor();
            public NkColor Right = new NkColor();
        }

        public class NkCommandTriangle : NkCommandBase
        {
            public ushort LineThickness;
            public NkPoint A = new NkPoint();
            public NkPoint B = new NkPoint();
            public NkPoint C = new NkPoint();
            public NkColor Color = new NkColor();
        }

        public class NkCommandTriangleFilled : NkCommandBase
        {
            public NkPoint A = new NkPoint();
            public NkPoint B = new NkPoint();
            public NkPoint C = new NkPoint();
            public NkColor Color = new NkColor();
        }

        public class NkCommandCircle : NkCommandBase
        {
            public short X;
            public short Y;
            public ushort LineThickness;
            public ushort W;
            public ushort H;
            public NkColor Color = new NkColor();
        }

        public class NkCommandCircleFilled : NkCommandBase
        {
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public NkColor Color = new NkColor();
        }

        public class NkCommandArc : NkCommandBase
        {
            public short Cx;
            public short Cy;
            public ushort R;
            public ushort LineThickness;
            public PinnedArray<float> A = new PinnedArray<float>(2);
            public NkColor Color = new NkColor();
        }

        public class NkCommandArcFilled : NkCommandBase
        {
            public short Cx;
            public short Cy;
            public ushort R;
            public PinnedArray<float> A = new PinnedArray<float>(2);
            public NkColor Color = new NkColor();
        }

        public class NkCommandPolygon : NkCommandBase
        {
            public NkColor Color;
            public ushort LineThickness;
            public ushort PointCount;
            public NkPoint[] Points;
        }

        public class NkCommandPolygonFilled : NkCommandBase
        {
            public NkColor Color;
            public ushort PointCount;
            public NkPoint[] Points;
        }

        public class NkCommandPolyline : NkCommandBase
        {
            public NkColor Color;
            public ushort LineThickness;
            public ushort PointCount;
            public NkPoint[] Points;
        }

        public class NkCommandImage : NkCommandBase
        {
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public NkImage Img = new NkImage();
            public NkColor Col = new NkColor();
        }

        public unsafe class NkCommandText : NkCommandBase
        {
            public NkUserFont Font;
            public NkColor Background;
            public NkColor Foreground;
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public float Height;
            public char* String;
            public int Length;
        }

        public class NkCommandCustom : NkCommandBase
        {
            public short X;
            public short Y;
            public ushort W;
            public ushort H;
            public NkHandle CallbackData;
            public NkCommandCustomCallback Callback;
        }

        public class NkPopupBuffer
        {
            public NkCommandBuffer OldBuffer;
            public readonly NkCommandBuffer Buffer = new NkCommandBuffer();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct NkConfigStackButtonBehaviorElement
        {
            public NkButtonBehavior old_value;
        }

        public class NkConvertConfig
        {
            public float GlobalAlpha;
            public bool LineAa;
            public bool ShapeAa;
            public uint CircleSegmentCount;
            public uint ArcSegmentCount;
            public uint CurveSegmentCount;
            public nk_draw_null_texture Null = new nk_draw_null_texture();
            public nk_draw_vertex_layout_element[] VertexLayout;
            public ulong VertexSize;
            public ulong VertexAlignment;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct NkUserFontGlyph
        {
            public fixed float uv_x[2];
            public fixed float uv_y[2];
            public NkVec2 offset;
            public float width;
            public float height;
            public float xadvance;
        }

    public static unsafe partial class Nk
    {

        private static readonly Func<object>[] CommandCreators =
        {
            null,
            () => nk_create_command<NkCommandScissor>(),
            () => nk_create_command<NkCommandLine>(),
            () => nk_create_command<NkCommandCurve>(),
            () => nk_create_command<NkCommandRect>(),
            () => nk_create_command<NkCommandRectFilled>(),
            () => nk_create_command<NkCommandRectMultiColor>(),
            () => nk_create_command<NkCommandCircle>(),
            () => nk_create_command<NkCommandCircleFilled>(),
            () => nk_create_command<NkCommandArc>(),
            () => nk_create_command<NkCommandArcFilled>(),
            () => nk_create_command<NkCommandTriangle>(),
            () => nk_create_command<NkCommandTriangleFilled>(),
            () => nk_create_command<NkCommandPolygon>(),
            () => nk_create_command<NkCommandPolygonFilled>(),
            () => nk_create_command<NkCommandPolyline>(),
            () => nk_create_command<NkCommandText>(),
            () => nk_create_command<NkCommandImage>(),
            () => nk_create_command<NkCommandCustom>()
        };

        private static object nk_create_command<T>() where T : new()
        {
            return new T();
        }

        public static void nk_command_buffer_init(NkCommandBuffer cmdbuf, bool clip)
        {
            cmdbuf.UseClipping = clip;
            cmdbuf.First = cmdbuf.Last = null;
            cmdbuf.Count = 0;
        }

        public static void nk_command_buffer_reset(NkCommandBuffer cmdbuf)
        {
            if (cmdbuf == null) return;
            cmdbuf.First = cmdbuf.Last = null;
            cmdbuf.Count = 0;
            cmdbuf.Clip = nk_null_rect;
        }

        public static NkCommandBase nk_command_buffer_push(NkCommandBuffer b, NkCommandType t)
        {
            if (b == null || t < 0 || (int)t >= CommandCreators.Length || CommandCreators[(int)t] == null) return null;

            var creator = CommandCreators[(int)t];

            var command = (NkCommandBase)creator();

            command.Header = new nk_command
            {
                type = t
            };

            if (b.Last == null)
            {
                b.First = command;
                b.Last = command;
            }
            else
            {
                b.Last.Next = command;
                b.Last = command;
            }

            ++b.Count;

            return command;
        }

        public static NkWindow nk__begin(NkContext ctx)
        {
            if (ctx == null || ctx.Count == 0) return null;
            if (ctx.Build == false)
            {
                nk_build(ctx);
                ctx.Build = true;
            }

            var iter = ctx.Begin;
            while (iter != null &&
                   (iter.Buffer.Count == 0 || (iter.Flags & PanelFlags.HIDDEN) != 0 || iter.Seq != ctx.Seq))
            {
                iter = iter.Next;
            }

            return iter;
        }


        public static void nk_build(NkContext ctx)
        {
            if (ctx.Style.CursorActive == null) ctx.Style.CursorActive = ctx.Style.Cursors[(int)NkStyleCursor.ARROW];
            if (ctx.Style.CursorActive != null && ctx.Input.mouse.Grabbed == 0 && ctx.Style.CursorVisible)
            {
                var mouseBounds = new NkRect();
                var cursor = ctx.Style.CursorActive;
                nk_command_buffer_init(ctx.Overlay, false);
                nk_start_buffer(ctx, ctx.Overlay);
                mouseBounds.x = ctx.Input.mouse.Pos.x - cursor.offset.x;
                mouseBounds.y = ctx.Input.mouse.Pos.y - cursor.offset.y;
                mouseBounds.w = cursor.size.x;
                mouseBounds.h = cursor.size.y;
                ctx.Overlay.nk_draw_image(mouseBounds, cursor.img, nk_white);
            }

            var it = ctx.Begin;
            NkCommandBase cmd = null;
            for (; it != null;)
            {
                var next = it.Next;
                if ((it.Flags & PanelFlags.HIDDEN) != 0 || it.Seq != ctx.Seq)
                    goto cont;
                cmd = it.Buffer.Last;

                while (next != null &&
                       (next.Buffer == null || next.Buffer.Count == 0 || (next.Flags & PanelFlags.HIDDEN) != 0))
                {
                    next = next.Next;
                }

                if (next != null) cmd.Next = next.Buffer.First;
                cont:
                it = next;
            }

            it = ctx.Begin;

            while (it != null)
            {
                var next = it.Next;

                if (it.Popup.buf.Buffer.Count == 0) goto skip;

                var buf = it.Popup.buf.Buffer;
                cmd.Next = buf.First;
                cmd = buf.Last;

                it.Popup.buf.Buffer.Count = 0;

                skip:
                it = next;
            }
            if (cmd != null)
            {
                cmd.Next = ctx.Overlay.Count > 0 ? ctx.Overlay.First : null;
            }
        }

        public static float nk_inv_sqrt(float number)
        {
            var threehalfs = 1.5f;
            var conv = new NkInvSqrtUnion
            {
                i = 0,
                f = number
            };
            var x2 = number * 0.5f;
            conv.i = 0x5f375A84 - (conv.i >> 1);
            conv.f = conv.f * (threehalfs - x2 * conv.f * conv.f);

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

        public static void nk_textedit_text(nk_text_edit state, string text, int totalLen)
        {
            fixed (char* p = text)
            {
                nk_textedit_text(state, text, totalLen);
            }
        }

        public static string nk_style_get_color_by_name(int c)
        {
            return nk_color_names[c];
        }

        public static void nk_free(NkContext ctx)
        {
            if (ctx == null) return;

            ctx.Seq = 0;
            ctx.Build = false;
            ctx.Begin = null;
            ctx.End = null;
            ctx.Active = null;
            ctx.Current = null;
            ctx.Count = 0;
        }

        public static nk_table nk_create_table(NkContext ctx)
        {
            var result = new nk_table();

            return result;
        }

        public static NkWindow nk_create_window(NkContext ctx)
        {
            var result = new NkWindow { Seq = ctx.Seq };

            return result;
        }

        public static void nk_free_window(NkContext ctx, NkWindow win)
        {
            nk_table it = win.Tables;
            if (win.Popup.win != null)
            {
                nk_free_window(ctx, win.Popup.win);
                win.Popup.win = null;
            }

            win.Next = null;
            win.Prev = null;
            while (it != null)
            {
                var n = it.next;
                win.nk_remove_table(it);
                if (it == win.Tables) win.Tables = n;
                it = n;
            }
        }

        public static NkPanel nk_create_panel(NkContext ctx)
        {
            var result = new NkPanel();

            return result;
        }

        public static void nk_free_panel(NkContext ctx, NkPanel panel)
        {
        }

        public static int nk_popup_begin(NkContext ctx, NkPopupType type, string title, PanelFlags flags, NkRect rect)
        {
            fixed (char* ptr = title)
            {
                return nk_popup_begin(ctx, type, ptr, flags, rect);
            }
        }


        public static NkFont nk_font_atlas_add_default(NkFontAtlas atlas, float pixelHeight, nk_font_config config)
        {
            fixed (byte* ptr = nk_proggy_clean_ttf_compressed_data_base85)
            {
                return nk_font_atlas_add_compressed_base85(atlas, ptr, pixelHeight, config);
            }
        }

     

        public static void nk_stroke_polygon(NkCommandBuffer b, float* points, int pointCount, float lineThickness,
            NkColor col)
        {
            if (b == null || col.a == 0 || lineThickness <= 0) return;
            var cmd = (NkCommandPolygon)nk_command_buffer_push(b, NkCommandType.POLYGON);
            if (cmd == null) return;
            cmd.Color = col;
            cmd.LineThickness = (ushort)lineThickness;
            cmd.PointCount = (ushort)pointCount;
            cmd.Points = new NkPoint[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                cmd.Points[i].x = (short)points[i * 2];
                cmd.Points[i].y = (short)points[i * 2 + 1];
            }
        }

        public static void nk_fill_polygon(NkCommandBuffer b, float* points, int pointCount, NkColor col)
        {
            if (b == null || col.a == 0) return;
            var cmd = (NkCommandPolygonFilled)nk_command_buffer_push(b, NkCommandType.POLYGON_FILLED);
            if (cmd == null) return;
            cmd.Color = col;
            cmd.PointCount = (ushort)pointCount;
            cmd.Points = new NkPoint[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                cmd.Points[i].x = (short)points[i * 2 + 0];
                cmd.Points[i].y = (short)points[i * 2 + 1];
            }
        }

        public static void nk_stroke_polyline(NkCommandBuffer b, float* points, int pointCount, float lineThickness,
            NkColor col)
        {
            if (b == null || col.a == 0 || lineThickness <= 0) return;
            var cmd = (NkCommandPolyline)nk_command_buffer_push(b, NkCommandType.POLYLINE);
            if (cmd == null) return;
            cmd.Color = col;
            cmd.PointCount = (ushort)pointCount;
            cmd.LineThickness = (ushort)lineThickness;
            cmd.Points = new NkPoint[pointCount];
            for (var i = 0; i < pointCount; ++i)
            {
                cmd.Points[i].x = (short)points[i * 2];
                cmd.Points[i].y = (short)points[i * 2 + 1];
            }
        }

        public static int nk_strlen(byte* str)
        {
            int siz = 0;
            while (str != null && *str++ != '\0')
            {
                siz++;
            }
            return siz;
        }


        public static uint nk_murmur_hash(void* key, int len, uint seed)
        {
            NkMurmurHashUnion conv = new NkMurmurHashUnion(null);
            byte* data = (byte*)(key);
            int nblocks = (int)(len / 4);
            uint h1 = (uint)(seed);
            uint c1 = (uint)(0xcc9e2d51);
            uint c2 = (uint)(0x1b873593);
            byte* tail;
            uint* blocks;
            uint k1;
            int i;
            if (key == null) return (uint)(0);
            conv.b = (data + nblocks * 4);
            blocks = conv.i;
            for (i = (int)(-nblocks); i != 0; ++i)
            {
                k1 = (uint)(blocks[i]);
                k1 *= (uint)(c1);
                k1 = (uint)((k1) << (15) | ((k1) >> (32 - 15)));
                k1 *= (uint)(c2);
                h1 ^= (uint)(k1);
                h1 = (uint)((h1) << (13) | ((h1) >> (32 - 13)));
                h1 = (uint)(h1 * 5 + 0xe6546b64);
            }
            tail = (data + nblocks * 4);
            k1 = (uint)(0);
            int l = (int)(len & 3);
            switch (l)
            {
                case 1:
                case 2:
                case 3:
                    if ((l) == (2))
                    {
                        k1 ^= ((uint)(tail[1] << 8));
                    }
                    else if ((l) == (3))
                    {
                        k1 ^= ((uint)(tail[2] << 16));
                    }
                    k1 ^= (uint)(tail[0]);
                    k1 *= (uint)(c1);
                    k1 = (uint)((k1) << (15) | ((k1) >> (32 - 15)));
                    k1 *= (uint)(c2);
                    h1 ^= (uint)(k1);
                    break;
                default:
                    break;
            }

            h1 ^= ((uint)(len));
            h1 ^= (uint)(h1 >> 16);
            h1 *= (uint)(0x85ebca6b);
            h1 ^= (uint)(h1 >> 13);
            h1 *= (uint)(0xc2b2ae35);
            h1 ^= (uint)(h1 >> 16);
            return (uint)(h1);
        }
    }
}