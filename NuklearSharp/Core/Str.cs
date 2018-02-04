namespace NuklearSharp
{
	partial class Str
	{
		public void InitDefault()
		{
			buffer.Init(32);
			len = 0;
		}

		public void Init(ulong size)
		{
			buffer.Init(size);
			len = 0;
		}
	}
}