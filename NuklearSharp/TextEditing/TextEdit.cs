using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class TextEdit
	{
		public Clipboard clip = new Clipboard();
		public Str _string_ = new Str();
		public int (const nk_text_edit *, unsigned int)* filter;
		public Vec2 scrollbar = new Vec2();
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
		public TextUndoState undo = new TextUndoState();

		public float TexteditGetWidth(int line_start, int char_id, UserFont font)
		{
			int len = (int)(0);
			char unicode = (char)0;
			char* str = this._string_.AtConst((int)(line_start + char_id), &unicode, ref len);
			return (float)(font.width((Handle)(font.userdata), (float)(font.height), str, (int)(len)));
		}

		public int TexteditLocateCoord(float x, float y, UserFont font, float row_height)
		{
			TextEditRow r =  new TextEditRow();
			int n = (int)(this._string_.len);
			float base_y = (float)(0);float prev_x;
			int i = (int)(0);int k;
			r.x0 = (float)(r.x1 = (float)(0));
			r.ymin = (float)(r.ymax = (float)(0));
			r.num_chars = (int)(0);
			while ((i) < (n)) {
r.TexteditLayoutRow(this, (int)(i), (float)(row_height), font);if (r.num_chars <= 0) return (int)(n);if (((i) == (0)) && ((y) < (base_y + r.ymin))) return (int)(0);if ((y) < (base_y + r.ymax)) break;i += (int)(r.num_chars);base_y += (float)(r.baseline_y_delta);}
			if ((i) >= (n)) return (int)(n);
			if ((x) < (r.x0)) return (int)(i);
			if ((x) < (r.x1)) {
k = (int)(i);prev_x = (float)(r.x0);for (i = (int)(0); (i) < (r.num_chars); ++i) {
float w = (float)(TexteditGetWidth((int)(k), (int)(i), font));if ((x) < (prev_x + w)) {
if ((x) < (prev_x + w / 2)) return (int)(k + i); else return (int)(k + i + 1);}
prev_x += (float)(w);}}

			if ((this._string_.RuneAt((int)(i + r.num_chars - 1))) == ('\n')) return (int)(i + r.num_chars - 1); else return (int)(i + r.num_chars);
		}

		public void TexteditClick(float x, float y, UserFont font, float row_height)
		{
			this.cursor = (int)(TexteditLocateCoord((float)(x), (float)(y), font, (float)(row_height)));
			this.select_start = (int)(this.cursor);
			this.select_end = (int)(this.cursor);
			this.has_preferred_x = (byte)(0);
		}

		public void TexteditDrag(float x, float y, UserFont font, float row_height)
		{
			int p = (int)(TexteditLocateCoord((float)(x), (float)(y), font, (float)(row_height)));
			if ((this.select_start) == (this.select_end)) this.select_start = (int)(this.cursor);
			this.cursor = (int)(this.select_end = (int)(p));
		}

		public void TexteditClamp()
		{
			int n = (int)(this._string_.len);
			if (((this).select_start != (this).select_end)) {
if ((this.select_start) > (n)) this.select_start = (int)(n);if ((this.select_end) > (n)) this.select_end = (int)(n);if ((this.select_start) == (this.select_end)) this.cursor = (int)(this.select_start);}

			if ((this.cursor) > (n)) this.cursor = (int)(n);
		}

		public void TexteditDelete(int where, int len)
		{
			TexteditMakeundoDelete((int)(where), (int)(len));
			this._string_.DeleteRunes((int)(where), (int)(len));
			this.has_preferred_x = (byte)(0);
		}

		public void TexteditDeleteSelection()
		{
			TexteditClamp();
			if (((this).select_start != (this).select_end)) {
if ((this.select_start) < (this.select_end)) {
TexteditDelete((int)(this.select_start), (int)(this.select_end - this.select_start));this.select_end = (int)(this.cursor = (int)(this.select_start));}
 else {
TexteditDelete((int)(this.select_end), (int)(this.select_start - this.select_end));this.select_start = (int)(this.cursor = (int)(this.select_end));}
this.has_preferred_x = (byte)(0);}

		}

		public void TexteditSortselection()
		{
			if ((this.select_end) < (this.select_start)) {
int temp = (int)(this.select_end);this.select_end = (int)(this.select_start);this.select_start = (int)(temp);}

		}

		public void TexteditMoveToFirst()
		{
			if (((this).select_start != (this).select_end)) {
TexteditSortselection();this.cursor = (int)(this.select_start);this.select_end = (int)(this.select_start);this.has_preferred_x = (byte)(0);}

		}

		public void TexteditMoveToLast()
		{
			if (((this).select_start != (this).select_end)) {
TexteditSortselection();TexteditClamp();this.cursor = (int)(this.select_end);this.select_start = (int)(this.select_end);this.has_preferred_x = (byte)(0);}

		}

		public int IsWordBoundary(int idx)
		{
			int len;
			char c;
			if (idx <= 0) return (int)(1);
			if (this._string_.AtRune((int)(idx), &c, ref len)== null) return (int)(1);
			return (int)((((((((((((c) == (' ')) || ((c) == ('	'))) || ((c) == (0x3000))) || ((c) == (','))) || ((c) == (';'))) || ((c) == ('('))) || ((c) == (')')) || ((c) == ('{'))) || ((c) == ('}'))) || ((c) == ('['))) || ((c) == (']'))) || ((c) == ('|'))?1:0);
		}

		public int TexteditMoveToWordPrevious()
		{
			int c = (int)(this.cursor - 1);
			while (((c) >= (0)) && (IsWordBoundary((int)(c))== 0)) {--c;}
			if ((c) < (0)) c = (int)(0);
			return (int)(c);
		}

		public int TexteditMoveToWordNext()
		{
			int len = (int)(this._string_.len);
			int c = (int)(this.cursor + 1);
			while (((c) < (len)) && (IsWordBoundary((int)(c))== 0)) {++c;}
			if ((c) > (len)) c = (int)(len);
			return (int)(c);
		}

		public void TexteditPrepSelectionAtCursor()
		{
			if (!((this).select_start != (this).select_end)) this.select_start = (int)(this.select_end = (int)(this.cursor)); else this.cursor = (int)(this.select_end);
		}

		public int TexteditCut()
		{
			if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW)) return (int)(0);
			if (((this).select_start != (this).select_end)) {
TexteditDeleteSelection();this.has_preferred_x = (byte)(0);return (int)(1);}

			return (int)(0);
		}

		public int TexteditPaste(char* ctext, int len)
		{
			int glyphs;
			char* text = ctext;
			if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW)) return (int)(0);
			TexteditClamp();
			TexteditDeleteSelection();
			glyphs = (int)(Nuklear.UtfLen(ctext, (int)(len)));
			if ((this._string_.InsertTextChar((int)(this.cursor), text, (int)(len))) != 0) {
TexteditMakeundoInsert((int)(this.cursor), (int)(glyphs));this.cursor += (int)(len);this.has_preferred_x = (byte)(0);return (int)(1);}

			if ((this.undo.undo_point) != 0) --this.undo.undo_point;
			return (int)(0);
		}

		public void TexteditText(char* text, int total_len)
		{
			char unicode;
			int glyph_len;
			int text_len = (int)(0);
			if (((text== null) || (total_len== 0)) || ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW))) return;
			glyph_len = (int)(Nuklear.UtfDecode(text, &unicode, (int)(total_len)));
			while (((text_len) < (total_len)) && ((glyph_len) != 0)) {
if ((unicode) == (127)) goto next;if (((unicode) == ('\n')) && ((this.single_line) != 0)) goto next;if (((this.filter) != null) && (this.filter(this, unicode)== 0)) goto next;if ((!((this).select_start != (this).select_end)) && ((this.cursor) < (this._string_.len))) {
if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_REPLACE)) {
TexteditMakeundoReplace((int)(this.cursor), (int)(1), (int)(1));this._string_.DeleteRunes((int)(this.cursor), (int)(1));}
if ((this._string_.InsertTextUtf8((int)(this.cursor), text + text_len, (int)(1))) != 0) {
++this.cursor;this.has_preferred_x = (byte)(0);}
}
 else {
TexteditDeleteSelection();if ((this._string_.InsertTextUtf8((int)(this.cursor), text + text_len, (int)(1))) != 0) {
TexteditMakeundoInsert((int)(this.cursor), (int)(1));++this.cursor;this.has_preferred_x = (byte)(0);}
}
next:;
text_len += (int)(glyph_len);glyph_len = (int)(Nuklear.UtfDecode(text + text_len, &unicode, (int)(total_len - text_len)));}
		}

		public void TexteditKey(int key, int shift_mod, UserFont font, float row_height)
		{
			retry:;
switch (key){
case Nuklear.NK_KEY_NONE:case Nuklear.NK_KEY_CTRL:case Nuklear.NK_KEY_ENTER:case Nuklear.NK_KEY_SHIFT:case Nuklear.NK_KEY_TAB:case Nuklear.NK_KEY_COPY:case Nuklear.NK_KEY_CUT:case Nuklear.NK_KEY_PASTE:case Nuklear.NK_KEY_MAX:default: break;case Nuklear.NK_KEY_TEXT_UNDO:TexteditUndo();this.has_preferred_x = (byte)(0);break;case Nuklear.NK_KEY_TEXT_REDO:TexteditRedo();this.has_preferred_x = (byte)(0);break;case Nuklear.NK_KEY_TEXT_SELECT_ALL:TexteditSelectAll();this.has_preferred_x = (byte)(0);break;case Nuklear.NK_KEY_TEXT_INSERT_MODE:if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW)) this.mode = (byte)(Nuklear.NK_TEXT_EDIT_MODE_INSERT);break;case Nuklear.NK_KEY_TEXT_REPLACE_MODE:if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW)) this.mode = (byte)(Nuklear.NK_TEXT_EDIT_MODE_REPLACE);break;case Nuklear.NK_KEY_TEXT_RESET_MODE:if (((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_INSERT)) || ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_REPLACE))) this.mode = (byte)(Nuklear.NK_TEXT_EDIT_MODE_VIEW);break;case Nuklear.NK_KEY_LEFT:if ((shift_mod) != 0) {
TexteditClamp();TexteditPrepSelectionAtCursor();if ((this.select_end) > (0)) --this.select_end;this.cursor = (int)(this.select_end);this.has_preferred_x = (byte)(0);}
 else {
if (((this).select_start != (this).select_end)) TexteditMoveToFirst(); else if ((this.cursor) > (0)) --this.cursor;this.has_preferred_x = (byte)(0);}
break;case Nuklear.NK_KEY_RIGHT:if ((shift_mod) != 0) {
TexteditPrepSelectionAtCursor();++this.select_end;TexteditClamp();this.cursor = (int)(this.select_end);this.has_preferred_x = (byte)(0);}
 else {
if (((this).select_start != (this).select_end)) TexteditMoveToLast(); else ++this.cursor;TexteditClamp();this.has_preferred_x = (byte)(0);}
break;case Nuklear.NK_KEY_TEXT_WORD_LEFT:if ((shift_mod) != 0) {
if (!((this).select_start != (this).select_end)) TexteditPrepSelectionAtCursor();this.cursor = (int)(TexteditMoveToWordPrevious());this.select_end = (int)(this.cursor);TexteditClamp();}
 else {
if (((this).select_start != (this).select_end)) TexteditMoveToFirst(); else {
this.cursor = (int)(TexteditMoveToWordPrevious());TexteditClamp();}
}
break;case Nuklear.NK_KEY_TEXT_WORD_RIGHT:if ((shift_mod) != 0) {
if (!((this).select_start != (this).select_end)) TexteditPrepSelectionAtCursor();this.cursor = (int)(TexteditMoveToWordNext());this.select_end = (int)(this.cursor);TexteditClamp();}
 else {
if (((this).select_start != (this).select_end)) TexteditMoveToLast(); else {
this.cursor = (int)(TexteditMoveToWordNext());TexteditClamp();}
}
break;case Nuklear.NK_KEY_DOWN:{
TextFind find =  new TextFind();TextEditRow row =  new TextEditRow();int i;int sel = (int)(shift_mod);if ((this.single_line) != 0) {
key = (int)(Nuklear.NK_KEY_RIGHT);goto retry;}
if ((sel) != 0) TexteditPrepSelectionAtCursor(); else if (((this).select_start != (this).select_end)) TexteditMoveToLast();TexteditClamp();find.TexteditFindCharpos(this, (int)(this.cursor), (int)(this.single_line), font, (float)(row_height));if ((find.length) != 0) {
float x;float goal_x = (float)((this.has_preferred_x) != 0?this.preferred_x:find.x);int start = (int)(find.first_char + find.length);this.cursor = (int)(start);row.TexteditLayoutRow(this, (int)(this.cursor), (float)(row_height), font);x = (float)(row.x0);for (i = (int)(0); ((i) < (row.num_chars)) && ((x) < (row.x1)); ++i) {
float dx = (float)(TexteditGetWidth((int)(start), (int)(i), font));x += (float)(dx);if ((x) > (goal_x)) break;++this.cursor;}TexteditClamp();this.has_preferred_x = (byte)(1);this.preferred_x = (float)(goal_x);if ((sel) != 0) this.select_end = (int)(this.cursor);}
}
break;case Nuklear.NK_KEY_UP:{
TextFind find =  new TextFind();TextEditRow row =  new TextEditRow();int i;int sel = (int)(shift_mod);if ((this.single_line) != 0) {
key = (int)(Nuklear.NK_KEY_LEFT);goto retry;}
if ((sel) != 0) TexteditPrepSelectionAtCursor(); else if (((this).select_start != (this).select_end)) TexteditMoveToFirst();TexteditClamp();find.TexteditFindCharpos(this, (int)(this.cursor), (int)(this.single_line), font, (float)(row_height));if (find.prev_first != find.first_char) {
float x;float goal_x = (float)((this.has_preferred_x) != 0?this.preferred_x:find.x);this.cursor = (int)(find.prev_first);row.TexteditLayoutRow(this, (int)(this.cursor), (float)(row_height), font);x = (float)(row.x0);for (i = (int)(0); ((i) < (row.num_chars)) && ((x) < (row.x1)); ++i) {
float dx = (float)(TexteditGetWidth((int)(find.prev_first), (int)(i), font));x += (float)(dx);if ((x) > (goal_x)) break;++this.cursor;}TexteditClamp();this.has_preferred_x = (byte)(1);this.preferred_x = (float)(goal_x);if ((sel) != 0) this.select_end = (int)(this.cursor);}
}
break;case Nuklear.NK_KEY_DEL:if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW)) break;if (((this).select_start != (this).select_end)) TexteditDeleteSelection(); else {
int n = (int)(this._string_.len);if ((this.cursor) < (n)) TexteditDelete((int)(this.cursor), (int)(1));}
this.has_preferred_x = (byte)(0);break;case Nuklear.NK_KEY_BACKSPACE:if ((this.mode) == (Nuklear.NK_TEXT_EDIT_MODE_VIEW)) break;if (((this).select_start != (this).select_end)) TexteditDeleteSelection(); else {
TexteditClamp();if ((this.cursor) > (0)) {
TexteditDelete((int)(this.cursor - 1), (int)(1));--this.cursor;}
}
this.has_preferred_x = (byte)(0);break;case Nuklear.NK_KEY_TEXT_START:if ((shift_mod) != 0) {
TexteditPrepSelectionAtCursor();this.cursor = (int)(this.select_end = (int)(0));this.has_preferred_x = (byte)(0);}
 else {
this.cursor = (int)(this.select_start = (int)(this.select_end = (int)(0)));this.has_preferred_x = (byte)(0);}
break;case Nuklear.NK_KEY_TEXT_END:if ((shift_mod) != 0) {
TexteditPrepSelectionAtCursor();this.cursor = (int)(this.select_end = (int)(this._string_.len));this.has_preferred_x = (byte)(0);}
 else {
this.cursor = (int)(this._string_.len);this.select_start = (int)(this.select_end = (int)(0));this.has_preferred_x = (byte)(0);}
break;case Nuklear.NK_KEY_TEXT_LINE_START:{
if ((shift_mod) != 0) {
TextFind find =  new TextFind();TexteditClamp();TexteditPrepSelectionAtCursor();if (((this._string_.len) != 0) && ((this.cursor) == (this._string_.len))) --this.cursor;find.TexteditFindCharpos(this, (int)(this.cursor), (int)(this.single_line), font, (float)(row_height));this.cursor = (int)(this.select_end = (int)(find.first_char));this.has_preferred_x = (byte)(0);}
 else {
TextFind find =  new TextFind();if (((this._string_.len) != 0) && ((this.cursor) == (this._string_.len))) --this.cursor;TexteditClamp();TexteditMoveToFirst();find.TexteditFindCharpos(this, (int)(this.cursor), (int)(this.single_line), font, (float)(row_height));this.cursor = (int)(find.first_char);this.has_preferred_x = (byte)(0);}
}
break;case Nuklear.NK_KEY_TEXT_LINE_END:{
if ((shift_mod) != 0) {
TextFind find =  new TextFind();TexteditClamp();TexteditPrepSelectionAtCursor();find.TexteditFindCharpos(this, (int)(this.cursor), (int)(this.single_line), font, (float)(row_height));this.has_preferred_x = (byte)(0);this.cursor = (int)(find.first_char + find.length);if (((find.length) > (0)) && ((this._string_.RuneAt((int)(this.cursor - 1))) == ('\n'))) --this.cursor;this.select_end = (int)(this.cursor);}
 else {
TextFind find =  new TextFind();TexteditClamp();TexteditMoveToFirst();find.TexteditFindCharpos(this, (int)(this.cursor), (int)(this.single_line), font, (float)(row_height));this.has_preferred_x = (byte)(0);this.cursor = (int)(find.first_char + find.length);if (((find.length) > (0)) && ((this._string_.RuneAt((int)(this.cursor - 1))) == ('\n'))) --this.cursor;}
}
break;}

		}

		public void TexteditUndo()
		{
			TextUndoState s = this.undo;
			TextUndoRecord u =  new TextUndoRecord();TextUndoRecord* r;
			if ((s.undo_point) == (0)) return;
			u = (TextUndoRecord)(s.undo_rec[s.undo_point - 1]);
			r = (TextUndoRecord *)s.undo_rec + s.redo_point - 1;
			r->char_storage = (short)(-1);
			r->insert_length = (short)(u.delete_length);
			r->delete_length = (short)(u.insert_length);
			r->where = (int)(u.where);
			if ((u.delete_length) != 0) {
if ((s.undo_char_point + u.delete_length) >= (999)) {
r->insert_length = (short)(0);}
 else {
int i;while ((s.undo_char_point + u.delete_length) > (s.redo_char_point)) {
s.TexteditDiscardRedo();if ((s.redo_point) == (99)) return;}r = (TextUndoRecord *)s.undo_rec + s.redo_point - 1;r->char_storage = ((short)(s.redo_char_point - u.delete_length));s.redo_char_point = ((short)(s.redo_char_point - u.delete_length));for (i = (int)(0); (i) < (u.delete_length); ++i) {s.undo_char[r->char_storage + i] = (uint)(this._string_.RuneAt((int)(u.where + i)));}}
this._string_.DeleteRunes((int)(u.where), (int)(u.delete_length));}

			if ((u.insert_length) != 0) {
this._string_.InsertTextRunes((int)(u.where), (char *)s.undo_char + u.char_storage, (int)(u.insert_length));s.undo_char_point = ((short)(s.undo_char_point - u.insert_length));}

			this.cursor = (int)((short)(u.where + u.insert_length));
			s.undo_point--;
			s.redo_point--;
		}

		public void TexteditRedo()
		{
			TextUndoState s = this.undo;
			TextUndoRecord* u;TextUndoRecord r =  new TextUndoRecord();
			if ((s.redo_point) == (99)) return;
			u = (TextUndoRecord *)s.undo_rec + s.undo_point;
			r = (TextUndoRecord)(s.undo_rec[s.redo_point]);
			u->delete_length = (short)(r.insert_length);
			u->insert_length = (short)(r.delete_length);
			u->where = (int)(r.where);
			u->char_storage = (short)(-1);
			if ((r.delete_length) != 0) {
if ((s.undo_char_point + u->insert_length) > (s.redo_char_point)) {
u->insert_length = (short)(0);u->delete_length = (short)(0);}
 else {
int i;u->char_storage = (short)(s.undo_char_point);s.undo_char_point = ((short)(s.undo_char_point + u->insert_length));for (i = (int)(0); (i) < (u->insert_length); ++i) {
s.undo_char[u->char_storage + i] = (uint)(this._string_.RuneAt((int)(u->where + i)));}}
this._string_.DeleteRunes((int)(r.where), (int)(r.delete_length));}

			if ((r.insert_length) != 0) {
this._string_.InsertTextRunes((int)(r.where), (char *)s.undo_char + r.char_storage, (int)(r.insert_length));}

			this.cursor = (int)(r.where + r.insert_length);
			s.undo_point++;
			s.redo_point++;
		}

		public void TexteditMakeundoInsert(int where, int length)
		{
			this.undo.TexteditCreateundo((int)(where), (int)(0), (int)(length));
		}

		public void TexteditMakeundoDelete(int where, int length)
		{
			int i;
			uint* p = this.undo.TexteditCreateundo((int)(where), (int)(length), (int)(0));
			if ((p) != null) {
for (i = (int)(0); (i) < (length); ++i) {p[i] = (uint)(this._string_.RuneAt((int)(where + i)));}}

		}

		public void TexteditMakeundoReplace(int where, int old_length, int new_length)
		{
			int i;
			uint* p = this.undo.TexteditCreateundo((int)(where), (int)(old_length), (int)(new_length));
			if ((p) != null) {
for (i = (int)(0); (i) < (old_length); ++i) {p[i] = (uint)(this._string_.RuneAt((int)(where + i)));}}

		}

		public void TexteditClearState(int type, int (const nk_text_edit *, unsigned int)* filter)
		{
			this.undo.undo_point = (short)(0);
			this.undo.undo_char_point = (short)(0);
			this.undo.redo_point = (short)(99);
			this.undo.redo_char_point = (short)(999);
			this.select_end = (int)(this.select_start = (int)(0));
			this.cursor = (int)(0);
			this.has_preferred_x = (byte)(0);
			this.preferred_x = (float)(0);
			this.cursor_at_end_of_line = (byte)(0);
			this.initialized = (byte)(1);
			this.single_line = ((byte)((type) == (Nuklear.NK_TEXT_EDIT_SINGLE_LINE)?1:0));
			this.mode = (byte)(Nuklear.NK_TEXT_EDIT_MODE_VIEW);
			this.filter = filter;
			this.scrollbar = (Vec2)(Nuklear.Vec2z((float)(0), (float)(0)));
		}

		public void TexteditInitFixed(void * memory, ulong size)
		{
			if (((memory== null)) || (size== 0)) return;
			Nuklear.Memset(this, (int)(0), (ulong)(sizeof(TextEdit)));
			TexteditClearState((int)(Nuklear.NK_TEXT_EDIT_SINGLE_LINE), null);
			this._string_.InitFixed(memory, (ulong)(size));
		}

		public void TexteditInit( ulong size)
		{
			if ((this== null) ) return;
			Nuklear.Memset(this, (int)(0), (ulong)(sizeof(TextEdit)));
			TexteditClearState((int)(Nuklear.NK_TEXT_EDIT_SINGLE_LINE), null);
			this._string_.Init(alloc, (ulong)(size));
		}

		public void TexteditInitDefault()
		{
			if (this== null) return;
			Nuklear.Memset(this, (int)(0), (ulong)(sizeof(TextEdit)));
			TexteditClearState((int)(Nuklear.NK_TEXT_EDIT_SINGLE_LINE), null);
			this._string_.InitDefault();
		}

		public void TexteditSelectAll()
		{
			this.select_start = (int)(0);
			this.select_end = (int)(this._string_.len);
		}

		public void TexteditFree()
		{
			if (this== null) return;
			this._string_.Free();
		}

		public int FilterDefault(char unicode)
		{
			return (int)(Nuklear.nk_true);
		}

		public int FilterAscii(char unicode)
		{
			if ((unicode) > (128)) return (int)(Nuklear.nk_false); else return (int)(Nuklear.nk_true);
		}

		public int FilterFloat(char unicode)
		{
			if (((((unicode) < ('0')) || ((unicode) > ('9'))) && (unicode != '.')) && (unicode != '-')) return (int)(Nuklear.nk_false); else return (int)(Nuklear.nk_true);
		}

		public int FilterDecimal(char unicode)
		{
			if ((((unicode) < ('0')) || ((unicode) > ('9'))) && (unicode != '-')) return (int)(Nuklear.nk_false); else return (int)(Nuklear.nk_true);
		}

		public int FilterHex(char unicode)
		{
			if (((((unicode) < ('0')) || ((unicode) > ('9'))) && (((unicode) < ('a')) || ((unicode) > ('f')))) && (((unicode) < ('A')) || ((unicode) > ('F')))) return (int)(Nuklear.nk_false); else return (int)(Nuklear.nk_true);
		}

		public int FilterOct(char unicode)
		{
			if (((unicode) < ('0')) || ((unicode) > ('7'))) return (int)(Nuklear.nk_false); else return (int)(Nuklear.nk_true);
		}

		public int FilterBinary(char unicode)
		{
			if ((unicode != '0') && (unicode != '1')) return (int)(Nuklear.nk_false); else return (int)(Nuklear.nk_true);
		}

	}
}
