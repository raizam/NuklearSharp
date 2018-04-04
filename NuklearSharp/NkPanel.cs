namespace KlearUI
{
    public class NkPanel
        {
            public PanelKind Type;
            public PanelFlags Flags;
            public NkRect Bounds = new NkRect();
            public nk_scroll Offset;
            public float AtX;
            public float AtY;
            public float MaxX;
            public float FooterHeight;
            public float HeaderHeight;
            public float Border;
            public bool HasScrolling;
            public NkRect Clip = new NkRect();
            public nk_menu_state Menu = new nk_menu_state();
            public nk_row_layout Row = new nk_row_layout();
            public NkChart Chart = new NkChart();
            public NkCommandBuffer Buffer;
            public NkPanel Parent;
        }
}