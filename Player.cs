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
        Vector2 gravity;
        Vector2 acceleration;

        public Player(Vector2 initPos, AnimationTable initAnimationTable, Controller pController, ref CollisionManager pCollisionManager)
            : base(initPos, initAnimationTable, ref pCollisionManager)
        {
            position = initPos;
            controller = pController;
            gravity = new Vector2(0, 9.8f);
        }

        public override void update(GameTime gametime)
        {
            if (controller.keyHeld(Keys.Left))
            {
                flipHorizontally = SpriteEffects.FlipHorizontally;
                velocity.X -= 2f;
            }
            if (controller.keyHeld(Keys.Right))
            {
                velocity.X += 2f;
                flipHorizontally = SpriteEffects.None;
            }

            velocity += gravity;
            position += velocity;
            velocity = Vector2.Zero;
        }
    }
}
