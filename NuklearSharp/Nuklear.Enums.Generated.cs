using System;

namespace KlearUI
{



    public enum VertexLayoutFormat
    {
        SCHAR = 0,
        SSHORT = 1,
        SINT = 2,
        UCHAR = 3,
        USHORT = 4,
        UINT = 5,
        FLOAT = 6,
        DOUBLE = 7,
        COLOR_BEGIN = 8,
        R8G8B8 = 8,
        R16G15B16 = 10,
        R32G32B32 = 11,
        R8G8B8A8 = 12,
        B8G8R8A8 = 13,
        R16G15B16A16 = 14,
        R32G32B32A32 = 15,
        R32G32B32A32_FLOAT = 16,
        R32G32B32A32_DOUBLE = 17,
        RGB32 = 18,
        RGBA32 = 19,
        COLOR_END = 19,
        COUNT = 21
    }

    public enum VertexLayoutKind
    {
        Position = 0,
        Color = 1,
        TexCoord = 2,
        COUNT = 3
    }

    public enum CommandType
    {
        Scissor = 1,
        Line = 2,
        Curve = 3,
        Rect = 4,
        RectFilled = 5,
        RectMulticolor = 6,
        Circle = 7,
        CircleFilled = 8,
        Arc = 9,
        ArcFilled = 10,
        Triangle = 11,
        TriangleFilled = 12,
        Polygon = 13,
        PolygonFilled = 14,
        Polyline = 15,
        Text = 16,
        Image = 17,
        Custom = 18
    }
    
    public enum FontCoordType
    {
        CoordUV = 0,
        CoordPixel = 1
    }

    public enum ToggleKind
    {
        Check = 0,
        Option = 1
    }



    public enum PropertyFilterKind
    {
        FilterInt = 0,
        FilterFloat = 1
    }
    
    public enum Heading
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public enum ButtonBehavior
    {
        Default = 0,
        Repeater = 1
    }
    public enum Orientation
    {
        Vertical = 0,
        Horizontal = 1
    }
    public enum VisibleStates
    {
        Minimized = 0,
        Maximized = 1
    }

    public enum WidgetLayoutStates
    {
        Invalid = 0,
        Valid = 1,
        ROM = 2
    }

    [Flags]
    public enum WidgetStates
    {
        Modified = 2,
        Inactive = 4,
        Entered = 8,
        Hover = 16,
        Actived = 32,
        Left = 64,
        Hovered = 18,
        Active = 34
    }



    public enum StyleColors
    {
        Text = 0,
        Window = 1,
        Header = 2,
        Border = 3,
        Button = 4,
        ButtonHover = 5,
        ButtonActive = 6,
        Toogle = 7,
        ToogleHover = 8,
        ToogleCursor = 9,
        Select = 10,
        SelectActive = 11,
        Slider = 12,
        SliderCursor = 13,
        SliderCursorHover = 14,
        SliderCursorActive = 15,
        Property = 16,
        Edit = 17,
        EditCursor = 18,
        Combo = 19,
        Chart = 20,
        CharColor = 21,
        CharColorHighlight = 22,
        Scrollbar = 23,
        ScrollbarCursor = 24,
        ScrollbarCursorHover = 25,
        ScrollbarCursorActive = 26,
        TabHeader = 27,
        COUNT = 28
    }


    public enum CursorKind
    {
        Arrow = 0,
        Text = 1,
        Move = 2,
        ResizeVertical = 3,
        ResizeHorizontal = 4,
        ResizeAntiClockwise = 5,
        ResizeClockwise = 6,
        COUNT = 7
    }

    public enum Symbols
    {
        None = 0,
        X = 1,
        Underscore = 2,
        CircleSolid = 3,
        CircleOutline = 4,
        RectSolid = 5,
        RectOutline = 6,
        TriangleUp = 7,
        TriangleDown = 8,
        TriangleLeft = 9,
        TriangleRight = 10,
        Plus = 11,
        Minus = 12,
        MAX = 13
    }

    public enum ControlKeys
    {
        None = 0,
        Shift = 1,
        Ctrl = 2,
        Del = 3,
        Enter = 4,
        Tab = 5,
        Backspace = 6,
        Copy = 7,
        Cut = 8,
        Paste = 9,
        Up = 10,
        Down = 11,
        Left = 12,
        Right = 13,
        TextInsertMode = 14,
        TextReplaceMode = 15,
        TextResetMode = 16,
        TextLineStart = 17,
        TextLineEnd = 18,
        TextStart = 19,
        TextEnd = 20,
        TextUndo = 21,
        TextRedo = 22,
        TextSelectAll = 23,
        TextWordLeft = 24,
        TextWordRight = 25,
        ScrollStart = 26,
        ScrollEnd = 27,
        ScrollDown = 28,
        ScrollUp = 29,
        MAX = 30
    }

    public enum ChartKind
    {
        Lines = 0,
        Collumn = 1,
        NK_CHART_MAX = 2
    }


