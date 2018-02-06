using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sichem;

namespace NuklearSharp.Generation
{
	static partial class Program
	{
		private class StringFunctionBinding
		{
			public string Header { get; set; }
			public string[] Args { get; set; }
		}

		private static readonly Dictionary<string, StringFunctionBinding> _bindings = new Dictionary<string, StringFunctionBinding>();

		private static void Convert()
		{
			var cp = new ClangParser();

			var skipStructs = new HashSet<string>
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
				"nk_panel",
				"nk_config_stack_button_behavior_element",
				"nk_convert_config",
				"nk_user_font_glyph",
				"nk_popup_buffer",
			};

			var treatAsClasses = new HashSet<string>
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
				"nk_page",
				"nk_text_edit",
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
				"nk_memory_status",
				"nk_menu_state",
			};

			var skipGlobalVariables = new HashSet<string>
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
				"nk_cursor_data",
				"hue_colors",
			};

			var skipFunctions = new HashSet<string>
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
				"nk_propertyz",
				"nk_font_atlas_add_default",
				"nk_stroke_polygon",
				"nk_fill_polygon",
				"nk_stroke_polyline",
			};

			var globalArrays = new HashSet<string>
			{
				"nk_utfbyte",
				"nk_utfmask",
				"nk_utfmin",
				"nk_utfmax",
				"nk_proggy_clean_ttf_compressed_data_base85",
				"nk_custom_cursor_data",
				"nk_cursor_data",
			};

			var parameters = new ConversionParameters
			{
				InputPath = @"nuklear.h",
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
			};

			parameters.StructSource = n =>
			{
				var result = new StructGenerationConfig
				{
					Name = n.ToCSharpName(),
					Class = n.ToCSharpName()
				};

				if (!skipStructs.Contains(n) && !n.StartsWith("nk_command_"))
				{
					var s = n.ToCSharpName();
					var subFolder = string.Empty;
					if (!string.IsNullOrEmpty(s))
					{
						subFolder = GetSubFolder(s);
						if (!string.IsNullOrEmpty(subFolder))
						{
							subFolder += "\\";
						}
					}

					result.Source = @"..\..\..\..\..\NuklearSharp\" + subFolder + s + ".Generated.cs";
				}

				result.IsClass = treatAsClasses.Contains(n) || n.StartsWith("nk_command_") || n.StartsWith("nk_style_") ||
					n.StartsWith("nk_config_");

				return result;
			};

			parameters.GlobalVariableSource = n => skipGlobalVariables.Contains(n) ? null : @"..\..\..\..\..\NuklearSharp\Nuklear.GlobalVariables.Generated.cs";
			parameters.EnumSource = n => @"..\..\..\..\..\NuklearSharp\Enums.Generated.cs";
			parameters.FunctionSource = n =>
			{
				var fc = new FunctionGenerationConfig
				{
					Name = n.Name.ToCSharpName(),
					Source = @"..\..\..\..\..\NuklearSharp\Utility.Generated.cs"
				};

				var parts = n.Signature.Split(',');

				var s = string.Empty;
				if (parts.Length > 0)
				{
					var parts2 = parts[0].Split(' ');

					var typeName = parts2[0];

					if (typeName.EndsWith("*"))
					{
						typeName = typeName.Substring(0, typeName.Length - 1);
					}

					var recordType = cp.Processor.GetRecordType(typeName);
					if (recordType != RecordType.None)
					{
						s = typeName.ToCSharpName();
						fc.Class = typeName.ToCSharpName();
						fc.Source = @"..\..\..\..\..\NuklearSharp\";
						fc.ThisName = parts2[parts2.Length - 1];
					}
				}

				if (!string.IsNullOrEmpty(s))
				{
					var subFolder = GetSubFolder(s);

					if (!string.IsNullOrEmpty(subFolder))
					{
						subFolder += "\\";
					}

					fc.Source += subFolder + s + ".Generated.cs";
				}

				if (skipFunctions.Contains(n.Name))
				{
					fc.Source = null;
				}

				if (!string.IsNullOrEmpty(fc.Class) &&
				    fc.Name.StartsWith(fc.Class) &&
				    fc.Name.Length > fc.Class.Length &&
				    !char.IsLower(fc.Name[fc.Class.Length]))
				{
					fc.Name = fc.Name.Substring(fc.Class.Length);
				}

				if (fc.Name.StartsWith("Textedit") && fc.Name.Length > 8)
				{
					fc.Name = fc.Name.Substring(fc.Class.Length);
				}

				return fc;
			};

			parameters.TreatGlobalPointerAsArray = n => globalArrays.Contains(n);

			parameters.UseRefInsteadOfPointer = (f, t, n) => n == "custom" ||
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
			                                                 n == "select_end";

			parameters.CustomGlobalVariableProcessor += cpr =>
			{
				if (cpr.Info.Spelling == "nk_proggy_clean_ttf_compressed_data_base85" ||
				    cpr.Info.Spelling == "nk_custom_cursor_data")
				{
					var sb = new StringBuilder();

					sb.Append("{");

					var start = cpr.Expression.IndexOf('\"') + 1;
					for (var i = start; i < cpr.Expression.Length; ++i)
					{
						var c = cpr.Expression[i];

						if (c == '\"')
						{
							break;
						}

						if (i > start)
						{
							sb.Append(", ");
						}

						sb.AppendFormat("0x{0:X}", (int) c);
					}

					sb.Append("}");

					cpr.Expression = "byte[] " + cpr.Info.Spelling + " = " + sb.ToString();
				}
			};

			parameters.FunctionHeaderProcessed = (fn, args) =>
			{
				if (fn.Contains("nk_stricmpn") ||
				    fn.Contains("nk_tree_state_base") ||
				    fn.Contains("nk_tree_state_push") ||
				    fn.Contains("nk_tree_state_image_push") ||
				    fn.Contains("nk_group_scrolled_offset_begin") ||
				    fn.Contains("nk_parse_hex") ||
				    fn.Contains("nk_itoa") |
				    fn.Contains("nk_string_float_limit") ||
				    fn.Contains("nk_text_clamp") ||
				    fn.Contains("nk_text_calculate_text_bounds") ||
				    fn.Contains("nk_str_") ||
				    fn.Contains("nk_draw_") ||
				    fn.Contains("nk_font_"))
				{
					return;
				}

				if (args.Length == 0 || !args[0].StartsWith("nk_context"))
				{
					return;
				}
				var sb = new StringFunctionBinding
				{
					Header = fn,
					Args = args,
				};

				_bindings[fn] = sb;
			};

			var outputs = cp.Process(parameters);

			// Post processing
			Logger.Info("Post processing...");

			foreach (var output in outputs)
			{
				if (output.Key.Contains("GlobalVariables") ||
				    output.Key.Contains("Enums"))
				{
					continue;
				}

				var data = output.Value;

				data = PostProcess(data);

				File.WriteAllText(output.Key, data);
			}

		}

		private static void GenerateWrapper()
		{
			var str = new StringBuilder();

			str.AppendFormat("// Generated by Sichem at {0}\n\n", DateTime.Now);

			str.Append("namespace NuklearSharp {\n");
			str.Append("unsafe partial class ContextWrapper {\n");
			foreach (var s in _bindings)
			{
				try
				{
					str.Append("public ");
					var parts = s.Key.Split(' ');

					var isBool = parts.Length > 0 && parts[0] == "int";

					if (isBool)
					{
						str.Append("bool ");
					}

					for (var i = isBool ? 1 : 0; i < parts.Length - 1; ++i)
					{
						str.Append(parts[i]);
						str.Append(" ");
					}

					str.Append(parts[parts.Length - 1].ToCSharpName());
					str.Append("(");

					var hasCtx = s.Value.Args.Length > 0 && s.Value.Args[0].StartsWith("nk_context");

					var args = new List<Tuple<string, bool>>();
					for (var i = 0; i < s.Value.Args.Length; ++i)
					{
						var arg = s.Value.Args[i];
						var replaceLength = false;

						if (arg.StartsWith("sbyte*"))
						{
							parts = arg.Split(' ');
							var newArg = "string ";
							for (var j = 1; j < parts.Length; j++)
							{
								newArg += parts[j];
								if (j < parts.Length - 1)
								{
									newArg += " ";
								}
							}

							arg = newArg;

							if (i < s.Value.Args.Length - 1 && s.Value.Args[i + 1].StartsWith("int") && s.Value.Args[i + 1].EndsWith("len"))
							{
								++i;
								replaceLength = true;
							}
						}

						args.Add(new Tuple<string, bool>(arg, replaceLength));
					}

					var ps = string.Empty;
					for (var i = hasCtx ? 1 : 0; i < args.Count; ++i)
					{
						if (i > (hasCtx ? 1 : 0))
						{
							str.Append(", ");
						}

						str.Append(args[i].Item1);
					}

					str.Append(") ");
					str.Append("{");

					foreach (var arg in args)
					{
						if (arg.Item1.StartsWith("string"))
						{
							parts = arg.Item1.Split(' ');
							str.AppendFormat("fixed(char *{0}_ptr = " + ps + " {0}) {{", parts[parts.Length - 1]);
						}
					}

					parts = s.Key.Split(' ');

					if (parts[0] != "void")
					{
						str.Append("return ");
					}

					str.Append("Nuklear." + parts[parts.Length - 1] + "(");

					if (hasCtx)
					{
						str.Append("_ctx");
					}

					for (var i = hasCtx ? 1 : 0; i < args.Count; ++i)
					{
						if (i > 0)
						{
							str.Append(", ");
						}

						parts = args[i].Item1.Split(' ');

						if (parts[0] == "ref")
						{
							str.Append("ref ");
						}

						str.Append(parts[parts.Length - 1]);

						if (args[i].Item1.StartsWith("string "))
						{
							str.Append("_ptr");
						}

						if (args[i].Item2)
						{
							str.Append(", " + parts[parts.Length - 1] + ".Length");
						}
					}

					str.Append(")");

					if (isBool)
					{
						str.Append(" != 0");
					}

					str.Append(";\n");

					foreach (var arg in args)
					{
						if (arg.Item1.StartsWith("string"))
						{
							str.Append("}\n");
						}
					}
					str.Append("}\n\n");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
				}
			}

			str.Append("}\n");
			str.Append("}");

			var s2 = str.ToString();

			s2 = s2.Replace("int (const nk_text_edit *, unsigned int)*", "NkPluginFilter");
			s2 = s2.Replace("IntPtr* item_getter", "NkComboCallback item_getter");
			s2 = s2.Replace("void*", "IntPtr");
			s2 = s2.Replace("enum nk_chart_type", "int");

			// File.WriteAllText(@"..\..\..\..\..\NuklearSharp\ContextWrapper.Generated.cs", s2);			
		}

		private static string GetSubFolder(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}

			var result = string.Empty;

			if (s.StartsWith("Command") ||
			    s.Contains("EditState") ||
			    s.Contains("Panel") ||
			    s.Contains("Popup") ||
			    s.Contains("Property") ||
			    s.Contains("RowLayout") ||
			    s.StartsWith("Scroll") ||
			    s.Contains("Table") ||
			    s.StartsWith("Window") ||
			    s.Contains("MenuState"))
			{
				result = @"Commands";
			}
			else if (s.StartsWith("Config"))
			{
				result = @"Config";
			}
			else if (s.StartsWith("Chart") ||
			         s.Contains("Clipboard") ||
			         s.Contains("Color") ||
			         s.Contains("Conv") ||
			         s.Contains("Cursor") ||
			         s.Contains("Handle") ||
			         s.Contains("Image") ||
			         s.Contains("ListView") ||
			         s.StartsWith("Rect") ||
			         s.Contains("Str") ||
			         s == "Text" ||
			         s.Contains("Vec2"))
			{
				result = @"Core";
			}
			else if (s.Contains("Draw"))
			{
				result = @"Drawing";
			}
			else if (s.Contains("Font") ||
			         s.Contains("Tt"))
			{
				result = @"Fonts";
			}
			else if (s.Contains("Input") ||
			         s.Contains("Key") ||
			         s.Contains("Mouse"))
			{
				result = @"InputSystem";
			}
			else if (s.Contains("Buffer") ||
			         s.Contains("Memory"))
			{
				result = @"MemoryManagement";
			}
			else if (s.Contains("Rp"))
			{
				result = @"RectPacts";
			}
			else if (s.Contains("Style"))
			{
				result = @"Styling";
			}
			else if (s.Contains("Text"))
			{
				result = @"TextEditing";
			}

			return result;
		}

		private static void Process()
		{
			Convert();
//			GenerateWrapper();
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