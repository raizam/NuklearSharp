using System;
using NuklearSharp;
using NuklearSharp.MonoGame;

namespace Extended
{
    public static unsafe class GUI
    {
        private static readonly string[] items = { "Item 0", "item 1", "item 2" };
        private static readonly float[] ratio = { 0.15f, 0.85f };
        private static readonly float[] ratio2 = { 0.15f, 0.50f, 0.35f };
        private static readonly string[] items2 = { "Item 0", "item 1", "item 2" };

        private static readonly NkStr[] edit_strings = new NkStr[4];

        private static int option = 1;
        private static bool toggle0 = true;
        private static bool toggle1;
        private static bool toggle2 = true;
        private static bool image_active;
        private static bool check0 = true;
        private static bool check1;
        private static ulong prog = 80;
        private static int selected_item;
        private static int selected_image = 3;
        private static int selected_icon;
        private static bool piemenu_active;
        private static Nuklear.nk_vec2 piemenu_pos;
        private static bool grid_check = true;
        private static int selectedItem;

        /// <summary>
        ///     User interfaces the piemenu.
        /// </summary>
        /// <returns>The piemenu.</returns>
        /// <param name="ctx">Context.</param>
        /// <param name="pos">Position.</param>
        /// <param name="radius">Radius.</param>
        /// <param name="icons">Icons.</param>
        /// <param name="item_count">Item count.</param>
        public static int ui_piemenu(NuklearContext ctx, Nuklear.nk_vec2 pos, float radius,
            Nuklear.nk_image[] icons, int item_count)
        {
            var ret = -1;

            /* pie menu popup */
            var border = ctx.Ctx.style.window.border_color;
            var background = ctx.Ctx.style.window.fixed_background;

            ctx.Ctx.style.window.fixed_background = Nuklear.nk_style_item_hide();
            ctx.Ctx.style.window.border_color = Nuklear.nk_rgba(0, 0, 0, 0);

            var total_space = ctx.WindowGetContentRegion();
            ctx.Ctx.style.window.spacing = Nuklear.nk_vec2_(0, 0);
            ctx.Ctx.style.window.padding = Nuklear.nk_vec2_(0, 0);

            if (ctx.PopupBegin(Nuklear.NK_POPUP_STATIC, "piemenu", Nuklear.NK_WINDOW_NO_SCROLLBAR,
                Nuklear.nk_rect_(pos.x - total_space.x - radius, pos.y - radius - total_space.y,
                    2 * radius, 2 * radius)))
            {
                var o = ctx.WindowGetCanvas();
                var inp = ctx.Ctx.input;

                total_space = ctx.WindowGetContentRegion();
                ctx.Ctx.style.window.spacing = Nuklear.nk_vec2_(4, 4);
                ctx.Ctx.style.window.padding = Nuklear.nk_vec2_(8, 8);
                ctx.LayoutRowDynamic(total_space.h, 1);
                Nuklear.nk_rect bounds;
                Nuklear.nk_widget(&bounds, ctx.Ctx);

                /* outer circle */
                Nuklear.nk_fill_circle(o, bounds, Nuklear.nk_rgb(50, 50, 50));
                int active_item;
                {
                    /* circle buttons */
                    var step = 2 * 3.141592654f / Math.Max(1, item_count);
                    float a_min = 0;
                    var a_max = step;

                    var center = Nuklear.nk_vec2_(bounds.x + bounds.w / 2.0f, bounds.y + bounds.h / 2.0f);
                    var drag = Nuklear.nk_vec2_(inp.mouse.pos.x - center.x, inp.mouse.pos.y - center.y);
                    var angle = (float)Math.Atan2(drag.y, drag.x);
                    if (angle < -0.0f)
                        angle += 2.0f * 3.141592654f;
                    active_item = (int)(angle / step);

                    int i;
                    for (i = 0; i < item_count; ++i)
                    {
                        Nuklear.nk_rect content;
                        Nuklear.nk_fill_arc(o, center.x, center.y, bounds.w / 2.0f,
                            a_min, a_max, active_item == i ? Nuklear.nk_rgb(45, 100, 255) : Nuklear.nk_rgb(60, 60, 60));

                        /* separator line */
                        var rx = bounds.w / 2.0f;
                        float ry = 0;
                        var dx = rx * (float)Math.Cos(a_min) - ry * (float)Math.Sin(a_min);
                        var dy = rx * (float)Math.Sin(a_min) + ry * (float)Math.Cos(a_min);
                        Nuklear.nk_stroke_line(o, center.x, center.y,
                            center.x + dx, center.y + dy, 1.0f, Nuklear.nk_rgb(50, 50, 50));

                        /* button content */
                        var a = a_min + (a_max - a_min) / 2.0f;
                        rx = bounds.w / 2.5f;
                        ry = 0;
                        content.w = 30;
                        content.h = 30;
                        content.x = center.x + (rx * (float)Math.Cos(a) - ry * (float)Math.Sin(a) - content.w / 2.0f);
                        content.y = center.y + (rx * (float)Math.Sin(a) + ry * (float)Math.Cos(a) - content.h / 2.0f);
                        Nuklear.nk_draw_image(o, content, icons[i], Nuklear.nk_rgb(255, 255, 255));
                        a_min = a_max;
                        a_max += step;
                    }
                }
                {
                    /* inner circle */
                    Nuklear.nk_rect inner;
                    inner.x = bounds.x + bounds.w / 2 - bounds.w / 4;
                    inner.y = bounds.y + bounds.h / 2 - bounds.h / 4;
                    inner.w = bounds.w / 2;
                    inner.h = bounds.h / 2;
                    Nuklear.nk_fill_circle(o, inner, Nuklear.nk_rgb(45, 45, 45));

                    /* active icon content */
                    bounds.w = inner.w / 2.0f;
                    bounds.h = inner.h / 2.0f;
                    bounds.x = inner.x + inner.w / 2 - bounds.w / 2;
                    bounds.y = inner.y + inner.h / 2 - bounds.h / 2;
                    Nuklear.nk_draw_image(o, bounds, icons[active_item], Nuklear.nk_rgb(255, 255, 255));
                }
                ctx.LayoutSpaceEnd();
                if (Nuklear.nk_input_is_mouse_down(ctx.Ctx.input, Nuklear.NK_BUTTON_RIGHT) == 0)
                {
                    ctx.PopupClose();
                    ret = active_item;
                }
            }
            else
                ret = -2;
            ctx.Ctx.style.window.spacing = Nuklear.nk_vec2_(4, 4);
            ctx.Ctx.style.window.padding = Nuklear.nk_vec2_(8, 8);
            ctx.PopupEnd();

            ctx.Ctx.style.window.fixed_background = background;
            ctx.Ctx.style.window.border_color = border;
            return ret;
        }

