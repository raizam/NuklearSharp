namespace NuklearSharp
{
    static internal class Behaviors
    {
        public static bool nk_toggle_behavior(nk_input _in_, NkRect select, ref NkWidgetStates state, bool active)
        {
            if (((state) & NkWidgetStates.MODIFIED) != 0)
                (state) = (NkWidgetStates.INACTIVE | NkWidgetStates.MODIFIED);
            else (state) = (NkWidgetStates.INACTIVE);
            if ((Nk.nk_button_behavior(ref state, @select, _in_, NkButtonBehavior.Default)))
            {
                state = (NkWidgetStates.ACTIVE);
                active = !active;
            }

            if (((state & NkWidgetStates.HOVER) != 0) && (InputExtentions.nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(@select)) == false))
                state |= (NkWidgetStates.ENTERED);
            else if ((InputExtentions.nk_input_is_mouse_prev_hovering_rect(_in_, (NkRect)(@select)))) state |= (NkWidgetStates.LEFT);
            return active;
        }
    }
}