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
        int drops;
        
        public Player(Vector2 initPos, AnimationTable initAnimationTable, Controller pController)
            : base(initPos, initAnimationTable, ObjectType.Player)
        {
            position = initPos;
            controller = pController;
            scale = 0.1f;
            drops = 0;
            accel = 0.1f;
            decel = 0.01f;
            rotation = 1f;
            maxSpeed = 1;
        }

        public void addDrop()
        {
            drops+=2;
        }

        public override void update(GameTime gametime)
        {
            scale = (drops * 0.01f) + 0.1f;
            
            if (controller.keyHeld(Keys.Left))
            {
                flipHorizontally = SpriteEffects.FlipHorizontally;
                acceleration.X -= accel;
            }
            if (controller.keyHeld(Keys.Right))
            {
                flipHorizontally = SpriteEffects.None;
                acceleration.X += accel;
            }

          
            if (velocity.Length() > 0)
                state = PlayerState.Walking;
            if (velocity.Length() == 0)
                state = PlayerState.Stand;

            if(state == PlayerState.Walking)
                this.setAnimation("moving");
            if(state == PlayerState.Stand)
                this.setAnimation("stand");

            velocity += acceleration;
            position += velocity;
            velocity.X = MathHelper.Clamp(velocity.X, -maxSpeed, maxSpeed);
            if (position.X < 0)
                position.X = 0;
            if (position.X > 480)
                position.X = 480;
            }
    }
}
