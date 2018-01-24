using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
						"NK_INCLUDE_VERTEX_BUFFER_OUTPUT"
					},
					Namespace = "NuklearSharp",
					Class = "Nuklear",
					SkipStructs = new[]
					{
						"nk_allocator", 
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
						"nk_input"
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
						"nk_color_names"
					},
					SkipFunctions = new[]
					{
						"nk_inv_sqrt",
						"nk_murmur_hash",
						"nk_push_custom",
						"nk_command_buffer_init",
						"nk_command_buffer_reset",
						"nk_command_buffer_push",
						"nk_convert",
						"nk__begin",
						"nk__next",
						"nk_input_button",
						"nk_input_glyph",
						"nk_textedit_clear_state",
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
					},
					GlobalArrays = new[]
					{
						"nk_utfbyte",
						"nk_utfmask",
						"nk_utfmin",
						"nk_utfmax"
					}
				};
				
				var cp = new ClangParser();

				cp.Process(parameters);
				var data = output.ToString();

				// Post processing
				Logger.Info("Post processing...");

				// Build has of C functions
				var methods = new HashSet<string>();
				foreach (var f in typeof(CRuntime).GetMethods(BindingFlags.Public | BindingFlags.Static))
				{
					methods.Add(f.Name);
				}

				foreach (var m in methods)
				{
					data = data.Replace("(" + m + "(", "(CRuntime." + m + "(");
					data = data.Replace(" " + m + "(", " CRuntime." + m + "(");
					data = data.Replace(";" + m + "(", ";CRuntime." + m + "(");
					data = data.Replace("\t" + m + "(", "\tCRuntime." + m + "(");
					data = data.Replace("\n" + m + "(", "\nCRuntime." + m + "(");
					data = data.Replace("-" + m + "(", "-CRuntime." + m + "(");
					data = data.Replace("}" + m + "(", "}CRuntime." + m + "(");
				}

				data = data.Replace("(void *)(0)", "null");
				data = data.Replace("enum nk_anti_aliasing", "int");
				data = data.Replace("(sizeof(structnk_window),)", "sizeof(nk_window)");
				data = data.Replace("- -", "-");
				data = data.Replace("* *", "*");
				data = data.Replace("+ +", "+");
				data = data.Replace("sizeof(int);)", "sizeof(int))");
				data = data.Replace("sizeof(unsigned int);)", "sizeof(unsigned int))");
				data = data.Replace("((length) < (sizeof(int)))))", "((length) < (sizeof(int))))");
				data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
				data = data.Replace("(length <= sizeof(int))))", "(length <= sizeof(int)))");
				data = data.Replace("dst += sizeof(int));", "dst += sizeof(int);");
				data = data.Replace("unsignedint", "uint");
				data = data.Replace("(sizeof(uint)>)", "(sizeof(uint))");
				data = data.Replace("(3 * sizeof(uint))))", "(3 * sizeof(uint)))");
				data = data.Replace("t = (ulong)(size / sizeof(uint););", "t = (ulong)(size / sizeof(uint));");
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
				data = data.Replace("(int)(!(((img->w) == (0)) && ((img->h) == (0))));", "(int)((((img->w) == (0)) && ((img->h) == (0)))?1:0);");
				data = data.Replace("((!sep_len)?len:sep_len)", "((sep_len == 0)?len:sep_len)");

				data = data.Replace(", (ulong)(sizeof((cmd)))", "");
				data = data.Replace("sizeof((nk_utfmask)) / sizeof((nk_utfmask)[0])", "nk_utfmask.Length");
				data = data.Replace("full = (int)((b.size - ((b.size) < (size + alignment)?(b.size):(size + alignment))) <= b.allocated);",
					"full = (int)((b.size - ((b.size) < (size + alignment)?(b.size):(size + alignment))) <= b.allocated?1:0);");
				data = data.Replace("sizeof(char),)", "sizeof(char))");
				data = data.Replace("nk_memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char))));", "nk_memcopy(mem, str, (ulong)((ulong)(len) * sizeof(char)));");
				data = data.Replace("(sizeof((list.circle_vtx)) / sizeof((list.circle_vtx)[0]))", "(ulong)list.circle_vtx.Length");
				data = data.Replace("sizeof(structnk_vec2);", "sizeof(nk_vec2)");
				data = data.Replace("sizeof(structnk_draw_command);", "sizeof(nk_draw_command)");
				data = data.Replace("sizeof(nk_draw_index);", "sizeof(nk_draw_index)");
				data = data.Replace("sizeof((col))", "sizeof(nk_color)");
				data = data.Replace("sizeof((bgra))", "sizeof(nk_color)");
				data = data.Replace("sizeof((color))", "sizeof(uint)");
				data = data.Replace("return (int)(((element->attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element->format) == (NK_FORMAT_COUNT)));",
					"return (int)(((element->attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element->format) == (NK_FORMAT_COUNT))?1:0);");
				data = data.Replace("(sizeof(nk_command_arc_filled)", "((ulong)sizeof(nk_command_arc_filled)");
				data = data.Replace("sizeof(short);)", "sizeof(short))");
				data = data.Replace("sizeof((values[value_index]))", "sizeof(float)");
				data = data.Replace("nk_memcopy(attribute, &value, (ulong)(sizeof((value))));", "nk_memcopy(attribute, &value, (ulong)(sizeof(double)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(float))));", "attribute = (void *)(((sbyte*)(attribute) + sizeof(float)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(double))));", "attribute = (void *)(((sbyte*)(attribute) + sizeof(double)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(char))));", "attribute = (void *)(((sbyte*)(attribute) + sizeof(char)));");
				data = data.Replace("attribute = (void *)((sbyte*)(attribute) + sizeof((value)));", "attribute = (void *)((sbyte*)(attribute) + sizeof(short));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(nk_int))));", "attribute = (void *)(((sbyte*)(attribute) + sizeof(int)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(unsignedchar))));", "attribute = (void *)(((sbyte*)(attribute) + sizeof(byte)));");
				data = data.Replace("attribute = (void *)(((sbyte*)(attribute) + sizeof(nk_uint))));", "attribute = (void *)(((sbyte*)(attribute) + sizeof(uint)));");
				data = data.Replace("pnt_size * ((thick_line) != 0?5:3)", "pnt_size * (ulong)((thick_line) != 0?5:3)");

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