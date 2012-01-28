using System;
using System.Collections.Generic;
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
   

        public class AnimationTable
        {
            SpriteSheet spriteSheet;
            SortedDictionary<string, Animation> animations;
            string current;

            public AnimationTable(SpriteSheet spriteSheet, SortedDictionary<string, Animation> animations, string current)
            {
                this.spriteSheet = spriteSheet;
                this.animations = animations;
                this.current = current;
           
            }

            public AnimationTable(AnimationTable table)
            {
                this.animations = new SortedDictionary<string, Animation>();
                this.spriteSheet = table.spriteSheet;
                this.current =table.current;
               
                foreach(KeyValuePair<String, Animation> kvp in table.animations)
                {
                    this.animations.Add(kvp.Key, new Animation(kvp.Value.Indices, kvp.Value.AnimationInterval));
                }
            }

            public AnimationTable(SpriteSheet spriteSheet)
            {
                this.spriteSheet = spriteSheet;
                this.animations = new SortedDictionary<string, Animation>();
            }

            
            // Add animation
            public void addAnimation(string name, Animation animation)
            {
                if (!animations.ContainsKey(name))
                {
                    animations.Add(name, animation);
                    if (animations.Count == 1)
                        current = name;
                }
            }
            // Change to specified animation if valid
            public bool setAnimation(string animation)
            {
                if (current != animation && animations.ContainsKey(animation))
                {
                    current = animation;
                    return true;
                }
                return false;
            }

            public SpriteSheet SpriteSheet
            { get { return spriteSheet; } }

            public  Animation CurrentAnimation
            { get { return  animations[current]; } }

            public Rectangle SpriteSourceRectangle
            { get {return spriteSheet.SourceRectangle(CurrentAnimation.CurrentFrame); } }

            public TimeSpan AnimationInterval
            {
                get { return animations[current].AnimationInterval; }
                set { animations[current].AnimationInterval = value; }
            }
            
        }

        public class Animation
        {
            string[] indices;
            int currentFrame;
            TimeSpan animationInterval;
            TimeSpan prevFrameTime;

            public Animation(string[] indices, TimeSpan animationInterval)
            {
                this.indices = indices;
                this.animationInterval = animationInterval;
                this.currentFrame = 0;
                this.prevFrameTime = TimeSpan.Zero;
            }

            

            // Move to the next frame or wrap to 0
            public void incFrame(GameTime gTime)
            {
                TimeSpan currentTime = gTime.TotalGameTime;

                if (currentTime >= prevFrameTime + animationInterval)
                {
                    currentFrame = currentFrame < indices.Length - 1 ? currentFrame + 1 : 0;
                    prevFrameTime = currentTime;
                }
            }

            public string CurrentFrame
            { get { return indices[currentFrame]; } }

            public string[] Indices
            { get { return indices; } }

            public TimeSpan AnimationInterval
            { 
                get { return animationInterval; }
                set { animationInterval = value; }
            }
        }
    
}
