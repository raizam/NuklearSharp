using NuklearSharp;

namespace RaizamTest
{
	public class FontInfo
	{
		internal FontInfo(string name, string file, int size)
		{
			Size = size;
			Name = name;
			File = file;
		}

		public string Name { get; set; }
		public string File { get; set; }
		public int Size { get; set; }
	}

	public class NkFont
	{
		public string Name { get; set; }

		internal Nuklear.nk_font font;

		internal NkFont(string name, Nuklear.nk_font font)
		{
			this.Name = name;
			this.font = font;
		}

		public static FontInfo Define(string file, int size = 14, string name = null)
		{
			return new FontInfo(name, file, size);
		}
	}
}