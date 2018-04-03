using System;

namespace NuklearSharp
{



    public enum VertexLayoutFormat
    {
        //public const int NkDrawVertexLayoutFormat.SCHAR = 0;
        //public const int NkDrawVertexLayoutFormat.SSHORT = 1;
        //public const int NkDrawVertexLayoutFormat.SINT = 2;
        //public const int NkDrawVertexLayoutFormat.UCHAR = 3;
        //public const int NkDrawVertexLayoutFormat.USHORT = 4;
        //public const int NkDrawVertexLayoutFormat.UINT = 5;
        //public const int NkDrawVertexLayoutFormat.FLOAT = 6;
        //public const int NkDrawVertexLayoutFormat.DOUBLE = 7;
        //public const int NkDrawVertexLayoutFormat.COLOR_BEGIN = 8;
        //public const int NkDrawVertexLayoutFormat.R8G8B8 = NkDrawVertexLayoutFormat.COLOR_BEGIN;
        //public const int NkDrawVertexLayoutFormat.R16G15B16 = 10;
        //public const int NkDrawVertexLayoutFormat.R32G32B32 = 11;
        //public const int NkDrawVertexLayoutFormat.R8G8B8A8 = 12;
        //public const int NkDrawVertexLayoutFormat.B8G8R8A8 = 13;
        //public const int NkDrawVertexLayoutFormat.R16G15B16A16 = 14;
        //public const int NkDrawVertexLayoutFormat.R32G32B32A32 = 15;
        //public const int NkDrawVertexLayoutFormat.R32G32B32A32_FLOAT = 16;
        //public const int NkDrawVertexLayoutFormat.R32G32B32A32_DOUBLE = 17;
        //public const int NkDrawVertexLayoutFormat.RGB32 = 18;
        //public const int NkDrawVertexLayoutFormat.RGBA32 = 19;
        //public const int NkDrawVertexLayoutFormat.COLOR_END = NkDrawVertexLayoutFormat.RGBA32;
        //public const int NkDrawVertexLayoutFormat.COUNT = 21;
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

    //public enum NkAllocationType
    //{
    //    NK_BUFFER_FIXED = 0,
    //    NK_BUFFER_DYNAMIC = 1
    //}

    //public enum NkAntiAliasing
    //{
    //    NK_ANTI_ALIASING_OFF = 0,
    //    NK_ANTI_ALIASING_ON = 1
    //}

    //public enum NkDrawVertexLayoutAttribute
    //{
    //    NK_VERTEX_POSITION = 0,
    //    NK_VERTEX_COLOR = 1,
    //    NK_VERTEX_TEXCOORD = 2,
    //    NK_VERTEX_ATTRIBUTE_COUNT = 3
    //}

    //public enum NkStyleItemType
    //{
    //    NK_STYLE_ITEM_COLOR = 0,
    //    NK_STYLE_ITEM_IMAGE = 1
    //}

    //[Flags]
    //public enum NkPanelType
    //{
    //    NK_PANEL_WINDOW = 1,
    //    NK_PANEL_GROUP = 2,
    //    NK_PANEL_POPUP = 4,
    //    NK_PANEL_CONTEXTUAL = 16,
    //    NK_PANEL_COMBO = 32,
    //    NK_PANEL_MENU = 64,
    //    NK_PANEL_TOOLTIP = 128
    //}

    //public enum NkPanelRowLayoutType
    //{
    //    NK_LAYOUT_DYNAMIC_FIXED = 0,
    //    NK_LAYOUT_DYNAMIC_ROW = 1,
    //    NK_LAYOUT_DYNAMIC_FREE = 2,
    //    NK_LAYOUT_DYNAMIC = 3,
    //    NK_LAYOUT_STATIC_FIXED = 4,
    //    NK_LAYOUT_STATIC_ROW = 5,
    //    NK_LAYOUT_STATIC_FREE = 6,
    //    NK_LAYOUT_STATIC = 7,
    //    NK_LAYOUT_TEMPLATE = 8,
    //    NK_LAYOUT_COUNT = 9
    //}



