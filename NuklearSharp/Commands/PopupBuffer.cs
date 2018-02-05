using System.Collections.Generic;

namespace NuklearSharp
{
	public class PopupBuffer
	{
		private List<CommandBase> _commands;
		private int _lastIndex;

		public List<CommandBase> commands
		{
			get { return _commands; }

			set
			{
				if (value != null)
				{
					_commands = value;
					_lastIndex = value.Count - 1;
				}
			}
		}

		public CommandBase begin
		{
			get { return _commands[0]; }
		}

		public CommandBase last
		{
			get { return _commands[_commands.Count - 1]; }
		}

		public int active;
	}
}