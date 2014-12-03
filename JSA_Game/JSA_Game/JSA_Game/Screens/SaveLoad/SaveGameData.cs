using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Screens
{
    [Serializable]
    public class SaveGameData
    {
        //For saving/loading
        //public string charName;
        //public Character character;
        //public ArrayList playerChars;
        //public ArrayList inventory;

        
        public CharacterSaveData[] characters = new CharacterSaveData[10];
        //public CharacterSaveData character;
        public int levelProgress;
        public int money;
    }
}
