using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sichem;

namespace NuklearSharp.Generation
{
	class Program
	{
		private class StringFunctionBinding
		{
			public string Header { get; set; }
			public string[] Args { get; set; }
			public int Position { get; set; }
		}

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
						"nk_handle",
						"nk_user_font",
						"nk_font",
						"nk_allocator",
						"nk_clipboard",
						"nk_style_item_data",
						"nk_style_item",
						"nk_font_atlas",
						"nk_page_data",
						"nk_page_element",
						"nk_buffer",
						"nk_text_undo_state",
						"nk_property",
						"nk_property_variant",
						"nk_keyboard",
						"nk_mouse",
						"nk_draw_list",
						"nk_style",
						"nk_chart",
						"nk_command_custom",
						"nk_rp_context",
						"nk_context",
						"nk_page",
						"nk_pool",
						"nk_window",
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
						"nk_command_custom",
						"nk_command_buffer",
						"nk_panel",
						"nk_config_stack_button_behavior_element",
						"nk_convert_config",
						"nk_user_font_glyph",
					},
					Classes = new[]
					{
						"nk_str",
						"nk_clipboard",
						"nk_context",
						"nk_font_atlas",
						"nk_buffer",
						"nk_text_undo_state",
						"nk_page_element",
						"nk_keyboard",
						"nk_mouse",
						"nk_input",
						"nk_draw_list",
						"nk_command_buffer",
						"nk_style",
						"nk_chart",
						"nk_panel",
						"nk_window",
						"nk_popup_state",
						"nk_config_stack_style_item",
						"nk_config_stack_float",
						"nk_config_stack_vec2",
						"nk_config_stack_flags",
						"nk_config_stack_color",
						"nk_config_stack_user_font",
						"nk_config_stack_button_behavior",
						"nk_page",
						"nk_text_edit",
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
						"nk_pool",
						"nk_table",
						"nk_list_view",
						"nk_convert_config",
						"nk_style_item",
						"nk_config_stack_style_item_element",
						"nk_style_text",
						"nk_popup_buffer",
						"nk_image",
						"nk_cursor",
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
						"nk_user_font",
						"nk_font",
						"nk_config_stack_user_font_element",
						"nk_font_config",
						"nk_baked_font",
						"nk_chart_slot",
						"nk_row_layout",
						"nk_edit_state",
						"nk_property_state",
						"nk_configuration_stacks",
						"nk_scroll",
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
						"hue_colors",
					},
					SkipFunctions = new[]
					{
						"nk_inv_sqrt",
						"nk_strmatch_fuzzy_text",
						"nk_strmatch_fuzzy_string",
						"nk_str_append_text_runes",
						"nk_str_append_str_runes",
						"nk_stricmp",
						"nk_strfilter",
						"nk_utf_validate",
						"nk_utf_decode_byte",
						"nk_utf_decode",
						"nk_utf_encode_byte",
						"nk_utf_encode",
						"nk_utf_len",
						"nk_utf_at",
						"nk_style_get_color_by_name",
						"nk_pool_init_fixed",
						"nk_init_custom",
						"nk_pool_init",
						"nk_pool_free",
						"nk_pool_alloc",
						"nk_create_page_element",
						"nk_link_page_element_into_freelist",
						"nk_free_page_element",
						"nk_create_panel",
						"nk_free_panel",
						"nk_create_table",
						"nk_free_table",
						"nk_init_fixed",
						"nk_init",
						"nk_free",
						"nk_create_window",
						"nk_free_window",
						"nk_buffer_init_default",
						"nk_str_init_default",
						"nk_str_init",
						"nk_font_atlas_init_default",
						"nk_font_atlas_init",
						"nk_font_atlas_init_custom",
						"nk_init_default",
						"nk_command_buffer_push",
						"nk__begin",
						"nk_command_buffer_init",
						"nk_command_buffer_reset",
						"nk__next",
						"nk_build",
						"nk_property_",
						"nk_font_atlas_add_default",
						"nk_stroke_polygon",
						"nk_fill_polygon",
						"nk_stroke_polyline",
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

				parameters.UseRefInsteadOfPointer = (f, t, n) =>
				{
					if (n == "custom" ||
					    f == "nk_unify" ||
					    n == "state" ||
					    n == "ws" ||
					    n == "size" ||
					    n == "glyph_count" ||
						n == "width" ||
						n == "height" ||
						n == "value" ||
						n == "val" ||
						n == "cursor" ||
						n == "len" ||
						n == "select_begin" ||
						n == "select_end")
					{
						return true;
					}

					return false;
				};

				var bindings = new Dictionary<string, StringFunctionBinding>();

				parameters.FunctionHeaderProcessed = (fn, args) =>
				{
					if (fn.Contains("nk_stricmpn") ||
						fn.Contains("nk_tree_state_base") ||
						fn.Contains("nk_tree_state_push") ||
						fn.Contains("nk_tree_state_image_push") ||
						fn.Contains("nk_group_scrolled_offset_begin") ||
						fn.Contains("nk_parse_hex") ||
						fn.Contains("nk_itoa"))
					{
						return;
					}

					for (var i = 0; i < args.Length - 1; ++i)
					{
						if (args[i].Contains("sbyte* ") && args[i + 1].Contains("int"))
						{
							var sb = new StringFunctionBinding
							{
								Header = fn,
								Args = args,
								Position = i
							};

							bindings[fn] = sb;
							break;
						}
					}
				};

				var cp = new ClangParser();


				parameters.BeforeLastClosingBracket = () =>
				{
					foreach (var s in bindings)
					{
						var sb = new StringBuilder();

						sb.Append("public static ");
						sb.Append(s.Key);
						sb.Append("(");

						string[] parts;
						string ps = string.Empty;
						for (var i = 0; i < s.Value.Args.Length; ++i)
						{
							if (i > 0)
							{
								sb.Append(", ");
							}

							if (i == s.Value.Position)
							{
								sb.Append("string ");

								parts = s.Value.Args[i].Split(' ');
								ps = parts[parts.Length - 1];
								sb.Append(ps);

								++i;
								continue;
							}

							sb.Append(s.Value.Args[i]);
						}

						sb.Append(") ");
						sb.Append("{");
						sb.Append("fixed(char *ptr = " + ps + ") {");

						parts = s.Key.Split(' ');

						if (parts[0] != "void")
						{
							sb.Append("return ");
						}

						sb.Append(parts[parts.Length - 1]);
						sb.Append("(");

						for (var i = 0; i < s.Value.Args.Length; ++i)
						{
							if (i > 0)
							{
								sb.Append(", ");
							}

							if (i == s.Value.Position)
							{
								parts = s.Value.Args[i + 1].Split(' ');
								sb.Append("ptr");
								sb.Append(", ");

								if (parts[0] != "ref")
								{
									sb.Append("(" + parts[0] + ")");
								}
								else
								{
									sb.Append("ref ");
								}
								sb.Append(ps);
								sb.Append(".Length");

								++i;
								continue;
							}

							parts = s.Value.Args[i].Split(' ');

							if (parts[0] == "ref")
							{
								sb.Append("ref ");
							}

							sb.Append(parts[parts.Length - 1]);
						}

						sb.Append(");");
						sb.Append("}");
						sb.Append("}");

						cp.Processor.IndentedWriteLine(sb.ToString());
					}
				};


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
				data = data.Replace("),)", "))");
				data = data.Replace("unsignedint", "uint");
				string[] stringNames =
				{
					"ctext", "text", "_string_", "label", "title", "txt", "str", "name", "begin", "end",
					"X", "line", "output", "data_base85", "glyph", "cstr", "buffer", "remaining", "temp",
					"select_begin_ptr", "select_end_ptr", "cursor_ptr", "s1", "s2", "memory", "id",
					"selected"
				};

				foreach (var sn in stringNames)
				{
					data = data.Replace("sbyte* " + sn, "char* " + sn);
				}

				data = data.Replace("fixed sbyte", "fixed char");
				data = data.Replace("uint nk_str", "char nk_str");
				data = data.Replace("sbyte* nk_str", "char* nk_str");
				data = data.Replace("(sbyte*)(s.buffer.memory.ptr)", "(char*)(s.buffer.memory.ptr)");
				data = data.Replace("(sbyte*)(str.buffer.memory.ptr)", "(char*)(str.buffer.memory.ptr)");
				data = data.Replace("uint* unicode", "char* unicode");
				data = data.Replace("uint* runes", "char* runes");
				data = data.Replace("(uint)(runes[i])", "runes[i]");
				data = data.Replace("return ((sbyte*)((void *)((byte*)(s.buffer.memory.ptr) + (pos))));",
					"return ((char*)((void *)((byte*)(s.buffer.memory.ptr) + (pos))));");
				data = data.Replace("sbyte* nk", "char* nk");
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

				// nk_memset
				data = data.Replace("(sizeof(uint)>) > (2)", "sizeof(uint) > 2");
				data = data.Replace("if ((size) < (3 * sizeof(uint))))", "if ((size) < (3 * sizeof(uint)))");
				data = data.Replace("sizeof(uint);)", "sizeof(uint))");

				// nk_memcpy
				data = data.Replace("sizeof(int);)", "sizeof(int))");
				data = data.Replace("dst += sizeof(int));", "dst += sizeof(int);");
				data = data.Replace("((length) < (sizeof(int)))))", "((length) < (sizeof(int))))");
				data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
				data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
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
				data = data.Replace("(int)(!(((img.w) == (0)) && ((img.h) == (0))));",
					"(int)((((img.w) == (0)) && ((img.h) == (0)))?1:0);");
				data = data.Replace("((!sep_len)?len:sep_len)", "((sep_len == 0)?len:sep_len)");

				// nk_murmur_hash
				data =
					data.Replace("union (anonymous at nuklear.h:6644:5) conv = (union (anonymous at nuklear.h:6644:5))({ null });",
						"nk_murmur_hash_union conv = new nk_murmur_hash_union(null);");
				data = data.Replace("i; ++i", "i != 0; ++i");

				// nk_rgb
				data = data.Replace("((sbyte)(((col", "((char)(((col");
				data = data.Replace("((sbyte)((col", "((char)((col");

				// commands
				data = data.Replace("(int)(NK_COMMAND_SCISSOR), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_SCISSOR), (ulong)(sizeof(nk_command_scissor)");
				data = data.Replace("(int)(NK_COMMAND_LINE), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_LINE), (ulong)(sizeof(nk_command_line)");
				data = data.Replace("(int)(NK_COMMAND_CURVE), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_CURVE), (ulong)(sizeof(nk_command_curve)");
				data = data.Replace("(int)(NK_COMMAND_RECT), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_RECT), (ulong)(sizeof(nk_command_rect)");
				data = data.Replace("(int)(NK_COMMAND_RECT_FILLED), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_RECT_FILLED), (ulong)(sizeof(nk_command_rect_filled)");
				data = data.Replace("(int)(NK_COMMAND_RECT_MULTI_COLOR), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_RECT_MULTI_COLOR), (ulong)(sizeof(nk_command_rect_multi_color)");
				data = data.Replace("(int)(NK_COMMAND_CIRCLE), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_CIRCLE), (ulong)(sizeof(nk_command_circle)");
				data = data.Replace("(int)(NK_COMMAND_CIRCLE_FILLED), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_CIRCLE_FILLED), (ulong)(sizeof(nk_command_circle_filled)");
				data = data.Replace("(int)(NK_COMMAND_ARC), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_ARC), (ulong)(sizeof(nk_command_arc)");
				data = data.Replace("(int)(NK_COMMAND_ARC_FILLED), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_ARC_FILLED), (ulong)(sizeof(nk_command_arc_filled)");
				data = data.Replace("(int)(NK_COMMAND_TRIANGLE), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_TRIANGLE), (ulong)(sizeof(nk_command_triangle)");
				data = data.Replace("(int)(NK_COMMAND_TRIANGLE_FILLED), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_TRIANGLE_FILLED), (ulong)(sizeof(nk_command_triangle_filled)");
				data = data.Replace("(int)(NK_COMMAND_POLYGON), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_POLYGON), (ulong)(sizeof(nk_command_polygon)");
				data = data.Replace("(int)(NK_COMMAND_POLYGON_FILLED), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_POLYGON_FILLED), (ulong)(sizeof(nk_command_polygon_filled)");
				data = data.Replace("(int)(NK_COMMAND_POLYLINE), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_POLYLINE), (ulong)(sizeof(nk_command_polyline)");
				data = data.Replace("(int)(NK_COMMAND_IMAGE), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_IMAGE), (ulong)(sizeof(nk_command_image)");
				data = data.Replace("(int)(NK_COMMAND_CUSTOM), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_CUSTOM), (ulong)(sizeof(nk_command_custom)");
				data = data.Replace("(int)(NK_COMMAND_TEXT), (ulong)(sizeof((*cmd))",
					"(int)(NK_COMMAND_TEXT), (ulong)(sizeof(nk_command_text)");

				data = data.Replace("->callback =", "->set_callback =");


				//
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
						"return (int)(((element->attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element->format) == (NK_FORMAT_COUNT)));",
						"return (int)(((element->attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element->format) == (NK_FORMAT_COUNT))?1:0);");
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

