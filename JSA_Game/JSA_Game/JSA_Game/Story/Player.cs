using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game.Story
{
    class Player
    {
        int currentLevel;
        String username;
        Character[] activeUnits = new Character[10];
        Character[] barracks = new Character[100];

        public Player(String username)
        {
            this.username = username;
            currentLevel = 1;
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public Character[] ActiveUnits
        {
            get { return activeUnits; }
            set { activeUnits = value; }
        }

        public Character[] Barracks
        {
            get { return barracks; }
            set { barracks = value; }
        }
    }
}
