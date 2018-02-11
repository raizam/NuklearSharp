using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_text_undo_record
		{
			public int where;
			public short insert_length;
			public short delete_length;
			public short char_storage;
		}

		public unsafe partial class nk_text_edit
		{
			public nk_clipboard clip = new nk_clipboard();
			public nk_str _string_ = new nk_str();
			public NkPluginFilter filter;
			public nk_vec2 scrollbar = new nk_vec2();
			public int cursor;
			public int select_start;
			public int select_end;
			public byte mode;
			public byte cursor_at_end_of_line;
			public byte initialized;
			public byte has_preferred_x;
			public byte single_line;
			public byte active;
			public byte padding1;
			public float preferred_x;
			public nk_text_undo_state undo = new nk_text_undo_state();
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_text_find
		{
			public float x;
			public float y;
			public float height;
			public int first_char;
			public int length;
			public int prev_first;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_text_edit_row
		{
			public float x0;
			public float x1;
			public float baseline_y_delta;
			public float ymin;
			public float ymax;
			public int num_chars;
		}

		public static float nk_textedit_get_width(nk_text_edit edit, int line_start, int char_id, nk_user_font font)
		{
			int len = (int) (0);
			char unicode = (char) 0;
			char* str = nk_str_at_const(edit._string_, (int) (line_start + char_id), &unicode, ref len);
			return (float) (font.width((nk_handle) (font.userdata), (float) (font.height), str, (int) (len)));
		}

		public static void nk_textedit_layout_row(nk_text_edit_row* r, nk_text_edit edit, int line_start_id, float row_height,
			nk_user_font font)
		{
			int l = 0;
			int glyphs = (int) (0);
			char unicode;
			char* remaining;
			int len = (int) (nk_str_len_char(edit._string_));
			char* end = nk_str_get_const(edit._string_) + len;
			char* text = nk_str_at_const(edit._string_, (int) (line_start_id), &unicode, ref l);
			nk_vec2 size =
				(nk_vec2)
					(nk_text_calculate_text_bounds(font, text, (int) (end - text), (float) (row_height), &remaining, null, &glyphs,
						(int) (NK_STOP_ON_NEW_LINE)));
			r->x0 = (float) (0.0f);
			r->x1 = (float) (size.x);
			r->baseline_y_delta = (float) (size.y);
			r->ymin = (float) (0.0f);
			r->ymax = (float) (size.y);
			r->num_chars = (int) (glyphs);
		}

		public static int nk_textedit_locate_coord(nk_text_edit edit, float x, float y, nk_user_font font, float row_height)
		{
			nk_text_edit_row r = new nk_text_edit_row();
			int n = (int) (edit._string_.len);
			float base_y = (float) (0);
			float prev_x;
			int i = (int) (0);
			int k;
			r.x0 = (float) (r.x1 = (float) (0));
			r.ymin = (float) (r.ymax = (float) (0));
			r.num_chars = (int) (0);
			while ((i) < (n))
			{
				nk_textedit_layout_row(&r, edit, (int) (i), (float) (row_height), font);
				if (r.num_chars <= 0) return (int) (n);
				if (((i) == (0)) && ((y) < (base_y + r.ymin))) return (int) (0);
				if ((y) < (base_y + r.ymax)) break;
				i += (int) (r.num_chars);
				base_y += (float) (r.baseline_y_delta);
			}
			if ((i) >= (n)) return (int) (n);
			if ((x) < (r.x0)) return (int) (i);
			if ((x) < (r.x1))
			{
				k = (int) (i);
				prev_x = (float) (r.x0);
				for (i = (int) (0); (i) < (r.num_chars); ++i)
				{
					float w = (float) (nk_textedit_get_width(edit, (int) (k), (int) (i), font));
					if ((x) < (prev_x + w))
					{
						if ((x) < (prev_x + w/2)) return (int) (k + i);
						else return (int) (k + i + 1);
					}
					prev_x += (float) (w);
				}
			}

			if ((nk_str_rune_at(edit._string_, (int) (i + r.num_chars - 1))) == ('\n')) return (int) (i + r.num_chars - 1);
			else return (int) (i + r.num_chars);
		}

		public static void nk_textedit_click(nk_text_edit state, float x, float y, nk_user_font font, float row_height)
		{
			state.cursor = (int) (nk_textedit_locate_coord(state, (float) (x), (float) (y), font, (float) (row_height)));
			state.select_start = (int) (state.cursor);
			state.select_end = (int) (state.cursor);
			state.has_preferred_x = (byte) (0);
		}

		public static void nk_textedit_drag(nk_text_edit state, float x, float y, nk_user_font font, float row_height)
		{
			int p = (int) (nk_textedit_locate_coord(state, (float) (x), (float) (y), font, (float) (row_height)));
			if ((state.select_start) == (state.select_end)) state.select_start = (int) (state.cursor);
			state.cursor = (int) (state.select_end = (int) (p));
		}

		public static void nk_textedit_find_charpos(nk_text_find* find, nk_text_edit state, int n, int single_line,
			nk_user_font font, float row_height)
		{
			nk_text_edit_row r = new nk_text_edit_row();
			int prev_start = (int) (0);
			int z = (int) (state._string_.len);
			int i = (int) (0);
			int first;
			nk_zero(&r, (ulong) (sizeof (nk_text_edit_row)));
			if ((n) == (z))
			{
				nk_textedit_layout_row(&r, state, (int) (0), (float) (row_height), font);
				if ((single_line) != 0)
				{
					find->first_char = (int) (0);
					find->length = (int) (z);
				}
				else
				{
					while ((i) < (z))
					{
						prev_start = (int) (i);
						i += (int) (r.num_chars);
						nk_textedit_layout_row(&r, state, (int) (i), (float) (row_height), font);
					}
					find->first_char = (int) (i);
					find->length = (int) (r.num_chars);
				}
				find->x = (float) (r.x1);
				find->y = (float) (r.ymin);
				find->height = (float) (r.ymax - r.ymin);
				find->prev_first = (int) (prev_start);
				return;
			}

			find->y = (float) (0);
			for (;;)
			{
				nk_textedit_layout_row(&r, state, (int) (i), (float) (row_height), font);
				if ((n) < (i + r.num_chars)) break;
				prev_start = (int) (i);
				i += (int) (r.num_chars);
				find->y += (float) (r.baseline_y_delta);
			}
			find->first_char = (int) (first = (int) (i));
			find->length = (int) (r.num_chars);
			find->height = (float) (r.ymax - r.ymin);
			find->prev_first = (int) (prev_start);
			find->x = (float) (r.x0);
			for (i = (int) (0); (first + i) < (n); ++i)
			{
				find->x += (float) (nk_textedit_get_width(state, (int) (first), (int) (i), font));
			}
		}

		public static void nk_textedit_clamp(nk_text_edit state)
		{
			int n = (int) (state._string_.len);
			if (((state).select_start != (state).select_end))
			{
				if ((state.select_start) > (n)) state.select_start = (int) (n);
				if ((state.select_end) > (n)) state.select_end = (int) (n);
				if ((state.select_start) == (state.select_end)) state.cursor = (int) (state.select_start);
			}

			if ((state.cursor) > (n)) state.cursor = (int) (n);
		}

		public static void nk_textedit_delete(nk_text_edit state, int where, int len)
		{
			nk_textedit_makeundo_delete(state, (int) (where), (int) (len));
			nk_str_delete_runes(state._string_, (int) (where), (int) (len));
			state.has_preferred_x = (byte) (0);
		}

		public static void nk_textedit_delete_selection(nk_text_edit state)
		{
			nk_textedit_clamp(state);
			if (((state).select_start != (state).select_end))
			{
				if ((state.select_start) < (state.select_end))
				{
					nk_textedit_delete(state, (int) (state.select_start), (int) (state.select_end - state.select_start));
					state.select_end = (int) (state.cursor = (int) (state.select_start));
				}
				else
				{
					nk_textedit_delete(state, (int) (state.select_end), (int) (state.select_start - state.select_end));
					state.select_start = (int) (state.cursor = (int) (state.select_end));
				}
				state.has_preferred_x = (byte) (0);
			}

		}

		public static void nk_textedit_sortselection(nk_text_edit state)
		{
			if ((state.select_end) < (state.select_start))
			{
				int temp = (int) (state.select_end);
				state.select_end = (int) (state.select_start);
				state.select_start = (int) (temp);
			}

		}

		public static void nk_textedit_move_to_first(nk_text_edit state)
		{
			if (((state).select_start != (state).select_end))
			{
				nk_textedit_sortselection(state);
				state.cursor = (int) (state.select_start);
				state.select_end = (int) (state.select_start);
				state.has_preferred_x = (byte) (0);
			}

		}

		public static void nk_textedit_move_to_last(nk_text_edit state)
		{
			if (((state).select_start != (state).select_end))
			{
				nk_textedit_sortselection(state);
				nk_textedit_clamp(state);
				state.cursor = (int) (state.select_end);
				state.select_start = (int) (state.select_end);
				state.has_preferred_x = (byte) (0);
			}

		}

		public static int nk_is_word_boundary(nk_text_edit state, int idx)
		{
			int len = 0;
			char c;
			if (idx <= 0) return (int) (1);
			if (nk_str_at_rune(state._string_, (int) (idx), &c, ref len) == null) return (int) (1);
			return
				(int)
					((((((((((((c) == (' ')) || ((c) == ('	'))) || ((c) == (0x3000))) || ((c) == (','))) || ((c) == (';'))) ||
					      ((c) == ('('))) || ((c) == (')')) || ((c) == ('{'))) || ((c) == ('}'))) || ((c) == ('['))) || ((c) == (']'))) ||
					 ((c) == ('|'))
						? 1
						: 0);
		}

		public static int nk_textedit_move_to_word_previous(nk_text_edit state)
		{
			int c = (int) (state.cursor - 1);
			while (((c) >= (0)) && (nk_is_word_boundary(state, (int) (c)) == 0))
			{
				--c;
			}
			if ((c) < (0)) c = (int) (0);
			return (int) (c);
		}

		public static int nk_textedit_move_to_word_next(nk_text_edit state)
		{
			int len = (int) (state._string_.len);
			int c = (int) (state.cursor + 1);
			while (((c) < (len)) && (nk_is_word_boundary(state, (int) (c)) == 0))
			{
				++c;
			}
			if ((c) > (len)) c = (int) (len);
			return (int) (c);
		}

		public static void nk_textedit_prep_selection_at_cursor(nk_text_edit state)
		{
			if (!((state).select_start != (state).select_end))
				state.select_start = (int) (state.select_end = (int) (state.cursor));
			else state.cursor = (int) (state.select_end);
		}

		public static int nk_textedit_cut(nk_text_edit state)
		{
			if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) return (int) (0);
			if (((state).select_start != (state).select_end))
			{
				nk_textedit_delete_selection(state);
				state.has_preferred_x = (byte) (0);
				return (int) (1);
			}

			return (int) (0);
		}

		public static int nk_textedit_paste(nk_text_edit state, char* ctext, int len)
		{
			int glyphs;
			char* text = ctext;
			if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) return (int) (0);
			nk_textedit_clamp(state);
			nk_textedit_delete_selection(state);
			glyphs = (int) (nk_utf_len(ctext, (int) (len)));
			if ((nk_str_insert_text_char(state._string_, (int) (state.cursor), text, (int) (len))) != 0)
			{
				nk_textedit_makeundo_insert(state, (int) (state.cursor), (int) (glyphs));
				state.cursor += (int) (len);
				state.has_preferred_x = (byte) (0);
				return (int) (1);
			}

			if ((state.undo.undo_point) != 0) --state.undo.undo_point;
			return (int) (0);
		}

		public static void nk_textedit_text(nk_text_edit state, char* text, int total_len)
		{
			char unicode;
			int glyph_len;
			int text_len = (int) (0);
			if (((text == null) || (total_len == 0)) || ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW))) return;
			glyph_len = (int) (nk_utf_decode(text, &unicode, (int) (total_len)));
			while (((text_len) < (total_len)) && ((glyph_len) != 0))
			{
				if ((unicode) == (127)) goto next;
				if (((unicode) == ('\n')) && ((state.single_line) != 0)) goto next;
				if (((state.filter) != null) && (state.filter(state, unicode) == 0)) goto next;
				if ((!((state).select_start != (state).select_end)) && ((state.cursor) < (state._string_.len)))
				{
					if ((state.mode) == (NK_TEXT_EDIT_MODE_REPLACE))
					{
						nk_textedit_makeundo_replace(state, (int) (state.cursor), (int) (1), (int) (1));
						nk_str_delete_runes(state._string_, (int) (state.cursor), (int) (1));
					}
					if ((nk_str_insert_text_utf8(state._string_, (int) (state.cursor), text + text_len, (int) (1))) != 0)
					{
						++state.cursor;
						state.has_preferred_x = (byte) (0);
					}
				}
				else
				{
					nk_textedit_delete_selection(state);
					if ((nk_str_insert_text_utf8(state._string_, (int) (state.cursor), text + text_len, (int) (1))) != 0)
					{
						nk_textedit_makeundo_insert(state, (int) (state.cursor), (int) (1));
						++state.cursor;
						state.has_preferred_x = (byte) (0);
					}
				}
				next:
				;
				text_len += (int) (glyph_len);
				glyph_len = (int) (nk_utf_decode(text + text_len, &unicode, (int) (total_len - text_len)));
			}
		}

		public static void nk_textedit_key(nk_text_edit state, int key, int shift_mod, nk_user_font font, float row_height)
		{
			retry:
			;
			switch (key)
			{
				case NK_KEY_NONE:
				case NK_KEY_CTRL:
				case NK_KEY_ENTER:
				case NK_KEY_SHIFT:
				case NK_KEY_TAB:
				case NK_KEY_COPY:
				case NK_KEY_CUT:
				case NK_KEY_PASTE:
				case NK_KEY_MAX:
				default:
					break;
				case NK_KEY_TEXT_UNDO:
					nk_textedit_undo(state);
					state.has_preferred_x = (byte) (0);
					break;
				case NK_KEY_TEXT_REDO:
					nk_textedit_redo(state);
					state.has_preferred_x = (byte) (0);
					break;
				case NK_KEY_TEXT_SELECT_ALL:
					nk_textedit_select_all(state);
					state.has_preferred_x = (byte) (0);
					break;
				case NK_KEY_TEXT_INSERT_MODE:
					if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) state.mode = (byte) (NK_TEXT_EDIT_MODE_INSERT);
					break;
				case NK_KEY_TEXT_REPLACE_MODE:
					if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) state.mode = (byte) (NK_TEXT_EDIT_MODE_REPLACE);
					break;
				case NK_KEY_TEXT_RESET_MODE:
					if (((state.mode) == (NK_TEXT_EDIT_MODE_INSERT)) || ((state.mode) == (NK_TEXT_EDIT_MODE_REPLACE)))
						state.mode = (byte) (NK_TEXT_EDIT_MODE_VIEW);
					break;
				case NK_KEY_LEFT:
					if ((shift_mod) != 0)
					{
						nk_textedit_clamp(state);
						nk_textedit_prep_selection_at_cursor(state);
						if ((state.select_end) > (0)) --state.select_end;
						state.cursor = (int) (state.select_end);
						state.has_preferred_x = (byte) (0);
					}
					else
					{
						if (((state).select_start != (state).select_end)) nk_textedit_move_to_first(state);
						else if ((state.cursor) > (0)) --state.cursor;
						state.has_preferred_x = (byte) (0);
					}
					break;
				case NK_KEY_RIGHT:
					if ((shift_mod) != 0)
					{
						nk_textedit_prep_selection_at_cursor(state);
						++state.select_end;
						nk_textedit_clamp(state);
						state.cursor = (int) (state.select_end);
						state.has_preferred_x = (byte) (0);
					}
					else
					{
						if (((state).select_start != (state).select_end)) nk_textedit_move_to_last(state);
						else ++state.cursor;
						nk_textedit_clamp(state);
						state.has_preferred_x = (byte) (0);
					}
					break;
				case NK_KEY_TEXT_WORD_LEFT:
					if ((shift_mod) != 0)
					{
						if (!((state).select_start != (state).select_end)) nk_textedit_prep_selection_at_cursor(state);
						state.cursor = (int) (nk_textedit_move_to_word_previous(state));
						state.select_end = (int) (state.cursor);
						nk_textedit_clamp(state);
					}
					else
					{
						if (((state).select_start != (state).select_end)) nk_textedit_move_to_first(state);
						else
						{
							state.cursor = (int) (nk_textedit_move_to_word_previous(state));
							nk_textedit_clamp(state);
						}
					}
					break;
				case NK_KEY_TEXT_WORD_RIGHT:
					if ((shift_mod) != 0)
					{
						if (!((state).select_start != (state).select_end)) nk_textedit_prep_selection_at_cursor(state);
						state.cursor = (int) (nk_textedit_move_to_word_next(state));
						state.select_end = (int) (state.cursor);
						nk_textedit_clamp(state);
					}
					else
					{
						if (((state).select_start != (state).select_end)) nk_textedit_move_to_last(state);
						else
						{
							state.cursor = (int) (nk_textedit_move_to_word_next(state));
							nk_textedit_clamp(state);
						}
					}
					break;
				case NK_KEY_DOWN:
				{
					nk_text_find find = new nk_text_find();
					nk_text_edit_row row = new nk_text_edit_row();
					int i;
					int sel = (int) (shift_mod);
					if ((state.single_line) != 0)
					{
						key = (int) (NK_KEY_RIGHT);
						goto retry;
					}
					if ((sel) != 0) nk_textedit_prep_selection_at_cursor(state);
					else if (((state).select_start != (state).select_end)) nk_textedit_move_to_last(state);
					nk_textedit_clamp(state);
					nk_textedit_find_charpos(&find, state, (int) (state.cursor), (int) (state.single_line), font, (float) (row_height));
					if ((find.length) != 0)
					{
						float x;
						float goal_x = (float) ((state.has_preferred_x) != 0 ? state.preferred_x : find.x);
						int start = (int) (find.first_char + find.length);
						state.cursor = (int) (start);
						nk_textedit_layout_row(&row, state, (int) (state.cursor), (float) (row_height), font);
						x = (float) (row.x0);
						for (i = (int) (0); ((i) < (row.num_chars)) && ((x) < (row.x1)); ++i)
						{
							float dx = (float) (nk_textedit_get_width(state, (int) (start), (int) (i), font));
							x += (float) (dx);
							if ((x) > (goal_x)) break;
							++state.cursor;
						}
						nk_textedit_clamp(state);
						state.has_preferred_x = (byte) (1);
						state.preferred_x = (float) (goal_x);
						if ((sel) != 0) state.select_end = (int) (state.cursor);
					}
				}
					break;
				case NK_KEY_UP:
				{
					nk_text_find find = new nk_text_find();
					nk_text_edit_row row = new nk_text_edit_row();
					int i;
					int sel = (int) (shift_mod);
					if ((state.single_line) != 0)
					{
						key = (int) (NK_KEY_LEFT);
						goto retry;
					}
					if ((sel) != 0) nk_textedit_prep_selection_at_cursor(state);
					else if (((state).select_start != (state).select_end)) nk_textedit_move_to_first(state);
					nk_textedit_clamp(state);
					nk_textedit_find_charpos(&find, state, (int) (state.cursor), (int) (state.single_line), font, (float) (row_height));
					if (find.prev_first != find.first_char)
					{
						float x;
						float goal_x = (float) ((state.has_preferred_x) != 0 ? state.preferred_x : find.x);
						state.cursor = (int) (find.prev_first);
						nk_textedit_layout_row(&row, state, (int) (state.cursor), (float) (row_height), font);
						x = (float) (row.x0);
						for (i = (int) (0); ((i) < (row.num_chars)) && ((x) < (row.x1)); ++i)
						{
							float dx = (float) (nk_textedit_get_width(state, (int) (find.prev_first), (int) (i), font));
							x += (float) (dx);
							if ((x) > (goal_x)) break;
							++state.cursor;
						}
						nk_textedit_clamp(state);
						state.has_preferred_x = (byte) (1);
						state.preferred_x = (float) (goal_x);
						if ((sel) != 0) state.select_end = (int) (state.cursor);
					}
				}
					break;
				case NK_KEY_DEL:
					if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) break;
					if (((state).select_start != (state).select_end)) nk_textedit_delete_selection(state);
					else
					{
						int n = (int) (state._string_.len);
						if ((state.cursor) < (n)) nk_textedit_delete(state, (int) (state.cursor), (int) (1));
					}
					state.has_preferred_x = (byte) (0);
					break;
				case NK_KEY_BACKSPACE:
					if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) break;
					if (((state).select_start != (state).select_end)) nk_textedit_delete_selection(state);
					else
					{
						nk_textedit_clamp(state);
						if ((state.cursor) > (0))
						{
							nk_textedit_delete(state, (int) (state.cursor - 1), (int) (1));
							--state.cursor;
						}
					}
					state.has_preferred_x = (byte) (0);
					break;
				case NK_KEY_TEXT_START:
					if ((shift_mod) != 0)
					{
						nk_textedit_prep_selection_at_cursor(state);
						state.cursor = (int) (state.select_end = (int) (0));
						state.has_preferred_x = (byte) (0);
					}
					else
					{
						state.cursor = (int) (state.select_start = (int) (state.select_end = (int) (0)));
						state.has_preferred_x = (byte) (0);
					}
					break;
				case NK_KEY_TEXT_END:
					if ((shift_mod) != 0)
					{
						nk_textedit_prep_selection_at_cursor(state);
						state.cursor = (int) (state.select_end = (int) (state._string_.len));
						state.has_preferred_x = (byte) (0);
					}
					else
					{
						state.cursor = (int) (state._string_.len);
						state.select_start = (int) (state.select_end = (int) (0));
						state.has_preferred_x = (byte) (0);
					}
					break;
				case NK_KEY_TEXT_LINE_START:
				{
					if ((shift_mod) != 0)
					{
						nk_text_find find = new nk_text_find();
						nk_textedit_clamp(state);
						nk_textedit_prep_selection_at_cursor(state);
						if (((state._string_.len) != 0) && ((state.cursor) == (state._string_.len))) --state.cursor;
						nk_textedit_find_charpos(&find, state, (int) (state.cursor), (int) (state.single_line), font, (float) (row_height));
						state.cursor = (int) (state.select_end = (int) (find.first_char));
						state.has_preferred_x = (byte) (0);
					}
					else
					{
						nk_text_find find = new nk_text_find();
						if (((state._string_.len) != 0) && ((state.cursor) == (state._string_.len))) --state.cursor;
						nk_textedit_clamp(state);
						nk_textedit_move_to_first(state);
						nk_textedit_find_charpos(&find, state, (int) (state.cursor), (int) (state.single_line), font, (float) (row_height));
						state.cursor = (int) (find.first_char);
						state.has_preferred_x = (byte) (0);
					}
				}
					break;
				case NK_KEY_TEXT_LINE_END:
				{
					if ((shift_mod) != 0)
					{
						nk_text_find find = new nk_text_find();
						nk_textedit_clamp(state);
						nk_textedit_prep_selection_at_cursor(state);
						nk_textedit_find_charpos(&find, state, (int) (state.cursor), (int) (state.single_line), font, (float) (row_height));
						state.has_preferred_x = (byte) (0);
						state.cursor = (int) (find.first_char + find.length);
						if (((find.length) > (0)) && ((nk_str_rune_at(state._string_, (int) (state.cursor - 1))) == ('\n')))
							--state.cursor;
						state.select_end = (int) (state.cursor);
					}
					else
					{
						nk_text_find find = new nk_text_find();
						nk_textedit_clamp(state);
						nk_textedit_move_to_first(state);
						nk_textedit_find_charpos(&find, state, (int) (state.cursor), (int) (state.single_line), font, (float) (row_height));
						state.has_preferred_x = (byte) (0);
						state.cursor = (int) (find.first_char + find.length);
						if (((find.length) > (0)) && ((nk_str_rune_at(state._string_, (int) (state.cursor - 1))) == ('\n')))
							--state.cursor;
					}
				}
					break;
			}

		}

		public static void nk_textedit_flush_redo(nk_text_undo_state state)
		{
			state.redo_point = (short) (99);
			state.redo_char_point = (short) (999);
		}

		public static void nk_textedit_discard_undo(nk_text_undo_state state)
		{
			if ((state.undo_point) > (0))
			{
				if ((state.undo_rec[0].char_storage) >= (0))
				{
					int n = (int) (state.undo_rec[0].insert_length);
					int i;
					state.undo_char_point = ((short) (state.undo_char_point - n));
					nk_memcopy(state.undo_char, (char*) (char*) state.undo_char + n,
						(ulong) ((ulong) (state.undo_char_point)*sizeof (uint)));
					for (i = (int) (0); (i) < (state.undo_point); ++i)
					{
						if ((((nk_text_undo_record*) state.undo_rec + i)->char_storage) >= (0))
							((nk_text_undo_record*) state.undo_rec + i)->char_storage =
								((short) (((nk_text_undo_record*) state.undo_rec + i)->char_storage - n));
					}
				}
				--state.undo_point;
				nk_memcopy(state.undo_rec, (nk_text_undo_record*) state.undo_rec + 1,
					(ulong) ((ulong) (state.undo_point)*(ulong) sizeof (nk_text_undo_record)));
			}

		}

		public static void nk_textedit_discard_redo(nk_text_undo_state state)
		{
			ulong num;
			int k = (int) (99 - 1);
			if (state.redo_point <= k)
			{
				if ((state.undo_rec[k].char_storage) >= (0))
				{
					int n = (int) (state.undo_rec[k].insert_length);
					int i;
					state.redo_char_point = ((short) (state.redo_char_point + n));
					num = ((ulong) (999 - state.redo_char_point));
					nk_memcopy((char*) state.undo_char + state.redo_char_point, (char*) state.undo_char + state.redo_char_point - n,
						(ulong) (num*sizeof (char)));
					for (i = (int) (state.redo_point); (i) < (k); ++i)
					{
						if ((((nk_text_undo_record*) state.undo_rec + i)->char_storage) >= (0))
						{
							((nk_text_undo_record*) state.undo_rec + i)->char_storage =
								((short) (((nk_text_undo_record*) state.undo_rec + i)->char_storage + n));
						}
					}
				}
				++state.redo_point;
				num = ((ulong) (99 - state.redo_point));
				if ((num) != 0)
					nk_memcopy((nk_text_undo_record*) state.undo_rec + state.redo_point - 1,
						(nk_text_undo_record*) state.undo_rec + state.redo_point, (ulong) (num*(ulong) sizeof (nk_text_undo_record)));
			}

		}

		public static nk_text_undo_record* nk_textedit_create_undo_record(nk_text_undo_state state, int numchars)
		{
			nk_textedit_flush_redo(state);
			if ((state.undo_point) == (99)) nk_textedit_discard_undo(state);
			if ((numchars) > (999))
			{
				state.undo_point = (short) (0);
				state.undo_char_point = (short) (0);
				return null;
			}

			while ((state.undo_char_point + numchars) > (999))
			{
				nk_textedit_discard_undo(state);
			}
			return (nk_text_undo_record*) state.undo_rec + (state.undo_point++);
		}

		public static char* nk_textedit_createundo(nk_text_undo_state state, int pos, int insert_len, int delete_len)
		{
			nk_text_undo_record* r = nk_textedit_create_undo_record(state, (int) (insert_len));
			if ((r) == (null)) return null;
			r->where = (int) (pos);
			r->insert_length = ((short) (insert_len));
			r->delete_length = ((short) (delete_len));
			if ((insert_len) == (0))
			{
				r->char_storage = (short) (-1);
				return null;
			}
			else
			{
				r->char_storage = (short) (state.undo_char_point);
				state.undo_char_point = ((short) (state.undo_char_point + insert_len));
				return (char*) state.undo_char + r->char_storage;
			}

		}

		public static void nk_textedit_undo(nk_text_edit state)
		{
			nk_text_undo_state s = state.undo;
			nk_text_undo_record u = new nk_text_undo_record();
			nk_text_undo_record* r;
			if ((s.undo_point) == (0)) return;
			u = (nk_text_undo_record) (s.undo_rec[s.undo_point - 1]);
			r = (nk_text_undo_record*) s.undo_rec + s.redo_point - 1;
			r->char_storage = (short) (-1);
			r->insert_length = (short) (u.delete_length);
			r->delete_length = (short) (u.insert_length);
			r->where = (int) (u.where);
			if ((u.delete_length) != 0)
			{
				if ((s.undo_char_point + u.delete_length) >= (999))
				{
					r->insert_length = (short) (0);
				}
				else
				{
					int i;
					while ((s.undo_char_point + u.delete_length) > (s.redo_char_point))
					{
						nk_textedit_discard_redo(s);
						if ((s.redo_point) == (99)) return;
					}
					r = (nk_text_undo_record*) s.undo_rec + s.redo_point - 1;
					r->char_storage = ((short) (s.redo_char_point - u.delete_length));
					s.redo_char_point = ((short) (s.redo_char_point - u.delete_length));
					for (i = (int) (0); (i) < (u.delete_length); ++i)
					{
						s.undo_char[r->char_storage + i] = (char) (nk_str_rune_at(state._string_, (int) (u.where + i)));
					}
				}
				nk_str_delete_runes(state._string_, (int) (u.where), (int) (u.delete_length));
			}

			if ((u.insert_length) != 0)
			{
				nk_str_insert_text_runes(state._string_, (int) (u.where), (char*) s.undo_char + u.char_storage,
					(int) (u.insert_length));
				s.undo_char_point = ((short) (s.undo_char_point - u.insert_length));
			}

			state.cursor = (int) ((short) (u.where + u.insert_length));
			s.undo_point--;
			s.redo_point--;
		}

		public static void nk_textedit_redo(nk_text_edit state)
		{
			nk_text_undo_state s = state.undo;
			nk_text_undo_record* u;
			nk_text_undo_record r = new nk_text_undo_record();
			if ((s.redo_point) == (99)) return;
			u = (nk_text_undo_record*) s.undo_rec + s.undo_point;
			r = (nk_text_undo_record) (s.undo_rec[s.redo_point]);
			u->delete_length = (short) (r.insert_length);
			u->insert_length = (short) (r.delete_length);
			u->where = (int) (r.where);
			u->char_storage = (short) (-1);
			if ((r.delete_length) != 0)
			{
				if ((s.undo_char_point + u->insert_length) > (s.redo_char_point))
				{
					u->insert_length = (short) (0);
					u->delete_length = (short) (0);
				}
				else
				{
					int i;
					u->char_storage = (short) (s.undo_char_point);
					s.undo_char_point = ((short) (s.undo_char_point + u->insert_length));
					for (i = (int) (0); (i) < (u->insert_length); ++i)
					{
						s.undo_char[u->char_storage + i] = (char) (nk_str_rune_at(state._string_, (int) (u->where + i)));
					}
				}
				nk_str_delete_runes(state._string_, (int) (r.where), (int) (r.delete_length));
			}

			if ((r.insert_length) != 0)
			{
				nk_str_insert_text_runes(state._string_, (int) (r.where), (char*) s.undo_char + r.char_storage,
					(int) (r.insert_length));
			}

			state.cursor = (int) (r.where + r.insert_length);
			s.undo_point++;
			s.redo_point++;
		}

		public static void nk_textedit_makeundo_insert(nk_text_edit state, int where, int length)
		{
			nk_textedit_createundo(state.undo, (int) (where), (int) (0), (int) (length));
		}

		public static void nk_textedit_makeundo_delete(nk_text_edit state, int where, int length)
		{
			int i;
			char* p = nk_textedit_createundo(state.undo, (int) (where), (int) (length), (int) (0));
			if ((p) != null)
			{
				for (i = (int) (0); (i) < (length); ++i)
				{
					p[i] = (char) (nk_str_rune_at(state._string_, (int) (where + i)));
				}
			}

		}

		public static void nk_textedit_makeundo_replace(nk_text_edit state, int where, int old_length, int new_length)
		{
			int i;
			char* p = nk_textedit_createundo(state.undo, (int) (where), (int) (old_length), (int) (new_length));
			if ((p) != null)
			{
				for (i = (int) (0); (i) < (old_length); ++i)
				{
					p[i] = (char) (nk_str_rune_at(state._string_, (int) (where + i)));
				}
			}

		}

		public static void nk_textedit_clear_state(nk_text_edit state, int type, NkPluginFilter filter)
		{
			state.undo.undo_point = (short) (0);
			state.undo.undo_char_point = (short) (0);
			state.undo.redo_point = (short) (99);
			state.undo.redo_char_point = (short) (999);
			state.select_end = (int) (state.select_start = (int) (0));
			state.cursor = (int) (0);
			state.has_preferred_x = (byte) (0);
			state.preferred_x = (float) (0);
			state.cursor_at_end_of_line = (byte) (0);
			state.initialized = (byte) (1);
			state.single_line = ((byte) ((type) == (NK_TEXT_EDIT_SINGLE_LINE) ? 1 : 0));
			state.mode = (byte) (NK_TEXT_EDIT_MODE_VIEW);
			state.filter = filter;
			state.scrollbar = (nk_vec2) (nk_vec2_((float) (0), (float) (0)));
		}

		public static void nk_textedit_init_fixed(nk_text_edit state, void* memory, ulong size)
		{
			if (((memory == null)) || (size == 0)) return;

			nk_textedit_clear_state(state, (int) (NK_TEXT_EDIT_SINGLE_LINE), null);
			nk_str_init_fixed(state._string_, memory, (ulong) (size));
		}

		public static void nk_textedit_init(nk_text_edit state, ulong size)
		{
			if ((state == null)) return;

			nk_textedit_clear_state(state, (int) (NK_TEXT_EDIT_SINGLE_LINE), null);
			nk_str_init(state._string_, (ulong) (size));
		}

		public static void nk_textedit_init_default(nk_text_edit state)
		{
			if (state == null) return;

			nk_textedit_clear_state(state, (int) (NK_TEXT_EDIT_SINGLE_LINE), null);
			nk_str_init_default(state._string_);
		}

		public static void nk_textedit_select_all(nk_text_edit state)
		{
			state.select_start = (int) (0);
			state.select_end = (int) (state._string_.len);
		}

		public static void nk_textedit_free(nk_text_edit state)
		{
			if (state == null) return;
			nk_str_free(state._string_);
		}

		public static int nk_filter_default(nk_text_edit box, char unicode)
		{
			return (int) (nk_true);
		}

		public static int nk_filter_ascii(nk_text_edit box, char unicode)
		{
			if ((unicode) > (128)) return (int) (nk_false);
			else return (int) (nk_true);
		}

		public static int nk_filter_float(nk_text_edit box, char unicode)
		{
			if (((((unicode) < ('0')) || ((unicode) > ('9'))) && (unicode != '.')) && (unicode != '-')) return (int) (nk_false);
			else return (int) (nk_true);
		}

		public static int nk_filter_decimal(nk_text_edit box, char unicode)
		{
			if ((((unicode) < ('0')) || ((unicode) > ('9'))) && (unicode != '-')) return (int) (nk_false);
			else return (int) (nk_true);
		}

		public static int nk_filter_hex(nk_text_edit box, char unicode)
		{
			if (((((unicode) < ('0')) || ((unicode) > ('9'))) && (((unicode) < ('a')) || ((unicode) > ('f')))) &&
			    (((unicode) < ('A')) || ((unicode) > ('F')))) return (int) (nk_false);
			else return (int) (nk_true);
		}

		public static int nk_filter_oct(nk_text_edit box, char unicode)
		{
			if (((unicode) < ('0')) || ((unicode) > ('7'))) return (int) (nk_false);
			else return (int) (nk_true);
		}

		public static int nk_filter_binary(nk_text_edit box, char unicode)
		{
			if ((unicode != '0') && (unicode != '1')) return (int) (nk_false);
			else return (int) (nk_true);
		}
	}
}