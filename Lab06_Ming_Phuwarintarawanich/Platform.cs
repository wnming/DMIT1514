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
    public class Platform
    {
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 dimensions;

        protected PlatformCollider left;
        protected PlatformCollider right;
        protected PlatformCollider top;
        protected PlatformCollider bottom;

        public Platform(Vector2 position, Vector2 dimensions)
        {
            left = new PlatformCollider(
                                        PlatformCollider.PlatformColliderType.Left,
                                        new Vector2(position.X + 3, position.Y),
                                        new Vector2(dimensions.X - 6, 1)
                                        );
            right = new PlatformCollider(
                                        PlatformCollider.PlatformColliderType.Right,
                                        new Vector2(position.X + dimensions.X - 1, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2)
                                        );
            top = new PlatformCollider(
                                        PlatformCollider.PlatformColliderType.Top,
                                        new Vector2(position.X + 3, position.Y + dimensions.Y),
                                        new Vector2(dimensions.X - 6, 1)
                                        );
            bottom = new PlatformCollider(
                                        PlatformCollider.PlatformColliderType.Bottom,
                                        new Vector2(position.X, position.Y + 1),
                                        new Vector2(1, dimensions.Y - 2)
                                        );
        }

        internal void LoadContent(ContentManager Content)
        {
            left.LoadContent(Content);
            right.LoadContent(Content);
            top.LoadContent(Content);
            bottom.LoadContent(Content);
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            left.Draw(spriteBatch);
            right.Draw(spriteBatch);
            top.Draw(spriteBatch);
            bottom.Draw(spriteBatch);
        }

        internal void ProcessCollisions(Player player)
        {
            left.ProcessCollisions(player);
            right.ProcessCollisions(player);
            top.ProcessCollisions(player);
            bottom.ProcessCollisions(player);
        }
    }
}
