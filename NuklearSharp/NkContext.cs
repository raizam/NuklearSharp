namespace NuklearSharp
{
    public class NkContext
    {
        public nk_input Input = new nk_input();
        public NkStyle Style = new NkStyle();
        public NkClipboard Clip = new NkClipboard();
        public NkWidgetStates LastWidgetState;
        public NkButtonBehavior ButtonBehavior;
        public nk_configuration_stacks Stacks = new nk_configuration_stacks();
        public float DeltaTimeSeconds;
        public NkDrawList DrawList = new NkDrawList();
        public NkHandle Userdata = new NkHandle();
        public nk_text_edit TextEdit = new nk_text_edit();
        public NkCommandBuffer Overlay = new NkCommandBuffer();
        public bool Build;
        public NkWindow Begin;
        public NkWindow End;
        public NkWindow Active;
        public NkWindow Current;
        public uint Count;
        public uint Seq;
    }

    public class NkDrawList
    {
        public NkRect ClipRect;
        public readonly NkVec2[] CircleVtx = new NkVec2[12];
        public NkConvertConfig Config;
        public readonly NkBuffer<NkVec2> Points = new NkBuffer<NkVec2>();
        public NkBuffer<nk_draw_command> Buffer;
        public NkBuffer<byte> Vertices;
        public readonly NkBuffer<NkVec2> Normals = new NkBuffer<NkVec2>();
        public NkBuffer<ushort> Elements;
        public bool LineAa;
        public bool ShapeAa;
        public NkHandle Userdata;

        public int VertexOffset
        {
            get { return Vertices.Count / (int)Config.VertexSize; }
        }

        public int AddElements(int size)
        {
            int result = Elements.Count;

            Elements.AddToEnd(size);

            Buffer.Data[Buffer.Count - 1].elem_count += (uint)size;

            return result;
        }
    }
}