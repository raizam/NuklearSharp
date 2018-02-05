using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConfigStackFlags
	{
		public int head;
		public ConfigStackFlagsElement[] elements = new ConfigStackFlagsElement[32];

	}
}
