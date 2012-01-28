using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rain
{
    class GameObject
    {
        protected Vector2 position;
        protected float rotation;
        protected int zOrder;
        protected Color color;
        protected float alpha;
        protected CollisionManager collisionManager;
        protected float scale;
        protected Boolean remove;
        protected Boolean visible;
        protected Boolean tested;
    
       
       
        private Boolean solid = false;
        
        AnimationTable animationTable;

        //------------------------------------------------------------------
        public GameObject(Vector2 initPos, AnimationTable initAnimationTable,ref CollisionManager pCollisionManager)
        {
          
           // pCollisionManager.addCollisionObject(this);
            collisionManager = pCollisionManager;
            position = initPos;
            animationTable = initAnimationTable;
            zOrder = 0;
            scale = 1f;
            alpha = 1f;
            color = new Color(255, 255, 255, 255);
            visible = true;
            remove = false;
            solid = false;
            collisionManager.addCollisionObject(this);
         
        }



        public void setAnimation(string animation)
        {
            animationTable.setAnimation(animation);
        }

     

        public Boolean setSolid()
        {
            if (solid == false)
            {
               // collisionManager.addCollisionObject(this);
                solid = true;
                return true;
            }
            return false;
        }


        public Boolean unsetSolid()
        {
            if (solid == true)
            {
               // collisionManager.removeCollisionObject( this);
                solid = false;
                return true;
            }
            return false;
        }


        public virtual void update(GameTime gametime)
        {
            tested = false;
        }

        //------------------------------------------------------------------
        public AnimationTable AnimationTable
        {
            get { return animationTable; }

        }

        public Boolean Remove
        {
            get { return remove; }
            set { remove = value; }
        }
        
        public int ZOrder
        {
            get { return zOrder; }
            set { zOrder = value; }
        }


        public Boolean Solid
        {
            get { return solid; }
            set { solid = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public Vector2 Center
        {
            get { return new Vector2(position.X + animationTable.SpriteSourceRectangle.Width / 2, position.Y + animationTable.SpriteSourceRectangle.Height / 2); }
        }

        public byte Red
        {
            get { return color.R; }
            set { color.R = (byte)MathHelper.Clamp(value, 1, 255); }    
        }

        public byte Blue
        {
            get { return color.B; }
            set { color.B = (byte)MathHelper.Clamp(value, 1, 255); }
        }

        public byte Green
        {
            get { return color.G; }
            set { color.G = (byte)MathHelper.Clamp(value, 1, 255); ; }
        }

        public float Alpha
        {
            get { return alpha; }
            set { alpha = MathHelper.Clamp(value, 0, 1);  }
        }

        public Boolean Visible
        {
            get { return visible; }
            set { visible = value; }
        }


        public Boolean Tested
        {
            get { return tested; }
            set { tested = value; }
        }
        

        public Color Color
        {
            get { return color * alpha; }
            set { color = value; }
        }


        
    }
}


