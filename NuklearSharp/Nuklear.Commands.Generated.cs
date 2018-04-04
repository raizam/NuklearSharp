using System.Runtime.InteropServices;

namespace KlearUI
{

    public class nk_scroll
    {
        public uint x;
        public uint y;
    }



    public unsafe partial class nk_menu_state
    {
        public float x;
        public float y;
        public float w;
        public float h;
        public nk_scroll offset = new nk_scroll();
    }

    public unsafe partial class nk_popup_state
    {
        public NkWindow win;
        public PanelKind type;
        public NkPopupBuffer buf = new NkPopupBuffer();
        public uint name;
        public int active;
        public uint combo_count;
        public uint con_count;
        public uint con_old;
        public uint active_con;
        public NkRect header = new NkRect();
    }

    public unsafe partial class nk_edit_state
    {
        public uint name;
        public uint seq;
        public uint old;
        public int active;
        public int prev;
        public int cursor;
        public int sel_start;
        public int sel_end;
        public nk_scroll scrollbar = new nk_scroll();
        public NkTextEditMode mode;
        public byte single_line;
    }

    public unsafe partial class nk_property_state
    {
        public int active;
        public int prev;
        public string buffer;
        public int cursor;
        public int select_start;
        public int select_end;
        public uint name;
        public uint seq;
        public uint old;
        public PropertyStatus state;
    }

    public unsafe partial class nk_table
    {
        public uint seq;
        public uint size;
        public PinnedArray<uint> keys = new PinnedArray<uint>(51);
        public PinnedArray<uint> values = new PinnedArray<uint>(51);
        public nk_table next;
        public nk_table prev;
    }
}