using System;
using System.Runtime.InteropServices;

namespace KlearUI
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





    public class NkStyleItemData
    {
        public NkImage Image;
        public NkColor Color;
    }

    public class NkStyleItem
    {
        public StyleItemKind Type;
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
        public NkCursor[] Cursors = new NkCursor[(int)CursorKind.COUNT];
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
        public PropertyKind kind;
        public NkProperty value;
        public NkProperty min_value;
        public NkProperty max_value;
        public NkProperty step;
    }

    public class NkStyle
    {
        public NkUserFont Font;
        public NkCursor[] Cursors = new NkCursor[(int)CursorKind.COUNT];
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

        public static string nk_style_get_color_by_name(int c)
        {
            return Nk.nk_color_names[c];
        }
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

    

    public class NkPopupBuffer
    {
        public NkCommandBuffer OldBuffer;
        public readonly NkCommandBuffer Buffer = new NkCommandBuffer();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NkConfigStackButtonBehaviorElement
    {
        public ButtonBehavior old_value;
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
                nk_textedit_text(state, p, totalLen);
            }
        }

        public static nk_table nk_create_table(NkContext ctx)
        {
            var result = new nk_table();

            return result;
        }

        public static NkPanel nk_create_panel(NkContext ctx)
        {
            var result = new NkPanel();

            return result;
        }

        public static void nk_free_panel(NkContext ctx, NkPanel panel)
        {
        }

        public static int nk_popup_begin(NkContext ctx, PopupKind type, string title, PanelFlags flags, NkRect rect)
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
            var cmd = (NkCommandPolygon)b.nk_command_buffer_push(CommandType.Polygon);
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
            var cmd = (NkCommandPolygonFilled)b.nk_command_buffer_push(CommandType.PolygonFilled);
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
            var cmd = (NkCommandPolyline)b.nk_command_buffer_push(CommandType.Polyline);
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