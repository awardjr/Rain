using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using SpriteSheetRuntime;

namespace Rain
{
    class FogObject : GameObject
    {
       
        public FogObject(Vector2 initPos, AnimationTable initAnimationTable, ref CollisionManager pCollisionManager)
            : base(initPos, initAnimationTable, ref pCollisionManager)
        {
            position = initPos;
           
        }

        public override void update(GameTime gametime)
        {
           
        }
    }
}
