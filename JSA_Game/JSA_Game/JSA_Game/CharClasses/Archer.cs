using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;
using JSA_Game.Battle_Controller.StatEffect;

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

            Attack = new Battle_Controller.Action("Attack", "A standard ranged attack.", null,
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, false, 1.0, 0, 5, 0, "bow_attack");

            Battle_Controller.Action actionLongshot = new Battle_Controller.Action("Longshot", "A long ranged shot.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, false, 1.0, 5, 8, 0, "bow_attack");
            Actions[0] = actionLongshot;

            Battle_Controller.Action actionSnipe = new Battle_Controller.Action("Snipe", "A powerful attack. Ignores enemy stats.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.1, 7, 5, 0, "bow_attack");
            Actions[1] = actionSnipe;

            Battle_Controller.Action actionMultishot = new Battle_Controller.Action("Multishot", "Shoot multiple targets.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, false, false, true, .6, 4, 5, 2, "multishot");
            Actions[2] = actionMultishot;

            Battle_Controller.Action actionFireArrow = new Battle_Controller.Action("Fire Arrow", "Shoot a flaming arrow.",
                new Status("Burn", "Burnt, taking damage each turn.", 6,
                    new StatType[] { StatType.Hp }, new int[] { 3 }, "", false, true),
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.2, 4, 5, 0, "bow_attack");
            Actions[3] = actionFireArrow;

            Weapon = new Items.Weapon("Wooden Bow", 2, 2, 2, 2);
        }
    }
}
