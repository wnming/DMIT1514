using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Lab2_Napat_Phuwarintarawanich
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Texture2D backgroundImage;
        Texture2D xTexture;
        Texture2D oTexture;

        const int WindowWidth = 601;
        const int WindowHeight = 601;

        MouseState mouseState;
        MouseState previousMouseState;

        Tile[,] GameBoard = new Tile[3, 3]; 

        public enum Turn
        {
            X,
            O
        }
        Turn playerTurn;

        public enum GameState
        {
            Initialize,
            WaitForPlayerMove,
            MakePlayerMove,
            EvaluatePlayerMove,
            GameOver
        }
        GameState currentGameState = GameState.Initialize;

        public enum MouseButtonStates
        {
            IsReleased,
            IsPressed,
            WasPressedThisFrame,
            WasReleasedThisFrame
        }
        MouseButtonStates currentMouseState = MouseButtonStates.IsReleased;

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

            playerTurn = Turn.X;

            currentMouseState = MouseButtonStates.IsReleased;
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    GameBoard[col, row] = new Tile(new Rectangle(new Point((col * 50) + (col * 10), (row * 50) + (row * 20))
                        }
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
            xTexture = Content.Load<Texture2D>("X");
            oTexture = Content.Load<Texture2D>("O");
        }

        protected override void Update(GameTime gameTime)
        {
            //currentMouseState = Mouse.GetState();

            switch (currentMouseState)
            {
                case MouseButtonStates.IsReleased:
                    if(Mouse.GetState().LeftButton == ButtonState.Pressed)
                    {
                        currentMouseState = MouseButtonStates.WasPressedThisFrame;
                    }
                    break;
                case MouseButtonStates.IsPressed:
                    if (Mouse.GetState().LeftButton == ButtonState.Released)
                    {
                        currentMouseState = MouseButtonStates.WasReleasedThisFrame;
                    }
                    break;
                default:
                    break;
            }

            switch (currentGameState)
            {
                //make sure every rectangle exists on board
                //if all the rectangle rea
                case GameState.Initialize:
                    currentMouseState = MouseButtonStates.IsReleased;
                    foreach(Tile tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.WaitForPlayerMove:
                    break;
                case GameState.MakePlayerMove:
                    break;
                case GameState.EvaluatePlayerMove:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }

            if (previousMouseState.LeftButton == ButtonState.Pressed && currentMouseState == MouseButtonStates.IsReleased)
            {
                //there has been a mouse click (mouse release)
                playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
            }

            //previousMouseState = currentMouseState;
            currentMouseState = (MouseButtonStates)Mouse.GetState().LeftButton;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.WaitForPlayerMove:
                    break;
                case GameState.MakePlayerMove:
                    break;
                case GameState.EvaluatePlayerMove:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }

            Vector2 adjustedMousePosition =
                new Vector2(mouseState.Position.X - (xTexture.Width / 2),
                    mouseState.Position.Y - (xTexture.Height / 2));

            Texture2D imageToDraw = playerTurn == Turn.O ? oTexture : xTexture;

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
            spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);
            //for (int row = 0; row < 3; row++)
            //{
            //    for (int col = 0; col < 3; col++)
            //    {
            //        if (tileState[col,row] != TileVisualState.BlankState)
            //        {
            //            if (tileState[col, row] == TileVisualState.XTileState)
            //            {
            //                spriteBatch.Draw(xTexture, GameBoard[col, row], Color.White);
            //            }else if(tileState[col, row] == TileVisualState.XTileState)
            //            {
            //                spriteBatch.Draw(oTexture, GameBoard[col, row], Color.White);
            //            }
            //        }
            //    }
            //}
            foreach(Tile tile in GameBoard)
            {
                Texture2D texture2D = null;
                if(tile._TileState == Tile.TileState.X)
                {
                    texture2D = xTexture;
                }
                else
                {
                    if(tile._TileState == Tile.TileState.O)
                    {
                        texture2D = oTexture;
                    }
                }
                spriteBatch.Draw(texture2D, tile._Rectangle, Color.White);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}