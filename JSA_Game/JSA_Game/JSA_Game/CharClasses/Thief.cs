using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game.CharClasses
{
    class Thief : Character
    {
        public Thief(Level level)
        {
            AI = new AggressiveAI(this, level);
            Texture = "player";

            MaxHP = STANDARD_STAT;
            MaxMP = STANDARD_STAT;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STANDARD_STAT;
            Armor = WEAK_STAT;
            Accuracy = STRONG_STAT;
            Dodge = STRONG_STAT;
            Magic = STANDARD_STAT;
            Resist = WEAK_STAT;

            Battle_Controller.Action actionStab = new Battle_Controller.Action("Stab", "A precise attack. Ignores enemy stats.", 
                new StatType[] { StatType.Hp },
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.0, 1, 1);
            Actions[0] = actionStab;
        }
    }
}
