namespace NuklearSharp
{
	unsafe partial class FontAtlas
	{
		public void* pixel;
		public int tex_width;
		public int tex_height;
		public Recti custom;
		public Cursor[] cursors = new Cursor[Nuklear.NK_CURSOR_COUNT];
		public int glyph_count;
		public FontGlyph* glyphs;
		public Font default_font;
		public Font fonts;
		public FontConfig config;
		public int font_num;

		public FontAtlas()
		{
			for (var i = 0; i < cursors.Length; ++i)
			{
				cursors[i] = new Cursor();
			}
		}

		public Font AddDefault(float pixel_height, FontConfig config)
		{
			fixed (byte* ptr = Nuklear.nk_proggy_clean_ttf_compressed_data_base85)
			{
				return AddCompressedBase85((sbyte*)ptr, pixel_height, config);
			}
		}
	}
}
