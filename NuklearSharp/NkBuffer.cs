using System;

namespace NuklearSharp
{
	public class NkBuffer<T>
	{
		private T[] _data;
		private int _count = 0;

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

		public void reset()
		{
			_count = 0;
		}

		private void ensureSize(int required)
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

		public void append(T data)
		{
			ensureSize(_count + 1);

			_data[_count] = data;
			++_count;
		}

		public void addToEnd(int length)
		{
			ensureSize(_count + length);

			updateCount(length);
		}

		public void extendAt(int index, int length)
		{
			ensureSize(_count + length);

			Array.Copy(_data, index, _data, index + length, length);

			_count += length;
		}

		public void narrowAt(int index, int length)
		{
			Array.Copy(_data, index + length, _data, index, length);
			updateCount(-length);
		}

		public void cutFromEnd(int length)
		{
			updateCount(-length);
		}

		private void updateCount(int delta)
		{
			_count += delta;
			if (_count < 0)
			{
				_count = 0;
			}
		}
	}
}
