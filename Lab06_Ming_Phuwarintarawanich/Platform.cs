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

        protected List<PlatformCollider> platformColliders = new();

        public Platform(Vector2 position, Vector2 dimensions)
        {
            platformColliders.Add(new PlatformCollider(
                            PlatformCollider.PlatformColliderType.Left,
                            new Vector2(position.X, position.Y + 3),
                            new Vector2(10, dimensions.Y + 5)));
            platformColliders.Add(new PlatformCollider(
                            PlatformCollider.PlatformColliderType.Right,
                            new Vector2(position.X + dimensions.X - 9, position.Y + 3),
                            new Vector2(10, dimensions.Y + 5)));
            platformColliders.Add(new PlatformCollider(
                            PlatformCollider.PlatformColliderType.Top,
                            new Vector2(position.X + 3, position.Y),
                            new Vector2(dimensions.X - 6, 10)));
            platformColliders.Add(new PlatformCollider(
                            PlatformCollider.PlatformColliderType.Bottom,
                            new Vector2(position.X + 4, position.Y + dimensions.Y),
                            new Vector2(dimensions.X - 6, 10)
                            ));
        }

        internal void LoadContent(ContentManager Content)
        {
            platformColliders[(int)PlatformCollider.PlatformColliderType.Left].LoadContent(Content);
            platformColliders[(int)PlatformCollider.PlatformColliderType.Right].LoadContent(Content);
            platformColliders[(int)PlatformCollider.PlatformColliderType.Top].LoadContent(Content);
            platformColliders[(int)PlatformCollider.PlatformColliderType.Bottom].LoadContent(Content);
        }

        internal void IsCollide(Player player)
        {
            for(int index = 0; index < platformColliders.Count; index++)
            {
                platformColliders[index].CheckCollision(player);
            }
        }

        internal void Draw(SpriteBatch spriteBatch, Color color)
        {
            for (int index = 0; index < platformColliders.Count; index++)
            {
                platformColliders[index].Draw(spriteBatch, color);
            }
        }
    }
}