        /// <summary>
        ///     Grids the demo.
        /// </summary>
        /// <param name="ctx">Context.</param>
        /// <param name="media">Media.</param>
        public static void grid_demo(NuklearContext ctx, Media media)
        {
            int i;
            ctx.StyleSetFont(media.font_20.handle);
            if (ctx.Begin("Grid Demo", Nuklear.nk_rect_(600, 350, 275, 250),
                Nuklear.NK_WINDOW_TITLE | Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE |
                Nuklear.NK_WINDOW_NO_SCROLLBAR))
            {
                ctx.StyleSetFont(media.font_18.handle);
                ctx.LayoutRowDynamic(30, 2);
                ctx.Label("String:", Nuklear.NK_TEXT_RIGHT);
                ctx.EditString(Nuklear.NK_EDIT_FIELD, ref edit_strings[0], 64, Nuklear.nk_filter_default);
                ctx.Label("Floating point:", Nuklear.NK_TEXT_RIGHT);
                ctx.EditString(Nuklear.NK_EDIT_FIELD, ref edit_strings[1], 64, Nuklear.nk_filter_float);
                ctx.Label("Hexadecimal:", Nuklear.NK_TEXT_RIGHT);
                ctx.EditString(Nuklear.NK_EDIT_FIELD, ref edit_strings[2], 64, Nuklear.nk_filter_hex);
                ctx.Label("Binary:", Nuklear.NK_TEXT_RIGHT);
                ctx.EditString(Nuklear.NK_EDIT_FIELD, ref edit_strings[3], 64, Nuklear.nk_filter_binary);
                ctx.Label("Checkbox:", Nuklear.NK_TEXT_RIGHT);
                ctx.CheckboxLabel("Check me", ref grid_check);
                ctx.Label("Combobox:", Nuklear.NK_TEXT_RIGHT);
                if (ctx.ComboBeginLabel(items[selectedItem], Nuklear.nk_vec2_(ctx.WidgetWidth(), 200)))
                {
                    ctx.LayoutRowDynamic(25, 1);
                    for (i = 0; i < 3; ++i)
                        if (ctx.ComboItemLabel(items[i], Nuklear.NK_TEXT_LEFT))
                            selectedItem = i;
                    ctx.ComboEnd();
                }
            }
            ctx.End();
            ctx.StyleSetFont(media.font_14.handle);
        }

