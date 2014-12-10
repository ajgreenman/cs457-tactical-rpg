using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;
using JSA_Game.Battle_Controller.StatEffect;

namespace JSA_Game.CharClasses
{
    class Thief : Character
    {
        public Thief(int startingLevel = 1)
        {
            charLevel = 1;
            //AI = new AggressiveAI(this, level);
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
            LevelUpManager.LevelUpCharacter(this, startingLevel);

            Battle_Controller.Action actionStab = new Battle_Controller.Action("Stab", "A precise attack. Ignores enemy stats.", 
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.0, 4, 1, 0, "sword_attack", true);
            Actions[0] = actionStab;

            Battle_Controller.Action actionAmbush = new Battle_Controller.Action("Ambush", "A powerful attack. Ignores enemy stats.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.5, 7, 1, 0, "multishot", true);
            Actions[1] = actionAmbush;

            Battle_Controller.Action actionPrepare = new Battle_Controller.Action("Prepare", "Prepare to attack. Increasing stats.",
                null, // Buff stats.
                new StatType[] { StatType.Mp }, ActionType.Physical, false, true, false, 1.2, 4, 1, 0, "ice_spell", false);
            Actions[2] = actionPrepare;

            Battle_Controller.Action actionPoisonedStrike = new Battle_Controller.Action("Poisoned Strike", "Attack with a poisoned weapon.",
                new Status("Poison", "Poisons the target.", 3, new StatType[] { StatType.Hp }, new int[] { 2 }, "", false, true), // Poison effect.
                new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 1.2, 7, 1, 0, "swoosh", true);
            Actions[3] = actionPoisonedStrike;
        }
    }
}
