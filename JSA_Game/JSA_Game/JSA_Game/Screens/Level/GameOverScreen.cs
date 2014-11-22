using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameStateManagement;

namespace JSA_Game.Screens
{
    class GameOverScreen : MenuScreen
    {

        public GameOverScreen()
            : base("Game Over!")
        {

            MenuEntry titleMenuEntry = new MenuEntry("Title Menu");
            MenuEntry exitGameMenuEntry = new MenuEntry("Exit Game");


            titleMenuEntry.Selected += titleMenuEntrySelected;
            exitGameMenuEntry.Selected += ExitGameMenuEntrySelected;

            MenuEntries.Add(titleMenuEntry);
            MenuEntries.Add(exitGameMenuEntry);
        }

        /// <summary>
        /// Event handler for when the Title Menu menu entry is selected.
        /// </summary>
        void titleMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            foreach (GameScreen screen in ScreenManager.GetScreens())
                screen.ExitScreen();
            Game1.resetLevels();
            ScreenManager.AddScreen(new MainMenuBackgroundScreen(), null);
            ScreenManager.AddScreen(new MainMenuScreen(), null);
        }


        /// <summary>
        /// Event handler for when the Exit Game menu entry is selected.
        /// </summary>
        void ExitGameMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
          ScreenManager.Game.Exit();
        }



        /// <summary>
        /// Event handler for accepting the message box
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }



    }
}