    //public enum NkSymbolType
    //{
    //    NkSymbolType.NONE = 0,
    //    NkSymbolType.X = 1,
    //    NkSymbolType.UNDERSCORE = 2,
    //    NkSymbolType.CIRCLE_SOLID = 3,
    //    NkSymbolType.CIRCLE_OUTLINE = 4,
    //    NkSymbolType.RECT_SOLID = 5,
    //    NkSymbolType.RECT_OUTLINE = 6,
    //    NkSymbolType.TRIANGLE_UP = 7,
    //    NkSymbolType.TRIANGLE_DOWN = 8,
    //    NkSymbolType.TRIANGLE_LEFT = 9,
    //    NkSymbolType.TRIANGLE_RIGHT = 10,
    //    NkSymbolType.PLUS = 11,
    //    NkSymbolType.MINUS = 12,
    //    NkSymbolType.MAX = 13
    //}

    //public enum NkStyleHeaderAlign
    //{
    //    NK_HEADER_LEFT = 0,
    //    NK_HEADER_RIGHT = 1
    //}

    //public enum NkButtonBehavior
    //{
    //    NkButtons.DEFAULT = 0,
    //    NkButtons.REPEATER = 1
    //}

    ////public enum Nk
    ////{
    ////    nk_false = 0,
    ////    nk_true = 1
    ////}



    //public enum NkModify
    //{
    //    NK_FIXED = 0,
    //    NK_MODIFIABLE = 1
    //}





    //public enum NkShowStates
    //{
    //    NK_HIDDEN = 0,
    //    NK_SHOWN = 1
    //}

    //public enum NkChartEvent
    //{
    //    NK_CHART_HOVERING = 1,
    //    NK_CHART_CLICKED = 2
    //}

    //public enum NkColorFormat
    //{
    //    NK_RGB = 0,
    //    NK_RGBA = 1
    //}

    //public enum NkPopupType
    //{
    //    NK_POPUP_STATIC = 0,
    //    NK_POPUP_DYNAMIC = 1
    //}

    //public enum NkLayoutFormat
    //{
    //    NK_DYNAMIC = 0,
    //    NK_STATIC = 1
    //}

    //public enum NkTreeType
    //{
    //    NK_TREE_NODE = 0,
    //    NK_TREE_TAB = 1
    //}



    //public enum NkButtons
    //{
    //    NkButtons.LEFT = 0,
    //    NkButtons.MIDDLE = 1,
    //    NkButtons.RIGHT = 2,
    //    NkButtons.DOUBLE = 3,
    //    NkButtons.MAX = 4
    //}

    //[Flags]
    //public enum NkConvertResult
    //{
    //    NkConvertResult.SUCCESS = 0,
    //    NkConvertResult.INVALID_PARAM = 1,
    //    NkConvertResult.COMMAND_BUFFER_FULL = 2,
    //    NkConvertResult.VERTEX_BUFFER_FULL = 4,
    //    NkConvertResult.ELEMENT_BUFFER_FULL = 8
    //}

    //public enum NkCommandType
    //{
    //    NK_COMMAND_NOP = 0,
    //    NK_COMMAND_SCISSOR = 1,
    //    NK_COMMAND_LINE = 2,
    //    NK_COMMAND_CURVE = 3,
    //    NK_COMMAND_RECT = 4,
    //    NK_COMMAND_RECT_FILLED = 5,
    //    NK_COMMAND_RECT_MULTI_COLOR = 6,
    //    NK_COMMAND_CIRCLE = 7,
    //    NK_COMMAND_CIRCLE_FILLED = 8,
    //    NK_COMMAND_ARC = 9,
    //    NK_COMMAND_ARC_FILLED = 10,
    //    NK_COMMAND_TRIANGLE = 11,
    //    NK_COMMAND_TRIANGLE_FILLED = 12,
    //    NK_COMMAND_POLYGON = 13,
    //    NK_COMMAND_POLYGON_FILLED = 14,
    //    NK_COMMAND_POLYLINE = 15,
    //    NK_COMMAND_TEXT = 16,
    //    NK_COMMAND_IMAGE = 17,
    //    NK_COMMAND_CUSTOM = 18
    //}






    //[Flags]
    //public enum NkEditFlags
    //{
    //    NkEditFlags.DEFAULT = 0,
    //    NkEditFlags.READ_ONLY = 1,
    //    NkEditFlags.AUTO_SELECT = 2,
    //    NkEditFlags.SIG_ENTER = 4,
    //    NkEditFlags.ALLOW_TAB = 8,
    //    NkEditFlags.NO_CURSOR = 16,
    //    NkEditFlags.SELECTABLE = 32,
    //    NkEditFlags.CLIPBOARD = 64,
    //    NkEditFlags.CTRL_ENTER_NEWLINE = 128,
    //    NkEditFlags.NO_HORIZONTAL_SCROLL = 256,
    //    NkEditFlags.ALWAYS_INSERT_MODE = 512,
    //    NkEditFlags.MULTILINE = 1024,
    //    NkEditFlags.GOTO_END_ON_ACTIVATE = 2048
    //}

