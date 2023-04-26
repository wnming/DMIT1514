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
            colliderTexture = Content.Load<Texture2D>("platform");
        }
        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(colliderTexture, BoundingBox, new Rectangle(0, 0, 1, 1), color);
        }

        internal void IsCollide(Player player)
        {
            if (BoundingBox.Intersects(player.RectangleBounds))
            {
                switch (colliderType)
                {
                    case PlatformColliderType.Left:
                        if (player.Velocity.X > 0)
                        {
                            player.MoveLeftRight(0);
                        }
                        break;
                    case PlatformColliderType.Right:
                        if (player.Velocity.X < 0)
                        {
                            player.MoveLeftRight(0);
                        }
                        break;
                    case PlatformColliderType.Top:
                        player.Ground(BoundingBox);
                        player.Stand();
                        break;
                    case PlatformColliderType.Bottom:
                        if (player.Velocity.Y < 0)
                        {
                            player.MoveUpDown(0);
                        }
                        break;
                }
            }
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
