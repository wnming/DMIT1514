using System;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Lesson05_Animations
{
    /// <summary>
    /// Controls playback of a CelAnimationSequence.
    /// </summary>
    public class CelAnimationPlayer
    {
        private CelAnimationSequence celAnimationSequence;
        private int celIndex;
        private float celTimeElapsed;
        private Rectangle celSourceRectangle;

        private int currentCelIndex = 0;

        /// <summary>
        /// Begins or continues playback of a CelAnimationSequence.
        /// </summary>
        public void Play(CelAnimationSequence celAnimationSequence)
        {
            if (celAnimationSequence == null)
            {
                throw new Exception("CelAnimationPlayer.PlayAnimation received null CelAnimationSequence");
            }
            // If this animation is already running, do not restart it...
            if (celAnimationSequence != this.celAnimationSequence)
            {
                this.celAnimationSequence = celAnimationSequence;
                celIndex = 0;
                celTimeElapsed = 0.0f;

                celSourceRectangle.X = 0;
                celSourceRectangle.Y = 0;
                celSourceRectangle.Width = this.celAnimationSequence.CelWidth;
                celSourceRectangle.Height = this.celAnimationSequence.CelHeight;
            }
        }

        /// <summary>
        /// Update the state of the CelAnimationPlayer.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (celAnimationSequence != null)
            {
                celTimeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (celTimeElapsed >= celAnimationSequence.CelTime)
                {
                    celTimeElapsed -= celAnimationSequence.CelTime;

                    // Advance the frame index looping as appropriate...
                    celIndex = (celIndex + 1) % celAnimationSequence.CelCount;
                    currentCelIndex = celIndex;

                    //if more than 1 row
                    if(celIndex > celAnimationSequence.CelCount / celAnimationSequence.CelRow)
                    {
                        //set celSourceRectangle.Y to Y point of that frame
                        celSourceRectangle.Y = celSourceRectangle.Width * ((celIndex - 1) / celAnimationSequence.CelRow);
                        //update  the current celIndex so celSourceRectangle.X will have the correct value
                        currentCelIndex = (celIndex - celAnimationSequence.CelColumn) % celAnimationSequence.CelCount;
                    }
                    else
                    {
                        //set Y = 0 for the first row
                        celSourceRectangle.Y = 0;
                    }

                    celSourceRectangle.X = currentCelIndex * celSourceRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Draws the current cel of the animation.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (celAnimationSequence != null)
            {
                spriteBatch.Draw(celAnimationSequence.Texture, position, celSourceRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, spriteEffects, 0.0f);
            }
        }
    }
}
