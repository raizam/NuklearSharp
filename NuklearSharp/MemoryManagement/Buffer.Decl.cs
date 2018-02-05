namespace NuklearSharp
{
	partial class Buffer
	{
		public BufferMarker[] marker = new BufferMarker[2];
		public int type;
		public Memory memory;
		public float grow_factor;
		public ulong allocated;
		public ulong needed;
		public ulong calls;
		public ulong size;

		public void InitDefault()
		{
			Init(4*1024);
		}
	}
}