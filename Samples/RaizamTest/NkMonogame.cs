using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuklearSharp;

namespace RaizamTest
{
	public partial class NuklearContext //  : IGameComponent, IDrawable //, IUpdateable, IDrawableDecorator
	{
		readonly GraphicsDevice device;
		//   readonly IApp app;
		public NuklearContext(GraphicsDevice device)
		{
			this.device = device;
		}

		public List<Texture2D> textures = new List<Texture2D>();
		private DynamicVertexBuffer vertexBuffer;
		private DynamicIndexBuffer indexBuffer;
		private BasicEffect basicEffect;


		Matrix world = Matrix.Identity;
		Matrix view = Matrix.Identity; //Matrix.CreateLookAt(Vector3.Forward, Vector3.Backward, Vector3.Up);
		Matrix projection;

		protected int CreateTexture(byte[] data, int width, int height)
		{
			Texture2D texture = new Texture2D(device, width, height, false, SurfaceFormat.Color);
			texture.SetData(data, 0, width*height*4);
			using (var file = File.OpenWrite(".\\atlas_output.png"))
				texture.SaveAsPng(file, width, height);
			textures.Add(texture);
			return textures.Count;
		}

		public void Initialize()
		{
			_ctx = new Nuklear.nk_context();
			atlas = new Nuklear.nk_font_atlas();
			_cmds = new Nuklear.nk_buffer();
			Nuklear.nk_init_default(_ctx, null);

			Nuklear.nk_buffer_init_default(_cmds);
			vertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 30000,
				BufferUsage.WriteOnly);
			indexBuffer = new DynamicIndexBuffer(device, typeof (ushort), 10000, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(device);

		}

		MouseState previousMouseState = default(MouseState);

		float DepthBias = 0F;
		float SlopeScaleDepthBias = 0F;

		public void Draw(GameTime gameTime)
		{
			handleInputs();

			basicEffect.World = world;
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			basicEffect.TextureEnabled = true;
			basicEffect.LightingEnabled = false;
			device.SetVertexBuffer(vertexBuffer);
			device.Indices = indexBuffer;
			RasterizerState rasterizerState = new RasterizerState();
			rasterizerState.CullMode = CullMode.CullCounterClockwiseFace;
			//     rasterizerState.SlopeScaleDepthBias
			rasterizerState.DepthBias = DepthBias;
			// rasterizerState.SlopeScaleDepthBias = SlopeScaleDepthBias;
			//  rasterizerState.
			// rasterizerState.
			rasterizerState.ScissorTestEnable = true;

			//   rasterizerState.MultiSampleAntiAlias = true;
			device.BlendState = BlendState.NonPremultiplied;
			device.RasterizerState = rasterizerState;

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
					_null_ = nullTexture,
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
					//          SetSisorRect((int)cmd->clip_rect.x, (int)cmd->clip_rect.y, (int)cmd->clip_rect.w, (int)cmd->clip_rect.h, cmd->texture.id, offset);
					//scissor.left = (LONG)cmd->clip_rect.x;
					//scissor.right = (LONG)(cmd->clip_rect.x + cmd->clip_rect.w);
					//scissor.top = (LONG)cmd->clip_rect.y;
					//scissor.bottom = (LONG)(cmd->clip_rect.y + cmd->clip_rect.h);
					offset += cmd->elem_count;
				}
				Nuklear.nk_buffer_free(vbuf);
				Nuklear.nk_buffer_free(ebuf);
				Nuklear.nk_clear(_ctx);
			}
		}


		const int WHEEL_DELTA = 120;
		int previousWheel = 0;

		private void handleInputs()
		{
			// Clear();
			getProjectionMatrix(device.PresentationParameters.Bounds.Width, device.PresentationParameters.Bounds.Height,
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
			device.ScissorRectangle = new Rectangle(x, y, w, h);
			if (textureId != 0)
			{
				basicEffect.TextureEnabled = true;
				basicEffect.Texture = textures[textureId - 1];
			}
			else basicEffect.TextureEnabled = false;

			foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, startIndex, primitiveCount);
			}
		}

		private void getProjectionMatrix(int width, int height, ref Matrix mtx)
		{

			mtx = Matrix.CreateOrthographic(width, height, 0, 500);
			//return;
			const float L = 0.5f;
			float R = (float) width + 0.5f;
			const float T = 0.5f;
			float B = (float) height + 0.5f;
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
			//fixed (float* pos = result[i].Position.Z)
			//    *(pos + 2) = 0F + i;// + i / 10F;



			vertexBuffer.SetData(result);
			indexBuffer.SetData(indices);
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