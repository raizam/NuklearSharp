using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
    public unsafe static partial class Nk
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
            public int coord_type;
            public nk_vec2 spacing = new nk_vec2();
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

        public static int nk_tt_InitFont(nk_tt_fontinfo* info, byte* data2, int fontstart)
        {
            uint cmap;
            uint t;
            int i;
            int numTables;
            byte* data = data2;
            info->data = data;
            info->fontstart = (int)(fontstart);
            cmap = (uint)(nk_tt__find_table(data, (uint)(fontstart), "cmap"));
            info->loca = ((int)(nk_tt__find_table(data, (uint)(fontstart), "loca")));
            info->head = ((int)(nk_tt__find_table(data, (uint)(fontstart), "head")));
            info->glyf = ((int)(nk_tt__find_table(data, (uint)(fontstart), "glyf")));
            info->hhea = ((int)(nk_tt__find_table(data, (uint)(fontstart), "hhea")));
            info->hmtx = ((int)(nk_tt__find_table(data, (uint)(fontstart), "hmtx")));
            info->kern = ((int)(nk_tt__find_table(data, (uint)(fontstart), "kern")));
            if ((((((cmap == 0) || (info->loca == 0)) || (info->head == 0)) || (info->glyf == 0)) || (info->hhea == 0)) ||
                (info->hmtx == 0)) return (int)(0);
            t = (uint)(nk_tt__find_table(data, (uint)(fontstart), "maxp"));
            if ((t) != 0) info->numGlyphs = (int)(nk_ttUSHORT(data + t + 4));
            else info->numGlyphs = (int)(0xffff);
            numTables = (int)(nk_ttUSHORT(data + cmap + 2));
            info->index_map = (int)(0);
            for (i = (int)(0); (i) < (numTables); ++i)
            {
                uint encoding_record = (uint)(cmap + 4 + 8 * (uint)(i));
                switch (nk_ttUSHORT(data + encoding_record))
                {
                    case NK_TT_PLATFORM_ID_MICROSOFT:
                        switch (nk_ttUSHORT(data + encoding_record + 2))
                        {
                            case NK_TT_MS_EID_UNICODE_BMP:
                            case NK_TT_MS_EID_UNICODE_FULL:
                                info->index_map = ((int)(cmap + nk_ttULONG(data + encoding_record + 4)));
                                break;
                            default:
                                break;
                        }
                        break;
                    case NK_TT_PLATFORM_ID_UNICODE:
                        info->index_map = ((int)(cmap + nk_ttULONG(data + encoding_record + 4)));
                        break;
                    default:
                        break;
                }
            }
            if ((info->index_map) == (0)) return (int)(0);
            info->indexToLocFormat = (int)(nk_ttUSHORT(data + info->head + 50));
            return (int)(1);
        }

        public static int nk_tt_FindGlyphIndex(nk_tt_fontinfo* info, int unicode_codepoint)
        {
            byte* data = info->data;
            uint index_map = (uint)(info->index_map);
            ushort format = (ushort)(nk_ttUSHORT(data + index_map + 0));
            if ((format) == (0))
            {
                int bytes = (int)(nk_ttUSHORT(data + index_map + 2));
                if ((unicode_codepoint) < (bytes - 6)) return (int)(*(data + index_map + 6 + unicode_codepoint));
                return (int)(0);
            }
            else if ((format) == (6))
            {
                uint first = (uint)(nk_ttUSHORT(data + index_map + 6));
                uint count = (uint)(nk_ttUSHORT(data + index_map + 8));
                if ((((uint)(unicode_codepoint)) >= (first)) && (((uint)(unicode_codepoint)) < (first + count)))
                    return (int)(nk_ttUSHORT(data + index_map + 10 + (unicode_codepoint - (int)(first)) * 2));
                return (int)(0);
            }
            else if ((format) == (2))
            {
                return (int)(0);
            }
            else if ((format) == (4))
            {
                ushort segcount = (ushort)(nk_ttUSHORT(data + index_map + 6) >> 1);
                ushort searchRange = (ushort)(nk_ttUSHORT(data + index_map + 8) >> 1);
                ushort entrySelector = (ushort)(nk_ttUSHORT(data + index_map + 10));
                ushort rangeShift = (ushort)(nk_ttUSHORT(data + index_map + 12) >> 1);
                uint endCount = (uint)(index_map + 14);
                uint search = (uint)(endCount);
                if ((unicode_codepoint) > (0xffff)) return (int)(0);
                if ((unicode_codepoint) >= (nk_ttUSHORT(data + search + rangeShift * 2))) search += ((uint)(rangeShift * 2));
                search -= (uint)(2);
                while ((entrySelector) != 0)
                {
                    ushort end;
                    searchRange >>= 1;
                    end = (ushort)(nk_ttUSHORT(data + search + searchRange * 2));
                    if ((unicode_codepoint) > (end)) search += ((uint)(searchRange * 2));
                    --entrySelector;
                }
                search += (uint)(2);
                {
                    ushort offset;
                    ushort start;
                    ushort item = (ushort)((search - endCount) >> 1);
                    start = (ushort)(nk_ttUSHORT(data + index_map + 14 + segcount * 2 + 2 + 2 * item));
                    if ((unicode_codepoint) < (start)) return (int)(0);
                    offset = (ushort)(nk_ttUSHORT(data + index_map + 14 + segcount * 6 + 2 + 2 * item));
                    if ((offset) == (0))
                        return (int)((ushort)(unicode_codepoint + nk_ttSHORT(data + index_map + 14 + segcount * 4 + 2 + 2 * item)));
                    return
                        (int)(nk_ttUSHORT(data + offset + (unicode_codepoint - start) * 2 + index_map + 14 + segcount * 6 + 2 + 2 * item));
                }
            }
            else if (((format) == (12)) || ((format) == (13)))
            {
                uint ngroups = (uint)(nk_ttULONG(data + index_map + 12));
                int low;
                int high;
                low = (int)(0);
                high = ((int)(ngroups));
                while ((low) < (high))
                {
                    int mid = (int)(low + ((high - low) >> 1));
                    uint start_char = (uint)(nk_ttULONG(data + index_map + 16 + mid * 12));
                    uint end_char = (uint)(nk_ttULONG(data + index_map + 16 + mid * 12 + 4));
                    if (((uint)(unicode_codepoint)) < (start_char)) high = (int)(mid);
                    else if (((uint)(unicode_codepoint)) > (end_char)) low = (int)(mid + 1);
                    else
                    {
                        uint start_glyph = (uint)(nk_ttULONG(data + index_map + 16 + mid * 12 + 8));
                        if ((format) == (12)) return (int)((int)(start_glyph) + unicode_codepoint - (int)(start_char));
                        else return (int)(start_glyph);
                    }
                }
                return (int)(0);
            }

            return (int)(0);
        }

        public static void nk_tt_setvertex(nk_tt_vertex* v, byte type, int x, int y, int cx, int cy)
        {
            v->type = (byte)(type);
            v->x = ((short)(x));
            v->y = ((short)(y));
            v->cx = ((short)(cx));
            v->cy = ((short)(cy));
        }

        public static int nk_tt__GetGlyfOffset(nk_tt_fontinfo* info, int glyph_index)
        {
            int g1;
            int g2;
            if ((glyph_index) >= (info->numGlyphs)) return (int)(-1);
            if ((info->indexToLocFormat) >= (2)) return (int)(-1);
            if ((info->indexToLocFormat) == (0))
            {
                g1 = (int)(info->glyf + nk_ttUSHORT(info->data + info->loca + glyph_index * 2) * 2);
                g2 = (int)(info->glyf + nk_ttUSHORT(info->data + info->loca + glyph_index * 2 + 2) * 2);
            }
            else
            {
                g1 = (int)(info->glyf + (int)(nk_ttULONG(info->data + info->loca + glyph_index * 4)));
                g2 = (int)(info->glyf + (int)(nk_ttULONG(info->data + info->loca + glyph_index * 4 + 4)));
            }

            return (int)((g1) == (g2) ? -1 : g1);
        }

        public static int nk_tt_GetGlyphBox(nk_tt_fontinfo* info, int glyph_index, int* x0, int* y0, int* x1, int* y1)
        {
            int g = (int)(nk_tt__GetGlyfOffset(info, (int)(glyph_index)));
            if ((g) < (0)) return (int)(0);
            if ((x0) != null) *x0 = (int)(nk_ttSHORT(info->data + g + 2));
            if ((y0) != null) *y0 = (int)(nk_ttSHORT(info->data + g + 4));
            if ((x1) != null) *x1 = (int)(nk_ttSHORT(info->data + g + 6));
            if ((y1) != null) *y1 = (int)(nk_ttSHORT(info->data + g + 8));
            return (int)(1);
        }

        public static int stbtt__close_shape(nk_tt_vertex* vertices, int num_vertices, int was_off, int start_off, int sx,
            int sy, int scx, int scy, int cx, int cy)
        {
            if ((start_off) != 0)
            {
                if ((was_off) != 0)
                    nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vcurve), (int)((cx + scx) >> 1), (int)((cy + scy) >> 1),
                        (int)(cx), (int)(cy));
                nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vcurve), (int)(sx), (int)(sy), (int)(scx), (int)(scy));
            }
            else
            {
                if ((was_off) != 0)
                    nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vcurve), (int)(sx), (int)(sy), (int)(cx), (int)(cy));
                else nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vline), (int)(sx), (int)(sy), (int)(0), (int)(0));
            }

            return (int)(num_vertices);
        }

        public static int nk_tt_GetGlyphShape(nk_tt_fontinfo* info, int glyph_index, nk_tt_vertex** pvertices)
        {
            short numberOfContours;
            byte* endPtsOfContours;
            byte* data = info->data;
            nk_tt_vertex* vertices = null;
            int num_vertices = (int)(0);
            int g = (int)(nk_tt__GetGlyfOffset(info, (int)(glyph_index)));
            *pvertices = null;
            if ((g) < (0)) return (int)(0);
            numberOfContours = (short)(nk_ttSHORT(data + g));
            if ((numberOfContours) > (0))
            {
                byte flags = (byte)(0);
                byte flagcount;
                int ins;
                int i;
                int j = (int)(0);
                int m;
                int n;
                int next_move;
                int was_off = (int)(0);
                int off;
                int start_off = (int)(0);
                int x;
                int y;
                int cx;
                int cy;
                int sx;
                int sy;
                int scx;
                int scy;
                byte* points;
                endPtsOfContours = (data + g + 10);
                ins = (int)(nk_ttUSHORT(data + g + 10 + numberOfContours * 2));
                points = data + g + 10 + numberOfContours * 2 + 2 + ins;
                n = (int)(1 + nk_ttUSHORT(endPtsOfContours + numberOfContours * 2 - 2));
                m = (int)(n + 2 * numberOfContours);
                vertices = (nk_tt_vertex*)(CRuntime.Malloc((ulong)((ulong)(m) * (ulong)sizeof(nk_tt_vertex))));
                if ((vertices) == (null)) return (int)(0);
                next_move = (int)(0);
                flagcount = (byte)(0);
                off = (int)(m - n);
                for (i = (int)(0); (i) < (n); ++i)
                {
                    if ((flagcount) == (0))
                    {
                        flags = (byte)(*points++);
                        if ((flags & 8) != 0) flagcount = (byte)(*points++);
                    }
                    else --flagcount;
                    vertices[off + i].type = (byte)(flags);
                }
                x = (int)(0);
                for (i = (int)(0); (i) < (n); ++i)
                {
                    flags = (byte)(vertices[off + i].type);
                    if ((flags & 2) != 0)
                    {
                        short dx = (short)(*points++);
                        x += (int)((flags & 16) != 0 ? dx : -dx);
                    }
                    else
                    {
                        if ((flags & 16) == 0)
                        {
                            x = (int)(x + (short)(points[0] * 256 + points[1]));
                            points += 2;
                        }
                    }
                    vertices[off + i].x = ((short)(x));
                }
                y = (int)(0);
                for (i = (int)(0); (i) < (n); ++i)
                {
                    flags = (byte)(vertices[off + i].type);
                    if ((flags & 4) != 0)
                    {
                        short dy = (short)(*points++);
                        y += (int)((flags & 32) != 0 ? dy : -dy);
                    }
                    else
                    {
                        if ((flags & 32) == 0)
                        {
                            y = (int)(y + (short)(points[0] * 256 + points[1]));
                            points += 2;
                        }
                    }
                    vertices[off + i].y = ((short)(y));
                }
                num_vertices = (int)(0);
                sx = (int)(sy = (int)(cx = (int)(cy = (int)(scx = (int)(scy = (int)(0))))));
                for (i = (int)(0); (i) < (n); ++i)
                {
                    flags = (byte)(vertices[off + i].type);
                    x = (int)(vertices[off + i].x);
                    y = (int)(vertices[off + i].y);
                    if ((next_move) == (i))
                    {
                        if (i != 0)
                            num_vertices =
                                (int)
                                    (stbtt__close_shape(vertices, (int)(num_vertices), (int)(was_off), (int)(start_off), (int)(sx), (int)(sy),
                                        (int)(scx), (int)(scy), (int)(cx), (int)(cy)));
                        start_off = (int)((flags & 1) == 0 ? 1 : 0);
                        if ((start_off) != 0)
                        {
                            scx = (int)(x);
                            scy = (int)(y);
                            if ((vertices[off + i + 1].type & 1) == 0)
                            {
                                sx = (int)((x + (int)(vertices[off + i + 1].x)) >> 1);
                                sy = (int)((y + (int)(vertices[off + i + 1].y)) >> 1);
                            }
                            else
                            {
                                sx = ((int)(vertices[off + i + 1].x));
                                sy = ((int)(vertices[off + i + 1].y));
                                ++i;
                            }
                        }
                        else
                        {
                            sx = (int)(x);
                            sy = (int)(y);
                        }
                        nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vmove), (int)(sx), (int)(sy), (int)(0), (int)(0));
                        was_off = (int)(0);
                        next_move = (int)(1 + nk_ttUSHORT(endPtsOfContours + j * 2));
                        ++j;
                    }
                    else
                    {
                        if ((flags & 1) == 0)
                        {
                            if ((was_off) != 0)
                                nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vcurve), (int)((cx + x) >> 1), (int)((cy + y) >> 1),
                                    (int)(cx), (int)(cy));
                            cx = (int)(x);
                            cy = (int)(y);
                            was_off = (int)(1);
                        }
                        else
                        {
                            if ((was_off) != 0)
                                nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vcurve), (int)(x), (int)(y), (int)(cx), (int)(cy));
                            else
                                nk_tt_setvertex(&vertices[num_vertices++], (byte)(NK_TT_vline), (int)(x), (int)(y), (int)(0), (int)(0));
                            was_off = (int)(0);
                        }
                    }
                }
                num_vertices =
                    (int)
                        (stbtt__close_shape(vertices, (int)(num_vertices), (int)(was_off), (int)(start_off), (int)(sx), (int)(sy),
                            (int)(scx), (int)(scy), (int)(cx), (int)(cy)));
            }
            else if ((numberOfContours) == (-1))
            {
                int more = (int)(1);
                byte* comp = data + g + 10;
                num_vertices = (int)(0);
                vertices = null;
                while ((more) != 0)
                {
                    ushort flags;
                    ushort gidx;
                    int comp_num_verts = (int)(0);
                    int i;
                    nk_tt_vertex* comp_verts = null;
                    nk_tt_vertex* tmp = null;
                    float* mtx = stackalloc float[6];
                    mtx[0] = (float)(1);
                    mtx[1] = (float)(0);
                    mtx[2] = (float)(0);
                    mtx[3] = (float)(1);
                    mtx[4] = (float)(0);
                    mtx[5] = (float)(0);
                    float m;
                    float n;
                    flags = ((ushort)(nk_ttSHORT(comp)));
                    comp += 2;
                    gidx = ((ushort)(nk_ttSHORT(comp)));
                    comp += 2;
                    if ((flags & 2) != 0)
                    {
                        if ((flags & 1) != 0)
                        {
                            mtx[4] = (float)(nk_ttSHORT(comp));
                            comp += 2;
                            mtx[5] = (float)(nk_ttSHORT(comp));
                            comp += 2;
                        }
                        else
                        {
                            mtx[4] = (float)(*(sbyte*)(comp));
                            comp += 1;
                            mtx[5] = (float)(*(sbyte*)(comp));
                            comp += 1;
                        }
                    }
                    else
                    {
                    }
                    if ((flags & (1 << 3)) != 0)
                    {
                        mtx[0] = (float)(mtx[3] = (float)(nk_ttSHORT(comp) / 16384.0f));
                        comp += 2;
                        mtx[1] = (float)(mtx[2] = (float)(0));
                    }
                    else if ((flags & (1 << 6)) != 0)
                    {
                        mtx[0] = (float)(nk_ttSHORT(comp) / 16384.0f);
                        comp += 2;
                        mtx[1] = (float)(mtx[2] = (float)(0));
                        mtx[3] = (float)(nk_ttSHORT(comp) / 16384.0f);
                        comp += 2;
                    }
                    else if ((flags & (1 << 7)) != 0)
                    {
                        mtx[0] = (float)(nk_ttSHORT(comp) / 16384.0f);
                        comp += 2;
                        mtx[1] = (float)(nk_ttSHORT(comp) / 16384.0f);
                        comp += 2;
                        mtx[2] = (float)(nk_ttSHORT(comp) / 16384.0f);
                        comp += 2;
                        mtx[3] = (float)(nk_ttSHORT(comp) / 16384.0f);
                        comp += 2;
                    }
                    m = (float)(nk_sqrt((float)(mtx[0] * mtx[0] + mtx[1] * mtx[1])));
                    n = (float)(nk_sqrt((float)(mtx[2] * mtx[2] + mtx[3] * mtx[3])));
                    comp_num_verts = (int)(nk_tt_GetGlyphShape(info, (int)(gidx), &comp_verts));
                    if ((comp_num_verts) > (0))
                    {
                        for (i = (int)(0); (i) < (comp_num_verts); ++i)
                        {
                            nk_tt_vertex* v = &comp_verts[i];
                            short x;
                            short y;
                            x = (short)(v->x);
                            y = (short)(v->y);
                            v->x = ((short)(m * (mtx[0] * x + mtx[2] * y + mtx[4])));
                            v->y = ((short)(n * (mtx[1] * x + mtx[3] * y + mtx[5])));
                            x = (short)(v->cx);
                            y = (short)(v->cy);
                            v->cx = ((short)(m * (mtx[0] * x + mtx[2] * y + mtx[4])));
                            v->cy = ((short)(n * (mtx[1] * x + mtx[3] * y + mtx[5])));
                        }
                        tmp =
                            (nk_tt_vertex*)
                                (CRuntime.Malloc((ulong)((ulong)(num_vertices + comp_num_verts) * (ulong)sizeof(nk_tt_vertex))));
                        if (tmp == null)
                        {
                            if ((vertices) != null) CRuntime.Free(vertices);
                            if ((comp_verts) != null) CRuntime.Free(comp_verts);
                            return (int)(0);
                        }
                        if ((num_vertices) > (0))
                            nk_memcopy(tmp, vertices, (ulong)((ulong)(num_vertices) * (ulong)sizeof(nk_tt_vertex)));
                        nk_memcopy(tmp + num_vertices, comp_verts, (ulong)((ulong)(comp_num_verts) * (ulong)sizeof(nk_tt_vertex)));
                        if ((vertices) != null) CRuntime.Free(vertices);
                        vertices = tmp;
                        CRuntime.Free(comp_verts);
                        num_vertices += (int)(comp_num_verts);
                    }
                    more = (int)(flags & (1 << 5));
                }
            }
            else if ((numberOfContours) < (0))
            {
            }
            else
            {
            }

            *pvertices = vertices;
            return (int)(num_vertices);
        }

        public static void nk_tt_GetGlyphHMetrics(nk_tt_fontinfo* info, int glyph_index, int* advanceWidth,
            int* leftSideBearing)
        {
            ushort numOfLongHorMetrics = (ushort)(nk_ttUSHORT(info->data + info->hhea + 34));
            if ((glyph_index) < (numOfLongHorMetrics))
            {
                if ((advanceWidth) != null) *advanceWidth = (int)(nk_ttSHORT(info->data + info->hmtx + 4 * glyph_index));
                if ((leftSideBearing) != null) *leftSideBearing = (int)(nk_ttSHORT(info->data + info->hmtx + 4 * glyph_index + 2));
            }
            else
            {
                if ((advanceWidth) != null)
                    *advanceWidth = (int)(nk_ttSHORT(info->data + info->hmtx + 4 * (numOfLongHorMetrics - 1)));
                if ((leftSideBearing) != null)
                    *leftSideBearing =
                        (int)(nk_ttSHORT(info->data + info->hmtx + 4 * numOfLongHorMetrics + 2 * (glyph_index - numOfLongHorMetrics)));
            }

        }

        public static void nk_tt_GetFontVMetrics(nk_tt_fontinfo* info, int* ascent, int* descent, int* lineGap)
        {
            if ((ascent) != null) *ascent = (int)(nk_ttSHORT(info->data + info->hhea + 4));
            if ((descent) != null) *descent = (int)(nk_ttSHORT(info->data + info->hhea + 6));
            if ((lineGap) != null) *lineGap = (int)(nk_ttSHORT(info->data + info->hhea + 8));
        }

        public static float nk_tt_ScaleForPixelHeight(nk_tt_fontinfo* info, float height)
        {
            int fheight = (int)(nk_ttSHORT(info->data + info->hhea + 4) - nk_ttSHORT(info->data + info->hhea + 6));
            return (float)(height / (float)(fheight));
        }

        public static float nk_tt_ScaleForMappingEmToPixels(nk_tt_fontinfo* info, float pixels)
        {
            int unitsPerEm = (int)(nk_ttUSHORT(info->data + info->head + 18));
            return (float)(pixels / (float)(unitsPerEm));
        }

        public static void nk_tt_GetGlyphBitmapBoxSubpixel(nk_tt_fontinfo* font, int glyph, float scale_x, float scale_y,
            float shift_x, float shift_y, int* ix0, int* iy0, int* ix1, int* iy1)
        {
            int x0;
            int y0;
            int x1;
            int y1;
            if (nk_tt_GetGlyphBox(font, (int)(glyph), &x0, &y0, &x1, &y1) == 0)
            {
                if ((ix0) != null) *ix0 = (int)(0);
                if ((iy0) != null) *iy0 = (int)(0);
                if ((ix1) != null) *ix1 = (int)(0);
                if ((iy1) != null) *iy1 = (int)(0);
            }
            else
            {
                if ((ix0) != null) *ix0 = (int)(nk_ifloorf((float)((float)(x0) * scale_x + shift_x)));
                if ((iy0) != null) *iy0 = (int)(nk_ifloorf((float)((float)(-y1) * scale_y + shift_y)));
                if ((ix1) != null) *ix1 = (int)(nk_iceilf((float)((float)(x1) * scale_x + shift_x)));
                if ((iy1) != null) *iy1 = (int)(nk_iceilf((float)((float)(-y0) * scale_y + shift_y)));
            }

        }

        public static void nk_tt_GetGlyphBitmapBox(nk_tt_fontinfo* font, int glyph, float scale_x, float scale_y, int* ix0,
            int* iy0, int* ix1, int* iy1)
        {
            nk_tt_GetGlyphBitmapBoxSubpixel(font, (int)(glyph), (float)(scale_x), (float)(scale_y), (float)(0.0f),
                (float)(0.0f), ix0, iy0, ix1, iy1);
        }

        public static void* nk_tt__hheap_alloc(nk_tt__hheap* hh, ulong size)
        {
            if ((hh->first_free) != null)
            {
                void* p = hh->first_free;
                hh->first_free = *(void**)(p);
                return p;
            }
            else
            {
                if ((hh->num_remaining_in_head_chunk) == (0))
                {
                    int count = (int)((size) < (32) ? 2000 : (size) < (128) ? 800 : 100);
                    nk_tt__hheap_chunk* c =
                        (nk_tt__hheap_chunk*)(CRuntime.Malloc((ulong)((ulong)sizeof(nk_tt__hheap_chunk) + size * (ulong)(count))));
                    if ((c) == (null)) return null;
                    c->next = hh->head;
                    hh->head = c;
                    hh->num_remaining_in_head_chunk = (int)(count);
                }
                --hh->num_remaining_in_head_chunk;
                return (sbyte*)(hh->head) + size * (ulong)(hh->num_remaining_in_head_chunk);
            }

        }

        public static void nk_tt__hheap_free(nk_tt__hheap* hh, void* p)
        {
            *(void**)(p) = hh->first_free;
            hh->first_free = p;
        }

        public static void nk_tt__hheap_cleanup(nk_tt__hheap* hh)
        {
            nk_tt__hheap_chunk* c = hh->head;
            while ((c) != null)
            {
                nk_tt__hheap_chunk* n = c->next;
                CRuntime.Free(c);
                c = n;
            }
        }

        public static nk_tt__active_edge* nk_tt__new_active(nk_tt__hheap* hh, nk_tt__edge* e, int off_x, float start_point)
        {
            nk_tt__active_edge* z = (nk_tt__active_edge*)(nk_tt__hheap_alloc(hh, (ulong)(sizeof(nk_tt__active_edge))));
            float dxdy = (float)((e->x1 - e->x0) / (e->y1 - e->y0));
            if (z == null) return z;
            z->fdx = (float)(dxdy);
            z->fdy = (float)((dxdy != 0) ? (1 / dxdy) : 0);
            z->fx = (float)(e->x0 + dxdy * (start_point - e->y0));
            z->fx -= ((float)(off_x));
            z->direction = (float)((e->invert) != 0 ? 1.0f : -1.0f);
            z->sy = (float)(e->y0);
            z->ey = (float)(e->y1);
            z->next = null;
            return z;
        }

        public static void nk_tt__rasterize_sorted_edges(nk_tt__bitmap* result, nk_tt__edge* e, int n, int vsubsample,
            int off_x, int off_y)
        {
            nk_tt__hheap hh = new nk_tt__hheap();
            nk_tt__active_edge* active = null;
            int y;
            int j = (int)(0);
            int i;
            float* scanline_data = stackalloc float[129];
            float* scanline;
            float* scanline2;
            nk_zero(&hh, (ulong)(sizeof(nk_tt__hheap)));

            if ((result->w) > (64)) scanline = (float*)(CRuntime.Malloc((ulong)((ulong)(result->w * 2 + 1) * sizeof(float))));
            else scanline = scanline_data;
            scanline2 = scanline + result->w;
            y = (int)(off_y);
            e[n].y0 = (float)((float)(off_y + result->h) + 1);
            while ((j) < (result->h))
            {
                float scan_y_top = (float)((float)(y) + 0.0f);
                float scan_y_bottom = (float)((float)(y) + 1.0f);
                nk_tt__active_edge** step = &active;
                nk_memset(scanline, (int)(0), (ulong)((ulong)(result->w) * sizeof(float)));
                nk_memset(scanline2, (int)(0), (ulong)((ulong)(result->w + 1) * sizeof(float)));
                while ((*step) != null)
                {
                    nk_tt__active_edge* z = *step;
                    if (z->ey <= scan_y_top)
                    {
                        *step = z->next;
                        z->direction = (float)(0);
                        nk_tt__hheap_free(&hh, z);
                    }
                    else
                    {
                        step = &((*step)->next);
                    }
                }
                while (e->y0 <= scan_y_bottom)
                {
                    if (e->y0 != e->y1)
                    {
                        nk_tt__active_edge* z = nk_tt__new_active(&hh, e, (int)(off_x), (float)(scan_y_top));
                        if (z != null)
                        {
                            z->next = active;
                            active = z;
                        }
                    }
                    ++e;
                }
                if ((active) != null)
                    nk_tt__fill_active_edges_new(scanline, scanline2 + 1, (int)(result->w), active, (float)(scan_y_top));
                {
                    float sum = (float)(0);
                    for (i = (int)(0); (i) < (result->w); ++i)
                    {
                        float k;
                        int m;
                        sum += (float)(scanline2[i]);
                        k = (float)(scanline[i] + sum);
                        k = (float)((((k) < (0)) ? -(k) : (k)) * 255.0f + 0.5f);
                        m = ((int)(k));
                        if ((m) > (255)) m = (int)(255);
                        result->pixels[j * result->stride + i] = ((byte)(m));
                    }
                }
                step = &active;
                while ((*step) != null)
                {
                    nk_tt__active_edge* z = *step;
                    z->fx += (float)(z->fdx);
                    step = &((*step)->next);
                }
                ++y;
                ++j;
            }
            nk_tt__hheap_cleanup(&hh);
            if (scanline != scanline_data) CRuntime.Free(scanline);
        }

        public static void nk_tt__sort_edges_ins_sort(nk_tt__edge* p, int n)
        {
            int i;
            int j;
            for (i = (int)(1); (i) < (n); ++i)
            {
                nk_tt__edge t = (nk_tt__edge)(p[i]);
                nk_tt__edge* a = &t;
                j = (int)(i);
                while ((j) > (0))
                {
                    nk_tt__edge* b = &p[j - 1];
                    int c = (int)(((a)->y0) < ((b)->y0) ? 1 : 0);
                    if (c == 0) break;
                    p[j] = (nk_tt__edge)(p[j - 1]);
                    --j;
                }
                if (i != j) p[j] = (nk_tt__edge)(t);
            }
        }

        public static void nk_tt__sort_edges_quicksort(nk_tt__edge* p, int n)
        {
            while ((n) > (12))
            {
                nk_tt__edge t = new nk_tt__edge();
                int c01;
                int c12;
                int c;
                int m;
                int i;
                int j;
                m = (int)(n >> 1);
                c01 = (int)(((&p[0])->y0) < ((&p[m])->y0) ? 1 : 0);
                c12 = (int)(((&p[m])->y0) < ((&p[n - 1])->y0) ? 1 : 0);
                if (c01 != c12)
                {
                    int z;
                    c = (int)(((&p[0])->y0) < ((&p[n - 1])->y0) ? 1 : 0);
                    z = (int)(((c) == (c12)) ? 0 : n - 1);
                    t = (nk_tt__edge)(p[z]);
                    p[z] = (nk_tt__edge)(p[m]);
                    p[m] = (nk_tt__edge)(t);
                }
                t = (nk_tt__edge)(p[0]);
                p[0] = (nk_tt__edge)(p[m]);
                p[m] = (nk_tt__edge)(t);
                i = (int)(1);
                j = (int)(n - 1);
                for (; ; )
                {
                    for (; ; ++i)
                    {
                        if (!(((&p[i])->y0) < ((&p[0])->y0))) break;
                    }
                    for (; ; --j)
                    {
                        if (!(((&p[0])->y0) < ((&p[j])->y0))) break;
                    }
                    if ((i) >= (j)) break;
                    t = (nk_tt__edge)(p[i]);
                    p[i] = (nk_tt__edge)(p[j]);
                    p[j] = (nk_tt__edge)(t);
                    ++i;
                    --j;
                }
                if ((j) < (n - i))
                {
                    nk_tt__sort_edges_quicksort(p, (int)(j));
                    p = p + i;
                    n = (int)(n - i);
                }
                else
                {
                    nk_tt__sort_edges_quicksort(p + i, (int)(n - i));
                    n = (int)(j);
                }
            }
        }

        public static void nk_tt__sort_edges(nk_tt__edge* p, int n)
        {
            nk_tt__sort_edges_quicksort(p, (int)(n));
            nk_tt__sort_edges_ins_sort(p, (int)(n));
        }

        public static void nk_tt__rasterize(nk_tt__bitmap* result, nk_tt__point* pts, int* wcount, int windings, float scale_x,
            float scale_y, float shift_x, float shift_y, int off_x, int off_y, int invert)
        {
            float y_scale_inv = (float)((invert) != 0 ? -scale_y : scale_y);
            nk_tt__edge* e;
            int n;
            int i;
            int j;
            int k;
            int m;
            int vsubsample = (int)(1);
            n = (int)(0);
            for (i = (int)(0); (i) < (windings); ++i)
            {
                n += (int)(wcount[i]);
            }
            e = (nk_tt__edge*)(CRuntime.Malloc((ulong)((ulong)sizeof(nk_tt__edge) * (ulong)(n + 1))));
            if ((e) == (null)) return;
            n = (int)(0);
            m = (int)(0);
            for (i = (int)(0); (i) < (windings); ++i)
            {
                nk_tt__point* p = pts + m;
                m += (int)(wcount[i]);
                j = (int)(wcount[i] - 1);
                for (k = (int)(0); (k) < (wcount[i]); j = (int)(k++))
                {
                    int a = (int)(k);
                    int b = (int)(j);
                    if ((p[j].y) == (p[k].y)) continue;
                    e[n].invert = (int)(0);
                    if (invert != 0 ? (p[j].y > p[k].y) : (p[j].y < p[k].y))
                    {
                        e[n].invert = (int)(1);
                        a = (int)(j);
                        b = (int)(k);
                    }
                    e[n].x0 = (float)(p[a].x * scale_x + shift_x);
                    e[n].y0 = (float)((p[a].y * y_scale_inv + shift_y) * (float)(vsubsample));
                    e[n].x1 = (float)(p[b].x * scale_x + shift_x);
                    e[n].y1 = (float)((p[b].y * y_scale_inv + shift_y) * (float)(vsubsample));
                    ++n;
                }
            }
            nk_tt__sort_edges(e, (int)(n));
            nk_tt__rasterize_sorted_edges(result, e, (int)(n), (int)(vsubsample), (int)(off_x), (int)(off_y));
            CRuntime.Free(e);
        }

        public static void nk_tt__add_point(nk_tt__point* points, int n, float x, float y)
        {
            if (points == null) return;
            points[n].x = (float)(x);
            points[n].y = (float)(y);
        }

        public static int nk_tt__tesselate_curve(nk_tt__point* points, int* num_points, float x0, float y0, float x1, float y1,
            float x2, float y2, float objspace_flatness_squared, int n)
        {
            float mx = (float)((x0 + 2 * x1 + x2) / 4);
            float my = (float)((y0 + 2 * y1 + y2) / 4);
            float dx = (float)((x0 + x2) / 2 - mx);
            float dy = (float)((y0 + y2) / 2 - my);
            if ((n) > (16)) return (int)(1);
            if ((dx * dx + dy * dy) > (objspace_flatness_squared))
            {
                nk_tt__tesselate_curve(points, num_points, (float)(x0), (float)(y0), (float)((x0 + x1) / 2.0f),
                    (float)((y0 + y1) / 2.0f), (float)(mx), (float)(my), (float)(objspace_flatness_squared), (int)(n + 1));
                nk_tt__tesselate_curve(points, num_points, (float)(mx), (float)(my), (float)((x1 + x2) / 2.0f),
                    (float)((y1 + y2) / 2.0f), (float)(x2), (float)(y2), (float)(objspace_flatness_squared), (int)(n + 1));
            }
            else
            {
                nk_tt__add_point(points, (int)(*num_points), (float)(x2), (float)(y2));
                *num_points = (int)(*num_points + 1);
            }

            return (int)(1);
        }

        public static nk_tt__point* nk_tt_FlattenCurves(nk_tt_vertex* vertices, int num_verts, float objspace_flatness,
            int** contour_lengths, int* num_contours)
        {
            nk_tt__point* points = null;
            int num_points = (int)(0);
            float objspace_flatness_squared = (float)(objspace_flatness * objspace_flatness);
            int i;
            int n = (int)(0);
            int start = (int)(0);
            int pass;
            for (i = (int)(0); (i) < (num_verts); ++i)
            {
                if ((vertices[i].type) == (NK_TT_vmove)) ++n;
            }
            *num_contours = (int)(n);
            if ((n) == (0)) return null;
            *contour_lengths = (int*)(CRuntime.Malloc((ulong)((ulong)sizeof(int) * (ulong)(n))));
            if ((*contour_lengths) == (null))
            {
                *num_contours = (int)(0);
                return null;
            }

            for (pass = (int)(0); (pass) < (2); ++pass)
            {
                float x = (float)(0);
                float y = (float)(0);
                if ((pass) == (1))
                {
                    points = (nk_tt__point*)(CRuntime.Malloc((ulong)((ulong)(num_points) * (ulong)sizeof(nk_tt__point))));
                    if ((points) == (null)) goto error;
                }
                num_points = (int)(0);
                n = (int)(-1);
                for (i = (int)(0); (i) < (num_verts); ++i)
                {
                    switch (vertices[i].type)
                    {
                        case NK_TT_vmove:
                            if ((n) >= (0)) (*contour_lengths)[n] = (int)(num_points - start);
                            ++n;
                            start = (int)(num_points);
                            x = (float)(vertices[i].x);
                            y = (float)(vertices[i].y);
                            nk_tt__add_point(points, (int)(num_points++), (float)(x), (float)(y));
                            break;
                        case NK_TT_vline:
                            x = (float)(vertices[i].x);
                            y = (float)(vertices[i].y);
                            nk_tt__add_point(points, (int)(num_points++), (float)(x), (float)(y));
                            break;
                        case NK_TT_vcurve:
                            nk_tt__tesselate_curve(points, &num_points, (float)(x), (float)(y), (float)(vertices[i].cx),
                                (float)(vertices[i].cy), (float)(vertices[i].x), (float)(vertices[i].y), (float)(objspace_flatness_squared),
                                (int)(0));
                            x = (float)(vertices[i].x);
                            y = (float)(vertices[i].y);
                            break;
                        default:
                            break;
                    }
                }
                (*contour_lengths)[n] = (int)(num_points - start);
            }
            return points;
            error:
            ;
            CRuntime.Free(points);
            CRuntime.Free(*contour_lengths);
            *contour_lengths = null;
            *num_contours = (int)(0);
            return null;
        }

        public static void nk_tt_Rasterize(nk_tt__bitmap* result, float flatness_in_pixels, nk_tt_vertex* vertices,
            int num_verts, float scale_x, float scale_y, float shift_x, float shift_y, int x_off, int y_off, int invert)
        {
            float scale = (float)((scale_x) > (scale_y) ? scale_y : scale_x);
            int winding_count;
            int* winding_lengths;
            nk_tt__point* windings = nk_tt_FlattenCurves(vertices, (int)(num_verts), (float)(flatness_in_pixels / scale),
                &winding_lengths, &winding_count);
            if ((windings) != null)
            {
                nk_tt__rasterize(result, windings, winding_lengths, (int)(winding_count), (float)(scale_x), (float)(scale_y),
                    (float)(shift_x), (float)(shift_y), (int)(x_off), (int)(y_off), (int)(invert));
                CRuntime.Free(winding_lengths);
                CRuntime.Free(windings);
            }

        }

        public static void nk_tt_MakeGlyphBitmapSubpixel(nk_tt_fontinfo* info, byte* output, int out_w, int out_h,
            int out_stride, float scale_x, float scale_y, float shift_x, float shift_y, int glyph)
        {
            int ix0;
            int iy0;
            nk_tt_vertex* vertices;
            int num_verts = (int)(nk_tt_GetGlyphShape(info, (int)(glyph), &vertices));
            nk_tt__bitmap gbm = new nk_tt__bitmap();
            nk_tt_GetGlyphBitmapBoxSubpixel(info, (int)(glyph), (float)(scale_x), (float)(scale_y), (float)(shift_x),
                (float)(shift_y), &ix0, &iy0, null, null);
            gbm.pixels = output;
            gbm.w = (int)(out_w);
            gbm.h = (int)(out_h);
            gbm.stride = (int)(out_stride);
            if (((gbm.w) != 0) && ((gbm.h) != 0))
                nk_tt_Rasterize(&gbm, (float)(0.35f), vertices, (int)(num_verts), (float)(scale_x), (float)(scale_y),
                    (float)(shift_x), (float)(shift_y), (int)(ix0), (int)(iy0), (int)(1));
            CRuntime.Free(vertices);
        }

        public static int nk_tt_PackBegin(nk_tt_pack_context* spc, byte* pixels, int pw, int ph, int stride_in_bytes,
            int padding)
        {
            int num_nodes = (int)(pw - padding);
            NkRpContext* context = (NkRpContext*)(CRuntime.Malloc((ulong)(sizeof(NkRpContext))));
            nk_rp_node* nodes = (nk_rp_node*)(CRuntime.Malloc((ulong)((ulong)sizeof(nk_rp_node) * (ulong)(num_nodes))));
            if (((context) == (null)) || ((nodes) == (null)))
            {
                if (context != null) CRuntime.Free(context);
                if (nodes != null) CRuntime.Free(nodes);
                return (int)(0);
            }

            spc->width = (int)(pw);
            spc->height = (int)(ph);
            spc->pixels = pixels;
            spc->pack_info = context;
            spc->nodes = nodes;
            spc->padding = (int)(padding);
            spc->stride_in_bytes = (int)((stride_in_bytes != 0) ? stride_in_bytes : pw);
            spc->h_oversample = (uint)(1);
            spc->v_oversample = (uint)(1);
            nk_rp_init_target(context, (int)(pw - padding), (int)(ph - padding), nodes, (int)(num_nodes));
            if ((pixels) != null) nk_memset(pixels, (int)(0), (ulong)(pw * ph));
            return (int)(1);
        }

        public static void nk_tt_PackEnd(nk_tt_pack_context* spc)
        {
            CRuntime.Free(spc->nodes);
            CRuntime.Free(spc->pack_info);
        }

        public static void nk_tt_PackSetOversampling(nk_tt_pack_context* spc, uint h_oversample, uint v_oversample)
        {
            if (h_oversample <= 8) spc->h_oversample = (uint)(h_oversample);
            if (v_oversample <= 8) spc->v_oversample = (uint)(v_oversample);
        }

        public static int nk_tt_PackFontRangesGatherRects(nk_tt_pack_context* spc, nk_tt_fontinfo* info,
            nk_tt_pack_range* ranges, int num_ranges, nk_rp_rect* rects)
        {
            int i;
            int j;
            int k;
            k = (int)(0);
            for (i = (int)(0); (i) < (num_ranges); ++i)
            {
                float fh = (float)(ranges[i].font_size);
                float scale =
                    (float)
                        (((fh) > (0))
                            ? nk_tt_ScaleForPixelHeight(info, (float)(fh))
                            : nk_tt_ScaleForMappingEmToPixels(info, (float)(-fh)));
                ranges[i].h_oversample = ((byte)(spc->h_oversample));
                ranges[i].v_oversample = ((byte)(spc->v_oversample));
                for (j = (int)(0); (j) < (ranges[i].num_chars); ++j)
                {
                    int x0;
                    int y0;
                    int x1;
                    int y1;
                    int codepoint =
                        (int)
                            ((ranges[i].first_unicode_codepoint_in_range) != 0
                                ? ranges[i].first_unicode_codepoint_in_range + j
                                : ranges[i].array_of_unicode_codepoints[j]);
                    int glyph = (int)(nk_tt_FindGlyphIndex(info, (int)(codepoint)));
                    nk_tt_GetGlyphBitmapBoxSubpixel(info, (int)(glyph), (float)(scale * (float)(spc->h_oversample)),
                        (float)(scale * (float)(spc->v_oversample)), (float)(0), (float)(0), &x0, &y0, &x1, &y1);
                    rects[k].w = ((ushort)(x1 - x0 + spc->padding + (int)(spc->h_oversample) - 1));
                    rects[k].h = ((ushort)(y1 - y0 + spc->padding + (int)(spc->v_oversample) - 1));
                    ++k;
                }
            }
            return (int)(k);
        }

        public static int nk_tt_PackFontRangesRenderIntoRects(nk_tt_pack_context* spc, nk_tt_fontinfo* info,
            nk_tt_pack_range* ranges, int num_ranges, nk_rp_rect* rects)
        {
            int i;
            int j;
            int k;
            int return_value = (int)(1);
            int old_h_over = (int)(spc->h_oversample);
            int old_v_over = (int)(spc->v_oversample);
            k = (int)(0);
            for (i = (int)(0); (i) < (num_ranges); ++i)
            {
                float fh = (float)(ranges[i].font_size);
                float recip_h;
                float recip_v;
                float sub_x;
                float sub_y;
                float scale =
                    (float)
                        ((fh) > (0) ? nk_tt_ScaleForPixelHeight(info, (float)(fh)) : nk_tt_ScaleForMappingEmToPixels(info, (float)(-fh)));
                spc->h_oversample = (uint)(ranges[i].h_oversample);
                spc->v_oversample = (uint)(ranges[i].v_oversample);
                recip_h = (float)(1.0f / (float)(spc->h_oversample));
                recip_v = (float)(1.0f / (float)(spc->v_oversample));
                sub_x = (float)(nk_tt__oversample_shift((int)(spc->h_oversample)));
                sub_y = (float)(nk_tt__oversample_shift((int)(spc->v_oversample)));
                for (j = (int)(0); (j) < (ranges[i].num_chars); ++j)
                {
                    nk_rp_rect* r = &rects[k];
                    if ((r->was_packed) != 0)
                    {
                        nk_tt_packedchar* bc = &ranges[i].chardata_for_range[j];
                        int advance;
                        int lsb;
                        int x0;
                        int y0;
                        int x1;
                        int y1;
                        int codepoint =
                            (int)
                                ((ranges[i].first_unicode_codepoint_in_range) != 0
                                    ? ranges[i].first_unicode_codepoint_in_range + j
                                    : ranges[i].array_of_unicode_codepoints[j]);
                        int glyph = (int)(nk_tt_FindGlyphIndex(info, (int)(codepoint)));
                        ushort pad = (ushort)(spc->padding);
                        r->x = ((ushort)((int)(r->x) + (int)(pad)));
                        r->y = ((ushort)((int)(r->y) + (int)(pad)));
                        r->w = ((ushort)((int)(r->w) - (int)(pad)));
                        r->h = ((ushort)((int)(r->h) - (int)(pad)));
                        nk_tt_GetGlyphHMetrics(info, (int)(glyph), &advance, &lsb);
                        nk_tt_GetGlyphBitmapBox(info, (int)(glyph), (float)(scale * (float)(spc->h_oversample)),
                            (float)(scale * (float)(spc->v_oversample)), &x0, &y0, &x1, &y1);
                        nk_tt_MakeGlyphBitmapSubpixel(info, spc->pixels + r->x + r->y * spc->stride_in_bytes,
                            (int)(r->w - spc->h_oversample + 1), (int)(r->h - spc->v_oversample + 1), (int)(spc->stride_in_bytes),
                            (float)(scale * (float)(spc->h_oversample)), (float)(scale * (float)(spc->v_oversample)), (float)(0),
                            (float)(0), (int)(glyph));
                        if ((spc->h_oversample) > (1))
                            nk_tt__h_prefilter(spc->pixels + r->x + r->y * spc->stride_in_bytes, (int)(r->w), (int)(r->h),
                                (int)(spc->stride_in_bytes), (int)(spc->h_oversample));
                        if ((spc->v_oversample) > (1))
                            nk_tt__v_prefilter(spc->pixels + r->x + r->y * spc->stride_in_bytes, (int)(r->w), (int)(r->h),
                                (int)(spc->stride_in_bytes), (int)(spc->v_oversample));
                        bc->x0 = (ushort)(r->x);
                        bc->y0 = (ushort)(r->y);
                        bc->x1 = ((ushort)(r->x + r->w));
                        bc->y1 = ((ushort)(r->y + r->h));
                        bc->xadvance = (float)(scale * (float)(advance));
                        bc->xoff = (float)((float)(x0) * recip_h + sub_x);
                        bc->yoff = (float)((float)(y0) * recip_v + sub_y);
                        bc->xoff2 = (float)(((float)(x0) + r->w) * recip_h + sub_x);
                        bc->yoff2 = (float)(((float)(y0) + r->h) * recip_v + sub_y);
                    }
                    else
                    {
                        return_value = (int)(0);
                    }
                    ++k;
                }
            }
            spc->h_oversample = ((uint)(old_h_over));
            spc->v_oversample = ((uint)(old_v_over));
            return (int)(return_value);
        }

        public static void nk_tt_GetPackedQuad(nk_tt_packedchar* chardata, int pw, int ph, int char_index, float* xpos,
            float* ypos, nk_tt_aligned_quad* q, int align_to_integer)
        {
            float ipw = (float)(1.0f / (float)(pw));
            float iph = (float)(1.0f / (float)(ph));
            nk_tt_packedchar* b = (chardata + char_index);
            if ((align_to_integer) != 0)
            {
                int tx = (int)(nk_ifloorf((float)((*xpos + b->xoff) + 0.5f)));
                int ty = (int)(nk_ifloorf((float)((*ypos + b->yoff) + 0.5f)));
                float x = (float)(tx);
                float y = (float)(ty);
                q->x0 = (float)(x);
                q->y0 = (float)(y);
                q->x1 = (float)(x + b->xoff2 - b->xoff);
                q->y1 = (float)(y + b->yoff2 - b->yoff);
            }
            else
            {
                q->x0 = (float)(*xpos + b->xoff);
                q->y0 = (float)(*ypos + b->yoff);
                q->x1 = (float)(*xpos + b->xoff2);
                q->y1 = (float)(*ypos + b->yoff2);
            }

            q->s0 = (float)(b->x0 * ipw);
            q->t0 = (float)(b->y0 * iph);
            q->s1 = (float)(b->x1 * ipw);
            q->t1 = (float)(b->y1 * iph);
            *xpos += (float)(b->xadvance);
        }

        public static int nk_font_bake_pack(nk_font_baker* baker, ulong* image_memory, ref int width, ref int height,
            ref nk_recti custom, nk_font_config config_list, int count)
        {
            ulong max_height = (ulong)(1024 * 32);
            nk_font_config config_iter;
            nk_font_config it;
            int total_glyph_count = (int)(0);
            int total_range_count = (int)(0);
            int range_count = (int)(0);
            int i = (int)(0);
            if (((((image_memory == null))) || (config_list == null)) || (count == 0))
                return (int)(nk_false);
            for (config_iter = config_list; config_iter != null; config_iter = config_iter.next)
            {
                it = config_iter;
                do
                {
                    range_count = (int)(nk_range_count(it.range));
                    total_range_count += (int)(range_count);
                    total_glyph_count += (int)(nk_range_glyph_count(it.range, (int)(range_count)));
                } while ((it = it.n) != config_iter);
            }
            for (config_iter = config_list; config_iter != null; config_iter = config_iter.next)
            {
                it = config_iter;
                do
                {
                    if (nk_tt_InitFont(&baker->build[i++].info, (byte*)(it.ttf_blob), (int)(0)) == 0) return (int)(nk_false);
                } while ((it = it.n) != config_iter);
            }
            height = (int)(0);
            width = (int)(((total_glyph_count) > (1000)) ? 1024 : 512);
            nk_tt_PackBegin(&baker->spc, null, (int)(width), (int)(max_height), (int)(0), (int)(1));
            {
                int input_i = (int)(0);
                int range_n = (int)(0);
                int rect_n = (int)(0);
                int char_n = (int)(0);
                {
                    nk_rp_rect custom_space = new nk_rp_rect();
                    nk_zero(&custom_space, (ulong)(sizeof(nk_rp_rect)));
                    custom_space.w = ((ushort)((custom.w * 2) + 1));
                    custom_space.h = ((ushort)(custom.h + 1));
                    nk_tt_PackSetOversampling(&baker->spc, (uint)(1), (uint)(1));
                    nk_rp_pack_rects((NkRpContext*)(baker->spc.pack_info), &custom_space, (int)(1));
                    height = (int)((height) < (custom_space.y + custom_space.h) ? (custom_space.y + custom_space.h) : (height));
                    custom.x = ((short)(custom_space.x));
                    custom.y = ((short)(custom_space.y));
                    custom.w = ((short)(custom_space.w));
                    custom.h = ((short)(custom_space.h));
                }
                for (input_i = (int)(0), config_iter = config_list;
                    ((input_i) < (count)) && ((config_iter) != null);
                    config_iter = config_iter.next)
                {
                    it = config_iter;
                    do
                    {
                        int n = (int)(0);
                        int glyph_count;
                        uint* in_range;
                        nk_font_config cfg = it;
                        nk_font_bake_data* tmp = &baker->build[input_i++];
                        glyph_count = (int)(0);
                        range_count = (int)(0);
                        for (in_range = cfg.range; ((in_range[0]) != 0) && ((in_range[1]) != 0); in_range += 2)
                        {
                            glyph_count += (int)((int)(in_range[1] - in_range[0]) + 1);
                            range_count++;
                        }
                        tmp->ranges = baker->ranges + range_n;
                        tmp->range_count = ((uint)(range_count));
                        range_n += (int)(range_count);
                        for (i = (int)(0); (i) < (range_count); ++i)
                        {
                            in_range = &cfg.range[i * 2];
                            tmp->ranges[i].font_size = (float)(cfg.size);
                            tmp->ranges[i].first_unicode_codepoint_in_range = ((int)(in_range[0]));
                            tmp->ranges[i].num_chars = (int)((int)(in_range[1] - in_range[0]) + 1);
                            tmp->ranges[i].chardata_for_range = baker->packed_chars + char_n;
                            char_n += (int)(tmp->ranges[i].num_chars);
                        }
                        tmp->rects = baker->rects + rect_n;
                        rect_n += (int)(glyph_count);
                        nk_tt_PackSetOversampling(&baker->spc, (uint)(cfg.oversample_h), (uint)(cfg.oversample_v));
                        n =
                            (int)
                                (nk_tt_PackFontRangesGatherRects(&baker->spc, &tmp->info, tmp->ranges, (int)(tmp->range_count), tmp->rects));
                        nk_rp_pack_rects((NkRpContext*)(baker->spc.pack_info), tmp->rects, (int)(n));
                        for (i = (int)(0); (i) < (n); ++i)
                        {
                            if ((tmp->rects[i].was_packed) != 0)
                                height = (int)((height) < (tmp->rects[i].y + tmp->rects[i].h) ? (tmp->rects[i].y + tmp->rects[i].h) : (height));
                        }
                    } while ((it = it.n) != config_iter);
                }
            }

            height = ((int)(nk_round_up_pow2((uint)(height))));
            *image_memory = (ulong)((ulong)(width) * (ulong)(height));
            return (int)(nk_true);
        }

        public static void nk_font_bake(nk_font_baker* baker, void* image_memory, int width, int height, nk_font_glyph* glyphs,
            int glyphs_count, nk_font_config config_list, int font_count)
        {
            int input_i = (int)(0);
            uint glyph_n = (uint)(0);
            nk_font_config config_iter;
            nk_font_config it;
            if (((((((image_memory == null) || (width == 0)) || (height == 0)) || (config_list == null)) || (font_count == 0)) ||
                 (glyphs == null)) || (glyphs_count == 0)) return;
            nk_zero(image_memory, (ulong)((ulong)(width) * (ulong)(height)));
            baker->spc.pixels = (byte*)(image_memory);
            baker->spc.height = (int)(height);
            for (input_i = (int)(0), config_iter = config_list;
                ((input_i) < (font_count)) && ((config_iter) != null);
                config_iter = config_iter.next)
            {
                it = config_iter;
                do
                {
                    nk_font_config cfg = it;
                    nk_font_bake_data* tmp = &baker->build[input_i++];
                    nk_tt_PackSetOversampling(&baker->spc, (uint)(cfg.oversample_h), (uint)(cfg.oversample_v));
                    nk_tt_PackFontRangesRenderIntoRects(&baker->spc, &tmp->info, tmp->ranges, (int)(tmp->range_count), tmp->rects);
                } while ((it = it.n) != config_iter);
            }
            nk_tt_PackEnd(&baker->spc);
            for (input_i = (int)(0), config_iter = config_list;
                ((input_i) < (font_count)) && ((config_iter) != null);
                config_iter = config_iter.next)
            {
                it = config_iter;
                do
                {
                    ulong i = (ulong)(0);
                    int char_idx = (int)(0);
                    uint glyph_count = (uint)(0);
                    nk_font_config cfg = it;
                    nk_font_bake_data* tmp = &baker->build[input_i++];
                    nk_baked_font dst_font = cfg.font;
                    float font_scale = (float)(nk_tt_ScaleForPixelHeight(&tmp->info, (float)(cfg.size)));
                    int unscaled_ascent;
                    int unscaled_descent;
                    int unscaled_line_gap;
                    nk_tt_GetFontVMetrics(&tmp->info, &unscaled_ascent, &unscaled_descent, &unscaled_line_gap);
                    if (cfg.merge_mode == 0)
                    {
                        dst_font.ranges = cfg.range;
                        dst_font.height = (float)(cfg.size);
                        dst_font.ascent = (float)((float)(unscaled_ascent) * font_scale);
                        dst_font.descent = (float)((float)(unscaled_descent) * font_scale);
                        dst_font.glyph_offset = (uint)(glyph_n);
                    }
                    for (i = (ulong)(0); (i) < (tmp->range_count); ++i)
                    {
                        nk_tt_pack_range* range = &tmp->ranges[i];
                        for (char_idx = (int)(0); (char_idx) < (range->num_chars); char_idx++)
                        {
                            char codepoint = (char)0;
                            float dummy_x = (float)(0);
                            float dummy_y = (float)(0);
                            nk_tt_aligned_quad q = new nk_tt_aligned_quad();
                            nk_font_glyph* glyph;
                            nk_tt_packedchar* pc = &range->chardata_for_range[char_idx];
                            if ((((pc->x0 == 0) && (pc->x1 == 0)) && (pc->y0 == 0)) && (pc->y1 == 0)) continue;
                            codepoint = ((char)(range->first_unicode_codepoint_in_range + char_idx));
                            nk_tt_GetPackedQuad(range->chardata_for_range, (int)(width), (int)(height), (int)(char_idx), &dummy_x,
                                &dummy_y, &q, (int)(0));
                            glyph = &glyphs[dst_font.glyph_offset + dst_font.glyph_count + glyph_count];
                            glyph->codepoint = codepoint;
                            glyph->x0 = (float)(q.x0);
                            glyph->y0 = (float)(q.y0);
                            glyph->x1 = (float)(q.x1);
                            glyph->y1 = (float)(q.y1);
                            glyph->y0 += (float)(dst_font.ascent + 0.5f);
                            glyph->y1 += (float)(dst_font.ascent + 0.5f);
                            glyph->w = (float)(glyph->x1 - glyph->x0 + 0.5f);
                            glyph->h = (float)(glyph->y1 - glyph->y0);
                            if ((cfg.coord_type) == (NK_COORD_PIXEL))
                            {
                                glyph->u0 = (float)(q.s0 * (float)(width));
                                glyph->v0 = (float)(q.t0 * (float)(height));
                                glyph->u1 = (float)(q.s1 * (float)(width));
                                glyph->v1 = (float)(q.t1 * (float)(height));
                            }
                            else
                            {
                                glyph->u0 = (float)(q.s0);
                                glyph->v0 = (float)(q.t0);
                                glyph->u1 = (float)(q.s1);
                                glyph->v1 = (float)(q.t1);
                            }
                            glyph->xadvance = (float)(pc->xadvance + cfg.spacing.x);
                            if ((cfg.pixel_snap) != 0) glyph->xadvance = ((float)((int)(glyph->xadvance + 0.5f)));
                            glyph_count++;
                        }
                    }
                    dst_font.glyph_count += (uint)(glyph_count);
                    glyph_n += (uint)(glyph_count);
                } while ((it = it.n) != config_iter);
            }
        }

        public static nk_font_glyph* nk_font_find_glyph(NkFont font, char unicode)
        {
            int i = (int)(0);
            int count;
            int total_glyphs = (int)(0);
            nk_font_glyph* glyph = null;
            nk_font_config iter = null;
            if ((font == null) || (font.Glyphs == null)) return null;
            glyph = font.Fallback;
            iter = font.Config;
            do
            {
                count = (int)(nk_range_count(iter.range));
                for (i = (int)(0); (i) < (count); ++i)
                {
                    uint f = (uint)(iter.range[(i * 2) + 0]);
                    uint t = (uint)(iter.range[(i * 2) + 1]);
                    int diff = (int)((t - f) + 1);
                    if (((unicode) >= (f)) && (unicode <= t)) return &font.Glyphs[((uint)(total_glyphs) + (unicode - f))];
                    total_glyphs += (int)(diff);
                }
            } while ((iter = iter.n) != font.Config);
            return glyph;
        }

        public static void nk_font_init(NkFont font, float pixel_height, char fallback_codepoint, nk_font_glyph* glyphs,
            nk_baked_font baked_font, NkHandle atlas)
        {
            nk_baked_font baked = new nk_baked_font();
            if (((font == null) || (glyphs == null)) || (baked_font == null)) return;
            baked = (nk_baked_font)(baked_font);
            font.Fallback = null;
            font.Info = (nk_baked_font)(baked);
            font.Scale = (float)(pixel_height / font.Info.height);
            font.Glyphs = &glyphs[baked_font.glyph_offset];
            font.Texture = (NkHandle)(atlas);
            font.FallbackCodepoint = fallback_codepoint;
            font.Fallback = nk_font_find_glyph(font, fallback_codepoint);
            font.Handle.Height = (float)(font.Info.height * font.Scale);
            font.Handle.Width = font.text_width;

            font.Handle.Query = font.query_font_glyph;
            font.Handle.Texture = (NkHandle)(font.Texture);
        }

        public static void nk_font_atlas_begin(NkFontAtlas atlas)
        {
            if (((((atlas == null))))) return;
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

            cfg = nk_font_config_clone(config);
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
                cfg.ttf_blob = CRuntime.Malloc((ulong)(cfg.ttf_size));
                if (cfg.ttf_blob == null)
                {
                    atlas.FontNum++;
                    return null;
                }
                nk_memcopy(cfg.ttf_blob, config.ttf_blob, (ulong)(cfg.ttf_size));
                cfg.ttf_data_owned_by_atlas = (byte)(1);
            }

            atlas.FontNum++;
            return font;
        }

        public static NkFont nk_font_atlas_add_from_memory(NkFontAtlas atlas, void* memory, ulong size, float height,
            nk_font_config config)
        {
            nk_font_config cfg = new nk_font_config();
            if (((((((atlas == null))) || (memory == null)) || (size == 0)))) return null;
            cfg = (nk_font_config)((config != null) ? config : nk_font_config_((float)(height)));
            cfg.ttf_blob = memory;
            cfg.ttf_size = (ulong)(size);
            cfg.size = (float)(height);
            cfg.ttf_data_owned_by_atlas = (byte)(0);
            return nk_font_atlas_add(atlas, cfg);
        }

        public static NkFont nk_font_atlas_add_compressed(NkFontAtlas atlas, void* compressed_data, ulong compressed_size,
            float height, nk_font_config config)
        {
            uint decompressed_size;
            void* decompressed_data;
            nk_font_config cfg = new nk_font_config();
            if ((((((atlas == null) || (compressed_data == null)))))) return null;
            decompressed_size = (uint)(nk_decompress_length((byte*)(compressed_data)));
            decompressed_data = CRuntime.Malloc((ulong)(decompressed_size));
            if (decompressed_data == null) return null;
            nk_decompress((byte*)(decompressed_data), (byte*)(compressed_data), (uint)(compressed_size));
            cfg = (nk_font_config)((config != null) ? config : nk_font_config_((float)(height)));
            cfg.ttf_blob = decompressed_data;
            cfg.ttf_size = (ulong)(decompressed_size);
            cfg.size = (float)(height);
            cfg.ttf_data_owned_by_atlas = (byte)(1);
            return nk_font_atlas_add(atlas, cfg);
        }

        public static NkFont nk_font_atlas_add_compressed_base85(NkFontAtlas atlas, byte* data_base85, float height,
            nk_font_config config)
        {
            int compressed_size;
            void* compressed_data;
            if ((((((atlas == null) || (data_base85 == null)))))) return null;
            compressed_size = (int)(((nk_strlen(data_base85) + 4) / 5) * 4);
            compressed_data = CRuntime.Malloc((ulong)(compressed_size));
            if (compressed_data == null) return null;
            nk_decode_85((byte*)(compressed_data), (byte*)(data_base85));
            NkFont font = nk_font_atlas_add_compressed(atlas, compressed_data, (ulong)(compressed_size), (float)(height),
                config);
            CRuntime.Free(compressed_data);
            return font;
        }

        public static void* nk_font_atlas_bake(NkFontAtlas atlas, ref int width, ref int height, int fmt)
        {
            int i = (int)(0);
            void* tmp = null;
            ulong tmp_size;
            ulong img_size;
            NkFont font_iter;
            nk_font_baker* baker;
            if (atlas == null) return null;
            if (atlas.FontNum == 0) atlas.DefaultFont = nk_font_atlas_add_default(atlas, (float)(13.0f), null);
            if (atlas.FontNum == 0) return null;
            nk_font_baker_memory(&tmp_size, ref atlas.GlyphCount, atlas.Config, (int)(atlas.FontNum));
            tmp = CRuntime.Malloc((ulong)(tmp_size));
            if (tmp == null) goto failed;
            baker = nk_font_baker_(tmp, (int)(atlas.GlyphCount), (int)(atlas.FontNum));
            atlas.Glyphs =
                (nk_font_glyph*)(CRuntime.Malloc((ulong)((ulong)sizeof(nk_font_glyph) * (ulong)(atlas.GlyphCount))));
            if (atlas.Glyphs == null) goto failed;
            atlas.Custom.w = (short)((90 * 2) + 1);
            atlas.Custom.h = (short)(27 + 1);
            if (
                nk_font_bake_pack(baker, &img_size, ref width, ref height, ref atlas.Custom, atlas.Config, (int)(atlas.FontNum)) ==
                0) goto failed;
            atlas.Pixel = CRuntime.Malloc((ulong)(img_size));
            if (atlas.Pixel == null) goto failed;
            nk_font_bake(baker, atlas.Pixel, (int)(width), (int)(height), atlas.Glyphs, (int)(atlas.GlyphCount), atlas.Config,
                (int)(atlas.FontNum));
            fixed (byte* ptr = nk_custom_cursor_data)
            {
                nk_font_bake_custom_data(atlas.Pixel, (int)(width), (int)(height), (nk_recti)(atlas.Custom), ptr, (int)(90),
                    (int)(27), ('.'), ('X'));
            }
            if ((fmt) == (NK_FONT_ATLAS_RGBA32))
            {
                void* img_rgba = CRuntime.Malloc((ulong)(width * height * 4));
                if (img_rgba == null) goto failed;
                nk_font_bake_convert(img_rgba, (int)(width), (int)(height), atlas.Pixel);
                CRuntime.Free(atlas.Pixel);
                atlas.Pixel = img_rgba;
            }

            atlas.TexWidth = (int)(width);
            atlas.TexHeight = (int)(height);
            for (font_iter = atlas.Fonts; font_iter != null; font_iter = font_iter.Next)
            {
                NkFont font = font_iter;
                nk_font_config config = font.Config;
                nk_font_init(font, (float)(config.size), config.fallback_glyph, atlas.Glyphs, config.font,
                    (NkHandle)(nk_handle_ptr(null)));
            }
            for (i = (int)(0); (i) < (NK_CURSOR_COUNT); ++i)
            {
                nk_cursor cursor = atlas.Cursors[i];
                cursor.img.w = ((ushort)(width));
                cursor.img.h = ((ushort)(height));
                cursor.img.region[0] = ((ushort)(atlas.Custom.x + nk_cursor_data[i, 0].x));
                cursor.img.region[1] = ((ushort)(atlas.Custom.y + nk_cursor_data[i, 0].y));
                cursor.img.region[2] = ((ushort)(nk_cursor_data[i, 1].x));
                cursor.img.region[3] = ((ushort)(nk_cursor_data[i, 1].y));
                cursor.size = (nk_vec2)(nk_cursor_data[i, 1]);
                cursor.offset = (nk_vec2)(nk_cursor_data[i, 2]);
            }
            CRuntime.Free(tmp);
            return atlas.Pixel;
            failed:
            ;
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
            int i = (int)(0);
            NkFont font_iter;
            if (atlas == null)
            {
                if (_null_ == null) return;
                _null_->texture = (NkHandle)(texture);
                _null_->uv = (nk_vec2)(nk_vec2_((float)(0.5f), (float)(0.5f)));
            }

            if ((_null_) != null)
            {
                _null_->texture = (NkHandle)(texture);
                _null_->uv.x = (float)((atlas.Custom.x + 0.5f) / (float)(atlas.TexWidth));
                _null_->uv.y = (float)((atlas.Custom.y + 0.5f) / (float)(atlas.TexHeight));
            }

            for (font_iter = atlas.Fonts; font_iter != null; font_iter = font_iter.Next)
            {
                font_iter.Texture = (NkHandle)(texture);
                font_iter.Handle.Texture = (NkHandle)(texture);
            }
            for (i = (int)(0); (i) < (NK_CURSOR_COUNT); ++i)
            {
                atlas.Cursors[i].img.handle = (NkHandle)(texture);
            }
            CRuntime.Free(atlas.Pixel);
            atlas.Pixel = null;
            atlas.TexWidth = (int)(0);
            atlas.TexHeight = (int)(0);
            atlas.Custom.x = (short)(0);
            atlas.Custom.y = (short)(0);
            atlas.Custom.w = (short)(0);
            atlas.Custom.h = (short)(0);
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