namespace NuklearSharp
{
	public static unsafe partial class Nuklear
	{
		public delegate float NkTextWidthDelegate(Handle handle, float height, char* text, int length);

		public delegate void NkQueryFontGlyphDelegate(Handle handle,
			float height, UserFontGlyph* glyph, char codepoint, char next_codepoint);

		public delegate void NkCommandCustomCallback(
			DrawList list, short x, short y, ushort w, ushort h, Handle callback_data);

		public delegate void NkPluginPaste(Handle handle, TextEdit text_edit);

		public delegate void NkPluginCopy(Handle handle, char* text, int length);

		public delegate void NkDrawNotify(CommandBuffer buffer, Handle handle);

		public delegate int NkPluginFilter(TextEdit text_edit, char unicode);

		public delegate float NkFloatValueGetter(void* handle, int index);

		public delegate float NkComboCallback(void* handle, int index, char** item);

		public delegate int QSortComparer(void* a, void* b);
	}
}