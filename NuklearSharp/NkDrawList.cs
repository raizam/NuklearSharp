namespace KlearUI
{
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