				// nk_input_button
				data = data.Replace("&_in_.mouse.buttons[id]", "(nk_mouse_button *)_in_.mouse.buttons + id");

				// nk_input_begin
				data = data.Replace("&_in_.mouse.buttons[id]", "(nk_mouse_button *)_in_.mouse.buttons + id");
				data = data.Replace("_in_.mouse.buttons[NK_BUTTON_LEFT].",
					"((nk_mouse_button *)_in_.mouse.buttons + NK_BUTTON_LEFT)->");

				data = data.Replace("char* glyph = stackalloc sbyte[4];", "char* glyph = stackalloc char[4];");
				data = data.Replace("glyph[0] = (sbyte)(c);", "glyph[0] = c;");

				data = data.Replace(", sbyte c)", ", char c)");
				data = data.Replace("sbyte* rune = stackalloc sbyte[4];", "char* rune = stackalloc char[4];");
				data = data.Replace("rune[0] = (sbyte)(c);", "rune[0] = c;");

				// nk_input_has_mouse_click
				data = data.Replace("&i.mouse.buttons[id]", "(nk_mouse_button *)i.mouse.buttons + id");

				// nk_input_is_key_pressed
				data = data.Replace("_in_.keyboard.keys[key].", "((nk_key *)_in_.keyboard.keys + key)->");
				data = data.Replace("_in_.keyboard.keys[i].", "((nk_key *)_in_.keyboard.keys + i)->");
				data = data.Replace("&_in_.keyboard.text[_in_.keyboard.text_len]",
					"(char *)_in_.keyboard.text + _in_.keyboard.text_len");
				data = data.Replace("&i.keyboard.keys[key]", "(nk_key *)i.keyboard.keys + key");

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

