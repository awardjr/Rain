using System;
using System.Collections.Generic;
using System.Linq;

namespace Rain
{
    class Globals
    {
        //"Previous" value of MouseState.ScrollWheelValue
        public static int SCROLL_VALUE;

        //Horizontal resolution
        public static float RES_H = 720;

        //Vertical resolution
        public static float RES_V  = 480;

        //Camera Move Rate
        public static float CAMERA_MOVE_RATE = 1.5f;
        //Horizontal Border Scroll Zone
        public static int SCROLL_ZONE_X = 30;

        //Vertical Border Scroll Zone
        public static int SCROLL_ZONE_Y = 30;



        //Width and height of a tile in pixels (depends on texture)
        public const int TILE_WIDTH = 25;

        //Width of map in tiles
        public const int MAP_WIDTH = 100;

        //Height of map in tiles
        public const int MAP_HEIGHT = 100;
        
        //Size of the margin beyond the map that the camera can move to
        public const float MAP_BOUNDING_MARGIN = 100f;

        //Bounds of camera scale
        public const float MAP_SCALE_MIN = 0.75f;
        public const float MAP_SCALE_MAX = 1.25f;

        //Self explanatory
        public const float ZOOM_SPEED_KEY = 0.015f;

        //Self explanatory
        public const float ZOOM_SPEED_MOUSE = 0.025f;
               
        //Default amount of player mana
        public const int MANA_DEFAULT = 100;

        //Max camera speed
        public const int CAMERA_SPEED_MAX = 20;
    }
}