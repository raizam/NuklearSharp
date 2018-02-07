using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework.Graphics;

namespace NuklearSharp.MonoGame
{
	public unsafe class FontAtlasWrapper
	{
		private readonly GraphicsDevice _device;
		private readonly FontAtlas _atlas = new FontAtlas();


		internal FontAtlasWrapper(BaseContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			_context = context;

			_atlas.Begin();
		}

		public Font AddDefaultFont(float height)
		{
			return _atlas.AddDefault(height, null);
		}

		public Font AddFont(Stream input, float height)
		{
			Font font;
			using (var memoryStream = new MemoryStream())
			{
				input.CopyTo(memoryStream);
				var bytes = memoryStream.ToArray();

				fixed (void* ptr = bytes)
				{
					font = _atlas.AddFromMemory(ptr, (ulong) bytes.Length, height, null);
				}
			}

			return font;
		}

		public void Bake()
		{
			int w = 0, h = 0;

			var image = (int*) _atlas.Bake(ref w, ref h, Nuklear.NK_FONT_ATLAS_RGBA32);
			var buffSize = w*h*4;
			var arr = new byte[buffSize];
			Marshal.Copy((IntPtr) image, arr, 0, buffSize);

			var textureId = _context.CreateTexture(w, h, arr);

			_atlas.End(new Handle {id = textureId}, null);
		}
	}
}