				data = data.Replace("nk_zero(&r, (ulong)(sizeof((r))));", "nk_zero(&r, (ulong)(sizeof(nk_text_edit_row)));");

				data = data.Replace("&text[len]", "text+len");

				data = data.Replace("return &state.undo_char[r.char_storage];", "return (char *)state.undo_char + r.char_storage;");
				data = data.Replace("active = (int)(!active);", "active = active != 0?0:1;");
				data = data.Replace("value = (int)(!(value));", "value = value != 0?0:1;");
				data = data.Replace("return (int)(was_active != *active);", "return was_active != *active?1:0;");
				data = data.Replace("return (int)(old_value != value);", "return old_value != value?1:0;");
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

				// nk_text_calculate_text_bounds
				data = data.Replace("sbyte** remaining", "char** remaining");

				// nk_draw_list_add_text
				data = data.Replace("uint next = (uint)(0);", "char next = (char)(0);");
				data = data.Replace(" (uint)(((next) == (0xFFFD))?'\\0':next)", "(next == 0xFFFD)?'\\0':next");

				// nk_rect_height_compare
				data = data.Replace("return (int)(((p->w) > (q->w))?-1:((p->w) < (q->w)));",
					"return (int)(((p->w) > (q->w))?-1:((p->w) < (q->w))?1:0);");

				// nk_rect_original_order
				data = data.Replace("return (int)(((p->was_packed) < (q->was_packed))?-1:((p->was_packed) > (q->was_packed)));",
					"return (int)(((p->was_packed) < (q->was_packed))?-1:((p->was_packed) > (q->was_packed))?1:0);");

