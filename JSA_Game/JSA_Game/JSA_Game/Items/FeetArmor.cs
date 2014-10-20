using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    class FeetArmor : Protection
    {

        public FeetArmor(String name, int worth, int armor, int dodge, int resist) :
            base(name, worth, ArmorType.Feet, armor, dodge, resist)
        {

        }

        public FeetArmor() :
            base("Leather Boots", 2, ArmorType.Feet, 0, 1, 0)
        {

        }
    }
}
