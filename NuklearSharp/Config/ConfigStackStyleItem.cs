using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConfigStackStyleItem
	{
		public int head;
		public ConfigStackStyleItemElement[] elements = new ConfigStackStyleItemElement[16];

	}
}