				// nk_rp_init_target
				data = data.Replace("extra[0]", "extra_0");
				data = data.Replace("extra[1]", "extra_1");


				// nk_rp_pack_rects
				data = data.Replace("(int)(!(((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff))));",
					"(int)((((rects[i].x) == (0xffff)) && ((rects[i].y) == (0xffff)))?0:1);");

				// nk_rp_qsort
				data = data.Replace("nk_rp_qsort(nk_rp_rect* array, uint len, IntPtr* cmp)",
					"nk_rp_qsort(nk_rp_rect* array, uint len, QSortComparer cmp)");
				data = data.Replace("for (right = (uint)(left - 1); {", "for (right = (uint)(left - 1);;) {");
				data = data.Replace("; ) {}", "");

				// nk_tt__find_table
				data = data.Replace("sbyte* tag", "string tag");

				// nk_tt__hheap_alloc
				data = data.Replace("(sizeof(nk_tt__hheap_chunk)", "((ulong)sizeof(nk_tt__hheap_chunk)");

				// nk_tt__hheap_cleanup

				// nk_tt__new_active
				data = data.Replace("(sizeof((*z))", "(sizeof(nk_tt__active_edge)");

				// nk_tt_GetGlyphShape
				data = data.Replace("(flags & 16)?dx:-dx", "(flags & 16) != 0?dx:-dx");
				data = data.Replace("(flags & 32)?dy:-dy", "(flags & 32) != 0?dy:-dy");
				data = data.Replace("!(flags & 1)", "(flags & 1)==0?1:0");
				data = data.Replace("sizeof((vertices[0]))", "sizeof(nk_tt_vertex)");
				data = data.Replace("sizeof(nk_tt_vertex)", "(ulong)sizeof(nk_tt_vertex)");

				// nk_tt__sort_edges_ins_sort
				data = data.Replace("(int)(((a)->y0) < ((b)->y0))", "(int)(((a)->y0) < ((b)->y0)?1:0)");

				// nk_tt__sort_edges_quicksort
				data = data.Replace("for (++i; {", "for (;;++i) {");
				data = data.Replace("for (--j; {", "for (;;--j) {");

				// nk_tt__rasterize
				data = data.Replace("sizeof((*e))", "(ulong)sizeof(nk_tt__edge)");
				data = data.Replace("(((invert) != 0?(p[j].y) > (p[k].y):(p[j].y) < (p[k].y)) != 0)",
					"(invert != 0?(p[j].y > p[k].y):(p[j].y < p[k].y))");

				// nk_tt__rasterize_sorted_edges
				data = data.Replace("nk_zero(&hh, (ulong)(sizeof((hh))));", "nk_zero(&hh, (ulong)(sizeof(nk_tt__hheap)));");
				data = data.Replace("hh.set_alloc", "hh.alloc");
				data = data.Replace("(result->w * 2 + 1) * sizeof(float)))));", "(result->w * 2 + 1) * sizeof(float))));");
				data = data.Replace("sizeof((scanline[0]))", "sizeof(float)");

				// nk_tt_FlattenCurves
				data = data.Replace("sizeof((**contour_lengths))", "(ulong)sizeof(int)");
				data = data.Replace("sizeof((points[0]))", "(ulong)sizeof(nk_tt__point)");

				// nk_tt_PackBegin
				data = data.Replace("sizeof((*context))", "sizeof(nk_rp_context)");
				data = data.Replace("sizeof((*nodes))", "(ulong)sizeof(nk_rp_node)");

				// nk_font_baker_memory
				data = data.Replace("for (iter = config_list; iter;", "for (iter = config_list; iter != null;");
				data = data.Replace("* sizeof(nk_", "* (ulong)sizeof(nk_");

				// nk_tt_PackEnd
				// nk_font_bake_pack
				data = data.Replace("config_iter; config_iter", "config_iter != null; config_iter");
				data = data.Replace("sizeof((custom_space))", "sizeof(nk_rp_rect)");

				// nk_font_query_font_glyph
				data = data.Replace("uint codepoint = (uint)(0);", "char codepoint = (char)0;");
				data = data.Replace("uint codepoint", "char codepoint");
				data = data.Replace("uint next_codepoint", "char next_codepoint");
				data = data.Replace("(uint)(codepoint)", "codepoint");

				// nk_font_init
				data = data.Replace("uint fallback_codepoint = (uint)(0);", "char fallback_codepoint = (char)0;");
				data = data.Replace("uint fallback_codepoint", "char fallback_codepoint");
				data = data.Replace("(uint)(fallback_codepoint)", "fallback_codepoint");
				data = data.Replace("font.handle.width = nk_font_text_width;", "font.handle.width = font.text_width;");
				data = data.Replace("font.handle.query = nk_font_query_font_glyph;", "font.handle.query = font.query_font_glyph;");

				// nk_font_bake
				data = data.Replace("(uint)(range->first_unicode_codepoint_in_range + char_idx)",
					"(char)(range->first_unicode_codepoint_in_range + char_idx)");

				// nk_font_text_width
				data = data.Replace("nk_font_text_width(nk_handle handle", "nk_font_text_width(nk_font font");
				data = data.Replace("nk_font font = (nk_font)(handle.ptr);", "");

				// nk_font_query_font_glyph
				data = data.Replace("nk_font_query_font_glyph(nk_handle handle", "nk_font_query_font_glyph(nk_font font");
				data = data.Replace("nk_font font;", "");
				data = data.Replace("font = (nk_font)(handle.ptr);", "");

