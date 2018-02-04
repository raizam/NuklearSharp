using Sichem;

namespace NuklearSharp.Generation
{
	partial class Program
	{
		private static string PostProcess(string data)
		{
			// Build has of C functions
			data = Utility.ReplaceNativeCalls(data);

			data = data.Replace("(void *)(0)", "null");
			data = data.Replace("public IntPtr* draw_begin;", "public Nuklear.NkDrawNotify draw_begin;");
			data = data.Replace("public IntPtr* draw_end;", "public Nuklear.NkDrawNotify draw_end;");
			data = data.Replace("enum anti_aliasing", "int");
			data = data.Replace("- -", "-");
			data = data.Replace("* *", "*");
			data = data.Replace("+ +", "+");
			data = data.Replace("),)", "))");
			data = data.Replace("unsignedint", "uint");
			string[] stringNames =
			{
				"ctext", "text", "_string_", "label", "title", "txt", "str", "name", "begin", "end",
				"X", "line", "output", "dataBase85", "glyph", "cstr", "buffer", "remaining", "temp",
				"select_begin_ptr", "select_end_ptr", "cursor_ptr", "s1", "s2", "memory", "id",
				"selected", "hash",
			};

			foreach (var sn in stringNames)
			{
				data = data.Replace("sbyte* " + sn, "char* " + sn);
			}

			data = data.Replace("fixed sbyte", "fixed char");
			data = data.Replace("sbyte* At", "char* At");
			data = data.Replace("sbyte* Get", "char* Get");
			data = data.Replace("(sbyte*)(this.buffer.memory.ptr)", "(char*)(this.buffer.memory.ptr)");
			data = data.Replace("(sbyte*)(this.buffer.memory.ptr)", "(char*)(this.buffer.memory.ptr)");
			data = data.Replace("uint* unicode", "char* unicode");
			data = data.Replace("uint* runes", "char* runes");
			data = data.Replace("(uint)(runes[i])", "runes[i]");
			data = data.Replace("return ((sbyte*)((void *)((byte*)(this.buffer.memory.ptr) + (pos))));",
				"return ((char*)((void *)((byte*)(this.buffer.memory.ptr) + (pos))));");
			data = data.Replace("sbyte* I", "char* I");
			data = data.Replace("sbyte* D", "char* D");
			data = data.Replace("sbyte* p = str;", "char* p = str;");
			data = data.Replace("sbyte* s)", "char* s)");
			data = data.Replace("sbyte* s,", "char* s,");
			data = data.Replace("sbyte** endptr", "char** endptr");
			data = data.Replace("sbyte t;", "char t;");
			data = data.Replace("(sbyte)(t)", "t");
			data = data.Replace("(sbyte)(s[", "(s[");
			data = data.Replace("(sbyte)('", "('");
			data = data.Replace("s[i] = (sbyte)(", "s[i] = (char)(");
			data = data.Replace("+ (sbyte)(", "+ (char)(");
			data = data.Replace("+ (sbyte)(", "+ (char)(");
			data = data.Replace("*c = (sbyte)(0);", "*c = (char)0;");
			data = data.Replace("sbyte* c = s;", "char* c = s;");
			data = data.Replace("sbyte* c = _string_;", "char* c = _string_;");
			data = data.Replace("sbyte black", "char black");
			data = data.Replace("sbyte white", "char white");
			data = data.Replace("' + (n % 10)", "' + (char)(n % 10)");
			data = data.Replace("s[i++] = (('0' + (char)(n % 10)));", "s[i++] = (char)(('0' + (char)(n % 10)));");
			data = data.Replace("*(c++) = (('0'", "*(c++) = (char)(('0'");
			data = data.Replace("= (sbyte)(c[", "= (c[");

			// Memset
			data = data.Replace("(sizeof(uint)>) > (2)", "sizeof(uint) > 2");
			data = data.Replace("if ((size) < (3 * sizeof(uint))))", "if ((size) < (3 * sizeof(uint)))");
			data = data.Replace("sizeof(uint);)", "sizeof(uint))");

			// Memcpy
			data = data.Replace("sizeof(int);)", "sizeof(int))");
			data = data.Replace("dst += sizeof(int));", "dst += sizeof(int);");
			data = data.Replace("((length) < (sizeof(int)))))", "((length) < (sizeof(int))))");
			data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
			data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
			data = data.Replace("for (pow = (int)(0); *p; p++)", "for (pow = (int)(0); *p != 0; p++)");
			data = data.Replace("(int)((((d) >= (0)) << 1) - 1);", "(int)((((d) >= (0)?1:0) << 1) - 1);");
			data = data.Replace("best_letter = 0", "best_letter = null");
			data = data.Replace("best_letter != 0", "best_letter != null");
			data = data.Replace("(int)(IsLower((int)(str_letter)) != 0);", "(int)(IsLower((int)(str_letter)) != 0?1:0);");
			data = data.Replace("''", "'\\0'");
			data = data.Replace("'\n'", "'\\n'");
			data = data.Replace("'\r'", "'\\r'");
			data = data.Replace("sizeof((handle))", "sizeof(Handle)");
			data = data.Replace("sizeof((s))", "sizeof(Image)");
			data = data.Replace("(int)(!(((this.w) == (0)) && ((this.h) == (0))));",
				"(int)((((this.w) == (0)) && ((this.h) == (0)))?1:0);");
			data = data.Replace("((!sep_len)?len:sep_len)", "((sep_len == 0)?len:sep_len)");

			// murmur_hash
			data =
				data.Replace("union (anonymous at nuklear.h:6644:5) conv = (union (anonymous at nuklear.h:6644:5))({ null });",
					"MurmurHashUnion conv = new MurmurHashUnion(null);");
			data = data.Replace("i; ++i", "i != 0; ++i");

			// rgb
			data = data.Replace("((sbyte)(((col", "((char)(((col");
			data = data.Replace("((sbyte)((col", "((char)((col");

			// commands
			data = data.Replace("(int)(Nuklear.NK_COMMAND_SCISSOR), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_SCISSOR), (ulong)(sizeof(CommandScissor)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_LINE), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_LINE), (ulong)(sizeof(CommandLine)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_CURVE), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_CURVE), (ulong)(sizeof(CommandCurve)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_RECT), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_RECT), (ulong)(sizeof(CommandRect)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_RECT_FILLED), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_RECT_FILLED), (ulong)(sizeof(CommandRectFilled)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_RECT_MULTI_COLOR), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_RECT_MULTI_COLOR), (ulong)(sizeof(CommandRectMultiColor)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_CIRCLE), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_CIRCLE), (ulong)(sizeof(CommandCircle)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_CIRCLE_FILLED), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_CIRCLE_FILLED), (ulong)(sizeof(CommandCircleFilled)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_ARC), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_ARC), (ulong)(sizeof(CommandArc)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_ARC_FILLED), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_ARC_FILLED), (ulong)(sizeof(CommandArcFilled)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_TRIANGLE), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_TRIANGLE), (ulong)(sizeof(CommandTriangle)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_TRIANGLE_FILLED), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_TRIANGLE_FILLED), (ulong)(sizeof(CommandTriangleFilled)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_POLYGON), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_POLYGON), (ulong)(sizeof(CommandPolygon)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_POLYGON_FILLED), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_POLYGON_FILLED), (ulong)(sizeof(CommandPolygonFilled)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_POLYLINE), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_POLYLINE), (ulong)(sizeof(CommandPolyline)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_IMAGE), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_IMAGE), (ulong)(sizeof(CommandImage)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_CUSTOM), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_CUSTOM), (ulong)(sizeof(CommandCustom)");
			data = data.Replace("(int)(Nuklear.NK_COMMAND_TEXT), (ulong)(sizeof((*cmd))",
				"(int)(Nuklear.NK_COMMAND_TEXT), (ulong)(sizeof(CommandText)");

			//
			data = data.Replace("sizeof((utfmask)) / sizeof((utfmask)[0])", "utfmask.Length");
			data =
				data.Replace(
					"full = (int)((this.size - ((this.size) < (size + alignment)?(this.size):(size + alignment))) <= this.allocated);",
					"full = (int)((this.size - ((this.size) < (size + alignment)?(this.size):(size + alignment))) <= this.allocated?1:0);");
			data = data.Replace("Nuklear.Memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char))));",
				"Nuklear.Memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char)));");
			data = data.Replace("(sizeof((this.circle_vtx)) / sizeof((this.circle_vtx)[0]))", "(ulong)this.circle_vtx.Length");
			data = data.Replace("sizeof(structVec2);", "sizeof(Vec2)");
			data = data.Replace("sizeof(structDrawCommand);", "sizeof(DrawCommand)");
			data = data.Replace("sizeof(draw_index);", "sizeof(draw_index)");
			data = data.Replace("sizeof((col))", "sizeof(Color)");
			data = data.Replace("sizeof((bgra))", "sizeof(Color)");
			data = data.Replace("sizeof((color))", "sizeof(uint)");
			data =
				data.Replace(
					"return (int)(((this.attribute) == (Nuklear.NK_VERTEX_ATTRIBUTE_COUNT)) || ((this.format) == (Nuklear.NK_FORMAT_COUNT)));",
					"return (int)(((this.attribute) == (Nuklear.NK_VERTEX_ATTRIBUTE_COUNT)) || ((this.format) == (Nuklear.NK_FORMAT_COUNT))?1:0);");
			data = data.Replace("(sizeof(CommandArcFilled)", "((ulong)sizeof(CommandArcFilled)");
			data = data.Replace("sizeof(short);)", "sizeof(short))");
			data = data.Replace("sizeof((values[value_index]))", "sizeof(float)");
			data = data.Replace("Memcopy(attribute, &value, (ulong)(sizeof((value))));",
				"Memcopy(attribute, &value, (ulong)(sizeof(double)));");
			data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(float))));",
				"attribute = (void *)(((sbyte*)(attribute) + sizeof(float)));");
			data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(double))));",
				"attribute = (void *)(((sbyte*)(attribute) + sizeof(double)));");
			data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(char))));",
				"attribute = (void *)(((sbyte*)(attribute) + sizeof(char)));");
			data = data.Replace("attribute = (void *)((sbyte*)(attribute) + sizeof((value)));",
				"attribute = (void *)((sbyte*)(attribute) + sizeof(short));");
			data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(int))));",
				"attribute = (void *)(((sbyte*)(attribute) + sizeof(int)));");
			data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(unsignedchar))));",
				"attribute = (void *)(((sbyte*)(attribute) + sizeof(byte)));");
			data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(uint))));",
				"attribute = (void *)(((sbyte*)(attribute) + sizeof(uint)));");
			data = data.Replace("pnt_size * ((thick_line) != 0?5:3)", "pnt_size * (ulong)((thick_line) != 0?5:3)");

			// inputButton
			data = data.Replace("&_in_.mouse.buttons[id]", "(MouseButton *)_in_.mouse.buttons + id");

			// input_begin
			data = data.Replace("&this.mouse.buttons[id]", "(MouseButton *)this.mouse.buttons + id");
			data = data.Replace("_in_.mouse.buttons[Nuklear.NK_BUTTON_LEFT].",
				"((MouseButton *)_in_.mouse.buttons + Nuklear.NK_BUTTON_LEFT)->");

			data = data.Replace("char* glyph = stackalloc sbyte[4];", "char* glyph = stackalloc char[4];");
			data = data.Replace("glyph[0] = (sbyte)(c);", "glyph[0] = c;");

			data = data.Replace("(sbyte c)", "(char c)");
			data = data.Replace("sbyte* rune = stackalloc sbyte[4];", "char* rune = stackalloc char[4];");
			data = data.Replace("rune[0] = (sbyte)(c);", "rune[0] = c;");

			// input_has_Mouse_click
			data = data.Replace("&_in_.mouse.buttons[id]", "(MouseButton *)_in_.mouse.buttons + id");

			// input_is_Key_pressed
			data = data.Replace("_in_.keyboard.keys[key].", "((Key *)_in_.keyboard.keys + key)->");
			data = data.Replace("_in_.keyboard.keys[i].", "((Key *)_in_.keyboard.keys + i)->");
			data = data.Replace("&_in_.keyboard.text[this.keyboard.text_len]",
				"(char *)_in_.keyboard.text + this.keyboard.text_len");
			data = data.Replace("&this.keyboard.keys[key]", "(Key *)this.keyboard.keys + key");

			data =
				data.Replace(
					"(int)((((rect.x) <= (this.mouse.pos.x)) && ((this.mouse.pos.x) < (rect.x + rect.w))) && (((rect.y) <= (this.mouse.pos.y)) && ((this.mouse.pos.y) < (rect.y + rect.h))));",
					"((((rect.x) <= (this.mouse.pos.x)) && ((this.mouse.pos.x) < (rect.x + rect.w))) && (((rect.y) <= (this.mouse.pos.y)) && ((this.mouse.pos.y) < (rect.y + rect.h))))?1:0;");
			data =
				data.Replace(
					"(int)((((rect.x) <= (this.mouse.prev.x)) && ((this.mouse.prev.x) < (rect.x + rect.w))) && (((rect.y) <= (this.mouse.prev.y)) && ((this.mouse.prev.y) < (rect.y + rect.h))));",
					"((((rect.x) <= (this.mouse.prev.x)) && ((this.mouse.prev.x) < (rect.x + rect.w))) && (((rect.y) <= (this.mouse.prev.y)) && ((this.mouse.prev.y) < (rect.y + rect.h))))?1:0;");
			data = data.Replace("return (int)((this.mouse.buttons[id].down== 0) && ((this.mouse.buttons[id].clicked) != 0));",
				"return ((this.mouse.buttons[id].down== 0) && ((this.mouse.buttons[id].clicked) != 0))?1:0;");

			data = data.Replace("Nuklear.Zero(&r, (ulong)(sizeof((r))));", "Nuklear.Zero(&r, (ulong)(sizeof(TextEditRow)));");

			data = data.Replace("&text[len]", "text+len");

			data = data.Replace("return &this.undo_char[r.char_storage];", "return (char *)state.undo_char + r.char_storage;");
			data = data.Replace("active = (int)(!active);", "active = active != 0?0:1;");
			data = data.Replace("value = (int)(!(value));", "value = value != 0?0:1;");
			data = data.Replace("return (int)(was_active != *active);", "return was_active != *active?1:0;");
			data = data.Replace("return (int)(old_value != value);", "return old_value != value?1:0;");
			data = data.Replace("int (const nk_text_edit *, unsigned int)*", "Nuklear.NkPluginFilter");
			data = data.Replace("enum nk_text_edit_type", "int");

			data = data.Replace("(flags & Nuklear.NK_EDIT_MULTILINE)?", "(flags & Nuklear.NK_EDIT_MULTILINE)!=0?");

			data =
				data.Replace(
					"edit.active = (byte)((((bounds.x) <= (this.mouse.pos.x)) && ((this.mouse.pos.x) < (bounds.x + bounds.w))) && (((bounds.y) <= (this.mouse.pos.y)) && ((this.mouse.pos.y) < (bounds.y + bounds.h))));",
					"edit.active = (byte)((((bounds.x) <= (this.mouse.pos.x)) && ((this.mouse.pos.x) < (bounds.x + bounds.w))) && (((bounds.y) <= (this.mouse.pos.y)) && ((this.mouse.pos.y) < (bounds.y + bounds.h)))?1:0);");

			data = data.Replace("void (void *, short, short, unsigned short, unsigned short, nk_handle)*",
				"Nuklear.NkCommandCustomCallback");

			data = data.Replace("Nuklear.Memcopy(cmd._string_, _string_, (ulong)(length));",
				"cmd._string_ = _string_;");

			// Text_calculateText_bounds
			data = data.Replace("sbyte** remaining", "char** remaining");

			// draw_list_addText
			data = data.Replace("uint next = (uint)(0);", "char next = (char)(0);");
			data = data.Replace(" (uint)(((next) == (0xFFFD))?'\\0':next)", "(next == 0xFFFD)?'\\0':next");

			// Rect_height_compare
			data = data.Replace("return (int)(((p->w) > (q->w))?-1:((p->w) < (q->w)));",
				"return (int)(((p->w) > (q->w))?-1:((p->w) < (q->w))?1:0);");

			// Rect_original_order
			data = data.Replace("return (int)(((p->was_packed) < (q->was_packed))?-1:((p->was_packed) > (q->was_packed)));",
				"return (int)(((p->was_packed) < (q->was_packed))?-1:((p->was_packed) > (q->was_packed))?1:0);");

			// Rp_init_target
			data = data.Replace("extra[0]", "extra_0");
			data = data.Replace("extra[1]", "extra_1");


			// Rp_packRects
			data = data.Replace("(int)(!(((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff))));",
				"(int)((((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff)))?0:1);");

			// RpQsort
			data = data.Replace("RpQsort(RpRect* array, uint len, IntPtr* cmp)",
				"RpQsort(RpRect* array, uint len, Nuklear.QSortComparer cmp)");
			data = data.Replace("for (right = (uint)(left - 1); {", "for (right = (uint)(left - 1);;) {");
			data = data.Replace("; ) {}", "");

			// Tt__findTable
			data = data.Replace("sbyte* tag", "string tag");

			// TtHheap_alloc
			data = data.Replace("(sizeof(TtHheapChunk)", "((ulong)sizeof(TtHheapChunk)");

			// TtHheap_cleanup

			// Tt__new_active
			data = data.Replace("(sizeof((*z))", "(sizeof(TtActiveEdge)");

			// Tt_GetGlyphShape
			data = data.Replace("(flags & 16)?dx:-dx", "(flags & 16) != 0?dx:-dx");
			data = data.Replace("(flags & 32)?dy:-dy", "(flags & 32) != 0?dy:-dy");
			data = data.Replace("!(flags & 1)", "(flags & 1)==0?1:0");
			data = data.Replace("sizeof((vertices[0]))", "sizeof(TtVertex)");
			data = data.Replace("sizeof(TtVertex)", "(ulong)sizeof(TtVertex)");

			// Tt__sortEdges_ins_sort
			data = data.Replace("(int)(((a)->y0) < ((b)->y0))", "(int)(((a)->y0) < ((b)->y0)?1:0)");

			// Tt__sortEdges_quicksort
			data = data.Replace("for (++i; {", "for (;;++i) {");
			data = data.Replace("for (--j; {", "for (;;--j) {");

			// Tt__rasterize
			data = data.Replace("sizeof((*e))", "(ulong)sizeof(TtEdge)");
			data = data.Replace("(((invert) != 0?(p[j].y) > (p[k].y):(p[j].y) < (p[k].y)) != 0)",
				"(invert != 0?(p[j].y > p[k].y):(p[j].y < p[k].y))");

			// Tt__rasterize_sortedEdges
			data = data.Replace("Nuklear.Zero(&hh, (ulong)(sizeof((hh))));", "Nuklear.Zero(&hh, (ulong)(sizeof(TtHheap)));");
			data = data.Replace("hh.set_alloc", "hh.alloc");
			data = data.Replace("(this.w * 2 + 1) * sizeof(float)))));", "(this.w * 2 + 1) * sizeof(float))));");
			data = data.Replace("sizeof((scanline[0]))", "sizeof(float)");

			// Tt_FlattenCurves
			data = data.Replace("sizeof((**contour_lengths))", "(ulong)sizeof(int)");
			data = data.Replace("sizeof((points[0]))", "(ulong)sizeof(TtPoint)");

			// Tt_PackBegin
			data = data.Replace("sizeof((*context))", "sizeof(RpContext)");
			data = data.Replace("sizeof((*nodes))", "(ulong)sizeof(RpNode)");

			// FontBaker_Memory
			data = data.Replace("for (iter = config_list; iter;", "for (iter = config_list; iter != null;");

			// Tt_PackEnd
			// FontBake_pack
			data = data.Replace("config_iter; config_iter", "config_iter != null; config_iter");
			data = data.Replace("sizeof((custom_space))", "sizeof(RpRect)");

			// FontQueryFontGlyph
			data = data.Replace("uint codepoint = (uint)(0);", "char codepoint = (char)0;");
			data = data.Replace("uint codepoint", "char codepoint");
			data = data.Replace("uint next_codepoint", "char next_codepoint");
			data = data.Replace("(uint)(codepoint)", "codepoint");

			// Font_init
			data = data.Replace("uint fallback_codepoint = (uint)(0);", "char fallback_codepoint = (char)0;");
			data = data.Replace("uint fallback_codepoint", "char fallback_codepoint");
			data = data.Replace("(uint)(fallback_codepoint)", "fallback_codepoint");
			data = data.Replace("this.handle.width = nk_font_text_width;", "this.handle.width = this.TextWidth;");
			data = data.Replace("this.handle.query = nk_font_query_font_glyph;", "this.handle.query = this.QueryFontGlyph;");
			data = data.Replace("this.handle.userdata.ptr = this;", "");

			// FontBake
			data = data.Replace("(uint)(range->first_unicode_codepoint_in_range + char_idx)",
				"(char)(range->first_unicode_codepoint_in_range + char_idx)");

			// FontTextWidth
			data = data.Replace("FontTextWidth(Handle Handle", "FontTextWidth(Font font");
			data = data.Replace("Font font = (Font)(this.ptr);", "");

			// FontQueryFontGlyph
			data = data.Replace("FontQueryFontGlyph(Handle Handle", "FontQueryFontGlyph(Font font");
			data = data.Replace("font = (Font)(this.ptr);", "");

			// Font_init

			// decode_85_byte
			data = data.Replace("('\\')", "('\\\\')");

			// FontConfig_
			data = data.Replace("sizeof((cfg))", "sizeof(FontConfig)");

			// FontAtlas_init
			data = data.Replace("Nuklear.Zero(atlas, (ulong)(sizeof((atlas))));", "");

			// FontAtlas_add
			data = data.Replace("sizeof((*config))", "sizeof(FontConfig)");
			data = data.Replace("sizeof((*font))", "sizeof(Font)");
			data =
				data.Replace(
					"font = (Font)(this.permanent.alloc((Handle)(this.permanent.userdata), null, (ulong)(sizeof(Font))));",
					"font = new Font();");
			data = data.Replace("Nuklear.Zero(font, (ulong)(sizeof((font))));if (font== null) return null;", "");

			// FontAtlas_addFrom_Memory
			data = data.Replace("(config)?", "(config != null)?");

			// FontAtlas_add_compressedBase85

			// 
			data = data.Replace("Vec2[] CursorData", "Vec2[,] CursorData");

			// FontAtlas_end
			data = data.Replace("font_iter; font_iter = font_iter.next", "font_iter != null; font_iter = font_iter.next");

			// FontAtlas_cleanup
			data = data.Replace("iter; iter = iter->next", "iter != null; iter = iter->next");

			// FontAtlas_clear
			data = data.Replace("iter; iter = next", "iter != null; iter = next");

			// FontAtlasBake
			data = data.Replace("(sizeof(FontGlyph)", "((ulong)sizeof(FontGlyph)");
			data = data.Replace("*width *height", "*width * *height");
			data = data.Replace("uint fallback_glyph", "char fallback_glyph");
			data = data.Replace("(uint)(config.fallback_glyph)", "config.fallback_glyph");
			data = data.Replace("font_iter; font_iter", "font_iter != null; font_iter");
			data = data.Replace("(uint)('?')", "'?'");
			data = data.Replace("nk_cursor_data[i][", "nk_cursor_data[i,");

			// input_begin
			data = data.Replace("_in_.mouse.buttons[i].", "((MouseButton *)_in_.mouse.buttons + i)->");

			// convert
			data = data.Replace("((c) == (']'))) || ((c) == ('|')));", "((c) == (']'))) || ((c) == ('|'))?1:0);");

			data = data.Replace("cmd._string_ = _string_;",
				"cmd._string_ = new PinnedArray<char>(length); CRuntime.memcpy((void *)cmd._string_, _string_, length * sizeof(char));");

			data = data.Replace("this.undo_char + n", "(char *)this.undo_char + n");
			data = data.Replace("this.undo_rec + ", "(TextUndoRecord *)this.undo_rec + ");
			data = data.Replace("sizeof((this.undo_rec[0]))", "(ulong)sizeof(TextUndoRecord)");
			data = data.Replace("this.undo_rec[i].", "((TextUndoRecord *)this.undo_rec + i)->");
			data = data.Replace("this.undo_char + ", "(char *)this.undo_char + ");
			data = data.Replace("return &this.undo_rec[this.undo_point++];",
				"return (TextUndoRecord *)this.undo_rec + (this.undo_point++);");
			data = data.Replace("return &this.undo_char[r->char_storage];", "return (char *)this.undo_char + r->char_storage;");
			data = data.Replace("&s.undo_rec[s.redo_point - 1]", "(TextUndoRecord *)s.undo_rec + s.redo_point - 1");
			data = data.Replace("&s.undo_char[u.char_storage]", "(char *)s.undo_char + u.char_storage");
			data = data.Replace("&s.undo_char[r.char_storage]", "(char *)s.undo_char + r.char_storage");
			data = data.Replace("&s.undo_rec[s.undo_point]", "(TextUndoRecord *)s.undo_rec + s.undo_point");

			data = data.Replace("uint unicode;", "char unicode;");
			data = data.Replace("uint unicode = (uint)(0)", "char unicode = (char)0");
			data = data.Replace("uint _next_ = (uint)(0)", "char _next_ = (char)0");
			data = data.Replace("uint c;", "char c;");
			data = data.Replace("(uint)(unicode)", "unicode");
			data = data.Replace("uint unicode", "char unicode");

			data = data.Replace("(uint)(((_next_) == (0xFFFD))?'\\0':_next_)", "((_next_) == (0xFFFD))?'\0':_next_");
			data = data.Replace("uint* Text", "char* Text");
			data = data.Replace("(uint)(this._string_.RuneAt", "(char)(this._string_.RuneAt");
			data = data.Replace("uint* p = this.undo.Text", "char* p = this.undo.Text");
			data = data.Replace("unicode = (uint)", "unicode = (char)");

			data = data.Replace("(byte)((type) == (Nuklear.NK_TEXT_EDIT_SINGLE_LINE))",
				"(byte)((type) == (Nuklear.NK_TEXT_EDIT_SINGLE_LINE)?1:0)");
			data = data.Replace("(int)(sizeof((seperator)) / sizeof((seperator)[0]))", "1");

			data = data.Replace("X,", "&X,");

			// Textedit_discard_redo
			data = data.Replace("num * sizeof(char))));", "num * sizeof(char)));");

			// draw_symbol
			data =
				data.Replace(
					"char* X = ((type) == (Nuklear.NK_SYMBOL_X))?\"x\":((type) == (Nuklear.NK_SYMBOL_UNDERSCORE))?\"_\":((type) == (Nuklear.NK_SYMBOL_PLUS))?\"+\":\"-\";",
					"char X = ((type) == (Nuklear.NK_SYMBOL_X))?'x':((type) == (Nuklear.NK_SYMBOL_UNDERSCORE))?'_':((type) == (Nuklear.NK_SYMBOL_PLUS))?'+':'-';");

			// do_property
			data = data.Replace("Nuklear.NkPluginFilter* filters = stackalloc int (const nk_text_edit *, unsigned int)[2];",
				"Nuklear.NkPluginFilter[] filters = new Nuklear.NkPluginFilter[2];");
			data = data.Replace("char* _string_ = stackalloc sbyte[64];", "char* _string_ = stackalloc char[64];");
			data = data.Replace("sbyte* dst = null;", "char *dst = null;");
			data = data.Replace("int num_len;", "int num_len = 0;");

			// StyleFromTable
			data = data.Replace("StyleFromTable(Color* table)", "StyleFromTable(Color[] table)");
			data = data.Replace("StyleLoadAllCursors(Cursor cursors)", "StyleLoadAllCursors(Cursor[] cursors)");
			data = data.Replace("(!table)?", "(table == null)?");

			// Style_pushFont
			data = data.Replace("((int)(sizeof((font_stack.elements)) / sizeof((font_stack.elements)[0])))",
				"(int)font_stack.elements.Length");
			data = data.Replace("&font_stack.elements[font_stack.head++];",
				"(ConfigStackUserFontElement *)font_stack.elements + (font_stack.head++);");

			// Style_push_style_item
			data = data.Replace("((int)(sizeof((type_stack.elements)) / sizeof((type_stack.elements)[0])))",
				"(int)type_stack.elements.Length");
			data = data.Replace("&font_stack.elements[--font_stack.head];",
				"(ConfigStackUserFontElement *)font_stack.elements + (--font_stack.head);");

			// clear
			data = data.Replace("Nuklear.Zero(it, (ulong)(sizeof(PageData)));",
				"Nuklear.Zero(it, (ulong)(sizeof(nkTable)));");
			data = data.Replace("Nuklear.Zero(it, (ulong)(sizeof(union nk_page_data)));FreeTable(it);",
				"");

			// nkPanel_is_sub
			data = data.Replace("(type & Nuklear.NK_PANEL_SET_SUB)", "(type & Nuklear.NK_PANEL_SET_SUB) != 0");

			// nkPanel_is_nonblock
			data = data.Replace("(type & Nuklear.NK_PANEL_SET_NONBLOCK)", "(type & Nuklear.NK_PANEL_SET_NONBLOCK) != 0");

			// nkPanel_begin
			data = data.Replace("(win.flags & Nuklear.NK_WINDOW_NO_INPUT)?", "(win.flags & Nuklear.NK_WINDOW_NO_INPUT) != 0?");
			data = data.Replace("(uint)(~Nuklear.NK_WINDOW_MINIMIZED)", "(uint)(~(uint)Nuklear.NK_WINDOW_MINIMIZED)");
			data = data.Replace("(layout.flags & Nuklear.NK_WINDOW_MINIMIZED)?", "(layout.flags & Nuklear.NK_WINDOW_MINIMIZED) != 0?");

			// add_value
			data = data.Replace("return &win.tables.values[win.tables.size++];",
				"return (uint *)win.tables.values + (win.tables.size++);");

			// find_value
			data = data.Replace("return &iter.values[i];", "return (uint *)iter.values + i;");

			// begin_titled
			data = data.Replace("win.name_string[name_length] = (sbyte)(0)", "win.name_string[name_length] = (char)(0)");
			data = data.Replace("(!(win.flags & Nuklear.NK_WINDOW_MINIMIZED))?", "((win.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0)?");
			data = data.Replace("!(iter.flags & Nuklear.NK_WINDOW_HIDDEN)", "(iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0");
			data = data.Replace("!(iter.flags & Nuklear.NK_WINDOW_MINIMIZED)", "(iter.flags & Nuklear.NK_WINDOW_MINIMIZED) == 0");
			data = data.Replace("FreePanel(current.layout);", "");

			// end
			data = data.Replace("!(iter.flags & Nuklear.NK_WINDOW_HIDDEN)", "(iter.flags & Nuklear.NK_WINDOW_HIDDEN) == 0");

			// layout_space_end
			data = data.Replace("sizeof((layout.row.item))", "sizeof(Rect)");

			// TreeStateBase
			data = data.Replace("!(layout.flags & Nuklear.NK_WINDOW_ROM)", "(layout.flags & Nuklear.NK_WINDOW_ROM) == 0");

			// nkButton_push_behavior
			data = data.Replace("sizeof((button_stack.elements)) / sizeof((button_stack.elements)[0])",
				"(int)button_stack.elements.Length");
			data = data.Replace("&button_stack.elements[button_stack.head++]",
				"(Config_stackButton_behaviorElement *)button_stack.elements + (button_stack.head++);");
			data = data.Replace("&button_stack.elements[--button_stack.head]",
				"(Config_stackButton_behaviorElement *)button_stack.elements + (--button_stack.head);");

			// checkboxText
			data = data.Replace("return (int)(old_val != *active);", "return (old_val != *active)?1:0;");

			// progress
			data = data.Replace("return (int)(*cur != old_value);", "return (*cur != old_value)?1:0;");

			// edit_string
			data = data.Replace("(!filter)?", "(filter == null)?");

			// radioText
			data = data.Replace("return (int)(old_value != *active);", "return (old_value != *active)?1:0;");

			// slider_float
			data = data.Replace("return (int)(((old_value) > (value)) || ((old_value) < (value)));",
				"return (((old_value) > (value)) || ((old_value) < (value)))?1:0;");

			// edit_buffer
			data = data.Replace("(win.layout.flags & Nuklear.NK_WINDOW_ROM)?",
				"(win.layout.flags & Nuklear.NK_WINDOW_ROM) != 0?");
			data = data.Replace("(flags & Nuklear.NK_EDIT_READ_ONLY)?",
				"(flags & Nuklear.NK_EDIT_READ_ONLY) != 0?");

			// property_
			data = data.Replace("enum property_filter", "int");

			// property_
			data = data.Replace("sbyte* dummy_buffer = stackalloc sbyte[64];",
				"char* dummy_buffer = stackalloc char[64];");

			// chart_beginColored
			data = data.Replace("Nuklear.Zero(chart, (ulong)(sizeof((chart))));", "");

			// chart_add_slotColored
			data = data.Replace("enum nk_chart_type", "int");

			// chart_end
			data = data.Replace("Nuklear.Memset(chart, (int)(0), (ulong)(sizeof((chart))));", "");

			// plot_function
			data = data.Replace("IntPtr* value_getter", "Nuklear.NkFloatValueGetter value_getter");

			// group_scrolled_offset_begin
			data = data.Replace("Nuklear.Zero(panel, (ulong)(sizeof((panel))));", "");
			data = data.Replace("(flags & Nuklear.NK_WINDOW_TITLE)?", "(flags & Nuklear.NK_WINDOW_TITLE) != 0?");

			// group_scrolled_end
			data = data.Replace("Nuklear.Zero(pan, (ulong)(sizeof((pan))));", "");

			// popup_begin
			data = data.Replace("Nuklear.Zero(popup, (ulong)(sizeof((popup))));", "");

			// combo_begin
			data = data.Replace("(popup)?", "(popup != null)?");

			// combo
			data = data.Replace("sbyte** items", "char** items");

			// combo_separator
			data = data.Replace("sbyte* items_separated_by", "char* items_separated_by");
			data = data.Replace("sbyte* current_item;", "char* current_item;");
			data = data.Replace("sbyte* iter", "char* iter;");

			// combobox_callback
			data = data.Replace("IntPtr* item_getter", "Nuklear.NkComboCallback item_getter");
			data = data.Replace("sbyte* item;", "char* item;");

			// menu_begin
			data = data.Replace("popup?", "popup != null?");

			// Removing allocatog
			data = data.Replace("Allocator* a, ", "");
			data = data.Replace("Allocator* alloc, ", "");
			data = data.Replace(", Allocator* alloc", "");
			data = data.Replace("Allocator* alloc", "");
			data = data.Replace(", alloc", "");
			data = data.Replace("alloc, ", "");
			data = data.Replace(", &this.alloc", "");
			data = data.Replace("&this.alloc", "");
			data = data.Replace(", &baker->alloc", "");
			data = data.Replace(", &this.temporary", "");
			data = data.Replace("|| (alloc== null)", "");
			data = data.Replace("(alloc== null)", "");
			data = data.Replace("|| (a== null)", "");
			data = data.Replace("public Allocator alloc;", "");
			data = data.Replace("this.pool = (Allocator)(*a);", "");
			data = data.Replace(" || (this.pool.alloc== null)", "");
			data = data.Replace(" || (this.pool.free== null)", "");
			data = data.Replace("if (this.pool.free== null) return;", "");
			data = data.Replace("hh.alloc = (Allocator)(*alloc);", "");
			data = data.Replace("baker->alloc = (Allocator)(*alloc);", "");
			data = data.Replace(" || (this.temporary.alloc== null)", "");
			data = data.Replace(" || (this.temporary.free== null)", "");
			data = data.Replace(" || (this.permanent.alloc== null)", "");
			data = data.Replace(" || (this.permanent.free== null)", "");
			data = data.Replace("a->alloc((Handle)(a->userdata), null, ", "CRuntime.malloc(");
			data = data.Replace("this.pool.alloc((Handle)(this.pool.userdata), this.memory.ptr, ", "CRuntime.malloc(");
			data = data.Replace("alloc->alloc((Handle)(alloc->userdata), null, ", "CRuntime.malloc(");
			data = data.Replace("this.permanent.alloc((Handle)(this.permanent.userdata), null, ", "CRuntime.malloc(");
			data = data.Replace("this.alloc.alloc((Handle)(this.alloc.userdata), null, ", "CRuntime.malloc(");
			data = data.Replace("this.temporary.alloc((Handle)(this.temporary.userdata), null, ", "CRuntime.malloc(");
			data = data.Replace("this.pool.free((Handle)(this.pool.userdata), ", "CRuntime.free(");
			data = data.Replace("this.alloc.free((Handle)(this.alloc.userdata), ", "CRuntime.free(");
			data = data.Replace("alloc->free((Handle)(alloc->userdata), ", "CRuntime.free(");
			data = data.Replace("this.permanent.free((Handle)(this.permanent.userdata), ", "CRuntime.free(");
			data = data.Replace("this.temporary.free((Handle)(this.temporary.userdata), ", "CRuntime.free(");

			// rest
			data = data.Replace("Rect* clip = &this.clip;", "");
			data = data.Replace("clip->", "this.clip.");
			data = data.Replace("Rect* c = &this.clip;", "");
			data =
				data.Replace(
					"if ((((c->w) == (0)) || ((c->h) == (0))) || (!(!(((((c->x) > (r.x + r.w)) || ((c->x + c->w) < (r.x))) || ((c->y) > (r.y + r.h))) || ((c->y + c->h) < (r.y)))))) return;}",
					"if ((((this.clip.w) == (0)) || ((this.clip.h) == (0))) || (!(!(((((this.clip.x) > (r.x + r.w)) || ((this.clip.x + this.clip.w) < (r.x))) || ((this.clip.y) > (r.y + r.h))) || ((this.clip.y + this.clip.h) < (r.y)))))) return;}");
			data = data.Replace("Rect* c = &this.clip;", "");
			data =
				data.Replace(
					"if ((!(!(((((bounds.x) > (c->x + c->w)) || ((bounds.x + bounds.w) < (c->x))) || ((bounds.y) > (c->y + c->h))) || ((bounds.y + bounds.h) < (c->y)))))",
					"if ((!(!(((((bounds.x) > (win.layout.clip.x + win.layout.clip.w)) || ((bounds.x + bounds.w) < (win.layout.clip.x))) || ((bounds.y) > (win.layout.clip.y + win.layout.clip.h))) || ((bounds.y + bounds.h) < (win.layout.clip.y)))))");
			data = data.Replace("Rect* c = &win.layout.clip;", "");
			data = data.Replace("if ((custom) != null)", "");
			data = data.Replace("Zero(s, (ulong)(sizeof(Image)));", "");
			data = data.Replace(", (ulong)(sizeof((cmd)))", "");
			data = data.Replace(", (ulong)(sizeof((cmd)) + (ulong)(length + 1))", "");
			data = data.Replace("\tCommand* cmd;", "\t");
			data = data.Replace("for ((cmd) = Begin(); (cmd) != null; (cmd) = Next(cmd))",
				"var top_window = Begin(); foreach (var cmd in top_window.buffer.commands)");
			data = data.Replace("cmd->type", "cmd.header.type");
			data = data.Replace("this.memory, ", "");
			data = data.Replace("draw_list.userdata = (Handle)(cmd->userdata);",
				"draw_list.userdata = (Handle)(cmd.userdata);");
			data = data.Replace("iter; iter", "iter != null; iter");
			data = data.Replace("Zero(cfg, (ulong)(sizeof(FontConfig)));", "");
			data = data.Replace("Nuklear.Memcopy(cfg, config, (ulong)(sizeof((config))));", "cfg = config;");
			data = data.Replace("cfg = (FontConfig)(CRuntime.malloc((ulong)(sizeof(FontConfig))));", "");
			data = data.Replace("CRuntime.free(iter);", "");
			data = data.Replace("CRuntime.free(i);", "");
			data = data.Replace("&layout.row.templates[i]", "(float *)layout.row.templates + i");
			data = data.Replace("uint ws;", "uint ws = 0;");
			data = data.Replace("return (int)(TreeStateBase((int)(type), img, title, ref (int)(state)));",
				"int kkk = (int)(*state); int result=(int)(TreeStateBase((int)(type), img, title, ref kkk));*state = (uint)kkk;return result;");
			data = data.Replace("uint ws;", "uint ws = 0;");
			data = data.Replace("win.layout.offset_x = &win.scrollbar.x;", "win.layout.offset = win.scrollbar;");
			data = data.Replace("win.layout.offset_y = &win.scrollbar.y;", "");
			data = data.Replace("layout.menu.offset.x = (uint)(*layout.offset_x);", "layout.menu.offset = layout.offset;");
			data = data.Replace("layout.menu.offset.y = (uint)(*layout.offset_y);", "");
			data = data.Replace("popup.layout.offset_x = &popup.scrollbar.x;", "popup.layout.offset = popup.scrollbar;");
			data = data.Replace("popup.layout.offset_y = &popup.scrollbar.y;", "");
			data = data.Replace("*layout.offset_", "layout.offset.");
			data = data.Replace("layout.offset_", "layout.offset.");
			data = data.Replace("*g.offset_", "g.offset.");
			data = data.Replace("uint* x_offset, uint* y_offset", "Scroll offset");
			data = data.Replace("(uint)(*x_offset)", "offset.x");
			data = data.Replace("(uint)(*y_offset)", "offset.y");
			data = data.Replace("panel.layout.offset.x = x_offset;", "panel.layout.offset = offset;");
			data = data.Replace("panel.layout.offset.y = y_offset;", "");
			data = data.Replace("view.scroll_value = offset.y;", "view.scroll_value = *y_offset;");
			data = data.Replace("&scroll.x, &scroll.y", "scroll");
			data = data.Replace("x_offset, y_offset", "new Scroll {x = *x_offset, y = *y_offset}");
			data =
				data.Replace(
					"this.custom.FontBakeCustomData(this.pixel, (int)(width), (int)(height), Nuklear.nk_custom_cursor_data, (int)(90), (int)(27), ('.'), ('X'));",
					"fixed(byte *ptr = Nuklear.nk_custom_cursor_data) { this.custom.FontBakeCustomData(this.pixel, (int)(width), (int)(height), ptr, (int)(90), (int)(27), ('.'), ('X'));}");
			data = data.Replace("Nuklear.Zero(&layout.row.item, (ulong)(sizeof(Rect)));",
				"fixed(void *ptr = &layout.row.item) {Nuklear.Zero(ptr, (ulong)(sizeof(Rect)));}");
			data = data.Replace("element.address = &this.button_behavior;", "");
			data = data.Replace("*element.address = (int)(element.old_value);", "this.button_behavior = element.old_value;");
			data = data.Replace("return FontTextWidth(Handle", "return FontTextWidth(font");

			data = data.Replace("Nuklear.Zero(b, (ulong)(sizeof((b))));", "");
			data = data.Replace("size = (ulong)(sizeof((cmd)) + sizeof(short)* 2 * (ulong)(point_count));", "");

			data = data.Replace("(Colorf)({ 0, 0, 0, 0 })", "new Colorf()");
			data = data.Replace("(Handle)({ null })", "new Handle()");
			data = data.Replace("ctrl[0]", "ctrl_0");
			data = data.Replace("ctrl[1]", "ctrl_1");
			data = data.Replace("Nuklear.Zero(list, (ulong)(sizeof((list))));", "");
			data = data.Replace("(Vec2)(g.uv[0]), (Vec2)(g.uv[1])",
				"Nuklear.Vec2z(g.uv_x[0], g.uv_y[0]), Nuklear.Vec2z(g.uv_x[1], g.uv_y[1])");
			data = data.Replace("glyph->uv[0] = (Vec2)(Nuklear.Vec2z((float)(g->u0), (float)(g->v0)));",
				"glyph->uv_x[0] = g->u0; glyph->uv_y[0] = g->v0;");
			data = data.Replace("glyph->uv[1] = (Vec2)(Nuklear.Vec2z((float)(g->u1), (float)(g->v1)));",
				"glyph->uv_x[1] = g->u1; glyph->uv_y[1] = g->v1;");
			data = data.Replace("Nuklear.Memset(this, (int)(0), (ulong)(sizeof(TextEdit)));", "");
			data = data.Replace("(Rect)({ 0, 0, 0, 0 })", "new Rect()");
			data = data.Replace("Nuklear.Zero(button, (ulong)(sizeof((button))));", "");
			data = data.Replace("Nuklear.Zero(toggle, (ulong)(sizeof((toggle))));", "");
			data = data.Replace("Nuklear.Zero(select, (ulong)(sizeof((select))));", "");
			data = data.Replace("Nuklear.Zero(slider, (ulong)(sizeof((slider))));", "");
			data = data.Replace("Nuklear.Zero(prog, (ulong)(sizeof((prog))));", "");
			data = data.Replace("Nuklear.Zero(scroll, (ulong)(sizeof((scroll))));", "");
			data = data.Replace("Nuklear.Zero(edit, (ulong)(sizeof((edit))));", "");
			data = data.Replace("Nuklear.Zero(property, (ulong)(sizeof((property))));", "");
			data = data.Replace("Nuklear.Zero(ctx, (ulong)(sizeof((ctx))));", "");
			data =
				data.Replace(
					"if ((this.use_pool) != 0) this.memory.Clear(); else this.memory.Reset((int)(Nuklear.NK_BUFFER_FRONT));",
					"this.memory.Reset((int)(Nuklear.NK_BUFFER_FRONT));");
			data = data.Replace("Nuklear.Zero(this.current.layout, (ulong)(sizeof((this.current.layout))));", "");
			data = data.Replace("Nuklear.Zero(current.layout, (ulong)(sizeof((current.layout))));", "");
			data = data.Replace("Nuklear.Zero(this.current.layout, (ulong)(sizeof(Panel)));", "");
			data = data.Replace("(layout.flags & Nuklear.NK_WINDOW_DYNAMIC)?", "(layout.flags & Nuklear.NK_WINDOW_DYNAMIC) != 0?");
			data = data.Replace("Nuklear.Zero(window.property, (ulong)(sizeof((window.property))));", "");
			data = data.Replace("Nuklear.Zero(window.edit, (ulong)(sizeof((window.edit))));", "");
			data = data.Replace("Nuklear.Zero(this, (ulong)(sizeof((this))));", "");
			data = data.Replace("(this== null) || ", "");
			data = data.Replace("if (this == null) return;", "");
			data = data.Replace("if (this== null) return;", "");
			data = data.Replace("if (((((this== null))))) return;", "");
			data = data.Replace("if (((this == null))) return;", "");
			data = data.Replace("Nuklear.Memset(this.overlay, (int)(0), (ulong)(sizeof((this.overlay))));", "");
			data = data.Replace("Decode85Byte(char c)", "Decode85Byte(sbyte c)");
			data = data.Replace("(RpContext*)(this.spc.pack_info)", "((RpContext*)(this.spc.pack_info))");
			data = data.Replace("uint* Create", "char* Create");
			data = data.Replace("uint* p = this.undo", "char* p = this.undo");

			return data;
		}
	}
}
