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
            // Create our menu entries.
            MenuEntry titleMenuEntry = new MenuEntry("Title Menu");
            MenuEntry exitGameMenuEntry = new MenuEntry("Exit Game");

            // Hook up menu event handlers.
            titleMenuEntry.Selected += titleMenuEntrySelected;
            exitGameMenuEntry.Selected += ExitGameMenuEntrySelected;

            // Add entries to the menu.
            MenuEntries.Add(titleMenuEntry);
            MenuEntries.Add(exitGameMenuEntry);
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
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
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        protected override void OnCancel(PlayerIndex playerIndex)
        {
            //const string message = "Are you sure you want to exit this sample?";

           // MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

           // confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

           // ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }



    }
}