				// nk_font_init
				data = data.Replace("font.handle.userdata.ptr = font;", "");

				// nk_decode_85_byte
				data = data.Replace("('\\')", "('\\\\')");

				// nk_font_config_
				data = data.Replace("sizeof((cfg))", "sizeof(nk_font_config)");

				// nk_font_atlas_init
				data = data.Replace("nk_zero(atlas, (ulong)(sizeof((atlas))));", "");

				// nk_font_atlas_add
				data = data.Replace("sizeof((*config))", "sizeof(nk_font_config)");
				data = data.Replace("sizeof((*font))", "sizeof(nk_font)");
				data =
					data.Replace(
						"font = (nk_font)(atlas.permanent.alloc((nk_handle)(atlas.permanent.userdata), null, (ulong)(sizeof(nk_font))));",
						"font = new nk_font();");
				data = data.Replace("nk_zero(font, (ulong)(sizeof((font))));if (font== null) return null;", "");

				// nk_font_atlas_add_from_memory
				data = data.Replace("(config)?", "(config != null)?");

				// nk_font_atlas_add_compressed_base85

				// 
				data = data.Replace("nk_vec2[] nk_cursor_data", "nk_vec2[,] nk_cursor_data");

				// nk_font_atlas_end
				data = data.Replace("font_iter; font_iter = font_iter.next", "font_iter != null; font_iter = font_iter.next");

				// nk_font_atlas_cleanup
				data = data.Replace("iter; iter = iter->next", "iter != null; iter = iter->next");

				// nk_font_atlas_clear
				data = data.Replace("iter; iter = next", "iter != null; iter = next");

				// nk_font_atlas_bake
				data = data.Replace("(sizeof(nk_font_glyph)", "((ulong)sizeof(nk_font_glyph)");
				data = data.Replace("*width *height", "*width * *height");
				data = data.Replace("uint fallback_glyph", "char fallback_glyph");
				data = data.Replace("(uint)(config.fallback_glyph)", "config.fallback_glyph");
				data = data.Replace("font_iter; font_iter", "font_iter != null; font_iter");
				data = data.Replace("(uint)('?')", "'?'");
				data = data.Replace("nk_cursor_data[i][", "nk_cursor_data[i,");

				// nk_input_begin
				data = data.Replace("_in_.mouse.buttons[i].", "((nk_mouse_button *)_in_.mouse.buttons + i)->");

				// nk_convert
				data = data.Replace("((c) == (']'))) || ((c) == ('|')));", "((c) == (']'))) || ((c) == ('|'))?1:0);");

				data = data.Replace("cmd._string_ = _string_;",
					"cmd._string_ = new PinnedArray<char>(length); CRuntime.memcpy((void *)cmd._string_, _string_, length * sizeof(char));");

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
				data = data.Replace("(int)(sizeof((seperator)) / sizeof((seperator)[0]))", "1");

				data = data.Replace("X,", "&X,");

				// nk_textedit_discard_redo
				data = data.Replace("num * sizeof(char))));", "num * sizeof(char)));");

				// nk_draw_symbol
				data =
					data.Replace(
						"char* X = ((type) == (NK_SYMBOL_X))?\"x\":((type) == (NK_SYMBOL_UNDERSCORE))?\"_\":((type) == (NK_SYMBOL_PLUS))?\"+\":\"-\";",
						"char X = ((type) == (NK_SYMBOL_X))?'x':((type) == (NK_SYMBOL_UNDERSCORE))?'_':((type) == (NK_SYMBOL_PLUS))?'+':'-';");

				// nk_do_property
				data = data.Replace("NkPluginFilter* filters = stackalloc int (const nk_text_edit *, unsigned int)[2];",
					"NkPluginFilter[] filters = new NkPluginFilter[2];");
				data = data.Replace("char* _string_ = stackalloc sbyte[64];", "char* _string_ = stackalloc char[64];");
				data = data.Replace("sbyte* dst = null;", "char *dst = null;");
				data = data.Replace("int num_len;", "int num_len = 0;");

				// nk_style_from_table
				data = data.Replace("nk_style_from_table(nk_context ctx, nk_color* table)",
					"nk_style_from_table(nk_context ctx, nk_color[] table)");
				data = data.Replace("(!table)?", "(table == null)?");

				// nk_style_push_font
				data = data.Replace("((int)(sizeof((font_stack.elements)) / sizeof((font_stack.elements)[0])))",
					"(int)font_stack.elements.Length");
				data = data.Replace("&font_stack.elements[font_stack.head++];",
					"(nk_config_stack_user_font_element *)font_stack.elements + (font_stack.head++);");

				// nk_style_push_style_item
				data = data.Replace("((int)(sizeof((type_stack.elements)) / sizeof((type_stack.elements)[0])))",
					"(int)type_stack.elements.Length");
				data = data.Replace("&font_stack.elements[--font_stack.head];",
					"(nk_config_stack_user_font_element *)font_stack.elements + (--font_stack.head);");

				// nk_clear
				data = data.Replace("nk_zero(it, (ulong)(sizeof(nk_page_data)));",
					"nk_zero(it, (ulong)(sizeof(nk_table)));");
				data = data.Replace("nk_zero(it, (ulong)(sizeof(union nk_page_data)));nk_free_table(ctx, it);",
					"");

				// nk_panel_is_sub
				data = data.Replace("(type & NK_PANEL_SET_SUB)", "(type & NK_PANEL_SET_SUB) != 0");

