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
        public Archer(Level level, int startingLevel)
        {
            charLevel = 1;
            AI = new AggressiveAI(this, level);
            Texture = "Archer";
            name = "Archer";
            className = "Archer";

            MaxHP = STANDARD_HPMP;
            MaxMP = STANDARD_HPMP;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STRONG_STAT;
            Armor = WEAK_STAT;
            Accuracy = STRONG_STAT;
            Dodge = STANDARD_STAT;
            Magic = WEAK_STAT;
            Resist = STANDARD_STAT;
            LevelUpManager.LevelUpCharacter(this, startingLevel);

            //Attack = new Battle_Controller.Action("Attack", "A standard ranged attack.", null,
            //    new StatType[] {StatType.Mp}, ActionType.Physical, false, false, false, 1.0, 0, 5, 0);

            Attack = new Battle_Controller.Action("Attack", "A standard ranged attack.", null,
                new StatType[] {StatType.Mp}, ActionType.Physical, false, false, true, 1.0, 0, 5, 1);

            Battle_Controller.Action actionLongshot = new Battle_Controller.Action("Longshot", "A long ranged shot.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, false, 1.0, 3, 8, 0);
            Actions[0] = actionLongshot;

            Battle_Controller.Action actionSnipe = new Battle_Controller.Action("Snipe", "A powerful attack. Ignores enemy stats.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.1, 5, 5, 0);
            Actions[1] = actionSnipe;

            Battle_Controller.Action actionMultishot = new Battle_Controller.Action("Multishot", "Shoot multiple targets.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, true, .6, 4, 5, 2);
            Actions[2] = actionMultishot;

            Battle_Controller.Action actionFireArrow = new Battle_Controller.Action("Fire Arrow", "Shoot a flaming arrow.",
                null, // Add a burn status?
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.2, 4, 5, 0);
            Actions[3] = actionFireArrow;

            Weapon = new Items.Weapon("Wooden Bow", 2, 2, 2, 2);
        }
    }
}
