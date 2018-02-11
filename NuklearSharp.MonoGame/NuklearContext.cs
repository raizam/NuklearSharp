﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NuklearSharp.MonoGame
{
	public class NuklearContext : BaseContext
	{
		private const float DepthBias = 0F;
		private const int WHEEL_DELTA = 120;

		private readonly GraphicsDevice _device;
		private readonly DynamicVertexBuffer _vertexBuffer;
		private readonly DynamicIndexBuffer _indexBuffer;
		private readonly BasicEffect basicEffect;
		private readonly List<Texture2D> _textures = new List<Texture2D>();
		private readonly RasterizerState _rasterizerState = new RasterizerState
		{
			CullMode = CullMode.None,
			ScissorTestEnable = true
		};

		private BlendState _oldBlendState;
		private RasterizerState _oldRasterizerState;
		private SamplerState _oldSamplerState;
		private DepthStencilState _oldDepthStencilState;

		private MouseState _previousMouseState = default(MouseState);
		private int _previousWheel;

		public List<Texture2D> Textures
		{
			get { return _textures; }
		}

		public NuklearContext(GraphicsDevice device)
		{
			_device = device;
			_vertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 30000,
				BufferUsage.WriteOnly);
			_indexBuffer = new DynamicIndexBuffer(device, typeof (ushort), 10000, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(device);
		}

		public int CreateTexture(Texture2D texture)
		{
			_textures.Add(texture);

			return _textures.Count;
		}

		public override int CreateTexture(int width, int height, byte[] data)
		{
			var texture = new Texture2D(_device, width, height, false, SurfaceFormat.Color);
			texture.SetData(data, 0, data.Length);

			return CreateTexture(texture);
		}

		private static void GetProjectionMatrix(int width, int height, out Matrix mtx)
		{
			const float L = 0.5f;
			var R = width + 0.5f;
			const float T = 0.5f;
			var B = height + 0.5f;
			mtx = new Matrix(2.0f/(R - L), 0.0f, 0.0f, 0.0f, 0.0f, 2.0f/(T - B), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
				(R + L)/(L - R), (T + B)/(B - T), 0.0f, 1.0f);
		}

		protected override void BeginDraw()
		{
			UpdateInput();

			basicEffect.World = Matrix.Identity;
			Matrix projection;
			GetProjectionMatrix(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height,
				out projection);
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			basicEffect.TextureEnabled = true;
			basicEffect.LightingEnabled = false;
			_device.SetVertexBuffer(_vertexBuffer);
			_device.Indices = _indexBuffer;

			_oldSamplerState = _device.SamplerStates[0];
			_oldBlendState = _device.BlendState;
			_oldDepthStencilState = _device.DepthStencilState;
			_oldRasterizerState = _device.RasterizerState;

			_device.SamplerStates[0] = SamplerState.LinearClamp;
			_device.BlendState = BlendState.NonPremultiplied;
			_device.DepthStencilState = DepthStencilState.None;
			_device.RasterizerState = _rasterizerState;
		}

		protected override unsafe void SetBuffers(byte[] vertices, short[] indices, int vertex_count, int vertex_stride)
		{
			if (vertex_count == 0) return;

			var result = new VertexPositionColorTexture[vertices.Length/sizeof (VertexPositionColorTexture)];

			fixed (VertexPositionColorTexture* vx = &result[0])
			{
				var b = (byte*) vx;
				for (int i = 0; i < vertices.Length; i++)
				{
					*(b + i) = vertices[i];
				}
			}

			for (var i = 0; i < result.Length; i++)
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

		protected override void Draw(int x, int y, int w, int h, int textureId, int startIndex, int primitiveCount)
		{
			_device.ScissorRectangle = new Rectangle(x, y, w, h);
			if (textureId != 0)
			{
				basicEffect.TextureEnabled = true;
				basicEffect.Texture = _textures[textureId - 1];
			}
			else basicEffect.TextureEnabled = false;

			foreach (var pass in basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				_device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, startIndex, primitiveCount);
			}
		}

		protected override void EndDraw()
		{
			_device.SamplerStates[0] = _oldSamplerState;
			_device.BlendState = _oldBlendState;
			_device.DepthStencilState = _oldDepthStencilState;
			_device.RasterizerState = _oldRasterizerState;
		}

		private void UpdateInput()
		{
			var state = Mouse.GetState();

			InputBegin();

			if (_previousMouseState.LeftButton == ButtonState.Released && state.LeftButton == ButtonState.Pressed)
				InputButton(Nuklear.NK_BUTTON_LEFT, state.X, state.Y, 1);
			else if (_previousMouseState.LeftButton == ButtonState.Pressed && state.LeftButton == ButtonState.Released)
				InputButton(Nuklear.NK_BUTTON_LEFT, state.X, state.Y, 0);

			if (_previousMouseState.RightButton == ButtonState.Released && state.RightButton == ButtonState.Pressed)
				InputButton(Nuklear.NK_BUTTON_RIGHT, state.X, state.Y, 1);
			else if (_previousMouseState.RightButton == ButtonState.Pressed && state.RightButton == ButtonState.Released)
				InputButton(Nuklear.NK_BUTTON_RIGHT, state.X, state.Y, 0);

			InputMotion(state.X, state.Y);
			InputScroll(new Nuklear.nk_vec2 {x = 0, y = (state.ScrollWheelValue - _previousWheel)/WHEEL_DELTA});
			InputEnd();

			_previousWheel = state.ScrollWheelValue;
			_previousMouseState = state;
		}

		public bool BeginTitled(string name, string title, Rectangle bounds, uint flags)
		{
			return BeginTitled(name, title, bounds.ToRect(), flags);
		}

		public bool ButtonColor(Color color)
		{
			return ButtonColor(color.ToNkColor());
		}

		public void LabelColored(string str, uint align, Color color)
		{
			LabelColored(str, align, color.ToNkColor());
		}

		public bool ComboBeginColor(Color color, Vector2 size)
		{
			return ComboBeginColor(color.ToNkColor(), size.ToNkVec2());
		}

		public Color ColorPicker(Color color, int fmt)
		{
			return ColorPicker(color.ToNkColorf(), fmt).ToColor();
		}
	}
}