using Microsoft.Xna.Framework;
using System;

namespace PlatformerGame
{
    public struct Transform
    {
        public Vector2 _position;
        public float _rotation;
        public float _scale;

        public Transform(Vector2 position, float rotation, float scale)
        {
            _position = position;
            _rotation = rotation;
            _scale = scale;
        }

        public void SetPosition(Vector2 newPosition)
        {
            _position = newPosition;
        }

        public Vector2 MovePosition(Vector2 posOffset)
        {
            SetPosition(_position + posOffset);
            return _position + posOffset;
        }

        public void PointTowards(Vector2 target)
        {
            Vector2 dir = (target - _position);
            dir.Normalize();
            _rotation = (float)Math.Atan2((double)dir.Y, (double)dir.X) + MathHelper.PiOver2;
        }

        public void SetScale(float newScale)
        {
            _scale = newScale;
        }


        public Point ToPoint()
        {
            return new Point((int)_position.X, (int)_position.Y);
        }
    }
}
