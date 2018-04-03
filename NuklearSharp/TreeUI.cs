using NuklearSharp;

namespace NuklearSharp
{
    internal static class TreeUI
    {
        public static unsafe bool nk_tree_state_base(NkContext ctx, NkTreeType type, NkImage img, char* title, ref NkCollapseStates state)
        {
            NkWindow win;
            NkPanel layout;
            NkStyle style;
            NkCommandBuffer _out_;
            nk_input _in_;
            nk_style_button button;
            NkSymbolType symbol;
            float row_height;
            NkVec2 item_spacing = new NkVec2();
            NkRect header = new NkRect();
            NkRect sym = new NkRect();
            nk_text text = new nk_text();
            NkWidgetStates ws = (uint)(0);
            NkWidgetLayoutStates widget_state;
            if (((ctx == null) || (ctx.Current == null)) || (ctx.Current.Layout == null)) return false;
            win = ctx.Current;
            layout = win.Layout;
            _out_ = win.Buffer;
            style = ctx.Style;
            item_spacing = (NkVec2)(style.Window.spacing);
            row_height = (float)(style.Font.Height + 2 * style.Tab.padding.y);
            Layout.nk_layout_set_min_row_height(ctx, (float)(row_height));
            Layout.nk_layout_row_dynamic(ctx, (float)(row_height), (int)(1));
            Layout.nk_layout_reset_min_row_height(ctx);
            widget_state = Nk.nk_widget(&header, ctx);
            if ((type) == (NkTreeType.NK_TREE_TAB))
            {
                NkStyleItem background = style.Tab.background;
                if ((background.Type) == (NkStyleItemType.IMAGE))
                {
                    _out_.nk_draw_image((NkRect)(header), background.Data.Image, (NkColor)(Nk.nk_white));
                    text.background = (NkColor)(NkColor.nk_rgba((int)(0), (int)(0), (int)(0), (int)(0)));
                }
                else
                {
                    text.background = (NkColor)(background.Data.Color);
                    _out_.nk_fill_rect((NkRect)(header), (float)(0), (NkColor)(style.Tab.border_color));
                    _out_.nk_fill_rect((NkRect)(Nk.nk_shrink_rect_((NkRect)(header), (float)(style.Tab.border))),
                        (float)(style.Tab.rounding), (NkColor)(background.Data.Color));
                }
            }
            else text.background = (NkColor)(style.Window.background);
            _in_ = ((layout.Flags & PanelFlags.ROM) == 0) ? ctx.Input : null;
            _in_ = (((_in_) != null) && ((widget_state) == (NkWidgetLayoutStates.NK_WIDGET_VALID))) ? ctx.Input : null;
            if ((Nk.nk_button_behavior(ref ws, (NkRect)(header), _in_, NkButtonBehavior.Default)))
                state = (((state) == (NkCollapseStates.NK_MAXIMIZED)) ? NkCollapseStates.NK_MINIMIZED : NkCollapseStates.NK_MAXIMIZED);
            if ((state) == (NkCollapseStates.NK_MAXIMIZED))
            {
                symbol = (style.Tab.sym_maximize);
                if ((type) == (NkTreeType.NK_TREE_TAB)) button = style.Tab.tab_maximize_button;
                else button = style.Tab.node_maximize_button;
            }
            else
            {
                symbol = (style.Tab.sym_minimize);
                if ((type) == (NkTreeType.NK_TREE_TAB)) button = style.Tab.tab_minimize_button;
                else button = style.Tab.node_minimize_button;
            }

            {
                sym.w = (float)(sym.h = (float)(style.Font.Height));
                sym.y = (float)(header.y + style.Tab.padding.y);
                sym.x = (float)(header.x + style.Tab.padding.x);
                Nk.nk_do_button_symbol(ref ws, win.Buffer, (NkRect)(sym), (symbol), NkButtonBehavior.Default, button, null,
                    style.Font);
                if ((img) != null)
                {
                    sym.x = (float)(sym.x + sym.w + 4 * item_spacing.x);
                    win.Buffer.nk_draw_image((NkRect)(sym), img, (NkColor)(Nk.nk_white));
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
                text.padding = (NkVec2)(Nk.nk_vec2_((float)(0), (float)(0)));
                _out_.nk_widget_text((NkRect)(label), title, (int)(Nk.nk_strlen(title)), &text, (Alignment.MIDDLELEFT), style.Font);
            }

            if ((state) == (NkCollapseStates.NK_MAXIMIZED))
            {
                layout.AtX = (float)(header.x + (float)(layout.Offset.x) + style.Tab.indent);
                layout.Bounds.w = (float)((layout.Bounds.w) < (style.Tab.indent) ? (style.Tab.indent) : (layout.Bounds.w));
                layout.Bounds.w -= (float)(style.Tab.indent + style.Window.padding.x);
                layout.Row.tree_depth++;
                return true;
            }
            else return false;
        }

        public static unsafe bool nk_tree_base(NkContext ctx, NkTreeType type, NkImage img, char* title, int initial_state, char* hash,
            int len, int line)
        {
            NkWindow win = ctx.Current;
            int title_len = (int)(0);
            uint tree_hash = (uint)(0);
            uint* state = null;
            if (hash == null)
            {
                title_len = (int)(Nk.nk_strlen(title));
                tree_hash = (uint)(Nk.nk_murmur_hash(title, (int)(title_len), (uint)(line)));
            }
            else tree_hash = (uint)(Nk.nk_murmur_hash(hash, (int)(len), (uint)(line)));
            state = win.nk_find_value((uint)(tree_hash));
            if (state == null)
            {
                state = Nk.nk_add_value(ctx, win, (uint)(tree_hash), (uint)(0));
                *state = (uint)(initial_state);
            }

            NkCollapseStates kkk = (NkCollapseStates)(*state);
            var result = (nk_tree_state_base(ctx, (type), img, title, ref kkk));
            *state = (uint)(int)kkk;
            return result;
        }

        public static unsafe bool nk_tree_state_push(NkContext ctx, NkTreeType type, char* title, ref NkCollapseStates state)
        {
            return (nk_tree_state_base(ctx, (type), null, title, ref state));
        }

        public static unsafe bool nk_tree_state_image_push(NkContext ctx, NkTreeType type, NkImage img, char* title, ref NkCollapseStates state)
        {
            return (nk_tree_state_base(ctx, (type), img, title, ref state));
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

        public static unsafe bool nk_tree_push_hashed(NkContext ctx, NkTreeType type, char* title, int initial_state, char* hash, int len,
            int line)
        {
            return (nk_tree_base(ctx, (type), null, title, (int)(initial_state), hash, (int)(len), (int)(line)));
        }

        public static unsafe bool nk_tree_image_push_hashed(NkContext ctx, NkTreeType type, NkImage img, char* title, int initial_state,
            char* hash, int len, int seed)
        {
            return (nk_tree_base(ctx, (type), img, title, (int)(initial_state), hash, (int)(len), (int)(seed)));
        }

        public static void nk_tree_pop(NkContext ctx)
        {
            nk_tree_state_pop(ctx);
        }
    }

}
