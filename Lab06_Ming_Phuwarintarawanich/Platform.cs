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

        protected ColliderLeft colliderLeft;
        protected ColliderRight colliderRight;
        protected ColliderTop colliderTop;
        protected ColliderBottom colliderBottom;

        public Platform(Vector2 position, Vector2 dimensions)
        {
            colliderLeft = new ColliderLeft(
                                new Vector2(position.X, position.Y + 3),
                                new Vector2(10, dimensions.Y + 5));
            colliderRight = new ColliderRight(
                                new Vector2(position.X + dimensions.X - 9, position.Y + 3),
                                new Vector2(10, dimensions.Y + 5));
            colliderTop = new ColliderTop( 
                                new Vector2(position.X + 3, position.Y),
                                new Vector2(dimensions.X - 6, 10));
            colliderBottom = new ColliderBottom(
                                new Vector2(position.X + 4, position.Y + dimensions.Y),
                                new Vector2(dimensions.X - 6, 10));
        }

        internal void LoadContent(ContentManager Content)
        {
            colliderLeft.LoadContent(Content);
            colliderRight.LoadContent(Content);
            colliderTop.LoadContent(Content);
            colliderBottom.LoadContent(Content);
        }

        internal void IsCollide(Player player)
        {
            colliderLeft.CheckCollision(player);
            colliderRight.CheckCollision(player);
            colliderTop.CheckCollision(player);
            colliderBottom.CheckCollision(player);
        }

        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            colliderLeft.Draw(spriteBatch, color);
            colliderRight.Draw(spriteBatch, color);
            colliderTop.Draw(spriteBatch, color);
            colliderBottom.Draw(spriteBatch, color);
        }
    }
}
