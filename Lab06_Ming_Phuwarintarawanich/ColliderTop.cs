using Microsoft.Xna.Framework;
using PlatformerGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGame
{
    public class ColliderTop : PlatformCollider
    {
        public ColliderTop(Vector2 position, Vector2 dimensions) : base(position, dimensions)
        {
        }

        internal void CheckCollision(Player player)
        {
            if (ColliderBox.Intersects(player.RectangleBounds))
            {
                player.Ground(ColliderBox);
                player.Stand();
            }
        }
    }
}
