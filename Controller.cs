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

namespace Rain
{
    class Controller
    {
        bool enabled;
        KeyboardState lastKeyboardState, kState;
        MouseState lastMouseState, mState;

        public Controller()
        {
            enabled = true;
            lastKeyboardState = Keyboard.GetState();
            lastMouseState = Mouse.GetState();
            kState = lastKeyboardState;
            mState = lastMouseState;

        }

        //====================================================================================================

        public bool keyPressed(Keys key)
        {
            return enabled && !lastKeyboardState.IsKeyDown(key) && kState.IsKeyDown(key);
        }

        public bool keyHeld(Keys key)
        {
            return enabled && lastKeyboardState.IsKeyDown(key) && kState.IsKeyDown(key);
        }

        //====================================================================================================

        public bool LeftClick
        {
            get { return enabled && lastMouseState.LeftButton != ButtonState.Pressed && mState.LeftButton == ButtonState.Pressed; }
        }

        public bool LeftClickHeld
        {
            get { return enabled && lastMouseState.LeftButton == ButtonState.Pressed && mState.LeftButton == ButtonState.Pressed; }
        }

        //====================================================================================================

        public bool RightClick
        {
            get { return enabled && lastMouseState.RightButton != ButtonState.Pressed && mState.RightButton == ButtonState.Pressed; }
        }

        public bool RightClickHeld
        {
            get { return enabled && lastMouseState.RightButton == ButtonState.Pressed && mState.RightButton == ButtonState.Pressed; }
        }

        //====================================================================================================

        public bool MiddleClick
        {
            get { return enabled && lastMouseState.MiddleButton != ButtonState.Pressed && mState.RightButton == ButtonState.Pressed; }
        }

        public bool MiddleClickHeld
        {
            get { return enabled && lastMouseState.RightButton == ButtonState.Pressed && mState.MiddleButton == ButtonState.Pressed; }
        }

        //====================================================================================================           

        public KeyboardState KeyboardState
        {
            get { return Keyboard.GetState(); }
        }

        public MouseState MouseState
        {
            get { return mState; }
        }

        public Vector2 mousePosition
        {
            get { return new Vector2(mState.X, mState.Y); }
        }



        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        //====================================================================================================

        public void update()
        {
            lastKeyboardState = kState;
            lastMouseState = mState;
            kState = Keyboard.GetState();
            mState = Mouse.GetState();
        }
    }
}
