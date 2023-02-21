using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Lab3_Napat_Phuwarintarawanich
{
    public class Pong : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const int WindowWidth = 800;
        private const int WindowHeight = 650;

        private const int GameHeight = 500;

        private Texture2D bgTexture;
        private Texture2D bgGameOverTexture;

        private Rectangle gameArea;

        private double elapsedTime = 0;
        private float changeSpeed = 20;

        Ball donutBall;
        Paddle paddleLeft;
        Paddle paddleRight;
        HUD hud;

        private SpriteFont gamOverfont;
        private SpriteFont font;

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
            gameArea = new Rectangle(0, 0, WindowWidth, GameHeight);

            donutBall = new Ball();
            donutBall.Initialize(new Rectangle(0, 0, WindowWidth, GameHeight).Center.ToVector2(), gameArea, new Vector2(1, -1));

            paddleLeft = new Paddle();
            paddleLeft.Initialize(new Vector2(20, GameHeight / 2 - paddleLeft.Height / 2), gameArea);

            paddleRight = new Paddle();
            paddleRight.Initialize(new Vector2(WindowWidth - paddleRight.Width - 20, GameHeight / 2 - paddleRight.Height / 2), gameArea);

            hud = new HUD();
            hud.Initialize(new Vector2(0, GameHeight));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgTexture = Content.Load<Texture2D>("background");
            bgGameOverTexture = Content.Load<Texture2D>("gameOver-background");
            paddleLeft.LoadContent(Content);
            paddleRight.LoadContent(Content);
            donutBall.LoadContent(Content);
            hud.LoadContent(Content);
            gamOverfont = Content.Load<SpriteFont>("Games");
            font = Content.Load<SpriteFont>("aBlackLives");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (currentGameState)
            {
                case GameState.Initialize:
                    elapsedTime = 0;
                    hud.LeftHit = 0;
                    hud.RightHit = 0;
                    donutBall.Initialize(new Rectangle(0, 0, WindowWidth, GameHeight).Center.ToVector2(), gameArea, new Vector2(1, -1));
                    paddleLeft.Initialize(new Vector2(20, GameHeight / 2 - paddleLeft.Height / 2), gameArea);
                    paddleRight.Initialize(new Vector2(WindowWidth - paddleRight.Width - 20, GameHeight / 2 - paddleRight.Height / 2), gameArea);
                    currentGameState = GameState.Start;
                    break;
                case GameState.Start:
                    currentGameState = GameState.Serving;
                    break;
                case GameState.Serving:
                    //delay serving
                    elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsedTime > 5000)
                    {
                        currentGameState = GameState.Playing;
                    }
                    break;
                case GameState.Playing:
                    paddleLeft.Direction = Vector2.Zero;
                    paddleRight.Direction = Vector2.Zero;

                    //paddle left -> w/s
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        paddleLeft.Direction = new Vector2(0, -1);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        paddleLeft.Direction = new Vector2(0, 1);
                    }

                    //paddle right -> up/down
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        paddleRight.Direction = new Vector2(0, -1);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        paddleRight.Direction = new Vector2(0, 1);
                    }

                    //speed up pong ball - n
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                    {
                        donutBall.Speed += changeSpeed;
                    }
                    //slow down ball - m
                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        donutBall.Speed -= changeSpeed;
                    }

                    if (donutBall.IsCollide(paddleLeft.PaddingRectangle))
                    {
                        hud.LeftHit += 1;
                        hud.SetHighScore(hud.LeftHit, "left");
                        paddleLeft.isHit = true;
                    }
                    if (donutBall.IsCollide(paddleRight.PaddingRectangle))
                    {
                        hud.RightHit += 1;
                        hud.SetHighScore(hud.RightHit, "right");
                        paddleRight.isHit = true;
                    }

                    donutBall.Update(gameTime);
                    paddleLeft.Update(gameTime);
                    paddleRight.Update(gameTime);

                    if (donutBall.IsOffBorder())
                    {
                        currentGameState = GameState.GameOver;
                    }
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        currentGameState = GameState.Initialize;
                    }
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(bgTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.Start:
                    paddleLeft.Draw(_spriteBatch);
                    paddleRight.Draw(_spriteBatch);
                    hud.Draw(_spriteBatch);
                    break;
                case GameState.Serving:
                    paddleLeft.Draw(_spriteBatch);
                    paddleRight.Draw(_spriteBatch);
                    donutBall.Draw(_spriteBatch);
                    hud.Draw(_spriteBatch);
                    break;
                case GameState.Playing:
                    paddleLeft.Draw(_spriteBatch);
                    paddleRight.Draw(_spriteBatch);
                    donutBall.Draw(_spriteBatch);
                    hud.Draw(_spriteBatch);
                    break;
                case GameState.GameOver:
                    _spriteBatch.Draw(bgGameOverTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                    Vector2 gameOverCenter = gamOverfont.MeasureString("Game Over") / 2f;
                    _spriteBatch.DrawString(gamOverfont, "Game Over", new Vector2(WindowWidth / 2, WindowHeight / 2 - 100), Color.DeepSkyBlue, 0, gameOverCenter, 2.0f, SpriteEffects.None, 0);
                    Vector2 highScoreCenter = font.MeasureString("High Score: " + hud.HighestScore + " (" + hud.HighestSide + ")") / 2f;
                    _spriteBatch.DrawString(font, "High Score: " + hud.HighestScore + " (" + hud.HighestSide + ")", new Vector2(WindowWidth / 2, WindowHeight / 2 + 20), Color.ForestGreen, 0, highScoreCenter, 2.0f, SpriteEffects.None, 0);
                    Vector2 playAgainCenter = font.MeasureString("Press Enter to play again.") / 2f;
                    _spriteBatch.DrawString(font, "Press Enter to play again.", new Vector2(WindowWidth / 2, WindowHeight - 100), Color.DeepPink, 0, playAgainCenter, 2.0f, SpriteEffects.None, 0);
                    break;
                default:
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}