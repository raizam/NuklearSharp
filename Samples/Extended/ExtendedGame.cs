using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KlearUI;
using KlearUI.MonoGame;

namespace Extended
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ExtendedGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private NuklearContext _contextWrapper;
        private readonly Media _media = new Media();

        public ExtendedGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 768;
        }

        private string GetAssetPath(string path)
        {
            return Path.Combine(Content.RootDirectory, path);
        }

        private static byte ApplyAlpha(byte color, byte alpha)
        {
            var fc = color / 255.0f;
            var fa = alpha / 255.0f;

            var fr = (int)(255.0f * fc * fa);

            if (fr < 0)
            {
                fr = 0;
            }

            if (fr > 255)
            {
                fr = 255;
            }

            return (byte)fr;
        }

        public static void PremultiplyAlpha(byte[] data)
        {
            for (var i = 0; i < data.Length / 4; ++i)
            {
                var a = data[i * 4 + 3];
                data[i * 4] = ApplyAlpha(data[i * 4], a);
                data[i * 4 + 1] = ApplyAlpha(data[i * 4 + 1], a);
                data[i * 4 + 2] = ApplyAlpha(data[i * 4 + 2], a);
            }
        }

        private NkImage LoadImage(string path)
        {
            using (var stream = File.OpenRead(GetAssetPath(path)))
            {
                var texture = Texture2D.FromStream(GraphicsDevice, stream);
                var result = Nk.nk_image_id(_contextWrapper.CreateTexture(texture));

                result.w = (ushort)texture.Width;
                result.h = (ushort)texture.Height;

                return result;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Window.Title = "Demo";

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _contextWrapper = new NuklearContext(GraphicsDevice);

            // Fonts
            var fontData = File.ReadAllBytes(GetAssetPath("Fonts/Roboto-Regular.ttf"));
            var fontAtlas = new FontAtlasWrapper(_contextWrapper);

            using (var stream = new MemoryStream(fontData))
            {
                _media.Font14 = fontAtlas.AddFont(stream, 14);
            }

            using (var stream = new MemoryStream(fontData))
            {
                _media.Font18 = fontAtlas.AddFont(stream, 18);
            }

            using (var stream = new MemoryStream(fontData))
            {
                _media.Font20 = fontAtlas.AddFont(stream, 20);
            }

            using (var stream = new MemoryStream(fontData))
            {
                _media.Font22 = fontAtlas.AddFont(stream, 22);
            }

            _contextWrapper.ConvertConfig.Null = fontAtlas.Bake();

            _media.Uncheckd = LoadImage("Icons/unchecked.png");
            _media.Checkd = LoadImage("Icons/checked.png");
            _media.Rocket = LoadImage("Icons/rocket.png");
            _media.Cloud = LoadImage("Icons/cloud.png");
            _media.Pen = LoadImage("Icons/pen.png");
            _media.Play = LoadImage("Icons/play.png");
            _media.Pause = LoadImage("Icons/pause.png");
            _media.Stop = LoadImage("Icons/stop.png");
            _media.Next = LoadImage("Icons/next.png");
            _media.Prev = LoadImage("Icons/prev.png");
            _media.Tools = LoadImage("Icons/tools.png");
            _media.Dir = LoadImage("Icons/directory.png");
            _media.Copy = LoadImage("Icons/copy.png");
            _media.Convert = LoadImage("Icons/export.png");
            _media.Del = LoadImage("Icons/delete.png");
            _media.Edit = LoadImage("Icons/edit.png");
            _media.Menu[0] = LoadImage("Icons/home.png");
            _media.Menu[1] = LoadImage("Icons/phone.png");
            _media.Menu[2] = LoadImage("Icons/plane.png");
            _media.Menu[3] = LoadImage("Icons/wifi.png");
            _media.Menu[4] = LoadImage("Icons/settings.png");
            _media.Menu[5] = LoadImage("Icons/volume.png");

            for (var i = 0; i < _media.Images.Length; ++i)
            {
                _media.Images[i] = LoadImage("Images/image" + (i + 1) + ".png");
            }

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0.3f, 0.3f, 0.3f));

            // TODO: Add your drawing code here
            Gui.basic_demo(_contextWrapper, _media);
            Gui.button_demo(_contextWrapper, _media);
            Gui.grid_demo(_contextWrapper, _media);

            _contextWrapper.Draw();

            /*_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			var texture = _contextWrapper.Textures[0];
			_spriteBatch.Draw(texture, Vector2.Zero, Color.White);
			_spriteBatch.End();*/


            base.Draw(gameTime);
        }
    }
}