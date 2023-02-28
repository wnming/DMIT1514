using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            playerControls = new Controls(GamePad.GetState(0).DPad.Right == ButtonState.Pressed,
                GamePad.GetState(0).DPad.Left == ButtonState.Pressed, 
                GamePad.GetState(0).Buttons.A == ButtonState.Pressed);

            base.Initialize();

            playerOne = new Player(playerTexture, new Rectangle(0, 0, WindowWidth, WindowHeight - 10).Center.ToVector2(), gameArea, playerControls);
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

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
  
            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Rectangle(0, 0, WindowWidth, WindowHeight), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}