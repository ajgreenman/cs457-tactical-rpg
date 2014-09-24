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

using JSA_Game.CharClasses;
using JSA_Game.Maps;

namespace JSA_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Constants
        const int TILE_IMAGE_COUNT = 1;
        const int TILE_SIZE = 50;
        int MAP_START_H = 30;
        int MAP_START_W = 30;

        //Game Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //GameBoard board;

        //Example Level
        Level testLevel;

        //Timing variables
        float elapsed;
        float delay = 500f;
        float cursorTimeElapsed;
        float cursorMoveDelay = 100;

        //Cursor Variables
        Texture2D cursor;
        int cursorFrames = 0;
        Boolean selected = false;
        Vector2 cursorPos;
        Rectangle cursorSourceRect;
       

        //Image Data Structures
        Texture2D[] tileImages;
        Dictionary<String, Texture2D> characterImages;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 620;
            graphics.PreferredBackBufferWidth = 560;
            this.IsMouseVisible = true;

            //Initialize Example Level
            testLevel = new Level(10, 10, 1, 1);
            testLevel.addUnit(1, new Warrior(), 1, testLevel.BoardHeight/2);
            testLevel.addUnit(0, new Mage(), testLevel.BoardWidth -2, testLevel.BoardHeight/2);

            //To print System.Diagnostics.Debug.Print("Text");

            Content.RootDirectory = "Content";
            //board = new GameBoard();
            tileImages = new Texture2D[TILE_IMAGE_COUNT];
            characterImages = new Dictionary<String, Texture2D>();
            cursorPos = new Vector2(0, 0);
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


            //Example level Content (more efficient loop to come...)
            foreach( Character c in testLevel.PlayerUnits){
                System.Diagnostics.Debug.Print("Created a player unit");
                if (c == null) break;
                //if (characterImages[c.Texture] == null)
               // {
                    characterImages.Add(c.Texture, Content.Load<Texture2D>(c.Texture));
                //}
            }
            foreach (Character c in testLevel.EnemyUnits)
            {
                if (c == null) break;
                //if (characterImages[c.Texture] == null)
                //{
                    characterImages.Add(c.Texture, Content.Load<Texture2D>(c.Texture));
                //}
            }

            cursor = Content.Load<Texture2D>("cursorAnim");

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

            //Animate cursor
            elapsed += (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            if(elapsed >= delay){
                if(cursorFrames >= 1){
                    cursorFrames = 0;
                }
                else{
                    cursorFrames++;
                }
                elapsed = 0;
            }
            cursorSourceRect = new Rectangle(cursor.Width /2 * cursorFrames, 0, cursor.Width / 2, cursor.Height);


            //Move cursor
            cursorTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (!selected && cursorTimeElapsed >= cursorMoveDelay)
            {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left) && cursorPos.X != 0 && elapsed < delay)
                {
                    cursorPos.X--;
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right) && cursorPos.X != testLevel.BoardWidth - 1 && elapsed < delay)
                {
                    cursorPos.X++;
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up) && cursorPos.Y != 0 && elapsed < delay)
                {
                    cursorPos.Y--;
                }
                else if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down) && cursorPos.Y != testLevel.BoardHeight - 1 && elapsed < delay)
                {
                    cursorPos.Y++;
                }
                cursorTimeElapsed = 0;
            }

            //Select button
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Z) && testLevel.Board[(int)cursorPos.X,(int)cursorPos.Y].IsOccupied)
            {
                selected = true;
            }

            //Unselect button
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.X))
            {
                selected = false;
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //Draw board
            for (int i = 0; i < testLevel.BoardWidth; i++)
            {
                for (int j = 0; j < testLevel.BoardHeight; j++)
                {
                    spriteBatch.Draw(tileImages[0], new Rectangle(MAP_START_W + TILE_SIZE * i, MAP_START_H + TILE_SIZE * j, TILE_SIZE, TILE_SIZE), Color.White);
                }
            }


            //Draw characters
            foreach (Character c in testLevel.PlayerUnits)
            {
               if (c == null) break;
               //System.Diagnostics.Debug.Print("Drawing player character at position " + c.Pos.X + "," + c.Pos.Y + ". Texture = " + c.Texture);
               spriteBatch.Draw(characterImages[c.Texture], new Rectangle(MAP_START_W + TILE_SIZE * (int)c.Pos.X, MAP_START_H + TILE_SIZE * (int)c.Pos.Y, TILE_SIZE, TILE_SIZE), Color.White);
               
            }
            foreach (Character c in testLevel.EnemyUnits)
            {
               
                if (c == null) break;
                //System.Diagnostics.Debug.Print("Drawing enemy character at position " + c.Pos.X + "," + c.Pos.Y + ". Texture = " + c.Texture);
                spriteBatch.Draw(characterImages[c.Texture], new Rectangle(MAP_START_W + TILE_SIZE * (int)c.Pos.X, MAP_START_H + TILE_SIZE * (int)c.Pos.Y, TILE_SIZE, TILE_SIZE), Color.White);
            }

            //Draw Cursor
            spriteBatch.Draw(cursor, new Rectangle(MAP_START_W + TILE_SIZE * (int)cursorPos.X, MAP_START_H + TILE_SIZE * (int)cursorPos.Y, cursor.Width/2, cursor.Height),
                                     cursorSourceRect,Color.White);



            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