        public static void ui_header(NuklearContext ctx, Media media, string title)
        {
            ctx.StyleSetFont(media.font_18.handle);
            ctx.LayoutRowDynamic(20, 1);
            ctx.Label(title, Nuklear.NK_TEXT_LEFT);
        }

        public static void ui_widget(NuklearContext ctx, Media media, float height)
        {
            ctx.StyleSetFont(media.font_22.handle);
            ctx.LayoutRow(Nuklear.NK_DYNAMIC, height, 2, ratio);
            ctx.Spacing(1);
        }

        public static void ui_widget_centered(NuklearContext ctx, Media media, float height)
        {
            ctx.StyleSetFont(media.font_22.handle);
            ctx.LayoutRow(Nuklear.NK_DYNAMIC, height, 3, ratio2);
            ctx.Spacing(1);
        }

        public static void button_demo(NuklearContext ctx, Media media)
        {
            ctx.StyleSetFont(media.font_20.handle);
            ctx.Begin("Button Demo", Nuklear.nk_rect_(50, 50, 255, 610),
                Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_TITLE);

            /*------------------------------------------------
     *                  MENU
     *------------------------------------------------*/
            ctx.MenubarBegin();
            {
                /* toolbar */
                ctx.LayoutRowStatic(40, 40, 4);
                if (ctx.MenuBeginImage("Music", media.play, Nuklear.nk_vec2_(110, 120)))
                {
                    /* settings */
                    ctx.LayoutRowDynamic(25, 1);
                    ctx.MenuItemImageLabel(media.play, "Play", Nuklear.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.stop, "Stop", Nuklear.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.pause, "Pause", Nuklear.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.next, "Next", Nuklear.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.prev, "Prev", Nuklear.NK_TEXT_RIGHT);
                    ctx.MenuEnd();
                }
                ctx.ButtonImage(media.tools);
                ctx.ButtonImage(media.cloud);
                ctx.ButtonImage(media.pen);
            }
            ctx.MenubarEnd();

            /*------------------------------------------------
     *                  BUTTON
     *------------------------------------------------*/
            ui_header(ctx, media, "Push buttons");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonLabel("Push me"))
                Console.Write("pushed!\n");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(media.rocket, "Styled", Nuklear.NK_TEXT_CENTERED))
                Console.Write("rocket!\n");

