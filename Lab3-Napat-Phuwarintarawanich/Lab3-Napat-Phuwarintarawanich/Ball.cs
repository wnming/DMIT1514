using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
//using static Lab3_Napat_Phuwarintarawanich.Pong;
using System.Reflection.Metadata;
using static Lab3_Napat_Phuwarintarawanich.Pong;

namespace Lab3_Napat_Phuwarintarawanich
{
    internal class Ball
    {
        private const float InitialSpeed = 300;
        private const float MinSpeed = 100;
        private const float MaxSpeed = 500;

        private float speed;
        internal float Speed
        {
            get => speed;
            set 
            { 
                if(value >= MinSpeed && value <= MaxSpeed)
                {
                    speed = value;
                }
            }
        }

        private Texture2D pongBallTexture;
        private Vector2 pongBallDirection;
        private Vector2 pongBallPosition;

        private Rectangle gameArea;

        private Rectangle ballRectangle => new Rectangle((int)pongBallPosition.X, (int)pongBallPosition.Y, pongBallTexture.Width, pongBallTexture.Height);


        internal void Initialize(Vector2 initialPosition, Rectangle area, Vector2 initialDirection)
        {
            pongBallDirection = initialDirection;
            pongBallPosition = initialPosition;
            this.gameArea = area;
            Speed = InitialSpeed;
        }

        internal void LoadContent(ContentManager content)
        {
            pongBallTexture = content.Load<Texture2D>("pong-ball");
        }

        internal void Update(GameTime gameTime)
        {
            pongBallPosition += pongBallDirection * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (pongBallPosition.Y > gameArea.Height - pongBallTexture.Height || pongBallPosition.Y < 0)
            {
                pongBallDirection.Y *= -1;
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pongBallTexture, pongBallPosition, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }

        internal bool IsCollide(Rectangle paddingRectangle)
        {
            bool isCollide = false;
            if (paddingRectangle.Intersects(ballRectangle))
            {
                pongBallDirection.X *= -1;
                isCollide = true;
            }
            return isCollide;
        }

        internal bool IsOffBorder()
        {
            bool isOffBorder = false;
            if (ballRectangle.X < 0 || ballRectangle.X > gameArea.Width)
            {
                isOffBorder = true;
            }
            return isOffBorder;
        }
    }
}
