using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_mouse_button
		{
			public int down;
			public uint clicked;
			public nk_vec2 clicked_pos;
		}

		public unsafe partial class nk_input
		{
			public nk_keyboard keyboard = new nk_keyboard();
			public nk_mouse mouse = new nk_mouse();
		}

		public static int nk_input_has_mouse_click(nk_input i, int id)
		{
			nk_mouse_button* btn;
			if (i == null) return (int) (nk_false);
			btn = (nk_mouse_button*) i.mouse.buttons + id;
			return (int) ((((btn->clicked) != 0) && ((btn->down) == (nk_false))) ? nk_true : nk_false);
		}

		public static int nk_input_has_mouse_click_in_rect(nk_input i, int id, nk_rect b)
		{
			nk_mouse_button* btn;
			if (i == null) return (int) (nk_false);
			btn = (nk_mouse_button*) i.mouse.buttons + id;
			if (
				!((((b.x) <= (btn->clicked_pos.x)) && ((btn->clicked_pos.x) < (b.x + b.w))) &&
				  (((b.y) <= (btn->clicked_pos.y)) && ((btn->clicked_pos.y) < (b.y + b.h))))) return (int) (nk_false);
			return (int) (nk_true);
		}

		public static int nk_input_has_mouse_click_down_in_rect(nk_input i, int id, nk_rect b, int down)
		{
			nk_mouse_button* btn;
			if (i == null) return (int) (nk_false);
			btn = (nk_mouse_button*) i.mouse.buttons + id;
			return
				(int) (((nk_input_has_mouse_click_in_rect(i, (int) (id), (nk_rect) (b))) != 0) && ((btn->down) == (down)) ? 1 : 0);
		}

		public static int nk_input_is_mouse_click_in_rect(nk_input i, int id, nk_rect b)
		{
			nk_mouse_button* btn;
			if (i == null) return (int) (nk_false);
			btn = (nk_mouse_button*) i.mouse.buttons + id;
			return
				(int)
					((((nk_input_has_mouse_click_down_in_rect(i, (int) (id), (nk_rect) (b), (int) (nk_false))) != 0) &&
					  ((btn->clicked) != 0))
						? nk_true
						: nk_false);
		}

		public static int nk_input_is_mouse_click_down_in_rect(nk_input i, int id, nk_rect b, int down)
		{
			nk_mouse_button* btn;
			if (i == null) return (int) (nk_false);
			btn = (nk_mouse_button*) i.mouse.buttons + id;
			return
				(int)
					((((nk_input_has_mouse_click_down_in_rect(i, (int) (id), (nk_rect) (b), (int) (down))) != 0) &&
					  ((btn->clicked) != 0))
						? nk_true
						: nk_false);
		}

		public static int nk_input_any_mouse_click_in_rect(nk_input _in_, nk_rect b)
		{
			int i;
			int down = (int) (0);
			for (i = (int) (0); (i) < (NK_BUTTON_MAX); ++i)
			{
				down = (int) (((down) != 0) || ((nk_input_is_mouse_click_in_rect(_in_, (int) (i), (nk_rect) (b))) != 0) ? 1 : 0);
			}
			return (int) (down);
		}

		public static int nk_input_is_mouse_hovering_rect(nk_input i, nk_rect rect)
		{
			if (i == null) return (int) (nk_false);
			return (((rect.x) <= (i.mouse.pos.x)) && ((i.mouse.pos.x) < (rect.x + rect.w))) &&
			       (((rect.y) <= (i.mouse.pos.y)) && ((i.mouse.pos.y) < (rect.y + rect.h)))
				? 1
				: 0;
		}

		public static int nk_input_is_mouse_prev_hovering_rect(nk_input i, nk_rect rect)
		{
			if (i == null) return (int) (nk_false);
			return (((rect.x) <= (i.mouse.prev.x)) && ((i.mouse.prev.x) < (rect.x + rect.w))) &&
			       (((rect.y) <= (i.mouse.prev.y)) && ((i.mouse.prev.y) < (rect.y + rect.h)))
				? 1
				: 0;
		}

		public static int nk_input_mouse_clicked(nk_input i, int id, nk_rect rect)
		{
			if (i == null) return (int) (nk_false);
			if (nk_input_is_mouse_hovering_rect(i, (nk_rect) (rect)) == 0) return (int) (nk_false);
			return (int) (nk_input_is_mouse_click_in_rect(i, (int) (id), (nk_rect) (rect)));
		}

		public static int nk_input_is_mouse_down(nk_input i, int id)
		{
			if (i == null) return (int) (nk_false);
			return (int) (i.mouse.buttons[id].down);
		}

		public static int nk_input_is_mouse_pressed(nk_input i, int id)
		{
			nk_mouse_button* b;
			if (i == null) return (int) (nk_false);
			b = (nk_mouse_button*) i.mouse.buttons + id;
			if (((b->down) != 0) && ((b->clicked) != 0)) return (int) (nk_true);
			return (int) (nk_false);
		}

		public static int nk_input_is_mouse_released(nk_input i, int id)
		{
			if (i == null) return (int) (nk_false);
			return ((i.mouse.buttons[id].down == 0) && ((i.mouse.buttons[id].clicked) != 0)) ? 1 : 0;
		}

		public static int nk_input_is_key_pressed(nk_input i, int key)
		{
			nk_key* k;
			if (i == null) return (int) (nk_false);
			k = (nk_key*) i.keyboard.keys + key;
			if ((((k->down) != 0) && ((k->clicked) != 0)) || ((k->down == 0) && ((k->clicked) >= (2)))) return (int) (nk_true);
			return (int) (nk_false);
		}

		public static int nk_input_is_key_released(nk_input i, int key)
		{
			nk_key* k;
			if (i == null) return (int) (nk_false);
			k = (nk_key*) i.keyboard.keys + key;
			if (((k->down == 0) && ((k->clicked) != 0)) || (((k->down) != 0) && ((k->clicked) >= (2)))) return (int) (nk_true);
			return (int) (nk_false);
		}

		public static int nk_input_is_key_down(nk_input i, int key)
		{
			nk_key* k;
			if (i == null) return (int) (nk_false);
			k = (nk_key*) i.keyboard.keys + key;
			if ((k->down) != 0) return (int) (nk_true);
			return (int) (nk_false);
		}

		public static int nk_toggle_behavior(nk_input _in_, nk_rect select, ref uint state, int active)
		{
			if (((state) & NK_WIDGET_STATE_MODIFIED) != 0)
				(state) = (uint) (NK_WIDGET_STATE_INACTIVE | NK_WIDGET_STATE_MODIFIED);
			else (state) = (uint) (NK_WIDGET_STATE_INACTIVE);
			if ((nk_button_behavior(ref state, (nk_rect) (select), _in_, (int) (NK_BUTTON_DEFAULT))) != 0)
			{
				state = (uint) (NK_WIDGET_STATE_ACTIVE);
				active = active != 0 ? 0 : 1;
			}

			if (((state & NK_WIDGET_STATE_HOVER) != 0) && (nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (select)) == 0))
				state |= (uint) (NK_WIDGET_STATE_ENTERED);
			else if ((nk_input_is_mouse_prev_hovering_rect(_in_, (nk_rect) (select))) != 0) state |= (uint) (NK_WIDGET_STATE_LEFT);
			return (int) (active);
		}
	}
}