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

        public unsafe class NkFont
        {
            public NkFont Next;
            public NkUserFont Handle = new NkUserFont();
            public nk_baked_font Info = new nk_baked_font();
            public float Scale;
            public nk_font_glyph* Glyphs;
            public nk_font_glyph* Fallback;
            public char FallbackCodepoint;
            public NkHandle Texture = new NkHandle();
            public nk_font_config Config;

            public float text_width(NkHandle h, float height, char* s, int length)
            {
                char unicode;
                int textLen;
                float textWidth = 0;

                if (s == null || length == 0) return 0;
                var scale = height / Info.height;
                var glyphLen = textLen = Nk.nk_utf_decode(s, &unicode, length);
                if (glyphLen == 0) return 0;
                while (textLen <= length && glyphLen != 0)
                {
                    if (unicode == 0xFFFD) break;
                    var g = Nk.nk_font_find_glyph(this, unicode);
                    textWidth += g->xadvance * scale;
                    glyphLen = Nk.nk_utf_decode(s + textLen, &unicode, length - textLen);
                    textLen += glyphLen;
                }
                return textWidth;
            }

            public void query_font_glyph(NkHandle h, float height, NkUserFontGlyph* glyph, char codepoint,
                char nextCodepoint)
            {
                Nk.nk_font_query_font_glyph(this, height, glyph, codepoint, nextCodepoint);
            }
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

        public class NkContext
        {
            public nk_input Input = new nk_input();
            public NkStyle Style = new NkStyle();
            public NkClipboard Clip = new NkClipboard();
            public NkWidgetStates LastWidgetState;
            public NkButtonBehavior ButtonBehavior;
            public nk_configuration_stacks Stacks = new nk_configuration_stacks();
            public float DeltaTimeSeconds;
            public NkDrawList DrawList = new NkDrawList();
            public NkHandle Userdata = new NkHandle();
            public nk_text_edit TextEdit = new nk_text_edit();
            public NkCommandBuffer Overlay = new NkCommandBuffer();
            public int Build;
            public NkWindow Begin;
            public NkWindow End;
            public NkWindow Active;
            public NkWindow Current;
            public uint Count;
            public uint Seq;
        }

        public class NkPanel
        {
            public NkPanelType Type;
            public PanelFlags Flags;
            public NkRect Bounds = new NkRect();
            public nk_scroll Offset;
            public float AtX;
            public float AtY;
            public float MaxX;
            public float FooterHeight;
            public float HeaderHeight;
            public float Border;
            public uint HasScrolling;
            public NkRect Clip = new NkRect();
            public nk_menu_state Menu = new nk_menu_state();
            public nk_row_layout Row = new nk_row_layout();
            public NkChart Chart = new NkChart();
            public NkCommandBuffer Buffer;
            public NkPanel Parent;
        }

        public class NkWindow
        {
            public uint Seq;
            public uint Name;
            public PinnedArray<char> NameString = new PinnedArray<char>(64);
            public PanelFlags Flags;
            public NkRect Bounds = new NkRect();
            public nk_scroll Scrollbar = new nk_scroll();
            public NkCommandBuffer Buffer = new NkCommandBuffer();
            public NkPanel Layout;
            public float ScrollbarHidingTimer;
            public nk_property_state Property = new nk_property_state();
            public nk_popup_state Popup = new nk_popup_state();
            public nk_edit_state Edit = new nk_edit_state();
            public uint Scrolled;
            public nk_table Tables;
            public uint TableCount;
            public NkWindow Next;
            public NkWindow Prev;
            public NkWindow Parent;
        }

        public class NkDrawList
        {
            public NkRect ClipRect;
            public readonly NkVec2[] CircleVtx = new NkVec2[12];
            public NkConvertConfig Config;
            public readonly NkBuffer<NkVec2> Points = new NkBuffer<NkVec2>();
            public NkBuffer<nk_draw_command> Buffer;
            public NkBuffer<byte> Vertices;
            public readonly NkBuffer<NkVec2> Normals = new NkBuffer<NkVec2>();
            public NkBuffer<ushort> Elements;
            public bool LineAa;
            public bool ShapeAa;
            public NkHandle Userdata;

            public int VertexOffset
            {
                get { return Vertices.Count / (int)Config.VertexSize; }
            }

            public int AddElements(int size)
            {
                int result = Elements.Count;

                Elements.AddToEnd(size);

                Buffer.Data[Buffer.Count - 1].elem_count += (uint)size;

                return result;
            }
        }

        public class NkStyleItemData
        {
            public NkImage Image;
            public NkColor Color;
        }

        public class NkStyleItem
        {
            public NkStyleItemType Type;
            public NkStyleItemData Data = new NkStyleItemData();
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
            public int CursorVisible;
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

        public class NkCommandBuffer
        {
            public NkCommandBase First;
            public NkCommandBase Last;
            public int Count;

            public NkRect Clip;
            public bool UseClipping;
            public NkHandle Userdata = new NkHandle();
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
            if (ctx.Build == 0)
            {
                nk_build(ctx);
                ctx.Build = nk_true;
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
            if (ctx.Style.CursorActive != null && ctx.Input.mouse.Grabbed == 0 && ctx.Style.CursorVisible != 0)
            {
                var mouseBounds = new NkRect();
                var cursor = ctx.Style.CursorActive;
                nk_command_buffer_init(ctx.Overlay, false);
                nk_start_buffer(ctx, ctx.Overlay);
                mouseBounds.x = ctx.Input.mouse.Pos.x - cursor.offset.x;
                mouseBounds.y = ctx.Input.mouse.Pos.y - cursor.offset.y;
                mouseBounds.w = cursor.size.x;
                mouseBounds.h = cursor.size.y;
                nk_draw_image(ctx.Overlay, mouseBounds, cursor.img, nk_white);
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
            ctx.Build = 0;
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
                nk_remove_table(win, it);
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

        public static void nk_property_(NkContext ctx, char* name, NkPropertyVariant* variant, float incPerPixel,
            NkPropertyFilter filter)
        {
            var bounds = new NkRect();
            uint hash;
            string dummyBuffer = null;
            NkPropertyStatus dummyState = NkPropertyStatus.NK_PROPERTY_DEFAULT;
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

            var _in_ = s == NkWidgetLayoutStates.NK_WIDGET_ROM && win.Property.active == 0 || (layout.Flags & PanelFlags.ROM) != 0
                ? null
                : ctx.Input;

            NkPropertyStatus oldState, state;
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
            if (_in_ != null && state != NkPropertyStatus.NK_PROPERTY_DEFAULT && win.Property.active == 0)
            {
                win.Property.active = 1;
                win.Property.buffer = buffer;
                win.Property.cursor = cursor;
                win.Property.state = state;
                win.Property.name = hash;
                win.Property.select_start = selectBegin;
                win.Property.select_end = selectEnd;
                if (state == NkPropertyStatus.NK_PROPERTY_DRAG)
                {
                    ctx.Input.mouse.Grab = nk_true;
                    ctx.Input.mouse.Grabbed = nk_true;
                }
            }

            if (state == NkPropertyStatus.NK_PROPERTY_DEFAULT && oldState != NkPropertyStatus.NK_PROPERTY_DEFAULT)
            {
                if (oldState == NkPropertyStatus.NK_PROPERTY_DRAG)
                {
                    ctx.Input.mouse.Grab = nk_false;
                    ctx.Input.mouse.Grabbed = nk_false;
                    ctx.Input.mouse.Ungrab = nk_true;
                }
                win.Property.select_start = 0;
                win.Property.select_end = 0;
                win.Property.active = 0;
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
                p = src.p
            };
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


        public static uint* nk_font_default_glyph_ranges()
        {
            return default_ranges;
        }

        public static uint* nk_font_chinese_glyph_ranges()
        {
            return chinese_ranges;
        }

        public static uint* nk_font_cyrillic_glyph_ranges()
        {
            return cyrillic_ranges;
        }

        public static uint* nk_font_korean_glyph_ranges()
        {
            return korean_ranges;
        }

        public static NkVec2 nk_rect_pos(NkRect r)
        {
            NkVec2 ret = new NkVec2
            {
                x = r.x,
                y = r.y
            };
            return ret;
        }
    }
}