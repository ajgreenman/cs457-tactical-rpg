using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using GameStateManagement;
using JSA_Game.CharClasses;

namespace JSA_Game.Screens
{
    class LoadGameScreen : MenuScreen
    {
        IAsyncResult result;
        bool gameLoadRequested = false;

        public LoadGameScreen()
            : base("Load Game", false, "center", 175, 20, Color.White)
        {
            //Menu entries
            MenuEntry slot1MenuEntry = new MenuEntry("Slot 1");
            MenuEntry slot2MenuEntry = new MenuEntry("Slot 2");
            MenuEntry slot3MenuEntry = new MenuEntry("Slot 3");
            MenuEntry placeHolderEntry = new MenuEntry("");
            MenuEntry backMenuEntry = new MenuEntry("Back");
            
            //Menu event handlers
            slot1MenuEntry.Selected += slot1MenuEntrySelected;
            slot2MenuEntry.Selected += slot2MenuEntrySelected;
            slot3MenuEntry.Selected += slot3MenuEntrySelected;
            backMenuEntry.Selected += backMenuEntrySelected;

            //Add entries to menu
            MenuEntries.Add(slot1MenuEntry);
            MenuEntries.Add(slot2MenuEntry);
            MenuEntries.Add(slot3MenuEntry);
            MenuEntries.Add(placeHolderEntry);
            MenuEntries.Add(backMenuEntry);

        }


        //Event handlers

        private void slot1MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            loadGame(1);
        }

        private void slot2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            loadGame(2);
        }

        private void slot3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            loadGame(3);
        }

        private void backMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            ExitScreen();

        }


        private void loadGame(int slot)
        {
            /*
            string message = "Loading slot " + slot + " (Temp message)\n" +
                "Going to transfer to\nscreen in future";
            MessageBoxScreen loadMessageBox = new MessageBoxScreen(message, false);
            ScreenManager.AddScreen(loadMessageBox, null);
             * */

            //Loading
            if (!Guide.IsVisible && !gameLoadRequested)
            {
                gameLoadRequested = true;
                result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            }

            if ((gameLoadRequested) && (result.IsCompleted))
            {
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    System.Diagnostics.Debug.Print("Loading...");
                    doLoadGame(device, slot);
                }
            }
        }

        private void doLoadGame(StorageDevice device, int slotNum)
        {
            System.Diagnostics.Debug.Print("Loading slot " + slotNum + ".");
            // Open a storage container.
            IAsyncResult result = device.BeginOpenContainer("JSA_Game", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame" + slotNum +".sav";
            System.Diagnostics.Debug.Print("Does save file exist?");
            // Check to see whether the save exists.
            if (!container.FileExists(filename))
            {
                // If not, dispose of the container and return.
                container.Dispose();
                return;
            }
            System.Diagnostics.Debug.Print("Save file exists.  Loading...");
            // Open the file.
            Stream stream = container.OpenFile(filename, FileMode.Open);

            // Read the data from the file.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
            SaveGameData data = (SaveGameData)serializer.Deserialize(stream);

            // Close the file.
            stream.Close();

            // Dispose the container.
            container.Dispose();

            // Report the data to the console.
            //System.Diagnostics.Debug.Print("Character name:     " + data.charName);
            //System.Diagnostics.Debug.Print("Character level:     " + data.character.Level);
            for (int i = 0; i < data.characters.Length; i++)
            {
                CharacterSaveData charData = data.characters[i];
                System.Diagnostics.Debug.Print("Character Class:    " + charData.className);
                System.Diagnostics.Debug.Print("Character Accuracy:    " + charData.accuracy);
            }
            System.Diagnostics.Debug.Print("Level:    " + data.levelProgress);
            System.Diagnostics.Debug.Print("Money: " + data.money);

            //Woohoo! Now for something useful
            ArrayList loadedChars = new ArrayList();
            for (int k = 0; k < data.characters.Length; k++) 
            {
                CharacterSaveData charData = data.characters[k];
                Character c = new Character();
                if(charData.className.Equals("Thief"))
                {
                    c = new Thief(null);
                }
                else if (charData.className.Equals("Cleric"))
                {
                    c = new Cleric(null);
                }

                c.Name = charData.name;
                c.ClassName = charData.className;

                c.MaxHP = charData.maxHP;
                c.MaxMP = charData.maxMP;
                c.Strength = charData.strength;
                c.Accuracy = charData.accuracy;
                c.Armor = charData.armor;
                c.Dodge = charData.dodge;
                c.Magic = charData.magic;
                c.Resist = charData.resist;
                c.Movement = charData.movement;
                c.Level = charData.level;
                c.CurrExp = charData.currExp;

                c.Weapon = charData.weapon;
                c.Protection = charData.protection;

                //Inventory not working yet.
                //c.Inventory = 

                loadedChars.Add(c);
            }

            Game1.setPlayerChars(loadedChars);
            Game1.setCurrLevelNum(data.levelProgress);

            //go to town screen
            ScreenManager.AddScreen(new TownBackgroundScreen(), null);
            ScreenManager.AddScreen(new TownScreen(), null);


        }

        
    }
}
