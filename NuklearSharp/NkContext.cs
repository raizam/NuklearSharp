namespace NuklearSharp
{
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
        public bool Build;
        public NkWindow Begin;
        public NkWindow End;
        public NkWindow Active;
        public NkWindow Current;
        public uint Count;
        public uint Seq;

        public NkRect nk_widget_bounds()
        {
            NkRect bounds = new NkRect();
            if ((this == null) || (Current == null))
                return (NkRect)(Nk.nk_rect_((float)(0), (float)(0), (float)(0), (float)(0)));
            Nk.nk_layout_peek(&bounds, this);
            return (NkRect)(bounds);
        }

        public NkVec2 nk_widget_position()
        {
            NkRect bounds = new NkRect();
            if ((this == null) || (Current == null)) return (NkVec2)(Nk.nk_vec2_((float)(0), (float)(0)));
            Nk.nk_layout_peek(&bounds, this);
            return (NkVec2)(Nk.nk_vec2_((float)(bounds.x), (float)(bounds.y)));
        }

        public NkVec2 nk_widget_size()
        {
            NkRect bounds = new NkRect();
            if ((this == null) || (Current == null)) return (NkVec2)(Nk.nk_vec2_((float)(0), (float)(0)));
            Nk.nk_layout_peek(&bounds, this);
            return (NkVec2)(Nk.nk_vec2_((float)(bounds.w), (float)(bounds.h)));
        }

        public float nk_widget_width()
        {
            NkRect bounds = new NkRect();
            if ((this == null) || (Current == null)) return (float)(0);
            Nk.nk_layout_peek(&bounds, this);
            return (float)(bounds.w);
        }

        public float nk_widget_height()
        {
            NkRect bounds = new NkRect();
            if ((this == null) || (Current == null)) return (float)(0);
            Nk.nk_layout_peek(&bounds, this);
            return (float)(bounds.h);
        }

        public bool nk_widget_is_hovered()
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkRect bounds = new NkRect();
            if (((this == null) || (Current == null)) || (Active != Current)) return false;
            c = (NkRect)(Current.Layout.Clip);
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            Nk.nk_layout_peek(&bounds, this);
            Nk.nk_unify(ref v, ref c, (float)(bounds.x), (float)(bounds.y), (float)(bounds.x + bounds.w),
                (float)(bounds.y + bounds.h));
            if (
                !(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
                    ((bounds.y + bounds.h) < (c.y))))) return false;
            return (Nk.nk_input_is_mouse_hovering_rect(Input, (NkRect)(bounds)));
        }

        public bool nk_widget_is_mouse_clicked(NkButtons btn)
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkRect bounds = new NkRect();
            if (((this == null) || (Current == null)) || (Active != Current)) return false;
            c = (NkRect)(Current.Layout.Clip);
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            Nk.nk_layout_peek(&bounds, this);
            Nk.nk_unify(ref v, ref c, (float)(bounds.x), (float)(bounds.y), (float)(bounds.x + bounds.w),
                (float)(bounds.y + bounds.h));
            if (
                !(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
                    ((bounds.y + bounds.h) < (c.y))))) return false;
            return (Nk.nk_input_mouse_clicked(Input, (btn), (NkRect)(bounds)));
        }

        public bool nk_widget_has_mouse_click_down(NkButtons btn, bool down)
        {
            NkRect c = new NkRect();
            NkRect v = new NkRect();
            NkRect bounds = new NkRect();
            if (((this == null) || (Current == null)) || (Active != Current)) return false;
            c = (NkRect)(Current.Layout.Clip);
            c.x = ((float)((int)(c.x)));
            c.y = ((float)((int)(c.y)));
            c.w = ((float)((int)(c.w)));
            c.h = ((float)((int)(c.h)));
            Nk.nk_layout_peek(&bounds, this);
            Nk.nk_unify(ref v, ref c, (float)(bounds.x), (float)(bounds.y), (float)(bounds.x + bounds.w),
                (float)(bounds.y + bounds.h));
            if (
                !(!(((((bounds.x) > (c.x + c.w)) || ((bounds.x + bounds.w) < (c.x))) || ((bounds.y) > (c.y + c.h))) ||
                    ((bounds.y + bounds.h) < (c.y))))) return false;
            return (Nk.nk_input_has_mouse_click_down_in_rect(Input, (btn), (NkRect)(bounds), (down)));
        }

        public void nk_spacing(int cols)
        {
            NkWindow win;
            NkPanel layout;
            NkRect none = new NkRect();
            int i;
            int index;
            int rows;
            if (((this == null) || (Current == null)) || (Current.Layout == null)) return;
            win = Current;
            layout = win.Layout;
            index = (int)((layout.Row.index + cols) % layout.Row.columns);
            rows = (int)((layout.Row.index + cols) / layout.Row.columns);
            if ((rows) != 0)
            {
                for (i = (int)(0); (i) < (rows); ++i)
                {
                    Nk.nk_panel_alloc_row(this, win);
                }
                cols = (int)(index);
            }

            if ((layout.Row.type != NkPanelRowLayoutType.DYNAMIC_FIXED) && (layout.Row.type != NkPanelRowLayoutType.STATIC_FIXED))
            {
                for (i = (int)(0); (i) < (cols); ++i)
                {
                    Nk.nk_panel_alloc_space(&none, this);
                }
            }

            layout.Row.index = (int)(index);
        }
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
}