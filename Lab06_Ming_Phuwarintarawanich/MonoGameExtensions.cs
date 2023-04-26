using Microsoft.Xna.Framework;

namespace MonoGameExtensions
{
    /// <summary>
    /// Exentsion methods for the struct. Most of these are stuff we've done in class, but they shouldn't need to be in the actual class definition.
    /// </summary>
    internal static class TransformExtensions
    {
        /// <summary>
        /// Returns the top-left corner of the transform, using half the reference size.
        /// </summary>
        /// <param name="referenceSize"></param>
        /// <returns></returns>
        internal static Point Location(this Vector2 _position, Point referenceSize)
        {
            int x = (int)_position.X - referenceSize.X / 2;
            int y = (int)_position.Y - referenceSize.Y / 2;
            return new Point(x, y);
        }
        /// <summary>
        /// Returns the angle from the transform's current position to the target.
        /// </summary>
        /// <param name="targetPosition"></param>
        /// <returns>The angle pointing from the position to the target.</returns>
        internal static float LookTowards(this Vector2 _position, Vector2 targetPosition)
        {
            Vector2 dir = (targetPosition - _position);
            dir.Normalize();
            return (float)System.Math.Atan2((double)dir.Y, (double)dir.X) + MathHelper.PiOver2;
        }

    }
}
