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
    public class Line
    {
        Vector2 point1;
        Vector2 point2;

        public Line(Vector2 point1, Vector2 point2, GraphicsDevice graphicsDevice)
        {
            Texture2D blank = new Texture2D(graphicsDevice, 1, 1, true, SurfaceFormat.Color);
            blank.SetData(new[] { Color.White });
            this.point1 = point1;
            this.point2= point2;
        }

        public Vector2 Point1
        {
            set { point1 = value; }
            get { return point1; }
        }

        public Vector2 Point2
        {
            set { point2 = value; }
            get { return point2; }
        }

    }
}
