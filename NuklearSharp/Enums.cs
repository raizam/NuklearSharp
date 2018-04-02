using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuklearSharp
{
    public enum NkAllocationType
    {
        NK_BUFFER_FIXED = 0,
        NK_BUFFER_DYNAMIC = 1
    }

    public enum NkAntiAliasing
    {
        NK_ANTI_ALIASING_OFF = 0,
        NK_ANTI_ALIASING_ON = 1
    }

    public enum NkDrawVertexLayoutAttribute
    {
        NK_VERTEX_POSITION = 0,
        NK_VERTEX_COLOR = 1,
        NK_VERTEX_TEXCOORD = 2,
        NK_VERTEX_ATTRIBUTE_COUNT = 3
    }

    public enum NkDrawVertexLayoutFormat
    {
        NK_FORMAT_SCHAR = 0,
        NK_FORMAT_SSHORT = 1,
        NK_FORMAT_SINT = 2,
        NK_FORMAT_UCHAR = 3,
        NK_FORMAT_USHORT = 4,
        NK_FORMAT_UINT = 5,
        NK_FORMAT_FLOAT = 6,
        NK_FORMAT_DOUBLE = 7,
        NK_FORMAT_COLOR_BEGIN = 8,
        NK_FORMAT_R8G8B8 = 8,
        NK_FORMAT_R16G15B16 = 9,
        NK_FORMAT_R32G32B32 = 10,
        NK_FORMAT_R8G8B8A8 = 11,
        NK_FORMAT_B8G8R8A8 = 12,
        NK_FORMAT_R16G15B16A16 = 13,
        NK_FORMAT_R32G32B32A32 = 14,
        NK_FORMAT_R32G32B32A32_FLOAT = 15,
        NK_FORMAT_R32G32B32A32_DOUBLE = 16,
        NK_FORMAT_RGB32 = 17,
        NK_FORMAT_RGBA32 = 18,
        NK_FORMAT_COLOR_END = 18,
        NK_FORMAT_COUNT = 19
    }

    public enum NkStyleItemType
    {
        NK_STYLE_ITEM_COLOR = 0,
        NK_STYLE_ITEM_IMAGE = 1
    }

    [Flags]
    public enum NkPanelType
    {
        NK_PANEL_WINDOW = 1,
        NK_PANEL_GROUP = 2,
        NK_PANEL_POPUP = 4,
        NK_PANEL_CONTEXTUAL = 16,
        NK_PANEL_COMBO = 32,
        NK_PANEL_MENU = 64,
        NK_PANEL_TOOLTIP = 128
    }

    public enum NkPanelRowLayoutType
    {
        NK_LAYOUT_DYNAMIC_FIXED = 0,
        NK_LAYOUT_DYNAMIC_ROW = 1,
        NK_LAYOUT_DYNAMIC_FREE = 2,
        NK_LAYOUT_DYNAMIC = 3,
        NK_LAYOUT_STATIC_FIXED = 4,
        NK_LAYOUT_STATIC_ROW = 5,
        NK_LAYOUT_STATIC_FREE = 6,
        NK_LAYOUT_STATIC = 7,
        NK_LAYOUT_TEMPLATE = 8,
        NK_LAYOUT_COUNT = 9
    }

    public enum NkChartType
    {
        NK_CHART_LINES = 0,
        NK_CHART_COLUMN = 1,
        NK_CHART_MAX = 2
    }

    public enum NkSymbolType
    {
        NK_SYMBOL_NONE = 0,
        NK_SYMBOL_X = 1,
        NK_SYMBOL_UNDERSCORE = 2,
        NK_SYMBOL_CIRCLE_SOLID = 3,
        NK_SYMBOL_CIRCLE_OUTLINE = 4,
        NK_SYMBOL_RECT_SOLID = 5,
        NK_SYMBOL_RECT_OUTLINE = 6,
        NK_SYMBOL_TRIANGLE_UP = 7,
        NK_SYMBOL_TRIANGLE_DOWN = 8,
        NK_SYMBOL_TRIANGLE_LEFT = 9,
        NK_SYMBOL_TRIANGLE_RIGHT = 10,
        NK_SYMBOL_PLUS = 11,
        NK_SYMBOL_MINUS = 12,
        NK_SYMBOL_MAX = 13
    }

    public enum NkStyleHeaderAlign
    {
        NK_HEADER_LEFT = 0,
        NK_HEADER_RIGHT = 1
    }

    public enum NkButtonBehavior
    {
        NK_BUTTON_DEFAULT = 0,
        NK_BUTTON_REPEATER = 1
    }

    public enum Nk
    {
        nk_false = 0,
        nk_true = 1
    }

    public enum NkHeading
    {
        NK_UP = 0,
        NK_RIGHT = 1,
        NK_DOWN = 2,
        NK_LEFT = 3
    }

    public enum NkModify
    {
        NK_FIXED = 0,
        NK_MODIFIABLE = 1
    }

    public enum NkOrientation
    {
        NK_VERTICAL = 0,
        NK_HORIZONTAL = 1
    }

    public enum NkCollapseStates
    {
        NK_MINIMIZED = 0,
        NK_MAXIMIZED = 1
    }

    public enum NkShowStates
    {
        NK_HIDDEN = 0,
        NK_SHOWN = 1
    }

    public enum NkChartEvent
    {
        NK_CHART_HOVERING = 1,
        NK_CHART_CLICKED = 2
    }

    public enum NkColorFormat
    {
        NK_RGB = 0,
        NK_RGBA = 1
    }

    public enum NkPopupType
    {
        NK_POPUP_STATIC = 0,
        NK_POPUP_DYNAMIC = 1
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

    public enum NkKeys
    {
        NK_KEY_NONE = 0,
        NK_KEY_SHIFT = 1,
        NK_KEY_CTRL = 2,
        NK_KEY_DEL = 3,
        NK_KEY_ENTER = 4,
        NK_KEY_TAB = 5,
        NK_KEY_BACKSPACE = 6,
        NK_KEY_COPY = 7,
        NK_KEY_CUT = 8,
        NK_KEY_PASTE = 9,
        NK_KEY_UP = 10,
        NK_KEY_DOWN = 11,
        NK_KEY_LEFT = 12,
        NK_KEY_RIGHT = 13,
        NK_KEY_TEXT_INSERT_MODE = 14,
        NK_KEY_TEXT_REPLACE_MODE = 15,
        NK_KEY_TEXT_RESET_MODE = 16,
        NK_KEY_TEXT_LINE_START = 17,
        NK_KEY_TEXT_LINE_END = 18,
        NK_KEY_TEXT_START = 19,
        NK_KEY_TEXT_END = 20,
        NK_KEY_TEXT_UNDO = 21,
        NK_KEY_TEXT_REDO = 22,
        NK_KEY_TEXT_SELECT_ALL = 23,
        NK_KEY_TEXT_WORD_LEFT = 24,
        NK_KEY_TEXT_WORD_RIGHT = 25,
        NK_KEY_SCROLL_START = 26,
        NK_KEY_SCROLL_END = 27,
        NK_KEY_SCROLL_DOWN = 28,
        NK_KEY_SCROLL_UP = 29,
        NK_KEY_MAX = 30
    }

    public enum NkButtons
    {
        NK_BUTTON_LEFT = 0,
        NK_BUTTON_MIDDLE = 1,
        NK_BUTTON_RIGHT = 2,
        NK_BUTTON_DOUBLE = 3,
        NK_BUTTON_MAX = 4
    }

    [Flags]
    public enum NkConvertResult
    {
        NK_CONVERT_SUCCESS = 0,
        NK_CONVERT_INVALID_PARAM = 1,
        NK_CONVERT_COMMAND_BUFFER_FULL = 2,
        NK_CONVERT_VERTEX_BUFFER_FULL = 4,
        NK_CONVERT_ELEMENT_BUFFER_FULL = 8
    }

    public enum NkCommandType
    {
        NK_COMMAND_NOP = 0,
        NK_COMMAND_SCISSOR = 1,
        NK_COMMAND_LINE = 2,
        NK_COMMAND_CURVE = 3,
        NK_COMMAND_RECT = 4,
        NK_COMMAND_RECT_FILLED = 5,
        NK_COMMAND_RECT_MULTI_COLOR = 6,
        NK_COMMAND_CIRCLE = 7,
        NK_COMMAND_CIRCLE_FILLED = 8,
        NK_COMMAND_ARC = 9,
        NK_COMMAND_ARC_FILLED = 10,
        NK_COMMAND_TRIANGLE = 11,
        NK_COMMAND_TRIANGLE_FILLED = 12,
        NK_COMMAND_POLYGON = 13,
        NK_COMMAND_POLYGON_FILLED = 14,
        NK_COMMAND_POLYLINE = 15,
        NK_COMMAND_TEXT = 16,
        NK_COMMAND_IMAGE = 17,
        NK_COMMAND_CUSTOM = 18
    }

    [Flags]
    public enum NkPanelFlags
    {
        NK_WINDOW_BORDER = 1,
        NK_WINDOW_MOVABLE = 2,
        NK_WINDOW_SCALABLE = 4,
        NK_WINDOW_CLOSABLE = 8,
        NK_WINDOW_MINIMIZABLE = 16,
        NK_WINDOW_NO_SCROLLBAR = 32,
        NK_WINDOW_TITLE = 64,
        NK_WINDOW_SCROLL_AUTO_HIDE = 128,
        NK_WINDOW_BACKGROUND = 256,
        NK_WINDOW_SCALE_LEFT = 512,
        NK_WINDOW_NO_INPUT = 1024
    }

    public enum NkWidgetLayoutStates
    {
        NK_WIDGET_INVALID = 0,
        NK_WIDGET_VALID = 1,
        NK_WIDGET_ROM = 2
    }

    public enum NkWidgetStates
    {
        NK_WIDGET_STATE_MODIFIED = 2,
        NK_WIDGET_STATE_INACTIVE = 4,
        NK_WIDGET_STATE_ENTERED = 8,
        NK_WIDGET_STATE_HOVER = 16,
        NK_WIDGET_STATE_ACTIVED = 32,
        NK_WIDGET_STATE_LEFT = 64,
        NK_WIDGET_STATE_HOVERED = 18,
        NK_WIDGET_STATE_ACTIVE = 34
    }

    [Flags]
    public enum NkTextAlign
    {
        NK_TEXT_ALIGN_LEFT = 1,
        NK_TEXT_ALIGN_CENTERED = 2,
        NK_TEXT_ALIGN_RIGHT = 4,
        NK_TEXT_ALIGN_TOP = 8,
        NK_TEXT_ALIGN_MIDDLE = 16,
        NK_TEXT_ALIGN_BOTTOM = 32
    }

    public enum NkTextAlignment
    {
        NK_TEXT_LEFT = 17,
        NK_TEXT_CENTERED = 18,
        NK_TEXT_RIGHT = 20
    }

    [Flags]
    public enum NkEditFlags
    {
        NK_EDIT_DEFAULT = 0,
        NK_EDIT_READ_ONLY = 1,
        NK_EDIT_AUTO_SELECT = 2,
        NK_EDIT_SIG_ENTER = 4,
        NK_EDIT_ALLOW_TAB = 8,
        NK_EDIT_NO_CURSOR = 16,
        NK_EDIT_SELECTABLE = 32,
        NK_EDIT_CLIPBOARD = 64,
        NK_EDIT_CTRL_ENTER_NEWLINE = 128,
        NK_EDIT_NO_HORIZONTAL_SCROLL = 256,
        NK_EDIT_ALWAYS_INSERT_MODE = 512,
        NK_EDIT_MULTILINE = 1024,
        NK_EDIT_GOTO_END_ON_ACTIVATE = 2048
    }

    public enum NkEditTypes
    {
        NK_EDIT_SIMPLE = 512,
        NK_EDIT_FIELD = 608,
        NK_EDIT_BOX = 1640,
        NK_EDIT_EDITOR = 1128
    }

    [Flags]
    public enum NkEditEvents
    {
        NK_EDIT_ACTIVE = 1,
        NK_EDIT_INACTIVE = 2,
        NK_EDIT_ACTIVATED = 4,
        NK_EDIT_DEACTIVATED = 8,
        NK_EDIT_COMMITED = 16
    }

    public enum NkStyleColors
    {
        NK_COLOR_TEXT = 0,
        NK_COLOR_WINDOW = 1,
        NK_COLOR_HEADER = 2,
        NK_COLOR_BORDER = 3,
        NK_COLOR_BUTTON = 4,
        NK_COLOR_BUTTON_HOVER = 5,
        NK_COLOR_BUTTON_ACTIVE = 6,
        NK_COLOR_TOGGLE = 7,
        NK_COLOR_TOGGLE_HOVER = 8,
        NK_COLOR_TOGGLE_CURSOR = 9,
        NK_COLOR_SELECT = 10,
        NK_COLOR_SELECT_ACTIVE = 11,
        NK_COLOR_SLIDER = 12,
        NK_COLOR_SLIDER_CURSOR = 13,
        NK_COLOR_SLIDER_CURSOR_HOVER = 14,
        NK_COLOR_SLIDER_CURSOR_ACTIVE = 15,
        NK_COLOR_PROPERTY = 16,
        NK_COLOR_EDIT = 17,
        NK_COLOR_EDIT_CURSOR = 18,
        NK_COLOR_COMBO = 19,
        NK_COLOR_CHART = 20,
        NK_COLOR_CHART_COLOR = 21,
        NK_COLOR_CHART_COLOR_HIGHLIGHT = 22,
        NK_COLOR_SCROLLBAR = 23,
        NK_COLOR_SCROLLBAR_CURSOR = 24,
        NK_COLOR_SCROLLBAR_CURSOR_HOVER = 25,
        NK_COLOR_SCROLLBAR_CURSOR_ACTIVE = 26,
        NK_COLOR_TAB_HEADER = 27,
        NK_COLOR_COUNT = 28
    }

    public enum NkStyleCursor
    {
        NK_CURSOR_ARROW = 0,
        NK_CURSOR_TEXT = 1,
        NK_CURSOR_MOVE = 2,
        NK_CURSOR_RESIZE_VERTICAL = 3,
        NK_CURSOR_RESIZE_HORIZONTAL = 4,
        NK_CURSOR_RESIZE_TOP_LEFT_DOWN_RIGHT = 5,
        NK_CURSOR_RESIZE_TOP_RIGHT_DOWN_LEFT = 6,
        NK_CURSOR_COUNT = 7
    }

    public enum NkFontCoordType
    {
        NK_COORD_UV = 0,
        NK_COORD_PIXEL = 1
    }

    public enum NkFontAtlasFormat
    {
        NK_FONT_ATLAS_ALPHA8 = 0,
        NK_FONT_ATLAS_RGBA32 = 1
    }

    public enum NkBufferAllocationType
    {
        NK_BUFFER_FRONT = 0,
        NK_BUFFER_BACK = 1,
        NK_BUFFER_MAX = 2
    }

    public enum NkTextEditType
    {
        NK_TEXT_EDIT_SINGLE_LINE = 0,
        NK_TEXT_EDIT_MULTI_LINE = 1
    }

    public enum NkTextEditMode
    {
        NK_TEXT_EDIT_MODE_VIEW = 0,
        NK_TEXT_EDIT_MODE_INSERT = 1,
        NK_TEXT_EDIT_MODE_REPLACE = 2
    }

    public enum NkCommandClipping
    {
        NK_CLIPPING_OFF = 0,
        NK_CLIPPING_ON = 1
    }

    public enum NkDrawListStroke
    {
        NK_STROKE_OPEN = 0,
        NK_STROKE_CLOSED = 1
    }

    public enum NkPanelSet
    {
        NK_PANEL_SET_NONBLOCK = 240,
        NK_PANEL_SET_POPUP = 244,
        NK_PANEL_SET_SUB = 246
    }

    public enum NkWindowFlags
    {
        NK_WINDOW_PRIVATE = 2048,
        NK_WINDOW_DYNAMIC = 2048,
        NK_WINDOW_ROM = 4096,
        NK_WINDOW_NOT_INTERACTIVE = 5120,
        NK_WINDOW_HIDDEN = 8192,
        NK_WINDOW_CLOSED = 16384,
        NK_WINDOW_MINIMIZED = 32768,
        NK_WINDOW_REMOVE_ROM = 65536
    }

    public enum NK
    {
        NK_DO_NOT_STOP_ON_NEW_LINE = 0,
        NK_STOP_ON_NEW_LINE = 1
    }

    public enum NK_RP_HEURISTIC
    {
        NK_RP_HEURISTIC_Skyline_default = 0,
        NK_RP_HEURISTIC_Skyline_BL_sortHeight = 0,
        NK_RP_HEURISTIC_Skyline_BF_sortHeight = 1
    }

    public enum NK_RP_INIT_STATE
    {
        NK_RP__INIT_skyline = 1
    }

    public enum NK_TT_v
    {
        NK_TT_vmove = 1,
        NK_TT_vline = 2,
        NK_TT_vcurve = 3
    }

    public enum NK_TT_PLATFORM_ID
    {
        NK_TT_PLATFORM_ID_UNICODE = 0,
        NK_TT_PLATFORM_ID_MAC = 1,
        NK_TT_PLATFORM_ID_ISO = 2,
        NK_TT_PLATFORM_ID_MICROSOFT = 3
    }

    public enum NK_TT_UNICODE_EID
    {
        NK_TT_UNICODE_EID_UNICODE_1_0 = 0,
        NK_TT_UNICODE_EID_UNICODE_1_1 = 1,
        NK_TT_UNICODE_EID_ISO_10646 = 2,
        NK_TT_UNICODE_EID_UNICODE_2_0_BMP = 3,
        NK_TT_UNICODE_EID_UNICODE_2_0_FULL = 4
    }

    public enum NK_TT_MS_EID
    {
        NK_TT_MS_EID_SYMBOL = 0,
        NK_TT_MS_EID_UNICODE_BMP = 1,
        NK_TT_MS_EID_SHIFTJIS = 2,
        NK_TT_MS_EID_UNICODE_FULL = 10
    }

    public enum NK_TT_MAC_EID
    {
        NK_TT_MAC_EID_ROMAN = 0,
        NK_TT_MAC_EID_ARABIC = 4,
        NK_TT_MAC_EID_JAPANESE = 1,
        NK_TT_MAC_EID_HEBREW = 5,
        NK_TT_MAC_EID_CHINESE_TRAD = 2,
        NK_TT_MAC_EID_GREEK = 6,
        NK_TT_MAC_EID_KOREAN = 3,
        NK_TT_MAC_EID_RUSSIAN = 7
    }

    public enum NK_TT_MS_LANG
    {
        NK_TT_MS_LANG_ENGLISH = 1033,
        NK_TT_MS_LANG_ITALIAN = 1040,
        NK_TT_MS_LANG_CHINESE = 2052,
        NK_TT_MS_LANG_JAPANESE = 1041,
        NK_TT_MS_LANG_DUTCH = 1043,
        NK_TT_MS_LANG_KOREAN = 1042,
        NK_TT_MS_LANG_FRENCH = 1036,
        NK_TT_MS_LANG_RUSSIAN = 1049,
        NK_TT_MS_LANG_GERMAN = 1031,
        NK_TT_MS_LANG_SPANISH = 1033,
        NK_TT_MS_LANG_HEBREW = 1037,
        NK_TT_MS_LANG_SWEDISH = 1053
    }

    public enum NK_TT_MAC_LANG
    {
        NK_TT_MAC_LANG_ENGLISH = 0,
        NK_TT_MAC_LANG_JAPANESE = 11,
        NK_TT_MAC_LANG_ARABIC = 12,
        NK_TT_MAC_LANG_KOREAN = 23,
        NK_TT_MAC_LANG_DUTCH = 4,
        NK_TT_MAC_LANG_RUSSIAN = 32,
        NK_TT_MAC_LANG_FRENCH = 1,
        NK_TT_MAC_LANG_SPANISH = 6,
        NK_TT_MAC_LANG_GERMAN = 2,
        NK_TT_MAC_LANG_SWEDISH = 5,
        NK_TT_MAC_LANG_HEBREW = 10,
        NK_TT_MAC_LANG_CHINESE_SIMPLIFIED = 33,
        NK_TT_MAC_LANG_ITALIAN = 3,
        NK_TT_MAC_LANG_CHINESE_TRAD = 19
    }

    public enum NkToggleType
    {
        NK_TOGGLE_CHECK = 0,
        NK_TOGGLE_OPTION = 1
    }

    public enum NkPropertyStatus
    {
        NK_PROPERTY_DEFAULT = 0,
        NK_PROPERTY_EDIT = 1,
        NK_PROPERTY_DRAG = 2
    }

    public enum NkPropertyFilter
    {
        NK_FILTER_INT = 0,
        NK_FILTER_FLOAT = 1
    }

    public enum NkPropertyKind
    {
        NK_PROPERTY_INT = 0,
        NK_PROPERTY_FLOAT = 1,
        NK_PROPERTY_DOUBLE = 2
    }

    public enum NkWindowInsertLocation
    {
        NK_INSERT_BACK = 0,
        NK_INSERT_FRONT = 1
    }
}
