namespace NuklearSharp
{
	public class Keyboard
	{
		public PinnedArray<Key> keys = new PinnedArray<Key>(new Key[Nuklear.NK_KEY_MAX]);
		public PinnedArray<char> text = new PinnedArray<char>(new char[16]);
		public int text_len;
	}
}