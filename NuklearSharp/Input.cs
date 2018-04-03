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

        public class NkKeyboard
        {
            public PinnedArray<nk_key> Keys = new PinnedArray<nk_key>(new nk_key[(int)NkKeys.MAX]);
            public PinnedArray<char> Text = new PinnedArray<char>(new char[16]);
            public int TextLen;
        }

        public class NkMouse
        {
            public PinnedArray<nk_mouse_button> Buttons =
                new PinnedArray<nk_mouse_button>(new nk_mouse_button[(int)NkButtons.MAX]);

            public NkVec2 Pos;
            public NkVec2 Prev;
            public NkVec2 Delta;
            public NkVec2 ScrollDelta;
            public byte Grab;
            public byte Grabbed;
            public byte Ungrab;
        }


        public NkKeyboard keyboard = new NkKeyboard();
        public NkMouse mouse = new NkMouse();

        //nk_input_has_mouse_click_in_rect
    }



    public static unsafe class InputExtentions
    {
        public static bool nk_input_has_mouse_click(this nk_input nkInput, NkButtons id)
        {
            //nk_input_has_mouse_click
            nk_mouse_button* btn;
            if (nkInput == null) return false;
            btn = (nk_mouse_button*)nkInput.mouse.Buttons + (int)id;
            return ((((btn->clicked) != 0) && ((btn->down) == (0))));
        }

        public static bool nk_input_has_mouse_click_in_rect(this nk_input nkInput, NkButtons id, NkRect b)
        {
            nk_mouse_button* btn;
            if (nkInput == null) return false;
            btn = (nk_mouse_button*)nkInput.mouse.Buttons + (int)id;
            if (
                !((((b.x) <= (btn->clicked_pos.x)) && ((btn->clicked_pos.x) < (b.x + b.w))) &&
                  (((b.y) <= (btn->clicked_pos.y)) && ((btn->clicked_pos.y) < (b.y + b.h))))) return false;
            return true;
        }

        public static bool nk_input_has_mouse_click_down_in_rect(this nk_input nkInput, NkButtons id, NkRect b, bool down)
        {
            nk_mouse_button* btn;
            if (nkInput == null) return false;
            btn = (nk_mouse_button*)nkInput.mouse.Buttons + (int)id;
            return
                (((nk_input_has_mouse_click_in_rect(nkInput, (id), (NkRect)(b)))) && ((btn->down != 0) == (down)));
        }

        public static bool nk_input_is_mouse_click_in_rect(this nk_input nkInput, NkButtons id, NkRect b)
        {
            nk_mouse_button* btn;
            if (nkInput == null) return false;
            btn = (nk_mouse_button*)nkInput.mouse.Buttons + (int)id;
            return

                ((((nk_input_has_mouse_click_down_in_rect(nkInput, (id), (NkRect)(b), false))) &&
                  ((btn->clicked) != 0))
                    ? true
                    : false);
        }

        public static bool nk_input_is_mouse_click_down_in_rect(this nk_input nkInput, NkButtons id, NkRect b, bool down)
        {
            nk_mouse_button* btn;
            if (nkInput == null) return false;
            btn = (nk_mouse_button*)nkInput.mouse.Buttons + (int)id;
            return
                ((((nk_input_has_mouse_click_down_in_rect(nkInput, (id), (NkRect)(b), (down)))) &&
                  ((btn->clicked) != 0)));
        }

        public static bool nk_input_any_mouse_click_in_rect(this nk_input nkInput, NkRect b)
        {

            for (int i = 0; (i) < ((int)NkButtons.MAX); ++i)
            {
                if (nk_input_is_mouse_click_in_rect(nkInput, (NkButtons)(i), (NkRect)(b)))
                    return true;
            }
            return false;
        }

        public static bool nk_input_is_mouse_hovering_rect(this nk_input nkInput, NkRect rect)
        {
            if (nkInput == null) return false;
            return (((rect.x) <= (nkInput.mouse.Pos.x)) && ((nkInput.mouse.Pos.x) < (rect.x + rect.w))) &&
                   (((rect.y) <= (nkInput.mouse.Pos.y)) && ((nkInput.mouse.Pos.y) < (rect.y + rect.h)));
        }

        public static bool nk_input_is_mouse_prev_hovering_rect(this nk_input nkInput, NkRect rect)
        {
            if (nkInput == null) return false;
            return (((rect.x) <= (nkInput.mouse.Prev.x)) && ((nkInput.mouse.Prev.x) < (rect.x + rect.w))) &&
                   (((rect.y) <= (nkInput.mouse.Prev.y)) && ((nkInput.mouse.Prev.y) < (rect.y + rect.h)));
        }

        public static bool nk_input_mouse_clicked(this nk_input nkInput, NkButtons id, NkRect rect)
        {
            if (nkInput == null) return false;
            if (nk_input_is_mouse_hovering_rect(nkInput, (NkRect)(rect)) == false) return false;
            return (nk_input_is_mouse_click_in_rect(nkInput, (id), (NkRect)(rect)));
        }

        public static bool nk_input_is_mouse_down(this nk_input nkInput, NkButtons id)
        {
            if (nkInput == null) return false;
            return (nkInput.mouse.Buttons[(int)id].down != 0);
        }

        public static bool nk_input_is_mouse_pressed(this nk_input nkInput, NkButtons id)
        {
            nk_mouse_button* b;
            if (nkInput == null) return false;
            b = (nk_mouse_button*)nkInput.mouse.Buttons + (int)id;
            if (((b->down != 0)) && ((b->clicked != 0))) return true;
            return false;
        }

        public static bool nk_input_is_mouse_released(this nk_input nkInput, NkButtons id)
        {
            if (nkInput == null) return false;
            return ((nkInput.mouse.Buttons[(int)id].down == 0) && ((nkInput.mouse.Buttons[(int)id].clicked) != 0));
        }

        public static bool nk_input_is_key_pressed(this nk_input nkInput, NkKeys key)
        {
            nk_key* k;
            if (nkInput == null) return false;
            k = (nk_key*)nkInput.keyboard.Keys + (int)key;
            if ((((k->down) != 0) && ((k->clicked) != 0)) || ((k->down == 0) && ((k->clicked) >= (2)))) return true;
            return false;
        }

        public static bool nk_input_is_key_released(this nk_input nkInput, int key)
        {
            nk_key* k;
            if (nkInput == null) return false;
            k = (nk_key*)nkInput.keyboard.Keys + key;
            if (((k->down == 0) && ((k->clicked) != 0)) || (((k->down) != 0) && ((k->clicked) >= (2)))) return true;
            return false;
        }

        public static bool nk_input_is_key_down(this nk_input nkInput, int key)
        {
            nk_key* k;
            if (nkInput == null) return false;
            k = (nk_key*)nkInput.keyboard.Keys + key;
            if ((k->down) != 0) return true;
            return false;
        }
    }
}