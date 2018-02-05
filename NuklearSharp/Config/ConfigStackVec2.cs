using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConfigStackVec2
	{
		public int head;
		public ConfigStackVec2Element[] elements = new ConfigStackVec2Element[16];

	}
}
