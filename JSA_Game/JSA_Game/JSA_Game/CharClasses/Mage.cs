using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSA_Game.Maps;
using JSA_Game.AI;
using JSA_Game.Battle_Controller.StatEffect;

namespace JSA_Game.CharClasses
{
    class Mage : Character
    {
        public Mage(Level level, int startingLevel = 1)
        {
            charLevel = 1;
            Texture = "Mage";
            name = "Mage";
            className = "Mage";
            //AI = new AggressiveAI(this, level);
            AiType = "Aggressive";

            MaxHP = STANDARD_HPMP;
            MaxMP = STRONG_HPMP;
            CurrHp = MaxHP;
            CurrMp = MaxMP;
            Strength = WEAK_STAT;
            Armor = WEAK_STAT;
            Accuracy = STANDARD_STAT;
            Dodge = STANDARD_STAT;
            Magic = STRONG_STAT;
            Resist = STANDARD_STAT;
            LevelUpManager.LevelUpCharacter(this, startingLevel);

            Battle_Controller.Action actionFireball = new Battle_Controller.Action("Fireball", "Blast the enemy with a raging fireball.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.0, 4, 5, 0, "fire_spell");
            Actions[0] = actionFireball;

            Battle_Controller.Action actionShock = new Battle_Controller.Action("Shock",
                "Call down a lightning storm, affecting all enemies in range.",
                null,
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, true, 1.0, 4, 3, 2, "shock_spell");
            Actions[1] = actionShock;

            Battle_Controller.Action actionSlow = new Battle_Controller.Action("Slow", "Slows the enemy, lowering dodge and movement.",
                new Status("Slow", "Slowed, lowering dodge and movement.", 2, level, new StatType[] { StatType.Movement, StatType.Dodge },
                    new int[] { 2, 4 }, "", false, false),
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.0, 4, 5, 0, "swoosh");
            Actions[2] = actionSlow;

            Battle_Controller.Action actionIceBolt = new Battle_Controller.Action("Ice Bolt", "Shoot a frosty bolt of ice at the enemy. May freeze the enemy",
                null,
                new StatType[] { StatType.Mp }, ActionType.Spell, false, false, false, 1.4, 6, 5, 0, "ice_spell");
            Actions[3] = actionIceBolt;
        }
    }
}