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
        public Thief(Level level, int startingLevel = 1)
        {
            charLevel = startingLevel;
            AI = new AggressiveAI(this, level);
            Texture = "player";
            name = "Thief";
            className = "Thief";

            MaxHP = STANDARD_HPMP;
            MaxMP = STANDARD_HPMP;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STANDARD_STAT;
            Armor = WEAK_STAT;
            Accuracy = STRONG_STAT;
            Dodge = STRONG_STAT;
            Magic = STANDARD_STAT;
            Resist = WEAK_STAT;

            Battle_Controller.Action actionStab = new Battle_Controller.Action("Stab", "A precise attack. Ignores enemy stats.", 
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.0, 1, 1);
            Actions[0] = actionStab;
        }
    }
}
