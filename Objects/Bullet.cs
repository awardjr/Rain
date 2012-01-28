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

namespace Rain.Objects
{
   

    class Bullet : GameObject
    {
        float angle;
        float speed;
        float deceleration;
        Vector2 velocity;

        public Bullet(Vector2 initPos, AnimationTable initAnimationTable, float pSpeed = 10f, float pAngle = 0f, float pDeceleration = 0f)
            : base(initPos, initAnimationTable)
        {
            position = initPos;
            speed = pSpeed;
            angle = pAngle;
            deceleration = pDeceleration;
            velocity = new Vector2((float)(Math.Cos(angle) * speed), (float)(Math.Sin(angle) * speed)); 
        }

        public override void update(GameTime gametime)
        {
            position += velocity;
            velocity.X -= deceleration;
            velocity.Y -= deceleration;

        }
    }
}
