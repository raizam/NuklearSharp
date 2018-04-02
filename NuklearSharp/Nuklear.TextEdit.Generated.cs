using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
    public unsafe static partial class Nk
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
            public NkClipboard clip = new NkClipboard();
            public NkStr _string_ = new NkStr();
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
            public NkTextUndoState undo = new NkTextUndoState();
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

        public static float nk_textedit_get_width(nk_text_edit edit, int line_start, int char_id, NkUserFont font)
        {
            fixed (char* str2 = edit._string_.Str)
            {
                char* str = str2 + line_start + char_id;
                return (float)(font.Width((NkHandle)(font.Userdata), (float)(font.Height), str, 1));
            }
        }

        public static void nk_textedit_layout_row(nk_text_edit_row* r, nk_text_edit edit, int line_start_id, float row_height,
            NkUserFont font)
        {
            int glyphs = (int)(0);
            char* remaining;
            int len = (int)(edit._string_.Len);
            fixed (char* str2 = edit._string_.Str)
            {
                char* end = str2 + len;

                char* text = str2 + line_start_id;
                nk_vec2 size =
                    (nk_vec2)
                        (nk_text_calculate_text_bounds(font, text, (int)(end - text), (float)(row_height), &remaining, null, &glyphs,
                            (int)(NK_STOP_ON_NEW_LINE)));
                r->x0 = (float)(0.0f);
                r->x1 = (float)(size.x);
                r->baseline_y_delta = (float)(size.y);
                r->ymin = (float)(0.0f);
                r->ymax = (float)(size.y);
                r->num_chars = (int)(glyphs);
            }
        }

        public static int nk_textedit_locate_coord(nk_text_edit edit, float x, float y, NkUserFont font, float row_height)
        {
            nk_text_edit_row r = new nk_text_edit_row();
            int n = (int)(edit._string_.Len);
            float base_y = (float)(0);
            float prev_x;
            int i = (int)(0);
            int k;
            r.x0 = (float)(r.x1 = (float)(0));
            r.ymin = (float)(r.ymax = (float)(0));
            r.num_chars = (int)(0);
            while ((i) < (n))
            {
                nk_textedit_layout_row(&r, edit, (int)(i), (float)(row_height), font);
                if (r.num_chars <= 0) return (int)(n);
                if (((i) == (0)) && ((y) < (base_y + r.ymin))) return (int)(0);
                if ((y) < (base_y + r.ymax)) break;
                i += (int)(r.num_chars);
                base_y += (float)(r.baseline_y_delta);
            }
            if ((i) >= (n)) return (int)(n);
            if ((x) < (r.x0)) return (int)(i);
            if ((x) < (r.x1))
            {
                k = (int)(i);
                prev_x = (float)(r.x0);
                for (i = (int)(0); (i) < (r.num_chars); ++i)
                {
                    float w = (float)(nk_textedit_get_width(edit, (int)(k), (int)(i), font));
                    if ((x) < (prev_x + w))
                    {
                        if ((x) < (prev_x + w / 2)) return (int)(k + i);
                        else return (int)(k + i + 1);
                    }
                    prev_x += (float)(w);
                }
            }

            if ((edit._string_[i + r.num_chars - 1] == ('\n'))) return (int)(i + r.num_chars - 1);
            else return (int)(i + r.num_chars);
        }

        public static void nk_textedit_click(nk_text_edit state, float x, float y, NkUserFont font, float row_height)
        {
            state.cursor = (int)(nk_textedit_locate_coord(state, (float)(x), (float)(y), font, (float)(row_height)));
            state.select_start = (int)(state.cursor);
            state.select_end = (int)(state.cursor);
            state.has_preferred_x = (byte)(0);
        }

        public static void nk_textedit_drag(nk_text_edit state, float x, float y, NkUserFont font, float row_height)
        {
            int p = (int)(nk_textedit_locate_coord(state, (float)(x), (float)(y), font, (float)(row_height)));
            if ((state.select_start) == (state.select_end)) state.select_start = (int)(state.cursor);
            state.cursor = (int)(state.select_end = (int)(p));
        }

        public static void nk_textedit_find_charpos(nk_text_find* find, nk_text_edit state, int n, int single_line,
            NkUserFont font, float row_height)
        {
            nk_text_edit_row r = new nk_text_edit_row();
            int prev_start = (int)(0);
            int z = (int)(state._string_.Len);
            int i = (int)(0);
            int first;
            nk_zero(&r, (ulong)(sizeof(nk_text_edit_row)));
            if ((n) == (z))
            {
                nk_textedit_layout_row(&r, state, (int)(0), (float)(row_height), font);
                if ((single_line) != 0)
                {
                    find->first_char = (int)(0);
                    find->length = (int)(z);
                }
                else
                {
                    while ((i) < (z))
                    {
                        prev_start = (int)(i);
                        i += (int)(r.num_chars);
                        nk_textedit_layout_row(&r, state, (int)(i), (float)(row_height), font);
                    }
                    find->first_char = (int)(i);
                    find->length = (int)(r.num_chars);
                }
                find->x = (float)(r.x1);
                find->y = (float)(r.ymin);
                find->height = (float)(r.ymax - r.ymin);
                find->prev_first = (int)(prev_start);
                return;
            }

            find->y = (float)(0);
            for (; ; )
            {
                nk_textedit_layout_row(&r, state, (int)(i), (float)(row_height), font);
                if ((n) < (i + r.num_chars)) break;
                prev_start = (int)(i);
                i += (int)(r.num_chars);
                find->y += (float)(r.baseline_y_delta);
            }
            find->first_char = (int)(first = (int)(i));
            find->length = (int)(r.num_chars);
            find->height = (float)(r.ymax - r.ymin);
            find->prev_first = (int)(prev_start);
            find->x = (float)(r.x0);
            for (i = (int)(0); (first + i) < (n); ++i)
            {
                find->x += (float)(nk_textedit_get_width(state, (int)(first), (int)(i), font));
            }
        }

        public static void nk_textedit_clamp(nk_text_edit state)
        {
            int n = (int)(state._string_.Len);
            if (((state).select_start != (state).select_end))
            {
                if ((state.select_start) > (n)) state.select_start = (int)(n);
                if ((state.select_end) > (n)) state.select_end = (int)(n);
                if ((state.select_start) == (state.select_end)) state.cursor = (int)(state.select_start);
            }

            if ((state.cursor) > (n)) state.cursor = (int)(n);
        }

        public static void nk_textedit_delete(nk_text_edit state, int where, int len)
        {
            nk_textedit_makeundo_delete(state, (int)(where), (int)(len));
            state._string_.remove_at((int)(where), (int)(len));
            state.has_preferred_x = (byte)(0);
        }

        public static void nk_textedit_delete_selection(nk_text_edit state)
        {
            nk_textedit_clamp(state);
            if (((state).select_start != (state).select_end))
            {
                if ((state.select_start) < (state.select_end))
                {
                    nk_textedit_delete(state, (int)(state.select_start), (int)(state.select_end - state.select_start));
                    state.select_end = (int)(state.cursor = (int)(state.select_start));
                }
                else
                {
                    nk_textedit_delete(state, (int)(state.select_end), (int)(state.select_start - state.select_end));
                    state.select_start = (int)(state.cursor = (int)(state.select_end));
                }
                state.has_preferred_x = (byte)(0);
            }

        }

        public static void nk_textedit_sortselection(nk_text_edit state)
        {
            if ((state.select_end) < (state.select_start))
            {
                int temp = (int)(state.select_end);
                state.select_end = (int)(state.select_start);
                state.select_start = (int)(temp);
            }

        }

        public static void nk_textedit_move_to_first(nk_text_edit state)
        {
            if (((state).select_start != (state).select_end))
            {
                nk_textedit_sortselection(state);
                state.cursor = (int)(state.select_start);
                state.select_end = (int)(state.select_start);
                state.has_preferred_x = (byte)(0);
            }

        }

        public static void nk_textedit_move_to_last(nk_text_edit state)
        {
            if (((state).select_start != (state).select_end))
            {
                nk_textedit_sortselection(state);
                nk_textedit_clamp(state);
                state.cursor = (int)(state.select_end);
                state.select_start = (int)(state.select_end);
                state.has_preferred_x = (byte)(0);
            }

        }

        public static int nk_is_word_boundary(nk_text_edit state, int idx)
        {
            if (idx <= 0) return (int)(1);
            if (state._string_.Len < idx) return (int)(1);
            char c = state._string_.Str[idx];
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
            int c = (int)(state.cursor - 1);
            while (((c) >= (0)) && (nk_is_word_boundary(state, (int)(c)) == 0))
            {
                --c;
            }
            if ((c) < (0)) c = (int)(0);
            return (int)(c);
        }

        public static int nk_textedit_move_to_word_next(nk_text_edit state)
        {
            int len = (int)(state._string_.Len);
            int c = (int)(state.cursor + 1);
            while (((c) < (len)) && (nk_is_word_boundary(state, (int)(c)) == 0))
            {
                ++c;
            }
            if ((c) > (len)) c = (int)(len);
            return (int)(c);
        }

        public static void nk_textedit_prep_selection_at_cursor(nk_text_edit state)
        {
            if (!((state).select_start != (state).select_end))
                state.select_start = (int)(state.select_end = (int)(state.cursor));
            else state.cursor = (int)(state.select_end);
        }

        public static int nk_textedit_cut(nk_text_edit state)
        {
            if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) return (int)(0);
            if (((state).select_start != (state).select_end))
            {
                nk_textedit_delete_selection(state);
                state.has_preferred_x = (byte)(0);
                return (int)(1);
            }

            return (int)(0);
        }

        public static int nk_textedit_paste(nk_text_edit state, char* ctext, int len)
        {
            int glyphs;
            char* text = ctext;
            if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) return (int)(0);
            nk_textedit_clamp(state);
            nk_textedit_delete_selection(state);
            glyphs = (int)(nk_utf_len(ctext, (int)(len)));
            if ((state._string_.insert_at((int)(state.cursor), text, (int)(len))) != 0)
            {
                nk_textedit_makeundo_insert(state, (int)(state.cursor), (int)(glyphs));
                state.cursor += (int)(len);
                state.has_preferred_x = (byte)(0);
                return (int)(1);
            }

            if ((state.undo.UndoPoint) != 0) --state.undo.UndoPoint;
            return (int)(0);
        }

        public static void nk_textedit_text(nk_text_edit state, char* text, int total_len)
        {
            char unicode;
            int glyph_len;
            int text_len = (int)(0);
            if (((text == null) || (total_len == 0)) || ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW))) return;
            glyph_len = (int)(nk_utf_decode(text, &unicode, (int)(total_len)));
            while (((text_len) < (total_len)) && ((glyph_len) != 0))
            {
                if ((unicode) == (127)) goto next;
                if (((unicode) == ('\n')) && ((state.single_line) != 0)) goto next;
                if (((state.filter) != null) && (state.filter(state, unicode) == 0)) goto next;
                if ((!((state).select_start != (state).select_end)) && ((state.cursor) < (state._string_.Len)))
                {
                    if ((state.mode) == (NK_TEXT_EDIT_MODE_REPLACE))
                    {
                        nk_textedit_makeundo_replace(state, (int)(state.cursor), (int)(1), (int)(1));
                        state._string_.remove_at((int)(state.cursor), (int)(1));
                    }
                    if ((state._string_.insert_at((int)(state.cursor), text + text_len, (int)(1))) != 0)
                    {
                        ++state.cursor;
                        state.has_preferred_x = (byte)(0);
                    }
                }
                else
                {
                    nk_textedit_delete_selection(state);
                    if ((state._string_.insert_at((int)(state.cursor), text + text_len, (int)(1))) != 0)
                    {
                        nk_textedit_makeundo_insert(state, (int)(state.cursor), (int)(1));
                        ++state.cursor;
                        state.has_preferred_x = (byte)(0);
                    }
                }
                next:
                ;
                text_len += (int)(glyph_len);
                glyph_len = (int)(nk_utf_decode(text + text_len, &unicode, (int)(total_len - text_len)));
            }
        }

        public static void nk_textedit_key(nk_text_edit state, int key, int shift_mod, NkUserFont font, float row_height)
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
                    state.has_preferred_x = (byte)(0);
                    break;
                case NK_KEY_TEXT_REDO:
                    nk_textedit_redo(state);
                    state.has_preferred_x = (byte)(0);
                    break;
                case NK_KEY_TEXT_SELECT_ALL:
                    nk_textedit_select_all(state);
                    state.has_preferred_x = (byte)(0);
                    break;
                case NK_KEY_TEXT_INSERT_MODE:
                    if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) state.mode = (byte)(NK_TEXT_EDIT_MODE_INSERT);
                    break;
                case NK_KEY_TEXT_REPLACE_MODE:
                    if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) state.mode = (byte)(NK_TEXT_EDIT_MODE_REPLACE);
                    break;
                case NK_KEY_TEXT_RESET_MODE:
                    if (((state.mode) == (NK_TEXT_EDIT_MODE_INSERT)) || ((state.mode) == (NK_TEXT_EDIT_MODE_REPLACE)))
                        state.mode = (byte)(NK_TEXT_EDIT_MODE_VIEW);
                    break;
                case NK_KEY_LEFT:
                    if ((shift_mod) != 0)
                    {
                        nk_textedit_clamp(state);
                        nk_textedit_prep_selection_at_cursor(state);
                        if ((state.select_end) > (0)) --state.select_end;
                        state.cursor = (int)(state.select_end);
                        state.has_preferred_x = (byte)(0);
                    }
                    else
                    {
                        if (((state).select_start != (state).select_end)) nk_textedit_move_to_first(state);
                        else if ((state.cursor) > (0)) --state.cursor;
                        state.has_preferred_x = (byte)(0);
                    }
                    break;
                case NK_KEY_RIGHT:
                    if ((shift_mod) != 0)
                    {
                        nk_textedit_prep_selection_at_cursor(state);
                        ++state.select_end;
                        nk_textedit_clamp(state);
                        state.cursor = (int)(state.select_end);
                        state.has_preferred_x = (byte)(0);
                    }
                    else
                    {
                        if (((state).select_start != (state).select_end)) nk_textedit_move_to_last(state);
                        else ++state.cursor;
                        nk_textedit_clamp(state);
                        state.has_preferred_x = (byte)(0);
                    }
                    break;
                case NK_KEY_TEXT_WORD_LEFT:
                    if ((shift_mod) != 0)
                    {
                        if (!((state).select_start != (state).select_end)) nk_textedit_prep_selection_at_cursor(state);
                        state.cursor = (int)(nk_textedit_move_to_word_previous(state));
                        state.select_end = (int)(state.cursor);
                        nk_textedit_clamp(state);
                    }
                    else
                    {
                        if (((state).select_start != (state).select_end)) nk_textedit_move_to_first(state);
                        else
                        {
                            state.cursor = (int)(nk_textedit_move_to_word_previous(state));
                            nk_textedit_clamp(state);
                        }
                    }
                    break;
                case NK_KEY_TEXT_WORD_RIGHT:
                    if ((shift_mod) != 0)
                    {
                        if (!((state).select_start != (state).select_end)) nk_textedit_prep_selection_at_cursor(state);
                        state.cursor = (int)(nk_textedit_move_to_word_next(state));
                        state.select_end = (int)(state.cursor);
                        nk_textedit_clamp(state);
                    }
                    else
                    {
                        if (((state).select_start != (state).select_end)) nk_textedit_move_to_last(state);
                        else
                        {
                            state.cursor = (int)(nk_textedit_move_to_word_next(state));
                            nk_textedit_clamp(state);
                        }
                    }
                    break;
                case NK_KEY_DOWN:
                    {
                        nk_text_find find = new nk_text_find();
                        nk_text_edit_row row = new nk_text_edit_row();
                        int i;
                        int sel = (int)(shift_mod);
                        if ((state.single_line) != 0)
                        {
                            key = (int)(NK_KEY_RIGHT);
                            goto retry;
                        }
                        if ((sel) != 0) nk_textedit_prep_selection_at_cursor(state);
                        else if (((state).select_start != (state).select_end)) nk_textedit_move_to_last(state);
                        nk_textedit_clamp(state);
                        nk_textedit_find_charpos(&find, state, (int)(state.cursor), (int)(state.single_line), font, (float)(row_height));
                        if ((find.length) != 0)
                        {
                            float x;
                            float goal_x = (float)((state.has_preferred_x) != 0 ? state.preferred_x : find.x);
                            int start = (int)(find.first_char + find.length);
                            state.cursor = (int)(start);
                            nk_textedit_layout_row(&row, state, (int)(state.cursor), (float)(row_height), font);
                            x = (float)(row.x0);
                            for (i = (int)(0); ((i) < (row.num_chars)) && ((x) < (row.x1)); ++i)
                            {
                                float dx = (float)(nk_textedit_get_width(state, (int)(start), (int)(i), font));
                                x += (float)(dx);
                                if ((x) > (goal_x)) break;
                                ++state.cursor;
                            }
                            nk_textedit_clamp(state);
                            state.has_preferred_x = (byte)(1);
                            state.preferred_x = (float)(goal_x);
                            if ((sel) != 0) state.select_end = (int)(state.cursor);
                        }
                    }
                    break;
                case NK_KEY_UP:
                    {
                        nk_text_find find = new nk_text_find();
                        nk_text_edit_row row = new nk_text_edit_row();
                        int i;
                        int sel = (int)(shift_mod);
                        if ((state.single_line) != 0)
                        {
                            key = (int)(NK_KEY_LEFT);
                            goto retry;
                        }
                        if ((sel) != 0) nk_textedit_prep_selection_at_cursor(state);
                        else if (((state).select_start != (state).select_end)) nk_textedit_move_to_first(state);
                        nk_textedit_clamp(state);
                        nk_textedit_find_charpos(&find, state, (int)(state.cursor), (int)(state.single_line), font, (float)(row_height));
                        if (find.prev_first != find.first_char)
                        {
                            float x;
                            float goal_x = (float)((state.has_preferred_x) != 0 ? state.preferred_x : find.x);
                            state.cursor = (int)(find.prev_first);
                            nk_textedit_layout_row(&row, state, (int)(state.cursor), (float)(row_height), font);
                            x = (float)(row.x0);
                            for (i = (int)(0); ((i) < (row.num_chars)) && ((x) < (row.x1)); ++i)
                            {
                                float dx = (float)(nk_textedit_get_width(state, (int)(find.prev_first), (int)(i), font));
                                x += (float)(dx);
                                if ((x) > (goal_x)) break;
                                ++state.cursor;
                            }
                            nk_textedit_clamp(state);
                            state.has_preferred_x = (byte)(1);
                            state.preferred_x = (float)(goal_x);
                            if ((sel) != 0) state.select_end = (int)(state.cursor);
                        }
                    }
                    break;
                case NK_KEY_DEL:
                    if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) break;
                    if (((state).select_start != (state).select_end)) nk_textedit_delete_selection(state);
                    else
                    {
                        int n = (int)(state._string_.Len);
                        if ((state.cursor) < (n)) nk_textedit_delete(state, (int)(state.cursor), (int)(1));
                    }
                    state.has_preferred_x = (byte)(0);
                    break;
                case NK_KEY_BACKSPACE:
                    if ((state.mode) == (NK_TEXT_EDIT_MODE_VIEW)) break;
                    if (((state).select_start != (state).select_end)) nk_textedit_delete_selection(state);
                    else
                    {
                        nk_textedit_clamp(state);
                        if ((state.cursor) > (0))
                        {
                            nk_textedit_delete(state, (int)(state.cursor - 1), (int)(1));
                            --state.cursor;
                        }
                    }
                    state.has_preferred_x = (byte)(0);
                    break;
                case NK_KEY_TEXT_START:
                    if ((shift_mod) != 0)
                    {
                        nk_textedit_prep_selection_at_cursor(state);
                        state.cursor = (int)(state.select_end = (int)(0));
                        state.has_preferred_x = (byte)(0);
                    }
                    else
                    {
                        state.cursor = (int)(state.select_start = (int)(state.select_end = (int)(0)));
                        state.has_preferred_x = (byte)(0);
                    }
                    break;
                case NK_KEY_TEXT_END:
                    if ((shift_mod) != 0)
                    {
                        nk_textedit_prep_selection_at_cursor(state);
                        state.cursor = (int)(state.select_end = (int)(state._string_.Len));
                        state.has_preferred_x = (byte)(0);
                    }
                    else
                    {
                        state.cursor = (int)(state._string_.Len);
                        state.select_start = (int)(state.select_end = (int)(0));
                        state.has_preferred_x = (byte)(0);
                    }
                    break;
                case NK_KEY_TEXT_LINE_START:
                    {
                        if ((shift_mod) != 0)
                        {
                            nk_text_find find = new nk_text_find();
                            nk_textedit_clamp(state);
                            nk_textedit_prep_selection_at_cursor(state);
                            if (((state._string_.Len) != 0) && ((state.cursor) == (state._string_.Len))) --state.cursor;
                            nk_textedit_find_charpos(&find, state, (int)(state.cursor), (int)(state.single_line), font, (float)(row_height));
                            state.cursor = (int)(state.select_end = (int)(find.first_char));
                            state.has_preferred_x = (byte)(0);
                        }
                        else
                        {
                            nk_text_find find = new nk_text_find();
                            if (((state._string_.Len) != 0) && ((state.cursor) == (state._string_.Len))) --state.cursor;
                            nk_textedit_clamp(state);
                            nk_textedit_move_to_first(state);
                            nk_textedit_find_charpos(&find, state, (int)(state.cursor), (int)(state.single_line), font, (float)(row_height));
                            state.cursor = (int)(find.first_char);
                            state.has_preferred_x = (byte)(0);
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
                            nk_textedit_find_charpos(&find, state, (int)(state.cursor), (int)(state.single_line), font, (float)(row_height));
                            state.has_preferred_x = (byte)(0);
                            state.cursor = (int)(find.first_char + find.length);
                            if (((find.length) > (0)) && ((state._string_[(int)(state.cursor - 1)]) == ('\n')))
                                --state.cursor;
                            state.select_end = (int)(state.cursor);
                        }
                        else
                        {
                            nk_text_find find = new nk_text_find();
                            nk_textedit_clamp(state);
                            nk_textedit_move_to_first(state);
                            nk_textedit_find_charpos(&find, state, (int)(state.cursor), (int)(state.single_line), font, (float)(row_height));
                            state.has_preferred_x = (byte)(0);
                            state.cursor = (int)(find.first_char + find.length);
                            if (((find.length) > (0)) && ((state._string_[(int)(state.cursor - 1)]) == ('\n')))
                                --state.cursor;
                        }
                    }
                    break;
            }

        }

        public static void nk_textedit_flush_redo(NkTextUndoState state)
        {
            state.RedoPoint = (short)(99);
            state.RedoCharPoint = (short)(999);
        }

        public static void nk_textedit_discard_undo(NkTextUndoState state)
        {
            if ((state.UndoPoint) > (0))
            {
                if ((state.UndoRec[0].char_storage) >= (0))
                {
                    int n = (int)(state.UndoRec[0].insert_length);
                    int i;
                    state.UndoCharPoint = ((short)(state.UndoCharPoint - n));
                    nk_memcopy(state.UndoChar, (char*)(char*)state.UndoChar + n,
                        (ulong)((ulong)(state.UndoCharPoint) * sizeof(uint)));
                    for (i = (int)(0); (i) < (state.UndoPoint); ++i)
                    {
                        if ((((nk_text_undo_record*)state.UndoRec + i)->char_storage) >= (0))
                            ((nk_text_undo_record*)state.UndoRec + i)->char_storage =
                                ((short)(((nk_text_undo_record*)state.UndoRec + i)->char_storage - n));
                    }
                }
                --state.UndoPoint;
                nk_memcopy(state.UndoRec, (nk_text_undo_record*)state.UndoRec + 1,
                    (ulong)((ulong)(state.UndoPoint) * (ulong)sizeof(nk_text_undo_record)));
            }

        }

        public static void nk_textedit_discard_redo(NkTextUndoState state)
        {
            ulong num;
            int k = (int)(99 - 1);
            if (state.RedoPoint <= k)
            {
                if ((state.UndoRec[k].char_storage) >= (0))
                {
                    int n = (int)(state.UndoRec[k].insert_length);
                    int i;
                    state.RedoCharPoint = ((short)(state.RedoCharPoint + n));
                    num = ((ulong)(999 - state.RedoCharPoint));
                    nk_memcopy((char*)state.UndoChar + state.RedoCharPoint, (char*)state.UndoChar + state.RedoCharPoint - n,
                        (ulong)(num * sizeof(char)));
                    for (i = (int)(state.RedoPoint); (i) < (k); ++i)
                    {
                        if ((((nk_text_undo_record*)state.UndoRec + i)->char_storage) >= (0))
                        {
                            ((nk_text_undo_record*)state.UndoRec + i)->char_storage =
                                ((short)(((nk_text_undo_record*)state.UndoRec + i)->char_storage + n));
                        }
                    }
                }
                ++state.RedoPoint;
                num = ((ulong)(99 - state.RedoPoint));
                if ((num) != 0)
                    nk_memcopy((nk_text_undo_record*)state.UndoRec + state.RedoPoint - 1,
                        (nk_text_undo_record*)state.UndoRec + state.RedoPoint, (ulong)(num * (ulong)sizeof(nk_text_undo_record)));
            }

        }

        public static nk_text_undo_record* nk_textedit_create_undo_record(NkTextUndoState state, int numchars)
        {
            nk_textedit_flush_redo(state);
            if ((state.UndoPoint) == (99)) nk_textedit_discard_undo(state);
            if ((numchars) > (999))
            {
                state.UndoPoint = (short)(0);
                state.UndoCharPoint = (short)(0);
                return null;
            }

            while ((state.UndoCharPoint + numchars) > (999))
            {
                nk_textedit_discard_undo(state);
            }
            return (nk_text_undo_record*)state.UndoRec + (state.UndoPoint++);
        }

        public static char* nk_textedit_createundo(NkTextUndoState state, int pos, int insert_len, int delete_len)
        {
            nk_text_undo_record* r = nk_textedit_create_undo_record(state, (int)(insert_len));
            if ((r) == (null)) return null;
            r->where = (int)(pos);
            r->insert_length = ((short)(insert_len));
            r->delete_length = ((short)(delete_len));
            if ((insert_len) == (0))
            {
                r->char_storage = (short)(-1);
                return null;
            }
            else
            {
                r->char_storage = (short)(state.UndoCharPoint);
                state.UndoCharPoint = ((short)(state.UndoCharPoint + insert_len));
                return (char*)state.UndoChar + r->char_storage;
            }

        }

        public static void nk_textedit_undo(nk_text_edit state)
        {
            NkTextUndoState s = state.undo;
            nk_text_undo_record u = new nk_text_undo_record();
            nk_text_undo_record* r;
            if ((s.UndoPoint) == (0)) return;
            u = (nk_text_undo_record)(s.UndoRec[s.UndoPoint - 1]);
            r = (nk_text_undo_record*)s.UndoRec + s.RedoPoint - 1;
            r->char_storage = (short)(-1);
            r->insert_length = (short)(u.delete_length);
            r->delete_length = (short)(u.insert_length);
            r->where = (int)(u.where);
            if ((u.delete_length) != 0)
            {
                if ((s.UndoCharPoint + u.delete_length) >= (999))
                {
                    r->insert_length = (short)(0);
                }
                else
                {
                    int i;
                    while ((s.UndoCharPoint + u.delete_length) > (s.RedoCharPoint))
                    {
                        nk_textedit_discard_redo(s);
                        if ((s.RedoPoint) == (99)) return;
                    }
                    r = (nk_text_undo_record*)s.UndoRec + s.RedoPoint - 1;
                    r->char_storage = ((short)(s.RedoCharPoint - u.delete_length));
                    s.RedoCharPoint = ((short)(s.RedoCharPoint - u.delete_length));
                    for (i = (int)(0); (i) < (u.delete_length); ++i)
                    {
                        s.UndoChar[r->char_storage + i] = (char)(state._string_[(int)(u.where + i)]);
                    }
                }
                state._string_.remove_at((int)(u.where), (int)(u.delete_length));
            }

            if ((u.insert_length) != 0)
            {
                state._string_.insert_at((int)(u.where), (char*)s.UndoChar + u.char_storage,
                    (int)(u.insert_length));
                s.UndoCharPoint = ((short)(s.UndoCharPoint - u.insert_length));
            }

            state.cursor = (int)((short)(u.where + u.insert_length));
            s.UndoPoint--;
            s.RedoPoint--;
        }

        public static void nk_textedit_redo(nk_text_edit state)
        {
            NkTextUndoState s = state.undo;
            nk_text_undo_record* u;
            nk_text_undo_record r = new nk_text_undo_record();
            if ((s.RedoPoint) == (99)) return;
            u = (nk_text_undo_record*)s.UndoRec + s.UndoPoint;
            r = (nk_text_undo_record)(s.UndoRec[s.RedoPoint]);
            u->delete_length = (short)(r.insert_length);
            u->insert_length = (short)(r.delete_length);
            u->where = (int)(r.where);
            u->char_storage = (short)(-1);
            if ((r.delete_length) != 0)
            {
                if ((s.UndoCharPoint + u->insert_length) > (s.RedoCharPoint))
                {
                    u->insert_length = (short)(0);
                    u->delete_length = (short)(0);
                }
                else
                {
                    int i;
                    u->char_storage = (short)(s.UndoCharPoint);
                    s.UndoCharPoint = ((short)(s.UndoCharPoint + u->insert_length));
                    for (i = (int)(0); (i) < (u->insert_length); ++i)
                    {
                        s.UndoChar[u->char_storage + i] = (char)(state._string_[(int)(u->where + i)]);
                    }
                }
                state._string_.remove_at((int)(r.where), (int)(r.delete_length));
            }

            if ((r.insert_length) != 0)
            {
                state._string_.insert_at((int)(r.where), (char*)s.UndoChar + r.char_storage,
                    (int)(r.insert_length));
            }

            state.cursor = (int)(r.where + r.insert_length);
            s.UndoPoint++;
            s.RedoPoint++;
        }

        public static void nk_textedit_makeundo_insert(nk_text_edit state, int where, int length)
        {
            nk_textedit_createundo(state.undo, (int)(where), (int)(0), (int)(length));
        }

        public static void nk_textedit_makeundo_delete(nk_text_edit state, int where, int length)
        {
            int i;
            char* p = nk_textedit_createundo(state.undo, (int)(where), (int)(length), (int)(0));
            if ((p) != null)
            {
                for (i = (int)(0); (i) < (length); ++i)
                {
                    p[i] = (char)(state._string_[(int)(where + i)]);
                }
            }
        }

        public static void nk_textedit_makeundo_replace(nk_text_edit state, int where, int old_length, int new_length)
        {
            int i;
            char* p = nk_textedit_createundo(state.undo, (int)(where), (int)(old_length), (int)(new_length));
            if ((p) != null)
            {
                for (i = (int)(0); (i) < (old_length); ++i)
                {
                    p[i] = (char)(state._string_[(int)(where + i)]);
                }
            }
        }

        public static void nk_textedit_clear_state(nk_text_edit state, int type, NkPluginFilter filter)
        {
            state.undo.UndoPoint = (short)(0);
            state.undo.UndoCharPoint = (short)(0);
            state.undo.RedoPoint = (short)(99);
            state.undo.RedoCharPoint = (short)(999);
            state.select_end = (int)(state.select_start = (int)(0));
            state.cursor = (int)(0);
            state.has_preferred_x = (byte)(0);
            state.preferred_x = (float)(0);
            state.cursor_at_end_of_line = (byte)(0);
            state.initialized = (byte)(1);
            state.single_line = ((byte)((type) == (NK_TEXT_EDIT_SINGLE_LINE) ? 1 : 0));
            state.mode = (byte)(NK_TEXT_EDIT_MODE_VIEW);
            state.filter = filter;
            state.scrollbar = (nk_vec2)(nk_vec2_((float)(0), (float)(0)));
        }

        public static void nk_textedit_init_fixed(nk_text_edit state, void* memory, ulong size)
        {
            if (((memory == null)) || (size == 0)) return;

            nk_textedit_clear_state(state, (int)(NK_TEXT_EDIT_SINGLE_LINE), null);
        }

        public static void nk_textedit_init(nk_text_edit state, ulong size)
        {
            if ((state == null)) return;

            nk_textedit_clear_state(state, (int)(NK_TEXT_EDIT_SINGLE_LINE), null);
        }

        public static void nk_textedit_init_default(nk_text_edit state)
        {
            if (state == null) return;

            nk_textedit_clear_state(state, (int)(NK_TEXT_EDIT_SINGLE_LINE), null);
        }

        public static void nk_textedit_select_all(nk_text_edit state)
        {
            state.select_start = (int)(0);
            state.select_end = (int)(state._string_.Len);
        }

        public static void nk_textedit_free(nk_text_edit state)
        {
            if (state == null) return;
            state._string_.Str = string.Empty;
        }

        public static int nk_filter_default(nk_text_edit box, char unicode)
        {
            return (int)(nk_true);
        }

        public static int nk_filter_ascii(nk_text_edit box, char unicode)
        {
            if ((unicode) > (128)) return (int)(nk_false);
            else return (int)(nk_true);
        }

        public static int nk_filter_float(nk_text_edit box, char unicode)
        {
            if (((((unicode) < ('0')) || ((unicode) > ('9'))) && (unicode != '.')) && (unicode != '-')) return (int)(nk_false);
            else return (int)(nk_true);
        }

        public static int nk_filter_decimal(nk_text_edit box, char unicode)
        {
            if ((((unicode) < ('0')) || ((unicode) > ('9'))) && (unicode != '-')) return (int)(nk_false);
            else return (int)(nk_true);
        }

        public static int nk_filter_hex(nk_text_edit box, char unicode)
        {
            if (((((unicode) < ('0')) || ((unicode) > ('9'))) && (((unicode) < ('a')) || ((unicode) > ('f')))) &&
                (((unicode) < ('A')) || ((unicode) > ('F')))) return (int)(nk_false);
            else return (int)(nk_true);
        }

        public static int nk_filter_oct(nk_text_edit box, char unicode)
        {
            if (((unicode) < ('0')) || ((unicode) > ('7'))) return (int)(nk_false);
            else return (int)(nk_true);
        }

        public static int nk_filter_binary(nk_text_edit box, char unicode)
        {
            if ((unicode != '0') && (unicode != '1')) return (int)(nk_false);
            else return (int)(nk_true);
        }
    }
}