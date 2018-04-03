using System.Runtime.InteropServices;

namespace NuklearSharp
{

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_mouse_button
    {
        public int down;
        // public bool down { get { return _down != 0; } set { _down = value ? 1 : 0; } }
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
        public static bool nk_input_has_mouse_click(nk_input i, NkButtons id)
        {
            nk_mouse_button* btn;
            if (i == null) return false;
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return ((((btn->clicked) != 0) && ((btn->down) == (0))));
        }

        public static bool nk_input_has_mouse_click_in_rect(nk_input i, NkButtons id, NkRect b)
        {
            nk_mouse_button* btn;
            if (i == null) return false;
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            if (
                !((((b.x) <= (btn->clicked_pos.x)) && ((btn->clicked_pos.x) < (b.x + b.w))) &&
                  (((b.y) <= (btn->clicked_pos.y)) && ((btn->clicked_pos.y) < (b.y + b.h))))) return false;
            return true;
        }

        public static bool nk_input_has_mouse_click_down_in_rect(nk_input i, NkButtons id, NkRect b, bool down)
        {
            nk_mouse_button* btn;
            if (i == null) return false;
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return
                (((nk_input_has_mouse_click_in_rect(i, (id), (NkRect)(b)))) && ((btn->down != 0 ? true : false) == (down)));
        }

        public static bool nk_input_is_mouse_click_in_rect(nk_input i, NkButtons id, NkRect b)
        {
            nk_mouse_button* btn;
            if (i == null) return false;
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return

                    ((((nk_input_has_mouse_click_down_in_rect(i, (id), (NkRect)(b), false))) &&
                      ((btn->clicked) != 0))
                        ? true
                        : false);
        }

        public static bool nk_input_is_mouse_click_down_in_rect(nk_input i, NkButtons id, NkRect b, bool down)
        {
            nk_mouse_button* btn;
            if (i == null) return false;
            btn = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            return
                    ((((nk_input_has_mouse_click_down_in_rect(i, (id), (NkRect)(b), (down)))) &&
                      ((btn->clicked) != 0)));
        }

        public static bool nk_input_any_mouse_click_in_rect(nk_input _in_, NkRect b)
        {

            for (int i = 0; (i) < ((int)NkButtons.MAX); ++i)
            {
                if (nk_input_is_mouse_click_in_rect(_in_, (NkButtons)(i), (NkRect)(b)))
                    return true;
            }
            return false;
        }

        public static bool nk_input_is_mouse_hovering_rect(nk_input i, NkRect rect)
        {
            if (i == null) return false;
            return (((rect.x) <= (i.mouse.Pos.x)) && ((i.mouse.Pos.x) < (rect.x + rect.w))) &&
                   (((rect.y) <= (i.mouse.Pos.y)) && ((i.mouse.Pos.y) < (rect.y + rect.h)));
        }

        public static bool nk_input_is_mouse_prev_hovering_rect(nk_input i, NkRect rect)
        {
            if (i == null) return false;
            return (((rect.x) <= (i.mouse.Prev.x)) && ((i.mouse.Prev.x) < (rect.x + rect.w))) &&
                   (((rect.y) <= (i.mouse.Prev.y)) && ((i.mouse.Prev.y) < (rect.y + rect.h)));
        }

        public static bool nk_input_mouse_clicked(nk_input i, NkButtons id, NkRect rect)
        {
            if (i == null) return false;
            if (nk_input_is_mouse_hovering_rect(i, (NkRect)(rect)) == false) return false;
            return (nk_input_is_mouse_click_in_rect(i, (id), (NkRect)(rect)));
        }

        public static bool nk_input_is_mouse_down(nk_input i, NkButtons id)
        {
            if (i == null) return false;
            return (i.mouse.Buttons[(int)id].down != 0);
        }

        public static bool nk_input_is_mouse_pressed(nk_input i, NkButtons id)
        {
            nk_mouse_button* b;
            if (i == null) return false;
            b = (nk_mouse_button*)i.mouse.Buttons + (int)id;
            if (((b->down != 0)) && ((b->clicked != 0))) return true;
            return false;
        }

        public static bool nk_input_is_mouse_released(nk_input i, NkButtons id)
        {
            if (i == null) return false;
            return ((i.mouse.Buttons[(int)id].down == 0) && ((i.mouse.Buttons[(int)id].clicked) != 0));
        }

        public static bool nk_input_is_key_pressed(nk_input i, NkKeys key)
        {
            nk_key* k;
            if (i == null) return false;
            k = (nk_key*)i.keyboard.Keys + (int)key;
            if ((((k->down) != 0) && ((k->clicked) != 0)) || ((k->down == 0) && ((k->clicked) >= (2)))) return true;
            return false;
        }

        public static bool nk_input_is_key_released(nk_input i, int key)
        {
            nk_key* k;
            if (i == null) return false;
            k = (nk_key*)i.keyboard.Keys + key;
            if (((k->down == 0) && ((k->clicked) != 0)) || (((k->down) != 0) && ((k->clicked) >= (2)))) return true;
            return false;
        }

        public static bool nk_input_is_key_down(nk_input i, int key)
        {
            nk_key* k;
            if (i == null) return false;
            k = (nk_key*)i.keyboard.Keys + key;
            if ((k->down) != 0) return true;
            return false;
        }

        public static bool nk_toggle_behavior(nk_input _in_, NkRect select, ref NkWidgetStates state, bool active)
        {
            if (((state) & NkWidgetStates.MODIFIED) != 0)
                (state) = (NkWidgetStates.INACTIVE | NkWidgetStates.MODIFIED);
            else (state) = (NkWidgetStates.INACTIVE);
            if ((nk_button_behavior(ref state, select, _in_, NkButtonBehavior.Default)))
            {
                state = (NkWidgetStates.ACTIVE);
                active = !active;
            }

            if (((state & NkWidgetStates.HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(select)) == false))
                state |= (NkWidgetStates.ENTERED);
            else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(select)))) state |= (NkWidgetStates.LEFT);
            return active;
        }
    }
}