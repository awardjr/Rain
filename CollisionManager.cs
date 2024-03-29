﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Collections;


namespace Rain.Objects
{
    class CollisionManager
    {
        public List<GameObject> objects;

        public CollisionManager(){

            objects = new List<GameObject>();
        }

         
        public Vector2 testCollision(GameObject a, GameObject b)
        {
            Rectangle aRectangle = new Rectangle((int)(a.Position.X - (a.Width / 2)), (int)(a.Position.Y - (a.Height / 2)), (int)(a.AnimationTable.SpriteSourceRectangle.Width * a.Scale), (int)(a.AnimationTable.SpriteSourceRectangle.Height * a.Scale));
            Rectangle bRectangle = new Rectangle((int)(b.Position.X - (b.Width / 2)), (int)(b.Position.Y - (b.Height / 2)), (int)(b.AnimationTable.SpriteSourceRectangle.Width * b.Scale), (int)(b.AnimationTable.SpriteSourceRectangle.Height * b.Scale));

                return RectangleExtensions.GetIntersectionDepth(aRectangle, bRectangle);
        }

        public GameObject testTop(GameObject a, Vector2 offest, ref Vector2 depth)
        {
            Rectangle aRectangle = new Rectangle((int)a.Position.X, (int)(a.Position.Y + offest.Y), (int)(a.AnimationTable.SpriteSourceRectangle.Width * a.Scale), (int)offest.Y);
            // Boolean collision = false;
            
            foreach (GameObject b in objects)
            {
                if (!b.Equals(a)&& b.Solid)
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);
                    
                    depth = RectangleExtensions.GetIntersectionDepth(aRectangle, bRectangle);
                    
                    if (depth != Vector2.Zero)
                        return a;
                }
            }

            return null;
        }


        public List<GameObject> testTop(GameObject a, Vector2 offest, Boolean ignoreSolidFlag = false)
        {

            List<GameObject> hit = new List<GameObject>();

            Rectangle aRectangle = new Rectangle((int)a.Position.X, (int)(a.Position.Y + offest.Y), (int)(a.AnimationTable.SpriteSourceRectangle.Width * a.Scale), (int)offest.Y);
            // Boolean collision = false;

            foreach (GameObject b in objects)
            {
                if (!b.Equals(a) && (ignoreSolidFlag || b.Solid))
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);

                    if (aRectangle.Intersects(bRectangle))
                        hit.Add(b);
                }
            }

            return hit;
        }

         public  GameObject testBottom(GameObject a, Vector2 offset, ref Vector2 depth)
        {
            Rectangle aRectangle = new Rectangle((int)a.Position.X, (int)(a.Position.Y + a.AnimationTable.SpriteSourceRectangle.Height * a.Scale + offset.Y), (int)(a.AnimationTable.SpriteSourceRectangle.Width * a.Scale), (int)offset.Y + 2);
            // Boolean collision = false;
           // System.Console.WriteLine(aRectangle);
            foreach (GameObject b in objects)
            {
                if (!b.Equals(a) && b.Solid)
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);
                    
                    depth = RectangleExtensions.GetIntersectionDepth(aRectangle, bRectangle);
                        if (depth != Vector2.Zero)
                        {
                            return a;
                        }
                    }
            }

            return null;
        }

         public List<GameObject> testBottom(GameObject a, Vector2 offset, ref Vector2 depth, Boolean ignoreSolidFlag = false)
         {
             List<GameObject> hit = new List<GameObject>();

             Rectangle aRectangle = new Rectangle((int)a.Position.X, (int)(a.Position.Y + a.AnimationTable.SpriteSourceRectangle.Height * a.Scale + offset.Y), (int)(a.AnimationTable.SpriteSourceRectangle.Width * a.Scale), (int)offset.Y + 2);
             // Boolean collision = false;
             // System.Console.WriteLine(aRectangle);
             foreach (GameObject b in objects)
             {
                 if (!b.Equals(a) && (b.Solid || ignoreSolidFlag))
                 {
                     Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);

                     depth = RectangleExtensions.GetIntersectionDepth(aRectangle, bRectangle);
                     if (depth != Vector2.Zero)
                     {
                         hit.Add(b);
                     }
                 }
             }

             return hit;
         }



        public GameObject testLeft(GameObject a, Vector2 offset)
        {
            Rectangle aRectangle = new Rectangle((int)(a.Position.X + offset.X), (int)a.Position.Y, (int)offset.X, (int)(a.AnimationTable.SpriteSourceRectangle.Height * a.Scale));
            // Boolean collision = false;

            foreach (GameObject b in objects)
            {
                if (!b.Equals(a) && b.Solid)
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);

                    if (aRectangle.Intersects(bRectangle))
                        return a;
                }
            }

            return null;
        }

        public List<GameObject> testLeft(GameObject a, Vector2 offset, Vector2 depth, Boolean ignoreSolidFlag = false)
        {
            List<GameObject> hit = new List<GameObject>();

            Rectangle aRectangle = new Rectangle((int)(a.Position.X + offset.X), (int)a.Position.Y, (int)offset.X, (int)(a.AnimationTable.SpriteSourceRectangle.Height * a.Scale));
            // Boolean collision = false;

            foreach (GameObject b in objects)
            {
                if (!b.Equals(a) && (b.Solid || ignoreSolidFlag))
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);
                    depth = RectangleExtensions.GetIntersectionDepth(aRectangle, bRectangle);
                    
                    if (depth != Vector2.Zero)
                        hit.Add(b);
                }
            }

            return hit;
        }

        public GameObject testRight(GameObject a, Vector2 offset)
        {
            Rectangle aRectangle = new Rectangle((int)(a.Position.X + (a.AnimationTable.SpriteSourceRectangle.Width * a.Scale)), (int)a.Position.Y , (int)(offset.X), (int)(a.AnimationTable.SpriteSourceRectangle.Height * a.Scale) -3);
            // Boolean collision = false;

            foreach (GameObject b in objects)
            {
                if (!b.Equals(a) && b.Solid)
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);

                    if (aRectangle.Intersects(bRectangle))
                        return a;
                }
            }

            return null;
        }


        public List<GameObject> testRight(GameObject a, Vector2 offset, Vector2 depth, Boolean ignoreSolidFlag = false)
        {
            List<GameObject> hit = new List<GameObject>();

            Rectangle aRectangle = new Rectangle((int)(a.Position.X + (a.AnimationTable.SpriteSourceRectangle.Width * a.Scale)), (int)a.Position.Y, (int)(offset.X), (int)(a.AnimationTable.SpriteSourceRectangle.Height * a.Scale) - 3);
            // Boolean collision = false;

            foreach (GameObject b in objects)
            {
                if (!b.Equals(a) && (b.Solid || ignoreSolidFlag))
                {
                    Rectangle bRectangle = new Rectangle((int)b.Position.X, (int)b.Position.Y, b.AnimationTable.SpriteSourceRectangle.Width, b.AnimationTable.SpriteSourceRectangle.Height);

                    depth = RectangleExtensions.GetIntersectionDepth(aRectangle, bRectangle);
                    if (depth != Vector2.Zero)
                        hit.Add(b);
                }
            }

            return hit;
        }

        public List<GameObject> Objects
        {
            get { return objects; }
        }
    }
}
