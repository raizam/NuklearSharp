using System;
using System.Collections.Generic;

namespace NuklearSharp
{
	public class CommandBase
	{
		public Command header;
		public Handle userdata;
		public CommandBase next;
	}

	public class CommandScissor : CommandBase
	{
		public short x;
		public short y;
		public ushort w;
		public ushort h;
	}

	public class CommandLine : CommandBase
	{
		public ushort line_thickness;
		public Vec2i begin = new Vec2i();
		public Vec2i end = new Vec2i();
		public Color color = new Color();
	}

	public class CommandCurve : CommandBase
	{
		public ushort line_thickness;
		public Vec2i begin = new Vec2i();
		public Vec2i end = new Vec2i();
		public Vec2i ctrl_0 = new Vec2i();
		public Vec2i ctrl_1 = new Vec2i();
		public Color color = new Color();
	}

	public class CommandRect : CommandBase
	{
		public ushort rounding;
		public ushort line_thickness;
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public Color color = new Color();
	}

	public class CommandRectFilled : CommandBase
	{
		public ushort rounding;
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public Color color = new Color();
	}

	public class CommandRectMultiColor : CommandBase
	{
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public Color left = new Color();
		public Color top = new Color();
		public Color bottom = new Color();
		public Color right = new Color();
	}

	public class CommandTriangle : CommandBase
	{
		public ushort line_thickness;
		public Vec2i a = new Vec2i();
		public Vec2i b = new Vec2i();
		public Vec2i c = new Vec2i();
		public Color color = new Color();
	}

	public class CommandTriangleFilled : CommandBase
	{
		public Vec2i a = new Vec2i();
		public Vec2i b = new Vec2i();
		public Vec2i c = new Vec2i();
		public Color color = new Color();
	}

	public class CommandCircle : CommandBase
	{
		public short x;
		public short y;
		public ushort line_thickness;
		public ushort w;
		public ushort h;
		public Color color = new Color();
	}

	public class CommandCircleFilled : CommandBase
	{
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public Color color = new Color();
	}

	public class CommandArc : CommandBase
	{
		public short cx;
		public short cy;
		public ushort r;
		public ushort line_thickness;
		public PinnedArray<float> a = new PinnedArray<float>(2);
		public Color color = new Color();
	}

	public class CommandArcFilled : CommandBase
	{
		public short cx;
		public short cy;
		public ushort r;
		public PinnedArray<float> a = new PinnedArray<float>(2);
		public Color color = new Color();
	}

	public class CommandPolygon : CommandBase
	{
		public Color color;
		public ushort line_thickness;
		public ushort point_count;
		public Vec2i[] points;
	}

	public class CommandPolygonFilled : CommandBase
	{
		public Color color;
		public ushort point_count;
		public Vec2i[] points;
	}

	public class CommandPolyline : CommandBase
	{
		public Color color;
		public ushort line_thickness;
		public ushort point_count;
		public Vec2i[] points;
	}

	public class CommandImage : CommandBase
	{
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public Image img = new Image();
		public Color col = new Color();
	}

	public unsafe class CommandText : CommandBase
	{
		public UserFont font;
		public Color background;
		public Color foreground;
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public float height;
		public char* _string_;
		public int length;
	}

	public class CommandCustom : CommandBase
	{
		public short x;
		public short y;
		public ushort w;
		public ushort h;
		public Handle callback_data;
		public Nuklear.NkCommandCustomCallback callback;
	}

	unsafe partial class CommandBuffer
	{
		private readonly List<CommandBase> _commands = new List<CommandBase>();

		public List<CommandBase> commands
		{
			get { return _commands; }
		}

		public CommandBase begin
		{
			get { return _commands[0]; }
		}

		public CommandBase last
		{
			get { return _commands[_commands.Count - 1]; }
		}

		public Rect clip;
		public int use_clipping;
		public Handle userdata = new Handle();

		private static object CreateCommand<T>() where T : new()
		{
			return new T();
		}

		public static readonly Func<object>[] _commandCreators =
		{
			null,
			() => CreateCommand<CommandScissor>(),
			() => CreateCommand<CommandLine>(),
			() => CreateCommand<CommandCurve>(),
			() => CreateCommand<CommandRect>(),
			() => CreateCommand<CommandRectFilled>(),
			() => CreateCommand<CommandRectMultiColor>(),
			() => CreateCommand<CommandCircle>(),
			() => CreateCommand<CommandCircleFilled>(),
			() => CreateCommand<CommandArc>(),
			() => CreateCommand<CommandArcFilled>(),
			() => CreateCommand<CommandTriangle>(),
			() => CreateCommand<CommandTriangleFilled>(),
			() => CreateCommand<CommandPolygon>(),
			() => CreateCommand<CommandPolygonFilled>(),
			() => CreateCommand<CommandPolyline>(),
			() => CreateCommand<CommandText>(),
			() => CreateCommand<CommandImage>(),
			() => CreateCommand<CommandCustom>()
		};

		public void CommandBuffer_init(int clip)
		{
			use_clipping = clip;
			commands.Clear();
		}

		public void CommandBuffer_reset()
		{
			commands.Clear();
			clip = Nuklear.nk_null_rect;
		}

		public CommandBase Push(int t)
		{
			if (t < 0 || t >= _commandCreators.Length || _commandCreators[t] == null) return null;

			var creator = _commandCreators[t];

			var command = (CommandBase) creator();

			command.header = new Command
			{
				type = t
			};

			commands.Add(command);

			return command;
		}

		public void nk_stroke_polygon(float* points, int point_count, float line_thickness,
			Color col)
		{
			if ((col.a == 0) || (line_thickness <= 0)) return;
			var cmd = (CommandPolygon) Push(Nuklear.NK_COMMAND_POLYGON);
			if (cmd == null) return;
			cmd.color = col;
			cmd.line_thickness = (ushort) line_thickness;
			cmd.point_count = (ushort) point_count;
			cmd.points = new Vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public void nk_fill_polygon(float* points, int point_count, Color col)
		{
			if ((col.a == 0)) return;
			var cmd = (CommandPolygonFilled) Push(Nuklear.NK_COMMAND_POLYGON_FILLED);
			if (cmd == null) return;
			cmd.color = col;
			cmd.point_count = (ushort) point_count;
			cmd.points = new Vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2 + 0];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}

		public void nk_stroke_polyline(float* points, int point_count, float line_thickness,
			Color col)
		{
			if ((col.a == 0) || (line_thickness <= 0)) return;
			var cmd = (CommandPolyline) Push(Nuklear.NK_COMMAND_POLYLINE);
			if (cmd == null) return;
			cmd.color = col;
			cmd.point_count = (ushort) point_count;
			cmd.line_thickness = (ushort) line_thickness;
			cmd.points = new Vec2i[point_count];
			for (var i = 0; i < point_count; ++i)
			{
				cmd.points[i].x = (short) points[i*2];
				cmd.points[i].y = (short) points[i*2 + 1];
			}
		}
	}
}