    //public enum NkEditTypes
    //{
    //    NkEditFlags.SIMPLE = 512,
    //    NkEditFlags.FIELD = 608,
    //    NkEditFlags.BOX = 1640,
    //    NkEditFlags.EDITOR = 1128
    //}

    //[Flags]
    //public enum NkEditEvents
    //{
    //    NkEditFlags.ACTIVE = 1,
    //    NkEditFlags.INACTIVE = 2,
    //    NkEditFlags.ACTIVATED = 4,
    //    NkEditFlags.DEACTIVATED = 8,
    //    NkEditFlags.COMMITED = 16
    //}

    //public enum NkStyleColors
    //{
    //    NkStyleColors.TEXT = 0,
    //    NkStyleColors.WINDOW = 1,
    //    NkStyleColors.HEADER = 2,
    //    NkStyleColors.BORDER = 3,
    //    NkStyleColors.BUTTON = 4,
    //    NkStyleColors.BUTTON_HOVER = 5,
    //    NkStyleColors.BUTTON_ACTIVE = 6,
    //    NkStyleColors.TOGGLE = 7,
    //    NkStyleColors.TOGGLE_HOVER = 8,
    //    NkStyleColors.TOGGLE_CURSOR = 9,
    //    NkStyleColors.SELECT = 10,
    //    NkStyleColors.SELECT_ACTIVE = 11,
    //    NkStyleColors.SLIDER = 12,
    //    NkStyleColors.SLIDER_CURSOR = 13,
    //    NkStyleColors.SLIDER_CURSOR_HOVER = 14,
    //    NkStyleColors.SLIDER_CURSOR_ACTIVE = 15,
    //    NkStyleColors.PROPERTY = 16,
    //    NkStyleColors.EDIT = 17,
    //    NkStyleColors.EDIT_CURSOR = 18,
    //    NkStyleColors.COMBO = 19,
    //    NkStyleColors.CHART = 20,
    //    NkStyleColors.CHART_COLOR = 21,
    //    NkStyleColors.CHART_COLOR_HIGHLIGHT = 22,
    //    NkStyleColors.SCROLLBAR = 23,
    //    NkStyleColors.SCROLLBAR_CURSOR = 24,
    //    NkStyleColors.SCROLLBAR_CURSOR_HOVER = 25,
    //    NkStyleColors.SCROLLBAR_CURSOR_ACTIVE = 26,
    //    NkStyleColors.TAB_HEADER = 27,
    //    NkStyleColors.COUNT = 28
    //}

    //public enum NkStyleCursor
    //{
    //    NkStyleCursor.ARROW = 0,
    //    NkStyleCursor.TEXT = 1,
    //    NkStyleCursor.MOVE = 2,
    //    NkStyleCursor.RESIZE_VERTICAL = 3,
    //    NkStyleCursor.RESIZE_HORIZONTAL = 4,
    //    NkStyleCursor.RESIZE_TOP_LEFT_DOWN_RIGHT = 5,
    //    NkStyleCursor.RESIZE_TOP_RIGHT_DOWN_LEFT = 6,
    //    NkStyleCursor.COUNT = 7
    //}

    public enum NkFontCoordType
    {
        NK_COORD_UV = 0,
        NK_COORD_PIXEL = 1
    }

    //public enum NkFontAtlasFormat
    //{
    //    NK_FONT_ATLAS_ALPHA8 = 0,
    //    NK_FONT_ATLAS_RGBA32 = 1
    //}

    //public enum NkBufferAllocationType
    //{
    //    NK_BUFFER_FRONT = 0,
    //    NK_BUFFER_BACK = 1,
    //    NK_BUFFER_MAX = 2
    //}

    //public enum NkTextEditType
    //{
    //    NK_TEXT_EDIT_SINGLE_LINE = 0,
    //    NK_TEXT_EDIT_MULTI_LINE = 1
    //}

    //public enum NkTextEditMode
    //{
    //    NK_TEXT_EDIT_MODE_VIEW = 0,
    //    NK_TEXT_EDIT_MODE_INSERT = 1,
    //    NK_TEXT_EDIT_MODE_REPLACE = 2
    //}