    [Flags]
    public enum Align
    {
        Left = 1,
        Centered = 2,
        Right = 4,
        Top = 8,
        Middle = 16,
        Bottom = 32,

        MiddleLeft = 17,
        MiddleCentered = 18,
        MiddleRight = 20
    }

    [Flags]
    public enum PanelFlags : uint
    {
        Border = 1,
        Movable = 2,
        Scalable = 4,
        Closable = 8,
        Minimizable = 16,
        NoScrollbar = 32,
        Title = 64,
        ScrollAutoHide = 128,
        Background = 256,
        ScaleLeft = 512,
        NonInput = 1024,

        Private = (1 << (11)),
        Dynamic = PanelFlags.Private,
        Rom = (1 << (12)),
        NotInteractive = PanelFlags.Rom | PanelFlags.NonInput,
        Hidden = (1 << (13)),
        Closed = (1 << (14)),
        Minimized = (1 << (15)),
        RemoveRom = (1 << (16)),
    }

    [Flags]
    public enum EditState : uint
    {
        None = 0,
        Active = (1 << 0),
        Inactive = (1 << 1),
        Activated = (1 << 2),
        Deactivated = (1 << 3),
        Commited = (1 << 4),
    }

    public enum EditFlags : uint
    {
        Default = 0,
        ReadOnly = (1 << (0)),
        AutoSelect = (1 << (1)),
        SigEnter = (1 << (2)),
        AllowTab = (1 << (3)),
        NoCursor = (1 << (4)),
        Selectable = (1 << (5)),
        Clipboard = (1 << (6)),
        CtrlEnterNewLine = (1 << (7)),
        NoHorizontalScroll = (1 << (8)),
        AlwaysInsertMode = (1 << (9)),
        Multiline = (1 << (10)),
        GotoEndOnActivate = (1 << (11)),
        Simple = AlwaysInsertMode,
        Filed = Simple | Selectable | Clipboard,

        Box = AlwaysInsertMode | Selectable | Multiline | AllowTab | Clipboard,

        Editor = Selectable | Multiline | AllowTab | Clipboard,
    }

    public enum ChartEvent
    {
        Hovering = 1,
        Clicked = 2
    }

    public enum PopupKind
    {
        PopupStatic = 0,
        PopupDynamic = 1
    }

    public enum ShowStates
    {
        Hidden = 0,
        Shown = 1
    }

    public enum ColorFormat
    {
        Rgb = 0,
        Rgba = 1
    }

    public enum LayoutFormat
    {
        Dynamic = 0,
        Static = 1
    }
    public enum TreeKind
    {
        Node = 0,
        Tab = 1
    }
    public enum MouseButtons
    {
        Left = 0,
        Middle = 1,
        Right = 2,
        Double = 3,
        MAX = 4
    }

    [Flags]
    public enum VertexConvertResult
    {
        Success = 0,
        InvalidParam = 1,
        CommandBufferFull = 2,
        VertexBufferFull = 4,
        ElementBufferFull = 8
    }

    [Flags]
    public enum PanelKind
    {
        Window = (1 << (0)),
        Group = (1 << (1)),
        Popup = (1 << (2)),
        Contextual = (1 << (4)),
        Combo = (1 << (5)),
        Menu = (1 << (6)),
        Tooltip = (1 << (7)),
        SetNonBlock =    PanelKind.Contextual | PanelKind.Combo | PanelKind.Menu | PanelKind.Tooltip,
        SetPopup = PanelKind.SetNonBlock | PanelKind.Popup,
        SetSub = PanelKind.SetPopup | PanelKind.Group,
    }

    public enum FontAtlasFormat
    {
        Alpha8,
        Rgba32
    }


    public enum PanelRowLayoutType
    {
        DynamicFixed = 0,
        DynamicRow = 1,
        DynamicFree = 2,
        Dynamic = 3,
        StatixFixed = 4,
        StaticRow = 5,
        StaticFree = 6,
        Static = 7,
        Template = 8,
        COUNT = 9
    }

    public enum PropertyKind
    {
        Int = 0,
        Float = 1,
        Double = 2
    }

    public enum PropertyStatus
    {
        Default = 0,
        Edit = 1,
        Drag = 2
    }

    public enum TextEditMode
    {
        SingleLine = 0,
        Multiline = 1
    }

    public enum NkTextEditMode
    {
        View = 0,
        Insert = 1,
        Replace = 2
    } 

    public enum StyleItemKind
    {
        Color = 0,
        Image = 1
    }

    public enum StyleHeaderAlign
    {
        Left = 0,
        Right = 1
    }

    unsafe partial class Nk
    {
   
        public const int NK_RP_HEURISTIC_Skyline_default = 0;
        public const int NK_RP_HEURISTIC_Skyline_BL_sortHeight = NK_RP_HEURISTIC_Skyline_default;
        public const int NK_RP_HEURISTIC_Skyline_BF_sortHeight = 2;
        public const int NK_RP__INIT_skyline = 1;

        public const int NK_INSERT_BACK = 0;
        public const int NK_INSERT_FRONT = 1;
    }
}