using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    public class Protection : Item
    {
        protected int armor;
        protected int dodge;
        protected int resist;

        public Protection(String name, int worth, int armor, int dodge, int resist) :
            base(name, worth)
        {
            this.armor = armor;
            this.dodge = dodge;
            this.resist = resist;
        }

        public Protection() :
            base("Leather Armor", 2)
        {
            armor = 2;
            dodge = 2;
            resist = 2;
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
