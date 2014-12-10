using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;
using JSA_Game.Maps;

namespace JSA_Game.Screens
{
    class CharacterListScreen : GameScreen
    {
        const int MAX_SHOWING = 10;

        List<CharacterEntry> charEntries = new List<CharacterEntry>();
        int selectedEntry = 0;

        int startY = 100;
        string alignment;

        InputAction menuUp;
        InputAction menuDown;
        InputAction menuSelect;
        InputAction menuCancel;

        int highestEntryLength = 0;
        int showStartIndex = 0;

        //Reference to the level
        Level level;
        Vector2 positionToPlace;

        public CharacterListScreen(GraphicsDevice gDevice, List<Character> charList, string alignment, Level level, Vector2 pos)
        {
            foreach (Character c in charList)
            {
                charEntries.Add(new CharacterEntry(gDevice, c));
            }

            this.alignment = alignment;
            IsPopup = true;

            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            menuUp = new InputAction(null, new Keys[] { Keys.Up }, true);
            menuDown = new InputAction(null, new Keys[] { Keys.Down }, true);
            menuSelect = new InputAction(null, new Keys[] { Keys.Z }, true);
            menuCancel = new InputAction(null, new Keys[] { Keys.X }, true);

            this.level = level;
            positionToPlace = pos;
        }



        public override void HandleInput(GameTime gameTime, InputState input)
        {
            PlayerIndex playerIndex;

            // Move to the previous menu entry?
            if (menuUp.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                selectedEntry--;
                if (selectedEntry < 0)
                {
                    selectedEntry = charEntries.Count - 1;
                    if (selectedEntry > MAX_SHOWING - 1)
                    {
                        showStartIndex = selectedEntry - MAX_SHOWING + 1;
                    }  
                }
                if (selectedEntry < showStartIndex)
                    showStartIndex--;
            }

            // Move to the next menu entry?
            if (menuDown.Evaluate(input, ControllingPlayer, out playerIndex))
            {
                selectedEntry++;
                if (selectedEntry >= charEntries.Count)
                {
                    selectedEntry = 0;
                    showStartIndex = 0;
                }
                else if (selectedEntry > showStartIndex + MAX_SHOWING - 1)
                    showStartIndex++;
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
            if (level != null)
            {
                charEntries[entryIndex].OnSelectEntry(playerIndex);
                // level.SelectedChar = Game1.getPlayerChars()[entryIndex];
                //level.IsCharSelected = true;
                level.addUnit(1, Game1.getPlayerChars()[entryIndex], positionToPlace);
                System.Diagnostics.Debug.Print("Character Class: " + Game1.getPlayerChars()[entryIndex].ClassName);

                if (Game1.getPlayerChars().Count == level.PUnits.Count - level.NumPreplacedUnits)
                {
                    string message = "Start level?";
                    MessageBoxScreen confirmStartMessageBox = new MessageBoxScreen(message, true);

                    confirmStartMessageBox.Accepted += ConfirmStartMessageBoxAccepted;

                    ScreenManager.AddScreen(confirmStartMessageBox, null);
                }

                ExitScreen();
            }
        }


        void ConfirmStartMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            for (int i = 0; i < level.BoardWidth; i++)
            {
                for (int j = 0; j < level.BoardHeight; j++)
                {
                    level.Board[i, j].HlState = HighlightState.NONE;
                }
            }
            level.State = LevelState.CursorSelection;
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
        protected virtual void UpdateCharEntryLocations(SpriteFont font)
        {
            // Make the menu slide into place during transitions, using a
            // power curve to make things look more interesting (this makes
            // the movement slow down as it nears the end).
            float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

            // start at Y = 175; each X value is generated per entry
            Vector2 position = new Vector2(0, startY);

            //Find longest entry so boxes are same size
            for (int i = 0; i < charEntries.Count; i++)
            {
                int entryLength = (int)font.MeasureString(charEntries[i].Text).X;
                if (entryLength > highestEntryLength)
                {
                    highestEntryLength = entryLength;
                }
            }

            // update each menu entry's location in turn
            for (int i = showStartIndex; i < showStartIndex + MAX_SHOWING; i++)
            {
                if (i >= charEntries.Count) break;
                CharacterEntry charEntry = charEntries[i];
                if(alignment.ToLower().Equals("left"))
                    position.X = 0;
                else if(alignment.ToLower().Equals("center"))
                    position.X = (ScreenManager.GraphicsDevice.Viewport.Width-highestEntryLength)/2;
                else if(alignment.ToLower().Equals("right"))
                    position.X = ScreenManager.GraphicsDevice.Viewport.Width - highestEntryLength - 15;

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

            GraphicsDevice graphics = ScreenManager.GraphicsDevice;
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            SpriteFont font = ScreenManager.Font;

            // make sure our entries are in the right place before we draw them
            UpdateCharEntryLocations(font);


            spriteBatch.Begin();

            // Draw each menu entry in turn.
            for (int i = showStartIndex; i < showStartIndex + MAX_SHOWING; i++)
            {
                if (i >= charEntries.Count) break;
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
