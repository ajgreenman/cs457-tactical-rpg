using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using JSA_Game.Maps.State;
using JSA_Game.Maps;

namespace JSA_Game.Screens
{

    class LevelScreen : GameScreen
    {

        //Screen Variables
        ContentManager content;

        Random random = new Random();

        float pauseAlpha;

        InputAction pauseAction;
        InputAction startAction;

        Level currLevel;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelScreen(string levelName, ScreenManager screenManager)
        {
            
            TransitionOnTime = TimeSpan.FromSeconds(0.25);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            
            //Basic inputs: excape and Z
            pauseAction = new InputAction(
                null,
                new Keys[] { Keys.Escape },
                true);
            startAction = new InputAction(
                null,
                new Keys[] { Keys.Z },
                true);

            currLevel = new Level(levelName, screenManager);
            currLevel.loadContent(Game1.getContent());
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void Activate(bool instancePreserved)
        {
            if (!instancePreserved)
            {
                if (content == null)
                    content = new ContentManager(ScreenManager.Game.Services, "Content");

                //Thread.Sleep(5000);

                ScreenManager.Game.ResetElapsedTime();
                currLevel.loadContent(content);
            }

        }


        public override void Deactivate()
        {
            base.Deactivate();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void Unload()
        {
            content.Unload();
        }


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {

                if (currLevel.WinState == WinLossState.InProgess)
                {
                    currLevel.update(gameTime);
                }
                else if (currLevel.WinState == WinLossState.Win)
                {
                   //After win, go back to town
                    foreach (GameScreen screen in ScreenManager.GetScreens())
                        screen.ExitScreen();
                    ScreenManager.AddScreen(new TownBackgroundScreen(), null);
                    ScreenManager.AddScreen(new TownScreen(), null);

                    /*
                    //next level
                    levels.Remove(currLevel);
                    if (levels.Count > 0)
                    {
                        currLevel = (Level)levels[0];
                        currLevel.loadContent(Content);
                        currLevel.ButtonPressed = true;
                    }
                     * */
                }
                else
                {
                    //Lost
                    ScreenManager.AddScreen(new GameOverScreen(), null);
                }

            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("LevelScreen: HandleInput Exception");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];

            PlayerIndex player;
            if (pauseAction.Evaluate(input, ControllingPlayer, out player))
            {
                ScreenManager.AddScreen(new PauseMenuScreen(currLevel), ControllingPlayer);
            }
            else if (startAction.Evaluate(input, ControllingPlayer, out player))
            {
                int x = (int)currLevel.Cursor.CursorPos.X + currLevel.ShowStartX;
                int y = (int)currLevel.Cursor.CursorPos.Y + currLevel.ShowStartY;
                if (currLevel.State == LevelState.Placement && currLevel.Board[x, y].HlState != HighlightState.MOVE && currLevel.PUnits.Count > 0)
                {
                    string message = "Start level?";
                    MessageBoxScreen confirmStartMessageBox = new MessageBoxScreen(message, true);

                    confirmStartMessageBox.Accepted += ConfirmStartMessageBoxAccepted;


                    ScreenManager.AddScreen(confirmStartMessageBox, null);
                }
            }
        }

        void ConfirmStartMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < currLevel.BoardWidth; i++)
            {
                for (int j = 0; j < currLevel.BoardHeight; j++)
                {
                    currLevel.Board[i, j].HlState = HighlightState.NONE;
                }
            }
            foreach (Character p in currLevel.PUnits)
            {
                p.IsPlaced = false;
                currLevel.AllCharacters.Add(p);
            }
            foreach (Character f in currLevel.EUnits)
            {
                currLevel.AllCharacters.Add(f);
            }

            currLevel.HUD.setTargetList(currLevel.AllCharacters);

            currLevel.State = LevelState.CursorSelection;
            ScreenManager.AddScreen(new TransitionScreen("Game Start", Color.White), null);
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);

            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

            spriteBatch.Begin();

            currLevel.draw(spriteBatch);

            spriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

    }
}
