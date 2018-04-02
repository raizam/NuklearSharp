using System.Runtime.InteropServices;

namespace NuklearSharp
{

    [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct NkColor
        {
            public byte r;
            public byte g;
            public byte b;
            public byte a;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct NkColorF
        {
            public float r;
            public float g;
            public float b;
            public float a;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct NkVec2
        {
            public float x;
            public float y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct NkPoint
        {
            public short x;
            public short y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct NkRect
        {
            public float x;
            public float y;
            public float w;
            public float h;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct NkRectI
        {
            public short x;
            public short y;
            public short w;
            public short h;
        }

        public unsafe partial class NkImage
        {
            public NkHandle handle = new NkHandle();
            public ushort w;
            public ushort h;
            public PinnedArray<ushort> region = new PinnedArray<ushort>(4);
        }

        public unsafe partial class NkCursor
        {
            public NkImage img = new NkImage();
            public NkVec2 size = new NkVec2();
            public NkVec2 offset = new NkVec2();
        }

        public unsafe partial class nk_list_view
        {
            public int begin;
            public int end;
            public int count;
            public int total_height;
            public NkContext ctx;
            public uint* scroll_pointer;
            public uint scroll_value;
        }

        public unsafe partial class nk_chart_slot
        {
            public int type;
            public NkColor color = new NkColor();
            public NkColor highlight = new NkColor();
            public float min;
            public float max;
            public float range;
            public int count;
            public NkVec2 last = new NkVec2();
            public int index;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct nk_text
        {
            public NkVec2 padding;
            public NkColor background;
            public NkColor text;
        }
    public unsafe static partial class Nk
    {
        public static NkRect nk_recta(NkVec2 pos, NkVec2 size)
        {
            return (NkRect)(nk_rect_((float)(pos.x), (float)(pos.y), (float)(size.x), (float)(size.y)));
        }

        public static NkVec2 nk_rect_size(NkRect r)
        {
            NkVec2 ret = new NkVec2();
            ret.x = (float)(r.w);
            ret.y = (float)(r.h);
            return (NkVec2)(ret);
        }

        public static NkRect nk_shrink_rect_(NkRect r, float amount)
        {
            NkRect res = new NkRect();
            r.w = (float)((r.w) < (2 * amount) ? (2 * amount) : (r.w));
            r.h = (float)((r.h) < (2 * amount) ? (2 * amount) : (r.h));
            res.x = (float)(r.x + amount);
            res.y = (float)(r.y + amount);
            res.w = (float)(r.w - 2 * amount);
            res.h = (float)(r.h - 2 * amount);
            return (NkRect)(res);
        }

        public static NkRect nk_pad_rect(NkRect r, NkVec2 pad)
        {
            r.w = (float)((r.w) < (2 * pad.x) ? (2 * pad.x) : (r.w));
            r.h = (float)((r.h) < (2 * pad.y) ? (2 * pad.y) : (r.h));
            r.x += (float)(pad.x);
            r.y += (float)(pad.y);
            r.w -= (float)(2 * pad.x);
            r.h -= (float)(2 * pad.y);
            return (NkRect)(r);
        }

        public static NkColor nk_rgba_cf(NkColorF c)
        {
            return (NkColor)(nk_rgba_f((float)(c.r), (float)(c.g), (float)(c.b), (float)(c.a)));
        }

        public static NkColor nk_rgb_cf(NkColorF c)
        {
            return (NkColor)(nk_rgb_f((float)(c.r), (float)(c.g), (float)(c.b)));
        }

        public static uint nk_color_u32(NkColor _in_)
        {
            uint _out_ = (uint)(_in_.r);
            _out_ |= (uint)((uint)(_in_.g) << 8);
            _out_ |= (uint)((uint)(_in_.b) << 16);
            _out_ |= (uint)((uint)(_in_.a) << 24);
            return (uint)(_out_);
        }

        public static NkColorF nk_color_cf(NkColor _in_)
        {
            NkColorF o = new NkColorF();
            nk_color_f(&o.r, &o.g, &o.b, &o.a, (NkColor)(_in_));
            return (NkColorF)(o);
        }

        public static NkImage nk_subimage_handle(NkHandle handle, ushort w, ushort h, NkRect r)
        {
            NkImage s = new NkImage();

            s.handle = (NkHandle)(handle);
            s.w = (ushort)(w);
            s.h = (ushort)(h);
            s.region[0] = ((ushort)(r.x));
            s.region[1] = ((ushort)(r.y));
            s.region[2] = ((ushort)(r.w));
            s.region[3] = ((ushort)(r.h));
            return (NkImage)(s);
        }

        public static NkImage nk_image_handle(NkHandle handle)
        {
            NkImage s = new NkImage();

            s.handle = (NkHandle)(handle);
            s.w = (ushort)(0);
            s.h = (ushort)(0);
            s.region[0] = (ushort)(0);
            s.region[1] = (ushort)(0);
            s.region[2] = (ushort)(0);
            s.region[3] = (ushort)(0);
            return (NkImage)(s);
        }

        public static int nk_image_is_subimage(NkImage img)
        {
            return (int)((((img.w) == (0)) && ((img.h) == (0))) ? 1 : 0);
        }

        public static void nk_unify(ref NkRect clip, ref NkRect a, float x0, float y0, float x1, float y1)
        {
            clip.x = (float)((a.x) < (x0) ? (x0) : (a.x));
            clip.y = (float)((a.y) < (y0) ? (y0) : (a.y));
            clip.w = (float)(((a.x + a.w) < (x1) ? (a.x + a.w) : (x1)) - clip.x);
            clip.h = (float)(((a.y + a.h) < (y1) ? (a.y + a.h) : (y1)) - clip.y);
            clip.w = (float)((0) < (clip.w) ? (clip.w) : (0));
            clip.h = (float)((0) < (clip.h) ? (clip.h) : (0));
        }

        public static void nk_triangle_from_direction(NkVec2* result, NkRect r, float pad_x, float pad_y, int direction)
        {
            float w_half;
            float h_half;
            r.w = (float)((2 * pad_x) < (r.w) ? (r.w) : (2 * pad_x));
            r.h = (float)((2 * pad_y) < (r.h) ? (r.h) : (2 * pad_y));
            r.w = (float)(r.w - 2 * pad_x);
            r.h = (float)(r.h - 2 * pad_y);
            r.x = (float)(r.x + pad_x);
            r.y = (float)(r.y + pad_y);
            w_half = (float)(r.w / 2.0f);
            h_half = (float)(r.h / 2.0f);
            if ((direction) == (NK_UP))
            {
                result[0] = (NkVec2)(nk_vec2_((float)(r.x + w_half), (float)(r.y)));
                result[1] = (NkVec2)(nk_vec2_((float)(r.x + r.w), (float)(r.y + r.h)));
                result[2] = (NkVec2)(nk_vec2_((float)(r.x), (float)(r.y + r.h)));
            }
            else if ((direction) == (NK_RIGHT))
            {
                result[0] = (NkVec2)(nk_vec2_((float)(r.x), (float)(r.y)));
                result[1] = (NkVec2)(nk_vec2_((float)(r.x + r.w), (float)(r.y + h_half)));
                result[2] = (NkVec2)(nk_vec2_((float)(r.x), (float)(r.y + r.h)));
            }
            else if ((direction) == (NK_DOWN))
            {
                result[0] = (NkVec2)(nk_vec2_((float)(r.x), (float)(r.y)));
                result[1] = (NkVec2)(nk_vec2_((float)(r.x + r.w), (float)(r.y)));
                result[2] = (NkVec2)(nk_vec2_((float)(r.x + w_half), (float)(r.y + r.h)));
            }
            else
            {
                result[0] = (NkVec2)(nk_vec2_((float)(r.x), (float)(r.y + h_half)));
                result[1] = (NkVec2)(nk_vec2_((float)(r.x + r.w), (float)(r.y)));
                result[2] = (NkVec2)(nk_vec2_((float)(r.x + r.w), (float)(r.y + r.h)));
            }

        }

        public static void* nk_malloc(NkHandle unused, void* old, ulong size)
        {
            return CRuntime.Malloc((ulong)(size));
        }

        public static void nk_mfree(NkHandle unused, void* ptr)
        {
            CRuntime.Free(ptr);
        }

        public static void nk_font_query_font_glyph(NkFont font, float height, NkUserFontGlyph* glyph, char codepoint,
            char next_codepoint)
        {
            float scale;
            nk_font_glyph* g;


            if ((font == null) || (glyph == null)) return;
            scale = (float)(height / font.Info.height);
            g = nk_font_find_glyph(font, codepoint);
            glyph->width = (float)((g->x1 - g->x0) * scale);
            glyph->height = (float)((g->y1 - g->y0) * scale);
            glyph->offset = (NkVec2)(nk_vec2_((float)(g->x0 * scale), (float)(g->y0 * scale)));
            glyph->xadvance = (float)(g->xadvance * scale);
            glyph->uv_x[0] = g->u0;
            glyph->uv_y[0] = g->v0;
            glyph->uv_x[1] = g->u1;
            glyph->uv_y[1] = g->v1;
        }

        public static NkStyleItem nk_style_item_image(NkImage img)
        {
            NkStyleItem i = new NkStyleItem();
            i.Type = (int)(NK_STYLE_ITEM_IMAGE);
            i.Data.Image = (NkImage)(img);
            return (NkStyleItem)(i);
        }

        public static NkStyleItem nk_style_item_color(NkColor col)
        {
            NkStyleItem i = new NkStyleItem();
            i.Type = (int)(NK_STYLE_ITEM_COLOR);
            i.Data.Color = (NkColor)(col);
            return (NkStyleItem)(i);
        }

        public static void nk_layout_widget_space(NkRect* bounds, NkContext ctx, NkWindow win, int modify)
        {
            NkPanel layout;
            NkStyle style;
            NkVec2 spacing = new NkVec2();
            NkVec2 padding = new NkVec2();
            float item_offset = (float)(0);
            float item_width = (float)(0);
            float item_spacing = (float)(0);
            float panel_space = (float)(0);
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            style = ctx.Style;
            spacing = (NkVec2)(style.Window.spacing);
            padding = (NkVec2)(nk_panel_get_padding(style, (int)(layout.Type)));
            panel_space =
                (float)
                    (nk_layout_row_calculate_usable_space(ctx.Style, (int)(layout.Type), (float)(layout.Bounds.w),
                        (int)(layout.Row.columns)));
            switch (layout.Row.type)
            {
                case NK_LAYOUT_DYNAMIC_FIXED:
                    {
                        item_width = (float)(((1.0f) < (panel_space - 1.0f) ? (panel_space - 1.0f) : (1.0f)) / (float)(layout.Row.columns));
                        item_offset = (float)((float)(layout.Row.index) * item_width);
                        item_spacing = (float)((float)(layout.Row.index) * spacing.x);
                    }
                    break;
                case NK_LAYOUT_DYNAMIC_ROW:
                    {
                        item_width = (float)(layout.Row.item_width * panel_space);
                        item_offset = (float)(layout.Row.item_offset);
                        item_spacing = (float)(0);
                        if ((modify) != 0)
                        {
                            layout.Row.item_offset += (float)(item_width + spacing.x);
                            layout.Row.filled += (float)(layout.Row.item_width);
                            layout.Row.index = (int)(0);
                        }
                    }
                    break;
                case NK_LAYOUT_DYNAMIC_FREE:
                    {
                        bounds->x = (float)(layout.AtX + (layout.Bounds.w * layout.Row.item.x));
                        bounds->x -= ((float)(layout.Offset.x));
                        bounds->y = (float)(layout.AtY + (layout.Row.height * layout.Row.item.y));
                        bounds->y -= ((float)(layout.Offset.y));
                        bounds->w = (float)(layout.Bounds.w * layout.Row.item.w);
                        bounds->h = (float)(layout.Row.height * layout.Row.item.h);
                        return;
                    }
                case NK_LAYOUT_DYNAMIC:
                    {
                        float ratio;
                        ratio =
                            (float)
                                (((layout.Row.ratio[layout.Row.index]) < (0)) ? layout.Row.item_width : layout.Row.ratio[layout.Row.index]);
                        item_spacing = (float)((float)(layout.Row.index) * spacing.x);
                        item_width = (float)(ratio * panel_space);
                        item_offset = (float)(layout.Row.item_offset);
                        if ((modify) != 0)
                        {
                            layout.Row.item_offset += (float)(item_width);
                            layout.Row.filled += (float)(ratio);
                        }
                    }
                    break;
                case NK_LAYOUT_STATIC_FIXED:
                    {
                        item_width = (float)(layout.Row.item_width);
                        item_offset = (float)((float)(layout.Row.index) * item_width);
                        item_spacing = (float)((float)(layout.Row.index) * spacing.x);
                    }
                    break;
                case NK_LAYOUT_STATIC_ROW:
                    {
                        item_width = (float)(layout.Row.item_width);
                        item_offset = (float)(layout.Row.item_offset);
                        item_spacing = (float)((float)(layout.Row.index) * spacing.x);
                        if ((modify) != 0) layout.Row.item_offset += (float)(item_width);
                    }
                    break;
                case NK_LAYOUT_STATIC_FREE:
                    {
                        bounds->x = (float)(layout.AtX + layout.Row.item.x);
                        bounds->w = (float)(layout.Row.item.w);
                        if (((bounds->x + bounds->w) > (layout.MaxX)) && ((modify) != 0)) layout.MaxX = (float)(bounds->x + bounds->w);
                        bounds->x -= ((float)(layout.Offset.x));
                        bounds->y = (float)(layout.AtY + layout.Row.item.y);
                        bounds->y -= ((float)(layout.Offset.y));
                        bounds->h = (float)(layout.Row.item.h);
                        return;
                    }
                case NK_LAYOUT_STATIC:
                    {
                        item_spacing = (float)((float)(layout.Row.index) * spacing.x);
                        item_width = (float)(layout.Row.ratio[layout.Row.index]);
                        item_offset = (float)(layout.Row.item_offset);
                        if ((modify) != 0) layout.Row.item_offset += (float)(item_width);
                    }
                    break;
                case NK_LAYOUT_TEMPLATE:
                    {
                        item_width = (float)(layout.Row.templates[layout.Row.index]);
                        item_offset = (float)(layout.Row.item_offset);
                        item_spacing = (float)((float)(layout.Row.index) * spacing.x);
                        if ((modify) != 0) layout.Row.item_offset += (float)(item_width);
                    }
                    break;
                default:
                    ;
                    break;
            }

            bounds->w = (float)(item_width);
            bounds->h = (float)(layout.Row.height - spacing.y);
            bounds->y = (float)(layout.AtY - (float)(layout.Offset.y));
            bounds->x = (float)(layout.AtX + item_offset + item_spacing + padding.x);
            if (((bounds->x + bounds->w) > (layout.MaxX)) && ((modify) != 0)) layout.MaxX = (float)(bounds->x + bounds->w);
            bounds->x -= ((float)(layout.Offset.x));
        }

        public static void nk_panel_alloc_space(NkRect* bounds, NkContext ctx)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            if ((layout.Row.index) >= (layout.Row.columns)) nk_panel_alloc_row(ctx, win);
            nk_layout_widget_space(bounds, ctx, win, (int)(nk_true));
            layout.Row.index++;
        }

        public static void nk_layout_peek(NkRect* bounds, NkContext ctx)
        {
            float y;
            int index;
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            y = (float)(layout.AtY);
            index = (int)(layout.Row.index);
            if ((layout.Row.index) >= (layout.Row.columns))
            {
                layout.AtY += (float)(layout.Row.height);
                layout.Row.index = (int)(0);
            }

            nk_layout_widget_space(bounds, ctx, win, (int)(nk_false));
            if (layout.Row.index == 0)
            {
                bounds->x -= (float)(layout.Row.item_offset);
            }

            layout.AtY = (float)(y);
            layout.Row.index = (int)(index);
        }

        public static int nk_widget(NkRect* bounds, NkContext ctx)
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkWindow win;
            NkPanel layout;
            nk_input _in_;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(NK_WIDGET_INVALID);
            nk_panel_alloc_space(bounds, ctx);
            win = ctx.Current;
            layout = win.Layout;
            _in_ = ctx.Input;
            c = (NkRect)(layout.Clip);
            bounds->x = ((float)((int)(bounds->x)));
            bounds->y = ((float)((int)(bounds->y)));
            bounds->w = ((float)((int)(bounds->w)));
            bounds->h = ((float)((int)(bounds->h)));
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            nk_unify(ref v, ref c, (float)(bounds->x), (float)(bounds->y), (float)(bounds->x + bounds->w),
                (float)(bounds->y + bounds->h));
            if (
                !(!(((((bounds->x) > (c.x + c.w)) || ((bounds->x + bounds->w) < (c.x))) || ((bounds->y) > (c.y + c.h))) ||
                    ((bounds->y + bounds->h) < (c.y))))) return (int)(NK_WIDGET_INVALID);
            if (
                !((((v.x) <= (_in_.mouse.Pos.x)) && ((_in_.mouse.Pos.x) < (v.x + v.w))) &&
                  (((v.y) <= (_in_.mouse.Pos.y)) && ((_in_.mouse.Pos.y) < (v.y + v.h))))) return (int)(NK_WIDGET_ROM);
            return (int)(NK_WIDGET_VALID);
        }

        public static int nk_widget_fitting(NkRect* bounds, NkContext ctx, NkVec2 item_padding)
        {
            NkWindow win;
            NkStyle style;
            NkPanel layout;
            int state;
            NkVec2 panel_padding = new NkVec2();
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return (int)(NK_WIDGET_INVALID);
            win = ctx.Current;
            style = ctx.Style;
            layout = win.Layout;
            state = (int)(nk_widget(bounds, ctx));
            panel_padding = (NkVec2)(nk_panel_get_padding(style, (int)(layout.Type)));
            if ((layout.Row.index) == (1))
            {
                bounds->w += (float)(panel_padding.x);
                bounds->x -= (float)(panel_padding.x);
            }
            else bounds->x -= (float)(item_padding.x);
            if ((layout.Row.index) == (layout.Row.columns)) bounds->w += (float)(panel_padding.x);
            else bounds->w += (float)(item_padding.x);
            return (int)(state);
        }

        public static void nk_list_view_end(nk_list_view view)
        {
            NkContext ctx;
            NkWindow win;
            NkPanel layout;
            if ((view == null) || (view.ctx == null)) return;
            ctx = view.ctx;
            win = ctx.Current;
            layout = win.Layout;
            layout.AtY = (float)(layout.Bounds.y + (float)(view.total_height));
            *view.scroll_pointer = (uint)(*view.scroll_pointer + view.scroll_value);
            nk_group_end(view.ctx);
        }
    }
}