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
        PlayerState state;
        int drops;
        
        public Player(Vector2 initPos, AnimationTable initAnimationTable, Controller pController)
            : base(initPos, initAnimationTable, ObjectType.Player)
        {
            position = initPos;
            controller = pController;
            scale = 0.2f;
            drops = 0;
        }

        public void addDrop()
        {
            drops+=2;
        }

        public override void update(GameTime gametime)
        {
            scale = (drops * 0.01f) + 0.2f;
            
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
            if (velocity.Length() > 0)
                state = PlayerState.Walking;
            if (velocity.Length() == 0)
                state = PlayerState.Stand;

            if(state == PlayerState.Walking)
                this.setAnimation("moving");
            if(state == PlayerState.Stand)
                this.setAnimation("stand");

            if (controller.keyPressed(Keys.Space) && Jumpin == false)
            {
                Jumpin = true;
                velocity.Y = -30;
            }

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
