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
using GameStateManagement;

using JSA_Game.CharClasses;
using JSA_Game.Maps;
using JSA_Game.Screens;

namespace JSA_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        //Constants
        private const int LEVEL_COUNT = 3;

        //Game Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Screen Manager
        ScreenManager screenManager;
        ScreenFactory screenFactory;
        static SpriteFont smallMenuFont;

        //List of levels
        private static string[] levels;
        private static int currentLevel;
        //public static ArrayList levels;

        //Current level
        //Level currLevel;

        //List of player characters
        private static ArrayList playerChars;

        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 500;
            this.IsMouseVisible = true;
            this.Components.Add(new GamerServicesComponent(this));
            Content.RootDirectory = "Content";

            Sound sound = new Sound(Content);

            //Create screen factory and add to Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            //Create the screen manager
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            AddInitialScreens();


            playerChars = new ArrayList();

            //Let's add a character!
            //So, change constructor in character classes to no longer take
            // a level as a parameter.  This will be set when a level initializes.
            // Also, need a way to keep track of a character's AI option.  Need to
            // reinitialize the right one at the start of each level.  String would work.
            playerChars.Add(new Thief(null));
            playerChars.Add(new Cleric(null));

            levels = new string[LEVEL_COUNT];

          //  levels = new ArrayList();
           // levels.Add("JSAtestlevel");
           // levels.Add("Coast");
           // levels.Add("Arena");

            currentLevel = 0;
            levels[0] = "JSAtestlevel";
            levels[1] = "Coast";
            levels[2] = "Arena";
            
            
            //Set first level
            //currLevel = (Level) levels[0];


           
        }

        private void AddInitialScreens()
        {
            // Activate the first screens.
            screenManager.AddScreen(new MainMenuBackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);

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
            smallMenuFont = Content.Load<SpriteFont>("smallMenufont");

            //Load level content
            //currLevel.loadContent(Content);

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

            /*
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
            */
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
            /*
            currLevel.draw(spriteBatch);
            */
            spriteBatch.End();

            base.Draw(gameTime);
        }
        

        public static string getNextLevelName()
        {
            
            string name = "";
            if(levels.Length >  currentLevel)
                name = levels[currentLevel];
            currentLevel++;
            return name;
        }
        public static int getCurrLevelNum()
        {
            return currentLevel;
        }
        public static void setCurrLevelNum(int levelNum)
        {
            currentLevel = levelNum;
        }

        public static SpriteFont getSmallFont()
        {
            return smallMenuFont;
        }

        public static void resetLevels()
        {
            currentLevel = 0;
        }

        public static ArrayList getPlayerChars()
        {
            return playerChars;
        }
        public static void setPlayerChars(ArrayList chars)
        {
            playerChars = chars;
        }
    
    }
}