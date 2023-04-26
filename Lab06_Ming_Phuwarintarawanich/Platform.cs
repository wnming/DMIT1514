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
                                    new Vector2(position.X, position.Y + 3),
                                    new Vector2(10, dimensions.Y + 5)
                                    );
            right = new PlatformCollider(
                                    PlatformCollider.PlatformColliderType.Right,
                                    new Vector2(position.X + dimensions.X - 9, position.Y + 3),
                                    new Vector2(10, dimensions.Y + 5)
                                    );
            top = new PlatformCollider(
                                    PlatformCollider.PlatformColliderType.Top,
                                    new Vector2(position.X + 3, position.Y),
                                    new Vector2(dimensions.X - 6, 10)
                                    );
            bottom = new PlatformCollider(
                                    PlatformCollider.PlatformColliderType.Bottom,
                                    new Vector2(position.X + 4, position.Y + dimensions.Y),
                                    new Vector2(dimensions.X - 6, 10)
                                    );
        }

        internal void LoadContent(ContentManager Content)
        {
            left.LoadContent(Content);
            right.LoadContent(Content);
            top.LoadContent(Content);
            bottom.LoadContent(Content);
        }

        internal void IsCollide(Player player)
        {
            left.IsCollide(player);
            right.IsCollide(player);
            top.IsCollide(player);
            bottom.IsCollide(player);
        }

        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            left.Draw(spriteBatch, color);
            right.Draw(spriteBatch, color);
            top.Draw(spriteBatch, color);
            bottom.Draw(spriteBatch, color);
        }
    }
}
