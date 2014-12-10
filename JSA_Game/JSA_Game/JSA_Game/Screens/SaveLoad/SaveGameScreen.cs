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
using JSA_Game.Items;

namespace JSA_Game.Screens
{
    class SaveGameScreen : MenuScreen
    {
        //Saving variables
        IAsyncResult result;
        bool gameSaveRequested = false;

        public SaveGameScreen()
            : base("Save Game", false, "center", 250, 20, Color.White)
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
            saveGame(1);
        }

        private void slot2MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            saveGame(2);
        }

        private void slot3MenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            saveGame(3);
        }

        private void backMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            foreach (GameScreen screen in ScreenManager.GetScreens())
                screen.ExitScreen();
            ScreenManager.AddScreen(new TownBackgroundScreen(), null);
            ScreenManager.AddScreen(new TownScreen(), null);

        }

        private void saveGame(int slot)
        {

            string message = "Saved to slot " + slot + " (Somewhat)";
            MessageBoxScreen saveMessageBox = new MessageBoxScreen(message, false);
            ScreenManager.AddScreen(saveMessageBox, null);

            //Saving
            if (!Guide.IsVisible && !gameSaveRequested)
            {
                gameSaveRequested = true;
                result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            }

            if ((gameSaveRequested) && (result.IsCompleted))
            {
                gameSaveRequested = false;
                StorageDevice device = StorageDevice.EndShowSelector(result);
                if (device != null && device.IsConnected)
                {
                    System.Diagnostics.Debug.Print("Saving...");
                    doSaveGame(device, slot);
                    //test loading to see if saving correctly
                    doLoadGame(device, slot);
                    

                }
            }
        }
 

        private void doSaveGame(StorageDevice device, int slotNum)
        {
            SaveGameData data = new SaveGameData();
            List<Character> charList = Game1.getPlayerChars();
            CharacterSaveData[] playerChars = new CharacterSaveData[charList.Count];
            int i = 0;
            foreach (Character c in charList)
            {
                System.Diagnostics.Debug.Print("Converting a " + c.ClassName + " into saved data");
                CharacterSaveData charData = new CharacterSaveData();
                charData.name = c.Name;
                charData.className = c.ClassName;
                charData.maxHP = c.MaxHP;
                charData.maxMP = c.MaxMP;
                charData.strength = c.Strength;
                charData.accuracy = c.Accuracy;
                charData.armor = c.Armor;
                charData.dodge = c.Dodge;
                charData.magic = c.Magic;
                charData.resist = c.Resist;
                charData.movement = c.Movement;
                charData.level = c.Level;
                charData.currExp = c.CurrExp;
                charData.weapon = c.Weapon;
                charData.protection = c.Protection;

                for (int j = 0; j < c.Inventory.Length; j++ )
                {
                    if (c.Inventory[j] == null) break;
                    ConsumableSaveData consData = new ConsumableSaveData();
                    consData.itemName = c.Inventory[j].Name;
                    charData.inventory[j] = consData;
                    
                }
                

                playerChars[i] = charData;
                i++;
            }

            //data.character = c;
            //data.playerChars = Game1.getPlayerChars();
            //data.inventory = 
            data.characters = playerChars;
            data.levelProgress = Game1.getCurrLevelNum();
            data.money = 5;  //Change later of course

            // Open a storage container.
            IAsyncResult result = device.BeginOpenContainer("JSA_Game_Saves", null, null);

            // Wait for the WaitHandle to become signaled.
            result.AsyncWaitHandle.WaitOne();

            StorageContainer container = device.EndOpenContainer(result);

            // Close the wait handle.
            result.AsyncWaitHandle.Close();

            string filename = "savegame" + slotNum +".sav";

            // Check to see whether the save exists.
            if (container.FileExists(filename))
                // Delete it so that we can create one fresh.
                container.DeleteFile(filename);

            // Create the file.
            Stream stream = container.CreateFile(filename);

            //Add extra types SaveGameData uses
            Type[] extraTypes = new Type[2];
            extraTypes[0] = typeof(CharacterSaveData);
            extraTypes[1] = typeof(ConsumableSaveData);

            // Convert the object to XML data and put it in the stream.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData), extraTypes);
            serializer.Serialize(stream, data);

            // Close the file.
            stream.Close();

            // Dispose the container, to commit changes.
            container.Dispose();
            System.Diagnostics.Debug.Print("Saving to slot " + slotNum + " complete.");
        }


        private static void doLoadGame(StorageDevice device, int slotNum)
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
        }



    }
}