    //public enum NkCommandClipping
    //{
    //    NK_CLIPPING_OFF = 0,
    //    NK_CLIPPING_ON = 1
    //}

    //public enum NkDrawListStroke
    //{
    //    NK_STROKE_OPEN = 0,
    //    NK_STROKE_CLOSED = 1
    //}

    //public enum NkPanelSet
    //{
    //    NK_PANEL_SET_NONBLOCK = 240,
    //    NK_PANEL_SET_POPUP = 244,
    //    NK_PANEL_SET_SUB = 246
    //}

    //public enum NkWindowFlags
    //{
    //    NkPanelFlags.PRIVATE = 2048,
    //    NkPanelFlags.DYNAMIC = 2048,
    //    NkPanelFlags.ROM = 4096,
    //    NkPanelFlags.NOT_INTERACTIVE = 5120,
    //    NkPanelFlags.HIDDEN = 8192,
    //    NkPanelFlags.CLOSED = 16384,
    //    NkPanelFlags.MINIMIZED = 32768,
    //    NkPanelFlags.REMOVE_ROM = 65536
    //}


    //public enum NkToggleType
    //{
    //    NK_TOGGLE_CHECK = 0,
    //    NK_TOGGLE_OPTION = 1
    //}

    //public enum NkPropertyStatus
    //{
    //    NK_PROPERTY_DEFAULT = 0,
    //    NK_PROPERTY_EDIT = 1,
    //    NK_PROPERTY_DRAG = 2
    //}

    //public enum NkPropertyFilter
    //{
    //    NK_FILTER_INT = 0,
    //    NK_FILTER_FLOAT = 1
    //}

    //public enum NkPropertyKind
    //{
    //    NK_PROPERTY_INT = 0,
    //    NK_PROPERTY_FLOAT = 1,
    //    NK_PROPERTY_DOUBLE = 2
    //}

    //public enum NkWindowInsertLocation
    //{
    //    NK_INSERT_BACK = 0,
    //    NK_INSERT_FRONT = 1
    //}

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

