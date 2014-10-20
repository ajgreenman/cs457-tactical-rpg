using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Items
{
    class HeadArmor : Protection
    {
        public HeadArmor(String name, int worth, int armor, int dodge, int resist) :
            base(name, worth, ArmorType.Head, armor, dodge, resist)
        {

        }

        public HeadArmor() :
            base("Leather Cap", 3, ArmorType.Head, 1, 1, 1)
        {

        }
    }
}
