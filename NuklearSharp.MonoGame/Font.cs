using Microsoft.Xna.Framework.Graphics;

namespace NuklearSharp.MonoGame
{
	public class NkFont
	{
		private readonly Nuklear.nk_font _font;
		private readonly Texture2D _texture;

		public Nuklear.nk_font Font
		{
			get { return _font; }
		}

		public Texture2D Texture
		{
			get { return _texture; }
		}

		internal NkFont(Nuklear.nk_font font, Texture2D texture)
		{
			_font = font;
			_texture = texture;
		}
	}
}