    unsafe partial class Nk
    {
        public const int nk_false = 0;
        public const int nk_true = 1;
        //public const int NK_UP = 0;
        //public const int NK_RIGHT = 1;
        //public const int NK_DOWN = 2;
        //public const int NK_LEFT = 3;
        //public const int NkButtons.DEFAULT = 0;
        //public const int NkButtons.REPEATER = 1;
        //  public const int NK_FIXED = nk_false;
        //  public const int NK_MODIFIABLE = nk_true;
        //public const int NK_VERTICAL = 0;
        //public const int NK_HORIZONTAL = 1;
        //public const int NK_MINIMIZED = nk_false;
        //public const int NK_MAXIMIZED = nk_true;
        //public const int NK_HIDDEN = nk_false;
        //public const int NK_SHOWN = nk_true;
        //public const int NK_CHART_LINES = 0;
        //public const int NK_CHART_COLUMN = 1;
        //public const int NK_CHART_MAX = 2;
        //public const int NK_CHART_HOVERING = 0x01;
        //public const int NK_CHART_CLICKED = 0x02;
        //public const int NK_RGB = 0;
        //public const int NK_RGBA = 1;
        //public const int NK_POPUP_STATIC = 0;
        //public const int NK_POPUP_DYNAMIC = 1;
        //public const int NK_DYNAMIC = 0;
        //public const int NK_STATIC = 1;
        //public const int NK_TREE_NODE = 0;
        //public const int NK_TREE_TAB = 1;
        //public const int NkSymbolType.NONE = 0;
        //public const int NkSymbolType.X = 1;
        //public const int NkSymbolType.UNDERSCORE = 2;
        //public const int NkSymbolType.CIRCLE_SOLID = 3;
        //public const int NkSymbolType.CIRCLE_OUTLINE = 4;
        //public const int NkSymbolType.RECT_SOLID = 5;
        //public const int NkSymbolType.RECT_OUTLINE = 6;
        //public const int NkSymbolType.TRIANGLE_UP = 7;
        //public const int NkSymbolType.TRIANGLE_DOWN = 8;
        //public const int NkSymbolType.TRIANGLE_LEFT = 9;
        //public const int NkSymbolType.TRIANGLE_RIGHT = 10;
        //public const int NkSymbolType.PLUS = 11;
        //public const int NkSymbolType.MINUS = 12;
        //public const int NkSymbolType.MAX = 13;
        //public const int NkKeys.NONE = 0;
        //public const int NkKeys.SHIFT = 1;
        //public const int NkKeys.CTRL = 2;
        //public const int NkKeys.DEL = 3;
        //public const int NkKeys.ENTER = 4;
        //public const int NkKeys.TAB = 5;
        //public const int NkKeys.BACKSPACE = 6;
        //public const int NkKeys.COPY = 7;
        //public const int NkKeys.CUT = 8;
        //public const int NkKeys.PASTE = 9;
        //public const int NkKeys.UP = 10;
        //public const int NkKeys.DOWN = 11;
        //public const int NkKeys.LEFT = 12;
        //public const int NkKeys.RIGHT = 13;
        //public const int NkKeys.TEXT_INSERT_MODE = 14;
        //public const int NkKeys.TEXT_REPLACE_MODE = 15;
        //public const int NkKeys.TEXT_RESET_MODE = 16;
        //public const int NkKeys.TEXT_LINE_START = 17;
        //public const int NkKeys.TEXT_LINE_END = 18;
        //public const int NkKeys.TEXT_START = 19;
        //public const int NkKeys.TEXT_END = 20;
        //public const int NkKeys.TEXT_UNDO = 21;
        //public const int NkKeys.TEXT_REDO = 22;
        //public const int NkKeys.TEXT_SELECT_ALL = 23;
        //public const int NkKeys.TEXT_WORD_LEFT = 24;
        //public const int NkKeys.TEXT_WORD_RIGHT = 25;
        //public const int NkKeys.SCROLL_START = 26;
        //public const int NkKeys.SCROLL_END = 27;
        //public const int NkKeys.SCROLL_DOWN = 28;
        //public const int NkKeys.SCROLL_UP = 29;
        //public const int NkKeys.MAX = 30;
        //public const int NkButtons.LEFT = 0;
        //public const int NkButtons.MIDDLE = 1;
        //public const int NkButtons.RIGHT = 2;
        //public const int NkButtons.DOUBLE = 3;
        //public const int NkButtons.MAX = 4;
        //public const int NK_ANTI_ALIASING_OFF = 0;
        //public const int NK_ANTI_ALIASING_ON = 1;
        //public const int NkConvertResult.SUCCESS = 0;
        //public const int NkConvertResult.INVALID_PARAM = 1;
        //public const int NkConvertResult.COMMAND_BUFFER_FULL = (1 << (1));
        //public const int NkConvertResult.VERTEX_BUFFER_FULL = (1 << (2));
        //public const int NkConvertResult.ELEMENT_BUFFER_FULL = (1 << (3));
        //public const int NkPanelFlags.BORDER = (1 << (0));
        //public const int NkPanelFlags.MOVABLE = (1 << (1));
        //public const int NkPanelFlags.SCALABLE = (1 << (2));
        //public const int NkPanelFlags.CLOSABLE = (1 << (3));
        //public const int NkPanelFlags.MINIMIZABLE = (1 << (4));
        //public const int NkPanelFlags.NO_SCROLLBAR = (1 << (5));
        //public const int NkPanelFlags.TITLE = (1 << (6));
        //public const int NkPanelFlags.SCROLL_AUTO_HIDE = (1 << (7));
        //public const int NkPanelFlags.BACKGROUND = (1 << (8));
        //public const int NkPanelFlags.SCALE_LEFT = (1 << (9));
        //public const int NkPanelFlags.NO_INPUT = (1 << (10));
        public const int NK_WIDGET_INVALID = 0;
        public const int NK_WIDGET_VALID = 1;
        public const int NK_WIDGET_ROM = 2;

