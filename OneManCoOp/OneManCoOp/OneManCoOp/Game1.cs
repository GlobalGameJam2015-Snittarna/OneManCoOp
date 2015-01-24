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

namespace OneManCoOp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const float GRAVITY = 1;

        public const int SCREEN_W = 832;
        public const int SCREEN_H = 720;

        public static int GlobalTimer;
        public const int MAXTIMER = 200;
        public static int numberOfCorpses;

        public static Vector2 SPAWNPOSITION = new Vector2(500, 3000);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        internal static Player player;

        internal static List<GameObject> objects;

        List<Corpse> corpses = new List<Corpse>();

        public static int[,] corpsesX = new int[100, 1000];
        public static int[,] corpsesY = new int[100, 1000];

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = SCREEN_W;
            graphics.PreferredBackBufferHeight = SCREEN_H;
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Input.Initialize();
            TextureManager.Load(Content);
            objects = new List<GameObject>();
            Map.Initialize();
            Camera.Position = new Vector2(Chunk.sizePx.X, Chunk.sizePx.Y) / 2;
            Camera.Scale = 1;
            Camera.Origin = new Vector2(SCREEN_W, SCREEN_H) / 2;
            Camera.FollowSpeed = .5f;
            player = new Player(new Vector2(500, 3000));
            objects.Add(new Puzzel(player.Position, Puzzel.Type.Door, 0, 0));
            objects.Add(new Button(new Vector2(player.Position.X+100, player.Position.Y+128), Color.White, 0));
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
            GlobalTimer++;

            Input.Update();
            if (Input.newKs.IsKeyDown(Keys.Escape)) this.Exit();

            Camera.Follow(player.Position, new Vector2(0, 1));

            player.Update();

            corpsesX[numberOfCorpses, GlobalTimer] = (int)player.Position.X;
            corpsesY[numberOfCorpses, GlobalTimer] = (int)player.Position.Y;

            foreach (GameObject g in objects) g.Update();

            if (GlobalTimer == MAXTIMER)
            {
                GlobalTimer = 0;
                corpses.Add(new Corpse(numberOfCorpses));
                numberOfCorpses++;
            }

            foreach (Corpse c in corpses)
            {
                c.Update();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.Transform);

            Map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            foreach (GameObject g in objects) g.Draw(spriteBatch);
            foreach (Corpse c in corpses) { c.Draw(spriteBatch); }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
