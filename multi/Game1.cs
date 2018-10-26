using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace multi
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Vector2 position;
		Texture2D tex;
		Texture2D pointer;

		Vector2 pointToClickSim;
		Vector2 v = Vector2.Zero;

		Vector2? p = null;
		Vector2 foraword = new Vector2(-1,0);
		int CurrAngle = 0;

		public Game1()
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
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);
			position = new Vector2(GraphicsDevice.Viewport.Width / 4, GraphicsDevice.Viewport.Height / 4);
			pointToClickSim = new Vector2(GraphicsDevice.Viewport.Width / 2,( GraphicsDevice.Viewport.Height / 2));

			tex = Content.Load<Texture2D>("character");
			pointer = Content.Load<Texture2D>("pointer");

			// TODO: use this.Content to load your game content here
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
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			MouseState mouse = Mouse.GetState();

			// TODO: Add your update logic here
			

	

			if (mouse.LeftButton == ButtonState.Pressed)
			{
				p = foraword;

				v = (position - pointToClickSim);
				v.Normalize();
				CurrAngle++;

				double angle = MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(v, (Vector2)p)));

				CurrAngle = (int)angle;
			}





			if (mouse.RightButton == ButtonState.Pressed)
			{

				v = (position - pointToClickSim);
				v.Normalize();
				CurrAngle--;

				double angle = MathHelper.ToDegrees((float)Math.Acos(Vector2.Dot(v, (Vector2)p)));

				/*	float dot = Vector2.Dot((Vector2)p, v);
					double det = p.Value.X * v.Y - p.Value.Y * v.X;
					double angle = Math.Atan2(p.Value.Y-v.Y, p.Value.X- v.X);*/

				if ((float)CurrAngle - (int)angle < angle)
				{
					position -= Rotate((Vector2)p, (float)CurrAngle - (int)angle);
				}
			//	Vector

				Window.Title = angle.ToString() +" "+ Rotate((Vector2)p, (float)angle);
			}

			//	Window.Title = MathHelper.ToDegrees(Vector2.Dot(new Vector2(1, 0), v)).ToString();

			base.Update(gameTime);
		}


		public Vector2 Rotate(Vector2 v, float degrees)
		{
			return RotateRadians(v,MathHelper.ToRadians(degrees));
		}

		public Vector2 RotateRadians(Vector2 v, float radians)
		{
			var ca =  (double)Math.Cos(radians);
			var sa = (double)Math.Sin(radians);
			return new Vector2((float)(ca * v.X - sa * v.Y), (float)(sa * v.X + ca * v.Y));
		}


		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);


			spriteBatch.Begin();
			spriteBatch.Draw(tex, position, Color.White);
			spriteBatch.Draw(pointer, pointToClickSim, Color.White);

			spriteBatch.End();
			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
