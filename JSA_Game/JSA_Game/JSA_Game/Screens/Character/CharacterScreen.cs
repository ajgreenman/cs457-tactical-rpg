using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game.Screens
{
    class CharacterScreen : MenuScreen
    {

        public CharacterScreen()
            : base("Town", false, "left", 250, 20, Color.Black)
        {
            //Menu entries
            MenuEntry talentsMenuEntry = new MenuEntry("Talents");
            MenuEntry equipmentMenuEntry = new MenuEntry("Equipment");
            MenuEntry itemsMenuEntry = new MenuEntry("Items");
            MenuEntry backMenuEntry = new MenuEntry("Back");
            

            //Menu event handlers
            talentsMenuEntry.Selected += talentsMenuEntrySelected;
            equipmentMenuEntry.Selected += equipmentMenuEntrySelected;
            itemsMenuEntry.Selected += itemsMenuEntrySelected;
            backMenuEntry.Selected += backMenuEntrySelected;

            //Add entries to menu
            MenuEntries.Add(talentsMenuEntry);
            MenuEntries.Add(equipmentMenuEntry);
            MenuEntries.Add(itemsMenuEntry);

            MenuEntries.Add(backMenuEntry);
        }


            //Event handlers

            private void talentsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                const string message = "Armory currently unavailable.";
                MessageBoxScreen armoryMessageBox = new MessageBoxScreen(message, false);
                ScreenManager.AddScreen(armoryMessageBox, null);
            }

            private void equipmentMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
               // const string message = "Tavern currently unavailable.";
               // MessageBoxScreen tavernMessageBox = new MessageBoxScreen(message, false);
               // ScreenManager.AddScreen(tavernMessageBox, null);
                ScreenManager.AddScreen(new TavernScreen(), e.PlayerIndex);

            }

            private void itemsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
            {
                const string message = "Church currently unavailable.";
                MessageBoxScreen churchMessageBox = new MessageBoxScreen(message, false);
                ScreenManager.AddScreen(churchMessageBox, null);
            }

            private void backMenuEntrySelected(object sender, PlayerIndexEventArgs e)
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
                    ScreenManager.AddScreen(new LevelScreen(levelName), e.PlayerIndex);

            }
    }
}
