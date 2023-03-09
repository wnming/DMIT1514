using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerBullet : GameObject
{
    public ProjectileState playerBulletState { get; set; }

    public PlayerBullet(Sprite sprite, Transform transform) : base(sprite, transform)
    {
        this.sprite = sprite;
        this.transform = transform;
    }
    public new void Update(GameTime gameTime)
    {
    }
}
