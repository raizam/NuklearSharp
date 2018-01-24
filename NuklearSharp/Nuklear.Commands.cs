using System;

namespace NuklearSharp
{
	unsafe partial class Nuklear
	{
		public delegate void NkCommandCustomCallback(
			void* canvas, short x, short y, ushort w, ushort h, nk_handle callback_data);

		public class nk_command_base
		{
			public nk_command header = new nk_command();
		}

		public class nk_command_scissor: nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
		}

		public class nk_command_line : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i begin = new nk_vec2i();
			public nk_vec2i end = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_curve : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i begin = new nk_vec2i();
			public nk_vec2i end = new nk_vec2i();
			public nk_vec2i ctrl_0 = new nk_vec2i();
			public nk_vec2i ctrl_1 = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_rect : nk_command_base
		{
			public ushort rounding;
			public ushort line_thickness;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_rect_filled : nk_command_base
		{
			public ushort rounding;
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_rect_multi_color : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color left = new nk_color();
			public nk_color top = new nk_color();
			public nk_color bottom = new nk_color();
			public nk_color right = new nk_color();
		}

		public class nk_command_triangle : nk_command_base
		{
			public ushort line_thickness;
			public nk_vec2i a = new nk_vec2i();
			public nk_vec2i b = new nk_vec2i();
			public nk_vec2i c = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_triangle_filled : nk_command_base
		{
			public nk_vec2i a = new nk_vec2i();
			public nk_vec2i b = new nk_vec2i();
			public nk_vec2i c = new nk_vec2i();
			public nk_color color = new nk_color();
		}

		public class nk_command_circle : nk_command_base
		{
			public short x;
			public short y;
			public ushort line_thickness;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_circle_filled : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_color color = new nk_color();
		}

		public class nk_command_arc : nk_command_base
		{
			public short cx;
			public short cy;
			public ushort r;
			public ushort line_thickness;
			public PinnedArray<float> a = new PinnedArray<float>(2);
			public nk_color color = new nk_color();
		}

		public class nk_command_arc_filled : nk_command_base
		{
			public short cx;
			public short cy;
			public ushort r;
			public PinnedArray<float> a = new PinnedArray<float>(2);
			public nk_color color = new nk_color();
		}

		public class nk_command_polygon : nk_command_base
		{
			public nk_color color = new nk_color();
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i points = new nk_vec2i();
		}

		public class nk_command_polygon_filled : nk_command_base
		{
			public nk_color color = new nk_color();
			public ushort point_count;
			public nk_vec2i points = new nk_vec2i();
		}

		public class nk_command_polyline : nk_command_base
		{
			public nk_color color = new nk_color();
			public ushort line_thickness;
			public ushort point_count;
			public nk_vec2i points = new nk_vec2i();
		}

		public class nk_command_image : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_image img = new nk_image();
			public nk_color col = new nk_color();
		}

		public class nk_command_text : nk_command_base
		{
			public nk_user_font font;
			public nk_color background = new nk_color();
			public nk_color foreground = new nk_color();
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public float height;
			public int length;
			public PinnedArray<sbyte> _string_ = new PinnedArray<sbyte>(1);
		}

		public class nk_command_custom : nk_command_base
		{
			public short x;
			public short y;
			public ushort w;
			public ushort h;
			public nk_handle callback_data;
			public NkCommandCustomCallback callback;
		}

		public static void nk_push_custom(nk_command_buffer b, nk_rect r, NkCommandCustomCallback cb, nk_handle usr)
		{
			nk_command_custom* cmd;
			if (b == null) return;
			if ((b.use_clipping) != 0)
			{
				nk_rect* c = &b.clip; if ((((c->w) == (0)) || ((c->h) == (0))) || (!(!(((((c->x) > (r.x + r.w)) || ((c->x + c->w) < (r.x))) || ((c->y) > (r.y + r.h))) || ((c->y + c->h) < (r.y)))))) return;
			}

			cmd = (nk_command_custom*)(nk_command_buffer_push(b, (int)(NK_COMMAND_CUSTOM), (ulong)(sizeof(nk_command_arc_filled))));
			if (cmd == null) return;
			cmd->x = ((short)(r.x));
			cmd->y = ((short)(r.y));
			cmd->w = ((ushort)((0) < (r.w) ? (r.w) : (0)));
			cmd->h = ((ushort)((0) < (r.h) ? (r.h) : (0)));
			cmd->callback_data = (nk_handle)(usr);
			cmd->callback = cb;
		}

		public static void nk_command_buffer_init(nk_command_buffer cmdbuf, int clip)
		{
			cmdbuf.use_clipping = (int)(clip);
			cmdbuf.commands.Clear();
		}

		public static void nk_command_buffer_reset(nk_command_buffer buffer)
		{
			if (buffer == null) return;
			buffer.commands.Clear();
			buffer.clip = (nk_rect)(nk_null_rect);
		}

		private static object nk_create_command<T>() where T : new()
		{
			return new T();
		}

		private static Func<object>[] _commandCreators =
		{
			null,
			() => nk_create_command<nk_command_scissor>(),
			() => nk_create_command<nk_command_line>(),
			() => nk_create_command<nk_command_curve>(),
			() => nk_create_command<nk_command_rect>(),
			() => nk_create_command<nk_command_rect_filled>(),
			() => nk_create_command<nk_command_rect_multi_color>(),
			() => nk_create_command<nk_command_circle>(),
			() => nk_create_command<nk_command_circle_filled>(),
			() => nk_create_command<nk_command_arc>(),
			() => nk_create_command<nk_command_arc_filled>(),
			() => nk_create_command<nk_command_triangle>(),
			() => nk_create_command<nk_command_triangle_filled>(),
			() => nk_create_command<nk_command_polygon>(),
			() => nk_create_command<nk_command_polygon_filled>(),
			() => nk_create_command<nk_command_polyline>(),
			() => nk_create_command<nk_command_text>(),
			() => nk_create_command<nk_command_image>(),
			() => nk_create_command<nk_command_custom>()
		};

		public static object nk_command_buffer_push(nk_command_buffer b, int t)
		{
			if (b == null) return null;

			var cmd = _co

			var cmd = new nk_command
			{
				type = t
			};

			cmd = (nk_command*)(nk_buffer_alloc(b._base_, NK_BUFFER_FRONT, (ulong)(size), (ulong)(align)));
			if (cmd == null) return (null);
			b.last = ((ulong)((byte*)(cmd) - (byte*)(b._base_.memory.ptr)));
			alignment = ((ulong)((byte*)(memory) - (byte*)(unaligned)));
			cmd->type = (int)(t);
			cmd->_next_ = (ulong)(b._base_.allocated + alignment);
			b.end = (ulong)(cmd->_next_);
			return cmd;
		}
	}
}
