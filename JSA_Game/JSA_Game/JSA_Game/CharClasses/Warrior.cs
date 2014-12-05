using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;
using JSA_Game.AI;
using JSA_Game.Battle_Controller.StatEffect;

namespace JSA_Game.CharClasses
{
    class Warrior : Character
    {

        public Warrior(Level level, int startingLevel = 1)
        {
            charLevel = 1;
            AI = new AggressiveAI(this, level);
            Texture = "Warrior";
            name = "Warrior";
            className = "Warrior";

            MaxHP = STRONG_HPMP;
            MaxMP = STANDARD_HPMP;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STRONG_STAT;
            Accuracy = STANDARD_STAT;
            Armor = STANDARD_STAT;
            Dodge = STANDARD_STAT;
            Magic = WEAK_STAT;
            Resist = WEAK_STAT;
            LevelUpManager.LevelUpCharacter(this, startingLevel);

            Battle_Controller.Action actionCripple =
                new Battle_Controller.Action("Cripple", "Cripple the target, lowering dodge and movement. Ignores enemy stats.",
                    new Status("Cripple", "Cripple the target, lowering dodge and movement.  Ignores enemy stats.", 2, 
                        new StatType[] {StatType.Dodge,StatType.Movement},new int[] {4,2},"enemy", false, false),
                    new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 0.5, 5, 1, 0, "sword_attack");
            Actions[0] = actionCripple;

            Battle_Controller.Action actionBattleCry =
                new Battle_Controller.Action("Battle Cry", "Lowers enemy accuracy, strength, and magic.",
                    new Status("Fright", "Put fear into the enemy, lowering accuracy, strength, and magic.", 2,
                        new StatType[] { StatType.Accuracy, StatType.Strength, StatType.Magic }, new int[] { 3, 2, 2 }, "", false, false),
                    new StatType[] { StatType.Mp }, ActionType.Physical, false, false, false, 1.0, 4, 0, 3, "swoosh");
            Actions[1] = actionBattleCry;

            Battle_Controller.Action actionPowerfulStrike =
                new Battle_Controller.Action("Powerful Strike", "A powerful strike. Ignores enemy stats.",
                    null,
                    new StatType[] { StatType.Hp, StatType.Mp }, ActionType.Physical, true, false, false, 1.5, 8, 1, 0, "multishot");
            Actions[2] = actionPowerfulStrike;

            Battle_Controller.Action actionRage =
                new Battle_Controller.Action("Rage", "Go into a range, increasing strength but lowering accuracy.",
                    new Status("Enraged", "Blinded by rage, increasing strength but lowering accuracy.", 2,
                        new StatType[] { StatType.Strength, StatType.Accuracy }, new int[] { 5, -5 }, "", true, false),
                    new StatType[] { StatType.Hp }, ActionType.Physical, false, true, false, 1.0, 3, 0, 0, "swoosh");
            Actions[3] = actionRage;
        }

    }
}
