using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Lab2_Napat_Phuwarintarawanich
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D backgroundImage;
        Texture2D xImage;
        Texture2D oImage;

        const int WindowWidth = 601;
        const int WindowHeight = 601;

        MouseState currentMouseState;
        MouseState previousMouseState;

        public enum GameSpaceState
        {
            Empty,
            X,
            O
        }

        GameSpaceState nextTokenToBePlayed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            graphics.ApplyChanges();

            nextTokenToBePlayed = GameSpaceState.X;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
            xImage = Content.Load<Texture2D>("X");
            oImage = Content.Load<Texture2D>("O");
        }

        protected override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();

            if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState.LeftButton == ButtonState.Released)
            {
                //there has been a mouse click (mouse release)
                if (nextTokenToBePlayed == GameSpaceState.X)
                {
                    nextTokenToBePlayed = GameSpaceState.O;
                }
                else if (nextTokenToBePlayed == GameSpaceState.O)
                {
                    nextTokenToBePlayed = GameSpaceState.X;
                }
            }

            previousMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Vector2 adjustedMousePosition =
                new Vector2(currentMouseState.Position.X - (xImage.Width / 2),
                    currentMouseState.Position.Y - (xImage.Height / 2));

            Texture2D imageToDraw = xImage;
            if (nextTokenToBePlayed == GameSpaceState.O)
            {
                imageToDraw = oImage;
            }
            else if (nextTokenToBePlayed == GameSpaceState.X)
            {
                imageToDraw = xImage;
            }

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
            spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}