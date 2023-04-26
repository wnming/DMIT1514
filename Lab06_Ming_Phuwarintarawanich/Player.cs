using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PlatformerGame
{
    internal class Player : GameObject
    {
        protected Vector2 velocity;
        internal Vector2 Velocity { get => velocity; }

        private const int PlayerJumpForce = -200;
        private const int PlayerSpeed = 120;

        CelAnimationPlayer celPlayer;
        CelAnimationSet animationSet;

        internal PlayerState currentState = PlayerState.idle;
        PlayerDirection playerDirection = PlayerDirection.right;

        public Player(Transform transform, Game game, Rectangle rectangle, Texture2D idleTexture, CelAnimationSet animationSet) : base(game, transform, rectangle, idleTexture)
        {
            this.animationSet = animationSet;
            celPlayer = new CelAnimationPlayer();
            celPlayer.Play(animationSet.Idle);
        }

        internal void Update(GameTime gameTime)
        {
            _rectangleBounds.Location = _transform.Position.ToPoint();
            velocity.Y += PlatformerGame.Gravity;
            _transform.MovePosition(Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (Math.Abs(velocity.Y) > PlatformerGame.Gravity)
            {
                currentState = PlayerState.jump;
                celPlayer.Play(animationSet.Jump);
            }

            playerDirection = velocity.X < 0 ? PlayerDirection.right : PlayerDirection.left;
            celPlayer.Update(gameTime);
        }

        internal void MoveLeftRight(float direction)
        {
            velocity.X = direction * PlayerSpeed;
            if (currentState != PlayerState.jump)
            {
                celPlayer.Play(animationSet.Run);
                currentState = PlayerState.walk;
            }
        }

        internal void MoveUpDown(float direction)
        {
            velocity.Y = direction * PlayerSpeed;
        }

        internal void Ground(Rectangle collider)
        {
            if (velocity.Y > 0)
            {
                _transform.SetPosition(_transform.Position.X, collider.Top - _rectangleBounds.Height + 1);
                velocity.Y = 0;
                currentState = PlayerState.walk;
            }
        }
        internal void Stand()
        {
            velocity.Y -= PlatformerGame.Gravity;
        }

        internal void StopMoving()
        {
            if (currentState == PlayerState.walk)
            {
                velocity = Vector2.Zero;
                currentState = PlayerState.idle;
                celPlayer.Play(animationSet.Idle);
            }
        }
        internal void Jump()
        {
            velocity.Y = currentState != PlayerState.jump ? PlayerJumpForce : 0;
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            celPlayer.Draw(spriteBatch, _transform.Position, (SpriteEffects)playerDirection);
            spriteBatch.End();
        }

        internal enum PlayerState
        {
            idle,
            walk,
            jump
        }
        internal enum PlayerDirection
        {
            left,
            right
        }
    }
}
