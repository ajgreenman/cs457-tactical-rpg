using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    class HandArmor : Protection
    {

        public HandArmor(String name, int worth, int armor, int dodge, int resist) :
            base(name, worth, ArmorType.Hands, armor, dodge, resist)
        {

        }

        public HandArmor() :
            base("Leather Gloves", 2, ArmorType.Hands, 1, 0, 1)
        {

        }
    }
}
