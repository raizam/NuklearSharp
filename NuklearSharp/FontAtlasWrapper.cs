﻿using System;
using System.IO;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
    public unsafe class FontAtlasWrapper
    {
        private readonly Nk.NkFontAtlas _atlas = new Nk.NkFontAtlas();
        private readonly BaseContext _context;

        public FontAtlasWrapper(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;

            Nk.nk_font_atlas_begin(_atlas);
        }

        public Nk.NkFont AddDefaultFont(float height)
        {
            return Nk.nk_font_atlas_add_default(_atlas, height, null);
        }

        public Nk.NkFont AddFont(Stream input, float height)
        {
            Nk.NkFont font;
            using (var memoryStream = new MemoryStream())
            {
                input.CopyTo(memoryStream);
                var bytes = memoryStream.ToArray();

                fixed (void* ptr = bytes)
                {
                    font = Nk.nk_font_atlas_add_from_memory(_atlas, ptr, (ulong)bytes.Length, height, null);
                }
            }

            return font;
        }

        public Nk.nk_draw_null_texture Bake()
        {
            int w = 0, h = 0;

            var image = (int*)Nk.nk_font_atlas_bake(_atlas, ref w, ref h, Nk.NK_FONT_ATLAS_RGBA32);
            var buffSize = w * h * 4;
            var arr = new byte[buffSize];
            Marshal.Copy((IntPtr)image, arr, 0, buffSize);

            var textureId = _context.CreateTexture(w, h, arr);

            Nk.nk_draw_null_texture result;
            Nk.nk_font_atlas_end(_atlas, new Nk.NkHandle { id = textureId }, &result);

            return result;
        }
    }
}
