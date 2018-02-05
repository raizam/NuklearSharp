using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConfigStackColorElement
	{
		public Color* address;
		public Color old_value = new Color();

	}
}
