using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{

    public unsafe partial class nk_config_stack_style_item_element
    {
        public NkStyleItem address;
        public NkStyleItem old_value = new NkStyleItem();
    }

    public unsafe partial class nk_config_stack_float_element
    {
        public float* address;
        public float old_value;
    }

    public unsafe partial class nk_config_stack_vec2_element
    {
        public NkVec2* address;
        public NkVec2 old_value = new NkVec2();
    }

    public unsafe partial class nk_config_stack_flags_element
    {
        public uint* address;
        public uint old_value;
    }

    public unsafe partial class nk_config_stack_color_element
    {
        public NkColor* address;
        public NkColor old_value = new NkColor();
    }

    public unsafe partial class nk_config_stack_user_font_element
    {
        public NkUserFont address;
        public NkUserFont old_value;
    }

    public unsafe partial class nk_config_stack_style_item
    {
        public int head;
        public nk_config_stack_style_item_element[] elements = new nk_config_stack_style_item_element[16];
    }

    public unsafe partial class nk_config_stack_float
    {
        public int head;
        public nk_config_stack_float_element[] elements = new nk_config_stack_float_element[32];
    }

    public unsafe partial class nk_config_stack_vec2
    {
        public int head;
        public nk_config_stack_vec2_element[] elements = new nk_config_stack_vec2_element[16];
    }

    public unsafe partial class nk_config_stack_flags
    {
        public int head;
        public nk_config_stack_flags_element[] elements = new nk_config_stack_flags_element[32];
    }

    public unsafe partial class nk_config_stack_color
    {
        public int head;
        public nk_config_stack_color_element[] elements = new nk_config_stack_color_element[32];
    }

    public unsafe partial class nk_config_stack_user_font
    {
        public int head;
        public nk_config_stack_user_font_element[] elements = new nk_config_stack_user_font_element[8];
    }

    public unsafe partial class nk_config_stack_button_behavior
    {
        public int head;
        public NkConfigStackButtonBehaviorElement[] elements = new NkConfigStackButtonBehaviorElement[8];
    }

    public unsafe partial class nk_configuration_stacks
    {
        public nk_config_stack_style_item style_items = new nk_config_stack_style_item();
        public nk_config_stack_float floats = new nk_config_stack_float();
        public nk_config_stack_vec2 vectors = new nk_config_stack_vec2();
        public nk_config_stack_flags flags = new nk_config_stack_flags();
        public nk_config_stack_color colors = new nk_config_stack_color();
        public nk_config_stack_user_font fonts = new nk_config_stack_user_font();
        public nk_config_stack_button_behavior button_behaviors = new nk_config_stack_button_behavior();

    }
}