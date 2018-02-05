using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class Str
	{
		public Buffer buffer = new Buffer();
		public int len;

		public void InitFixed(void * memory, ulong size)
		{
			this.buffer.InitFixed(memory, (ulong)(size));
			this.len = (int)(0);
		}

		public int AppendTextChar(char* str, int len)
		{
			sbyte* mem;
			if (((str== null)) || (len== 0)) return (int)(0);
			mem = (sbyte*)(this.buffer.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)((ulong)(len) * sizeof(char)), (ulong)(0)));
			if (mem== null) return (int)(0);
			Nuklear.Memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char)));
			this.len += (int)(Nuklear.UtfLen(str, (int)(len)));
			return (int)(len);
		}

		public int AppendStrChar(char* str)
		{
			return (int)(AppendTextChar(str, (int)(Nuklear.Strlen(str))));
		}

		public int AppendTextUtf8(char* text, int len)
		{
			int i = (int)(0);
			int byte_len = (int)(0);
			char unicode;
			if (((text== null)) || (len== 0)) return (int)(0);
			for (i = (int)(0); (i) < (len); ++i) {byte_len += (int)(Nuklear.UtfDecode(text + byte_len, &unicode, (int)(4)));}
			AppendTextChar(text, (int)(byte_len));
			return (int)(len);
		}

		public int AppendStrUtf8(char* text)
		{
			int runes = (int)(0);
			int byte_len = (int)(0);
			int num_runes = (int)(0);
			int glyph_len = (int)(0);
			char unicode;
			if ((text== null)) return (int)(0);
			glyph_len = (int)(byte_len = (int)(Nuklear.UtfDecode(text + byte_len, &unicode, (int)(4))));
			while ((unicode != '\0') && ((glyph_len) != 0)) {
glyph_len = (int)(Nuklear.UtfDecode(text + byte_len, &unicode, (int)(4)));byte_len += (int)(glyph_len);num_runes++;}
			AppendTextChar(text, (int)(byte_len));
			return (int)(runes);
		}

		public int InsertAtChar(int pos, char* str, int len)
		{
			int i;
			void * mem;
			sbyte* src;
			sbyte* dst;
			int copylen;
			if ((((str== null)) || (len== 0)) || (((ulong)(pos)) > (this.buffer.allocated))) return (int)(0);
			if (((this.buffer.allocated + (ulong)(len)) >= (this.buffer.memory.size)) && ((this.buffer.type) == (Nuklear.NK_BUFFER_FIXED))) return (int)(0);
			copylen = (int)((int)(this.buffer.allocated) - pos);
			if (copylen== 0) {
AppendTextChar(str, (int)(len));return (int)(1);}

			mem = this.buffer.Alloc((int)(Nuklear.NK_BUFFER_FRONT), (ulong)((ulong)(len) * sizeof(char)), (ulong)(0));
			if (mem== null) return (int)(0);
			dst = ((sbyte*)((void *)((byte*)(this.buffer.memory.ptr) + (pos + len + (copylen - 1)))));
			src = ((sbyte*)((void *)((byte*)(this.buffer.memory.ptr) + (pos + (copylen - 1)))));
			for (i = (int)(0); (i) < (copylen); ++i) {*dst-- = (sbyte)(*src--);}
			mem = ((void *)((byte*)(this.buffer.memory.ptr) + (pos)));
			Nuklear.Memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char)));
			this.len = (int)(Nuklear.UtfLen((char*)(this.buffer.memory.ptr), (int)(this.buffer.allocated)));
			return (int)(1);
		}

		public int InsertAtRune(int pos, char* cstr, int len)
		{
			int glyph_len;
			char unicode;
			char* begin;
			char* buffer;
			if (((cstr== null)) || (len== 0)) return (int)(0);
			begin = AtRune((int)(pos), &unicode, ref glyph_len);
			if (this.len== 0) return (int)(AppendTextChar(cstr, (int)(len)));
			buffer = GetConst();
			if (begin== null) return (int)(0);
			return (int)(InsertAtChar((int)(begin - buffer), cstr, (int)(len)));
		}

		public int InsertTextChar(int pos, char* text, int len)
		{
			return (int)(InsertTextUtf8((int)(pos), text, (int)(len)));
		}

		public int InsertStrChar(int pos, char* text)
		{
			return (int)(InsertTextUtf8((int)(pos), text, (int)(Nuklear.Strlen(text))));
		}

		public int InsertTextUtf8(int pos, char* text, int len)
		{
			int i = (int)(0);
			int byte_len = (int)(0);
			char unicode;
			if (((text== null)) || (len== 0)) return (int)(0);
			for (i = (int)(0); (i) < (len); ++i) {byte_len += (int)(Nuklear.UtfDecode(text + byte_len, &unicode, (int)(4)));}
			InsertAtRune((int)(pos), text, (int)(byte_len));
			return (int)(len);
		}

		public int InsertStrUtf8(int pos, char* text)
		{
			int runes = (int)(0);
			int byte_len = (int)(0);
			int num_runes = (int)(0);
			int glyph_len = (int)(0);
			char unicode;
			if ((text== null)) return (int)(0);
			glyph_len = (int)(byte_len = (int)(Nuklear.UtfDecode(text + byte_len, &unicode, (int)(4))));
			while ((unicode != '\0') && ((glyph_len) != 0)) {
glyph_len = (int)(Nuklear.UtfDecode(text + byte_len, &unicode, (int)(4)));byte_len += (int)(glyph_len);num_runes++;}
			InsertAtRune((int)(pos), text, (int)(byte_len));
			return (int)(runes);
		}

		public int InsertTextRunes(int pos, char* runes, int len)
		{
			int i = (int)(0);
			int byte_len = (int)(0);
			char* glyph = stackalloc char[4];
			if (((runes== null)) || (len== 0)) return (int)(0);
			for (i = (int)(0); (i) < (len); ++i) {
byte_len = (int)(Nuklear.UtfEncode(runes[i], glyph, (int)(4)));if (byte_len== 0) break;InsertAtRune((int)(pos + i), glyph, (int)(byte_len));}
			return (int)(len);
		}

		public int InsertStrRunes(int pos, char* runes)
		{
			int i = (int)(0);
			char* glyph = stackalloc char[4];
			int byte_len;
			if ((runes== null)) return (int)(0);
			while (runes[i] != '\0') {
byte_len = (int)(Nuklear.UtfEncode(runes[i], glyph, (int)(4)));InsertAtRune((int)(pos + i), glyph, (int)(byte_len));i++;}
			return (int)(i);
		}

		public void RemoveChars(int len)
		{
			if ((((len) < (0))) || (((ulong)(len)) > (this.buffer.allocated))) return;
			this.buffer.allocated -= ((ulong)(len));
			this.len = (int)(Nuklear.UtfLen((char*)(this.buffer.memory.ptr), (int)(this.buffer.allocated)));
		}

		public void RemoveRunes(int len)
		{
			int index;
			char* begin;
			char* end;
			char unicode;
			if (((len) < (0))) return;
			if ((len) >= (this.len)) {
this.len = (int)(0);return;}

			index = (int)(this.len - len);
			begin = AtRune((int)(index), &unicode, ref len);
			end = (char*)(this.buffer.memory.ptr) + this.buffer.allocated;
			RemoveChars((int)((int)(end - begin) + 1));
		}

		public void DeleteChars(int pos, int len)
		{
			if ((((len== 0)) || (((ulong)(pos)) > (this.buffer.allocated))) || (((ulong)(pos + len)) > (this.buffer.allocated))) return;
			if (((ulong)(pos + len)) < (this.buffer.allocated)) {
sbyte* dst = ((sbyte*)((void *)((byte*)(this.buffer.memory.ptr) + (pos))));sbyte* src = ((sbyte*)((void *)((byte*)(this.buffer.memory.ptr) + (pos + len))));Nuklear.Memcopy(dst, src, (ulong)(this.buffer.allocated - (ulong)(pos + len)));this.buffer.allocated -= ((ulong)(len));}
 else RemoveChars((int)(len));
			this.len = (int)(Nuklear.UtfLen((char*)(this.buffer.memory.ptr), (int)(this.buffer.allocated)));
		}

		public void DeleteRunes(int pos, int len)
		{
			char* temp;
			char unicode;
			char* begin;
			char* end;
			int unused;
			if ((this.len) < (pos + len)) len = (int)(((this.len - pos) < (this.len)?(this.len - pos):(this.len)) < (0)?(0):((this.len - pos) < (this.len)?(this.len - pos):(this.len)));
			if (len== 0) return;
			temp = (char*)(this.buffer.memory.ptr);
			begin = AtRune((int)(pos), &unicode, ref unused);
			if (begin== null) return;
			this.buffer.memory.ptr = begin;
			end = AtRune((int)(len), &unicode, ref unused);
			this.buffer.memory.ptr = temp;
			if (end== null) return;
			DeleteChars((int)(begin - temp), (int)(end - begin));
		}

		public char* AtChar(int pos)
		{
			if (((pos) > ((int)(this.buffer.allocated)))) return null;
			return ((char*)((void *)((byte*)(this.buffer.memory.ptr) + (pos))));
		}

		public char* AtRune(int pos, char* unicode, ref int len)
		{
			int i = (int)(0);
			int src_len = (int)(0);
			int glyph_len = (int)(0);
			char* text;
			int text_len;
			if (((unicode== null)) || (len== null)) return null;
			if ((pos) < (0)) {
*unicode = (char)(0);len = (int)(0);return null;}

			text = (char*)(this.buffer.memory.ptr);
			text_len = ((int)(this.buffer.allocated));
			glyph_len = (int)(Nuklear.UtfDecode(text, unicode, (int)(text_len)));
			while ((glyph_len) != 0) {
if ((i) == (pos)) {
len = (int)(glyph_len);break;}
i++;src_len = (int)(src_len + glyph_len);glyph_len = (int)(Nuklear.UtfDecode(text + src_len, unicode, (int)(text_len - src_len)));}
			if (i != pos) return null;
			return text + src_len;
		}

		public char* AtCharConst(int pos)
		{
			if (((pos) > ((int)(this.buffer.allocated)))) return null;
			return ((char*)((void *)((byte*)(this.buffer.memory.ptr) + (pos))));
		}

		public char* AtConst(int pos, char* unicode, ref int len)
		{
			int i = (int)(0);
			int src_len = (int)(0);
			int glyph_len = (int)(0);
			char* text;
			int text_len;
			if (((unicode== null)) || (len== null)) return null;
			if ((pos) < (0)) {
*unicode = (char)(0);len = (int)(0);return null;}

			text = (char*)(this.buffer.memory.ptr);
			text_len = ((int)(this.buffer.allocated));
			glyph_len = (int)(Nuklear.UtfDecode(text, unicode, (int)(text_len)));
			while ((glyph_len) != 0) {
if ((i) == (pos)) {
len = (int)(glyph_len);break;}
i++;src_len = (int)(src_len + glyph_len);glyph_len = (int)(Nuklear.UtfDecode(text + src_len, unicode, (int)(text_len - src_len)));}
			if (i != pos) return null;
			return text + src_len;
		}

		public uint RuneAt(int pos)
		{
			int len;
			char unicode = (char)0;
			AtConst((int)(pos), &unicode, ref len);
			return unicode;
		}

		public char* Get()
		{
			if (((this.len== 0)) || (this.buffer.allocated== 0)) return null;
			return (char*)(this.buffer.memory.ptr);
		}

		public char* GetConst()
		{
			if (((this.len== 0)) || (this.buffer.allocated== 0)) return null;
			return (char*)(this.buffer.memory.ptr);
		}

		public int Len()
		{
			if (((this.len== 0)) || (this.buffer.allocated== 0)) return (int)(0);
			return (int)(this.len);
		}

		public int LenChar()
		{
			if (((this.len== 0)) || (this.buffer.allocated== 0)) return (int)(0);
			return (int)(this.buffer.allocated);
		}

		public void Clear()
		{
			this.buffer.Clear();
			this.len = (int)(0);
		}

		public void Free()
		{
			this.buffer.Free();
			this.len = (int)(0);
		}

	}
}
