using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlatformerGame.Player;

namespace PlatformerGame
{
    public class CollectableItem : GameObject
    {
        CelAnimationPlayer celPlayer;
        CelAnimationSequence celAnimationSequence;

        protected ItemState itemState = ItemState.NotCollect;

        public CollectableItem(Game game, Transform transform, Rectangle rectangle, Texture2D texture) : base(game, transform, rectangle, texture)
        {
            _rectangleBounds.Location = transform.Position.ToPoint();
            celPlayer = new CelAnimationPlayer();
            celAnimationSequence = new CelAnimationSequence(Texture, 40, 0.5f);
            celPlayer.Play(celAnimationSequence);
        }

        public void Update(GameTime gameTime)
        {
            celPlayer.Update(gameTime);
        }

        public bool CheckItemCollistion(Rectangle playerBound)
        {
            bool isCollide = false;
            if(itemState == ItemState.NotCollect && _rectangleBounds.Intersects(playerBound))
            {
                Collide();
                isCollide = true;
            }
            return isCollide;
        }

        public void Collide()
        {
            if(itemState == ItemState.NotCollect)
            {
                itemState = ItemState.Collect;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            switch (itemState)
            {
                case ItemState.NotCollect:
                    spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                    celPlayer.Draw(spriteBatch, _transform.Position, SpriteEffects.None);
                    spriteBatch.End();
                    break;
                case ItemState.Collect:
                    break;
                default:
                    break;
            }
        }

        public enum ItemState
        {
            NotCollect,
            Collect
        }
    }
}
