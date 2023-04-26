using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlatformerGame
{
    public class PlatformerGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WindowWidth = 850;
        const int WindowHeight = 550;

        internal const float Gravity = 9.81f;

        Texture2D background;

        Player player;
        CelAnimationSet animationSet;
        List<Texture2D> playerTexture = new();

        protected PlatformCollider groundCollider;
        protected PlatformCollider topCollider;
        protected PlatformCollider leftCollider;
        protected PlatformCollider rightCollider;
        protected List<PlatformCollider> boundsCollider = new();
        protected List<Platform> platforms = new();

        List<Texture2D> itemsTexture = new();
        protected List<CollectableItem> items = new();
        List<int> randomNumber = new List<int>() { 0, 0, 0, 0, 0, 0, 0};

        private Random random = new Random();
        int redIndensity = 255, greenIndensity = 255, blueIndensity = 255;
        private const float delayTime = 5f;
        private float remainDelayTime = delayTime;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            boundsCollider.Add(new PlatformCollider(PlatformCollider.PlatformColliderType.Right, new Vector2(0, 0), new Vector2(5, WindowHeight)));
            boundsCollider.Add(new PlatformCollider(PlatformCollider.PlatformColliderType.Left, new Vector2(WindowWidth - 5, 0), new Vector2(5, WindowHeight)));
            boundsCollider.Add(new PlatformCollider(PlatformCollider.PlatformColliderType.Top, new Vector2(0, WindowHeight - 5), new Vector2(WindowWidth, 5)));
            boundsCollider.Add(new PlatformCollider(PlatformCollider.PlatformColliderType.Bottom, new Vector2(0, 0), new Vector2(WindowWidth, 5)));
            platforms.Add(new Platform(new Vector2(80, WindowHeight - 400), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(250, WindowHeight - 320), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(490, WindowHeight - 320), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(670, WindowHeight - 400), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(370, WindowHeight - 450), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(100, WindowHeight - 200), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(650, WindowHeight - 200), new Vector2(100, 25)));

            randomNumber = randomNumber.Select(x => random.Next(1, 4)).ToList();

            base.Initialize();

            Window.Title = "Platformer Game";
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.ApplyChanges();

            player = new Player(new Transform(new Vector2(48, 300), 0, 1), this, new Rectangle(0, 0, 48, 83), playerTexture[0], animationSet);

            items.Add(new CollectableItem(this, new Transform(new Vector2(110, WindowHeight - 460), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[0]));
            items.Add(new CollectableItem(this, new Transform(new Vector2(280, WindowHeight - 380), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[1]));
            items.Add(new CollectableItem(this, new Transform(new Vector2(520, WindowHeight - 380), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[2]));
            items.Add(new CollectableItem(this, new Transform(new Vector2(700, WindowHeight - 460), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[3]));
            items.Add(new CollectableItem(this, new Transform(new Vector2(395, WindowHeight - 510), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[4]));
            items.Add(new CollectableItem(this, new Transform(new Vector2(130, WindowHeight - 260), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[5]));
            items.Add(new CollectableItem(this, new Transform(new Vector2(675, WindowHeight - 260), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[6]));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background = Content.Load<Texture2D>("background");

            foreach (PlatformCollider bound in boundsCollider)
            {
                bound.LoadContent(Content);
            }
            foreach (Platform platform in platforms)
            {
                platform.LoadContent(Content);
            }
            for(int count = 0; count < 7; count++)
            {
                itemsTexture.Add(Content.Load<Texture2D>($"tetris-{randomNumber[count]}"));
            }

            playerTexture.Add(Content.Load<Texture2D>("player-idle"));
            playerTexture.Add(Content.Load<Texture2D>("player-walk"));
            playerTexture.Add(Content.Load<Texture2D>("player-jump"));
            animationSet = new CelAnimationSet(playerTexture[0], playerTexture[1], playerTexture[2]);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                player.MoveLeftRight(-1);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                player.MoveLeftRight(1);
            }
            else
            {
                player.StopMoving();
                player.currentState = Player.PlayerState.idle;
            }

            if (keyboardState.IsKeyDown(Keys.Space))
            {
                player.Jump();
            }

            foreach (PlatformCollider bound in boundsCollider)
            {
                bound.IsCollide(player);
            }
            foreach (Platform platform in platforms)
            {
                platform.IsCollide(player);
            }
            foreach (CollectableItem item in items)
            {
                item.CheckItemCollistion(player.RectangleBounds);
                item.Update(gameTime);
            }

            player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, WindowWidth, WindowHeight), new Color(Color.White, 1f));

            var timer = (float)gameTime.ElapsedGameTime.TotalSeconds;

            remainDelayTime -= timer;

            if (remainDelayTime < 0)
            {
                redIndensity = random.Next(0, 255);
                greenIndensity = random.Next(0, 255);
                blueIndensity = random.Next(0, 255);
                remainDelayTime = delayTime;
            }
            foreach (Platform platform in platforms)
            {
                platform.Draw(_spriteBatch, new Color(redIndensity, greenIndensity, blueIndensity));
            }
            
            foreach (PlatformCollider bound in boundsCollider)
            {
                bound.Draw(_spriteBatch, Color.White);
            }
            
            foreach(CollectableItem item in items)
            {
                item.Draw(gameTime);
            }
            
            player.Draw(gameTime);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}