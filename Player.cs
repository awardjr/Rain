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
    class Player : GameObject
    {
        Controller controller;
        CollisionManager collisonManager;
        Vector2 velocity;
        
        public Player(Vector2 initPos, AnimationTable initAnimationTable, Controller pController, ref CollisionManager pCollisionManager)
            : base(initPos, initAnimationTable, ref pCollisionManager)
        {
            position = initPos;
            controller = pController;
        }

        public override void update(GameTime gametime)
        {
            if (controller.keyHeld(Keys.Up))
            {
                rotation = MathHelper.ToRadians(270);
                velocity.Y -=2f;
            }
            if (controller.keyHeld(Keys.Down))
            {
                rotation = MathHelper.ToRadians(90);
                velocity.Y += 2f;
            }
            if (controller.keyHeld(Keys.Left))
            {
                rotation = MathHelper.ToRadians(180);
                velocity.X -= 2f;
            }
            if (controller.keyHeld(Keys.Right))
            {
                rotation = MathHelper.ToRadians(0);
                velocity.X += 2f;
            }

            position += velocity;
            rotation = (float)Math.Atan2(velocity.Y, velocity.X);
            velocity = Vector2.Zero;
        }
    }
}
