using System.Runtime.InteropServices;

namespace KlearUI
{
    public unsafe partial class nk_font_config
    {
        public nk_font_config next;
        public void* ttf_blob;
        public ulong ttf_size;
        public byte ttf_data_owned_by_atlas;
        public byte merge_mode;
        public byte pixel_snap;
        public byte oversample_v;
        public byte oversample_h;
        public PinnedArray<byte> padding = new PinnedArray<byte>(3);
        public float size;
        public FontCoordType coord_type;
        public NkVec2 spacing = new NkVec2();
        public uint* range;
        public nk_baked_font font;
        public char fallback_glyph;
        public nk_font_config n;
        public nk_font_config p;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_font_glyph
    {
        public char codepoint;
        public float xadvance;
        public float x0;
        public float y0;
        public float x1;
        public float y1;
        public float w;
        public float h;
        public float u0;
        public float v0;
        public float u1;
        public float v1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_bakedchar
    {
        public ushort x0;
        public ushort y0;
        public ushort x1;
        public ushort y1;
        public float xoff;
        public float yoff;
        public float xadvance;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_aligned_quad
    {
        public float x0;
        public float y0;
        public float s0;
        public float t0;
        public float x1;
        public float y1;
        public float s1;
        public float t1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_packedchar
    {
        public ushort x0;
        public ushort y0;
        public ushort x1;
        public ushort y1;
        public float xoff;
        public float yoff;
        public float xadvance;
        public float xoff2;
        public float yoff2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_pack_range
    {
        public float font_size;
        public int first_unicode_codepoint_in_range;
        public int* array_of_unicode_codepoints;
        public int num_chars;
        public nk_tt_packedchar* chardata_for_range;
        public byte h_oversample;
        public byte v_oversample;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_pack_context
    {
        public void* pack_info;
        public int width;
        public int height;
        public int stride_in_bytes;
        public int padding;
        public uint h_oversample;
        public uint v_oversample;
        public byte* pixels;
        public void* nodes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_fontinfo
    {
        public byte* data;
        public int fontstart;
        public int numGlyphs;
        public int loca;
        public int head;
        public int glyf;
        public int hhea;
        public int hmtx;
        public int kern;
        public int index_map;
        public int indexToLocFormat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt_vertex
    {
        public short x;
        public short y;
        public short cx;
        public short cy;
        public byte type;
        public byte padding;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt__bitmap
    {
        public int w;
        public int h;
        public int stride;
        public byte* pixels;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt__hheap_chunk
    {
        public nk_tt__hheap_chunk* next;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt__hheap
    {
        public nk_tt__hheap_chunk* head;
        public void* first_free;
        public int num_remaining_in_head_chunk;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt__edge
    {
        public float x0;
        public float y0;
        public float x1;
        public float y1;
        public int invert;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt__active_edge
    {
        public nk_tt__active_edge* next;
        public float fx;
        public float fdx;
        public float fdy;
        public float direction;
        public float sy;
        public float ey;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_tt__point
    {
        public float x;
        public float y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_font_bake_data
    {
        public nk_tt_fontinfo info;
        public nk_rp_rect* rects;
        public nk_tt_pack_range* ranges;
        public uint range_count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe partial struct nk_font_baker
    {
        public nk_tt_pack_context spc;
        public nk_font_bake_data* build;
        public nk_tt_packedchar* packed_chars;
        public nk_rp_rect* rects;
        public nk_tt_pack_range* ranges;
    }

    public unsafe static partial class Nk
    {
        public static void nk_font_atlas_begin(NkFontAtlas atlas)
        {
            if ((((atlas == null)))) return;
            if ((atlas.Glyphs) != null)
            {
                CRuntime.Free(atlas.Glyphs);
                atlas.Glyphs = null;
            }

            if ((atlas.Pixel) != null)
            {
                CRuntime.Free(atlas.Pixel);
                atlas.Pixel = null;
            }
        }

        public static NkFont nk_font_atlas_add(NkFontAtlas atlas, nk_font_config config)
        {
            NkFont font = null;
            nk_font_config cfg;
            if (((((((((atlas == null) || (config == null)) || (config.ttf_blob == null)) || (config.ttf_size == 0)) ||
                    (config.size <= 0.0f)))))) return null;

            cfg = NkFont.nk_font_config_clone(config);
            cfg.n = cfg;
            cfg.p = cfg;
            if (config.merge_mode == 0)
            {
                if (atlas.Config == null)
                {
                    atlas.Config = cfg;
                    cfg.next = null;
                }
                else
                {
                    nk_font_config i = atlas.Config;
                    while ((i.next) != null)
                    {
                        i = i.next;
                    }

                    i.next = cfg;
                    cfg.next = null;
                }

                font = new NkFont();
                font.Config = cfg;
                if (atlas.Fonts == null)
                {
                    atlas.Fonts = font;
                    font.Next = null;
                }
                else
                {
                    NkFont i = atlas.Fonts;
                    while ((i.Next) != null)
                    {
                        i = i.Next;
                    }

                    i.Next = font;
                    font.Next = null;
                }

                cfg.font = font.Info;
            }
            else
            {
                NkFont f = null;
                nk_font_config c = null;
                f = atlas.Fonts;
                c = f.Config;
                cfg.font = f.Info;
                cfg.n = c;
                cfg.p = c.p;
                c.p.n = cfg;
                c.p = cfg;
            }

            if (config.ttf_data_owned_by_atlas == 0)
            {
                cfg.ttf_blob = CRuntime.Malloc((ulong) (cfg.ttf_size));
                if (cfg.ttf_blob == null)
                {
                    atlas.FontNum++;
                    return null;
                }

                nk_memcopy(cfg.ttf_blob, config.ttf_blob, (ulong) (cfg.ttf_size));
                cfg.ttf_data_owned_by_atlas = (byte) (1);
            }

            atlas.FontNum++;
            return font;
        }

        public static NkFont nk_font_atlas_add_from_memory(NkFontAtlas atlas, void* memory, ulong size, float height,
            nk_font_config config)
        {
            nk_font_config cfg = new nk_font_config();
            if (((((((atlas == null))) || (memory == null)) || (size == 0)))) return null;
            cfg = (nk_font_config) ((config != null) ? config : nk_font_config_((float) (height)));
            cfg.ttf_blob = memory;
            cfg.ttf_size = (ulong) (size);
            cfg.size = (float) (height);
            cfg.ttf_data_owned_by_atlas = (byte) (0);
            return nk_font_atlas_add(atlas, cfg);
        }

        public static NkFont nk_font_atlas_add_compressed(NkFontAtlas atlas, void* compressed_data,
            ulong compressed_size,
            float height, nk_font_config config)
        {
            uint decompressed_size;
            void* decompressed_data;
            nk_font_config cfg = new nk_font_config();
            if ((((((atlas == null) || (compressed_data == null)))))) return null;
            decompressed_size = (uint) (nk_decompress_length((byte*) (compressed_data)));
            decompressed_data = CRuntime.Malloc((ulong) (decompressed_size));
            if (decompressed_data == null) return null;
            nk_decompress((byte*) (decompressed_data), (byte*) (compressed_data), (uint) (compressed_size));
            cfg = (nk_font_config) ((config != null) ? config : nk_font_config_((float) (height)));
            cfg.ttf_blob = decompressed_data;
            cfg.ttf_size = (ulong) (decompressed_size);
            cfg.size = (float) (height);
            cfg.ttf_data_owned_by_atlas = (byte) (1);
            return nk_font_atlas_add(atlas, cfg);
        }

        public static NkFont nk_font_atlas_add_compressed_base85(NkFontAtlas atlas, byte* data_base85, float height,
            nk_font_config config)
        {
            int compressed_size;
            void* compressed_data;
            if ((((((atlas == null) || (data_base85 == null)))))) return null;
            compressed_size = (int) (((nk_strlen(data_base85) + 4) / 5) * 4);
            compressed_data = CRuntime.Malloc((ulong) (compressed_size));
            if (compressed_data == null) return null;
            nk_decode_85((byte*) (compressed_data), (byte*) (data_base85));
            NkFont font = nk_font_atlas_add_compressed(atlas, compressed_data, (ulong) (compressed_size),
                (float) (height),
                config);
            CRuntime.Free(compressed_data);
            return font;
        }

        public static void* nk_font_atlas_bake(NkFontAtlas atlas, ref int width, ref int height, FontAtlasFormat fmt)
        {
            int i = (int) (0);
            void* tmp = null;
            ulong tmp_size;
            ulong img_size;
            NkFont font_iter;
            nk_font_baker* baker;
            if (atlas == null) return null;
            if (atlas.FontNum == 0) atlas.DefaultFont = nk_font_atlas_add_default(atlas, (float) (13.0f), null);
            if (atlas.FontNum == 0) return null;
            nk_font_baker_memory(&tmp_size, ref atlas.GlyphCount, atlas.Config, (int) (atlas.FontNum));
            tmp = CRuntime.Malloc((ulong) (tmp_size));
            if (tmp == null) goto failed;
            baker = nk_font_baker_(tmp, (int) (atlas.GlyphCount), (int) (atlas.FontNum));
            atlas.Glyphs =
                (nk_font_glyph*) (CRuntime.Malloc(
                    (ulong) ((ulong) sizeof(nk_font_glyph) * (ulong) (atlas.GlyphCount))));
            if (atlas.Glyphs == null) goto failed;
            atlas.Custom.w = (short) ((90 * 2) + 1);
            atlas.Custom.h = (short) (27 + 1);
            if (NkFont.nk_font_bake_pack(baker, &img_size, ref width, ref height, ref atlas.Custom, atlas.Config,
                    (int) (atlas.FontNum)) == false) goto failed;
            atlas.Pixel = CRuntime.Malloc((ulong) (img_size));
            if (atlas.Pixel == null) goto failed;
            NkFont.nk_font_bake(baker, atlas.Pixel, (int) (width), (int) (height), atlas.Glyphs, (int) (atlas.GlyphCount),
                atlas.Config,
                (int) (atlas.FontNum));
            fixed (byte* ptr = nk_custom_cursor_data)
            {
                nk_font_bake_custom_data(atlas.Pixel, (int) (width), (int) (height), (NkRectI) (atlas.Custom), ptr,
                    (int) (90),
                    (int) (27), ('.'), ('X'));
            }

            if ((fmt) == FontAtlasFormat.Rgba32)
            {
                void* img_rgba = CRuntime.Malloc((ulong) (width * height * 4));
                if (img_rgba == null) goto failed;
                nk_font_bake_convert(img_rgba, (int) (width), (int) (height), atlas.Pixel);
                CRuntime.Free(atlas.Pixel);
                atlas.Pixel = img_rgba;
            }

            atlas.TexWidth = (int) (width);
            atlas.TexHeight = (int) (height);
            for (font_iter = atlas.Fonts; font_iter != null; font_iter = font_iter.Next)
            {
                NkFont font = font_iter;
                nk_font_config config = font.Config;
                NkFont.nk_font_init(font, (float) (config.size), config.fallback_glyph, atlas.Glyphs, config.font,
                    (NkHandle) (nk_handle_ptr(null)));
            }

            for (i = (int) (0); (i) < ((int)CursorKind.COUNT); ++i)
            {
                NkCursor cursor = atlas.Cursors[i];
                cursor.img.w = ((ushort) (width));
                cursor.img.h = ((ushort) (height));
                cursor.img.region[0] = ((ushort) (atlas.Custom.x + nk_cursor_data[i, 0].x));
                cursor.img.region[1] = ((ushort) (atlas.Custom.y + nk_cursor_data[i, 0].y));
                cursor.img.region[2] = ((ushort) (nk_cursor_data[i, 1].x));
                cursor.img.region[3] = ((ushort) (nk_cursor_data[i, 1].y));
                cursor.size = (NkVec2) (nk_cursor_data[i, 1]);
                cursor.offset = (NkVec2) (nk_cursor_data[i, 2]);
            }

            CRuntime.Free(tmp);
            return atlas.Pixel;
            failed: ;
            if ((tmp) != null) CRuntime.Free(tmp);
            if ((atlas.Glyphs) != null)
            {
                CRuntime.Free(atlas.Glyphs);
                atlas.Glyphs = null;
            }

            if ((atlas.Pixel) != null)
            {
                CRuntime.Free(atlas.Pixel);
                atlas.Pixel = null;
            }

            return null;
        }

        public static void nk_font_atlas_end(NkFontAtlas atlas, NkHandle texture, nk_draw_null_texture* _null_)
        {
            int i = (int) (0);
            NkFont font_iter;
            if (atlas == null)
            {
                if (_null_ == null) return;
                _null_->texture = (NkHandle) (texture);
                _null_->uv = (NkVec2) (nk_vec2_((float) (0.5f), (float) (0.5f)));
            }

            if ((_null_) != null)
            {
                _null_->texture = (NkHandle) (texture);
                _null_->uv.x = (float) ((atlas.Custom.x + 0.5f) / (float) (atlas.TexWidth));
                _null_->uv.y = (float) ((atlas.Custom.y + 0.5f) / (float) (atlas.TexHeight));
            }

            for (font_iter = atlas.Fonts; font_iter != null; font_iter = font_iter.Next)
            {
                font_iter.Texture = (NkHandle) (texture);
                font_iter.Handle.Texture = (NkHandle) (texture);
            }

            for (i = (int) (0); (i) < ((int)CursorKind.COUNT); ++i)
            {
                atlas.Cursors[i].img.handle = (NkHandle) (texture);
            }

            CRuntime.Free(atlas.Pixel);
            atlas.Pixel = null;
            atlas.TexWidth = (int) (0);
            atlas.TexHeight = (int) (0);
            atlas.Custom.x = (short) (0);
            atlas.Custom.y = (short) (0);
            atlas.Custom.w = (short) (0);
            atlas.Custom.h = (short) (0);
        }

        public static void nk_font_atlas_cleanup(NkFontAtlas atlas)
        {
            if (((atlas == null))) return;
            if ((atlas.Config) != null)
            {
                nk_font_config iter;
                for (iter = atlas.Config; iter != null; iter = iter.next)
                {
                    nk_font_config i;
                    for (i = iter.n; i != iter; i = i.n)
                    {
                        CRuntime.Free(i.ttf_blob);
                        i.ttf_blob = null;
                    }

                    CRuntime.Free(iter.ttf_blob);
                    iter.ttf_blob = null;
                }
            }
        }

        public static void nk_font_atlas_clear(NkFontAtlas atlas)
        {
            if (((atlas == null))) return;
            if ((atlas.Config) != null)
            {
                nk_font_config iter;
                nk_font_config next;
                for (iter = atlas.Config; iter != null; iter = next)
                {
                    nk_font_config i;
                    nk_font_config n;
                    for (i = iter.n; i != iter; i = n)
                    {
                        n = i.n;
                        if ((i.ttf_blob) != null) CRuntime.Free(i.ttf_blob);
                    }

                    next = iter.next;
                    if ((i.ttf_blob) != null) CRuntime.Free(iter.ttf_blob);
                }

                atlas.Config = null;
            }

            if ((atlas.Fonts) != null)
            {
                NkFont iter;
                NkFont next;
                for (iter = atlas.Fonts; iter != null; iter = next)
                {
                    next = iter.Next;
                }

                atlas.Fonts = null;
            }

            if ((atlas.Glyphs) != null) CRuntime.Free(atlas.Glyphs);
        }
    }
}