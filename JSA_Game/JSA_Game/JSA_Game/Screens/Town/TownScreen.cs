﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using Microsoft.Xna.Framework;


namespace JSA_Game.Screens
{
    class TownScreen : MenuScreen
    {

        public TownScreen()
            : base("Town", false, "left", 250, 20, Color.Black)
        {
            //Menu entries
            MenuEntry armoryMenuEntry = new MenuEntry("Armory");
            MenuEntry tavernMenuEntry = new MenuEntry("Tavern");
            MenuEntry churchMenuEntry = new MenuEntry("Church");
            MenuEntry placeHolderEntry = new MenuEntry("");
            MenuEntry manageCharactersMenuEntry = new MenuEntry("Characters");
            MenuEntry itemsMenuEntry = new MenuEntry("Items");
            MenuEntry saveGameMenuEntry = new MenuEntry("Save Game");
            MenuEntry continueMenuEntry = new MenuEntry("Continue\nAdventure");

            //Menu event handlers
            armoryMenuEntry.Selected += armoryMenuEntrySelected;
            tavernMenuEntry.Selected += tavernMenuEntrySelected;
            churchMenuEntry.Selected += churchMenuEntrySelected;
            manageCharactersMenuEntry.Selected += manageCharactersEntrySelected;
            continueMenuEntry.Selected += continueMenuEntrySelected;
            saveGameMenuEntry.Selected += saveGameEntrySelected;

            //Add entries to menu
            MenuEntries.Add(armoryMenuEntry);
            MenuEntries.Add(tavernMenuEntry);
            MenuEntries.Add(churchMenuEntry);
           
            MenuEntries.Add(placeHolderEntry);
            MenuEntries.Add(manageCharactersMenuEntry);
            MenuEntries.Add(itemsMenuEntry);

            MenuEntries.Add(placeHolderEntry);
            MenuEntries.Add(saveGameMenuEntry);
            MenuEntries.Add(continueMenuEntry);

            Sound.PlaySound("town");
        }


            //Event handlers

            private void armoryMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                const string message = "Armory currently unavailable.";
                MessageBoxScreen armoryMessageBox = new MessageBoxScreen(message, false);
                ScreenManager.AddScreen(armoryMessageBox, null);
            }

            private void tavernMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                const string message = "Tavern currently unavailable.";
                MessageBoxScreen tavernMessageBox = new MessageBoxScreen(message, false);
                ScreenManager.AddScreen(tavernMessageBox, null);
               // ScreenManager.AddScreen(new TavernScreen(), e.PlayerIndex);

            }

            private void churchMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                const string message = "Church currently unavailable.";
                MessageBoxScreen churchMessageBox = new MessageBoxScreen(message, false);
                ScreenManager.AddScreen(churchMessageBox, null);
            }

            private void manageCharactersEntrySelected(object sender, PlayerIndexEventArgs e)
            {
               // ScreenManager.AddScreen(new CharacterBackgroundScreen(), e.PlayerIndex);
               // ScreenManager.AddScreen(new CharacterScreen(), e.PlayerIndex);
                ScreenManager.AddScreen(new CharacterListScreen(ScreenManager.GraphicsDevice, Game1.getPlayerChars(),
                    "right", null, new Vector2(0,0)), e.PlayerIndex);
            }

        private void saveGameEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                ScreenManager.AddScreen(new SaveGameBackgroundScreen(), e.PlayerIndex);
                ScreenManager.AddScreen(new SaveGameScreen(), e.PlayerIndex);         
            }
          

            private void continueMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                string levelName = Game1.getNextLevelName();
                if (levelName.Equals(""))
                {
                    //No more levels left
                    const string message = "No more levels left";
                    MessageBoxScreen noMoreLevelsMessageBox = new MessageBoxScreen(message, false);
                    ScreenManager.AddScreen(noMoreLevelsMessageBox, null);
                }
                else
                    ScreenManager.AddScreen(new LevelScreen(levelName, ScreenManager), e.PlayerIndex);

            }
        
    }
}
