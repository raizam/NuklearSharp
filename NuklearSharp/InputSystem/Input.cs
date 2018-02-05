using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Input
	{
		public Keyboard keyboard = new Keyboard();
		public Mouse mouse = new Mouse();

		public int HasMouseClick(int id)
		{
			MouseButton* btn;
			if (this== null) return (int)(Nuklear.nk_false);
			btn = &this.mouse.buttons[id];
			return (int)((((btn->clicked) != 0) && ((btn->down) == (Nuklear.nk_false)))?Nuklear.nk_true:Nuklear.nk_false);
		}

		public int HasMouseClickInRect(int id, Rect b)
		{
			MouseButton* btn;
			if (this== null) return (int)(Nuklear.nk_false);
			btn = &this.mouse.buttons[id];
			if (!((((b.x) <= (btn->clicked_pos.x)) && ((btn->clicked_pos.x) < (b.x + b.w))) && (((b.y) <= (btn->clicked_pos.y)) && ((btn->clicked_pos.y) < (b.y + b.h))))) return (int)(Nuklear.nk_false);
			return (int)(Nuklear.nk_true);
		}

		public int HasMouseClickDownInRect(int id, Rect b, int down)
		{
			MouseButton* btn;
			if (this== null) return (int)(Nuklear.nk_false);
			btn = &this.mouse.buttons[id];
			return (int)(((HasMouseClickInRect((int)(id), (Rect)(b))) != 0) && ((btn->down) == (down))?1:0);
		}

		public int IsMouseClickInRect(int id, Rect b)
		{
			MouseButton* btn;
			if (this== null) return (int)(Nuklear.nk_false);
			btn = &this.mouse.buttons[id];
			return (int)((((HasMouseClickDownInRect((int)(id), (Rect)(b), (int)(Nuklear.nk_false))) != 0) && ((btn->clicked) != 0))?Nuklear.nk_true:Nuklear.nk_false);
		}

		public int IsMouseClickDownInRect(int id, Rect b, int down)
		{
			MouseButton* btn;
			if (this== null) return (int)(Nuklear.nk_false);
			btn = &this.mouse.buttons[id];
			return (int)((((HasMouseClickDownInRect((int)(id), (Rect)(b), (int)(down))) != 0) && ((btn->clicked) != 0))?Nuklear.nk_true:Nuklear.nk_false);
		}

		public int AnyMouseClickInRect(Rect b)
		{
			int i;int down = (int)(0);
			for (i = (int)(0); (i) < (Nuklear.NK_BUTTON_MAX); ++i) {down = (int)(((down) != 0) || ((IsMouseClickInRect((int)(i), (Rect)(b))) != 0)?1:0);}
			return (int)(down);
		}

		public int IsMouseHoveringRect(Rect rect)
		{
			if (this== null) return (int)(Nuklear.nk_false);
			return (int)((((rect.x) <= (this.mouse.pos.x)) && ((this.mouse.pos.x) < (rect.x + rect.w))) && (((rect.y) <= (this.mouse.pos.y)) && ((this.mouse.pos.y) < (rect.y + rect.h))));
		}

		public int IsMousePrevHoveringRect(Rect rect)
		{
			if (this== null) return (int)(Nuklear.nk_false);
			return (int)((((rect.x) <= (this.mouse.prev.x)) && ((this.mouse.prev.x) < (rect.x + rect.w))) && (((rect.y) <= (this.mouse.prev.y)) && ((this.mouse.prev.y) < (rect.y + rect.h))));
		}

		public int MouseClicked(int id, Rect rect)
		{
			if (this== null) return (int)(Nuklear.nk_false);
			if (IsMouseHoveringRect((Rect)(rect))== 0) return (int)(Nuklear.nk_false);
			return (int)(IsMouseClickInRect((int)(id), (Rect)(rect)));
		}

		public int IsMouseDown(int id)
		{
			if (this== null) return (int)(Nuklear.nk_false);
			return (int)(this.mouse.buttons[id].down);
		}

		public int IsMousePressed(int id)
		{
			MouseButton* b;
			if (this== null) return (int)(Nuklear.nk_false);
			b = &this.mouse.buttons[id];
			if (((b->down) != 0) && ((b->clicked) != 0)) return (int)(Nuklear.nk_true);
			return (int)(Nuklear.nk_false);
		}

		public int IsMouseReleased(int id)
		{
			if (this== null) return (int)(Nuklear.nk_false);
			return (int)((this.mouse.buttons[id].down== 0) && ((this.mouse.buttons[id].clicked) != 0));
		}

		public int IsKeyPressed(int key)
		{
			Key* k;
			if (this== null) return (int)(Nuklear.nk_false);
			k = &this.keyboard.keys[key];
			if ((((k->down) != 0) && ((k->clicked) != 0)) || ((k->down== 0) && ((k->clicked) >= (2)))) return (int)(Nuklear.nk_true);
			return (int)(Nuklear.nk_false);
		}

		public int IsKeyReleased(int key)
		{
			Key* k;
			if (this== null) return (int)(Nuklear.nk_false);
			k = &this.keyboard.keys[key];
			if (((k->down== 0) && ((k->clicked) != 0)) || (((k->down) != 0) && ((k->clicked) >= (2)))) return (int)(Nuklear.nk_true);
			return (int)(Nuklear.nk_false);
		}

		public int IsKeyDown(int key)
		{
			Key* k;
			if (this== null) return (int)(Nuklear.nk_false);
			k = &this.keyboard.keys[key];
			if ((k->down) != 0) return (int)(Nuklear.nk_true);
			return (int)(Nuklear.nk_false);
		}

		public int ToggleBehavior(Rect select, ref uint state, int active)
		{
			if (((state) & Nuklear.NK_WIDGET_STATE_MODIFIED) != 0) (state) = (uint)(Nuklear.NK_WIDGET_STATE_INACTIVE | Nuklear.NK_WIDGET_STATE_MODIFIED); else (state) = (uint)(Nuklear.NK_WIDGET_STATE_INACTIVE);
			if ((Nuklear.ButtonBehavior(ref state, (Rect)(select), this, (int)(Nuklear.NK_BUTTON_DEFAULT))) != 0) {
state = (uint)(Nuklear.NK_WIDGET_STATE_ACTIVE);active = active != 0?0:1;}

			if (((state & Nuklear.NK_WIDGET_STATE_HOVER) != 0) && (IsMousePrevHoveringRect((Rect)(select))== 0)) state |= (uint)(Nuklear.NK_WIDGET_STATE_ENTERED); else if ((IsMousePrevHoveringRect((Rect)(select))) != 0) state |= (uint)(Nuklear.NK_WIDGET_STATE_LEFT);
			return (int)(active);
		}

	}
}
