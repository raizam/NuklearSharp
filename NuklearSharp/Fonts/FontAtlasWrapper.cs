using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe class FontAtlasWrapper
	{
		private readonly Nuklear.nk_font_atlas _atlas = new Nuklear.nk_font_atlas();
		private readonly BaseContext _context;

		internal FontAtlasWrapper(BaseContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			_context = context;

			Nuklear.nk_font_atlas_begin(_atlas);
		}

		public Nuklear.nk_font AddDefaultFont(float height)
		{
			return Nuklear.nk_font_atlas_add_default(_atlas, height, null);
		}

		public Nuklear.nk_font AddFont(Stream input, float height)
		{
			Nuklear.nk_font font;
			using (var memoryStream = new MemoryStream())
			{
				input.CopyTo(memoryStream);
				var bytes = memoryStream.ToArray();

				fixed (void* ptr = bytes)
				{
					font = Nuklear.nk_font_atlas_add_from_memory(_atlas, ptr, (ulong)bytes.Length, height, null);
				}
			}

			return font;
		}

		public void Bake()
		{
			int w = 0, h = 0;

			var image = (int*)Nuklear.nk_font_atlas_bake(_atlas, ref w, ref h, Nuklear.NK_FONT_ATLAS_RGBA32);
			var buffSize = w * h * 4;
			var arr = new byte[buffSize];
			Marshal.Copy((IntPtr)image, arr, 0, buffSize);

			var textureId = _context.CreateTexture(w, h, arr);

			Nuklear.nk_font_atlas_end(_atlas, new Nuklear.nk_handle { id = textureId }, null);
		}
	}
}
