using System.Runtime.InteropServices;

namespace NuklearSharp
{

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct nk_draw_null_texture
        {
            public NkHandle texture;
            public NkVec2 uv;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct nk_draw_vertex_layout_element
        {
            public NkDrawVertexLayoutAttribute attribute;
            public VertexLayoutFormat format;
            public ulong offset;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe partial struct nk_draw_command
        {
            public uint elem_count;
            public NkRect clip_rect;
            public NkHandle texture;
        }
    public unsafe static partial class Nk
    {
        public static void nk_draw_list_init(NkDrawList list)
        {
            ulong i = (ulong)(0);
            if (list == null) return;

            for (i = (ulong)(0); (i) < (ulong)list.CircleVtx.Length; ++i)
            {
                float a = (float)(((float)(i) / (float)(ulong)list.CircleVtx.Length) * 2 * 3.141592654f);
                list.CircleVtx[i].x = (float)(nk_cos((float)(a)));
                list.CircleVtx[i].y = (float)(nk_sin((float)(a)));
            }
        }

        public static void nk_draw_list_setup(NkDrawList canvas, NkConvertConfig config, NkBuffer<nk_draw_command> cmds,
            NkBuffer<byte> vertices, NkBuffer<ushort> elements, bool line_aa, bool shape_aa)
        {
            if (((((canvas == null) || (config == null)) || (cmds == null)) || (vertices == null)) || (elements == null)) return;
            canvas.Buffer = cmds;
            canvas.Config = (NkConvertConfig)(config);
            canvas.Elements = elements;
            canvas.Vertices = vertices;
            canvas.LineAa = (line_aa);
            canvas.ShapeAa = (shape_aa);
            canvas.ClipRect = (NkRect)(nk_null_rect);
        }

        public static void nk_draw_list_clear(NkDrawList list)
        {
            if (list == null) return;
            list.Buffer.Reset();
            list.Vertices.Reset();
            list.Elements.Reset();
            list.Points.Reset();
            list.Normals.Reset();
            list.ClipRect = (NkRect)nk_null_rect;
        }

        public static int nk_draw_list_alloc_path(NkDrawList list, int count)
        {
            var result = list.Points.Count;
            list.Points.AddToEnd(count);
            return result;
        }

        public static NkVec2 nk_draw_list_path_last(NkDrawList list)
        {
            return list.Points.Data[list.Points.Count - 1];
        }

        public static int nk_draw_list_push_command(NkDrawList list, NkRect clip, NkHandle texture)
        {
            var result = list.Buffer.Count;
            list.Buffer.AddToEnd(1);
            list.Buffer.Data[list.Buffer.Count - 1].elem_count = (uint)(0);
            list.Buffer.Data[list.Buffer.Count - 1].clip_rect = (NkRect)(clip);
            list.Buffer.Data[list.Buffer.Count - 1].texture = (NkHandle)(texture);
            list.ClipRect = (NkRect)(clip);

            return result;
        }

        public static void nk_draw_list_add_clip(NkDrawList list, NkRect rect)
        {
            if (list == null) return;
            if (list.Buffer.Count == 0)
            {
                nk_draw_list_push_command(list, (NkRect)(rect), (NkHandle)(list.Config.Null.texture));
            }
            else
            {
                fixed (nk_draw_command* prev2 = list.Buffer.Data)
                {
                    nk_draw_command* prev = prev2 + list.Buffer.Count - 1;
                    if ((prev->elem_count) == (0)) prev->clip_rect = (NkRect)(rect);
                    nk_draw_list_push_command(list, (NkRect)(rect), (NkHandle)(prev->texture));
                }
            }

        }

        public static void nk_draw_list_push_image(NkDrawList list, NkHandle texture)
        {
            if (list == null) return;
            if (list.Buffer.Count == 0)
            {
                nk_draw_list_push_command(list, (NkRect)(nk_null_rect), (NkHandle)(texture));
            }
            else
            {
                fixed (nk_draw_command* prev2 = list.Buffer.Data)
                {
                    nk_draw_command* prev = prev2 + list.Buffer.Count - 1;
                    if ((prev->elem_count) == (0))
                    {
                        prev->texture = (NkHandle)(texture);
                    }
                    else if (prev->texture.id != texture.id)
                        nk_draw_list_push_command(list, (NkRect)(prev->clip_rect), (NkHandle)(texture));
                }
            }

        }

        public static int nk_draw_vertex_layout_element_is_end_of_layout(nk_draw_vertex_layout_element* element)
        {
            return
                (int)(((element->attribute) == (NkDrawVertexLayoutAttribute.ATTRIBUTE_COUNT)) || ((element->format) == VertexLayoutFormat.COUNT) ? 1 : 0);
        }

        public static void nk_draw_list_stroke_poly_line(NkDrawList list, NkColor color,
            bool closed, float thickness, bool aliasing)
        {
            ulong count;
            int thick_line;

            ulong points_count = (ulong)list.Points.Count;
            NkColorF col = new NkColorF();
            NkColorF col_trans = new NkColorF();
            if ((list == null) || ((points_count) < (2))) return;
            color.a = ((byte)((float)(color.a) * list.Config.GlobalAlpha));
            count = (ulong)(points_count);
            if (!closed) count = (ulong)(points_count - 1);
            thick_line = (int)((thickness) > (1.0f) ? 1 : 0);
            color.a = ((byte)((float)(color.a) * list.Config.GlobalAlpha));
            nk_color_fv(&col.r, (NkColor)(color));
            col_trans = (NkColorF)(col);
            col_trans.a = (float)(0);
            if ((aliasing))
            {
                float AA_SIZE = (float)(1.0f);
                ulong i1 = (ulong)(0);
                ulong index = (ulong)(list.VertexOffset);
                ulong idx_count = (ulong)((thick_line) != 0 ? (count * 18) : (count * 12));
                ulong vtx_count = (ulong)((thick_line) != 0 ? (points_count * 4) : (points_count * 3));


                int vtxStart = list.Vertices.Count;
                list.Vertices.AddToEnd((int)(vtx_count * list.Config.VertexSize));
                int idxStart = list.AddElements((int)idx_count);

                NkVec2* temp;
                int points_total = (int)(((thick_line) != 0 ? 5 : 3) * (int)points_count);

                int normalsStart = list.Normals.Count;
                list.Normals.AddToEnd(points_total);

                fixed (NkVec2* normals2 = list.Normals.Data)
                {
                    NkVec2* normals = normals2 + normalsStart;
                    fixed (byte* vtx2 = list.Vertices.Data)
                    {
                        void* vtx = (void*)(vtx2 + vtxStart);
                        fixed (ushort* ids2 = list.Elements.Data)
                        {
                            ushort* ids = ids2 + idxStart;
                            if (normals == null) return;
                            temp = normals + points_count;
                            for (i1 = (ulong)(0); (i1) < (count); ++i1)
                            {
                                ulong i2 = (ulong)(((i1 + 1) == (ulong)(points_count)) ? 0 : (i1 + 1));
                                NkVec2 diff =
                                    (NkVec2)
                                        (nk_vec2_((float)((list.Points[i2]).x - (list.Points[i1]).x),
                                            (float)((list.Points[i2]).y - (list.Points[i1]).y)));
                                float len;
                                len = (float)((diff).x * (diff).x + (diff).y * (diff).y);
                                if (len != 0.0f) len = (float)(nk_inv_sqrt((float)(len)));
                                else len = (float)(1.0f);
                                diff = (NkVec2)(nk_vec2_((float)((diff).x * (len)), (float)((diff).y * (len))));
                                normals[i1].x = (float)(diff.y);
                                normals[i1].y = (float)(-diff.x);
                            }
                            if (!closed) normals[points_count - 1] = (NkVec2)(normals[points_count - 2]);
                            if (thick_line == 0)
                            {
                                ulong idx1;
                                ulong i;
                                if (!closed)
                                {
                                    NkVec2 d = new NkVec2();
                                    temp[0] =
                                        (NkVec2)
                                            (nk_vec2_(
                                                (float)
                                                    ((list.Points[0]).x + (nk_vec2_((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).x),
                                                (float)
                                                    ((list.Points[0]).y + (nk_vec2_((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).y)));
                                    temp[1] =
                                        (NkVec2)
                                            (nk_vec2_(
                                                (float)
                                                    ((list.Points[0]).x - (nk_vec2_((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).x),
                                                (float)
                                                    ((list.Points[0]).y - (nk_vec2_((float)((normals[0]).x * (AA_SIZE)), (float)((normals[0]).y * (AA_SIZE)))).y)));
                                    d =
                                        (NkVec2)
                                            (nk_vec2_((float)((normals[points_count - 1]).x * (AA_SIZE)),
                                                (float)((normals[points_count - 1]).y * (AA_SIZE))));
                                    temp[(points_count - 1) * 2 + 0] =
                                        (NkVec2)
                                            (nk_vec2_((float)((list.Points[points_count - 1]).x + (d).x),
                                                (float)((list.Points[points_count - 1]).y + (d).y)));
                                    temp[(points_count - 1) * 2 + 1] =
                                        (NkVec2)
                                            (nk_vec2_((float)((list.Points[points_count - 1]).x - (d).x),
                                                (float)((list.Points[points_count - 1]).y - (d).y)));
                                }
                                idx1 = (ulong)(index);
                                for (i1 = (ulong)(0); (i1) < (count); i1++)
                                {
                                    NkVec2 dm = new NkVec2();
                                    float dmr2;
                                    ulong i2 = (ulong)(((i1 + 1) == (points_count)) ? 0 : (i1 + 1));
                                    ulong idx2 = (ulong)(((i1 + 1) == (points_count)) ? index : (idx1 + 3));
                                    dm =
                                        (NkVec2)
                                            (nk_vec2_(
                                                (float)
                                                    ((nk_vec2_((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).x *
                                                     (0.5f)),
                                                (float)
                                                    ((nk_vec2_((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).y *
                                                     (0.5f))));
                                    dmr2 = (float)(dm.x * dm.x + dm.y * dm.y);
                                    if ((dmr2) > (0.000001f))
                                    {
                                        float scale = (float)(1.0f / dmr2);
                                        scale = (float)((100.0f) < (scale) ? (100.0f) : (scale));
                                        dm = (NkVec2)(nk_vec2_((float)((dm).x * (scale)), (float)((dm).y * (scale))));
                                    }
                                    dm = (NkVec2)(nk_vec2_((float)((dm).x * (AA_SIZE)), (float)((dm).y * (AA_SIZE))));
                                    temp[i2 * 2 + 0] =
                                        (NkVec2)(nk_vec2_((float)((list.Points[i2]).x + (dm).x), (float)((list.Points[i2]).y + (dm).y)));
                                    temp[i2 * 2 + 1] =
                                        (NkVec2)(nk_vec2_((float)((list.Points[i2]).x - (dm).x), (float)((list.Points[i2]).y - (dm).y)));
                                    ids[0] = ((ushort)(idx2 + 0));
                                    ids[1] = ((ushort)(idx1 + 0));
                                    ids[2] = ((ushort)(idx1 + 2));
                                    ids[3] = ((ushort)(idx1 + 2));
                                    ids[4] = ((ushort)(idx2 + 2));
                                    ids[5] = ((ushort)(idx2 + 0));
                                    ids[6] = ((ushort)(idx2 + 1));
                                    ids[7] = ((ushort)(idx1 + 1));
                                    ids[8] = ((ushort)(idx1 + 0));
                                    ids[9] = ((ushort)(idx1 + 0));
                                    ids[10] = ((ushort)(idx2 + 0));
                                    ids[11] = ((ushort)(idx2 + 1));
                                    ids += 12;
                                    idx1 = (ulong)(idx2);
                                }
                                for (i = (ulong)(0); (i) < (points_count); ++i)
                                {
                                    NkVec2 uv = (NkVec2)(list.Config.Null.uv);
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(list.Points[i]), (NkVec2)(uv), (NkColorF)(col));
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(temp[i * 2 + 0]), (NkVec2)(uv), (NkColorF)(col_trans));
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(temp[i * 2 + 1]), (NkVec2)(uv), (NkColorF)(col_trans));
                                }
                            }
                            else
                            {
                                ulong idx1;
                                ulong i;
                                float half_inner_thickness = (float)((thickness - AA_SIZE) * 0.5f);
                                if (!closed)
                                {
                                    NkVec2 d1 =
                                        (NkVec2)
                                            (nk_vec2_((float)((normals[0]).x * (half_inner_thickness + AA_SIZE)),
                                                (float)((normals[0]).y * (half_inner_thickness + AA_SIZE))));
                                    NkVec2 d2 =
                                        (NkVec2)
                                            (nk_vec2_((float)((normals[0]).x * (half_inner_thickness)), (float)((normals[0]).y * (half_inner_thickness))));
                                    temp[0] = (NkVec2)(nk_vec2_((float)((list.Points[0]).x + (d1).x), (float)((list.Points[0]).y + (d1).y)));
                                    temp[1] = (NkVec2)(nk_vec2_((float)((list.Points[0]).x + (d2).x), (float)((list.Points[0]).y + (d2).y)));
                                    temp[2] = (NkVec2)(nk_vec2_((float)((list.Points[0]).x - (d2).x), (float)((list.Points[0]).y - (d2).y)));
                                    temp[3] = (NkVec2)(nk_vec2_((float)((list.Points[0]).x - (d1).x), (float)((list.Points[0]).y - (d1).y)));
                                    d1 =
                                        (NkVec2)
                                            (nk_vec2_((float)((normals[points_count - 1]).x * (half_inner_thickness + AA_SIZE)),
                                                (float)((normals[points_count - 1]).y * (half_inner_thickness + AA_SIZE))));
                                    d2 =
                                        (NkVec2)
                                            (nk_vec2_((float)((normals[points_count - 1]).x * (half_inner_thickness)),
                                                (float)((normals[points_count - 1]).y * (half_inner_thickness))));
                                    temp[(points_count - 1) * 4 + 0] =
                                        (NkVec2)
                                            (nk_vec2_((float)((list.Points[points_count - 1]).x + (d1).x),
                                                (float)((list.Points[points_count - 1]).y + (d1).y)));
                                    temp[(points_count - 1) * 4 + 1] =
                                        (NkVec2)
                                            (nk_vec2_((float)((list.Points[points_count - 1]).x + (d2).x),
                                                (float)((list.Points[points_count - 1]).y + (d2).y)));
                                    temp[(points_count - 1) * 4 + 2] =
                                        (NkVec2)
                                            (nk_vec2_((float)((list.Points[points_count - 1]).x - (d2).x),
                                                (float)((list.Points[points_count - 1]).y - (d2).y)));
                                    temp[(points_count - 1) * 4 + 3] =
                                        (NkVec2)
                                            (nk_vec2_((float)((list.Points[points_count - 1]).x - (d1).x),
                                                (float)((list.Points[points_count - 1]).y - (d1).y)));
                                }
                                idx1 = (ulong)(index);
                                for (i1 = (ulong)(0); (i1) < (count); ++i1)
                                {
                                    NkVec2 dm_out = new NkVec2();
                                    NkVec2 dm_in = new NkVec2();
                                    ulong i2 = (ulong)(((i1 + 1) == (points_count)) ? 0 : (i1 + 1));
                                    ulong idx2 = (ulong)(((i1 + 1) == (points_count)) ? index : (idx1 + 4));
                                    NkVec2 dm =
                                        (NkVec2)
                                            (nk_vec2_(
                                                (float)
                                                    ((nk_vec2_((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).x *
                                                     (0.5f)),
                                                (float)
                                                    ((nk_vec2_((float)((normals[i1]).x + (normals[i2]).x), (float)((normals[i1]).y + (normals[i2]).y))).y *
                                                     (0.5f))));
                                    float dmr2 = (float)(dm.x * dm.x + dm.y * dm.y);
                                    if ((dmr2) > (0.000001f))
                                    {
                                        float scale = (float)(1.0f / dmr2);
                                        scale = (float)((100.0f) < (scale) ? (100.0f) : (scale));
                                        dm = (NkVec2)(nk_vec2_((float)((dm).x * (scale)), (float)((dm).y * (scale))));
                                    }
                                    dm_out =
                                        (NkVec2)
                                            (nk_vec2_((float)((dm).x * ((half_inner_thickness) + AA_SIZE)),
                                                (float)((dm).y * ((half_inner_thickness) + AA_SIZE))));
                                    dm_in = (NkVec2)(nk_vec2_((float)((dm).x * (half_inner_thickness)), (float)((dm).y * (half_inner_thickness))));
                                    temp[i2 * 4 + 0] =
                                        (NkVec2)(nk_vec2_((float)((list.Points[i2]).x + (dm_out).x), (float)((list.Points[i2]).y + (dm_out).y)));
                                    temp[i2 * 4 + 1] =
                                        (NkVec2)(nk_vec2_((float)((list.Points[i2]).x + (dm_in).x), (float)((list.Points[i2]).y + (dm_in).y)));
                                    temp[i2 * 4 + 2] =
                                        (NkVec2)(nk_vec2_((float)((list.Points[i2]).x - (dm_in).x), (float)((list.Points[i2]).y - (dm_in).y)));
                                    temp[i2 * 4 + 3] =
                                        (NkVec2)(nk_vec2_((float)((list.Points[i2]).x - (dm_out).x), (float)((list.Points[i2]).y - (dm_out).y)));
                                    ids[0] = ((ushort)(idx2 + 1));
                                    ids[1] = ((ushort)(idx1 + 1));
                                    ids[2] = ((ushort)(idx1 + 2));
                                    ids[3] = ((ushort)(idx1 + 2));
                                    ids[4] = ((ushort)(idx2 + 2));
                                    ids[5] = ((ushort)(idx2 + 1));
                                    ids[6] = ((ushort)(idx2 + 1));
                                    ids[7] = ((ushort)(idx1 + 1));
                                    ids[8] = ((ushort)(idx1 + 0));
                                    ids[9] = ((ushort)(idx1 + 0));
                                    ids[10] = ((ushort)(idx2 + 0));
                                    ids[11] = ((ushort)(idx2 + 1));
                                    ids[12] = ((ushort)(idx2 + 2));
                                    ids[13] = ((ushort)(idx1 + 2));
                                    ids[14] = ((ushort)(idx1 + 3));
                                    ids[15] = ((ushort)(idx1 + 3));
                                    ids[16] = ((ushort)(idx2 + 3));
                                    ids[17] = ((ushort)(idx2 + 2));
                                    ids += 18;
                                    idx1 = (ulong)(idx2);
                                }
                                for (i = (ulong)(0); (i) < (points_count); ++i)
                                {
                                    NkVec2 uv = (NkVec2)(list.Config.Null.uv);
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(temp[i * 4 + 0]), (NkVec2)(uv), (NkColorF)(col_trans));
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(temp[i * 4 + 1]), (NkVec2)(uv), (NkColorF)(col));
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(temp[i * 4 + 2]), (NkVec2)(uv), (NkColorF)(col));
                                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(temp[i * 4 + 3]), (NkVec2)(uv), (NkColorF)(col_trans));
                                }
                            }

                            list.Normals.Reset();
                        }
                    }
                }
            }
            else
            {
                ulong i1 = (ulong)(0);
                ulong idx = (ulong)(list.VertexOffset);
                ulong idx_count = (ulong)(count * 6);
                ulong vtx_count = (ulong)(count * 4);

                int vtxStart = list.Vertices.Count;
                list.Vertices.AddToEnd((int)(vtx_count * list.Config.VertexSize));
                int idxStart = list.AddElements((int)idx_count);

                fixed (byte* vtx2 = list.Vertices.Data)
                {
                    void* vtx = (void*)(vtx2 + vtxStart);
                    fixed (ushort* ids2 = list.Elements.Data)
                    {
                        ushort* ids = ids2 + idxStart;

                        for (i1 = (ulong)(0); (i1) < (count); ++i1)
                        {
                            float dx;
                            float dy;
                            NkVec2 uv = (NkVec2)(list.Config.Null.uv);
                            ulong i2 = (ulong)(((i1 + 1) == (points_count)) ? 0 : i1 + 1);
                            NkVec2 p1 = (NkVec2)(list.Points[i1]);
                            NkVec2 p2 = (NkVec2)(list.Points[i2]);
                            NkVec2 diff = (NkVec2)(nk_vec2_((float)((p2).x - (p1).x), (float)((p2).y - (p1).y)));
                            float len;
                            len = (float)((diff).x * (diff).x + (diff).y * (diff).y);
                            if (len != 0.0f) len = (float)(nk_inv_sqrt((float)(len)));
                            else len = (float)(1.0f);
                            diff = (NkVec2)(nk_vec2_((float)((diff).x * (len)), (float)((diff).y * (len))));
                            dx = (float)(diff.x * (thickness * 0.5f));
                            dy = (float)(diff.y * (thickness * 0.5f));
                            vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(p1.x + dy), (float)(p1.y - dx))),
                                (NkVec2)(uv), (NkColorF)(col));
                            vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(p2.x + dy), (float)(p2.y - dx))),
                                (NkVec2)(uv), (NkColorF)(col));
                            vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(p2.x - dy), (float)(p2.y + dx))),
                                (NkVec2)(uv), (NkColorF)(col));
                            vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(p1.x - dy), (float)(p1.y + dx))),
                                (NkVec2)(uv), (NkColorF)(col));
                            ids[0] = ((ushort)(idx + 0));
                            ids[1] = ((ushort)(idx + 1));
                            ids[2] = ((ushort)(idx + 2));
                            ids[3] = ((ushort)(idx + 0));
                            ids[4] = ((ushort)(idx + 2));
                            ids[5] = ((ushort)(idx + 3));
                            ids += 6;
                            idx += (ulong)(4);
                        }
                    }
                }
            }
        }

        public static void nk_draw_list_fill_poly_convex(NkDrawList list, NkColor color, bool aliasing)
        {
            NkColorF col = new NkColorF();
            NkColorF col_trans = new NkColorF();

            var points_count = (ulong)list.Points.Count;
            if ((list == null) || ((list.Points.Count) < (3))) return;
            color.a = ((byte)((float)(color.a) * list.Config.GlobalAlpha));
            nk_color_fv(&col.r, (NkColor)(color));
            col_trans = (NkColorF)(col);
            col_trans.a = (float)(0);
            if ((aliasing))
            {
                ulong i = (ulong)(0);
                ulong i0 = (ulong)(0);
                ulong i1 = (ulong)(0);
                float AA_SIZE = (float)(1.0f);
                ulong index = (ulong)(list.VertexOffset);
                ulong idx_count = (ulong)((points_count - 2) * 3 + points_count * 6);
                ulong vtx_count = (ulong)(points_count * 2);

                int vtxStart = list.Vertices.Count;
                list.Vertices.AddToEnd((int)(vtx_count * list.Config.VertexSize));
                int idxStart = list.AddElements((int)idx_count);

                fixed (byte* vtx2 = list.Vertices.Data)
                {
                    void* vtx = (void*)(vtx2 + vtxStart);
                    fixed (ushort* ids2 = list.Elements.Data)
                    {
                        ushort* ids = ids2 + idxStart;
                        uint vtx_inner_idx = (uint)(index + 0);
                        uint vtx_outer_idx = (uint)(index + 1);
                        if ((vtx == null) || (ids == null)) return;

                        int normalsStart = list.Normals.Count;
                        list.Normals.AddToEnd((int)points_count);

                        fixed (NkVec2* normals2 = list.Normals.Data)
                        {
                            NkVec2* normals = normals2 + normalsStart;

                            for (i = (ulong)(2); (i) < (points_count); i++)
                            {
                                ids[0] = ((ushort)(vtx_inner_idx));
                                ids[1] = ((ushort)(vtx_inner_idx + ((i - 1) << 1)));
                                ids[2] = ((ushort)(vtx_inner_idx + (i << 1)));
                                ids += 3;
                            }
                            for (i0 = (ulong)(points_count - 1), i1 = (ulong)(0); (i1) < (points_count); i0 = (ulong)(i1++))
                            {
                                NkVec2 p0 = (NkVec2)(list.Points[i0]);
                                NkVec2 p1 = (NkVec2)(list.Points[i1]);
                                NkVec2 diff = (NkVec2)(nk_vec2_((float)((p1).x - (p0).x), (float)((p1).y - (p0).y)));
                                float len = (float)((diff).x * (diff).x + (diff).y * (diff).y);
                                if (len != 0.0f) len = (float)(nk_inv_sqrt((float)(len)));
                                else len = (float)(1.0f);
                                diff = (NkVec2)(nk_vec2_((float)((diff).x * (len)), (float)((diff).y * (len))));
                                normals[i0].x = (float)(diff.y);
                                normals[i0].y = (float)(-diff.x);
                            }
                            for (i0 = (ulong)(points_count - 1), i1 = (ulong)(0); (i1) < (points_count); i0 = (ulong)(i1++))
                            {
                                NkVec2 uv = (NkVec2)(list.Config.Null.uv);
                                NkVec2 n0 = (NkVec2)(normals[i0]);
                                NkVec2 n1 = (NkVec2)(normals[i1]);
                                NkVec2 dm =
                                    (NkVec2)
                                        (nk_vec2_((float)((nk_vec2_((float)((n0).x + (n1).x), (float)((n0).y + (n1).y))).x * (0.5f)),
                                            (float)((nk_vec2_((float)((n0).x + (n1).x), (float)((n0).y + (n1).y))).y * (0.5f))));
                                float dmr2 = (float)(dm.x * dm.x + dm.y * dm.y);
                                if ((dmr2) > (0.000001f))
                                {
                                    float scale = (float)(1.0f / dmr2);
                                    scale = (float)((scale) < (100.0f) ? (scale) : (100.0f));
                                    dm = (NkVec2)(nk_vec2_((float)((dm).x * (scale)), (float)((dm).y * (scale))));
                                }
                                dm = (NkVec2)(nk_vec2_((float)((dm).x * (AA_SIZE * 0.5f)), (float)((dm).y * (AA_SIZE * 0.5f))));
                                vtx = nk_draw_vertex(vtx, list.Config,
                                    (NkVec2)(nk_vec2_((float)((list.Points[i1]).x - (dm).x), (float)((list.Points[i1]).y - (dm).y))),
                                    (NkVec2)(uv),
                                    (NkColorF)(col));
                                vtx = nk_draw_vertex(vtx, list.Config,
                                    (NkVec2)(nk_vec2_((float)((list.Points[i1]).x + (dm).x), (float)((list.Points[i1]).y + (dm).y))),
                                    (NkVec2)(uv),
                                    (NkColorF)(col_trans));
                                ids[0] = ((ushort)(vtx_inner_idx + (i1 << 1)));
                                ids[1] = ((ushort)(vtx_inner_idx + (i0 << 1)));
                                ids[2] = ((ushort)(vtx_outer_idx + (i0 << 1)));
                                ids[3] = ((ushort)(vtx_outer_idx + (i0 << 1)));
                                ids[4] = ((ushort)(vtx_outer_idx + (i1 << 1)));
                                ids[5] = ((ushort)(vtx_inner_idx + (i1 << 1)));
                                ids += 6;
                            }
                        }
                    }
                }
                list.Normals.Reset();
            }
            else
            {
                ulong i = (ulong)(0);
                ulong index = (ulong)(list.VertexOffset);
                ulong idx_count = (ulong)((points_count - 2) * 3);
                ulong vtx_count = (ulong)(points_count);
                int vtxStart = list.Vertices.Count;
                list.Vertices.AddToEnd((int)(vtx_count * list.Config.VertexSize));
                int idxStart = list.AddElements((int)idx_count);

                fixed (byte* vtx2 = list.Vertices.Data)
                {
                    void* vtx = (void*)(vtx2 + vtxStart);
                    fixed (ushort* ids2 = list.Elements.Data)
                    {
                        ushort* ids = ids2 + idxStart;
                        if ((vtx == null) || (ids == null)) return;
                        for (i = (ulong)(0); (i) < (vtx_count); ++i)
                        {
                            vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(list.Points[i]), (NkVec2)(list.Config.Null.uv),
                                (NkColorF)(col));
                        }
                        for (i = (ulong)(2); (i) < (points_count); ++i)
                        {
                            ids[0] = ((ushort)(index));
                            ids[1] = ((ushort)(index + i - 1));
                            ids[2] = ((ushort)(index + i));
                            ids += 3;
                        }
                    }
                }
            }
        }

        public static void nk_draw_list_path_clear(NkDrawList list)
        {
            if (list == null) return;
            list.Points.Reset();
        }

        public static void nk_draw_list_path_line_to(NkDrawList list, NkVec2 pos)
        {
            if (list == null) return;
            if (list.Buffer.Count == 0) nk_draw_list_add_clip(list, (NkRect)(nk_null_rect));

            if ((list.Buffer[list.Buffer.Count - 1].texture.ptr != list.Config.Null.texture.ptr))
                nk_draw_list_push_image(list, (NkHandle)(list.Config.Null.texture));
            int i = list.Points.Count;
            list.Points.AddToEnd(1);
            list.Points[i] = pos;
        }

        public static void nk_draw_list_path_arc_to_fast(NkDrawList list, NkVec2 center, float radius, int a_min, int a_max)
        {
            int a = (int)(0);
            if (list == null) return;
            if (a_min <= a_max)
            {
                for (a = (int)(a_min); a <= a_max; a++)
                {
                    NkVec2 c = (NkVec2)(list.CircleVtx[(ulong)(a) % (ulong)list.CircleVtx.Length]);
                    float x = (float)(center.x + c.x * radius);
                    float y = (float)(center.y + c.y * radius);
                    nk_draw_list_path_line_to(list, (NkVec2)(nk_vec2_((float)(x), (float)(y))));
                }
            }

        }

        public static void nk_draw_list_path_arc_to(NkDrawList list, NkVec2 center, float radius, float a_min, float a_max,
            uint segments)
        {
            uint i = (uint)(0);
            if (list == null) return;
            if ((radius) == (0.0f)) return;
            {
                float d_angle = (float)((a_max - a_min) / (float)(segments));
                float sin_d = (float)(nk_sin((float)(d_angle)));
                float cos_d = (float)(nk_cos((float)(d_angle)));
                float cx = (float)(nk_cos((float)(a_min)) * radius);
                float cy = (float)(nk_sin((float)(a_min)) * radius);
                for (i = (uint)(0); i <= segments; ++i)
                {
                    float new_cx;
                    float new_cy;
                    float x = (float)(center.x + cx);
                    float y = (float)(center.y + cy);
                    nk_draw_list_path_line_to(list, (NkVec2)(nk_vec2_((float)(x), (float)(y))));
                    new_cx = (float)(cx * cos_d - cy * sin_d);
                    new_cy = (float)(cy * cos_d + cx * sin_d);
                    cx = (float)(new_cx);
                    cy = (float)(new_cy);
                }
            }

        }

        public static void nk_draw_list_path_rect_to(NkDrawList list, NkVec2 a, NkVec2 b, float rounding)
        {
            float r;
            if (list == null) return;
            r = (float)(rounding);
            r =
                (float)
                    ((r) < (((b.x - a.x) < (0)) ? -(b.x - a.x) : (b.x - a.x))
                        ? (r)
                        : (((b.x - a.x) < (0)) ? -(b.x - a.x) : (b.x - a.x)));
            r =
                (float)
                    ((r) < (((b.y - a.y) < (0)) ? -(b.y - a.y) : (b.y - a.y))
                        ? (r)
                        : (((b.y - a.y) < (0)) ? -(b.y - a.y) : (b.y - a.y)));
            if ((r) == (0.0f))
            {
                nk_draw_list_path_line_to(list, (NkVec2)(a));
                nk_draw_list_path_line_to(list, (NkVec2)(nk_vec2_((float)(b.x), (float)(a.y))));
                nk_draw_list_path_line_to(list, (NkVec2)(b));
                nk_draw_list_path_line_to(list, (NkVec2)(nk_vec2_((float)(a.x), (float)(b.y))));
            }
            else
            {
                nk_draw_list_path_arc_to_fast(list, (NkVec2)(nk_vec2_((float)(a.x + r), (float)(a.y + r))), (float)(r),
                    (int)(6), (int)(9));
                nk_draw_list_path_arc_to_fast(list, (NkVec2)(nk_vec2_((float)(b.x - r), (float)(a.y + r))), (float)(r),
                    (int)(9), (int)(12));
                nk_draw_list_path_arc_to_fast(list, (NkVec2)(nk_vec2_((float)(b.x - r), (float)(b.y - r))), (float)(r),
                    (int)(0), (int)(3));
                nk_draw_list_path_arc_to_fast(list, (NkVec2)(nk_vec2_((float)(a.x + r), (float)(b.y - r))), (float)(r),
                    (int)(3), (int)(6));
            }

        }

        public static void nk_draw_list_path_curve_to(NkDrawList list, NkVec2 p2, NkVec2 p3, NkVec2 p4, uint num_segments)
        {
            float t_step;
            uint i_step;
            NkVec2 p1 = new NkVec2();
            if ((list == null) || (list.Points.Count == 0)) return;
            num_segments = (uint)((num_segments) < (1) ? (1) : (num_segments));
            p1 = (NkVec2)(nk_draw_list_path_last(list));
            t_step = (float)(1.0f / (float)(num_segments));
            for (i_step = (uint)(1); i_step <= num_segments; ++i_step)
            {
                float t = (float)(t_step * (float)(i_step));
                float u = (float)(1.0f - t);
                float w1 = (float)(u * u * u);
                float w2 = (float)(3 * u * u * t);
                float w3 = (float)(3 * u * t * t);
                float w4 = (float)(t * t * t);
                float x = (float)(w1 * p1.x + w2 * p2.x + w3 * p3.x + w4 * p4.x);
                float y = (float)(w1 * p1.y + w2 * p2.y + w3 * p3.y + w4 * p4.y);
                nk_draw_list_path_line_to(list, (NkVec2)(nk_vec2_((float)(x), (float)(y))));
            }
        }

        public static void nk_draw_list_path_fill(NkDrawList list, NkColor color)
        {
            if (list == null) return;
            nk_draw_list_fill_poly_convex(list, (NkColor)(color),
                (list.Config.ShapeAa));
            nk_draw_list_path_clear(list);
        }

        public static void nk_draw_list_path_stroke(NkDrawList list, NkColor color, bool closed, float thickness)
        {
            if (list == null) return;
            nk_draw_list_stroke_poly_line(list, (NkColor)(color), (closed),
                (float)(thickness), (list.Config.LineAa));
            nk_draw_list_path_clear(list);
        }

        public static void nk_draw_list_stroke_line(NkDrawList list, NkVec2 a, NkVec2 b, NkColor col, float thickness)
        {
            if ((list == null) || (col.a == 0)) return;
            if ((list.LineAa))
            {
                nk_draw_list_path_line_to(list, (NkVec2)(a));
                nk_draw_list_path_line_to(list, (NkVec2)(b));
            }
            else
            {
                nk_draw_list_path_line_to(list,
                    (NkVec2)
                        (nk_vec2_((float)((a).x - (nk_vec2_((float)(0.5f), (float)(0.5f))).x),
                            (float)((a).y - (nk_vec2_((float)(0.5f), (float)(0.5f))).y))));
                nk_draw_list_path_line_to(list,
                    (NkVec2)
                        (nk_vec2_((float)((b).x - (nk_vec2_((float)(0.5f), (float)(0.5f))).x),
                            (float)((b).y - (nk_vec2_((float)(0.5f), (float)(0.5f))).y))));
            }

            nk_draw_list_path_stroke(list, (NkColor)(col), false, (float)(thickness));
        }

        public static void nk_draw_list_fill_rect(NkDrawList list, NkRect rect, NkColor col, float rounding)
        {
            if ((list == null) || (col.a == 0)) return;
            if ((list.LineAa))
            {
                nk_draw_list_path_rect_to(list, (NkVec2)(nk_vec2_((float)(rect.x), (float)(rect.y))),
                    (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));
            }
            else
            {
                nk_draw_list_path_rect_to(list, (NkVec2)(nk_vec2_((float)(rect.x - 0.5f), (float)(rect.y - 0.5f))),
                    (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));
            }

            nk_draw_list_path_fill(list, (NkColor)(col));
        }

        public static void nk_draw_list_stroke_rect(NkDrawList list, NkRect rect, NkColor col, float rounding,
            float thickness)
        {
            if ((list == null) || (col.a == 0)) return;
            if ((list.LineAa))
            {
                nk_draw_list_path_rect_to(list, (NkVec2)(nk_vec2_((float)(rect.x), (float)(rect.y))),
                    (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));
            }
            else
            {
                nk_draw_list_path_rect_to(list, (NkVec2)(nk_vec2_((float)(rect.x - 0.5f), (float)(rect.y - 0.5f))),
                    (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (float)(rounding));
            }

            nk_draw_list_path_stroke(list, (NkColor)(col), true, (float)(thickness));
        }

        public static void nk_draw_list_fill_rect_multi_color(NkDrawList list, NkRect rect, NkColor left, NkColor top,
            NkColor right, NkColor bottom)
        {
            NkColorF col_left = new NkColorF();
            NkColorF col_top = new NkColorF();
            NkColorF col_right = new NkColorF();
            NkColorF col_bottom = new NkColorF();
            ushort index;
            nk_color_fv(&col_left.r, (NkColor)(left));
            nk_color_fv(&col_right.r, (NkColor)(right));
            nk_color_fv(&col_top.r, (NkColor)(top));
            nk_color_fv(&col_bottom.r, (NkColor)(bottom));
            if (list == null) return;
            nk_draw_list_push_image(list, (NkHandle)(list.Config.Null.texture));
            index = ((ushort)(list.VertexOffset));
            int vtxStart = list.Vertices.Count;
            list.Vertices.AddToEnd((int)(4 * list.Config.VertexSize));
            int idxStart = list.AddElements(6);

            fixed (byte* vtx2 = list.Vertices.Data)
            {
                void* vtx = (void*)(vtx2 + vtxStart);
                fixed (ushort* ids2 = list.Elements.Data)
                {
                    ushort* idx = ids2 + idxStart;
                    if ((vtx == null) || (idx == null)) return;
                    idx[0] = ((ushort)(index + 0));
                    idx[1] = ((ushort)(index + 1));
                    idx[2] = ((ushort)(index + 2));
                    idx[3] = ((ushort)(index + 0));
                    idx[4] = ((ushort)(index + 2));
                    idx[5] = ((ushort)(index + 3));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(rect.x), (float)(rect.y))),
                        (NkVec2)(list.Config.Null.uv), (NkColorF)(col_left));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y))),
                        (NkVec2)(list.Config.Null.uv), (NkColorF)(col_top));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))),
                        (NkVec2)(list.Config.Null.uv), (NkColorF)(col_right));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(nk_vec2_((float)(rect.x), (float)(rect.y + rect.h))),
                        (NkVec2)(list.Config.Null.uv), (NkColorF)(col_bottom));
                }
            }
        }

        public static void nk_draw_list_fill_triangle(NkDrawList list, NkVec2 a, NkVec2 b, NkVec2 c, NkColor col)
        {
            if ((list == null) || (col.a == 0)) return;
            nk_draw_list_path_line_to(list, (NkVec2)(a));
            nk_draw_list_path_line_to(list, (NkVec2)(b));
            nk_draw_list_path_line_to(list, (NkVec2)(c));
            nk_draw_list_path_fill(list, (NkColor)(col));
        }

        public static void nk_draw_list_stroke_triangle(NkDrawList list, NkVec2 a, NkVec2 b, NkVec2 c, NkColor col,
            float thickness)
        {
            if ((list == null) || (col.a == 0)) return;
            nk_draw_list_path_line_to(list, (NkVec2)(a));
            nk_draw_list_path_line_to(list, (NkVec2)(b));
            nk_draw_list_path_line_to(list, (NkVec2)(c));
            nk_draw_list_path_stroke(list, (NkColor)(col), true, (float)(thickness));
        }

        public static void nk_draw_list_fill_circle(NkDrawList list, NkVec2 center, float radius, NkColor col, uint segs)
        {
            float a_max;
            if ((list == null) || (col.a == 0)) return;
            a_max = (float)(3.141592654f * 2.0f * ((float)(segs) - 1.0f) / (float)(segs));
            nk_draw_list_path_arc_to(list, (NkVec2)(center), (float)(radius), (float)(0.0f), (float)(a_max), (uint)(segs));
            nk_draw_list_path_fill(list, (NkColor)(col));
        }

        public static void nk_draw_list_stroke_circle(NkDrawList list, NkVec2 center, float radius, NkColor col, uint segs,
            float thickness)
        {
            float a_max;
            if ((list == null) || (col.a == 0)) return;
            a_max = (float)(3.141592654f * 2.0f * ((float)(segs) - 1.0f) / (float)(segs));
            nk_draw_list_path_arc_to(list, (NkVec2)(center), (float)(radius), (float)(0.0f), (float)(a_max), (uint)(segs));
            nk_draw_list_path_stroke(list, (NkColor)(col), true, (float)(thickness));
        }

        public static void nk_draw_list_stroke_curve(NkDrawList list, NkVec2 p0, NkVec2 cp0, NkVec2 cp1, NkVec2 p1,
            NkColor col, uint segments, float thickness)
        {
            if ((list == null) || (col.a == 0)) return;
            nk_draw_list_path_line_to(list, (NkVec2)(p0));
            nk_draw_list_path_curve_to(list, (NkVec2)(cp0), (NkVec2)(cp1), (NkVec2)(p1), (uint)(segments));
            nk_draw_list_path_stroke(list, (NkColor)(col), false, (float)(thickness));
        }

        public static void nk_draw_list_push_rect_uv(NkDrawList list, NkVec2 a, NkVec2 c, NkVec2 uva, NkVec2 uvc,
            NkColor color)
        {
            NkVec2 uvb = new NkVec2();
            NkVec2 uvd = new NkVec2();
            NkVec2 b = new NkVec2();
            NkVec2 d = new NkVec2();
            NkColorF col = new NkColorF();
            ushort index;
            if (list == null) return;
            nk_color_fv(&col.r, (NkColor)(color));
            uvb = (NkVec2)(nk_vec2_((float)(uvc.x), (float)(uva.y)));
            uvd = (NkVec2)(nk_vec2_((float)(uva.x), (float)(uvc.y)));
            b = (NkVec2)(nk_vec2_((float)(c.x), (float)(a.y)));
            d = (NkVec2)(nk_vec2_((float)(a.x), (float)(c.y)));
            index = ((ushort)(list.VertexOffset));
            int vtxStart = list.Vertices.Count;
            list.Vertices.AddToEnd((int)(4 * list.Config.VertexSize));
            int idxStart = list.AddElements(6);

            fixed (byte* vtx2 = list.Vertices.Data)
            {
                void* vtx = (void*)(vtx2 + vtxStart);
                fixed (ushort* ids2 = list.Elements.Data)
                {
                    ushort* idx = ids2 + idxStart;
                    if ((vtx == null) || (idx == null)) return;
                    idx[0] = ((ushort)(index + 0));
                    idx[1] = ((ushort)(index + 1));
                    idx[2] = ((ushort)(index + 2));
                    idx[3] = ((ushort)(index + 0));
                    idx[4] = ((ushort)(index + 2));
                    idx[5] = ((ushort)(index + 3));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(a), (NkVec2)(uva), (NkColorF)(col));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(b), (NkVec2)(uvb), (NkColorF)(col));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(c), (NkVec2)(uvc), (NkColorF)(col));
                    vtx = nk_draw_vertex(vtx, list.Config, (NkVec2)(d), (NkVec2)(uvd), (NkColorF)(col));
                }
            }
        }

        public static void nk_draw_list_add_image(NkDrawList list, NkImage texture, NkRect rect, NkColor color)
        {
            if (list == null) return;
            nk_draw_list_push_image(list, (NkHandle)(texture.handle));
            if ((nk_image_is_subimage(texture)))
            {
                NkVec2* uv = stackalloc NkVec2[2];
                uv[0].x = (float)((float)(texture.region[0]) / (float)(texture.w));
                uv[0].y = (float)((float)(texture.region[1]) / (float)(texture.h));
                uv[1].x = (float)((float)(texture.region[0] + texture.region[2]) / (float)(texture.w));
                uv[1].y = (float)((float)(texture.region[1] + texture.region[3]) / (float)(texture.h));
                nk_draw_list_push_rect_uv(list, (NkVec2)(nk_vec2_((float)(rect.x), (float)(rect.y))),
                    (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))), (NkVec2)(uv[0]), (NkVec2)(uv[1]),
                    (NkColor)(color));
            }
            else
                nk_draw_list_push_rect_uv(list, (NkVec2)(nk_vec2_((float)(rect.x), (float)(rect.y))),
                    (NkVec2)(nk_vec2_((float)(rect.x + rect.w), (float)(rect.y + rect.h))),
                    (NkVec2)(nk_vec2_((float)(0.0f), (float)(0.0f))), (NkVec2)(nk_vec2_((float)(1.0f), (float)(1.0f))),
                    (NkColor)(color));
        }

        public static void nk_draw_list_add_text(NkDrawList list, NkUserFont font, NkRect rect, char* text, int len,
            float font_height, NkColor fg)
        {
            float x = (float)(0);
            int text_len = (int)(0);
            char unicode = (char)0;
            char next = (char)(0);
            int glyph_len = (int)(0);
            int next_glyph_len = (int)(0);
            NkUserFontGlyph g = new NkUserFontGlyph();
            if (((list == null) || (len == 0)) || (text == null)) return;
            if (
                !(!(((((list.ClipRect.x) > (rect.x + rect.w)) || ((list.ClipRect.x + list.ClipRect.w) < (rect.x))) ||
                     ((list.ClipRect.y) > (rect.y + rect.h))) || ((list.ClipRect.y + list.ClipRect.h) < (rect.y))))) return;
            nk_draw_list_push_image(list, (NkHandle)(font.Texture));
            x = (float)(rect.x);
            glyph_len = (int)(nk_utf_decode(text, &unicode, (int)(len)));
            if (glyph_len == 0) return;
            fg.a = ((byte)((float)(fg.a) * list.Config.GlobalAlpha));
            while (((text_len) < (len)) && ((glyph_len) != 0))
            {
                float gx;
                float gy;
                float gh;
                float gw;
                float char_width = (float)(0);
                if ((unicode) == (0xFFFD)) break;
                next_glyph_len = (int)(nk_utf_decode(text + text_len + glyph_len, &next, (int)(len - text_len)));
                font.Query((NkHandle)(font.Userdata), (float)(font_height), &g, unicode, (next == 0xFFFD) ? '\0' : next);
                gx = (float)(x + g.offset.x);
                gy = (float)(rect.y + g.offset.y);
                gw = (float)(g.width);
                gh = (float)(g.height);
                char_width = (float)(g.xadvance);
                nk_draw_list_push_rect_uv(list, (NkVec2)(nk_vec2_((float)(gx), (float)(gy))),
                    (NkVec2)(nk_vec2_((float)(gx + gw), (float)(gy + gh))), nk_vec2_(g.uv_x[0], g.uv_y[0]),
                    nk_vec2_(g.uv_x[1], g.uv_y[1]), (NkColor)(fg));
                text_len += (int)(glyph_len);
                x += (float)(char_width);
                glyph_len = (int)(next_glyph_len);
                unicode = (char)(next);
            }
        }
    }
}