using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Screensaver
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D backgroundTexture;

        private Texture2D snoopyTexture;
        private Vector2 snoopyDirection = new Vector2();
        private Rectangle snoopyRectangle = new Rectangle();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            snoopyDirection = new Vector2(5f, 5f);
            base.Initialize();
            snoopyRectangle = snoopyTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundTexture = Content.Load<Texture2D>("snoopy_bg1");
            snoopyTexture = Content.Load<Texture2D>("snoopy3");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (snoopyRectangle.Bottom > _graphics.PreferredBackBufferHeight || snoopyRectangle.Top < 0)
                {
                    snoopyDirection.Y *= -1;
                }
                if (snoopyRectangle.Right > _graphics.PreferredBackBufferWidth || snoopyRectangle.Left < 0)
                {
                    snoopyDirection.X *= -1;
                }
                snoopyRectangle.Offset(snoopyDirection);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, new Rectangle(0, 0, 800, 480), Color.White);
            _spriteBatch.Draw(snoopyTexture, snoopyRectangle.Location.ToVector2(),Color.White);
            //_spriteBatch.Draw(snoopyTexture, new Rectangle((int)snoopyRectangle.Location.ToVector2().X,(int)snoopyRectangle.Location.ToVector2().Y,0,0 ), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}