using System;
using System.Collections.Generic;
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
        private const int Speed = 20;
        private const int MaxRigth = 550;
        private const int MaxLeft = 290;
        private const int MaxUp = 200;
        private const int MaxDown = 300;
        private const int SpriteColumn = 4;

        //background image
        private Texture2D bgTexture;
        //a foreground image
        private Texture2D otterFgTexture;

        //a running otter - 1st animation
        CelAnimationSequence otterRunning;
        CelAnimationPlayer otterPlayer;
        private Vector2 otterRunningDirection = new Vector2();

        //a walking man - 2nd animation
        private Texture2D walkingManTexture;
        private Vector2 walkingManDirection = new Vector2();
        Dictionary<string, Rectangle[]> walkingManRectangle = new Dictionary<string, Rectangle[]>();
        private string currentAnimation = "right";
        private int frame = 1;
        private float timeElapsed = 0.0f;
        private float timeToUpdate = 1 / 8.0f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.PreferredBackBufferWidth = WindowWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            otterRunningDirection = new Vector2(0, 180);
            walkingManDirection = new Vector2(290, 280);
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
            otterFgTexture = Content.Load<Texture2D>("otter_sleeping");
            walkingManTexture = Content.Load<Texture2D>("walking_man");

            Texture2D spriteSheet = Content.Load<Texture2D>("otter_running");
            otterRunning = new CelAnimationSequence(spriteSheet, 200, 200, 1 / 8.0f, 5, 2);

            //play a multi-row sprite sheet animation
            otterPlayer = new CelAnimationPlayer();
            otterPlayer.Play(otterRunning);

            //specific each frame's rectangle for a walking man (key input)
            walkingManRectangle["right"] = new Rectangle[4] { new Rectangle(0, 458, 102, 153), new Rectangle(102, 458, 102, 153), new Rectangle(204, 458, 102, 153), new Rectangle(306, 458, 102, 153) };
            walkingManRectangle["left"] = new Rectangle[4] { new Rectangle(0, 305, 102, 153), new Rectangle(102, 305, 102, 153), new Rectangle(204, 305, 102, 153), new Rectangle(306, 305, 102, 153) };
            walkingManRectangle["up"] = new Rectangle[4] { new Rectangle(0, 153, 102, 153), new Rectangle(102, 153, 102, 153), new Rectangle(204, 153, 102, 153), new Rectangle(306, 153, 102, 153) };
            walkingManRectangle["down"] = new Rectangle[4] { new Rectangle(0, 0, 102, 153), new Rectangle(102, 0, 102, 153), new Rectangle(204, 0, 102, 153), new Rectangle(306, 0, 102, 153) };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    walkingManDirection.X += Speed;
                    currentAnimation = "right";

                    frame++;
                    if (frame == SpriteColumn)
                    {
                        frame = 0;
                    }

                    if (walkingManDirection.X >= MaxRigth)
                    {
                        walkingManDirection.X = MaxRigth;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    walkingManDirection.X -= Speed;
                    currentAnimation = "left";

                    frame++;
                    if (frame == SpriteColumn)
                    {
                        frame = 0;
                    }

                    if (walkingManDirection.X <= MaxLeft)
                    {
                        walkingManDirection.X = MaxLeft;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    walkingManDirection.Y -= Speed;
                    currentAnimation = "up";

                    frame++;
                    if (frame == SpriteColumn)
                    {
                        frame = 0;
                    }

                    if (walkingManDirection.Y <= MaxUp)
                    {
                        walkingManDirection.Y = MaxUp;
                    }
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    walkingManDirection.Y += Speed;
                    currentAnimation = "down";

                    frame++;
                    if (frame == SpriteColumn)
                    {
                        frame = 0;
                    }

                    if (walkingManDirection.Y >= MaxDown)
                    {
                        walkingManDirection.Y = MaxDown;
                    }
                }
            }

            otterPlayer.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(bgTexture, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            //an animation sequence that is represented by a texture with more than one row (a multi-row sprite sheet)
            otterPlayer.Draw(_spriteBatch, otterRunningDirection, SpriteEffects.None);
            //a walking man who responds to key input and animates differently depending on what direction it's moving in
            _spriteBatch.Draw(walkingManTexture, walkingManDirection, walkingManRectangle[currentAnimation][frame], Color.White);
            //foreground - otter_sleeping
            _spriteBatch.Draw(otterFgTexture, new Rectangle(400, 280, 200, 200), Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}