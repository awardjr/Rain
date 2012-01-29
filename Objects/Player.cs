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
    class Player : GameObject
    {
        enum PlayerState { Stand, Walking, Jumping };
        Controller controller;
        Vector2 velocity;
        Vector2 acceleration;
        float accel;
        float decel;
        float maxSpeed;
        PlayerState state;
        float drops;
        
        public Player(Vector2 initPos, AnimationTable initAnimationTable, Controller pController)
            : base(initPos, initAnimationTable, ObjectType.Player)
        {
            position = initPos;
            controller = pController;
            scale = 0.1f;
            drops = 0;
            accel = 0.04f;
            decel = 0.55f;
            maxSpeed = 1;
        }

        public void addDrop()
        {
            drops+=5;
        }

        public void addAcid()
        {
            drops -= 10;
        }

        public override void update(GameTime gametime)
        {
            scale = (drops * 0.01f) + 0.1f;

            if (controller.keyHeld(Keys.Left))
            {
                drops-=0.09f;
                flipHorizontally = SpriteEffects.FlipHorizontally;
                acceleration.X -= accel;
            }
            if (controller.keyHeld(Keys.Right))
            {
                drops-=0.09f;
                flipHorizontally = SpriteEffects.None;
                acceleration.X += accel;
            }

            if (controller.keyHeld(Keys.Down))
            {
                acceleration.Y += accel;
            }

            if (controller.keyHeld(Keys.Up))
            {
                acceleration.Y -= accel;
            }


            if (velocity.Length() > 0)
                state = PlayerState.Walking;
            if (velocity.Length() == 0)
                state = PlayerState.Stand;

            if (state == PlayerState.Walking)
                this.setAnimation("moving");
            if (state == PlayerState.Stand)
                this.setAnimation("stand");
            acceleration.Y += 0.0002f;
            position += acceleration;
            drops = MathHelper.Clamp(drops, 0, 500);
            velocity.X = MathHelper.Clamp(velocity.X, -maxSpeed, maxSpeed);
            velocity.Y = MathHelper.Clamp(velocity.Y, -maxSpeed, maxSpeed);
            if ((position.X - Width / 2) <= 0)
            {
                position.X += (Math.Abs(position.X-Width / 2) + 1);
                velocity.X = 0;
                acceleration.X = 0;
            }
            if ((position.X+Width/2) >= 480)
            {
                position.X -= ((position.X + Width/2 - 480) + 1);
                velocity.X = 0;
                acceleration.X = 0;
            }
            

          /*  if((Position.Y-Height/2)<= 30)
            {
                position.Y += (Math.Abs(30 - (position.Y-Height / 2)) + 1);
                velocity.Y = 0;
                acceleration.Y = 0;
            }
             if ((position.Y+Height/2) >= 700)
            {
                position.Y -= ((position.Y + Height/2 - 700) + 1);
                velocity.Y = 0;
                acceleration.Y = 0;
            }
            */
        }
    }
}
