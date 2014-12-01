using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    public class Weapon : Item
    {
        protected int strength;
        protected int magic;
        protected int accuracy;

        public Weapon(String name, int worth, int strength, int magic, int accuracy) :
            base(name, worth)
        {
            this.strength = strength;
            this.magic = magic;
            this.accuracy = accuracy;
        }

        public Weapon() :
            base("Wooden Sword", 2)
        {
            strength = 2;
            magic = 2;
            accuracy = 2;
        }

        public int Strength
        {
            get { return strength; }
            set { strength = value; }
        }

        public int Magic
        {
            get { return magic; }
            set { magic = value; }
        }

        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }
    }
}
