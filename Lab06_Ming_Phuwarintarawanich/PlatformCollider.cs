using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
    public class PlatformCollider
    {
        public PlatformColliderType colliderType;
        protected Texture2D colliderTexture;
        protected Vector2 position;
        protected Vector2 dimensions;
        internal Rectangle BoundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        public PlatformCollider(PlatformColliderType colliderType,Vector2 position, Vector2 dimensions)
        {
            this.colliderType = colliderType;
            this.position = position;
            this.dimensions = dimensions;
        }

        internal void LoadContent(ContentManager Content)
        {
            colliderTexture = Content.Load<Texture2D>(colliderType.ToString());
        }
        internal void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(colliderTexture, BoundingBox, new Rectangle(0, 0, 1, 1), Color.White);
        }

        internal bool ProcessCollisions(Player player)
        {
            bool didCollide = false;
            if (BoundingBox.Intersects(player.BoundingBox))
            {
                didCollide = true;
                switch (colliderType)
                {
                    case PlatformColliderType.Left:
                        //if the player is moving rightwards
                        if (player.Velocity.X > 0)
                        {
                            player.MoveLeftRight(0);
                        }
                        break;
                    case PlatformColliderType.Right:
                        //if the player is moving leftwards
                        if (player.Velocity.X < 0)
                        {
                            player.MoveLeftRight(0);
                        }
                        break;
                    case PlatformColliderType.Top:
                        player.Land(BoundingBox);
                        player.StandOn(BoundingBox);
                        break;
                    case PlatformColliderType.Bottom:
                        if (player.Velocity.Y < 0)
                        {
                            player.MoveUpDown(0);
                        }
                        break;
                }
            }

            return didCollide;
        }

        public enum PlatformColliderType
        {
            Left,
            Right,
            Bottom,
            Top
        }
    }
}
