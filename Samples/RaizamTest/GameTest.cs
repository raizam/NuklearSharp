using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuklearSharp;
using NuklearSharp.MonoGame;

namespace RaizamTest
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameTest : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private NuklearContext _nkContext;
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

			_nkContext = new NuklearContext(GraphicsDevice);

			NkFont font;
			using (var stream = File.OpenRead(Path.Combine(Content.RootDirectory, "Fonts/Roboto-Regular.ttf")))
			{
				font = _nkContext.LoadFont(stream, 22);
			}

			_nkContext.SetFont(font);

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
			if (_nkContext.WindowBegin("demo2", new Rectangle(50, 50, 200, 200),
				Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_SCALABLE |
				Nuklear.NK_WINDOW_MINIMIZABLE | Nuklear.NK_WINDOW_TITLE))
			{

				_nkContext.RowStatic(30, 80, 1);
				_nkContext.RowDynamic(30, 1);
				if (_nkContext.Button("Button"))
				{
				}
				_nkContext.RowDynamic(30, 2);
				if (_nkContext.Option("Tea", _isTea))
					_isTea = true;

				if (_nkContext.Option("Coffee", !_isTea))
					_isTea = false;

				_nkContext.Button(Color.Red);
				_nkContext.RowDynamic(30, 1);
				//_nkContext.Property("#DepthBias", 0, ref DepthBias, 16, 0.1F);
				//_nkContext.Property("#SlopeScaleDepthBias", 0, ref SlopeScaleDepthBias, 16, 0.1F);
				_nkContext.RowDynamic(30, 2);
				_nkContext.Label("background", Nuklear.NK_TEXT_LEFT, _background);

				if (_nkContext.ComboBeginColor(_background, new Vector2(_nkContext.WidgetWidth, 400)))
				{
					_nkContext.RowDynamic(120, 1);
					_background = _nkContext.ColorPicker(_background);
					_nkContext.RowDynamic(25, 1);
					_background.R = (byte) _nkContext.Property("#R", 0, _background.R, 255, 1, 1);
					_background.G = (byte) _nkContext.Property("#G", 0, _background.G, 255, 1, 1);
					_background.B = (byte) _nkContext.Property("#B", 0, _background.B, 255, 1, 1);
					_background.A = (byte) _nkContext.Property("#A", 0, _background.A, 255, 1, 1);
					_nkContext.ComboEnd();
				}

				_nkContext.RowDynamic(30, 1);
			}
			_nkContext.WindowEnd();

			_nkContext.Draw(gameTime);

/*			_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			_spriteBatch.Draw(_nkContext.Textures[0], Vector2.Zero, Color.White);
			_spriteBatch.End();*/

			base.Draw(gameTime);
		}
	}
}