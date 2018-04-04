using System.Runtime.InteropServices;

namespace KlearUI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NkRect
    {
        public float x;
        public float y;
        public float w;
        public float h;

        public NkVec2 nk_rect_pos()
        {
            NkVec2 ret = new NkVec2
            {
                x = x,
                y = y
            };
            return ret;
        }
    }
}