using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlatformerGame
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WindowWidth = 1080;
        const int WindowHeight = 720;
        const int BackgroundWidth = 1900;
        const int BackgroundHeight = 850;

        internal const float Gravity = 9.81f;
        private Rectangle gameArea;

        Texture2D background;

        protected Player player;
        protected PlatformCollider groundCollider;
        protected PlatformCollider topCollider;
        protected PlatformCollider leftCollider;
        protected PlatformCollider rightCollider;
        protected List<Platform> platforms = new();

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            gameArea = new Rectangle(0, 0, WindowWidth, WindowHeight);

            player = new Player(new Vector2(250, 100), gameArea);
            groundCollider = new PlatformCollider(PlatformCollider.PlatformColliderType.Top, new Vector2(0, 500), new Vector2(WindowWidth, 1));
            //topCollider = new PlatformCollider(PlatformCollider.PlatformColliderType.Top, new Vector2(0, 0), new Vector2(WindowWidth, 1));
            platforms.Add(new Platform(new Vector2(50, 100), new Vector2(50, 25)));
            platforms.Add(new Platform(new Vector2(150, 150), new Vector2(50, 25)));
            platforms.Add(new Platform(new Vector2(250, 200), new Vector2(50, 25)));
            platforms.Add(new Platform(new Vector2(350, 250), new Vector2(50, 25)));

            base.Initialize();

            Window.Title = "Platformer Game";
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");
            player.LoadContent(Content);
            groundCollider.LoadContent(Content);
            //topCollider.LoadContent(Content);
            foreach (Platform platform in platforms)
            {
                platform.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState kbState = Keyboard.GetState();
            if (kbState.IsKeyDown(Keys.Left))
            {
                player.MoveLeftRight(-1);
            }
            else if (kbState.IsKeyDown(Keys.Right))
            {
                player.MoveLeftRight(1);
            }
            else
            {
                player.Stop();
            }
            if (kbState.IsKeyDown(Keys.Space))
            {
                player.Jump();
            }

            //topCollider.ProcessCollisions(player);
            groundCollider.ProcessCollisions(player);
            foreach (Platform platform in platforms)
            {
                platform.ProcessCollisions(player);
            }

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, BackgroundWidth, BackgroundHeight), Color.White);
            player.Draw(_spriteBatch);
            //topCollider.Draw(_spriteBatch);
            groundCollider.Draw(_spriteBatch);
            foreach (Platform platform in platforms)
            {
                platform.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}