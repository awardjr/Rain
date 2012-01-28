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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Layer> layers;
        Camera camera;
        Controller controller;
        List<Line> lines;
        Random randomNum;
        SortedDictionary<String, AnimationTable> animationTables = new SortedDictionary<string, AnimationTable>();
        SpriteSheet spritesheet;
        CollisionManager collisionManager;
        Player player;

        Layer main;
        Layer fog;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LineBatch.Init(GraphicsDevice);
            layers = new List<Layer>();
            camera = new Camera(Vector2.Zero);
            lines = new List<Line>();
            randomNum = new Random();
            collisionManager = new CollisionManager();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            controller = new Controller();
            camera = new Camera(Vector2.Zero);

            spritesheet = Content.Load<SpriteSheet>("sheet");
            loadAnimationTables();
           
            loadLayers();
            
            



            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
           
            
            controller.update();

            handleControls(gameTime);
            player.update(gameTime);

            foreach(Layer layer in layers)
            {
                layer.update(gameTime);
            }

            updateFog(gameTime);

            updateCamera(gameTime);

            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            foreach (Layer layer in layers)
                if (layer.Visible)
                    foreach (GameObject drawable in layer.Objects)
                        if (drawable.Visible)
                            spriteBatch.Draw(drawable.AnimationTable.SpriteSheet.Texture, camera.getRenderPosition(drawable.Position, layer.ScrollRateX, layer.ScrollRateY),
                               drawable.AnimationTable.SpriteSourceRectangle, drawable.Color, drawable.Rotation, new Vector2(drawable.AnimationTable.SpriteSourceRectangle.Width / 2, drawable.AnimationTable.SpriteSourceRectangle.Height / 2), drawable.Scale, drawable.FlipHorizontally, drawable.ZOrder + layer.ZOrder);

            foreach (Line line in lines)
                LineBatch.DrawLine(spriteBatch, camera, new Color(randomNum.Next(100, 255), randomNum.Next(255), randomNum.Next(255)), line);

            // DrawText();
            spriteBatch.End();


            base.Draw(gameTime);
        }

        protected void loadLayers()
        {

            player = new Player(new Vector2(100, 100), animationTables["player"], controller, ref collisionManager);
            player.setAnimation("stand");


            main = new Layer(1f, 1f, 1);
            main.add(player);

           
            Layer star1 = new Layer(0.8f, 0.8f, 2);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    star1.add(new GameObject(new Vector2(randomNum.Next(2000), randomNum.Next(2000)), animationTables["stars"], ref collisionManager));

            Layer star2 = new Layer(0.6f, 0.6f, 3);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    star2.add(new GameObject(new Vector2(randomNum.Next(3000), randomNum.Next(3000)), animationTables["stars"], ref collisionManager));

            Layer star3 = new Layer(0.4f, 0.4f, 4);
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    star3.add(new GameObject(new Vector2(randomNum.Next(3000), randomNum.Next(3000)), animationTables["stars"], ref collisionManager));

            Layer grass = new Layer(1f, 1f, 5);
            for(int i = 0; i<20; i++)
                for(int j=0; j<20; j++)
                    grass.add(new GameObject(new Vector2(i*256, j*256), animationTables["ground"], ref collisionManager));

            fog = new Layer(1f, 1f, 0);
            for (int i = 0; i < 50; i++)
                for (int j = 0; j < 50; j++)
                    fog.add(new FogObject(new Vector2(i * 16, j * 16), animationTables["clouds"], ref collisionManager));

            layers.Add(grass);
            layers.Add(star3);
            layers.Add(star2);
            layers.Add(star1);
            
            layers.Add(main);
            layers.Add(fog);
           
        }

        protected void loadAnimationTables()
        {
            AnimationTable player = new AnimationTable(spritesheet);
            AnimationTable ground = new AnimationTable(spritesheet);
            AnimationTable stars = new AnimationTable(spritesheet);
            AnimationTable bullets = new AnimationTable(spritesheet);
            AnimationTable clouds = new AnimationTable(spritesheet);

            player.addAnimation("stand", new Animation(new string[] { "kitty" }, TimeSpan.FromMilliseconds(100)));

            ground.addAnimation("grass", new Animation(new string[] { "grass" }, TimeSpan.FromMilliseconds(50)));

            stars.addAnimation("star1", new Animation(new string[] { "star1" }, TimeSpan.FromMilliseconds(50)));
            bullets.addAnimation("basic", new Animation(new string[] { "bullet1", "bullet2", "bullet3" }, TimeSpan.FromMilliseconds(200)));
           
            clouds.addAnimation("clouds", new Animation(new string[] { "cloud" }, TimeSpan.FromMilliseconds(50)));
           
            animationTables.Add("player", player);
            animationTables.Add("ground", ground);
            animationTables.Add("stars", stars);
            animationTables.Add("bullets", bullets);
            animationTables.Add("clouds", clouds);
        }

        protected void updateCamera(GameTime gameTime)
        {
            camera.Position = (new Vector2(player.Position.X - 360, player.Position.Y - 240));
            camera.update(gameTime);
        }

        protected void handleControls(GameTime gameTime)
        {
               if(controller.keyPressed(Keys.LeftShift) || controller.keyPressed(Keys.RightShift))
               {
                   Bullet bullet = new Bullet(player.Position, animationTables["bullets"], ref collisionManager, 10f, player.Rotation);
                   main.add(bullet);
               }
        }

        protected void updateFog(GameTime gametime)
        {
            foreach(FogObject fogTile in fog.Objects)
            {
                fogTile.Visible = true;
                if (Math.Abs(player.Position.X - fogTile.Position.X) < 100 && Math.Abs(player.Position.Y - fogTile.Position.Y) < 100)
                {
                    fogTile.Visible = false;
                }
                    
            }
        }
    }
}
