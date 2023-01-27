using System;
using System.Data.Common;
using System.Diagnostics;
using Lesson05_Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab1_Napat_Phuwarintarawanich
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private const int WindowWidth = 850;
        private const int WindowHeight = 611;
        private const int Speed = 5;

        private Texture2D bgTexture;
        private Texture2D fgTexture;

        CelAnimationSequence otterRunning;
        CelAnimationPlayer animationPlayer;

        private Texture2D otterTexture;
        private Vector2 otterDirection = new Vector2();
        private Vector2 otterSpeed = new Vector2(1, 1);
        //private Rectangle otterRectangle = new Rectangle();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.PreferredBackBufferWidth = WindowWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            otterDirection = new Vector2(300, 320);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
            //otterRectangle = otterTexture.Bounds;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgTexture = Content.Load<Texture2D>("zoo_background");
            fgTexture = Content.Load<Texture2D>("otter_sleeping");
            otterTexture = Content.Load<Texture2D>("otter_idle");

            Texture2D spriteSheet = Content.Load<Texture2D>("otter_running");
            otterRunning = new CelAnimationSequence(spriteSheet, 200, 1 / 8.0f);

            animationPlayer = new CelAnimationPlayer();
            animationPlayer.Play(otterRunning);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                //otterDirection.Y = 0;
                otterDirection.X += Speed;
                if (otterDirection.X >= _graphics.PreferredBackBufferWidth - otterTexture.Width)
                {
                    otterDirection.X = _graphics.PreferredBackBufferWidth - otterTexture.Width;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                otterDirection.X -= Speed;
                if (otterDirection.X <= 0)
                {
                    otterDirection.X = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                otterDirection.Y -= Speed;
                if (otterDirection.Y <= 0)
                {
                    otterDirection.Y = 0;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                otterDirection.Y += Speed;
                if (otterDirection.Y >= _graphics.PreferredBackBufferHeight - otterTexture.Height)
                {
                    otterDirection.Y = _graphics.PreferredBackBufferHeight - otterTexture.Height;
                }
            }

            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    otterDirection.Y = 0;
            //    otterDirection.X++;
            //    if (otterRectangle.Right >= _graphics.PreferredBackBufferWidth)
            //    {
            //        otterDirection.X = 0;
            //    }
            //    otterRectangle.Offset(otterDirection);
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    otterDirection.Y = 0;
            //    otterDirection.X--;
            //    if (otterRectangle.Left <= 0)
            //    {
            //        otterDirection.X = 0;
            //    }
            //    otterRectangle.Offset(otterDirection);
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Up))
            //{
            //    otterDirection.X = 0;
            //    otterDirection.Y--;
            //    if (otterRectangle.Top <= 0)
            //    {
            //        otterDirection.Y = 0;
            //    }
            //    otterRectangle.Offset(otterDirection);
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Down))
            //{
            //    otterDirection.X = 0;
            //    otterDirection.Y++;
            //    if (otterRectangle.Bottom >= _graphics.PreferredBackBufferHeight)
            //    {
            //        otterDirection.Y = 0;
            //    }
            //    otterRectangle.Offset(otterDirection);
            //}

            //--not using
            //if (Keyboard.GetState().IsKeyDown(Keys.Right))
            //{
            //    otterDirection.X += 5;
            //    //otterDirection.Y = 0;
            //    if (otterR
            //        . >= _graphics.PreferredBackBufferWidth)
            //    {
            //        otterDirection.X = WindowWidth;
            //    }
            //    Debug.WriteLine(otterDirection.X);
            //}

            //if (Keyboard.GetState().IsKeyDown(Keys.Left))
            //{
            //    otterDirection.X -= 5;
            //    //otterDirection.Y = 0;
            //    if (otterDirection.X <= 0)
            //    {
            //        otterDirection.X = 0;
            //    }
            //    Debug.WriteLine(otterDirection.X);
            //}
            animationPlayer.Update(gameTime, 2);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bgTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            animationPlayer.Draw(_spriteBatch, Vector2.Zero, SpriteEffects.None);
            _spriteBatch.Draw(fgTexture, new Rectangle(400, 260, 200, 200), Color.White);
            //_spriteBatch.Draw(otterTexture, otterRectangle.Location.ToVector2(), Color.White);
            //_spriteBatch.Draw(otterTexture, otterDirection, Color.White);
            _spriteBatch.Draw(otterTexture, otterDirection, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}