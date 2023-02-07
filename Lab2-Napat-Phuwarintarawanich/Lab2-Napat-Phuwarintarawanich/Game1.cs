using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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

        const int WindowWidth = 600;
        const int WindowHeight = 600;

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
            EvalBoard,
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
        MouseButtonStates currentMouseState;
        MouseButtonStates previousMouseState;

        MouseState currentPosition;

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
            previousMouseState = MouseButtonStates.IsReleased;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    GameBoard[col, row] =
                        new Tile(new Rectangle(new Point((col * 190) + (col * 10) + 10, (row * 190) + (row * 10) + 10), new(160, 160)));
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

            //switch (currentMouseState)
            //{
            //    case MouseButtonStates.IsReleased:
            //        if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            //        {
            //            currentMouseState = MouseButtonStates.WasPressedThisFrame;
            //        }
            //        break;
            //    case MouseButtonStates.IsPressed:
            //        if (Mouse.GetState().LeftButton == ButtonState.Released)
            //        {
            //            currentMouseState = MouseButtonStates.WasReleasedThisFrame;
            //        }
            //        break;
            //    default:
            //        break;
            //}
            currentPosition = Mouse.GetState();

            switch (currentGameState)
            {
                //make sure every rectangle exists on board
                //if all the rectangle rea
                case GameState.Initialize:
                    currentMouseState = MouseButtonStates.IsReleased;
                    currentGameState = GameState.WaitForPlayerMove;
                    foreach(Tile tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.WaitForPlayerMove:
                    if (previousMouseState == MouseButtonStates.IsPressed && currentMouseState == MouseButtonStates.IsReleased)
                    {
                        currentGameState = GameState.MakePlayerMove;
                        //playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    }
                    break;
                case GameState.MakePlayerMove:
                    currentGameState = GameState.EvaluatePlayerMove;
                    break;
                case GameState.EvaluatePlayerMove:
                    //Debug.WriteLine(currentPosition.X);
                    //Debug.WriteLine(currentPosition.Y);
                    //Debug.WriteLine(GameBoard[0, 0]._Rectangle);

                    ////playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    //GameBoard[0, 0] = new Tile(new Rectangle(new Point((0 * 170) + (0 * 170) + 25, (0 * 170) + (0 * 170) + 25), new (170, 170)), Tile.TileState.X);
                    //Debug.WriteLine(GameBoard[0, 0]._Rectangle);
                    //playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    //if(currentMouseState == MouseButtonStates.IsPressed)
                    //{
                        //playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    Debug.WriteLine(playerTurn);
                    foreach (Tile tile in GameBoard)
                    {
                        if (tile.TrySetState(currentPosition.Position, (Tile.TileState)(int)playerTurn + 1))
                        {
                            currentGameState = GameState.WaitForPlayerMove;
                        }
                    }
                    playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    //playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    //}
                    Debug.WriteLine(playerTurn);
                    //playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    break;
                case GameState.EvalBoard:
                    //gameboard[x,y]._tileState == gameboard[x+1,y]._tileState == gameboard[x+2,y]._tileState and gameboard[x,y]._tileState != TileState.Blank
                    //for (int row = 0; row < 3; row++)
                    //{
                    //    for (int col = 0; col < 3; col++)
                    //    {
                    //        if ()
                    //        {

                    //        }
                    //    }
                    //}
                    break;
                case GameState.GameOver:
                    break;
                default:
                    break;
            }

            //previousMouseState = currentMouseState;
            previousMouseState = currentMouseState;
            currentMouseState = (MouseButtonStates)Mouse.GetState().LeftButton;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);

            foreach (Tile tile in GameBoard)
            {
                Texture2D texture2D = null;
                if (tile._TileState == Tile.TileState.X)
                {
                    texture2D = xTexture;
                }
                else
                {
                    if (tile._TileState == Tile.TileState.O)
                    {
                        texture2D = oTexture;
                    }
                }
                if (texture2D != null)
                {
                    spriteBatch.Draw(texture2D, tile._Rectangle, Color.White);
                }
            }

            switch (currentGameState)
            {
                case GameState.Initialize:
                    break;
                case GameState.WaitForPlayerMove:
                    Vector2 adjustedMousePosition = new Vector2(currentPosition.Position.X - (xTexture.Width / 2),
                    currentPosition.Position.Y - (xTexture.Height / 2));

                    Texture2D imageToDraw = playerTurn == Turn.O ? oTexture : xTexture;
                    spriteBatch.Draw(imageToDraw, adjustedMousePosition, Color.White);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}