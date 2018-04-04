namespace KlearUI
{
    public unsafe partial class NkImage
    {
        public NkHandle handle = new NkHandle();
        public ushort w;
        public ushort h;
        public PinnedArray<ushort> region = new PinnedArray<ushort>(4);
    }
}