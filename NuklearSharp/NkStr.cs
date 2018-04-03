namespace NuklearSharp
{
    public unsafe class NkStr
    {
        public string Str;

        public int Len
        {
            get { return Str == null ? 0 : Str.Length; }
        }

        public char this[ulong index]
        {
            get { return Str[(int)index]; }
        }

        public char this[int index]
        {
            get { return Str[index]; }
        }

        public int Append(char* ptr, int l)
        {
            if (ptr == null || l == 0) return 0;

            var s2 = new string(ptr);
            Str += s2;
            return l;
        }

        public int Append(char* ptr)
        {
            return Append(ptr, Nk.nk_strlen(ptr));
        }

        public int insert_at(int pos, char* ptr, int l)
        {
            var s2 = new string(ptr);

            if (Str == null)
            {
                Str = s2;
            }
            else
            {
                Str = Str.Substring(0, pos) + s2 + Str.Substring(pos);
            }

            return l;
        }

        public int insert_at(int pos, char* ptr)
        {
            return insert_at(pos, ptr, Nk.nk_strlen(ptr));
        }

        public void Remove(int l)
        {
            if (Len == 0) return;

            Str = Str.Substring(0, Str.Length - l);
        }

        public void remove_at(int pos, int l)
        {
            if (l == 0) return;
            //Str.Remove(pos, l);
            Str = Str.Remove(pos, l); //Str.Substring(0, pos) + Str.Substring(pos + l);
        }
    }
}