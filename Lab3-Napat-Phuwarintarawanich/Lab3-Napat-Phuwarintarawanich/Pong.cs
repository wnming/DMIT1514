using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Lab3_Napat_Phuwarintarawanich
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const int WindowWidth = 750;
        private const int WindowHeight = 500;

        private Texture2D bgTexture;
        private Texture2D pongBallTexture;
        private Texture2D paddleLeftTexture;
        private Texture2D paddleRightTexture;

        private Vector2 paddleLeftDirection;
        private Vector2 paddleRightDirection;
        private Vector2 pongBallDirection;

        private Rectangle paddleLeftRectangle = new Rectangle();
        private Rectangle paddleRightRectangle = new Rectangle();
        private Rectangle pongBallRectangle = new Rectangle();

        private float timeElapsed = 0.0f;
        private float timeToUpdate = 1 / 8.0f;
        private const int Speed = 60;

        public enum GameState
        {
            Initialize,
            WaitForPlayerMove,
            MakePlayerMove,
            EvaluatePoint,
            GameOver
        }
        GameState currentGameState = GameState.Initialize;

        public Pong()
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
            paddleRightRectangle = paddleRightTexture.Bounds;
            pongBallRectangle = pongBallTexture.Bounds;

            paddleLeftDirection = new Vector2(5, WindowHeight / 2 - paddleLeftRectangle.Height / 2);
            paddleRightDirection = new Vector2(WindowWidth - paddleRightRectangle.Width - 5, WindowHeight / 2 - paddleRightRectangle.Height / 2);
            //pongBallDirection = new Vector2(WindowWidth / 2, WindowHeight / 2);
            pongBallDirection = new Vector2(5f, 5f);

            paddleLeftRectangle = new Rectangle((int)paddleLeftDirection.X, (int)paddleLeftDirection.Y, paddleLeftTexture.Width, paddleLeftTexture.Height);
            paddleRightRectangle = new Rectangle((int)paddleRightDirection.X, (int)paddleRightDirection.Y, paddleRightTexture.Width, paddleRightTexture.Height);
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
            //timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (timeElapsed > timeToUpdate)
            //{
            //    timeElapsed -= timeToUpdate;
            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.WaitForPlayerMove:
                    break;
                case GameState.MakePlayerMove:
                    break;
                case GameState.EvaluatePoint:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }
            if (paddleLeftRectangle.Intersects(pongBallRectangle) || paddleRightRectangle.Intersects(pongBallRectangle))
            {
                //if (pongBallRectangle.Right > _graphics.PreferredBackBufferWidth || pongBallRectangle.Left < 0)
                //{
                    pongBallDirection.X *= -1;
                //}
            }

            //if (paddleLeftRectangle.Intersects(pongBallRectangle) || paddleRightRectangle.Intersects(pongBallRectangle))
            //{
                if (pongBallRectangle.Bottom > _graphics.PreferredBackBufferHeight || pongBallRectangle.Top < 0)
                {
                    pongBallDirection.Y *= -1;
                }
                //if (pongBallRectangle.Right > _graphics.PreferredBackBufferWidth || pongBallRectangle.Left < 0)
                //{
                //    pongBallDirection.X *= -1;
                //}
                pongBallRectangle.Offset(pongBallDirection);
            //}

            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                //paddle right -> up/down
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    paddleRightDirection.Y -= Speed;

                    if (paddleRightDirection.Y <= 0)
                    {
                        paddleRightDirection.Y = 0;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    paddleRightDirection.Y += Speed;

                    if (paddleRightDirection.Y >= WindowHeight - paddleRightRectangle.Height)
                    {
                        paddleRightDirection.Y = WindowHeight - paddleRightRectangle.Height;
                    }
                }
                paddleRightRectangle.X = (int)paddleRightDirection.X;
                paddleRightRectangle.Y = (int)paddleRightDirection.Y;

                //paddle left -> w/s
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    paddleLeftDirection.Y -= Speed;

                    if (paddleLeftDirection.Y <= 0)
                    {
                        paddleLeftDirection.Y = 0;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    paddleLeftDirection.Y += Speed;

                    if (paddleLeftDirection.Y >= WindowHeight - paddleLeftRectangle.Height)
                    {
                        paddleLeftDirection.Y = WindowHeight - paddleLeftRectangle.Height;
                    }
                }
                //paddleLeftRectangle.Offset(paddleLeftDirection);
                paddleLeftRectangle.X = (int)paddleLeftDirection.X;
                paddleLeftRectangle.Y = (int)paddleLeftDirection.Y;

            }
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
            _spriteBatch.Draw(pongBallTexture, pongBallRectangle.Location.ToVector2(), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}