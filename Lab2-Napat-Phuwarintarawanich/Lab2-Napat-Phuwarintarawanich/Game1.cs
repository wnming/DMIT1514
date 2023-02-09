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
        Texture2D xTexture;
        Texture2D oTexture;
        Texture2D newGameTexture;

        Vector2 newGameDirection = new Vector2();
        Rectangle newGameRectangle = new Rectangle();

        SpriteFont font;

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
            IsPressed
        }
        MouseButtonStates currentMouseState;
        MouseButtonStates previousMouseState;

        MouseState currentPosition;

        bool gameOver = false;
        string gameWinner;

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
                        new Tile(new Rectangle(new Point((col * 190) + (col * 5) + 30, (row * 190) + (row * 5) + 30), new(150, 150)));
                }
            }

            base.Initialize();

            newGameDirection = new Vector2(140, 250);
            newGameRectangle = newGameTexture.Bounds;
            newGameRectangle.Offset(newGameDirection);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundImage = Content.Load<Texture2D>("TicTacToeBoard");
            xTexture = Content.Load<Texture2D>("X");
            oTexture = Content.Load<Texture2D>("O");
            newGameTexture = Content.Load<Texture2D>("start-new-game");
            font = Content.Load<SpriteFont>("RoseQuay");
        }

        protected override void Update(GameTime gameTime)
        {
            currentPosition = Mouse.GetState();

            switch (currentGameState)
            {
                case GameState.Initialize:
                    currentMouseState = MouseButtonStates.IsReleased;
                    currentGameState = GameState.WaitForPlayerMove;
                    playerTurn = Turn.X;
                    foreach (Tile tile in GameBoard)
                    {
                        tile.Reset();
                    }
                    break;
                case GameState.WaitForPlayerMove:
                    if (previousMouseState == MouseButtonStates.IsPressed && currentMouseState == MouseButtonStates.IsReleased)
                    {
                        currentGameState = GameState.MakePlayerMove;
                    }
                    break;
                case GameState.MakePlayerMove:
                    currentGameState = GameState.EvaluatePlayerMove;
                    break;
                case GameState.EvaluatePlayerMove:
                    foreach (Tile tile in GameBoard)
                    {
                        if (tile.TrySetState(currentPosition.Position, (Tile.TileState)(int)playerTurn + 1))
                        {
                            currentGameState = GameState.EvalBoard;
                        }
                    }
                    playerTurn = playerTurn == Turn.X ? Turn.O : Turn.X;
                    break;
                case GameState.EvalBoard:
                    Winner winner = new Winner();
                    if (winner.CheckTheWinner(0, 0, 0, 0, 1, 2, GameBoard) || winner.CheckTheWinner(1, 1, 1, 0, 1, 2, GameBoard)
                        || winner.CheckTheWinner(2, 2, 2, 0, 1, 2, GameBoard) || winner.CheckTheWinner(0, 1, 2, 0, 0, 0, GameBoard)
                        || winner.CheckTheWinner(0, 1, 2, 1, 1, 1, GameBoard) || winner.CheckTheWinner(0, 1, 2, 2, 2, 2, GameBoard)
                        || winner.CheckTheWinner(0, 1, 2, 0, 1, 2, GameBoard) || winner.CheckTheWinner(2, 1, 0, 0, 1, 2, GameBoard))
                    {
                        gameWinner = $"The winner is {winner._WinTile}";
                        gameOver = true;
                        currentGameState = GameState.GameOver;
                    }
                    else
                    {
                        if (!gameOver)
                        {
                            int countBlank = 0;
                            foreach (Tile tile in GameBoard)
                            {
                                if (tile._TileState == Tile.TileState.Blank)
                                {
                                    countBlank++;
                                }
                            }
                            if (countBlank == 0)
                            {
                                gameWinner = "...... It is a Tie ......";
                                gameOver = true;
                                currentGameState = GameState.GameOver;
                            }
                            else
                            {
                                currentGameState = GameState.WaitForPlayerMove;
                            }
                        }
                    }
                    break;
                case GameState.GameOver:
                    this.IsMouseVisible = true;
                    if (newGameRectangle.Contains(new Point(currentPosition.X, currentPosition.Y)) 
                        && previousMouseState == MouseButtonStates.IsPressed && currentMouseState == MouseButtonStates.IsReleased)
                    {
                        currentGameState = GameState.Initialize;
                        gameOver = false;
                    }
                    break;
                default:
                    break;
            }

            previousMouseState = currentMouseState;
            currentMouseState = (MouseButtonStates)Mouse.GetState().LeftButton;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();

            if (!gameOver)
            {
                spriteBatch.Draw(backgroundImage, Vector2.Zero, Color.White);
                foreach (Tile tile in GameBoard)
                {
                    Texture2D texture2D = tile._TileState == Tile.TileState.X ? xTexture : tile._TileState == Tile.TileState.O ? oTexture : null;
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
            }
            else
            {
                Vector2 textCenter = font.MeasureString("") / 2f;
                spriteBatch.DrawString(font, gameWinner, new Vector2(110, 150), Color.DarkGoldenrod, 0, textCenter, 2.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(newGameTexture, newGameDirection, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}