        //public const int NkTextAlign.LEFT = 0x01;
        //public const int NkTextAlign.CENTERED = 0x02;
        //public const int NkTextAlign.RIGHT = 0x04;
        //public const int NkTextAlign.TOP = 0x08;
        //public const int NkTextAlign.MIDDLE = 0x10;
        //public const int NkTextAlign.BOTTOM = 0x20;
        //public const int NkTextAlign.MIDDLELEFT = NkTextAlign.MIDDLE | NkTextAlign.LEFT;
        //public const int NkTextAlign.MIDDLECENTERED = NkTextAlign.MIDDLE | NkTextAlign.CENTERED;
        //public const int NkTextAlign.MIDDLERIGHT = NkTextAlign.MIDDLE | NkTextAlign.RIGHT;
        //public const int NkEditFlags.DEFAULT = 0;
        //public const int NkEditFlags.READ_ONLY = (1 << (0));
        //public const int NkEditFlags.AUTO_SELECT = (1 << (1));
        //public const int NkEditFlags.SIG_ENTER = (1 << (2));
        //public const int NkEditFlags.ALLOW_TAB = (1 << (3));
        //public const int NkEditFlags.NO_CURSOR = (1 << (4));
        //public const int NkEditFlags.SELECTABLE = (1 << (5));
        //public const int NkEditFlags.CLIPBOARD = (1 << (6));
        //public const int NkEditFlags.CTRL_ENTER_NEWLINE = (1 << (7));
        //public const int NkEditFlags.NO_HORIZONTAL_SCROLL = (1 << (8));
        //public const int NkEditFlags.ALWAYS_INSERT_MODE = (1 << (9));
        //public const int NkEditFlags.MULTILINE = (1 << (10));
        //public const int NkEditFlags.GOTO_END_ON_ACTIVATE = (1 << (11));
        //public const int NkEditFlags.SIMPLE = NkEditFlags.ALWAYS_INSERT_MODE;
        //public const int NkEditFlags.FIELD = NkEditFlags.SIMPLE | NkEditFlags.SELECTABLE | NkEditFlags.CLIPBOARD;

        //public const int NkEditFlags.BOX =
        //    NkEditFlags.ALWAYS_INSERT_MODE | NkEditFlags.SELECTABLE | NkEditFlags.MULTILINE |
        //    NkEditFlags.ALLOW_TAB | NkEditFlags.CLIPBOARD;

        //public const int NkEditFlags.EDITOR =
        //    NkEditFlags.SELECTABLE | NkEditFlags.MULTILINE | NkEditFlags.ALLOW_TAB | NkEditFlags.CLIPBOARD;

        //public const int NkEditFlags.ACTIVE = (1 << (0));
        //public const int NkEditFlags.INACTIVE = (1 << (1));
        //public const int NkEditFlags.ACTIVATED = (1 << (2));
        //public const int NkEditFlags.DEACTIVATED = (1 << (3));
        //public const int NkEditFlags.COMMITED = (1 << (4));

        //public const int NkStyleCursor.ARROW = 0;
        //public const int NkStyleCursor.TEXT = 1;
        //public const int NkStyleCursor.MOVE = 2;
        //public const int NkStyleCursor.RESIZE_VERTICAL = 3;
        //public const int NkStyleCursor.RESIZE_HORIZONTAL = 4;
        //public const int NkStyleCursor.RESIZE_TOP_LEFT_DOWN_RIGHT = 5;
        //public const int NkStyleCursor.RESIZE_TOP_RIGHT_DOWN_LEFT = 6;
        //public const int NkStyleCursor.COUNT = 7;
        public const int NK_COORD_UV = 0;
        public const int NK_COORD_PIXEL = 1;
        public const int NK_FONT_ATLAS_ALPHA8 = 0;
        public const int NK_FONT_ATLAS_RGBA32 = 1;
        public const int NK_BUFFER_FIXED = 0;
        public const int NK_BUFFER_DYNAMIC = 1;
        public const int NK_BUFFER_FRONT = 0;
        public const int NK_BUFFER_BACK = 1;
        public const int NK_BUFFER_MAX = 2;
        public const int NK_TEXT_EDIT_SINGLE_LINE = 0;
        public const int NK_TEXT_EDIT_MULTI_LINE = 1;
        public const int NK_TEXT_EDIT_MODE_VIEW = 0;
        public const int NK_TEXT_EDIT_MODE_INSERT = 1;
        public const int NK_TEXT_EDIT_MODE_REPLACE = 2;
        public const int NK_COMMAND_NOP = 0;
        public const int NK_COMMAND_SCISSOR = 1;
        public const int NK_COMMAND_LINE = 2;
        public const int NK_COMMAND_CURVE = 3;
        public const int NK_COMMAND_RECT = 4;
        public const int NK_COMMAND_RECT_FILLED = 5;
        public const int NK_COMMAND_RECT_MULTI_COLOR = 6;
        public const int NK_COMMAND_CIRCLE = 7;
        public const int NK_COMMAND_CIRCLE_FILLED = 8;
        public const int NK_COMMAND_ARC = 9;
        public const int NK_COMMAND_ARC_FILLED = 10;
        public const int NK_COMMAND_TRIANGLE = 11;
        public const int NK_COMMAND_TRIANGLE_FILLED = 12;
        public const int NK_COMMAND_POLYGON = 13;
        public const int NK_COMMAND_POLYGON_FILLED = 14;
        public const int NK_COMMAND_POLYLINE = 15;
        public const int NK_COMMAND_TEXT = 16;
        public const int NK_COMMAND_IMAGE = 17;
        public const int NK_COMMAND_CUSTOM = 18;
        public const int NK_CLIPPING_OFF = nk_false;
        public const int NK_CLIPPING_ON = nk_true;
        public const int NK_STROKE_OPEN = nk_false;
        public const int NK_STROKE_CLOSED = nk_true;
        public const int NK_VERTEX_POSITION = 0;
        public const int NK_VERTEX_COLOR = 1;
        public const int NK_VERTEX_TEXCOORD = 2;
        public const int NK_VERTEX_ATTRIBUTE_COUNT = 3;

