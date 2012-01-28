using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace Rain
{
    public class Camera
    {
        Vector2 position;
        Vector2 velocity;
        Matrix transform;
        float scale;
       
    //====================================================================================================

        public Camera(Vector2 initPos)
        {
            position = initPos;
            velocity = Vector2.Zero;
            scale = 1f;
        }

    //====================================================================================================

        public void move(Vector2 moveVector)
        {
            position += moveVector;
        }

   

        public void zoom(float speed, MouseState mState)
        {
            float x = (mState.X - Globals.RES_H / 2) / (Globals.RES_H / 2) * Globals.CAMERA_SPEED_MAX;
            float y = (mState.Y - Globals.RES_V / 2) / (Globals.RES_V / 2) * Globals.CAMERA_SPEED_MAX;

            scale += speed;
            incrementVelocity(x, y);
        }

        public void incrementVelocity(float x, float y)
        {
            velocity += new Vector2(x, y);
        }

        public Vector2 getRenderPosition(Vector2 spritePosition, float xOffset = 1, float yOffset = 1)
        {
            Vector2 newPosition = new Vector2(xOffset, yOffset);
        
 
            return (spritePosition - position)* newPosition ;
        }

        public void update(GameTime gameTime)
        {
            velocity += velocity * gameTime.ElapsedGameTime.Seconds;

            //Restrict camera velocity to CAMERA_SPEED_MAX
            velocity.X = MathHelper.Clamp(velocity.X, -Globals.CAMERA_SPEED_MAX, Globals.CAMERA_SPEED_MAX);
            velocity.Y = MathHelper.Clamp(velocity.Y, -Globals.CAMERA_SPEED_MAX, Globals.CAMERA_SPEED_MAX);

            //Move camera based on current cameraVelocity
            move(velocity);

            if (velocity != Vector2.Zero)
                velocity += Vector2.Normalize(Vector2.Negate(velocity));

            //Fix camera deceleration
                velocity.X *= Math.Abs(velocity.X) < 0.5 ? 0f : 1f;
                velocity.Y *= Math.Abs(velocity.Y) < 0.5 ? 0f : 1f;

            enforceScaleBounds();
            enforcePositionBounds();

            transform = Matrix.CreateScale(new Vector3(scale, scale, 0f));
        }

        public void enforceScaleBounds()
        {
            scale = MathHelper.Clamp(scale, Globals.MAP_SCALE_MIN, Globals.MAP_SCALE_MAX);
        }

        public void enforcePositionBounds()
        {
            float scaledMargin = Globals.MAP_BOUNDING_MARGIN * scale;
            float scaledMapWidth = Globals.MAP_WIDTH * Globals.TILE_WIDTH * scale;
            float scaledMapHeight = Globals.MAP_HEIGHT * Globals.TILE_WIDTH * scale;

            if (position.X * scale < -scaledMargin)
                position.X = -Globals.MAP_BOUNDING_MARGIN;

            if (position.X * scale + Globals.RES_H > scaledMapWidth + scaledMargin)
                position.X = (scaledMapWidth + scaledMargin - Globals.RES_H) / scale;

            if (position.Y * scale < -scaledMargin)
                position.Y = -Globals.MAP_BOUNDING_MARGIN;

            if (position.Y * scale + Globals.RES_V > scaledMapHeight + scaledMargin)
                position.Y = (scaledMapHeight + scaledMargin - Globals.RES_V) / scale;
        }

    //====================================================================================================

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public float VelocityX
        {
            get { return velocity.X; }
            set { velocity.X = value; }
        }

        public float VelocityY
        {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        public Matrix Transform
        {
            get { return transform; }
            set { transform = value; }
        }

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
    }
}