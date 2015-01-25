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
using System.Diagnostics;

namespace OneManCoOp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum GameState { Won, Credits, Game, Paused }

        public const float GRAVITY = 1;

        public const int SCREEN_W = 832;
        public const int SCREEN_H = 720;

        public static int roundTime;
        public static int GlobalTimer;
        public static int maxTime;
        public static int numberOfCorpses;

        public static Vector2 SPAWNPOSITION = new Vector2(500, 3000);

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public byte flashCount;

        byte creditsDelay;

        short tutorialCount;

        public static GameState gameState;

        internal static Player player;

        internal static List<GameObject> objects;
        internal static List<Puzzel> puzzels = new List<Puzzel>();
        internal static List<Button> buttons = new List<Button>();
        internal static List<Ladder> ladders = new List<Ladder>();
        internal static List<Lava> lavas = new List<Lava>();
        internal static List<Particle> particles = new List<Particle>();
        public static List<Vector2> spawnPoints = new List<Vector2>();
        internal static List<Booster> boosters = new List<Booster>();

        internal static List<Corpse> corpses = new List<Corpse>();

        public static int[,] corpsesX = new int[9999, 9999];
        public static int[,] corpsesY = new int[9999, 9999];
        public static SpriteEffects[,] corpseEffect = new SpriteEffects[400, 2000];
        public static byte[,] corpseFrame = new byte[400, 2000];

        public static short currentChunk = 1;

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
            puzzels.Clear();
            buttons.Clear();
            ladders.Clear();
            lavas.Clear();
            particles.Clear();

            gameState = GameState.Game;

            Input.Initialize();

            TextureManager.Load(Content);

            SoundManager.Load(Content);

            objects = new List<GameObject>();

            Map.Initialize();

            Camera.Position = new Vector2(Chunk.sizePx.X, Chunk.sizePx.Y) / 2;
            Camera.Scale = 1;
            Camera.Origin = new Vector2(SCREEN_W, SCREEN_H) / 2;
            Camera.FollowSpeed = .5f;

            player = new Player(spawnPoints[spawnPoints.Count - 1]);
            //puzzels.Add(new Puzzel(player.Position, Puzzel.Type.Door, 0, 1));
            /*puzzels.Add(new Puzzel(new Vector2(player.Position.X + 100, player.Position.Y + 128), Puzzel.Type.Door, 1, 3));
            buttons.Add(new Button(new Vector2(player.Position.X+100, player.Position.Y+128), Color.Red, 1));
            buttons.Add(new Button(new Vector2(player.Position.X -100, player.Position.Y + 128), Color.Red, 1));
            buttons.Add(new Button(new Vector2(player.Position.X - 160, player.Position.Y + 128), Color.Red, 1));
            */
            maxTime = 500;
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
        public void resetGame()
        {
            puzzels.Clear();
            buttons.Clear();
            ladders.Clear();
            lavas.Clear();
            particles.Clear();
            corpses.Clear();

            gameState = GameState.Game;

            Input.Initialize();
            
            objects = new List<GameObject>();

            Map.Initialize();

            Camera.Position = new Vector2(Chunk.sizePx.X, Chunk.sizePx.Y) / 2;
            Camera.Scale = 1;
            Camera.Origin = new Vector2(SCREEN_W, SCREEN_H) / 2;
            Camera.FollowSpeed = .5f;

            GlobalTimer = 0;

            player = new Player(spawnPoints[spawnPoints.Count - 1]);
            //puzzels.Add(new Puzzel(player.Position, Puzzel.Type.Door, 0, 1));
            /*puzzels.Add(new Puzzel(new Vector2(player.Position.X + 100, player.Position.Y + 128), Puzzel.Type.Door, 1, 3));
            buttons.Add(new Button(new Vector2(player.Position.X+100, player.Position.Y+128), Color.Red, 1));
            buttons.Add(new Button(new Vector2(player.Position.X -100, player.Position.Y + 128), Color.Red, 1));
            buttons.Add(new Button(new Vector2(player.Position.X - 160, player.Position.Y + 128), Color.Red, 1));
            */
            maxTime = 500;
            tutorialCount = 0;
            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            Input.Update();
            if (Input.KeyWasJustPressed(Keys.R) || Input.ButtonJustPressed(Buttons.Y)) GlobalTimer = maxTime;
            Camera.Follow(player.Position, new Vector2(0, 1));

            if (gameState == GameState.Won && (Input.KeyWasJustPressed(Keys.Enter) || Input.ButtonJustPressed(Buttons.A))) gameState = GameState.Credits;
            if (gameState == GameState.Paused)
            {
                if (Input.KeyWasJustPressed(Keys.Escape) || Input.ButtonJustPressed(Buttons.Start)) gameState = GameState.Game;
                return;
            }

            if (Input.KeyWasJustPressed(Keys.Escape) || Input.ButtonJustPressed(Buttons.Start)) gameState = GameState.Paused;

            player.Update();
            roundTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            GlobalTimer++;
            corpsesX[numberOfCorpses, GlobalTimer] = (int)player.Position.X;
            corpsesY[numberOfCorpses, GlobalTimer] = (int)player.Position.Y;
            //corpseEffect[numberOfCorpses, GlobalTimer] = player.Effects;
            //corpseFrame[numberOfCorpses, GlobalTimer] = player.AnimationFrame;

            if (Input.KeyWasJustPressed(Keys.K)) player.Position = spawnPoints[0];

            foreach (GameObject g in objects) g.Update();
            foreach (Button b in buttons) { b.Update(); }
            foreach (Puzzel p in puzzels) { p.Update(); }
            foreach (Ladder l in ladders) l.Update();
            foreach (Lava l in lavas) { l.Update(); }
            foreach (Particle p in particles) { p.Update(); }
            foreach (Booster b in boosters) b.Update();

            if (GlobalTimer >= maxTime)
            {
                GlobalTimer = 0;
                corpses.Add(new Corpse(numberOfCorpses));
                numberOfCorpses++;
                player.Position = spawnPoints[spawnPoints.Count - 1];
            }

            foreach (Corpse c in corpses)
            {
                c.Update();
            }

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].Dead)
                    particles.RemoveAt(i);
            }

            base.Update(gameTime);
        }

        // Just C things
        public void DrawUi()
        {
            switch(gameState)
            {
                case GameState.Won:
                spriteBatch.DrawString(TextureManager.font, "YOU WON THE GAME! YOUR TIME IS: " + roundTime, new Vector2(Camera.Position.X-100, Camera.Position.Y), Color.Yellow);
                spriteBatch.DrawString(TextureManager.font, "Press enter", new Vector2(Camera.Position.X, Camera.Position.Y + 100), Color.White);
                roundTime = 0;
                    break;
            
                case GameState.Game:
                Color tmpColor = new Color(255, 255 - (maxTime - GlobalTimer)/5, 255 - (maxTime - GlobalTimer)/5);
                
                if ((maxTime - GlobalTimer) / 60 <= 3)
                {
                    flashCount += 1;
                    if (flashCount % 4 == 0) spriteBatch.DrawString(TextureManager.font, "TIME LEFT: " + (maxTime - GlobalTimer) / 60, new Vector2(Camera.Position.X - 100, Camera.Position.Y - 350), tmpColor);
                }
                else
                {
                    spriteBatch.DrawString(TextureManager.font, "TIME LEFT: " + (maxTime - GlobalTimer) / 60, new Vector2(Camera.Position.X- 100, Camera.Position.Y - 350), tmpColor);
                }
                spriteBatch.DrawString(TextureManager.font, "TIME: " + roundTime / 1000, Camera.TotalOffset + Camera.Origin - new Vector2(250, 350), Color.White);
                if(tutorialCount < 2000) tutorialCount += 1;
                if (tutorialCount <= 128*2)
                {
                    if (!GamePad.GetState(PlayerIndex.One).IsConnected) spriteBatch.DrawString(TextureManager.font, "A, S, D to move. W too stand on a \"ghost\". \nR to deplete your time and place a \"ghost\" \nAll buttons has to \nJump with space and W to stand on ghosts \nbe pressed down in a room by a \nplayer or \"ghost\" to open the trapdoor", new Vector2(Camera.Position.X - 400 + 32, Camera.Position.Y - 300), Color.Green);
                    if (GamePad.GetState(PlayerIndex.One).IsConnected) spriteBatch.DrawString(TextureManager.font, "Thumbstick to move \nY to deplete your time and place a \"ghost\" \nJump with A and pull the thumbstick up to stand on ghosts \nAll buttons has to \nbe pressed down in a room by a player or \"ghost\" to open the trapdoor", new Vector2(Camera.Position.X - 400 + 32, Camera.Position.Y - 300), Color.Green);

                }
                    break; 

                case GameState.Credits:
                    string s = "Made by:\nJohannes Larsson\nTom Leonardsson\nKristoffer Franzon\nEmil Jönsson";
                    spriteBatch.DrawString(TextureManager.font, s, Camera.TotalOffset + Camera.Origin - TextureManager.font.MeasureString(s) / 2, Color.White);
                    spriteBatch.DrawString(TextureManager.font, "Press enter", new Vector2(Camera.Position.X, Camera.Position.Y+100), Color.Yellow);
                    creditsDelay += 1;
                    if (Input.KeyWasJustPressed(Keys.Enter) && creditsDelay >= 10)
                    {
                        creditsDelay = 0;
                        resetGame();
                        gameState = GameState.Game;
                    }
                    break;
            }
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, Camera.Transform);
            if (gameState == GameState.Game || gameState == GameState.Paused)
            {
                if (GameState.Paused == gameState)
                {
                    string s = "PAUSED";
                    spriteBatch.DrawString(TextureManager.font, s, Camera.TotalOffset + Camera.Origin - TextureManager.font.MeasureString(s) / 2, Color.White);
                }
                Map.Draw(spriteBatch);
                player.Draw(spriteBatch);
                
                foreach (GameObject g in objects) g.Draw(spriteBatch);
                foreach (Corpse c in corpses) { c.Draw(spriteBatch); }
                foreach (Button b in buttons) { b.Draw(spriteBatch); }
                foreach (Puzzel p in puzzels) { p.Draw(spriteBatch); }
                foreach (Ladder l in ladders) l.Draw(spriteBatch);
                foreach (Lava l in lavas) { l.Draw(spriteBatch); }
                foreach (Particle p in particles) { p.Draw(spriteBatch); }
                foreach (Booster b in boosters) b.Draw(spriteBatch); 
                DrawUi();
            }
            else
                DrawUi();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
