using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    abstract class Armor : Item
    {
        protected ArmorType type;
        protected int armor;
        protected int dodge;
        protected int resist;

        public Armor(String name, int worth, ArmorType type, int armor, int dodge, int resist) :
            base(name, worth)
        {
            this.type = type;
            this.armor = armor;
            this.dodge = dodge;
            this.resist = resist;
        }

        public ArmorType Type
        {
            get { return type; }
            set { type = value; }
        }

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        public int Dodge
        {
            get { return dodge; }
            set { dodge = value; }
        }

        public int Resist
        {
            get { return resist; }
            set { resist = value; }
        }
    }
}
