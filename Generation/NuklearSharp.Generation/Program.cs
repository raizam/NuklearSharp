using System;
using System.IO;
using Sichem;

namespace NuklearSharp.Generation
{
	class Program
	{
		static void Process()
		{
			using (var output = new StringWriter())
			{
				var parameters = new ConversionParameters
				{
					InputPath = @"nuklear.h",
					Output = output,
					Defines = new[]
					{
						"NK_IMPLEMENTATION",
						"NK_INCLUDE_DEFAULT_ALLOCATOR",
						"NK_INCLUDE_VERTEX_BUFFER_OUTPUT",
						"NK_INCLUDE_FONT_BAKING",
						"NK_INCLUDE_DEFAULT_FONT",
						"NK_INCLUDE_COMMAND_USERDATA"
					},
					Namespace = "NuklearSharp",
					Class = "Nuklear",
					SkipStructs = new[]
					{
						"nk_allocator",
						"nk_memory_status",
						"nk_buffer_marker",
						"nk_memory",
						"nk_str",
						"nk_pool",
						"nk_context",
						"nk_convert_config",
						"nk_user_font",
						"nk_buffer",
						"nk_clipboard",
						"nk_text_undo_state",
						"nk_text_edit",
						"nk_style_item",
						"nk_style",
						"nk_page_element",
						"nk_property",
						"nk_property_variant",
						"nk_draw_list",
						"nk_command",
						"nk_command_buffer",
						"nk_command_scissor",
						"nk_command_line",
						"nk_command_curve",
						"nk_command_rect",
						"nk_command_rect_filled",
						"nk_command_rect_multi_color",
						"nk_command_triangle",
						"nk_command_triangle_filled",
						"nk_command_circle",
						"nk_command_circle_filled",
						"nk_command_arc",
						"nk_command_arc_filled",
						"nk_command_polygon",
						"nk_command_polygon_filled",
						"nk_command_polyline",
						"nk_command_image",
						"nk_command_text",
						"nk_command_buffer",
						"nk_command_scissor",
						"nk_command_line",
						"nk_command_curve",
						"nk_command_rect",
						"nk_command_rect_filled",
						"nk_command_rect_multi_color",
						"nk_command_triangle",
						"nk_command_triangle_filled",
						"nk_command_circle",
						"nk_command_circle_filled",
						"nk_command_arc",
						"nk_command_arc_filled",
						"nk_command_polygon",
						"nk_command_polygon_filled",
						"nk_command_polyline",
						"nk_command_image",
						"nk_command_text",
						"nk_command_buffer",
						"nk_command_custom",
						"nk_mouse",
						"nk_keyboard",
						"nk_input",
						"nk_popup_buffer",
						"nk_page_data",
						"nk_page",
						"nk_window",
						"nk_table",
						"nk_property_state",
						"nk_popup_state",
						"nk_font_baker",
						"nk_tt__hheap",
						"nk_font_atlas",
						"nk_tt_pack_context",
						"nk_font",
					},
					SkipGlobalVariables = new[]
					{
						"nk_null_rect",
						"nk_red",
						"nk_green",
						"nk_blue",
						"nk_white",
						"nk_black",
						"nk_yellow",
						"nk_default_color_style",
						"nk_color_names",
						"nk_proggy_clean_ttf_compressed_data_base85",
						"nk_custom_cursor_data",
						"nk_cursor_data",
					},
					SkipFunctions = new[]
					{
						"nk_inv_sqrt",
						"nk_command_buffer_init",
						"nk_command_buffer_reset",
						"nk_command_buffer_push",
						"nk_buffer_init_default",
						"nk_buffer_init",
						"nk_buffer_init_fixed",
						"nk_buffer_align",
						"nk_buffer_realloc",
						"nk_buffer_alloc",
						"nk_buffer_push",
						"nk_buffer_mark",
						"nk_buffer_reset",
						"nk_buffer_clear",
						"nk_buffer_free",
						"nk_buffer_info",
						"nk_buffer_memory",
						"nk_buffer_memory_const",
						"nk_buffer_total",
						"nk_free",
						"nk_str_init_default",
						"nk_str_init",
						"nk_str_init_fixed",
						"nk_str_append_text_char",
						"nk_str_append_str_char",
						"nk_str_append_text_utf8",
						"nk_str_append_str_utf8",
						"nk_str_append_text_runes",
						"nk_str_append_str_runes",
						"nk_str_insert_at_char",
						"nk_str_insert_at_rune",
						"nk_str_insert_text_char",
						"nk_str_insert_str_char",
						"nk_str_insert_text_utf8",
						"nk_str_insert_str_utf8",
						"nk_str_insert_text_runes",
						"nk_str_insert_str_runes",
						"nk_str_remove_chars",
						"nk_str_remove_runes",
						"nk_str_delete_chars",
						"nk_str_delete_runes",
						"nk_str_at_char",
						"nk_str_at_rune",
						"nk_str_at_char_const",
						"nk_str_at_const",
						"nk_str_rune_at",
						"nk_str_get",
						"nk_str_get_const",
						"nk_str_len",
						"nk_str_len_char",
						"nk_str_clear",
						"nk_str_free",
						"nk_strlen",
						"nk_strtoi",
						"nk_strtod",
						"nk_strtof",
						"nk_stricmp",
						"nk_stricmpn",
						"nk_str_match_here",
						"nk_str_match_star",
						"nk_strfilter",
						"nk_strmatch_fuzzy_text",
						"nk_strmatch_fuzzy_string",
						"nk_string_float_limit",
						"nk_strrev_ascii",
						"nk_itoa",
						"nk_dtoa",
						"nk_utf_validate",
						"nk_utf_decode_byte",
						"nk_utf_decode",
						"nk_utf_encode_byte",
						"nk_utf_encode",
						"nk_utf_len",
						"nk_utf_at",
						"nk_draw_list_setup",
						"nk__draw_list_begin",
						"nk__draw_list_end",
						"nk__draw_list_next",
						"nk_draw_list_clear",
						"nk_draw_list_alloc_path",
						"nk_draw_list_path_last",
						"nk_draw_list_push_command",
						"nk_draw_list_command_last",
						"nk_draw_list_path_fill",
						"nk_draw_list_path_stroke",
						"nk__begin",
						"nk__next",
						"nk__draw_begin",
						"nk__draw_end",
						"nk__draw_next",
						"nk_input_glyph",
						"nk_input_unicode",
						"nk_input_char",
						"nk_textedit_get_width",
						"nk_textedit_layout_row",
						"nk_memcopy",
						"nk_memset",
						"nk_text_clamp",
						"nk_text_calculate_text_bounds",
						"nk_draw_text",
						"nk_draw_list_add_text",
						"nk_textedit_init_fixed",
						"nk_textedit_init",
						"nk_textedit_paste",
						"nk_textedit_text",
						"nk_style_get_color_by_name",
						"nk_pool_init",
						"nk_pool_free",
						"nk_pool_init_fixed",
						"nk_pool_alloc",
						"nk_init_default",
						"nk_init_fixed",
						"nk_init_custom",
						"nk_init",
						"nk_build",
						"nk_clear",
						"nk_create_page_element",
						"nk_link_page_element_into_freelist",
						"nk_free_page_element",
						"nk_create_panel",
						"nk_free_panel",
						"nk_create_table",
						"nk_free_table",
						"nk_push_table",
						"nk_remove_table",
						"nk_add_value",
						"nk_find_value",
						"nk_create_window",
						"nk_free_window",
						"nk_find_window",
						"nk_insert_window",
						"nk_remove_window",
						"nk_begin_titled",
						"nk_tt__find_table",
						"nk_font_baker_memory",
						"nk_font_baker_",
						"nk_font_atlas_init_default",
						"nk_font_atlas_init",
						"nk_font_atlas_init_custom",
						"nk_font_atlas_begin",
						"nk_font_atlas_bake",
					},
					Classes = new[]
					{
						"nk_allocator",
						"nk_user_font",
						"nk_buffer",
						"nk_str",
						"nk_clipboard",
						"nk_mouse",
						"nk_keyboard",
						"nk_convert_config",
						"nk_list_view",
						"nk_page_element",
						"nk_context",
						"nk_draw_list",
						"nk_chart",
						"nk_panel",
						"nk_popup_state",
						"nk_window",
						"nk_config_stack_style_item",
						"nk_config_stack_float",
						"nk_config_stack_vec2",
						"nk_config_stack_flags",
						"nk_config_stack_color",
						"nk_config_stack_user_font",
						"nk_config_stack_button_behavior",
						"nk_page",
						"nk_pool",
						"nk_input",
						"nk_text_edit",
						"nk_command_scissor",
						"nk_command_line",
						"nk_command_curve",
						"nk_command_rect",
						"nk_command_rect_filled",
						"nk_command_rect_multi_color",
						"nk_command_triangle",
						"nk_command_triangle_filled",
						"nk_command_circle",
						"nk_command_circle_filled",
						"nk_command_arc",
						"nk_command_arc_filled",
						"nk_command_polygon",
						"nk_command_polygon_filled",
						"nk_command_polyline",
						"nk_command_image",
						"nk_command_text",
						"nk_command_buffer",
						"nk_command_scissor",
						"nk_command_line",
						"nk_command_curve",
						"nk_command_rect",
						"nk_command_rect_filled",
						"nk_command_rect_multi_color",
						"nk_command_triangle",
						"nk_command_triangle_filled",
						"nk_command_circle",
						"nk_command_circle_filled",
						"nk_command_arc",
						"nk_command_arc_filled",
						"nk_command_polygon",
						"nk_command_polygon_filled",
						"nk_command_polyline",
						"nk_command_image",
						"nk_command_text",
						"nk_command_buffer",
						"nk_command_custom",
						"nk_text_undo_state",
						"nk_draw_command",
						"nk_mouse_button",
						"nk_key",
						"nk_style",
						"nk_style_button",
						"nk_style_toggle",
						"nk_style_selectable",
						"nk_style_slider",
						"nk_style_progress",
						"nk_style_scrollbar",
						"nk_style_edit",
						"nk_style_property",
						"nk_style_chart",
						"nk_style_combo",
						"nk_style_tab",
						"nk_style_window_header",
						"nk_style_window",
						"nk_config_stack_user_font_element",
						"nk_cursor",
						"nk_property_state",
						"nk_font_atlas",
						"nk_font",
						"nk_rp_context",
						"nk_tt_pack_context",
						"nk_font_baker",
					},
					GlobalArrays = new[]
					{
						"nk_utfbyte",
						"nk_utfmask",
						"nk_utfmin",
						"nk_utfmax",
						"nk_proggy_clean_ttf_compressed_data_base85",
						"nk_custom_cursor_data",
						"nk_cursor_data",
					}
				};

				var cp = new ClangParser();

				cp.Process(parameters);
				var data = output.ToString();

				// Post processing
				Logger.Info("Post processing...");

				// Build has of C functions
				data = Utility.ReplaceNativeCalls(data);

				data = data.Replace("(void *)(0)", "null");
				data = data.Replace("public IntPtr* draw_begin;", "public NkDrawNotify draw_begin;");
				data = data.Replace("public IntPtr* draw_end;", "public NkDrawNotify draw_end;");
				data = data.Replace("enum nk_anti_aliasing", "int");
				data = data.Replace("- -", "-");
				data = data.Replace("* *", "*");
				data = data.Replace("+ +", "+");
				data = data.Replace("((length) < (sizeof(int)))))", "((length) < (sizeof(int))))");
				data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
				data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
				data = data.Replace("unsignedint", "uint");
//				data = data.Replace("(sizeof(uint)>)", "(sizeof(uint))");
	//			data = data.Replace("(3 * sizeof(uint))))", "(3 * sizeof(uint)))");
//				data = data.Replace("t = (ulong)(size / sizeof(uint););", "t = (ulong)(size / sizeof(uint));");
				data = data.Replace("for (pow = (int)(0); *p; p++)", "for (pow = (int)(0); *p != 0; p++)");
				data = data.Replace("(int)((((d) >= (0)) << 1) - 1);", "(int)((((d) >= (0)?1:0) << 1) - 1);");
				data = data.Replace("best_letter = 0", "best_letter = null");
				data = data.Replace("best_letter != 0", "best_letter != null");
				data = data.Replace("(int)(nk_is_lower((int)(str_letter)) != 0);", "(int)(nk_is_lower((int)(str_letter)) != 0?1:0);");
				data = data.Replace("''", "'\\0'");
				data = data.Replace("'\n'", "'\\n'");
				data = data.Replace("'\r'", "'\\r'");
				data = data.Replace("sizeof((handle))", "sizeof(nk_handle)");
				data = data.Replace("sizeof((s))", "sizeof(nk_image)");
				data = data.Replace("(int)(!(((img->w) == (0)) && ((img->h) == (0))));",
					"(int)((((img->w) == (0)) && ((img->h) == (0)))?1:0);");
				data = data.Replace("((!sep_len)?len:sep_len)", "((sep_len == 0)?len:sep_len)");

				// nk_murmur_hash
				data = data.Replace("union (anonymous at nuklear.h:6645:5) conv = (union (anonymous at nuklear.h:6645:5))({ null });",
					"nk_murmur_hash_union conv = new nk_murmur_hash_union(null);");
				data = data.Replace("i; ++i", "i != 0; ++i");

				//
				data = data.Replace(", (ulong)(sizeof((cmd)))", "");
				data = data.Replace("sizeof((nk_utfmask)) / sizeof((nk_utfmask)[0])", "nk_utfmask.Length");
				data =
					data.Replace(
						"full = (int)((b.size - ((b.size) < (size + alignment)?(b.size):(size + alignment))) <= b.allocated);",
						"full = (int)((b.size - ((b.size) < (size + alignment)?(b.size):(size + alignment))) <= b.allocated?1:0);");
				data = data.Replace("nk_memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char))));",
					"nk_memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char)));");
				data = data.Replace("(sizeof((list.circle_vtx)) / sizeof((list.circle_vtx)[0]))", "(ulong)list.circle_vtx.Length");
				data = data.Replace("sizeof(structnk_vec2);", "sizeof(nk_vec2)");
				data = data.Replace("sizeof(structnk_draw_command);", "sizeof(nk_draw_command)");
				data = data.Replace("sizeof(nk_draw_index);", "sizeof(nk_draw_index)");
				data = data.Replace("sizeof((col))", "sizeof(nk_color)");
				data = data.Replace("sizeof((bgra))", "sizeof(nk_color)");
				data = data.Replace("sizeof((color))", "sizeof(uint)");
				data =
					data.Replace(
						"return (int)(((element.attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element.format) == (NK_FORMAT_COUNT)));",
						"return (int)(((element.attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element.format) == (NK_FORMAT_COUNT))?1:0);");
				data = data.Replace("(sizeof(nk_command_arc_filled)", "((ulong)sizeof(nk_command_arc_filled)");
				data = data.Replace("sizeof(short);)", "sizeof(short))");
				data = data.Replace("sizeof((values[value_index]))", "sizeof(float)");
				data = data.Replace("nk_memcopy(attribute, &value, (ulong)(sizeof((value))));",
					"nk_memcopy(attribute, &value, (ulong)(sizeof(double)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(float))));",
					"attribute = (void *)(((sbyte*)(attribute) + sizeof(float)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(double))));",
					"attribute = (void *)(((sbyte*)(attribute) + sizeof(double)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(char))));",
					"attribute = (void *)(((sbyte*)(attribute) + sizeof(char)));");
				data = data.Replace("attribute = (void *)((sbyte*)(attribute) + sizeof((value)));",
					"attribute = (void *)((sbyte*)(attribute) + sizeof(short));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(nk_int))));",
					"attribute = (void *)(((sbyte*)(attribute) + sizeof(int)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(unsignedchar))));",
					"attribute = (void *)(((sbyte*)(attribute) + sizeof(byte)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(nk_uint))));",
					"attribute = (void *)(((sbyte*)(attribute) + sizeof(uint)));");
				data = data.Replace("pnt_size * ((thick_line) != 0?5:3)", "pnt_size * (ulong)((thick_line) != 0?5:3)");
				data =
					data.Replace(
						"(int)((((rect.x) <= (i.mouse.pos.x)) && ((i.mouse.pos.x) < (rect.x + rect.w))) && (((rect.y) <= (i.mouse.pos.y)) && ((i.mouse.pos.y) < (rect.y + rect.h))));",
						"(((rect.x) <= (i.mouse.pos.x)) && ((i.mouse.pos.x) < (rect.x + rect.w))) && (((rect.y) <= (i.mouse.pos.y)) && ((i.mouse.pos.y) < (rect.y + rect.h)))?1:0;");
				data =
					data.Replace(
						"(int)((((rect.x) <= (i.mouse.prev.x)) && ((i.mouse.prev.x) < (rect.x + rect.w))) && (((rect.y) <= (i.mouse.prev.y)) && ((i.mouse.prev.y) < (rect.y + rect.h))));",
						"(((rect.x) <= (i.mouse.prev.x)) && ((i.mouse.prev.x) < (rect.x + rect.w))) && (((rect.y) <= (i.mouse.prev.y)) && ((i.mouse.prev.y) < (rect.y + rect.h)))?1:0;");
				data = data.Replace("return (int)((i.mouse.buttons[id].down== 0) && ((i.mouse.buttons[id].clicked) != 0));",
					"return ((i.mouse.buttons[id].down== 0) && ((i.mouse.buttons[id].clicked) != 0))?1:0;");

				data = data.Replace("_string_.len", "_string_.Length");
				data = data.Replace("nk_zero(&r, (ulong)(sizeof((r))));", "nk_zero(&r, (ulong)(sizeof(nk_text_edit_row)));");

				string[] stringNames =
				{
					"ctext", "text", "_string_", "label", "title", "txt", "str", "name", "begin", "X", "line"
				};

				foreach (var sn in stringNames)
				{
					data = data.Replace("sbyte* " + sn + ", int total_len", "StringView " + sn);
					data = data.Replace("sbyte* " + sn + ", int length", "StringView " + sn);
					data = data.Replace("sbyte* " + sn + ", int len", "StringView " + sn);
					data = data.Replace("sbyte* " + sn + ", int text_len", "StringView " + sn);
					data = data.Replace("sbyte* " + sn + ", int byte_len", "StringView " + sn);
					data = data.Replace("sbyte* " + sn, "StringView " + sn);

					data = data.Replace(sn + ", " + "(int)(len)", sn);
				}

				data = data.Replace("&text[len]", "text+len");

				data = data.Replace("return &state.undo_char[r.char_storage];", "return (char *)state.undo_char + r.char_storage;");
				data = data.Replace("active = (int)(!active);", "active = active != 0?0:1;");
				data = data.Replace("*value = (int)(!(*value));", "*value = *value != 0?0:1;");
				data = data.Replace("return (int)(was_active != *active);", "return was_active != *active?1:0;");
				data = data.Replace("return (int)(old_value != *value);", "return old_value != *value?1:0;");
				data = data.Replace("int (const nk_text_edit *, unsigned int)*", "NkPluginFilter");
				data = data.Replace("enum nk_text_edit_type", "int");

				data = data.Replace("(flags & NK_EDIT_MULTILINE)?", "(flags & NK_EDIT_MULTILINE)!=0?");

				data =
					data.Replace(
						"edit.active = (byte)((((bounds.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (bounds.x + bounds.w))) && (((bounds.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (bounds.y + bounds.h))));",
						"edit.active = (byte)((((bounds.x) <= (_in_.mouse.pos.x)) && ((_in_.mouse.pos.x) < (bounds.x + bounds.w))) && (((bounds.y) <= (_in_.mouse.pos.y)) && ((_in_.mouse.pos.y) < (bounds.y + bounds.h)))?1:0);");

				data = data.Replace("void (void *, short, short, unsigned short, unsigned short, nk_handle)*",
					"NkCommandCustomCallback");

				data = data.Replace("nk_memcopy(cmd._string_, _string_, (ulong)(length));",
					"cmd._string_ = _string_;");
				data = data.Replace("cmd._string_[length] = (sbyte)('\\0');", "");

				data = data.Replace("nk_draw_vertex_layout_element* elem_iter = config.vertex_layout;",
					"var elem_iter = 0;");
				data = data.Replace("(nk_draw_vertex_layout_element)(*elem_iter)", "(config.vertex_layout[elem_iter])");
				data = data.Replace("elem_iter->", "config.vertex_layout[elem_iter].");

				data = data.Replace("ulong vertex_offset;", "");
				data = data.Replace("vertex_offset = ((ulong)((byte*)(vtx) - (byte*)(list.vertices.memory.ptr)));", "");
				data = data.Replace("vtx = (void *)((byte*)(list.vertices.memory.ptr) + vertex_offset);", "");
				data = data.Replace("list.path_offset = (uint)(0);", "");

				// nk_rect_height_compare
				data = data.Replace("return (int)(((p->w) > (q->w))?-1:((p->w) < (q->w)));", "return (int)(((p->w) > (q->w))?-1:((p->w) < (q->w))?1:0);");

				// nk_rect_original_order
				data = data.Replace("return (int)(((p->was_packed) < (q->was_packed))?-1:((p->was_packed) > (q->was_packed)));",
					"return (int)(((p->was_packed) < (q->was_packed))?-1:((p->was_packed) > (q->was_packed))?1:0);");

				// nk_rp_init_target
				data = data.Replace("&context.extra[0]", "(nk_rp_node *)context.extra");
				data = data.Replace("&context.extra[1]", "(nk_rp_node *)context.extra + 1");
				data = data.Replace("context.extra[", "((nk_rp_node *)context.extra)[");

				// nk_rp_pack_rects
				data = data.Replace("(int)(!(((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff))));", 
					"(int)((((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff)))?0:1);");

				// nk_rp_qsort
				data = data.Replace("nk_rp_qsort(nk_rp_rect* array, uint len, IntPtr* cmp)",
					"nk_rp_qsort(nk_rp_rect* array, uint len, QSortComparer cmp)");
				data = data.Replace("for (right = (uint)(left - 1); {", "for (right = (uint)(left - 1);;) {");
				data = data.Replace("; ) {}", "");

				// nk_tt__hheap_alloc
				data = data.Replace("(sizeof(nk_tt__hheap_chunk)", "((ulong)sizeof(nk_tt__hheap_chunk)");

				// nk_tt__hheap_cleanup
				data = data.Replace("hh->alloc.free((nk_handle)(hh->alloc.userdata), ", "CRuntime.free(");

				// nk_tt__new_active
				data = data.Replace("(sizeof((*z))", "(sizeof(nk_tt__active_edge)");

				// nk_tt_GetGlyphShape
				data = data.Replace(", nk_allocator alloc", "");
				data = data.Replace(", alloc", "");
				data = data.Replace("(flags & 16)?dx:-dx", "(flags & 16) != 0?dx:-dx");
				data = data.Replace("(flags & 32)?dy:-dy", "(flags & 32) != 0?dy:-dy");
				data = data.Replace("!(flags & 1)", "(flags & 1)==0?1:0");
				data = data.Replace("nk_tt_GetGlyphShape(info, alloc", "nk_tt_GetGlyphShape(info");
				data = data.Replace("hh->alloc.alloc((nk_handle)(hh->alloc.userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("alloc.alloc((nk_handle)(alloc.userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("sizeof((vertices[0]))", "sizeof(nk_tt_vertex)");
				data = data.Replace("sizeof(nk_tt_vertex)", "(ulong)sizeof(nk_tt_vertex)");
				data = data.Replace("alloc.free((nk_handle)(alloc.userdata), ", "CRuntime.free(");

				// nk_tt__sort_edges_ins_sort
				data = data.Replace("(int)(((a)->y0) < ((b)->y0))", "(int)(((a)->y0) < ((b)->y0)?1:0)");

				// nk_tt__sort_edges_quicksort
				data = data.Replace("for (++i; {", "for (;;++i) {");
				data = data.Replace("for (--j; {", "for (;;--j) {");

				// nk_tt__rasterize
				data = data.Replace("sizeof((*e))", "(ulong)sizeof(nk_tt__edge)");
				data = data.Replace("(((invert) != 0?(p[j].y) > (p[k].y):(p[j].y) < (p[k].y)) != 0)", "(invert != 0?(p[j].y > p[k].y):(p[j].y < p[k].y))");

				// nk_tt__rasterize_sorted_edges
				data = data.Replace("sizeof((hh))", "sizeof(nk_tt__hheap)");
				data = data.Replace("hh.alloc = (nk_allocator)(alloc);", "");
				data = data.Replace("(result->w * 2 + 1) * sizeof(float)))));", "(result->w * 2 + 1) * sizeof(float))));");
				data = data.Replace("sizeof((scanline[0]))", "sizeof(float)");

				// nk_tt_FlattenCurves
				data = data.Replace("sizeof((**contour_lengths))", "(ulong)sizeof(int)");
				data = data.Replace("sizeof((points[0]))", "(ulong)sizeof(nk_tt__point)");

				// nk_tt_PackBegin
				data = data.Replace("nk_rp_context context = (nk_rp_context)(CRuntime.malloc((ulong)(sizeof((context)))));",
					"nk_rp_context context = new nk_rp_context();");
				data = data.Replace("sizeof((*nodes))", "(ulong)sizeof(nk_rp_node)");
				data = data.Replace("CRuntime.free(context);", "");

				// nk_font_baker_memory
				data = data.Replace("for (iter = config_list; iter;", "for (iter = config_list; iter != null;");
				data = data.Replace("* sizeof(nk_", "* (ulong)sizeof(nk_");

				// nk_tt_PackEnd
				data = data.Replace("CRuntime.free(spc.pack_info);", "");

				// nk_font_bake_pack
				data = data.Replace("config_iter; config_iter", "config_iter != null; config_iter");
				data = data.Replace("sizeof((custom_space))", "sizeof(nk_rp_rect)");
				data = data.Replace("tmp->rects, baker.alloc", "tmp->rects");
				data = data.Replace("nk_tt_PackEnd(baker.spc, baker.alloc);", "nk_tt_PackEnd(baker.spc);");

				// nk_font_text_width
				data = data.Replace("nk_font_text_width(nk_handle handle", "nk_font_text_width(nk_font font");
				data = data.Replace("nk_font font = (nk_font)(handle.ptr);", "");
				data = data.Replace("nk_utf_decode(text, &unicode, (int)(len)))", "nk_utf_decode(text, &unicode, text.Length))");
				data = data.Replace("(text_len <= len)", "(text_len <= text.Length)");
				data = data.Replace("(int)(len - text_len)));text_len += (int)(glyph_len);", "(int)(text.Length - text_len)));text_len += (int)(glyph_len);");

				// nk_font_query_font_glyph
				data = data.Replace("nk_font_query_font_glyph(nk_handle handle", "nk_font_query_font_glyph(nk_font font");
				data = data.Replace("nk_font font;", "");
				data = data.Replace("font = (nk_font)(handle.ptr);", "");
				data = data.Replace("uint codepoint = (uint)(0);", "char codepoint = (char)0;");
				data = data.Replace("uint codepoint", "char codepoint");
				data = data.Replace("(uint)(codepoint)", "codepoint");

				// nk_font_init
				data = data.Replace("uint fallback_codepoint = (uint)(0);", "char fallback_codepoint = (char)0;");
				data = data.Replace("uint fallback_codepoint", "char fallback_codepoint");
				data = data.Replace("(uint)(fallback_codepoint)", "fallback_codepoint");

				// nk_font_bake
				data = data.Replace("(uint)(range->first_unicode_codepoint_in_range + char_idx)", "(char)(range->first_unicode_codepoint_in_range + char_idx)");

				// nk_font_init
				data = data.Replace("font.handle.width = nk_font_text_width;", "font.handle.WidthDelegate = font.text_width;");
				data = data.Replace("font.handle.userdata.ptr = font;", "");
				data = data.Replace("font.handle.query = nk_font_query_font_glyph;", "font.handle.query = font.query_font_glyph;");

				// nk_decode_85_byte
				data = data.Replace("('\\')", "('\\\\')");

				// nk_font_config_
				data = data.Replace("sizeof((cfg))", "sizeof(nk_font_config)");

				// nk_font_atlas_add
				data = data.Replace("|| (atlas.permanent.alloc== null)", "");
				data = data.Replace("|| (atlas.permanent.free== null)", "");
				data = data.Replace("|| (atlas.temporary.alloc== null)", "");
				data = data.Replace("|| (atlas.temporary.free== null)", "");
				data = data.Replace("(nk_font_config*)(atlas.permanent.alloc((nk_handle)(atlas.permanent.userdata), null, ",
					"(nk_font_config*)(CRuntime.malloc(");
				data = data.Replace("sizeof((*config))", "sizeof(nk_font_config)");
				data = data.Replace("font = (nk_font)(atlas.permanent.alloc((nk_handle)(atlas.permanent.userdata), null, (ulong)(sizeof(nk_font))));",
					"font = new nk_font();");
				data = data.Replace("nk_zero(font, (ulong)(sizeof((font))));if (font== null) return null;", "");
				data = data.Replace("atlas.permanent.alloc((nk_handle)(atlas.permanent.userdata), null, ", "CRuntime.malloc(");

				// nk_font_atlas_add_from_memory
				data = data.Replace("(config)?", "(config != null)?");

				// nk_font_atlas_add_compressed_base85
				data = data.Replace("nk_strlen(data_base85)", "CRuntime.strlen(data_base85)");
				data = data.Replace("atlas.temporary.alloc((nk_handle)(atlas.temporary.userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("atlas.temporary.free((nk_handle)(atlas.temporary.userdata), ", "CRuntime.free(");

				// 
				data = data.Replace("nk_vec2[] nk_cursor_data", "nk_vec2[,] nk_cursor_data");

				// nk_font_atlas_end
				data = data.Replace("font_iter; font_iter = font_iter.next", "font_iter != null; font_iter = font_iter.next");

				// nk_font_atlas_cleanup
				data = data.Replace("iter; iter = iter->next", "iter != null; iter = iter->next");
				data = data.Replace("atlas.permanent.free((nk_handle)(atlas.permanent.userdata), ", "CRuntime.free(");

				// nk_font_atlas_clear
				data = data.Replace("iter; iter = next", "iter != null; iter = next");
				data = data.Replace("next = iter.next;CRuntime.free(iter);", "next = iter.next;");
				data = data.Replace("nk_zero(atlas, (ulong)(sizeof((atlas))));", "");

				// 

				// nk_convert
				data = data.Replace("nk_buffer cmds, nk_buffer vertices, nk_buffer elements",
					"nk_buffer<nk_command_base> cmds, nk_buffer<byte> vertices, nk_buffer<ushort> elements");
				data = data.Replace("nk_command* cmd;", "");
				data = data.Replace("cmd->userdata", "cmd.userdata");
				data = data.Replace("for ((cmd) = nk__begin(ctx); (cmd) != null; (cmd) = nk__next(ctx, cmd))",
					"var top_window = nk__begin(ctx); foreach (var cmd in top_window.buffer.commands)");
				data = data.Replace("cmd->type", "cmd.header.type");
				data = data.Replace("nk_str_delete_runes(", "nk_str_delete_runes(ref ");
				data = data.Replace("nk_str_insert_text_char(", "nk_str_insert_text_char(ref ");
				data = data.Replace("nk_str_insert_text_utf8(", "nk_str_insert_text_utf8(ref ");
				data = data.Replace("nk_str_insert_text_runes(", "nk_str_insert_text_runes(ref ");

				data = data.Replace("((c) == (']'))) || ((c) == ('|')));", "((c) == (']'))) || ((c) == ('|'))?1:0);");

				data = data.Replace("cmd._string_ = _string_;",
					"cmd._string_ = new PinnedArray<char>(length); CRuntime.memcpy((void *)cmd._string_, _string_, length * sizeof(char));");

				data = data.Replace("t._string_, (int)(t.length)", "t._string_");

				data = data.Replace("state.undo_char + n", "(char *)state.undo_char + n");
				data = data.Replace("state.undo_rec + ", "(nk_text_undo_record *)state.undo_rec + ");
				data = data.Replace("sizeof((state.undo_rec[0]))", "(ulong)sizeof(nk_text_undo_record)");
				data = data.Replace("state.undo_rec[i].", "((nk_text_undo_record *)state.undo_rec + i)->");
				data = data.Replace("state.undo_char + ", "(char *)state.undo_char + ");
				data = data.Replace("return &state.undo_rec[state.undo_point++];",
					"return (nk_text_undo_record *)state.undo_rec + (state.undo_point++);");
				data = data.Replace("return &state.undo_char[r->char_storage];", "return (char *)state.undo_char + r->char_storage;");
				data = data.Replace("&s.undo_rec[s.redo_point - 1]", "(nk_text_undo_record *)s.undo_rec + s.redo_point - 1");
				data = data.Replace("&s.undo_char[u.char_storage]", "(char *)s.undo_char + u.char_storage");
				data = data.Replace("&s.undo_char[r.char_storage]", "(char *)s.undo_char + r.char_storage");
				data = data.Replace("&s.undo_rec[s.undo_point]", "(nk_text_undo_record *)s.undo_rec + s.undo_point");

				data = data.Replace("uint unicode;", "char unicode;");
				data = data.Replace("uint unicode = (uint)(0)", "char unicode = (char)0");
				data = data.Replace("uint _next_ = (uint)(0)", "char _next_ = (char)0");
				data = data.Replace("uint c;", "char c;");
				data = data.Replace("(uint)(unicode)", "unicode");
				data = data.Replace("uint unicode", "char unicode");

				data = data.Replace("(uint)(((_next_) == (0xFFFD))?'\\0':_next_)", "((_next_) == (0xFFFD))?'\0':_next_");
				data = data.Replace("uint* nk_text", "char* nk_text");
				data = data.Replace("(uint)(nk_str_rune_at", "(char)(nk_str_rune_at");
				data = data.Replace("uint* p = nk_text", "char* p = nk_text");
				data = data.Replace("unicode = (uint)", "unicode = (char)");

				data = data.Replace("(byte)((type) == (NK_TEXT_EDIT_SINGLE_LINE))",
					"(byte)((type) == (NK_TEXT_EDIT_SINGLE_LINE)?1:0)");
				data = data.Replace("nk_str_free(state._string_);", "state._string_ = StringView.empty;");
				data = data.Replace("(int)(sizeof((seperator)) / sizeof((seperator)[0]))", "1");

				data = data.Replace("(done) < (len)", "done < _string_.Length");
				data = data.Replace("&_string_[done], (int)(fitting)", "new StringView(_string_, done, fitting)");
				data = data.Replace("&_string_[done], (int)(len - done)", "_string_+done");
				data = data.Replace("X, (int)(1)", "X");
				data = data.Replace(" || (len== 0)", "");
				data = data.Replace("byte_len", "text.Length");

				data = data.Replace("(edit.clip.copy)", "(edit.clip.CopyDelegate)");

				data = data.Replace("text + row_begin, (int)(text_len - row_begin)", "text + row_begin");
				data = data.Replace("sbyte* remaining;", "int remaining;");
				data = data.Replace("sbyte* remaining;", "int remaining;");

				data = data.Replace("sbyte* cursor_ptr", "StringView cursor_ptr");
				data = data.Replace("sbyte* select_begin_ptr", "StringView select_begin_ptr");
				data = data.Replace("sbyte* select_end_ptr", "StringView select_end_ptr");
				data = data.Replace("sbyte* end", "StringView end");

				data = data.Replace("(*cursor_ptr)", "cursor_ptr[0]");

				// nk_textedit_discard_redo
				data = data.Replace("num * sizeof(char))));", "num * sizeof(char)));");

				// nk_widget_text
				data = data.Replace(".width((nk_handle)(f.userdata), ", ".width(");

				// nk_do_property
				data = data.Replace("sbyte* buffer", "StringView buffer");
				data = data.Replace("NkPluginFilter* filters = stackalloc int (const nk_text_edit *, unsigned int)[2];",
					"NkPluginFilter[] filters = new NkPluginFilter[2];");
				data = data.Replace("StringView _string_ = stackalloc sbyte[64];", "StringView _string_ = null;");
				data = data.Replace("sbyte* dst = null;", "StringView dst;");
				data = data.Replace("nk_itoa(", "nk_itoa(out ");
				data = data.Replace("nk_dtoa(", "nk_dtoa(out ");
				data = data.Replace("nk_string_float_limit(", "nk_string_float_limit(ref ");
				data = data.Replace("int num_len;", "int num_len = 0;");
				data = data.Replace("nk_memcopy(buffer, dst, (ulong)(*length));", "buffer = new StringView(dst, 0, *length);");
				data = data.Replace("buffer[*len] = (sbyte)('\\0');", "buffer.Length = *len;");

				// nk_style_from_table
				data = data.Replace("nk_style_from_table(nk_context ctx, nk_color* table)",
					"nk_style_from_table(nk_context ctx, nk_color[] table)");
				data = data.Replace("(!table)?", "(table == null)?");

				// nk_style_push_font
				data = data.Replace("((int)(sizeof((font_stack.elements)) / sizeof((font_stack.elements)[0])))",
					"(int)font_stack.elements.Length");

				// nk_style_push_style_item
				data = data.Replace("((int)(sizeof((type_stack.elements)) / sizeof((type_stack.elements)[0])))",
					"(int)type_stack.elements.Length");

				// nk_clear
				// data = data.Replace("sizeof(unionnk_page_data))))", "sizeof(nk_table)))");

				// nk_build
				data = data.Replace("nk_command_buffer_init(ctx.overlay, ctx.memory, (int)(NK_CLIPPING_OFF));",
					"nk_command_buffer_init(ctx.overlay, (int)(NK_CLIPPING_OFF));");

				// nk_panel_is_sub
				data = data.Replace("(type & NK_PANEL_SET_SUB)", "(type & NK_PANEL_SET_SUB) != 0");

				// nk_panel_is_nonblock
				data = data.Replace("(type & NK_PANEL_SET_NONBLOCK)", "(type & NK_PANEL_SET_NONBLOCK) != 0");

				// nk_panel_begin
				data = data.Replace("(win.flags & NK_WINDOW_NO_INPUT)?(null)", "(win.flags & NK_WINDOW_NO_INPUT) != 0?null");
				data = data.Replace("(uint)(~NK_WINDOW_MINIMIZED)", "(uint)(~(uint)NK_WINDOW_MINIMIZED)");
				data = data.Replace("(layout.flags & NK_WINDOW_MINIMIZED)?", "(layout.flags & NK_WINDOW_MINIMIZED) != 0?");

				// nk_begin_titled
				data = data.Replace("name, (int)(title_len)", "name");
				data = data.Replace("label, (int)(nk_strlen(label))", "label");
				data = data.Replace("title, (int)(nk_strlen(title))", "title");
				data = data.Replace("str, (int)(nk_strlen(str))", "str");

				File.WriteAllText(@"..\..\..\..\..\NuklearSharp\Nuklear.Generated.cs", data);
			}
		}

		static void Main(string[] args)
		{
			try
			{
				Process();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			}

			Console.WriteLine("Finished. Press any key to quit.");
			Console.ReadKey();
		}
	}
}