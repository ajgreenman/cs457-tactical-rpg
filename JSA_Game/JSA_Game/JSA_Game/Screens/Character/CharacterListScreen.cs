using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using JSA_Game.CharClasses;

namespace JSA_Game.Screens
{
    class CharacterListScreen : GameScreen
    {
        List<CharacterEntry> charEntries = new List<CharacterEntry>();
        int selectedEntry = 0;

        int startY = 100;

        InputAction menuUp;
        InputAction menuDown;
        InputAction menuSelect;
        InputAction menuCancel;

        public CharacterListScreen(List<Character> charList)
        {
            foreach (Character c in charList)
            {
                charEntries.Add(new CharacterEntry(c));
            }

            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            menuUp = new InputAction(null, new Keys[] { Keys.Up }, true);
            menuDown = new InputAction(null, new Keys[] { Keys.Down }, true);
            menuSelect = new InputAction(null, new Keys[] { Keys.Z }, true);
            menuCancel = new InputAction(null, new Keys[] { Keys.X }, true);
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            PlayerIndex playerIndex;

            // Move to the previous menu entry?
            if (menuUp.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                selectedEntry--;
                if (selectedEntry < 0)
                    selectedEntry = charEntries.Count - 1;
            }

            // Move to the next menu entry?
            if (menuDown.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                selectedEntry++;
                if (selectedEntry >= charEntries.Count)
                    selectedEntry = 0;
            }

            if (menuSelect.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                OnSelectEntry(selectedEntry, playerIndex);
            }
            else if (menuCancel.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                OnCancel(playerIndex);
            }
        }

        protected virtual void OnSelectEntry(int entryIndex, PlayerIndex playerIndex)
        {
            charEntries[entryIndex].OnSelectEntry(playerIndex);
        }


        /// <summary>
        /// Handler for when the user has cancelled the menu.
        /// </summary>
        protected virtual void OnCancel(PlayerIndex playerIndex)
        {
            ExitScreen();
        }


        /// <summary>
        /// Helper overload makes it easy to use OnCancel as a MenuEntry event handler.
        /// </summary>
        protected void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(e.PlayerIndex);
        }

        /// <summary>
        /// Allows the screen the chance to position the menu entries. By default
        /// all menu entries are lined up in a vertical list, centered on the screen.
        /// </summary>
        protected virtual void UpdateCharEntryLocations()
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0f, startY);




            // update each menu entry's location in turn
            for (int i = 0; i < charEntries.Count; i++)
            {
                CharacterEntry charEntry = charEntries[i];
                //position.X = ScreenManager.GraphicsDevice.Viewport.Width - charEntry.GetWidth(this) - 10;

                if (ScreenState == ScreenState.TransitionOn)
                    position.X -= transitionOffset * 256;
                else
                    position.X += transitionOffset * 512;

                // set the entry's position
                charEntry.Position = position;

                // move down for the next entry the size of this entry
                position.Y += charEntry.GetHeight(this) + 5;
            }
        }


        /// <summary>
        /// Updates the menu.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            // Update each nested MenuEntry object.
            for (int i = 0; i < charEntries.Count; i++)
            {
                bool isSelected = IsActive && (i == selectedEntry);

                charEntries[i].Update(this, isSelected, gameTime);
            }
        }


        /// <summary>
        /// Draws the menu.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            // make sure our entries are in the right place before we draw them
            UpdateCharEntryLocations();

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            //Find longest entry so boxes are same size
            int highestEntryLength = 0;
            for (int i = 0; i < charEntries.Count; i++)
            {
                int entryLength = (int)font.MeasureString(charEntries[i].Text).X;
                if (entryLength> highestEntryLength)
                {
                    highestEntryLength = entryLength;
                }
            }

            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = 0; i < charEntries.Count; i++)
            {
                CharacterEntry charEntry = charEntries[i];

                bool isSelected = IsActive && (i == selectedEntry);

                charEntry.Draw(this, isSelected, gameTime, highestEntryLength);
            }

            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // Draw the menu title centered on the screen
            //Vector2 titlePosition = new Vector2(graphics.Viewport.Width / 2, 80);
            //Vector2 titleOrigin = font.MeasureString(menuTitle) / 2;
            //Color transTitleColor = new Color(titleColor.R, titleColor.G, titleColor.B) * TransitionAlpha;
            //float titleScale = 1.25f;

            //titlePosition.Y -= transitionOffset * 100;

            //spriteBatch.DrawString(font, menuTitle, titlePosition, transTitleColor, 0,
            //                       titleOrigin, titleScale, SpriteEffects.None, 0);

            spriteBatch.End();
        }



        protected IList<CharacterEntry> CharEntries
        {
            get { return charEntries; }
        }



    }
}
