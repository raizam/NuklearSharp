using System;

namespace NuklearSharp
{
    public class NkBuffer<T>
    {
        private T[] _data;
        private int _count;

        public T[] Data
        {
            get { return _data; }
        }

        public int Count
        {
            get { return _count; }
            set { _count = value; }
        }

        public int Size
        {
            get { return _data.Length; }
        }

        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public T this[ulong index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public NkBuffer()
        {
            _data = new T[1024];
        }

        public void Reset()
        {
            _count = 0;
        }

        private void EnsureSize(int required)
        {
            if (_data.Length >= required) return;

            // Realloc
            var oldData = _data;

            var newSize = _data.Length;
            while (newSize < required)
            {
                newSize *= 2;
            }

            _data = new T[newSize];

            Array.Copy(oldData, _data, oldData.Length);
        }

        public void AddToEnd(int length)
        {
            EnsureSize(_count + length);

            UpdateCount(length);
        }

        private void UpdateCount(int delta)
        {
            _count += delta;
            if (_count < 0)
            {
                _count = 0;
            }
        }
    }
}
