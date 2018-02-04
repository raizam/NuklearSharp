using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public abstract unsafe partial class BaseContext
	{
		private readonly Nuklear.nk_context _ctx;
		private readonly Nuklear.nk_buffer _cmds;

		public Nuklear.nk_context Ctx
		{
			get { return _ctx; }
		}

		public Nuklear.nk_buffer Cmds
		{
			get { return _cmds; }
		}

		protected BaseContext()
		{
			_ctx = new Nuklear.nk_context();
			_cmds = new Nuklear.nk_buffer();
			Nuklear.nk_init_default(_ctx, null);
			Nuklear.nk_buffer_init_default(_cmds);
		}

		public FontAtlasWrapper CreateFontAtlas()
		{
			return new FontAtlasWrapper(this);
		}

		public void SetFont(Nuklear.nk_font font)
		{
			Nuklear.nk_style_set_font(_ctx, font.handle);
		}

		public void Draw()
		{
			BeginDraw();

			var vbuf = new Nuklear.nk_buffer();
			var ebuf = new Nuklear.nk_buffer();
			Nuklear.nk_draw_command* cmd;
			//  ushort* offset = null;
			var config = new Nuklear.nk_convert_config
			{
				vertex_size = (uint) sizeof (NkVertex),
				vertex_alignment = 4,
				global_alpha = 1f,
				shape_AA = Nuklear.NK_ANTI_ALIASING_ON,
				line_AA = Nuklear.NK_ANTI_ALIASING_ON,
				circle_segment_count = 22,
				curve_segment_count = 22,
				arc_segment_count = 22,
				vertex_layout = new[]
				{
					new Nuklear.nk_draw_vertex_layout_element
					{
						attribute = Nuklear.NK_VERTEX_POSITION,
						format = Nuklear.NK_FORMAT_FLOAT,
						offset = 0
					},
					new Nuklear.nk_draw_vertex_layout_element
					{
						attribute = Nuklear.NK_VERTEX_COLOR,
						format = Nuklear.NK_FORMAT_B8G8R8A8,
						offset = 12
					},
					new Nuklear.nk_draw_vertex_layout_element
					{
						attribute = Nuklear.NK_VERTEX_TEXCOORD,
						format = Nuklear.NK_FORMAT_FLOAT,
						offset = 16
					},
					new Nuklear.nk_draw_vertex_layout_element
					{
						attribute = Nuklear.NK_VERTEX_ATTRIBUTE_COUNT
					}
				}
			};

			/* convert shapes into vertexes */
			Nuklear.nk_buffer_init_default(vbuf);
			Nuklear.nk_buffer_init_default(ebuf);

			Convert(_cmds, vbuf, ebuf, config);

			var vSize = (ulong) sizeof (NkVertex);

			var vertex_count = (uint) (vbuf.needed/vSize);
			var vertices = new byte[(int) vbuf.needed];
			var indices = new short[ebuf.needed/sizeof (short)];

			Marshal.Copy((IntPtr) vbuf.memory.ptr, vertices, 0, (int) vbuf.needed);

			/* iterate over and execute each draw command */
			uint offset = 0;

			Marshal.Copy((IntPtr) ebuf.memory.ptr, indices, 0, indices.Length);

			SetBuffers(vertices, indices, (int) vertex_count, sizeof (NkVertex));
			for (cmd = Nuklear.nk__draw_begin(_ctx, _cmds); (cmd) != null; (cmd) = Nuklear.nk__draw_next(cmd, _cmds, _ctx))
			{
				if (cmd->elem_count == 0) continue;

				Draw((int) cmd->clip_rect.x, (int) cmd->clip_rect.y, (int) cmd->clip_rect.w, (int) cmd->clip_rect.h,
					cmd->texture.id, (int) offset, (int) (cmd->elem_count/3));
				offset += cmd->elem_count;
			}
			Nuklear.nk_buffer_free(vbuf);
			Nuklear.nk_buffer_free(ebuf);
			Nuklear.nk_clear(_ctx);

			EndDraw();
		}

		/// <summary>
		/// Creates a texture and returns its unique id
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		protected internal  abstract int CreateTexture(int width, int height, byte[] data);

		/// <summary>
		/// Called at the beginning of the draw
		/// </summary>
		protected internal abstract void BeginDraw();

		/// <summary>
		/// Fills vertex and index buffers with data
		/// </summary>
		/// <param name="vertices"></param>
		/// <param name="indices"></param>
		/// <param name="vertex_count"></param>
		/// <param name="vertex_stride"></param>
		protected internal abstract void SetBuffers(byte[] vertices, short[] indices, int vertex_count, int vertex_stride);

		/// <summary>
		/// Draw
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="w"></param>
		/// <param name="h"></param>
		/// <param name="textureId"></param>
		/// <param name="startIndex"></param>
		/// <param name="primitiveCount"></param>
		protected internal abstract void Draw(int x, int y, int w, int h, int textureId, int startIndex, int primitiveCount);


		/// <summary>
		/// Called at the end of the draw
		/// </summary>
		protected internal abstract void EndDraw();
	}
}