using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using NuklearSharp;

namespace RaizamTest
{
	public partial class NuklearContext //: Handle
	{
		internal Nuklear.nk_context _ctx;
		Nuklear.nk_font_atlas atlas;
		Nuklear.nk_buffer _cmds;

		Nuklear.nk_draw_null_texture nullTexture;
		readonly uint vertexAlign;

		public static IntPtr Alloc<T>() where T : struct
		{
			return Marshal.AllocHGlobal(Marshal.SizeOf(typeof (T)));

		}

		public NkFont[] LoadFonts(params FontInfo[] ttfFiles)
		{
			atlas = new Nuklear.nk_font_atlas();
			Nuklear.nk_font_atlas_begin(atlas);

			List<NkFont> fonts = new List<NkFont>();
			foreach (var f in ttfFiles)
			{
				var bytes = File.ReadAllBytes(f.File);

				Nuklear.nk_font fPtr;
				unsafe
				{
					fixed (void* ptr = bytes)
					{
						fPtr = Nuklear.nk_font_atlas_add_from_memory(atlas, ptr, (ulong) bytes.Length, f.Size, null);
					}
				}

				fonts.Add(new NkFont(f.Name ?? Path.GetFileNameWithoutExtension(f.File), fPtr));
			}
			//nk_font* droid = nk_font_atlas_add_from_file(atlas, "./Content/Fonts/DroidSans.ttf", 14, nk_font_config.Null);
			////*/
			//nk_font* robot = nk_font_atlas_add_from_file(atlas, "./Content/Fonts/Roboto-Regular.ttf", 14, nk_font_config.Null);
			//nk_font* future = nk_font_atlas_add_from_file(atlas, "./Content/Fonts/kenvector_future_thin.ttf", 13, nk_font_config.Null);
			/*struct nk_font *clean = nk_font_atlas_add_from_file(atlas, "../../extra_font/ProggyClean.ttf", 12, 0);*/
			/*struct nk_font *tiny = nk_font_atlas_add_from_file(atlas, "../../extra_font/ProggyTiny.ttf", 10, 0);*/
			/*struct nk_font *cousine = nk_font_atlas_add_from_file(atlas, "../../extra_font/Cousine-Regular.ttf", 13, 0);*/

			int w = 0, h = 0;
			byte[] arr;

			unsafe
			{
				var image = (int*) Nuklear.nk_font_atlas_bake(atlas, ref w, ref h, Nuklear.NK_FONT_ATLAS_RGBA32);
				int buffSize = w*h*4;
				arr = new byte[buffSize];
				Marshal.Copy((IntPtr) image, arr, 0, buffSize);
			}

			int idx = CreateTexture(arr, w, h);

			unsafe
			{
				Nuklear.nk_font_atlas_end(atlas, new Nuklear.nk_handle {id = idx}, null);
			}
			//   nk_font_atlas_end(atlas.Ptr);

			//  nk_style_set_font(_ctx, future->handle.Ptr);
			return fonts.ToArray();
		}

		public void SetFont(NkFont font)
		{
			Nuklear.nk_style_set_font(_ctx, font.font.handle);
		}

		//protected unsafe void Update()
		//{
		//     nk_clear(_ctx.Ptr);
		//}
		public bool WindowBegin(string name, Rectangle rect, int flags)
		{
			Nuklear.nk_rect r = new Nuklear.nk_rect {x = rect.X, y = rect.Y, h = rect.Height, w = rect.Width};
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_begin_titled(_ctx, ptr, ptr, r, (uint) (flags)) == 1;
				}
			}

		}

		public void WindowEnd()
		{
			Nuklear.nk_end(_ctx);
		}

		public void Slider(ref float value, float min, float max, float step)
		{
			Nuklear.nk_slider_float(_ctx, min, ref value, max, step);
		}

		public float Slider(float value, float min, float max, float step)
		{
			float val = value;
			Nuklear.nk_slider_float(_ctx, min, ref val, max, step);
			return val;
		}

		public void Slider(ref int value, int min, int max, int step)
		{
			Nuklear.nk_slider_int(_ctx, min, ref value, max, step);
		}

		public bool Button(string label)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{
					return Nuklear.nk_button_label(_ctx, ptr) == 1;
				}
			}
		}

		public bool Button(Color color)
		{
			return Nuklear.nk_button_color(_ctx, color.ToNkColor()) == 1;
		}

		public bool Option(string label, bool isActive)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{
					return Nuklear.nk_option_label(_ctx, ptr, isActive ? 1 : 0) == 1;
				}
			}
		}

		public void RowStatic(float height, int item_width, int cols)
		{
			Nuklear.nk_layout_row_static(_ctx, height, item_width, cols);
		}

		public void RowDynamic(float height, int cols)
		{
			Nuklear.nk_layout_row_dynamic(_ctx, height, cols);
		}

		public void RowBegin(int format, float height, int cols)
		{
			Nuklear.nk_layout_row_begin(_ctx, format, height, cols);
		}

		public void RowEnd()
		{
			Nuklear.nk_layout_row_end(_ctx);
		}

		public void RowPush(float value)
		{
			Nuklear.nk_layout_row_push(_ctx, value);
		}

		public void Label(string label, int textAlign)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{

					Nuklear.nk_label(_ctx, ptr, (uint) textAlign);
				}
			}

		}

		public void Label(string label, int textAlign, Color color)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{

					Nuklear.nk_label_colored(_ctx, ptr, (uint) textAlign, color.ToNkColor());
				}
			}

		}

		public void Property(string name, float min, ref float val, float max, float step = 1F, float incrPerPixel = 1F)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{

					Nuklear.nk_property_float(_ctx, ptr, min, ref val, max, step, incrPerPixel);
				}
			}

		}

		public void Property(string name, double min, ref double val, double max, double step = 1.0, float incrPerPixel = 1F)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					Nuklear.nk_property_double(_ctx, ptr, min, ref val, max, step, incrPerPixel);
				}
			}

		}

		public void Property(string name, int min, ref int val, int max, int step = 1, int incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					Nuklear.nk_property_int(_ctx, ptr, min, ref val, max, step, incrPerPixel);
				}
			}

		}

		public int Property(string name, int min, int val, int max, int step = 1, int incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_propertyi(_ctx, ptr, min, val, max, step, incrPerPixel);
				}
			}
		}

		public float Property(string name, float min, float val, float max, float step = 1, float incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_propertyf(_ctx, ptr, min, val, max, step, incrPerPixel);
				}
			}
		}

		public double Property(string name, double min, double val, double max, double step = 1, float incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_propertyd(_ctx, ptr, min, val, max, step, incrPerPixel);
				}
			}
		}

		public void ComboEnd()
		{
			Nuklear.nk_combo_end(_ctx);
		}

		public float WidgetWidth
		{
			get { return Nuklear.nk_widget_width(_ctx); }
		}
	}
}