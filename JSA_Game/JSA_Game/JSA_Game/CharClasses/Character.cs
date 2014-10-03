using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game
{
    class Character
    {
        //Default Stats
        const int DEFAULT_HP = 15;
        const int DEFAULT_STATS = 5;


        protected Battle_Controller.Action[] actions;

        public Battle_Controller.Action[] Actions
        {
            get { return actions; }
            set { actions = value; }
        }

        Boolean isEnemy;
        public Boolean IsEnemy
        {
            get { return isEnemy; }
            set { isEnemy = value; }
        }

        int movement;
        int level;
        int currExp;

        //Image
        String texture;
        public String Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Character()
        {
            maxHP = DEFAULT_HP;
            maxMP = DEFAULT_STATS;
            currHp = DEFAULT_STATS;
            currHp = DEFAULT_STATS;
            strength = DEFAULT_STATS;
            armor = DEFAULT_STATS;
            accuracy = DEFAULT_STATS;
            dodge = DEFAULT_STATS;
            magic = DEFAULT_STATS;
            resist = DEFAULT_STATS;

            actions = new Battle_Controller.Action[4]; // Default number of possible actions.

            movement = DEFAULT_STATS;
            level = 1;
            isEnemy = false;
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
            get { return strength; }
            set { strength = value; }
        }

        private int armor;

        public int Armor
        {
            get { return armor; }
            set { armor = value; }
        }

        private int accuracy;

        public int Accuracy
        {
            get { return accuracy; }
            set { accuracy = value; }
        }

        private int dodge;

        public int Dodge
        {
            get { return dodge; }
            set { dodge = value; }
        }

        private int magic;

        public int Magic
        {
            get { return magic; }
            set { magic = value; }
        }

        private int resist;

        public int Resist
        {
            get { return resist; }
            set { resist = value; }
        }
    }
}
