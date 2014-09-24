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

        //
        Boolean isEnemy;
        public Boolean IsEnemy
        {
            get { return isEnemy; }
            set { isEnemy = value; }
        }

        //Character stats
        int maxHP;
        int maxMP;
        int currHP;
        int currMP;
        int strength;
        int armor;
        int accuracy;
        int dodge;
        int magic;
        int resist;

        int movement;
        int level;

        //Positioning
        Vector2 pos;
        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

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
            currHP = DEFAULT_STATS;
            currMP = DEFAULT_STATS;
            strength = DEFAULT_STATS;
            armor = DEFAULT_STATS;
            accuracy = DEFAULT_STATS;
            dodge = DEFAULT_STATS;
            magic = DEFAULT_STATS;
            resist = DEFAULT_STATS;

            movement = DEFAULT_STATS;
            level = 1;
            isEnemy = false;

            pos = new Vector2(0, 0);
        }

        public void move(float x, float y)
        {
            pos.X = x;
            pos.Y = y;
        }
    }
}