        public const int NK_STYLE_ITEM_COLOR = 0;
        public const int NK_STYLE_ITEM_IMAGE = 1;
        public const int NK_HEADER_LEFT = 0;
        public const int NK_HEADER_RIGHT = 1;
        public const int NK_PANEL_WINDOW = (1 << (0));
        public const int NK_PANEL_GROUP = (1 << (1));
        public const int NK_PANEL_POPUP = (1 << (2));
        public const int NK_PANEL_CONTEXTUAL = (1 << (4));
        public const int NK_PANEL_COMBO = (1 << (5));
        public const int NK_PANEL_MENU = (1 << (6));
        public const int NK_PANEL_TOOLTIP = (1 << (7));

        public const int NK_PANEL_SET_NONBLOCK =
            NK_PANEL_CONTEXTUAL | NK_PANEL_COMBO | NK_PANEL_MENU | NK_PANEL_TOOLTIP;

        public const int NK_PANEL_SET_POPUP = NK_PANEL_SET_NONBLOCK | NK_PANEL_POPUP;
        public const int NK_PANEL_SET_SUB = NK_PANEL_SET_POPUP | NK_PANEL_GROUP;
        public const int NK_LAYOUT_DYNAMIC_FIXED = 0;
        public const int NK_LAYOUT_DYNAMIC_ROW = 1;
        public const int NK_LAYOUT_DYNAMIC_FREE = 2;
        public const int NK_LAYOUT_DYNAMIC = 3;
        public const int NK_LAYOUT_STATIC_FIXED = 4;
        public const int NK_LAYOUT_STATIC_ROW = 5;
        public const int NK_LAYOUT_STATIC_FREE = 6;
        public const int NK_LAYOUT_STATIC = 7;
        public const int NK_LAYOUT_TEMPLATE = 8;
        public const int NK_LAYOUT_COUNT = 9;
        //public const int NkPanelFlags.PRIVATE = (1 << (11));
        //public const int NkPanelFlags.DYNAMIC = NkPanelFlags.PRIVATE;
        //public const int NkPanelFlags.ROM = (1 << (12));
        //public const int NkPanelFlags.NOT_INTERACTIVE = NkPanelFlags.ROM | NkPanelFlags.NO_INPUT;
        //public const int NkPanelFlags.HIDDEN = (1 << (13));
        //public const int NkPanelFlags.CLOSED = (1 << (14));
        //public const int NkPanelFlags.MINIMIZED = (1 << (15));
        //public const int NkPanelFlags.REMOVE_ROM = (1 << (16));
        public const int NK_DO_NOT_STOP_ON_NEW_LINE = 0;
        public const int NK_STOP_ON_NEW_LINE = 1;
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
        public const int NK_TOGGLE_CHECK = 0;
        public const int NK_TOGGLE_OPTION = 1;
        public const int NK_PROPERTY_DEFAULT = 0;
        public const int NK_PROPERTY_EDIT = 1;
        public const int NK_PROPERTY_DRAG = 2;
        public const int NK_FILTER_INT = 0;
        public const int NK_FILTER_FLOAT = 1;
        public const int NK_PROPERTY_INT = 0;
        public const int NK_PROPERTY_FLOAT = 1;
        public const int NK_PROPERTY_DOUBLE = 2;
        public const int NK_INSERT_BACK = 0;
        public const int NK_INSERT_FRONT = 1;
    }
}