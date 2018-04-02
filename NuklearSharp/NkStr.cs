namespace NuklearSharp
{
    public unsafe class NkStr
    {
        public string str;

        public int len
        {
            get { return str == null ? 0 : str.Length; }
        }

        public char this[ulong index]
        {
            get { return str[(int)index]; }
        }

        public char this[int index]
        {
            get { return str[index]; }
        }

        public int append(char* ptr, int l)
        {
            if (ptr == null || (l == 0)) return 0;

            var s2 = new string(ptr);
            str += s2;
            return l;
        }

        public int append(char* ptr)
        {
            return append(ptr, Nuklear.nk_strlen(ptr));
        }

        public int insert_at(int pos, char* ptr, int l)
        {
            var s2 = new string(ptr);

            if (str == null)
            {
                str = s2;
            }
            else
            {
                str = str.Substring(0, pos) + s2 + str.Substring(pos);
            }

            return l;
        }

        public int insert_at(int pos, char* ptr)
        {
            return insert_at(pos, ptr, Nuklear.nk_strlen(ptr));
        }

        public void remove(int l)
        {
            if (len == 0) return;

            str = str.Substring(0, str.Length - l);
        }

        public void remove_at(int pos, int l)
        {
            if (l == 0) return;

            str = str.Substring(0, pos) + str.Substring(pos + l);
        }
    }
}