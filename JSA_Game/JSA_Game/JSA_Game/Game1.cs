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

        //Game state
        GameState gameState;

        //List of levels
        ArrayList levels;

        //Current level
        Level currLevel;

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

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 500;
            this.IsMouseVisible = true;

            gameState = GameState.Level;
            //Idea: In warrior, mage, etc. constructors, get rid of Level parameter
            //In character class, add initializeChar() method
            //This method will initialize enemy AI and maybe other things.
            //This is so the created characters don't have to be sent the level
            //they're on since they will be stored in a list in this class.
            playerChars = new ArrayList();


            levels = new ArrayList();
            levels.Add(new Level("JSAtestlevel"));
            levels.Add(new Level("Coast"));
            levels.Add(new Level("Arena"));
            
            
            //Set first level
            currLevel = (Level) levels[0];


            Content.RootDirectory = "Content";

            // Load Sounds
            bow_attack = Content.Load<SoundEffect>("Audio\\bow_attack");
            fire_spell = Content.Load<SoundEffect>("Audio\\fire_spell");
            ice_spell = Content.Load<SoundEffect>("Audio\\ice_spell");
            multishot = Content.Load<SoundEffect>("Audio\\multishot");
            shock_spell = Content.Load<SoundEffect>("Audio\\shock_spell");
            swoosh = Content.Load<SoundEffect>("Audio\\swoosh");
            sword_attack = Content.Load<SoundEffect>("Audio\\sword_attack");
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

        public static void PlaySound(String sound) {
            Console.WriteLine(sound);
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
            }
        }
    }
}