            /*------------------------------------------------
     *                  REPEATER
     *------------------------------------------------*/
            ui_header(ctx, media, "Repeater");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonLabel("Press me"))
                Console.Write("pressed!\n");

            /*------------------------------------------------
     *                  TOGGLE
     *------------------------------------------------*/
            ui_header(ctx, media, "Toggle buttons");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(toggle0 ? media.checkd : media.uncheckd, "Toggle", Nuklear.NK_TEXT_LEFT))
                toggle0 = !toggle0;

            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(toggle1 ? media.checkd : media.uncheckd, "Toggle", Nuklear.NK_TEXT_LEFT))
                toggle1 = !toggle1;

            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(toggle2 ? media.checkd : media.uncheckd, "Toggle", Nuklear.NK_TEXT_LEFT))
                toggle2 = !toggle2;

            /*------------------------------------------------
     *                  RADIO
     *------------------------------------------------*/
            ui_header(ctx, media, "Radio buttons");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonSymbolLabel(option == 0 ? Nuklear.NK_SYMBOL_CIRCLE_OUTLINE : Nuklear.NK_SYMBOL_CIRCLE_SOLID, "Select",
                Nuklear.NK_TEXT_LEFT))
                option = 0;
            ui_widget(ctx, media, 35);
            if (ctx.ButtonSymbolLabel(option == 1 ? Nuklear.NK_SYMBOL_CIRCLE_OUTLINE : Nuklear.NK_SYMBOL_CIRCLE_SOLID, "Select",
                Nuklear.NK_TEXT_LEFT))
                option = 1;
            ui_widget(ctx, media, 35);
            if (ctx.ButtonSymbolLabel(option == 2 ? Nuklear.NK_SYMBOL_CIRCLE_OUTLINE : Nuklear.NK_SYMBOL_CIRCLE_SOLID, "Select",
                Nuklear.NK_TEXT_LEFT))
                option = 2;

            /*------------------------------------------------
     *                  CONTEXTUAL
     *------------------------------------------------*/
            ctx.StyleSetFont(media.font_18.handle);
            if (ctx.ContextualBegin(Nuklear.NK_WINDOW_NO_SCROLLBAR, Nuklear.nk_vec2_(150, 300), ctx.WindowGetBounds()))
            {
                ctx.LayoutRowDynamic(30, 1);
                if (ctx.ContextualItemImageLabel(media.copy, "Clone", Nuklear.NK_TEXT_RIGHT))
                    Console.Write("pressed clone!\n");
                if (ctx.ContextualItemImageLabel(media.del, "Delete", Nuklear.NK_TEXT_RIGHT))
                    Console.Write("pressed delete!\n");
                if (ctx.ContextualItemImageLabel(media.convert, "Convert", Nuklear.NK_TEXT_RIGHT))
                    Console.Write("pressed convert!\n");
                if (ctx.ContextualItemImageLabel(media.edit, "Edit", Nuklear.NK_TEXT_RIGHT))
                    Console.Write("pressed edit!\n");
                ctx.ContextualEnd();
            }
            ctx.StyleSetFont(media.font_14.handle);
            ctx.End();
        }

        public static void basic_demo(NuklearContext ctx, Media media)
        {
            int i;
            ctx.StyleSetFont(media.font_20.handle);
            ctx.Begin("Basic Demo", Nuklear.nk_rect_(320, 50, 275, 610),
                Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_TITLE);

            ui_header(ctx, media, "Popup & Scrollbar & Images");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(media.dir, "Images", Nuklear.NK_TEXT_CENTERED))
                image_active = !image_active;

            ui_header(ctx, media, "Selected Image");
            ui_widget_centered(ctx, media, 100);
            ctx.Image(media.images[selected_image]);

            if (image_active)
            {
                if (ctx.PopupBegin(Nuklear.NK_POPUP_STATIC, "Image Popup", 0, Nuklear.nk_rect_(265, 0, 320, 220)))
                {
                    ctx.LayoutRowStatic(82, 82, 3);
                    for (i = 0; i < 9; ++i)
                    {
                        if (ctx.ButtonImage(media.images[i]))
                        {
                            selected_image = i;
                            image_active = false;
                            ctx.PopupClose();
                        }
                    }
                    ctx.PopupEnd();
                }
            }

            ui_header(ctx, media, "Combo box");
            ui_widget(ctx, media, 40);
            if (ctx.ComboBeginLabel(items2[selected_item], Nuklear.nk_vec2_(ctx.WidgetWidth(), 200)))
            {
                ctx.LayoutRowDynamic(35, 1);
                for (i = 0; i < 3; ++i)
                    if (ctx.ComboItemLabel(items2[i], Nuklear.NK_TEXT_LEFT))
                        selected_item = i;
                ctx.ComboEnd();
            }

            ui_widget(ctx, media, 40);
            if (ctx.ComboBeginImageLabel(items2[selected_icon], media.images[selected_icon],
                Nuklear.nk_vec2_(ctx.WidgetWidth(), 200)))
            {
                ctx.LayoutRowDynamic(35, 1);
                for (i = 0; i < 3; ++i)
                    if (ctx.ComboItemImageLabel(media.images[i], items2[i], Nuklear.NK_TEXT_RIGHT))
                        selected_icon = i;
                ctx.ComboEnd();
            }

            ui_header(ctx, media, "Checkbox");
            ui_widget(ctx, media, 30);
            ctx.CheckboxLabel("Flag 1", ref check0);
            ui_widget(ctx, media, 30);
            ctx.CheckboxLabel("Flag 2", ref check1);

            ui_header(ctx, media, "Progressbar");
            ui_widget(ctx, media, 35);
            ctx.Progress(ref prog, 100, Nuklear.nk_true);

            if (Nuklear.nk_input_is_mouse_click_down_in_rect(ctx.Ctx.input, Nuklear.NK_BUTTON_RIGHT,
                ctx.WindowGetBounds(), Nuklear.nk_true) != 0)
            {
                piemenu_pos = ctx.Ctx.input.mouse.pos;
                piemenu_active = true;
            }

            if (piemenu_active)
            {
                var ret = ui_piemenu(ctx, piemenu_pos, 140, media.menu, 6);
                if (ret == -2)
                    piemenu_active = false;
                if (ret != -1)
                {
                    Console.Write("piemenu selected: {0}\n", ret);
                    piemenu_active = false;
                }
            }

            ctx.StyleSetFont(media.font_14.handle);
            ctx.End();
        }
    }
}