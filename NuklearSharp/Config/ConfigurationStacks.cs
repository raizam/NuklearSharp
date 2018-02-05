using System;
using System.Runtime.InteropServices;

namespace NuklearSharp
{
	public unsafe partial class ConfigurationStacks
	{
		public ConfigStackStyleItem style_items = new ConfigStackStyleItem();
		public ConfigStackFloat floats = new ConfigStackFloat();
		public ConfigStackVec2 vectors = new ConfigStackVec2();
		public ConfigStackFlags flags = new ConfigStackFlags();
		public ConfigStackColor colors = new ConfigStackColor();
		public ConfigStackUserFont fonts = new ConfigStackUserFont();
		public ConfigStackButtonBehavior button_behaviors = new ConfigStackButtonBehavior();

	}
}
