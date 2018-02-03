using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NuklearSharp.MonoGame
{
	public partial class NuklearContext
	{
		private const int WHEEL_DELTA = 120;
		private const float DepthBias = 0F;

		public Nuklear.nk_context _ctx;
		private Nuklear.nk_font_atlas _atlas;
		private readonly Nuklear.nk_buffer _cmds;
		private readonly GraphicsDevice _device;
		private readonly DynamicVertexBuffer _vertexBuffer;
		private readonly DynamicIndexBuffer _indexBuffer;
		private readonly BasicEffect basicEffect;
		private readonly Matrix world = Matrix.Identity;
		private Matrix projection;
		private readonly List<Texture2D> _textures = new List<Texture2D>();

		MouseState previousMouseState = default(MouseState);
		private int previousWheel;

		public List<Texture2D> Textures
		{
			get { return _textures; }
		}

		public NuklearContext(GraphicsDevice device)
		{
			this._device = device;
			_ctx = new Nuklear.nk_context();
			_atlas = new Nuklear.nk_font_atlas();
			_cmds = new Nuklear.nk_buffer();
			Nuklear.nk_init_default(_ctx, null);

			Nuklear.nk_buffer_init_default(_cmds);
			_vertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 30000,
				BufferUsage.WriteOnly);
			_indexBuffer = new DynamicIndexBuffer(device, typeof (ushort), 10000, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(device);
		}

		public NkFont LoadFont(Stream input, float height)
		{
			_atlas = new Nuklear.nk_font_atlas();
			Nuklear.nk_font_atlas_begin(_atlas);

			Nuklear.nk_font font = null;
			using (var memoryStream = new MemoryStream())
			{
				input.CopyTo(memoryStream);
				var bytes = memoryStream.ToArray();

				unsafe
				{
					fixed (void* ptr = bytes)
					{
						font = Nuklear.nk_font_atlas_add_from_memory(_atlas, ptr, (ulong) bytes.Length, height, null);
					}
				}
			}

			int w = 0, h = 0;
			byte[] arr;

			unsafe
			{
				var image = (int*) Nuklear.nk_font_atlas_bake(_atlas, ref w, ref h, Nuklear.NK_FONT_ATLAS_RGBA32);
				int buffSize = w*h*4;
				arr = new byte[buffSize];
				Marshal.Copy((IntPtr) image, arr, 0, buffSize);
			}

			var texture = new Texture2D(_device, w, h, false, SurfaceFormat.Color);
			texture.SetData(arr, 0, w*h*4);

			_textures.Add(texture);

			unsafe
			{
				Nuklear.nk_font_atlas_end(_atlas, new Nuklear.nk_handle {id = _textures.Count}, null);
			}

			return new NkFont(font, texture);
		}

		public void SetFont(NkFont font)
		{
			Nuklear.nk_style_set_font(_ctx, font.Font.handle);
		}

		public bool WindowBegin(string name, Rectangle rect, int flags)
		{
			Nuklear.nk_rect r = new Nuklear.nk_rect {x = rect.X, y = rect.Y, h = rect.Height, w = rect.Width};
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_begin_titled(_ctx, ptr, ptr, r, (uint) (flags)) == 1;
				}
			}
		}

		public void WindowEnd()
		{
			Nuklear.nk_end(_ctx);
		}

		public void Slider(ref float value, float min, float max, float step)
		{
			Nuklear.nk_slider_float(_ctx, min, ref value, max, step);
		}

		public float Slider(float value, float min, float max, float step)
		{
			float val = value;
			Nuklear.nk_slider_float(_ctx, min, ref val, max, step);
			return val;
		}

		public void Slider(ref int value, int min, int max, int step)
		{
			Nuklear.nk_slider_int(_ctx, min, ref value, max, step);
		}

		public bool Button(string label)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{
					return Nuklear.nk_button_label(_ctx, ptr) == 1;
				}
			}
		}

		public bool Button(Color color)
		{
			return Nuklear.nk_button_color(_ctx, color.ToNkColor()) == 1;
		}

		public bool Option(string label, bool isActive)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{
					return Nuklear.nk_option_label(_ctx, ptr, isActive ? 1 : 0) == 1;
				}
			}
		}

		public void RowStatic(float height, int item_width, int cols)
		{
			Nuklear.nk_layout_row_static(_ctx, height, item_width, cols);
		}

		public void RowDynamic(float height, int cols)
		{
			Nuklear.nk_layout_row_dynamic(_ctx, height, cols);
		}

		public void RowBegin(int format, float height, int cols)
		{
			Nuklear.nk_layout_row_begin(_ctx, format, height, cols);
		}

		public void RowEnd()
		{
			Nuklear.nk_layout_row_end(_ctx);
		}

		public void RowPush(float value)
		{
			Nuklear.nk_layout_row_push(_ctx, value);
		}

		public void Label(string label, int textAlign)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{
					Nuklear.nk_label(_ctx, ptr, (uint) textAlign);
				}
			}
		}

		public void Label(string label, int textAlign, Color color)
		{
			unsafe
			{
				fixed (char* ptr = label)
				{
					Nuklear.nk_label_colored(_ctx, ptr, (uint) textAlign, color.ToNkColor());
				}
			}
		}

		public void Property(string name, float min, ref float val, float max, float step = 1F, float incrPerPixel = 1F)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{

					Nuklear.nk_property_float(_ctx, ptr, min, ref val, max, step, incrPerPixel);
				}
			}
		}

		public void Property(string name, double min, ref double val, double max, double step = 1.0, float incrPerPixel = 1F)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					Nuklear.nk_property_double(_ctx, ptr, min, ref val, max, step, incrPerPixel);
				}
			}
		}

		public void Property(string name, int min, ref int val, int max, int step = 1, int incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					Nuklear.nk_property_int(_ctx, ptr, min, ref val, max, step, incrPerPixel);
				}
			}
		}

		public int Property(string name, int min, int val, int max, int step = 1, int incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_propertyi(_ctx, ptr, min, val, max, step, incrPerPixel);
				}
			}
		}

		public float Property(string name, float min, float val, float max, float step = 1, float incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_propertyf(_ctx, ptr, min, val, max, step, incrPerPixel);
				}
			}
		}

		public double Property(string name, double min, double val, double max, double step = 1, float incrPerPixel = 1)
		{
			unsafe
			{
				fixed (char* ptr = name)
				{
					return Nuklear.nk_propertyd(_ctx, ptr, min, val, max, step, incrPerPixel);
				}
			}
		}

		public void ComboEnd()
		{
			Nuklear.nk_combo_end(_ctx);
		}

		public float WidgetWidth
		{
			get { return Nuklear.nk_widget_width(_ctx); }
		}

		public void Draw(GameTime gameTime)
		{
			handleInputs();

			basicEffect.World = world;
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			basicEffect.TextureEnabled = true;
			basicEffect.LightingEnabled = false;
			_device.SetVertexBuffer(_vertexBuffer);
			_device.Indices = _indexBuffer;
			RasterizerState rasterizerState = new RasterizerState
			{
				CullMode = CullMode.CullCounterClockwiseFace,
				DepthBias = DepthBias,
				ScissorTestEnable = true
			};

			_device.BlendState = BlendState.NonPremultiplied;
			_device.RasterizerState = rasterizerState;

			unsafe
			{
				Nuklear.nk_buffer vbuf = new Nuklear.nk_buffer();
				Nuklear.nk_buffer ebuf = new Nuklear.nk_buffer();
				Nuklear.nk_draw_command* cmd;
				//  ushort* offset = null;
				Nuklear.nk_convert_config config = new Nuklear.nk_convert_config
				{
					vertex_size = (uint) sizeof (nk_vertex),
					vertex_alignment = 4,
					global_alpha = 1f,
					shape_AA = Nuklear.NK_ANTI_ALIASING_ON,
					line_AA = Nuklear.NK_ANTI_ALIASING_ON,
					circle_segment_count = 22,
					curve_segment_count = 22,
					arc_segment_count = 22,
					vertex_layout = new[]
					{
						new Nuklear.nk_draw_vertex_layout_element
						{
							attribute = Nuklear.NK_VERTEX_POSITION,
							format = Nuklear.NK_FORMAT_FLOAT,
							offset = 0
						},
						new Nuklear.nk_draw_vertex_layout_element
						{
							attribute = Nuklear.NK_VERTEX_COLOR,
							format = Nuklear.NK_FORMAT_B8G8R8A8,
							offset = 12
						},
						new Nuklear.nk_draw_vertex_layout_element
						{
							attribute = Nuklear.NK_VERTEX_TEXCOORD,
							format = Nuklear.NK_FORMAT_FLOAT,
							offset = 16
						},
						new Nuklear.nk_draw_vertex_layout_element
						{
							attribute = Nuklear.NK_VERTEX_ATTRIBUTE_COUNT
						}
					}
				};

				/* convert shapes into vertexes */
				Nuklear.nk_buffer_init_default(vbuf);
				Nuklear.nk_buffer_init_default(ebuf);
				Nuklear.nk_convert(_ctx, _cmds, vbuf, ebuf, config);

				var vSize = (ulong) sizeof (nk_vertex);

				var vertex_count = (uint) (vbuf.needed/vSize);
				//var count = vbuf.memory.size / sizeof(nk_vertex);
				var vertices = new byte[(int) vbuf.needed];
				var indices = new byte[ebuf.needed];

				Marshal.Copy((IntPtr) vbuf.memory.ptr, vertices, 0, (int) vbuf.needed);

				/* iterate over and execute each draw command */
				uint offset = 0;

				Marshal.Copy((IntPtr) ebuf.memory.ptr, indices, 0, indices.Length);

				setBuffers(vertices, indices, (int) vertex_count, (int) sizeof (nk_vertex));
				for (cmd = Nuklear.nk__draw_begin(_ctx, _cmds); (cmd) != null; (cmd) = Nuklear.nk__draw_next(cmd, _cmds, _ctx))
				{
					if (cmd->elem_count == 0) continue;

					drawCommand((int) cmd->clip_rect.x, (int) cmd->clip_rect.y, (int) cmd->clip_rect.w, (int) cmd->clip_rect.h,
						cmd->texture.id, (int) offset, (int) (cmd->elem_count/3));
					offset += cmd->elem_count;
				}
				Nuklear.nk_buffer_free(vbuf);
				Nuklear.nk_buffer_free(ebuf);
				Nuklear.nk_clear(_ctx);
			}
		}

		private void handleInputs()
		{
			// Clear();
			getProjectionMatrix(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height,
				ref projection);

			var state = Mouse.GetState();

			Nuklear.nk_input_begin(_ctx);

			if (previousMouseState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
				Nuklear.nk_input_button(_ctx, Nuklear.NK_BUTTON_LEFT, state.X, state.Y, 1);
			else if (previousMouseState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released)
				Nuklear.nk_input_button(_ctx, Nuklear.NK_BUTTON_LEFT, state.X, state.Y, 0);

			if (previousMouseState.RightButton == ButtonState.Released && state.RightButton == ButtonState.Pressed)
				Nuklear.nk_input_button(_ctx, Nuklear.NK_BUTTON_RIGHT, state.X, state.Y, 1);
			else if (previousMouseState.RightButton == ButtonState.Pressed && state.RightButton == ButtonState.Released)
				Nuklear.nk_input_button(_ctx, Nuklear.NK_BUTTON_RIGHT, state.X, state.Y, 0);

			Nuklear.nk_input_motion(_ctx, state.X, state.Y);
			Nuklear.nk_input_scroll(_ctx, new Nuklear.nk_vec2 {x = 0, y = (state.ScrollWheelValue - previousWheel)/WHEEL_DELTA});
			Nuklear.nk_input_end(_ctx);
			previousWheel = state.ScrollWheelValue;
			previousMouseState = state;
		}

		protected void drawCommand(int x, int y, int w, int h, int textureId, int startIndex, int primitiveCount)
		{
			_device.ScissorRectangle = new Rectangle(x, y, w, h);
			if (textureId != 0)
			{
				basicEffect.TextureEnabled = true;
				basicEffect.Texture = _textures[textureId - 1];
			}
			else basicEffect.TextureEnabled = false;

			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, startIndex, primitiveCount);
			}
		}

		private void getProjectionMatrix(int width, int height, ref Matrix mtx)
		{
			const float L = 0.5f;
			var R = width + 0.5f;
			const float T = 0.5f;
			var B = height + 0.5f;
			mtx = new Matrix(2.0f/(R - L), 0.0f, 0.0f, 0.0f, 0.0f, 2.0f/(T - B), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
				(R + L)/(L - R), (T + B)/(B - T), 0.0f, 1.0f);
		}

		protected unsafe void setBuffers(byte[] vertices, byte[] Ibuffer, int vertex_count, int vertex_stride)
		{
			if (vertex_count == 0) return;

			VertexPositionColorTexture[] result =
				new VertexPositionColorTexture[vertices.Length/sizeof (VertexPositionColorTexture)];

			var s1 = sizeof (VertexPositionColorTexture);

			var nkvSize = sizeof (nk_vertex);

			ushort[] indices = new ushort[Ibuffer.Length/2];
			fixed (VertexPositionColorTexture* vx = &result[0])
			{
				var b = (byte*) vx;
				for (int i = 0; i < vertices.Length; i++)
				{
					*(b + i) = vertices[i];
				}
			}

			fixed (ushort* vx = &indices[0])
			{
				var b = (byte*) vx;
				for (int i = 0; i < Ibuffer.Length; i++)
				{
					*(b + i) = Ibuffer[i];
				}
			}

			for (int i = 0; i < result.Length; i++)
			{
				var z = result[i].Position.Z;
				var c = result[i].Color;
				result[i].Color = new Color(c.B, c.G, c.R, c.A);
				if (float.IsNaN(z) || float.IsInfinity(z))
					result[i].Position.Z = 0F;
			}

			_vertexBuffer.SetData(result);
			_indexBuffer.SetData(indices);
		}

		public bool ComboBeginColor(Color color, Vector2 size)
		{
			return Nuklear.nk_combo_begin_color(_ctx, color.ToNkColor(), size.ToNkVec2()) == 1;
		}

		public Color ColorPicker(Color color, int format = Nuklear.NK_RGBA)
		{
			var c = color.ToNkColorf();
			var cp = Nuklear.nk_color_picker(_ctx, c, format);

			return cp.ToColor();
		}
	}
}