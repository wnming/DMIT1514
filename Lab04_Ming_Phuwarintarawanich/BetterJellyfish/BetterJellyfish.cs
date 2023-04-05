using Lab3_Napat_Phuwarintarawanich;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BetterJellyfish
{
    public class BetterJellyfish : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 630;
        const int GameHeight = 550;

        private Rectangle gameArea;

        private Texture2D mainMenuBackground;
        private Texture2D level1Background;
        private Texture2D level2Background;
        private Texture2D gameOverBackground;

        private SpriteFont cocogoose;
        private SpriteFont tommy;

        Player GamePlayer;
        PlayerControls PlayerControl;
        Sprite PlayerSprite;
        Texture2D PlayerSpriteSheet;

        Player PlayerBuddy;
        Sprite PlayerBuddySprite;
        Texture2D PlayerBuddySpriteSheet;

        List<Barrier> BarriersList = new();
        Texture2D BarrierTexture;
        Sprite BarrierSprite;

        private bool IsFirstTime;
        private bool IsLevel1;
        private bool IsPlayerWins;
        private const int MaxHeart = 3;
        private const int defaultNumberOfEnemy = 16;
        private const int defaulfEnemyRow = 2;
        private const int defaulfYPosition = 5;
        private int numberOfEnemy = defaultNumberOfEnemy;
        private int enemyRow = defaulfEnemyRow;
        private int initialYPosition = defaulfYPosition;
        private string GameStateMessage;
        private int CurrentAliveEnemy;
        List<Enemy> EnemyList = new();
        Sprite EnemySprite;
        Texture2D EnemySpriteSheet;

        public int TempPlayerHeart;
        public int TempPlayerScore;
        public int TempPlayerBuddyScore;

        private GameState CurrentGameState;
        protected KeyboardState PreviousState;

        HUD Hud;

        public BetterJellyfish()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameArea = new Rectangle(0, 0, WindowWidth, GameHeight);
            PlayerControl = new PlayerControls(Keys.Right, Keys.Left, Keys.Space);
            CurrentGameState = GameState.Initialize;
            IsFirstTime = true;
            IsLevel1 = true;
            PreviousState = Keyboard.GetState();

            Hud = new HUD();
            Hud.Initialize(new Vector2(0, GameHeight));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            level1Background = Content.Load<Texture2D>("Level1-background");
            level2Background = Content.Load<Texture2D>("Level2-background");
            mainMenuBackground = Content.Load<Texture2D>("MainMenu-background");
            gameOverBackground = Content.Load<Texture2D>("GameOver-background");

            cocogoose = Content.Load<SpriteFont>("cocogoose");
            tommy = Content.Load<SpriteFont>("tommy");

            PlayerSpriteSheet = Content.Load<Texture2D>("player");
            EnemySpriteSheet = Content.Load<Texture2D>("jellyfish-enemy");
            PlayerBuddySpriteSheet = Content.Load<Texture2D>("player-buddy");

            BarrierTexture = Content.Load<Texture2D>("barrier");

            Hud.LoadContent(Content, tommy);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            switch (CurrentGameState)
            {
                case GameState.Initialize:
                    EnemyList.Clear();
                    enemyRow = defaulfEnemyRow;
                    initialYPosition = defaulfYPosition;
                    numberOfEnemy = defaultNumberOfEnemy;
                    IsPlayerWins = false;

                    PlayerSprite = new Sprite(PlayerSpriteSheet, new Rectangle(0, 0, PlayerSpriteSheet.Bounds.Width / 6, PlayerSpriteSheet.Height), 108, 108, 1 / 7f, 6, 1);
                    GamePlayer = new Player(PlayerSprite, new ObjectTransform(), PlayerControl, gameArea, false);
                    GamePlayer.Transform.TranslatePosition(new Vector2(200, 440));
                    GamePlayer.LoadContent(Content);

                    PlayerBuddySprite = new Sprite(PlayerBuddySpriteSheet, new Rectangle(0, 0, PlayerBuddySpriteSheet.Bounds.Width / 4, PlayerBuddySpriteSheet.Height), 59, 93, 1 / 7f, 4, 1);
                    PlayerBuddy = new Player(PlayerBuddySprite, new ObjectTransform(), new PlayerControls(), gameArea, true);
                    PlayerBuddy.Transform.TranslatePosition(new Vector2(100, 480));
                    PlayerBuddy.LoadContent(Content);
                    if (!IsLevel1)
                    {
                        numberOfEnemy = 24;
                        enemyRow = 3;
                        GamePlayer.CurrentHeart = TempPlayerHeart;
                        GamePlayer.PlayerScore = TempPlayerScore;
                        PlayerBuddy.PlayerScore = TempPlayerBuddyScore;
                    }
                    else
                    {
                        GamePlayer.Reset(MaxHeart);
                        TempPlayerHeart = MaxHeart;
                        TempPlayerScore = 0;
                        TempPlayerBuddyScore = 0;
                    }

                    EnemySprite = new Sprite(EnemySpriteSheet, new Rectangle(0, 0, EnemySpriteSheet.Bounds.Width / 5, EnemySpriteSheet.Bounds.Height), 63, 53, 1 / 0.5f, 5, 1);
                    int numberOfEnemyPerRow = numberOfEnemy / enemyRow;
                    while (enemyRow > 0)
                    {
                        for (int i = 0; i < numberOfEnemyPerRow; i++)
                        {
                            Enemy newEnemy = new Enemy(EnemySprite, new ObjectTransform());
                            newEnemy.Transform.TranslatePosition(new Vector2(i * 60, initialYPosition));
                            newEnemy.LoadContent(Content);
                            EnemyList.Add(newEnemy);
                        }
                        initialYPosition += 70;
                        enemyRow -= 1;
                    }
                    BarrierSprite = new Sprite(BarrierTexture, BarrierTexture.Bounds, 77, 150, 0, 1, 1);
                    for (int index = 0; index < 2; index++)
                    {
                        Barrier newBarrier = new Barrier(BarrierSprite, new ObjectTransform());
                        newBarrier.Transform.TranslatePosition(new Vector2(index * 420 + 100, 350));
                        newBarrier.Sprite.UpdateSpriteBounds(newBarrier.Transform);
                        BarriersList.Add(newBarrier);
                    }
                    CurrentGameState = GameState.MainMenu;
                    break;
                case GameState.MainMenu:
                    if (IsFirstTime)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            CurrentGameState = GameState.Level1;
                        }
                    }
                    else
                    {
                        if (IsLevel1)
                        {
                            CurrentGameState = GameState.Level1;
                        }
                        else
                        {
                            CurrentGameState = GameState.Level2;
                        }
                    }
                    break;
                case GameState.Level1:
                    IsFirstTime = false;
                    if (GamePlayer.IsReloadBullet() && Keyboard.GetState().IsKeyDown(Keys.L) && PreviousState.IsKeyUp(Keys.L))
                    {
                        GamePlayer.ReloadBullet = GamePlayer.MaxLoadBullet;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) && PreviousState.IsKeyUp(Keys.P))
                    {
                        CurrentGameState = GameState.Paused;
                        GameStateMessage = "Paused, press P to start playing.";
                    }
                    GamePlayer.Update(gameTime);
                    PlayerBuddy.Update(gameTime);
                    CurrentAliveEnemy = 0;
                    foreach (Enemy enemy in EnemyList)
                    {
                        enemy.Update(gameTime);
                        foreach (Barrier barrier in BarriersList)
                        {
                            barrier.CheckBarrierCollision(enemy.Sprite.SpriteBounds);
                            if (enemy.CurrentEnemyState == EnemyState.Alive && GamePlayer.CheckBulletCollision(enemy.Sprite.SpriteBounds, barrier))
                            {
                                enemy.EnemyDie();
                            }
                            if (enemy.CurrentEnemyState == EnemyState.Alive && PlayerBuddy.CheckBulletCollision(enemy.Sprite.SpriteBounds, barrier))
                            {
                                enemy.EnemyDie();
                            }
                            if (GamePlayer.CurrentPlayerState == PlayerState.Alive && enemy.CurrentEnemyState == EnemyState.Alive && enemy.CheckEnenmyBulletCollision(GamePlayer.Sprite.SpriteBounds, barrier))
                            {
                                GamePlayer.CurrentHeart -= 1;
                                if (GamePlayer.CurrentHeart <= 0 || GamePlayer.CurrentPlayerState == PlayerState.Dead)
                                {
                                    GamePlayer.PlayerDie();
                                    CurrentGameState = GameState.GameOver;
                                }
                            }
                        }
                        if (enemy.CurrentEnemyState == EnemyState.Alive)
                        {
                            CurrentAliveEnemy += 1;
                        }
                    }
                    if (GamePlayer.CurrentPlayerState == PlayerState.Alive && EnemyList.Where(x => x.CurrentEnemyState == EnemyState.Dead).Count() == EnemyList.Count())
                    {
                        TempPlayerHeart = GamePlayer.CurrentHeart;
                        TempPlayerScore = GamePlayer.PlayerScore;
                        IsLevel1 = false;
                        CurrentGameState = GameState.Initialize;
                    }
                    Hud.Update(GamePlayer.CurrentHeart, CurrentAliveEnemy, GamePlayer.ReloadBullet);
                    break;
                case GameState.Level2:
                    IsLevel1 = false;
                    if (GamePlayer.IsReloadBullet() && Keyboard.GetState().IsKeyDown(Keys.L) && PreviousState.IsKeyUp(Keys.L))
                    {
                        GamePlayer.ReloadBullet = GamePlayer.MaxLoadBullet;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P) && PreviousState.IsKeyUp(Keys.P))
                    {
                        CurrentGameState = GameState.Paused;
                        GameStateMessage = "Paused, press P to continue.";
                    }
                    GamePlayer.Update(gameTime);
                    PlayerBuddy.Update(gameTime);
                    CurrentAliveEnemy = 0;
                    foreach (Enemy enemy in EnemyList)
                    {
                        enemy.Update(gameTime);
                        foreach (Barrier barrier in BarriersList)
                        {
                            barrier.CheckBarrierCollision(enemy.Sprite.SpriteBounds);
                            if (enemy.CurrentEnemyState == EnemyState.Alive && GamePlayer.CheckBulletCollision(enemy.Sprite.SpriteBounds, barrier))
                            {
                                enemy.EnemyDie();
                            }
                            if (enemy.CurrentEnemyState == EnemyState.Alive && PlayerBuddy.CheckBulletCollision(enemy.Sprite.SpriteBounds, barrier))
                            {
                                enemy.EnemyDie();
                            }
                            if (GamePlayer.CurrentPlayerState == PlayerState.Alive && enemy.CurrentEnemyState == EnemyState.Alive && enemy.CheckEnenmyBulletCollision(GamePlayer.Sprite.SpriteBounds, barrier))
                            {
                                GamePlayer.CurrentHeart -= 1;
                                if (GamePlayer.CurrentHeart <= 0 || GamePlayer.CurrentPlayerState == PlayerState.Dead)
                                {
                                    GamePlayer.PlayerDie();
                                    IsLevel1 = true;
                                    CurrentGameState = GameState.GameOver;
                                }
                            }
                        }
                        if (enemy.CurrentEnemyState == EnemyState.Alive)
                        {
                            CurrentAliveEnemy += 1;
                        }
                    }
                    if (GamePlayer.CurrentPlayerState == PlayerState.Alive && EnemyList.Where(x => x.CurrentEnemyState == EnemyState.Dead).Count() == EnemyList.Count())
                    {
                        IsPlayerWins = true;
                        IsLevel1 = true;
                        CurrentGameState = GameState.GameOver;
                    }
                    Hud.Update(GamePlayer.CurrentHeart, CurrentAliveEnemy, GamePlayer.ReloadBullet);
                    break;
                case GameState.Paused:
                    if (Keyboard.GetState().IsKeyDown(Keys.P) && PreviousState.IsKeyUp(Keys.P))
                    {
                        CurrentGameState = IsLevel1 ? GameState.Level1 : GameState.Level2;
                        GameStateMessage = string.Empty;
                    }
                    break;
                case GameState.GameOver:
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.Initialize;
                    }
                    break;
                default:
                    break;
            }
            PreviousState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            switch (CurrentGameState)
            {
                case GameState.MainMenu:
                    if (IsFirstTime)
                    {
                        _spriteBatch.Draw(mainMenuBackground, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                        _spriteBatch.DrawString(cocogoose, "Better Jellyfish", new Vector2(100, 100), Color.White);
                        _spriteBatch.DrawString(tommy, "Hit enter to start a game".ToUpper(), new Vector2(210, 220), Color.White);
                    }
                    break;
                case GameState.Level1:
                    _spriteBatch.Draw(level1Background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                    PlayerBuddy.Draw(_spriteBatch);
                    PlayerBuddy.DrawBullet(_spriteBatch);
                    GamePlayer.Draw(_spriteBatch);
                    GamePlayer.DrawBullet(_spriteBatch);
                    foreach (Enemy enemy in EnemyList)
                    {
                        if (enemy.CurrentEnemyState == EnemyState.Alive)
                        {
                            enemy.Draw(_spriteBatch);
                            enemy.DrawEnemyBullet(_spriteBatch);
                        }
                    }
                    if (GamePlayer.IsReloadBullet())
                    {
                        _spriteBatch.DrawString(tommy, "Press L to reload bullets..", new Vector2(270, 500), Color.Red);
                    }
                    foreach (Barrier barrier in BarriersList)
                    {
                        barrier.Draw(_spriteBatch);
                    }
                    Hud.Draw(_spriteBatch);
                    break;
                case GameState.Level2:
                    _spriteBatch.Draw(level2Background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                    PlayerBuddy.Draw(_spriteBatch);
                    PlayerBuddy.DrawBullet(_spriteBatch);
                    GamePlayer.Draw(_spriteBatch);
                    GamePlayer.DrawBullet(_spriteBatch);
                    foreach (Enemy enemy in EnemyList)
                    {
                        if (enemy.CurrentEnemyState == EnemyState.Alive)
                        {
                            enemy.Draw(_spriteBatch);
                            enemy.DrawEnemyBullet(_spriteBatch);
                        }
                    }
                    if (GamePlayer.IsReloadBullet())
                    {
                        _spriteBatch.DrawString(tommy, "Press L to reload bullets..", new Vector2(270, 500), Color.Red);
                    }
                    foreach (Barrier barrier in BarriersList)
                    {
                        barrier.Draw(_spriteBatch);
                    }
                    Hud.Draw(_spriteBatch);
                    break;
                case GameState.Paused:
                    _spriteBatch.Draw(IsLevel1 ? level1Background : level2Background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.DarkGray);
                    _spriteBatch.DrawString(tommy, GameStateMessage, new Vector2(280, 120), Color.White);
                    break;
                case GameState.GameOver:
                    _spriteBatch.Draw(gameOverBackground, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
                    _spriteBatch.DrawString(cocogoose, IsPlayerWins ? "Player Wins" : "Game Over", new Vector2(190, 80), Color.DeepPink);
                    _spriteBatch.DrawString(tommy, $"You shot {GamePlayer.PlayerScore} enemies.", new Vector2(270, 170), Color.DarkOrange);
                    _spriteBatch.DrawString(tommy, $"Your buddy shot {PlayerBuddy.PlayerScore} enemies.", new Vector2(230, 210), Color.BlueViolet);
                    _spriteBatch.DrawString(tommy, "Hit enter to Play Again".ToUpper(), new Vector2(210, WindowHeight - 60), Color.White);
                    break;
                default:
                    break;
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public enum GameState
    {
        Initialize,
        MainMenu,
        Level1,
        Level2,
        Paused,
        GameOver,
    }
    public enum BulletState
    {
        NotFlying,
        Flying
    }
}