using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game.CharClasses
{
    class Archer : Character
    {
        public Archer(Level level)
        {
            AI = new AggressiveAI(this, level);
            Texture = "Archer";

            MaxHP = STANDARD_STAT;
            MaxMP = STANDARD_STAT;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STRONG_STAT;
            Armor = WEAK_STAT;
            Accuracy = STRONG_STAT;
            Dodge = STANDARD_STAT;
            Magic = WEAK_STAT;
            Resist = STANDARD_STAT;

            Attack = new Battle_Controller.Action("Attack", "A standard ranged attack.", new StatType[] { StatType.Hp},
                new StatType[] {StatType.Mp}, ActionType.Physical, false, false, false, 1.0, 0, 5);

            Battle_Controller.Action actionLongshot = new Battle_Controller.Action("Longshot", "A long ranged shot.",
                new StatType[] { StatType.Hp },
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, false, 1.0, 1, 8);
            Actions[0] = actionLongshot;

            Battle_Controller.Action actionSnipe = new Battle_Controller.Action("Snipe", "A powerful attack. Ignores enemy stats.",
                new StatType[] { StatType.Hp },
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.1, 3, 5);
            Actions[1] = actionSnipe;

            Battle_Controller.Action actionMultishot = new Battle_Controller.Action("Multishot", "Shoot multiple targets.",
                new StatType[] { StatType.Hp },
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, true, .6, 3, 5);
            Actions[2] = actionMultishot;

            Weapon = new Items.Weapon("Wooden Bow", 2, 2, 2, 2);
        }
    }
}
