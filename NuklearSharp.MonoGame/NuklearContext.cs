using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NuklearSharp.MonoGame
{
    public class NuklearContext : BaseContext
    {
        private const int WHEEL_DELTA = 120;

        private readonly GraphicsDevice _device;
        private DynamicVertexBuffer _vertexBuffer;
        private DynamicIndexBuffer _indexBuffer;
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
        private KeyboardState _previousKeyboardState;

        private int _previousWheel;

        public List<Texture2D> Textures
        {
            get { return _textures; }
        }

        public NuklearContext(GraphicsDevice device)
        {
            _device = device;
            _vertexBuffer = new DynamicVertexBuffer(device, VertexPositionColorTexture.VertexDeclaration, 2000,
                BufferUsage.WriteOnly);
            _indexBuffer = new DynamicIndexBuffer(device, typeof(ushort), 6000, BufferUsage.WriteOnly);
            basicEffect = new BasicEffect(device);

            ConvertConfig.vertex_size = (uint)VertexPositionColorTexture.VertexDeclaration.VertexStride;

            ConvertConfig.vertex_layout = new[]
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
            };
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
            mtx = new Matrix(2.0f / (R - L), 0.0f, 0.0f, 0.0f, 0.0f, 2.0f / (T - B), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                (R + L) / (L - R), (T + B) / (B - T), 0.0f, 1.0f);
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

        protected override unsafe void SetBuffers(byte[] vertices, ushort[] indices, int indices_count, int vertex_count)
        {
            if (vertex_count == 0) return;

            var result = new VertexPositionColorTexture[vertex_count];

            fixed (VertexPositionColorTexture* vx = &result[0])
            {
                var b = (byte*)vx;
                for (int i = 0; i < vertex_count * sizeof(VertexPositionColorTexture); i++)
                {
                    *(b + i) = vertices[i];
                }
            }

            for (var i = 0; i < vertex_count; i++)
            {
                var c = result[i].Color;
                result[i].Color = new Color(c.B, c.G, c.R, c.A);
            }

            if (_vertexBuffer.VertexCount < result.Length)
            {
                // Resize vertex buffer if data doesnt fit
                _vertexBuffer = new DynamicVertexBuffer(_device, VertexPositionColorTexture.VertexDeclaration, result.Length * 2,
                    BufferUsage.WriteOnly);
            }
            _vertexBuffer.SetData(result);


            if (_indexBuffer.IndexCount < indices_count)
            {
                // Resize index buffer if data doesnt fit
                _indexBuffer = new DynamicIndexBuffer(_device, typeof(ushort), indices_count * 2, BufferUsage.WriteOnly);
            }

            _indexBuffer.SetData(indices, 0, indices_count);
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
            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            InputBegin();

            InputKey(Nuklear.NK_KEY_DEL, keyboardState.IsKeyDown(Keys.Delete));
            InputKey(Nuklear.NK_KEY_ENTER, keyboardState.IsKeyDown(Keys.Enter));
            InputKey(Nuklear.NK_KEY_TAB, keyboardState.IsKeyDown(Keys.Tab));
            InputKey(Nuklear.NK_KEY_BACKSPACE, keyboardState.IsKeyDown(Keys.Back));
            InputKey(Nuklear.NK_KEY_LEFT, keyboardState.IsKeyDown(Keys.Left));
            InputKey(Nuklear.NK_KEY_RIGHT, keyboardState.IsKeyDown(Keys.Right));
            InputKey(Nuklear.NK_KEY_UP, keyboardState.IsKeyDown(Keys.Up));
            InputKey(Nuklear.NK_KEY_DOWN, keyboardState.IsKeyDown(Keys.Down));
            if (keyboardState.IsKeyDown(Keys.LeftControl) ||
                keyboardState.IsKeyDown(Keys.RightControl))
            {
                InputKey(Nuklear.NK_KEY_COPY, keyboardState.IsKeyDown(Keys.C));
                InputKey(Nuklear.NK_KEY_PASTE, keyboardState.IsKeyDown(Keys.P));
                InputKey(Nuklear.NK_KEY_CUT, keyboardState.IsKeyDown(Keys.X));
                InputKey(Nuklear.NK_KEY_CUT, keyboardState.IsKeyDown(Keys.E));
                InputKey(Nuklear.NK_KEY_SHIFT, true);
            }
            else
            {
                InputKey(Nuklear.NK_KEY_COPY, false);
                InputKey(Nuklear.NK_KEY_PASTE, false);
                InputKey(Nuklear.NK_KEY_CUT, false);
                InputKey(Nuklear.NK_KEY_SHIFT, false);
            }

            var isShiftDown = keyboardState.IsKeyDown(Keys.LeftShift) ||
                              keyboardState.IsKeyDown(Keys.RightShift);

            var pressedKeys = keyboardState.GetPressedKeys();
            for (var i = 0; i < pressedKeys.Length; ++i)
            {
                var key = pressedKeys[i];
                if (!_previousKeyboardState.IsKeyDown(key))
                {
                    var ch = key.ToChar(isShiftDown);
                    if (ch != null)
                    {
                        InputChar(ch.Value);
                    }
                }
            }

            InputButton(Nuklear.NK_BUTTON_LEFT, mouseState.X, mouseState.Y, mouseState.LeftButton == ButtonState.Pressed);
            InputButton(Nuklear.NK_BUTTON_MIDDLE, mouseState.X, mouseState.Y, mouseState.MiddleButton == ButtonState.Pressed);
            InputButton(Nuklear.NK_BUTTON_RIGHT, mouseState.X, mouseState.Y, mouseState.RightButton == ButtonState.Pressed);

            InputMotion(mouseState.X, mouseState.Y);
            InputScroll(new Nuklear.nk_vec2 { x = 0, y = (mouseState.ScrollWheelValue - _previousWheel) / WHEEL_DELTA });
            InputEnd();

            _previousWheel = mouseState.ScrollWheelValue;
            _previousKeyboardState = keyboardState;
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