using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
    public class ColliderRight : PlatformCollider
    {
        public ColliderRight(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {
        }

        internal void CheckCollision(Player player)
        {
            if (ColliderBox.Intersects(player.RectangleBounds) && player.Velocity.X < 0)
            {
                player.MoveLeftRight(0);
            }
        }
    }
}
