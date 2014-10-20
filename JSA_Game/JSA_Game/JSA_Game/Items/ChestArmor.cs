using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    class ChestArmor : Protection
    {
        public ChestArmor(String name, int worth, int armor, int dodge, int resist) :
            base(name, worth, ArmorType.Chest, armor, dodge, resist)
        {

        }

        public ChestArmor() :
            base("Leather Vest", 5, ArmorType.Chest, 2, 1, 2)
        {

        }
    }
}
