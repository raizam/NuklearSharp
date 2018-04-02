using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NuklearSharp.MonoGame
{
    public class NuklearContext : BaseContext
    {
        private const int WheelDelta = 120;

        private readonly GraphicsDevice _device;
        private DynamicVertexBuffer _vertexBuffer;
        private DynamicIndexBuffer _indexBuffer;
        private readonly BasicEffect _basicEffect;
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
            _basicEffect = new BasicEffect(device);

            ConvertConfig.VertexSize = (uint)VertexPositionColorTexture.VertexDeclaration.VertexStride;

            ConvertConfig.VertexLayout = new[]
            {
                new nk_draw_vertex_layout_element
                {
                    attribute = Nk.NK_VERTEX_POSITION,
                    format = Nk.NK_FORMAT_FLOAT,
                    offset = 0
                },
                new nk_draw_vertex_layout_element
                {
                    attribute = Nk.NK_VERTEX_COLOR,
                    format = Nk.NK_FORMAT_B8G8R8A8,
                    offset = 12
                },
                new nk_draw_vertex_layout_element
                {
                    attribute = Nk.NK_VERTEX_TEXCOORD,
                    format = Nk.NK_FORMAT_FLOAT,
                    offset = 16
                },
                new nk_draw_vertex_layout_element
                {
                    attribute = Nk.NK_VERTEX_ATTRIBUTE_COUNT
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
            const float l = 0.5f;
            var r = width + 0.5f;
            const float T = 0.5f;
            var b = height + 0.5f;
            mtx = new Matrix(2.0f / (r - l), 0.0f, 0.0f, 0.0f, 0.0f, 2.0f / (T - b), 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                (r + l) / (l - r), (T + b) / (b - T), 0.0f, 1.0f);
        }

        protected override void BeginDraw()
        {
            UpdateInput();

            _basicEffect.World = Matrix.Identity;
            Matrix projection;
            GetProjectionMatrix(_device.PresentationParameters.Bounds.Width, _device.PresentationParameters.Bounds.Height,
                out projection);
            _basicEffect.Projection = projection;
            _basicEffect.VertexColorEnabled = true;
            _basicEffect.TextureEnabled = true;
            _basicEffect.LightingEnabled = false;
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

        protected override unsafe void SetBuffers(byte[] vertices, ushort[] indices, int indicesCount, int vertexCount)
        {
            if (vertexCount == 0) return;

            var result = new VertexPositionColorTexture[vertexCount];

            fixed (VertexPositionColorTexture* vx = &result[0])
            {
                var b = (byte*)vx;
                for (int i = 0; i < vertexCount * sizeof(VertexPositionColorTexture); i++)
                {
                    *(b + i) = vertices[i];
                }
            }

            for (var i = 0; i < vertexCount; i++)
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


            if (_indexBuffer.IndexCount < indicesCount)
            {
                // Resize index buffer if data doesnt fit
                _indexBuffer = new DynamicIndexBuffer(_device, typeof(ushort), indicesCount * 2, BufferUsage.WriteOnly);
            }

            _indexBuffer.SetData(indices, 0, indicesCount);
        }

        protected override void Draw(int x, int y, int w, int h, int textureId, int startIndex, int primitiveCount)
        {
            _device.ScissorRectangle = new Rectangle(x, y, w, h);
            if (textureId != 0)
            {
                _basicEffect.TextureEnabled = true;
                _basicEffect.Texture = _textures[textureId - 1];
            }
            else _basicEffect.TextureEnabled = false;

            foreach (var pass in _basicEffect.CurrentTechnique.Passes)
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

            InputKey(Nk.NK_KEY_DEL, keyboardState.IsKeyDown(Keys.Delete));
            InputKey(Nk.NK_KEY_ENTER, keyboardState.IsKeyDown(Keys.Enter));
            InputKey(Nk.NK_KEY_TAB, keyboardState.IsKeyDown(Keys.Tab));
            InputKey(Nk.NK_KEY_BACKSPACE, keyboardState.IsKeyDown(Keys.Back));
            InputKey(Nk.NK_KEY_LEFT, keyboardState.IsKeyDown(Keys.Left));
            InputKey(Nk.NK_KEY_RIGHT, keyboardState.IsKeyDown(Keys.Right));
            InputKey(Nk.NK_KEY_UP, keyboardState.IsKeyDown(Keys.Up));
            InputKey(Nk.NK_KEY_DOWN, keyboardState.IsKeyDown(Keys.Down));
            if (keyboardState.IsKeyDown(Keys.LeftControl) ||
                keyboardState.IsKeyDown(Keys.RightControl))
            {
                InputKey(Nk.NK_KEY_COPY, keyboardState.IsKeyDown(Keys.C));
                InputKey(Nk.NK_KEY_PASTE, keyboardState.IsKeyDown(Keys.P));
                InputKey(Nk.NK_KEY_CUT, keyboardState.IsKeyDown(Keys.X));
                InputKey(Nk.NK_KEY_CUT, keyboardState.IsKeyDown(Keys.E));
                InputKey(Nk.NK_KEY_SHIFT, true);
            }
            else
            {
                InputKey(Nk.NK_KEY_COPY, false);
                InputKey(Nk.NK_KEY_PASTE, false);
                InputKey(Nk.NK_KEY_CUT, false);
                InputKey(Nk.NK_KEY_SHIFT, false);
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

            InputButton(Nk.NK_BUTTON_LEFT, mouseState.X, mouseState.Y, mouseState.LeftButton == ButtonState.Pressed);
            InputButton(Nk.NK_BUTTON_MIDDLE, mouseState.X, mouseState.Y, mouseState.MiddleButton == ButtonState.Pressed);
            InputButton(Nk.NK_BUTTON_RIGHT, mouseState.X, mouseState.Y, mouseState.RightButton == ButtonState.Pressed);

            InputMotion(mouseState.X, mouseState.Y);
            InputScroll(new NkVec2 { x = 0, y = (mouseState.ScrollWheelValue - _previousWheel) / WheelDelta });
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