				// nk_panel_is_nonblock
				data = data.Replace("(type & NK_PANEL_SET_NONBLOCK)", "(type & NK_PANEL_SET_NONBLOCK) != 0");

				// nk_panel_begin
				data = data.Replace("(win.flags & NK_WINDOW_NO_INPUT)?", "(win.flags & NK_WINDOW_NO_INPUT) != 0?");
				data = data.Replace("(uint)(~NK_WINDOW_MINIMIZED)", "(uint)(~(uint)NK_WINDOW_MINIMIZED)");
				data = data.Replace("(layout.flags & NK_WINDOW_MINIMIZED)?", "(layout.flags & NK_WINDOW_MINIMIZED) != 0?");

				// nk_add_value
				data = data.Replace("return &win.tables.values[win.tables.size++];",
					"return (uint *)win.tables.values + (win.tables.size++);");

				// nk_find_value
				data = data.Replace("return &iter.values[i];", "return (uint *)iter.values + i;");

				// nk_begin_titled
				data = data.Replace("win.name_string[name_length] = (sbyte)(0)", "win.name_string[name_length] = (char)(0)");
				data = data.Replace("(!(win.flags & NK_WINDOW_MINIMIZED))?", "((win.flags & NK_WINDOW_MINIMIZED) == 0)?");
				data = data.Replace("!(iter.flags & NK_WINDOW_HIDDEN)", "(iter.flags & NK_WINDOW_HIDDEN) == 0");
				data = data.Replace("!(iter.flags & NK_WINDOW_MINIMIZED)", "(iter.flags & NK_WINDOW_MINIMIZED) == 0");
				data = data.Replace("nk_free_panel(ctx, ctx.current.layout);", "");

				// nk_end
				data = data.Replace("!(iter.flags & NK_WINDOW_HIDDEN)", "(iter.flags & NK_WINDOW_HIDDEN) == 0");

				// nk_layout_space_end
				data = data.Replace("sizeof((layout.row.item))", "sizeof(nk_rect)");

				// nk_tree_state_base
				data = data.Replace("!(layout.flags & NK_WINDOW_ROM)", "(layout.flags & NK_WINDOW_ROM) == 0");

				// nk_button_push_behavior
				data = data.Replace("sizeof((button_stack.elements)) / sizeof((button_stack.elements)[0])",
					"(int)button_stack.elements.Length");
				data = data.Replace("&button_stack.elements[button_stack.head++]",
					"(nk_config_stack_button_behavior_element *)button_stack.elements + (button_stack.head++);");
				data = data.Replace("&button_stack.elements[--button_stack.head]",
					"(nk_config_stack_button_behavior_element *)button_stack.elements + (--button_stack.head);");

				// nk_checkbox_text
				data = data.Replace("return (int)(old_val != *active);", "return (old_val != *active)?1:0;");

				// nk_progress
				data = data.Replace("return (int)(*cur != old_value);", "return (*cur != old_value)?1:0;");

				// nk_edit_string
				data = data.Replace("(!filter)?", "(filter == null)?");

				// nk_radio_text
				data = data.Replace("return (int)(old_value != *active);", "return (old_value != *active)?1:0;");

				// nk_slider_float
				data = data.Replace("return (int)(((old_value) > (value)) || ((old_value) < (value)));",
					"return (((old_value) > (value)) || ((old_value) < (value)))?1:0;");

				// nk_edit_buffer
				data = data.Replace("(win.layout.flags & NK_WINDOW_ROM)?",
					"(win.layout.flags & NK_WINDOW_ROM) != 0?");
				data = data.Replace("(flags & NK_EDIT_READ_ONLY)?",
					"(flags & NK_EDIT_READ_ONLY) != 0?");

				// nk_property_
				data = data.Replace("enum nk_property_filter", "int");

				// nk_property_
				data = data.Replace("sbyte* dummy_buffer = stackalloc sbyte[64];",
					"char* dummy_buffer = stackalloc char[64];");

				// nk_chart_begin_colored
				data = data.Replace("nk_zero(chart, (ulong)(sizeof((chart))));", "");

				// nk_chart_add_slot_colored
				data = data.Replace("enum nk_chart_type", "int");

				// nk_chart_end
				data = data.Replace("nk_memset(chart, (int)(0), (ulong)(sizeof((chart))));", "");

				// nk_plot_function
				data = data.Replace("IntPtr* value_getter", "NkFloatValueGetter value_getter");

				// nk_group_scrolled_offset_begin
				data = data.Replace("nk_zero(panel, (ulong)(sizeof((panel))));", "");
				data = data.Replace("(flags & NK_WINDOW_TITLE)?", "(flags & NK_WINDOW_TITLE) != 0?");

				// nk_group_scrolled_end
				data = data.Replace("nk_zero(pan, (ulong)(sizeof((pan))));", "");

				// nk_popup_begin
				data = data.Replace("nk_zero(popup, (ulong)(sizeof((popup))));", "");

				// nk_combo_begin
				data = data.Replace("(popup)?", "(popup != null)?");

				// nk_combo
				data = data.Replace("sbyte** items", "char** items");

				// nk_combo_separator
				data = data.Replace("sbyte* items_separated_by", "char* items_separated_by");
				data = data.Replace("sbyte* current_item;", "char* current_item;");
				data = data.Replace("sbyte* iter", "char* iter;");

				// nk_combobox_callback
				data = data.Replace("IntPtr* item_getter", "NkComboCallback item_getter");
				data = data.Replace("sbyte* item;", "char* item;");

				// nk_menu_begin
				data = data.Replace("popup?", "popup != null?");

