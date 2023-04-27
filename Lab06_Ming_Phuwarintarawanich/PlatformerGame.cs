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
        Texture2D mainmenuBackground;
        Texture2D finishBackground;

        Player player;
        CelAnimationSet animationSet;
        List<Texture2D> playerTexture = new();

        protected ColliderLeft leftCollider;
        protected ColliderRight rightCollider;
        protected ColliderTop topCollider;
        protected ColliderBottom bottomCollider;
        protected List<Platform> platforms = new();

        List<Texture2D> itemsTexture = new();
        protected List<CollectableItem> items = new();
        List<int> randomNumber = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };
        int itemsCelWidth = 40;

        Texture2D trophyTexture;
        CollectableItem trophy;

        private Random random = new Random();
        int redIndensity = 255, greenIndensity = 255, blueIndensity = 255;
        private const float delayTime = 5f;
        private float remainDelayTime = delayTime;

        internal static GameState gameState { get; private set; } = GameState.MainMenu;
        private double elapsedTime = 0;

        private SpriteFont font;
        private SpriteFont biggerFont;
        private int countStartTime = 1500;
        private int countStart = 3;

        public PlatformerGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            rightCollider = new ColliderRight(new Vector2(0, 0), new Vector2(5, WindowHeight));
            leftCollider = new ColliderLeft(new Vector2(WindowWidth - 5, 0), new Vector2(5, WindowHeight));
            topCollider = new ColliderTop(new Vector2(0, WindowHeight - 5), new Vector2(WindowWidth, 5));
            bottomCollider = new ColliderBottom(new Vector2(0, 0), new Vector2(WindowWidth, 5));
            platforms.Add(new Platform(new Vector2(80, WindowHeight - 400), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(250, WindowHeight - 320), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(490, WindowHeight - 320), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(670, WindowHeight - 400), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(370, WindowHeight - 450), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(100, WindowHeight - 200), new Vector2(100, 25)));
            platforms.Add(new Platform(new Vector2(650, WindowHeight - 200), new Vector2(100, 25)));

            randomNumber = randomNumber.Select(x => random.Next(1, 4)).ToList();

            base.Initialize();

            Window.Title = "Platformer Game - Collect all Tetris";
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            _graphics.ApplyChanges();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("kindergarten");
            biggerFont = Content.Load<SpriteFont>("kindergartenBigger");
            background = Content.Load<Texture2D>("background");
            mainmenuBackground = Content.Load<Texture2D>("mainmenu-bg");
            finishBackground = Content.Load<Texture2D>("finish-bg");

            rightCollider.LoadContent(Content);
            leftCollider.LoadContent(Content);
            topCollider.LoadContent(Content);
            bottomCollider.LoadContent(Content);

            foreach (Platform platform in platforms)
            {
                platform.LoadContent(Content);
            }
            for (int count = 0; count < 7; count++)
            {
                itemsTexture.Add(Content.Load<Texture2D>($"tetris-{randomNumber[count]}"));
            }
            trophyTexture = Content.Load<Texture2D>("trophy");
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
            switch (gameState)
            {
                case GameState.MainMenu:
                    elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsedTime > countStartTime)
                    {
                        countStart -= 1;
                        countStartTime += 1900;
                    }
                    if (elapsedTime > 5000)
                    {
                        player = new Player(new Transform(new Vector2(48, 300), 0, 1), this, new Rectangle(0, 0, 48, 83), playerTexture[0], animationSet);

                        items.Add(new CollectableItem(this, new Transform(new Vector2(110, WindowHeight - 460), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[0], itemsCelWidth));
                        items.Add(new CollectableItem(this, new Transform(new Vector2(280, WindowHeight - 380), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[1], itemsCelWidth));
                        items.Add(new CollectableItem(this, new Transform(new Vector2(520, WindowHeight - 380), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[2], itemsCelWidth));
                        items.Add(new CollectableItem(this, new Transform(new Vector2(700, WindowHeight - 460), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[3], itemsCelWidth));
                        items.Add(new CollectableItem(this, new Transform(new Vector2(395, WindowHeight - 510), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[4], itemsCelWidth));
                        items.Add(new CollectableItem(this, new Transform(new Vector2(130, WindowHeight - 260), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[5], itemsCelWidth));
                        items.Add(new CollectableItem(this, new Transform(new Vector2(675, WindowHeight - 260), 0, 1), new Rectangle(0, 0, 48, 83), itemsTexture[6], itemsCelWidth));
                         
                        trophy = new CollectableItem(this, new Transform(new Vector2(WindowWidth - 80, WindowHeight - 95), 0, 1), new Rectangle(0, 0, 48, 83), trophyTexture, 68);
                        trophy.SetState(CollectableItem.ItemState.Hidden);

                        gameState = GameState.PlayGame;
                    }
                    break;
                case GameState.PlayGame:
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
                    rightCollider.CheckCollision(player);
                    leftCollider.CheckCollision(player);
                    topCollider.CheckCollision(player);
                    bottomCollider.CheckCollision(player);
                    foreach (Platform platform in platforms)
                    {
                        platform.IsCollide(player);
                    }
                    foreach (CollectableItem item in items)
                    {
                        item.CheckItemCollistion(player.RectangleBounds);
                        item.Update(gameTime);
                    }
                    if (items.Where(x => x.GetState() == CollectableItem.ItemState.Collect).Count() == items.Count())
                    {
                        if(trophy.GetState() == CollectableItem.ItemState.Hidden)
                        {
                            trophy.SetState(CollectableItem.ItemState.NotCollect);
                        }
                        trophy.CheckItemCollistion(player.RectangleBounds);
                        trophy.Update(gameTime);
                    }
                    if (items.Where(x => x.GetState() == CollectableItem.ItemState.Collect).Count() == items.Count() && trophy.GetState() == CollectableItem.ItemState.Collect)
                    {
                        gameState = GameState.Finish;
                    }
                    player.Update(gameTime);
                    break;
                case GameState.Finish:
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        elapsedTime = 0;
                        countStartTime = 1500;
                        countStart = 3;
                        gameState = GameState.MainMenu;
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

            _spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.MainMenu:
                    _spriteBatch.Draw(mainmenuBackground, new Rectangle(0, 0, WindowWidth + 90, WindowHeight), Color.White);
                    _spriteBatch.DrawString(font, "Collect all tetris to win the game", new Vector2(20, WindowHeight / 2 - 90), Color.Violet);
                    _spriteBatch.DrawString(biggerFont, countStart.ToString(), new Vector2(220, WindowHeight - 300), Color.LightPink);
                    break;
                case GameState.PlayGame:
                    _spriteBatch.Draw(background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);

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
                    rightCollider.Draw(_spriteBatch, Color.White);
                    leftCollider.Draw(_spriteBatch, Color.White);
                    topCollider.Draw(_spriteBatch, Color.White);
                    bottomCollider.Draw(_spriteBatch, Color.White);
                    foreach (CollectableItem item in items)
                    {
                        item.Draw(gameTime);
                    }
                    trophy.Draw(gameTime);
                    player.Draw(gameTime);
                    break;
                case GameState.Finish:
                    _spriteBatch.Draw(finishBackground, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                    _spriteBatch.DrawString(font, "Thank you for playing the game!", new Vector2(165, WindowHeight / 2 - 100), Color.LightSeaGreen);
                    _spriteBatch.DrawString(font, "HAVE A GREAT DAY! :)", new Vector2(250, WindowHeight / 2 + 80), Color.LightSalmon);
                    _spriteBatch.DrawString(font, "Enter to play again...", new Vector2(265, WindowHeight - 60), Color.MediumVioletRed);
                    break;
                default:
                    break;
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        public enum GameState
        {
            MainMenu,
            PlayGame,
            Finish,
        }
    }
}