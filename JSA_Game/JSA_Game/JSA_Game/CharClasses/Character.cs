using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;
using JSA_Game.AI;
using JSA_Game.Battle_Controller.StatEffect;
using JSA_Game.CharClasses;

namespace JSA_Game
{
    class Character
    {
        protected String name = "Character";

        //Default Stats
        protected const int STRONG_HPMP = 20;
        protected const int STANDARD_HPMP = 15;
        protected const int WEAK_HPMP = 10;
        protected const int STRONG_STAT = 10;
        protected const int STANDARD_STAT = 5;
        protected const int WEAK_STAT = 2;

        protected String className;

        //Status effects
        private Status[] status;

        // Actions
        private Battle_Controller.Action attack;
        private Battle_Controller.Action[] actions;

        private Boolean isEnemy;

        private Boolean moveDisabled;
        private Boolean actionDisabled;
        private int movement;
        protected int charLevel;
        private int currExp;

        //Position
        private Vector2 pos;

        //AI
        private iAI ai;

        //Image
        String texture;
        

        public Character(int startingLevel = 1)
        {
            maxHP = STANDARD_HPMP;
            maxMP = STANDARD_HPMP;
            currHp = STANDARD_STAT;
            currMp = STANDARD_STAT;
            strength = STANDARD_STAT;
            armor = STANDARD_STAT;
            accuracy = STANDARD_STAT;
            dodge = STANDARD_STAT;
            magic = STANDARD_STAT;
            resist = STANDARD_STAT;

            weapon = new Items.Weapon();
            protection = new Items.Protection();
            inventory = new Items.Consumable[4];

            status = new Status[2];


            attack = new Battle_Controller.Action();   // Defa nult attack action.
            actions = new Battle_Controller.Action[4]; // Default number of possible actions.

            movement = STANDARD_STAT;
            charLevel = startingLevel;
            currExp = 0;
            isEnemy = false;
            moveDisabled = false;
            actionDisabled = false;
            pos = new Vector2(-1, -1);
        }

        public int yieldExp()
        {
            return charLevel; 
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
            get { return armor + Protection.Armor; }
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
            get { return dodge + Protection.Dodge; }
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
            get { return resist + Protection.Resist; }
            set { resist = value; }
        }

        // Items

        private Items.Protection protection;

        public Items.Protection Protection
        {
            get { return protection; }
            set { protection = value; }
        }

        private Items.Weapon weapon;
 
        public Items.Weapon Weapon
        {
            get { return weapon; }
            set { weapon = value; }
        }

        private Items.Consumable[] inventory;

        public Items.Consumable[] Inventory
        {
            get { return inventory; }
            set { inventory = value; }
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
            get { return charLevel; }
            set { charLevel = value; }
        }
        public int CurrExp
        {
            get { return currExp; }
            set
            {
                currExp = value;
            }
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
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public float hpPercent
        {
            get { return currHp / (float)maxHP; }
        }

        public float mpPercent
        {
            get { return currMp / (float)maxMP; }
        }
        public float expPercent
        {
            get { return LevelUpManager.GetExpPercent(this); }//(currExp-EXP_VALS[charLevel - 1]) / (float)(EXP_VALS[charLevel]-EXP_VALS[charLevel-1]); }
        }
        public Status[] Status
        {
            get { return status; }
            set { status = value; }
        }
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String ClassName
        {
            get { return className; }
            set { className = value; }
        }
    }
}
