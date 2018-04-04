namespace KlearUI
{
    public unsafe class NkWindow
    {
        public uint Seq;
        public uint Name;
        public PinnedArray<char> NameString = new PinnedArray<char>(64);
        public PanelFlags Flags;
        public NkRect Bounds = new NkRect();
        public nk_scroll Scrollbar = new nk_scroll();
        public NkCommandBuffer Buffer = new NkCommandBuffer();
        public NkPanel Layout;
        public float ScrollbarHidingTimer;
        public nk_property_state Property = new nk_property_state();
        public nk_popup_state Popup = new nk_popup_state();
        public nk_edit_state Edit = new nk_edit_state();
        public bool Scrolled;
        public nk_table Tables;
        public uint TableCount;
        public NkWindow Next;
        public NkWindow Prev;
        public NkWindow Parent;

        public void nk_push_table(nk_table tbl)
        {
            if (Tables == null)
            {
                Tables = tbl;
                tbl.next = null;
                tbl.prev = null;
                tbl.size = 0;
                TableCount = 1;
                return;
            }

            Tables.prev = tbl;
            tbl.next = Tables;
            tbl.prev = null;
            tbl.size = 0;
            Tables = tbl;
            TableCount++;
        }

        public void nk_remove_table(nk_table tbl)
        {
            if (Tables == tbl) Tables = tbl.next;
            if (tbl.next != null) tbl.next.prev = tbl.prev;
            if (tbl.prev != null) tbl.prev.next = tbl.next;
            tbl.next = null;
            tbl.prev = null;
        }

        public uint* nk_find_value(uint name)
        {
            nk_table iter = Tables;
            while (iter != null)
            {
                uint i = 0;
                uint size = iter.size;
                for (i = 0; i < size; ++i)
                {
                    if (iter.keys[i] == name)
                    {
                        iter.seq = Seq;
                        return (uint*)iter.values + i;
                    }
                }
                size = 51;
                iter = iter.next;
            }
            return null;
        }
    }
}