namespace NuklearSharp
{
	public abstract unsafe partial class BaseContext
	{
		private readonly Nuklear.nk_context _ctx;
		private readonly NkBuffer<Nuklear.nk_draw_command> _cmds = new NkBuffer<Nuklear.nk_draw_command>();
		private readonly NkBuffer<byte> _vertices = new NkBuffer<byte>();
		private readonly NkBuffer<ushort> _indices = new NkBuffer<ushort>();
		private readonly Nuklear.nk_convert_config _convertConfig;

		public Nuklear.nk_context Ctx
		{
			get { return _ctx; }
		}

		public NkBuffer<Nuklear.nk_draw_command> Cmds
		{
			get { return _cmds; }
		}

		public Nuklear.nk_convert_config ConvertConfig
		{
			get { return _convertConfig; }
		}

		protected BaseContext()
		{
			_ctx = new Nuklear.nk_context();
			Nuklear.nk_init_default(_ctx, null);

			_convertConfig = new Nuklear.nk_convert_config
			{
				vertex_alignment = 4,
				global_alpha = 1f,
				shape_AA = Nuklear.NK_ANTI_ALIASING_ON,
				line_AA = Nuklear.NK_ANTI_ALIASING_ON,
				circle_segment_count = 22,
				curve_segment_count = 22,
				arc_segment_count = 22
			};
		}

		public void SetFont(Nuklear.nk_font font)
		{
			Nuklear.nk_style_set_font(_ctx, font.handle);
		}

		public void Draw()
		{
			BeginDraw();

			//  ushort* offset = null;

			_cmds.reset();
			_vertices.reset();
			_indices.reset();
			Convert(_cmds, _vertices, _indices, _convertConfig);

			var vertex_count = (uint) ((ulong)_vertices.Count/_convertConfig.vertex_size);

			/* iterate over and execute each draw command */
			uint offset = 0;

			SetBuffers(_vertices.Data, _indices.Data, _indices.Count, (int) vertex_count);
			for(var i = 0; i < _cmds.Count; ++i)
			{
				var cmd = _cmds[i];
				if (cmd.elem_count == 0)
				{
					continue;
				}

				Draw((int) cmd.clip_rect.x, (int) cmd.clip_rect.y, (int) cmd.clip_rect.w, (int) cmd.clip_rect.h,
					cmd.texture.id, (int) offset, (int)(cmd.elem_count / 3));
				offset += cmd.elem_count;
			}
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
		public  abstract int CreateTexture(int width, int height, byte[] data);

		/// <summary>
		/// Called at the beginning of the draw
		/// </summary>
		protected internal abstract void BeginDraw();

		/// <summary>
		/// Fills vertex and index buffers with data
		/// </summary>
		/// <param name="vertices"></param>
		/// <param name="indices"></param>
		/// <param name="indices_count"></param>
		/// <param name="vertex_count"></param>
		/// <param name="vertex_stride"></param>
		protected internal abstract void SetBuffers(byte[] vertices, ushort[] indices, int indices_count, int vertex_count);

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