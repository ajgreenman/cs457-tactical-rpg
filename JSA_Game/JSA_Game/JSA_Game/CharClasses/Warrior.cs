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
                    new Status("Cripple","Cripple the target, lowering dodge and movement.  Ignores enemy stats.",2,
                        new StatType[] {StatType.Hp,StatType.Dodge,StatType.Movement},new int[] {0,4,2},"enemy",Color.White),
                    new StatType[] { StatType.Mp }, ActionType.Physical, true, false, false, 0.8, 0, 1, 0);
            Actions[0] = actionCripple;

            Battle_Controller.Action actionBattleCry =
                new Battle_Controller.Action("Battle Cry", "Lowers enemy accuracy, strength, and magic.", 
                    null,
                    new StatType[] { StatType.Mp }, ActionType.Physical, false, false, false, 1.0, 2, 3, 0);
            Actions[1] = actionBattleCry;

            Battle_Controller.Action actionPowerfulStrike =
                new Battle_Controller.Action("Powerful Strike", "A powerful strike. Ignores enemy stats.",
                    null,
                    new StatType[] { StatType.Hp }, ActionType.Physical, true, false, false, 1.5, 5, 1, 0);
            Actions[2] = actionPowerfulStrike;

            Battle_Controller.Action actionRage =
                new Battle_Controller.Action("Rage", "Go into a range, increasing strength but lowering accuracy.",
                    null, // Buff strength and debuff Accuracy
                    new StatType[] { StatType.Hp }, ActionType.Physical, false, true, false, 1.0, 4, 1, 0);
            Actions[3] = actionRage;
        }

    }
}
