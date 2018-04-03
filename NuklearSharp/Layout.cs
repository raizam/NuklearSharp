namespace NuklearSharp
{
    static internal class Layout
    {
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
            Nk.nk_row_layout_(ctx, (NkLayoutFormat.NK_DYNAMIC), (float)(height), (int)(cols), (int)(0));
        }

        public static void nk_layout_row_static(NkContext ctx, float height, int item_width, int cols)
        {
            Nk.nk_row_layout_(ctx, (NkLayoutFormat.NK_STATIC), (float)(height), (int)(cols), (int)(item_width));
        }

        public static unsafe void nk_layout_row_begin(NkContext ctx, NkLayoutFormat fmt, float row_height, int cols)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            Nk.nk_panel_layout(ctx, win, (float)(row_height), (int)(cols));
            if ((fmt) == (NkLayoutFormat.NK_DYNAMIC)) layout.Row.type = (NkPanelRowLayoutType.DYNAMIC_ROW);
            else layout.Row.type = (NkPanelRowLayoutType.STATIC_ROW);
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
            if ((layout.Row.type != NkPanelRowLayoutType.STATIC_ROW) && (layout.Row.type != NkPanelRowLayoutType.DYNAMIC_ROW)) return;
            if ((layout.Row.type) == (NkPanelRowLayoutType.DYNAMIC_ROW))
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
            if ((layout.Row.type != NkPanelRowLayoutType.STATIC_ROW) && (layout.Row.type != NkPanelRowLayoutType.DYNAMIC_ROW)) return;
            layout.Row.item_width = (float)(0);
            layout.Row.item_offset = (float)(0);
        }

        public static unsafe void nk_layout_row(NkContext ctx, NkLayoutFormat fmt, float height, int cols, float* ratio)
        {
            int i;
            int n_undef = (int)(0);
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            Nk.nk_panel_layout(ctx, win, (float)(height), (int)(cols));
            if ((fmt) == (NkLayoutFormat.NK_DYNAMIC))
            {
                float r = (float)(0);
                layout.Row.ratio = ratio;
                for (i = (int)(0); (i) < (cols); ++i)
                {
                    if ((ratio[i]) < (0.0f)) n_undef++;
                    else r += (float)(ratio[i]);
                }
                r = (float)((0) < ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) ? ((1.0f) < (1.0f - r) ? (1.0f) : (1.0f - r)) : (0));
                layout.Row.type = (NkPanelRowLayoutType.DYNAMIC);
                layout.Row.item_width = (float)((((r) > (0)) && ((n_undef) > (0))) ? (r / (float)(n_undef)) : 0);
            }
            else
            {
                layout.Row.ratio = ratio;
                layout.Row.type = (NkPanelRowLayoutType.STATIC);
                layout.Row.item_width = (float)(0);
                layout.Row.item_offset = (float)(0);
            }

            layout.Row.item_offset = (float)(0);
            layout.Row.filled = (float)(0);
        }

        public static unsafe void nk_layout_row_template_begin(NkContext ctx, float height)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            Nk.nk_panel_layout(ctx, win, (float)(height), (int)(1));
            layout.Row.type = (NkPanelRowLayoutType.TEMPLATE);
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
            if (layout.Row.type != NkPanelRowLayoutType.TEMPLATE) return;
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
            if (layout.Row.type != NkPanelRowLayoutType.TEMPLATE) return;
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
            if (layout.Row.type != NkPanelRowLayoutType.TEMPLATE) return;
            if ((layout.Row.columns) >= (16)) return;
            layout.Row.templates[layout.Row.columns++] = (float)(width);
        }

        public static unsafe void nk_layout_row_template_end(NkContext ctx)
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
            if (layout.Row.type != NkPanelRowLayoutType.TEMPLATE) return;
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
                    (Nk.nk_layout_row_calculate_usable_space(ctx.Style, (layout.Type), (float)(layout.Bounds.w),
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

        public static unsafe void nk_layout_space_begin(NkContext ctx, NkLayoutFormat fmt, float height, int widget_count)
        {
            NkWindow win;
            NkPanel layout;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return;
            win = ctx.Current;
            layout = win.Layout;
            Nk.nk_panel_layout(ctx, win, (float)(height), (int)(widget_count));
            if ((fmt) == (NkLayoutFormat.NK_STATIC)) layout.Row.type = (NkPanelRowLayoutType.STATIC_FREE);
            else layout.Row.type = (NkPanelRowLayoutType.DYNAMIC_FREE);
            layout.Row.ratio = null;
            layout.Row.filled = (float)(0);
            layout.Row.item_width = (float)(0);
            layout.Row.item_offset = (float)(0);
        }

        public static unsafe void nk_layout_space_end(NkContext ctx)
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
                Nk.nk_zero(ptr, (ulong)(sizeof(NkRect)));
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
    }
}