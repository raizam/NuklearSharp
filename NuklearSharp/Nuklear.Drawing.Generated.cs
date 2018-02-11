using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe static partial class Nuklear
	{
		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_draw_null_texture
		{
			public nk_handle texture;
			public nk_vec2 uv;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_draw_vertex_layout_element
		{
			public int attribute;
			public int format;
			public ulong offset;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe partial struct nk_draw_command
		{
			public uint elem_count;
			public nk_rect clip_rect;
			public nk_handle texture;
		}

		public static void nk_draw_list_init(nk_draw_list list)
		{
			ulong i = (ulong) (0);
			if (list == null) return;

			for (i = (ulong) (0); (i) < (ulong) list.circle_vtx.Length; ++i)
			{
				float a = (float) (((float) (i)/(float) (ulong) list.circle_vtx.Length)*2*3.141592654f);
				list.circle_vtx[i].x = (float) (nk_cos((float) (a)));
				list.circle_vtx[i].y = (float) (nk_sin((float) (a)));
			}
		}

		public static void nk_draw_list_setup(nk_draw_list canvas, nk_convert_config config, nk_buffer cmds,
			nk_buffer vertices, nk_buffer elements, int line_aa, int shape_aa)
		{
			if (((((canvas == null) || (config == null)) || (cmds == null)) || (vertices == null)) || (elements == null)) return;
			canvas.buffer = cmds;
			canvas.config = (nk_convert_config) (config);
			canvas.elements = elements;
			canvas.vertices = vertices;
			canvas.line_AA = (int) (line_aa);
			canvas.shape_AA = (int) (shape_aa);
			canvas.clip_rect = (nk_rect) (nk_null_rect);
		}

		public static nk_draw_command* nk__draw_list_begin(nk_draw_list canvas, nk_buffer buffer)
		{
			byte* memory;
			ulong offset;
			nk_draw_command* cmd;
			if (((buffer == null) || (buffer.size == 0)) || (canvas.cmd_count == 0)) return null;
			memory = (byte*) (buffer.memory.ptr);
			offset = (ulong) (buffer.memory.size - canvas.cmd_offset);
			cmd = ((nk_draw_command*) ((void*) ((memory) + (offset))));
			return cmd;
		}

		public static nk_draw_command* nk__draw_list_end(nk_draw_list canvas, nk_buffer buffer)
		{
			ulong size;
			ulong offset;
			byte* memory;
			nk_draw_command* end;
			if ((buffer == null) || (canvas == null)) return null;
			memory = (byte*) (buffer.memory.ptr);
			size = (ulong) (buffer.memory.size);
			offset = (ulong) (size - canvas.cmd_offset);
			end = ((nk_draw_command*) ((void*) ((memory) + (offset))));
			end -= (canvas.cmd_count - 1);
			return end;
		}

		public static nk_draw_command* nk__draw_list_next(nk_draw_command* cmd, nk_buffer buffer, nk_draw_list canvas)
		{
			nk_draw_command* end;
			if (((cmd == null) || (buffer == null)) || (canvas == null)) return null;
			end = nk__draw_list_end(canvas, buffer);
			if (cmd <= end) return null;
			return (cmd - 1);
		}

		public static void nk_draw_list_clear(nk_draw_list list)
		{
			if (list == null) return;
			if ((list.buffer) != null) nk_buffer_clear(list.buffer);
			if ((list.vertices) != null) nk_buffer_clear(list.vertices);
			if ((list.elements) != null) nk_buffer_clear(list.elements);
			list.element_count = (uint) (0);
			list.vertex_count = (uint) (0);
			list.cmd_offset = (ulong) (0);
			list.cmd_count = (uint) (0);
			list.path_count = (uint) (0);
			list.vertices = null;
			list.elements = null;
			list.clip_rect = (nk_rect) (nk_null_rect);
		}

		public static nk_vec2* nk_draw_list_alloc_path(nk_draw_list list, int count)
		{
			nk_vec2* points;
			ulong point_align = (ulong) (4);
			ulong point_size = (ulong) (sizeof (nk_vec2));
			points =
				(nk_vec2*)
					(nk_buffer_alloc(list.buffer, (int) (NK_BUFFER_FRONT), (ulong) (point_size*(ulong) (count)), (ulong) (point_align)));
			if (points == null) return null;
			if (list.path_offset == 0)
			{
				void* memory = nk_buffer_memory(list.buffer);
				list.path_offset = ((uint) ((byte*) (points) - (byte*) (memory)));
			}

			list.path_count += ((uint) (count));
			return points;
		}

		public static nk_vec2 nk_draw_list_path_last(nk_draw_list list)
		{
			void* memory;
			nk_vec2* point;
			memory = nk_buffer_memory(list.buffer);
			point = ((nk_vec2*) ((void*) ((byte*) (memory) + (list.path_offset))));
			point += (list.path_count - 1);
			return (nk_vec2) (*point);
		}

		public static nk_draw_command* nk_draw_list_push_command(nk_draw_list list, nk_rect clip, nk_handle texture)
		{
			ulong cmd_align = (ulong) (4);
			ulong cmd_size = (ulong) (sizeof (nk_draw_command));
			nk_draw_command* cmd;
			cmd =
				(nk_draw_command*) (nk_buffer_alloc(list.buffer, (int) (NK_BUFFER_BACK), (ulong) (cmd_size), (ulong) (cmd_align)));
			if (cmd == null) return null;
			if (list.cmd_count == 0)
			{
				byte* memory = (byte*) (nk_buffer_memory(list.buffer));
				ulong total = (ulong) (nk_buffer_total(list.buffer));
				memory = ((byte*) ((void*) ((memory) + (total))));
				list.cmd_offset = ((ulong) (memory - (byte*) (cmd)));
			}

			cmd->elem_count = (uint) (0);
			cmd->clip_rect = (nk_rect) (clip);
			cmd->texture = (nk_handle) (texture);
			list.cmd_count++;
			list.clip_rect = (nk_rect) (clip);
			return cmd;
		}

		public static nk_draw_command* nk_draw_list_command_last(nk_draw_list list)
		{
			void* memory;
			ulong size;
			nk_draw_command* cmd;
			memory = nk_buffer_memory(list.buffer);
			size = (ulong) (nk_buffer_total(list.buffer));
			cmd = ((nk_draw_command*) ((void*) ((byte*) (memory) + (size - list.cmd_offset))));
			return (cmd - (list.cmd_count - 1));
		}

		public static void nk_draw_list_add_clip(nk_draw_list list, nk_rect rect)
		{
			if (list == null) return;
			if (list.cmd_count == 0)
			{
				nk_draw_list_push_command(list, (nk_rect) (rect), (nk_handle) (list.config._null_.texture));
			}
			else
			{
				nk_draw_command* prev = nk_draw_list_command_last(list);
				if ((prev->elem_count) == (0)) prev->clip_rect = (nk_rect) (rect);
				nk_draw_list_push_command(list, (nk_rect) (rect), (nk_handle) (prev->texture));
			}

		}

		public static void nk_draw_list_push_image(nk_draw_list list, nk_handle texture)
		{
			if (list == null) return;
			if (list.cmd_count == 0)
			{
				nk_draw_list_push_command(list, (nk_rect) (nk_null_rect), (nk_handle) (texture));
			}
			else
			{
				nk_draw_command* prev = nk_draw_list_command_last(list);
				if ((prev->elem_count) == (0))
				{
					prev->texture = (nk_handle) (texture);
				}
				else if (prev->texture.id != texture.id)
					nk_draw_list_push_command(list, (nk_rect) (prev->clip_rect), (nk_handle) (texture));
			}

		}

		public static void* nk_draw_list_alloc_vertices(nk_draw_list list, ulong count)
		{
			void* vtx;
			if (list == null) return null;
			vtx = nk_buffer_alloc(list.vertices, (int) (NK_BUFFER_FRONT), (ulong) (list.config.vertex_size*count),
				(ulong) (list.config.vertex_alignment));
			if (vtx == null) return null;
			list.vertex_count += ((uint) (count));
			return vtx;
		}

		public static ushort* nk_draw_list_alloc_elements(nk_draw_list list, ulong count)
		{
			ushort* ids;
			nk_draw_command* cmd;
			ulong elem_align = (ulong) (2);
			ulong elem_size = (ulong) (sizeof (ushort));
			if (list == null) return null;
			ids =
				(ushort*) (nk_buffer_alloc(list.elements, (int) (NK_BUFFER_FRONT), (ulong) (elem_size*count), (ulong) (elem_align)));
			if (ids == null) return null;
			cmd = nk_draw_list_command_last(list);
			list.element_count += ((uint) (count));
			cmd->elem_count += ((uint) (count));
			return ids;
		}

		public static int nk_draw_vertex_layout_element_is_end_of_layout(nk_draw_vertex_layout_element* element)
		{
			return
				(int) (((element->attribute) == (NK_VERTEX_ATTRIBUTE_COUNT)) || ((element->format) == (NK_FORMAT_COUNT)) ? 1 : 0);
		}

		public static void nk_draw_list_stroke_poly_line(nk_draw_list list, nk_vec2* points, uint points_count, nk_color color,
			int closed, float thickness, int aliasing)
		{
			ulong count;
			int thick_line;
			nk_colorf col = new nk_colorf();
			nk_colorf col_trans = new nk_colorf();
			if ((list == null) || ((points_count) < (2))) return;
			color.a = ((byte) ((float) (color.a)*list.config.global_alpha));
			count = (ulong) (points_count);
			if (closed == 0) count = (ulong) (points_count - 1);
			thick_line = (int) ((thickness) > (1.0f) ? 1 : 0);
			color.a = ((byte) ((float) (color.a)*list.config.global_alpha));
			nk_color_fv(&col.r, (nk_color) (color));
			col_trans = (nk_colorf) (col);
			col_trans.a = (float) (0);
			if ((aliasing) == (NK_ANTI_ALIASING_ON))
			{
				float AA_SIZE = (float) (1.0f);
				ulong pnt_align = (ulong) (4);
				ulong pnt_size = (ulong) (sizeof (nk_vec2));
				ulong i1 = (ulong) (0);
				ulong vertex_offset;
				ulong index = (ulong) (list.vertex_count);
				ulong idx_count = (ulong) ((thick_line) != 0 ? (count*18) : (count*12));
				ulong vtx_count = (ulong) ((thick_line) != 0 ? (points_count*4) : (points_count*3));
				void* vtx = nk_draw_list_alloc_vertices(list, (ulong) (vtx_count));
				ushort* ids = nk_draw_list_alloc_elements(list, (ulong) (idx_count));
				ulong size;
				nk_vec2* normals;
				nk_vec2* temp;
				if ((vtx == null) || (ids == null)) return;
				vertex_offset = ((ulong) ((byte*) (vtx) - (byte*) (list.vertices.memory.ptr)));
				nk_buffer_mark(list.vertices, (int) (NK_BUFFER_FRONT));
				size = (ulong) (pnt_size*(ulong) ((thick_line) != 0 ? 5 : 3)*points_count);
				normals = (nk_vec2*) (nk_buffer_alloc(list.vertices, (int) (NK_BUFFER_FRONT), (ulong) (size), (ulong) (pnt_align)));
				if (normals == null) return;
				temp = normals + points_count;
				vtx = (void*) ((byte*) (list.vertices.memory.ptr) + vertex_offset);
				for (i1 = (ulong) (0); (i1) < (count); ++i1)
				{
					ulong i2 = (ulong) (((i1 + 1) == (points_count)) ? 0 : (i1 + 1));
					nk_vec2 diff =
						(nk_vec2) (nk_vec2_((float) ((points[i2]).x - (points[i1]).x), (float) ((points[i2]).y - (points[i1]).y)));
					float len;
					len = (float) ((diff).x*(diff).x + (diff).y*(diff).y);
					if (len != 0.0f) len = (float) (nk_inv_sqrt((float) (len)));
					else len = (float) (1.0f);
					diff = (nk_vec2) (nk_vec2_((float) ((diff).x*(len)), (float) ((diff).y*(len))));
					normals[i1].x = (float) (diff.y);
					normals[i1].y = (float) (-diff.x);
				}
				if (closed == 0) normals[points_count - 1] = (nk_vec2) (normals[points_count - 2]);
				if (thick_line == 0)
				{
					ulong idx1;
					ulong i;
					if (closed == 0)
					{
						nk_vec2 d = new nk_vec2();
						temp[0] =
							(nk_vec2)
								(nk_vec2_(
									(float) ((points[0]).x + (nk_vec2_((float) ((normals[0]).x*(AA_SIZE)), (float) ((normals[0]).y*(AA_SIZE)))).x),
									(float) ((points[0]).y + (nk_vec2_((float) ((normals[0]).x*(AA_SIZE)), (float) ((normals[0]).y*(AA_SIZE)))).y)));
						temp[1] =
							(nk_vec2)
								(nk_vec2_(
									(float) ((points[0]).x - (nk_vec2_((float) ((normals[0]).x*(AA_SIZE)), (float) ((normals[0]).y*(AA_SIZE)))).x),
									(float) ((points[0]).y - (nk_vec2_((float) ((normals[0]).x*(AA_SIZE)), (float) ((normals[0]).y*(AA_SIZE)))).y)));
						d =
							(nk_vec2)
								(nk_vec2_((float) ((normals[points_count - 1]).x*(AA_SIZE)), (float) ((normals[points_count - 1]).y*(AA_SIZE))));
						temp[(points_count - 1)*2 + 0] =
							(nk_vec2)
								(nk_vec2_((float) ((points[points_count - 1]).x + (d).x), (float) ((points[points_count - 1]).y + (d).y)));
						temp[(points_count - 1)*2 + 1] =
							(nk_vec2)
								(nk_vec2_((float) ((points[points_count - 1]).x - (d).x), (float) ((points[points_count - 1]).y - (d).y)));
					}
					idx1 = (ulong) (index);
					for (i1 = (ulong) (0); (i1) < (count); i1++)
					{
						nk_vec2 dm = new nk_vec2();
						float dmr2;
						ulong i2 = (ulong) (((i1 + 1) == (points_count)) ? 0 : (i1 + 1));
						ulong idx2 = (ulong) (((i1 + 1) == (points_count)) ? index : (idx1 + 3));
						dm =
							(nk_vec2)
								(nk_vec2_(
									(float)
										((nk_vec2_((float) ((normals[i1]).x + (normals[i2]).x), (float) ((normals[i1]).y + (normals[i2]).y))).x*(0.5f)),
									(float)
										((nk_vec2_((float) ((normals[i1]).x + (normals[i2]).x), (float) ((normals[i1]).y + (normals[i2]).y))).y*(0.5f))));
						dmr2 = (float) (dm.x*dm.x + dm.y*dm.y);
						if ((dmr2) > (0.000001f))
						{
							float scale = (float) (1.0f/dmr2);
							scale = (float) ((100.0f) < (scale) ? (100.0f) : (scale));
							dm = (nk_vec2) (nk_vec2_((float) ((dm).x*(scale)), (float) ((dm).y*(scale))));
						}
						dm = (nk_vec2) (nk_vec2_((float) ((dm).x*(AA_SIZE)), (float) ((dm).y*(AA_SIZE))));
						temp[i2*2 + 0] = (nk_vec2) (nk_vec2_((float) ((points[i2]).x + (dm).x), (float) ((points[i2]).y + (dm).y)));
						temp[i2*2 + 1] = (nk_vec2) (nk_vec2_((float) ((points[i2]).x - (dm).x), (float) ((points[i2]).y - (dm).y)));
						ids[0] = ((ushort) (idx2 + 0));
						ids[1] = ((ushort) (idx1 + 0));
						ids[2] = ((ushort) (idx1 + 2));
						ids[3] = ((ushort) (idx1 + 2));
						ids[4] = ((ushort) (idx2 + 2));
						ids[5] = ((ushort) (idx2 + 0));
						ids[6] = ((ushort) (idx2 + 1));
						ids[7] = ((ushort) (idx1 + 1));
						ids[8] = ((ushort) (idx1 + 0));
						ids[9] = ((ushort) (idx1 + 0));
						ids[10] = ((ushort) (idx2 + 0));
						ids[11] = ((ushort) (idx2 + 1));
						ids += 12;
						idx1 = (ulong) (idx2);
					}
					for (i = (ulong) (0); (i) < (points_count); ++i)
					{
						nk_vec2 uv = (nk_vec2) (list.config._null_.uv);
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (points[i]), (nk_vec2) (uv), (nk_colorf) (col));
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (temp[i*2 + 0]), (nk_vec2) (uv), (nk_colorf) (col_trans));
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (temp[i*2 + 1]), (nk_vec2) (uv), (nk_colorf) (col_trans));
					}
				}
				else
				{
					ulong idx1;
					ulong i;
					float half_inner_thickness = (float) ((thickness - AA_SIZE)*0.5f);
					if (closed == 0)
					{
						nk_vec2 d1 =
							(nk_vec2)
								(nk_vec2_((float) ((normals[0]).x*(half_inner_thickness + AA_SIZE)),
									(float) ((normals[0]).y*(half_inner_thickness + AA_SIZE))));
						nk_vec2 d2 =
							(nk_vec2)
								(nk_vec2_((float) ((normals[0]).x*(half_inner_thickness)), (float) ((normals[0]).y*(half_inner_thickness))));
						temp[0] = (nk_vec2) (nk_vec2_((float) ((points[0]).x + (d1).x), (float) ((points[0]).y + (d1).y)));
						temp[1] = (nk_vec2) (nk_vec2_((float) ((points[0]).x + (d2).x), (float) ((points[0]).y + (d2).y)));
						temp[2] = (nk_vec2) (nk_vec2_((float) ((points[0]).x - (d2).x), (float) ((points[0]).y - (d2).y)));
						temp[3] = (nk_vec2) (nk_vec2_((float) ((points[0]).x - (d1).x), (float) ((points[0]).y - (d1).y)));
						d1 =
							(nk_vec2)
								(nk_vec2_((float) ((normals[points_count - 1]).x*(half_inner_thickness + AA_SIZE)),
									(float) ((normals[points_count - 1]).y*(half_inner_thickness + AA_SIZE))));
						d2 =
							(nk_vec2)
								(nk_vec2_((float) ((normals[points_count - 1]).x*(half_inner_thickness)),
									(float) ((normals[points_count - 1]).y*(half_inner_thickness))));
						temp[(points_count - 1)*4 + 0] =
							(nk_vec2)
								(nk_vec2_((float) ((points[points_count - 1]).x + (d1).x), (float) ((points[points_count - 1]).y + (d1).y)));
						temp[(points_count - 1)*4 + 1] =
							(nk_vec2)
								(nk_vec2_((float) ((points[points_count - 1]).x + (d2).x), (float) ((points[points_count - 1]).y + (d2).y)));
						temp[(points_count - 1)*4 + 2] =
							(nk_vec2)
								(nk_vec2_((float) ((points[points_count - 1]).x - (d2).x), (float) ((points[points_count - 1]).y - (d2).y)));
						temp[(points_count - 1)*4 + 3] =
							(nk_vec2)
								(nk_vec2_((float) ((points[points_count - 1]).x - (d1).x), (float) ((points[points_count - 1]).y - (d1).y)));
					}
					idx1 = (ulong) (index);
					for (i1 = (ulong) (0); (i1) < (count); ++i1)
					{
						nk_vec2 dm_out = new nk_vec2();
						nk_vec2 dm_in = new nk_vec2();
						ulong i2 = (ulong) (((i1 + 1) == (points_count)) ? 0 : (i1 + 1));
						ulong idx2 = (ulong) (((i1 + 1) == (points_count)) ? index : (idx1 + 4));
						nk_vec2 dm =
							(nk_vec2)
								(nk_vec2_(
									(float)
										((nk_vec2_((float) ((normals[i1]).x + (normals[i2]).x), (float) ((normals[i1]).y + (normals[i2]).y))).x*(0.5f)),
									(float)
										((nk_vec2_((float) ((normals[i1]).x + (normals[i2]).x), (float) ((normals[i1]).y + (normals[i2]).y))).y*(0.5f))));
						float dmr2 = (float) (dm.x*dm.x + dm.y*dm.y);
						if ((dmr2) > (0.000001f))
						{
							float scale = (float) (1.0f/dmr2);
							scale = (float) ((100.0f) < (scale) ? (100.0f) : (scale));
							dm = (nk_vec2) (nk_vec2_((float) ((dm).x*(scale)), (float) ((dm).y*(scale))));
						}
						dm_out =
							(nk_vec2)
								(nk_vec2_((float) ((dm).x*((half_inner_thickness) + AA_SIZE)),
									(float) ((dm).y*((half_inner_thickness) + AA_SIZE))));
						dm_in = (nk_vec2) (nk_vec2_((float) ((dm).x*(half_inner_thickness)), (float) ((dm).y*(half_inner_thickness))));
						temp[i2*4 + 0] =
							(nk_vec2) (nk_vec2_((float) ((points[i2]).x + (dm_out).x), (float) ((points[i2]).y + (dm_out).y)));
						temp[i2*4 + 1] = (nk_vec2) (nk_vec2_((float) ((points[i2]).x + (dm_in).x), (float) ((points[i2]).y + (dm_in).y)));
						temp[i2*4 + 2] = (nk_vec2) (nk_vec2_((float) ((points[i2]).x - (dm_in).x), (float) ((points[i2]).y - (dm_in).y)));
						temp[i2*4 + 3] =
							(nk_vec2) (nk_vec2_((float) ((points[i2]).x - (dm_out).x), (float) ((points[i2]).y - (dm_out).y)));
						ids[0] = ((ushort) (idx2 + 1));
						ids[1] = ((ushort) (idx1 + 1));
						ids[2] = ((ushort) (idx1 + 2));
						ids[3] = ((ushort) (idx1 + 2));
						ids[4] = ((ushort) (idx2 + 2));
						ids[5] = ((ushort) (idx2 + 1));
						ids[6] = ((ushort) (idx2 + 1));
						ids[7] = ((ushort) (idx1 + 1));
						ids[8] = ((ushort) (idx1 + 0));
						ids[9] = ((ushort) (idx1 + 0));
						ids[10] = ((ushort) (idx2 + 0));
						ids[11] = ((ushort) (idx2 + 1));
						ids[12] = ((ushort) (idx2 + 2));
						ids[13] = ((ushort) (idx1 + 2));
						ids[14] = ((ushort) (idx1 + 3));
						ids[15] = ((ushort) (idx1 + 3));
						ids[16] = ((ushort) (idx2 + 3));
						ids[17] = ((ushort) (idx2 + 2));
						ids += 18;
						idx1 = (ulong) (idx2);
					}
					for (i = (ulong) (0); (i) < (points_count); ++i)
					{
						nk_vec2 uv = (nk_vec2) (list.config._null_.uv);
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (temp[i*4 + 0]), (nk_vec2) (uv), (nk_colorf) (col_trans));
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (temp[i*4 + 1]), (nk_vec2) (uv), (nk_colorf) (col));
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (temp[i*4 + 2]), (nk_vec2) (uv), (nk_colorf) (col));
						vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (temp[i*4 + 3]), (nk_vec2) (uv), (nk_colorf) (col_trans));
					}
				}
				nk_buffer_reset(list.vertices, (int) (NK_BUFFER_FRONT));
			}
			else
			{
				ulong i1 = (ulong) (0);
				ulong idx = (ulong) (list.vertex_count);
				ulong idx_count = (ulong) (count*6);
				ulong vtx_count = (ulong) (count*4);
				void* vtx = nk_draw_list_alloc_vertices(list, (ulong) (vtx_count));
				ushort* ids = nk_draw_list_alloc_elements(list, (ulong) (idx_count));
				if ((vtx == null) || (ids == null)) return;
				for (i1 = (ulong) (0); (i1) < (count); ++i1)
				{
					float dx;
					float dy;
					nk_vec2 uv = (nk_vec2) (list.config._null_.uv);
					ulong i2 = (ulong) (((i1 + 1) == (points_count)) ? 0 : i1 + 1);
					nk_vec2 p1 = (nk_vec2) (points[i1]);
					nk_vec2 p2 = (nk_vec2) (points[i2]);
					nk_vec2 diff = (nk_vec2) (nk_vec2_((float) ((p2).x - (p1).x), (float) ((p2).y - (p1).y)));
					float len;
					len = (float) ((diff).x*(diff).x + (diff).y*(diff).y);
					if (len != 0.0f) len = (float) (nk_inv_sqrt((float) (len)));
					else len = (float) (1.0f);
					diff = (nk_vec2) (nk_vec2_((float) ((diff).x*(len)), (float) ((diff).y*(len))));
					dx = (float) (diff.x*(thickness*0.5f));
					dy = (float) (diff.y*(thickness*0.5f));
					vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (p1.x + dy), (float) (p1.y - dx))),
						(nk_vec2) (uv), (nk_colorf) (col));
					vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (p2.x + dy), (float) (p2.y - dx))),
						(nk_vec2) (uv), (nk_colorf) (col));
					vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (p2.x - dy), (float) (p2.y + dx))),
						(nk_vec2) (uv), (nk_colorf) (col));
					vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (p1.x - dy), (float) (p1.y + dx))),
						(nk_vec2) (uv), (nk_colorf) (col));
					ids[0] = ((ushort) (idx + 0));
					ids[1] = ((ushort) (idx + 1));
					ids[2] = ((ushort) (idx + 2));
					ids[3] = ((ushort) (idx + 0));
					ids[4] = ((ushort) (idx + 2));
					ids[5] = ((ushort) (idx + 3));
					ids += 6;
					idx += (ulong) (4);
				}
			}

		}

		public static void nk_draw_list_fill_poly_convex(nk_draw_list list, nk_vec2* points, uint points_count, nk_color color,
			int aliasing)
		{
			nk_colorf col = new nk_colorf();
			nk_colorf col_trans = new nk_colorf();
			ulong pnt_align = (ulong) (4);
			ulong pnt_size = (ulong) (sizeof (nk_vec2));
			if ((list == null) || ((points_count) < (3))) return;
			color.a = ((byte) ((float) (color.a)*list.config.global_alpha));
			nk_color_fv(&col.r, (nk_color) (color));
			col_trans = (nk_colorf) (col);
			col_trans.a = (float) (0);
			if ((aliasing) == (NK_ANTI_ALIASING_ON))
			{
				ulong i = (ulong) (0);
				ulong i0 = (ulong) (0);
				ulong i1 = (ulong) (0);
				float AA_SIZE = (float) (1.0f);
				ulong vertex_offset = (ulong) (0);
				ulong index = (ulong) (list.vertex_count);
				ulong idx_count = (ulong) ((points_count - 2)*3 + points_count*6);
				ulong vtx_count = (ulong) (points_count*2);
				void* vtx = nk_draw_list_alloc_vertices(list, (ulong) (vtx_count));
				ushort* ids = nk_draw_list_alloc_elements(list, (ulong) (idx_count));
				ulong size = (ulong) (0);
				nk_vec2* normals = null;
				uint vtx_inner_idx = (uint) (index + 0);
				uint vtx_outer_idx = (uint) (index + 1);
				if ((vtx == null) || (ids == null)) return;
				vertex_offset = ((ulong) ((byte*) (vtx) - (byte*) (list.vertices.memory.ptr)));
				nk_buffer_mark(list.vertices, (int) (NK_BUFFER_FRONT));
				size = (ulong) (pnt_size*points_count);
				normals = (nk_vec2*) (nk_buffer_alloc(list.vertices, (int) (NK_BUFFER_FRONT), (ulong) (size), (ulong) (pnt_align)));
				if (normals == null) return;
				vtx = (void*) ((byte*) (list.vertices.memory.ptr) + vertex_offset);
				for (i = (ulong) (2); (i) < (points_count); i++)
				{
					ids[0] = ((ushort) (vtx_inner_idx));
					ids[1] = ((ushort) (vtx_inner_idx + ((i - 1) << 1)));
					ids[2] = ((ushort) (vtx_inner_idx + (i << 1)));
					ids += 3;
				}
				for (i0 = (ulong) (points_count - 1) , i1 = (ulong) (0); (i1) < (points_count); i0 = (ulong) (i1++))
				{
					nk_vec2 p0 = (nk_vec2) (points[i0]);
					nk_vec2 p1 = (nk_vec2) (points[i1]);
					nk_vec2 diff = (nk_vec2) (nk_vec2_((float) ((p1).x - (p0).x), (float) ((p1).y - (p0).y)));
					float len = (float) ((diff).x*(diff).x + (diff).y*(diff).y);
					if (len != 0.0f) len = (float) (nk_inv_sqrt((float) (len)));
					else len = (float) (1.0f);
					diff = (nk_vec2) (nk_vec2_((float) ((diff).x*(len)), (float) ((diff).y*(len))));
					normals[i0].x = (float) (diff.y);
					normals[i0].y = (float) (-diff.x);
				}
				for (i0 = (ulong) (points_count - 1) , i1 = (ulong) (0); (i1) < (points_count); i0 = (ulong) (i1++))
				{
					nk_vec2 uv = (nk_vec2) (list.config._null_.uv);
					nk_vec2 n0 = (nk_vec2) (normals[i0]);
					nk_vec2 n1 = (nk_vec2) (normals[i1]);
					nk_vec2 dm =
						(nk_vec2)
							(nk_vec2_((float) ((nk_vec2_((float) ((n0).x + (n1).x), (float) ((n0).y + (n1).y))).x*(0.5f)),
								(float) ((nk_vec2_((float) ((n0).x + (n1).x), (float) ((n0).y + (n1).y))).y*(0.5f))));
					float dmr2 = (float) (dm.x*dm.x + dm.y*dm.y);
					if ((dmr2) > (0.000001f))
					{
						float scale = (float) (1.0f/dmr2);
						scale = (float) ((scale) < (100.0f) ? (scale) : (100.0f));
						dm = (nk_vec2) (nk_vec2_((float) ((dm).x*(scale)), (float) ((dm).y*(scale))));
					}
					dm = (nk_vec2) (nk_vec2_((float) ((dm).x*(AA_SIZE*0.5f)), (float) ((dm).y*(AA_SIZE*0.5f))));
					vtx = nk_draw_vertex(vtx, list.config,
						(nk_vec2) (nk_vec2_((float) ((points[i1]).x - (dm).x), (float) ((points[i1]).y - (dm).y))), (nk_vec2) (uv),
						(nk_colorf) (col));
					vtx = nk_draw_vertex(vtx, list.config,
						(nk_vec2) (nk_vec2_((float) ((points[i1]).x + (dm).x), (float) ((points[i1]).y + (dm).y))), (nk_vec2) (uv),
						(nk_colorf) (col_trans));
					ids[0] = ((ushort) (vtx_inner_idx + (i1 << 1)));
					ids[1] = ((ushort) (vtx_inner_idx + (i0 << 1)));
					ids[2] = ((ushort) (vtx_outer_idx + (i0 << 1)));
					ids[3] = ((ushort) (vtx_outer_idx + (i0 << 1)));
					ids[4] = ((ushort) (vtx_outer_idx + (i1 << 1)));
					ids[5] = ((ushort) (vtx_inner_idx + (i1 << 1)));
					ids += 6;
				}
				nk_buffer_reset(list.vertices, (int) (NK_BUFFER_FRONT));
			}
			else
			{
				ulong i = (ulong) (0);
				ulong index = (ulong) (list.vertex_count);
				ulong idx_count = (ulong) ((points_count - 2)*3);
				ulong vtx_count = (ulong) (points_count);
				void* vtx = nk_draw_list_alloc_vertices(list, (ulong) (vtx_count));
				ushort* ids = nk_draw_list_alloc_elements(list, (ulong) (idx_count));
				if ((vtx == null) || (ids == null)) return;
				for (i = (ulong) (0); (i) < (vtx_count); ++i)
				{
					vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (points[i]), (nk_vec2) (list.config._null_.uv), (nk_colorf) (col));
				}
				for (i = (ulong) (2); (i) < (points_count); ++i)
				{
					ids[0] = ((ushort) (index));
					ids[1] = ((ushort) (index + i - 1));
					ids[2] = ((ushort) (index + i));
					ids += 3;
				}
			}

		}

		public static void nk_draw_list_path_clear(nk_draw_list list)
		{
			if (list == null) return;
			nk_buffer_reset(list.buffer, (int) (NK_BUFFER_FRONT));
			list.path_count = (uint) (0);
			list.path_offset = (uint) (0);
		}

		public static void nk_draw_list_path_line_to(nk_draw_list list, nk_vec2 pos)
		{
			nk_vec2* points = null;
			nk_draw_command* cmd = null;
			if (list == null) return;
			if (list.cmd_count == 0) nk_draw_list_add_clip(list, (nk_rect) (nk_null_rect));
			cmd = nk_draw_list_command_last(list);
			if (((cmd) != null) && (cmd->texture.ptr != list.config._null_.texture.ptr))
				nk_draw_list_push_image(list, (nk_handle) (list.config._null_.texture));
			points = nk_draw_list_alloc_path(list, (int) (1));
			if (points == null) return;
			points[0] = (nk_vec2) (pos);
		}

		public static void nk_draw_list_path_arc_to_fast(nk_draw_list list, nk_vec2 center, float radius, int a_min, int a_max)
		{
			int a = (int) (0);
			if (list == null) return;
			if (a_min <= a_max)
			{
				for (a = (int) (a_min); a <= a_max; a++)
				{
					nk_vec2 c = (nk_vec2) (list.circle_vtx[(ulong) (a)%(ulong) list.circle_vtx.Length]);
					float x = (float) (center.x + c.x*radius);
					float y = (float) (center.y + c.y*radius);
					nk_draw_list_path_line_to(list, (nk_vec2) (nk_vec2_((float) (x), (float) (y))));
				}
			}

		}

		public static void nk_draw_list_path_arc_to(nk_draw_list list, nk_vec2 center, float radius, float a_min, float a_max,
			uint segments)
		{
			uint i = (uint) (0);
			if (list == null) return;
			if ((radius) == (0.0f)) return;
			{
				float d_angle = (float) ((a_max - a_min)/(float) (segments));
				float sin_d = (float) (nk_sin((float) (d_angle)));
				float cos_d = (float) (nk_cos((float) (d_angle)));
				float cx = (float) (nk_cos((float) (a_min))*radius);
				float cy = (float) (nk_sin((float) (a_min))*radius);
				for (i = (uint) (0); i <= segments; ++i)
				{
					float new_cx;
					float new_cy;
					float x = (float) (center.x + cx);
					float y = (float) (center.y + cy);
					nk_draw_list_path_line_to(list, (nk_vec2) (nk_vec2_((float) (x), (float) (y))));
					new_cx = (float) (cx*cos_d - cy*sin_d);
					new_cy = (float) (cy*cos_d + cx*sin_d);
					cx = (float) (new_cx);
					cy = (float) (new_cy);
				}
			}

		}

		public static void nk_draw_list_path_rect_to(nk_draw_list list, nk_vec2 a, nk_vec2 b, float rounding)
		{
			float r;
			if (list == null) return;
			r = (float) (rounding);
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
				nk_draw_list_path_line_to(list, (nk_vec2) (a));
				nk_draw_list_path_line_to(list, (nk_vec2) (nk_vec2_((float) (b.x), (float) (a.y))));
				nk_draw_list_path_line_to(list, (nk_vec2) (b));
				nk_draw_list_path_line_to(list, (nk_vec2) (nk_vec2_((float) (a.x), (float) (b.y))));
			}
			else
			{
				nk_draw_list_path_arc_to_fast(list, (nk_vec2) (nk_vec2_((float) (a.x + r), (float) (a.y + r))), (float) (r),
					(int) (6), (int) (9));
				nk_draw_list_path_arc_to_fast(list, (nk_vec2) (nk_vec2_((float) (b.x - r), (float) (a.y + r))), (float) (r),
					(int) (9), (int) (12));
				nk_draw_list_path_arc_to_fast(list, (nk_vec2) (nk_vec2_((float) (b.x - r), (float) (b.y - r))), (float) (r),
					(int) (0), (int) (3));
				nk_draw_list_path_arc_to_fast(list, (nk_vec2) (nk_vec2_((float) (a.x + r), (float) (b.y - r))), (float) (r),
					(int) (3), (int) (6));
			}

		}

		public static void nk_draw_list_path_curve_to(nk_draw_list list, nk_vec2 p2, nk_vec2 p3, nk_vec2 p4, uint num_segments)
		{
			float t_step;
			uint i_step;
			nk_vec2 p1 = new nk_vec2();
			if ((list == null) || (list.path_count == 0)) return;
			num_segments = (uint) ((num_segments) < (1) ? (1) : (num_segments));
			p1 = (nk_vec2) (nk_draw_list_path_last(list));
			t_step = (float) (1.0f/(float) (num_segments));
			for (i_step = (uint) (1); i_step <= num_segments; ++i_step)
			{
				float t = (float) (t_step*(float) (i_step));
				float u = (float) (1.0f - t);
				float w1 = (float) (u*u*u);
				float w2 = (float) (3*u*u*t);
				float w3 = (float) (3*u*t*t);
				float w4 = (float) (t*t*t);
				float x = (float) (w1*p1.x + w2*p2.x + w3*p3.x + w4*p4.x);
				float y = (float) (w1*p1.y + w2*p2.y + w3*p3.y + w4*p4.y);
				nk_draw_list_path_line_to(list, (nk_vec2) (nk_vec2_((float) (x), (float) (y))));
			}
		}

		public static void nk_draw_list_path_fill(nk_draw_list list, nk_color color)
		{
			nk_vec2* points;
			if (list == null) return;
			points = (nk_vec2*) (nk_buffer_memory(list.buffer));
			nk_draw_list_fill_poly_convex(list, points, (uint) (list.path_count), (nk_color) (color),
				(int) (list.config.shape_AA));
			nk_draw_list_path_clear(list);
		}

		public static void nk_draw_list_path_stroke(nk_draw_list list, nk_color color, int closed, float thickness)
		{
			nk_vec2* points;
			if (list == null) return;
			points = (nk_vec2*) (nk_buffer_memory(list.buffer));
			nk_draw_list_stroke_poly_line(list, points, (uint) (list.path_count), (nk_color) (color), (int) (closed),
				(float) (thickness), (int) (list.config.line_AA));
			nk_draw_list_path_clear(list);
		}

		public static void nk_draw_list_stroke_line(nk_draw_list list, nk_vec2 a, nk_vec2 b, nk_color col, float thickness)
		{
			if ((list == null) || (col.a == 0)) return;
			if ((list.line_AA) == (NK_ANTI_ALIASING_ON))
			{
				nk_draw_list_path_line_to(list, (nk_vec2) (a));
				nk_draw_list_path_line_to(list, (nk_vec2) (b));
			}
			else
			{
				nk_draw_list_path_line_to(list,
					(nk_vec2)
						(nk_vec2_((float) ((a).x - (nk_vec2_((float) (0.5f), (float) (0.5f))).x),
							(float) ((a).y - (nk_vec2_((float) (0.5f), (float) (0.5f))).y))));
				nk_draw_list_path_line_to(list,
					(nk_vec2)
						(nk_vec2_((float) ((b).x - (nk_vec2_((float) (0.5f), (float) (0.5f))).x),
							(float) ((b).y - (nk_vec2_((float) (0.5f), (float) (0.5f))).y))));
			}

			nk_draw_list_path_stroke(list, (nk_color) (col), (int) (NK_STROKE_OPEN), (float) (thickness));
		}

		public static void nk_draw_list_fill_rect(nk_draw_list list, nk_rect rect, nk_color col, float rounding)
		{
			if ((list == null) || (col.a == 0)) return;
			if ((list.line_AA) == (NK_ANTI_ALIASING_ON))
			{
				nk_draw_list_path_rect_to(list, (nk_vec2) (nk_vec2_((float) (rect.x), (float) (rect.y))),
					(nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))), (float) (rounding));
			}
			else
			{
				nk_draw_list_path_rect_to(list, (nk_vec2) (nk_vec2_((float) (rect.x - 0.5f), (float) (rect.y - 0.5f))),
					(nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))), (float) (rounding));
			}

			nk_draw_list_path_fill(list, (nk_color) (col));
		}

		public static void nk_draw_list_stroke_rect(nk_draw_list list, nk_rect rect, nk_color col, float rounding,
			float thickness)
		{
			if ((list == null) || (col.a == 0)) return;
			if ((list.line_AA) == (NK_ANTI_ALIASING_ON))
			{
				nk_draw_list_path_rect_to(list, (nk_vec2) (nk_vec2_((float) (rect.x), (float) (rect.y))),
					(nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))), (float) (rounding));
			}
			else
			{
				nk_draw_list_path_rect_to(list, (nk_vec2) (nk_vec2_((float) (rect.x - 0.5f), (float) (rect.y - 0.5f))),
					(nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))), (float) (rounding));
			}

			nk_draw_list_path_stroke(list, (nk_color) (col), (int) (NK_STROKE_CLOSED), (float) (thickness));
		}

		public static void nk_draw_list_fill_rect_multi_color(nk_draw_list list, nk_rect rect, nk_color left, nk_color top,
			nk_color right, nk_color bottom)
		{
			void* vtx;
			nk_colorf col_left = new nk_colorf();
			nk_colorf col_top = new nk_colorf();
			nk_colorf col_right = new nk_colorf();
			nk_colorf col_bottom = new nk_colorf();
			ushort* idx;
			ushort index;
			nk_color_fv(&col_left.r, (nk_color) (left));
			nk_color_fv(&col_right.r, (nk_color) (right));
			nk_color_fv(&col_top.r, (nk_color) (top));
			nk_color_fv(&col_bottom.r, (nk_color) (bottom));
			if (list == null) return;
			nk_draw_list_push_image(list, (nk_handle) (list.config._null_.texture));
			index = ((ushort) (list.vertex_count));
			vtx = nk_draw_list_alloc_vertices(list, (ulong) (4));
			idx = nk_draw_list_alloc_elements(list, (ulong) (6));
			if ((vtx == null) || (idx == null)) return;
			idx[0] = ((ushort) (index + 0));
			idx[1] = ((ushort) (index + 1));
			idx[2] = ((ushort) (index + 2));
			idx[3] = ((ushort) (index + 0));
			idx[4] = ((ushort) (index + 2));
			idx[5] = ((ushort) (index + 3));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (rect.x), (float) (rect.y))),
				(nk_vec2) (list.config._null_.uv), (nk_colorf) (col_left));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y))),
				(nk_vec2) (list.config._null_.uv), (nk_colorf) (col_top));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))),
				(nk_vec2) (list.config._null_.uv), (nk_colorf) (col_right));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (nk_vec2_((float) (rect.x), (float) (rect.y + rect.h))),
				(nk_vec2) (list.config._null_.uv), (nk_colorf) (col_bottom));
		}

		public static void nk_draw_list_fill_triangle(nk_draw_list list, nk_vec2 a, nk_vec2 b, nk_vec2 c, nk_color col)
		{
			if ((list == null) || (col.a == 0)) return;
			nk_draw_list_path_line_to(list, (nk_vec2) (a));
			nk_draw_list_path_line_to(list, (nk_vec2) (b));
			nk_draw_list_path_line_to(list, (nk_vec2) (c));
			nk_draw_list_path_fill(list, (nk_color) (col));
		}

		public static void nk_draw_list_stroke_triangle(nk_draw_list list, nk_vec2 a, nk_vec2 b, nk_vec2 c, nk_color col,
			float thickness)
		{
			if ((list == null) || (col.a == 0)) return;
			nk_draw_list_path_line_to(list, (nk_vec2) (a));
			nk_draw_list_path_line_to(list, (nk_vec2) (b));
			nk_draw_list_path_line_to(list, (nk_vec2) (c));
			nk_draw_list_path_stroke(list, (nk_color) (col), (int) (NK_STROKE_CLOSED), (float) (thickness));
		}

		public static void nk_draw_list_fill_circle(nk_draw_list list, nk_vec2 center, float radius, nk_color col, uint segs)
		{
			float a_max;
			if ((list == null) || (col.a == 0)) return;
			a_max = (float) (3.141592654f*2.0f*((float) (segs) - 1.0f)/(float) (segs));
			nk_draw_list_path_arc_to(list, (nk_vec2) (center), (float) (radius), (float) (0.0f), (float) (a_max), (uint) (segs));
			nk_draw_list_path_fill(list, (nk_color) (col));
		}

		public static void nk_draw_list_stroke_circle(nk_draw_list list, nk_vec2 center, float radius, nk_color col, uint segs,
			float thickness)
		{
			float a_max;
			if ((list == null) || (col.a == 0)) return;
			a_max = (float) (3.141592654f*2.0f*((float) (segs) - 1.0f)/(float) (segs));
			nk_draw_list_path_arc_to(list, (nk_vec2) (center), (float) (radius), (float) (0.0f), (float) (a_max), (uint) (segs));
			nk_draw_list_path_stroke(list, (nk_color) (col), (int) (NK_STROKE_CLOSED), (float) (thickness));
		}

		public static void nk_draw_list_stroke_curve(nk_draw_list list, nk_vec2 p0, nk_vec2 cp0, nk_vec2 cp1, nk_vec2 p1,
			nk_color col, uint segments, float thickness)
		{
			if ((list == null) || (col.a == 0)) return;
			nk_draw_list_path_line_to(list, (nk_vec2) (p0));
			nk_draw_list_path_curve_to(list, (nk_vec2) (cp0), (nk_vec2) (cp1), (nk_vec2) (p1), (uint) (segments));
			nk_draw_list_path_stroke(list, (nk_color) (col), (int) (NK_STROKE_OPEN), (float) (thickness));
		}

		public static void nk_draw_list_push_rect_uv(nk_draw_list list, nk_vec2 a, nk_vec2 c, nk_vec2 uva, nk_vec2 uvc,
			nk_color color)
		{
			void* vtx;
			nk_vec2 uvb = new nk_vec2();
			nk_vec2 uvd = new nk_vec2();
			nk_vec2 b = new nk_vec2();
			nk_vec2 d = new nk_vec2();
			nk_colorf col = new nk_colorf();
			ushort* idx;
			ushort index;
			if (list == null) return;
			nk_color_fv(&col.r, (nk_color) (color));
			uvb = (nk_vec2) (nk_vec2_((float) (uvc.x), (float) (uva.y)));
			uvd = (nk_vec2) (nk_vec2_((float) (uva.x), (float) (uvc.y)));
			b = (nk_vec2) (nk_vec2_((float) (c.x), (float) (a.y)));
			d = (nk_vec2) (nk_vec2_((float) (a.x), (float) (c.y)));
			index = ((ushort) (list.vertex_count));
			vtx = nk_draw_list_alloc_vertices(list, (ulong) (4));
			idx = nk_draw_list_alloc_elements(list, (ulong) (6));
			if ((vtx == null) || (idx == null)) return;
			idx[0] = ((ushort) (index + 0));
			idx[1] = ((ushort) (index + 1));
			idx[2] = ((ushort) (index + 2));
			idx[3] = ((ushort) (index + 0));
			idx[4] = ((ushort) (index + 2));
			idx[5] = ((ushort) (index + 3));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (a), (nk_vec2) (uva), (nk_colorf) (col));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (b), (nk_vec2) (uvb), (nk_colorf) (col));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (c), (nk_vec2) (uvc), (nk_colorf) (col));
			vtx = nk_draw_vertex(vtx, list.config, (nk_vec2) (d), (nk_vec2) (uvd), (nk_colorf) (col));
		}

		public static void nk_draw_list_add_image(nk_draw_list list, nk_image texture, nk_rect rect, nk_color color)
		{
			if (list == null) return;
			nk_draw_list_push_image(list, (nk_handle) (texture.handle));
			if ((nk_image_is_subimage(texture)) != 0)
			{
				nk_vec2* uv = stackalloc nk_vec2[2];
				uv[0].x = (float) ((float) (texture.region[0])/(float) (texture.w));
				uv[0].y = (float) ((float) (texture.region[1])/(float) (texture.h));
				uv[1].x = (float) ((float) (texture.region[0] + texture.region[2])/(float) (texture.w));
				uv[1].y = (float) ((float) (texture.region[1] + texture.region[3])/(float) (texture.h));
				nk_draw_list_push_rect_uv(list, (nk_vec2) (nk_vec2_((float) (rect.x), (float) (rect.y))),
					(nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))), (nk_vec2) (uv[0]), (nk_vec2) (uv[1]),
					(nk_color) (color));
			}
			else
				nk_draw_list_push_rect_uv(list, (nk_vec2) (nk_vec2_((float) (rect.x), (float) (rect.y))),
					(nk_vec2) (nk_vec2_((float) (rect.x + rect.w), (float) (rect.y + rect.h))),
					(nk_vec2) (nk_vec2_((float) (0.0f), (float) (0.0f))), (nk_vec2) (nk_vec2_((float) (1.0f), (float) (1.0f))),
					(nk_color) (color));
		}

		public static void nk_draw_list_add_text(nk_draw_list list, nk_user_font font, nk_rect rect, char* text, int len,
			float font_height, nk_color fg)
		{
			float x = (float) (0);
			int text_len = (int) (0);
			char unicode = (char) 0;
			char next = (char) (0);
			int glyph_len = (int) (0);
			int next_glyph_len = (int) (0);
			nk_user_font_glyph g = new nk_user_font_glyph();
			if (((list == null) || (len == 0)) || (text == null)) return;
			if (
				!(!(((((list.clip_rect.x) > (rect.x + rect.w)) || ((list.clip_rect.x + list.clip_rect.w) < (rect.x))) ||
				     ((list.clip_rect.y) > (rect.y + rect.h))) || ((list.clip_rect.y + list.clip_rect.h) < (rect.y))))) return;
			nk_draw_list_push_image(list, (nk_handle) (font.texture));
			x = (float) (rect.x);
			glyph_len = (int) (nk_utf_decode(text, &unicode, (int) (len)));
			if (glyph_len == 0) return;
			fg.a = ((byte) ((float) (fg.a)*list.config.global_alpha));
			while (((text_len) < (len)) && ((glyph_len) != 0))
			{
				float gx;
				float gy;
				float gh;
				float gw;
				float char_width = (float) (0);
				if ((unicode) == (0xFFFD)) break;
				next_glyph_len = (int) (nk_utf_decode(text + text_len + glyph_len, &next, (int) (len - text_len)));
				font.query((nk_handle) (font.userdata), (float) (font_height), &g, unicode, (next == 0xFFFD) ? '\0' : next);
				gx = (float) (x + g.offset.x);
				gy = (float) (rect.y + g.offset.y);
				gw = (float) (g.width);
				gh = (float) (g.height);
				char_width = (float) (g.xadvance);
				nk_draw_list_push_rect_uv(list, (nk_vec2) (nk_vec2_((float) (gx), (float) (gy))),
					(nk_vec2) (nk_vec2_((float) (gx + gw), (float) (gy + gh))), nk_vec2_(g.uv_x[0], g.uv_y[0]),
					nk_vec2_(g.uv_x[1], g.uv_y[1]), (nk_color) (fg));
				text_len += (int) (glyph_len);
				x += (float) (char_width);
				glyph_len = (int) (next_glyph_len);
				unicode = (char) (next);
			}
		}

		public static nk_draw_command* nk__draw_next(nk_draw_command* cmd, nk_buffer buffer, nk_context ctx)
		{
			return nk__draw_list_next(cmd, buffer, ctx.draw_list);
		}
	}
}