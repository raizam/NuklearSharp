using System.Runtime.InteropServices;

namespace NuklearSharp
{

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct nk_mouse_button
        {
            public int down;
            public uint clicked;
            public NkVec2 clicked_pos;
        }

        public unsafe partial class nk_input
        {
            public NkKeyboard keyboard = new NkKeyboard();
            public NkMouse mouse = new NkMouse();
        }
    public unsafe static partial class Nk
    {
        public static int nk_input_has_mouse_click(nk_input i, NkButtons id)
        {
            nk_mouse_button* btn;
            if (i == null) return (int)(nk_false);
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return (int)((((btn->clicked) != 0) && ((btn->down) == (nk_false))) ? nk_true : nk_false);
        }

        public static int nk_input_has_mouse_click_in_rect(nk_input i, NkButtons id, NkRect b)
        {
            nk_mouse_button* btn;
            if (i == null) return (int)(nk_false);
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            if (
                !((((b.x) <= (btn->clicked_pos.x)) && ((btn->clicked_pos.x) < (b.x + b.w))) &&
                  (((b.y) <= (btn->clicked_pos.y)) && ((btn->clicked_pos.y) < (b.y + b.h))))) return (int)(nk_false);
            return (int)(nk_true);
        }

        public static int nk_input_has_mouse_click_down_in_rect(nk_input i, NkButtons id, NkRect b, int down)
        {
            nk_mouse_button* btn;
            if (i == null) return (int)(nk_false);
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return
                (int)(((nk_input_has_mouse_click_in_rect(i, (id), (NkRect)(b))) != 0) && ((btn->down) == (down)) ? 1 : 0);
        }

        public static int nk_input_is_mouse_click_in_rect(nk_input i, NkButtons id, NkRect b)
        {
            nk_mouse_button* btn;
            if (i == null) return (int)(nk_false);
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return
                (int)
                    ((((nk_input_has_mouse_click_down_in_rect(i,(id), (NkRect)(b), (int)(nk_false))) != 0) &&
                      ((btn->clicked) != 0))
                        ? nk_true
                        : nk_false);
        }

        public static int nk_input_is_mouse_click_down_in_rect(nk_input i, NkButtons id, NkRect b, int down)
        {
            nk_mouse_button* btn;
            if (i == null) return (int)(nk_false);
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return
                (int)
                    ((((nk_input_has_mouse_click_down_in_rect(i, (id), (NkRect)(b), (int)(down))) != 0) &&
                      ((btn->clicked) != 0))
                        ? nk_true
                        : nk_false);
        }

        public static int nk_input_any_mouse_click_in_rect(nk_input _in_, NkRect b)
        {
            int i;
            int down = (int)(0);
            for (i = (int)(0); (i) < ((int)NkButtons.MAX); ++i)
            {
                down = (int)(((down) != 0) || ((nk_input_is_mouse_click_in_rect(_in_, (NkButtons)(i), (NkRect)(b))) != 0) ? 1 : 0);
            }
            return (int)(down);
        }

        public static int nk_input_is_mouse_hovering_rect(nk_input i, NkRect rect)
        {
            if (i == null) return (int)(nk_false);
            return (((rect.x) <= (i.mouse.Pos.x)) && ((i.mouse.Pos.x) < (rect.x + rect.w))) &&
                   (((rect.y) <= (i.mouse.Pos.y)) && ((i.mouse.Pos.y) < (rect.y + rect.h)))
                ? 1
                : 0;
        }

        public static int nk_input_is_mouse_prev_hovering_rect(nk_input i, NkRect rect)
        {
            if (i == null) return (int)(nk_false);
            return (((rect.x) <= (i.mouse.Prev.x)) && ((i.mouse.Prev.x) < (rect.x + rect.w))) &&
                   (((rect.y) <= (i.mouse.Prev.y)) && ((i.mouse.Prev.y) < (rect.y + rect.h)))
                ? 1
                : 0;
        }

        public static int nk_input_mouse_clicked(nk_input i, NkButtons id, NkRect rect)
        {
            if (i == null) return (int)(nk_false);
            if (nk_input_is_mouse_hovering_rect(i, (NkRect)(rect)) == 0) return (int)(nk_false);
            return (int)(nk_input_is_mouse_click_in_rect(i, (id), (NkRect)(rect)));
        }

        public static int nk_input_is_mouse_down(nk_input i, NkButtons id)
        {
            if (i == null) return (int)(nk_false);
            return (int)(i.mouse.Buttons[(int)id].down);
        }

        public static int nk_input_is_mouse_pressed(nk_input i, NkButtons id)
        {
            nk_mouse_button* b;
            if (i == null) return (int)(nk_false);
            b = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            if (((b->down) != 0) && ((b->clicked) != 0)) return (int)(nk_true);
            return (int)(nk_false);
        }

        public static int nk_input_is_mouse_released(nk_input i, NkButtons id)
        {
            if (i == null) return (int)(nk_false);
            return ((i.mouse.Buttons[(int)id].down == 0) && ((i.mouse.Buttons[(int)id].clicked) != 0)) ? 1 : 0;
        }

        public static int nk_input_is_key_pressed(nk_input i, NkKeys key)
        {
            nk_key* k;
            if (i == null) return (int)(nk_false);
            k = (nk_key*)i.keyboard.Keys + (int)key;
            if ((((k->down) != 0) && ((k->clicked) != 0)) || ((k->down == 0) && ((k->clicked) >= (2)))) return (int)(nk_true);
            return (int)(nk_false);
        }

        public static int nk_input_is_key_released(nk_input i, int key)
        {
            nk_key* k;
            if (i == null) return (int)(nk_false);
            k = (nk_key*)i.keyboard.Keys + key;
            if (((k->down == 0) && ((k->clicked) != 0)) || (((k->down) != 0) && ((k->clicked) >= (2)))) return (int)(nk_true);
            return (int)(nk_false);
        }

        public static int nk_input_is_key_down(nk_input i, int key)
        {
            nk_key* k;
            if (i == null) return (int)(nk_false);
            k = (nk_key*)i.keyboard.Keys + key;
            if ((k->down) != 0) return (int)(nk_true);
            return (int)(nk_false);
        }

        public static int nk_toggle_behavior(nk_input _in_, NkRect select, ref NkWidgetStates state, int active)
        {
            if (((state) & NkWidgetStates.MODIFIED) != 0)
                (state) = (NkWidgetStates.INACTIVE | NkWidgetStates.MODIFIED);
            else (state) = (NkWidgetStates.INACTIVE);
            if ((nk_button_behavior(ref state, select, _in_, NkButtonBehavior.Default)) != 0)
            {
                state = (NkWidgetStates.ACTIVE);
                active = active != 0 ? 0 : 1;
            }

            if (((state & NkWidgetStates.HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(select)) == 0))
                state |= (NkWidgetStates.ENTERED);
            else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(select))) != 0) state |= (NkWidgetStates.LEFT);
            return (int)(active);
        }
    }
}