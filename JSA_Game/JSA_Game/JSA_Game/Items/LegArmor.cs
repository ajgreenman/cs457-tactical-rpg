using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    class LegArmor : Protection
    {

        public LegArmor(String name, int worth, int armor, int dodge, int resist) :
            base(name, worth, ArmorType.Legs, armor, dodge, resist)
        {

        }

        public LegArmor() :
            base("Leather Pants", 5, ArmorType.Legs, 1, 2, 1)
        {

        }
    }
}
