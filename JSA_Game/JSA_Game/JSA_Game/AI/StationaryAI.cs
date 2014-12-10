using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;
using JSA_Game.CharClasses;
using JSA_Game.Battle_Controller;

namespace JSA_Game.AI
{
    class StationaryAI : iAI
    {
        private Level currLevel;
        Character character;
        Vector2 targetPos;

        // AI knows what character it is and its surroundings.
        public StationaryAI(Character c, Level currentLevel)
        {
            character = c;
            currLevel = currentLevel;
            targetPos = new Vector2(-1, -1);
        }

        //Stationary AI doesn't move
        public void move(GameTime gameTime)
        {
            character.MoveDisabled = true;
        }

        //Attack only targets in range
        public void action()
        {
            //Find target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            ArrayList targetList = currLevel.PUnits.Contains(character) ? currLevel.EUnits : currLevel.PUnits;
            foreach (Character t in targetList)
            {
                dist = AStar.calcDist(character.Pos, t.Pos);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    targetPos = t.Pos;
                }

            }

            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                if (AStar.calcDist(character.Pos, targetPos) <= character.Attack.Range)
                {
                    currLevel.attackTarget(character.Pos, targetPos, character.Attack);
                }
            }
        }
        public Level CurrLevel
        {
            get { return currLevel; }
            set { currLevel = value; }
        }
    }
}
