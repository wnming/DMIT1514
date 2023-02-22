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
        private const string Up = "up";
        private const string Down = "down";
        private const string Left = "left";
        private const string Right = "right";

        private Texture2D bgTexture;
        private Texture2D bgGameOverTexture;
        private Texture2D bgStartTexture;

        private Rectangle gameArea;

        private double elapsedTime = 0;
        private float changeSpeed = 20;

        private bool isFirstTime;

        Ball donutBall;
        Ball anotherBall;
        Paddle paddleLeft;
        Paddle paddleRight;
        HUD hud;

        private SpriteFont Titlefont;
        private SpriteFont font;

        private KeyboardState previousKeyState;
        private KeyboardState previousLeftKeyState;
        private KeyboardState previousRightKeyState;

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

            //Initialize in Update - GameState.Initialize
            donutBall = new Ball();
            anotherBall = new Ball();
            paddleLeft = new Paddle();
            paddleRight = new Paddle();

            hud = new HUD();
            hud.Initialize(new Vector2(0, GameHeight));

            isFirstTime = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgTexture = Content.Load<Texture2D>("background");
            bgGameOverTexture = Content.Load<Texture2D>("gameOver-background");
            bgStartTexture = Content.Load<Texture2D>("start-background");
            paddleLeft.LoadContent(Content);
            paddleRight.LoadContent(Content);
            donutBall.LoadContent(Content);
            anotherBall.LoadContent(Content);
            hud.LoadContent(Content);
            Titlefont = Content.Load<SpriteFont>("Games");
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
                    hud.Reset();
                    donutBall.Initialize(new Rectangle(0, 0, WindowWidth, GameHeight).Center.ToVector2(), gameArea, new Vector2(1, -1));
                    anotherBall.Initialize(new Rectangle(0, 0, WindowWidth, GameHeight - 70).Center.ToVector2(), gameArea, new Vector2(-1, 1));
                    paddleLeft.Initialize(new Vector2(20, GameHeight / 2 - paddleLeft.Height / 2), gameArea);
                    paddleRight.Initialize(new Vector2(WindowWidth - paddleRight.Width - 20, GameHeight / 2 - paddleRight.Height / 2), gameArea);
                    currentGameState = GameState.Start;
                    break;
                case GameState.Start:
                    if (isFirstTime)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            currentGameState = GameState.Serving;
                        }
                    }
                    else
                    {
                        currentGameState = GameState.Serving;
                    }
                    break;
                case GameState.Serving:
                    //delay serving
                    elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsedTime > 3000)
                    {
                        currentGameState = GameState.Playing;
                    }
                    break;
                case GameState.Playing:
                    paddleLeft.Direction = Vector2.Zero;
                    paddleRight.Direction = Vector2.Zero;

                    //prevent too fast toggle menu by using current and previous
                    KeyboardState currentKeyState = Keyboard.GetState();
                    KeyboardState currentLeftKeyState = Keyboard.GetState();
                    KeyboardState currentRightKeyState = Keyboard.GetState();

                    //speed up pong ball - n
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                    {
                        donutBall.Speed += changeSpeed;
                        anotherBall.Speed += changeSpeed;
                    }
                    //slow down pong ball - m
                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        donutBall.Speed -= changeSpeed;
                        anotherBall.Speed -= changeSpeed;
                    }

                    if ((donutBall.IsBallStickLeft == false && donutBall.IsCollide(paddleLeft.PaddingRectangle, paddleLeft.IsSticky, Left)) || (anotherBall.IsBallStickLeft == false && anotherBall.IsCollide(paddleLeft.PaddingRectangle, paddleLeft.IsSticky, Left)))
                    {
                        hud.LeftHit += 1;
                        hud.SetHighScore(hud.LeftHit, Left);
                        paddleLeft.IsHit = true;
                    }
                    if ((donutBall.IsBallStickRight == false && donutBall.IsCollide(paddleRight.PaddingRectangle, paddleRight.IsSticky, Right)) || (anotherBall.IsBallStickRight == false && anotherBall.IsCollide(paddleRight.PaddingRectangle, paddleRight.IsSticky, Right)))
                    {
                        hud.RightHit += 1;
                        hud.SetHighScore(hud.RightHit, Right);
                        paddleRight.IsHit = true;
                    }

                    //allow ball to stick to the paddles
                    if (currentKeyState.IsKeyDown(Keys.Space) && !previousKeyState.IsKeyDown(Keys.Space))
                    {
                        paddleLeft.IsSticky = !paddleLeft.IsSticky;
                        paddleRight.IsSticky = !paddleRight.IsSticky;
                        //release balls
                        if (donutBall.IsBallStickLeft || donutBall.IsBallStickRight && paddleLeft.IsSticky == false)
                        {
                            donutBall.ReleaseStickBall();
                        }
                        if (anotherBall.IsBallStickLeft || anotherBall.IsBallStickRight && paddleLeft.IsSticky == false)
                        {
                            anotherBall.ReleaseStickBall();
                        }
                    }

                    //paddle left -> w/s
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        paddleLeft.Direction = new Vector2(0, -1);
                        donutBall.CheckSticky(donutBall.IsBallStickLeft, paddleLeft.PaddingRectangle, Up);
                        anotherBall.CheckSticky(anotherBall.IsBallStickLeft, paddleLeft.PaddingRectangle, Up);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        paddleLeft.Direction = new Vector2(0, 1);
                        donutBall.CheckSticky(donutBall.IsBallStickLeft, paddleLeft.PaddingRectangle, Down);
                        anotherBall.CheckSticky(anotherBall.IsBallStickLeft, paddleLeft.PaddingRectangle, Down);
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.W) && !previousLeftKeyState.IsKeyDown(Keys.S) || Keyboard.GetState().IsKeyUp(Keys.S) && !previousLeftKeyState.IsKeyDown(Keys.W))
                    {
                        if (donutBall.IsBallStickLeft)
                        {
                            donutBall.MoveStickyBall(true, false);
                        }
                        if (anotherBall.IsBallStickLeft)
                        {
                            anotherBall.MoveStickyBall(true, false);
                        }
                    }

                    //paddle right -> up/down
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        paddleRight.Direction = new Vector2(0, -1);
                        donutBall.CheckSticky(donutBall.IsBallStickRight, paddleRight.PaddingRectangle, Up);
                        anotherBall.CheckSticky(anotherBall.IsBallStickRight, paddleRight.PaddingRectangle, Up);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        paddleRight.Direction = new Vector2(0, 1);
                        donutBall.CheckSticky(donutBall.IsBallStickRight, paddleRight.PaddingRectangle, Down);
                        anotherBall.CheckSticky(anotherBall.IsBallStickRight, paddleRight.PaddingRectangle, Down);
                    }
                    if (Keyboard.GetState().IsKeyUp(Keys.Down) && !previousRightKeyState.IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyUp(Keys.Up) && !previousRightKeyState.IsKeyDown(Keys.Down))
                    {
                        if (donutBall.IsBallStickRight)
                        {
                            donutBall.MoveStickyBall(false, false);
                        }
                        if (anotherBall.IsBallStickRight)
                        {
                            anotherBall.MoveStickyBall(false, false);
                        }
                    }

                    donutBall.Update(gameTime);
                    anotherBall.Update(gameTime);
                    paddleLeft.Update(gameTime);
                    paddleRight.Update(gameTime);

                    if (donutBall.IsOffBorder() || anotherBall.IsOffBorder())
                    {
                        currentGameState = GameState.GameOver;
                    }

                    previousKeyState = currentKeyState;
                    previousLeftKeyState = currentLeftKeyState;
                    previousRightKeyState = currentRightKeyState;

                    break;
                case GameState.BallSticks:
                    break;
                case GameState.GameOver:
                    isFirstTime = false;
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
                    if (isFirstTime)
                    {
                        _spriteBatch.Draw(bgStartTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                        Vector2 pongCenter = Titlefont.MeasureString("Pong") / 2f;
                        _spriteBatch.DrawString(Titlefont, "Pong", new Vector2(WindowWidth / 2, WindowHeight / 2 - 60), Color.LightPink, 0, pongCenter, 2.0f, SpriteEffects.None, 0);
                        Vector2 startCenter = font.MeasureString("Press enter to start the game") / 2f;
                        _spriteBatch.DrawString(font, "Press enter to start the game", new Vector2(WindowWidth / 2, WindowHeight / 2 + 60), Color.PaleVioletRed, 0, startCenter, 2.0f, SpriteEffects.None, 0);
                    }
                    break;
                case GameState.Serving:
                    paddleLeft.Draw(_spriteBatch);
                    paddleRight.Draw(_spriteBatch);
                    donutBall.Draw(_spriteBatch);
                    anotherBall.Draw(_spriteBatch);
                    hud.Draw(_spriteBatch);
                    Vector2 servingCenter = font.MeasureString("Ready...") / 2f;
                    _spriteBatch.DrawString(font, "Ready...", new Vector2(WindowWidth / 2, WindowHeight / 2 - 150), Color.LightSeaGreen, 0, servingCenter, 2.0f, SpriteEffects.None, 0);
                    break;
                case GameState.Playing:
                    paddleLeft.Draw(_spriteBatch);
                    paddleRight.Draw(_spriteBatch);
                    donutBall.Draw(_spriteBatch);
                    anotherBall.Draw(_spriteBatch);
                    hud.Draw(_spriteBatch);
                    break;
                case GameState.BallSticks:
                    break;
                case GameState.GameOver:
                    _spriteBatch.Draw(bgGameOverTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                    Vector2 gameOverCenter = Titlefont.MeasureString("Game Over") / 2f;
                    _spriteBatch.DrawString(Titlefont, "Game Over", new Vector2(WindowWidth / 2, WindowHeight / 2 - 100), Color.DeepSkyBlue, 0, gameOverCenter, 2.0f, SpriteEffects.None, 0);
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