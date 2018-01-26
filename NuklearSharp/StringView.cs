namespace NuklearSharp
{
	public struct StringView
	{
		public static readonly StringView empty = new StringView(null, 0, 0);

		public string Text;
		public int StartPos;
		public int Length;

		public char this[int index]
		{
			get { return Text[StartPos + index]; }
		}

		public bool IsNullOrEmpty
		{
			get { return string.IsNullOrEmpty(Text) || Length <= 0 || StartPos >= Text.Length; }
		}

		public StringView(string t, int s, int l)
		{
			Text = t;
			StartPos = s;
			Length = l;
		}

		public StringView(string t)
		{
			Text = t;
			StartPos = 0;
			Length = t != null ? t.Length : 0;
		}

		public StringView(StringView t, int s, int l)
		{
			Text = t.Text;
			StartPos = t.StartPos + s;
			Length = l;
		}

		public static StringView operator +(StringView text, int pos)
		{
			return new StringView(text, pos, text.Length - pos);
		}

		public static int operator -(StringView a, StringView b)
		{
			return a.StartPos - b.StartPos;
		}

		public static bool operator ==(StringView a, string b)
		{
			if (a.Text == null && b == null)
			{
				return true;
			}

			if (a.Text == null || b == null)
			{
				return false;
			}

			if (a.Length - a.StartPos != b.Length)
			{
				return false;
			}

			return string.Compare(a.Text, a.StartPos, b, 0, b.Length) == 0;
		}

		public static bool operator !=(StringView a, string b)
		{
			return !(a == b);
		}

		public static implicit operator StringView(string t)
		{
			return new StringView(t);
		}
	}
}