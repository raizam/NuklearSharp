using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuklearSharp;
using NuklearSharp.MonoGame;
using System.Collections.Generic;

namespace Extended
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class ExtendedGame: Game
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

			Content.RootDirectory = "Content";
		}

		private string GetAssetPath(string path)
		{
			return Path.Combine(Content.RootDirectory, path);
		}

		private Nuklear.nk_font CreateFont(byte[] data, float height)
		{
			using (var stream = new MemoryStream(data))
			{
				var fontAtlas = _contextWrapper.CreateFontAtlas();
				var font = fontAtlas.AddFont(stream, 14);
				fontAtlas.Bake();

				return font;
			}			
		}

		private Nuklear.nk_image LoadImage(string path)
		{
			using(var stream = File.OpenRead(GetAssetPath(path)))
			{
				var texture = Texture2D.FromStream (GraphicsDevice, stream);
				return Nuklear.nk_image_id(_contextWrapper.CreateTexture (texture));
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

			base.Initialize();

			_spriteBatch = new SpriteBatch(GraphicsDevice);

			_contextWrapper = new NuklearContext(GraphicsDevice);

			// Fonts
			var fontData = File.ReadAllBytes(GetAssetPath("Fonts/Roboto-Regular.ttf"));

			_media.font_14 = CreateFont (fontData, 14);
			_media.font_18 = CreateFont (fontData, 18);
			_media.font_20 = CreateFont (fontData, 20);
			_media.font_22 = CreateFont (fontData, 22);

			_media.uncheckd = LoadImage("Icons/unchecked.png");
			_media.checkd = LoadImage("Icons/checked.png");
			_media.rocket = LoadImage("Icons/rocket.png");
			_media.cloud = LoadImage("Icons/cloud.png");
			_media.pen = LoadImage("Icons/pen.png");
			_media.play = LoadImage("Icons/play.png");
			_media.pause = LoadImage("Icons/pause.png");
			_media.stop = LoadImage("Icons/stop.png");
			_media.next =  LoadImage("Icons/next.png");
			_media.prev =  LoadImage("Icons/prev.png");
			_media.tools = LoadImage("Icons/tools.png");
			_media.dir = LoadImage("Icons/directory.png");
			_media.copy = LoadImage("Icons/copy.png");
			_media.convert = LoadImage("Icons/export.png");
			_media.del = LoadImage("Icons/delete.png");
			_media.edit = LoadImage("Icons/edit.png");
			_media.menu[0] = LoadImage("Icons/home.png");
			_media.menu[1] = LoadImage("Icons/phone.png");
			_media.menu[2] = LoadImage("Icons/plane.png");
			_media.menu[3] = LoadImage("Icons/wifi.png");
			_media.menu[4] = LoadImage("Icons/settings.png");
			_media.menu[5] = LoadImage("Icons/volume.png");

			for (var i = 0; i < _media.images.Length; ++i) {
				_media.images [i] = LoadImage ("Images/image" + (i + 1) + ".png");
			}
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
			GUI.basic_demo(_contextWrapper, _media);
//			GUI.button_demo (_contextWrapper, _media);
//			GUI.grid_demo (_contextWrapper, _media);

			_contextWrapper.Draw ();

			base.Draw(gameTime);
		}
	}
}