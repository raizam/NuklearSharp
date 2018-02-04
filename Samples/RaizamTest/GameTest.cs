using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuklearSharp;
using NuklearSharp.MonoGame;
using Color = Microsoft.Xna.Framework.Color;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace RaizamTest
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameTest : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private ContextWrapper _contextWrapper;
		private Context _ctx;
		Color _background = Color.Black;
		bool _isTea = true;

		public GameTest()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
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

			_contextWrapper = new ContextWrapper(GraphicsDevice);
			_ctx = _contextWrapper.Ctx;

			Font font;
			using (var stream = File.OpenRead(Path.Combine(Content.RootDirectory, "Fonts/Roboto-Regular.ttf")))
			{
				var fontAtlas = new FontAtlasWrapper(_contextWrapper);
				font = fontAtlas.AddDefaultFont(22);
				fontAtlas.Bake();
			}

			_ctx.StyleSetFont(font.handle);

			IsMouseVisible = true;
			Window.AllowUserResizing = true;
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
			GraphicsDevice.Clear(_background);

			// TODO: Add your drawing code here
			if (_ctx.BeginTitled("demo2", "demo2", new Rectangle(50, 50, 200, 200),
				Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_SCALABLE |
				Nuklear.NK_WINDOW_MINIMIZABLE | Nuklear.NK_WINDOW_TITLE))
			{
				_ctx.LayoutRowStatic(30, 80, 1);
				_ctx.LayoutRowDynamic(30, 1);
				_ctx.ButtonText("Button");
				_ctx.LayoutRowDynamic(30, 2);
				if (_ctx.OptionLabel("Tea", _isTea))
					_isTea = true;

				if (_ctx.OptionLabel("Coffee", !_isTea))
					_isTea = false;

				_ctx.ButtonColor(Color.Red);
				_ctx.LayoutRowDynamic(30, 1);
				_ctx.LayoutRowDynamic(30, 2);
				_ctx.LabelColored("background", Nuklear.NK_TEXT_LEFT, _background);

				if (_ctx.ComboBeginColor(_background, new Vector2(_ctx.WidgetWidth(), 400)))
				{
					_ctx.LayoutRowDynamic(120, 1);
					_background = _ctx.ColorPicker(_background, 0);
					_ctx.LayoutRowDynamic(25, 1);
					_background.R = (byte) _ctx.Propertyi("#R", 0, _background.R, 255, 1, 1);
					_background.G = (byte) _ctx.Propertyi("#G", 0, _background.G, 255, 1, 1);
					_background.B = (byte) _ctx.Propertyi("#B", 0, _background.B, 255, 1, 1);
					_background.A = (byte) _ctx.Propertyi("#A", 0, _background.A, 255, 1, 1);
					_ctx.ComboEnd();
				}

				_ctx.LayoutRowDynamic(30, 1);
				_ctx.LabelColored("Sichem Allocated: " + Pointer.AllocatedTotal, Nuklear.NK_TEXT_LEFT, _background);

			}
			_ctx.End();

			_contextWrapper.Draw();

			/*_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			_spriteBatch.Draw(((ContextWrapper)_contextWrapper.Renderer).Textures[0], Vector2.Zero, Color.White);
			_spriteBatch.End();*/

			base.Draw(gameTime);
		}
	}
}