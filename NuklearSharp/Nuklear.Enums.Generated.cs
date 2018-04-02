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

    //public enum NkChartType
    //{
    //    NK_CHART_LINES = 0,
    //    NK_CHART_COLUMN = 1,
    //    NK_CHART_MAX = 2
    //}

    //public enum NkSymbolType
    //{
    //    NK_SYMBOL_NONE = 0,
    //    NK_SYMBOL_X = 1,
    //    NK_SYMBOL_UNDERSCORE = 2,
    //    NK_SYMBOL_CIRCLE_SOLID = 3,
    //    NK_SYMBOL_CIRCLE_OUTLINE = 4,
    //    NK_SYMBOL_RECT_SOLID = 5,
    //    NK_SYMBOL_RECT_OUTLINE = 6,
    //    NK_SYMBOL_TRIANGLE_UP = 7,
    //    NK_SYMBOL_TRIANGLE_DOWN = 8,
    //    NK_SYMBOL_TRIANGLE_LEFT = 9,
    //    NK_SYMBOL_TRIANGLE_RIGHT = 10,
    //    NK_SYMBOL_PLUS = 11,
    //    NK_SYMBOL_MINUS = 12,
    //    NK_SYMBOL_MAX = 13
    //}

    //public enum NkStyleHeaderAlign
    //{
    //    NK_HEADER_LEFT = 0,
    //    NK_HEADER_RIGHT = 1
    //}

    //public enum NkButtonBehavior
    //{
    //    NK_BUTTON_DEFAULT = 0,
    //    NK_BUTTON_REPEATER = 1
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

    //public enum NkOrientation
    //{
    //    NK_VERTICAL = 0,
    //    NK_HORIZONTAL = 1
    //}

    //public enum NkCollapseStates
    //{
    //    NK_MINIMIZED = 0,
    //    NK_MAXIMIZED = 1
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

    //public enum NkKeys
    //{
    //    NK_KEY_NONE = 0,
    //    NK_KEY_SHIFT = 1,
    //    NK_KEY_CTRL = 2,
    //    NK_KEY_DEL = 3,
    //    NK_KEY_ENTER = 4,
    //    NK_KEY_TAB = 5,
    //    NK_KEY_BACKSPACE = 6,
    //    NK_KEY_COPY = 7,
    //    NK_KEY_CUT = 8,
    //    NK_KEY_PASTE = 9,
    //    NK_KEY_UP = 10,
    //    NK_KEY_DOWN = 11,
    //    NK_KEY_LEFT = 12,
    //    NK_KEY_RIGHT = 13,
    //    NK_KEY_TEXT_INSERT_MODE = 14,
    //    NK_KEY_TEXT_REPLACE_MODE = 15,
    //    NK_KEY_TEXT_RESET_MODE = 16,
    //    NK_KEY_TEXT_LINE_START = 17,
    //    NK_KEY_TEXT_LINE_END = 18,
    //    NK_KEY_TEXT_START = 19,
    //    NK_KEY_TEXT_END = 20,
    //    NK_KEY_TEXT_UNDO = 21,
    //    NK_KEY_TEXT_REDO = 22,
    //    NK_KEY_TEXT_SELECT_ALL = 23,
    //    NK_KEY_TEXT_WORD_LEFT = 24,
    //    NK_KEY_TEXT_WORD_RIGHT = 25,
    //    NK_KEY_SCROLL_START = 26,
    //    NK_KEY_SCROLL_END = 27,
    //    NK_KEY_SCROLL_DOWN = 28,
    //    NK_KEY_SCROLL_UP = 29,
    //    NK_KEY_MAX = 30
    //}

    //public enum NkButtons
    //{
    //    NK_BUTTON_LEFT = 0,
    //    NK_BUTTON_MIDDLE = 1,
    //    NK_BUTTON_RIGHT = 2,
    //    NK_BUTTON_DOUBLE = 3,
    //    NK_BUTTON_MAX = 4
    //}

    //[Flags]
    //public enum NkConvertResult
    //{
    //    NK_CONVERT_SUCCESS = 0,
    //    NK_CONVERT_INVALID_PARAM = 1,
    //    NK_CONVERT_COMMAND_BUFFER_FULL = 2,
    //    NK_CONVERT_VERTEX_BUFFER_FULL = 4,
    //    NK_CONVERT_ELEMENT_BUFFER_FULL = 8
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
    //public enum NkPanelFlags
    //{
    //    NK_WINDOW_BORDER = 1,
    //    NK_WINDOW_MOVABLE = 2,
    //    NK_WINDOW_SCALABLE = 4,
    //    NK_WINDOW_CLOSABLE = 8,
    //    NK_WINDOW_MINIMIZABLE = 16,
    //    NK_WINDOW_NO_SCROLLBAR = 32,
    //    NK_WINDOW_TITLE = 64,
    //    NK_WINDOW_SCROLL_AUTO_HIDE = 128,
    //    NK_WINDOW_BACKGROUND = 256,
    //    NK_WINDOW_SCALE_LEFT = 512,
    //    NK_WINDOW_NO_INPUT = 1024
    //}

    //public enum NkWidgetLayoutStates
    //{
    //    NK_WIDGET_INVALID = 0,
    //    NK_WIDGET_VALID = 1,
    //    NK_WIDGET_ROM = 2
    //}

    //public enum NkWidgetStates
    //{
    //    NK_WIDGET_STATE_MODIFIED = 2,
    //    NK_WIDGET_STATE_INACTIVE = 4,
    //    NK_WIDGET_STATE_ENTERED = 8,
    //    NK_WIDGET_STATE_HOVER = 16,
    //    NK_WIDGET_STATE_ACTIVED = 32,
    //    NK_WIDGET_STATE_LEFT = 64,
    //    NK_WIDGET_STATE_HOVERED = 18,
    //    NK_WIDGET_STATE_ACTIVE = 34
    //}

    //[Flags]
    //public enum NkTextAlign
    //{
    //    NK_TEXT_ALIGN_LEFT = 1,
    //    NK_TEXT_ALIGN_CENTERED = 2,
    //    NK_TEXT_ALIGN_RIGHT = 4,
    //    NK_TEXT_ALIGN_TOP = 8,
    //    NK_TEXT_ALIGN_MIDDLE = 16,
    //    NK_TEXT_ALIGN_BOTTOM = 32
    //}

    //public enum NkTextAlignment
    //{
    //    NK_TEXT_LEFT = 17,
    //    NK_TEXT_CENTERED = 18,
    //    NK_TEXT_RIGHT = 20
    //}

    //[Flags]
    //public enum NkEditFlags
    //{
    //    NK_EDIT_DEFAULT = 0,
    //    NK_EDIT_READ_ONLY = 1,
    //    NK_EDIT_AUTO_SELECT = 2,
    //    NK_EDIT_SIG_ENTER = 4,
    //    NK_EDIT_ALLOW_TAB = 8,
    //    NK_EDIT_NO_CURSOR = 16,
    //    NK_EDIT_SELECTABLE = 32,
    //    NK_EDIT_CLIPBOARD = 64,
    //    NK_EDIT_CTRL_ENTER_NEWLINE = 128,
    //    NK_EDIT_NO_HORIZONTAL_SCROLL = 256,
    //    NK_EDIT_ALWAYS_INSERT_MODE = 512,
    //    NK_EDIT_MULTILINE = 1024,
    //    NK_EDIT_GOTO_END_ON_ACTIVATE = 2048
    //}

    //public enum NkEditTypes
    //{
    //    NK_EDIT_SIMPLE = 512,
    //    NK_EDIT_FIELD = 608,
    //    NK_EDIT_BOX = 1640,
    //    NK_EDIT_EDITOR = 1128
    //}

    //[Flags]
    //public enum NkEditEvents
    //{
    //    NK_EDIT_ACTIVE = 1,
    //    NK_EDIT_INACTIVE = 2,
    //    NK_EDIT_ACTIVATED = 4,
    //    NK_EDIT_DEACTIVATED = 8,
    //    NK_EDIT_COMMITED = 16
    //}

    //public enum NkStyleColors
    //{
    //    NK_COLOR_TEXT = 0,
    //    NK_COLOR_WINDOW = 1,
    //    NK_COLOR_HEADER = 2,
    //    NK_COLOR_BORDER = 3,
    //    NK_COLOR_BUTTON = 4,
    //    NK_COLOR_BUTTON_HOVER = 5,
    //    NK_COLOR_BUTTON_ACTIVE = 6,
    //    NK_COLOR_TOGGLE = 7,
    //    NK_COLOR_TOGGLE_HOVER = 8,
    //    NK_COLOR_TOGGLE_CURSOR = 9,
    //    NK_COLOR_SELECT = 10,
    //    NK_COLOR_SELECT_ACTIVE = 11,
    //    NK_COLOR_SLIDER = 12,
    //    NK_COLOR_SLIDER_CURSOR = 13,
    //    NK_COLOR_SLIDER_CURSOR_HOVER = 14,
    //    NK_COLOR_SLIDER_CURSOR_ACTIVE = 15,
    //    NK_COLOR_PROPERTY = 16,
    //    NK_COLOR_EDIT = 17,
    //    NK_COLOR_EDIT_CURSOR = 18,
    //    NK_COLOR_COMBO = 19,
    //    NK_COLOR_CHART = 20,
    //    NK_COLOR_CHART_COLOR = 21,
    //    NK_COLOR_CHART_COLOR_HIGHLIGHT = 22,
    //    NK_COLOR_SCROLLBAR = 23,
    //    NK_COLOR_SCROLLBAR_CURSOR = 24,
    //    NK_COLOR_SCROLLBAR_CURSOR_HOVER = 25,
    //    NK_COLOR_SCROLLBAR_CURSOR_ACTIVE = 26,
    //    NK_COLOR_TAB_HEADER = 27,
    //    NK_COLOR_COUNT = 28
    //}

    //public enum NkStyleCursor
    //{
    //    NK_CURSOR_ARROW = 0,
    //    NK_CURSOR_TEXT = 1,
    //    NK_CURSOR_MOVE = 2,
    //    NK_CURSOR_RESIZE_VERTICAL = 3,
    //    NK_CURSOR_RESIZE_HORIZONTAL = 4,
    //    NK_CURSOR_RESIZE_TOP_LEFT_DOWN_RIGHT = 5,
    //    NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT = 6,
    //    NK_CURSOR_COUNT = 7
    //}

    //public enum NkFontCoordType
    //{
    //    NK_COORD_UV = 0,
    //    NK_COORD_PIXEL = 1
    //}

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
    //    NK_WINDOW_PRIVATE = 2048,
    //    NK_WINDOW_DYNAMIC = 2048,
    //    NK_WINDOW_ROM = 4096,
    //    NK_WINDOW_NOT_INTERACTIVE = 5120,
    //    NK_WINDOW_HIDDEN = 8192,
    //    NK_WINDOW_CLOSED = 16384,
    //    NK_WINDOW_MINIMIZED = 32768,
    //    NK_WINDOW_REMOVE_ROM = 65536
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
        NK_BUTTON_DEFAULT = 0,
        NK_BUTTON_REPEATER = 1
    }

    unsafe partial class Nk
    {
        public const int nk_false = 0;
        public const int nk_true = 1;
        //public const int NK_UP = 0;
        //public const int NK_RIGHT = 1;
        //public const int NK_DOWN = 2;
        //public const int NK_LEFT = 3;
        //public const int NK_BUTTON_DEFAULT = 0;
        //public const int NK_BUTTON_REPEATER = 1;
        public const int NK_FIXED = nk_false;
        public const int NK_MODIFIABLE = nk_true;
        public const int NK_VERTICAL = 0;
        public const int NK_HORIZONTAL = 1;
        public const int NK_MINIMIZED = nk_false;
        public const int NK_MAXIMIZED = nk_true;
        public const int NK_HIDDEN = nk_false;
        public const int NK_SHOWN = nk_true;
        public const int NK_CHART_LINES = 0;
        public const int NK_CHART_COLUMN = 1;
        public const int NK_CHART_MAX = 2;
        public const int NK_CHART_HOVERING = 0x01;
        public const int NK_CHART_CLICKED = 0x02;
        public const int NK_RGB = 0;
        public const int NK_RGBA = 1;
        public const int NK_POPUP_STATIC = 0;
        public const int NK_POPUP_DYNAMIC = 1;
        public const int NK_DYNAMIC = 0;
        public const int NK_STATIC = 1;
        public const int NK_TREE_NODE = 0;
        public const int NK_TREE_TAB = 1;
        public const int NK_SYMBOL_NONE = 0;
        public const int NK_SYMBOL_X = 1;
        public const int NK_SYMBOL_UNDERSCORE = 2;
        public const int NK_SYMBOL_CIRCLE_SOLID = 3;
        public const int NK_SYMBOL_CIRCLE_OUTLINE = 4;
        public const int NK_SYMBOL_RECT_SOLID = 5;
        public const int NK_SYMBOL_RECT_OUTLINE = 6;
        public const int NK_SYMBOL_TRIANGLE_UP = 7;
        public const int NK_SYMBOL_TRIANGLE_DOWN = 8;
        public const int NK_SYMBOL_TRIANGLE_LEFT = 9;
        public const int NK_SYMBOL_TRIANGLE_RIGHT = 10;
        public const int NK_SYMBOL_PLUS = 11;
        public const int NK_SYMBOL_MINUS = 12;
        public const int NK_SYMBOL_MAX = 13;
        public const int NK_KEY_NONE = 0;
        public const int NK_KEY_SHIFT = 1;
        public const int NK_KEY_CTRL = 2;
        public const int NK_KEY_DEL = 3;
        public const int NK_KEY_ENTER = 4;
        public const int NK_KEY_TAB = 5;
        public const int NK_KEY_BACKSPACE = 6;
        public const int NK_KEY_COPY = 7;
        public const int NK_KEY_CUT = 8;
        public const int NK_KEY_PASTE = 9;
        public const int NK_KEY_UP = 10;
        public const int NK_KEY_DOWN = 11;
        public const int NK_KEY_LEFT = 12;
        public const int NK_KEY_RIGHT = 13;
        public const int NK_KEY_TEXT_INSERT_MODE = 14;
        public const int NK_KEY_TEXT_REPLACE_MODE = 15;
        public const int NK_KEY_TEXT_RESET_MODE = 16;
        public const int NK_KEY_TEXT_LINE_START = 17;
        public const int NK_KEY_TEXT_LINE_END = 18;
        public const int NK_KEY_TEXT_START = 19;
        public const int NK_KEY_TEXT_END = 20;
        public const int NK_KEY_TEXT_UNDO = 21;
        public const int NK_KEY_TEXT_REDO = 22;
        public const int NK_KEY_TEXT_SELECT_ALL = 23;
        public const int NK_KEY_TEXT_WORD_LEFT = 24;
        public const int NK_KEY_TEXT_WORD_RIGHT = 25;
        public const int NK_KEY_SCROLL_START = 26;
        public const int NK_KEY_SCROLL_END = 27;
        public const int NK_KEY_SCROLL_DOWN = 28;
        public const int NK_KEY_SCROLL_UP = 29;
        public const int NK_KEY_MAX = 30;
        public const int NK_BUTTON_LEFT = 0;
        public const int NK_BUTTON_MIDDLE = 1;
        public const int NK_BUTTON_RIGHT = 2;
        public const int NK_BUTTON_DOUBLE = 3;
        public const int NK_BUTTON_MAX = 4;
        public const int NK_ANTI_ALIASING_OFF = 0;
        public const int NK_ANTI_ALIASING_ON = 1;
        public const int NK_CONVERT_SUCCESS = 0;
        public const int NK_CONVERT_INVALID_PARAM = 1;
        public const int NK_CONVERT_COMMAND_BUFFER_FULL = (1 << (1));
        public const int NK_CONVERT_VERTEX_BUFFER_FULL = (1 << (2));
        public const int NK_CONVERT_ELEMENT_BUFFER_FULL = (1 << (3));
        public const int NK_WINDOW_BORDER = (1 << (0));
        public const int NK_WINDOW_MOVABLE = (1 << (1));
        public const int NK_WINDOW_SCALABLE = (1 << (2));
        public const int NK_WINDOW_CLOSABLE = (1 << (3));
        public const int NK_WINDOW_MINIMIZABLE = (1 << (4));
        public const int NK_WINDOW_NO_SCROLLBAR = (1 << (5));
        public const int NK_WINDOW_TITLE = (1 << (6));
        public const int NK_WINDOW_SCROLL_AUTO_HIDE = (1 << (7));
        public const int NK_WINDOW_BACKGROUND = (1 << (8));
        public const int NK_WINDOW_SCALE_LEFT = (1 << (9));
        public const int NK_WINDOW_NO_INPUT = (1 << (10));
        public const int NK_WIDGET_INVALID = 0;
        public const int NK_WIDGET_VALID = 1;
        public const int NK_WIDGET_ROM = 2;
        public const int NK_WIDGET_STATE_MODIFIED = (1 << (1));
        public const int NK_WIDGET_STATE_INACTIVE = (1 << (2));
        public const int NK_WIDGET_STATE_ENTERED = (1 << (3));
        public const int NK_WIDGET_STATE_HOVER = (1 << (4));
        public const int NK_WIDGET_STATE_ACTIVED = (1 << (5));
        public const int NK_WIDGET_STATE_LEFT = (1 << (6));
        public const int NK_WIDGET_STATE_HOVERED = NK_WIDGET_STATE_HOVER | NK_WIDGET_STATE_MODIFIED;
        public const int NK_WIDGET_STATE_ACTIVE = NK_WIDGET_STATE_ACTIVED | NK_WIDGET_STATE_MODIFIED;
        public const int NK_TEXT_ALIGN_LEFT = 0x01;
        public const int NK_TEXT_ALIGN_CENTERED = 0x02;
        public const int NK_TEXT_ALIGN_RIGHT = 0x04;
        public const int NK_TEXT_ALIGN_TOP = 0x08;
        public const int NK_TEXT_ALIGN_MIDDLE = 0x10;
        public const int NK_TEXT_ALIGN_BOTTOM = 0x20;
        public const int NK_TEXT_LEFT = NK_TEXT_ALIGN_MIDDLE | NK_TEXT_ALIGN_LEFT;
        public const int NK_TEXT_CENTERED = NK_TEXT_ALIGN_MIDDLE | NK_TEXT_ALIGN_CENTERED;
        public const int NK_TEXT_RIGHT = NK_TEXT_ALIGN_MIDDLE | NK_TEXT_ALIGN_RIGHT;
        public const int NK_EDIT_DEFAULT = 0;
        public const int NK_EDIT_READ_ONLY = (1 << (0));
        public const int NK_EDIT_AUTO_SELECT = (1 << (1));
        public const int NK_EDIT_SIG_ENTER = (1 << (2));
        public const int NK_EDIT_ALLOW_TAB = (1 << (3));
        public const int NK_EDIT_NO_CURSOR = (1 << (4));
        public const int NK_EDIT_SELECTABLE = (1 << (5));
        public const int NK_EDIT_CLIPBOARD = (1 << (6));
        public const int NK_EDIT_CTRL_ENTER_NEWLINE = (1 << (7));
        public const int NK_EDIT_NO_HORIZONTAL_SCROLL = (1 << (8));
        public const int NK_EDIT_ALWAYS_INSERT_MODE = (1 << (9));
        public const int NK_EDIT_MULTILINE = (1 << (10));
        public const int NK_EDIT_GOTO_END_ON_ACTIVATE = (1 << (11));
        public const int NK_EDIT_SIMPLE = NK_EDIT_ALWAYS_INSERT_MODE;
        public const int NK_EDIT_FIELD = NK_EDIT_SIMPLE | NK_EDIT_SELECTABLE | NK_EDIT_CLIPBOARD;

        public const int NK_EDIT_BOX =
            NK_EDIT_ALWAYS_INSERT_MODE | NK_EDIT_SELECTABLE | NK_EDIT_MULTILINE |
            NK_EDIT_ALLOW_TAB | NK_EDIT_CLIPBOARD;

        public const int NK_EDIT_EDITOR =
            NK_EDIT_SELECTABLE | NK_EDIT_MULTILINE | NK_EDIT_ALLOW_TAB | NK_EDIT_CLIPBOARD;

        public const int NK_EDIT_ACTIVE = (1 << (0));
        public const int NK_EDIT_INACTIVE = (1 << (1));
        public const int NK_EDIT_ACTIVATED = (1 << (2));
        public const int NK_EDIT_DEACTIVATED = (1 << (3));
        public const int NK_EDIT_COMMITED = (1 << (4));
        public const int NK_COLOR_TEXT = 0;
        public const int NK_COLOR_WINDOW = 1;
        public const int NK_COLOR_HEADER = 2;
        public const int NK_COLOR_BORDER = 3;
        public const int NK_COLOR_BUTTON = 4;
        public const int NK_COLOR_BUTTON_HOVER = 5;
        public const int NK_COLOR_BUTTON_ACTIVE = 6;
        public const int NK_COLOR_TOGGLE = 7;
        public const int NK_COLOR_TOGGLE_HOVER = 8;
        public const int NK_COLOR_TOGGLE_CURSOR = 9;
        public const int NK_COLOR_SELECT = 10;
        public const int NK_COLOR_SELECT_ACTIVE = 11;
        public const int NK_COLOR_SLIDER = 12;
        public const int NK_COLOR_SLIDER_CURSOR = 13;
        public const int NK_COLOR_SLIDER_CURSOR_HOVER = 14;
        public const int NK_COLOR_SLIDER_CURSOR_ACTIVE = 15;
        public const int NK_COLOR_PROPERTY = 16;
        public const int NK_COLOR_EDIT = 17;
        public const int NK_COLOR_EDIT_CURSOR = 18;
        public const int NK_COLOR_COMBO = 19;
        public const int NK_COLOR_CHART = 20;
        public const int NK_COLOR_CHART_COLOR = 21;
        public const int NK_COLOR_CHART_COLOR_HIGHLIGHT = 22;
        public const int NK_COLOR_SCROLLBAR = 23;
        public const int NK_COLOR_SCROLLBAR_CURSOR = 24;
        public const int NK_COLOR_SCROLLBAR_CURSOR_HOVER = 25;
        public const int NK_COLOR_SCROLLBAR_CURSOR_ACTIVE = 26;
        public const int NK_COLOR_TAB_HEADER = 27;
        public const int NK_COLOR_COUNT = 28;
        public const int NK_CURSOR_ARROW = 0;
        public const int NK_CURSOR_TEXT = 1;
        public const int NK_CURSOR_MOVE = 2;
        public const int NK_CURSOR_RESIZE_VERTICAL = 3;
        public const int NK_CURSOR_RESIZE_HORIZONTAL = 4;
        public const int NK_CURSOR_RESIZE_TOP_LEFT_DOWN_RIGHT = 5;
        public const int NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT = 6;
        public const int NK_CURSOR_COUNT = 7;
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
        public const int NK_WINDOW_PRIVATE = (1 << (11));
        public const int NK_WINDOW_DYNAMIC = NK_WINDOW_PRIVATE;
        public const int NK_WINDOW_ROM = (1 << (12));
        public const int NK_WINDOW_NOT_INTERACTIVE = NK_WINDOW_ROM | NK_WINDOW_NO_INPUT;
        public const int NK_WINDOW_HIDDEN = (1 << (13));
        public const int NK_WINDOW_CLOSED = (1 << (14));
        public const int NK_WINDOW_MINIMIZED = (1 << (15));
        public const int NK_WINDOW_REMOVE_ROM = (1 << (16));
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