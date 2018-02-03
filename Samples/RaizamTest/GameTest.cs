using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NuklearSharp;

namespace RaizamTest
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class GameTest : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private NuklearContext nkContext;

		public GameTest()
		{
			graphics = new GraphicsDeviceManager(this);
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

			spriteBatch = new SpriteBatch(GraphicsDevice);

			nkContext = new NuklearContext(GraphicsDevice);
			nkContext.Initialize();
			var fonts = nkContext.LoadFonts(NkFont.Define(Path.Combine(Content.RootDirectory, "Fonts/Roboto-Regular.ttf"), 22));
			nkContext.SetFont(fonts[0]);

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
			spriteBatch = new SpriteBatch(GraphicsDevice);

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
		Color background = Color.Black;

		bool isTea = true;

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(background);

			// TODO: Add your drawing code here

			if (nkContext._ctx.begin != null)
			{
				nkContext._ctx.begin.buffer.commands.Clear();
			}

			if (nkContext.WindowBegin("demo2", new Rectangle(50, 50, 200, 200),
				Nuklear.NK_WINDOW_BORDER | Nuklear.NK_WINDOW_MOVABLE | Nuklear.NK_WINDOW_SCALABLE |
				Nuklear.NK_WINDOW_MINIMIZABLE | Nuklear.NK_WINDOW_TITLE))
			{

				nkContext.RowStatic(30, 80, 1);
				nkContext.RowDynamic(30, 1);
				if (nkContext.Button("Button"))
				{
				}
				nkContext.RowDynamic(30, 2);
				if (nkContext.Option("Tea", isTea))
					isTea = true;

				if (nkContext.Option("Coffee", !isTea))
					isTea = false;

				nkContext.Button(Color.Red);
				nkContext.RowDynamic(30, 1);
				//nkContext.Property("#DepthBias", 0, ref DepthBias, 16, 0.1F);
				//nkContext.Property("#SlopeScaleDepthBias", 0, ref SlopeScaleDepthBias, 16, 0.1F);
				nkContext.RowDynamic(30, 2);
				nkContext.Label("background", Nuklear.NK_TEXT_LEFT, background);

				if (nkContext.ComboBeginColor(background, new Vector2(nkContext.WidgetWidth, 400)))
				{
					nkContext.RowDynamic(120, 1);
					background = nkContext.ColorPicker(background);
					nkContext.RowDynamic(25, 1);
					background.R = (byte) nkContext.Property("#R", 0, background.R, 255, 1, 1);
					background.G = (byte) nkContext.Property("#G", 0, background.G, 255, 1, 1);
					background.B = (byte) nkContext.Property("#B", 0, background.B, 255, 1, 1);
					background.A = (byte) nkContext.Property("#A", 0, background.A, 255, 1, 1);
					nkContext.ComboEnd();
				}


				nkContext.RowDynamic(30, 1);
				//  ulong cur = (uint)DateTime.Now.Second % 7;
				//nk_style_get_color_by_name(NkStyleColors.NK_COLOR_BUTTON_ACTIVE);

				//var txt = "Test";
				//nk_text(_ctx, txt, txt.Length, 3);

				//nk_progress(_ctx, &cur, 7, 1);

			}
			nkContext.WindowEnd();

 			nkContext.Draw(gameTime);

/*			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
			spriteBatch.Draw(nkContext.textures[0], Vector2.Zero, Color.White);
			spriteBatch.End();*/

			base.Draw(gameTime);
		}
	}
}