				// Removing nk_allocatog
				data = data.Replace(", nk_allocator* a,", ",");
				data = data.Replace(", nk_allocator* alloc", "");
				data = data.Replace(", alloc", "");
				data = data.Replace(", &alloc", "");
				data = data.Replace(", &baker->alloc", "");
				data = data.Replace(", &atlas.temporary", "");
				data = data.Replace("|| (alloc== null)", "");
				data = data.Replace("(alloc== null)", "");
				data = data.Replace("|| (a== null)", "");
				data = data.Replace("public nk_allocator alloc;", "");
				data = data.Replace("b.pool = (nk_allocator)(*a);", "");
				data = data.Replace(" || (b.pool.alloc== null)", "");
				data = data.Replace(" || (b.pool.free== null)", "");
				data = data.Replace("if (b.pool.free== null) return;", "");
				data = data.Replace("hh.alloc = (nk_allocator)(*alloc);", "");
				data = data.Replace("baker->alloc = (nk_allocator)(*alloc);", "");
				data = data.Replace(" || (atlas.temporary.alloc== null)", "");
				data = data.Replace(" || (atlas.temporary.free== null)", "");
				data = data.Replace(" || (atlas.permanent.alloc== null)", "");
				data = data.Replace(" || (atlas.permanent.free== null)", "");
				data = data.Replace("a->alloc((nk_handle)(a->userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("b.pool.alloc((nk_handle)(b.pool.userdata), b.memory.ptr, ", "CRuntime.malloc(");
				data = data.Replace("alloc->alloc((nk_handle)(alloc->userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("atlas.permanent.alloc((nk_handle)(atlas.permanent.userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("hh->alloc.alloc((nk_handle)(hh->alloc.userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("atlas.temporary.alloc((nk_handle)(atlas.temporary.userdata), null, ", "CRuntime.malloc(");
				data = data.Replace("b.pool.free((nk_handle)(b.pool.userdata), ", "CRuntime.free(");
				data = data.Replace("hh->alloc.free((nk_handle)(hh->alloc.userdata), ", "CRuntime.free(");
				data = data.Replace("alloc->free((nk_handle)(alloc->userdata), ", "CRuntime.free(");
				data = data.Replace("atlas.permanent.free((nk_handle)(atlas.permanent.userdata), ", "CRuntime.free(");
				data = data.Replace("atlas.temporary.free((nk_handle)(atlas.temporary.userdata), ", "CRuntime.free(");

				// rest
				data = data.Replace("nk_rect* clip = &b.clip;", "");
				data = data.Replace("clip->", "b.clip.");
				data = data.Replace("nk_rect* c = &b.clip;", "");
				data =
					data.Replace(
						"if ((((c->w) == (0)) || ((c->h) == (0))) || (!(!(((((c->x) > (r.x + r.w)) || ((c->x + c->w) < (r.x))) || ((c->y) > (r.y + r.h))) || ((c->y + c->h) < (r.y)))))) return;}",
						"if ((((b.clip.w) == (0)) || ((b.clip.h) == (0))) || (!(!(((((b.clip.x) > (r.x + r.w)) || ((b.clip.x + b.clip.w) < (r.x))) || ((b.clip.y) > (r.y + r.h))) || ((b.clip.y + b.clip.h) < (r.y)))))) return;}");
				data = data.Replace("nk_rect* c = &b.clip;", "");
				data =
					data.Replace(
						"if ((!(!(((((bounds.x) > (c->x + c->w)) || ((bounds.x + bounds.w) < (c->x))) || ((bounds.y) > (c->y + c->h))) || ((bounds.y + bounds.h) < (c->y)))))",
						"if ((!(!(((((bounds.x) > (win.layout.clip.x + win.layout.clip.w)) || ((bounds.x + bounds.w) < (win.layout.clip.x))) || ((bounds.y) > (win.layout.clip.y + win.layout.clip.h))) || ((bounds.y + bounds.h) < (win.layout.clip.y)))))");
				data = data.Replace("nk_rect* c = &win.layout.clip;", "");
				data = data.Replace("if ((custom) != null)", "");
				data = data.Replace("nk_zero(s, (ulong)(sizeof(nk_image)));", "");
				data = data.Replace(", (ulong)(sizeof((cmd)))", "");
				data = data.Replace(", (ulong)(sizeof((cmd)) + (ulong)(length + 1))", "");
				data = data.Replace("nk_command* cmd;", "");
				data = data.Replace("for ((cmd) = nk__begin(ctx); (cmd) != null; (cmd) = nk__next(ctx, cmd))",
					"var top_window = nk__begin(ctx); foreach (var cmd in top_window.buffer.commands)");
				data = data.Replace("cmd->type", "cmd.header.type");
				data = data.Replace(", ctx.memory", "");
				data = data.Replace("ctx.draw_list.userdata = (nk_handle)(cmd->userdata);",
					"ctx.draw_list.userdata = (nk_handle)(cmd.userdata);");
				data = data.Replace("iter; iter", "iter != null; iter");
				data = data.Replace("nk_zero(cfg, (ulong)(sizeof(nk_font_config)));", "");
				data = data.Replace("nk_memcopy(cfg, config, (ulong)(sizeof((config))));", "cfg = config;");
				data = data.Replace("cfg = (nk_font_config)(CRuntime.malloc((ulong)(sizeof(nk_font_config))));", "");
				data = data.Replace("CRuntime.free(iter);", "");
				data = data.Replace("CRuntime.free(i);", "");
				data = data.Replace("&layout.row.templates[i]", "(float *)layout.row.templates + i");
				data = data.Replace("uint ws;", "uint ws = 0;");
				data = data.Replace("return (int)(nk_tree_state_base(ctx, (int)(type), img, title, ref (int)(state)));",
					"int kkk = (int)(*state); int result=(int)(nk_tree_state_base(ctx, (int)(type), img, title, ref kkk));*state = (uint)kkk;return result;");
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
				data = data.Replace("uint* x_offset, uint* y_offset", "nk_scroll offset");
				data = data.Replace("(uint)(*x_offset)", "offset.x");
				data = data.Replace("(uint)(*y_offset)", "offset.y");
				data = data.Replace("panel.layout.offset.x = x_offset;", "panel.layout.offset = offset;");
				data = data.Replace("panel.layout.offset.y = y_offset;", "");
				data = data.Replace("view.scroll_value = offset.y;", "view.scroll_value = *y_offset;");
				data = data.Replace("&scroll.x, &scroll.y", "scroll");
				data = data.Replace("x_offset, y_offset", "new nk_scroll {x = *x_offset, y = *y_offset}");
				data =
					data.Replace(
						"nk_font_bake_custom_data(atlas.pixel, (int)(width), (int)(height), (nk_recti)(atlas.custom), nk_custom_cursor_data, (int)(90), (int)(27), ('.'), ('X'));",
						"fixed(char *ptr = nk_custom_cursor_data) { nk_font_bake_custom_data(atlas.pixel, (int)(width), (int)(height), (nk_recti)(atlas.custom), ptr, (int)(90), (int)(27), ('.'), ('X'));}");
				data = data.Replace("nk_zero(&layout.row.item, (ulong)(sizeof(nk_rect)));",
					"fixed(void *ptr = &layout.row.item) {nk_zero(ptr, (ulong)(sizeof(nk_rect)));}");
				data = data.Replace("element->address = &ctx.button_behavior;", "");
				data = data.Replace("*element->address = (int)(element->old_value);", "ctx.button_behavior = element->old_value;");
				data = data.Replace("return nk_font_text_width(handle", "return nk_font_text_width(font");

				data = data.Replace("nk_zero(b, (ulong)(sizeof((b))));", "");
				data = data.Replace("size = (ulong)(sizeof((cmd)) + sizeof(short)* 2 * (ulong)(point_count));", "");

				data = data.Replace("(nk_colorf)({ 0, 0, 0, 0 })", "new nk_colorf()");
				data = data.Replace("(nk_handle)({ null })", "new nk_handle()");
				data = data.Replace("ctrl[0]", "ctrl_0");
				data = data.Replace("ctrl[1]", "ctrl_1");
				data = data.Replace("nk_zero(list, (ulong)(sizeof((list))));", "");
				data = data.Replace("(nk_vec2)(g.uv[0]), (nk_vec2)(g.uv[1])", "nk_vec2_(g.uv_x[0], g.uv_y[0]), nk_vec2_(g.uv_x[1], g.uv_y[1])");
				data = data.Replace("glyph->uv[0] = (nk_vec2)(nk_vec2_((float)(g->u0), (float)(g->v0)));",
					"glyph->uv_x[0] = g->u0; glyph->uv_y[0] = g->v0;");
				data = data.Replace("glyph->uv[1] = (nk_vec2)(nk_vec2_((float)(g->u1), (float)(g->v1)));",
					"glyph->uv_x[1] = g->u1; glyph->uv_y[1] = g->v1;");
				data = data.Replace("nk_memset(state, (int)(0), (ulong)(sizeof(nk_text_edit)));", "");
				data = data.Replace("(nk_rect)({ 0, 0, 0, 0 })", "new nk_rect()");
				data = data.Replace("nk_zero(button, (ulong)(sizeof((button))));", "");
				data = data.Replace("nk_zero(toggle, (ulong)(sizeof((toggle))));", "");
				data = data.Replace("nk_zero(select, (ulong)(sizeof((select))));", "");
				data = data.Replace("nk_zero(slider, (ulong)(sizeof((slider))));", "");
				data = data.Replace("nk_zero(prog, (ulong)(sizeof((prog))));", "");
				data = data.Replace("nk_zero(scroll, (ulong)(sizeof((scroll))));", "");
				data = data.Replace("nk_zero(edit, (ulong)(sizeof((edit))));", "");
				data = data.Replace("nk_zero(property, (ulong)(sizeof((property))));", "");
				data = data.Replace("nk_zero(ctx, (ulong)(sizeof((ctx))));", "");
				data = data.Replace("if ((ctx.use_pool) != 0) nk_buffer_clear(ctx.memory); else nk_buffer_reset(ctx.memory, (int)(NK_BUFFER_FRONT));",
					"nk_buffer_reset(ctx.memory, (int)(NK_BUFFER_FRONT));");
				data = data.Replace("nk_memset(ctx.overlay, (int)(0), (ulong)(sizeof((ctx.overlay))));", "");
				data = data.Replace("nk_zero(ctx.current.layout, (ulong)(sizeof((ctx.current.layout))));", "");
				data = data.Replace("nk_zero(ctx.current.layout, (ulong)(sizeof(nk_panel)));", "");
				data = data.Replace("(layout.flags & NK_WINDOW_DYNAMIC)?", "(layout.flags & NK_WINDOW_DYNAMIC) != 0?");
				data = data.Replace("nk_zero(window.property, (ulong)(sizeof((window.property))));", "");
				data = data.Replace("nk_zero(window.edit, (ulong)(sizeof((window.edit))));", "");

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