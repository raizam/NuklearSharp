using System;
using NuklearSharp;
using NuklearSharp.MonoGame;

namespace Extended
{
    public static unsafe class Gui
    {
        private static readonly string[] Items = { "Item 0", "item 1", "item 2" };
        private static readonly float[] Ratio = { 0.15f, 0.85f };
        private static readonly float[] Ratio2 = { 0.15f, 0.50f, 0.35f };
        private static readonly string[] Items2 = { "Item 0", "item 1", "item 2" };

        private static readonly NkStr[] EditStrings = new NkStr[4];

        private static int _option = 1;
        private static bool _toggle0 = true;
        private static bool _toggle1;
        private static bool _toggle2 = true;
        private static bool _imageActive;
        private static bool _check0 = true;
        private static bool _check1;
        private static ulong _prog = 80;
        private static int _selectedItem;
        private static int _selectedImage = 3;
        private static int _selectedIcon;
        private static bool _piemenuActive;
        private static Nk.nk_vec2 _piemenuPos;
        private static bool _gridCheck = true;
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
        public static int ui_piemenu(NuklearContext ctx, Nk.nk_vec2 pos, float radius,
            Nk.nk_image[] icons, int itemCount)
        {
            var ret = -1;

            /* pie menu popup */
            var border = ctx.Ctx.Style.Window.border_color;
            var background = ctx.Ctx.Style.Window.fixed_background;

            ctx.Ctx.Style.Window.fixed_background = Nk.nk_style_item_hide();
            ctx.Ctx.Style.Window.border_color = Nk.nk_rgba(0, 0, 0, 0);

            var totalSpace = ctx.WindowGetContentRegion();
            ctx.Ctx.Style.Window.spacing = Nk.nk_vec2_(0, 0);
            ctx.Ctx.Style.Window.padding = Nk.nk_vec2_(0, 0);

            if (ctx.PopupBegin(Nk.NK_POPUP_STATIC, "piemenu", Nk.NK_WINDOW_NO_SCROLLBAR,
                Nk.nk_rect_(pos.x - totalSpace.x - radius, pos.y - radius - totalSpace.y,
                    2 * radius, 2 * radius)))
            {
                var o = ctx.WindowGetCanvas();
                var inp = ctx.Ctx.Input;

                totalSpace = ctx.WindowGetContentRegion();
                ctx.Ctx.Style.Window.spacing = Nk.nk_vec2_(4, 4);
                ctx.Ctx.Style.Window.padding = Nk.nk_vec2_(8, 8);
                ctx.LayoutRowDynamic(totalSpace.h, 1);
                Nk.nk_rect bounds;
                Nk.nk_widget(&bounds, ctx.Ctx);

                /* outer circle */
                Nk.nk_fill_circle(o, bounds, Nk.nk_rgb(50, 50, 50));
                int activeItem;
                {
                    /* circle buttons */
                    var step = 2 * 3.141592654f / Math.Max(1, itemCount);
                    float aMin = 0;
                    var aMax = step;

                    var center = Nk.nk_vec2_(bounds.x + bounds.w / 2.0f, bounds.y + bounds.h / 2.0f);
                    var drag = Nk.nk_vec2_(inp.mouse.Pos.x - center.x, inp.mouse.Pos.y - center.y);
                    var angle = (float)Math.Atan2(drag.y, drag.x);
                    if (angle < -0.0f)
                        angle += 2.0f * 3.141592654f;
                    activeItem = (int)(angle / step);

                    int i;
                    for (i = 0; i < itemCount; ++i)
                    {
                        Nk.nk_rect content;
                        Nk.nk_fill_arc(o, center.x, center.y, bounds.w / 2.0f,
                            aMin, aMax, activeItem == i ? Nk.nk_rgb(45, 100, 255) : Nk.nk_rgb(60, 60, 60));

                        /* separator line */
                        var rx = bounds.w / 2.0f;
                        float ry = 0;
                        var dx = rx * (float)Math.Cos(aMin) - ry * (float)Math.Sin(aMin);
                        var dy = rx * (float)Math.Sin(aMin) + ry * (float)Math.Cos(aMin);
                        Nk.nk_stroke_line(o, center.x, center.y,
                            center.x + dx, center.y + dy, 1.0f, Nk.nk_rgb(50, 50, 50));

                        /* button content */
                        var a = aMin + (aMax - aMin) / 2.0f;
                        rx = bounds.w / 2.5f;
                        ry = 0;
                        content.w = 30;
                        content.h = 30;
                        content.x = center.x + (rx * (float)Math.Cos(a) - ry * (float)Math.Sin(a) - content.w / 2.0f);
                        content.y = center.y + (rx * (float)Math.Sin(a) + ry * (float)Math.Cos(a) - content.h / 2.0f);
                        Nk.nk_draw_image(o, content, icons[i], Nk.nk_rgb(255, 255, 255));
                        aMin = aMax;
                        aMax += step;
                    }
                }
                {
                    /* inner circle */
                    Nk.nk_rect inner;
                    inner.x = bounds.x + bounds.w / 2 - bounds.w / 4;
                    inner.y = bounds.y + bounds.h / 2 - bounds.h / 4;
                    inner.w = bounds.w / 2;
                    inner.h = bounds.h / 2;
                    Nk.nk_fill_circle(o, inner, Nk.nk_rgb(45, 45, 45));

                    /* active icon content */
                    bounds.w = inner.w / 2.0f;
                    bounds.h = inner.h / 2.0f;
                    bounds.x = inner.x + inner.w / 2 - bounds.w / 2;
                    bounds.y = inner.y + inner.h / 2 - bounds.h / 2;
                    Nk.nk_draw_image(o, bounds, icons[activeItem], Nk.nk_rgb(255, 255, 255));
                }
                ctx.LayoutSpaceEnd();
                if (Nk.nk_input_is_mouse_down(ctx.Ctx.Input, Nk.NK_BUTTON_RIGHT) == 0)
                {
                    ctx.PopupClose();
                    ret = activeItem;
                }
            }
            else
                ret = -2;
            ctx.Ctx.Style.Window.spacing = Nk.nk_vec2_(4, 4);
            ctx.Ctx.Style.Window.padding = Nk.nk_vec2_(8, 8);
            ctx.PopupEnd();

            ctx.Ctx.Style.Window.fixed_background = background;
            ctx.Ctx.Style.Window.border_color = border;
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
            ctx.StyleSetFont(media.Font20.Handle);
            if (ctx.Begin("Grid Demo", Nk.nk_rect_(600, 350, 275, 250),
                Nk.NK_WINDOW_TITLE | Nk.NK_WINDOW_BORDER | Nk.NK_WINDOW_MOVABLE |
                Nk.NK_WINDOW_NO_SCROLLBAR))
            {
                ctx.StyleSetFont(media.Font18.Handle);
                ctx.LayoutRowDynamic(30, 2);
                ctx.Label("String:", Nk.NK_TEXT_RIGHT);
                ctx.EditString(Nk.NK_EDIT_FIELD, ref EditStrings[0], 64, Nk.nk_filter_default);
                ctx.Label("Floating point:", Nk.NK_TEXT_RIGHT);
                ctx.EditString(Nk.NK_EDIT_FIELD, ref EditStrings[1], 64, Nk.nk_filter_float);
                ctx.Label("Hexadecimal:", Nk.NK_TEXT_RIGHT);
                ctx.EditString(Nk.NK_EDIT_FIELD, ref EditStrings[2], 64, Nk.nk_filter_hex);
                ctx.Label("Binary:", Nk.NK_TEXT_RIGHT);
                ctx.EditString(Nk.NK_EDIT_FIELD, ref EditStrings[3], 64, Nk.nk_filter_binary);
                ctx.Label("Checkbox:", Nk.NK_TEXT_RIGHT);
                ctx.CheckboxLabel("Check me", ref _gridCheck);
                ctx.Label("Combobox:", Nk.NK_TEXT_RIGHT);
                if (ctx.ComboBeginLabel(Items[selectedItem], Nk.nk_vec2_(ctx.WidgetWidth(), 200)))
                {
                    ctx.LayoutRowDynamic(25, 1);
                    for (i = 0; i < 3; ++i)
                        if (ctx.ComboItemLabel(Items[i], Nk.NK_TEXT_LEFT))
                            selectedItem = i;
                    ctx.ComboEnd();
                }
            }
            ctx.End();
            ctx.StyleSetFont(media.Font14.Handle);
        }

