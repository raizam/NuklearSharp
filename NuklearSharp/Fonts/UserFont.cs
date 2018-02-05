namespace NuklearSharp
{
	public unsafe partial class UserFont
	{
		public int TextClamp(char* text, int text_len, float space, int* glyphs, float* text_width, uint* sep_list,
			int sep_count)
		{
			int i = (int) (0);
			int glyph_len = (int) (0);
			float last_width = (float) (0);
			char unicode = (char) 0;
			float width = (float) (0);
			int len = (int) (0);
			int g = (int) (0);
			float s;
			int sep_len = (int) (0);
			int sep_g = (int) (0);
			float sep_width = (float) (0);
			sep_count = (int) ((sep_count) < (0) ? (0) : (sep_count));
			glyph_len = (int) (Nuklear.UtfDecode(text, &unicode, (int) (text_len)));
			while ((((glyph_len) != 0) && ((width) < (space))) && ((len) < (text_len)))
			{
				len += (int) (glyph_len);
				s = (float) (this.width((Handle) (this.userdata), (float) (this.height), text, (int) (len)));
				for (i = (int) (0); (i) < (sep_count); ++i)
				{
					if (unicode != sep_list[i]) continue;
					sep_width = (float) (last_width = (float) (width));
					sep_g = (int) (g + 1);
					sep_len = (int) (len);
					break;
				}
				if ((i) == (sep_count))
				{
					last_width = (float) (sep_width = (float) (width));
					sep_g = (int) (g + 1);
				}
				width = (float) (s);
				glyph_len = (int) (Nuklear.UtfDecode(text + len, &unicode, (int) (text_len - len)));
				g++;
			}
			if ((len) >= (text_len))
			{
				*glyphs = (int) (g);
				*text_width = (float) (last_width);
				return (int) (len);
			}
			else
			{
				*glyphs = (int) (sep_g);
				*text_width = (float) (sep_width);
				return (int) ((sep_len == 0) ? len : sep_len);
			}

		}

		public Vec2 TextCalculateTextBounds(char* begin, int byte_len, float row_height, char** remaining, Vec2* out_offset,
			int* glyphs, int op)
		{
			float line_height = (float) (row_height);
			Vec2 text_size = (Vec2) (Nuklear.Vec2z((float) (0), (float) (0)));
			float line_width = (float) (0.0f);
			float glyph_width;
			int glyph_len = (int) (0);
			char unicode = (char) 0;
			int text_len = (int) (0);
			if (((begin == null) || (byte_len <= 0)) || (this == null))
				return (Vec2) (Nuklear.Vec2z((float) (0), (float) (row_height)));
			glyph_len = (int) (Nuklear.UtfDecode(begin, &unicode, (int) (byte_len)));
			if (glyph_len == 0) return (Vec2) (text_size);
			glyph_width = (float) (this.width((Handle) (this.userdata), (float) (this.height), begin, (int) (glyph_len)));
			*glyphs = (int) (0);
			while (((text_len) < (byte_len)) && ((glyph_len) != 0))
			{
				if ((unicode) == ('\n'))
				{
					text_size.x = (float) ((text_size.x) < (line_width) ? (line_width) : (text_size.x));
					text_size.y += (float) (line_height);
					line_width = (float) (0);
					*glyphs += (int) (1);
					if ((op) == (Nuklear.NK_STOP_ON_NEW_LINE)) break;
					text_len++;
					glyph_len = (int) (Nuklear.UtfDecode(begin + text_len, &unicode, (int) (byte_len - text_len)));
					continue;
				}
				if ((unicode) == ('\r'))
				{
					text_len++;
					*glyphs += (int) (1);
					glyph_len = (int) (Nuklear.UtfDecode(begin + text_len, &unicode, (int) (byte_len - text_len)));
					continue;
				}
				*glyphs = (int) (*glyphs + 1);
				text_len += (int) (glyph_len);
				line_width += (float) (glyph_width);
				glyph_len = (int) (Nuklear.UtfDecode(begin + text_len, &unicode, (int) (byte_len - text_len)));
				glyph_width =
					(float) (this.width((Handle) (this.userdata), (float) (this.height), begin + text_len, (int) (glyph_len)));
				continue;
			}
			if ((text_size.x) < (line_width)) text_size.x = (float) (line_width);
			if ((out_offset) != null)
				*out_offset = (Vec2) (Nuklear.Vec2z((float) (line_width), (float) (text_size.y + line_height)));
			if (((line_width) > (0)) || ((text_size.y) == (0.0f))) text_size.y += (float) (line_height);
			if ((remaining) != null) *remaining = begin + text_len;
			return (Vec2) (text_size);
		}
	}
}