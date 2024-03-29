﻿using System;
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
    public class Character
    {
        protected String name = "Character";

        //Default Stats
        protected const int STRONG_HPMP = 25;
        protected const int STANDARD_HPMP = 20;
        protected const int WEAK_HPMP = 15;
        protected const int STRONG_STAT = 10;
        protected const int STANDARD_STAT = 5;
        protected const int WEAK_STAT = 2;

        protected String className;

        //Status effects
        private Status[] status;

        // Actions
        private Battle_Controller.Action attack;
        private Battle_Controller.Action defend;
        private Battle_Controller.Action rest;
        private Battle_Controller.Action[] actions;

        private Boolean isEnemy;

        private Boolean moveDisabled;
        private Boolean actionDisabled;
        private int movement;
        protected int charLevel;
        private int currExp;
        Boolean isPlaced;

        //Position
        private Vector2 pos;

        //AI
        private string aiType;
        public string AiType
        {
            get { return aiType; }
            set { aiType = value; }
        }
        private iAI ai;

        //Image
        String texture;

        int currDamage;
        int currHealing; 
        bool miss;
        bool didDefend;
        bool set;
        

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
            Items.Consumable potion = new Items.Consumable("Weak Potion", 2, 1,
                new Battle_Controller.Action("Weak Potion", "A simple potion of healing.", null, new StatType[] {StatType.Mp},
                    ActionType.Spell, false, true, false, 1.0, 0, 5, 0, "swoosh", true));
            inventory[0] = potion;
            isPlaced = false;
            status = new Status[2];

            attack = new Battle_Controller.Action();     // Default attack action.
            defend = new Battle_Controller.Action(true); // Default defend action.
            rest = new Battle_Controller.Action(false);  // Default rest action.
            actions = new Battle_Controller.Action[4];   // Default number of possible actions.

            movement = STANDARD_STAT;
            charLevel = startingLevel;
            currExp = 0;
            isEnemy = false;
            moveDisabled = false;
            actionDisabled = false;
            pos = new Vector2(-1, -1);

            currDamage = -1;
            currHealing = -1;
            miss = false;
            didDefend = false;
            set = false;
        }

        public int yieldExp()
        {
            return charLevel; 
        }

        private int calculateStatusEffect(StatType type)
        {
            int amount = 0;
            foreach (Status currentStatus in status)
            {
                if (currentStatus != null)
                {
                    for (int i = 0; i < currentStatus.AffectedStats.Length; i++)
                    {
                        if (type == currentStatus.AffectedStats[i])
                        {
                            if (currentStatus.Friendly)
                            {
                                amount += currentStatus.Amount[i];
                            }
                            else
                            {
                                amount -= currentStatus.Amount[i];
                            }
                        }
                    }
                }
            }
            return amount;
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
            get { if (currHp < 0) currHp = 0; if (currHp >= maxHP) currHp = maxHP; return currHp; }
            set { currHp = value; }
        }

        private int currMp;

        public int CurrMp
        {
            get { if (currMp < 0) currMp = 0; if (currMp > maxMP) currMp = maxMP; return currMp; }
            set { currMp = value; }
        }

        private int strength;

        public int Strength
        {
            get { return (strength + Weapon.Strength + calculateStatusEffect(StatType.Strength)) < 0 ?
                0 : strength + Weapon.Strength + calculateStatusEffect(StatType.Strength); }
            set { strength = value; }
        }

        private int armor;

        public int Armor
        {
            get { return (armor + Protection.Armor + calculateStatusEffect(StatType.Armor)) < 0 ?
                0 : armor + Protection.Armor + calculateStatusEffect(StatType.Armor); }
            set { armor = value; }
        }

        private int accuracy;

        public int Accuracy
        {
            get { return (accuracy + Weapon.Accuracy + calculateStatusEffect(StatType.Accuracy)) < 0 ?
                0 : accuracy + Weapon.Accuracy + calculateStatusEffect(StatType.Accuracy);
            }
            set { accuracy = value; }
        }

        private int dodge;

        public int Dodge
        {
            get { return (dodge + Protection.Dodge + calculateStatusEffect(StatType.Dodge)) < 0 ?
                0 : dodge + Protection.Dodge + calculateStatusEffect(StatType.Dodge);
            }
            set { dodge = value; }
        }

        private int magic;

        public int Magic
        {
            get { return (magic + Weapon.Magic + calculateStatusEffect(StatType.Magic)) < 0 ?
                0 : magic + Weapon.Magic + calculateStatusEffect(StatType.Magic); }
            set { magic = value; }
        }

        private int resist;

        public int Resist
        {
            get { return (resist + Protection.Resist + calculateStatusEffect(StatType.Resist)) < 0 ?
                0 : resist + Protection.Resist + calculateStatusEffect(StatType.Resist); }
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

        public Battle_Controller.Action Defend
        {
            get { return defend; }
            set { defend = value; }
        }

        public Battle_Controller.Action Rest
        {
            get { return rest; }
            set { rest = value; }
        }

        public int CurrDamage
        {
            get { return currDamage; }
            set { currDamage = value; }
        }

        public int CurrHealing
        {
            get { return currHealing; }
            set { currHealing = value; }
        }

        public Boolean Miss
        {
            get { return miss; }
            set { miss = value; }
        }

        public Boolean Set
        {
            get { return set; }
            set { set = value; }
        }

        public Boolean DidDefend
        {
            get { return didDefend; }
            set { didDefend = value; }
        }
        public bool IsPlaced
        {
            get { return isPlaced; }
            set { isPlaced = value; }
        }
    }
}