        public static void ui_header(NuklearContext ctx, Media media, string title)
        {
            ctx.StyleSetFont(media.Font18.Handle);
            ctx.LayoutRowDynamic(20, 1);
            ctx.Label(title, Nk.NK_TEXT_LEFT);
        }

        public static void ui_widget(NuklearContext ctx, Media media, float height)
        {
            ctx.StyleSetFont(media.Font22.Handle);
            ctx.LayoutRow(Nk.NK_DYNAMIC, height, 2, Ratio);
            ctx.Spacing(1);
        }

        public static void ui_widget_centered(NuklearContext ctx, Media media, float height)
        {
            ctx.StyleSetFont(media.Font22.Handle);
            ctx.LayoutRow(Nk.NK_DYNAMIC, height, 3, Ratio2);
            ctx.Spacing(1);
        }

        public static void button_demo(NuklearContext ctx, Media media)
        {
            ctx.StyleSetFont(media.Font20.Handle);
            ctx.Begin("Button Demo", Nk.nk_rect_(50, 50, 255, 610),
                Nk.NK_WINDOW_BORDER | Nk.NK_WINDOW_MOVABLE | Nk.NK_WINDOW_TITLE);

            /*------------------------------------------------
     *                  MENU
     *------------------------------------------------*/
            ctx.MenubarBegin();
            {
                /* toolbar */
                ctx.LayoutRowStatic(40, 40, 4);
                if (ctx.MenuBeginImage("Music", media.Play, Nk.nk_vec2_(110, 120)))
                {
                    /* settings */
                    ctx.LayoutRowDynamic(25, 1);
                    ctx.MenuItemImageLabel(media.Play, "Play", Nk.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.Stop, "Stop", Nk.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.Pause, "Pause", Nk.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.Next, "Next", Nk.NK_TEXT_RIGHT);
                    ctx.MenuItemImageLabel(media.Prev, "Prev", Nk.NK_TEXT_RIGHT);
                    ctx.MenuEnd();
                }
                ctx.ButtonImage(media.Tools);
                ctx.ButtonImage(media.Cloud);
                ctx.ButtonImage(media.Pen);
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
            if (ctx.ButtonImageLabel(media.Rocket, "Styled", Nk.NK_TEXT_CENTERED))
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
            if (ctx.ButtonImageLabel(_toggle0 ? media.Checkd : media.Uncheckd, "Toggle", Nk.NK_TEXT_LEFT))
                _toggle0 = !_toggle0;

            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(_toggle1 ? media.Checkd : media.Uncheckd, "Toggle", Nk.NK_TEXT_LEFT))
                _toggle1 = !_toggle1;

            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(_toggle2 ? media.Checkd : media.Uncheckd, "Toggle", Nk.NK_TEXT_LEFT))
                _toggle2 = !_toggle2;

