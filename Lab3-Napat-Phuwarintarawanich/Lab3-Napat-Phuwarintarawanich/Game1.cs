using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab3_Napat_Phuwarintarawanich
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const int WindowWidth = 650;
        private const int WindowHeight = 400;

        private Texture2D bgTexture;
        private Texture2D pongBallTexture;
        private Texture2D paddleLeftTexture;
        private Texture2D paddleRightTexture;

        private Vector2 paddleLeftDirection;
        private Vector2 paddleRightDirection;

        private Rectangle paddleLeftRectangle = new Rectangle();
        private Rectangle paddleRightRectangle = new Rectangle();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.PreferredBackBufferWidth = WindowWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            paddleLeftRectangle = paddleLeftTexture.Bounds;
            paddleLeftDirection = new Vector2(5, WindowHeight / 2 - paddleLeftRectangle.Height / 2);

            paddleRightRectangle = paddleRightTexture.Bounds;
            paddleRightDirection = new Vector2(WindowWidth - paddleRightRectangle.Width - 5, WindowHeight / 2 - paddleRightRectangle.Height / 2);

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgTexture = Content.Load<Texture2D>("background");
            pongBallTexture = Content.Load<Texture2D>("pong-ball");
            paddleLeftTexture = Content.Load<Texture2D>("paddle");
            paddleRightTexture = Content.Load<Texture2D>("paddle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            paddleLeftRectangle.Offset(paddleLeftDirection);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bgTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            _spriteBatch.Draw(paddleLeftTexture, paddleLeftDirection, Color.White);
            _spriteBatch.Draw(paddleRightTexture, paddleRightDirection, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}