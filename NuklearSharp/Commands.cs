namespace NuklearSharp
{
    public class NkCommandBase
    {
        public nk_command Header;
        public NkHandle Userdata;
        public NkCommandBase Next;
    }

    public class NkCommandScissor : NkCommandBase
    {
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
    }

    public class NkCommandLine : NkCommandBase
    {
        public ushort LineThickness;
        public NkPoint Begin = new NkPoint();
        public NkPoint End = new NkPoint();
        public NkColor Color = new NkColor();
    }

    public class NkCommandCurve : NkCommandBase
    {
        public ushort LineThickness;
        public NkPoint Begin = new NkPoint();
        public NkPoint End = new NkPoint();
        public NkPoint Ctrl0 = new NkPoint();
        public NkPoint Ctrl1 = new NkPoint();
        public NkColor Color = new NkColor();
    }

    public class NkCommandRect : NkCommandBase
    {
        public ushort Rounding;
        public ushort LineThickness;
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public NkColor Color = new NkColor();
    }

    public class NkCommandRectFilled : NkCommandBase
    {
        public ushort Rounding;
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public NkColor Color = new NkColor();
    }

    public class NkCommandRectMultiColor : NkCommandBase
    {
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public NkColor Left = new NkColor();
        public NkColor Top = new NkColor();
        public NkColor Bottom = new NkColor();
        public NkColor Right = new NkColor();
    }

    public class NkCommandTriangle : NkCommandBase
    {
        public ushort LineThickness;
        public NkPoint A = new NkPoint();
        public NkPoint B = new NkPoint();
        public NkPoint C = new NkPoint();
        public NkColor Color = new NkColor();
    }

    public class NkCommandTriangleFilled : NkCommandBase
    {
        public NkPoint A = new NkPoint();
        public NkPoint B = new NkPoint();
        public NkPoint C = new NkPoint();
        public NkColor Color = new NkColor();
    }

    public class NkCommandCircle : NkCommandBase
    {
        public short X;
        public short Y;
        public ushort LineThickness;
        public ushort W;
        public ushort H;
        public NkColor Color = new NkColor();
    }

    public class NkCommandCircleFilled : NkCommandBase
    {
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public NkColor Color = new NkColor();
    }

    public class NkCommandArc : NkCommandBase
    {
        public short Cx;
        public short Cy;
        public ushort R;
        public ushort LineThickness;
        public PinnedArray<float> A = new PinnedArray<float>(2);
        public NkColor Color = new NkColor();
    }

    public class NkCommandArcFilled : NkCommandBase
    {
        public short Cx;
        public short Cy;
        public ushort R;
        public PinnedArray<float> A = new PinnedArray<float>(2);
        public NkColor Color = new NkColor();
    }

    public class NkCommandPolygon : NkCommandBase
    {
        public NkColor Color;
        public ushort LineThickness;
        public ushort PointCount;
        public NkPoint[] Points;
    }

    public class NkCommandPolygonFilled : NkCommandBase
    {
        public NkColor Color;
        public ushort PointCount;
        public NkPoint[] Points;
    }

    public class NkCommandPolyline : NkCommandBase
    {
        public NkColor Color;
        public ushort LineThickness;
        public ushort PointCount;
        public NkPoint[] Points;
    }

    public class NkCommandImage : NkCommandBase
    {
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public NkImage Img = new NkImage();
        public NkColor Col = new NkColor();
    }

    public unsafe class NkCommandText : NkCommandBase
    {
        public NkUserFont Font;
        public NkColor Background;
        public NkColor Foreground;
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public float Height;
        public char* String;
        public int Length;
    }

    public class NkCommandCustom : NkCommandBase
    {
        public short X;
        public short Y;
        public ushort W;
        public ushort H;
        public NkHandle CallbackData;
        public NkCommandCustomCallback Callback;
    }
}