            /*------------------------------------------------
     *                  RADIO
     *------------------------------------------------*/
            ui_header(ctx, media, "Radio buttons");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonSymbolLabel(_option == 0 ? Nk.NK_SYMBOL_CIRCLE_OUTLINE : Nk.NK_SYMBOL_CIRCLE_SOLID, "Select",
                Nk.NK_TEXT_LEFT))
                _option = 0;
            ui_widget(ctx, media, 35);
            if (ctx.ButtonSymbolLabel(_option == 1 ? Nk.NK_SYMBOL_CIRCLE_OUTLINE : Nk.NK_SYMBOL_CIRCLE_SOLID, "Select",
                Nk.NK_TEXT_LEFT))
                _option = 1;
            ui_widget(ctx, media, 35);
            if (ctx.ButtonSymbolLabel(_option == 2 ? Nk.NK_SYMBOL_CIRCLE_OUTLINE : Nk.NK_SYMBOL_CIRCLE_SOLID, "Select",
                Nk.NK_TEXT_LEFT))
                _option = 2;

            /*------------------------------------------------
     *                  CONTEXTUAL
     *------------------------------------------------*/
            ctx.StyleSetFont(media.Font18.Handle);
            if (ctx.ContextualBegin(Nk.NK_WINDOW_NO_SCROLLBAR, Nk.nk_vec2_(150, 300), ctx.WindowGetBounds()))
            {
                ctx.LayoutRowDynamic(30, 1);
                if (ctx.ContextualItemImageLabel(media.Copy, "Clone", Nk.NK_TEXT_RIGHT))
                    Console.Write("pressed clone!\n");
                if (ctx.ContextualItemImageLabel(media.Del, "Delete", Nk.NK_TEXT_RIGHT))
                    Console.Write("pressed delete!\n");
                if (ctx.ContextualItemImageLabel(media.Convert, "Convert", Nk.NK_TEXT_RIGHT))
                    Console.Write("pressed convert!\n");
                if (ctx.ContextualItemImageLabel(media.Edit, "Edit", Nk.NK_TEXT_RIGHT))
                    Console.Write("pressed edit!\n");
                ctx.ContextualEnd();
            }
            ctx.StyleSetFont(media.Font14.Handle);
            ctx.End();
        }

        public static void basic_demo(NuklearContext ctx, Media media)
        {
            int i;
            ctx.StyleSetFont(media.Font20.Handle);
            ctx.Begin("Basic Demo", Nk.nk_rect_(320, 50, 275, 610),
                Nk.NK_WINDOW_BORDER | Nk.NK_WINDOW_MOVABLE | Nk.NK_WINDOW_TITLE);

            ui_header(ctx, media, "Popup & Scrollbar & Images");
            ui_widget(ctx, media, 35);
            if (ctx.ButtonImageLabel(media.Dir, "Images", Nk.NK_TEXT_CENTERED))
                _imageActive = !_imageActive;

            ui_header(ctx, media, "Selected Image");
            ui_widget_centered(ctx, media, 100);
            ctx.Image(media.Images[_selectedImage]);

            if (_imageActive)
            {
                if (ctx.PopupBegin(Nk.NK_POPUP_STATIC, "Image Popup", 0, Nk.nk_rect_(265, 0, 320, 220)))
                {
                    ctx.LayoutRowStatic(82, 82, 3);
                    for (i = 0; i < 9; ++i)
                    {
                        if (ctx.ButtonImage(media.Images[i]))
                        {
                            _selectedImage = i;
                            _imageActive = false;
                            ctx.PopupClose();
                        }
                    }
                    ctx.PopupEnd();
                }
            }

            ui_header(ctx, media, "Combo box");
            ui_widget(ctx, media, 40);
            if (ctx.ComboBeginLabel(Items2[_selectedItem], Nk.nk_vec2_(ctx.WidgetWidth(), 200)))
            {
                ctx.LayoutRowDynamic(35, 1);
                for (i = 0; i < 3; ++i)
                    if (ctx.ComboItemLabel(Items2[i], Nk.NK_TEXT_LEFT))
                        _selectedItem = i;
                ctx.ComboEnd();
            }

            ui_widget(ctx, media, 40);
            if (ctx.ComboBeginImageLabel(Items2[_selectedIcon], media.Images[_selectedIcon],
                Nk.nk_vec2_(ctx.WidgetWidth(), 200)))
            {
                ctx.LayoutRowDynamic(35, 1);
                for (i = 0; i < 3; ++i)
                    if (ctx.ComboItemImageLabel(media.Images[i], Items2[i], Nk.NK_TEXT_RIGHT))
                        _selectedIcon = i;
                ctx.ComboEnd();
            }

            ui_header(ctx, media, "Checkbox");
            ui_widget(ctx, media, 30);
            ctx.CheckboxLabel("Flag 1", ref _check0);
            ui_widget(ctx, media, 30);
            ctx.CheckboxLabel("Flag 2", ref _check1);

            ui_header(ctx, media, "Progressbar");
            ui_widget(ctx, media, 35);
            ctx.Progress(ref _prog, 100, Nk.nk_true);

            if (Nk.nk_input_is_mouse_click_down_in_rect(ctx.Ctx.Input, Nk.NK_BUTTON_RIGHT,
                ctx.WindowGetBounds(), Nk.nk_true) != 0)
            {
                _piemenuPos = ctx.Ctx.Input.mouse.Pos;
                _piemenuActive = true;
            }

            if (_piemenuActive)
            {
                var ret = ui_piemenu(ctx, _piemenuPos, 140, media.Menu, 6);
                if (ret == -2)
                    _piemenuActive = false;
                if (ret != -1)
                {
                    Console.Write("piemenu selected: {0}\n", ret);
                    _piemenuActive = false;
                }
            }

            ctx.StyleSetFont(media.Font14.Handle);
            ctx.End();
        }
    }
}