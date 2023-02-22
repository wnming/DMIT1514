using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Lab3_Napat_Phuwarintarawanich
{
    internal class Paddle
    {
        internal const int Speed = 400;
        private const int WIDTH = 10;
        private const int HEIGHT = 135;

        protected Texture2D paddleTexture;
        protected Vector2 paddlePosition;

        private Rectangle gameArea;

        internal bool IsHit { get; set; } = false;
        internal bool IsSticky { get; set; } = false;

        private Color paddleColor;
        private double elapsedTime = 0;

        internal int Width => WIDTH;
        internal int Height => HEIGHT;

        protected Vector2 direction;
        internal Vector2 Direction
        {
            get => direction;
            set => direction = value;
        }

        internal Rectangle PaddingRectangle
        {
            get
            {
                return new Rectangle((int)paddlePosition.X, (int)paddlePosition.Y, paddleTexture.Width, paddleTexture.Height);
            }
        }

        internal void Initialize( Vector2 initialPosition, Rectangle gameArea)
        {
            paddlePosition = initialPosition;
            this.gameArea = gameArea;
            elapsedTime = 0;
            paddleColor = Color.White;
            IsSticky = false;
            IsHit = false;
        }

        internal void LoadContent(ContentManager content)
        {
            paddleTexture = content.Load<Texture2D>("Paddle");
        }

        internal void Update(GameTime gameTime)
        {
            paddlePosition += Speed * Direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (paddlePosition.Y >= gameArea.Bottom - paddleTexture.Height)
            {
                paddlePosition.Y = gameArea.Bottom - paddleTexture.Height;
            }
            if (paddlePosition.Y <= gameArea.Top)
            {
                paddlePosition.Y = gameArea.Top;
            }

            //change paddle's color for 500 milliseconds when they're touched by the ball
            if (IsHit)
            {
                paddleColor = Color.MediumVioletRed;
                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (elapsedTime > 500)
                {
                    paddleColor = Color.White;
                    elapsedTime = 0;
                    IsHit = false;
                }
            }
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(paddleTexture, paddlePosition, null, paddleColor, 0, Vector2.Zero, 1, SpriteEffects.None, 0f);
        }
    }
}
