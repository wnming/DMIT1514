using Lesson05_Animations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BetterMosquitoes
{
    public class BetterMosquitoes : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WindowWidth = 800;
        const int WindowHeight = 550;

        private Rectangle gameArea;

        private Texture2D background;

        Player GamePlayer;
        PlayerControls PlayerControl;
        Sprite PlayerSprite;
        Texture2D PlayerSpriteSheet;

        List<Enemy> EnemyList = new();
        Sprite EnemySprite;
        Texture2D EnemySpriteSheet;

        public BetterMosquitoes()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = WindowWidth;
            _graphics.PreferredBackBufferHeight = WindowHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            //playerDirection = new Vector2(200, 440);
            //enemyDirection = new Vector2(5, 5);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameArea = new Rectangle(0, 0, WindowWidth, WindowHeight);
            PlayerControl = new PlayerControls(Keys.Right, Keys.Left, Keys.Space);

            base.Initialize();

            PlayerSprite = new Sprite(PlayerSpriteSheet, PlayerSpriteSheet.Bounds, 108, 108, 6, 1);
            GamePlayer = new Player(PlayerSprite, new ObjectTransform(), PlayerControl, gameArea);
            GamePlayer.Transform.TranslatePosition(new Vector2(200, 440));

            EnemySprite = new Sprite(EnemySpriteSheet, EnemySpriteSheet.Bounds, 63, 53, 5, 1);
            for (int i = 0; i < 5; i++)
            {
                Enemy newEnemy = new Enemy(EnemySprite, new ObjectTransform());
                newEnemy.Transform.TranslatePosition(new Vector2(i * 60, 5));
                EnemyList.Add(newEnemy);
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            background = Content.Load<Texture2D>("Background");

            PlayerSpriteSheet = Content.Load<Texture2D>("player");
            EnemySpriteSheet = Content.Load<Texture2D>("jellyfish-enemy");
            //player = new CelAnimationSequence(playerSpriteSheet, 108, 108, 1 / 9f, 6, 1);

            //playerAnimation = new CelAnimationPlayer();
            //playerAnimation.Play(player);

            //Texture2D enemySpriteSheet = Content.Load<Texture2D>("jellyfish-enemy");
            //enemy = new CelAnimationSequence(enemySpriteSheet, 63, 53, 1 / 9f, 5, 1);

            //enemyAnimation = new CelAnimationPlayer();
            //enemyAnimation.Play(enemy);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            GamePlayer.Update(gameTime);

            foreach(Enemy enemy in EnemyList)
            {
                enemy.Update(gameTime);
            }

            //playerAnimation.Update(gameTime);
            //enemyAnimation.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            GamePlayer.Draw(_spriteBatch);
            foreach(Enemy enemy in EnemyList)
            {
                enemy.Draw(_spriteBatch);
            }
            //playerAnimation.Draw(_spriteBatch, playerDirection, SpriteEffects.None);
            //enemyAnimation.Draw(_spriteBatch, enemyDirection, SpriteEffects.None);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}