namespace KlearUI
{
    static internal class Behaviors
    {
        public static bool nk_toggle_behavior(nk_input _in_, NkRect select, ref WidgetStates state, bool active)
        {
            if (((state) & WidgetStates.Modified) != 0)
                (state) = (WidgetStates.Inactive | WidgetStates.Modified);
            else (state) = (WidgetStates.Inactive);
            if ((Nk.nk_button_behavior(ref state, @select, _in_, ButtonBehavior.Default)))
            {
                state = (WidgetStates.Active);
                active = !active;
            }

            if (((state & WidgetStates.Hover) != 0) && (InputExtentions.nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(@select)) == false))
                state |= (WidgetStates.Entered);
            else if ((InputExtentions.nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(@select)))) state |= (WidgetStates.Left);
            return active;
        }
    }
}