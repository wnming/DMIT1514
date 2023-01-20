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

        private Texture2D bgTexture;
        private Texture2D fgTexture;

        CelAnimationSequence otterRunning;
        CelAnimationPlayer animationPlayer;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bgTexture = Content.Load<Texture2D>("zoo_background");
            fgTexture = Content.Load<Texture2D>("otter_foreground");

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
            animationPlayer.Update(gameTime);

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
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}