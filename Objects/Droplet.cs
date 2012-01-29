using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rain.Objects
{
    class Droplet : GameObject
    {
        public Droplet(Vector2 initPos, AnimationTable initAnimationTable)
            : base(initPos, initAnimationTable, ObjectType.Drops) { scale = 0.5f; }
    }
}
