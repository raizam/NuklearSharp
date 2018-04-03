namespace NuklearSharp
{
    public unsafe class NkContext
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
            return (InputExtentions.nk_input_is_mouse_hovering_rect(Input, (NkRect)(bounds)));
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
            return (InputExtentions.nk_input_mouse_clicked(Input, (btn), (NkRect)(bounds)));
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
            return (InputExtentions.nk_input_has_mouse_click_down_in_rect(Input, (btn), (NkRect)(bounds), (down)));
        }

        public void nk_spacing(int cols)
        {
            NkRect none = new NkRect();
            int i;
            if (((this == null) || (Current == null)) || (Current.Layout == null)) return;
            var win = Current;
            var layout = win.Layout;
            var index = (int)((layout.Row.index + cols) % layout.Row.columns);
            var rows = (int)((layout.Row.index + cols) / layout.Row.columns);
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

        public void nk_free()
        {
            if (this == null) return;

            Seq = 0;
            Build = false;
            Begin = null;
            End = null;
            Active = null;
            Current = null;
            Count = 0;
        }

        public NkWindow nk_create_window()
        {
            var result = new NkWindow { Seq = Seq };

            return result;
        }

        public void nk_free_window(NkWindow win)
        {
            nk_table it = win.Tables;
            if (win.Popup.win != null)
            {
                nk_free_window(win.Popup.win);
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

        public void nk_build()
        {
            if (Style.CursorActive == null) Style.CursorActive = Style.Cursors[(int)NkStyleCursor.ARROW];
            if (Style.CursorActive != null && Input.mouse.Grabbed == 0 && Style.CursorVisible)
            {
                var mouseBounds = new NkRect();
                var cursor = Style.CursorActive;
                Overlay.nk_command_buffer_init(false);
                Nk.nk_start_buffer(this, Overlay);
                mouseBounds.x = Input.mouse.Pos.x - cursor.offset.x;
                mouseBounds.y = Input.mouse.Pos.y - cursor.offset.y;
                mouseBounds.w = cursor.size.x;
                mouseBounds.h = cursor.size.y;
                Overlay.nk_draw_image(mouseBounds, cursor.img, Nk.nk_white);
            }

            var it = Begin;
            NkCommandBase cmd = null;
            for (; it != null;)
            {
                var next = it.Next;
                if ((it.Flags & PanelFlags.HIDDEN) != 0 || it.Seq != Seq)
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

            it = Begin;

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
                cmd.Next = Overlay.Count > 0 ? Overlay.First : null;
            }
        }

        public NkWindow nk__begin()
        {
            if (this == null || Count == 0) return null;
            if (Build == false)
            {
                nk_build();
                Build = true;
            }

            var iter = Begin;
            while (iter != null &&
                   (iter.Buffer.Count == 0 || (iter.Flags & PanelFlags.HIDDEN) != 0 || iter.Seq != Seq))
            {
                iter = iter.Next;
            }

            return iter;
        }
    }
}