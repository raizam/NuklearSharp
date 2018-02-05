using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	[StructLayout(LayoutKind.Sequential)]
	public unsafe partial struct TextFind
	{
		public float x;
		public float y;
		public float height;
		public int first_char;
		public int length;
		public int prev_first;

		public void TexteditFindCharpos(TextEdit state, int n, int single_line, UserFont font, float row_height)
		{
			TextEditRow r = new TextEditRow();
			int prev_start = (int) (0);
			int z = (int) (state._string_.len);
			int i = (int) (0);
			int first;
			Nuklear.Zero(&r, (ulong) (sizeof (TextEditRow)));
			if ((n) == (z))
			{
				r.TexteditLayoutRow(state, (int) (0), (float) (row_height), font);
				if ((single_line) != 0)
				{
					this.first_char = (int) (0);
					this.length = (int) (z);
				}
				else
				{
					while ((i) < (z))
					{
						prev_start = (int) (i);
						i += (int) (r.num_chars);
						r.TexteditLayoutRow(state, (int) (i), (float) (row_height), font);
					}
					this.first_char = (int) (i);
					this.length = (int) (r.num_chars);
				}
				this.x = (float) (r.x1);
				this.y = (float) (r.ymin);
				this.height = (float) (r.ymax - r.ymin);
				this.prev_first = (int) (prev_start);
				return;
			}

			this.y = (float) (0);
			for (;;)
			{
				r.TexteditLayoutRow(state, (int) (i), (float) (row_height), font);
				if ((n) < (i + r.num_chars)) break;
				prev_start = (int) (i);
				i += (int) (r.num_chars);
				this.y += (float) (r.baseline_y_delta);
			}
			this.first_char = (int) (first = (int) (i));
			this.length = (int) (r.num_chars);
			this.height = (float) (r.ymax - r.ymin);
			this.prev_first = (int) (prev_start);
			this.x = (float) (r.x0);
			for (i = (int) (0); (first + i) < (n); ++i)
			{
				this.x += (float) (state.TexteditGetWidth((int) (first), (int) (i), font));
			}
		}
	}
}