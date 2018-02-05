namespace NuklearSharp
{
	unsafe partial class Font
	{
		public Font next;
		public UserFont handle = new UserFont();
		public BakedFont info = new BakedFont();
		public float scale;
		public FontGlyph* glyphs;
		public FontGlyph* fallback;
		public char fallback_codepoint;
		public Handle texture = new Handle();
		public FontConfig config;

		public float TextWidth(Handle h, float height, char* s, int length)
		{
			return handle.width(h, height, s, length);
		}

		public void QueryFontGlyph(Handle h, float height, UserFontGlyph* glyph, char codepoint,
			char next_codepoint)
		{
			handle.query(h, height, glyph, codepoint, next_codepoint);
		}
	}
}