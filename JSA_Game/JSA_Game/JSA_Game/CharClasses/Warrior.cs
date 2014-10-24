using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game.CharClasses
{
    class Warrior : Character
    {
        
        public Warrior(Level level)
        {
            AI = new AggressiveAI(this, level);
            Texture = "Warrior";

            MaxHP = STRONG_STAT;
            MaxMP = STANDARD_STAT;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = STRONG_STAT;
            Accuracy = STANDARD_STAT;
            Armor = STANDARD_STAT;
            Dodge = STANDARD_STAT;
            Magic = WEAK_STAT;
            Resist = WEAK_STAT;

            Battle_Controller.Action actionCripple =
                new Battle_Controller.Action("Cripple", "Cripple the target, lowering dodge and movement. Ignores enemy stats.",
                    new StatType[] {StatType.Hp, StatType.Dodge, StatType.Movement},
                    new StatType[] {StatType.Mp}, ActionType.Physical, true, false, false, 0.8, 0, 1);
            Actions[0] = actionCripple;

            Battle_Controller.Action actionBattleCry =
                new Battle_Controller.Action("Battle Cry", "Lowers enemy accuracy, strength, and magic.", 
                    new StatType[] {StatType.Accuracy, StatType.Strength, StatType.Magic},
                    new StatType[] {StatType.Mp}, ActionType.Physical, false, false, false, 1.0, 2, 3);
            Actions[1] = actionBattleCry;
        }

    }
}
