using System;

namespace NuklearSharp
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

    public enum NkDrawVertexLayoutAttribute
    {
        POSITION = 0,
        COLOR = 1,
        TEXCOORD = 2,
        ATTRIBUTE_COUNT = 3
    }

    public enum NkCommandType
    {
        NOP = 0,
        SCISSOR = 1,
        LINE = 2,
        CURVE = 3,
        RECT = 4,
        RECT_FILLED = 5,
        RECT_MULTI_COLOR = 6,
        CIRCLE = 7,
        CIRCLE_FILLED = 8,
        ARC = 9,
        ARC_FILLED = 10,
        TRIANGLE = 11,
        TRIANGLE_FILLED = 12,
        POLYGON = 13,
        POLYGON_FILLED = 14,
        POLYLINE = 15,
        TEXT = 16,
        IMAGE = 17,
        CUSTOM = 18
    }
    
    public enum NkFontCoordType
    {
        NK_COORD_UV = 0,
        NK_COORD_PIXEL = 1
    }

    public enum NkToggleType
    {
        NK_TOGGLE_CHECK = 0,
        NK_TOGGLE_OPTION = 1
    }



    public enum NkPropertyFilter
    {
        NK_FILTER_INT = 0,
        NK_FILTER_FLOAT = 1
    }
    
    public enum NkHeading
    {
        NK_UP = 0,
        NK_RIGHT = 1,
        NK_DOWN = 2,
        NK_LEFT = 3
    }

    public enum NkButtonBehavior
    {
        Default = 0,
        Repeater = 1
    }
    public enum NkOrientation
    {
        Vertical = 0,
        Horizontal = 1
    }
    public enum NkCollapseStates
    {
        NK_MINIMIZED = 0,
        NK_MAXIMIZED = 1
    }

    public enum NkWidgetLayoutStates
    {
        NK_WIDGET_INVALID = 0,
        NK_WIDGET_VALID = 1,
        NK_WIDGET_ROM = 2
    }

    [Flags]
    public enum NkWidgetStates
    {
        MODIFIED = 2,
        INACTIVE = 4,
        ENTERED = 8,
        HOVER = 16,
        ACTIVED = 32,
        LEFT = 64,
        HOVERED = 18,
        ACTIVE = 34
    }



    public enum NkStyleColors
    {
        TEXT = 0,
        WINDOW = 1,
        HEADER = 2,
        BORDER = 3,
        BUTTON = 4,
        BUTTON_HOVER = 5,
        BUTTON_ACTIVE = 6,
        TOGGLE = 7,
        TOGGLE_HOVER = 8,
        TOGGLE_CURSOR = 9,
        SELECT = 10,
        SELECT_ACTIVE = 11,
        SLIDER = 12,
        SLIDER_CURSOR = 13,
        SLIDER_CURSOR_HOVER = 14,
        SLIDER_CURSOR_ACTIVE = 15,
        PROPERTY = 16,
        EDIT = 17,
        EDIT_CURSOR = 18,
        COMBO = 19,
        CHART = 20,
        CHART_COLOR = 21,
        CHART_COLOR_HIGHLIGHT = 22,
        SCROLLBAR = 23,
        SCROLLBAR_CURSOR = 24,
        SCROLLBAR_CURSOR_HOVER = 25,
        SCROLLBAR_CURSOR_ACTIVE = 26,
        TAB_HEADER = 27,
        COUNT = 28
    }


    public enum NkStyleCursor
    {
        ARROW = 0,
        TEXT = 1,
        MOVE = 2,
        RESIZE_VERTICAL = 3,
        RESIZE_HORIZONTAL = 4,
        RESIZE_TOP_LEFT_DOWN_RIGHT = 5,
        RESIZE_TOP_RIGHT_DOWN_LEFT = 6,
        COUNT = 7
    }

    public enum NkSymbolType
    {
        NONE = 0,
        X = 1,
        UNDERSCORE = 2,
        CIRCLE_SOLID = 3,
        CIRCLE_OUTLINE = 4,
        RECT_SOLID = 5,
        RECT_OUTLINE = 6,
        TRIANGLE_UP = 7,
        TRIANGLE_DOWN = 8,
        TRIANGLE_LEFT = 9,
        TRIANGLE_RIGHT = 10,
        PLUS = 11,
        MINUS = 12,
        MAX = 13
    }

    public enum NkKeys
    {
        NONE = 0,
        SHIFT = 1,
        CTRL = 2,
        DEL = 3,
        ENTER = 4,
        TAB = 5,
        BACKSPACE = 6,
        COPY = 7,
        CUT = 8,
        PASTE = 9,
        UP = 10,
        DOWN = 11,
        LEFT = 12,
        RIGHT = 13,
        TEXT_INSERT_MODE = 14,
        TEXT_REPLACE_MODE = 15,
        TEXT_RESET_MODE = 16,
        TEXT_LINE_START = 17,
        TEXT_LINE_END = 18,
        TEXT_START = 19,
        TEXT_END = 20,
        TEXT_UNDO = 21,
        TEXT_REDO = 22,
        TEXT_SELECT_ALL = 23,
        TEXT_WORD_LEFT = 24,
        TEXT_WORD_RIGHT = 25,
        SCROLL_START = 26,
        SCROLL_END = 27,
        SCROLL_DOWN = 28,
        SCROLL_UP = 29,
        MAX = 30
    }

    public enum NkChartType
    {
        NK_CHART_LINES = 0,
        NK_CHART_COLUMN = 1,
        NK_CHART_MAX = 2
    }


    [Flags]
    public enum Alignment
    {
        LEFT = 1,
        CENTERED = 2,
        RIGHT = 4,
        TOP = 8,
        MIDDLE = 16,
        BOTTOM = 32,

        MIDDLELEFT = 17,
        MIDDLECENTERED = 18,
        MIDDLERIGHT = 20
    }

    [Flags]
    public enum PanelFlags : uint
    {
        BORDER = 1,
        MOVABLE = 2,
        SCALABLE = 4,
        CLOSABLE = 8,
        MINIMIZABLE = 16,
        NO_SCROLLBAR = 32,
        TITLE = 64,
        SCROLL_AUTO_HIDE = 128,
        BACKGROUND = 256,
        SCALE_LEFT = 512,
        NO_INPUT = 1024,

        PRIVATE = (1 << (11)),
        DYNAMIC = PanelFlags.PRIVATE,
        ROM = (1 << (12)),
        NOT_INTERACTIVE = PanelFlags.ROM | PanelFlags.NO_INPUT,
        HIDDEN = (1 << (13)),
        CLOSED = (1 << (14)),
        MINIMIZED = (1 << (15)),
        REMOVE_ROM = (1 << (16)),
    }

    [Flags]
    public enum NkEditState : uint
    {
        None = 0,
        ACTIVE = (1 << 0),
        INACTIVE = (1 << 1),
        ACTIVATED = (1 << 2),
        DEACTIVATED = (1 << 3),
        COMMITED = (1 << 4),
    }

    public enum NkEditFlags : uint
    {
        DEFAULT = 0,
        READ_ONLY = (1 << (0)),
        AUTO_SELECT = (1 << (1)),
        SIG_ENTER = (1 << (2)),
        ALLOW_TAB = (1 << (3)),
        NO_CURSOR = (1 << (4)),
        SELECTABLE = (1 << (5)),
        CLIPBOARD = (1 << (6)),
        CTRL_ENTER_NEWLINE = (1 << (7)),
        NO_HORIZONTAL_SCROLL = (1 << (8)),
        ALWAYS_INSERT_MODE = (1 << (9)),
        MULTILINE = (1 << (10)),
        GOTO_END_ON_ACTIVATE = (1 << (11)),
        SIMPLE = ALWAYS_INSERT_MODE,
        FIELD = SIMPLE | SELECTABLE | CLIPBOARD,

        BOX = ALWAYS_INSERT_MODE | SELECTABLE | MULTILINE | ALLOW_TAB | CLIPBOARD,

        EDITOR = SELECTABLE | MULTILINE | ALLOW_TAB | CLIPBOARD,
    }

    public enum NkChartEvent
    {
        NK_CHART_HOVERING = 1,
        NK_CHART_CLICKED = 2
    }

    public enum NkPopupType
    {
        NK_POPUP_STATIC = 0,
        NK_POPUP_DYNAMIC = 1
    }

    public enum NkShowStates
    {
        NK_HIDDEN = 0,
        NK_SHOWN = 1
    }

    public enum NkColorFormat
    {
        NK_RGB = 0,
        NK_RGBA = 1
    }

    public enum NkLayoutFormat
    {
        NK_DYNAMIC = 0,
        NK_STATIC = 1
    }
    public enum NkTreeType
    {
        NK_TREE_NODE = 0,
        NK_TREE_TAB = 1
    }
    public enum NkButtons
    {
        LEFT = 0,
        MIDDLE = 1,
        RIGHT = 2,
        DOUBLE = 3,
        MAX = 4

    }

    [Flags]
    public enum NkConvertResult
    {
        SUCCESS = 0,
        INVALID_PARAM = 1,
        COMMAND_BUFFER_FULL = 2,
        VERTEX_BUFFER_FULL = 4,
        ELEMENT_BUFFER_FULL = 8
    }

    [Flags]
    public enum NkPanelType
    {
        WINDOW = (1 << (0)),
        GROUP = (1 << (1)),
        POPUP = (1 << (2)),
        CONTEXTUAL = (1 << (4)),
        COMBO = (1 << (5)),
        MENU = (1 << (6)),
        TOOLTIP = (1 << (7)),

        SET_NONBLOCK =    NkPanelType.CONTEXTUAL | NkPanelType.COMBO | NkPanelType.MENU | NkPanelType.TOOLTIP,

        SET_POPUP = NkPanelType.SET_NONBLOCK | NkPanelType.POPUP,
        SET_SUB = NkPanelType.SET_POPUP | NkPanelType.GROUP,
    }

    public enum FontAtlasFormat
    {
        Alpha8,
        Rgba32
    }


    public enum NkPanelRowLayoutType
    {
        DYNAMIC_FIXED = 0,
        DYNAMIC_ROW = 1,
        DYNAMIC_FREE = 2,
        DYNAMIC = 3,
        STATIC_FIXED = 4,
        STATIC_ROW = 5,
        STATIC_FREE = 6,
        STATIC = 7,
        TEMPLATE = 8,
        COUNT = 9
    }

    public enum NkPropertyKind
    {
        NK_PROPERTY_INT = 0,
        NK_PROPERTY_FLOAT = 1,
        NK_PROPERTY_DOUBLE = 2
    }

    public enum NkPropertyStatus
    {
        NK_PROPERTY_DEFAULT = 0,
        NK_PROPERTY_EDIT = 1,
        NK_PROPERTY_DRAG = 2
    }

    public enum NkTextEditType
    {
        SINGLE_LINE = 0,
        MULTI_LINE = 1
    }

    public enum NkTextEditMode
    {
        VIEW = 0,
        INSERT = 1,
        REPLACE = 2
    }

    public enum NkStyleItemType
    {
        COLOR = 0,
        IMAGE = 1
    }

    public enum NkStyleHeaderAlign
    {
        LEFT = 0,
        RIGHT = 1
    }

    unsafe partial class Nk
    {
        public const int nk_false = 0;
        public const int nk_true = 1;
    
        public const int NK_RP_HEURISTIC_Skyline_default = 0;
        public const int NK_RP_HEURISTIC_Skyline_BL_sortHeight = NK_RP_HEURISTIC_Skyline_default;
        public const int NK_RP_HEURISTIC_Skyline_BF_sortHeight = 2;
        public const int NK_RP__INIT_skyline = 1;
        public const int NK_TT_vmove = 1;
        public const int NK_TT_vline = 2;
        public const int NK_TT_vcurve = 3;
        public const int NK_TT_PLATFORM_ID_UNICODE = 0;
        public const int NK_TT_PLATFORM_ID_MAC = 1;
        public const int NK_TT_PLATFORM_ID_ISO = 2;
        public const int NK_TT_PLATFORM_ID_MICROSOFT = 3;
        public const int NK_TT_UNICODE_EID_UNICODE_1_0 = 0;
        public const int NK_TT_UNICODE_EID_UNICODE_1_1 = 1;
        public const int NK_TT_UNICODE_EID_ISO_10646 = 2;
        public const int NK_TT_UNICODE_EID_UNICODE_2_0_BMP = 3;
        public const int NK_TT_UNICODE_EID_UNICODE_2_0_FULL = 4;
        public const int NK_TT_MS_EID_SYMBOL = 0;
        public const int NK_TT_MS_EID_UNICODE_BMP = 1;
        public const int NK_TT_MS_EID_SHIFTJIS = 2;
        public const int NK_TT_MS_EID_UNICODE_FULL = 10;
        public const int NK_TT_MAC_EID_ROMAN = 0;
        public const int NK_TT_MAC_EID_ARABIC = 4;
        public const int NK_TT_MAC_EID_JAPANESE = 1;
        public const int NK_TT_MAC_EID_HEBREW = 5;
        public const int NK_TT_MAC_EID_CHINESE_TRAD = 2;
        public const int NK_TT_MAC_EID_GREEK = 6;
        public const int NK_TT_MAC_EID_KOREAN = 3;
        public const int NK_TT_MAC_EID_RUSSIAN = 7;
        public const int NK_TT_MS_LANG_ENGLISH = 0x0409;
        public const int NK_TT_MS_LANG_ITALIAN = 0x0410;
        public const int NK_TT_MS_LANG_CHINESE = 0x0804;
        public const int NK_TT_MS_LANG_JAPANESE = 0x0411;
        public const int NK_TT_MS_LANG_DUTCH = 0x0413;
        public const int NK_TT_MS_LANG_KOREAN = 0x0412;
        public const int NK_TT_MS_LANG_FRENCH = 0x040c;
        public const int NK_TT_MS_LANG_RUSSIAN = 0x0419;
        public const int NK_TT_MS_LANG_GERMAN = 0x0407;
        public const int NK_TT_MS_LANG_SPANISH = 0x0409;
        public const int NK_TT_MS_LANG_HEBREW = 0x040d;
        public const int NK_TT_MS_LANG_SWEDISH = 0x041D;
        public const int NK_TT_MAC_LANG_ENGLISH = 0;
        public const int NK_TT_MAC_LANG_JAPANESE = 11;
        public const int NK_TT_MAC_LANG_ARABIC = 12;
        public const int NK_TT_MAC_LANG_KOREAN = 23;
        public const int NK_TT_MAC_LANG_DUTCH = 4;
        public const int NK_TT_MAC_LANG_RUSSIAN = 32;
        public const int NK_TT_MAC_LANG_FRENCH = 1;
        public const int NK_TT_MAC_LANG_SPANISH = 6;
        public const int NK_TT_MAC_LANG_GERMAN = 2;
        public const int NK_TT_MAC_LANG_SWEDISH = 5;
        public const int NK_TT_MAC_LANG_HEBREW = 10;
        public const int NK_TT_MAC_LANG_CHINESE_SIMPLIFIED = 33;
        public const int NK_TT_MAC_LANG_ITALIAN = 3;
        public const int NK_TT_MAC_LANG_CHINESE_TRAD = 19;
        //public const int NK_TOGGLE_CHECK = 0;
        //public const int NK_TOGGLE_OPTION = 1;
        //public const int NK_PROPERTY_DEFAULT = 0;
        //public const int NK_PROPERTY_EDIT = 1;
        //public const int NK_PROPERTY_DRAG = 2;
        //public const int NK_FILTER_INT = 0;
        //public const int NK_FILTER_FLOAT = 1;
        //public const int NK_PROPERTY_INT = 0;
        //public const int NK_PROPERTY_FLOAT = 1;
        //public const int NK_PROPERTY_DOUBLE = 2;
        public const int NK_INSERT_BACK = 0;
        public const int NK_INSERT_FRONT = 1;
    }
}