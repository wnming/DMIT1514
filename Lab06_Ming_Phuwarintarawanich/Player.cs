using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
    public class Player
    {
        protected Vector2 position;
        protected Vector2 dimensions;
        protected Rectangle gameArea;

        protected Vector2 velocity;
        internal Vector2 Velocity { get => velocity; }

        private Texture2D walkingManTexture;
        Dictionary<string, Rectangle[]> walkingManRectangle = new Dictionary<string, Rectangle[]>();

        private const int PlayerJumpForce = -300;
        private const int PlayerSpeed = 150;
        private const int SpriteColumn = 4;

        private PlayerState currentState = PlayerState.left;
        private int frame = 1;

        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        public Player(Vector2 position, Rectangle gameArea)
        {
            this.position = position;
            this.gameArea = gameArea;
            this.dimensions = new Vector2(46, 40);
        }

        internal void LoadContent(ContentManager Content)
        {
            walkingManTexture = Content.Load<Texture2D>("walking_man");

            walkingManRectangle["right"] = new Rectangle[4] { new Rectangle(0, 458, 102, 153), new Rectangle(102, 458, 102, 153), new Rectangle(204, 458, 102, 153), new Rectangle(306, 458, 102, 153) };
            walkingManRectangle["left"] = new Rectangle[4] { new Rectangle(0, 305, 102, 153), new Rectangle(102, 305, 102, 153), new Rectangle(204, 305, 102, 153), new Rectangle(306, 305, 102, 153) };
            //walkingManRectangle["up"] = new Rectangle[4] { new Rectangle(0, 153, 102, 153), new Rectangle(102, 153, 102, 153), new Rectangle(204, 153, 102, 153), new Rectangle(306, 153, 102, 153) };
            walkingManRectangle["up"] = new Rectangle[4] { new Rectangle(0, 0, 102, 153), new Rectangle(102, 0, 102, 153), new Rectangle(204, 0, 102, 153), new Rectangle(306, 0, 102, 153) };
        }

        internal void Update(GameTime gameTime)
        {
            velocity.Y += PlatformerGame.Gravity;

            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (Math.Abs(velocity.Y) > PlatformerGame.Gravity)
            {
                currentState = PlayerState.up;
            }

            switch (currentState)
            {
                case PlayerState.left:
                    break;
                case PlayerState.right:
                    break;
                case PlayerState.up:
                    break;
                default:
                    break;
            }
        }

        internal void MoveLeftRight(float direction)
        {
            float oldXDirection = velocity.X;
            velocity.X = direction * PlayerSpeed;
            if (velocity.X > 0)
            {
                frame++;
                if (frame == SpriteColumn)
                {
                    frame = 0;
                }
                currentState = PlayerState.right;
            }
            else if (velocity.X < 0)
            {
                frame++;
                if (frame == SpriteColumn)
                {
                    frame = 0;
                }
                currentState = PlayerState.left;
            }
            //if (state != State.Jumping)
            //{
            //    animationPlayer.Play(walkSequence);
            //    state = State.Walking;
            //}
        }
        internal void MoveUpDown(float direction)
        {
            velocity.Y = direction * PlayerSpeed;
        }

        internal void Land(Rectangle whatILandedOn)
        {
            if (currentState == PlayerState.up)
            {
                position.Y = whatILandedOn.Top - dimensions.Y + 1;
                velocity.Y = 0;
            }
        }
        internal void StandOn(Rectangle whatImStandingOn)
        {
            velocity.Y -= PlatformerGame.Gravity;
        }
        internal void Stop()
        {
            if (currentState != PlayerState.up)
            {
                velocity = Vector2.Zero;
            }
            //if (state == State.Walking)
            //{
            //    velocity = Vector2.Zero;
            //    state = State.Idle;
            //    animationPlayer.Play(idleSequence);
            //}
        }
        internal void Jump()
        {
            velocity.Y = currentState == PlayerState.up ? PlayerJumpForce : 0;
        }

        internal void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(walkingManTexture, position, walkingManRectangle[currentState.ToString()][frame], Color.White);
        }

        protected enum PlayerState
        {
            left,
            right,
            up
        }
    }
}
