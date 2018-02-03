using System.Text;

namespace NuklearSharp.Generation
{
	public static class Utils
	{
		public static string ToCSharpName(this string nkName)
		{
			nkName = nkName.Trim();

			if (nkName.StartsWith("nk_"))
			{
				nkName = nkName.Substring(3);
			}

			if (string.IsNullOrEmpty(nkName))
			{
				return string.Empty;
			}

			var parts = nkName.Split('_');

			var sb = new StringBuilder();
			foreach (var p in parts)
			{
				if (string.IsNullOrEmpty(p))
				{
					continue;
				}

				sb.Append(char.ToUpper(p[0]) + p.Substring(1));
			}

			return sb.ToString();
		}
	}
}
