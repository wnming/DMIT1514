using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerGame
{
    /// <summary>
    /// Represents a cel animated texture.
    /// </summary>
    public class CelAnimationSequence
    {
        // The texture containing the animation sequence...
        protected Texture2D texture;

        // The length of time a cel is displayed...
        protected float celTime;

        // Sequence metrics
        protected int celWidth;
        protected int celHeight;

        // Calculated count of cels in the sequence
        protected int celCount;

        private int celRow;
        private int celColumn;

        /// <summary>
        /// Constructs a new CelAnimationSequence.
        /// </summary>        
        public CelAnimationSequence(Texture2D texture, int celHeight, int celWidth, float celTime, int celCount, int celRow)
        {
            this.texture = texture;
            this.celWidth = celWidth;
            this.celTime = celTime;

            //get value from parameter except for the celColumn
            this.celHeight = celHeight;
            //celCount needs to be passed because there are 5 frames with 2 rows if we use celColumn(3) * celRow(2) = 6, and there will be a gap when playing
            this.celCount = celCount;
            this.celColumn = Texture.Width / celWidth;
            this.celRow = celRow;
        }

        /// <summary>
        /// All frames in the animation arranged horizontally.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
        }

        /// <summary>
        /// Duration of time to show each cel.
        /// </summary>
        public float CelTime
        {
            get { return celTime; }
        }

        /// <summary>
        /// Gets the number of cels in the animation.
        /// </summary>
        public int CelCount
        {
            get { return celCount; }
        }

        /// <summary>
        /// Gets the width of a frame in the animation.
        /// </summary>
        public int CelWidth
        {
            get { return celWidth; }
        }

        /// <summary>
        /// Gets the height of a frame in the animation.
        /// </summary>
        public int CelHeight
        {
            get { return celHeight; }
        }

        /// <summary>
        /// Gets the row of a frame in the animation.
        /// </summary>
        public int CelRow
        {
            get { return celRow; }
        }


        /// <summary>
        /// Gets the column of a frame in the animation.
        /// </summary>
        public int CelColumn
        {
            get { return celColumn; }
        }
    }
}
