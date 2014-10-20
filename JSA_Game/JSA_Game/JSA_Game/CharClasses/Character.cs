using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;
using JSA_Game.AI;

namespace JSA_Game
{
    class Character
    {
        //Default Stats
        protected const int STRONG_STAT = 10;
        protected const int STANDARD_STAT = 5;
        protected const int WEAK_STAT = 2;

        // Actions
        private Battle_Controller.Action attack;
        private Battle_Controller.Action[] actions;
        private int attackRange;

        private Boolean isEnemy;

        private Boolean moveDisabled;
        private Boolean actionDisabled;
        private int movement;
        private int level;
        private int currExp;

        //Position
        private Vector2 pos;

        //AI
        private iAI ai;

        //Image
        String texture;
        

        public Character()
        {
            maxHP = STANDARD_STAT;
            maxMP = STANDARD_STAT;
            currHp = STANDARD_STAT;
            currMp = STANDARD_STAT;
            strength = STANDARD_STAT;
            armor = STANDARD_STAT;
            accuracy = STANDARD_STAT;
            dodge = STANDARD_STAT;
            magic = STANDARD_STAT;
            resist = STANDARD_STAT;

            // Starting Armor
            weapon = new Items.Weapon();
            headArmor = new Items.HeadArmor();
            chestArmor = new Items.ChestArmor();
            legArmor = new Items.LegArmor();
            handArmor = new Items.HandArmor();
            feetArmor = new Items.FeetArmor();

            attack = new Battle_Controller.Action();   // Default attack action.
            actions = new Battle_Controller.Action[4]; // Default number of possible actions.

            movement = STANDARD_STAT;
            level = 1;
            currExp = 0;
            isEnemy = false;
            moveDisabled = false;
            actionDisabled = false;
            attackRange = 1;
            pos = new Vector2(-1, -1);
        }

        private int getProtectionStats(StatType type)
        {
            switch (type)
            {
                case StatType.Armor:
                    return HeadArmor.Armor + ChestArmor.Armor + LegArmor.Armor + HandArmor.Armor + FeetArmor.Armor;
                case StatType.Dodge:
                    return HeadArmor.Dodge + ChestArmor.Dodge + LegArmor.Dodge + HandArmor.Dodge + FeetArmor.Dodge;
                case StatType.Resist:
                    return HeadArmor.Resist + ChestArmor.Resist + LegArmor.Resist + HandArmor.Resist + FeetArmor.Resist;
                default:
                    return 0;
            }
        }

        //Character stats
        private int maxHP;

        public int MaxHP
        {
            get { return maxHP; }
            set { maxHP = value; }
        }

        private int maxMP;

        public int MaxMP
        {
            get { return maxMP; }
            set { maxMP = value; }
        }

        private int currHp;

        public int CurrHp
        {
            get { return currHp; }
            set { currHp = value; }
        }

        private int currMp;

        public int CurrMp
        {
            get { return currMp; }
            set { currMp = value; }
        }

        private int strength;

        public int Strength
        {
            get { return strength + Weapon.Strength; }
            set { strength = value; }
        }

        private int armor;

        public int Armor
        {
            get { return armor + getProtectionStats(StatType.Armor); }
            set { armor = value; }
        }

        private int accuracy;

        public int Accuracy
        {
            get { return accuracy + Weapon.Accuracy; }
            set { accuracy = value; }
        }

        private int dodge;

        public int Dodge
        {
            get { return dodge + getProtectionStats(StatType.Dodge); }
            set { dodge = value; }
        }

        private int magic;

        public int Magic
        {
            get { return magic + Weapon.Magic; }
            set { magic = value; }
        }

        private int resist;

        public int Resist
        {
            get { return resist + getProtectionStats(StatType.Resist); }
            set { resist = value; }
        }

        // Armor
        private Items.HeadArmor headArmor;

        public Items.HeadArmor HeadArmor
        {
            get { return headArmor; }
            set { headArmor = value; }
        }

        private Items.Protection chestArmor;

        public Items.Protection ChestArmor
        {
            get { return chestArmor; }
            set { chestArmor = value; }
        }

        private Items.Protection handArmor;

        public Items.Protection HandArmor
        {
            get { return handArmor; }
            set { handArmor = value; }
        }

        private Items.Protection legArmor;

        public Items.Protection LegArmor
        {
            get { return legArmor; }
            set { legArmor = value; }
        }

        private Items.Protection feetArmor;

        public Items.Protection FeetArmor
        {
            get { return feetArmor; }
            set { feetArmor = value; }
        }

        private Items.Weapon weapon;
 
        public Items.Weapon Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        } 

        // Actions
        public Battle_Controller.Action Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public Battle_Controller.Action[] Actions
        {
            get { return actions; }
            set { actions = value; }
        }
        public Boolean MoveDisabled
        {
            get { return moveDisabled; }
            set { moveDisabled = value; }
        }
        public Boolean ActionDisabled
        {
            get { return actionDisabled; }
            set { actionDisabled = value; }
        }
        public int Movement
        {
            get { return movement; }
            set { movement = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int CurrExp
        {
            get { return currExp; }
            set { currExp = value; }
        }
        public Boolean IsEnemy
        {
            get { return isEnemy; }
            set { isEnemy = value; }
        }
        public String Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public iAI AI
        {
            get { return ai; }
            set { ai = value; }
        }
        public int AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
        }
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
    }
}
