using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using JSA_Game.Items;


namespace JSA_Game.Screens
{
    [Serializable]
    public class CharacterSaveData
    {
        public string name;
        public string className;

        //Stats
        public int maxHP;
        public int maxMP;
        public int strength;
        public int accuracy;
        public int armor;
        public int dodge;
        public int magic;
        public int resist;
        public int movement;
        public int level;
        public int currExp;

        //Items
        public Weapon weapon;
        public Protection protection;
        public ConsumableSaveData[] inventory = new ConsumableSaveData[4];


    }
}
