namespace NuklearSharp
{
    public abstract partial class BaseContext
    {
        private readonly NkContext _ctx;
        private readonly NkBuffer<nk_draw_command> _cmds = new NkBuffer<nk_draw_command>();
        private readonly NkBuffer<byte> _vertices = new NkBuffer<byte>();
        private readonly NkBuffer<ushort> _indices = new NkBuffer<ushort>();
        private readonly NkConvertConfig _convertConfig;

        public NkContext Ctx
        {
            get { return _ctx; }
        }

        public NkBuffer<nk_draw_command> Cmds
        {
            get { return _cmds; }
        }

        public NkConvertConfig ConvertConfig
        {
            get { return _convertConfig; }
        }

        protected BaseContext()
        {
            _ctx = new NkContext();
            Nk.nk_setup(_ctx, null);

            _convertConfig = new NkConvertConfig
            {
                VertexAlignment = 4,
                GlobalAlpha = 1f,
                ShapeAa = true,
                LineAa = true,
                CircleSegmentCount = 22,
                CurveSegmentCount = 22,
                ArcSegmentCount = 22
            };
        }

        public void SetFont(NkFont font)
        {
            Nk.nk_style_set_font(_ctx, font.Handle);
        }

        public void Draw()
        {
            BeginDraw();

            //  ushort* offset = null;

            _cmds.Reset();
            _vertices.Reset();
            _indices.Reset();
            Convert(_cmds, _vertices, _indices, _convertConfig);

            var vertexCount = (uint)((ulong)_vertices.Count / _convertConfig.VertexSize);

            /* iterate over and execute each draw command */
            uint offset = 0;

            SetBuffers(_vertices.Data, _indices.Data, _indices.Count, (int)vertexCount);
            for (var i = 0; i < _cmds.Count; ++i)
            {
                var cmd = _cmds[i];
                if (cmd.elem_count == 0)
                {
                    continue;
                }

                Draw((int)cmd.clip_rect.x, (int)cmd.clip_rect.y, (int)cmd.clip_rect.w, (int)cmd.clip_rect.h,
                    cmd.texture.id, (int)offset, (int)(cmd.elem_count / 3));
                offset += cmd.elem_count;
            }
            Nk.nk_clear(_ctx);

            EndDraw();
        }

        /// <summary>
        /// Creates a texture and returns its unique id
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract int CreateTexture(int width, int height, byte[] data);

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
        protected internal abstract void SetBuffers(byte[] vertices, ushort[] indices, int indicesCount, int vertexCount);

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