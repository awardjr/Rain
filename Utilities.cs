using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Rain
{
    class Utilities
    {
        public static bool intersect(Vector2 pos1, float w1, float h1, Vector2 pos2, float w2, float h2)
        {
            return !(pos1.X > pos2.X + w2 || pos1.X + w1 < pos2.X || pos1.Y > pos2.Y + h2 || pos1.Y + h1 < pos2.Y);
        }

        public static bool mouseIntersect(MouseState mState, Vector2 pos, float w, float h, Camera camera)
        {
            float scale = camera.Scale;
            float mX = mState.X + camera.Position.X * scale;
            float mY = mState.Y + camera.Position.Y * scale;

            return (mX >= pos.X * scale) && (mX <= (pos.X + w) * scale) && (mY >= pos.Y * scale) && (mY <= (pos.Y + h) * scale);
        }
    }
}