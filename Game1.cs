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
        SortedList<String, Layer> layers;
        Camera camera;
        Controller controller;
        List<Line> lines;
        Random randomNum;
        SortedDictionary<String, AnimationTable> animationTables = new SortedDictionary<string, AnimationTable>();
        SpriteSheet spritesheet;
        CollisionManager collisionManager;
        Player player;

        // State Machine
        CStateMachine _StateMachine;

      


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

            CMainMenuState _MainMenu = new CMainMenuState();
            _MainMenu.Enter();
            

            _StateMachine = new CStateMachine();
            _StateMachine.PushState(_MainMenu);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            LineBatch.Init(GraphicsDevice);
            layers = new SortedList<String, Layer>();
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
            _StateMachine.UpdateState(gameTime.ElapsedGameTime.Ticks);
           
            
            controller.update();

            handleControls(gameTime);
            player.update(gameTime);

            foreach (KeyValuePair<String, Layer> layer in layers)
            {
                layers[layer.Key].update(gameTime);
            }

            updateCamera(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _StateMachine.RenderState();


            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

            foreach (KeyValuePair<String, Layer> pair in layers) {
                layers[pair.Key].update(gameTime);
                foreach (GameObject drawable in layers[pair.Key].Objects)
                {
                    if (drawable.Visible)
                    {
                        spriteBatch.Draw(drawable.AnimationTable.SpriteSheet.Texture,
                            camera.getRenderPosition(drawable.Position, layers[pair.Key].ScrollRateX, layers[pair.Key].ScrollRateY),
                            drawable.AnimationTable.SpriteSourceRectangle,
                            drawable.Color,
                            drawable.Rotation,
                            new Vector2(drawable.AnimationTable.SpriteSourceRectangle.Width / 2, drawable.AnimationTable.SpriteSourceRectangle.Height / 2),
                            drawable.Scale,
                            drawable.FlipHorizontally,
                            drawable.ZOrder + layers[pair.Key].ZOrder);
                    }
                }
            }
            foreach (Line line in lines)
                LineBatch.DrawLine(spriteBatch, camera, new Color(randomNum.Next(100, 255), randomNum.Next(255), randomNum.Next(255)), line);

            // DrawText();
            spriteBatch.End();


            base.Draw(gameTime);
        }

        protected void loadLayers()
        {

            player = new Player(new Vector2(100, 100), animationTables["player"], controller);
            player.setAnimation("stand");

            layers.Add("main", new Layer(1f, 1f, 1));
            layers["main"].add(player);
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
                   Bullet bullet = new Bullet(player.Position, animationTables["bullets"], 10f, player.Rotation);
                   layers["main"].add(bullet);
               }
        }
    }
}
