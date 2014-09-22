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

namespace JSA_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Constants
        const int IMAGE_COUNT = 1;
        const int TILE_SIZE = 50;
        int MAP_START_H = 30;
        int MAP_START_W = 30;

        //Game Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GameBoard board;
        Texture2D[] tileImages;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 560;
            graphics.PreferredBackBufferWidth = 560;
            this.IsMouseVisible = true;


            //To print System.Diagnostics.Debug.Print("Text");

            Content.RootDirectory = "Content";
            board = new GameBoard();
            tileImages = new Texture2D[IMAGE_COUNT];
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

            tileImages[0] = Content.Load<Texture2D>("grass_tile");

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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            
            int temp = 10;
            for (int i = 0; i < temp; i++)
            {
                for (int j = 0; j<temp; j++)
                {
                    spriteBatch.Draw(tileImages[0], new Rectangle(MAP_START_W + TILE_SIZE * i, MAP_START_H + TILE_SIZE * j, TILE_SIZE, TILE_SIZE), Color.White);
                }
            }
            
            

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
