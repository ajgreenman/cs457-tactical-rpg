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
        ArrayList playerChars;

        // Audio
        private static SoundEffect bow_attack;
        private static SoundEffect fire_spell;
        private static SoundEffect ice_spell;
        private static SoundEffect multishot;
        private static SoundEffect shock_spell;
        private static SoundEffect swoosh;
        private static SoundEffect sword_attack;
        private static SoundEffect battle;
        private static SoundEffect cursor;
        private static SoundEffectInstance cursorInstance;
        private static SoundEffectInstance battleInstance;
        private static SoundEffect title;
        private static SoundEffectInstance titleInstance;
        private static SoundEffect town;
        private static SoundEffectInstance townInstance;
        private static SoundEffect wind;
        private static SoundEffectInstance windInstance;
        private static SoundEffect horn;
        private static SoundEffectInstance hornInstance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 500;
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";

            // Load Sounds
            bow_attack = Content.Load<SoundEffect>("Audio\\bow_attack");
            fire_spell = Content.Load<SoundEffect>("Audio\\fire_spell");
            ice_spell = Content.Load<SoundEffect>("Audio\\ice_spell");
            multishot = Content.Load<SoundEffect>("Audio\\multishot");
            shock_spell = Content.Load<SoundEffect>("Audio\\shock_spell");
            swoosh = Content.Load<SoundEffect>("Audio\\swoosh");
            sword_attack = Content.Load<SoundEffect>("Audio\\sword_attack");
            cursor = Content.Load<SoundEffect>("Audio\\cursor");
            cursorInstance = cursor.CreateInstance();
            battle = Content.Load<SoundEffect>("Audio\\battle");
            battleInstance = battle.CreateInstance();
            battleInstance.Volume = 0.5f;
            battleInstance.IsLooped = true;
            title = Content.Load<SoundEffect>("Audio\\title");
            titleInstance = title.CreateInstance();
            titleInstance.Volume = 0.5f;
            titleInstance.IsLooped = true;
            town = Content.Load<SoundEffect>("Audio\\town");
            townInstance = town.CreateInstance();
            townInstance.Volume = 0.5f;
            townInstance.IsLooped = true;
            wind = Content.Load<SoundEffect>("Audio\\wind");
            windInstance = wind.CreateInstance();
            windInstance.Volume = 0.3f;
            horn = Content.Load<SoundEffect>("Audio\\horn");
            hornInstance = horn.CreateInstance();
            hornInstance.Volume = 0.4f;
            //Create screen factory and add to Services
            screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            //Create the screen manager
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);

            AddInitialScreens();


            //Idea: In warrior, mage, etc. constructors, get rid of Level parameter
            //In character class, add initializeChar() method
            //This method will initialize enemy AI and maybe other things.
            //This is so the created characters don't have to be sent the level
            //they're on since they will be stored in a list in this class.
            playerChars = new ArrayList();

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


        public static void PlaySound(String sound) {
            switch (sound)
            {
                case "bow_attack":
                    bow_attack.Play();
                    break;
                case "fire_spell":
                    fire_spell.Play();
                    break;
                case "ice_spell":
                    ice_spell.Play();
                    break;
                case "multishot":
                    multishot.Play();
                    break;
                case "shock_spell":
                    shock_spell.Play();
                    break;
                case "swoosh":
                    swoosh.Play();
                    break;
                case "sword_attack":
                    sword_attack.Play();
                    break;
                case "cursor":
                    PlayAmbientSounds();
                    cursorInstance.Volume = 0.2f;
                    cursorInstance.Play();
                    break;
                case "battle":
                    StopSounds();
                    if(battleInstance.State == SoundState.Stopped)
                    {
                        battleInstance.Play();
                    }
                    break;
                case "title":
                    StopSounds();
                    if(titleInstance.State == SoundState.Stopped)
                    {
                        titleInstance.Play();
                    }
                    break;
                case "town":
                    StopSounds();
                    if (townInstance.State == SoundState.Stopped)
                    {
                        townInstance.Play();
                    }
                    break;
            }
        }

        private static void StopSounds()
        {
            if (battleInstance != null)
            {
                battleInstance.Stop();
            }

            if (titleInstance != null)
            {
                titleInstance.Stop();
            }

            if (townInstance != null)
            {
                townInstance.Stop();
            }
        }

        private static void PlayAmbientSounds()
        {
            Random rng1 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int rand1 = rng1.Next(0, 100);
            if (rand1 >= 95)
            {
                // 5% of the time play one of these sound effects.
                Random rng2 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                int rand2 = rng2.Next(0, 100);
                if (rand2 % 2 == 0)
                {
                    hornInstance.Play();
                }
                else
                {
                    windInstance.Play();
                }
            }
        }
        

        public static string getNextLevelName()
        {
            
            string name = "";
            if(levels.Length >  currentLevel)
                name = levels[currentLevel];
            currentLevel++;
            return name;
        }

        public static SpriteFont getSmallFont()
        {
            return smallMenuFont;
        }

        public static void resetLevels()
        {
            currentLevel = 0;
        }
    
    }
}