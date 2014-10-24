using System;
using System.Collections;
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

        //Game Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //List of levels
        ArrayList levels;

        //Example Level
        Level currLevel;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 500;
            this.IsMouseVisible = true;

            levels = new ArrayList();
            levels.Add( new Level("Arena"));
            levels.Add(new Level("JSAtestlevel"));

            //Set first level
            currLevel = (Level) levels[0];


            //Initialize Example Level
            //testLevel = new Level(10, 10, 1, 1);
            //testLevel = new Level("JSAtestlevel");
            //estLevel = new Level("Arena");

            //Character c = new Warrior(testLevel);
            //More movement for player
            //c.Movement = 8;

            //testLevel.addUnit(1, c, new Vector2(0, 0));
            //testLevel.addUnit(0, new Mage(testLevel), new Vector2(testLevel.BoardWidth -1, testLevel.BoardHeight - 1));

            

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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load level content
            currLevel.loadContent(Content);

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

            //Update level
            if (currLevel.WinState == WinLossState.InProgess)
            {
                currLevel.update(gameTime);
            }
            else if (currLevel.WinState == WinLossState.Win)
            {
                //next level
                levels.Remove(currLevel);
                if (levels.Count > 0)
                {
                    currLevel = (Level)levels[0];
                    currLevel.loadContent(Content);
                    currLevel.ButtonPressed = true;
                }
            }
            else
            {
                //Lost
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

            currLevel.draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}