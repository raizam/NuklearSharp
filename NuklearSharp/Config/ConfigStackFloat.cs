using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConfigStackFloat
	{
		public int head;
		public ConfigStackFloatElement[] elements = new ConfigStackFloatElement[32];

	}
}
