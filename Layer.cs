/*-----------------------------------------
 * Layer.cs
 * Arthur Ward Jr
 * Quantum Box
 * Version 0.1
 * 
 * Defines a drawable object layer
 * This class stores helps to organize objects into seperate layers for drawing and collision purposes
 * 
 -----------------------------------------*/

#region Using
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
using Rain.Objects;
#endregion

namespace Rain
{
    class Layer
    {

        private float scrollRateX;
        private float scrollRateY;
        private List<GameObject> objects;
        private Boolean visible;
        private int layerZOrder;

        public Layer(float scrollX, float scrollY, int zorder = 0, Boolean vis = true)
        {

            scrollRateX = scrollX;
            scrollRateY = scrollY;
            visible = vis;
            objects = new List<GameObject>();
            layerZOrder = zorder;
        }

        //Adds a game object to this layer
        public void add(GameObject obj)
        {
            objects.Add(obj);
        }

        //Removes a game object from this layer
        public Boolean remove(GameObject obj)
        {
            return objects.Remove(obj);

        }

        public void update(GameTime gameTime)
        {
            for (int i = 0; i < objects.Count; i++)

                if (objects[i].Remove)
                {
                    objects.Remove(objects[i]);

                }
                else
                {
                    objects[i].AnimationTable.CurrentAnimation.incFrame(gameTime);
                    objects[i].update(gameTime);
                }

        }

        //Change/Retreive the current scroll rates: Used for parallax scrolling
        public float ScrollRateX
        {
            get { return scrollRateX; }
            set { scrollRateX = value; }
        }

        public float ScrollRateY
        {
            get { return scrollRateY; }
            set { scrollRateY = value; }
        }

        //Get/Set Visibility
        public Boolean Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        //Get/Set Zorder
        public int ZOrder
        {
            get { return layerZOrder; }
            set { layerZOrder = value; }
        }



        //Retrieve the game objects contained within this Layer.
        public List<GameObject> Objects
        {
            get { return objects; }
        }

    }
}
