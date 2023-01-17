using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Diagnostics;

namespace Lesson1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D sushiSalmonTexture;
        private Vector2 sushiSalmonDirection = new Vector2();
        private Rectangle sushiSalmonRectangle = new Rectangle();

        private Texture2D sodaTexture;
        private Vector2 sodaDirection = new Vector2();
        private Rectangle sodaRectangle = new Rectangle();

        private Texture2D pizzaTexture;
        private Vector2 pizzaDirection = new Vector2();
        private Rectangle pizzaRectangle = new Rectangle();

        private Texture2D snoopyTexture;
        private Vector2 snoopyDirection = new Vector2();
        private Rectangle snoopyRectangle = new Rectangle();

        private SpriteFont gameFont;
        private SpriteFont game1Font;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sushiSalmonDirection = new Vector2(5f, 5f);
            sodaDirection = new Vector2(1f, 1f);
            pizzaDirection = new Vector2(9f, 9f);
            snoopyDirection = new Vector2(3f, 3f);
            base.Initialize();
            sushiSalmonRectangle = sushiSalmonTexture.Bounds;
            sodaRectangle = sodaTexture.Bounds;
            pizzaRectangle = pizzaTexture.Bounds;
            snoopyRectangle = snoopyTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sushiSalmonTexture = Content.Load<Texture2D>("sushiSalmon");
            sodaTexture = Content.Load<Texture2D>("sodaGlass");
            pizzaTexture = Content.Load<Texture2D>("pizza");
            snoopyTexture = Content.Load<Texture2D>("snoopy");
            // TODO: use this.Content to load your game content here

            gameFont = Content.Load<SpriteFont>("Raleway");
            game1Font = Content.Load<SpriteFont>("AbrilFatface-Regular");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //if (sushiSalmonRectangle.Bottom > _graphics.PreferredBackBufferHeight || sushiSalmonRectangle.Top < 0)
            //{
            //    sushiSalmonDirection.Y *= -1;
            //}
            //if (sushiSalmonRectangle.Right > _graphics.PreferredBackBufferWidth || sushiSalmonRectangle.Left < 0)
            //{
            //    sushiSalmonDirection.X *= -1;
            //}

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (sushiSalmonRectangle.Bottom > _graphics.PreferredBackBufferHeight || sushiSalmonRectangle.Top < 0)
                {
                    sushiSalmonDirection.Y *= -1;
                }
                if (sushiSalmonRectangle.Right > _graphics.PreferredBackBufferWidth || sushiSalmonRectangle.Left < 0)
                {
                    sushiSalmonDirection.X *= -1;
                }
                sushiSalmonRectangle.Offset(sushiSalmonDirection);
            }
            if (sodaRectangle.Bottom > _graphics.PreferredBackBufferHeight || sodaRectangle.Top < 0)
            {
                sodaDirection.Y *= -1;
            }
            if (sodaRectangle.Right > _graphics.PreferredBackBufferWidth || sodaRectangle.Left < 0)
            {
                sodaDirection.X *= -1;
            }

            if (pizzaRectangle.Bottom > _graphics.PreferredBackBufferHeight || pizzaRectangle.Top < 0)
            {
                pizzaDirection.Y *= -1;
            }
            if (pizzaRectangle.Right > _graphics.PreferredBackBufferWidth || pizzaRectangle.Left < 0)
            {
                pizzaDirection.X *= -1;
            }

            pizzaRectangle.Offset(pizzaDirection);

            if (snoopyRectangle.Bottom > _graphics.PreferredBackBufferHeight || snoopyRectangle.Top < 0)
            {
                snoopyDirection.Y *= -1;
            }
            if (snoopyRectangle.Right > _graphics.PreferredBackBufferWidth || snoopyRectangle.Left < 0)
            {
                snoopyDirection.X *= -1;
            }

            snoopyRectangle.Offset(snoopyDirection);

            //if (sushiSalmonRectangle.Intersects(sodaRectangle))
            //{
            //    sodaDirection.X *= 10;
            //    sodaDirection.Y *= 20;
            //}

            sodaRectangle.Offset(sodaDirection);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Wheat);
            Vector2 textCenter = gameFont.MeasureString("") / 2f;
            Vector2 textCenter1 = gameFont.MeasureString("") / 2f;

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(sushiSalmonTexture, sushiSalmonRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.Draw(sodaTexture, sodaRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.Draw(pizzaTexture, pizzaRectangle.Location.ToVector2(), Color.White);
            //_spriteBatch.Draw(snoopyTexture, snoopyRectangle.Location.ToVector2(), Color.White);
            //_spriteBatch.Draw(sodaTexture, sodaDirection, Color.White);
            _spriteBatch.DrawString(gameFont, "I need more food!! Give me FOODDDDD!!", new Vector2(140, 200), Color.DeepPink, 0, textCenter, 2.0f, SpriteEffects.None, 0);
            _spriteBatch.DrawString(game1Font, "New font has been added!! :D", new Vector2(150, 300), Color.DarkBlue, 0, textCenter1, 2.0f, SpriteEffects.None, 0);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}