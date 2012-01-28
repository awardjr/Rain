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
        Vector2 velocity;
        Boolean Jumpin;
    //    Vector2 gravity;

        public Player(Vector2 initPos, AnimationTable initAnimationTable, Controller pController)
            : base(initPos, initAnimationTable, ObjectType.Player)
        {
            position = initPos;
            controller = pController;
            Jumpin = false;
         //   gravity = new Vector2(0, 9.8f);
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

            if (controller.keyPressed(Keys.Space) && Jumpin == false)
            {
                Jumpin = true;
                velocity.Y = -30;
            }

         //   velocity += gravity;
            position += velocity;
            velocity.X = 0;

            if (velocity.Y < 0)
            {
                velocity.Y += 1;
            }
            else if (Jumpin == true && position.Y >= 500)
            {
                Jumpin = false;
            }

            if (position.Y < 500)
            {
                position.Y += 1;
            }
            
        }
    }
}
