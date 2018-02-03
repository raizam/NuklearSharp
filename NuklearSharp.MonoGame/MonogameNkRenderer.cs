using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NuklearSharp.MonoGame
{
	public class MonogameNkRenderer: IRenderer
	{
		private const float DepthBias = 0F;

		private readonly GraphicsDevice _device;
		private readonly DynamicVertexBuffer _vertexBuffer;
		private readonly DynamicIndexBuffer _indexBuffer;
		private readonly BasicEffect basicEffect;
		private readonly List<Texture2D> _textures = new List<Texture2D>();

		public List<Texture2D> Textures
		{
			get { return _textures; }
		}

		public MonogameNkRenderer(GraphicsDevice device)
		{
			_device = device;
			_vertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 30000,
				BufferUsage.WriteOnly);
			_indexBuffer = new DynamicIndexBuffer(device, typeof (ushort), 10000, BufferUsage.WriteOnly);
			basicEffect = new BasicEffect(device);
		}

		public int CreateTexture(int width, int height, byte[] data)
		{
			var texture = new Texture2D(_device, width, height, false, SurfaceFormat.Color);
			texture.SetData(data, 0, data.Length);
			_textures.Add(texture);

			return _textures.Count;
		}

		private void getProjectionMatrix(int width, int height, out Matrix mtx)
		{
			const float L = 0.5f;
			var R = width + 0.5f;
			const float T = 0.5f;
			var B = height + 0.5f;
			mtx = new Matrix(2.0f/(R - L), 0.0f, 0.0f, 0.0f, 0.0f, 2.0f/(T - B), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
				(R + L)/(L - R), (T + B)/(B - T), 0.0f, 1.0f);
		}

		public void BeginDraw()
		{
			basicEffect.World = Matrix.Identity;
			Matrix projection;
			getProjectionMatrix(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height, out projection);
			basicEffect.Projection = projection;
			basicEffect.VertexColorEnabled = true;
			basicEffect.TextureEnabled = true;
			basicEffect.LightingEnabled = false;
			_device.SetVertexBuffer(_vertexBuffer);
			_device.Indices = _indexBuffer;
			var rasterizerState = new RasterizerState
			{
				CullMode = CullMode.CullCounterClockwiseFace,
				DepthBias = DepthBias,
				ScissorTestEnable = true
			};

			_device.BlendState = BlendState.NonPremultiplied;
			_device.RasterizerState = rasterizerState;
		}

		public unsafe void SetBuffers(byte[] vertices, short[] indices, int vertex_count, int vertex_stride)
		{
			if (vertex_count == 0) return;

			var result = new VertexPositionColorTexture[vertices.Length / sizeof(VertexPositionColorTexture)];

			fixed (VertexPositionColorTexture* vx = &result[0])
			{
				var b = (byte*)vx;
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

		public void Draw(int x, int y, int w, int h, int textureId, int startIndex, int primitiveCount)
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

		public void EndDraw()
		{
		}
	}
}