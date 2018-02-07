namespace NuklearSharp
{
	partial class TextUndoState
	{
		public PinnedArray<TextUndoRecord> undo_rec = new PinnedArray<TextUndoRecord>(new TextUndoRecord[99]);
		public PinnedArray<char> undo_char = new PinnedArray<char>(new char[999]);
		public short undo_point;
		public short redo_point;
		public short undo_char_point;
		public short redo_char_point;
	}
}
