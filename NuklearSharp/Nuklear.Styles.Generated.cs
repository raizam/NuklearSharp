using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
    public unsafe static partial class Nk
    {
        public unsafe partial class nk_style_text
        {
            public nk_color color = new nk_color();
            public nk_vec2 padding = new nk_vec2();
        }

        public unsafe partial class nk_style_button
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_color text_background = new nk_color();
            public nk_color text_normal = new nk_color();
            public nk_color text_hover = new nk_color();
            public nk_color text_active = new nk_color();
            public uint text_alignment;
            public float border;
            public float rounding;
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 image_padding = new nk_vec2();
            public nk_vec2 touch_padding = new nk_vec2();
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_toggle
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public NkStyleItem cursor_normal = new NkStyleItem();
            public NkStyleItem cursor_hover = new NkStyleItem();
            public nk_color text_normal = new nk_color();
            public nk_color text_hover = new nk_color();
            public nk_color text_active = new nk_color();
            public nk_color text_background = new nk_color();
            public uint text_alignment;
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 touch_padding = new nk_vec2();
            public float spacing;
            public float border;
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_selectable
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem pressed = new NkStyleItem();
            public NkStyleItem normal_active = new NkStyleItem();
            public NkStyleItem hover_active = new NkStyleItem();
            public NkStyleItem pressed_active = new NkStyleItem();
            public nk_color text_normal = new nk_color();
            public nk_color text_hover = new nk_color();
            public nk_color text_pressed = new nk_color();
            public nk_color text_normal_active = new nk_color();
            public nk_color text_hover_active = new nk_color();
            public nk_color text_pressed_active = new nk_color();
            public nk_color text_background = new nk_color();
            public uint text_alignment;
            public float rounding;
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 touch_padding = new nk_vec2();
            public nk_vec2 image_padding = new nk_vec2();
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_slider
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_color bar_normal = new nk_color();
            public nk_color bar_hover = new nk_color();
            public nk_color bar_active = new nk_color();
            public nk_color bar_filled = new nk_color();
            public NkStyleItem cursor_normal = new NkStyleItem();
            public NkStyleItem cursor_hover = new NkStyleItem();
            public NkStyleItem cursor_active = new NkStyleItem();
            public float border;
            public float rounding;
            public float bar_height;
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 spacing = new nk_vec2();
            public nk_vec2 cursor_size = new nk_vec2();
            public int show_buttons;
            public nk_style_button inc_button = new nk_style_button();
            public nk_style_button dec_button = new nk_style_button();
            public int inc_symbol;
            public int dec_symbol;
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_progress
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public NkStyleItem cursor_normal = new NkStyleItem();
            public NkStyleItem cursor_hover = new NkStyleItem();
            public NkStyleItem cursor_active = new NkStyleItem();
            public nk_color cursor_border_color = new nk_color();
            public float rounding;
            public float border;
            public float cursor_border;
            public float cursor_rounding;
            public nk_vec2 padding = new nk_vec2();
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_scrollbar
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public NkStyleItem cursor_normal = new NkStyleItem();
            public NkStyleItem cursor_hover = new NkStyleItem();
            public NkStyleItem cursor_active = new NkStyleItem();
            public nk_color cursor_border_color = new nk_color();
            public float border;
            public float rounding;
            public float border_cursor;
            public float rounding_cursor;
            public nk_vec2 padding = new nk_vec2();
            public int show_buttons;
            public nk_style_button inc_button = new nk_style_button();
            public nk_style_button dec_button = new nk_style_button();
            public int inc_symbol;
            public int dec_symbol;
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_edit
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_style_scrollbar scrollbar = new nk_style_scrollbar();
            public nk_color cursor_normal = new nk_color();
            public nk_color cursor_hover = new nk_color();
            public nk_color cursor_text_normal = new nk_color();
            public nk_color cursor_text_hover = new nk_color();
            public nk_color text_normal = new nk_color();
            public nk_color text_hover = new nk_color();
            public nk_color text_active = new nk_color();
            public nk_color selected_normal = new nk_color();
            public nk_color selected_hover = new nk_color();
            public nk_color selected_text_normal = new nk_color();
            public nk_color selected_text_hover = new nk_color();
            public float border;
            public float rounding;
            public float cursor_size;
            public nk_vec2 scrollbar_size = new nk_vec2();
            public nk_vec2 padding = new nk_vec2();
            public float row_padding;
        }

        public unsafe partial class nk_style_property
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_color label_normal = new nk_color();
            public nk_color label_hover = new nk_color();
            public nk_color label_active = new nk_color();
            public int sym_left;
            public int sym_right;
            public float border;
            public float rounding;
            public nk_vec2 padding = new nk_vec2();
            public nk_style_edit edit = new nk_style_edit();
            public nk_style_button inc_button = new nk_style_button();
            public nk_style_button dec_button = new nk_style_button();
            public NkHandle userdata = new NkHandle();
            public NkDrawNotify draw_begin;
            public NkDrawNotify draw_end;
        }

        public unsafe partial class nk_style_chart
        {
            public NkStyleItem background = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_color selected_color = new nk_color();
            public nk_color color = new nk_color();
            public float border;
            public float rounding;
            public nk_vec2 padding = new nk_vec2();
        }

        public unsafe partial class nk_style_combo
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_color label_normal = new nk_color();
            public nk_color label_hover = new nk_color();
            public nk_color label_active = new nk_color();
            public nk_color symbol_normal = new nk_color();
            public nk_color symbol_hover = new nk_color();
            public nk_color symbol_active = new nk_color();
            public nk_style_button button = new nk_style_button();
            public int sym_normal;
            public int sym_hover;
            public int sym_active;
            public float border;
            public float rounding;
            public nk_vec2 content_padding = new nk_vec2();
            public nk_vec2 button_padding = new nk_vec2();
            public nk_vec2 spacing = new nk_vec2();
        }

        public unsafe partial class nk_style_tab
        {
            public NkStyleItem background = new NkStyleItem();
            public nk_color border_color = new nk_color();
            public nk_color text = new nk_color();
            public nk_style_button tab_maximize_button = new nk_style_button();
            public nk_style_button tab_minimize_button = new nk_style_button();
            public nk_style_button node_maximize_button = new nk_style_button();
            public nk_style_button node_minimize_button = new nk_style_button();
            public int sym_minimize;
            public int sym_maximize;
            public float border;
            public float rounding;
            public float indent;
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 spacing = new nk_vec2();
        }

        public unsafe partial class nk_style_window_header
        {
            public NkStyleItem normal = new NkStyleItem();
            public NkStyleItem hover = new NkStyleItem();
            public NkStyleItem active = new NkStyleItem();
            public nk_style_button close_button = new nk_style_button();
            public nk_style_button minimize_button = new nk_style_button();
            public int close_symbol;
            public int minimize_symbol;
            public int maximize_symbol;
            public nk_color label_normal = new nk_color();
            public nk_color label_hover = new nk_color();
            public nk_color label_active = new nk_color();
            public int align;
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 label_padding = new nk_vec2();
            public nk_vec2 spacing = new nk_vec2();
        }

        public unsafe partial class nk_style_window
        {
            public nk_style_window_header header = new nk_style_window_header();
            public NkStyleItem fixed_background = new NkStyleItem();
            public nk_color background = new nk_color();
            public nk_color border_color = new nk_color();
            public nk_color popup_border_color = new nk_color();
            public nk_color combo_border_color = new nk_color();
            public nk_color contextual_border_color = new nk_color();
            public nk_color menu_border_color = new nk_color();
            public nk_color group_border_color = new nk_color();
            public nk_color tooltip_border_color = new nk_color();
            public NkStyleItem scaler = new NkStyleItem();
            public float border;
            public float combo_border;
            public float contextual_border;
            public float menu_border;
            public float group_border;
            public float tooltip_border;
            public float popup_border;
            public float min_row_height_padding;
            public float rounding;
            public nk_vec2 spacing = new nk_vec2();
            public nk_vec2 scrollbar_size = new nk_vec2();
            public nk_vec2 min_size = new nk_vec2();
            public nk_vec2 padding = new nk_vec2();
            public nk_vec2 group_padding = new nk_vec2();
            public nk_vec2 popup_padding = new nk_vec2();
            public nk_vec2 combo_padding = new nk_vec2();
            public nk_vec2 contextual_padding = new nk_vec2();
            public nk_vec2 menu_padding = new nk_vec2();
            public nk_vec2 tooltip_padding = new nk_vec2();
        }

        public static nk_vec2 nk_panel_get_padding(NkStyle style, int type)
        {
            switch (type)
            {
                default:
                case NK_PANEL_WINDOW:
                    return (nk_vec2)(style.Window.padding);
                case NK_PANEL_GROUP:
                    return (nk_vec2)(style.Window.group_padding);
                case NK_PANEL_POPUP:
                    return (nk_vec2)(style.Window.popup_padding);
                case NK_PANEL_CONTEXTUAL:
                    return (nk_vec2)(style.Window.contextual_padding);
                case NK_PANEL_COMBO:
                    return (nk_vec2)(style.Window.combo_padding);
                case NK_PANEL_MENU:
                    return (nk_vec2)(style.Window.menu_padding);
                case NK_PANEL_TOOLTIP:
                    return (nk_vec2)(style.Window.menu_padding);
            }

        }

        public static float nk_panel_get_border(NkStyle style, uint flags, int type)
        {
            if ((flags & NK_WINDOW_BORDER) != 0)
            {
                switch (type)
                {
                    default:
                    case NK_PANEL_WINDOW:
                        return (float)(style.Window.border);
                    case NK_PANEL_GROUP:
                        return (float)(style.Window.group_border);
                    case NK_PANEL_POPUP:
                        return (float)(style.Window.popup_border);
                    case NK_PANEL_CONTEXTUAL:
                        return (float)(style.Window.contextual_border);
                    case NK_PANEL_COMBO:
                        return (float)(style.Window.combo_border);
                    case NK_PANEL_MENU:
                        return (float)(style.Window.menu_border);
                    case NK_PANEL_TOOLTIP:
                        return (float)(style.Window.menu_border);
                }
            }
            else return (float)(0);
        }

        public static nk_color nk_panel_get_border_color(NkStyle style, int type)
        {
            switch (type)
            {
                default:
                case NK_PANEL_WINDOW:
                    return (nk_color)(style.Window.border_color);
                case NK_PANEL_GROUP:
                    return (nk_color)(style.Window.group_border_color);
                case NK_PANEL_POPUP:
                    return (nk_color)(style.Window.popup_border_color);
                case NK_PANEL_CONTEXTUAL:
                    return (nk_color)(style.Window.contextual_border_color);
                case NK_PANEL_COMBO:
                    return (nk_color)(style.Window.combo_border_color);
                case NK_PANEL_MENU:
                    return (nk_color)(style.Window.menu_border_color);
                case NK_PANEL_TOOLTIP:
                    return (nk_color)(style.Window.menu_border_color);
            }

        }

        public static float nk_layout_row_calculate_usable_space(NkStyle style, int type, float total_space, int columns)
        {
            float panel_padding;
            float panel_spacing;
            float panel_space;
            nk_vec2 spacing = new nk_vec2();
            nk_vec2 padding = new nk_vec2();
            spacing = (nk_vec2)(style.Window.spacing);
            padding = (nk_vec2)(nk_panel_get_padding(style, (int)(type)));
            panel_padding = (float)(2 * padding.x);
            panel_spacing = (float)((float)((columns - 1) < (0) ? (0) : (columns - 1)) * spacing.x);
            panel_space = (float)(total_space - panel_padding - panel_spacing);
            return (float)(panel_space);
        }
    }
}