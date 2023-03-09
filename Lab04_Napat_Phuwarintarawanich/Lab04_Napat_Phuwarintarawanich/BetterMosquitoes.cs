using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Lab04_Napat_Phuwarintarawanich
{
    public class BetterMosquitoes : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int WindowWidth = 780;
        const int WindowHeight = 550;

        private Rectangle gameArea;

        private Texture2D background;
        private Texture2D playerTexture;

        Player playerOne;
        Controls playerControls;

        GameObject testObject;
        Sprite testSprite;
        Transform testTransform;
        List<Player> playerList = new List<Player>();
        List<Controls> playerControlList = new List<Controls>();
        List<PlayerBullet> playerBulletList = new();
        List<Enemy> enemyList = new();

        public BetterMosquitoes()
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
            gameArea = new Rectangle(0, 0, WindowWidth, WindowHeight);
            //change to keyboard stuff
            //playerControls = new Controls(GamePad.GetState(0).DPad.Right == ButtonState.Pressed,
            //    GamePad.GetState(0).DPad.Left == ButtonState.Pressed, 
            //    GamePad.GetState(0).Buttons.A == ButtonState.Pressed);
            playerControls = new Controls(Keys.Right, Keys.Left, Keys.Space);
            //playerControlList.Add(new Controls(Keys.D, Keys.A, Keys.Space));
            //playerControlList.Add(new Controls(Keys.Right, Keys.Left, Keys.Space));
            //playerControlList.Add(new Controls(Keys.B, Keys.M, Keys.Space));


            base.Initialize();

            //playerOne = new Player(playerTexture, new Rectangle(0, 0, WindowWidth, WindowHeight - 10).Center.ToVector2(), gameArea, playerControls);

            //test gamobj
            testTransform = new Transform();
            testSprite = new Sprite(playerTexture, playerTexture.Bounds, 1, gameArea);
            for(int i = 0; i < 3; i++)
            {
                //Player newPlayer = new Player(testSprite, new Transform(new (0, 128 * i), Vector2.Zero, 0, 0), playerControls);
                Player newPlayer = new Player(testSprite, testTransform, playerControls);
                newPlayer.transform.TranslatePosition(new Vector2(0, i * 128));
                playerList.Add(newPlayer);
            }
            //testObject = new GameObject(testSprite, testTransform);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("Background");
            playerTexture = Content.Load<Texture2D>("Cannon");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            //testObject.Update(gameTime);
            foreach(Player player in playerList)
            {
                //player.transform.TranslatePosition(new Vector2(1, 0));
                player.Update(gameTime);
            }

            foreach(PlayerBullet bullet in playerBulletList)
            {
                if(bullet.playerBulletState == ProjectileState.Flying)
                {
                    bullet.Update(gameTime);
                    //if (bullet.sprite.Bounds.Intersect(enemyList[0].sprite.Bounds))
                    //{
                    //    bullet.playerBulletState = ProjectileState.NotFlying;
                    //}
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
  
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            //testObject.Draw(_spriteBatch);
            foreach (Player player in playerList)
            {
                player.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}