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
    class StationaryAI
    {

        Level currLevel;
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
        public void move()
        {  
        }

        //Attack only targets in range
        public void attack()
        {
            //Find target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            ArrayList targetList = currLevel.PUnits.Contains(character) ? currLevel.EUnits : currLevel.PUnits;
            foreach (Character t in targetList)
            {
                dist = currLevel.calcDist(character.Pos, t.Pos);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    targetPos = t.Pos;
                }

            }

            if (!targetPos.Equals(new Vector2(-1, -1)))
            {

                //Attack target
                if (currLevel.calcDist(character.Pos, targetPos) <= character.Attack.Range)
                {
                    Character target = currLevel.Board[(int)targetPos.X, (int)targetPos.Y].Occupant;

                    if (BattleController.isValidAction(character.Attack, character, character.Pos, targetPos) && currLevel.calcDist(character.Pos, targetPos) <= character.Attack.Range)
                    {
                        System.Diagnostics.Debug.Print("Target HP is " + target.CurrHp);
                        BattleController.performAction(character.Attack, character, target);
                        System.Diagnostics.Debug.Print("Target HP now is " + target.CurrHp);
                    }

                    if (target.CurrHp < 1)
                    {
                        currLevel.Board[(int)targetPos.X, (int)targetPos.Y].IsOccupied = false;
                        if (target.IsEnemy)
                            currLevel.EUnits.Remove(target);
                        else
                            currLevel.PUnits.Remove(target);
                    }
                }
            }
        }

    }
}
